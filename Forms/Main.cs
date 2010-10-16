using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Timer = System.Timers.Timer;
using Google.Documents;

using Nocs.Helpers;
using Nocs.Models;
using Nocs.Properties;


namespace Nocs.Forms
{
    public partial class Main : Form
    {
        // a common synchronizer that will be referenced throughout
        private Synchronizer _synchronizer;

        // an event for notifying individual Noc's that settings have changed
        public delegate void MainFormEventHandler();
        public event MainFormEventHandler SettingsChanged;

        // a delegate for thread-safe control-manipulation
        private delegate void MainFormThreadSafeDelegate(object value);

        // because the loading of new documents is done asynchronously, we'll use a threadLock object as help
        private readonly object _threadLock = new object();
        private int _contentUpdaterWorkers;

        // we'll also use a timer in the main form for queuing up retrievals for all entries (documents and folders)
        private const int AutoFetchAllEntriesInterval = 4;
        private readonly Timer _autoFetchAllEntriesTimer;
        private const string AutoFetchId = "autoFetchId";

        // we'll use only one instance of FindReplaceDialog
        private FindReplaceDialog _findReplace;

        // a helper Point to determine on which tab a context menu is opened at
        private Point _tabMenuLocation;


        public Main(IList<string> args)
        {
            InitializeComponent();

            // let's instantiate our Synchronizer
            _synchronizer = new Synchronizer();
            _synchronizer.ErrorWhileSyncing += SynchronizerErrorWhileSyncing;
            _synchronizer.AutoFetchAllEntriesFinished += AutoFetchAllEntriesFinished;
            _synchronizer.InitializeSynchronizer();
            _synchronizer.Start();

            // if arguments contain a txt-file to be opened, let's add that as a new tab instead of creating a new one
            if (args != null && args.Count > 0 && File.Exists(args[0]))
            {
                ReadAndLoadLocalFile(args[0]);
            }
            else
            {
                // let's then create a new tab (Noc) for the editor and add the editor inside the tab
                AddNoc();
            }

            // let's also initialize the timer for automatically retrieving all documents
            _autoFetchAllEntriesTimer = new Timer
            {
                Enabled = true,
                Interval = AutoFetchAllEntriesInterval * 60 * 1000
            };
            _autoFetchAllEntriesTimer.Elapsed += AutoFetchAllEntriesTimerElapsed;
        }


        #region File Menu

        private void MenuNewClick(object sender, EventArgs e)
        {
            // let's create a new tab (Noc)
            AddNoc();

            // if this was the first tab added, let's also select its txtContent
            if (tabs.TabCount == 1)
            {
                var editor = GetCurrentNocTextEditor();
                editor.Focus();
                ((Noc)tabs.TabPages[0]).UpdateCaretPos(editor);
            }
        }

        private void MenuSaveClick(object sender, EventArgs e)
        {
            // let's first make sure the document content has changed
            var currentTab = tabs.SelectedTab as Noc;
            if (currentTab == null || (!currentTab.Document.IsDraft && !currentTab.ContentHasChanged))
            {
                return;
            }

            // let's stop here if we're already saving the current tab
            if (currentTab.SaveWorkerIsBusy())
            {
                return;
            }

            // make sure we are authenticated
            if (!NocsService.UserIsAuthenticated())
            {
                // user's not authenticated, let's ask for credentials to retrieve items
                var login = new Login();
                if (login.ShowDialog() == DialogResult.OK)
                {
                    RetrieveDocuments();
                }
                return;
            }

            // if current file is not new, let's just save it
            if (!currentTab.Document.IsDraft)
            {
                Status(StatusType.Save, "Saving...");
                currentTab.Save();
            }
            else
            {
                // ask for a name
                var saveResponse = SaveDialog.SaveDialogBox("Enter a name for your file:", "Save", "Untitled");

                if (!string.IsNullOrEmpty(saveResponse.DocumentName))
                {
                    Status(StatusType.Save, "Saving...");
                    currentTab.SaveAs(saveResponse);
                }
            }
        }


