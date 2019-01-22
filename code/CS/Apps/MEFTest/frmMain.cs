using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Org.TSK.Business;
using Org.WSO.Transactions;
using Org.TP;
using Org.GS;
using Org.GS.Configuration;
using Org.GS.Performance;

namespace Org.MEFTest
{
  public partial class frmMain : Form
  {
    private a a;
    private Dictionary<string, TaskInfo> _taskInfo;
    private Dictionary<string, string> _envDictionary;
    private Dictionary<string, string> _dbForEnv;
    private string _fileTask = String.Empty;
    private string _env = String.Empty;
    private bool _processInputFile;

    List<Assembly> testAssemblies = new List<Assembly>();

    private StringBuilder sbAsmReport = new StringBuilder();

    [ImportMany(typeof(ITaskProcessorFactory))]
    public IEnumerable<Lazy<ITaskProcessorFactory, ITaskProcessorMetadata>> taskProcessorFactories;
    public CompositionContainer CompositionContainer;
    public Dictionary<string, ITaskProcessorFactory> LoadedTaskProcessorFactories;

    public bool _continueTask = true;

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
        case "RunTaskProcessor":
          RunTaskProcessor();
          break;

        case "WrapText":
          txtOut.WordWrap = ckWrapText.Checked;
          break;

        case "GetTypesFromTestAssemblies":
          GetTypesFromTestAssemblies();
          break;

        case "CancelTask":
          CancelTask();
          break;

        case "ClearOutput":
          txtOut.Text = String.Empty;
          Application.DoEvents();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void RunTaskProcessor()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        txtOut.Text = String.Empty;
        Application.DoEvents();

        string taskName = cboTaskToRun.Text;
        var taskInfo = _taskInfo[taskName];
        var parms = GetParms(taskInfo);
        var taskRequest = new TaskRequest(0, taskInfo.TaskName, taskInfo.TaskProcessorName, taskInfo.TaskProcessorVersion, 0, TaskRequestType.ScheduledTask,
                                          parms, DateTime.Now, false, false, null, null, false);


        TaskResult taskResult = null;

        ITaskProcessorFactory taskProcessorFactory = GetTaskProcessorFactory(taskInfo.TaskProcessorNameAndVersion);

