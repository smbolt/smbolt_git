using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SystemChecker
{
  public partial class frmMain : Form
  {
    public frmMain()
    {
      InitializeComponent();
      InitializeApplication();
    }

    private void InitializeApplication()
    {
      RefreshSystemInfo();
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnCopyToClipboard_Click(object sender, EventArgs e)
    {
      Clipboard.SetText(txtOut.Text);

      MessageBox.Show("The system information has been copied to the Windows Clipboard.", "System Checker",
                      MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void RefreshSystemInfo()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("SYSTEM CHECKER 1.0" + Common.crlf);
      sb.Append("DATE: " + DateTime.Now.ToLongDateString() + "  " + DateTime.Now.ToLongTimeString() + Common.crlf2);

      try
      {

        var systemInfo = new SystemInfo();
        sb.Append(systemInfo.SystemInfoString + Common.crlf2);

        sb.Append("USER IS LOCAL ADMINISTRATOR : " + systemInfo.IsUserLocalAdmin.ToString().ToUpper() + Common.crlf2);

        sb.Append("INSTALLED VERSIONS OF .NET FRAMEWORK" + Common.crlf);
        foreach (string framework in systemInfo.InstalledFrameworks)
        {
          sb.Append("    " + framework + Common.crlf);
        }



      }
      catch (Exception ex)
      {
        sb.Append("AN EXCEPTION OCCURRED ATTEMPTING TO EVALUATE SYSTEM CHARACTERISTICS" + Common.crlf2 + ex.ToReport());
      }

      txtOut.Text = sb.ToString();
    }

    private void mnuFileExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void mnuOptionsRefresh_Click(object sender, EventArgs e)
    {
      RefreshSystemInfo();
      MessageBox.Show("System information refreshed.", "System Checker", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
  }
}
