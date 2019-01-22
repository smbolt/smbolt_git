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
    private bool _firstShowing = true;
    private bool _uiIsCondensed = false;
    private SoftwareUpdateSource _softwareUpdateSource;
    private string _softwareUpdateRoot;
    private string _softwareModuleName;
    private bool _applyAnyUpdate;
    private Logger _logger = new Logger();

    public bool UpdateExists {
      get;
      private set;
    }
    public bool LaunchModule {
      get;
      private set;
    }

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

      _logger.Log("The software updates module is starting.");

      CheckForUpdates(false);

      if (_softwareUpdateSource == SoftwareUpdateSource.FileSystem)
      {
        FsCheckForUpdates();
      }
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
          TestFileSystemAccess(true);
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

        case "FsSkipUpdates":
          this.Close();
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

    private void CheckForUpdates(bool showSuccessMessage = false)
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
          var fileAccessTestResult = TestFileSystemAccess(showSuccessMessage);
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

    private TaskResult TestFileSystemAccess(bool showSuccessMessage = false)
    {
      var taskResult = new TaskResult("TestFileSystemAccess");

      try
      {
        lblStatus.Text = "TestFileSystemAccess";

        if (_softwareUpdateRoot.IsBlank())
          return taskResult.Failed("The file system path for the software update root is blank or null.");

        var fsu = new FileSystemUtility();
        if (!fsu.CanAccessFileSystem(_softwareUpdateRoot, FileSystemAccessType.ListDirectory))
        {
          MessageBox.Show("File system access to the software updates directory failed.",
                          "Software Updates - File System Access Check Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
          string errorMessage = "File System Access Checked Failed - cannot access path '" + _softwareUpdateRoot + "'.";
          _logger.Log(LogSeverity.MAJR, errorMessage, 0);
          txtLog.AppendAndScroll(errorMessage, g.crlf2);
        }
        else
        {
          string successMessage = "File System Access Checked Succeeded - can access path '" + _softwareUpdateRoot + "'.";
          _logger.Log(LogSeverity.INFO, successMessage, 0);

          if (!showSuccessMessage)
            return taskResult.Success();

          //MessageBox.Show("File system access to the software updates directory is successful.",
          //                "Software Updates - File System Access Check Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

          txtLog.AppendAndScroll(successMessage, g.crlf2);
        }

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
      _logger.Log(g.SystemInfo.SystemInfoString);
      txtLog.AppendAndScroll(g.SystemInfo.SystemInfoString, g.crlf2);
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
      try
      {
        string updatePath = g.CI("SoftwareUpdateRoot");

        if (!Directory.Exists(updatePath))
        {
          _logger.Log("The software updates directory cannot be found - path is '" + updatePath + "'.");
          txtLog.AppendAndScroll("The software updates directory cannot be found - path is '" + updatePath + "'.");
          return;
        }

        string[] updateFiles = Directory.GetFiles(updatePath);

        string mainFile = String.Empty;
        foreach (string fileName in updateFiles)
        {
          if (fileName.Contains("Gulfport.LoeForecaster.dll"))
          {
            mainFile = fileName;
          }
        }

        if (mainFile.IsBlank())
        {
          _logger.Log("There is no software update available for this program.");
          txtLog.AppendAndScroll("There is no software update available for this program.");
          return;
        }

        DateTime mainFileTimeStamp = DateTime.MinValue;
        DateTime existingModuleTimeStamp = DateTime.MinValue;

        FileInfo mainFileFileInfo = new FileInfo(mainFile);
        mainFileTimeStamp = mainFileFileInfo.LastWriteTimeUtc;

        string appFolder = Path.GetFileName(Directory.GetDirectories(g.MEFCatalog, "LOE*").FirstOrDefault().DbToString());
        string versionFolder = Path.GetFileName(Directory.GetDirectories(g.MEFCatalog + @"\" + appFolder).FirstOrDefault().DbToString());
        string mefTargetFolder = g.MEFCatalog + @"\" + appFolder + @"\" + versionFolder;

        string currentModuleFile = mefTargetFolder + @"\Gulfport.LoeForecaster.dll";

        if (!File.Exists(currentModuleFile))
        {
          _logger.Log("The software update is not properly configured - the main module file is missing.");
          txtLog.AppendAndScroll("The software update is not properly configured - the main module file is missing.");
          return;
        }

        FileInfo mainFileInstalledFileInfo = new FileInfo(currentModuleFile);
        existingModuleTimeStamp = mainFileInstalledFileInfo.LastWriteTimeUtc;

        if (_applyAnyUpdate)
        {
          _logger.Log("The program is configured to re-apply updates that have already been applied.");
          txtLog.AppendAndScroll("The program is configured to re-apply updates that have already been applied.");
        }
        else
        {
          if (mainFileTimeStamp <= existingModuleTimeStamp)
          {
            _logger.Log("The software update for this program has already been applied.");
            txtLog.AppendAndScroll("The software update for this program has already been applied.");
            return;
          }
        }

        _logger.Log("There is an update available for this program.");
        txtLog.AppendAndScroll("There is an update available for this program.");

        this.UpdateExists = true;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception has occurred during the check for software updates." + g.crlf2 + ex.ToReport(),
                        "Software Updates - Error Checking for Available Updates", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
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

          var archiver = new Org.GS.Archiver();

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

        var extractor = new Org.GS.Archiver();
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
      try
      {
        this.Cursor = Cursors.WaitCursor;

        string mefCatalogPath = g.MEFCatalog;
        string archiveFolder = Path.GetDirectoryName(mefCatalogPath) + @"\MEFArchive";

        if (Directory.Exists(archiveFolder))
        {
          _logger.Log("Module archive folder '" + archiveFolder + "' already exists.");
          txtLog.AppendAndScroll("Module archive folder '" + archiveFolder + "' already exists.", g.crlf2);
        }
        else
        {
          Directory.CreateDirectory(archiveFolder);
          _logger.Log("Module archive folder '" + archiveFolder + "' has been created.");
          txtLog.AppendAndScroll("Module archive folder '" + archiveFolder + "' has been created.", g.crlf2);
        }

        string archiveDateTime = DateTime.Now.ToString("yyyyMMdd-HHmmss");
        string fullArchivePath = archiveFolder + @"\" + archiveDateTime;

        if (Directory.Exists(fullArchivePath))
        {
          _logger.Log("Module archive folder '" + fullArchivePath + "' already exists.");
          txtLog.AppendAndScroll("Module archive folder '" + fullArchivePath + "' already exists.", g.crlf2);
        }
        else
        {
          Directory.CreateDirectory(fullArchivePath);
          _logger.Log("Module archive folder '" + fullArchivePath + "' has been created.");
          txtLog.AppendAndScroll("Module archive folder '" + fullArchivePath + "' has been created.", g.crlf2);
        }

        string appFolder = Path.GetFileName(Directory.GetDirectories(g.MEFCatalog, "LOE*").FirstOrDefault().DbToString());
        string versionFolder = Path.GetFileName(Directory.GetDirectories(g.MEFCatalog + @"\" + appFolder).FirstOrDefault().DbToString());
        string mefTargetFolder = g.MEFCatalog + @"\" + appFolder + @"\" + versionFolder;

        _logger.Log("Module target folder is '" + mefTargetFolder + "'.");
        txtLog.AppendAndScroll("Module target folder is '" + mefTargetFolder + "'.", g.crlf2);

        var existingFiles = Directory.GetFiles(mefTargetFolder).ToList();

        foreach (var existingFile in existingFiles)
        {
          string existingFileName = Path.GetFileName(existingFile);
          if (existingFileName.ToLower().StartsWith("gulfport.") || existingFileName.ToLower().StartsWith("org."))
          {
            File.Move(existingFile, fullArchivePath + @"\" + existingFileName);
            _logger.Log("Module file has been archived: " + existingFileName);
            txtLog.AppendAndScroll("Module file has been archived: " + existingFileName);
          }
        }

        var fileUpdates = Directory.GetFiles(_softwareUpdateRoot).ToList();
        foreach (var fileUpdate in fileUpdates)
        {
          File.Copy(fileUpdate, mefTargetFolder + @"\" + Path.GetFileName(fileUpdate), true);
          _logger.Log("Updated module file has been deployed: " + Path.GetFileName(fileUpdate));
          txtLog.AppendAndScroll("Updated module file has been deployed: " + Path.GetFileName(fileUpdate));
        }

        _logger.Log("Software update process has completed successfully.");

        txtLog.AppendAndScroll(" ", g.crlf3);
        txtLog.AppendAndScroll("Software update process has completed successfully.");

        this.Cursor = Cursors.Default;

        if (_uiIsCondensed)
        {
          MessageBox.Show("Software updates have been successfully applied." + g.crlf2 + "Click 'OK' to launch program.",
                          "Software Updates Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
          this.LaunchModule = true;
          this.Close();
        }
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred attempting to check for software updates." + g.crlf2 + ex.ToReport(),
                        "Software Updates Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void WriteToDisplay(string message)
    {
      _logger.Log(message);
      txtLog.AppendAndScroll(message);
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
      pnlBottom.Visible = false;
      this.LaunchModule = true;
      this.UpdateExists = false;

      tabMain.TabPages.Remove(tabPageWebServiceUpdates);

      _softwareUpdateSource = g.ToEnum<SoftwareUpdateSource>(g.CI("SoftwareUpdateSource"), SoftwareUpdateSource.NotSet);
      _applyAnyUpdate = g.CI("ApplyAnyUpdate").ToBoolean();

      if (_softwareUpdateSource == SoftwareUpdateSource.FileSystem)
        _softwareUpdateRoot = g.CI("SoftwareUpdateRoot");

      cboEnvironment.LoadItems(g.GetDictionary("Environments").Values.ToList());
      cboEnvironment.SelectItem(g.CI("DefaultEnvironment"));

      XmlMapper.AddAssembly(Assembly.GetAssembly(typeof(Org.WSO.Transactions.TransactionBase)));
      _wsHost = MessageFactoryBase.GetWebServiceHost();

      ToggleUI();
    }

    private void frmUpdateManager_Shown(object sender, EventArgs e)
    {
      if (!_firstShowing)
        return;

      _firstShowing = false;

      if (this.UpdateExists)
      {
        lblStatus.Text = "A software update exists.";

        MessageBox.Show("A software update exists for this program." + g.crlf2 +
                        "Click the 'Download Software Updates' button to apply the update.",
                        "Software Update Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);


      }
    }

    private void frmUpdateManager_DoubleClick(object sender, EventArgs e)
    {
      ToggleUI();
    }

    private void ToggleUI()
    {
      if (_uiIsCondensed)
      {
        pnlLog.Visible = true;
        lblStatus.Visible = false;
        this.Size = new Size(1000, 600);
        pnlTopControl.Height = 145;
        btnFsDownloadUpdates.Location = new Point(234, 40);
        btnFsSkipSoftwareUpdate.Location = new Point(453, 40);
        btnFsTestFileSystemAccess.Visible = true;
        btnFsGetFrameworkVersions.Visible = true;
        btnFsCheckForUpdates.Visible = true;
        this.CenterFormOnScreen();
        pnlToolbar.Visible = true;
        _uiIsCondensed = false;
      }
      else
      {
        pnlLog.Visible = false;
        this.Size = new Size(267, 140);
        lblStatus.Visible = false;
        pnlTopControl.Height = 110;
        btnFsDownloadUpdates.Location = new Point(14, 15);
        btnFsSkipSoftwareUpdate.Location = new Point(14, 40);
        btnFsTestFileSystemAccess.Visible = false;
        btnFsGetFrameworkVersions.Visible = false;
        btnFsCheckForUpdates.Visible = false;
        this.CenterFormOnScreen();
        pnlToolbar.Visible = false;
        _uiIsCondensed = true;
      }
    }

    private void frmUpdateManager_KeyUp(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F2:
          ToggleUI();
          break;
      }
    }
  }
}
