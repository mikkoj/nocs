using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;

using Google.Documents;
using Nocs.Properties;


namespace Nocs.Models
{
    /// <summary>
    /// Represents a single document-tab. Derived from TabPage in order to include document related info/funtionality.
    /// </summary>
    public partial class Noc : TabPage
    {
        public Document Document { get; set; }

        // holds the current editor content
        private string TxtNocContentNow { get; set; }

        // hold the editor content at the last time this Noc was synced (saved or content updated)
        // ReSharper disable InconsistentNaming
        private string TxtNocContentAtLastSync = string.Empty;
        private string TxtNocContentAtLastSuccessfulSave = string.Empty;
        // ReSharper restore InconsistentNaming

        // intervals for synchronization in minutes
        private const double ForegroundNocSyncInterval = 0.25;
        private const double BackgroundNocSyncInterval = 4;

        // a reference to the main form's Synchronizer
        private readonly Synchronizer _synchronizer;
        private System.Timers.Timer _addToSyncQueueTimer;
        private System.Timers.Timer _autoSaveTimer;

        // a helper booleans for checking if current content is modified
        private bool _tabTitleModifiedBecauseContentHasChanged;
        public bool ContentHasChanged { get; private set; }

        // events for notifying the Main form
        public delegate void NocEventHandler(string eventInfo);
        public event NocEventHandler CaretPositionChanged;
        public event NocEventHandler NocTitleChanged;

        public delegate void NocStatusEventHandler(StatusType statusType, object value);
        public event NocStatusEventHandler Status;

        public delegate void NocBasicEventHandler();
        public event NocBasicEventHandler TokenExpired;

        // event for thread-safety
        private delegate void NocUpdateContentAndTitle(RichTextBox txtContent);

        // a backgroundWorker for saving
        private BackgroundWorker _bgWorkerSaveNoc;

        private bool _insertModeEnabled;


        #region Constructors

        public Noc()
        {
        }

        public Noc(Synchronizer synchronizer, ContextMenuStrip contextMenuEditor)
        {
            // let's set up the reference to the main form's synchronizer
            // so we can access it through individual tabs (Noc's)
            _synchronizer = synchronizer;
            Document = new Document
            {
                Title = "Untitled",
                Content = string.Empty,
                AtomEntry = { IsDraft = true }
            };

            InitializeNoc(contextMenuEditor);
        }


        /// <summary>
        /// Constructor for initiliaizing a loaded document. Won't be activated until Noc.Activate() is called.
        /// </summary>
        /// <param name="document">The document to be initialized.</param>
        /// <param name="synchronizer">A reference to main forms Synchronizer.</param>
        /// <param name="contextMenuEditor">Context menu for the text editor (from the Main form).</param>
        public Noc(Document document, Synchronizer synchronizer, ContextMenuStrip contextMenuEditor)
        {
            // let's set up the reference to the main form's synchronizer
            // so we can access it through individual tabs (Noc's)
            _synchronizer = synchronizer;

            Document = document;
            base.Text = document.Title;
            Name = document.Id;
            InitializeNoc(contextMenuEditor);

            var txtContent = (RichTextBox)Controls.Find("txtNocContent", false)[0];
            txtContent.Enabled = false;
            _autoSaveTimer.Stop();
            _addToSyncQueueTimer.Stop();
        }

        private void InitializeNoc(ContextMenuStrip contextMenuEditor)
        {
            Padding = new Padding(3);
            UseVisualStyleBackColor = true;
            _insertModeEnabled = false;

            // let's first create, setup, and add the text editor for this tab
            var txtNocContent = new RichTextBox
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Font = Settings.Default.Font,
                WordWrap = Settings.Default.WordWrap,
                Name = "txtNocContent",
                Location = new Point(0, 0),
                Size = new Size(Width, Height),
                Multiline = true,
                AllowDrop = true,
                HideSelection = false,
                AcceptsTab = true,
                AutoWordSelection = false,
                ContextMenuStrip = contextMenuEditor
            };

