using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.MOD.Concrete;
using Org.UI;
using Org.GS.Configuration;
using Org.GS;

namespace Org.FsHelper
{
  public partial class frmMain : Form
  {
    private ModuleBase _module;
    private bool _isAuthenticated;
    private int _orgId;
    private ConfigDbSpec _configDbSpec;


    private ControlSpec _controlSpec;
    private List<string> _controlSpecNames;
    private string _lastPanelDisplayed = String.Empty;


    public frmMain(ModuleBase module)
    {
      InitializeComponent();
      _module = module;
      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {

        case "Exit":
          CloseApplication();
          break;
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.Close();
    }



    private void InitializeForm()
    {


    }


    public void ShutDownForAppDomainUnload()
    {
      this.Close();
    }


    private void CloseApplication()
    {


      this.Close();
    }
  }
}
