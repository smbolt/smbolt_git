using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Org.AX;
using Org.GS;

namespace Org.MigrTest
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
        case "RunAction":
          RunAction();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void RunAction()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {

        string mpxPath = txtProfilePath.Text + @"\" + cboProfileFile.Text; 
        string mpxData = File.ReadAllText(mpxPath);
        XElement mpxXml = XElement.Parse(mpxData);

        string profileName = cboProfileFile.Text; 

        using (var f = new ObjectFactory2())
        {
          var axProfileSet = (AxProfileSet)f.Deserialize(mpxXml);
          axProfileSet.ResolveVariables();

          using (var axEngine = new AxEngine())
          {

            var taskResult = axEngine.RunAxProfile(axProfileSet, profileName, ckDryRun.Checked);



          }
        }

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred during the running of the action." + g.crlf2 + ex.ToReport(),
                        "MigrTest - Action Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

    }

    private void InitializeForm()
    {
      try
      {
        a = new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the creation of the application object 'a'." + g.crlf2 + ex.ToReport(),
                        "MigrTest - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }


      try
      {
        txtProfilePath.Text = g.CI("ProfilePath");

        if (Directory.Exists(txtProfilePath.Text))
        {
          cboProfileFile.LoadItems(Directory.GetFiles(txtProfilePath.Text, "*.mpx").ToFileNameList());
          cboProfileFile.SelectItem(g.CI("SelectedProfile")); 
        }

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization of the program." + g.crlf2 + ex.ToReport(),
                        "MigrTest - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

  }
}
