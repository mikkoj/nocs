using System;
using System.Windows.Forms;
using Nocs.Helpers;


namespace Nocs.Forms
{
    public partial class FindReplaceDialog : Form
    {
        // reference for the main form so we can access its methods/properties
        private readonly RichTextBox _currentTextBox;

        public FindReplaceDialog()
        {
            InitializeComponent();
        }
        
        public FindReplaceDialog(RichTextBox currentTextBox)
        {
            _currentTextBox = currentTextBox;
            InitializeComponent();
        }


        #region Control Event Handlers

        private void FindReplaceDialog_Activated(object sender, EventArgs e)
        {
            // focus the field for search string
            txtFindWhat.Focus();
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            // use Tools.FindNext to search for text with the given options
            RegexHelpers.FindNext(txtFindWhat.Text, chbCaseSensitive.Checked, _currentTextBox, chbRxp.Checked);
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            // use Tools.Replace to replace text with the given options
            RegexHelpers.Replace(txtFindWhat.Text, txtReplaceWith.Text, chbCaseSensitive.Checked, _currentTextBox, chbRxp.Checked);
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            // use Tools.ReplaceAll to replace all occurrences of a string
            RegexHelpers.ReplaceAll(txtFindWhat.Text, txtReplaceWith.Text, chbCaseSensitive.Checked, _currentTextBox, chbRxp.Checked);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // close the Find & Replace dialog
            Close();
        }

        #endregion
    }
}