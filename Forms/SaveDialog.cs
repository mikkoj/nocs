using System;
using System.Linq;
using System.Windows.Forms;

using Google.Documents;
using Nocs.Models;
using Nocs.Helpers;


namespace Nocs.Forms
{
    public partial class SaveDialog : Form
    {
        // basic function which has parameters for the dialog window title,
        // string to prompt the user for and the default value for the input field
        public SaveDialog()
        {
            InitializeComponent();
        }


        #region Form and Control Events

        private void InputBox_Load(object sender, EventArgs e)
        {
            // when the dialog loads, set its members' values with the properties of the current instance
            lblPrompt.Text = FormPrompt;
            Text = FormCaption;
            txtInput.SelectionStart = 0;
            txtInput.SelectionLength = txtInput.Text.Length;
            txtInput.Focus();
            btnOK.Enabled = false;
            Response = new SaveDialogResponse();

            Tools.PopulateFoldersForComboBox(cmbSaveFolder, true);
            
            // let's also check if we had to add the default folder
            if (cmbSaveFolder.Items.Cast<Document>().Any(doc => doc.Id == "draft" && doc.Title == "Nocs"))
            {
                Response.CreateDefaultFolder = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Response.DocumentName = txtInput.Text.Trim();
            if (!Response.CreateDefaultFolder)
            {
                var documentId = ((Document)cmbSaveFolder.SelectedItem).ResourceId;
                if (!string.IsNullOrEmpty(documentId))
                {
                    Response.Folder = ((Document)cmbSaveFolder.SelectedItem).ResourceId;
                }
                else
                {
                    Response.Folder = string.Empty;
                }
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // close the window
            Close();
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            // disable ok button whenever the field is empty or default value (Untitled)
            if (txtInput.Text.Trim() != string.Empty && txtInput.Text.Trim() != DefaultValue)
            {
                btnOK.Enabled = true;
            }
            else
            {
                btnOK.Enabled = false;
            }
        }

        private void cmbFolder_Format(object sender, ListControlConvertEventArgs e)
        {
            var folder = (Document)e.ListItem;
            e.Value = folder.Title;
        }

        #endregion


        public static SaveDialogResponse SaveDialogBox(string prompt, string title, string defaultInput)
        {
            // we'll create a new instance of the class and set the given arguments as properties
            var ib = new SaveDialog
            {
                FormPrompt = prompt,
                FormCaption = title,
                DefaultValue = defaultInput
            };

            // show the actual dialog
            ib.ShowDialog();

            // retrieve the response string, close the dialog and return the string
            var response = ib.Response;
            ib.Close();
            return response;
        }

    }
}