using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using Adsdi.EBCD;
using Adsdi.GS;

namespace Adsdi.EBCD_Builder
{
    public partial class frmMain : Form
    { 
        private EBCD.EBCD _ebcd;
        private string filePath = @"C:\DevProjects\Main\Source\EBCD_Projects\EBCD\Resources\ebcd.bin";

        public frmMain()
        {
            InitializeComponent();
            Initialize_Application();
        }

        private void Initialize_Application()
        {
            _ebcd = new EBCD.EBCD();
            _ebcd.Name = "Test EBCD Object1";
            _ebcd.Version = "1.0.0.0";
            _ebcd.BuildDate = DateTime.Now;
            Populate_Form();
        }

        private void Populate_Form()
        {
            txtObjectName.Text = _ebcd.Name;
            txtVersion.Text = _ebcd.Version;
            lblBuildDateValue.Text = _ebcd.BuildDate.ToString();

            txtKey.Text = "E4wWAwi7z7K4973Vtg8REVfv5CcQ6bgGTIbeW8PmMhYDxVtY";

            Load_lvMain();
        }

        private void Load_lvMain()
        {
            lvMain.Items.Clear();

            foreach (KeyValuePair<string, string> kvp in _ebcd.Config)
            {
                ListViewItem i = new ListViewItem();
                i.Text = kvp.Key;
                i.SubItems.Add(kvp.Value);
                lvMain.Items.Add(i);
            }
        }

        private void btnBuildEBCD_Click(object sender, EventArgs e)
        {
            txtKey.Text = txtKey.Text.Trim();

            if (txtKey.Text.Trim().Length != 48)
            {
                MessageBox.Show("The encryption key is required and must be exactly 48 characters long", 
                    "Invalid Encryption Key", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtKey.SelectAll();
                txtKey.Focus();
                return;                    
            }

            EBCDFactory factory = new EBCDFactory();
            factory.Key = txtKey.Text;
            Initialize_Encryption(factory.Key);
            _ebcd.Name = txtObjectName.Text.Trim();
            _ebcd.Version = txtVersion.Text.Trim();
            _ebcd.BuildDate = DateTime.Now;
            
            SerializeEBCD(_ebcd, filePath);

            MessageBox.Show("EBCD Object " + _ebcd.Name + " " + _ebcd.Version + " successfully built" + Environment.NewLine +
                "Binary file location is " + filePath, "EBCD Build Successful");
        }

        private void Initialize_Encryption(string ebcdKey)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] dek = encoding.GetBytes(ebcdKey);
            Encryptor encryptor = new Encryptor();
            encryptor.Initialize_EncryptionKeys(dek);
        }

        private static void SerializeEBCD(EBCD.EBCD ebcd, string FullFilePath)
        {
            MemoryStream memStream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memStream, ebcd);
            Encryptor encryptor = new Encryptor();
            
            string encryptedEBCD = encryptor.EncryptByteArray(memStream.GetBuffer());
            StreamWriter sw = new StreamWriter(FullFilePath);
            sw.Write(encryptedEBCD);
            memStream.Close();
            sw.Close();
            sw.Dispose();
            memStream.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtObjectName.Clear();
            txtVersion.Clear();
            lblBuildDateValue.Text = String.Empty;
            lvMain.Items.Clear();
        }

        private void btnGetEBCD_Click(object sender, EventArgs e)
        {
            txtKey.Text = txtKey.Text.Trim();

            if (txtKey.Text.Trim().Length != 48)
            {
                MessageBox.Show("The encryption key is required and must be exactly 48 characters long", "Invalid Encryption Key", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtKey.SelectAll();
                txtKey.Focus();
                return;
            }

            EBCDFactory factory = new EBCDFactory();
            factory.Key = txtKey.Text;
            _ebcd = factory.GetEBCD();
            Populate_Form();
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            if (Terminate_Application())
                this.Close();
        }

        private bool Terminate_Application()
        {
            return true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            txtEBCDKey.Text = txtEBCDKey.Text.Trim();
            txtEBCDValue.Text = txtEBCDValue.Text.Trim();

            if (txtEBCDKey.Text.Length == 0 || txtEBCDValue.Text.Length == 0)
            {
                MessageBox.Show("The EBCD Key and EBCD Value must both contain data", 
                    "EBCD Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEBCDKey.SelectAll();
                txtEBCDKey.Focus();
            }

            if(_ebcd.Config.ContainsKey(txtEBCDKey.Text))
                _ebcd.Config[txtEBCDKey.Text] = txtEBCDValue.Text;
            else
                _ebcd.Config.Add(txtEBCDKey.Text.Trim(), txtEBCDValue.Text.Trim());

            txtEBCDKey.Clear();
            txtEBCDValue.Clear();
            Load_lvMain();
            txtEBCDKey.Focus();
        }

        private void ctxMnuLVMainDelete_Click(object sender, EventArgs e)
        {
            if (lvMain.SelectedItems.Count == 0)
                return;

            if (MessageBox.Show("Are you sure you want to delete item: " +
                lvMain.SelectedItems[0].Text + "?", "Confirm EBCD Item Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _ebcd.Config.Remove(lvMain.SelectedItems[0].Text);
                Load_lvMain();
            }
        }

        private void ctxMnuLVMain_Opening(object sender, CancelEventArgs e)
        {
            if (lvMain.SelectedItems.Count == 0)
                e.Cancel = true;
        }

        private void lvMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvMain.SelectedItems.Count == 0)
                return;

            txtEBCDKey.Text = lvMain.SelectedItems[0].Text;
            txtEBCDValue.Text = lvMain.SelectedItems[0].SubItems[1].Text;
        }

    }
}
