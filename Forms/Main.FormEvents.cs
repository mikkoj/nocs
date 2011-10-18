using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using Google.Documents;

using Nocs.Models;
using Nocs.Properties;
using Nocs.Helpers;


namespace Nocs.Forms
{
    public partial class Main
    {
        private void MainFormLoad(object sender, EventArgs e)
        {
            // set window location, window size, editor's font, editor's word wrap setting,
            // encryption key, AutoSave and window's "always on top" option based on user settings

            if (Settings.Default.WindowLocation.X != 0 || Settings.Default.WindowLocation.Y != 0)
            {
                Location = Settings.Default.WindowLocation;
            }

            if (Location.X < 0 || Location.Y < 0)
            {
                Trace.WriteLine(string.Format("{0} - Location out of bounds: X: {1} - Y: {2}",
                                              DateTime.Now, Location.X, Location.Y));
                CenterToScreen();
                Settings.Default.WindowLocation = new Point(0, 0);
            }
            Size = Settings.Default.WindowSize;


            menuWordWrap.Checked = Settings.Default.WordWrap;
            TopMost = Settings.Default.AlwaysOnTop;
            menuAlwaysOnTop.Checked = Settings.Default.AlwaysOnTop;

            // ------------------------------------------------------------------------------------------------------

            // if Google login info hasn't been saved, let's show the Login form
            if (string.IsNullOrEmpty(Settings.Default.GoogleUsername) || string.IsNullOrEmpty(Settings.Default.GooglePassword))
            {
                var nocsLogin = new Login();
                nocsLogin.ShowDialog();

                // make sure user was validated
                if (!string.IsNullOrEmpty(NocsService.Username) && !string.IsNullOrEmpty(NocsService.Password))
                {
                    RetrieveDocuments();

                    // reset help variable
                    NocsService.AccountChanged = false;
                }
            }

            // if user has already used the application with proper Google credentials, we'll start the Google DocumentsList service
            else
            {
                // copy the account info from settings to NocsService
                NocsService.Username = Settings.Default.GoogleUsername;
                NocsService.Password = Tools.Decrypt(Settings.Default.GooglePassword);

                // if Google login info hasn't been saved, let's show the Login form
                if (string.IsNullOrEmpty(NocsService.Password))
                {
                    var nocsLogin = new Login();
                    nocsLogin.ShowDialog();

                    // make sure user was validated
                    if (!string.IsNullOrEmpty(NocsService.Username) && !string.IsNullOrEmpty(NocsService.Password))
                    {
                        RetrieveDocuments();

                        // reset help variable
                        NocsService.AccountChanged = false;
                    }
                }
                else
                {
                    // inform user that we are starting the service
                    Status(StatusType.Authorize, "Authenticating with Google..");

                    // disable menu options for changing the Google Account, for browsing Google Docs
                    // and for saving while items are being retrieved
                    menuGoogleAccount.Enabled = false;
                    menuBrowse.Enabled = false;
                    menuSave.Enabled = false;

                    // run the backgroundworker for starting the service
                    BgWorkerStartService.RunWorkerAsync(false);
                }
            }
        }


        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            // for pinned documents
            var pinnedDocuments = new List<string>();

            // let's first find out if any of the open tabs are unsaved
            foreach (Noc tab in tabs.TabPages)
            {
                // let's also collect potential pinned documents
                if (tab.Pinned)
                {
                    pinnedDocuments.Add(tab.Document.ResourceId);
                }

                if (tab.ContentHasChanged && NocsService.UserIsAuthenticated())
                {
                    // let's first stop all processing
                    if (!_synchronizer.SyncStopped || _autoFetchAllEntriesTimer.Enabled)
                    {
                        _synchronizer.Stop();
                        _autoFetchAllEntriesTimer.Stop();
                    }

                    // ..and display a msg asking the user to save changes or abort
                    var result = MessageBox.Show("Do you want to save the changes to " + tab.Text + "?", "Nocs", MessageBoxButtons.YesNoCancel);

                    if (result == DialogResult.Yes)
                    {
                        // let's cancel the FormClosing-event
                        e.Cancel = true;

                        // let's stop here if we're already saving the current tab
                        if (tab.SaveWorkerIsBusy())
                        {
                            // re-enable all processing and exit
                            _synchronizer.Start();
                            _autoFetchAllEntriesTimer.Start();
                            return;
                        }

                        // if current file is not new, let's just save it
                        if (!tab.Document.IsDraft)
                        {
                            Status(StatusType.Save, "Saving...");
                            tab.Save();
                        }
                        else
                        {
                            // ask for a name
                            var saveResponse = SaveDialog.SaveDialogBox("Enter a name for your file:", "Save", "Untitled");

                            if (!string.IsNullOrEmpty(saveResponse.DocumentName))
                            {
                                Status(StatusType.Save, "Saving...");
                                tab.SaveAs(saveResponse);
                            }
                        }

                        // re-enable all processing and exit
                        _synchronizer.Start();
                        _autoFetchAllEntriesTimer.Start();
                        return;
                    }

                    if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        // re-enable all processing
                        _synchronizer.Start();
                        _autoFetchAllEntriesTimer.Start();
                        return;
                    }
                }
            }

