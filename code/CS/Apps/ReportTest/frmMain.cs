using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;

namespace Org.ReportTest
{
  public partial class frmMain : Form
  {
    private a a;

    public frmMain()
    {
      InitializeComponent();
      InitializeApplication();
    }

    private void Action(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      string action = g.GetActionFromEvent(sender);

      switch (action)
      {


        case "Exit":
          this.Close();
          break;
      }

      this.Cursor = Cursors.Default;
    }

    private void InitializeApplication()
    {
      try
      {
        a = new a();

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 + ex.ToReport(), "Report Test - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
