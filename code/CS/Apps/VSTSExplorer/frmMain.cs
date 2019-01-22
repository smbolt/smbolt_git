using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.VSTS;
using Org.GS;

namespace VSTSExplorer
{
  public partial class frmMain : Form
  {
    private Dictionary<string, string> _vstsConnections;
    private string _personalAccessToken;
    private VSTSClient _vstsClient;
    private string _selectedVstsClientUri;
    
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
        case "Connect":
          Connect();
          break;

        case "GetAccounts":
          GetAccounts();
          break;

        case "GetUsers":
          GetUsers();
          break;

        case "GetWebApiToken":
          GetWebApiToken();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void Connect()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;
        _vstsClient = new VSTSClient(_selectedVstsClientUri, _personalAccessToken);

        txtOut.Text = "Connected to " + _selectedVstsClientUri + ".";

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception has occurred while attempting to connect to VSTS." + g.crlf2 +
                        ex.ToReport(), "VSTS Explorer - VSTS Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        this.Cursor = Cursors.Default;
      }
    }

    private void GetAccounts()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;
        txtOut.Text = _vstsClient.GetAccounts();
        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        txtOut.Text = "An exception has occurred while attempting to get account information." + g.crlf2 + ex.ToReport();

        this.Cursor = Cursors.Default;
      }
    }

    private void GetUsers()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;
        txtOut.Text = _vstsClient.GetUsers();
        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        txtOut.Text = "An exception has occurred while attempting to get account information." + g.crlf2 + ex.ToReport();

        this.Cursor = Cursors.Default;
      }
    }

    private void GetWebApiToken()
    {
      txtOut.Text = _vstsClient.GetWebApiToken().Result;
    }

    private void InitializeForm()
    {
      try
      {
        new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception has occurred while attempting to initialize the application object 'a'." + g.crlf2 +
                        ex.ToReport(), "VSTS Explorer - Application Object Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      try
      {
        this.SetInitialSizeAndLocation(75, 85);
        _vstsConnections = g.GetDictionary("VSTSConnections");
        _selectedVstsClientUri = _vstsConnections["Gulfport"];
        _personalAccessToken = g.CI("SBOLT_PAT");

        splitterMain.Panel1Collapsed = true;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception has occurred while attempting to initialize the application." + g.crlf2 +
                        ex.ToReport(), "VSTS Explorer - Application Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
