using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Nocs.Helpers;
using Nocs.Properties;


namespace Nocs.Forms
{
    public partial class Login : Form
    {
        // determines whether the user has already been _validated with Google
        private bool _validated;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // if user is already authenticated, fill the fields with user's credentials
            if ((!string.IsNullOrEmpty(Settings.Default.GoogleUsername) && !string.IsNullOrEmpty(Settings.Default.GooglePassword)) ||
                (!string.IsNullOrEmpty(NocsService.Username) && !string.IsNullOrEmpty(NocsService.Password)))
            {
                _validated = true;

                // temporarily disable TextChanged wiring
                txtGUser.TextChanged -= TxtGbUserTextChanged;
                txtGPassword.TextChanged -= TxtGbPasswordTextChanged;

                // fill fields
                if (!string.IsNullOrEmpty(NocsService.Username) && !string.IsNullOrEmpty(NocsService.Password))
                {
                    txtGUser.Text = NocsService.Username;
                    txtGPassword.Text = NocsService.Password;
                }
                else
                {
                    txtGUser.Text = Settings.Default.GoogleUsername;
                    txtGPassword.Text = Tools.Decrypt(Settings.Default.GooglePassword);
                }

                chkSaveGoogleAccount.Checked = Settings.Default.SaveGoogleAccount;

                // re-enable events for TextChanged
                txtGUser.TextChanged += TxtGbUserTextChanged;
                txtGPassword.TextChanged += TxtGbPasswordTextChanged;

                btnValidateOK.Text = "OK";
                btnValidateOK.Enabled = true;
            }
        }


        #region Control Event Handlers

        private void BtnValidateOkClick(object sender, EventArgs e)
        {
            // when Validate / OK button is pressed either validate user
            // with the given credentials or close the dialog
            if (!_validated)
            {
                // disable controls
                boxValidating.Image = Resources.Loader;
                btnValidateOK.Enabled = false;
                btnCancel.Enabled = false;
                boxValidating.Visible = true;

                // disable access to fields while validating
                txtGUser.Enabled = false;
                txtGPassword.Enabled = false;
                lblValidateInfo.Text = "Authenticating.. please wait!";
                bgWorker_Validate.RunWorkerAsync();
            }
            else
            {
                if (!chkSaveGoogleAccount.Checked)
                {
                    // clear google account info from options
                    Settings.Default.GoogleUsername = string.Empty;
                    Settings.Default.GooglePassword = string.Empty;
                    Settings.Default.SaveGoogleAccount = false;
                }
                else
                {
                    // update potential change in account saving
                    Settings.Default.GoogleUsername = txtGUser.Text;
                    Settings.Default.GooglePassword = Tools.Encrypt(txtGPassword.Text);
                    Settings.Default.SaveGoogleAccount = true;
                }

                // let's close the Login form
                Dispose();
            }
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            Dispose();
        }

        private void TxtGbUserTextChanged(object sender, EventArgs e)
        {
            // disable OK/Validate-button whenever the user or password-field is empty
            if (!string.IsNullOrEmpty(txtGUser.Text) && !string.IsNullOrEmpty(txtGPassword.Text))
            {
                boxValidating.Visible = false;
                lblValidateInfo.Text = "";
                btnValidateOK.Enabled = true;
                btnCancel.Enabled = true;
                btnValidateOK.Text = "Validate";
                _validated = false;
            }
            else
            {
                boxValidating.Visible = false;
                lblValidateInfo.Text = "";
                btnValidateOK.Enabled = false;
                btnCancel.Enabled = true;
                btnValidateOK.Text = "Validate";
                _validated = false;
            }
        }

        private void TxtGbPasswordTextChanged(object sender, EventArgs e)
        {
            // same as txtGBUser_TextChanged, but this monitors the password-field
            if (!string.IsNullOrEmpty(txtGUser.Text) && !string.IsNullOrEmpty(txtGPassword.Text))
            {
                boxValidating.Visible = false;
                lblValidateInfo.Text = "";
                btnValidateOK.Enabled = true;
                btnCancel.Enabled = true;
                btnValidateOK.Text = "Validate";
                _validated = false;
            }
            else
            {
                boxValidating.Visible = false;
                lblValidateInfo.Text = "";
                btnValidateOK.Enabled = false;
                btnCancel.Enabled = true;
                btnValidateOK.Text = "Validate";
                _validated = false;
            }
        }

        #endregion


        #region Background Workers

        private void BgWorkerValidateDoWork(object sender, DoWorkEventArgs e)
        {
            if (Tools.IsConnected())
            {
                // try to start the NocsServiceervice with given credentials
                NocsService.AuthenticateUser(txtGUser.Text, txtGPassword.Text, true);
            }
            else
            {
                throw new Exception("No internet connection");
            }
        }

        private void BgWorkerValidateRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // there was an error during the operation, show it to user
                if (e.Error.Message.Length > 27)
                {
                    lblValidateInfo.Text = String.Format("{0}..", e.Error.Message.Substring(0, 26));
                }
                else
                {
                    lblValidateInfo.Text = e.Error.Message;
                }
                btnValidateOK.Enabled = true;
                btnCancel.Enabled = true;
                boxValidating.Image = Resources.Error;
                txtGUser.Enabled = true;
                txtGPassword.Enabled = true;
            }
            else
            {
                // the operation succeeded, save settings
                _validated = true;
                boxValidating.Image = Resources.OK;
                lblValidateInfo.Text = "Validation successful.";
                btnValidateOK.Text = "OK";
                btnValidateOK.ForeColor = Color.MediumBlue;
                btnValidateOK.Enabled = true;
                btnCancel.Enabled = false;

                NocsService.AccountChanged = true;
                NocsService.Username = txtGUser.Text;
                NocsService.Password = txtGPassword.Text;

                // also save to config file if wanted
                if (chkSaveGoogleAccount.Checked)
                {
                    Settings.Default.GoogleUsername = txtGUser.Text;
                    Settings.Default.GooglePassword = Tools.Encrypt(txtGPassword.Text);
                }
                else
                {
                    Settings.Default.GoogleUsername = string.Empty;
                    Settings.Default.GooglePassword = string.Empty;
                }

                Settings.Default.Save();
            }
        }

        #endregion
    }
}