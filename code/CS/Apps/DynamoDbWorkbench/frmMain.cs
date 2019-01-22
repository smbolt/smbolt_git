using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.DynamoDB;
using Org.GS;

namespace DynamoDbWorkbench
{
  public partial class frmMain : Form
  {
    private DbParms _dbParms;
    private DbClient _dbClient;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void Action(object c, EventArgs e)
    {
      switch (c.ActionTag())
      {

        case "TestConnection":
          TestConnection();
          break;

        case "Go":
          Go();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void TestConnection()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        EnsureClient();

        _dbClient.Connect();

        this.Cursor = Cursors.Default;

        MessageBox.Show("Connection to DynamoDb running at '" + _dbParms.SerivceURL + "' is successful.",
                        "DynamoDb Workbench - Connection Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to determine if a connection to the DynamoDb instance can be established." + ex.ToReport(),
                        "DynamoDb Workbench - Test DB Connection Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void EnsureClient()
    {
      if (_dbClient == null)
        _dbClient = new DbClient(_dbParms);
    }

    private void Go()
    {
      txtMain.Text = "Go";
    }

    private void InitializeForm()
    {
      try
      {
        new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization of the application object 'a'." + ex.ToReport(),
                        "DynamoDb Workbench - Error Initializing of Application Object 'a'",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      try
      {
        this.SetInitialSizeAndLocation();

        _dbParms = new DbParms();
        _dbParms.UseLocalInstance = g.CI("DynamoDbUseLocalInstance").ToBoolean();
        _dbParms.Host = g.CI("DynamoDbHost");
        _dbParms.Port = g.CI("DynamoDbPort").ToInt32();
        _dbParms.TcpConnectWaitMilliseconds = g.CI("DynamoDbTcpConnectWaitMilliseconds").ToInt32OrDefault(3000);

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during application initialization." + ex.ToReport(),
                        "DynamoDb Workbench - Error During Application Initialization",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }
  }
}
