using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Google.Documents;
using Google.GData.Client;
using Nocs.Models;
using Nocs.Properties;
using Nocs.Helpers;


namespace Nocs.Forms
{
    public partial class Preferences : Form
    {
        public Preferences()
        {
            InitializeComponent();
        }

        private void Preferences_Load(object sender, EventArgs e)
        {
            // sorting of documents
            if (string.IsNullOrEmpty(Settings.Default.DocumentSortOrder) ||
                Settings.Default.DocumentSortOrder == "ByTitle")
            {
                rdByTitle.Checked = true;
            }
            else
            {
                rdByDate.Checked = true;
            }

            // ------------------------------------------------------------------------------
            // status bar style

            switch (Settings.Default.StatusBarStyle)
            {
                case "TextColor": { cmbStatusBar.SelectedIndex = 0; break; }
                case "TextBlackAndWhite": { cmbStatusBar.SelectedIndex = 1; break; }
                case "Minimal": { cmbStatusBar.SelectedIndex = 2; break; }
                default: { cmbStatusBar.SelectedIndex = 0; break; }
            }

            // ------------------------------------------------------------------------------
            // autosave

            chkAutoSave.Checked = Settings.Default.AutoSave;
            txtAutoSaveTimeout.Enabled = Settings.Default.AutoSave;
            lblAutoSaveTimeout2.Enabled = Settings.Default.AutoSave;
            txtAutoSaveTimeout.Text = Settings.Default.AutoSaveTimeout.ToString();

            // ------------------------------------------------------------------------------
            // default save folder

            Tools.PopulateFoldersForComboBox(cmbDefaultFolder, false);

            // ------------------------------------------------------------------------------
            // proxy settings

            chkUseProxy.Checked = Settings.Default.UseProxy;
            
            // default value for proxy protocol is http
            //cmbProtocol.SelectedIndex = 0;

            if (Settings.Default.UseProxy)
            {
                rdUseDefaultProxy.Checked = Settings.Default.AutomaticProxyDetection;
                rdUseManualProxy.Checked = !Settings.Default.AutomaticProxyDetection;

                if (Settings.Default.AutomaticProxyDetection)
                {
                    txtProxyHost.Clear();
                    txtProxyPort.Text = "80";
                    txtProxyUsername.Clear();
                    txtProxyPassword.Clear();
                }
                else
                {
                    //cmbProtocol.SelectedIndex = Settings.Default.ProxyProtocol.Equals("http") ? 0 : 1;
                    txtProxyHost.Text = Settings.Default.ProxyHost;
                    txtProxyPort.Text = Settings.Default.ProxyPort;
                    txtProxyUsername.Text = Settings.Default.ProxyUsername;
                    txtProxyPassword.Text = Tools.Decrypt(Settings.Default.ProxyPassword);
                }
            }
        }

