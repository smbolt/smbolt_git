using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.ApiUtility;
using Org.GS;

namespace Org.ApiWorkbench
{
  public partial class frmMain : Form
  {
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


        case "Exit":
          this.Close();
          break;
      }
    }


    private void InitializeForm()
    {
      try
      {
        new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An error occurred during the initialization of the application object 'a'.  See message below." + g.crlf2 + ex.ToReport(),
                        "API Workbench - Error Initializing the Application Object (a)", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      try
      {
        int formHorizontalSize = g.GetCI("MainFormHorizontalSize").ToInt32OrDefault(90);
        int formVerticalSize = g.GetCI("MainFormVerticalSize").ToInt32OrDefault(90);

        this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * formHorizontalSize / 100,
                             Screen.PrimaryScreen.Bounds.Height * formVerticalSize / 100);
        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                  Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2);
      }
      catch (Exception ex)
      {
        MessageBox.Show("An error occurred attempting to initialize the program.  See message below." + g.crlf2 + ex.ToReport(),
                        "API Workbench - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
