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
using Org.Json;
using Org.WSO;
using Org.WSO.Transactions;
using Org.GS.Json;
using Org.GS;

namespace Org.JsonTest
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
        case "BuildCommandSet":
          BuildCommandSet();
          break;

        //case "BuildMessage":
        //  BuildMessage();
        //  break;

        case "SerializeObject":
          SerializeObject();
          break;

        case "DeserializeObject":
          DeserializeObject();
          break;

        case "LoadAppConfig":
          LoadAppConfig();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void BuildCommandSet()
    {
      var wsCommandSet = new WsCommandSet();

      var wsCommand = new WsCommand(WsCommandName.StartWinService);
      wsCommand.Parms.Add("Parm1", "Value1");
      wsCommand.Parms.Add("Parm2", "Value2");

      wsCommandSet.Add(wsCommand);

      string json = wsCommandSet.JsonSerialize();

      txtRaw.Text = json;
    }

    private void DeserializeObject()
    {
      try
      {
        string rawText = txtRaw.Text;

        var jo = new JsonObject(rawText);

        var o = jo.JObject;

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to deserialize the JSON text into an object." + g.crlf2 + ex.ToReport(),
                        "JSON Test - Deserialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    public void LoadAppConfig()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {

        //var dbSpec = j.GetDbSpec("Logging");
        //var dbSpec2 = j.GetDbSpec("Logging");
        //var smtpSpec = j.GetSmtpSpec("Default");
        //var ftpSpec = j.GetFtpSpec("Default");

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An error occurred while attempting to load the AppConfig object." + g.crlf2 + ex.ToReport(), "JsonTest - Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }


    //private void BuildMessage()
    //{
    //  var msg = new Msg.Message();
    //  PopulateMessageProperties(msg);
    //  var trans = new GetAssemblyReportRequest();
    //  trans.IncludeAllAssemblies = true;
    //  msg.MessageBody.Transaction = trans;

    //  string json = msg.JsonSerialize();

    //  txtRaw.Text = json;
    //}

    //private void PopulateMessageProperties(Msg.Message msg)
    //{
    //  var h = msg.MessageHeader;
    //  h.Version = "1.0.0.0";
    //  h.AppName = "TestAppName";
    //  h.AppVersion = "1.1.0.0";
    //  h.OrgId = 5;
    //  h.UserName = "TestUserName";
    //  h.Password = "TestPassword";
    //}

    private void SerializeObject()
    {

    }

    //private void SerializeObject()
    //{
    //  j.AppConfig.ClearCISet();

    //  j.AppConfig.AddCI(new CI("Key01", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key02", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key03", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key04", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key05", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key06", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key07", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key08", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key09", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key10", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key11", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key12", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key13", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key14", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key15", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key16", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key17", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key18", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key19", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key20", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key21", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key22", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key23", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key24", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key25", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key26", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key27", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key28", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key29", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key30", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key31", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key32", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key33", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key34", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key35", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key36", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key37", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key38", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key39", "Value01"));
    //  j.AppConfig.AddCI(new CI("Key40", "Value01"));

    //  string json = j.AppConfig.CISet.JsonSerialize().ToCISetFormat();
    //  txtJson.Text = json;

    //  tabMain.SelectedTab = tabPageJson;
    //}



    private void InitializeForm()
    {
      try
      {
        new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization while creating the 'a' application object." + g.crlf2 + ex.ToReport(),
                        "JSON Test - Application Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      try
      {

        this.SetInitialSizeAndLocation();

        txtRaw.Text = File.ReadAllText(g.ImportsPath + @"\VSTS_BuildDefinitions.json");

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 + ex.ToReport(),
                        "JSON Test - Application Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
