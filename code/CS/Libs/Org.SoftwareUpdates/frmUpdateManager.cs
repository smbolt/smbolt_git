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
using System.Reflection;
using System.Windows.Forms;
using Org.GS;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.Software.Tasks;
using Org.Software.Tasks.Transactions;
using Org.WSO;
using Org.WSO.Transactions;
using Org.ZIP;

namespace Org.SoftwareUpdates
{
  public partial class frmUpdateManager : Form
  {
    private SoftwareUpdateSource _softwareUpdateSource;
    private string _softwareUpdateRoot;
    private string _softwareModuleName;
    private Logger _logger = new Logger();

    public bool LaunchModule { get; private set; }
    
    private Dictionary<string, string> _env; 
    private ConfigWsSpec _configWsSpec;
    private WsHost _wsHost; 
    //private WCFTransMap _wcfTransMap;
    private string _softwareUpdateVersion = String.Empty;
    private string _softwareUpdatePlatformString = String.Empty;

    public frmUpdateManager()
    {
      InitializeComponent();
      InitializeForm();

      _logger.Log("AppLauncher starting up."); 

      CheckForUpdates();
    }
    
    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      TaskResult taskResult = null;

      switch (action)
      {
        case "WsTestConnection":
          TestWebServiceConnection();
          break;

        case "WsGetFrameworkVersions":
          WsGetFrameworkVersions();
          break;

        case "FsTestFileSystemAccess":
          TestFileSystemAccess();
          break;

        case "FsGetFrameworkVersions":
          FsGetFrameworkVersions();
          break;

        case "WsCheckForUpdates":
          WsCheckForUpdates();
          break;

        case "FsCheckForUpdates":
          FsCheckForUpdates();
          break;

        case "WsDownloadSoftware":
          WsDownloadSoftware();
          break;

        case "FsDownloadSoftware":
          FsDownloadSoftware();
          break;

        case "ClearDisplay":
          txtLog.Clear();
          Application.DoEvents();
          break;

        case "Terminate":
          this.LaunchModule = false;
          this.Close();
          break;

        case "Continue":
          this.Close();
          break;
      }

      ProcessTaskResult(taskResult);
    }

    private void ProcessTaskResult(TaskResult taskResult)
    {
      if (taskResult == null)
        return;


    }

