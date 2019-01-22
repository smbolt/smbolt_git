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

namespace Org.OpsManager
{
  public partial class frmDisplay : Form
  {
    public frmDisplay(string report)
    {
      InitializeComponent();

      InitializeReport(report);
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "Close":
          this.Close();
          break;
      }
    }

    private void InitializeReport(string report)
    {
      txtOut.Text = report;
    }
  }
}