        private void MenuSaveAsClick(object sender, EventArgs e)
        {
            // let's first make sure the document content has changed
            var currentTab = tabs.SelectedTab as Noc;
            if (currentTab == null)
            {
                return;
            }

            // let's stop here if we're already saving the current tab
            if (currentTab.SaveWorkerIsBusy())
            {
                return;
            }

            // make sure we are authenticated
            if (!NocsService.UserIsAuthenticated())
            {
                // user's not authenticated, let's ask for credentials to retrieve items
                var login = new Login();
                if (login.ShowDialog() == DialogResult.OK)
                {
                    RetrieveDocuments();
                }
                return;
            }

            // ask for a name
            var inputName = new SaveDialog();
            var saveResponse = SaveDialog.SaveDialogBox("Enter a name for your file:", "Save", "Untitled");

            if (!string.IsNullOrEmpty(saveResponse.DocumentName))
            {
                Status(StatusType.Save, "Saving...");
                currentTab.SaveAs(saveResponse);
            }
        }


        private void MenuBrowseClick(object sender, EventArgs e)
        {
            // make sure we are authenticated
            if (!NocsService.UserIsAuthenticated())
            {
                // user's not authenticated, let's ask for credentials to retrieve items
                var login = new Login();
                if (login.ShowDialog() == DialogResult.OK)
                {
                    Status(StatusType.Retrieve, "Retrieving items...");
                    menuGoogleAccount.Enabled = false;
                    menuBrowse.Enabled = false;
                    menuSave.Enabled = false;
                    BgWorkerGetAllItems.RunWorkerAsync();
                }
                return;
            }

            var currentTab = tabs.SelectedTab as Noc;

            Browse nocsBrowse;

            // we will give the Browse-form the current selected documentId (from selected tab),
            // so the Browse-form can select it from its listBox on load
            // we'll also give Browse a reference to Synchronizer because AutoFetchAllEventFinished might fire
            if (currentTab != null && !currentTab.Document.IsDraft)
            {
                nocsBrowse = new Browse(ref _synchronizer, currentTab.Document.ResourceId);
            }
            else
            {
                nocsBrowse = new Browse(ref _synchronizer);
            }

            // Browse-form can tell us to add+load a document, or notify us of document renames/deletions
            nocsBrowse.AddDocumentToMainForm += BrowseAddDocumentToMainForm;
            nocsBrowse.DocumentRenamed += BrowseDocumentRenamed;
            nocsBrowse.DocumentDeleted += BrowseDocumentDeleted;

            nocsBrowse.ShowDialog();
        }


        private void MenuLoadFileClick(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                DefaultExt = "txt",
                Filter = "Text files (*.txt)|*.txt"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // let's get the array of file paths
                var filePath = openFileDialog.FileName;
                if (File.Exists(filePath))
                {
                    ReadAndLoadLocalFile(filePath);
                }
            }
        }


        private void MenuSaveFileAsClick(object sender, EventArgs e)
        {
            // let's first get the current file content
            var txtContent = GetCurrentNocTextEditor();
            if (txtContent != null)
            {
                // let's ask the user where to save it
                var saveFileDialog = new SaveFileDialog
                {
                    DefaultExt = "txt",
                    Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                    AddExtension = true
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtContent.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                }
            }
        }


        private void MenuPageSetupClick(object sender, EventArgs e)
        {
            // set the default page settings and show the pageSetup dialog
            var pageSetupDialog = new PageSetupDialog
            {
                PageSettings = printDocument.DefaultPageSettings,
                PrinterSettings = printDocument.PrinterSettings,
                AllowMargins = true,
                AllowOrientation = true,
                EnableMetric = true,
                MinMargins = new Margins(40, 40, 40, 40)
            };

            // save the settings
            if (pageSetupDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.DefaultPageSettings = pageSetupDialog.PageSettings;
                printDocument.PrinterSettings = pageSetupDialog.PrinterSettings;
            }
        }