    private void CheckForUpdates()
    {      
      _logger.Log("Checking for software updates."); 
      _logger.Log("Testing connection to software updates service."); 

      if (g.AppConfig.ContainsKey("CheckForUpdatesDisabledUntil"))
      {
        DateTime updatesDisabledUntil = g.CI("CheckForUpdatesDisabledUntil").CCYYMMDDHHMMSSToDateTime(); 
        if (updatesDisabledUntil > DateTime.Now)
        {
          _logger.Log("Checking for software updates is disabled until " + updatesDisabledUntil.ToString("yyyy/MM/dd HH:mm:ss") + "."); 
          this.Close();
          return;
        }
      }

      SoftwareUpdateError updateError;
      Color errorBgColor = Color.Black; 
      if (g.AppConfig.ContainsKey("ErrorFormBackgroundColor"))
      {
        int[] color = g.CI("ErrorFormBackgroundColor").ToTokenArrayInt32(Constants.PeriodDelimiter); 
        errorBgColor = Color.FromArgb(color[0], color[1], color[2]); 
      }

      string programName = "Software Updates";
      if (g.AppConfig.ContainsKey("ErrorFormProgramName"))
        programName = g.CI("ErrorFormProgramName");


      switch (_softwareUpdateSource)
      {
        case SoftwareUpdateSource.FileSystem:
          var fileAccessTestResult = TestFileSystemAccess();
          switch (fileAccessTestResult.TaskResultStatus)
          {
            case TaskResultStatus.Success:
              _logger.Log("Successful access of  software update file system root."); 
              break;

            default:
              updateError = new SoftwareUpdateError();
              updateError.BackgroundColor = errorBgColor;
              updateError.Title = programName + " - Software Update Error";
              updateError.ErrorDetail = "An error occurred while attempting to verify access to the software update file system root.  The file system access attempt failed." + g.crlf2 +
                                        "Error Detail:" + g.crlf + fileAccessTestResult.Message + g.crlf2 + fileAccessTestResult.FullErrorDetail;
              updateError.ErrorActions = new List<string>();
              updateError.ErrorActions.Add(String.Empty); 
              updateError.ErrorActions.Add("Ignore"); 
              updateError.ErrorActions.Add("Disable software update checks for today"); 
              updateError.ErrorActions.Add("Disable software update checks for 2 days"); 
              updateError.ErrorActions.Add("Disable software update checks for 1 week"); 
              updateError.ErrorActions.Add("Disable software update checks permanently"); 
              UserResponseToError(updateError); 
              this.Close();
              return;
          }
          break;

        case SoftwareUpdateSource.WebService:
          var connTestResult = TestWebServiceConnection();

          switch(connTestResult.TaskResultStatus)
          {
            case TaskResultStatus.Success:
              _logger.Log("Successful connection to software updates service - duration: "  + connTestResult.DurationString + "."); 
              break;

            default:
              updateError = new SoftwareUpdateError();
              updateError.BackgroundColor = errorBgColor;
              updateError.Title = programName + " - Software Update Error";
              updateError.ErrorDetail = "An error occurred while attempting to verify the connection to the software updates web service.  The connection attempt failed." + g.crlf2 + 
                                        "Error Detail:" + g.crlf + connTestResult.Message + g.crlf2 + connTestResult.FullErrorDetail;
              updateError.ErrorActions = new List<string>();
              updateError.ErrorActions.Add(String.Empty); 
              updateError.ErrorActions.Add("Ignore"); 
              updateError.ErrorActions.Add("Disable software update checks for today"); 
              updateError.ErrorActions.Add("Disable software update checks for 2 days"); 
              updateError.ErrorActions.Add("Disable software update checks for 1 week"); 
              updateError.ErrorActions.Add("Disable software update checks permanently"); 
              UserResponseToError(updateError); 
              this.Close();
              return;
          }
          break;
      }
    }

    private void UserResponseToError(SoftwareUpdateError updateError)
    {
      frmUpdateError fUpdateError = new frmUpdateError(updateError);
      fUpdateError.ShowDialog();
      DateTime disableUntil = DateTime.Now;
      string programConfigName = g.AppConfig.GetProgramForGroupFullSearch("Modularity"); 
          
      switch(fUpdateError.ErrorAction)
      {
        case SoftwareUpdateErrorAction.DisableCheckingToday:
          disableUntil = disableUntil.EndOfDay(); 
          _logger.Log("Checking for software updates will be disabled until " +  disableUntil.ToString("yyyyMMddHHmmss") + "."); 
          g.AppConfig.SetCI(programConfigName, "Modularity", "CheckForUpdatesDisabledUntil", disableUntil.ToString("yyyyMMddHHmmss")); 
          g.AppConfig.Save(); 

          break;

        case SoftwareUpdateErrorAction.DisableChecking2Days:
          
          break;

        case SoftwareUpdateErrorAction.DisableChecking1Week:

          break;

        case SoftwareUpdateErrorAction.DisableCheckingPermanently:

          break; 
      }
    }