            txtNocContent.TextChanged += TxtNocContentTextChanged;
            txtNocContent.DragEnter += TxtNocContentDragEnter;
            txtNocContent.SelectionChanged += TxtNocContentSelectionChanged;
            txtNocContent.DragDrop += TxtNocContentDragDrop;
            // these events are for maintaining Caret-style when Insert-mode changes
            txtNocContent.KeyDown += TxtNocContentKeyDown;
            txtNocContent.KeyUp += TxtNocContentKeyUp;
            txtNocContent.MouseDown += TxtNocContentMouseDown;
            txtNocContent.GotFocus += TxtNocContentGotFocus;
            
            TxtNocContentNow = string.Empty;

            // events for changing sync-timer's interval when tab is selected or background
            Enter += NocEnter;
            Leave += NocLeave;

            // we'll notify the main form whenever the title for the tab changes
            TextChanged += Noc_TextChanged;

            Controls.Add(txtNocContent);

            _addToSyncQueueTimer = new System.Timers.Timer { Interval = ForegroundNocSyncInterval * 60 * 1000 };
            _addToSyncQueueTimer.Elapsed += SynchronizerTimerElapsed;

            // let's also setup the auto-saver
            _autoSaveTimer = new System.Timers.Timer();
            _autoSaveTimer.Elapsed += AutoSaveTimerElapsed;
            _autoSaveTimer.Interval = (Settings.Default.AutoSaveTimeout * 1000);

            // we won't enable the autoSave timer if this is a new document
            if (!Document.IsDraft)
            {
                _autoSaveTimer.Enabled = Settings.Default.AutoSave;
            }

            // let's also wire up the handler for content changes
            _synchronizer.ContentUpdated += SynchronizerContentUpdated;

            _bgWorkerSaveNoc = new BackgroundWorker();
            _bgWorkerSaveNoc.DoWork += BgWorkerSaveNoc_DoWork;
            _bgWorkerSaveNoc.RunWorkerCompleted += BgWorkerSaveNoc_Completed;
        }


        #endregion


        #region Actions

        /// <summary>
        /// Activates the editor and starts timers for syncing/auto-saving.
        /// </summary>
        public void Activate()
        {
            var txtContent = (RichTextBox)Controls.Find("txtNocContent", false)[0];
            txtContent.Enabled = true;
            txtContent.Text = Document.Content;
            Text = Document.Title;
            TxtNocContentNow = txtContent.Text;
            // we will set the last-sync content as it is right now, so the first
            // content update will compare changes against it, instead of an empty string
            TxtNocContentAtLastSync = txtContent.Text;
            TxtNocContentAtLastSuccessfulSave = txtContent.Text;

            txtContent.Select();
            _autoSaveTimer.Start();
            _addToSyncQueueTimer.Start();
        }

        public void Deactivate()
        {
            // we'll unhook some events to avoid unnecessary actions on a disposed object
            Enter -= NocEnter;
            Leave -= NocLeave;
            TextChanged -= Noc_TextChanged;
            _synchronizer.ContentUpdated -= SynchronizerContentUpdated;
            _autoSaveTimer.Stop();
            _addToSyncQueueTimer.Stop();
        }

        public void Save()
        {
            string folderId = null;
            if (Document.ParentFolders.Count > 0)
            {
                folderId = Document.ParentFolders[0];
            }

            Save(folderId, Document.Title, false, false);
        }

        public void SaveAs(SaveDialogResponse response)
        {
            Save(response.Folder, response.DocumentName, true, response.CreateDefaultFolder);
        }

        private void Save(string folder, string title, bool saveAs, bool createDefaultFolder)
        {
            if (!NocsService.Working)
            {
                // let's pause the synchronizer and queue-adder while saving
                _synchronizer.Stop();
                _addToSyncQueueTimer.Stop();

                // if content hasn't changed, there's no need to save (unless we want to "save as")
                if (!ContentHasChanged && !saveAs)
                {
                    if (!InvokeRequired)
                    {
                        Status(StatusType.Reset, null);
                    }
                    return;
                }

                // mostly because of "enter a name for untitled document"-scenarious,
                // we will re-check the bgWorker for auto-saves here also
                if (SaveWorkerIsBusy())
                {
                    if (!InvokeRequired)
                    {
                        Status(StatusType.Reset, null);
                    }
                    return;
                }

                // let's update the document content with the current textbox-content before saving the document
                Document.Content = TxtNocContentNow;

                // let's run the backgroundWorker for saving this document
                var args = new object[] { folder, title, saveAs, createDefaultFolder };
                _bgWorkerSaveNoc.RunWorkerAsync(args);
            }
        }

