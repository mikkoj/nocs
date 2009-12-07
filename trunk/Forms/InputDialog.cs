using System;
using System.Windows.Forms;

namespace Nocs.Forms
{
    public partial class InputDialog : Form
    {
        // basic function which has parameters for the dialog window title,
        // string to prompt the user for and the default value for the input field
        public InputDialog()
        {
            InitializeComponent();
        }


        #region Form and Control Events

        private void InputBox_Load(object sender, EventArgs e)
        {
            // when the dialog loads, set its members' values with the properties of the current instance
            lblPrompt.Text = formPrompt;
            Text = formCaption;
            txtInput.SelectionStart = 0;
            txtInput.SelectionLength = txtInput.Text.Length;
            txtInput.Focus();
            btnOK.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            InputResponse = txtInput.Text.Trim();
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
            if (encrypt)
            {
                if (txtInput.Text.Trim() != string.Empty)
                {
                    btnOK.Enabled = true;
                }
                else
                {
                    btnOK.Enabled = false;
                }
            }
            else
            {
                if (txtInput.Text.Trim() != string.Empty && txtInput.Text.Trim() != defaultValue)
                {
                    btnOK.Enabled = true;
                }
                else
                {
                    btnOK.Enabled = false;
                }
            }
        }

        #endregion


        public string InputBox(string prompt, string title, string defaultInput)
        {
            // we'll create a new instance of the class and set the given arguments as properties
            var ib = new InputDialog
            {
                FormPrompt = prompt,
                FormCaption = title,
                DefaultValue = defaultInput
            };

            // show the actual dialog
            ib.ShowDialog();

            // retrieve the response string, close the dialog and return the string
            var response = ib.InputResponse;
            ib.Close();
            return response;
        }
    }
}