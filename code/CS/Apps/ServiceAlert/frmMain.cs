using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Org.ServiceAlert
{
  public partial class frmMain : Form
  {
      private string _message;


      public frmMain()
      {
        InitializeComponent();
        InitializeApplication();
      }

      private void InitializeApplication()
      {
        btnSendToSupport.Visible = false;

        string[] args = Environment.GetCommandLineArgs();

        if (args.Count() < 2)
            _message = "An unknown error occurred.";
        else
            _message = args[1];
            
        txtError.Text = _message;
        txtError.SelectionStart = 0;
        txtError.SelectionLength = 0;
      }

      private void btnClose_Click(object sender, EventArgs e)
      {
        this.Close();
      }

      private void btnSendToAdsdi_Click(object sender, EventArgs e)
      {
        MessageBox.Show("Send function not yet implemented.", "Service Alert - Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
      }
  }
}
