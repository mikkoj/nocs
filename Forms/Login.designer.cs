namespace Nocs.Forms
{
    partial class Login
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
            this.lblGUser = new System.Windows.Forms.Label();
            this.txtGUser = new System.Windows.Forms.TextBox();
            this.lblGPassword = new System.Windows.Forms.Label();
            this.txtGPassword = new System.Windows.Forms.TextBox();
            this.btnValidateOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.bgWorker_Validate = new System.ComponentModel.BackgroundWorker();
            this.lblValidateInfo = new System.Windows.Forms.Label();
            this.chkSaveGoogleAccount = new System.Windows.Forms.CheckBox();
            this.boxValidating = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.boxValidating)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGUser
            // 
            this.lblGUser.AutoSize = true;
            this.lblGUser.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGUser.Location = new System.Drawing.Point(12, 15);
            this.lblGUser.Name = "lblGUser";
            this.lblGUser.Size = new System.Drawing.Size(95, 13);
            this.lblGUser.TabIndex = 0;
            this.lblGUser.Text = "Google Username:";
            // 
            // txtGUser
            // 
            this.txtGUser.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtGUser.Location = new System.Drawing.Point(114, 12);
            this.txtGUser.Name = "txtGUser";
            this.txtGUser.Size = new System.Drawing.Size(192, 21);
            this.txtGUser.TabIndex = 1;
            this.txtGUser.TextChanged += new System.EventHandler(this.TxtGbUserTextChanged);
            // 
            // lblGPassword
            // 
            this.lblGPassword.AutoSize = true;
            this.lblGPassword.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGPassword.Location = new System.Drawing.Point(14, 46);
            this.lblGPassword.Name = "lblGPassword";
            this.lblGPassword.Size = new System.Drawing.Size(93, 13);
            this.lblGPassword.TabIndex = 2;
            this.lblGPassword.Text = "Google Password:";
            // 
            // txtGPassword
            // 
            this.txtGPassword.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtGPassword.Location = new System.Drawing.Point(114, 43);
            this.txtGPassword.Name = "txtGPassword";
            this.txtGPassword.PasswordChar = '*';
            this.txtGPassword.Size = new System.Drawing.Size(192, 21);
            this.txtGPassword.TabIndex = 2;
            this.txtGPassword.TextChanged += new System.EventHandler(this.TxtGbPasswordTextChanged);
            // 
            // btnValidateOK
            // 
            this.btnValidateOK.Enabled = false;
            this.btnValidateOK.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnValidateOK.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnValidateOK.Location = new System.Drawing.Point(152, 110);
            this.btnValidateOK.Name = "btnValidateOK";
            this.btnValidateOK.Size = new System.Drawing.Size(72, 25);
            this.btnValidateOK.TabIndex = 4;
            this.btnValidateOK.Text = "Validate";
            this.btnValidateOK.UseVisualStyleBackColor = true;
            this.btnValidateOK.Click += new System.EventHandler(this.BtnValidateOkClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(237, 110);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // bgWorker_Validate
            // 
            this.bgWorker_Validate.WorkerSupportsCancellation = true;
            this.bgWorker_Validate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorkerValidateDoWork);
            this.bgWorker_Validate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorkerValidateRunWorkerCompleted);
            // 
            // lblValidateInfo
            // 
            this.lblValidateInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblValidateInfo.AutoSize = true;
            this.lblValidateInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValidateInfo.ForeColor = System.Drawing.Color.Black;
            this.lblValidateInfo.Location = new System.Drawing.Point(54, 104);
            this.lblValidateInfo.MaximumSize = new System.Drawing.Size(80, 0);
            this.lblValidateInfo.Name = "lblValidateInfo";
            this.lblValidateInfo.Size = new System.Drawing.Size(0, 13);
            this.lblValidateInfo.TabIndex = 10;
            this.lblValidateInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkSaveGoogleAccount
            // 
            this.chkSaveGoogleAccount.AutoSize = true;
            this.chkSaveGoogleAccount.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkSaveGoogleAccount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSaveGoogleAccount.Location = new System.Drawing.Point(114, 74);
            this.chkSaveGoogleAccount.Name = "chkSaveGoogleAccount";
            this.chkSaveGoogleAccount.Size = new System.Drawing.Size(143, 17);
            this.chkSaveGoogleAccount.TabIndex = 3;
            this.chkSaveGoogleAccount.Text = "Remember my password";
            this.chkSaveGoogleAccount.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.chkSaveGoogleAccount.UseVisualStyleBackColor = true;
            // 
            // boxValidating
            // 
            this.boxValidating.BackColor = System.Drawing.SystemColors.Control;
            this.boxValidating.Location = new System.Drawing.Point(15, 101);
            this.boxValidating.Name = "boxValidating";
            this.boxValidating.Size = new System.Drawing.Size(31, 31);
            this.boxValidating.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.boxValidating.TabIndex = 9;
            this.boxValidating.TabStop = false;
            this.boxValidating.Visible = false;
            // 
            // Login
            // 
            this.AcceptButton = this.btnValidateOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 149);
            this.Controls.Add(this.chkSaveGoogleAccount);
            this.Controls.Add(this.lblValidateInfo);
            this.Controls.Add(this.boxValidating);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnValidateOK);
            this.Controls.Add(this.txtGPassword);
            this.Controls.Add(this.lblGPassword);
            this.Controls.Add(this.txtGUser);
            this.Controls.Add(this.lblGUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Login";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login to Google Docs";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.boxValidating)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGUser;
        private System.Windows.Forms.TextBox txtGUser;
        private System.Windows.Forms.Label lblGPassword;
        private System.Windows.Forms.TextBox txtGPassword;
        private System.Windows.Forms.Button btnValidateOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox boxValidating;
        private System.ComponentModel.BackgroundWorker bgWorker_Validate;
        private System.Windows.Forms.Label lblValidateInfo;
        private System.Windows.Forms.CheckBox chkSaveGoogleAccount;
    }
}