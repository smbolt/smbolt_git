using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Resources;
using System.Reflection;
using TPL = System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Org.TSK;
using Org.WinSvc;
using Org.Notify;
using Org.SF;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS;

namespace Org.WinServiceHost
{
  public partial class frmMain : Form
  {
    private IWinServiceEngine _svcEngine;
    private string _windowsServiceEngine;
    private Logger _logger = new Logger();
    private WinServiceParms _winServiceParms;
    private TaskEngineParms _taskEngineParms;
    protected Assembly Assembly { get; set; }
    protected ResourceManager ResourceManager { get; set; }
    private Image SplashImage;

    private string _appConfigPath = String.Empty;
    private string _appDataPath = String.Empty;
    private a a;

    private bool _closeUponStartup = false;
    private bool _isFirstShowing = true;


    public frmMain()
    {
      InitializeComponent();
      InitializeApplication();
    }

    private void InitializeApplication()
    {
      try
      {
        a = new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An error occurred attempting to initialize the program.  See message below." + g.crlf2 + ex.ToReport(),
                        "WinService Host - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        _closeUponStartup = true;
        return;
      }

      _windowsServiceEngine = g.CI("WindowsServiceEngine");
      if (_windowsServiceEngine.IsBlank())
      {
        throw new Exception("The configuration item (CI) named 'WindowsServiceEngine' must be included in the AppConfig.xml file " +
                            "and must specify a valid Windows Service Engine type."); 
      }

      this.Text += " - " + _windowsServiceEngine; 

      this.ResourceManager = new ResourceManager("Org.WinServiceHost.Properties.Resources", Assembly.GetExecutingAssembly());
      var splashImage = this.ResourceManager.GetObject("SplashImage");
      this.SplashImage = (Bitmap)this.ResourceManager.GetObject("SplashImage");

     _logger.Log(g.AppInfo.AppName + " Host is starting up.", 3046, 300);
      mnuToggleShowControls.Text = "Hide Controls";

      cboFunction.Items.Add(String.Empty);
      cboFunction.Items.Add("Display Tasks");

      frmSplash fSplash = new frmSplash(g.AppInfo.AppName, this.SplashImage, "1.0", "Copyright string");
      fSplash.VersionInfo = "Version " + g.AppInfo.AppVersion;
      fSplash.CopyrightInfo = "Copyright " + DateTime.Now.ToString("yyyy") + " Organization";
      fSplash.SetMessage(g.AppInfo.AppName + " Host is starting up...");

      fSplash.Show();
      Application.DoEvents();
      System.Threading.Thread.Sleep(1000);

      fSplash.SetMessage(g.AppInfo.AppName + " Host is starting up...");
      Application.DoEvents();
      System.Threading.Thread.Sleep(1000);

      bool splashEscaped = fSplash.Escaped;
      fSplash.Close();

      SetControls("INITIAL");

      _taskEngineParms = new TaskEngineParms();
      _taskEngineParms.TaskScheduleMode = g.ToEnum<TaskScheduleMode>(g.CI("TaskScheduleMode"), TaskScheduleMode.FromDatabase);
      _taskEngineParms.TaskLoadIntervalSeconds = g.CI("TaskLoadIntervalSeconds").ToInt32OrDefault(1200);
      _taskEngineParms.TasksDbSpecPrefix = g.CI("TasksDbSpecPrefix").OrDefault("Tasks");
      _taskEngineParms.TaskProfile = g.CI("TaskProfile").OrDefault("Normal");
      _taskEngineParms.WSHTaskProfile = g.CI("WSHTaskProfile").OrDefault("FastTest"); 
      _taskEngineParms.MEFModulesPath = g.CI("MEFModulesPath");
      _taskEngineParms.LimitMEFImports = g.CI("LimitMEFImports").ToBoolean();
      _taskEngineParms.MEFLimitListName = g.CI("MEFLimitListName");

      _winServiceParms = new WinServiceParms();
      _winServiceParms.WindowsServiceName = g.CI("WindowsServiceName");
      _winServiceParms.EntityId = g.CI("EntityId").ToInt32OrDefault(0);
      _winServiceParms.SleepInterval = g.CI("SleepInterval").ToInt32OrDefault(1000); 
      _winServiceParms.IsRunningAsWindowsService = false; // defaults to true in constructor, needs to be false from WinServiceHost
      _winServiceParms.ConfigLogSpecPrefix = g.CI("ConfigLogSpecPrefix").OrDefault("Default");
      _winServiceParms.ConfigNotifySpecPrefix = g.CI("ConfigNotifySpecPrefix").OrDefault("Default"); 
      _winServiceParms.MaxWaitIntervalMilliseconds = g.CI("MaxWaitIntervalMilliseconds").ToInt32OrDefault(1000);
      _winServiceParms.InDiagnosticsMode = g.CI("InDiagnosticsMode").ToBoolean();
      _winServiceParms.SuppressNonErrorOutput = g.CI("SuppressNonErrorOutput").ToBoolean();
      _winServiceParms.SuppressTaskReload = g.CI("SuppressTaskReload").ToBoolean();
      _winServiceParms.RunWebService = g.CI("RunWebService").ToBoolean();
      _winServiceParms.UseAlerter = g.CI("UseAlerter").ToBoolean();
      _winServiceParms.AlerterPath = g.CI("AlerterPath"); 

      IPDX.TestProperty = "WinService";

      //IPDX.FireIpdxEvent +=  new IpdxEventHandler(IPDX_FireIpdxEvent);
      _logger.Log(g.AppInfo.AppName + " Host start-up is complete.", 3046, 300);
    }

