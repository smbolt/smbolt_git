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

namespace Org.TrayAppLauncher
{
  public partial class frmMain : Form
  {
    private Dictionary<string, string> _launchMenuItems;


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
        case "LaunchApp":
          LaunchApp(sender);
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void LaunchApp(object sender)
    {
      string programName = ((ToolStripMenuItem)sender).Text;

      if (!_launchMenuItems.ContainsKey(programName))
      {
        MessageBox.Show("The Tray Application Launcher is not configured properly to launch program '" + programName + "'." + g.crlf2 +
                        "There is no entry in the program table for this program.", "Tray Application Launcher - Configuration Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      string executablePath = _launchMenuItems[programName];

      using (var processHelper = new ProcessHelper())
      {
        var processParms = new ProcessParms();
        processParms.ExecutablePath = executablePath;
        processHelper.RunExternalProcessNoWait(processParms);
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
        MessageBox.Show("An exception occurred while initializating the application object 'a'." + g.crlf2 + ex.ToReport(),
                        "DxWorkbookTester - Application Object Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      try
      {
        this.Location = new Point(0, 0);
        this.Size = new Size(0, 0);

        _launchMenuItems = g.GetDictionary("LaunchMenuItems");

        ctxMnuTray.Items.Clear();

        foreach (var kvp in _launchMenuItems)
        {
          var menuItem = new ToolStripMenuItem(kvp.Key);
          menuItem.Tag = "LaunchApp";
          menuItem.Click += Action;
          ctxMnuTray.Items.Add(menuItem);
        }


        var exitMenuItem = new ToolStripMenuItem("E&xit");
        exitMenuItem.Tag = "Exit";
        exitMenuItem.Click += Action;
        ctxMnuTray.Items.Add(exitMenuItem);

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while initializating the application." + g.crlf2 + ex.ToReport(),
                        "DxWorkbookTester - Application Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void frmMain_Shown(object sender, EventArgs e)
    {
      this.Visible = false;
    }
  }
}