        private void MenuPrintClick(object sender, EventArgs e)
        {
            // set the document for printing
            var printDialog = new PrintDialog
            {
                Document = printDocument,
                PrinterSettings = printDocument.PrinterSettings
            };

            // print the document
            var result = printDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void PrintDocumentPrintPage(object sender, PrintPageEventArgs e)
        {
            // get page margins from event arguments
            var r = e.MarginBounds;

            // "draw" the content
            var currentEditor = GetCurrentNocTextEditor();
            if (currentEditor != null)
            {
                e.Graphics.DrawString(currentEditor.Text, currentEditor.Font, Brushes.Black, r);
            }
        }

        private void MenuExitClick(object sender, EventArgs e)
        {
            Close();
        }

        #endregion


        #region Edit Menu

        private void menuUndo_Click(object sender, EventArgs e)
        {
            var currentEditor = GetCurrentNocTextEditor();
            if (currentEditor != null)
            {
                currentEditor.Undo();
            }
        }

        private void menuRedo_Click(object sender, EventArgs e)
        {
            var currentEditor = GetCurrentNocTextEditor();
            if (currentEditor != null)
            {
                currentEditor.Redo();
            }
        }

        private void menuFindReplace_Click(object sender, EventArgs e)
        {
            // if the Find & Replace dialog isn't open, let's create and show it
            if (_findReplace != null && _findReplace.Created)
            {
                _findReplace.Focus();
                return;
            }

            var currentTxtEditor = GetCurrentNocTextEditor();
            if (currentTxtEditor != null)
            {
                _findReplace = new FindReplaceDialog(currentTxtEditor);
                _findReplace.Show();
            }
        }

        private void menuFindNext_Click(object sender, EventArgs e)
        {
            // if Find & Replace dialog is open, we'll use RegexHelpers.FindNext to find the next occurrence with the given options
            if (_findReplace == null || !_findReplace.Created)
            {
                return;
            }

            var currentTxtEditor = GetCurrentNocTextEditor();
            if (currentTxtEditor != null)
            {
                RegexHelpers.FindNext(_findReplace.txtFindWhat.Text, _findReplace.chbCaseSensitive.Checked, currentTxtEditor, _findReplace.chbRxp.Checked);
            }
        }

        private void menuCut_Click(object sender, EventArgs e)
        {
            var currentEditor = GetCurrentNocTextEditor();
            if (currentEditor != null)
            {
                currentEditor.Cut();
            }
        }

        private void menuCopy_Click(object sender, EventArgs e)
        {
            var currentEditor = GetCurrentNocTextEditor();
            if (currentEditor != null)
            {
                currentEditor.Copy();
            }
        }

        private void menuPaste_Click(object sender, EventArgs e)
        {
            var currentEditor = GetCurrentNocTextEditor();
            if (currentEditor != null)
            {
                currentEditor.Paste();
            }
        }

        private void menuSelectAll_Click(object sender, EventArgs e)
        {
            var currentEditor = GetCurrentNocTextEditor();
            if (currentEditor != null)
            {
                currentEditor.SelectAll();
            }
        }

        private void menuAddTimeDate_Click(object sender, EventArgs e)
        {
            var currentEditor = GetCurrentNocTextEditor();
            if (currentEditor != null)
            {
                currentEditor.SelectedText = DateTime.Now.ToString();
            }
        }

        #endregion


        #region Options Menu

        private void MenuWordWrapCheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.WordWrap = menuWordWrap.Checked;
            Settings.Default.Save();
            if (SettingsChanged != null)
            {
                SettingsChanged();
            }
        }