        private void BgWorkerSaveNoc_DoWork(object sender, DoWorkEventArgs e)
        {
            var folder = ((object[])e.Argument)[0];
            folder = folder != null ? folder.ToString() : string.Empty;
            var title = ((object[])e.Argument)[1].ToString();
            var saveAs = (bool)(((object[])e.Argument)[2]);
            var createDefaultFolder = (bool)(((object[])e.Argument)[3]);

            // we need to preserve the original content to avoid problems when changing txtEditor-content while saving
            var documentContentWhenStartedSaving = Document.Content;

            // let's see if we have to create a new Document:
            // - if Document is a draft it hasn't been saved yet
            // - if saveAs is true, user wanted to save this document with a new name
            if (Document.IsDraft || saveAs)
            {
                Document = _synchronizer.CreateNewDocument(folder.ToString(), title, Document.Content, createDefaultFolder);
            }
            else
            {
                Document = _synchronizer.SaveDocument(Document);
            }

            if (Document != null)
            {
                Document.Content = documentContentWhenStartedSaving;
                e.Result = Document;
            }
        }

        private void BgWorkerSaveNoc_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Trace.WriteLine(DateTime.Now + " - BgWorkerSaveNoc_Completed: error while saving: " + e.Error.Message);

                // check if token was expired
                if (e.Error.Message.ToLower().Contains("token expired"))
                {
                    // let's resume the synchronizer
                    _synchronizer.Start();

                    // let's also start up the sync timer if not already started
                    // (this might be  the first time saving)
                    if (!_addToSyncQueueTimer.Enabled)
                    {
                        _addToSyncQueueTimer.Start();
                    }

                    if (Settings.Default.AutoSave)
                    {
                        _autoSaveTimer.Start();
                    }

                    TokenExpired();
                    return;
                }

