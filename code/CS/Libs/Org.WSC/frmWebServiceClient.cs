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

namespace Org.WSC
{
  public partial class frmWebServiceClient : Form
  {
    public frmWebServiceClient()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "SendMessage":
          SendMessage();
          break;

        case "PingPort":
          PingPort();
          break;

        case "Close":
          this.Close();
          break;
      }
    }

    private void SendMessage()
    {
      txtOut.Text = "SendMessage";
    }

    private void PingPort()
    {
      txtOut.Text = "PingPort";
    }

    private void InitializeForm()
    {

    }
  }
}
