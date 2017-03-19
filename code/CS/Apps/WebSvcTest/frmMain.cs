using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using System.Reflection;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Net;
using Org.Dx.Business;
using Org.GS;
using Org.GS.Logging;
using Org.GS.Configuration;
using Org.GS.Network;
using Org.GS.Security;

using Request = FieldingSystems.FieldVisor.API.DataContracts.Request;
using Response = FieldingSystems.FieldVisor.API.DataContracts.Response;
using Newtonsoft.Json;
using Newtonsoft;

// added to test integration of new WFC Service - may remove later
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;

using Org.WSO;
using Org.WSO.Transactions;

namespace Org.WebSvcTest
{
  public partial class frmMain : Form
  {
    private a a;
    private Logger logger;
    private bool parm;
    private UserSession _userSession;
    private string _fvAuthId = String.Empty;

    [ImportMany(typeof(IMessageFactory))]
    public IEnumerable<Lazy<IMessageFactory, IMessageFactoryMetadata>> messageFactories;
    public CompositionContainer CompositionContainer;
    public Dictionary<string, IMessageFactory> LoadedMessageFactories; 

    private bool _initializationComplete = false;
    private bool _continueUsingCpu = true;

    private int wsCallCount = 0; 

    private System.Timers.Timer _timer;
    private DateTime TestEndDateTime; 

    private string _softwareUpdateVersion = String.Empty;
    private string _softwareUpdatePlatformString = String.Empty;

    private float _frequency;
    private float _duration;
    private ConfigWsSpec _configWsSpec;
    string _trans = String.Empty;
    SqlConnection conn; 


    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "ValidateSecurity":
          ValidateSecurity();
          break;

        case "ListFunctionsByRole":
          ListFunctionsByRole();
          break;
        
        case "RunSecurityTest":
          RunSecurityTest();
          break;
        
        case "LoginUser":
          LoginUser();
          break;
        
        case "DownloadSoftware":
          DownloadSoftware();
          break;
        
        case "DisplaySystemInfo":
          DisplaySystemInfo();
          break;
        
        case "CheckUserFunction":
          CheckUserFunction();
          break;

        case "ShareFileAPI":
          ShareFileAPI();
          break;

        case "TestDxWorkbook":
          TestDxWorkbook();
          break;

        case "SendMessage":
          wsCallCount = 0;
          this.Cursor = Cursors.WaitCursor;
          if (ckClearDisplay.Checked)
            txtOut.Clear();
          Application.DoEvents();

          SetConfigWsSpec();
          SendMessage();
          this.Cursor = Cursors.Default;
          break;

        case "RunTestLoop":
          this.Cursor = Cursors.WaitCursor;
          wsCallCount = 0;
          txtOut.Clear();
          Application.DoEvents();
          SetConfigWsSpec();
          RunTestLoop();
          this.Cursor = Cursors.Default;
          break;

        case "TestSecurityToken":
          this.Cursor = Cursors.WaitCursor;
          TestSecurityToken();
          this.Cursor = Cursors.Default;
          break;

        case "DecryptToken":
          this.Cursor = Cursors.WaitCursor;
          DecryptToken();
          this.Cursor = Cursors.Default;
          break;

        case "CancelTestLoop":
          CancelTestLoop();
          break;

        case "PingPort":
          PingPort();
          break;

        case "GenerateSendFiles":
          GenerateSendFiles();
          break;

        case "CLEAR_DISPLAY":
          txtOut.Clear();
          Application.DoEvents();
          break;

        case "UseCPU":
          txtOut.Clear();
          _continueUsingCpu = true;
          UseCPU();
          Application.DoEvents();
          break;

        case "LIST_APP_POOLS":
          ListAppPools();
          break;

        case "LIST_WEB_SITES":
          ListWebSites();
          break;

        case "FvWebService":
          RunFvWebService();
          break;

        case "Tokenize":
          Tokenize();
          break;

        case "Detokenize":
          Detokenize();
          break;

        case "Stop":
          _continueUsingCpu = false;
          Application.DoEvents();
          break;

        case "EXIT":
          this.Close();
          break;
      }

