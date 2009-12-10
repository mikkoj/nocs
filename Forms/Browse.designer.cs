namespace Nocs.Forms
{
    partial class Browse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Browse));
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.BgWorkerRenameEntry = new System.ComponentModel.BackgroundWorker();
            this.BgWorkerDeleteEntry = new System.ComponentModel.BackgroundWorker();
            this.lblError = new System.Windows.Forms.Label();
            this.treeFolders = new System.Windows.Forms.TreeView();
            this.foldersImageList = new System.Windows.Forms.ImageList();
            this.contextMenuFolders = new System.Windows.Forms.ContextMenuStrip();
            this.menuRenameFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeleteFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.BgWorkerMoveEntry = new System.ComponentModel.BackgroundWorker();
            this.BgWorkerRemoveEntryFromAllFolders = new System.ComponentModel.BackgroundWorker();
            this.boxWorking = new System.Windows.Forms.PictureBox();
            this.BgWorkerCreateFolder = new System.ComponentModel.BackgroundWorker();
            this.contextMenuCreateFolder = new System.Windows.Forms.ContextMenuStrip();
            this.menuCreateFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuFolders.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.contextMenuCreateFolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnLoad.Location = new System.Drawing.Point(173, 272);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(56, 24);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnRename
            // 
            this.btnRename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRename.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRename.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRename.ForeColor = System.Drawing.Color.Green;
            this.btnRename.Location = new System.Drawing.Point(235, 272);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(57, 24);
            this.btnRename.TabIndex = 4;
            this.btnRename.Text = "Rename";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.DarkRed;
            this.btnDelete.Location = new System.Drawing.Point(298, 272);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(57, 24);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(361, 272);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(54, 24);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lstItems
            // 
            this.lstItems.AllowDrop = true;
            this.lstItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstItems.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstItems.FormattingEnabled = true;
            this.lstItems.IntegralHeight = false;
            this.lstItems.ItemHeight = 14;
            this.lstItems.Location = new System.Drawing.Point(0, 3);
            this.lstItems.Margin = new System.Windows.Forms.Padding(0);
            this.lstItems.Name = "lstItems";
            this.lstItems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstItems.Size = new System.Drawing.Size(242, 258);
            this.lstItems.Sorted = true;
            this.lstItems.TabIndex = 2;
            this.lstItems.SelectedIndexChanged += new System.EventHandler(this.lstItems_SelectedIndexChanged);
            this.lstItems.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.lstItems_Format);
            this.lstItems.DragOver += new System.Windows.Forms.DragEventHandler(this.lstItems_DragOver);
            this.lstItems.DoubleClick += new System.EventHandler(this.lstItems_DoubleClick);
            this.lstItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstItems_KeyDown);
            this.lstItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstItems_MouseDown);
            // 
            // BgWorkerRenameEntry
            // 
            this.BgWorkerRenameEntry.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorkerRenameEntry_DoWork);
            this.BgWorkerRenameEntry.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorkerRenameEntry_RunWorkerCompleted);
            // 
            // BgWorkerDeleteEntry
            // 
            this.BgWorkerDeleteEntry.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorkerDeleteEntry_DoWork);
            this.BgWorkerDeleteEntry.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorkerDeleteEntry_RunWorkerCompleted);
            // 
            // lblError
            // 
            this.lblError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.IndianRed;
            this.lblError.Location = new System.Drawing.Point(246, 276);
            this.lblError.MaximumSize = new System.Drawing.Size(80, 0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 13);
            this.lblError.TabIndex = 11;
            this.lblError.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // treeFolders
            // 
            this.treeFolders.AllowDrop = true;
            this.treeFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeFolders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeFolders.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeFolders.HideSelection = false;
            this.treeFolders.ImageIndex = 0;
            this.treeFolders.ImageList = this.foldersImageList;
            this.treeFolders.Indent = 5;
            this.treeFolders.Location = new System.Drawing.Point(3, 3);
            this.treeFolders.Margin = new System.Windows.Forms.Padding(0);
            this.treeFolders.Name = "treeFolders";
            this.treeFolders.SelectedImageIndex = 0;
            this.treeFolders.ShowLines = false;
            this.treeFolders.Size = new System.Drawing.Size(165, 258);
            this.treeFolders.TabIndex = 1;
            this.treeFolders.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeFolders_AfterCollapse);
            this.treeFolders.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeFolders_AfterExpand);
            this.treeFolders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeFolders_AfterSelect);
            this.treeFolders.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeFolders_DragDrop);
            this.treeFolders.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeFolders_DragEnter);
            this.treeFolders.DragOver += new System.Windows.Forms.DragEventHandler(this.treeFolders_DragOver);
            this.treeFolders.DragLeave += new System.EventHandler(this.treeFolders_DragLeave);
            this.treeFolders.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeFolders_MouseDown);
            // 
            // foldersImageList
            // 
            this.foldersImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("foldersImageList.ImageStream")));
            this.foldersImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.foldersImageList.Images.SetKeyName(0, "folder_closed_16x16.ico");
            this.foldersImageList.Images.SetKeyName(1, "folder_open.ico");
            // 
            // contextMenuFolders
            // 
            this.contextMenuFolders.AutoSize = false;
            this.contextMenuFolders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.contextMenuFolders.ImageScalingSize = new System.Drawing.Size(0, 0);
            this.contextMenuFolders.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRenameFolder,
            this.menuDeleteFolder});
            this.contextMenuFolders.Name = "contextMenuFolders";
            this.contextMenuFolders.ShowImageMargin = false;
            this.contextMenuFolders.ShowItemToolTips = false;
            this.contextMenuFolders.Size = new System.Drawing.Size(92, 48);
            // 
            // menuRenameFolder
            // 
            this.menuRenameFolder.AutoSize = false;
            this.menuRenameFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuRenameFolder.MergeAction = System.Windows.Forms.MergeAction.Remove;
            this.menuRenameFolder.Name = "menuRenameFolder";
            this.menuRenameFolder.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.menuRenameFolder.Size = new System.Drawing.Size(91, 22);
            this.menuRenameFolder.Text = "Rename folder";
            this.menuRenameFolder.Click += new System.EventHandler(this.menuRenameFolder_Click);
            // 
            // menuDeleteFolder
            // 
            this.menuDeleteFolder.AutoSize = false;
            this.menuDeleteFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuDeleteFolder.MergeAction = System.Windows.Forms.MergeAction.Remove;
            this.menuDeleteFolder.Name = "menuDeleteFolder";
            this.menuDeleteFolder.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.menuDeleteFolder.Size = new System.Drawing.Size(91, 22);
            this.menuDeleteFolder.Text = "Delete folder";
            this.menuDeleteFolder.Click += new System.EventHandler(this.menuDeleteFolder_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(0, 2);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.AllowDrop = true;
            this.splitContainer.Panel1.Controls.Add(this.treeFolders);
            this.splitContainer.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.splitContainer_Panel1_MouseDown);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.AllowDrop = true;
            this.splitContainer.Panel2.Controls.Add(this.lstItems);
            this.splitContainer.Size = new System.Drawing.Size(418, 264);
            this.splitContainer.SplitterDistance = 168;
            this.splitContainer.SplitterWidth = 5;
            this.splitContainer.TabIndex = 13;
            // 
            // BgWorkerMoveEntry
            // 
            this.BgWorkerMoveEntry.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorkerMoveEntry_DoWork);
            this.BgWorkerMoveEntry.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorkerMoveEntry_RunWorkerCompleted);
            // 
            // BgWorkerRemoveEntryFromAllFolders
            // 
            this.BgWorkerRemoveEntryFromAllFolders.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorkerRemoveEntryFromAllFolders_DoWork);
            this.BgWorkerRemoveEntryFromAllFolders.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorkerRemoveEntryFromAllFolders_RunWorkerCompleted);
            // 
            // boxWorking
            // 
            this.boxWorking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.boxWorking.BackColor = System.Drawing.SystemColors.Control;
            this.boxWorking.Image = ((System.Drawing.Image)(resources.GetObject("boxWorking.Image")));
            this.boxWorking.Location = new System.Drawing.Point(30, 276);
            this.boxWorking.Name = "boxWorking";
            this.boxWorking.Size = new System.Drawing.Size(110, 16);
            this.boxWorking.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.boxWorking.TabIndex = 10;
            this.boxWorking.TabStop = false;
            this.boxWorking.Visible = false;
            // 
            // BgWorkerCreateFolder
            // 
            this.BgWorkerCreateFolder.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorkerCreateFolder_DoWork);
            this.BgWorkerCreateFolder.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorkerCreateFolder_RunWorkerCompleted);
            // 
            // contextMenuCreateFolder
            // 
            this.contextMenuCreateFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.contextMenuCreateFolder.ImageScalingSize = new System.Drawing.Size(0, 0);
            this.contextMenuCreateFolder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCreateFolder});
            this.contextMenuCreateFolder.Name = "contextMenuCreateFolder";
            this.contextMenuCreateFolder.ShowImageMargin = false;
            this.contextMenuCreateFolder.ShowItemToolTips = false;
            this.contextMenuCreateFolder.Size = new System.Drawing.Size(128, 48);
            // 
            // menuCreateFolder
            // 
            this.menuCreateFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuCreateFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuCreateFolder.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuCreateFolder.MergeAction = System.Windows.Forms.MergeAction.Remove;
            this.menuCreateFolder.Name = "menuCreateFolder";
            this.menuCreateFolder.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
            this.menuCreateFolder.ShowShortcutKeys = false;
            this.menuCreateFolder.Size = new System.Drawing.Size(127, 22);
            this.menuCreateFolder.Text = "Create folder";
            this.menuCreateFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.menuCreateFolder.Click += new System.EventHandler(this.menuCreateFolder_Click);
            // 
            // Browse
            // 
            this.AcceptButton = this.btnLoad;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(418, 301);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.boxWorking);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnLoad);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 200);
            this.Name = "Browse";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " Documents";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Browse_FormClosing);
            this.Load += new System.EventHandler(this.Browse_FormLoad);
            this.contextMenuFolders.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.contextMenuCreateFolder.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListBox lstItems;
        private System.Windows.Forms.PictureBox boxWorking;
        private System.ComponentModel.BackgroundWorker BgWorkerRenameEntry;
        private System.ComponentModel.BackgroundWorker BgWorkerDeleteEntry;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.TreeView treeFolders;
        private System.Windows.Forms.ImageList foldersImageList;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ContextMenuStrip contextMenuFolders;
        private System.Windows.Forms.ToolStripMenuItem menuDeleteFolder;
        private System.Windows.Forms.ToolStripMenuItem menuRenameFolder;
        private System.ComponentModel.BackgroundWorker BgWorkerMoveEntry;
        private System.ComponentModel.BackgroundWorker BgWorkerRemoveEntryFromAllFolders;
        private System.ComponentModel.BackgroundWorker BgWorkerCreateFolder;
        private System.Windows.Forms.ContextMenuStrip contextMenuCreateFolder;
        private System.Windows.Forms.ToolStripMenuItem menuCreateFolder;
    }
}