namespace Nocs.Forms
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.menuLoadFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveFileAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddTimeDate = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWordWrap = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFont = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPreferences = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGoogleAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabs = new System.Windows.Forms.TabControl();
            this.menuTab = new System.Windows.Forms.ContextMenuStrip();
            this.menuCloseTab = new System.Windows.Forms.ToolStripMenuItem();
            this.BgWorkerStartService = new System.ComponentModel.BackgroundWorker();
            this.BgWorkerGetAllItems = new System.ComponentModel.BackgroundWorker();
            this.lblCaretPosition = new System.Windows.Forms.Label();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuEditor = new System.Windows.Forms.ContextMenuStrip();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.menuEditorSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditorTimeDate = new System.Windows.Forms.ToolStripMenuItem();
            this.pictStatusMinimal = new System.Windows.Forms.PictureBox();
            this.menuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBrowse = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFindReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFindNext = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditorUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditorRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditorCut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditorCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditorPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.menuTab.SuspendLayout();
            this.contextMenuEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.AutoSize = false;
            this.menuMain.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuMain.GripMargin = new System.Windows.Forms.Padding(0);
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu,
            this.optionsMenu,
            this.helpMenu});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Padding = new System.Windows.Forms.Padding(0);
            this.menuMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuMain.Size = new System.Drawing.Size(728, 20);
            this.menuMain.TabIndex = 1;
            // 
            // fileMenu
            // 
            this.fileMenu.AutoSize = false;
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNew,
            this.menuBrowse,
            this.toolStripSeparator,
            this.menuSave,
            this.menuSaveAs,
            this.toolStripSeparator9,
            this.menuLoadFile,
            this.menuSaveFileAs,
            this.toolStripSeparator4,
            this.menuExit});
            this.fileMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(32, 20);
            this.fileMenu.Text = "&File";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(209, 6);
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.menuSaveAs.Size = new System.Drawing.Size(212, 22);
            this.menuSaveAs.Text = "Save &As...";
            this.menuSaveAs.Click += new System.EventHandler(this.MenuSaveAsClick);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(209, 6);
            // 
            // menuLoadFile
            // 
            this.menuLoadFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuLoadFile.Name = "menuLoadFile";
            this.menuLoadFile.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.O)));
            this.menuLoadFile.Size = new System.Drawing.Size(212, 22);
            this.menuLoadFile.Text = "Load File...";
            this.menuLoadFile.Click += new System.EventHandler(this.MenuLoadFileClick);
            // 
            // menuSaveFileAs
            // 
            this.menuSaveFileAs.Name = "menuSaveFileAs";
            this.menuSaveFileAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.menuSaveFileAs.Size = new System.Drawing.Size(212, 22);
            this.menuSaveFileAs.Text = "Save File As...";
            this.menuSaveFileAs.Click += new System.EventHandler(this.MenuSaveFileAsClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(209, 6);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(212, 22);
            this.menuExit.Text = "E&xit";
            this.menuExit.Click += new System.EventHandler(this.MenuExitClick);
            // 
            // editMenu
            // 
            this.editMenu.AutoSize = false;
            this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuUndo,
            this.menuRedo,
            this.toolStripSeparator6,
            this.menuFindReplace,
            this.menuFindNext,
            this.toolStripSeparator1,
            this.menuCut,
            this.menuCopy,
            this.menuPaste,
            this.toolStripSeparator7,
            this.menuSelectAll,
            this.menuAddTimeDate});
            this.editMenu.Name = "editMenu";
            this.editMenu.Size = new System.Drawing.Size(35, 20);
            this.editMenu.Text = "&Edit";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(189, 6);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(189, 6);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(189, 6);
            // 
            // menuSelectAll
            // 
            this.menuSelectAll.Name = "menuSelectAll";
            this.menuSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.menuSelectAll.Size = new System.Drawing.Size(192, 22);
            this.menuSelectAll.Text = "Select &All";
            this.menuSelectAll.Click += new System.EventHandler(this.menuSelectAll_Click);
            // 
            // menuAddTimeDate
            // 
            this.menuAddTimeDate.Name = "menuAddTimeDate";
            this.menuAddTimeDate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.menuAddTimeDate.Size = new System.Drawing.Size(192, 22);
            this.menuAddTimeDate.Text = "Time/Date";
            this.menuAddTimeDate.Click += new System.EventHandler(this.menuAddTimeDate_Click);
            // 
            // optionsMenu
            // 
            this.optionsMenu.AutoSize = false;
            this.optionsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuWordWrap,
            this.menuFont,
            this.menuPreferences,
            this.menuGoogleAccount,
            this.menuAlwaysOnTop});
            this.optionsMenu.Name = "optionsMenu";
            this.optionsMenu.Size = new System.Drawing.Size(54, 20);
            this.optionsMenu.Text = "&Options";
            // 
            // menuWordWrap
            // 
            this.menuWordWrap.Checked = true;
            this.menuWordWrap.CheckOnClick = true;
            this.menuWordWrap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuWordWrap.Name = "menuWordWrap";
            this.menuWordWrap.Size = new System.Drawing.Size(161, 22);
            this.menuWordWrap.Text = "Word Wrap";
            this.menuWordWrap.CheckedChanged += new System.EventHandler(this.MenuWordWrapCheckedChanged);
            // 
            // menuFont
            // 
            this.menuFont.Name = "menuFont";
            this.menuFont.Size = new System.Drawing.Size(161, 22);
            this.menuFont.Text = "Font...";
            this.menuFont.Click += new System.EventHandler(this.MenuFontClick);
            // 
            // menuPreferences
            // 
            this.menuPreferences.Name = "menuPreferences";
            this.menuPreferences.Size = new System.Drawing.Size(161, 22);
            this.menuPreferences.Text = "Preferences...";
            this.menuPreferences.Click += new System.EventHandler(this.MenuPreferencesClick);
            // 
            // menuGoogleAccount
            // 
            this.menuGoogleAccount.Name = "menuGoogleAccount";
            this.menuGoogleAccount.Size = new System.Drawing.Size(161, 22);
            this.menuGoogleAccount.Text = "Google Account...";
            this.menuGoogleAccount.Click += new System.EventHandler(this.MenuGoogleAccountClick);
            // 
            // menuAlwaysOnTop
            // 
            this.menuAlwaysOnTop.CheckOnClick = true;
            this.menuAlwaysOnTop.Name = "menuAlwaysOnTop";
            this.menuAlwaysOnTop.Size = new System.Drawing.Size(161, 22);
            this.menuAlwaysOnTop.Text = "Always On Top";
            this.menuAlwaysOnTop.CheckedChanged += new System.EventHandler(this.MenuAlwaysOnTopCheckedChanged);
            // 
            // helpMenu
            // 
            this.helpMenu.AutoSize = false;
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAbout});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(38, 20);
            this.helpMenu.Text = "&Help";
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(115, 22);
            this.menuAbout.Text = "&About...";
            this.menuAbout.Click += new System.EventHandler(this.MenuAboutClick);
            // 
            // statusBar
            // 
            this.statusBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.statusBar.Dock = System.Windows.Forms.DockStyle.None;
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusBar.Location = new System.Drawing.Point(711, 480);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(17, 22);
            this.statusBar.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Margin = new System.Windows.Forms.Padding(0, 7, 0, 1);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 14);
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabs.Location = new System.Drawing.Point(0, 25);
            this.tabs.Margin = new System.Windows.Forms.Padding(0);
            this.tabs.Name = "tabs";
            this.tabs.Padding = new System.Drawing.Point(7, 5);
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(728, 457);
            this.tabs.TabIndex = 4;
            this.tabs.TabStop = false;
            this.tabs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabs_KeyDown);
            this.tabs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabs_MouseClick);
            // 
            // menuTab
            // 
            this.menuTab.ImageScalingSize = new System.Drawing.Size(0, 0);
            this.menuTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCloseTab});
            this.menuTab.Name = "menuTab";
            this.menuTab.ShowImageMargin = false;
            this.menuTab.ShowItemToolTips = false;
            this.menuTab.Size = new System.Drawing.Size(139, 26);
            // 
            // menuCloseTab
            // 
            this.menuCloseTab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuCloseTab.MergeAction = System.Windows.Forms.MergeAction.Remove;
            this.menuCloseTab.Name = "menuCloseTab";
            this.menuCloseTab.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.menuCloseTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.menuCloseTab.Size = new System.Drawing.Size(138, 22);
            this.menuCloseTab.Text = "Close Tab";
            this.menuCloseTab.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.menuCloseTab.MouseUp += new System.Windows.Forms.MouseEventHandler(this.menuCloseTab_MouseUp);
            // 
            // BgWorkerStartService
            // 
            this.BgWorkerStartService.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorkerStartService_DoWork);
            this.BgWorkerStartService.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorkerStartService_RunWorkerCompleted);
            // 
            // BgWorkerGetAllItems
            // 
            this.BgWorkerGetAllItems.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorkerGetAllItems_DoWork);
            this.BgWorkerGetAllItems.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorkerGetAllItems_RunWorkerCompleted);
            // 
            // lblCaretPosition
            // 
            this.lblCaretPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCaretPosition.AutoSize = true;
            this.lblCaretPosition.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaretPosition.Location = new System.Drawing.Point(2, 487);
            this.lblCaretPosition.Name = "lblCaretPosition";
            this.lblCaretPosition.Size = new System.Drawing.Size(77, 13);
            this.lblCaretPosition.TabIndex = 7;
            this.lblCaretPosition.Text = "Line: 1 - Col: 1";
            // 
            // fontDialog
            // 
            this.fontDialog.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // printDocument
            // 
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocumentPrintPage);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(189, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(189, 6);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(189, 6);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.toolStripMenuItem9.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem9.Text = "Select &All";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.toolStripMenuItem10.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem10.Text = "Time/Date";
            // 
            // contextMenuEditor
            // 
            this.contextMenuEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEditorUndo,
            this.menuEditorRedo,
            this.toolStripSeparator8,
            this.menuEditorCut,
            this.menuEditorCopy,
            this.menuEditorPaste,
            this.toolStripSeparator11,
            this.menuEditorSelectAll,
            this.menuEditorTimeDate});
            this.contextMenuEditor.Name = "contextMenuEditor";
            this.contextMenuEditor.Size = new System.Drawing.Size(162, 170);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(158, 6);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(158, 6);
            // 
            // menuEditorSelectAll
            // 
            this.menuEditorSelectAll.Name = "menuEditorSelectAll";
            this.menuEditorSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.menuEditorSelectAll.Size = new System.Drawing.Size(161, 22);
            this.menuEditorSelectAll.Text = "Select &All";
            this.menuEditorSelectAll.Click += new System.EventHandler(this.menuSelectAll_Click);
            // 
            // menuEditorTimeDate
            // 
            this.menuEditorTimeDate.Name = "menuEditorTimeDate";
            this.menuEditorTimeDate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.menuEditorTimeDate.Size = new System.Drawing.Size(161, 22);
            this.menuEditorTimeDate.Text = "Time/Date";
            this.menuEditorTimeDate.Click += new System.EventHandler(this.menuAddTimeDate_Click);
            // 
            // pictStatusMinimal
            // 
            this.pictStatusMinimal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictStatusMinimal.BackColor = System.Drawing.SystemColors.Control;
            this.pictStatusMinimal.Image = global::Nocs.Properties.Resources.loaderMainMinimal2;
            this.pictStatusMinimal.Location = new System.Drawing.Point(669, 488);
            this.pictStatusMinimal.Name = "pictStatusMinimal";
            this.pictStatusMinimal.Size = new System.Drawing.Size(36, 12);
            this.pictStatusMinimal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictStatusMinimal.TabIndex = 8;
            this.pictStatusMinimal.TabStop = false;
            this.pictStatusMinimal.Visible = false;
            // 
            // menuNew
            // 
            this.menuNew.Image = ((System.Drawing.Image)(resources.GetObject("menuNew.Image")));
            this.menuNew.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.menuNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuNew.Name = "menuNew";
            this.menuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuNew.Size = new System.Drawing.Size(212, 22);
            this.menuNew.Text = "&New";
            this.menuNew.Click += new System.EventHandler(this.MenuNewClick);
            // 
            // menuBrowse
            // 
            this.menuBrowse.Image = ((System.Drawing.Image)(resources.GetObject("menuBrowse.Image")));
            this.menuBrowse.ImageTransparentColor = System.Drawing.Color.White;
            this.menuBrowse.Name = "menuBrowse";
            this.menuBrowse.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuBrowse.Size = new System.Drawing.Size(212, 22);
            this.menuBrowse.Text = "&Browse Google Docs";
            this.menuBrowse.Click += new System.EventHandler(this.MenuBrowseClick);
            // 
            // menuSave
            // 
            this.menuSave.Image = ((System.Drawing.Image)(resources.GetObject("menuSave.Image")));
            this.menuSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuSave.Name = "menuSave";
            this.menuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuSave.Size = new System.Drawing.Size(212, 22);
            this.menuSave.Text = "&Save";
            this.menuSave.Click += new System.EventHandler(this.MenuSaveClick);
            // 
            // menuUndo
            // 
            this.menuUndo.Image = ((System.Drawing.Image)(resources.GetObject("menuUndo.Image")));
            this.menuUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuUndo.Name = "menuUndo";
            this.menuUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.menuUndo.Size = new System.Drawing.Size(192, 22);
            this.menuUndo.Text = "&Undo";
            this.menuUndo.Click += new System.EventHandler(this.menuUndo_Click);
            // 
            // menuRedo
            // 
            this.menuRedo.Image = ((System.Drawing.Image)(resources.GetObject("menuRedo.Image")));
            this.menuRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuRedo.Name = "menuRedo";
            this.menuRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.menuRedo.Size = new System.Drawing.Size(192, 22);
            this.menuRedo.Text = "&Redo";
            this.menuRedo.Click += new System.EventHandler(this.menuRedo_Click);
            // 
            // menuFindReplace
            // 
            this.menuFindReplace.Image = ((System.Drawing.Image)(resources.GetObject("menuFindReplace.Image")));
            this.menuFindReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuFindReplace.Name = "menuFindReplace";
            this.menuFindReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.menuFindReplace.Size = new System.Drawing.Size(192, 22);
            this.menuFindReplace.Text = "Find / Replace...";
            this.menuFindReplace.Click += new System.EventHandler(this.menuFindReplace_Click);
            // 
            // menuFindNext
            // 
            this.menuFindNext.Image = ((System.Drawing.Image)(resources.GetObject("menuFindNext.Image")));
            this.menuFindNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuFindNext.Name = "menuFindNext";
            this.menuFindNext.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.menuFindNext.Size = new System.Drawing.Size(192, 22);
            this.menuFindNext.Text = "Find Next";
            this.menuFindNext.Click += new System.EventHandler(this.menuFindNext_Click);
            // 
            // menuCut
            // 
            this.menuCut.Image = ((System.Drawing.Image)(resources.GetObject("menuCut.Image")));
            this.menuCut.ImageTransparentColor = System.Drawing.Color.White;
            this.menuCut.Name = "menuCut";
            this.menuCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.menuCut.Size = new System.Drawing.Size(192, 22);
            this.menuCut.Text = "Cu&t";
            this.menuCut.Click += new System.EventHandler(this.menuCut_Click);
            // 
            // menuCopy
            // 
            this.menuCopy.Image = ((System.Drawing.Image)(resources.GetObject("menuCopy.Image")));
            this.menuCopy.ImageTransparentColor = System.Drawing.Color.White;
            this.menuCopy.Name = "menuCopy";
            this.menuCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menuCopy.Size = new System.Drawing.Size(192, 22);
            this.menuCopy.Text = "&Copy";
            this.menuCopy.Click += new System.EventHandler(this.menuCopy_Click);
            // 
            // menuPaste
            // 
            this.menuPaste.Image = ((System.Drawing.Image)(resources.GetObject("menuPaste.Image")));
            this.menuPaste.ImageTransparentColor = System.Drawing.Color.White;
            this.menuPaste.Name = "menuPaste";
            this.menuPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.menuPaste.Size = new System.Drawing.Size(192, 22);
            this.menuPaste.Text = "&Paste";
            this.menuPaste.Click += new System.EventHandler(this.menuPaste_Click);
            // 
            // menuEditorUndo
            // 
            this.menuEditorUndo.Image = ((System.Drawing.Image)(resources.GetObject("menuEditorUndo.Image")));
            this.menuEditorUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuEditorUndo.Name = "menuEditorUndo";
            this.menuEditorUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.menuEditorUndo.Size = new System.Drawing.Size(161, 22);
            this.menuEditorUndo.Text = "&Undo";
            this.menuEditorUndo.Click += new System.EventHandler(this.menuUndo_Click);
            // 
            // menuEditorRedo
            // 
            this.menuEditorRedo.Image = ((System.Drawing.Image)(resources.GetObject("menuEditorRedo.Image")));
            this.menuEditorRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuEditorRedo.Name = "menuEditorRedo";
            this.menuEditorRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.menuEditorRedo.Size = new System.Drawing.Size(161, 22);
            this.menuEditorRedo.Text = "&Redo";
            this.menuEditorRedo.Click += new System.EventHandler(this.menuRedo_Click);
            // 
            // menuEditorCut
            // 
            this.menuEditorCut.Image = ((System.Drawing.Image)(resources.GetObject("menuEditorCut.Image")));
            this.menuEditorCut.ImageTransparentColor = System.Drawing.Color.White;
            this.menuEditorCut.Name = "menuEditorCut";
            this.menuEditorCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.menuEditorCut.Size = new System.Drawing.Size(161, 22);
            this.menuEditorCut.Text = "Cu&t";
            this.menuEditorCut.Click += new System.EventHandler(this.menuCut_Click);
            // 
            // menuEditorCopy
            // 
            this.menuEditorCopy.Image = ((System.Drawing.Image)(resources.GetObject("menuEditorCopy.Image")));
            this.menuEditorCopy.ImageTransparentColor = System.Drawing.Color.White;
            this.menuEditorCopy.Name = "menuEditorCopy";
            this.menuEditorCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menuEditorCopy.Size = new System.Drawing.Size(161, 22);
            this.menuEditorCopy.Text = "&Copy";
            this.menuEditorCopy.Click += new System.EventHandler(this.menuCopy_Click);
            // 
            // menuEditorPaste
            // 
            this.menuEditorPaste.Image = ((System.Drawing.Image)(resources.GetObject("menuEditorPaste.Image")));
            this.menuEditorPaste.ImageTransparentColor = System.Drawing.Color.White;
            this.menuEditorPaste.Name = "menuEditorPaste";
            this.menuEditorPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.menuEditorPaste.Size = new System.Drawing.Size(161, 22);
            this.menuEditorPaste.Text = "&Paste";
            this.menuEditorPaste.Click += new System.EventHandler(this.menuPaste_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
            this.toolStripMenuItem2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.toolStripMenuItem2.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem2.Text = "&Undo";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem3.Image")));
            this.toolStripMenuItem3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.toolStripMenuItem3.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem3.Text = "&Redo";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem4.Image")));
            this.toolStripMenuItem4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.toolStripMenuItem4.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem4.Text = "Find / Replace...";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem5.Image")));
            this.toolStripMenuItem5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.toolStripMenuItem5.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem5.Text = "Find Next";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem6.Image")));
            this.toolStripMenuItem6.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.toolStripMenuItem6.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem6.Text = "Cu&t";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem7.Image")));
            this.toolStripMenuItem7.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.toolStripMenuItem7.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem7.Text = "&Copy";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem8.Image")));
            this.toolStripMenuItem8.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.toolStripMenuItem8.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem8.Text = "&Paste";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 503);
            this.Controls.Add(this.lblCaretPosition);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.pictStatusMinimal);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.statusBar);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Main";
            this.Text = " Untitled - Nocs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_FormLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.menuTab.ResumeLayout(false);
            this.contextMenuEditor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem menuNew;
        private System.Windows.Forms.ToolStripMenuItem menuBrowse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem menuLoadFile;
        private System.Windows.Forms.ToolStripMenuItem menuSaveFileAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem editMenu;
        private System.Windows.Forms.ToolStripMenuItem menuUndo;
        private System.Windows.Forms.ToolStripMenuItem menuRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem menuFindReplace;
        private System.Windows.Forms.ToolStripMenuItem menuFindNext;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuCut;
        private System.Windows.Forms.ToolStripMenuItem menuCopy;
        private System.Windows.Forms.ToolStripMenuItem menuPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem menuSelectAll;
        private System.Windows.Forms.ToolStripMenuItem menuAddTimeDate;
        private System.Windows.Forms.ToolStripMenuItem optionsMenu;
        private System.Windows.Forms.ToolStripMenuItem menuWordWrap;
        private System.Windows.Forms.ToolStripMenuItem menuPreferences;
        private System.Windows.Forms.ToolStripMenuItem menuFont;
        private System.Windows.Forms.ToolStripMenuItem menuAlwaysOnTop;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.ContextMenuStrip menuTab;
        private System.Windows.Forms.ToolStripMenuItem menuCloseTab;
        private System.Windows.Forms.ToolStripMenuItem menuGoogleAccount;
        private System.ComponentModel.BackgroundWorker BgWorkerStartService;
        private System.ComponentModel.BackgroundWorker BgWorkerGetAllItems;
        private System.Windows.Forms.Label lblCaretPosition;
        private System.Windows.Forms.PictureBox pictStatusMinimal;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ContextMenuStrip contextMenuEditor;
        private System.Windows.Forms.ToolStripMenuItem menuEditorUndo;
        private System.Windows.Forms.ToolStripMenuItem menuEditorRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem menuEditorCut;
        private System.Windows.Forms.ToolStripMenuItem menuEditorCopy;
        private System.Windows.Forms.ToolStripMenuItem menuEditorPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem menuEditorSelectAll;
        private System.Windows.Forms.ToolStripMenuItem menuEditorTimeDate;
    }
}