    private TaskResult TestWebServiceConnection()
    {
      var taskResult = new TaskResult("TestWebServiceConnection"); 

      try
      {
        txtLog.Text = String.Empty;
        Application.DoEvents();
        System.Threading.Thread.Sleep(50); 

        lblStatus.Text = "TestWebServiceConnection";

        var f = new ObjectFactory2();
        WsMessage responseMessage = null; 

        SetConfigWsSpec();

        if (!_configWsSpec.IsReadyToConnect())
          return taskResult.Failed("Web service configuration is not ready to connect."); 

        this.Cursor = Cursors.WaitCursor;
        DateTime dtBegin = DateTime.Now;

        WsParms wsParms = new WsParms();      
        wsParms.MessagingParticipant = MessagingParticipant.Sender; 
        wsParms.UserName = "UserName";
        wsParms.Password = "Password";
        wsParms.AppName = g.AppInfo.ModuleName;
        wsParms.AppVersion = g.AppInfo.OrgVersion;
        wsParms.ConfigWsSpec = _configWsSpec;
        wsParms.TrackPerformance = ckTrackPerformance.Checked;
        wsParms.WsHost = _wsHost;      
        wsParms.TransactionName = "Ping";
        wsParms.TransactionVersion = "1.0.0.0";

        var messageFactory = new Org.WSO.MessageFactory();
        var requestMessage = messageFactory.CreateRequestMessage(wsParms); 
        responseMessage = WsClient.InvokeServiceCall(wsParms, requestMessage); 

        if (responseMessage != null)
        {
          switch(responseMessage.TransactionStatus)
          {
            case TransactionStatus.Success:
              txtLog.Text = responseMessage.TransactionName + " Status=" + responseMessage.TransactionStatus.ToString();
              break;

            default:
              switch (responseMessage.TransactionName)
              {
                case "ErrorResponse":
                  ErrorResponse errorResponse = f.Deserialize(responseMessage.TransactionBody) as ErrorResponse;
                  string errorResponseMessage = errorResponse.Message; 
                  errorResponseMessage += errorResponse.HasException ? (g.crlf + errorResponse.WsException.ToReport()) : (g.crlf + "No exception" + g.crlf);
                  txtLog.Text = errorResponseMessage;
                  this.Cursor = Cursors.Default;

                  taskResult.TaskResultStatus = TaskResultStatus.Failed;
                  taskResult.Message = "Connection test to endpoint '" + wsParms.ConfigWsSpec.WebServiceEndpoint + "' failed."; 
                  taskResult.FullErrorDetail = errorResponseMessage; 
                  return taskResult;

                default:
                  taskResult.TaskResultStatus = TaskResultStatus.Failed;
                  taskResult.Message = "WCF transaction error - status is '" + responseMessage.TransactionStatus.ToString() +
                                       "', transaction name is '" + responseMessage.TransactionName + "'."; 

                  txtLog.Text = "WCF Transaction Error" + g.crlf + 
                                "WCF Status: " + responseMessage.TransactionStatus.ToString() + g.crlf + 
                                "Response Transaction Name: " + responseMessage.TransactionName;
                  return taskResult; 
              }
          }

        }
        else
        {
          taskResult.TaskResultStatus = TaskResultStatus.Failed;
          taskResult.Message = "WCF service response message is null."; 

          txtLog.Text = "WCF service response message is null."; 
          taskResult.EndDateTime = DateTime.Now;
          return taskResult; 
        }

        taskResult.TaskResultStatus = TaskResultStatus.Success;
        taskResult.Message = "Connection test was successful to endpoint '" + wsParms.ConfigWsSpec.WebServiceEndpoint + "'.";
        txtLog.Text = "Connection test was successful to endpoint '" + wsParms.ConfigWsSpec.WebServiceEndpoint + "'.";
        taskResult.EndDateTime = DateTime.Now;
        return taskResult; 
      }
      catch(Exception ex)
      {
        taskResult.TaskResultStatus = TaskResultStatus.Failed;
        taskResult.Message = "An exception occurred attempting to test the connection to the software update service." + g.crlf2 + 
                        "Web Service Endpoint is '" + _configWsSpec.WebServiceEndpoint + "'.";
        taskResult.FullErrorDetail = ex.ToReport();
        taskResult.Exception = ex;
        return taskResult; 
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private TaskResult TestFileSystemAccess()
    {
      var taskResult = new TaskResult("TestFileSystemAccess"); 

      try
      {
        txtLog.Text = String.Empty;
        Application.DoEvents();
        System.Threading.Thread.Sleep(50); 

        lblStatus.Text = "TestFileSystemAccess";

        if (_softwareUpdateRoot.IsBlank())
          return taskResult.Failed("The file system path for the software update root is blank or null.");

        var fsu = new FileSystemUtility();
        if (!fsu.CanAccessFileSystem(_softwareUpdateRoot, FileSystemAccessType.ListDirectory))
          return taskResult.Failed("Cannot access the software update root folder '" + _softwareUpdateRoot + "'.");

        return taskResult.Success();
      }
      catch (Exception ex)
      {
        taskResult.TaskResultStatus = TaskResultStatus.Failed;
        taskResult.Message = "An exception occurred attempting to test the connection to the software update service." + g.crlf2 +
                        "Web Service Endpoint is '" + _configWsSpec.WebServiceEndpoint + "'.";
        taskResult.FullErrorDetail = ex.ToReport();
        taskResult.Exception = ex;
        return taskResult;

      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }


    private void WsGetFrameworkVersions()
    {
      this.Cursor = Cursors.WaitCursor;
      var taskResult = new TaskResult("WsGetFrameworkVersions"); 

      try
      {
        txtLog.Text = String.Empty;
        Application.DoEvents();
        System.Threading.Thread.Sleep(50); 

        lblStatus.Text = "WsGetFrameworkVersions";
        StringBuilder sb = new StringBuilder();
        SetConfigWsSpec();

        var f = new ObjectFactory2();
        DateTime dtBegin = DateTime.Now;
        WsParms wsParms = InitializeWsParms("GetFrameworkVersions", "1.0.0.0");
        WsMessage responseMessage = null;
        var messageFactory = new Org.SoftwareTasks.MessageFactory();
        WsMessage requestMessage = messageFactory.CreateRequestMessage(wsParms);
        responseMessage = WsClient.InvokeServiceCall(wsParms, requestMessage);
      
        if (responseMessage != null)
        {
          switch(responseMessage.TransactionStatus)
          {
            case TransactionStatus.Success:
              GetFrameworkVersionsResponse getFrameworkVersionsResponse = f.Deserialize(responseMessage.TransactionBody) as GetFrameworkVersionsResponse;
              g.FxVersionSet = getFrameworkVersionsResponse.FxVersionSet; 
              sb.Append(g.crlf + "FrameworkVersionString    Version        VersionNum     SP" + g.crlf); 
              foreach (var fxVersion in getFrameworkVersionsResponse.FxVersionSet.Values)
              {
                sb.Append(fxVersion.FrameworkVersionString.PadTo(26) +
                          fxVersion.Version.PadTo(15) +
                          fxVersion.VersionNum.PadTo(15) +
                          fxVersion.ServicePackString + g.crlf); 
              }

              txtLog.Text = sb.ToString();

              break;

            default:
              switch (responseMessage.TransactionName)
              {
                case "ErrorResponse":
                  ErrorResponse errorResponse = f.Deserialize(responseMessage.TransactionBody) as ErrorResponse;
                  string errorResponseMessage = f.Serialize(errorResponse).ToString();
                  txtLog.Text = errorResponseMessage;
                  this.Cursor = Cursors.Default;
                  return;

                default:
                  txtLog.Text = "WCF Transaction Error" + g.crlf + 
                                "WCF Status: " + responseMessage.TransactionStatus.ToString() + g.crlf + 
                                "Response Transaction Name: " + responseMessage.TransactionName;
                  break;
              }
              break;
          }
        }
        else
        {
          txtLog.Text = "WCF service response message is null.";
        }
      }
      catch(Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to retrieve list of Framework Versions from the server." + g.crlf2 + ex.ToReport(), 
                        "RPDM - Software Updates Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
      }

      this.Cursor = Cursors.Default;
    }

    private void FsGetFrameworkVersions()
    {
      txtLog.Text = String.Empty;
      Application.DoEvents();
      System.Threading.Thread.Sleep(50); 

      lblStatus.Text = "FsGetFrameworkVersions";
      var taskResult = new TaskResult("FsGetFrameworkVersions");

    }

    private void WsCheckForUpdates()
    {
      this.Cursor = Cursors.WaitCursor;

      lblStatus.Text = "WsCheckForUpdates";
      var taskResult = new TaskResult("WsCheckForUpdates");

      try
      {
        txtLog.Text = String.Empty;
        Application.DoEvents();
        System.Threading.Thread.Sleep(50); 

        lblStatus.Text = "WsCheckForUpdates";
        StringBuilder sb = new StringBuilder();
        SetConfigWsSpec();

        var f = new ObjectFactory2();
        DateTime dtBegin = DateTime.Now;
        WsParms wsParms = InitializeWsParms("CheckForUpdates", "1.0.0.0");
        WsMessage responseMessage = null;
        var messageFactory = new Org.SoftwareTasks.MessageFactory();
        WsMessage requestMessage = messageFactory.CreateRequestMessage(wsParms); 
        responseMessage = WsClient.InvokeServiceCall(wsParms, requestMessage);
      
        if (responseMessage != null)
        {
          switch(responseMessage.TransactionStatus)
          {
            case TransactionStatus.Success:
              CheckForUpdatesResponse checkForUpdatesResponse =
                    f.Deserialize(responseMessage.TransactionBody) as CheckForUpdatesResponse;

              if (!checkForUpdatesResponse.UpgradeAvailable)
              {
                _softwareUpdateVersion = String.Empty;
                _softwareUpdatePlatformString = String.Empty;
                sb.Append("No update is available for ");
              }
              else
              {
                _softwareUpdateVersion = checkForUpdatesResponse.UpgradeVersion;
                _softwareUpdatePlatformString = checkForUpdatesResponse.PlatformString;

                sb.Append("Upgrade is available from version '" + checkForUpdatesResponse.CurrentVersion + ":" + g.crlf +
                  "  Version   : " + checkForUpdatesResponse.UpgradeVersion + g.crlf +
                  "  Platform  : " + checkForUpdatesResponse.PlatformString + g.crlf);                 
              }
              
              txtLog.Text = sb.ToString();

              break;

            default:
              switch (responseMessage.TransactionName)
              {
                case "ErrorResponse":
                  ErrorResponse errorResponse = f.Deserialize(responseMessage.TransactionBody) as ErrorResponse;
                  string errorResponseMessage = f.Serialize(errorResponse).ToString();
                  txtLog.Text = errorResponseMessage;
                  this.Cursor = Cursors.Default;
                  return;

                default:
                  txtLog.Text = "WCF Transaction Error" + g.crlf + 
                                "WCF Status: " + responseMessage.TransactionStatus.ToString() + g.crlf + 
                                "Response Transaction Name: " + responseMessage.TransactionName;
                  break;
              }
              break;
          }
        }
        else
        {
          txtLog.Text = "WCF service response message is null.";
        }
      }
      catch(Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to check for software updates." + g.crlf2 + ex.ToReport(), 
                        "RPDM - Software Updates Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
      }

      this.Cursor = Cursors.Default;
    }

    private void FsCheckForUpdates()
    {
      this.Cursor = Cursors.WaitCursor;

      txtLog.Text = String.Empty;
      Application.DoEvents();
      System.Threading.Thread.Sleep(50); 

      var taskResult = new TaskResult("FsCheckForUpdates");


      lblStatus.Text = "FsCheckForUpdates";   




      this.Cursor = Cursors.Default;
    }

    private void WsDownloadSoftware()
    {
      this.Cursor = Cursors.WaitCursor;
      var taskResult = new TaskResult("WsDownloadSoftware");

      try
      {
        txtLog.Text = String.Empty;
        Application.DoEvents();
        System.Threading.Thread.Sleep(50); 

        lblStatus.Text = "WsDownloadSoftware";     
        var fsu = new FileSystemUtility();
        string modulePath = g.MEFCatalog;
        string moduleName = g.CI("ModuleToRun"); 
        int[] versionTokens = _softwareUpdateVersion.ToTokenArrayInt32(Constants.PeriodDelimiter);
        string versionFolder = versionTokens[0].ToString() + "." + versionTokens[1].ToString() + "." + 
                               versionTokens[2].ToString() + "." + versionTokens[3].ToString(); 
        string downloadPath = modulePath + @"\" + moduleName + @"\" + versionFolder; 
        if (!Directory.Exists(downloadPath))
          Directory.CreateDirectory(downloadPath); 

        string[] filesInDownloadPath = Directory.GetFiles(downloadPath); 
        string[] directoriesInDownloadPath = Directory.GetDirectories(downloadPath); 

        if (filesInDownloadPath.Length > 0 || directoriesInDownloadPath.Length > 0)
        {
          string archiveFolder = g.MEFCatalog + @"\Z_Archive\" + moduleName + @"\" + versionFolder; 
          if (!Directory.Exists(archiveFolder))
            Directory.CreateDirectory(archiveFolder); 

          var archiver = new Archiver();

          string archiveFileName = archiveFolder + @"\" + DateTime.Now.ToString("yyyyMMdd-HHmmssfff") + "_Archive.zip";
          while (File.Exists(archiveFileName))
          {
            System.Threading.Thread.Sleep(5); 
            archiveFileName = archiveFolder + @"\" + DateTime.Now.ToString("yyyyMMdd-HHmmssfff") + "_Archive.zip";
          }

          archiver.CreateArchive(downloadPath, archiveFolder + @"\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_Archive.zip"); 
          fsu.DeleteDirectoryContentsRecursive(downloadPath); 
        }

        StringBuilder sb = new StringBuilder();
        SetConfigWsSpec();

        var f = new ObjectFactory2();
        DateTime dtBegin = DateTime.Now;
        WsParms wsParms = InitializeWsParms("DownloadSoftware", "1.0.0.0");
        WsMessage responseMessage = null; 
        DownloadSoftwareRequest downloadSoftwareRequest = null;
        DownloadSoftwareResponse downloadSoftwareResponse = null;

        var messageFactory = new Org.SoftwareTasks.MessageFactory();
        WsMessage requestMessage = messageFactory.CreateRequestMessage(wsParms);
        
        int segmentNumber = 0;  
        int totalSegmentsToDownload = 999; 
        bool continueDownload = true; 

        while(continueDownload)
        {
          downloadSoftwareRequest = f.Deserialize(requestMessage.TransactionBody) as DownloadSoftwareRequest;
          downloadSoftwareRequest.UpgradeVersion = _softwareUpdateVersion;
          downloadSoftwareRequest.UpgradePlatformString = _softwareUpdatePlatformString;

          if (segmentNumber == 0)
          {
            downloadSoftwareRequest.RequestType = RequestType.InitialRequest;
          }
          else
          {
            downloadSoftwareRequest.RequestType = RequestType.GetNextSegment;
            downloadSoftwareRequest.SegmentNumber = segmentNumber;
          }

          requestMessage.TransactionBody = f.Serialize(downloadSoftwareRequest); 
          responseMessage = WsClient.InvokeServiceCall(wsParms, requestMessage);
      
          if (responseMessage != null)
          {
            switch(responseMessage.TransactionStatus)
            {
              case TransactionStatus.Success:
                
                downloadSoftwareResponse = f.Deserialize(responseMessage.TransactionBody) as DownloadSoftwareResponse;

                sb.Append(g.crlf + "Software download in progress, segment " +
                      downloadSoftwareResponse.SegmentNumber.ToString("##,##0") + " of " +
                      downloadSoftwareResponse.TotalSegments.ToString("##,##0") + " received " +
                      "segment size is " + downloadSoftwareResponse.SegmentSize.ToString("##,###,##0") +
                      " total file size is " + downloadSoftwareResponse.TotalFileSize.ToString("#,###,###,##0") + 
                      g.crlf + downloadSoftwareResponse.SegmentData.PadTo(200) + g.crlf);

                if (downloadSoftwareResponse.SegmentNumber > 0)
                {
                  string fileName = "module(seg-" + downloadSoftwareResponse.SegmentNumber.ToString("000") + 
                                             "-of-" + downloadSoftwareResponse.TotalSegments.ToString("000") +
                                             ")(ext-zip).seg"; 

                  File.WriteAllText(downloadPath + @"\" + fileName, downloadSoftwareResponse.SegmentData); 
                }

                segmentNumber = downloadSoftwareResponse.SegmentNumber + 1;
                totalSegmentsToDownload = downloadSoftwareResponse.TotalSegments; 

                if (segmentNumber > totalSegmentsToDownload)
                  continueDownload = false; 

                txtLog.Text = sb.ToString();
                txtLog.SelectionStart = txtLog.Text.Length - 1;
                txtLog.SelectionLength = 0;
                txtLog.ScrollToCaret();
                Application.DoEvents();
                break;

              default:
                switch (responseMessage.TransactionName)
                {
                  case "ErrorResponse":
                    ErrorResponse errorResponse = f.Deserialize(responseMessage.TransactionBody) as ErrorResponse;
                    string errorResponseMessage = f.Serialize(errorResponse).ToString();
                    txtLog.Text = errorResponseMessage;
                    this.Cursor = Cursors.Default;
                    continueDownload = false;
                    break;

                  default:
                    txtLog.Text = "WCF Transaction Error" + g.crlf + 
                                  "WCF Status: " + responseMessage.TransactionStatus.ToString() + g.crlf + 
                                  "Response Transaction Name: " + responseMessage.TransactionName;
                    continueDownload = false; 
                    break;
                }
                break;
            }
          }
          else
          {
            continueDownload = false; 
            txtLog.Text = "WCF service response message is null.";
          }
        }

        fsu.DesegmentizeFile(downloadPath + @"\module.zip"); 

        var extractor = new Archiver();
        extractor.ExtractArchive(downloadPath + @"\module(DOWNLOADED).zip", downloadPath); 
        File.Delete(downloadPath + @"\module(DOWNLOADED).zip");
      }
      catch(Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to check for software updates." + g.crlf2 + ex.ToReport(), 
                        "RPDM - Software Updates Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
      }

      this.Cursor = Cursors.Default;
    }

    private void FsDownloadSoftware()
    {
      txtLog.Text = String.Empty;
      Application.DoEvents();
      System.Threading.Thread.Sleep(50); 

      lblStatus.Text = "FsDownloadSoftware"; 
    }
    
    private void SetConfigWsSpec()
    {
      string env = cboEnvironment.Text;
      _configWsSpec = g.AppConfig.GetWsSpec(env); 
    }

    private WsParms InitializeWsParms(string transactionName, string transactionVersion)
    {      
      WsParms wsParms = new WsParms();  
      wsParms.TransactionName = transactionName;
      wsParms.TransactionVersion = transactionVersion;
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

      wsParms.ModuleCode = 301;
      wsParms.ModuleName = g.CI("ModuleToRun"); 
      wsParms.ModuleVersion = "1.0.0.0";  
      wsParms.OrgId = 3; 

      return wsParms;
    }

    private void InitializeForm()
    {
      this.LaunchModule = true;

      _softwareUpdateSource = g.ToEnum<SoftwareUpdateSource>(g.CI("SoftwareUpdateSource"), SoftwareUpdateSource.NotSet);

      if (_softwareUpdateSource == SoftwareUpdateSource.FileSystem)
        _softwareUpdateRoot = g.CI("SoftwareUpdateRoot");

      //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      //pnlTopControl.Visible = false;
      //pnlLog.Visible = false; 
      //this.Size = new Size(200, 100); 

      _env = g.AppConfig.GetDictionary("Environments"); 

      foreach(KeyValuePair<string, string> kvp in _env)
      {
        cboEnvironment.Items.Add(kvp.Key); 
      }

      string defaultEnvironment = g.GetCI("DefaultEnvironment"); 

      for(int i = 0; i < cboEnvironment.Items.Count; i++)
      {
        if (cboEnvironment.Items[i].ToString() == defaultEnvironment)
        {
          cboEnvironment.SelectedIndex = i;
          break;
        }
      }

      XmlMapper.AddAssembly(Assembly.GetAssembly(typeof(Org.WSO.Transactions.TransactionBase)));
      //_wcfTransMap = new WCFTransMap();  

      //#if(GULFPORT)
      //  GulfportExtension.Extend();
      //#else
      //  OrgExtention.Extend();
      //  OrgExtention.ExtendWCFTransMap(_wcfTransMap); 
      //#endif

      _wsHost = MessageFactoryBase.GetWebServiceHost();
    }

  }
}