      this.Cursor = Cursors.Default;
    }

    private void InitializeForm()
    {
      XmlMapper.AddAssembly(Assembly.GetAssembly(typeof(MainSvcEngine))); 
      //XmlMapper.AddAssembly(Assembly.GetAssembly(typeof(CheckForUpdatesRequest))); 
      XmlMapper.AddAssembly(Assembly.GetAssembly(typeof(ExcelExtractRequest))); 
      XmlMapper.AddAssembly(Assembly.GetAssembly(typeof(DxWorkbook)));
      try
      {
        a = new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the application." + g.crlf2 +
                          ex.ToReport(), "WebSvcTest - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return; 
      }

      string platformString = g.SystemInfo.PlatformString;

      XmlMapper.Load();
      logger = new Logger(); 

      logger.Log(g.AppInfo.AppTitle + " v" + g.AppInfo.AppVersion + " Windows application initialization is starting.");
            

      this.LoadedMessageFactories = new Dictionary<string, IMessageFactory>();
      using (var catalog = new AggregateCatalog())
      {
        if (g.AppConfig.ContainsKey("MEFModulesPath"))
        {
          catalog.Catalogs.Add(new DirectoryCatalog(g.CI("MEFModulesPath")));
        }
        else
        {
          var mefCatalog = new OSFolder(g.MEFCatalog);
          mefCatalog.SearchParms.ProcessChildFolders = true;
          var leafFolders = mefCatalog.GetLeafFolders();

          foreach (string leafFolder in leafFolders)
            catalog.Catalogs.Add(new DirectoryCatalog(leafFolder));
        }

        this.CompositionContainer = new CompositionContainer(catalog);

        try
        {
          this.CompositionContainer.ComposeParts(this);
        }
        catch (CompositionException ex)
        {
          throw new Exception("An exception occurred attempting to compose MEF components.", ex);
        }
      }

            
      List<string> hostList = g.AppConfig.GetList("HostNames"); 
      List<string> serviceNamesList = g.AppConfig.GetList("ServiceNames");
      List<string> transNamesList = g.AppConfig.GetList("TransNames");

      cboWebServiceHost.Items.Clear();
      foreach (string host in hostList)
      {
        if (host.StartsWith("*"))
          cboWebServiceHost.Items.Insert(0, host.Substring(1));
        else
          cboWebServiceHost.Items.Add(host);
      }

      string selectedHost = g.CI("SelectedHost");
      if (selectedHost.IsNotBlank())
      {
        for (int i = 0; i < cboWebServiceHost.Items.Count; i++)
        {
          if (cboWebServiceHost.Items[0].ToString() == selectedHost)
          {
            cboWebServiceHost.SelectedIndex = i;
            break;
          }
        }
      }

      cboWebService.Items.Clear();
      foreach (string serviceName in serviceNamesList)
      {
        if (serviceName.StartsWith("*"))
          cboWebService.Items.Insert(0, serviceName.Substring(1));
        else
          cboWebService.Items.Add(serviceName);
      }

      string selectedWebService = g.CI("SelectedWebService");
      if (selectedWebService.IsNotBlank())
      {
        for (int i = 0; i < cboWebService.Items.Count; i++)
        {
          if (cboWebService.Items[i].ToString() == selectedWebService)
          {
            cboWebService.SelectedIndex = i;
            break;
          }
        }
      }

      cboTransaction.Items.Clear();
      foreach (string transName in transNamesList)
      {
        if (transName.StartsWith("*"))
          cboTransaction.Items.Insert(0, transName.Substring(1));
        else
          cboTransaction.Items.Add(transName);
      }

      string selectedTransaction = g.CI("SelectedTransaction");
      if (selectedTransaction.IsNotBlank())
      {
        for (int i = 0; i < cboTransaction.Items.Count; i++)
        {
          if (cboTransaction.Items[i].ToString() == selectedTransaction)
          {
            cboTransaction.SelectedIndex = i;
            break;
          }
        }
      }
      
      this._frequency = 0.0F;
      this._duration = 0.0F;
            
      if (g.AppConfig.ContainsKey("Frequency"))
      {
        this._frequency = g.AppConfig.GetFloat("Frequency");
        txtFrequency.Text = this._frequency.ToString("0.000"); 
      }

      if (g.AppConfig.ContainsKey("Duration"))
      {
        this._duration = g.AppConfig.GetFloat("Duration");
        txtDuration.Text = this._duration.ToString("0.000"); 
      }

      _initializationComplete = true;

      dtpFvApiFromDT.Value = DateTime.Now.AddDays(-15);
      dtpFvApiToDT.Value = DateTime.Now.AddDays(1);  

      SetServerAndPort();
    }

    private void DisplaySystemInfo()
    {
      var sysInfo = new SystemInfo();

      var frameworks = sysInfo.InstalledFrameworks;

      string highestFramework = sysInfo.HighestFramework;
      string highestFrameworkShort = sysInfo.HighestFrameworkShort; 

      StringBuilder sb = new StringBuilder();
      sb.Append("Frameworks" + g.crlf); 

      foreach(string framework in frameworks)
      {
        sb.Append("  " + framework + g.crlf); 
      }

      sb.Append(g.crlf); 
      sb.Append("HighestFramework" + g.crlf);
      sb.Append("  " + highestFramework + g.crlf); 
      sb.Append("  " + highestFrameworkShort + g.crlf); 
      sb.Append(g.crlf + sysInfo.SystemInfoString + g.crlf); 
      sb.Append(g.crlf + sysInfo.PlatformString + g.crlf); 

      txtOut.Text = sb.ToString();
    }

    private void ValidateSecurity()
    {
      var programConfig = g.AppConfig.ProgramConfigSet[g.AppConfig.ConfigName];
      var pfc = programConfig.ProgramFunctionControl;

      if (pfc.IsValid())
        txtOut.Text = "Security Configration is valid.";
      else
        txtOut.Text = pfc.GetValidationReport();
    }

    private void ListFunctionsByRole()
    {
      var programConfig = g.AppConfig.ProgramConfigSet[g.AppConfig.ConfigName];
      var pfc = programConfig.ProgramFunctionControl;

      StringBuilder sb = new StringBuilder();
        
      var groups = g.GetList("UserGroups");

      foreach (var group in groups)
      {
        if (sb.Length > 0)
          sb.Append(g.crlf);

        sb.Append("Checking security for group '" + group + g.crlf);

        var role = pfc.ProgramRoleSet.GetRoleByName(group); 
        if (role == null)
        {
          sb.Append("  No program role found for group '" + group + g.crlf);
          continue;
        }

        var functionNames = pfc.GetProgramFunctionNamesForRoleName(group);
        foreach (string functionName in functionNames)
        {
          sb.Append("  Program Function '" + functionName + "' is allowed." + g.crlf); 
        }
      }


      string result = sb.ToString();

      txtOut.Text = result;
    }

    private void RunSecurityTest()
    {
      var programConfig = g.AppConfig.ProgramConfigSet[g.AppConfig.ConfigName];
      var pfc = programConfig.ProgramFunctionControl;

      StringBuilder sb = new StringBuilder();
        
      var securityTest = g.GetList("SecurityTest");

      foreach (var securityTestEntry in securityTest)
      {
        var testGroup = securityTestEntry.Split(Constants.CommaDelimiter)[0];
        var testFunction = securityTestEntry.Split(Constants.CommaDelimiter)[1];

        var groupsForUser = new List<string>();
        groupsForUser.Add(testGroup);

        if (pfc.AllowFunctionForUserGroups(testFunction, groupsForUser))
          sb.Append("     " + testFunction.PadTo(30) + "   IS ALLOWED        for user in group   '" + testGroup + "'" + g.crlf); 
        else
          sb.Append("***  " + testFunction.PadTo(30) + "   IS NOT ALLOWED    for user in group   '" + testGroup + "'" + g.crlf); 
      }

      string result = sb.ToString();

      txtOut.Text = result;
    }

    private void LoginUser()
    {
      if (txtUserName.Text.IsBlank())
      {
        txtOut.Text = "User name is blank.";
        return;
      }


      var functionControl = g.AppConfig.ProgramConfigSet[g.AppInfo.ConfigName].ProgramFunctionControl;
      var securityModel = g.GetCI("SecurityModel").ToEnum<SecurityModel>(SecurityModel.Config);

      string userName = txtUserName.Text.Trim();
      _userSession = new UserSession();
      _userSession.ConfigSecurity = g.AppConfig.GetConfigSecurityForUser(userName); 
      _userSession.ConfigSecurity.Initialize(functionControl, userName, securityModel);

      string securityReport = _userSession.ConfigSecurity.GetUserGroupAndFunctionSummary();

      txtOut.Text = "User Group and Function Summary" + g.crlf2 + securityReport;
    }

    private void CheckUserFunction()
    {
      if (txtCheckUserFunction.Text.IsBlank())
      {
        txtOut.Text = "Function is blank.";
        return;
      }

      string requestedFunction = txtCheckUserFunction.Text.Trim();
      bool functionAllowed = g.AppConfig.ConfigSecurity.AllowFunctionForUser(requestedFunction);

      txtOut.Text = "Function '" + requestedFunction + "' is " + (functionAllowed ? "allowed" : "not allowed");
    }

    private void UseCPU()
    {
      while (_continueUsingCpu)
      {
        int i = 0; 
        byte[] binaryData = File.ReadAllBytes(@"C:\_data\binaryData.bin");
        foreach (byte b in binaryData)
        {
          i++;
          string s = b.ToString();
          if (i % 10000 == 0)
          {
            txtOut.Text = "Loop " + i.ToString();
            Application.DoEvents();
            //System.Threading.Thread.Sleep(10);
          }
        }
        Application.DoEvents();
      }          
    }

    private void UpgradePermissions()
    {
      txtOut.Text = "Upgrading Permissions";
      string sql = "exec sp_setapprole 'TiAppSystem', 'gen126_Titanium456'";

      IDbCommand cmd = conn.CreateCommand();
      cmd.CommandTimeout = 60; 
      cmd.CommandText = sql;
      cmd.ExecuteNonQuery();
    }

    private void TestSecurityToken()
    {
      SecurityToken t = new SecurityToken();
      t.AccountId = 123456;
      t.AuthenticationDateTime = DateTime.Now;
      t.TokenExpirationDateTime = t.AuthenticationDateTime.AddMinutes(30);

      string token = t.SerializeToken();

      txtOut.Text = token;
    }

    private void DecryptToken()
    {
      SecurityToken t = new SecurityToken();
      string decryptedToken = t.DeserializeToken(txtOut.Text);

      txtOut.Text = decryptedToken;
    }


    private void RunFvWebService()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        if (_fvAuthId.IsBlank())
        {
          string companyCode = g.GetCI("CompanyCode"); 
          string securityKey = g.GetCI("SecurityKey"); 
          string username = securityKey.Split(Constants.CommaDelimiter).FirstOrDefault();
          string password = securityKey.Split(Constants.CommaDelimiter).LastOrDefault();
          string authId = string.Empty;

          if (!Login(companyCode, username, password, out authId))
          {
            MessageBox.Show("Web service login failed.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            txtOut.Text = "Web service login failed.";
            this.Cursor = Cursors.Default;
            return;
          }

          _fvAuthId = authId;
        }

        DateTime startDate = dtpFvApiFromDT.Value;
        DateTime endDate = dtpFvApiToDT.Value; 

        string entity = cboFvEntity.Text;

        string content = FvWebService(_fvAuthId, entity, startDate, endDate, ckGaugeToGauge.Checked);

        txtOut.Text = FormatJSON(content); 
      }
      catch (Exception ex)
      {
          MessageBox.Show("Exception occurred attemping to use FieldVisor web service." + g.crlf2 + ex.ToReport(), 
                          "Web Service Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          txtOut.Text = "Exception occurred attemping to use FieldVisor web service." + g.crlf2 + ex.ToReport();
          
      }

      this.Cursor = Cursors.Default;
    }


    private string FvWebService(string authId, string servicename, DateTime? startdatetime, DateTime? enddatetime, bool useGaugeToGauge)
    {
      try
      {
        string _pullURI = g.GetCI("FieldingSystemsURI") + "/" + servicename;

        if (startdatetime != null)
          _pullURI += "?StartDT=" + Convert.ToDateTime(startdatetime).ToShortDateString();

        if (enddatetime != null)
          _pullURI += "&EndDT=" + Convert.ToDateTime(enddatetime).ToShortDateString();

        string wellID = txtFvApiWellID.Text.Trim();


        if (servicename == "dailyproduction" && wellID.IsNumeric())
          _pullURI += "&WellId=" + wellID;
 
        if (servicename == "productionentry" && wellID.IsNumeric())
          _pullURI += "&WellId=" + wellID;

        if (servicename == "waterticket" && wellID.IsNumeric())
          _pullURI += "&WellId=" + wellID;

        if (servicename == "oilticket" && wellID.IsNumeric())
          _pullURI += "&WellId=" + wellID;

        if (servicename == "tankentryendofday" && wellID.IsNumeric())
          _pullURI += "&WellId=" + wellID;

        if (servicename == "downtime" && wellID.IsNumeric())
          _pullURI += "&WellId=" + wellID;

        if (useGaugeToGauge)          
          _pullURI += "&G2GDates=true";

        var http = (HttpWebRequest)WebRequest.Create(new Uri(_pullURI));
        http.Timeout = 180000;
        http.Accept = "application/json";
        http.ContentType = "application/json";
        http.Method = "GET";
        http.Headers["AuthID"] = authId;

        var response = http.GetResponse();

        var stream = response.GetResponseStream();
        var sr = new StreamReader(stream);
        var content = sr.ReadToEnd();
        //content - DATA RETURNED

        return content;

      }
      catch (Exception ex)
      {
      }

      return string.Empty;
    }

    private string FormatJSON(string json)
    {
      dynamic parsedJson = JsonConvert.DeserializeObject(json);
      return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
    }

    //This is how to authenticate to the API via the console app.
    private static bool Login(string companyCode, string username, string password, out string authId)
    {
      authId = String.Empty;

      string _authURI = g.GetCI("AuthenticateURI");
      var http = (HttpWebRequest)WebRequest.Create(new Uri(_authURI));
      http.Accept = "application/json";
      http.ContentType = "application/json";
      http.Method = "POST";

      var authRequestObj = new Request.Authentication(companyCode, username, password);

      string requestContent = new JavaScriptSerializer().Serialize(authRequestObj);

      ASCIIEncoding encoding = new ASCIIEncoding();
      Byte[] bytes = encoding.GetBytes(requestContent);

      Stream newStream = http.GetRequestStream();
      newStream.Write(bytes, 0, bytes.Length);
      newStream.Close();

      var response = http.GetResponse();

      var stream = response.GetResponseStream();
      var sr = new StreamReader(stream);
      var content = sr.ReadToEnd();

      var authResponseObj = new JavaScriptSerializer().Deserialize<Response.Authentication>(content);

      authId = authResponseObj.AuthID;
      return authResponseObj.Status.Success;
    }

    private void ShareFileAPI()
    {
      //try
      //{
      //  string _pullURI = g.GetCI("FieldingSystemsURI") + "/" + servicename;

      //  if (startdatetime != null)
      //    _pullURI += "?StartDT=" + Convert.ToDateTime(startdatetime).ToShortDateString();

      //  if (enddatetime != null)
      //    _pullURI += "&EndDT=" + Convert.ToDateTime(enddatetime).ToShortDateString();

      //  string wellID = txtFvApiWellID.Text.Trim();


      //  if (servicename == "dailyproduction" && wellID.IsNumeric())
      //    _pullURI += "&WellId=" + wellID;

      //  if (servicename == "productionentry" && wellID.IsNumeric())
      //    _pullURI += "&WellId=" + wellID;

      //  if (useGaugeToGauge)
      //    _pullURI += "&G2GDates=true";

      //  var http = (HttpWebRequest)WebRequest.Create(new Uri(_pullURI));
      //  http.Timeout = 180000;
      //  http.Accept = "application/json";
      //  http.ContentType = "application/json";
      //  http.Method = "GET";
      //  http.Headers["AuthID"] = authId;

      //  var response = http.GetResponse();

      //  var stream = response.GetResponseStream();
      //  var sr = new StreamReader(stream);
      //  var content = sr.ReadToEnd();
      //  //content - DATA RETURNED

      //  return content;

      //}
      //catch (Exception ex)
      //{
      //}

      //return string.Empty;
    }

    private void RunTestLoop()
    {
      if (!ValidateParameters(null))
        return;


      TestEndDateTime = DateTime.Now.AddSeconds(this._duration);

      if (this._frequency > 0)
      {
        _timer = new System.Timers.Timer();
        _timer.Interval = this._frequency * 1000;
        _timer.Elapsed += _timer_Elapsed;
        _timer.Enabled = true;
      }
      else
      {
        DateTime beginDt = DateTime.Now;
        int transCount = 0;
        while (DateTime.Now < TestEndDateTime)
        {
          SendMessage();
          transCount++;
        }
        TimeSpan ts = DateTime.Now - beginDt;
        float totalSeconds = (float) ts.TotalSeconds;
        float tps = transCount / totalSeconds;

        txtOut.Text = "Test Loop Results" + g.crlf2 +
                      "  Transaction Count     : " + transCount.ToString() + g.crlf +
                      "  Elapsed Seconds       : " + totalSeconds.ToString() + g.crlf +
                      "  TPS                   : " + tps.ToString() + g.crlf; 

      }
    }

    private void _timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      if (DateTime.Now > TestEndDateTime)
      {
        _timer.Enabled = false;
        return;
      }

      SendMessage();
    }

    private void CancelTestLoop()
    {
      if (_timer != null)
          _timer.Enabled = false;
    }

    private void SetConfigWsSpec()
    {
      string host = cboWebServiceHost.Text;
      string webService = cboWebService.Text;
      _trans = cboTransaction.Text;
      parm = txtParameters.Text.ToBoolean();

      _configWsSpec = g.AppConfig.GetWsSpec("WsHost"); 

      string env = "PROD";

      switch (host)
      {
        case "10.3.12.37":
        case "173.248.152.174":
        case "OKC1WEB0001":
          env = "PROD";
          break;

        case "localhost":
          env = "LOCAL";
          break;

        default:
          env = "DEV";
          break;
      }

      _configWsSpec.WsHost = host;
      _configWsSpec.WsPort = GetWsPort(env, webService);
      if (webService == "MainSvc")
        _configWsSpec.WsServiceName = "MainSvc.svc";
      else if (webService == "OpsMgmt01")
        _configWsSpec.WsServiceName = "OpsMgmt01.svc";
    }

    private void SetServerAndPort()
    {
      SetConfigWsSpec();
      txtPort.Text = _configWsSpec.WsPort; 
    }

    private void SendMessage()
    {
      var f = new ObjectFactory2();

      DateTime dtBegin = DateTime.Now;

      var wsParms = BuildWsParms();
      WsMessage responseMessage = null;


      if (!ValidateParameters(wsParms))
      {
        this.Cursor = Cursors.Default;
        return;
      }

      UpdateWsParms(wsParms);

      var messageFactory = GetMessageFactory(wsParms);

      if (messageFactory == null)
        return;

      var requestMessage = messageFactory.CreateRequestMessage(wsParms);

      XElement transactionBody = requestMessage.TransactionBody;

      var f3 = new ObjectFactory2();
      var excelExtractRequest = f3.Deserialize(transactionBody) as ExcelExtractRequest; 

      responseMessage = WsClient.InvokeServiceCall(wsParms, requestMessage);

            
      wsCallCount++;


      switch (responseMessage.TransactionHeader.TransactionName)
      {
        case "ErrorResponse":
          ErrorResponse errorResponse = f.Deserialize(responseMessage.TransactionBody) as ErrorResponse;
          string errorResponseMessage = errorResponse.Message; 
          errorResponseMessage += errorResponse.HasException ? (g.crlf + errorResponse.WsException.ToReport()) : (g.crlf + "No exception" + g.crlf);
          if (this.InvokeRequired)
            this.Invoke((Action)((() => txtOut.Text += errorResponseMessage)));
          else
            txtOut.Text += errorResponseMessage;
          this.Cursor = Cursors.Default;
          return;

        case "WsCommand":
            if (ckShowMessageOutput.Checked)
            {
              ObjectFactory2 f2 = new ObjectFactory2();
              WsCommandResponse commandResponse = f2.Deserialize(responseMessage.TransactionBody) as WsCommandResponse;
              string commandResponseMessage = f2.Serialize(commandResponse).ToString();
              if (this.InvokeRequired)
                this.Invoke((Action)((() => txtOut.Text += commandResponseMessage)));
              else
                txtOut.Text += commandResponseMessage;
              this.Cursor = Cursors.Default;
              return;
            }
            break;

        case "GetAssemblyReport":
            if (ckShowMessageOutput.Checked)
            {
              ObjectFactory2 f2 = new ObjectFactory2();
              GetAssemblyReportResponse assemblyResponse = f2.Deserialize(responseMessage.TransactionBody) as GetAssemblyReportResponse;
              string assemblyResponseMessage = f2.Serialize(assemblyResponse).ToString();
              if (this.InvokeRequired)
              {
                this.Invoke((Action)((() =>  
                {
                  txtOut.Text += assemblyResponseMessage;
                  this.Cursor = Cursors.Default;
                })));
              }
              else
              {
                txtOut.Text += assemblyResponse.AssemblyReport.ToString();
                this.Cursor = Cursors.Default;
              }
              return;
            }
            break;

        case "GetRunningTasksReport":
          if(ckShowMessageOutput.Checked)
          {
            ObjectFactory2 f2 = new ObjectFactory2();
            GetRunningTasksReportResponse runningTasksResponse = f2.Deserialize(responseMessage.TransactionBody) as GetRunningTasksReportResponse;
            string runningTasksResponseMessage = f2.Serialize(runningTasksResponse).ToString();
            if (this.InvokeRequired)
            {
              this.Invoke((Action)((() =>
              {
                txtOut.Text += runningTasksResponseMessage;
                this.Cursor = Cursors.Default;
              })));
            }
            else
            {
              txtOut.Text += runningTasksResponse.RunningTasksReport.ToString();
              this.Cursor = Cursors.Default;
            }
            return;
          }
          break;
      }


      if (this._frequency == 0)
        return;

      string transResult = "Transaction Result" + g.crlf;
      StringBuilder sb = new StringBuilder();

      switch (responseMessage.TransactionName)
      {
        //case "CheckForUpdates":
        //  CheckForUpdatesResponse checkForUpdatesResponse =
        //        f.Deserialize(responseMessage.TransactionBody) as CheckForUpdatesResponse;

        //  if (!checkForUpdatesResponse.UpgradeAvailable)
        //  {
        //    _softwareUpdateVersion = String.Empty;
        //    _softwareUpdatePlatformString = String.Empty;
        //    sb.Append("No update is available for ");
        //  }
        //  else
        //  {
        //    _softwareUpdateVersion = checkForUpdatesResponse.UpgradeVersion;
        //    _softwareUpdatePlatformString = checkForUpdatesResponse.PlatformString;

        //    sb.Append("Upgrade is available from version '" + checkForUpdatesResponse.CurrentVersion + ":" + g.crlf +
        //      "  Version   : " + checkForUpdatesResponse.UpgradeVersion + g.crlf +
        //      "  Platform  : " + checkForUpdatesResponse.PlatformString + g.crlf);                 
        //  }

        //  transResult += sb.ToString();
        //  break;

        //case "DownloadSoftware":
        //  DownloadSoftwareResponse downloadSoftwareResponse =
        //        f.Deserialize(responseMessage.TransactionBody) as DownloadSoftwareResponse;

        //  switch (downloadSoftwareResponse.ResponseType)
        //  {
        //    case ResponseType.Ready:
        //      transResult += g.crlf + "Software download ready - file size is " +
        //                      downloadSoftwareResponse.TotalFileSize.ToString("#,###,###,##0") +
        //                      " segment count is " + downloadSoftwareResponse.TotalSegments.ToString("##,##0");
        //      break;

        //    case ResponseType.SegmentReturned:
        //      transResult += g.crlf + "Software download in progress, segment " +
        //                      downloadSoftwareResponse.SegmentNumber.ToString("##,##0") + " of " +
        //                      downloadSoftwareResponse.TotalSegments.ToString("##,##0") + " received " +
        //                      "segment size is " + downloadSoftwareResponse.SegmentSize.ToString("##,###,##0") +
        //                      " total file size is " + downloadSoftwareResponse.TotalFileSize.ToString("#,###,###,##0");
        //      txtParameters.Text = (downloadSoftwareResponse.SegmentNumber + 1).ToString();

        //      break;


        //  }

        //  if (downloadSoftwareResponse.SegmentData.IsNotBlank())
        //  {
        //    transResult += g.crlf + downloadSoftwareResponse.SegmentData.PadTo(1000);
        //  }

        //  transResult += sb.ToString();
        //  break;


        case "ExcelExtract":
          ExcelExtractResponse excelExtractResponse = f.Deserialize(responseMessage.TransactionBody) as ExcelExtractResponse;
          string workbook = responseMessage.TransactionBody.ToString();
          txtOut.Text = workbook;
          return;
      }


      string result = String.Empty;
      string perfResult = String.Empty;

      if (ckTrackPerformance.Checked)
      {
        perfResult = responseMessage.MessageHeader.PerformanceInfoSet.GetReport(); 
      }
        
      result = wsCallCount.ToString("00000") + "   " +
                      responseMessage.MessageHeader.ServerResponseTime.Seconds.ToString("00") + "." +
                      responseMessage.MessageHeader.ServerResponseTime.Milliseconds.ToString("000") + "    " +
                      responseMessage.MessageHeader.TotalResponseTime.Seconds.ToString("00") + "." +
                      responseMessage.MessageHeader.TotalResponseTime.Milliseconds.ToString("000") + g.crlf  + 
                      responseMessage.MessageDebug.Replace("^", g.crlf);



      result += g.crlf + transResult + g.crlf + perfResult;

      TimeSpan ts = DateTime.Now - dtBegin;

      result += g.crlf + "Overall Duration: " + ts.TotalSeconds.ToString("000") + "." + ts.Milliseconds.ToString("000") + g.crlf2;

      if (this.InvokeRequired)
        this.Invoke((Action)((() => txtOut.Text = result)));
      else
        txtOut.Text = result;
    }


    private WsParms BuildWsParms()
    {
      WsParms wsParms = new WsParms();
      wsParms.TransactionName = _trans;
      wsParms.TransactionVersion = "1.0.0.0";
      wsParms.MessagingParticipant = MessagingParticipant.Sender;
      wsParms.ConfigWsSpec = _configWsSpec;
      wsParms.TrackPerformance = ckTrackPerformance.Checked;

      wsParms.DomainName = g.SystemInfo.DomainName;
      wsParms.MachineName = g.SystemInfo.ComputerName;
      wsParms.UserName = g.SystemInfo.UserName;
      wsParms.ModuleCode = g.AppInfo.ModuleCode;
      wsParms.ModuleName = g.AppInfo.ModuleName;
      wsParms.ModuleVersion = g.AppInfo.AppVersion;
      wsParms.AppName = g.AppInfo.AppName;
      wsParms.AppVersion = g.AppInfo.AppVersion;

      wsParms.ModuleCode = 1224;
      wsParms.ModuleName = "WebSvcTest";
      wsParms.ModuleVersion = "1.0.0.0";
      wsParms.OrgId = 3;

      return wsParms;
    }

    private void UpdateWsParms(WsParms wsParms)
    {
      switch (wsParms.TransactionName)
      {
        case "GetAssemblyReport":
          wsParms.ParmSet.Add(new Parm("IncludeAllAssemblies", txtParameters.Text.ToBoolean()));
          break;

        case "ExcelExtract":
          wsParms.ParmSet.Add(new Parm("FullPath", g.CI("FullPath")));

          //var worksheetsToInclude = g.GetList("WorksheetsToInclude");
          //if (worksheetsToInclude != null && worksheetsToInclude.Count > 0)
          //{
          //  wsParms.ParmSet.Add(new Parm("WorksheetsToInclude", worksheetsToInclude)); 
          //}

          break;

        case "WsCommand":
          WsCommand wsCommand = new WsCommand();
          wsCommand.WsCommandName = cboCommandName.Text.ToEnum<WsCommandName>(WsCommandName.NotSet);

          wsCommand.Message = txtParameters.Text;
          //wsCommand.Parms.Add("ServiceName", txtParameters.Text);
          Parm parm = new Parm("WsCommand", wsCommand);
          parm.ParameterType = typeof(WsCommand);
          wsParms.ParmSet.Add(parm);
          break;
      }
    }


    public IMessageFactory GetMessageFactory(WsParms wsParms)
    {
      string transactionVersion = wsParms.TransactionName + "_" + wsParms.TransactionVersion;

      try
      {
        if (this.LoadedMessageFactories.ContainsKey(transactionVersion))
          return this.LoadedMessageFactories[transactionVersion];

        foreach (Lazy<IMessageFactory, IMessageFactoryMetadata> messageFactory in messageFactories)
        {
          if (messageFactory.Metadata.Transactions.Contains(transactionVersion))
          {
            var factory = messageFactory.Value;
            this.LoadedMessageFactories.Add(transactionVersion, factory);
            return messageFactory.Value;
          }
        }

        return null;
      }
      catch (CompositionContractMismatchException cex)
      {
        MessageBox.Show("A composition contract mismatch exception has occurred while attempting to retrieve the message factory for transaction '" + transactionVersion + "'." +
                        g.crlf2 + cex.ToReport(), "WebSvcTest - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        return null;
      }
      catch (Exception ex)
      {
        MessageBox.Show("A exception has occurred while attempting to retrieve the message factory for transaction '" + transactionVersion + "'." +
                        g.crlf2 + ex.ToReport(), "WebSvcTest - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        return null;
      }
    }


    private void DownloadSoftware()
    {
      //var f = new ObjectFactory2();

      //DateTime dtBegin = DateTime.Now;

      //WsParms wsParms = new WsParms();
      //wsParms.TransactionName = "DownloadSoftware";
      //wsParms.TransactionVersion = "1.0.0.0";
      //wsParms.MessagingParticipant = MessagingParticipant.Sender; 
      //wsParms.ConfigWsSpec = _configWsSpec;
      //wsParms.TrackPerformance = ckTrackPerformance.Checked;
        
      //wsParms.DomainName = g.SystemInfo.DomainName;
      //wsParms.MachineName = g.SystemInfo.ComputerName; 
      //wsParms.UserName = g.SystemInfo.UserName; 
      //wsParms.ModuleCode = g.AppInfo.ModuleCode;
      //wsParms.ModuleName = g.AppInfo.ModuleName;
      //wsParms.ModuleVersion = g.AppInfo.AppVersion;
      //wsParms.AppName = g.AppInfo.AppName;
      //wsParms.AppVersion = g.AppInfo.AppVersion; 

      //wsParms.ModuleCode = 301;
      //wsParms.ModuleName = "BulkTrans.RPDM";
      //wsParms.ModuleVersion = "1.0.0.0";  
      //wsParms.OrgId = 3; 

      //WsMessage responseMessage = null;
      //WsMessage requestMessage = null;
      //DownloadSoftwareRequest downloadSoftwareRequest = null;

      //using (var messageFactory = GetMessageFactory(wsParms))
      //{
      //  requestMessage = messageFactory.CreateRequestMessage(wsParms);

      //  downloadSoftwareRequest = f.Deserialize(requestMessage.TransactionBody) as DownloadSoftwareRequest;
      //  downloadSoftwareRequest.RequestType = RequestType.InitialRequest;

      //  if (txtParameters.Text.IsNumeric())
      //  {
      //    downloadSoftwareRequest.RequestType = RequestType.GetNextSegment;
      //    downloadSoftwareRequest.SegmentNumber = txtParameters.Text.ToInt32();
      //  }

      //  downloadSoftwareRequest.UpgradeVersion = _softwareUpdateVersion;
      //  downloadSoftwareRequest.UpgradePlatformString = _softwareUpdatePlatformString;
      //  requestMessage.TransactionBody = f.Serialize(downloadSoftwareRequest); 

      //  responseMessage = WsClient.InvokeServiceCall(wsParms, requestMessage);
      //}            
            
      //wsCallCount++;

      //if (responseMessage.TransactionHeader.TransactionName == "ErrorResponse")
      //{
      //  ErrorResponse errorResponse = f.Deserialize(responseMessage.TransactionBody) as ErrorResponse;
      //  string errorResponseMessage = errorResponse.Message; 
      //  errorResponseMessage += errorResponse.HasException ? (g.crlf + errorResponse.WsException.ToReport()) : (g.crlf + "No exception" + g.crlf);
      //  if (this.InvokeRequired)
      //    this.Invoke((Action)((() => txtOut.Text += errorResponseMessage)));
      //  else
      //    txtOut.Text += errorResponseMessage;
      //  this.Cursor = Cursors.Default;
      //  return;
      //}

      //if (this._frequency == 0)
      //  return;

      //string transResult = "Transaction Result" + g.crlf;
      //StringBuilder sb = new StringBuilder();

      //DownloadSoftwareResponse downloadSoftwareResponse =
      //      f.Deserialize(responseMessage.TransactionBody) as DownloadSoftwareResponse;


      //int totalSegments = downloadSoftwareResponse.TotalSegments;

      //for (int i = 1; i < totalSegments + 1; i++)
      //{
      //  downloadSoftwareRequest.SegmentNumber = i;
      //  downloadSoftwareRequest.RequestType = RequestType.GetNextSegment;
      //  requestMessage.TransactionBody = f.Serialize(downloadSoftwareRequest); 
      //  responseMessage = WsClient.InvokeServiceCall(wsParms, requestMessage);
      //  if (responseMessage.TransactionHeader.TransactionName == "ErrorResponse")
      //  {
      //    MessageBox.Show("Temporary Error when bad web service call");
      //    return;
      //  }

      //  downloadSoftwareResponse = f.Deserialize(responseMessage.TransactionBody) as DownloadSoftwareResponse;

      //  transResult += g.crlf + "Software download in progress, segment " +
      //                  downloadSoftwareResponse.SegmentNumber.ToString("##,##0") + " of " +
      //                  downloadSoftwareResponse.TotalSegments.ToString("##,##0") + " received " +
      //                  "segment size is " + downloadSoftwareResponse.SegmentSize.ToString("##,###,##0") +
      //                  " total file size is " + downloadSoftwareResponse.TotalFileSize.ToString("#,###,###,##0") + 
      //                  g.crlf + downloadSoftwareResponse.SegmentData.PadTo(1000) + g.crlf;

      //  txtOut.Text = transResult;
      //  Application.DoEvents();
      //}

      
      //if (downloadSoftwareResponse.SegmentData.IsNotBlank())
      //{
      //  transResult += g.crlf + downloadSoftwareResponse.SegmentData.PadTo(1000); 
      //}

      //transResult += sb.ToString();


      //string result = String.Empty;
      //string perfResult = String.Empty;

      //if (ckTrackPerformance.Checked)
      //{
      //  perfResult = responseMessage.MessageHeader.PerformanceInfoSet.GetReport(); 
      //}
        
      //result = wsCallCount.ToString("00000") + "   " +
      //                responseMessage.MessageHeader.ServerResponseTime.Seconds.ToString("00") + "." +
      //                responseMessage.MessageHeader.ServerResponseTime.Milliseconds.ToString("000") + "    " +
      //                responseMessage.MessageHeader.TotalResponseTime.Seconds.ToString("00") + "." +
      //                responseMessage.MessageHeader.TotalResponseTime.Milliseconds.ToString("000") + g.crlf  + 
      //                responseMessage.MessageDebug.Replace("^", g.crlf);



      //result += g.crlf + transResult + g.crlf + perfResult;

      //TimeSpan ts = DateTime.Now - dtBegin;

      //result += g.crlf + "Overall Duration: " + ts.TotalSeconds.ToString("000") + "." + ts.Milliseconds.ToString("000") + g.crlf2;

      //if (this.InvokeRequired)
      //  this.Invoke((Action)((() => txtOut.Text = result)));
      //else
      //  txtOut.Text = result;
    }


    private bool ValidateParameters(WsParms wsParms)
    {
      if (wsParms != null)
      {
        switch (wsParms.TransactionName)
        {
          case "GetAssemblyReport":
            if (!txtParameters.Text.IsBoolean())
            {
              MessageBox.Show("Please enter a valid boolean parameter for the 'IncludeAllAssemblies' property.",
                              "WebSvcTest - Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              txtParameters.SelectAll();
              txtParameters.Focus();
              return false;
            }

            break;

        }

        return true;
      }

      string frequencyString = txtFrequency.Text.Trim();
      decimal frequency = 0;
      Decimal.TryParse(frequencyString, out frequency);

      string durationString = txtDuration.Text.Trim();
      decimal duration = 0;
      Decimal.TryParse(durationString, out duration);

      if ((float)frequency < 0.000F || (float) frequency > 30.000)
      {
        txtOut.Text = "Frequency must be greather than 0 milliseconds and less than 30 seconds.";
        return false;
      }

      if ((float)duration < 5.00F || (float) duration > 21600.00)
      {
        txtOut.Text = "Duration must be greather than 5 seconds and not greather than 6 hours.";
        return false;
      }

      this._frequency = (float)frequency;
      this._duration = (float)duration;

      return true;
    }

    private void PingPort()
    {
      this.Cursor = Cursors.WaitCursor;

      txtOut.Clear();
      Application.DoEvents();

      if (txtPort.Text.IsNotNumeric())
      {
        txtOut.Text = "Port is not numeric.";
        this.Cursor = Cursors.Default;
        return;
      }

      string ipAddress = cboWebServiceHost.Text;
      int port = Int32.Parse(txtPort.Text.Trim()); 

      TaskResult taskResult = PortPing.PingPort(ipAddress, port);

      txtOut.Text = taskResult.Message;

      this.Cursor = Cursors.Default; 
    }

    private void Tokenize()
    {
      string rawData = txtTokenIn.Text;
      if (ckConciseTokenization.Checked)
        txtDetokenizeIn.Text = TokenMaker.GenerateToken2(rawData);
      else
        txtDetokenizeIn.Text = TokenMaker.GenerateToken(rawData);
    }

    private void Detokenize()
    {
      string tokenizedData = txtDetokenizeIn.Text.Trim();

      if (ckConciseTokenization.Checked)
        txtOut.Text = TokenMaker.DecodeToken2(tokenizedData);
      else
        txtOut.Text = TokenMaker.DecodeToken(tokenizedData);
    }

    private void GenerateSendFiles()
    {
      ClearFilesToSend();

      string sendFilesFolder = g.AppConfig.GetCI("SendFilesFolder");
      if (sendFilesFolder != "EXPORTS")
        return;

      sendFilesFolder = g.AppDataPath + @"\Exports";

      StringBuilder sb = new StringBuilder();
      int sendFileGenerateCount = g.AppConfig.GetInteger("SendFilesGenerate");

      DateTime dt = DateTime.Now;
      string datePortion = dt.ToString("yyyyMMddHHmmssfff"); 

      for (int i = 0; i < sendFileGenerateCount; i++)
      {
        string fileName = "TestFile-" + datePortion + "-" + i.ToString("000") + ".txt";
        sb.Append("  " + fileName + g.crlf);
        string fileData = GetFileData(fileName);
        File.WriteAllText(sendFilesFolder + @"\" + fileName , fileData); 
      }

      txtOut.Text = "Will generate " + sendFileGenerateCount.ToString() + " files in folder '" + sendFilesFolder + "'." + g.crlf2 + sb.ToString();
    }

    private string GetFileData(string fileName)
    {
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < 300; i++)
      {
        sb.Append(fileName + "|" + fileName + "|" + fileName + "|" + fileName + "|" + fileName + "|" + fileName + "|" + fileName + "|" + fileName + "|" + fileName + g.crlf);
      }

      return sb.ToString();
    }

    private List<FileObject> GetFilesToSend()
    {
      List<FileObject> filesToSend = new List<FileObject>();

      string sendFilesFolder = g.AppConfig.GetCI("SendFilesFolder");
      if (sendFilesFolder != "EXPORTS")
        return filesToSend;

      sendFilesFolder = g.AppDataPath + @"\Exports";
      List<string> files = Directory.GetFiles(sendFilesFolder).ToList();
      foreach (string file in files)
      {
        FileObject fo = new FileObject();
        fo.FileName = file;
        fo.Data = Convert.ToBase64String(Encoding.UTF8.GetBytes(File.ReadAllText(file)));
        filesToSend.Add(fo); 
      }

      return filesToSend;
    }

    private void ClearFilesToSend()
    {
      string sendFilesFolder = g.AppConfig.GetCI("SendFilesFolder");
      if (sendFilesFolder != "EXPORTS")
        return;

      sendFilesFolder = g.AppDataPath + @"\Exports";
      List<string> filesToDelete = Directory.GetFiles(sendFilesFolder).ToList();
      foreach (string fileToDelete in filesToDelete)
        File.Delete(fileToDelete); 
    }

    private void DropDowns_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (_initializationComplete)
        SetServerAndPort();
    }

    private void ListAppPools()
    {
      //this.Cursor = Cursors.WaitCursor;
      //string pools = WebSiteManager.GetAppPoolList();
      //txtOut.Text = pools;
      //this.Cursor = Cursors.Default;
    }

    private void ListWebSites()
    {
      //this.Cursor = Cursors.WaitCursor;
      //string sites = WebSiteManager.GetWebSiteList();
      //txtOut.Text = sites;
      //this.Cursor = Cursors.Default;
    }

    private void TestDxWorkbook()
    {
      //string excelPath = g.CI("FullPath");

      //DxWorkbook wb = Utility.GetWorkbook(excelPath);

      //var f = new ObjectFactory2();

      //XElement wbElement = f.Serialize(wb);

      //var wb2 = f.Deserialize(wbElement) as DxWorkbook;


    }


    private object GetWsPort_LockObject = new object();
    private string GetWsPort(string env, string serviceName)
    {
      lock (GetWsPort_LockObject)
      {
        switch (env)
        {
          case "PROD":
            switch (serviceName)
            {
              case "MainSvc":
                return "36301";
              case "OpsMgmt01":
                return "36302";
              case "GPTaskService01":
                return "36101";
              case "GPTaskService02":
                return "36102";
            }
            break;


          case "DEV":
            switch (serviceName)
            {
              case "MainSvc":
                return "32301";
              case "OpsMgmt01":
                return "32302";
              case "GPTaskService01":
                return "32101";
              case "GPTaskService02":
                return "32102";
            }
            break;


          case "LOCAL":
            switch (serviceName)
            {
              case "MainSvc":
                return "32001";
              case "OpsMgmt01":
                return "32002";
              case "GPTaskService01":
                return "32101";
              case "GPTaskService02":
                return "32102";
            }
            break;
        }

        throw new Exception("Web service '" + serviceName + "' in environment '" + env + "' is not defined in WscHelper.GetWsPort method.");
      }
    }
  }
}
    