                // else just show the error to user
                Status(StatusType.Error, e.Error.Message);
            }
            else
            {
                // let's get our saved document from the result and update current tab with it
                var newDoc = e.Result as Document;
                if (newDoc != null)
                {
                    // let's reset the sync situation (unless this document wasn't saved because of Etag mismatch)
                    if (newDoc.Summary != "unchanged")
                    {
                        // TxtNocContentAtLastSave works mainly only as a helper to remember the previously saved content
                        // TxtNocContentAtLastSync is used when merging incoming Google Docs content with those previous changes
                        Debug.WriteLine("SaveCompleted > TxtNocContentAtLastSync = " + TxtNocContentAtLastSuccessfulSave);
                        TxtNocContentAtLastSync = TxtNocContentAtLastSuccessfulSave;

                        Debug.WriteLine("SaveCompleted > TxtNocContentAtLastSuccessfulSave = " + Document.Content);
                        TxtNocContentAtLastSuccessfulSave = Document.Content;
                    }

                    Document.Summary = null;
                    ContentHasChanged = false;
                    _tabTitleModifiedBecauseContentHasChanged = false;

                    // need to check for InvokeRequired because we can only make changes to WinForm controls from the master thread
                    if (InvokeRequired)
                    {
                        NocEventHandler setTabTitle = SetTabTitleText;
                        Invoke(setTabTitle, newDoc.Title);

                        NocUpdateContentAndTitle updateNocContentAndTitle = UpdateTxtNocContentNowAndTabTitle;
                        Invoke(updateNocContentAndTitle, new object[] { null });
                    }
                    else
                    {
                        Text = newDoc.Title;
                        UpdateTxtNocContentNowAndTabTitle(null);
                    }
                }
                else
                {
                    // couldn't create document / save, possibly because of "could not convert document"
                    if (InvokeRequired)
                    {
                        NocUpdateContentAndTitle updateNocContentAndTitle = UpdateTxtNocContentNowAndTabTitle;
                        Invoke(updateNocContentAndTitle, new object[] { null });
                    }
                    else
                    {
                        UpdateTxtNocContentNowAndTabTitle(null);
                    }

                    Debug.WriteLine(DateTime.Now + " - BgWorkerSaveNoc_Completed: e.Result is null");
                }

                // item saved, let's notify the Main-form (unless auto-saving)
                // if InvokeRequired, it means that we're running through auto-save and a separate thread -> no need to update status
                if (!InvokeRequired)
                {
                    Status(StatusType.Reset, null);
                }

                // let's resume the synchronizer
                _synchronizer.Start();

                // let's also start up the sync timer if not already started
                // (this might be  the first time saving)
                if (!_addToSyncQueueTimer.Enabled)
                {
                    _addToSyncQueueTimer.Start();
                }

                if (Settings.Default.AutoSave)
                {
                    _autoSaveTimer.Start();
                }
            }
        }

        #endregion


        #region Helpers

        // ReSharper disable InconsistentNaming
        private const int EM_LINEINDEX = 0xbb;
        // ReSharper restore InconsistentNaming

        [DllImport("user32.dll")]
        extern static int SendMessage(IntPtr hwnd, int message, int wparam, int lparam);

        [DllImport("user32.dll")]
        static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int nWidth, int nHeight);
        
        [DllImport("user32.dll")]
        static extern bool ShowCaret(IntPtr hWnd);

        [DllImport("User32.dll")]
        static extern bool DestroyCaret();

        /// <summary>
        /// A helper method for determining the current caret position.
        /// </summary>
        public void UpdateCaretPos(RichTextBox txtContent)
        {
            try
            {
                var index = txtContent.SelectionStart;
                var line = txtContent.GetLineFromCharIndex(index);
                var col = index - SendMessage(txtContent.Handle, EM_LINEINDEX, -1, 0);
                var newPosition = string.Format("Line: {0} - Col: {1}", (++line), (++col));

                // let's notify the listeners (unless there are none)
                if (CaretPositionChanged != null)
                {
                    CaretPositionChanged(newPosition);
                }
            }
            catch (ObjectDisposedException)
            {
                // it shouldn't be possible for a disposed Noc to be updated
                // "caret position" -wise, but we'll catch it here either way
            }
        }

        private void AdjustCaretStyle(RichTextBox txtEditor)
        {
            try
            {
                if (_insertModeEnabled)
                {
                    DestroyCaret();

                    // we'll adjust the width and height of the caret based on the font size
                    txtEditor.SelectionChanged -= TxtNocContentSelectionChanged;
                    var currentSelectionLength = txtEditor.SelectionLength;
                    txtEditor.SelectionLength = 1;
                    var stringMeasure = txtEditor.SelectedText;
                    txtEditor.SelectionLength = currentSelectionLength;
                    txtEditor.SelectionChanged += TxtNocContentSelectionChanged;

                    // let's check if we're at the end of line
                    if (string.IsNullOrEmpty(stringMeasure) ||
                        stringMeasure.Trim().Equals("\n") ||
                        stringMeasure.Trim().Length == 0)
                    {
                        stringMeasure = "t";
                    }

                    var proposedSize = new Size(int.MaxValue, int.MaxValue);
                    var caretSize = TextRenderer.MeasureText(CreateGraphics(), stringMeasure, txtEditor.Font, proposedSize, TextFormatFlags.NoPadding);
                    CreateCaret(txtEditor.Handle, IntPtr.Zero, caretSize.Width, caretSize.Height);
                    ShowCaret(txtEditor.Handle);
                }
                else
                {
                    DestroyCaret();
                    var caretHeight = txtEditor.Font.Height - (txtEditor.Font.Height / 10);
                    CreateCaret(txtEditor.Handle, IntPtr.Zero, 1, caretHeight);
                    ShowCaret(txtEditor.Handle);
                }
            }
            catch (ObjectDisposedException)
            {
                // it shouldn't be possible for a disposed Noc to be updated
                // "caret position" -wise, but we'll catch it here either way
            }
        }


        public bool SaveWorkerIsBusy()
        {
            return _bgWorkerSaveNoc.IsBusy;
        }

        #endregion
    }
}