        if (taskProcessorFactory == null)
        {
          this.Cursor = Cursors.Default;
          MessageBox.Show("The TaskProcessorFactory was not found for task '" + taskInfo.TaskProcessorNameAndVersion + "'." + g.crlf2 +
                          "Ensure that the configured path for MEF Modules is correct.",
                          "MEFTest - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        var taskProcessor = taskProcessorFactory.CreateTaskProcessor(taskInfo.TaskProcessorNameAndVersion);

        if (taskProcessor == null)
        {
          MessageBox.Show("Task processor '" + taskInfo.TaskProcessorNameAndVersion + "' was not created by the TaskProcessorFactory.",
                          "MEFTest - Task Processor Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
          this.Cursor = Cursors.Default;
          return;
        }

        taskProcessor.TaskRequest = taskRequest;
        btnCancelTask.Enabled = true;

        taskProcessor.ProcessTaskAsync(this.CheckContinue).ContinueWith(r =>
        {
          var asyncTaskResult = r;
          DisableCancelButton();
          string outputMessage = String.Empty;

          switch (asyncTaskResult.Status)
          {
            case TaskStatus.RanToCompletion:
              taskResult = asyncTaskResult.Result;

              switch (taskResult.TaskResultStatus)
              {
                case TaskResultStatus.Success:
                  outputMessage = "Task processor results successful." + g.crlf;
                  if (taskResult.Message.IsNotBlank())
                    outputMessage += g.crlf + taskResult.Message;
                  DisplayOutput(outputMessage, Cursors.Default);
                  break;

                default:
                  if (taskResult.Code == 6008)
                  {
                    var errorResponse = taskResult.Object as ErrorResponse;
                    outputMessage = taskResult.Message + g.crlf2 + errorResponse.ToReport();
                  }
                  else
                  {
                    outputMessage = taskResult.Message;
                    if (taskResult.Exception != null)
                      outputMessage += g.crlf2 + taskResult.Exception.ToReport();
                  }
                  DisplayOutput(outputMessage, Cursors.Default);
                  break;
              }
              break;

            case TaskStatus.Faulted:
              Exception asyncTaskException = asyncTaskResult.Exception;
              if (asyncTaskException != null)
                outputMessage = asyncTaskException.ToReport();
              else
                outputMessage = "The async task result is faulted, but no exception exists.";
              DisplayOutput(outputMessage, Cursors.Default);
              break;

            default:
              outputMessage = "Unhandled status returned from async task '" + asyncTaskResult.Status.ToString() + "'.";
              DisplayOutput(outputMessage, Cursors.Default);
              break;
          }
        });
      }
      catch (Exception ex)
      {
        btnCancelTask.Enabled = false;
        DisplayOutput("An exception occurred attempting to run the task processor '" + cboTaskToRun.Text + "'." + g.crlf2 + ex.ToReport(), Cursors.Default);
      }
    }

    private void GetTypesFromTestAssemblies()
    {
      try
      {

        txtOut.Text = String.Empty;

        foreach (var assy in testAssemblies)
        {
          txtOut.Text += g.crlf;
          txtOut.Text += "Assembly:" + assy.FullName + g.crlf;
          foreach (var type in assy.GetTypes())
          {
            var ca = type.GetCustomAttributes();

            if (ca.Count() > 0)
            {
              if (type.GetCustomAttributes().OfType<System.ComponentModel.Composition.ExportMetadataAttribute>().Count() > 0)
              {
                txtOut.Text += "  Type:" + type.FullName + g.crlf;
                var o = Activator.CreateInstance(type);
              }
            }

          }
        }
      }
      catch (Exception ex)
      {
        txtOut.Text += g.crlf2 + ex.ToReport();
      }
    }


    private void DisableCancelButton()
    {
      if (txtOut.InvokeRequired)
        txtOut.Invoke((Action)((() => {
        btnCancelTask.Enabled = false;
      })));
      else
        btnCancelTask.Enabled = false;
    }

    private void DisplayOutput(string message, Cursor cursor)
    {
      if (txtOut.InvokeRequired)
      {
        txtOut.Invoke((Action)((() =>
        {
          txtOut.Text = message;
          this.Cursor = cursor;
        })));
      }
      else
      {
        txtOut.Text = message;
        this.Cursor = cursor;
      }
    }

    private ParmSet GetParms(TaskInfo taskInfo)
    {
      bool getParmsFromDb = g.CI("GetParmsFromDb").ToBoolean();

      if (getParmsFromDb)
        return GetParmsFromDb(taskInfo);

      ParmSet parmSet = new ParmSet();
      string env = g.AppConfig.Variables["ENV-PARM"];

      string wsSpecPrefix = "MainSvc" + env;
      if (ckUseLocalWebService.Checked)
        wsSpecPrefix = "MainSvcLocal";

      bool isDryRun = g.CI("IsDryRun").ToBoolean();
      if (isDryRun)
        parmSet.Add(new Parm("IsDryRun", true));

      // task node folders are defined for processors derived from the FileTaskProcessorBase class.
      if (taskInfo.TaskNodeFolder.IsNotBlank())
      {
        parmSet.Add(new Parm("$FSSTEM$", g.AppConfig.Variables["FSSTEM-PARM"]));
        parmSet.Add(new Parm("$ENV$", g.AppConfig.Variables["ENV-PARM"]));
        parmSet.Add(new Parm("$TASKNODE$", g.AppConfig.Variables["TASKNODE-PARM"]));
        parmSet.Add(new Parm("InputFilePath", g.CI("InputFilePath")));
        parmSet.Add(new Parm("ProcessedFilePath", g.CI("ProcessedFilePath")));
        parmSet.Add(new Parm("ErrorFilePath", g.CI("ErrorFilePath")));
        parmSet.Add(new Parm("ConfigWsSpec", g.AppConfig.GetWsSpec(wsSpecPrefix)));

        string excelExtractWcfTimeoutSeconds = g.CI("ExcelExtractWcfTimeoutSeconds");
        if (excelExtractWcfTimeoutSeconds.IsBlank())
          excelExtractWcfTimeoutSeconds = "0";
        parmSet.Add(new Parm("ExcelExtractWcfTimeoutSeconds", excelExtractWcfTimeoutSeconds));

        var stmtType = taskInfo.StatementType;
        if (stmtType.IsNotBlank())
          parmSet.Add(new Parm("StatementType", stmtType));
      }

      if (taskInfo.InputDbPrefix.IsNotBlank())
      {
        var dbSpec = g.GetDbSpec(taskInfo.InputDbPrefix);
        string dbKey = taskInfo.InputDbPrefix + env;
        if (!_dbForEnv.ContainsKey(dbKey))
          throw new Exception("The key '" + dbKey + "' does not exist in the 'DbForEnv' dictionary.");
        dbSpec.DbServer = _dbForEnv[dbKey];
        parmSet.Add(new Parm("InputDbSpec", dbSpec));
      }

      if (taskInfo.OutputDbPrefix.IsNotBlank())
      {
        var dbSpec = g.GetDbSpec(taskInfo.OutputDbPrefix);
        string dbKey = taskInfo.OutputDbPrefix + env;
        if (!_dbForEnv.ContainsKey(dbKey))
          throw new Exception("The key '" + dbKey + "' does not exist in the 'DbForEnv' dictionary.");
        dbSpec.DbServer = _dbForEnv[dbKey];
        parmSet.Add(new Parm("OutputDbSpec", dbSpec));
      }

      switch (taskInfo.TaskName)
      {
        case "EMK3DataImport":
          parmSet.Add(new Parm("InputDbSpec", g.GetDbSpec(env + "Input")));
          parmSet.Add(new Parm("OutputDbSpec", g.GetDbSpec(env + "Output")));
          parmSet.Add(new Parm("OpsControlDbSpec", g.GetDbSpec(env + "OpsControl")));
          parmSet.Add(new Parm("ProductionCutoffDay", g.CI("ProductionCutoffDay")));
          parmSet.Add(new Parm("LastProductionDateProcessed", g.CI("LastProductionDateProcessed")));
          parmSet.Add(new Parm("ScheduledTaskID", g.CI("ScheduledTaskID")));
          parmSet.Add(new Parm("ProductIdCrossReference", g.AppConfig.GetDictionary("ProductIdCrossReference")));
          parmSet.Add(new Parm("EMK3ImportComponents", g.AppConfig.GetDictionary("EMK3ImportComponents")));
          parmSet.Add(new Parm("EMK3ImportParms", g.AppConfig.GetDictionary("EMK3ImportParms")));
          parmSet.Add(new Parm("ProductionDataCfg", g.AppConfig.GetDictionary("ProductionDataCfg")));
          parmSet.Add(new Parm("GasAnalysisCfg", g.AppConfig.GetDictionary("GasAnalysisCfg")));
          parmSet.Add(new Parm("GasAnalysisLiquidsCfg", g.AppConfig.GetDictionary("GasAnalysisLiquidsCfg")));
          parmSet.Add(new Parm("PurchaserStatementDataCfg", g.AppConfig.GetDictionary("PurchaserStatementDataCfg")));
          parmSet.Add(new Parm("PurchaserStatementLiquidsCfg", g.AppConfig.GetDictionary("PurchaserStatementLiquidsCfg")));
          parmSet.Add(new Parm("PurchaserStatementFeesCfg", g.AppConfig.GetDictionary("PurchaserStatementFeesCfg")));
          if (g.CIExists("OverrideTargetProductionDate"))
            parmSet.Add(new Parm("OverrideTargetProductionDate", g.CI("OverrideTargetProductionDate")));
          break;

        case "EMK3CoreDataImport":
          parmSet.Add(new Parm("InputDbSpec", g.GetDbSpec(env + "CoreInput")));
          parmSet.Add(new Parm("OutputDbSpec", g.GetDbSpec(env + "CoreOutput")));
          parmSet.Add(new Parm("ProductionDate", g.CI("ProductionDate")));
          parmSet.Add(new Parm("EMK3CoreImportComponents", g.AppConfig.GetDictionary("EMK3CoreImportComponents")));
          parmSet.Add(new Parm("EMK3ImportParms", g.AppConfig.GetDictionary("EMK3ImportParms")));
          parmSet.Add(new Parm("ProductionDataCfg", g.AppConfig.GetDictionary("ProductionDataCfg")));
          parmSet.Add(new Parm("WellheaderCfg", g.AppConfig.GetDictionary("WellheaderCfg")));
          break;

        case "FieldVisorAPI":
          parmSet.Add(new Parm("FieldVisorDbSpecPrefix", g.CI("FieldVisorDbSpecPrefix")));
          parmSet.Add(new Parm("FVDbSpecPrefix", g.CI("FVDbSpecPrefix")));
          parmSet.Add(new Parm("AuthenticateURI", g.CI("AuthenticateURI")));
          parmSet.Add(new Parm("FieldingSystemsURI", g.CI("FieldingSystemsURI")));
          parmSet.Add(new Parm("SecurityKey", g.CI("SecurityKey")));
          parmSet.Add(new Parm("CompanyCode", g.CI("CompanyCode")));
          parmSet.Add(new Parm("DailyProduction_DaysToProcess", g.CI("DailyProduction_DaysToProcess")));
          parmSet.Add(new Parm("TankEntryEod_DaysToProcess", g.CI("TankEntryEod_DaysToProcess")));
          parmSet.Add(new Parm("ProductionEntry_DaysToProcess", g.CI("ProductionEntry_DaysToProcess")));
          parmSet.Add(new Parm("ConnectionStringName", g.CI("ConnectionStringName")));
          parmSet.Add(new Parm("DefaultConnectionStringName", g.CI("DefaultConnectionStringName")));
          parmSet.Add(new Parm("ProcessControl", g.CI("ProcessControl")));
          break;

        case "GenscapeStmt":
          parmSet.Add(new Parm("ConfigFilePath", g.CI("ConfigFilePath")));
          break;

        case "OpsDiagnostics":
          parmSet.Add(new Parm("PerfProfileSet", GetPerfProfileSet()));
          break;

        case "LogMaintenance":
          parmSet.Add(new Parm("LoggingDbSpec", g.GetDbSpec("Log")));
          break;

        case "ListRemoteFolder":
          parmSet.Add(new Parm("ShareFileHostName", g.CI("ShareFileHostName")));
          parmSet.Add(new Parm("ShareFileUserName", g.CI("ShareFileUserName")));
          parmSet.Add(new Parm("ShareFilePassword", g.CI("ShareFilePassword")));
          parmSet.Add(new Parm("ShareFileClientId", g.CI("ShareFileClientId")));
          parmSet.Add(new Parm("ShareFileClientSecret", g.CI("ShareFileClientSecret")));
          parmSet.Add(new Parm("RootFolderId", g.CI("RootFolderId")));
          parmSet.Add(new Parm("RemoteTargetFolder", g.CI("RemoteTargetFolder")));
          break;

        case "UploadSingleFile":
          parmSet.Add(new Parm("ShareFileHostName", g.CI("ShareFileHostName")));
          parmSet.Add(new Parm("ShareFileUserName", g.CI("ShareFileUserName")));
          parmSet.Add(new Parm("ShareFilePassword", g.CI("ShareFilePassword")));
          parmSet.Add(new Parm("ShareFileClientId", g.CI("ShareFileClientId")));
          parmSet.Add(new Parm("ShareFileClientSecret", g.CI("ShareFileClientSecret")));
          parmSet.Add(new Parm("RootFolderId", g.CI("RootFolderId")));
          parmSet.Add(new Parm("RemoteTargetFolder", g.CI("RemoteTargetFolder")));
          parmSet.Add(new Parm("AllowDuplicateFiles", g.CI("AllowDuplicateFiles")));
          parmSet.Add(new Parm("ArchiveFolder", g.CI("ArchiveFolder")));
          parmSet.Add(new Parm("UploadFilePath", g.CI("UploadFilePath")));
          parmSet.Add(new Parm("MaxFilesUpload", g.CI("MaxFilesUpload")));
          parmSet.Add(new Parm("MaxUploadSize", g.CI("MaxUploadSize")));
          break;

        case "UploadByFolder":
          parmSet.Add(new Parm("ShareFileHostName", g.CI("ShareFileHostName")));
          parmSet.Add(new Parm("ShareFileUserName", g.CI("ShareFileUserName")));
          parmSet.Add(new Parm("ShareFilePassword", g.CI("ShareFilePassword")));
          parmSet.Add(new Parm("ShareFileClientId", g.CI("ShareFileClientId")));
          parmSet.Add(new Parm("ShareFileClientSecret", g.CI("ShareFileClientSecret")));
          parmSet.Add(new Parm("RootFolderId", g.CI("RootFolderId")));
          parmSet.Add(new Parm("RemoteTargetFolder", g.CI("RemoteTargetFolder")));
          parmSet.Add(new Parm("AllowDuplicateFiles", g.CI("AllowDuplicateFiles")));
          parmSet.Add(new Parm("ArchiveFolder", g.CI("ArchiveFolder")));
          parmSet.Add(new Parm("UploadFolderPath", g.CI("UploadFolderPath")));
          parmSet.Add(new Parm("MaxFilesUpload", g.CI("MaxFilesUpload")));
          parmSet.Add(new Parm("MaxUploadSize", g.CI("MaxUploadSize")));
          break;

        case "DeleteRemoteFile":
          parmSet.Add(new Parm("ShareFileHostName", g.CI("ShareFileHostName")));
          parmSet.Add(new Parm("ShareFileUserName", g.CI("ShareFileUserName")));
          parmSet.Add(new Parm("ShareFilePassword", g.CI("ShareFilePassword")));
          parmSet.Add(new Parm("ShareFileClientId", g.CI("ShareFileClientId")));
          parmSet.Add(new Parm("ShareFileClientSecret", g.CI("ShareFileClientSecret")));
          parmSet.Add(new Parm("RootFolderId", g.CI("RootFolderId")));
          parmSet.Add(new Parm("RemoteTargetFolder", g.CI("RemoteTargetFolder")));
          parmSet.Add(new Parm("DeleteFileName", g.CI("DeleteFileName")));
          break;

        case "ClearRemoteFolder":
          parmSet.Add(new Parm("ShareFileHostName", g.CI("ShareFileHostName")));
          parmSet.Add(new Parm("ShareFileUserName", g.CI("ShareFileUserName")));
          parmSet.Add(new Parm("ShareFilePassword", g.CI("ShareFilePassword")));
          parmSet.Add(new Parm("ShareFileClientId", g.CI("ShareFileClientId")));
          parmSet.Add(new Parm("ShareFileClientSecret", g.CI("ShareFileClientSecret")));
          parmSet.Add(new Parm("RootFolderId", g.CI("RootFolderId")));
          parmSet.Add(new Parm("RemoteTargetFolder", g.CI("RemoteTargetFolder")));
          break;

        case "DailyDownloadLaScadaFiles":
          parmSet.Add(new Parm("ShareFileLoginParms", g.AppConfig.GetDictionary("ShareFileLoginParms")));
          parmSet.Add(new Parm("RootFolderId", g.CI("RootFolderId")));
          parmSet.Add(new Parm("OutputFolderPath", g.CI("OutputFolderPath")));
          parmSet.Add(new Parm("RemoteTargetFolder", g.CI("RemoteTargetFolder")));
          parmSet.Add(new Parm("RemoteArchiveFolder", g.CI("RemoteArchiveFolder")));
          parmSet.Add(new Parm("FileProcessingOption", g.CI("FileProcessingOption")));
          parmSet.Add(new Parm("ArchiveRemoteFiles", g.CI("ArchiveRemoteFiles")));
          parmSet.Add(new Parm("SuppressRemoteDelete", g.CI("SuppressRemoteDelete")));
          parmSet.Add(new Parm("MaxFilesDownload", g.CI("MaxFilesDownload")));
          parmSet.Add(new Parm("MaxDownloadSize", g.CI("MaxDownloadSize")));
          break;

        case "TaskMonitor":
          parmSet.Add(new Parm("HourlyMonitoredTasks", g.AppConfig.GetDictionary("HourlyMonitoredTasks")));
          parmSet.Add(new Parm("ApprovalEmailHexString", g.CI("ApprovalEmailHexString")));
          parmSet.Add(new Parm("ConfigDbSpec", g.GetDbSpec("Tasks")));
          parmSet.Add(new Parm("LastNotificationSent", g.AppConfig.GetDictionary("LastNotificationSent")));
          break;

        case "TaskScheduler":
          parmSet.Add(new Parm("TasksDbSpec", g.GetDbSpec("Tasks")));
          parmSet.Add(new Parm("Interval", g.CI("Interval")));
          break;
      }

      return parmSet;
    }

    private ParmSet GetParmsFromDb(TaskInfo taskInfo)
    {
      string dbSpecPrefix = "TaskScheduling" + _env;
      var configDbSpec = g.GetDbSpec(dbSpecPrefix);

      try
      {
        using (var repo = new TaskRepository(configDbSpec))
        {
          var parmSet = repo.GetParametersForTask(taskInfo.TaskName);
          return parmSet;
        }
      }
      catch (Exception exc)
      {
        throw new Exception("An exception occurred while attempting to retrieve the schedule task parameters from the database for task name '" + taskInfo.TaskName + "'.", exc);
      }
    }

    private void taskProcessor_TaskMessage(NotifyMessage obj)
    {
      // see if we can get progress messages
    }

    public ITaskProcessorFactory GetTaskProcessorFactory(string processorKey)
    {
      if (this.LoadedTaskProcessorFactories.ContainsKey(processorKey))
        return this.LoadedTaskProcessorFactories[processorKey];

      foreach (Lazy<ITaskProcessorFactory, ITaskProcessorMetadata> taskProcessorFactory in taskProcessorFactories)
      {
        if (taskProcessorFactory.Metadata.Processors.ToListContains(Constants.SpaceDelimiter, processorKey))
        {
          this.LoadedTaskProcessorFactories.Add(processorKey, taskProcessorFactory.Value);
          return taskProcessorFactory.Value;
        }
      }

      return null;
    }

    private void InitializeForm()
    {
      try
      {
        a = new a();

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during initialization of the application object 'a'." + g.crlf2 +
                        ex.ToReport(), "MEFTest - MEF Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      btnCancelTask.Enabled = false;
      txtOut.WordWrap = ckWrapText.Checked;

      SetUpTaskInfo();

      _dbForEnv = g.GetDictionary("DbForEnv");

      var taskProcessorList = _taskInfo.Keys.ToList();

      taskProcessorList.Insert(0, String.Empty);
      cboTaskToRun.DataSource = taskProcessorList;

      string selectedTaskProcessor = g.CI("SelectedTaskProcessor");
      bool taskProcessorSelected = false;
      if (selectedTaskProcessor.IsNotBlank())
      {
        for (int i = 0; i < cboTaskToRun.Items.Count; i++)
        {
          if (cboTaskToRun.Items[i].ToString() == selectedTaskProcessor)
          {
            cboTaskToRun.SelectedIndex = i;
            taskProcessorSelected = true;
            break;
          }
        }
      }

      _envDictionary = g.AppConfig.GetDictionary("EnvironmentDictionary");
      var envList = _envDictionary.Keys.ToList();
      envList.Insert(0, string.Empty);
      cboEnvironment.DataSource = envList;

      string selectedEnvironment = g.CI("SelectedEnvironment");
      bool environmentSelected = false;
      if (selectedEnvironment.IsNotBlank())
      {
        for (int i = 0; i < cboEnvironment.Items.Count; i++)
        {
          if (cboEnvironment.Items[i].ToString() == selectedEnvironment)
          {
            cboEnvironment.SelectedIndex = i;
            environmentSelected = true;
            break;
          }
        }
      }

      try
      {
        _processInputFile = true;
        if (g.CIExists("ProcessInputFile"))
          _processInputFile = g.CI("ProcessInputFile").ToBoolean();

        btnRunTaskProcessor.Enabled = taskProcessorSelected && environmentSelected;
        _continueTask = true;

        using (var catalog = new AggregateCatalog())
        {
          string modulesPath = g.CI("ModulesPath");

          var directoryCatalog = new DirectoryCatalog(modulesPath);

          catalog.Catalogs.Add(directoryCatalog);
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

        this.LoadedTaskProcessorFactories = new Dictionary<string, ITaskProcessorFactory>();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during application initialization." + g.crlf2 + ex.ToReport(),
                        "MEFTest - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void SetUpTaskInfo()
    {
      var taskInfoDictionary = g.GetDictionary("TaskInfoDictionary");

      _taskInfo = new Dictionary<string, TaskInfo>();

      foreach (var kvp in taskInfoDictionary)
      {
        if (kvp.Key != "LEGEND")
        {
          var taskInfo = new TaskInfo(kvp.Key, kvp.Value);
          if (_taskInfo.ContainsKey(taskInfo.TaskName))
            throw new Exception("A duplicate key exists in the TaskInfoDictionary.");
          _taskInfo.Add(taskInfo.TaskName, taskInfo);
        }
      }
    }

    private void taskProcessor_TaskUpdate(TaskResult taskResult)
    {
      string message = taskResult.Message;

      if (txtOut.InvokeRequired)
      {
        txtOut.BeginInvoke((Action)((() =>
        {
          txtOut.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " " + message + g.crlf;
          txtOut.SelectionStart = txtOut.Text.Length;
          txtOut.SelectionLength = 0;
          txtOut.ScrollToCaret();
        }
                                    )));
        Application.DoEvents();
      }
      else
      {
        txtOut.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " " + message + g.crlf;
        txtOut.SelectionStart = txtOut.Text.Length;
        txtOut.SelectionLength = 0;
        txtOut.ScrollToCaret();
        Application.DoEvents();
      }
    }

    private void CancelTask()
    {
      _continueTask = false;
    }

    private bool CheckContinue()
    {
      return _continueTask;
    }

    private void Combo_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cboTaskToRun.Text.IsBlank() || cboEnvironment.Text.IsBlank())
      {
        btnRunTaskProcessor.Enabled = false;
        return;
      }

      btnRunTaskProcessor.Enabled = true;

      _fileTask = _taskInfo[cboTaskToRun.Text].TaskNodeFolder;
      _env = _envDictionary[cboEnvironment.Text];

      if (!_processInputFile)
        return;

      g.AppConfig.Variables["TASKNODE-PARM"] = _fileTask;
      g.AppConfig.Variables["ENV-PARM"] = _env;

      string fsStem = g.AppConfig.Variables["FSSTEM-PARM"];
      string env = g.AppConfig.Variables["ENV-PARM"];
      string taskNode = g.AppConfig.Variables["TASKNODE-PARM"];

      string inputFilePath = g.CI("InputFilePath");
      inputFilePath = inputFilePath.Replace("$FSSTEM$", fsStem).Replace("$ENV$", env).Replace("$TASKNODE$", taskNode);

      txtOut.Text = inputFilePath;
    }

    private PerfProfileSet GetPerfProfileSet()
    {
      var perfProfileSet = new PerfProfileSet();
      var perfProfile = new PerfProfile();
      perfProfile.ProfileName = "TestProfile";

      var category = new Category();
      category.CategoryName = "Processor";
      category.CategoryType = PerformanceCounterCategoryType.MultiInstance;

      var counter = new Counter();
      counter.CategoryName = "Processor";
      counter.CounterType = PerformanceCounterType.Timer100NsInverse;
      counter.CounterName = "% Processor Time";
      counter.InstanceName = "_Total";
      category.CounterSet.Add("Processor_Time_Total", counter);

      counter = new Counter();
      counter.CategoryName = "Processor";
      counter.CounterType = PerformanceCounterType.Timer100NsInverse;
      counter.CounterName = "% Processor Time";
      counter.InstanceName = "0";
      category.CounterSet.Add("Processor_Time_0", counter);

      counter = new Counter();
      counter.CategoryName = "Processor";
      counter.CounterType = PerformanceCounterType.Timer100NsInverse;
      counter.CounterName = "% Processor Time";
      counter.InstanceName = "1";
      category.CounterSet.Add("Processor_Time_1", counter);

      counter = new Counter();
      counter.CategoryName = "Processor";
      counter.CounterType = PerformanceCounterType.Timer100NsInverse;
      counter.CounterName = "% Processor Time";
      counter.InstanceName = "2";
      category.CounterSet.Add("Processor_Time_2", counter);

      counter = new Counter();
      counter.CategoryName = "Processor";
      counter.CounterType = PerformanceCounterType.Timer100NsInverse;
      counter.CounterName = "% Processor Time";
      counter.InstanceName = "3";
      category.CounterSet.Add("Processor_Time_3", counter);

      perfProfile.CategorySet.Add(category.CategoryName, category);

      category = new Category();
      category.CategoryName = "Memory";
      category.CategoryType = PerformanceCounterCategoryType.MultiInstance;

      counter = new Counter();
      counter.CategoryName = "Memory";
      counter.InstanceName = String.Empty;
      counter.CounterType = PerformanceCounterType.NumberOfItems64;
      counter.CounterName = "Available Bytes";
      category.CounterSet.Add("Memory_AvailableBytes", counter);

      counter = new Counter();
      counter.CategoryName = "Memory";
      counter.InstanceName = String.Empty;
      counter.CounterType = PerformanceCounterType.NumberOfItems64;
      counter.CounterName = "Committed Bytes";
      category.CounterSet.Add("Memory_CommittedBytes", counter);

      counter = new Counter();
      counter.CategoryName = "Memory";
      counter.InstanceName = String.Empty;
      counter.CounterType = PerformanceCounterType.RawFraction;
      counter.CounterName = "% Committed Bytes In Use";
      category.CounterSet.Add("Memory_PctCommittedBytesInUse", counter);

      perfProfile.CategorySet.Add(category.CategoryName, category);

      perfProfileSet.Add(perfProfile.ProfileName, perfProfile);

      return perfProfileSet;
    }
  }
}
