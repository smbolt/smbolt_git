using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.FTP;
using Org.GS.Configuration;
using Org.GS;

namespace FtpTest
{
  public partial class frmMain : Form
  {
    private a a;
    private ConfigFtpSpec _configFtpSpec;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }


    private void Action(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "Go":
          Go();
          break;

        case "Exit":
          this.Close();
          break;
      }

      this.Cursor = Cursors.Default;
    }

    private void Go()
    {
      txtOut.Text = "I goed.";
    }


    private void InitializeForm()
    {
      try
      {
        a = new a();
      }
      catch (Exception ex)
      {
        txtOut.Text = "An exception occurred during the initialization of the application object." + g.crlf2 +
                      "Memory Log:" + g.crlf + g.MemoryLog + g.crlf2 +
                      "Exception Report:" + g.crlf + ex.ToReport();
        return;
      }

      _configFtpSpec = g.GetFtpSpec("PDS");

    }
  }
}