    private void IPDX_FireIpdxEvent(string testData)
    {
      if (testData == null)
        return;

      WriteToDisplay(testData);
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "StartService":
          StartService();
          break;
            
        case "PauseService":
          PauseService();
          break;
            
        case "ResumeService":
          ResumeService();
          break;
            
        case "StopService":
          StopService();
          break;
            
        case "DumpTrace":
          txtReport.Text += g.crlf + g.Trace + g.crlf;
          break;

        case "ShowServiceAlert":
          ShowServiceAlert();
          break;

        case "ClearReport":
            ClearReport();
            break;

        case "GetNextNow":
            GetNextNow();
            break;

        case "RefreshTaskList":
            RefreshTaskList();
            break;

        case "ToggleShowControls":
            ToggleShowControls();
            break;

        case "RunFunction":
            RunFunction();
            break;

        case "TestLogWrite":
            TestLogWrite();
            break;

        case "TestNotifications":
            TestNotifications();
            break;

        case "CloseForm":
            CloseForm();
            break;
      }
    }
        
    private void RunFunction()
    {
      if (cboFunction.SelectedIndex == -1)
        return;

      string function = cboFunction.SelectedItem.ToString();

      switch (function)
      {
        case "Display Tasks":
          DisplayTasks();
          break;

        default:
          txtReport.Text = "Run Function" + g.crlf + txtParms.Text;
          break;
      }
    }

    private void ClearReport()
    {
      txtReport.Clear();
    }

    private void ToggleShowControls()
    {
      if (this.splitContainer1.Panel1Collapsed)
      {
        this.splitContainer1.Panel1Collapsed = false;
        mnuToggleShowControls.Text = "Hide Controls";
      }
      else
      {
        this.splitContainer1.Panel1Collapsed = true;
        mnuToggleShowControls.Text = "Show Controls";
      }        
    }

    private void DisplayTasks()
    {
      string taskProfile = g.AppConfig.GetCI("WSHTaskProfile");

      TaskConfigurations tc = g.AppConfig.GetTaskConfigurations();
      if (tc == null)
      {
        WriteToDisplay("No TaskConfigurations found.");
        return;
      }

      if (!tc.ContainsKey(taskProfile))
      {
        WriteToDisplay("TaskProfile '" + taskProfile + "' not found in TaskConfigurations for " + g.AppInfo.ConfigName + ".");
        return;
      }

      TaskConfigSet tcs = tc[taskProfile];
      ObjectFactory2 f = new ObjectFactory2();
      string tcsString = f.Serialize(tcs).ToString() + g.crlf;

      WriteToDisplay(tcsString);
    }
    
