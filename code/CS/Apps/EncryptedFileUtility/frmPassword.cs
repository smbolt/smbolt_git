using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Adsdi.GS;

namespace Adsdi.EncryptedFileUtility
{
    public partial class frmPassword : Form
    {
        private string _password = String.Empty;

        public frmPassword(string password)
        {
            InitializeComponent();
            _password = password;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim().Length == 0)
                btnOK.Enabled = false;
            else
                btnOK.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string password = "scanAdmin";
            if (_password.IsNotBlank())
                password = _password;

            if (txtPassword.Text.Trim() == password)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("The password entered was not correct." + g.crlf2 +
                    "Please try again.", "Encrypted File Utility", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.SelectAll();
                txtPassword.Focus();
                return;
            }
        }


    }
}