        private void MenuFontClick(object sender, EventArgs e)
        {
            // open font dialog and change all editors' fonts if OK is clicked
            fontDialog.Font = Settings.Default.Font;
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.Font = fontDialog.Font;
                Settings.Default.Save();
                if (SettingsChanged != null)
                {
                    SettingsChanged();
                }
            }
        }

        private void MenuPreferencesClick(object sender, EventArgs e)
        {
            // make sure we are authenticated
            if (!NocsService.UserIsAuthenticated())
            {
                // user's not authenticated, let's ask for credentials to retrieve items
                var login = new Login();
                if (login.ShowDialog() == DialogResult.OK)
                {
                    RetrieveDocuments();
                }
                return;
            }

            var nocsPreferences = new Preferences();
            if (nocsPreferences.ShowDialog() == DialogResult.OK)
            {
                // let's notify listeners
                if (SettingsChanged != null)
                {
                    SettingsChanged();
                }
            }

            // if proxy is enabled after preferences were saved, and for some reason the Browsing isn't enabled and
            // no worker is busy, let's try to re-retrieve all items
            if (Settings.Default.UseProxy && !menuBrowse.Enabled && !BgWorkerStartService.IsBusy && !BgWorkerGetAllItems.IsBusy)
            {
                // make sure user was validated
                if (!string.IsNullOrEmpty(NocsService.Username) && !string.IsNullOrEmpty(NocsService.Password))
                {
                    // inform user that we are retrieving items (documents)
                    Status(StatusType.Retrieve, "Retrieving the list of documents..");

                    // disable menu options for changing the Google Account and for browsing Google Docs while items are being retrieved
                    menuGoogleAccount.Enabled = false;
                    menuBrowse.Enabled = false;
                    menuSave.Enabled = false;

                    // reset help variable
                    NocsService.AccountChanged = false;

                    // run the backgroundworker for retrieving items
                    BgWorkerGetAllItems.RunWorkerAsync();
                }
            }
        }

        private void MenuGoogleAccountClick(object sender, EventArgs e)
        {
            // let's first stop all processing
            _synchronizer.Stop();
            _autoFetchAllEntriesTimer.Stop();

            // let's then find out if any of the open tabs are unsaved
            foreach (Noc tab in tabs.TabPages)
            {
                if (tab.ContentHasChanged && NocsService.UserIsAuthenticated())
                {
                    // display a msg asking the user to save changes or abort
                    var result = MessageBox.Show("Do you want to save the changes to " + tab.Text + "?", "Nocs", MessageBoxButtons.YesNoCancel);

                    if (result == DialogResult.Yes)
                    {
                        // let's stop here if we're already saving the current tab
                        if (tab.SaveWorkerIsBusy())
                        {
                            // re-enable all processing
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
            }

            // we're past saving questions, let's show the Login dialog for changing Google account
            var login = new Login();
            login.ShowDialog();

            // let's check if user account was changed once the login window is closed
            if (NocsService.AccountChanged)
            {
                // inform the new user that we are retrieving items
                Status(StatusType.Retrieve, "Synchronizing with Google Docs...");

                // disable menu options for changing google account and opening documents while items are being retrieved
                menuGoogleAccount.Enabled = false;
                menuBrowse.Enabled = false;
                menuSave.Enabled = false;

                // reset helping variable
                NocsService.AccountChanged = false;

                // let's reset titlebar, clear tabs, etc.
                SetMainTitle(string.Empty);
                tabs.TabPages.Clear();

                // let's clear synchronizer queues
                _synchronizer.JobQueue.Clear();
                _synchronizer.ErrorQueue.Clear();

                // let's then create a new tab (Noc)
                AddNoc();

                // and finally let's run the backgroundworker to retrieve new items for this account
                BgWorkerGetAllItems.RunWorkerAsync(false);
            }
            
            // either way, let's re-enable timers
            _synchronizer.Start();
            _autoFetchAllEntriesTimer.Start();

        }

        private void MenuAlwaysOnTopCheckedChanged(object sender, EventArgs e)
        {
            TopMost = menuAlwaysOnTop.Checked;
            Settings.Default.AlwaysOnTop = menuAlwaysOnTop.Checked;
            Settings.Default.Save();
        }

        #endregion


        #region Help Menu

        private void MenuAboutClick(object sender, EventArgs e)
        {
            var about = new About();
            about.ShowDialog();
        }

        #endregion


        #region Tab Menu

        private void menuCloseTab_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (var i = 0; i < tabs.TabCount; ++i)
                {
                    if (tabs.GetTabRect(i).Contains(_tabMenuLocation))
                    {
                        CloseTab(i);
                        break;
                    }
                }
            }
        }

        private void tabs_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (var i = 0; i < tabs.TabCount; ++i)
                {
                    if (tabs.GetTabRect(i).Contains(new Point(e.X, e.Y)))
                    {
                        // let's save the location for future reference
                        _tabMenuLocation = e.Location;

                        menuTab.Show(tabs, e.Location);
                        break;
                    }
                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                for (var i = 0; i < tabs.TabCount; ++i)
                {
                    if (tabs.GetTabRect(i).Contains(new Point(e.X, e.Y)))
                    {
                        CloseTab(i);
                        break;
                    }
                }

            }
        }

        private void Noc_Enter(object sender, EventArgs e)
        {
            var currentTab = tabs.SelectedTab as Noc;
            if (currentTab != null)
            {
                SetMainTitle(currentTab.Text);
            }
        }

        #endregion


        #region Helpers

        private void ReadAndLoadLocalFile(string filePath)
        {
            // we'll use a StringBuilder to collect all text data from the dropped document(s)
            var builder = new StringBuilder();

            // let's read the contents and append it to the textbox
            using (TextReader tr = new StreamReader(filePath, Encoding.Default))
            {
                builder.AppendLine(tr.ReadToEnd());
            }

            var openedContent = builder.ToString();
            if (openedContent.Length > 0)
            {
                var textbox = GetCurrentNocTextEditor();
                if (textbox != null)
                {
                    // get the start position to drop the text
                    var i = textbox.SelectionStart;
                    var s = textbox.Text.Substring(i);

                    // let's drop the text on the RichTextBox
                    textbox.Text = textbox.Text.Substring(0, i) + openedContent + s;
                }
                else
                {
                    // no tabs found, let's create one and try again
                    AddNoc();
                    textbox = GetCurrentNocTextEditor();
                    // get the start position to drop the text  
                    var i = textbox.SelectionStart;
                    var s = textbox.Text.Substring(i);

                    // let's drop the text on the RichTextBox
                    textbox.Text = textbox.Text.Substring(0, i) + openedContent + s;
                }
            }
        }


        private void AddNoc()
        {
            var untitledNoc = new Noc(_synchronizer, contextMenuEditor)
            {
                Name = "Untitled"
            };

            // let's hook up events
            untitledNoc.CaretPositionChanged += NocCaretPositionChanged;
            untitledNoc.Enter += Noc_Enter;
            untitledNoc.TokenExpired += TokenExpiredWhileSaving;
            untitledNoc.NocTitleChanged += NocTitleChanged;
            untitledNoc.Status += Status;
            untitledNoc.Text = "Untitled";
            SettingsChanged += untitledNoc.SettingsChanged;

            // let's add the new Noc to tabcontrol
            tabs.Controls.Add(untitledNoc);

            // let's update the selectedIndex
            var selectedIndex = (tabs.TabCount - 1);
            if (selectedIndex < 0)
            {
                selectedIndex = 0;
            }
            tabs.SelectedIndex = selectedIndex;
            SetMainTitle("Untitled");

            // let's also make sure "Save to file" is enabled
            menuSaveFileAs.Enabled = true;
        }

        private void AddInactiveNoc(Document doc)
        {
            var inactiveNoc = new Noc(doc, _synchronizer, contextMenuEditor);

            // let's hook up events
            inactiveNoc.CaretPositionChanged += NocCaretPositionChanged;
            inactiveNoc.Enter += Noc_Enter;
            inactiveNoc.TokenExpired += TokenExpiredWhileSaving;
            inactiveNoc.NocTitleChanged += NocTitleChanged;
            inactiveNoc.Status += Status;
            SettingsChanged += inactiveNoc.SettingsChanged;

            // let's add the new Noc to tabcontrol
            tabs.Controls.Add(inactiveNoc);

            // let's update the selectedIndex
            var selectedIndex = (tabs.TabCount - 1);
            if (selectedIndex < 0)
            {
                selectedIndex = 0;
            }
            tabs.SelectedIndex = selectedIndex;
            SetMainTitle(doc.Title);

            // let's also make sure "Save to file" is enabled
            menuSaveFileAs.Enabled = true;
        }

        private void SetMainTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                Text = " Nocs";
                tabs.Focus();
            }
            else
            {
                Text = string.Format(" {0} - Nocs", title);
            }
        }

        /// <summary>
        /// Will find the current tab's RichTextBox-control and return it.
        /// If none found, will return null.
        /// </summary>
        private RichTextBox GetCurrentNocTextEditor()
        {
            if (tabs == null || tabs.TabCount == 0)
            {
                return null;
            }

            var currentTab = tabs.SelectedTab as Noc;
            if (currentTab != null)
            {
                var currentNocRichTextBox = (RichTextBox)currentTab.Controls.Find("txtNocContent", false)[0];
                if (currentNocRichTextBox != null)
                {
                    return currentNocRichTextBox;
                }
            }
            return null;
        }

        #endregion
    }
}