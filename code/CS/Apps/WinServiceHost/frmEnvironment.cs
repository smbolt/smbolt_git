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

namespace Org.WinServiceHost
{
  public partial class frmEnvironment : Form
  {
    public string SelectedEnvironment {
      get;
      private set;
    }

    public frmEnvironment()
    {
      InitializeComponent();
      btnOK.Enabled = false;
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "OK":
          this.SelectedEnvironment = cboEnvironment.Text;
          this.DialogResult = DialogResult.OK;
          this.Close();
          break;

        case "Cancel":
          this.SelectedEnvironment = String.Empty;
          this.DialogResult = DialogResult.Cancel;
          this.Close();
          break;

      }
    }

    private void cboEnvironment_SelectedIndexChanged(object sender, EventArgs e)
    {
      btnOK.Enabled = cboEnvironment.Text.IsNotBlank();
    }
  }
}