            // save all current settings just before closing application
            Settings.Default.WindowLocation = Location;
            Settings.Default.WindowSize = WindowState == FormWindowState.Normal ? Size : RestoreBounds.Size;

            // let's also save pinned documents
            if (pinnedDocuments.Any())
            {
                var pinnedDocumentsJoined = String.Join(";", pinnedDocuments.Where(s => !string.IsNullOrEmpty(s)));
                Settings.Default.PinnedDocuments = pinnedDocumentsJoined;
            }
            else
            {
                Settings.Default.PinnedDocuments = string.Empty;
            }

            Settings.Default.Save();

            // let's flush the trace content to the file
            Trace.Flush();
        }



        #region Browse Events

        /// <summary>
        /// Will launch whenever the Browse-form wants to load a new document.
        /// Will open up a new inactive tab (Noc), load that Noc's content in the background
        /// and then update the noc.
        /// </summary>
        /// <param name="document">Document to be loaded.</param>
        /// <param name="pinned">Determines whether document to be added is a pinned document.</param>
        void BrowseAddDocumentToMainForm(Document document, bool pinned = false)
        {
            // let's first see if the document is already open
            for (var i = 0; i < tabs.TabCount; i++)
            {
                var t = tabs.TabPages[i] as Noc;
                if (t != null && !t.Document.IsDraft && t.Document.ResourceId == document.ResourceId)
                {
                    tabs.SelectedIndex = i;
                    return;
                }
            }

            // if there is a single Untitled tab with no content open, let's close it
            if (tabs.TabCount > 0)
            {
                var firstTab = tabs.TabPages[0] as Noc;
                if (firstTab != null && !firstTab.ContentHasChanged && firstTab.Document.IsDraft)
                {
                    CloseTab(0);
                }
            }

            // let's add an inactive tab with a title to the tabControl
            AddInactiveNoc(document, pinned);

            // let's then fetch the content for the document

            // 1. let's create a new bgWorker
            var worker = new System.ComponentModel.BackgroundWorker();
            worker.DoWork += BgWorkerLoadDocumentContent_DoWork;
            worker.RunWorkerCompleted += BgWorkerLoadDocumentContent_Completed;

            // 2. let's increase the number of current workers for syncing purposes
            lock (_threadLock)
            {
                _contentUpdaterWorkers++;
            }

            // 3. let's update status and run our worker
            Status(StatusType.Retrieve, "Retrieving content...");
            worker.RunWorkerAsync(document);
        }


        /// <summary>
        /// Will launch whenever the Browse-form wants to notify Main-form for a document rename.
        /// </summary>
        /// <param name="documentId">A documentId for the document that has been renamed.</param>
        void BrowseDocumentRenamed(string documentId)
        {
            // let's check if the deleted document is open in a tab
            foreach (var t in tabs.TabPages)
            {
                var tab = t as Noc;
                if (tab != null && !tab.Document.IsDraft && tab.Document.ResourceId == documentId)
                {
                    Debug.WriteLine(DateTime.Now + " - Main: found a renamed document open in a tab - updating its state");
                    // found the renamed document open in a tab - let's update its state
                    tab.Document = NocsService.AllDocuments[documentId];
                    tab.Text = tab.Document.Title;
                    SetMainTitle(tab.Document.Title);
                }
            }
        }


        /// <summary>
        /// Will launch whenever the Browse-form wants to notify Main-form for document deletion.
        /// </summary>
        /// <param name="documentId">A documentId for the document that has been deleted.</param>
        void BrowseDocumentDeleted(string documentId)
        {
            // let's check if the deleted document is open in a tab
            for (var i = 0; i < tabs.TabPages.Count; i++)
            {
                var tab = tabs.TabPages[i] as Noc;
                if (tab != null && !tab.Document.IsDraft && tab.Document.ResourceId == documentId)
                {
                    Debug.WriteLine(DateTime.Now + " - Main: found a deleted document open in a tab - removing it");
                    tabs.TabPages.RemoveAt(i);

                    // if no more tabs left, let's reset the title to "Nocs" and disable saving to file
                    if (tabs.TabPages.Count == 0)
                    {
                        SetMainTitle(string.Empty);
                        lblCaretPosition.Text = string.Empty;
                        menuSaveFileAs.Enabled = false;
                    }
                }
            }
        }

        #endregion


        #region Other Events

        private void SynchronizerErrorWhileSyncing(SyncResult result)
        {
            if (!string.IsNullOrEmpty(result.Error))
            {
                Trace.WriteLine(string.Format("{0} - Main: error while syncing - type: {1} - message: {2}", DateTime.Now,
                                              result.Job.Type, result.Error));

                var error = result.Error.ToLowerInvariant();

                if (error.Contains("authorization") || error.Contains("token expired"))
                {
                    TokenExpiredWhileSaving();
                }
                else if (error.Contains("internet down") ||
                         error.Contains("connection timed out") ||
                         error.Contains("remote name could not be resolved") ||
                         error.Contains("unable to connect to the remote server"))
                {
                    MessageBox.Show(new Form { TopMost = true },
                                    "Can't connect to internet. Make sure you're online and try again.",
                                    "Can't connect to internet",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (error.Contains("resource not found"))
                {
                    MessageBox.Show(new Form { TopMost = true },
                                    "A resource couldn't be found while attempting an update. Please investigate nocs.log and report any errors at http://nocs.googlecode.com/. Thanks!",
                                    "Resource not found",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(new Form { TopMost = true },
                                    "Error occurred while syncing, please close Nocs, inspect nocs.log and report found errors to http://nocs.googlecode.com/",
                                    "Error while syncing",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            Trace.Flush();
        }


        /// <summary>
        /// Invoked when a Noc notifies the Main form of an expired token.
        /// </summary>
        private void TokenExpiredWhileSaving()
        {
            Trace.WriteLine(DateTime.Now + " - Token expired: Reauthenticating with Google");
            Status(StatusType.Authorize, "Token expired: Reauthenticating with Google..");

            // disable menu options for changing the Google Account, for browsing Google Docs
            // and for saving while items are being retrieved
            menuGoogleAccount.Enabled = false;
            menuBrowse.Enabled = false;
            menuSave.Enabled = false;

            // run the backgroundworker for starting the service
            BgWorkerStartService.RunWorkerAsync(true);
        }


        /// <summary>
        /// Invoked when a Noc notifies the Main form of a title change.
        /// </summary>
        /// <param name="documentId"></param>
        private void NocTitleChanged(string documentId)
        {
            if (tabs.TabPages.Count == 0)
            {
                return;
            }

            var currentTab = tabs.SelectedTab as Noc;

            // let's make sure the user hasn't changed the tab while it was saving
            if (currentTab != null)
            {
                if (string.IsNullOrEmpty(documentId) ||
                    currentTab.Document.IsDraft ||
                    currentTab.Document.ResourceId == documentId)
                {
                    SetMainTitle(currentTab.Text);
                }
            }
        }


        /// <summary>
        /// Invoked when a Noc notifies the Main form of a change in caret position.
        /// </summary>
        /// <param name="caretPosition">Current position</param>
        private void NocCaretPositionChanged(string caretPosition)
        {
            // let's make sure there's atleast one tab available
            if (tabs.TabPages.Count > 0)
            {
                lblCaretPosition.Text = caretPosition;
            }
        }


        /// <summary>
        /// Will be called every 15 minutes in order to keep documents checked.
        /// </summary>
        private void AutoFetchAllEntriesTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (!BgWorkerGetAllItems.IsBusy && !_synchronizer.IsJobAlreadyQueued(AutoFetchId))
            {
                var autofetchJob = new SyncJob
                {
                    Id = AutoFetchId,
                    ErrorsOccurred = 0,
                    Type = SyncJobType.UpdateAllEntries
                };
                _synchronizer.AddJobToQueue(autofetchJob);
            }
        }


        /// <summary>
        /// Will be called whenever an autofetch for all documents finishes.
        /// </summary>
        private void AutoFetchAllEntriesFinished(SyncResult result)
        {
            // we'll have to check if any of the open tabs were deleted
            RemoveTabsNotFoundInAllDocuments();
        }

        private void RemoveTabsNotFoundInAllDocuments()
        {
            for (var i = 0; i < tabs.TabCount; i++)
            {
                var tab = tabs.TabPages[i] as Noc;
                if (tab != null && !tab.Document.IsDraft && !NocsService.AllDocuments.ContainsKey(tab.Document.ResourceId))
                {
                    // tab document isn't a draft (untitled)
                    // AND we couldn't find the documentId in AllDocuments after a background-"GetAllItems"
                    // -> we have to remove the document from tabs completely (in a thread-safe way)
                    Debug.WriteLine(DateTime.Now + " - Main: removing an open tab of a document that was removed in Google Docs: " + tab.Document.Title);
                    MainFormThreadSafeDelegate removeTab = RemoveTabThreadSafe;
                    Invoke(removeTab, i);
                }
            }
        }

        private void RemoveTabThreadSafe(object value)
        {
            int tabIndex;
            if (int.TryParse(value.ToString(), out tabIndex))
            {
                // if selected tab will be removed, let's jump to the one on its left
                var selectedRemoved = false;
                var newSelectedIndex = 0;
                if (tabIndex == tabs.SelectedIndex)
                {
                    selectedRemoved = true;
                    newSelectedIndex = tabIndex - 1;
                    if (newSelectedIndex < 0)
                    {
                        newSelectedIndex = 0;
                    }
                }

                // let's get the TabPage for dispose/deactivation purposes
                var tab = tabs.TabPages[tabIndex] as Noc;

                // if get here, we can remove the tab
                tabs.TabPages.RemoveAt(tabIndex);

                // let's handle disposing
                if (tab != null)
                {
                    tab.Deactivate();
                    SettingsChanged -= tab.SettingsChanged;
                    tab.Dispose();
                }

                // let's handle the selection changes
                if (selectedRemoved)
                {
                    tabs.SelectedIndex = newSelectedIndex;
                }

                // if no more tabs left, let's reset the title to "Nocs" and disable saving to file
                if (tabs.TabPages.Count == 0)
                {
                    SetMainTitle(string.Empty);
                    lblCaretPosition.Text = string.Empty;
                    menuSaveFileAs.Enabled = false;
                }
            }
        }


        /// <summary>
        /// Will track key combinations in order to move between tabs.
        /// </summary>
        private void MainKeyDown(object sender, KeyEventArgs e)
        {
            // ctrl + w was pressed, let's close the current tab
            if (e.Control && e.KeyCode == Keys.W)
            {
                if (tabs.TabCount > 0)
                {
                    CloseTab(tabs.SelectedIndex);
                }
            }

            // no need to continue if there's no more than 1 tab
            if (tabs.TabPages.Count <= 1)
            {
                return;
            }

            // ctrl + right was pressed, let's move to right in tabs
            if (e.Alt && e.KeyCode == Keys.Right)
            {
                var newSelectedIndex = tabs.SelectedIndex + 1;

                // let's make sure there's enough tabs on the rightside of the current selectedIndex
                if (tabs.TabPages.Count > newSelectedIndex)
                {
                    tabs.SelectedIndex = newSelectedIndex;
                }
                else
                {
                    // no more tabs on the right, let's move back to start
                    tabs.SelectedIndex = 0;
                }
            }


            // ctrl + left was pressed, let's move to left in tabs
            if (e.Alt && e.KeyCode == Keys.Left)
            {
                var newSelectedIndex = tabs.SelectedIndex - 1;

                // let's make sure there's a tab on the leftside of the current selectedIndex
                if (newSelectedIndex >= 0)
                {
                    tabs.SelectedIndex = newSelectedIndex;
                }
                else
                {
                    // no more tabs on the left, let's move to the last tab
                    tabs.SelectedIndex = (tabs.TabPages.Count - 1);
                }
            }
        }


        private void TabsKeyDown(object sender, KeyEventArgs e)
        {
            // ctrl + o was pressed, let's open the browse screen
            if (menuBrowse.Enabled && e.Control && e.KeyCode == Keys.O)
            {
                MenuBrowseClick(null, null);
            }
        }


        #endregion


        /// <summary>
        /// Will update the statusbar with given info.
        /// </summary>
        /// <param name="statusType">Type of the status update.</param>
        /// <param name="statusObject">A potential status message/object.</param>
        private void Status(StatusType statusType, object statusObject)
        {
            // if the statusbar is minimal or hidden altogether, let's just leave
            if (Settings.Default.StatusBarStyle == "Hidden")
            {
                return;
            }

            if (Settings.Default.StatusBarStyle == "Minimal")
            {
                lblStatus.Visible = false;
                pictStatusMinimal.Visible = true;
            }
            else
            {
                lblStatus.Visible = true;
                pictStatusMinimal.Visible = false;
            }

            // if status type is 'Reset', let's clear both the label and hide the loader
            if (statusType == StatusType.Reset)
            {
                lblStatus.Text = string.Empty;
                pictStatusMinimal.Visible = false;
                return;
            }

            if (statusType == StatusType.ContentUpdateError)
            {
                var error = statusObject.ToString();
                if (error.ToLower().Contains("connection timed out"))
                {
                    lblStatus.Text = "Couldn't get document content, connected to Internet?";
                }
                else
                {
                    lblStatus.Text = error;
                }
                lblStatus.Visible = true;
                lblStatus.ForeColor = Color.FromArgb(((((192)))), ((((0)))), ((((0)))));
                pictStatusMinimal.Visible = false;
                return;
            }

            // -------------------------------------------------------------------------

            // let's change the status style based on the type of the status update
            switch (statusType)
            {
                case StatusType.Authorize:
                    {
                        if (Settings.Default.StatusBarStyle == "Minimal")
                        {
                            pictStatusMinimal.Visible = true;
                        }
                        else
                        {
                            lblStatus.ForeColor = Color.FromArgb(((((0)))), ((((81)))), ((((0)))));
                        }
                        break;
                    }

                case StatusType.Info:
                    {
                        lblStatus.ForeColor = Color.FromArgb(((((0)))), ((((81)))), ((((0)))));
                        break;
                    }
                case StatusType.Error:
                    {
                        lblStatus.Visible = true;
                        lblStatus.ForeColor = Color.FromArgb(((((192)))), ((((0)))), ((((0)))));
                        pictStatusMinimal.Visible = false;

                        // we'll cut too long errors
                        if (statusObject.ToString().Length > 90)
                        {
                            statusObject = statusObject.ToString().Substring(0, 85) + "..";
                        }

                        break;
                    }

                case StatusType.Save:
                    {
                        if (Settings.Default.StatusBarStyle == "Minimal")
                        {
                            pictStatusMinimal.Visible = true;
                        }
                        else
                        {
                            lblStatus.ForeColor = Color.DarkBlue;
                        }
                        break;
                    }

                case StatusType.Retrieve:
                    {
                        if (Settings.Default.StatusBarStyle == "Minimal")
                        {
                            pictStatusMinimal.Visible = true;
                        }
                        else
                        {
                            lblStatus.ForeColor = Color.DarkBlue;
                        }
                        break;
                    }

                case StatusType.Sync:
                    {
                        if (Settings.Default.StatusBarStyle == "Minimal")
                        {
                            pictStatusMinimal.Visible = true;
                        }
                        else
                        {
                            lblStatus.ForeColor = Color.DarkGreen;
                        }

                        break;
                    }

                default:
                    break;
            }

            // -------------------------------------------------------------------------

            // let's check for statusbar styles in general and update status
            if (Settings.Default.StatusBarStyle == "Minimal" && statusType != StatusType.Error && statusType != StatusType.ContentUpdateError)
            {
                lblStatus.Visible = false;
            }
            else
            {
                if (Settings.Default.StatusBarStyle == "TextBlackAndWhite")
                {
                    lblStatus.ForeColor = Color.Black;
                    lblStatus.BackColor = SystemColors.Control;
                }

                lblStatus.Text = statusObject.ToString();
            }
        }


        /// <summary>
        /// Closes a tab with a given index.
        /// </summary>
        /// <param name="tabIndex">Index for the tab to be closed</param>
        private void CloseTab(int tabIndex)
        {
            // let's makes sure the index is valid
            if (tabIndex < 0 || tabIndex > (tabs.TabCount - 1))
            {
                return;
            }

            var tabToBeClosed = tabs.TabPages[tabIndex] as Noc;
            if (tabToBeClosed != null && tabToBeClosed.ContentHasChanged && NocsService.UserIsAuthenticated())
            {
                // let's first stop all processing
                _synchronizer.Stop();
                _autoFetchAllEntriesTimer.Stop();

                var result = MessageBox.Show("Do you want to save the changes to " + tabToBeClosed.Text + "?", "Nocs", MessageBoxButtons.YesNoCancel);

                // display a msg asking the user to save changes or abort
                if (result == DialogResult.Yes)
                {
                    // let's stop here if we're already saving the current tab
                    if (tabToBeClosed.SaveWorkerIsBusy())
                    {
                        // re-enable all processing
                        _synchronizer.Start();
                        _autoFetchAllEntriesTimer.Start();
                        return;
                    }

                    // if current file is not new, let's just save it
                    if (!tabToBeClosed.Document.IsDraft)
                    {
                        Status(StatusType.Save, "Saving...");
                        tabToBeClosed.Save();
                    }
                    else
                    {
                        // ask for a name
                        var saveResponse = SaveDialog.SaveDialogBox("Enter a name for your file:", "Save", "Untitled");

                        if (!string.IsNullOrEmpty(saveResponse.DocumentName))
                        {
                            Status(StatusType.Save, "Saving...");
                            tabToBeClosed.SaveAs(saveResponse);
                        }
                    }

                    // re-enable all processing
                    _synchronizer.Start();
                    _autoFetchAllEntriesTimer.Start();
                    return;
                }

                if (result == DialogResult.Cancel)
                {
                    // re-enable all processing
                    _synchronizer.Start();
                    _autoFetchAllEntriesTimer.Start();
                    return;
                }

            }

            // if selected tab will be removed, let's jump to the one on its left
            var selectedRemoved = false;
            var newSelectedIndex = 0;
            if (tabIndex == tabs.SelectedIndex)
            {
                selectedRemoved = true;
                newSelectedIndex = tabIndex - 1;
                if (newSelectedIndex < 0)
                {
                    newSelectedIndex = 0;
                }
            }

            // let's handle the selection changes
            if (selectedRemoved)
            {
                tabs.SelectedIndex = newSelectedIndex;
            }

            // let's get the TabPage for dispose/deactivation purposes
            var tab = tabs.TabPages[tabIndex] as Noc;

            // if get here, we can remove the tab
            tabs.TabPages.RemoveAt(tabIndex);

            // let's handle disposing
            if (tab != null)
            {
                tab.Deactivate();
                SettingsChanged -= tab.SettingsChanged;
                tab.Dispose();
            }

            // if no more tabs left, let's reset the title to "Nocs" and disable saving to file
            if (tabs.TabPages.Count == 0)
            {
                SetMainTitle(string.Empty);
                lblCaretPosition.Text = string.Empty;
                menuSaveFileAs.Enabled = false;
            }
        }


        /// <summary>
        /// Starts retrieving documents from Google Docs.
        /// </summary>
        private void RetrieveDocuments()
        {
            // let's inform user that we are retrieving items (documents & folders)
            Status(StatusType.Retrieve, "Retrieving documents..");

            // disable menu options for changing the Google Account, for browsing Google Docs
            // and for saving while items are being retrieved
            menuGoogleAccount.Enabled = false;
            menuBrowse.Enabled = false;
            menuSave.Enabled = false;

            // run the backgroundworker for retrieving items
            BgWorkerGetAllItems.RunWorkerAsync();
        }

    }
}