using System;
using System.Diagnostics;
using System.Windows.Forms;


namespace Nocs.Forms
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lnkContact_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // let's try catch since there might be no app associated with 'mailto'
            try
            {
                Process.Start("mailto:mikko.junnila@gmail.com");
            }
            catch
            {
                return;
            }
        }

        private void lnkTwitter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://twitter.com/mikkoj");
        }

        private void lnkGoogleCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://nocs.googlecode.com/");
        }
    }
}