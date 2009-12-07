using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Timers;
using System.Windows.Forms;

using Nocs.Helpers;
using Nocs.Properties;


namespace Nocs.Models
{
    public partial class Noc
    {
        #region Events

        private void SynchronizerContentUpdated(SyncResult result)
        {
            // no need to continue checking if this is an Untitled-document
            if (!Document.IsDraft && result.Job.SyncDocument.ResourceId == Document.ResourceId && result.ContentUpdated)
            {
                // in case for some reason the content gets "updated" on google's side
                // and the content hasn't actually changed, let's just stop here
                if (result.Document.Content == TxtNocContentNow && result.Document.Title == Document.Title)
                {
                    Debug.WriteLine("SynchronizerContentUpdated and incoming document is identical");
                    Document.ETag = result.Document.ETag;
                    return;
                }

                // also, if we're currently writing something (Content has changed),
                // let's not merge changes yet to avoid deletion of our own work
                if (ContentHasChanged)
                {
                    return;
                }

                // let's update the fields for this document
                Document = result.Document;

                // let's get the merged text based on text at last sync
                //Debug.WriteLine("SyncContentUpdated > From: TxtNocContentAtLastSync:\t" + TxtNocContentAtLastSync);
                Debug.WriteLine("SyncContentUpdated > From: TxtNocContentAtLastSuccessfulSave:\t" + TxtNocContentAtLastSuccessfulSave);
                Debug.WriteLine("SyncContentUpdated >   To: Incoming:\t\t\t\t\t" + Document.Content);
                Debug.WriteLine("SyncContentUpdated > TxtNocContentAtLastSave:\t" + TxtNocContentAtLastSuccessfulSave);
                Debug.WriteLine("SyncContentUpdated > TxtNocContentNow:\t\t\t" + TxtNocContentNow);
                Document.Content = Tools.MergeText(TxtNocContentAtLastSuccessfulSave, TxtNocContentNow, Document.Content);
                Debug.WriteLine("SyncContentUpdated > Merged:\t\t\t\t\t" + Document.Content);

                // let's find out if we've changed the editor text since last sync
                var editorContentChangedSinceLastSync = TxtNocContentAtLastSync != TxtNocContentNow;

                // let's reset the sync situation
                Debug.WriteLine("SyncContentUpdated > TxtNocContentAtLastSync/Save = " + Document.Content);
                TxtNocContentAtLastSync = Document.Content;
                TxtNocContentAtLastSuccessfulSave = Document.Content;
                ContentHasChanged = false;
                _tabTitleModifiedBecauseContentHasChanged = false;

                // this event will always be called from a separate thread so we'll have to use Invoke to modify Noc's properties
                // (can only make changes to WinForm controls from the master thread)
                if (IsHandleCreated)
                {
                    // let's update the tab's title and the RichTextBox content
                    NocEventHandler setTabTitle = SetTabTitleText;
                    Invoke(setTabTitle, result.Document.Title);

                    NocEventHandler setEditorContent = SetEditorContent;
                    Invoke(setEditorContent, Document.Content);
                }
                else
                {
                    Trace.WriteLine(DateTime.Now + " - Noc: Handle not created while trying to invoke content changes for: " + result.Document.Title);
                    return;
                }

                // in case we've change the text since last sync (while syncing for example),
                // let's save this document again
                if (editorContentChangedSinceLastSync && !NocsService.Working && !_bgWorkerSaveNoc.IsBusy)
                {
                    Debug.WriteLine("ContentUpdated: editor content changed since last sync, saving current content");
                    
                    // let's run the backgroundWorker for saving this document
                    var folderId = string.Empty;
                    if (Document.ParentFolders.Count > 0)
                    {
                        folderId = Document.ParentFolders[0];
                    }
                    var args = new object[] { folderId, Document.Title, false, false };
                    _bgWorkerSaveNoc.RunWorkerAsync(args);
                }
            }
        }


        /// <summary>
        /// Fires every time the text in current tab's textbox changes.
        /// Will update caret position and the local copy of the txtContent as well as reset the autoSaveTimer.
        /// </summary>
        private void TxtNocContentTextChanged(object sender, EventArgs e)
        {
            var txtContent = sender as RichTextBox;
            if (txtContent != null)
            {
                UpdateTxtNocContentNowAndTabTitle(txtContent);
            }
        }