        private void chkAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoSave.Checked)
            {
                lblAutoSaveTimeout2.Enabled = true;
                txtAutoSaveTimeout.Enabled = true;
            }
            else
            {
                lblAutoSaveTimeout2.Enabled = false;
                txtAutoSaveTimeout.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // no reason to continue if proxy is invalid
            if (!rdUseDefaultProxy.Checked && !ValidateProxyFields())
            {
                return;
            }

            // let's collect all given settings, save them and close the form
            Settings.Default.DocumentSortOrder = rdByTitle.Checked ? DocumentSortOrder.ByTitle.ToString() : DocumentSortOrder.ByDate.ToString();

            var selectedStatusBarStyle = cmbStatusBar.SelectedItem.ToString();
            StatusBarStyle statusBarStyle;
            switch (selectedStatusBarStyle)
            {
                case "Text with color":
                {
                    statusBarStyle = StatusBarStyle.TextColor;
                    break;
                }
                case "Black & white text":
                {
                    statusBarStyle = StatusBarStyle.TextBlackAndWhite;
                    break;
                }
                case "Minimal":
                {
                    statusBarStyle = StatusBarStyle.Minimal;
                    break;
                }
                default:
                {
                    statusBarStyle = StatusBarStyle.TextColor;
                    break;
                }
            }

            Settings.Default.StatusBarStyle = statusBarStyle.ToString();
            Settings.Default.AutoSave = chkAutoSave.Checked;
            Settings.Default.AutoSaveTimeout = Convert.ToInt32(txtAutoSaveTimeout.Text.Trim());
            
            // the first item in default folder -combobox is "no folder".
            // if it's selected, we'll reset the save folder
            if (cmbDefaultFolder.SelectedIndex != 0 && ((Document)cmbDefaultFolder.SelectedItem).Id != "draft")
            {
                Settings.Default.DefaultSaveFolder = ((Document)cmbDefaultFolder.SelectedItem).ResourceId;
            }
            else
            {
                Settings.Default.DefaultSaveFolder = string.Empty;
            }

            Settings.Default.UseProxy = chkUseProxy.Checked;
            if (Settings.Default.UseProxy)
            {
                Settings.Default.AutomaticProxyDetection = rdUseDefaultProxy.Checked;
                if (Settings.Default.AutomaticProxyDetection)
                {
                    Settings.Default.ProxyProtocol = string.Empty;
                    Settings.Default.ProxyHost = string.Empty;
                    Settings.Default.ProxyPort = string.Empty;
                    Settings.Default.ProxyUsername = string.Empty;
                    Settings.Default.ProxyPassword = string.Empty;
                }
                else
                {
                    //Settings.Default.ProxyProtocol = cmbProtocol.SelectedItem.ToString();
                    Settings.Default.ProxyHost = txtProxyHost.Text.Trim();
                    Settings.Default.ProxyPort = txtProxyPort.Text.Trim();
                    Settings.Default.ProxyUsername = txtProxyUsername.Text.Trim();
                    Settings.Default.ProxyPassword = Tools.Encrypt(txtProxyPassword.Text.Trim());
                }
            }

            // close preferences (unless proxy fields are invalid)
            if (!Settings.Default.UseProxy || Settings.Default.AutomaticProxyDetection)
            {
                Settings.Default.Save();
            }

            btnSave.DialogResult = DialogResult.OK;
            Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // close preferences
            Settings.Default.Save();
            Dispose();
        }

        private void txtAutoSaveTimeout_Leave(object sender, EventArgs e)
        {
            // validate AutoSaveTimeout
            if (chkAutoSave.Checked && !Regex.IsMatch(txtAutoSaveTimeout.Text.Trim(), @"^([5-9]|[1-5][0-9]|60)$"))
            {
                if (MessageBox.Show("Invalid timeout for auto-save! (must be 5-60 seconds)") == DialogResult.OK)
                {
                    txtAutoSaveTimeout.Focus();
                    btnSave.DialogResult = DialogResult.None;
                }
            }
        }
        
        private void cmbDefaultFolder_Format(object sender, ListControlConvertEventArgs e)
        {
            var folder = (Document)e.ListItem;
            e.Value = folder.Title;
        }

        private void chkUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseProxy.Checked)
            {
                rdUseDefaultProxy.Enabled = true;
                rdUseManualProxy.Enabled = true;
                pnlManualProxySettings.Enabled = rdUseManualProxy.Checked;
            }
            else
            {
                rdUseDefaultProxy.Enabled = false;
                rdUseManualProxy.Enabled = false;
                pnlManualProxySettings.Enabled = false;
            }
        }

        private void rdUseManualProxy_CheckedChanged(object sender, EventArgs e)
        {
            pnlManualProxySettings.Enabled = rdUseManualProxy.Checked;
        }

        private void txtProxyHost_Leave(object sender, EventArgs e)
        {
            //ValidateProxyFields();
        }

        private void txtProxyPort_Leave(object sender, EventArgs e)
        {
            //ValidateProxyFields();
        }

        private bool ValidateProxyFields()
        {
            // we'll validate proxy as well
            //var protocol = cmbProtocol.SelectedItem.ToString();
            var host = txtProxyHost.Text;
            var port = txtProxyPort.Text;

            try
            {
                var proxyAddress = string.Format("http://{0}:{1}/", host, port);
                var proxy = new WebProxy(proxyAddress, true);
                proxy.GetProxy(new Uri("http://www.google.com"));
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Invalid proxy, make sure the host and port are valid!", "Invalid proxy");
                btnSave.DialogResult = DialogResult.None;
                return false;
            }
        }
    }
}