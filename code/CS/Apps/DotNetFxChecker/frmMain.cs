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

namespace DotNetFxChecker
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
      switch (sender.ActionTag())
      {
        case "GetFxVersions":
          GetFxVersions();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void GetFxVersions()
    {
      g.SystemInfo.Refresh();

      txtOut.Text = g.SystemInfo.SystemInfoString;
    }



    private void InitializeForm()
    {
      try
      {
        new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the creation of the application object 'a'." + g.crlf2 + ex.ToReport(),
                        ".NET Framework Checker - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      try
      {
        this.SetInitialSizeAndLocation();

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization of the application." + g.crlf2 + ex.ToReport(),
                        ".NET Framework Checkerh - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

  }
}
