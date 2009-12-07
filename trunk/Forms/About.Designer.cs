namespace Nocs.Forms
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.lblAbout = new System.Windows.Forms.Label();
            this.lblContact = new System.Windows.Forms.Label();
            this.lnkEmail = new System.Windows.Forms.LinkLabel();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.panelAbout = new System.Windows.Forms.Panel();
            this.lnkGoogleCode = new System.Windows.Forms.LinkLabel();
            this.lnkTwitter = new System.Windows.Forms.LinkLabel();
            this.lblGoogleCode = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAbout
            // 
            this.lblAbout.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAbout.AutoSize = true;
            this.lblAbout.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbout.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblAbout.Location = new System.Drawing.Point(9, 2);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(57, 22);
            this.lblAbout.TabIndex = 0;
            this.lblAbout.Text = "Nocs";
            // 
            // lblContact
            // 
            this.lblContact.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblContact.AutoSize = true;
            this.lblContact.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContact.Location = new System.Drawing.Point(11, 42);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(49, 13);
            this.lblContact.TabIndex = 3;
            this.lblContact.Text = "Contact:";
            // 
            // lnkEmail
            // 
            this.lnkEmail.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lnkEmail.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lnkEmail.BackColor = System.Drawing.SystemColors.Control;
            this.lnkEmail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkEmail.ForeColor = System.Drawing.Color.Blue;
            this.lnkEmail.LinkArea = new System.Windows.Forms.LinkArea(0, 23);
            this.lnkEmail.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkEmail.LinkColor = System.Drawing.Color.MediumBlue;
            this.lnkEmail.Location = new System.Drawing.Point(56, 41);
            this.lnkEmail.Name = "lnkEmail";
            this.lnkEmail.Size = new System.Drawing.Size(130, 15);
            this.lnkEmail.TabIndex = 4;
            this.lnkEmail.TabStop = true;
            this.lnkEmail.Text = "mikko.junnila@gmail.com";
            this.lnkEmail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lnkEmail.VisitedLinkColor = System.Drawing.Color.MediumBlue;
            this.lnkEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkContact_LinkClicked);
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(65, 9);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(29, 13);
            this.lblVersion.TabIndex = 5;
            this.lblVersion.Text = "v2.1";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(256, 129);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "Close";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panelAbout
            // 
            this.panelAbout.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelAbout.Controls.Add(this.lnkGoogleCode);
            this.panelAbout.Controls.Add(this.lnkTwitter);
            this.panelAbout.Controls.Add(this.lblGoogleCode);
            this.panelAbout.Controls.Add(this.lblAbout);
            this.panelAbout.Controls.Add(this.lblContact);
            this.panelAbout.Controls.Add(this.lblVersion);
            this.panelAbout.Controls.Add(this.lnkEmail);
            this.panelAbout.Location = new System.Drawing.Point(109, 11);
            this.panelAbout.Name = "panelAbout";
            this.panelAbout.Size = new System.Drawing.Size(217, 111);
            this.panelAbout.TabIndex = 8;
            // 
            // lnkGoogleCode
            // 
            this.lnkGoogleCode.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lnkGoogleCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkGoogleCode.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkGoogleCode.LinkColor = System.Drawing.Color.MediumBlue;
            this.lnkGoogleCode.Location = new System.Drawing.Point(58, 81);
            this.lnkGoogleCode.Name = "lnkGoogleCode";
            this.lnkGoogleCode.Size = new System.Drawing.Size(149, 13);
            this.lnkGoogleCode.TabIndex = 8;
            this.lnkGoogleCode.TabStop = true;
            this.lnkGoogleCode.Text = "http://nocs.googlecode.com/";
            this.lnkGoogleCode.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGoogleCode_LinkClicked);
            // 
            // lnkTwitter
            // 
            this.lnkTwitter.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lnkTwitter.AutoSize = true;
            this.lnkTwitter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkTwitter.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkTwitter.LinkColor = System.Drawing.Color.MediumBlue;
            this.lnkTwitter.Location = new System.Drawing.Point(58, 58);
            this.lnkTwitter.Name = "lnkTwitter";
            this.lnkTwitter.Size = new System.Drawing.Size(127, 13);
            this.lnkTwitter.TabIndex = 7;
            this.lnkTwitter.TabStop = true;
            this.lnkTwitter.Text = "http://twitter.com/mikkoj";
            this.lnkTwitter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTwitter_LinkClicked);
            // 
            // lblGoogleCode
            // 
            this.lblGoogleCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGoogleCode.AutoSize = true;
            this.lblGoogleCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoogleCode.Location = new System.Drawing.Point(15, 81);
            this.lblGoogleCode.Name = "lblGoogleCode";
            this.lblGoogleCode.Size = new System.Drawing.Size(48, 13);
            this.lblGoogleCode.TabIndex = 6;
            this.lblGoogleCode.Text = "Project: ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(96, 87);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // About
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(331, 159);
            this.Controls.Add(this.panelAbout);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "About";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " About";
            this.TopMost = true;
            this.panelAbout.ResumeLayout(false);
            this.panelAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblAbout;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.LinkLabel lnkEmail;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelAbout;
        private System.Windows.Forms.Label lblGoogleCode;
        private System.Windows.Forms.LinkLabel lnkTwitter;
        private System.Windows.Forms.LinkLabel lnkGoogleCode;

    }
}