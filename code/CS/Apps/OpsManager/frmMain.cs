using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;
using System.Xml.Linq;
using Org.Dx.Business;
using OpsCtl = Org.OpsManager.Controls;
using Org.TSK.Business;
using Org.TSK.Business.Models;
using tsk = Org.TSK.Business.Models;
using Org.SF;
using Org.WSO.Transactions;
using Org.WSO;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS;

namespace Org.OpsManager
{
  public partial class frmMain : Form
  {
    private a a;
    private SortedList<int, ScheduledTask> _scheduledTasks;
    private SortedList<int, ScheduledTaskGroup> _scheduledTaskGroups;
    private List<string> _windowsServices;
    private Dictionary<string, string> _serviceEndpoints;
    private int? _selectedTaskGroup = null;
    private OpsData _opsData = new OpsData();
    private bool _isInitLoad = true;
    private string _winMergeExePath;
    private bool _isFirstShowing = true;

    private AppLogSet _appLogSet;

    private List<NotifyConfigSet> _notifyConfigSets;
    private List<string> _rrTest = new List<string>();
    private List<string> _rrProd = new List<string>();
    private List<string> _dbTest = new List<string>();
    private List<string> _dbProd = new List<string>();
    private string _testMapPath = String.Empty;
    private string _prodMapPath = String.Empty;
    private ConfigDbSpec _currConfigDbSpec = new ConfigDbSpec();
    private ConfigDbSpec _oppConfigDbSpec = new ConfigDbSpec();
    private DataTable _maps = new DataTable();

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
      lblStatus.Text = "Ready";
      _isInitLoad = false;
    }

    #region Action

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "GetScheduledTasks":
          LoadScheduledTasksToGrid();
          break;

        case "EditScheduledTask":
          if (IsValidDoubleLeftClick(sender, (MouseEventArgs)e))
            EditScheduledTask();
          break;

        case "EditMaps":
          EditMaps();
          break;

        case "NewScheduledTask":
          NewScheduledTask();
          break;

        case "DeleteScheduledTask":
          DeleteScheduledTask();
          break;

        case "DisplayTaskReport":
          GetTaskReport(true);
          break;

        case "GetTasksReport":
          GetTaskReport(false);
          break;

        case "MigrateTask":
          MigrateScheduledTask();
          break;

        case "ViewProdMap":
          ViewProdMap();
          break;

        case "ViewTestMap":
          ViewTestMap();
          break;

        case "CompareMapFiles":
          CompareMapFiles();
          break;

        case "DeployToProd":
          DeployToProd();
          break;

        case "CopyToTest":
          CopyToTest();
          break;

        case "ViewRunHistory":
          ViewRunHistory();
          break;

        case "CopyScheduleAndParametersFrom":
          CopyScheduleAndParametersFrom();
          break;

        case "MaintainTaskParameters":
          MaintainTaskParameters();
          break;

        case "ViewScheduledRuns":
          ViewScheduledRuns();
          break;

        case "EnvironmentChange":
          if (!_isInitLoad)
            UpdateConfigDbSpec();
          break;

        case "GetMaps":
          GetMaps();
          break;

        case "GetWindowsServices":
          GetWinServices();
          break;

        case "StartWinService":
        case "StopWinService":
        case "PauseWinService":
        case "ResumeWinService":
          StartStopWindowsService(action);
          break;

        case "GetWebSites":
          GetWebSites();
          break;

        case "StartWebSite":
        case "StopWebSite":
          StartStopWebSite(action);
          break;

        case "GetAppPools":
          GetAppPools();
          break;

        case "StartAppPool":
        case "StopAppPool":
          StartStopAppPool(action);
          break;

        case "SetActiveOn":
        case "SetActiveOff":
          SetActive(action);
          break;

        case "SetTaskFreq300":
        case "SetTaskFreq600":
        case "SetTaskFreq900":
        case "SetTaskFreq1800":
        case "SetTaskFreq3600":
          SetTaskFrequencyTo(action);
          break;

        case "SetDryRunOn":
        case "SetDryRunOff":
          SetDryRun(action);
          break;

        case "SetRunUntilOn":
        case "SetRunUntilOff":
          SetRunUntil(action);
          break;

        case "SetRunUntilOverrideOn":
        case "SetRunUntilOverrideOff":
          SetRunUntilOverride(action);
          break;

        case "AssignToTaskGroup":
          AssignToTaskGroup();
          break;

        case "RefreshLog":
          RefreshLog();
          break;

        case "LogSelectionChange":
          UpdateLogDetails();
          break;

        case "GetNotifyConfigReport":
          GetNotifyConfigReport();
          break;

        case "NewNotificationObject":
          NewNotificationObject();
          break;

        case "DeleteNotificationObject":
          DeleteNotificationObject();
          break;

        case "RefreshNotificationsTree":
          ListNotifyConfigsTree();
          break;

        case "IdentifierSelectionChange":
          UpdateIdentifiersGrid();
          break;

        case "InsertIdentifier":
          InsertUpdateIdentifier(action);
          break;

        case "UpdateIdentifier":
          if (IsValidDoubleLeftClick(sender, (MouseEventArgs)e))
            InsertUpdateIdentifier(action);
          break;

        case "MigrateIdentifier":
          MigrateIdentifier();
          break;

        case "IdentifierDescChange":
          ShowHideIdentifiers();
          break;

        case "ShowServiceTaskReport":
        case "RemoveFromQueue":
        case "RemoveFromAssignment":
        case "RunNow":
        case "RefreshTaskRequests":
        case "RefreshTaskList":
        case "PingWebService":
        case "PauseTaskProcessing":
        case "ResumeTaskProcessing":
          ManageTaskProcessing(action);
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    #endregion

    #region Scheduled Tasks

    private SortedList<int, ScheduledTaskGroup> GetScheduledTaskGroups()
    {
      try
      {
        using (var taskRepo = new TaskRepository(_opsData.TasksDbSpec))
        {
          return taskRepo.GetScheduledTaskGroups();
        }
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        throw new Exception("An exception occurred when attempting to get the list of ScheduledTaskGroups.", ex);
      }
    }

    private void LoadScheduledTasksToGrid()
    {
      this.Cursor = Cursors.WaitCursor;

      string taskName = String.Empty;

      try
      {
        lblStatus.Text = "Loading tasks...";

        _scheduledTasks = new SortedList<int, ScheduledTask>();
        var taskSchedules = new SortedList<int, tsk.TaskSchedule>();
        var lastSuccessfulRuns = new Dictionary<string, DateTime>();

        using (var taskRepo = new TaskRepository(_opsData.TasksDbSpec))
        {
          _scheduledTasks = taskRepo.GetScheduledTasks(null);

          foreach (var scheduledTask in _scheduledTasks.Values)
          {
            var taskGroup = taskRepo.GetScheduledTaskGroup(scheduledTask.ScheduledTaskId);

            if (taskGroup != null)
            {
              scheduledTask.TaskGroupId = taskGroup.TaskGroupId;
              scheduledTask.TaskGroupName = taskGroup.TaskGroupName;
            }
          }

          taskSchedules = taskRepo.GetTaskSchedules();
          lastSuccessfulRuns = taskRepo.GetLastSuccessfulRun(_scheduledTasks.Values.ToList());

          foreach (var taskSchedule in taskSchedules)
          {
            if (_scheduledTasks.ContainsKey(taskSchedule.Value.ScheduledTaskId))
            {
              var task = _scheduledTasks[taskSchedule.Value.ScheduledTaskId];
              task.TaskSchedule = taskSchedule.Value;
              if (taskSchedule.Value.IsActive && taskSchedule.Value.TaskScheduleId == task.ActiveScheduleId)
              {
                var scheduleElements = taskRepo.GetScheduleElements(taskSchedule.Value.TaskScheduleId);
                foreach (var scheduleElement in scheduleElements)
                {
                  if (scheduleElement.Value.IsActive)
                    taskSchedule.Value.TaskScheduleElements.Add(scheduleElement.Value);
                }
              }
            }
          }
        }

        gvScheduledTasks.Rows.Clear();

        foreach (var st in _scheduledTasks.Values)
        {
          // if a task group is selected, only include tasks in that group
          if (_selectedTaskGroup.HasValue)
          {
            if (_selectedTaskGroup.Value == -1)
            {
              if (st.TaskGroupId != null)
                continue;
            }
            else
            {
              if (_selectedTaskGroup.Value != st.TaskGroupId)
                continue;
            }
          }

          string processorType = st.ProcessorTypeId.ToEnum<ProcessorType>(ProcessorType.NotSet).ToString();
          string periodContext = st.RunUntilPeriodContextID.ToEnum<PeriodContexts>(PeriodContexts.NotSet).ToString();
          if (periodContext == "NotSet")
            periodContext = String.Empty;

          string taskAssignments = String.Empty;
          if (st.TaskAssignmentSet != null && st.TaskAssignmentSet.Count > 0)
          {
            foreach (var taskAssignment in st.TaskAssignmentSet)
            {
              if (taskAssignments.IsNotBlank())
                taskAssignments += "," + taskAssignment.TaskServiceName;
              else
                taskAssignments = taskAssignment.TaskServiceName;
            }
          }

          string startTime = String.Empty;
          string endTime = String.Empty;
          string freq = String.Empty;
          string runForPeriod = String.Empty;
          if (st.RunUntilTask)
          {
            if (lastSuccessfulRuns.ContainsKey(st.TaskName))
            {
              int offsetMinutes = st.RunUntilOffsetMinutes.HasValue ? st.RunUntilOffsetMinutes.Value : 0;
              DateTime lastRunTime = lastSuccessfulRuns[st.TaskName];
              DateTime currentTime = DateTime.Now;

              var currentPeriod = st.CurrentPeriod;

              if (lastRunTime > currentPeriod.StartDateTime && lastRunTime < currentPeriod.EndDateTime)
                runForPeriod = "Y";
              else
                runForPeriod = "N";
            }
            else
              runForPeriod = "N";
          }

          string isDryRun = String.Empty;
          using (var taskRepo = new TaskRepository(_opsData.TasksDbSpec))
          {
            var taskParameters = taskRepo.GetTaskParameters(st.ScheduledTaskId);
            foreach (var p in taskParameters.Values)
            {
              if (p.ParameterName == "IsDryRun")
              {
                if (p.ParameterValue.ToLower() == "true")
                  isDryRun = "Y";
                else if (p.ParameterValue.ToLower() == "false")
                  isDryRun = "N";
                break;
              }
            }
          }

          if (st.TaskSchedule != null)
          {
            var taskScheduleElement = st.TaskSchedule.TaskScheduleElements.FirstOrDefault();
            if (taskScheduleElement != null)
            {
              startTime = taskScheduleElement.StartTime.HasValue ? taskScheduleElement.StartTime.Value.ToString(@"hh\:mm") : String.Empty;
              endTime = taskScheduleElement.EndTime.HasValue ? taskScheduleElement.EndTime.Value.ToString(@"hh\:mm") : String.Empty;
              freq = taskScheduleElement.FrequencySeconds.HasValue ? taskScheduleElement.FrequencySeconds.ToString() : String.Empty;
            }
          }

          string runUntilOverride = String.Empty;
          if (st.RunUntilTask)
          {
            if (st.RunUntilOverride)
              runUntilOverride = "Y";
            else
              runUntilOverride = "N";
          }

          taskName = st.TaskName;

          gvScheduledTasks.Rows.Add(
            st.ScheduledTaskId,
            st.TaskName,
            st.ProcessorName + "_" + st.ProcessorVersion,
            (st.TaskGroupName.IsNotBlank() ? st.TaskGroupName : String.Empty),
            (st.ProcessorTypeId == 0 ? "MEFCatalog" : "StandardCatalog"),
            startTime,
            endTime,
            freq,
            st.IsActive ? "Y" : "N",
            isDryRun,
            st.RunUntilTask ? "Y" : "N",
            runForPeriod,
            runUntilOverride,
            periodContext,
            st.RunUntilOffsetMinutes,
            st.TrackHistory ? "Y" : "N",
            taskAssignments);
        }

        lblStatus.Text = "Loaded " + _scheduledTasks.Count.ToString() + " Scheduled Tasks";

        clbScheduledRuns.Items.Clear();
        List<string> tasks = _scheduledTasks.Values.Select(t => t.TaskName).ToList();
        tasks.Sort();

        foreach (var task in tasks)
        {
          clbScheduledRuns.Items.Add(task);
        }
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        throw new Exception("An exception occurred when trying to list Scheduled Tasks", ex);
      }

      this.Cursor = Cursors.Default;
    }

    private void NewScheduledTask()
    {
      _opsData.CurrentScheduledTask = new ScheduledTask();
      bool isNewTask = true;
      frmScheduledTask frm = new frmScheduledTask(_opsData, isNewTask);
      frm.ShowDialog();
      LoadScheduledTasksToGrid();
    }

    private void EditScheduledTask()
    {
      int scheduledTaskID = gvScheduledTasks.SelectedRows[0].Cells[0].Value.ToInt32();
      _opsData.CurrentScheduledTask = _scheduledTasks[scheduledTaskID];
      bool isNewTask = false;
      frmScheduledTask frm = new frmScheduledTask(_opsData, isNewTask);
      frm.ShowDialog();
      _opsData.CurrentScheduledTask = new ScheduledTask();
      LoadScheduledTasksToGrid();
    }

    private void EditMaps()
    {

    }