    private void StartService()
    {
      var configDbSpec = g.GetDbSpec("Tasks");
      if (configDbSpec.DbServer == "OKC1EDW0001")
      {
        if (MessageBox.Show("WinServiceHost is configured to run in the production environment." + g.crlf2 +
                        "If this is not your intention, please click 'Cancel' and update the application configurations appropriately.",
                        "WinServiceHost - Confirm Run in the Production Enviornment", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
          return; 

      }

      try
      {
        this.Cursor = Cursors.WaitCursor;

        if (_svcEngine != null)
          if (_svcEngine.WinServiceState == WinServiceState.Running)
              return;

        _svcEngine = GetServiceEngine();
        _svcEngine.NotifyHost += taskEngine_NotifyHost;

        if (!_svcEngine.Start())
        {
          this.Cursor = Cursors.Default;
          return;
        }

        //List<TaskConfig> tasks = taskEngine.GetAllTasks();

        SetControls("START");
        string message = "***********************************************************************************************" + g.crlf +
                          "** WinService Host has been Started at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm.ss.fff") + g.crlf +
                          "***********************************************************************************************" + g.crlf;
        this.WriteToDisplay(message, false);

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An error occurred starting the WinService Host program - see message below." + g.crlf2 + ex.ToReport(),
            "WinService Host - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        _svcEngine = null;
      }
    }

    private IWinServiceEngine GetServiceEngine()
    {
      switch (_windowsServiceEngine)
      {
        case "Not Used - Leave in place":
          return null;

        #if TaskEngine
        case "TaskEngine":
          return new TaskEngine(_winServiceParms, _taskEngineParms);
        #endif

        #if PerfEngine
        case "PerfEngine":
          return new PerfEngine(_winServiceParms);
        #endif
      }

      throw new Exception("The configuration value for CI 'WindowsServiceEngine' is not valid: '" + _windowsServiceEngine + "'."); 
    }

    private void taskEngine_NotifyHost(IpdxMessage message)
    {
      if (message == null)
        return;

      switch (message.Recipient)
      {
        case "UI":
          switch (message.MessageType)
          {
            case IpdxMessageType.Text:
              WriteToDisplay(message.Text);
              break;

            case IpdxMessageType.Notification:
              switch (message.Text)
              {
                case "STARTED":
                  this.Invoke((Action)((() => SetStartedUI())));
                  break;

                case "STOPPED":
                  this.Invoke((Action)((() => SetStoppedUI())));
                  break;

                case "PAUSED":
                  this.Invoke((Action)((() => SetPausedUI())));
                  break;

                case "RESUMED":
                  this.Invoke((Action)((() => SetResumedUI())));
                  break;
              }
              break;
          }
          break;
      }
    }


    private void SetStartedUI()
    {
      SetControls("START");
      string message = "***********************************************************************************************" + g.crlf +
                          "** WinService Host has been Started at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm.ss.fff") + g.crlf +
                          "***********************************************************************************************" + g.crlf;
      this.WriteToDisplay(message, false);
    }

    private void SetStoppedUI()
    {
      string message = "***********************************************************************************************" + g.crlf +
                        "** WinService Host has been Stopped at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm.ss.fff") + g.crlf +
                        "***********************************************************************************************" + g.crlf;
      this.WriteToDisplay(message, false);

