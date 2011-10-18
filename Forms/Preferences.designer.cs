namespace Nocs.Forms
{
    partial class Preferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Preferences));
            this.chkAutoSave = new System.Windows.Forms.CheckBox();
            this.grpGeneral = new System.Windows.Forms.GroupBox();
            this.lblDefaultFolder = new System.Windows.Forms.Label();
            this.cmbDefaultFolder = new System.Windows.Forms.ComboBox();
            this.rdByDate = new System.Windows.Forms.RadioButton();
            this.lblSortDocuments = new System.Windows.Forms.Label();
            this.rdByTitle = new System.Windows.Forms.RadioButton();
            this.lblStatusBarStyle = new System.Windows.Forms.Label();
            this.cmbStatusBar = new System.Windows.Forms.ComboBox();
            this.lblAutoSaveTimeout2 = new System.Windows.Forms.Label();
            this.txtAutoSaveTimeout = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpProxy = new System.Windows.Forms.GroupBox();
            this.pnlManualProxySettings = new System.Windows.Forms.Panel();
            this.lblProxyPassword = new System.Windows.Forms.Label();
            this.txtProxyUsername = new System.Windows.Forms.TextBox();
            this.lblProxyUsername = new System.Windows.Forms.Label();
            this.txtProxyPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProxyPort = new System.Windows.Forms.TextBox();
            this.txtProxyHost = new System.Windows.Forms.TextBox();
            this.lblProxyHttp = new System.Windows.Forms.Label();
            this.rdUseManualProxy = new System.Windows.Forms.RadioButton();
            this.rdUseDefaultProxy = new System.Windows.Forms.RadioButton();
            this.chkUseProxy = new System.Windows.Forms.CheckBox();
            this.grpGeneral.SuspendLayout();
            this.grpProxy.SuspendLayout();
            this.pnlManualProxySettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkAutoSave
            // 
            this.chkAutoSave.AutoSize = true;
            this.chkAutoSave.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkAutoSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoSave.Location = new System.Drawing.Point(12, 89);
            this.chkAutoSave.Name = "chkAutoSave";
            this.chkAutoSave.Size = new System.Drawing.Size(136, 17);
            this.chkAutoSave.TabIndex = 4;
            this.chkAutoSave.Text = "Auto-save files every: ";
            this.chkAutoSave.UseVisualStyleBackColor = true;
            this.chkAutoSave.CheckedChanged += new System.EventHandler(this.chkAutoSave_CheckedChanged);
            // 
            // grpGeneral
            // 
            this.grpGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGeneral.Controls.Add(this.lblDefaultFolder);
            this.grpGeneral.Controls.Add(this.cmbDefaultFolder);
            this.grpGeneral.Controls.Add(this.rdByDate);
            this.grpGeneral.Controls.Add(this.lblSortDocuments);
            this.grpGeneral.Controls.Add(this.rdByTitle);
            this.grpGeneral.Controls.Add(this.lblStatusBarStyle);
            this.grpGeneral.Controls.Add(this.cmbStatusBar);
            this.grpGeneral.Controls.Add(this.lblAutoSaveTimeout2);
            this.grpGeneral.Controls.Add(this.txtAutoSaveTimeout);
            this.grpGeneral.Controls.Add(this.chkAutoSave);
            this.grpGeneral.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpGeneral.Location = new System.Drawing.Point(10, 8);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(292, 153);
            this.grpGeneral.TabIndex = 2;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // lblDefaultFolder
            // 
            this.lblDefaultFolder.AutoSize = true;
            this.lblDefaultFolder.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefaultFolder.Location = new System.Drawing.Point(9, 123);
            this.lblDefaultFolder.Name = "lblDefaultFolder";
            this.lblDefaultFolder.Size = new System.Drawing.Size(77, 13);
            this.lblDefaultFolder.TabIndex = 10;
            this.lblDefaultFolder.Text = "Default folder:";
            // 
            // cmbDefaultFolder
            // 
            this.cmbDefaultFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDefaultFolder.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDefaultFolder.FormattingEnabled = true;
            this.cmbDefaultFolder.Location = new System.Drawing.Point(104, 120);
            this.cmbDefaultFolder.MaxDropDownItems = 100;
            this.cmbDefaultFolder.MaximumSize = new System.Drawing.Size(174, 0);
            this.cmbDefaultFolder.Name = "cmbDefaultFolder";
            this.cmbDefaultFolder.Size = new System.Drawing.Size(174, 21);
            this.cmbDefaultFolder.TabIndex = 9;
            this.cmbDefaultFolder.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.cmbDefaultFolder_Format);
            // 
            // rdByDate
            // 
            this.rdByDate.AutoSize = true;
            this.rdByDate.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rdByDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdByDate.Location = new System.Drawing.Point(171, 25);
            this.rdByDate.Name = "rdByDate";
            this.rdByDate.Size = new System.Drawing.Size(62, 17);
            this.rdByDate.TabIndex = 2;
            this.rdByDate.Text = "by date";
            this.rdByDate.UseVisualStyleBackColor = true;
            // 
            // lblSortDocuments
            // 
            this.lblSortDocuments.AutoSize = true;
            this.lblSortDocuments.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSortDocuments.Location = new System.Drawing.Point(9, 27);
            this.lblSortDocuments.Name = "lblSortDocuments";
            this.lblSortDocuments.Size = new System.Drawing.Size(86, 13);
            this.lblSortDocuments.TabIndex = 8;
            this.lblSortDocuments.Text = "Sort documents:";
            // 
            // rdByTitle
            // 
            this.rdByTitle.AutoSize = true;
            this.rdByTitle.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rdByTitle.Checked = true;
            this.rdByTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdByTitle.Location = new System.Drawing.Point(104, 25);
            this.rdByTitle.Name = "rdByTitle";
            this.rdByTitle.Size = new System.Drawing.Size(58, 17);
            this.rdByTitle.TabIndex = 1;
            this.rdByTitle.TabStop = true;
            this.rdByTitle.Text = "by title";
            this.rdByTitle.UseVisualStyleBackColor = true;
            // 
            // lblStatusBarStyle
            // 
            this.lblStatusBarStyle.AutoSize = true;
            this.lblStatusBarStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusBarStyle.Location = new System.Drawing.Point(9, 53);
            this.lblStatusBarStyle.Name = "lblStatusBarStyle";
            this.lblStatusBarStyle.Size = new System.Drawing.Size(61, 13);
            this.lblStatusBarStyle.TabIndex = 6;
            this.lblStatusBarStyle.Text = "Status bar:";
            // 
            // cmbStatusBar
            // 
            this.cmbStatusBar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusBar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatusBar.FormattingEnabled = true;
            this.cmbStatusBar.Items.AddRange(new object[] {
            "Text with color",
            "Black & white text",
            "Minimal"});
            this.cmbStatusBar.Location = new System.Drawing.Point(104, 50);
            this.cmbStatusBar.Name = "cmbStatusBar";
            this.cmbStatusBar.Size = new System.Drawing.Size(146, 21);
            this.cmbStatusBar.TabIndex = 3;
            // 
            // lblAutoSaveTimeout2
            // 
            this.lblAutoSaveTimeout2.AutoSize = true;
            this.lblAutoSaveTimeout2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutoSaveTimeout2.Location = new System.Drawing.Point(174, 92);
            this.lblAutoSaveTimeout2.Name = "lblAutoSaveTimeout2";
            this.lblAutoSaveTimeout2.Size = new System.Drawing.Size(79, 13);
            this.lblAutoSaveTimeout2.TabIndex = 4;
            this.lblAutoSaveTimeout2.Text = "(5-60 seconds)";
            // 
            // txtAutoSaveTimeout
            // 
            this.txtAutoSaveTimeout.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAutoSaveTimeout.Location = new System.Drawing.Point(145, 88);
            this.txtAutoSaveTimeout.Name = "txtAutoSaveTimeout";
            this.txtAutoSaveTimeout.Size = new System.Drawing.Size(25, 21);
            this.txtAutoSaveTimeout.TabIndex = 5;
            this.txtAutoSaveTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAutoSaveTimeout.Leave += new System.EventHandler(this.txtAutoSaveTimeout_Leave);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(0)))));
            this.btnSave.Location = new System.Drawing.Point(170, 371);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(63, 25);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(239, 371);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(63, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grpProxy
            // 
            this.grpProxy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpProxy.Controls.Add(this.pnlManualProxySettings);
            this.grpProxy.Controls.Add(this.rdUseManualProxy);
            this.grpProxy.Controls.Add(this.rdUseDefaultProxy);
            this.grpProxy.Controls.Add(this.chkUseProxy);
            this.grpProxy.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpProxy.Location = new System.Drawing.Point(10, 171);
            this.grpProxy.Name = "grpProxy";
            this.grpProxy.Size = new System.Drawing.Size(292, 190);
            this.grpProxy.TabIndex = 10;
            this.grpProxy.TabStop = false;
            this.grpProxy.Text = "Proxy";
            // 
            // pnlManualProxySettings
            // 
            this.pnlManualProxySettings.Controls.Add(this.lblProxyPassword);
            this.pnlManualProxySettings.Controls.Add(this.txtProxyUsername);
            this.pnlManualProxySettings.Controls.Add(this.lblProxyUsername);
            this.pnlManualProxySettings.Controls.Add(this.txtProxyPassword);
            this.pnlManualProxySettings.Controls.Add(this.label2);
            this.pnlManualProxySettings.Controls.Add(this.txtProxyPort);
            this.pnlManualProxySettings.Controls.Add(this.txtProxyHost);
            this.pnlManualProxySettings.Controls.Add(this.lblProxyHttp);
            this.pnlManualProxySettings.Enabled = false;
            this.pnlManualProxySettings.Location = new System.Drawing.Point(6, 95);
            this.pnlManualProxySettings.Name = "pnlManualProxySettings";
            this.pnlManualProxySettings.Size = new System.Drawing.Size(280, 85);
            this.pnlManualProxySettings.TabIndex = 13;
            // 
            // lblProxyPassword
            // 
            this.lblProxyPassword.AutoSize = true;
            this.lblProxyPassword.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProxyPassword.Location = new System.Drawing.Point(74, 63);
            this.lblProxyPassword.Name = "lblProxyPassword";
            this.lblProxyPassword.Size = new System.Drawing.Size(57, 13);
            this.lblProxyPassword.TabIndex = 11;
            this.lblProxyPassword.Text = "Password:";
            // 
            // txtProxyUsername
            // 
            this.txtProxyUsername.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProxyUsername.Location = new System.Drawing.Point(133, 32);
            this.txtProxyUsername.Name = "txtProxyUsername";
            this.txtProxyUsername.Size = new System.Drawing.Size(142, 21);
            this.txtProxyUsername.TabIndex = 11;
            // 
            // lblProxyUsername
            // 
            this.lblProxyUsername.AutoSize = true;
            this.lblProxyUsername.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProxyUsername.Location = new System.Drawing.Point(74, 35);
            this.lblProxyUsername.Name = "lblProxyUsername";
            this.lblProxyUsername.Size = new System.Drawing.Size(59, 13);
            this.lblProxyUsername.TabIndex = 10;
            this.lblProxyUsername.Text = "Username:";
            // 
            // txtProxyPassword
            // 
            this.txtProxyPassword.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProxyPassword.Location = new System.Drawing.Point(133, 60);
            this.txtProxyPassword.Name = "txtProxyPassword";
            this.txtProxyPassword.Size = new System.Drawing.Size(142, 21);
            this.txtProxyPassword.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(227, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(8, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = ":";
            // 
            // txtProxyPort
            // 
            this.txtProxyPort.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProxyPort.Location = new System.Drawing.Point(237, 3);
            this.txtProxyPort.Name = "txtProxyPort";
            this.txtProxyPort.Size = new System.Drawing.Size(38, 21);
            this.txtProxyPort.TabIndex = 10;
            this.txtProxyPort.Text = "80";
            this.txtProxyPort.Leave += new System.EventHandler(this.txtProxyPort_Leave);
            // 
            // txtProxyHost
            // 
            this.txtProxyHost.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProxyHost.Location = new System.Drawing.Point(78, 3);
            this.txtProxyHost.Name = "txtProxyHost";
            this.txtProxyHost.Size = new System.Drawing.Size(148, 21);
            this.txtProxyHost.TabIndex = 9;
            this.txtProxyHost.Leave += new System.EventHandler(this.txtProxyHost_Leave);
            // 
            // lblProxyHttp
            // 
            this.lblProxyHttp.AutoSize = true;
            this.lblProxyHttp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProxyHttp.Location = new System.Drawing.Point(39, 6);
            this.lblProxyHttp.Name = "lblProxyHttp";
            this.lblProxyHttp.Size = new System.Drawing.Size(39, 13);
            this.lblProxyHttp.TabIndex = 7;
            this.lblProxyHttp.Text = "http://";
            // 
            // rdUseManualProxy
            // 
            this.rdUseManualProxy.AutoSize = true;
            this.rdUseManualProxy.Enabled = false;
            this.rdUseManualProxy.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdUseManualProxy.Location = new System.Drawing.Point(33, 75);
            this.rdUseManualProxy.Name = "rdUseManualProxy";
            this.rdUseManualProxy.Size = new System.Drawing.Size(152, 17);
            this.rdUseManualProxy.TabIndex = 8;
            this.rdUseManualProxy.Text = "Use manual proxy settings";
            this.rdUseManualProxy.UseVisualStyleBackColor = true;
            this.rdUseManualProxy.CheckedChanged += new System.EventHandler(this.rdUseManualProxy_CheckedChanged);
            // 
            // rdUseDefaultProxy
            // 
            this.rdUseDefaultProxy.AutoSize = true;
            this.rdUseDefaultProxy.Checked = true;
            this.rdUseDefaultProxy.Enabled = false;
            this.rdUseDefaultProxy.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdUseDefaultProxy.Location = new System.Drawing.Point(33, 53);
            this.rdUseDefaultProxy.Name = "rdUseDefaultProxy";
            this.rdUseDefaultProxy.Size = new System.Drawing.Size(152, 17);
            this.rdUseDefaultProxy.TabIndex = 7;
            this.rdUseDefaultProxy.TabStop = true;
            this.rdUseDefaultProxy.Text = "Use default proxy settings";
            this.rdUseDefaultProxy.UseVisualStyleBackColor = true;
            // 
            // chkUseProxy
            // 
            this.chkUseProxy.AutoSize = true;
            this.chkUseProxy.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkUseProxy.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUseProxy.Location = new System.Drawing.Point(12, 30);
            this.chkUseProxy.Name = "chkUseProxy";
            this.chkUseProxy.Size = new System.Drawing.Size(89, 17);
            this.chkUseProxy.TabIndex = 6;
            this.chkUseProxy.Text = "Enable proxy";
            this.chkUseProxy.UseVisualStyleBackColor = true;
            this.chkUseProxy.CheckedChanged += new System.EventHandler(this.chkUseProxy_CheckedChanged);
            // 
            // Preferences
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(312, 405);
            this.Controls.Add(this.grpProxy);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpGeneral);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Preferences";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " Preferences";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Preferences_Load);
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            this.grpProxy.ResumeLayout(false);
            this.grpProxy.PerformLayout();
            this.pnlManualProxySettings.ResumeLayout(false);
            this.pnlManualProxySettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkAutoSave;
        private System.Windows.Forms.GroupBox grpGeneral;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtAutoSaveTimeout;
        private System.Windows.Forms.Label lblAutoSaveTimeout2;
        private System.Windows.Forms.ComboBox cmbStatusBar;
        private System.Windows.Forms.Label lblStatusBarStyle;
        private System.Windows.Forms.Label lblSortDocuments;
        private System.Windows.Forms.RadioButton rdByTitle;
        private System.Windows.Forms.RadioButton rdByDate;
        private System.Windows.Forms.GroupBox grpProxy;
        private System.Windows.Forms.CheckBox chkUseProxy;
        private System.Windows.Forms.TextBox txtProxyPort;
        private System.Windows.Forms.TextBox txtProxyPassword;
        private System.Windows.Forms.TextBox txtProxyUsername;
        private System.Windows.Forms.RadioButton rdUseManualProxy;
        private System.Windows.Forms.RadioButton rdUseDefaultProxy;
        private System.Windows.Forms.Label lblProxyPassword;
        private System.Windows.Forms.Label lblProxyUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProxyHost;
        private System.Windows.Forms.Label lblProxyHttp;
        private System.Windows.Forms.Panel pnlManualProxySettings;
        private System.Windows.Forms.Label lblDefaultFolder;
        private System.Windows.Forms.ComboBox cmbDefaultFolder;
    }
}