        private bool HasContentChanged()
        {
            return TxtNocContentNow != Document.Content;
        }

        /// <summary>
        /// Occurs when the title for the tab changes (we'll have to notify the Main-form)
        /// </summary>
        private void Noc_TextChanged(object sender, EventArgs e)
        {
            if (Document.IsDraft)
            {
                NocTitleChanged(string.Empty);
            }
            else
            {
                NocTitleChanged(Document.ResourceId);
            }
        }


        /// <summary>
        /// Will be invoked by the syncTimer to add a sync-check-job in
        /// Synchronizer's Queue, unless it has already been added.
        /// </summary>
        void SynchronizerTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // let's make sure this document isn't already queued
            if (!_synchronizer.IsJobAlreadyQueued(Document.ResourceId))
            {
                // let's remove a job and execute it
                var job = new SyncJob
                {
                    Id = Document.ResourceId,
                    SyncDocument = Document,
                    ErrorsOccurred = 0,
                    Type = SyncJobType.CheckForChanges
                };

                _synchronizer.AddJobToQueue(job);
            }
            else
            {
                Debug.WriteLine("Job already added to queue: " + Document.Title);
            }
        }


        /// <summary>
        /// Occurs whenever the auto-save Timer elapses.
        /// Will save the document if all of the following conditions are met:
        /// 1. the text inside the editor has changed
        /// 2. AutoSave is enabled
        /// 3. service is idle
        /// 4. we're properly authenticated
        /// 5. current file is not new (a draft)
        /// 6. bgWorker for Saving this document isn't busy
        /// </summary>
        private void AutoSaveTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // let's make sure all the necessary conditions are met
            if (ContentHasChanged &&
                Settings.Default.AutoSave &&
                !NocsService.Working &&
                NocsService.UserIsAuthenticated() &&
                !Document.IsDraft &&
                !_bgWorkerSaveNoc.IsBusy)
            {
                _autoSaveTimer.Stop();
                Save();
            }
        }

        private void NocLeave(object sender, EventArgs e)
        {
            // let's set background tab interval and stop the autosave-checker
            _addToSyncQueueTimer.Interval = BackgroundNocSyncInterval * 60 * 1000;
        }

        private void NocEnter(object sender, EventArgs e)
        {
            // let's set the interval for foreground tab checks and start the autosave-checker
            _addToSyncQueueTimer.Interval = ForegroundNocSyncInterval * 60 * 1000;

            // let's also focus the textContent
            var txtContent = (RichTextBox)Controls.Find("txtNocContent", false)[0];
            if (txtContent != null)
            {
                // we'll unwire the event for NocEnter to prevent firing it twice
                Enter -= NocEnter;
                txtContent.Select();
                Enter += NocEnter;

                UpdateCaretPos(txtContent);
            }
        }

        private void TxtNocContentSelectionChanged(object sender, EventArgs e)
        {
            var txtContent = sender as RichTextBox;
            if (txtContent != null)
            {
                AdjustCaretStyle(txtContent);
            }
        }

        private void TxtNocContentGotFocus(object sender, EventArgs e)
        {
            var txtContent = sender as RichTextBox;
            if (txtContent != null)
            {
                AdjustCaretStyle(txtContent);
            }
        }

        private void TxtNocContentKeyDown(object sender, KeyEventArgs e)
        {
            var txtContent = sender as RichTextBox;
            if (txtContent != null)
            {
                if (e.KeyCode == Keys.Insert)
                {
                    _insertModeEnabled = !_insertModeEnabled;
                }

                AdjustCaretStyle(txtContent);
                UpdateCaretPos(txtContent);
            }
        }

        private void TxtNocContentKeyUp(object sender, EventArgs e)
        {
            var txtContent = sender as RichTextBox;
            if (txtContent != null)
            {
                AdjustCaretStyle(txtContent);
                UpdateCaretPos(txtContent);
            }
        }

        private void TxtNocContentMouseDown(object sender, EventArgs e)
        {
            var txtContent = sender as RichTextBox;
            if (txtContent != null)
            {
                AdjustCaretStyle(txtContent);
                UpdateCaretPos(txtContent);
            }
        }

        private static void TxtNocContentDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private static void TxtNocContentDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // we'll use a StringBuilder to collect all text data from the dropped document(s)
                var builder = new StringBuilder();

                // let's get the array of file paths
                var filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (var fileLoc in filePaths)
                {
                    if (File.Exists(fileLoc))
                    {
                        // let's read the contents and append it to the textbox
                        using (TextReader tr = new StreamReader(fileLoc, Encoding.UTF8))
                        {
                            // we'll add a line break at the end to separate multiple documents
                            builder.AppendLine(tr.ReadToEnd() + "\n");
                        }
                    }
                }

                var droppedContent = builder.ToString();
                if (droppedContent.Length > 0)
                {
                    var textbox = sender as RichTextBox;
                    if (textbox != null)
                    {
                        // get the start position to drop the text  
                        var i = textbox.SelectionStart;
                        var s = textbox.Text.Substring(i);

                        // let's drop the text on the RichTextBox
                        textbox.Text = textbox.Text.Substring(0, i) + droppedContent + s;
                    }
                }
            }
        }


        /// <summary>
        /// Will be invoked when main form fires the event for settings changing.
        /// </summary>
        public void SettingsChanged()
        {
            // we'll update Noc's settings based on current settings
            _autoSaveTimer.Enabled = Settings.Default.AutoSave;
            _autoSaveTimer.Interval = Settings.Default.AutoSaveTimeout * 1000;
            var txtContent = Controls.Find("txtNocContent", false)[0] as RichTextBox;
            if (txtContent != null)
            {
                txtContent.WordWrap = Settings.Default.WordWrap;

                // let's unhook the event for textbox content changes and change the font
                txtContent.TextChanged -= TxtNocContentTextChanged;
                txtContent.Font = Settings.Default.Font;
                txtContent.TextChanged += TxtNocContentTextChanged;

                /*
                   For some reason, when changing the Font of the RichTextBox-control, the text doesn't match Document.Content anymore.
                   The reason might be GdiCharset or NativeFont property differences.
                   In either case, we'll update the Document.Content to prevent from unnecessary auto-saves.
                */
                Document.Content = txtContent.Text;
            }
        }


        /// <summary>
        /// Will be invoked through a delegate because the caller will be in
        /// a separate thread than the one this tab page was created on.
        /// </summary>
        /// <param name="title">A string to be set as this tab's title.</param>
        private void SetTabTitleText(string title)
        {
            Text = title;
        }


        /// <summary>
        /// Will be invoked through a delegate because the caller will be in
        /// a separate thread than the one this tab page was created on.
        /// </summary>
        /// <param name="newContent">A string to be set as this tab's editor's content.</param>
        private void SetEditorContent(string newContent)
        {
            var txtContent = (RichTextBox)Controls.Find("txtNocContent", false)[0];

            // let's first calculate the new selectionStart based on old/new content difference in length
            var newSelectionStart = txtContent.SelectionStart + (newContent.Length - txtContent.TextLength);
            if (newSelectionStart < 0)
            {
                newSelectionStart = 0;
            }

            txtContent.Text = newContent;

            // let's reset the cursor to the previous selection
            txtContent.SelectionStart = newSelectionStart;
            txtContent.SelectionLength = 0;
        }

        private void UpdateTxtNocContentNowAndTabTitle(RichTextBox txtContent)
        {
            // txtContent will be null when caling after bgWorker for saving is complete
            if (txtContent == null)
            {
                txtContent = (RichTextBox)Controls.Find("txtNocContent", false)[0];
            }

            UpdateCaretPos(txtContent);
            TxtNocContentNow = txtContent.Text;
            // if document content is different, let's add "*" to title
            // (the first boolean check is so we won't add more than one "*")
            ContentHasChanged = HasContentChanged();
            if (!_tabTitleModifiedBecauseContentHasChanged && ContentHasChanged)
            {
                Text += "*";
                _tabTitleModifiedBecauseContentHasChanged = true;
            }
            else if (_tabTitleModifiedBecauseContentHasChanged && !ContentHasChanged)
            {
                // let's remove the added star
                Text = Text.Remove(Text.Length - 1, 1);
                _tabTitleModifiedBecauseContentHasChanged = false;
            }

            // if auto-save is enabled, let's reset timer
            if (Settings.Default.AutoSave)
            {
                _autoSaveTimer.Stop();
                _autoSaveTimer.Start();
            }
        }

        #endregion
    }
}
