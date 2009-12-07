namespace Nocs.Forms
{
    partial class FindReplaceDialog
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
            this.lblFindWhat = new System.Windows.Forms.Label();
            this.lblReplaceWith = new System.Windows.Forms.Label();
            this.btnFindNext = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnReplaceAll = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.chbRxp = new System.Windows.Forms.CheckBox();
            this.chbCaseSensitive = new System.Windows.Forms.CheckBox();
            this.txtFindWhat = new System.Windows.Forms.TextBox();
            this.txtReplaceWith = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblFindWhat
            // 
            this.lblFindWhat.AutoSize = true;
            this.lblFindWhat.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFindWhat.Location = new System.Drawing.Point(24, 15);
            this.lblFindWhat.Name = "lblFindWhat";
            this.lblFindWhat.Size = new System.Drawing.Size(58, 13);
            this.lblFindWhat.TabIndex = 2;
            this.lblFindWhat.Text = "Find what:";
            // 
            // lblReplaceWith
            // 
            this.lblReplaceWith.AutoSize = true;
            this.lblReplaceWith.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReplaceWith.Location = new System.Drawing.Point(10, 48);
            this.lblReplaceWith.Name = "lblReplaceWith";
            this.lblReplaceWith.Size = new System.Drawing.Size(72, 13);
            this.lblReplaceWith.TabIndex = 3;
            this.lblReplaceWith.Text = "Replace with:";
            // 
            // btnFindNext
            // 
            this.btnFindNext.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindNext.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnFindNext.Location = new System.Drawing.Point(340, 10);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(83, 26);
            this.btnFindNext.TabIndex = 6;
            this.btnFindNext.Text = "Find Next";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.btnFindNext_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReplace.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnReplace.Location = new System.Drawing.Point(340, 46);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(83, 23);
            this.btnReplace.TabIndex = 7;
            this.btnReplace.Text = "Replace";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnReplaceAll
            // 
            this.btnReplaceAll.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReplaceAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnReplaceAll.Location = new System.Drawing.Point(340, 75);
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.Size = new System.Drawing.Size(83, 23);
            this.btnReplaceAll.TabIndex = 8;
            this.btnReplaceAll.Text = "Replace All";
            this.btnReplaceAll.UseVisualStyleBackColor = true;
            this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(340, 109);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(83, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chbRxp
            // 
            this.chbRxp.AutoSize = true;
            this.chbRxp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbRxp.Location = new System.Drawing.Point(89, 83);
            this.chbRxp.Name = "chbRxp";
            this.chbRxp.Size = new System.Drawing.Size(144, 17);
            this.chbRxp.TabIndex = 4;
            this.chbRxp.Text = "Use Regular Expressions";
            this.chbRxp.UseVisualStyleBackColor = true;
            // 
            // chbCaseSensitive
            // 
            this.chbCaseSensitive.AutoSize = true;
            this.chbCaseSensitive.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbCaseSensitive.Location = new System.Drawing.Point(89, 106);
            this.chbCaseSensitive.Name = "chbCaseSensitive";
            this.chbCaseSensitive.Size = new System.Drawing.Size(97, 17);
            this.chbCaseSensitive.TabIndex = 5;
            this.chbCaseSensitive.Text = "Case-Sensitive";
            this.chbCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // txtFindWhat
            // 
            this.txtFindWhat.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFindWhat.Location = new System.Drawing.Point(85, 12);
            this.txtFindWhat.Name = "txtFindWhat";
            this.txtFindWhat.Size = new System.Drawing.Size(234, 21);
            this.txtFindWhat.TabIndex = 0;
            // 
            // txtReplaceWith
            // 
            this.txtReplaceWith.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReplaceWith.Location = new System.Drawing.Point(85, 45);
            this.txtReplaceWith.Name = "txtReplaceWith";
            this.txtReplaceWith.Size = new System.Drawing.Size(234, 21);
            this.txtReplaceWith.TabIndex = 1;
            // 
            // FindReplaceDialog
            // 
            this.AcceptButton = this.btnFindNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(436, 145);
            this.Controls.Add(this.txtReplaceWith);
            this.Controls.Add(this.txtFindWhat);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnReplaceAll);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.btnFindNext);
            this.Controls.Add(this.chbCaseSensitive);
            this.Controls.Add(this.chbRxp);
            this.Controls.Add(this.lblReplaceWith);
            this.Controls.Add(this.lblFindWhat);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindReplaceDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Find / Replace";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.FindReplaceDialog_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFindWhat;
        private System.Windows.Forms.Label lblReplaceWith;
        private System.Windows.Forms.Button btnFindNext;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnReplaceAll;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtReplaceWith;
        public System.Windows.Forms.CheckBox chbRxp;
        public System.Windows.Forms.CheckBox chbCaseSensitive;
        public System.Windows.Forms.TextBox txtFindWhat;
    }
}