      SetControls("STOP");
    }


    private void SetPausedUI()
    {
      string message = "***********************************************************************************************" + g.crlf +
                        "** WinService Host has been Paused at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm.ss.fff") + g.crlf +
                        "***********************************************************************************************" + g.crlf;
      this.WriteToDisplay(message, false);

      SetControls("PAUSE");
    }


    private void SetResumedUI()
    {
      string message = "***********************************************************************************************" + g.crlf +
                        "** WinService Host has been Resumed at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm.ss.fff") + g.crlf +
                        "***********************************************************************************************" + g.crlf;
      this.WriteToDisplay(message, false);

      SetControls("RESUME");
    }

    private void taskEngine_TaskUpdate(TaskResult taskResult)
    {
      if (taskResult.TaskResultStatus == TaskResultStatus.NotExecuted)
      {
        int priority = 99;
        //if (taskResult.OriginalTask != null)
        //    priority = ((Task)taskResult.OriginalTask).Priority;

        lblStatus.Invoke((Action)((() => lblStatus.Text = taskResult.BeginDateTime.ToString("MM/dd/yyyy HH:mm:ss.fff") + "  " + taskResult.Message + " PRI:" + priority.ToString("00"))));
        return;
      }

      //if (taskResult.TaskName == "LogTest" && taskResult.TaskStatus == TaskStatus.Success)
      //  return;

      string exceptionReport = String.Empty;
      if (taskResult.Exception != null)
      {
        exceptionReport = "Exception: " + taskResult.Exception.Message + g.crlf + taskResult.Exception.StackTrace + g.crlf; 
      }

      string result = taskResult.TaskName.PadTo(16) + " "
              + taskResult.TaskResultStatus.ToString().PadTo(15) + "  TH:"
              + taskResult.ThreadId.ToString("000000") + " TSK:"
              + taskResult.TaskId.ToString("00000") + " "
              + taskResult.BeginDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + " "
              + taskResult.EndDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + " "
              + taskResult.DurationString + g.crlf
              + g.BlankString(24) + taskResult.Message + g.crlf
              + exceptionReport + g.crlf;

      _logger.Log(result);
      WriteToDisplay(result);

      if (taskResult.TaskName == "TaskQueueFailure")
      {
        StopService();
      }
    }

    private void PauseService()
    {
      if (_svcEngine == null)
        return;
      _svcEngine.Pause();

      string message = "***********************************************************************************************" + g.crlf +
                        "** WinService Host has been Paused at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm.ss.fff") + g.crlf +
                        "***********************************************************************************************" + g.crlf;
      this.WriteToDisplay(message, false);

      SetControls("PAUSE");
    }

    private void ResumeService()
    {
      if (_svcEngine == null)
        return;
      _svcEngine.Resume();

      string message = "***********************************************************************************************" + g.crlf +
                        "** WinService Host has been Resumed at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm.ss.fff") + g.crlf +
                        "***********************************************************************************************" + g.crlf;
      this.WriteToDisplay(message, false);

      SetControls("RESUME");
    }

    private void StopService()
    {
      //btnRun.Enabled = false;
      //cboTask.SelectedIndex = -1;
      //cboTask.Enabled = false;

      if (_svcEngine == null)
        return;

      _svcEngine.Stop();
      _svcEngine = null;

      UnsuspendTasks();

      string message = "***********************************************************************************************" + g.crlf +
                        "** WinService Host has been Stopped at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm.ss.fff") + g.crlf +
                        "***********************************************************************************************" + g.crlf;
      this.WriteToDisplay(message, false);

      SetControls("STOP");
    }

    private void SuspendTasks()
    {
      if (_svcEngine == null)
        return;

      _svcEngine.IsSuspended = true;
      //lblSuspended.Visible = true;

      //cboTask.Items.Remove("Suspend Tasks");
      //cboTask.Items.Add("Unsuspend Tasks");
    }

    private void UnsuspendTasks()
    {
      if (_svcEngine == null)
        return;

      _svcEngine.IsSuspended = false;
      //lblSuspended.Visible = false;

      //cboTask.Items.Remove("Unsuspend Tasks");
      //cboTask.Items.Add("Suspend Tasks");
    }

    private void SetControls(string command)
    {
      switch (command)
      {
        case "INITIAL":
          //mnuServiceStart.Enabled = true;
          //mnuServicePause.Enabled = false;
          //mnuServiceResume.Enabled = false;
          //mnuServiceStop.Enabled = false;
          tbtnStartService.Enabled = true;
          tbtnPauseService.Enabled = false;
          tbtnResumeService.Enabled = false;
          tbtnStopService.Enabled = false;
          break;

        case "START":
          //mnuServiceStart.Enabled = true;
          //mnuServicePause.Enabled = true;
          //mnuServiceResume.Enabled = false;
          //mnuServiceStop.Enabled = true;
          tbtnStartService.Enabled = false;
          tbtnPauseService.Enabled = true;
          tbtnResumeService.Enabled = false;
          tbtnStopService.Enabled = true;
          break;

        case "PAUSE":
          //mnuServiceStart.Enabled = false;
          //mnuServicePause.Enabled = false;
          //mnuServiceResume.Enabled = true;
          //mnuServiceStop.Enabled = true;
          tbtnStartService.Enabled = false;
          tbtnPauseService.Enabled = false;
          tbtnResumeService.Enabled = true;
          tbtnStopService.Enabled = true;
          break;

        case "RESUME":
          //mnuServiceStart.Enabled = false;
          //mnuServicePause.Enabled = true;
          //mnuServiceResume.Enabled = false;
          //mnuServiceStop.Enabled = true;
          tbtnStartService.Enabled = false;
          tbtnPauseService.Enabled = true;
          tbtnResumeService.Enabled = false;
          tbtnStopService.Enabled = true;
          break;

        case "STOP":
          //mnuServiceStart.Enabled = true;
          //mnuServicePause.Enabled = false;
          //mnuServiceResume.Enabled = false;
          //mnuServiceStop.Enabled = false;
          if (this.InvokeRequired)
          {
            this.Invoke((Action)(((() => tbtnStartService.Enabled = true))));
            this.Invoke((Action)(((() => tbtnPauseService.Enabled = false))));
            this.Invoke((Action)(((() => tbtnResumeService.Enabled = false))));
            this.Invoke((Action)(((() => tbtnStopService.Enabled = false))));
          }
          else
          {
            tbtnStartService.Enabled = true;
            tbtnPauseService.Enabled = false;
            tbtnResumeService.Enabled = false;
            tbtnStopService.Enabled = false;
          }

          break;
      }
    }

    private void GetNextNow()
    {
      if (_svcEngine == null)
        return;

      if (_svcEngine.WinServiceState != WinServiceState.Running)
        return;

      _svcEngine.GetNextTaskNow = true;
    }

    private void RefreshTaskList()
    {
      if (_svcEngine == null)
        return;

      if (_svcEngine.WinServiceState != WinServiceState.Running)
        return;

      _svcEngine.RefreshTaskList = true;
    }

    private void WriteToDisplay(string message)
    {
      if (txtReport.InvokeRequired)
      {
        txtReport.Invoke((Action)((() => txtReport.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " " + message + g.crlf)));
        txtReport.Invoke((Action)((() => txtReport.SelectionStart = txtReport.Text.Length)));
        txtReport.Invoke((Action)((() => txtReport.SelectionLength = 0)));
        txtReport.Invoke((Action)((() => txtReport.ScrollToCaret())));
      }
      else
      {
        txtReport.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " " + message + g.crlf;
        txtReport.SelectionStart = txtReport.Text.Length;
        txtReport.SelectionLength = 0;
        txtReport.ScrollToCaret(); 
      }

      Application.DoEvents();
    }

    private void WriteToDisplay(string message, bool prependTimeStamp)
    {
      string timestamp = String.Empty;
      if (prependTimeStamp)
        timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " ";

      if (txtReport.InvokeRequired)
      {
        txtReport.Invoke((Action)((() => {
          txtReport.Text += timestamp + message + g.crlf;
          txtReport.SelectionStart = txtReport.Text.Length;
          txtReport.SelectionLength = 0;
          txtReport.ScrollToCaret();
        })));
      }
      else
      {
        txtReport.Text += timestamp + message + g.crlf;
        txtReport.SelectionStart = txtReport.Text.Length;
        txtReport.SelectionLength = 0;
        txtReport.ScrollToCaret();
      }

      Application.DoEvents();
    }

    private void CloseForm()
    {
      // make sure we clear the queues... or at least let transactions complete... 
      this.Close();
    }


    private void frmMain_Shown(object sender, EventArgs e)
    {
      if (!_isFirstShowing)
        return;

      if (_closeUponStartup)
      {
        this.Close();
        return;
      }

      _isFirstShowing = false;
    }

    private void frmMain_KeyUp(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F1:
          break;
      }
    }

    private void ShowServiceAlert()
    {
      WinServiceHelper.ServiceAlert("Test service alert message."); 
    }

    private void TestLogWrite()
    {
      try
      {
        StringBuilder sb = new StringBuilder();
        sb.Append("LOG PATH: " + g.LogPath + g.crlf);
        sb.Append("AppConfig: " + g.crlf + g.AppConfig.CurrentImage + g.crlf);
        WriteToDisplay(sb.ToString()); 


        WriteToDisplay("Trying to write log record.");
        StartupLogging.WriteStartupLog("In TestWriteLog"); 
        using (var logger = new Logger())
        {
          logger.Log("Testing the log writing function.");
        }
      }
      catch (Exception ex)
      {
        WriteToDisplay(ex.ToReport());
      }
    }

    private void TestNotifications()
    {
      try
      {
        StringBuilder sb = new StringBuilder();

        using (var nm = new NotificationsManager("0"))
        {
        }
      }
      catch (Exception ex)
      {
        WriteToDisplay(ex.ToReport());
      }
    }
  }
}
