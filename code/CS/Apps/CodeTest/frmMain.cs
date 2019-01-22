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

namespace Org.CodeTest
{
  public partial class frmMain : Form
  {
    private a a;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
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
        MessageBox.Show("An exception occurred during initialization of the application object 'a'." + g.crlf2 +
                        ex.ToReport(), "Code Test - Program Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }

  }
}
