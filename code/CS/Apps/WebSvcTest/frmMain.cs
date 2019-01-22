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
    private Logger logger;
    private bool parm;
    private UserSession _userSession;

    private Dictionary<string, string> _devUsers;
    private Dictionary<string, string> _hostsAndEnvironments;
    private Dictionary<string, string> _servicesAndEnvironments;
    private Dictionary<string, string> _serviceNames;
    private string _env;

    private bool _continueUsingCpu = true;

    private int wsCallCount = 0;

    private System.Timers.Timer _timer;
    private DateTime TestEndDateTime;

    private string _softwareUpdateVersion = String.Empty;
    private string _softwareUpdatePlatformString = String.Empty;

    private float _frequency;
    private float _duration;
    private ConfigWsSpec _configWsSpec;
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

        case "SendMessage":
          wsCallCount = 0;
          this.Cursor = Cursors.WaitCursor;
          txtOut.Clear();
          Application.DoEvents();

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
        new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the application." + g.crlf2 +
                          ex.ToReport(), "WebSvcTest - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }


      logger = new Logger();
      logger.Log(g.AppInfo.AppTitle + " v" + g.AppInfo.AppVersion + " Windows application initialization is starting.");


      int formHorizontalSize = g.GetCI("MainFormHorizontalSize").ToInt32OrDefault(90);
      int formVerticalSize = g.GetCI("MainFormVerticalSize").ToInt32OrDefault(90);

      this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * formHorizontalSize / 100,
                           Screen.PrimaryScreen.Bounds.Height * formVerticalSize / 100);
      this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2);

      XmlMapper.Load();

      txtFileName.Text = g.CI("FileName");
      txtParameters.Text = g.CI("Parameters");

      _hostsAndEnvironments = g.GetDictionary("HostsAndEnvironments");
      _servicesAndEnvironments = g.GetDictionary("ServicesAndEnvironments");
      _serviceNames = g.GetDictionary("ServiceNames");

      cboWebServiceHost.LoadItems(_hostsAndEnvironments.Keys.ToList(), false);
      cboWebServiceHost.SelectItem(g.CI("SelectedHost"));

      cboWebService.SelectItem(g.CI("SelectedWebService"));

      _devUsers = g.GetDictionary("DevUsers");

      cboTransaction.LoadItems(g.GetList("TransNames"), true);
      cboTransaction.SelectItem(g.CI("SelectedTransaction"));

      cboSubCommand.LoadItems(g.GetList("SubCommands"), true);
      cboSubCommandParameter.LoadItems(g.GetList("SubCommandParameters"), true);

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
    }

    private void DisplaySystemInfo()
    {
      var sysInfo = new SystemInfo();

      var frameworks = sysInfo.InstalledFrameworks;

      string highestFramework = sysInfo.HighestFramework;
      string highestFrameworkShort = sysInfo.HighestFrameworkShort;

      StringBuilder sb = new StringBuilder();
      sb.Append("Frameworks" + g.crlf);

      foreach (string framework in frameworks)
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
        float totalSeconds = (float)ts.TotalSeconds;
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
      _configWsSpec = null;
      txtPort.Text = String.Empty;

      string host = cboWebServiceHost.Text;
      string webService = cboWebService.Text;

      if (webService.IsBlank())
        return;

      int ptr = webService.IndexOf("(");
      if (ptr == -1)
      {
        MessageBox.Show("Invalid format of web service name '" + webService + "'." + g.crlf2 +
                        "The format must include the port number to be used in parentheses, for example 'ServiceName (32001)'.",
                        "WebSvcTest - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      string port = webService.Substring(ptr + 1).Trim().Replace(")", String.Empty);
      txtPort.Text = port;
      string wsName = webService.Substring(0, ptr).Trim();

      _configWsSpec = g.AppConfig.GetWsSpec("WsHost");

      _configWsSpec.WsHost = host;
      _configWsSpec.WsPort = port;
      if (_serviceNames.ContainsKey(wsName))
        _configWsSpec.WsServiceName = _serviceNames[wsName];
    }

    private void SendMessage()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        var f = new ObjectFactory2();

        DateTime dtBegin = DateTime.Now;

        var wsParms = InitializeWsParms(cboTransaction.Text);

        WsMessage responseMessage = null;

        if (!ValidateParameters(wsParms))
        {
          this.Cursor = Cursors.Default;
          return;
        }

        UpdateWsParms(wsParms);

        IMessageFactory messageFactory = GetMessageFactory(wsParms.TransactionName);

        if (messageFactory == null)
        {
          this.Cursor = Cursors.Default;
          MessageBox.Show("No MessageFactory object was created for transaction '" + wsParms.TransactionName + "'.",
                          "WebSvcTest - MessageFactory Creation Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        var requestMessage = messageFactory.CreateRequestMessage(wsParms);

        XElement transactionBody = requestMessage.TransactionBody;

        responseMessage = WsClient.InvokeServiceCall(wsParms, requestMessage);

        wsCallCount++;


        switch (responseMessage.TransactionHeader.TransactionName)
        {
          case "ErrorResponse":
            ErrorResponse errorResponse = f.Deserialize(responseMessage.TransactionBody, true) as ErrorResponse;
            string errorResponseMessage = errorResponse.Message;
            errorResponseMessage += errorResponse.HasException ? (g.crlf + errorResponse.WsException.ToReport()) : (g.crlf + "No exception" + g.crlf);
            if (this.InvokeRequired)
              this.Invoke((Action)((() => txtOut.Text += errorResponseMessage)));
            else
              txtOut.Text += errorResponseMessage;
            this.Cursor = Cursors.Default;
            return;

          case "WsCommand":
            if (!ckSuppressMessageOutput.Checked)
            {
              ObjectFactory2 f2 = new ObjectFactory2();
              WsCommandResponse commandResponse = f2.Deserialize(responseMessage.TransactionBody, true) as WsCommandResponse;
              string commandResponseMessage = f2.Serialize(commandResponse).ToString().Replace("&#xD;&#xA;", g.crlf);
              if (this.InvokeRequired)
                this.Invoke((Action)((() => txtOut.Text += commandResponseMessage)));
              else
                txtOut.Text += commandResponseMessage;
              this.Cursor = Cursors.Default;
              return;
            }
            break;

          case "GetAssemblyReport":
            if (!ckSuppressMessageOutput.Checked)
            {
              ObjectFactory2 f2 = new ObjectFactory2();
              GetAssemblyReportResponse assemblyResponse = f2.Deserialize(responseMessage.TransactionBody, true) as GetAssemblyReportResponse;
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
            if (!ckSuppressMessageOutput.Checked)
            {
              ObjectFactory2 f2 = new ObjectFactory2();
              GetRunningTasksReportResponse runningTasksResponse = f2.Deserialize(responseMessage.TransactionBody, true) as GetRunningTasksReportResponse;
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
          //        f.Deserialize(responseMessage.TransactionBody, true) as CheckForUpdatesResponse;

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
          //        f.Deserialize(responseMessage.TransactionBody, true) as DownloadSoftwareResponse;

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
            ExcelExtractResponse excelExtractResponse = f.Deserialize(responseMessage.TransactionBody, true) as ExcelExtractResponse;
            string workbook = responseMessage.TransactionBody.ToString();
            txtOut.Text = workbook;

            string report = excelExtractResponse.DxWorkbook.Report;
            txtOut2.Text = report;

            return;

          case "PdfExtract":
            PdfExtractResponse pdfExtractResponse = f.Deserialize(responseMessage.TransactionBody, true) as PdfExtractResponse;
            string workbook2 = responseMessage.TransactionBody.ToString();
            txtOut.Text = workbook2;
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
                        responseMessage.MessageHeader.TotalResponseTime.Milliseconds.ToString("000") + g.crlf +
                        responseMessage.MessageDebug.Replace("^", g.crlf);



        result += g.crlf + transResult + g.crlf + perfResult;

        TimeSpan ts = DateTime.Now - dtBegin;

        result += g.crlf + "Overall Duration: " + ts.TotalSeconds.ToString("000") + "." + ts.Milliseconds.ToString("000") + g.crlf2;

        if (this.InvokeRequired)
          this.Invoke((Action)((() => txtOut.Text = result)));
        else
          txtOut.Text = result;

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        txtOut.Text = "An exception occurred while processing the web service message." + g.crlf2 + ex.ToReport();
      }
    }


    private WsParms InitializeWsParms(string transName)
    {
      WsParms wsParms = new WsParms();
      wsParms.TransactionName = transName;
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
      WsCommand subCommand = null;
      string subCommandParameter = String.Empty;

      switch (wsParms.TransactionName)
      {
        case "ExcelExtract":
          wsParms.ParmSet.Add(new Parm("FullPath", txtFileName.Text));
          if (txtParameters.Text.IsNotBlank())
          {
            string[] parms = txtParameters.Text.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);
            wsParms.ParmSet.Add(new Parm("MapName", parms[0].ToString()));
            wsParms.ParmSet.Add(new Parm("FileExtractMode", parms[1]));
          }
          break;

        case "PdfExtract":
          wsParms.ParmSet.Add(new Parm("FullPath", txtFileName.Text));
          if (txtParameters.Text.IsNotBlank())
          {
            wsParms.ParmSet.Add(new Parm("MapName", txtParameters.Text));
          }
          break;

        case "WsCommand":
          subCommand = new WsCommand();
          subCommand.WsCommandName = cboSubCommand.Text.ToEnum<WsCommandName>(WsCommandName.NotSet);
          subCommandParameter = cboSubCommandParameter.Text;

          switch (subCommand.WsCommandName)
          {
            case WsCommandName.GetAssemblyReport:
              bool includeAllAssemblies = true;
              if (subCommandParameter.IsBoolean())
                includeAllAssemblies = subCommandParameter.ToBoolean();
              subCommand.Parms.Add("IncludeAllAssemblies", includeAllAssemblies.ToString());
              wsParms.ParmSet.Add(new Parm("Command" + wsParms.ParmSet.Count.ToString(), subCommand));
              break;
              
            case WsCommandName.GetAppDomainReport:
              wsParms.ParmSet.Add(new Parm("Command" + wsParms.ParmSet.Count.ToString(), subCommand));
              break;
          }
          break;

        case "ServiceEngineCommand":
          subCommand = new WsCommand();
          subCommand.WsCommandName = cboSubCommand.Text.ToEnum<WsCommandName>(WsCommandName.NotSet);
          subCommandParameter = cboSubCommandParameter.Text;

          switch (subCommand.WsCommandName)
            {
            case WsCommandName.FlushAppDomains:
              wsParms.ParmSet.Add(new Parm("Command" + wsParms.ParmSet.Count.ToString(), subCommand));
              break;
          }
          break;


          //if (cboSubCommandParameter.Text.IsNotBlank())
          //{
          //  var cmd = new WsCommand();
          //  switch (cboSubCommandParameter.Text.Trim().ToLower())
          //  {
          //    case "start":
          //      cmd.WsCommandName = WsCommandName.StartWinService;
          //      break;

          //    case "stop":
          //      cmd.WsCommandName = WsCommandName.StopWinService;
          //      break;

          //    case "pause":
          //      cmd.WsCommandName = WsCommandName.PauseWinService;
          //      break;

          //    case "resume":
          //      cmd.WsCommandName = WsCommandName.ResumeWinService;
          //      break;

          //    case "refreshtasklist":
          //      cmd.WsCommandName = WsCommandName.RefreshTaskList;
          //      break;

          //    case "refreshtaskrequests":
          //      cmd.WsCommandName = WsCommandName.RefreshTaskRequests;
          //      break;
          //  }

          //  wsParms.ParmSet.Add(new Parm("Command" + wsParms.ParmSet.Count.ToString(), cmd));
          //}
          //break;
      }
    }

    private IMessageFactory GetMessageFactory(string transName)
    {
      switch (transName)
      {
        case "Ping":
        case "ServiceEngineCommand":
        case "WsCommand":
        case "GetRunningTasksReport":
          return new Org.WSO.MessageFactory();

        case "ExcelExtract":
        case "PdfExtract":
          return new Org.Dx.Business.MessageFactory();
      }

      return null;
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

      //  downloadSoftwareRequest = f.Deserialize(requestMessage.TransactionBody, true) as DownloadSoftwareRequest;
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
      //  ErrorResponse errorResponse = f.Deserialize(responseMessage.TransactionBody, true) as ErrorResponse;
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
      //      f.Deserialize(responseMessage.TransactionBody, true) as DownloadSoftwareResponse;


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

      //  downloadSoftwareResponse = f.Deserialize(responseMessage.TransactionBody, true) as DownloadSoftwareResponse;

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

      if ((float)frequency < 0.000F || (float)frequency > 30.000)
      {
        txtOut.Text = "Frequency must be greather than 0 milliseconds and less than 30 seconds.";
        return false;
      }

      if ((float)duration < 5.00F || (float)duration > 21600.00)
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
        File.WriteAllText(sendFilesFolder + @"\" + fileName, fileData);
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

    //private void TestDxWorkbook()
    //{
    //  string excelPath = g.CI("FullPath");

    //  DxWorkbook wb = Utility.GetWorkbook(excelPath);

    //  var f = new ObjectFactory2();

    //  XElement wbElement = f.Serialize(wb);

    //  var wb2 = f.Deserialize(wbElement, true) as DxWorkbook;


    //}


    private void cboWebServiceHost_SelectedIndexChanged(object sender, EventArgs e)
    {
      cboWebService.Items.Clear();

      if (cboWebServiceHost.Text.IsBlank())
        return;

      string host = cboWebServiceHost.Text;
      _env = _hostsAndEnvironments[host];

      cboWebService.Items.Add(String.Empty);
      foreach (var kvpSvcEnv in _servicesAndEnvironments)
      {
        if (kvpSvcEnv.Value == _env)
          cboWebService.Items.Add(kvpSvcEnv.Key);
      }
    }

    private void cboWebService_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cboTransaction.Items.Count > 0)
        cboTransaction.SelectedIndex = 0;
    }

    private void cboTransaction_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cboTransaction.Text.IsBlank())
        return;

      SetConfigWsSpec();
    }

    private void cboCommand_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void cboCommandName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
  }
}