    private void DeleteScheduledTask()
    {
      DialogResult result = MessageBox.Show("Are you sure you want to permanently delete Scheduled Task '" +
                                            gvScheduledTasks.SelectedRows[0].Cells[1].Value.ToString() +
                                            "' from the Task Scheduling Database?", "Ops Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
      if (result == DialogResult.Yes)
      {
        int selectedTaskID = gvScheduledTasks.SelectedRows[0].Cells[0].Value.ToInt32();
        using (var repo = new TaskRepository(_opsData.TasksDbSpec))
        {
          repo.DeleteScheduledTask(selectedTaskID);
        }
        LoadScheduledTasksToGrid();
      }
    }

    private void GetTaskReport(bool singleTask)
    {
      var taskRequestSet = new TaskRequestSet(1000);
      var taskSet = new ScheduledTaskSet();

      using (var taskRepo = new TaskRepository(_opsData.TasksDbSpec))
      {
        if (singleTask)
        {
          int taskId = gvScheduledTasks.SelectedRows[0].Cells[0].Value.ToInt32();
          taskSet.Add(taskRepo.GetScheduledTask(taskId, true));
        }
        else
        {
          var tasks = taskRepo.GetTasksForScheduling(true);

          foreach (var task in tasks)
            taskSet.Add(task);
        }
      }

      DateTime toDate = DateTime.Now.ToNextDateAtMidnight();
      switch (cboScheduleInterval.Text)
      {
        case "Week":
          toDate = toDate.AddDays(7);
          break;
        case "Month":
          toDate = toDate.AddMonths(1);
          break;
      }

      var ts = toDate - DateTime.Now;
      int loadIntervalSeconds = ts.TotalSeconds.ToInt32();

      taskSet.LoadTasksToRun(loadIntervalSeconds, false, true);
      taskRequestSet.AddTaskRequests(taskSet.GetTaskRequests());

      string parmReport = taskSet.GetParametersReport();
      string taskReport = "TASKS SCHEDULED DURING THE NEXT " + cboScheduleInterval.Text.ToUpper() + g.crlf2 + taskRequestSet.TaskReport + g.crlf2;

      frmDisplay fDisplay = new frmDisplay(parmReport + taskReport);
      fDisplay.ShowDialog();
    }

    private void MigrateScheduledTask()
    {
      try
      {
        DateTime currentDate = DateTime.Now;
        int taskId = gvScheduledTasks.SelectedRows[0].Cells[0].Value.ToInt32();
        ScheduledTask scheduledTask = _scheduledTasks[taskId];
        scheduledTask.CreatedBy = g.SystemInfo.DomainAndUser;
        scheduledTask.CreatedDate = currentDate;
        scheduledTask.ModifiedBy = null;
        scheduledTask.ModifiedDate = null;
        var taskParameters = new List<TaskParameter>();
        var taskSchedules = new List<tsk.TaskSchedule>();

        using (var taskRepo = new TaskRepository(_opsData.TasksDbSpec))
        {
          var parameters = taskRepo.GetTaskParameters(taskId);
          foreach (var parm in parameters.Values)
          {
            parm.CreatedBy = g.SystemInfo.DomainAndUser;
            parm.CreatedDate = currentDate;
            parm.ModifiedBy = null;
            parm.ModifiedDate = null;
            taskParameters.Add(parm);
          }

          var schedules = taskRepo.GetTaskSchedules(taskId);
          foreach (var schedule in schedules.Values)
          {
            schedule.CreatedBy = g.SystemInfo.DomainAndUser;
            schedule.CreatedDate = currentDate;
            schedule.ModifiedBy = null;
            schedule.ModifiedDate = null;

            if (schedule.TaskScheduleId == scheduledTask.ActiveScheduleId)
              schedule.IsActive = true;
            else
              schedule.IsActive = false;

            var schedElements = taskRepo.GetScheduleElements(schedule.TaskScheduleId);
            foreach (var elem in schedElements.Values)
            {
              elem.CreatedBy = g.SystemInfo.DomainAndUser;
              elem.CreatedDate = currentDate;
              elem.ModifiedBy = null;
              elem.ModifiedDate = null;
              schedule.TaskScheduleElements.Add(elem);
            }
            taskSchedules.Add(schedule);
          }
        }

        string destinationEnv;
        if (_opsData.Environment == "Test")
          destinationEnv = "Prod";
        else
          destinationEnv = "Test";

        if (MessageBox.Show("Are you sure you want to migrate Task '" + scheduledTask.TaskName + "' to " + destinationEnv + "?",
                            "Confirm ScheduledTask Migration to " + destinationEnv, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
        {
          return;
        }

        var destinationDbSpec = g.GetDbSpec("TaskScheduling" + destinationEnv);

        using (var taskRepo = new TaskRepository(destinationDbSpec))
        {
          taskRepo.MigrateScheduledTask(scheduledTask, taskSchedules, taskParameters);
        }

        MessageBox.Show("The ScheduledTask with its schedules and parameters have been migrated to the " + destinationEnv + " environment.",
                        "ScheduledTask Migration Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred when try to migrate task." + g.crlf2 + ex.ToReport(), "ScheduledTask Migration Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void ViewProdMap()
    {
      string map = gvMaps.SelectedRows[0].Cells[0].Value.ToString();
      DirectoryInfo di = new DirectoryInfo(_prodMapPath);
      var fullPath = di.GetFiles(map + "*").First();
      string mapPath = fullPath.FullName;

      var textDisplay = new frmTextDisplay();
      string content = File.ReadAllText(mapPath);
      textDisplay.SetText(content);
      textDisplay.Text = (map + " in " + cboEnvironment.SelectedItem.ToString().ToLower());
      textDisplay.ShowDialog();
    }

    private void ViewTestMap()
    {
      string map = gvMaps.SelectedRows[0].Cells[0].Value.ToString();
      DirectoryInfo di = new DirectoryInfo(_testMapPath);
      var fullPath = di.GetFiles(map + "*").First();
      string mapPath = fullPath.FullName;

      var textDisplay = new frmTextDisplay();
      string content = File.ReadAllText(mapPath);
      textDisplay.SetText(content);
      textDisplay.Text = (map + " in " + cboEnvironment.SelectedItem.ToString().ToLower());
      textDisplay.ShowDialog();
    }

    private void CompareMapFiles()
    {
      var env = cboEnvironment.SelectedItem.ToString();
      string leftPath = String.Empty;
      string rightPath = String.Empty;
      string mapName = gvMaps.SelectedRows[0].Cells[0].Value.ToString();

      if (env == "Prod")
      {
        var mapNameWithExt = new DirectoryInfo(_prodMapPath).EnumerateFiles(mapName + ".*").FirstOrDefault();
        if (mapNameWithExt == null || mapNameWithExt.Length == 0)
          throw new Exception("The map " + mapName + " with an appropriate extension was not found in the following directory " + _prodMapPath + ".");
        leftPath = _prodMapPath + "\\" + mapNameWithExt;
        rightPath = _testMapPath + "\\" + mapNameWithExt;
      }
      else
      {
        var mapNameWithExt = new DirectoryInfo(_prodMapPath).EnumerateFiles(mapName + ".*").FirstOrDefault();
        if (mapNameWithExt == null || mapNameWithExt.Length == 0)
          throw new Exception("The map " + mapName + " with an appropriate extension was not found in the following directory " + _testMapPath + ".");
        leftPath = _testMapPath + "\\" + mapNameWithExt;
        rightPath = _prodMapPath + "\\" + mapNameWithExt;
      }

      ProcessParms processParms = new ProcessParms();
      processParms.ExecutablePath = _winMergeExePath;
      processParms.Args = new string[] {
        "/e",
        "/s",
        "/u",
        leftPath,
        rightPath
      };

      var processHelper = new ProcessHelper();
      processHelper.RunExternalProcess(processParms);
    }

    private void DeployToProd()
    {
      DialogResult result = MessageBox.Show("Do you want to proceed? Yes will copy the file to the Production map folder and overwrite the file if it exists!", "Please Confirm",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

      if (result == DialogResult.Yes)
      {
        string mapName = gvMaps.SelectedRows[0].Cells[0].Value.ToString();
        string ext = Path.GetExtension(_testMapPath + "\\" + mapName);
        if (ext.Length == 0)
        {
          var mapNameExt = new DirectoryInfo(_testMapPath).EnumerateFiles(mapName + ".*").FirstOrDefault();
          mapName = mapNameExt.ToString();
        }

        File.Copy(_testMapPath + "\\" + mapName, _prodMapPath + "\\" + mapName, true);
      }
    }

    private void CopyToTest()
    {
      DialogResult result = MessageBox.Show("Do you want to proceed? Yes will copy the file to the Test map folder and overwrite the file if it exists!", "Please Confirm",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
      if (result == DialogResult.Yes)
      {
        string mapName = gvMaps.SelectedRows[0].Cells[0].Value.ToString();
        string ext = Path.GetExtension(_prodMapPath + "\\" + mapName);
        if (ext.Length == 0)
        {
          var mapNameExt = new DirectoryInfo(_prodMapPath).EnumerateFiles(mapName + ".*").FirstOrDefault();
          mapName = mapNameExt.ToString();
        }

        File.Copy(_prodMapPath + "\\" + mapName, _testMapPath + "\\" + mapName, true);
      }
    }

    private void GetMaps()
    {
      this.Cursor = Cursors.WaitCursor;
      try
      {
        var mapPath = g.GetCI("MapPath");
        _testMapPath = mapPath + "\\Test\\Maps";
        _prodMapPath = mapPath + "\\Prod\\Maps";
        var testDbSpec = g.GetDbSpec("TaskSchedulingTest");
        var prodDbSpec = g.GetDbSpec("TaskSchedulingProd");
        var mapReconSet = new MapReconSet();

        string[] rrTestMaps = Directory.GetFiles(_testMapPath).Select(file => Path.GetFileName(file)).ToArray();
        _rrTest.Clear();

        foreach (var map in rrTestMaps)
        {
          string newMap = String.Empty;
          if (map.ToString() == "ColumnIndexMap.xml")
            continue;
          if (map.Contains(".ext"))
          {
            newMap = map.Replace(".ext", "");
            _rrTest.Add(newMap);
          }
          else if (map.Contains(".dxmap"))
          {
            newMap = map.Replace(".dxmap", "");
            _rrTest.Add(newMap);
          }
          else
            _rrTest.Add(map.ToString());

        }
        string[] rrProdMaps = Directory.GetFiles(_prodMapPath).Select(file => Path.GetFileName(file)).ToArray();
        _rrProd.Clear();

        foreach (var map in rrProdMaps)
        {
          string newMap = String.Empty;
          if (map.ToString() == "ColumnIndexMap.xml")
            continue;
          if (map.Contains(".ext"))
          {
            newMap = map.Replace(".ext", "");
            _rrProd.Add(newMap);
          }
          else if (map.Contains(".dxmap"))
          {
            newMap = map.Replace(".dxmap", "");
            _rrProd.Add(newMap);
          }
          else
            _rrProd.Add(map.ToString());

        }
        using (var taskRepo = new TaskRepository(testDbSpec))
        {
          _dbTest.Clear();
          _dbTest = taskRepo.GetMaps();
        }
        using (var taskRepo = new TaskRepository(prodDbSpec))
        {
          _dbProd.Clear();
          _dbProd = taskRepo.GetMaps();
        }

        switch (cboEnvironment.SelectedItem)
        {
          case "Test":
            mapReconSet = GetMapReconSet(_rrTest, _rrProd, _dbTest, _dbProd, "Test");
            break;

          case "Prod":
            mapReconSet = GetMapReconSet(_rrProd, _rrTest, _dbProd, _dbTest, "Prod");
            break;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get a list of maps.", ex);
      }

      this.Cursor = Cursors.Default;
    }

    private MapReconSet GetMapReconSet(List<string> currEnvList, List<string> oppEnvList, List<string> currDbList, List<string> oppDbList, string env)
    {
      string oppEnv = String.Empty;
      var mapReconSet = new MapReconSet();
      try
      {
        if (env == "Test")
        {
          oppEnv = "Prod";
          _currConfigDbSpec = g.GetDbSpec("TaskSchedulingTest");
          _oppConfigDbSpec = g.GetDbSpec("TaskSchedulingProd");
        }
        else
        {
          oppEnv = "Test";
          _currConfigDbSpec = g.GetDbSpec("TaskSchedulingProd");
          _oppConfigDbSpec = g.GetDbSpec("TaskSchedulingTest");
        }

        if (currEnvList.Count == 0 || currEnvList == null || oppEnvList.Count == 0 || oppEnvList == null)
          throw new Exception("There are no maps in the list of maps provided.");

        foreach (var map in currEnvList)
        {
          var mapRecon = new MapRecon();

          mapRecon.MapName = map;
          mapRecon.InMapsFolder = "True".ToBoolean();
          if (oppEnvList.Contains(map))
            mapRecon.InOppositeEnvMapsFolder = "True".ToBoolean();
          else
            mapRecon.InOppositeEnvMapsFolder = "False".ToBoolean();
          mapRecon.MatchStatus = MatchStatus.NotCompared;
          if (currDbList.Contains(map))
            mapRecon.MapInDatabase = "True".ToBoolean();
          else
            mapRecon.MapInDatabase = "False".ToBoolean();
          if (oppDbList.Contains(map))
            mapRecon.MapInOppositeDatabase = "True".ToBoolean();
          else
            mapRecon.MapInOppositeDatabase = "False".ToBoolean();

          using (var taskRepo = new TaskRepository(_currConfigDbSpec))
          {
            var name = taskRepo.GetTaskName(map);
            mapRecon.TaskName = name.Trim().ToString();
          }

          using (var taskRepo = new TaskRepository(_oppConfigDbSpec))
          {
            var name = taskRepo.GetTaskName(map);
            mapRecon.OppositeTaskName = name.Trim().ToString();
          }

          if (mapRecon.TaskName != "Multiple tasks use this map.")
            if (mapRecon.OppositeTaskName != "Multiple tasks use this map.")
              using (var curTaskRepo = new TaskRepository(_currConfigDbSpec))
              {
                bool b = curTaskRepo.GetIsActive(map);
                if (b)
                  mapRecon.TaskActive = "True".ToBoolean();
                else
                  mapRecon.TaskActive = "False".ToBoolean();
              }

          if (mapRecon.TaskName != "Multiple tasks use this map.")
            if (mapRecon.OppositeTaskName != "Multiple tasks use this map.")
              using (var oppTaskRepo = new TaskRepository(_oppConfigDbSpec))
              {
                bool b = oppTaskRepo.GetIsActive(map);
                if (b)
                  mapRecon.TaskActiveInOppositeEnv = "True".ToBoolean();
                else
                  mapRecon.TaskActiveInOppositeEnv = "False".ToBoolean();
              }

          mapReconSet.Add(map, mapRecon);
        }

        _maps.Rows.Clear();
        _maps.Columns.Clear();
        InitializeMapsGrid();

        foreach (var mapRecon in mapReconSet.Values)
        {
          _maps.Rows.Add(mapRecon.MapName, mapRecon.TaskName, mapRecon.InMapsFolder, mapRecon.MapInDatabase, mapRecon.TaskActive, mapRecon.OppositeTaskName, mapRecon.InOppositeEnvMapsFolder,
                         mapRecon.MapInOppositeDatabase, mapRecon.TaskActiveInOppositeEnv, mapRecon.MatchStatus);
        }

        gvMaps.DataSource = _maps;
        gvMaps.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        gvMaps.Columns[0].Width = 250;
        gvMaps.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        gvMaps.Columns[1].Width = 250;
        gvMaps.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        gvMaps.Columns[2].Width = 110;
        gvMaps.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        gvMaps.Columns[3].Width = 110;
        gvMaps.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        gvMaps.Columns[4].Width = 110;
        gvMaps.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        gvMaps.Columns[5].Width = 250;
        gvMaps.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        gvMaps.Columns[6].Width = 110;
        gvMaps.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        gvMaps.Columns[7].Width = 110;
        gvMaps.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        gvMaps.Columns[8].Width = 110;
        gvMaps.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        foreach (DataGridViewColumn col in gvMaps.Columns)
        {
          col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        lblStatus.Text = "Loaded " + mapReconSet.Count + " maps to grid.";
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurrred while attempting to populate the MapReconSet.", ex);
      }
      return mapReconSet;
    }

    private void MaintainTaskParameters()
    {
      bool isSpecificTask = false;
      _opsData.CurrentScheduledTask = null;
      frmTaskParameters frm = new frmTaskParameters(_opsData, isSpecificTask);
      frm.ShowDialog();
    }

    private void ViewRunHistory()
    {
      int taskId = gvScheduledTasks.SelectedRows[0].Cells[0].Value.ToInt32();
      _opsData.CurrentScheduledTask = _scheduledTasks[taskId];
      frmRunHistory frm = new frmRunHistory(_opsData);
      frm.ShowDialog();
    }

    private void SetActive(string action)
    {
      bool setActiveTo = action == "SetActiveOn" ? true : false;

      if (gvScheduledTasks.SelectedRows.Count != 1)
        return;

      int scheduledTaskId = gvScheduledTasks.SelectedRows[0].Cells[0].Value.ToInt32();

      var task = _scheduledTasks[scheduledTaskId];
      task.IsActive = setActiveTo;

      using (var taskRepo = new TaskRepository(_opsData.TasksDbSpec))
        taskRepo.UpdateScheduledTask(task);

      gvScheduledTasks.SelectedRows[0].Cells[8].Value = setActiveTo == true ? "Y" : "N";
    }

    private void SetDryRun(string action)
    {
      bool setDryRunTo = action == "SetDryRunOn" ? true : false;

      if (gvScheduledTasks.SelectedRows.Count != 1)
        return;

      var parameter = new TaskParameter();
      parameter.ScheduledTaskID = gvScheduledTasks.SelectedRows[0].Cells[0].Value.ToInt32();
      parameter.ParameterName = "IsDryRun";
      parameter.ParameterValue = setDryRunTo.ToString();
      parameter.DataType = "System.String";
      parameter.CreatedBy = g.SystemInfo.DomainAndUser;
      parameter.ModifiedDate = DateTime.Now;
      parameter.ModifiedBy = g.SystemInfo.DomainAndUser;
      parameter.ModifiedDate = DateTime.Now;

      using (var taskRepo = new TaskRepository(_opsData.TasksDbSpec))
      {
        if (gvScheduledTasks.SelectedRows[0].Cells[9].Value.ToString().IsBlank())
          taskRepo.InsertTaskParameter(parameter);
        else
          taskRepo.UpdateTaskParameterByNameAndSchedTaskId(parameter);
      }

      gvScheduledTasks.SelectedRows[0].Cells[9].Value = setDryRunTo == true ? "Y" : "N";
    }

    private void SetRunUntil(string action)
    {
      bool setRunUntilTo = action == "SetRunUntilOn" ? true : false;

      if (gvScheduledTasks.SelectedRows.Count != 1)
        return;

      int scheduledTaskId = gvScheduledTasks.SelectedRows[0].Cells[0].Value.ToInt32();

      var task = _scheduledTasks[scheduledTaskId];
      task.RunUntilTask = setRunUntilTo;

      using (var taskRepo = new TaskRepository(_opsData.TasksDbSpec))
        taskRepo.UpdateScheduledTask(task);

      gvScheduledTasks.SelectedRows[0].Cells[10].Value = setRunUntilTo == true ? "Y" : "N";
    }

    private void SetTaskFrequencyTo(string action)
    {
      try
      {
        int seconds = action.GetIntegerFromString();

        int scheduledTaskId = gvScheduledTasks.SelectedRows[0].Cells[0].Value.ToInt32();
        string taskName = gvScheduledTasks.SelectedRows[0].Cells[1].Value.ToString();

        using (var taskRepo = new TaskRepository(_opsData.TasksDbSpec))
        {
          if (taskRepo.SetTaskFrequencyTo(scheduledTaskId, seconds))
          {
            //MessageBox.Show("The frequency for task '" + taskName + "' has been set to " + seconds.ToString() + " seconds.",
            //                "Task Frequency Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadScheduledTasksToGrid();
          }
          else
          {
            MessageBox.Show("Unable to update the frequency.  Task may be inactive, or may have more than one schedule element.",
                            "Task Frequency Update Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to update the frequency of the task." + g.crlf2 + ex.ToReport(),
                        "Task Frequency Update - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void SetRunUntilOverride(string action)
    {
      bool setRunUntilOverrideTo = action == "SetRunUntilOverrideOn" ? true : false;

      if (gvScheduledTasks.SelectedRows.Count != 1)
        return;

      int scheduledTaskId = gvScheduledTasks.SelectedRows[0].Cells[0].Value.ToInt32();

      var task = _scheduledTasks[scheduledTaskId];
      task.RunUntilOverride = setRunUntilOverrideTo;

      using (var taskRepo = new TaskRepository(_opsData.TasksDbSpec))
        taskRepo.UpdateScheduledTask(task);

      gvScheduledTasks.SelectedRows[0].Cells[12].Value = setRunUntilOverrideTo == true ? "Y" : "N";
    }

    private void AssignToTaskGroup()
    {
      if (gvScheduledTasks.SelectedRows.Count != 1)
        return;

      int scheduledTaskId = gvScheduledTasks.SelectedRows[0].Cells[0].Value.ToInt32();
      var task = _scheduledTasks[scheduledTaskId];

      using (var fAssignTaskGroup = new frmAssignTaskGroup(task, _scheduledTaskGroups, _opsData.TasksDbSpec))
      {
        if (fAssignTaskGroup.ShowDialog() == DialogResult.OK)
        {
          var row = gvScheduledTasks.SelectedRows[0];
          row.Cells[3].Value = task.TaskGroupName;
          MessageBox.Show("Task Group Updated", "Task Group Assignment");
        }
      }
    }

    private void CopyScheduleAndParametersFrom()
    {
      if (gvScheduledTasks.SelectedRows.Count != 1)
        return;

      if (MessageBox.Show("Any schedules and parameters that exist for the ScheduledTask will be deleted before copying the values from the selected task." + g.crlf2 +
                          "Are you sure you want to continue?", "Confirm ScheduledTask Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
      {
        return;
      }

      int taskId = gvScheduledTasks.SelectedRows[0].Cells[0].Value.ToInt32();

      using (var fScheduledTaskList = new frmScheduledTaskList(taskId, _scheduledTasks, _opsData.TasksDbSpec))
      {
        if (fScheduledTaskList.ShowDialog() == DialogResult.OK)
        {
          MessageBox.Show(fScheduledTaskList.Result  + g.crlf2 + "Be sure to update any parameter values that were uniquely for the task the parameters were copied from.",
                          "Scheduled Task Items Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
          LoadScheduledTasksToGrid();
        }
      }
    }

    private void ViewScheduledRuns()
    {
      try
      {
        gvScheduledRuns.Rows.Clear();
        List<string> listOfTasks = new List<string>();
        foreach (var item in clbScheduledRuns.CheckedItems)
          listOfTasks.Add(item.ToString());

        var scheduledTaskSet = new ScheduledTaskSet();
        using (var taskRepo = new TaskRepository(_opsData.TasksDbSpec))
        {
          var tasks = taskRepo.GetTasksForScheduling(true, true);
          foreach (var task in tasks.Where(t => t.IsActive))
            scheduledTaskSet.Add(task);
        }

        DateTime intervalStart = dtpIntervalStartDate.Value.Date.Add(dtpIntervalStartTime.Value.TimeOfDay);
        if (intervalStart < DateTime.Now)
          intervalStart = DateTime.Now;
        DateTime intervalEnd = dtpIntervalEndDate.Value.Date.Add(dtpIntervalEndTime.Value.TimeOfDay);
        scheduledTaskSet.LoadTasksToRun(1, false, true);

        List<ScheduledRun> scheduledRuns = scheduledTaskSet.GetScheduledRuns();

        foreach (var scheduledRun in scheduledRuns)
        {
          ProcessorType processorType = scheduledRun.ScheduledTask.ProcessorTypeId.ToEnum(ProcessorType.NotSet);
          string processorNameAndVersion = scheduledRun.ScheduledTask.ProcessorName + "_" + scheduledRun.ScheduledTask.ProcessorVersion;

          gvScheduledRuns.Rows.Add(scheduledRun.ScheduledTask.TaskName, scheduledRun.ScheduledRunDateTime, processorType, processorNameAndVersion,
                                   scheduledRun.ScheduledRunType, scheduledRun.ScheduledTask.RunUntilTask ? "Y" : "N");
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred trying to get a list of ScheduledRuns." + g.crlf2 + ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void ManageTaskProcessing(string action)
    {
      try
      {
        if (gvScheduledTasks.SelectedRows.Count != 1)
          return;

        string serviceName = gvScheduledTasks.SelectedRows[0].Cells[16].Value.ToString().Trim();
        if (serviceName.IsBlank())
          return;

        int scheduledTaskId = gvScheduledTasks.SelectedRows[0].Cells[0].Value.ToInt32();

        var configWsSpec = GetServiceEndpointWsSpec(serviceName);
        if (configWsSpec == null)
        {
          MessageBox.Show("Configuration data for management web service for Windows service named '" + serviceName + "' is incorrect." + g.crlf2 +
                          "Could not create specification for web service endpoint.", "OpsManager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        var f = new ObjectFactory2();
        var wsTransName = "WsCommand";
        if (action == "PingWebService")
          wsTransName = "Ping";

        var wsParms = InitializeWsParms(wsTransName, configWsSpec);

        if (wsTransName == "WsCommand")
        {
          var subCommand = new WsCommand();

          string subCommandName = action;
          if (subCommandName == "PauseTaskProcessing")
            subCommandName = "PauseWinService";
          if (subCommandName == "ResumeTaskProcessing")
            subCommandName = "ResumeWinService";

          subCommand.WsCommandName = g.ToEnum<WsCommandName>(subCommandName, WsCommandName.NotSet);

          if (subCommand.WsCommandName == WsCommandName.NotSet)
          {
            MessageBox.Show("Unable to determine the action to request of the web service.  The action value is '" + subCommandName + "'.",
                            "OpsManager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }

          subCommand.Parms.Add("ScheduledTaskID", scheduledTaskId.ToString());
          wsParms.ParmSet.Add(new Parm(subCommandName, subCommand));
        }


        this.Cursor = Cursors.WaitCursor;

        IMessageFactory messageFactory = new Org.WSO.MessageFactory();
        var requestMessage = messageFactory.CreateRequestMessage(wsParms);
        var responseMessage = WsClient.InvokeServiceCall(wsParms, requestMessage);

        switch (responseMessage.TransactionStatus)
        {
          case TransactionStatus.Success:
            switch (responseMessage.TransactionName)
            {
              case "Ping":
                PingResponse pingResponse = f.Deserialize(responseMessage.TransactionBody, true) as PingResponse;
                MessageBox.Show(pingResponse.Message, "OpsManager - Web Service Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;

              case "WsCommand":
                WsCommandResponse commandResponse = f.Deserialize(responseMessage.TransactionBody, true) as WsCommandResponse;
                string commandResponseMessage = commandResponse.WsCommandSet[0].Message;
                this.Cursor = Cursors.Default;
                MessageBox.Show(commandResponseMessage, "OpsManager - Web Service Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;
            }
            break;

          case TransactionStatus.Error:
            ErrorResponse errorResponse = f.Deserialize(responseMessage.TransactionBody, true) as ErrorResponse;
            string errorResponseMessage = errorResponse.Message;
            errorResponseMessage += errorResponse.HasException ? (g.crlf + errorResponse.WsException.ToReport()) : (g.crlf + "No exception" + g.crlf);
            this.Cursor = Cursors.Default;
            MessageBox.Show("The web service called failed." + g.crlf2 + errorResponseMessage, "OpsManager - Web Service Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            break;
        }

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred trying to get a list of ScheduledRuns." + g.crlf2 + ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }


    private WsParms InitializeWsParms(string transName, ConfigWsSpec configWsSpec)
    {
      WsParms wsParms = new WsParms();
      wsParms.TransactionName = transName;
      wsParms.TransactionVersion = "1.0.0.0";
      wsParms.MessagingParticipant = MessagingParticipant.Sender;
      wsParms.ConfigWsSpec = configWsSpec;
      wsParms.TrackPerformance = false;

      wsParms.DomainName = g.SystemInfo.DomainName;
      wsParms.MachineName = g.SystemInfo.ComputerName;
      wsParms.UserName = g.SystemInfo.UserName;
      wsParms.ModuleCode = g.AppInfo.ModuleCode;
      wsParms.ModuleName = g.AppInfo.ModuleName;
      wsParms.ModuleVersion = g.AppInfo.AppVersion;
      wsParms.AppName = g.AppInfo.AppName;
      wsParms.AppVersion = g.AppInfo.AppVersion;

      wsParms.ModuleCode = 1219;
      wsParms.ModuleName = "OpsManager";
      wsParms.ModuleVersion = "1.0.0.0";
      wsParms.OrgId = 3;

      return wsParms;
    }

    private ConfigWsSpec GetServiceEndpointWsSpec(string serviceName)
    {
      string env = cboEnvironment.Text;

      if (ckUseLocalhostService.Checked)
      {
        serviceName = "WinServiceHost";
        env = "Dev";
      }

      string endPointKey = env + "_" + serviceName;

      if (!_serviceEndpoints.ContainsKey(endPointKey))
        return null;

      string endPoint = _serviceEndpoints[endPointKey];
      string[] tokens = endPoint.Split(Constants.ColonDelimiter, StringSplitOptions.RemoveEmptyEntries);
      if (tokens.Length != 2 || tokens[1].IsNotInteger())
        return null;

      var configWsSpec = new ConfigWsSpec(WebServiceBinding.BasicHttp, tokens[0], tokens[1]);
      configWsSpec.SetOriginalProperties();
      configWsSpec.SetAsVerified();


      return configWsSpec;
    }

    #endregion

    #region Notifications

    private void ListNotifyConfigsTree()
    {
      try
      {
        treeViewNotifications.Nodes.Clear();
        pnlNotificationHolder.Controls.Clear();
        var configSets = new List<NotifyConfigSet>();
        var notifyConfigs = new List<NotifyConfig>();
        var notifyGroups = new List<NotifyGroup>();
        var notifyPersons = new List<NotifyPerson>();

        bool includeFullHierarchy = true;
        using (var notifyRepo = new NotifyRepository(_opsData.NotifyDbSpec))
        {
          configSets = notifyRepo.GetNotifyConfigSets(includeFullHierarchy);
          notifyConfigs = notifyRepo.GetNotifyConfigs(includeFullHierarchy);
          notifyGroups = notifyRepo.GetNotifyGroups(includeFullHierarchy);
          notifyPersons = notifyRepo.GetNotifyPersons();
        }

        //Populate ConfigurationsInUse Node
        TreeNode configurationsInUse = treeViewNotifications.Nodes.Add("ConfigurationsInUse");
        configurationsInUse.ImageKey = configurationsInUse.SelectedImageKey = "treeroot.ico";
        configurationsInUse.Tag = new OpsCtl.PanelData(OpsCtl.NotifyType.NotSet, OpsCtl.NodeType.NotSet, null, null, null, configurationsInUse.FullPath, _opsData.NotifyDbSpec);

        foreach (var configSet in configSets)
        {
          TreeNode configSetNode = configurationsInUse.Nodes.Add(configSet.Name);
          configSetNode.ImageKey = configSetNode.SelectedImageKey = "configset.ico";
          configSetNode.Tag = new OpsCtl.PanelData(OpsCtl.NotifyType.NotifyConfigSet, OpsCtl.NodeType.Object, configSet.NotifyConfigSetId, null, null, configSetNode.FullPath, _opsData.NotifyDbSpec);
          foreach (var config in configSet)
          {
            TreeNode configNode = configSetNode.Nodes.Add(config.Key);
            configNode.ImageKey = configNode.SelectedImageKey = "config.ico";
            configNode.Tag = new OpsCtl.PanelData(OpsCtl.NotifyType.NotifyConfig, OpsCtl.NodeType.Reference, config.Value.NotifyConfigId, null,
                                                  configSet.NotifyConfigSetId, configNode.FullPath, _opsData.NotifyDbSpec);
            foreach (var nEvent in config.Value.NotifyEventSet)
            {
              TreeNode eventNode = configNode.Nodes.Add(nEvent.Key);
              eventNode.ImageKey = eventNode.SelectedImageKey = "event.ico";
              eventNode.Tag = new OpsCtl.PanelData(OpsCtl.NotifyType.NotifyEvent, OpsCtl.NodeType.Object, nEvent.Value.NotifyEventId, null, config.Value.NotifyConfigId, eventNode.FullPath, _opsData.NotifyDbSpec);

              foreach (var neg in nEvent.Value)
              {
                NotifyGroup group = config.Value.NotifyGroupSet[neg.NotifyGroupName];
                TreeNode groupNode = eventNode.Nodes.Add(group.Name);
                groupNode.ImageKey = groupNode.SelectedImageKey = "group.ico";
                int eventGroupId = config.Value.NotifyEventGroups.First(eg => eg.Value.NotifyEventID == nEvent.Value.NotifyEventId &&
                                   eg.Value.NotifyGroupID == group.NotifyGroupId).Key;
                groupNode.Tag = new OpsCtl.PanelData(OpsCtl.NotifyType.NotifyGroup, OpsCtl.NodeType.Reference, group.NotifyGroupId, eventGroupId,
                                                     nEvent.Value.NotifyEventId, groupNode.FullPath, _opsData.NotifyDbSpec);
                foreach (var person in group)
                {
                  TreeNode personNode = groupNode.Nodes.Add(person.Name);
                  personNode.ImageKey = personNode.SelectedImageKey = "person.ico";
                  personNode.Tag = new OpsCtl.PanelData(OpsCtl.NotifyType.NotifyPerson, OpsCtl.NodeType.Reference, person.NotifyPersonId, person.NotifyPersonGroupId,
                                                        group.NotifyGroupId, personNode.FullPath, _opsData.NotifyDbSpec);
                }
              }
            }
          }
        }

        //Populate AllConfigurations Node
        TreeNode allConfigurations = treeViewNotifications.Nodes.Add("AllConfigurations");
        allConfigurations.ImageKey = allConfigurations.SelectedImageKey = "treeroot.ico";
        allConfigurations.Tag = new OpsCtl.PanelData(OpsCtl.NotifyType.NotSet, OpsCtl.NodeType.NotSet, null, null, null, allConfigurations.FullPath, _opsData.NotifyDbSpec);

        //ConfigsNode
        TreeNode configsNode = allConfigurations.Nodes.Add("NotifyConfigs");
        configsNode.ImageKey = configsNode.SelectedImageKey = "config.ico";
        configsNode.Tag = new OpsCtl.PanelData(OpsCtl.NotifyType.NotSet, OpsCtl.NodeType.NotSet, null, null, null, configsNode.FullPath, _opsData.NotifyDbSpec);
        foreach (var config in notifyConfigs)
        {
          TreeNode configNode = configsNode.Nodes.Add(config.Name);
          configNode.ImageKey = configNode.SelectedImageKey = "config.ico";
          configNode.Tag = new OpsCtl.PanelData(OpsCtl.NotifyType.NotifyConfig, OpsCtl.NodeType.Object, config.NotifyConfigId, null, null, configNode.FullPath, _opsData.NotifyDbSpec);
          foreach (var nEvent in config.NotifyEventSet)
          {
            TreeNode eventNode = configNode.Nodes.Add(nEvent.Key);
            eventNode.ImageKey = eventNode.SelectedImageKey = "event.ico";
            eventNode.Tag = new OpsCtl.PanelData(OpsCtl.NotifyType.NotifyEvent, OpsCtl.NodeType.Object, nEvent.Value.NotifyEventId, null, config.NotifyConfigId, eventNode.FullPath, _opsData.NotifyDbSpec);

            foreach (var neg in nEvent.Value)
            {
              NotifyGroup group = config.NotifyGroupSet[neg.NotifyGroupName];
              TreeNode groupNode = eventNode.Nodes.Add(group.Name);
              groupNode.ImageKey = groupNode.SelectedImageKey = "group.ico";
              int eventGroupId = config.NotifyEventGroups.First(eg => eg.Value.NotifyEventID == nEvent.Value.NotifyEventId &&
                                 eg.Value.NotifyGroupID == group.NotifyGroupId).Key;
              groupNode.Tag = new OpsCtl.PanelData(OpsCtl.NotifyType.NotifyGroup, OpsCtl.NodeType.Reference, group.NotifyGroupId, eventGroupId,
                                                   nEvent.Value.NotifyEventId, groupNode.FullPath, _opsData.NotifyDbSpec);
              foreach (var person in group)
              {
                TreeNode personNode = groupNode.Nodes.Add(person.Name);
                personNode.ImageKey = personNode.SelectedImageKey = "person.ico";
                personNode.Tag = new OpsCtl.PanelData(OpsCtl.NotifyType.NotifyPerson, OpsCtl.NodeType.Reference, person.NotifyPersonId, person.NotifyPersonGroupId,
                                                      group.NotifyGroupId, personNode.FullPath, _opsData.NotifyDbSpec);
              }
            }
          }
        }

        //GroupsNode
        TreeNode groupsNode = allConfigurations.Nodes.Add("NotifyGroups");
        groupsNode.ImageKey = groupsNode.SelectedImageKey = "group.ico";
        groupsNode.Tag = new OpsCtl.PanelData(OpsCtl.NotifyType.NotSet, OpsCtl.NodeType.NotSet, null, null, null, groupsNode.FullPath, _opsData.NotifyDbSpec);
        foreach (var group in notifyGroups)
        {
          TreeNode groupNode = groupsNode.Nodes.Add(group.Name);
          groupNode.ImageKey = groupNode.SelectedImageKey = "group.ico";
          groupNode.Tag = new OpsCtl.PanelData(OpsCtl.NotifyType.NotifyGroup, OpsCtl.NodeType.Object, group.NotifyGroupId, null, null, groupNode.FullPath, _opsData.NotifyDbSpec);
          foreach (var person in group)
          {
            TreeNode personNode = groupNode.Nodes.Add(person.Name);
            personNode.ImageKey = personNode.SelectedImageKey = "person.ico";
            personNode.Tag = new OpsCtl.PanelData(OpsCtl.NotifyType.NotifyPerson, OpsCtl.NodeType.Reference, person.NotifyPersonId, person.NotifyPersonGroupId,
                                                  group.NotifyGroupId, personNode.FullPath, _opsData.NotifyDbSpec);
          }
        }

        //PersonsNode
        TreeNode personsNode = allConfigurations.Nodes.Add("NotifyPersons");
        personsNode.ImageKey = personsNode.SelectedImageKey = "person.ico";
        personsNode.Tag = new OpsCtl.PanelData(OpsCtl.NotifyType.NotSet, OpsCtl.NodeType.NotSet, null, null, null, personsNode.FullPath, _opsData.NotifyDbSpec);
        foreach (var person in notifyPersons)
        {
          TreeNode personNode = personsNode.Nodes.Add(person.Name);
          personNode.ImageKey = personNode.SelectedImageKey = "person.ico";
          personNode.Tag = new OpsCtl.PanelData(OpsCtl.NotifyType.NotifyPerson, OpsCtl.NodeType.Object, person.NotifyPersonId, null, null, personNode.FullPath, _opsData.NotifyDbSpec);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred when trying to list NotifyConfigs TreeView." + g.crlf2 + ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void GetNotifyConfigReport()
    {
      try
      {
        NotifyConfigSet notifyConfigSet = new NotifyConfigSet();
        bool includeFullHierarchy = true;

        using (var notifyRepo = new NotifyRepository(_opsData.NotifyDbSpec))
          notifyConfigSet = notifyRepo.GetNotifyConfigSet(cboNotifyConfigSets.Text, includeFullHierarchy);

        string report = notifyConfigSet.GetNotifyConfigsReport();

        frmDisplay fDisplay = new frmDisplay(report);
        fDisplay.ShowDialog();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when trying to generate Notify Config Report.", ex);
      }
    }

    private void panel_NotifyRefInsert(OpsCtl.NotifyChangeResult result)
    {
      var nodeInsertList = new List<TreeNode>();
      var newNode = new TreeNode();
      var nodeType = OpsCtl.NodeType.NotSet;

      switch (result.NotifyType)
      {
        case OpsCtl.NotifyType.NotifyConfig:
        case OpsCtl.NotifyType.NotifyGroup:
        case OpsCtl.NotifyType.NotifyPerson:
          nodeType = OpsCtl.NodeType.Reference;
          break;

        case OpsCtl.NotifyType.NotifyConfigSet:
        case OpsCtl.NotifyType.NotifyEvent:
          nodeType = OpsCtl.NodeType.Reference;
          break;
      }

      if (result.NotifyType == OpsCtl.NotifyType.NotifyConfigSet)
        nodeInsertList.Add(treeViewNotifications.Nodes[0]);
      else
      {
        nodeInsertList = GetInsertNodesRecursive(treeViewNotifications.Nodes[0], result, nodeInsertList);
        nodeInsertList = GetInsertNodesRecursive(treeViewNotifications.Nodes[1], result, nodeInsertList);
      }

      foreach (TreeNode node in nodeInsertList)
      {
        newNode = node.Nodes.Add(result.NewObjectName);
        newNode.ImageIndex = newNode.SelectedImageIndex = result.NotifyType.ToInt32();
        newNode.Tag = new OpsCtl.PanelData(result.NotifyType, nodeType, result.ObjectId, result.XRefId, result.ParentId, newNode.FullPath, _opsData.NotifyDbSpec);
      }

      TreeNode nodeToSelect = newNode;
      treeViewNotifications.SelectedNode = nodeToSelect;
    }

    private void panel_NotifyObjInsert(OpsCtl.NotifyChangeResult result)
    {
      var newNode = new TreeNode();
      switch (result.NotifyType)
      {
        case OpsCtl.NotifyType.NotifyConfig:
          newNode = treeViewNotifications.Nodes[1].Nodes[0].Nodes.Add(result.NewObjectName);
          newNode.Tag = new OpsCtl.PanelData(result.NotifyType, OpsCtl.NodeType.Object, result.ObjectId, null, null, newNode.FullPath, _opsData.NotifyDbSpec);
          break;

        case OpsCtl.NotifyType.NotifyGroup:
          newNode = treeViewNotifications.Nodes[1].Nodes[1].Nodes.Add(result.NewObjectName);
          newNode.Tag = new OpsCtl.PanelData(result.NotifyType, OpsCtl.NodeType.Object, result.ObjectId, null, null, newNode.FullPath, _opsData.NotifyDbSpec);
          break;

        case OpsCtl.NotifyType.NotifyPerson:
          newNode = treeViewNotifications.Nodes[1].Nodes[2].Nodes.Add(result.NewObjectName);
          newNode.Tag = new OpsCtl.PanelData(result.NotifyType, OpsCtl.NodeType.Object, result.ObjectId, null, null, newNode.FullPath, _opsData.NotifyDbSpec);
          break;
      }

      newNode.ImageIndex = newNode.SelectedImageIndex = result.NotifyType.ToInt32();
      treeViewNotifications.SelectedNode = newNode;
    }

    private void panel_NotifyUpdate(OpsCtl.NotifyChangeResult result)
    {
      ApplyNodeUpdateRecursive(treeViewNotifications.Nodes[0], result);
      ApplyNodeUpdateRecursive(treeViewNotifications.Nodes[1], result);
    }

    private void panel_NotifyObjDelete(OpsCtl.NotifyChangeResult result)
    {
      var nodesToDelete = new List<TreeNode>();

      nodesToDelete = GetNodesToDeleteRecursive(treeViewNotifications.Nodes[0], result, nodesToDelete);
      nodesToDelete = GetNodesToDeleteRecursive(treeViewNotifications.Nodes[1], result, nodesToDelete);

      foreach (TreeNode node in nodesToDelete)
        node.Remove();
    }

    private void panel_NotifyRefDelete(OpsCtl.NotifyChangeResult result)
    {
      var nodesToDelete = new List<TreeNode>();

      nodesToDelete = GetRefNodesToDeleteRecursive(treeViewNotifications.Nodes[0], result, nodesToDelete);
      nodesToDelete = GetRefNodesToDeleteRecursive(treeViewNotifications.Nodes[1], result, nodesToDelete);

      foreach (TreeNode node in nodesToDelete)
        node.Remove();
    }

    private List<TreeNode> GetInsertNodesRecursive(TreeNode oParentNode, OpsCtl.NotifyChangeResult result, List<TreeNode> nodeList)
    {
      foreach (TreeNode oSubNode in oParentNode.Nodes)
      {
        if (((OpsCtl.PanelData)oSubNode.Tag).NotifyType.ToInt32() == result.NotifyType.ToInt32() - 1 && ((OpsCtl.PanelData)oSubNode.Tag).ObjectId == result.ParentId)
          nodeList.Add(oSubNode);
        GetInsertNodesRecursive(oSubNode, result, nodeList);
      }
      return nodeList;
    }

    private void ApplyNodeUpdateRecursive(TreeNode oParentNode, OpsCtl.NotifyChangeResult result)
    {
      foreach (TreeNode oSubNode in oParentNode.Nodes)
      {
        if (((OpsCtl.PanelData)oSubNode.Tag).NotifyType == result.NotifyType && ((OpsCtl.PanelData)oSubNode.Tag).ObjectId == result.ObjectId)
          oSubNode.Text = result.NewObjectName;
        ApplyNodeUpdateRecursive(oSubNode, result);
      }
    }

    private List<TreeNode> GetNodesToDeleteRecursive(TreeNode oParentNode, OpsCtl.NotifyChangeResult result, List<TreeNode> nodesToDelete)
    {
      foreach (TreeNode oSubNode in oParentNode.Nodes)
      {
        OpsCtl.PanelData panelData = oSubNode.Tag as OpsCtl.PanelData;
        if (panelData.NotifyType == result.NotifyType && panelData.ObjectId == result.ObjectId)
          nodesToDelete.Add(oSubNode);
        GetNodesToDeleteRecursive(oSubNode, result, nodesToDelete);
      }
      return nodesToDelete;
    }

    private List<TreeNode> GetRefNodesToDeleteRecursive(TreeNode oParentNode, OpsCtl.NotifyChangeResult result, List<TreeNode> nodesToDelete)
    {
      foreach (TreeNode oSubNode in oParentNode.Nodes)
      {
        OpsCtl.PanelData panelData = oSubNode.Tag as OpsCtl.PanelData;
        if (panelData.NotifyType == result.NotifyType && panelData.ObjectId == result.ObjectId && panelData.ParentId == result.ParentId)
          nodesToDelete.Add(oSubNode);
        GetRefNodesToDeleteRecursive(oSubNode, result, nodesToDelete);
      }
      return nodesToDelete;
    }

    private void NewNotificationObject()
    {
      pnlNotificationHolder.Controls.Clear();

      TreeNode rootNode = treeViewNotifications.SelectedNode;
      while (rootNode.Parent != null)
        rootNode = rootNode.Parent;

      TreeNode currentNode = treeViewNotifications.SelectedNode;
      OpsCtl.PanelData currentTag = currentNode.Tag as OpsCtl.PanelData;
      OpsCtl.NotifyType newNotifyType = OpsCtl.NotifyType.NotSet;
      OpsCtl.PanelData panelData;
      var panelFactory = new OpsCtl.PanelFactory();
      OpsCtl.BasePanel panel;
      if (rootNode == treeViewNotifications.Nodes[0])
      {
        newNotifyType = (currentTag.NotifyType.ToInt32() + 1).ToEnum<OpsCtl.NotifyType>(OpsCtl.NotifyType.NotSet);
        panelData = new OpsCtl.PanelData(newNotifyType, OpsCtl.NodeType.NotSet, null, null, currentTag.ObjectId, currentNode.FullPath + "\\[NewObject]", _opsData.NotifyDbSpec);
        panel = panelFactory.CreatePanel(panelData, OpsCtl.ChangeType.InsertWithXRef);

        if (panel.NotifyType == OpsCtl.NotifyType.NotifyConfig || panel.NotifyType == OpsCtl.NotifyType.NotifyGroup || panel.NotifyType == OpsCtl.NotifyType.NotifyPerson)
          panel.NotifyInsert += panel_NotifyObjInsert;
        panel.NotifyInsert += panel_NotifyRefInsert;
      }
      else
      {

        if (currentTag.NotifyType == OpsCtl.NotifyType.NotSet)
        {
          switch (currentNode.Text)
          {
            case "NotifyConfigs":
              newNotifyType = OpsCtl.NotifyType.NotifyConfig;
              break;
            case "NotifyGroups":
              newNotifyType = OpsCtl.NotifyType.NotifyGroup;
              break;
            case "NotifyPersons":
              newNotifyType = OpsCtl.NotifyType.NotifyPerson;
              break;
          }
          panelData = new OpsCtl.PanelData(newNotifyType, OpsCtl.NodeType.NotSet, null, null, null, currentNode.FullPath + "\\[NewObject]", _opsData.NotifyDbSpec);
          panel = panelFactory.CreatePanel(panelData, OpsCtl.ChangeType.Insert);
          panel.NotifyInsert += panel_NotifyObjInsert;
        }
        else
        {
          newNotifyType = (currentTag.NotifyType.ToInt32() + 1).ToEnum<OpsCtl.NotifyType>(OpsCtl.NotifyType.NotSet);
          panelData = new OpsCtl.PanelData(newNotifyType, OpsCtl.NodeType.NotSet, null, null, currentTag.ObjectId, currentNode.FullPath + "\\[NewObject]", _opsData.NotifyDbSpec);
          panel = panelFactory.CreatePanel(panelData, OpsCtl.ChangeType.InsertWithXRef);
          panel.NotifyInsert += panel_NotifyObjInsert;
          panel.NotifyInsert += panel_NotifyRefInsert;
        }
      }

      pnlNotificationHolder.Controls.Add(panel);
    }

    private void DeleteNotificationObject()
    {
      if (pnlNotificationHolder.Controls.Count > 0)
      {
        var basePanel = pnlNotificationHolder.Controls[0] as OpsCtl.BasePanel;
        switch (basePanel.NotifyType)
        {
          case OpsCtl.NotifyType.NotifyConfigSet:
            var ncsPanel = pnlNotificationHolder.Controls[0] as OpsCtl.NotifyConfigSetPanel;
            ncsPanel.Delete();
            break;
          case OpsCtl.NotifyType.NotifyConfig:
            var ncPanel = pnlNotificationHolder.Controls[0] as OpsCtl.NotifyConfigPanel;
            ncPanel.Delete();
            break;
          case OpsCtl.NotifyType.NotifyEvent:
            var ePanel = pnlNotificationHolder.Controls[0] as OpsCtl.NotifyEventPanel;
            ePanel.Delete();
            break;
          case OpsCtl.NotifyType.NotifyGroup:
            var gPanel = pnlNotificationHolder.Controls[0] as OpsCtl.NotifyGroupPanel;
            gPanel.Delete();
            break;
          case OpsCtl.NotifyType.NotifyPerson:
            var pPanel = pnlNotificationHolder.Controls[0] as OpsCtl.NotifyPersonPanel;
            pPanel.Delete();
            break;
          default:
            return;
        }
      }
    }

    #endregion

    #region Notifications Events

    private void treeViewNotifications_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        treeViewNotifications.SelectedNode = e.Node;

        string deleteText = String.Empty;
        string newObjectName = String.Empty;

        OpsCtl.PanelData nodeTag = e.Node.Tag as OpsCtl.PanelData;
        switch (nodeTag.NotifyType)
        {
          case OpsCtl.NotifyType.NotifyConfigSet:
            newObjectName = "NotifyConfig";
            deleteText = "Delete " + e.Node.Text;
            ctxMenuNotifications.Items[0].Visible = true;
            ctxMenuNotifications.Items[1].Visible = true;
            break;

          case OpsCtl.NotifyType.NotifyConfig:
            newObjectName = "NotifyEvent";
            if (nodeTag.NodeType == OpsCtl.NodeType.Object)
              deleteText = "Delete " + e.Node.Text;
            else
              deleteText = "Remove " + e.Node.Text + " from " + e.Node.Parent.Text;
            ctxMenuNotifications.Items[0].Visible = true;
            ctxMenuNotifications.Items[1].Visible = true;
            break;

          case OpsCtl.NotifyType.NotifyEvent:
            newObjectName = "NotifyGroup";
            deleteText = "Delete " + e.Node.Text;
            ctxMenuNotifications.Items[0].Visible = true;
            ctxMenuNotifications.Items[1].Visible = true;
            break;

          case OpsCtl.NotifyType.NotifyGroup:
            newObjectName = "NotifyPerson";
            if (nodeTag.NodeType == OpsCtl.NodeType.Object)
              deleteText = "Delete " + e.Node.Text;
            else
              deleteText = "Remove " + e.Node.Text + " from " + e.Node.Parent.Text;
            ctxMenuNotifications.Items[0].Visible = true;
            ctxMenuNotifications.Items[1].Visible = true;
            break;

          case OpsCtl.NotifyType.NotifyPerson:
            if (nodeTag.NodeType == OpsCtl.NodeType.Object)
              deleteText = "Delete " + e.Node.Text;
            else
              deleteText = "Remove " + e.Node.Text + " from " + e.Node.Parent.Text;
            ctxMenuNotifications.Items[0].Visible = false;
            ctxMenuNotifications.Items[1].Visible = true;
            break;

          case OpsCtl.NotifyType.NotSet:
            switch (e.Node.Text)
            {
              case "ConfigurationsInUse":
                newObjectName = "NotifyConfigSet";
                break;
              case "NotifyConfigs":
                newObjectName = "NotifyConfig";
                break;
              case "NotifyGroups":
                newObjectName = "NotifyGroup";
                break;
              case "NotifyPersons":
                newObjectName = "NotifyPerson";
                break;
              default:
                return;
            }

            ctxMenuNotifications.Items[0].Visible = true;
            ctxMenuNotifications.Items[1].Visible = false;
            break;

          default:
            return;
        }

        ctxMenuNotifications.Items[0].Text = "Add New " + newObjectName;
        ctxMenuNotifications.Items[1].Text = deleteText;

        ctxMenuNotifications.Show(Cursor.Position);
      }
    }

    private void treeViewNotifications_BeforeSelect(object sender, TreeViewCancelEventArgs e)
    {
      if (RequestChangeSave() == DialogResult.Cancel)
        e.Cancel = true;
    }

    private void treeViewNotifications_AfterSelect(object sender, TreeViewEventArgs e)
    {
      pnlNotificationHolder.Controls.Clear();

      if (e.Node.Level == 0)
        return;

      var panelFactory = new OpsCtl.PanelFactory();
      OpsCtl.BasePanel panel = panelFactory.CreatePanel((OpsCtl.PanelData)e.Node.Tag, OpsCtl.ChangeType.Update);

      pnlNotificationHolder.Controls.Add(panel);
      panel.NotifyUpdate += panel_NotifyUpdate;
      if (panel.NodeType == OpsCtl.NodeType.Object)
        panel.NotifyDelete += panel_NotifyObjDelete;
      else
        panel.NotifyDelete += panel_NotifyRefDelete;

      treeViewNotifications.Focus();
    }

    private void treeViewNotifications_ItemDrag(object sender, ItemDragEventArgs e)
    {
      TreeNode rootNode = ((TreeNode)e.Item);
      while (rootNode.Parent != null)
        rootNode = rootNode.Parent;

      if (rootNode.Text == "AllConfigurations" && ((TreeNode)e.Item).Level == 2)
        DoDragDrop(e.Item, DragDropEffects.Copy);
    }

    private void treeViewNotifications_DragOver(object sender, DragEventArgs e)
    {
      Point targetPoint = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));

      TreeNode targetNode = ((TreeView)sender).GetNodeAt(targetPoint);
      TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
      if (targetNode == null || draggedNode == null)
        return;

      OpsCtl.PanelData targetTag = targetNode.Tag as OpsCtl.PanelData;
      OpsCtl.PanelData draggedTag = draggedNode.Tag as OpsCtl.PanelData;

      if (targetTag.NotifyType.ToInt32() + 1 == draggedTag.NotifyType.ToInt32())
        e.Effect = DragDropEffects.Copy;
      else
        e.Effect = DragDropEffects.None;
    }

    private void treeViewNotifications_DragDrop(object sender, DragEventArgs e)
    {
      Point targetPoint = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));

      TreeNode targetNode = ((TreeView)sender).GetNodeAt(targetPoint);
      OpsCtl.PanelData targetTag = targetNode.Tag as OpsCtl.PanelData;

      TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
      OpsCtl.PanelData draggedTag = draggedNode.Tag as OpsCtl.PanelData;

      foreach (TreeNode child in targetNode.Nodes)
        if (child.Text == draggedNode.Text)
        {
          MessageBox.Show(targetTag.NotifyType.ToString() + " '" + targetNode.Text + "' already contains " + draggedTag.NotifyType.ToString() + " '" + draggedNode.Text + "'.",
                          "OpsManager - Invalid Insert", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

      int? newXRefId = null;
      if (targetTag.NotifyType.ToInt32() + 1 == draggedTag.NotifyType.ToInt32())
      {
        var result = new OpsCtl.NotifyChangeResult();
        result.NotifyType = draggedTag.NotifyType;
        result.ObjectId = draggedTag.ObjectId.Value;
        result.NewObjectName = draggedNode.Text;
        result.ParentId = targetTag.ObjectId.Value;
        switch (draggedTag.NotifyType)
        {
          case OpsCtl.NotifyType.NotifyConfig:
            using (var notifyRepo = new NotifyRepository(_opsData.NotifyDbSpec))
              notifyRepo.InsertNotifyConfigXRef(targetTag.ObjectId.Value, draggedTag.ObjectId.Value);
            break;

          case OpsCtl.NotifyType.NotifyGroup:
            var neg = new NotifyEventGroup();
            neg.NotifyEventID = targetTag.ObjectId.Value;
            neg.NotifyGroupID = draggedTag.ObjectId.Value;
            neg.CreatedBy = g.SystemInfo.DomainAndUser;
            neg.CreatedOn = DateTime.Now;
            using (var notifyRepo = new NotifyRepository(_opsData.NotifyDbSpec))
              result.XRefId = newXRefId = notifyRepo.InsertNotifyEventGroup(neg);
            break;

          case OpsCtl.NotifyType.NotifyPerson:
            var npg = new NotifyPersonGroup();
            npg.NotifyGroupId = targetTag.ObjectId.Value;
            npg.NotifyPersonId = draggedTag.ObjectId.Value;
            npg.CreatedBy = g.SystemInfo.DomainAndUser;
            npg.CreatedOn = DateTime.Now;
            using (var notifyRepo = new NotifyRepository(_opsData.NotifyDbSpec))
              result.XRefId = newXRefId = notifyRepo.InsertNotifyPersonGroup(npg);
            break;

          default:
            return;
        }

        TreeNode clone = draggedNode.Clone() as TreeNode;
        string clonePath = targetNode.FullPath + "/" + clone.Text;
        clone.Tag = new OpsCtl.PanelData(draggedTag.NotifyType, OpsCtl.NodeType.Reference, draggedTag.ObjectId, newXRefId, targetTag.ObjectId, clonePath, _opsData.NotifyDbSpec);
        foreach (TreeNode child in clone.Nodes)
          ((OpsCtl.PanelData)child.Tag).TreeNodePath = clonePath + "/" + child.Text;

        var nodeInsertList = new List<TreeNode>();

        nodeInsertList = GetInsertNodesRecursive(treeViewNotifications.Nodes[0], result, nodeInsertList);
        nodeInsertList = GetInsertNodesRecursive(treeViewNotifications.Nodes[1], result, nodeInsertList);

        foreach (TreeNode node in nodeInsertList)
        {
          node.Nodes.Add(clone);
          clone = clone.Clone() as TreeNode;
        }

        treeViewNotifications.SelectedNode = targetNode.Nodes.Cast<TreeNode>().Where(n => n.Text == draggedNode.Text).FirstOrDefault();

        targetNode.Expand();
      }
    }
    #endregion

    #region WebSites / WindowsServices

    private void StartStopWindowsService(string command)
    {
      this.Cursor = Cursors.WaitCursor;
      try
      {
        WsCommand wsCommandRequest = new WsCommand();

        string winServiceName = gvWindowsServices.SelectedRows[0].Cells[0].Value.ToString();
        wsCommandRequest.Message = "WinServiceName." + winServiceName;

        switch (command)
        {
          case "StartWinService":
            wsCommandRequest.WsCommandName = WsCommandName.StartWinService;
            break;
          case "StopWinService":
            wsCommandRequest.WsCommandName = WsCommandName.StopWinService;
            break;
          case "PauseWinService":
            wsCommandRequest.WsCommandName = WsCommandName.PauseWinService;
            break;
          case "ResumeWinService":
            wsCommandRequest.WsCommandName = WsCommandName.ResumeWinService;
            break;
          default:
            this.Cursor = Cursors.Default;
            return;
        }

        WsMessage responseMessage = SendRequestMessage(wsCommandRequest);
        var winService = GetWsMessageResponseObject(responseMessage) as WinService;

        if (winService == null)
        {
          this.Cursor = Cursors.Default;
          return;
        }
        gvWindowsServices.SelectedRows[0].Cells[2].Value = winService.WinServiceStatus.ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred trying to start/stop a windows service." + g.crlf2 + ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      this.Cursor = Cursors.Default;
    }

    private void StartStopWebSite(string command)
    {
      this.Cursor = Cursors.WaitCursor;
      try
      {
        WsCommand wsCommandRequest = new WsCommand();

        string siteName = gvWebSites.SelectedRows[0].Cells[0].Value.ToString();
        wsCommandRequest.Message = "WebSiteName." + siteName;

        switch (command)
        {
          case "StartWebSite":
            wsCommandRequest.WsCommandName = WsCommandName.StartWebSite;
            break;
          case "StopWebSite":
            wsCommandRequest.WsCommandName = WsCommandName.StopWebSite;
            break;
          default:
            this.Cursor = Cursors.Default;
            return;
        }

        WsMessage responseMessage = SendRequestMessage(wsCommandRequest);
        var webSite = GetWsMessageResponseObject(responseMessage) as WebSite;

        if (webSite == null)
        {
          this.Cursor = Cursors.Default;
          return;
        }

        gvWebSites.SelectedRows[0].Cells[2].Value = webSite.WebSiteStatus.ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred trying to start/stop a web site." + g.crlf2 + ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      this.Cursor = Cursors.Default;
    }

    private void StartStopAppPool(string command)
    {
      this.Cursor = Cursors.WaitCursor;
      try
      {
        WsCommand wsCommandRequest = new WsCommand();

        string appPoolName = gvAppPools.SelectedRows[0].Cells[0].Value.ToString();
        wsCommandRequest.Message = "AppPoolName." + appPoolName;

        switch (command)
        {
          case "StartAppPool":
            wsCommandRequest.WsCommandName = WsCommandName.StartAppPool;
            break;
          case "StopAppPool":
            wsCommandRequest.WsCommandName = WsCommandName.StopAppPool;
            break;
          default:
            return;
        }

        WsMessage responseMessage = SendRequestMessage(wsCommandRequest);
        var appPool = GetWsMessageResponseObject(responseMessage) as AppPool;

        if (appPool == null)
        {
          this.Cursor = Cursors.Default;
          return;
        }

        gvAppPools.SelectedRows[0].Cells[3].Value = appPool.AppPoolStatus.ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred trying to start/stop an app pool." + g.crlf2 + ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      this.Cursor = Cursors.Default;
    }

    public void GetWinServices()
    {
      this.Cursor = Cursors.WaitCursor;
      try
      {
        gvWindowsServices.Rows.Clear();
        gvWindowsServices.Refresh();

        WsCommand wsCommandRequest = new WsCommand();
        wsCommandRequest.WsCommandName = WsCommandName.GetWinServices;

        WsMessage responseMessage = SendRequestMessage(wsCommandRequest);

        var winServices = GetWsMessageResponseObject(responseMessage) as WinServiceSet;

        if (winServices == null)
        {
          this.Cursor = Cursors.Default;
          return;
        }

        foreach (var service in winServices)
        {
          if (_windowsServices != null && _windowsServices.Count > 0)
          {
            if (_windowsServices.Where(s => s.ToLower() == service.Name.ToLower()).Count() == 0)
              continue;
          }

          int rowId = gvWindowsServices.Rows.Add();
          DataGridViewRow row = gvWindowsServices.Rows[rowId];

          row.Cells["ServiceName"].Value = service.Name;
          row.Cells["MachineName"].Value = service.MachineName;
          row.Cells["ServiceStatus"].Value = service.WinServiceStatus;
          row.Cells["CanPauseAndContinue"].Value = service.CanPauseAndContinue;
          row.Cells["CanStop"].Value = service.CanStop;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred trying to list windows services." + g.crlf2 + ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      this.Cursor = Cursors.Default;
    }

    public void GetWebSites()
    {
      this.Cursor = Cursors.WaitCursor;
      try
      {
        gvWebSites.Rows.Clear();
        gvWebSites.Refresh();

        WsCommand wsCommandRequest = new WsCommand();
        wsCommandRequest.WsCommandName = WsCommandName.GetWebSites;

        WsMessage responseMessage = SendRequestMessage(wsCommandRequest);

        var webSites = GetWsMessageResponseObject(responseMessage) as WebSiteSet;

        if (webSites == null)
        {
          this.Cursor = Cursors.Default;
          return;
        }

        foreach (var site in webSites)
        {
          int rowId = gvWebSites.Rows.Add();
          DataGridViewRow row = gvWebSites.Rows[rowId];

          row.Cells["WebSiteName"].Value = site.Name;
          row.Cells["WebSiteId"].Value = site.Id;
          row.Cells["WebSiteStatus"].Value = site.WebSiteStatus;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred trying to list web sites." + g.crlf2 + ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      this.Cursor = Cursors.Default;
    }

    public void GetAppPools()
    {
      this.Cursor = Cursors.WaitCursor;
      try
      {
        gvAppPools.Rows.Clear();
        gvAppPools.Refresh();

        WsCommand wsCommandRequest = new WsCommand();
        wsCommandRequest.WsCommandName = WsCommandName.GetAppPools;

        WsMessage responseMessage = SendRequestMessage(wsCommandRequest);

        var appPools = GetWsMessageResponseObject(responseMessage) as AppPoolSet;

        if (appPools == null)
        {
          this.Cursor = Cursors.Default;
          return;
        }

        foreach (var appPool in appPools)
        {
          int rowId = gvAppPools.Rows.Add();
          DataGridViewRow row = gvAppPools.Rows[rowId];

          row.Cells["AppPoolName"].Value = appPool.Name;
          row.Cells["AutoStart"].Value = appPool.AutoStart;
          row.Cells["Enable32BitAppOnWin64"].Value = appPool.Enable32BitAppOnWin64;
          row.Cells["AppPoolStatus"].Value = appPool.AppPoolStatus;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred trying to list web sites." + g.crlf2 + ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      this.Cursor = Cursors.Default;
    }

    public WsMessage SendRequestMessage(WsCommand wsCommand)
    {
      WsParms wsParms = new WsParms();
      wsParms.TransactionName = "WsCommand";
      wsParms.TransactionVersion = "1.0.0.0";
      wsParms.MessagingParticipant = MessagingParticipant.Sender;
      wsParms.ConfigWsSpec = _opsData.OpsMgmt01WsSpec;
      wsParms.TrackPerformance = false;

      wsParms.DomainName = g.SystemInfo.DomainName;
      wsParms.MachineName = g.SystemInfo.ComputerName;
      wsParms.UserName = g.SystemInfo.UserName;
      wsParms.ModuleCode = g.AppInfo.ModuleCode;
      wsParms.ModuleName = g.AppInfo.ModuleName;
      wsParms.ModuleVersion = g.AppInfo.AppVersion;
      wsParms.AppName = g.AppInfo.AppName;
      wsParms.AppVersion = g.AppInfo.AppVersion;

      wsParms.ModuleCode = 1219;
      wsParms.ModuleName = "OpsManager";
      wsParms.ModuleVersion = "1.0.0.0";
      wsParms.OrgId = 3;

      Parm parm = new Parm("WsCommand", wsCommand);
      parm.ParameterType = typeof(WsCommand);
      wsParms.ParmSet.Add(parm);

      WsMessage requestMessage = new WSO.MessageFactory().CreateRequestMessage(wsParms);

      return WsClient.InvokeServiceCall(wsParms, requestMessage);
    }

    private object GetWsMessageResponseObject(WsMessage responseMessage)
    {
      try
      {
        ObjectFactory2 f = new ObjectFactory2();

        switch (responseMessage.TransactionHeader.TransactionName)
        {
          case "ErrorResponse":
            ErrorResponse errorResponse = f.Deserialize(responseMessage.TransactionBody) as ErrorResponse;
            string errorResponseMessage = errorResponse.Message;
            errorResponseMessage += errorResponse.HasException ? (g.crlf + errorResponse.WsException.ToReport()) : (g.crlf + "No exception" + g.crlf);
            MessageBox.Show(errorResponseMessage, "WsCommand - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;

          case "WsCommand":
            WsCommandResponse commandResponse = f.Deserialize(responseMessage.TransactionBody) as WsCommandResponse;
            if (commandResponse.WsCommandSet.Count == 0)
            {
              MessageBox.Show("There was no WsCommand in the WsCommandResponse after getting a list of of Windows Services.", "WsCommand - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              return null;
            }

            var wsCommand = commandResponse.WsCommandSet.First();
            if (wsCommand.TaskResultStatus == TaskResultStatus.Failed)
            {
              MessageBox.Show(wsCommand.Message, "WsCommand - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              return null;
            }

            if (wsCommand.ObjectWrapper == null && wsCommand.ObjectWrapper.Object.IsBlank() && wsCommand.ObjectWrapper.AssemblyQualifiedName.IsBlank())
            {
              MessageBox.Show("The WsCommand Response had an invalid ObjectWrapper and was expecting a L", "WsCommand - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              return null;
            }

            return wsCommand.ObjectWrapper.GetObject();

          default:
            return null;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while trying to get the object from the WsMessage Response.", ex);
      }
    }

    private void ctxMenuWinServices_Opening(object sender, CancelEventArgs e)
    {
      ContextMenuStrip ctxMenu = sender as ContextMenuStrip;
      if (ctxMenu != null)
      {
        var mousePos = ctxMenu.PointToClient(Control.MousePosition);
        if (ctxMenu.ClientRectangle.Contains(mousePos))
        {
          var gvMousePos = gvWindowsServices.PointToClient(Control.MousePosition);
          var hit = gvWindowsServices.HitTest(gvMousePos.X, gvMousePos.Y);
          if (hit.RowIndex == -1)
          {
            e.Cancel = true;
            return;
          }

          gvWindowsServices.ClearSelection();
          gvWindowsServices.Rows[hit.RowIndex].Selected = true;

          string wStatus = gvWindowsServices.SelectedRows[0].Cells[2].Value.ToString();
          bool CanPause = gvWindowsServices.SelectedRows[0].Cells[3].Value.ToBoolean();
          bool CanStop = gvWindowsServices.SelectedRows[0].Cells[4].Value.ToBoolean();

          ctxMenuWinServicesStart.Enabled = wStatus == "Stopped";
          ctxMenuWinServicesStop.Enabled = wStatus == "Running" || wStatus == "Paused" && CanStop.ToString() == "True";
          pauseToolStripMenuItem.Enabled = CanPause.ToString() == "True" && wStatus.ToString() != "Paused";
          resumeToolStripMenuItem.Enabled = wStatus == "Paused";
        }
        else e.Cancel = true;
      }
    }

    private void ctxWebSites_Opening(object sender, CancelEventArgs e)
    {
      ContextMenuStrip ctxMenu = sender as ContextMenuStrip;
      if (ctxMenu != null)
      {
        var mousePos = ctxMenu.PointToClient(Control.MousePosition);
        if (ctxMenu.ClientRectangle.Contains(mousePos))
        {
          var gvMousePos = gvWebSites.PointToClient(Control.MousePosition);
          var hit = gvWebSites.HitTest(gvMousePos.X, gvMousePos.Y);
          if (hit.RowIndex == -1)
          {
            e.Cancel = true;
            return;
          }

          gvWebSites.ClearSelection();
          gvWebSites.Rows[hit.RowIndex].Selected = true;

          if (gvWebSites.SelectedRows[0].Cells[0].Value.ToString() == "OpsMgmt01")
          {
            ctxMenuWebSitesStart.Enabled = false;
            ctxMenuWebSitesStop.Enabled = false;
            return;
          }

          string status = gvWebSites.SelectedRows[0].Cells[2].Value.ToString();
          ctxMenuWebSitesStart.Enabled = status == "Stopped";
          ctxMenuWebSitesStop.Enabled = status == "Started";
        }
        else e.Cancel = true;
      }
    }

    private void ctxAppPools_Opening(object sender, CancelEventArgs e)
    {
      ContextMenuStrip ctxMenu = sender as ContextMenuStrip;
      if (ctxMenu != null)
      {
        var mousePos = ctxMenu.PointToClient(Control.MousePosition);
        if (ctxMenu.ClientRectangle.Contains(mousePos))
        {
          var gvMousePos = gvAppPools.PointToClient(Control.MousePosition);
          var hit = gvAppPools.HitTest(gvMousePos.X, gvMousePos.Y);
          if (hit.RowIndex == -1)
          {
            e.Cancel = true;
            return;
          }

          gvAppPools.ClearSelection();
          gvAppPools.Rows[hit.RowIndex].Selected = true;

          if (gvAppPools.SelectedRows[0].Cells[0].Value.ToString() == "OpsMgmt01")
          {
            ctxMenu.Items[0].Enabled = false;
            ctxMenu.Items[1].Enabled = false;
            return;
          }

          string status = gvAppPools.SelectedRows[0].Cells[3].Value.ToString();
          ctxMenu.Items[0].Enabled = status == "Stopped";
          ctxMenu.Items[1].Enabled = status == "Started";
        }
        else e.Cancel = true;
      }
    }

    #endregion

    #region Logging

    public void RefreshLog()
    {
      try
      {
        _appLogSet = new AppLogSet();

        bool descending = chkDescending.Checked;
        string recordCount = cboRecordCount.Text.IsBlank() ? "9999" : cboRecordCount.Text;
        string severity = cboSeverityCode.Text;
        string message = txtLogMessage.Text;
        string modules = cboLogModules.Text.ToCommaDelimitedString();
        string events = cboLogEvents.Text.ToCommaDelimitedString();
        string entities = cboLogEntities.Text.ToCommaDelimitedString();

        if (modules == "ERROR")
        {
          MessageBox.Show("Invalid modules filter entry.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
          cboLogModules.SelectionStart = 0;
          cboLogModules.SelectionLength = cboLogModules.Text.Length;
          return;
        }
        if (events == "ERROR")
        {
          MessageBox.Show("Invalid events filter entry.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
          cboLogEvents.SelectionStart = 0;
          cboLogEvents.SelectionLength = cboLogEvents.Text.Length;
          return;
        }
        if (entities == "ERROR")
        {
          MessageBox.Show("Invalid entities filter entry.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
          cboLogEntities.SelectionStart = 0;
          cboLogEntities.SelectionLength = cboLogEntities.Text.Length;
          return;
        }

        DateTime? fromDt = ckUseDateRange.Checked ? dtpLogFromDate.Value : (DateTime?) null;
        DateTime? toDt = ckUseDateRange.Checked ? dtpLogToDate.Value : (DateTime?) null;

        using (var logRepo = new LoggingRepository(_opsData.LoggingDbSpec))
        {
          _appLogSet = logRepo.GetAppLogSet(recordCount, fromDt, toDt, severity, message, modules, events, entities, descending);
        }

        gvLogging.Rows.Clear();
        foreach (var al in _appLogSet)
        {
          string entityDesc = al.EntityId.HasValue ? _opsData.AppLogEntities[al.EntityId.Value] + " (" + al.EntityId + ")" : "";
          string moduleDesc = al.ModuleId.HasValue ? _opsData.AppLogModules[al.ModuleId.Value] + " (" + al.ModuleId + ")" : "";
          string client = (al.ClientHost.IsNotBlank() || al.ClientIp.IsNotBlank() || al.ClientUser.IsNotBlank() ||
                           al.ClientApplication.IsNotBlank() || al.ClientApplicationVersion.IsNotBlank() || al.TransactionName.IsNotBlank()) ?
                          "Y" : "";
          string detail = al.AppLogDetailSet.Count > 0 ? "Y" : "";
          gvLogging.Rows.Add(al.LogDateTime, al.SeverityCode, al.Message, moduleDesc, al.EventCode, entityDesc, client, detail, al.LogId);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when trying to Refresh logs", ex);
      }
    }

    public void UpdateLogDetails()
    {
      try
      {
        txtLogDetails.Text = "";

        if (gvLogging.SelectedRows.Count != 1)
          return;

        StringBuilder sb = new StringBuilder();
        int logId = gvLogging.SelectedRows[0].Cells[8].Value.ToInt32();
        string logMessage = gvLogging.SelectedRows[0].Cells[2].Value.ToString();

        //Append Event description
        int? eventCode = _appLogSet.First(al => al.LogId == logId).EventCode;
        if (eventCode.HasValue)
        {
          string eventDesc = _opsData.AppLogEvents[eventCode.Value];
          sb.Append("Event:" + g.crlf + eventDesc + g.crlf2);
        }

        AppLog appLog = _appLogSet.First(al => al.LogId == logId);
        if (gvLogging.SelectedRows[0].Cells[6].Value.ToString() == "Y")
        {
          sb.Append("Client Data:" + g.crlf +
                    "  Host:             " + appLog.ClientHost + g.crlf +
                    "  IP:               " + appLog.ClientIp + g.crlf +
                    "  User:             " + appLog.ClientUser + g.crlf +
                    "  Application:      " + appLog.ClientApplication + g.crlf +
                    "  App Version:      " + appLog.ClientApplicationVersion + g.crlf +
                    "  Transaction Name: " + appLog.TransactionName + g.crlf2);
        }

        AppLogDetailSet logDetailSet = appLog.AppLogDetailSet;
        if (logDetailSet.Count() == 0)
        {
          sb.Append("Message:" + g.crlf + logMessage);
          txtLogDetails.Text = sb.ToString();
          return;
        }

        //Find Overflow SetID and list all other IDs
        List<int> setIds = new List<int>();
        int? overflowSetId = null;
        foreach (var logDetail in logDetailSet)
        {
          if (logDetail.LogDetailType == LogDetailType.MSGX)
          {
            overflowSetId = logDetail.SetId;
            continue;
          }
          if (!setIds.Contains(logDetail.SetId))
            setIds.Add(logDetail.SetId);
        }

        //Append message and message overflows
        sb.Append("Message:" + g.crlf + logMessage);
        if (overflowSetId.HasValue)
        {
          var overflowLogDetails = logDetailSet.Where(ld => ld.SetId == overflowSetId).OrderBy(ld => ld.LogDetailId).ToList();
          foreach (var logDetail in overflowLogDetails)
            sb.Append(logDetail.LogDetail);
        }
        sb.Append(g.crlf2);

        //Append all other LogDetail records
        LogDetailType logDetailType = LogDetailType.NotSet;
        foreach (var setId in setIds)
        {
          var logDetailsWithSetId = logDetailSet.Where(ld => ld.SetId == setId).OrderBy(ld => ld.LogDetailId).ToList();
          logDetailType = logDetailsWithSetId[0].LogDetailType;

          sb.Append(logDetailType + ":" + g.crlf);

          foreach (var logDetail in logDetailsWithSetId)
            sb.Append(logDetail.LogDetail);

          sb.Append(g.crlf2);

        }
        txtLogDetails.Text = sb.ToString();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when trying to update Log Detail Label.", ex);
      }
    }

    private void logFilters_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
        e.Handled = true;

      string text = (sender as ComboBox).Text;

      if (e.KeyChar == ',' && text[text.Length - 1] == ',')
        e.Handled = true;
    }

    private void cboRecordCount_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
        e.Handled = true;

      string currValueString = (sender as ComboBox).Text;

      if ((currValueString + e.KeyChar).ToInt32() > 9999)
        e.Handled = true;
    }

    #endregion

    #region Identifiers

    private void UpdateIdentifiersGrid()
    {
      try
      {
        gvIdentifiers.Rows.Clear();

        var identifiers = new Dictionary<int, string>();
        using (var logRepo = new LoggingRepository(_opsData.LoggingDbSpec))
        {
          switch (cboIdentifiers.Text)
          {
            case "Modules":
              identifiers = logRepo.GetAppLogModules();
              btnNewIdentifier.Text = "New Module";
              btnNewIdentifier.Enabled = true;
              break;

            case "Events":
              identifiers = logRepo.GetAppLogEvents();
              btnNewIdentifier.Text = "New Event";
              btnNewIdentifier.Enabled = true;
              break;

            case "Entities":
              identifiers = logRepo.GetAppLogEntities();
              btnNewIdentifier.Text = "New Entity";
              btnNewIdentifier.Enabled = true;
              break;

            default:
              btnNewIdentifier.Enabled = false;
              break;
          }
        }

        foreach (var kvp in identifiers)
          gvIdentifiers.Rows.Add(kvp.Key, kvp.Value);

        ShowHideIdentifiers();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred when trying to list " + cboIdentifiers.Text + "on gvIdentifiers." + g.crlf2 + ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void InsertUpdateIdentifier(string action)
    {
      IdentifierType identifierType;

      switch (cboIdentifiers.Text)
      {
        case "Modules":
          identifierType = IdentifierType.Module;
          break;

        case "Events":
          identifierType = IdentifierType.Event;
          break;

        case "Entities":
          identifierType = IdentifierType.Entity;
          break;

        default:
          return;
      }

      bool isNewIdentifier;
      Identifier identifier;

      if (action == "InsertIdentifier")
      {
        isNewIdentifier = true;
        identifier = new Identifier(identifierType);
      }
      else
      {
        isNewIdentifier = false;
        int id = gvIdentifiers.SelectedCells[0].Value.ToInt32();
        string description = gvIdentifiers.SelectedCells[1].Value.ToString();
        identifier = new Identifier(identifierType, id, description);
      }

      frmIdentifier frm = new frmIdentifier(_opsData, isNewIdentifier, identifier);
      frm.ShowDialog();

      InitializeLogFilters();
      UpdateIdentifiersGrid();
    }

    private void MigrateIdentifier()
    {
      try
      {
        string destinationEnv;
        if (_opsData.Environment == "Test")
          destinationEnv = "Prod";
        else
          destinationEnv = "Test";

        int id = gvIdentifiers.SelectedCells[0].Value.ToInt32();
        string description = gvIdentifiers.SelectedCells[1].Value.ToString();

        var result = MessageBox.Show("Are you sure you want to migrate this identifier to " + destinationEnv + "?",
                                     "OpsManager - Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result == DialogResult.No)
          return;

        var destinationDbSpec = g.GetDbSpec("Logging" + destinationEnv);
        bool idInUse;
        string destDescription = String.Empty;
        using (var logRepo = new LoggingRepository(destinationDbSpec))
        {
          switch (cboIdentifiers.Text)
          {
            case "Modules":
              idInUse = logRepo.InsertAppLogModule(id, description);
              if (idInUse)
                destDescription = logRepo.GetAppLogModule(id);
              else return;
              break;

            case "Events":
              idInUse = logRepo.InsertAppLogEvent(id, description);
              if (idInUse)
                destDescription = logRepo.GetAppLogEvent(id);
              else return;
              break;

            case "Entities":
              idInUse = logRepo.InsertAppLogEntity(id, description);
              if (idInUse)
                destDescription = logRepo.GetAppLogEntity(id);
              else return;
              break;

            default:
              return;
          }
        }

        result = MessageBox.Show("A " + cboIdentifiers.Text.Substring(0, cboIdentifiers.Text.Length - 1) + " with ID " + id + " already exists in " + destinationEnv + " with description: " +
                                 destDescription + "." + g.crlf2 + "Are you sure you want to overwrite?", "OpsManager - Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

        if (result == DialogResult.No)
          return;

        using (var logRepo = new LoggingRepository(destinationDbSpec))
        {
          switch (cboIdentifiers.Text)
          {
            case "Modules":
              logRepo.UpdateAppLogModule(id, description);
              break;
            case "Events":
              logRepo.UpdateAppLogEvent(id, description);
              break;
            case "Entities":
              logRepo.UpdateAppLogEntity(id, description);
              break;
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred when trying to migrate identifier." + g.crlf2 + ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void ShowHideIdentifiers()
    {
      try
      {
        string searchCrit = txtIdentifierDescription.Text.ToLower();
        foreach (DataGridViewRow row in gvIdentifiers.Rows)
        {
          if (row.Cells[1].Value.ToString().ToLower().Contains(searchCrit))
            row.Visible = true;
          else
            row.Visible = false;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred when trying Show/Hide Identifiers." + g.crlf2 + ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    #endregion

    #region Global Methods

    private void UpdateConfigDbSpec()
    {
      _opsData.Environment = cboEnvironment.Text;
      _opsData.TasksDbSpec = g.GetDbSpec("TaskScheduling" + _opsData.Environment);
      _opsData.LoggingDbSpec = g.GetDbSpec("Logging" + cboEnvironment.Text);
      _opsData.NotifyDbSpec = g.GetDbSpec("Notify" + cboEnvironment.Text);
      _opsData.OpsMgmt01WsSpec = g.AppConfig.GetWsSpec("OpsMgmt01" + cboEnvironment.Text);

      if (_opsData.Environment == "Test")
      {
        ckUseLocalhostService.Enabled = true;
        ckUseLocalhostService.Checked = false;
        ctxMenuScheduledTaskMigrateTask.Text = "Migrate Task to Prod";
        ctxMenuIdentifiersMigrate.Text = "Migrate to Prod";
      }
      else
      {
        ckUseLocalhostService.Checked = false;
        ckUseLocalhostService.Enabled = false;
        ctxMenuScheduledTaskMigrateTask.Text = "Migrate Task to Test";
        ctxMenuIdentifiersMigrate.Text = "Migrate to Test";
      }

      if (!_isInitLoad)
      {
        gvScheduledTasks.Rows.Clear();
        treeViewNotifications.Nodes.Clear();
        pnlNotificationHolder.Controls.Clear();
        gvWindowsServices.Rows.Clear();
        gvWebSites.Rows.Clear();
        gvAppPools.Rows.Clear();
        gvLogging.Rows.Clear();
        if (gvIdentifiers.Rows.Count > 0)
          UpdateIdentifiersGrid();
        _appLogSet = new AppLogSet();
      }

      if (!_isFirstShowing)
        LoadScheduledTasksToGrid();

      InitializeNotifyConfigSets();
      InitializeLogFilters();
    }

    private bool IsValidDoubleLeftClick(object sender, MouseEventArgs e)
    {
      var gridView = sender as DataGridView;

      if (gridView == null)
        return false;

      if (e.Button == MouseButtons.Left)
      {
        DataGridView.HitTestInfo hit = gridView.HitTest(e.X, e.Y);
        if (hit.Type == DataGridViewHitTestType.Cell)
          return true;
      }

      return false;
    }

    private void gridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.RowIndex < 0)
        return;

      var gridView = sender as DataGridView;
      if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == MouseButtons.Right)
      {
        DataGridView.HitTestInfo hit = gridView.HitTest(e.X, e.Y);
        DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];
        if (!row.Selected)
        {
          gridView.ClearSelection();
          gridView.Rows[e.RowIndex].Selected = true;
        }
      }

      gridView.Rows[e.RowIndex].Selected = true;

    }

    private void gridContextMenu_Opening(object sender, CancelEventArgs e)
    {
      ContextMenuStrip ctxMenu = sender as ContextMenuStrip;

      if (ctxMenu.SourceControl.GetType() != typeof(DataGridView))
      {
        e.Cancel = true;
        return;
      }

      if (ctxMenu.Name == "ctxMenuMaps")
      {
        var gvMapMousePos = gvMaps.PointToClient(Control.MousePosition);
        var hitTestIndex = gvMaps.HitTest(gvMapMousePos.X, gvMapMousePos.Y);
        var mapsRowIndex = hitTestIndex.RowIndex;
        if (hitTestIndex.RowIndex == -1)
        {
          e.Cancel = true;
          return;
        }

        if (mapsRowIndex == -1)
          return;

        var mapRow = gvMaps.Rows[mapsRowIndex];
        var cells = mapRow.Cells;
        string environment = cboEnvironment.SelectedItem.ToString();

        bool inEnvFolder = cells[2].FormattedValue.ToBoolean();
        bool inOppEnvFolder = cells[6].FormattedValue.ToBoolean();
        string envTaskName = cells[1].FormattedValue.ToString();
        string oppEnvTaskName = cells[5].FormattedValue.ToString();
        bool envMultipleTasks = false;
        bool oppEnvMultipleTasks = false;
        if (envTaskName == "Multiple tasks use this map.")
          envMultipleTasks = true;
        if (oppEnvTaskName == "Multiple tasks use this map.")
          oppEnvMultipleTasks = true;
        bool compareMaps = (inEnvFolder && inOppEnvFolder);

        var ctxMenuItems = ctxMenu.Items;
        ctxMenuItems[0].Visible = false;
        ctxMenuItems[1].Visible = false;
        ctxMenuItems[2].Visible = false;
        ctxMenuItems[3].Visible = false;
        ctxMenuItems[4].Visible = false;

        if (environment == "Prod")
        {
          if (inEnvFolder)
          {
            ctxMenuItems[0].Visible = true;
            ctxMenuItems[4].Visible = true;
          }

          if (inOppEnvFolder)
            ctxMenuItems[1].Visible = true;
          ctxMenuItems[4].Visible = true;
        }
        else
        {
          if (inEnvFolder)
          {
            ctxMenuItems[1].Visible = true;
            if (!envMultipleTasks && !oppEnvMultipleTasks && !inOppEnvFolder)
              ctxMenuItems[3].Visible = true;
          }

          if (inOppEnvFolder)
          {
            ctxMenuItems[0].Visible = true;
            ctxMenuItems[3].Visible = true;
          }

        }
        if (compareMaps)
          ctxMenuItems[2].Visible = true;


      }

      DataGridView gridView = ctxMenu.SourceControl as DataGridView;

      int colIndex = -1;
      int rowIndex = -1;

      if (ctxMenu != null)
      {
        var mousePos = ctxMenu.PointToClient(Control.MousePosition);
        if (ctxMenu.ClientRectangle.Contains(mousePos))
        {
          var gvMousePos = gridView.PointToClient(Control.MousePosition);
          var hitTestIndex = gridView.HitTest(gvMousePos.X, gvMousePos.Y);
          colIndex = hitTestIndex.ColumnIndex;
          rowIndex = hitTestIndex.RowIndex;
          if (hitTestIndex.RowIndex == -1)
          {
            e.Cancel = true;
            return;
          }
        }
        else
        {
          e.Cancel = true;
          return;
        }
      }

      if (colIndex == -1 || rowIndex == -1)
        return;

      var col = gridView.Columns[colIndex];
      var row = gridView.Rows[rowIndex];

      string cellValue = row.Cells[colIndex].Value != null ? row.Cells[colIndex].Value.ToString() : String.Empty;

      switch (col.Name)
      {
        case "IsDryRun":
          if (cellValue == "Y")
          {
            ctxMenuScheduledTaskSetDryRunOn.Visible = false;
            ctxMenuScheduledTaskSetDryRunOff.Visible = true;
          }
          else
          {
            ctxMenuScheduledTaskSetDryRunOn.Visible = true;
            ctxMenuScheduledTaskSetDryRunOff.Visible = false;
          }

          ctxMenuScheduledTaskSetFrequency.Visible = false;
          ctxMenuScheduledTaskDelete.Visible = false;
          ctxMenuScheduledTaskDisplayTaskReport.Visible = false;
          ctxMenuScheduledTaskMigrateTask.Visible = false;
          ctxMenuScheduledTaskViewRunHistory.Visible = false;
          ctxMenuScheduledTaskSetActiveOff.Visible = false;
          ctxMenuScheduledTaskSetActiveOn.Visible = false;
          ctxMenuScheduledTaskSetRunUntilOverrideOn.Visible = false;
          ctxMenuScheduledTaskSetRunUntilOverrideOff.Visible = false;
          ctxMenuScheduledTaskRunNow.Visible = false;
          ctxMenuScheduledTaskShowServiceTaskReport.Visible = false;
          ctxMenuScheduledTaskRemoveFromQueue.Visible = false;
          ctxMenuScheduledTaskRemoveFromAssignment.Visible = false;
          ctxMenuScheduledTaskRefreshTaskList.Visible = false;
          ctxMenuScheduledTaskRefreshTaskRequests.Visible = false;
          ctxMenuScheduledTaskPingWebService.Visible = false;
          ctxMenuScheduledTaskPauseTaskProcessing.Visible = false;
          ctxMenuScheduledTaskResumeTaskProcessing.Visible = false;
          break;

        case "Freq":
          ctxMenuScheduledTaskSetFrequency.Visible = true;

          ctxMenuScheduledTaskSetDryRunOn.Visible = false;
          ctxMenuScheduledTaskSetDryRunOff.Visible = false;
          ctxMenuScheduledTaskDelete.Visible = false;
          ctxMenuScheduledTaskDisplayTaskReport.Visible = false;
          ctxMenuScheduledTaskMigrateTask.Visible = false;
          ctxMenuScheduledTaskViewRunHistory.Visible = false;
          ctxMenuScheduledTaskSetActiveOff.Visible = false;
          ctxMenuScheduledTaskSetActiveOn.Visible = false;
          ctxMenuScheduledTaskSetRunUntilOverrideOn.Visible = false;
          ctxMenuScheduledTaskSetRunUntilOverrideOff.Visible = false;
          ctxMenuScheduledTaskRunNow.Visible = false;
          ctxMenuScheduledTaskShowServiceTaskReport.Visible = false;
          ctxMenuScheduledTaskRemoveFromQueue.Visible = false;
          ctxMenuScheduledTaskRemoveFromAssignment.Visible = false;
          ctxMenuScheduledTaskRefreshTaskList.Visible = false;
          ctxMenuScheduledTaskRefreshTaskRequests.Visible = false;
          ctxMenuScheduledTaskPingWebService.Visible = false;
          ctxMenuScheduledTaskPauseTaskProcessing.Visible = false;
          ctxMenuScheduledTaskResumeTaskProcessing.Visible = false;
          break;

        case "IsActive":
          if (cellValue == "Y")
          {
            ctxMenuScheduledTaskSetActiveOn.Visible = false;
            ctxMenuScheduledTaskSetActiveOff.Visible = true;
          }
          else
          {
            ctxMenuScheduledTaskSetActiveOn.Visible = true;
            ctxMenuScheduledTaskSetActiveOff.Visible = false;
          }

          ctxMenuScheduledTaskSetFrequency.Visible = false;
          ctxMenuScheduledTaskDelete.Visible = false;
          ctxMenuScheduledTaskDisplayTaskReport.Visible = false;
          ctxMenuScheduledTaskMigrateTask.Visible = false;
          ctxMenuScheduledTaskViewRunHistory.Visible = false;
          ctxMenuScheduledTaskSetDryRunOff.Visible = false;
          ctxMenuScheduledTaskSetDryRunOn.Visible = false;
          ctxMenuScheduledTaskSetRunUntilOverrideOn.Visible = false;
          ctxMenuScheduledTaskSetRunUntilOverrideOff.Visible = false;
          ctxMenuScheduledTaskRunNow.Visible = false;
          ctxMenuScheduledTaskShowServiceTaskReport.Visible = false;
          ctxMenuScheduledTaskRemoveFromQueue.Visible = false;
          ctxMenuScheduledTaskRemoveFromAssignment.Visible = false;
          break;

        case "RunUntilTask":
          if (cellValue == "Y")
          {
            ctxMenuScheduledTaskSetRunUntilOff.Visible = true;
            ctxMenuScheduledTaskSetRunUntilOn.Visible = false;
          }
          else if (cellValue == "N")
          {
            ctxMenuScheduledTaskSetRunUntilOff.Visible = false;
            ctxMenuScheduledTaskSetRunUntilOn.Visible = true;
          }
          else
          {
            e.Cancel = true;
            return;
          }
          break;

        case "RunUntilOverride":
          if (cellValue == "Y")
          {
            ctxMenuScheduledTaskSetRunUntilOverrideOn.Visible = false;
            ctxMenuScheduledTaskSetRunUntilOverrideOff.Visible = true;
          }
          else if (cellValue == "N")
          {
            ctxMenuScheduledTaskSetRunUntilOverrideOn.Visible = true;
            ctxMenuScheduledTaskSetRunUntilOverrideOff.Visible = false;
          }
          else
          {
            e.Cancel = true;
            return;
          }

          ctxMenuScheduledTaskSetFrequency.Visible = false;
          ctxMenuScheduledTaskDelete.Visible = false;
          ctxMenuScheduledTaskDisplayTaskReport.Visible = false;
          ctxMenuScheduledTaskMigrateTask.Visible = false;
          ctxMenuScheduledTaskViewRunHistory.Visible = false;
          ctxMenuScheduledTaskSetActiveOff.Visible = false;
          ctxMenuScheduledTaskSetActiveOn.Visible = false;
          ctxMenuScheduledTaskSetDryRunOff.Visible = false;
          ctxMenuScheduledTaskSetDryRunOn.Visible = false;
          ctxMenuScheduledTaskRunNow.Visible = false;
          ctxMenuScheduledTaskShowServiceTaskReport.Visible = false;
          ctxMenuScheduledTaskRemoveFromQueue.Visible = false;
          ctxMenuScheduledTaskRemoveFromAssignment.Visible = false;
          break;

        case "TaskAssignments":
          if (cellValue.IsBlank())
          {
            ctxMenuScheduledTaskDelete.Visible = true;
            ctxMenuScheduledTaskDisplayTaskReport.Visible = true;
            ctxMenuScheduledTaskMigrateTask.Visible = true;
            ctxMenuScheduledTaskViewRunHistory.Visible = true;
            ctxMenuScheduledTaskSetFrequency.Visible = false;
            ctxMenuScheduledTaskSetActiveOff.Visible = false;
            ctxMenuScheduledTaskSetActiveOn.Visible = false;
            ctxMenuScheduledTaskSetDryRunOff.Visible = false;
            ctxMenuScheduledTaskSetDryRunOn.Visible = false;
            ctxMenuScheduledTaskSetRunUntilOverrideOn.Visible = false;
            ctxMenuScheduledTaskSetRunUntilOverrideOff.Visible = false;
            ctxMenuScheduledTaskRunNow.Visible = false;
            ctxMenuScheduledTaskShowServiceTaskReport.Visible = false;
            ctxMenuScheduledTaskRemoveFromQueue.Visible = false;
            ctxMenuScheduledTaskRemoveFromAssignment.Visible = false;
            ctxMenuScheduledTaskRefreshTaskList.Visible = false;
            ctxMenuScheduledTaskRefreshTaskRequests.Visible = false;
            ctxMenuScheduledTaskPingWebService.Visible = false;
            ctxMenuScheduledTaskPauseTaskProcessing.Visible = false;
            ctxMenuScheduledTaskResumeTaskProcessing.Visible = false;
          }
          else
          {
            ctxMenuScheduledTaskDelete.Visible = false;
            ctxMenuScheduledTaskDisplayTaskReport.Visible = false;
            ctxMenuScheduledTaskMigrateTask.Visible = false;
            ctxMenuScheduledTaskViewRunHistory.Visible = false;
            ctxMenuScheduledTaskSetDryRunOff.Visible = false;
            ctxMenuScheduledTaskSetDryRunOn.Visible = false;
            ctxMenuScheduledTaskSetActiveOn.Visible = false;
            ctxMenuScheduledTaskSetActiveOff.Visible = false;
            ctxMenuScheduledTaskSetRunUntilOn.Visible = false;
            ctxMenuScheduledTaskSetRunUntilOff.Visible = false;
            ctxMenuScheduledTaskSetRunUntilOverrideOn.Visible = false;
            ctxMenuScheduledTaskSetRunUntilOverrideOff.Visible = false;
            ctxMenuScheduledTaskCopyScheduleAndParametersFrom.Visible = false;
            ctxMenuScheduledTaskSetFrequency.Visible = false;
            ctxMenuScheduledTaskAssignToTaskGroup.Visible = false;
            ctxMenuScheduledTaskRunNow.Visible = true;
            ctxMenuScheduledTaskShowServiceTaskReport.Visible = true;
            ctxMenuScheduledTaskRemoveFromQueue.Visible = true;
            ctxMenuScheduledTaskRemoveFromAssignment.Visible = true;
            ctxMenuScheduledTaskRefreshTaskList.Visible = true;
            ctxMenuScheduledTaskRefreshTaskRequests.Visible = true;
            ctxMenuScheduledTaskPingWebService.Visible = true;
            ctxMenuScheduledTaskPauseTaskProcessing.Visible = true;
            ctxMenuScheduledTaskResumeTaskProcessing.Visible = true;
          }

          break;

        default:
          ctxMenuScheduledTaskDelete.Visible = true;
          ctxMenuScheduledTaskDisplayTaskReport.Visible = true;
          ctxMenuScheduledTaskMigrateTask.Visible = true;
          ctxMenuScheduledTaskViewRunHistory.Visible = true;
          ctxMenuScheduledTaskSetActiveOff.Visible = false;
          ctxMenuScheduledTaskSetActiveOn.Visible = false;
          ctxMenuScheduledTaskSetDryRunOff.Visible = false;
          ctxMenuScheduledTaskSetDryRunOn.Visible = false;
          ctxMenuScheduledTaskSetRunUntilOverrideOn.Visible = false;
          ctxMenuScheduledTaskSetRunUntilOverrideOff.Visible = false;
          ctxMenuScheduledTaskRunNow.Visible = false;
          ctxMenuScheduledTaskRemoveFromQueue.Visible = false;
          ctxMenuScheduledTaskRemoveFromAssignment.Visible = false;
          ctxMenuScheduledTaskRefreshTaskList.Visible = false;
          ctxMenuScheduledTaskRefreshTaskRequests.Visible = false;
          ctxMenuScheduledTaskPingWebService.Visible = false;
          ctxMenuScheduledTaskPauseTaskProcessing.Visible = false;
          ctxMenuScheduledTaskResumeTaskProcessing.Visible = false;
          break;
      }
    }

    private DialogResult RequestChangeSave()
    {
      DialogResult result = DialogResult.No;
      if (pnlNotificationHolder.Controls.Count > 0)
      {
        OpsCtl.BasePanel panel = pnlNotificationHolder.Controls[0] as OpsCtl.BasePanel;
        if (panel.IsDirty)
        {
          result = MessageBox.Show("Changes have been made to the current window." + g.crlf + "Do you want to save your changes?",
                                   "OpsManager - Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
          if (result == DialogResult.Yes)
          {
            switch (panel.NotifyType)
            {
              case OpsCtl.NotifyType.NotifyConfigSet:
                panel = pnlNotificationHolder.Controls[0] as OpsCtl.NotifyConfigSetPanel;
                break;
              case OpsCtl.NotifyType.NotifyConfig:
                panel = pnlNotificationHolder.Controls[0] as OpsCtl.NotifyConfigPanel;
                break;
              case OpsCtl.NotifyType.NotifyEvent:
                panel = pnlNotificationHolder.Controls[0] as OpsCtl.NotifyEventPanel;
                break;
              case OpsCtl.NotifyType.NotifyGroup:
                panel = pnlNotificationHolder.Controls[0] as OpsCtl.NotifyGroupPanel;
                break;
              case OpsCtl.NotifyType.NotifyPerson:
                panel = pnlNotificationHolder.Controls[0] as OpsCtl.NotifyPersonPanel;
                break;
              default:
                return DialogResult.None;
            }
            panel.Controls.OfType<Button>().First(b => b.Name == "btnSave").PerformClick();
          }
          else if (result == DialogResult.No)
            pnlNotificationHolder.Controls.Clear();
        }
      }
      return result;
    }

    private void tabMain_Selecting(object sender, TabControlCancelEventArgs e)
    {
      if (RequestChangeSave() == DialogResult.Cancel)
        e.Cancel = true;
    }

    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (RequestChangeSave() == DialogResult.Cancel)
        e.Cancel = true;
    }

    #endregion

    #region Initialize

    private void InitializeForm()
    {
      try
      {
        a = new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialiation." + g.crlf2 +
                        ex.ToReport(), "OpsManager - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      try
      {
        ResourceManager resourceManager = new ResourceManager("Org.OpsManager.Properties.Resources", Assembly.GetExecutingAssembly());
        Image splashImage = (Bitmap)resourceManager.GetObject("SplashImage");

        var fSplash = new frmSplash("OpsManager", splashImage, "Version " + g.AppInfo.AppVersion, Constants.Copyright + " Gulfport Energy Corporation " + DateTime.Now.Year.ToString());
        fSplash.SetMessage("Program initializing...");
        fSplash.TopMost = true;
        fSplash.Show();
        Application.DoEvents();
        System.Threading.Thread.Sleep(200);

        fSplash.SetMessage("Initializing user interface...");
        fSplash.Show();
        Application.DoEvents();
        System.Threading.Thread.Sleep(500);

        cboEnvironment.LoadItems(g.GetList("Environments"), false);
        cboEnvironment.SelectItem(g.CI("InitialEnv"));

        _windowsServices = g.GetList("WindowsServices");
        _serviceEndpoints = g.GetDictionary("ServiceEndpoints");

        dtpIntervalEndTime.Value = dtpIntervalStartTime.Value.AddHours(1);

        dtpLogToDate.Value = DateTime.Now.ToEndOfTonight();
        dtpLogFromDate.Value = DateTime.Now.ToLastMidnight();

        UpdateConfigDbSpec();

        InitializeGridConfigs();
        InitializeScheduledTasksGrid();
        InitializeScheduledRunsGrid();
        InitializeWindowsServicesGrid();
        InitializeWebSiteGrid();
        InitializeAppPoolGrid();
        InitializeLoggingGrid();
        InitializeIdentifiersGrid();
        InitializeMapsGrid();

        fSplash.SetMessage("Loading scheduled task groups...");
        fSplash.Show();
        Application.DoEvents();
        System.Threading.Thread.Sleep(500);
        _scheduledTaskGroups = GetScheduledTaskGroups();

        cboTaskGroup.Items.Clear();
        cboTaskGroup.Items.Add(String.Empty);
        cboTaskGroup.Items.Add("Unassigned");
        foreach (var group in _scheduledTaskGroups.Values)
          cboTaskGroup.Items.Add(group.TaskGroupName);

        fSplash.SetMessage("Loading scheduled tasks...");
        fSplash.Show();
        Application.DoEvents();
        System.Threading.Thread.Sleep(500);
        LoadScheduledTasksToGrid();

        fSplash.SetMessage("Initialization complete...");
        fSplash.Show();
        Application.DoEvents();
        System.Threading.Thread.Sleep(200);

        fSplash.Close();
        fSplash.Dispose();
        fSplash = null;

        this.SetInitialSizeAndLocation();

        this._winMergeExePath = g.CI("WinMergeExePath");
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialiation." + g.crlf2 +
                        ex.ToReport(), "OpsManager - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void InitializeGridConfigs()
    {
      try
      {
        string gridConfigXml = File.ReadAllText(g.ImportsPath + @"\GridConfig.xml");
        ObjectFactory2 f = new ObjectFactory2();
        _opsData.GridViewSet = f.Deserialize(XElement.Parse(gridConfigXml)) as GridViewSet;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to process GridView configurations." + g.crlf2 +
                        ex.ToReport(), "OpsManager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void InitializeScheduledTasksGrid()
    {
      try
      {
        gvScheduledTasks.Columns.Clear();
        GridView view = _opsData.GridViewSet["scheduledTasks"];
        view.SetColumnWidths(gvScheduledTasks.ClientSize.Width);

        foreach (GridColumn gc in view)
        {
          DataGridViewColumn col = new DataGridViewTextBoxColumn();
          col.Name = gc.Name;
          col.HeaderText = gc.Text;
          col.Width = gc.WidthPixels;
          col.Visible = gc.Visible;

          switch (gc.Align)
          {
            case "Right":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
              break;
            case "Left":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
              break;
            case "Center":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
              break;
          }

          if (gc.Fill)
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

          gvScheduledTasks.Columns.Add(col);
        }

        cboScheduleInterval.SelectedIndex = 0;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the ScheduledTasks grid using view 'scheduledTasks' " +
                        g.crlf2 + ex.ToReport(), "OpsManager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void InitializeScheduledRunsGrid()
    {
      try
      {
        gvScheduledRuns.Columns.Clear();
        GridView view = _opsData.GridViewSet["ScheduledRuns"];
        view.SetColumnWidths(gvScheduledRuns.ClientSize.Width);

        foreach (GridColumn gc in view)
        {
          DataGridViewColumn col = new DataGridViewTextBoxColumn();
          col.Name = gc.Name;
          col.HeaderText = gc.Text;
          col.Width = gc.WidthPixels;
          col.Visible = gc.Visible;

          switch (gc.Align)
          {
            case "Right":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
              break;
            case "Left":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
              break;
            case "Center":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
              break;
          }

          if (gc.Fill)
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

          gvScheduledRuns.Columns.Add(col);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the ScheduledRuns grid using view 'ScheduledRuns' " +
                        g.crlf2 + ex.ToReport(), "OpsManager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void InitializeWindowsServicesGrid()
    {
      try
      {
        gvWindowsServices.Columns.Clear();
        GridView view = _opsData.GridViewSet["WindowsServices"];
        view.SetColumnWidths(gvWindowsServices.ClientSize.Width);

        foreach (GridColumn gc in view)
        {
          DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
          col.Name = gc.Name;
          col.HeaderText = gc.Text;
          col.Width = gc.WidthPixels;
          col.Visible = gc.Visible;

          switch (gc.Align)
          {
            case "Right":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
              break;
            case "Left":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
              break;
            case "Center":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
              break;
          }

          if (gc.Fill)
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
          gvWindowsServices.Columns.Add(col);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the WindowsServices grid using view 'WindowsServices' " +
                        g.crlf2 + ex.ToReport(), "OpsManager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void InitializeWebSiteGrid()
    {
      try
      {
        gvWebSites.Columns.Clear();
        GridView view = _opsData.GridViewSet["WebSites"];
        view.SetColumnWidths(gvWebSites.ClientSize.Width);

        foreach (GridColumn gc in view)
        {
          DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
          col.Name = gc.Name;
          col.HeaderText = gc.Text;
          col.Width = gc.WidthPixels;
          col.Visible = gc.Visible;

          switch (gc.Align)
          {
            case "Right":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
              break;
            case "Left":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
              break;
            case "Center":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
              break;
          }

          if (gc.Fill)
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
          gvWebSites.Columns.Add(col);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the WebSite grid using view 'WebSites' " +
                        g.crlf2 + ex.ToReport(), "OpsManager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void InitializeAppPoolGrid()
    {
      try
      {
        gvAppPools.Columns.Clear();
        GridView view = _opsData.GridViewSet["AppPools"];
        view.SetColumnWidths(gvAppPools.ClientSize.Width);

        foreach (GridColumn gc in view)
        {
          DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
          col.Name = gc.Name;
          col.HeaderText = gc.Text;
          col.Width = gc.WidthPixels;
          col.Visible = gc.Visible;

          switch (gc.Align)
          {
            case "Right":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
              break;
            case "Left":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
              break;
            case "Center":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
              break;
          }

          if (gc.Fill)
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
          gvAppPools.Columns.Add(col);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the WebSite grid using view 'AppPools' " +
                        g.crlf2 + ex.ToReport(), "OpsManager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void InitializeLoggingGrid()
    {
      try
      {
        gvLogging.Columns.Clear();
        GridView view = _opsData.GridViewSet["Logging"];
        view.SetColumnWidths(gvLogging.ClientSize.Width);

        foreach (GridColumn gc in view)
        {
          DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
          col.Name = gc.Name;
          col.HeaderText = gc.Text;
          col.Width = gc.WidthPixels;
          col.Visible = gc.Visible;

          switch (gc.Align)
          {
            case "Right":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
              break;
            case "Left":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
              break;
            case "Center":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
              break;
          }

          if (gc.Fill)
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
          gvLogging.Columns.Add(col);
        }

        InitializeLogFilters();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the WindowsServices grid using view 'windowsServices' " +
                        g.crlf2 + ex.ToReport(), "OpsManager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void InitializeMapsGrid()
    {
      try
      {
        string mapEnv = cboEnvironment.SelectedItem.ToString();
        string oppMapEnv = String.Empty;
        if (mapEnv == "Prod")
          oppMapEnv = "Test";
        else
          oppMapEnv = "Prod";

        _maps.Columns.Add("Map Name", typeof(String));
        _maps.Columns.Add("Task Name", typeof(String));
        _maps.Columns.Add(mapEnv + " Map Folder", typeof(bool));
        _maps.Columns.Add(mapEnv + " Database", typeof(bool));
        _maps.Columns.Add(mapEnv + " Task Active", typeof(bool));
        _maps.Columns.Add(oppMapEnv + " Task Name", typeof(String));
        _maps.Columns.Add(oppMapEnv + " Map Folder", typeof(bool));
        _maps.Columns.Add(oppMapEnv + " Database", typeof(bool));
        _maps.Columns.Add(oppMapEnv + " Task Active", typeof(bool));
        _maps.Columns.Add("Match Status", typeof(bool));
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the WindowsServices grid using view 'windowsServices' " +
                        g.crlf2 + ex.ToReport(), "OpsManager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void InitializeLogFilters()
    {
      try
      {
        using (var logRepo = new LoggingRepository(_opsData.LoggingDbSpec))
        {
          _opsData.AppLogEntities = logRepo.GetAppLogEntities();
          _opsData.AppLogModules = logRepo.GetAppLogModules();
          _opsData.AppLogEvents = logRepo.GetAppLogEvents();
        }

        cboSeverityCode.SelectedIndex = 0;
        cboLogEntities.Text = String.Empty;
        cboLogModules.Text = String.Empty;
        cboLogEvents.Text = String.Empty;

        cboLogModules.Items.Clear();
        foreach (var module in _opsData.AppLogModules)
          cboLogModules.Items.Add("(" + module.Key + ") " + module.Value);

        cboLogEvents.Items.Clear();
        foreach (var eventCode in _opsData.AppLogEvents)
          cboLogEvents.Items.Add(eventCode.Key);

        cboLogEntities.Items.Clear();
        foreach (var entity in _opsData.AppLogEntities)
          cboLogEntities.Items.Add("(" + entity.Key + ") " + entity.Value);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while initializing the log filters.", ex);
      }
    }

    private void InitializeNotifyConfigSets()
    {
      try
      {
        cboNotifyConfigSets.Items.Clear();
        using (var notifyRepo = new NotifyRepository(_opsData.NotifyDbSpec))
          _notifyConfigSets = notifyRepo.GetNotifyConfigSets();

        foreach (var notifyConfigSet in _notifyConfigSets)
          cboNotifyConfigSets.Items.Add(notifyConfigSet.Name);

        if (cboNotifyConfigSets.Items.Count > 0)
          cboNotifyConfigSets.SelectedIndex = 0;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the Notifications tab" +
                        g.crlf2 + ex.ToReport(), "OpsManager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void InitializeIdentifiersGrid()
    {
      try
      {
        gvIdentifiers.Columns.Clear();
        GridView view = _opsData.GridViewSet["Identifiers"];
        view.SetColumnWidths(gvIdentifiers.ClientSize.Width);

        foreach (GridColumn gc in view)
        {
          DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
          col.Name = gc.Name;
          col.HeaderText = gc.Text;
          col.Width = gc.WidthPixels;
          col.Visible = gc.Visible;

          switch (gc.Align)
          {
            case "Right":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
              break;
            case "Left":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
              break;
            case "Center":
              col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
              col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
              break;
          }

          if (gc.Fill)
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
          gvIdentifiers.Columns.Add(col);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the ModulesEventsEntities grid using view 'ModulesEventsEntities' " +
                        g.crlf2 + ex.ToReport(), "OpsManager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    #endregion

    private void cboTaskGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (_isFirstShowing)
        return;

      if (cboTaskGroup.Text == String.Empty)
      {
        _selectedTaskGroup = null;
        LoadScheduledTasksToGrid();
      }
      else
      {
        if (cboTaskGroup.Text == "Unassigned")
        {
          _selectedTaskGroup = -1;
          LoadScheduledTasksToGrid();
        }
        else
        {
          foreach (var kvpGroup in _scheduledTaskGroups)
          {
            if (kvpGroup.Value.TaskGroupName == cboTaskGroup.Text)
            {
              _selectedTaskGroup = kvpGroup.Key;
              LoadScheduledTasksToGrid();
            }
          }
        }
      }
    }

    private void frmMain_Shown(object sender, EventArgs e)
    {
      if (_isFirstShowing)
      {
        _isFirstShowing = false;
      }
    }

    private void ckUseDateRange_CheckedChanged(object sender, EventArgs e)
    {
      dtpLogFromDate.Enabled = ckUseDateRange.Checked;
      dtpLogToDate.Enabled = ckUseDateRange.Checked;
    }
  }

  public static class Extensions
  {
    public static string ToCommaDelimitedString(this string str)
    {
      if (str.IsBlank())
        return "";
      int leftParen = str.IndexOf("(");
      int rightParen = str.IndexOf(")");

      if ((leftParen == -1 && rightParen != -1) || (leftParen != -1 && rightParen == -1))
        return "ERROR";

      if (leftParen != -1 && rightParen != -1 && rightParen > leftParen)
        str = str.Substring(leftParen + 1, (rightParen - leftParen) - 1);


      str = Regex.Replace(str, "[^0-9,]", "");

      while (str[str.Length - 1] == ',')
        str = str.Substring(0, str.Length - 1);

      str = Regex.Replace(str, ",,", ",");

      return str;
    }
  }
}
