using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.TSK.Business.Models;
using Org.TSK.Business;
using Org.GS.Configuration;
using Org.GS.ServiceManagement;
using Org.GS;

namespace Org.SvcManager
{
  public partial class frmServiceAssignments : Form
  {
    private SortedList<int, ScheduledTask> _scheduledTasks;
    private TaskAssignmentSet _taskAssignmentSet;
    private Dictionary<int, bool> _dbAssignments;
    private Dictionary<int, bool> _gridAssignments;
    private SortedList<string, ScheduledTaskGroup> _scheduledTaskGroups;
    private ConfigDbSpec _taskDbSpec;
    private ServiceSpec _serviceSpec;

    private bool _initializationComplete = false;
    

    public frmServiceAssignments(ServiceSpec serviceSpec, ConfigDbSpec taskDbSpec)
    {
      InitializeComponent();

      _serviceSpec = serviceSpec;
      _taskDbSpec = taskDbSpec;

      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "Save":
          SaveUpdates();
          LoadScheduledTasksToGrid();
          break;

        case "Close":
          this.DialogResult = DialogResult.Cancel;
          this.Close();
          break;
      }

      this.Cursor = Cursors.Default;
    }

    private void SaveUpdates()
    {
      try
      {
        var assignedTaskIds = new List<int>();

        for (int i = 0; i < _gridAssignments.Count; i++)
        {
          int scheduledTaskId = _gridAssignments.Keys.ElementAt(i);
          if (_gridAssignments[scheduledTaskId])
          {
            assignedTaskIds.Add(scheduledTaskId); 
          }
        }

        using (var taskRepo = new TaskRepository(_taskDbSpec))
        {
          taskRepo.AssignTasksToService(_serviceSpec.TaskServiceID, assignedTaskIds);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurrred while attempting to update the task assignments for service '" + 
                        _serviceSpec.Name + "'." + g.crlf2 + ex.ToReport(), "Manage Service Assignments - Initialization Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }


    private void LoadScheduledTasksToGrid()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        int? taskGroupId = null;        

        if (cboTaskGroup.Text.IsNotBlank())
          taskGroupId = _scheduledTaskGroups[cboTaskGroup.Text].TaskGroupId;

        _scheduledTasks = new SortedList<int, ScheduledTask>();

        using (var taskRepo = new TaskRepository(_taskDbSpec))
        {
          _scheduledTasks = taskRepo.GetScheduledTasks(null);
          _taskAssignmentSet = GetTaskAssignmentSet(_serviceSpec.TaskServiceID);

          foreach (var scheduledTask in _scheduledTasks.Values)
          {
            var taskGroup = taskRepo.GetScheduledTaskGroup(scheduledTask.ScheduledTaskId);
            if (taskGroup != null)
            {
              scheduledTask.TaskGroupId = taskGroup.TaskGroupId;
              scheduledTask.TaskGroupName = taskGroup.TaskGroupName;
            }
          }      
        }

        gvTasks.Rows.Clear();

        _dbAssignments = new Dictionary<int, bool>();
        _gridAssignments = new Dictionary<int, bool>();

        foreach (var scheduledTask in _scheduledTasks.Values)
        {
          bool isAssigned = _taskAssignmentSet.IsTaskAssigned(scheduledTask.TaskName);
          _dbAssignments.Add(scheduledTask.ScheduledTaskId, isAssigned);
          _gridAssignments.Add(scheduledTask.ScheduledTaskId, isAssigned); 

          if (taskGroupId.HasValue && taskGroupId.Value != scheduledTask.TaskGroupId)
            continue;

          string processorType = scheduledTask.ProcessorTypeId.ToEnum<ProcessorType>(ProcessorType.NotSet).ToString();
          string periodContext = scheduledTask.RunUntilPeriodContextID.ToEnum<PeriodContexts>(PeriodContexts.NotSet).ToString();
          if (periodContext == "NotSet")
            periodContext = String.Empty;

          //string activeScheduleName = task.ActiveScheduleId.HasValue ? taskSchedules[st.ActiveScheduleId.ToInt32()].ScheduleName : "";

          string startTime = String.Empty;
          string endTime = String.Empty;
          string freq = String.Empty;
          string runForPeriod = String.Empty;          

          string isDryRun = String.Empty;

          //if (st.TaskSchedule != null)
          //{
          //  var taskScheduleElement = st.TaskSchedule.TaskScheduleElements.FirstOrDefault();
          //  if (taskScheduleElement != null)
          //  {
          //    startTime = taskScheduleElement.StartTime.HasValue ? taskScheduleElement.StartTime.Value.ToString(@"hh\:mm") : String.Empty;
          //    endTime = taskScheduleElement.EndTime.HasValue ? taskScheduleElement.EndTime.Value.ToString(@"hh\:mm") : String.Empty;
          //    freq = taskScheduleElement.FrequencySeconds.HasValue ? taskScheduleElement.FrequencySeconds.ToString() : String.Empty;
          //  }
          //}

          string runUntilOverride = String.Empty;
          if (scheduledTask.RunUntilTask)
          {
            if (scheduledTask.RunUntilOverride)
              runUntilOverride = "Y";
            else
              runUntilOverride = "N";
          }


          string activeScheduleName = "Active Schedule";

          gvTasks.Rows.Add(
            isAssigned,
            scheduledTask.TaskName,
            scheduledTask.ProcessorName + "_" + scheduledTask.ProcessorVersion,
            scheduledTask.TaskGroupName,
            (scheduledTask.ProcessorTypeId == 0 ? "MEFCatalog" : "StandardCatalog"),
            startTime,
            endTime,
            freq,
            scheduledTask.IsActive ? "Y" : "N",
            activeScheduleName,            
            scheduledTask.ScheduledTaskId);
        }

        lblAssignmentSummary.Text = _scheduledTasks.Count.ToString() + " total tasks listed, " + _taskAssignmentSet.Count.ToString() + " tasks assigned, no updates" +
               (cboTaskGroup.Text.IsBlank() ? String.Empty : ", list is filtered"); 
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        throw new Exception("An exception occurred when trying to list Scheduled Tasks", ex);
      }

      this.Cursor = Cursors.Default;
    }


    private SortedList<string, ScheduledTaskGroup> GetScheduledTaskGroups()
    {
      try
      {
        var taskGroupList = new SortedList<string, ScheduledTaskGroup>();

        using (var taskRepo = new TaskRepository(_taskDbSpec))
        {
          var taskGroupSet = taskRepo.GetScheduledTaskGroups();

          foreach (var taskGroup in taskGroupSet.Values)
          {
            taskGroupList.Add(taskGroup.TaskGroupName, taskGroup);
          }

          return taskGroupList;
        }
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        throw new Exception("An exception occurred when attempting to get the list of ScheduledTaskGroups.", ex);
      }
    }

    private TaskAssignmentSet GetTaskAssignmentSet(int taskServiceId)
    {
      try
      {
        using (var taskRepo = new TaskRepository(_taskDbSpec))
        {
          return taskRepo.GetTaskAssignmentsForTaskId(taskServiceId);
        }
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        throw new Exception("An exception occurred when attempting to get the TaskServiceSet.", ex);
      }
    }

    private void InitializeForm()
    {
      try
      {
        lblEnvironmentValue.Text = _serviceSpec.ServiceEnvironment.Name;
        lblHostValue.Text = _serviceSpec.ServiceHost.Name;
        lblServiceNameValue.Text = _serviceSpec.Name;
        Application.DoEvents();

        btnSave.Enabled = false;

        _scheduledTaskGroups = GetScheduledTaskGroups();
        _taskAssignmentSet = GetTaskAssignmentSet(_serviceSpec.TaskServiceID);

        cboTaskGroup.Items.Clear();
        cboTaskGroup.Items.Add(String.Empty);
        foreach (var taskGroup in _scheduledTaskGroups.Values)
        {
          cboTaskGroup.Items.Add(taskGroup.TaskGroupName);
        }
        cboTaskGroup.SelectedIndex = 0;

        InitializeGrid();
        LoadScheduledTasksToGrid();

        _initializationComplete = true;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization of the Manage Service Assignments form." + g.crlf2 + ex.ToReport(),
                        "Manage Service Assignments - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
      }
    }


    private void InitializeGrid()
    {
      gvTasks.Columns.Clear();


      DataGridViewColumn col = new DataGridViewCheckBoxColumn();
      col.Name = "Assign";
      col.HeaderText = "Assign";
      col.Width = 60;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.SortMode = DataGridViewColumnSortMode.NotSortable;
      gvTasks.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "TaskName";
      col.HeaderText = "TaskName";
      col.Width = 200;
      gvTasks.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "Processor";
      col.HeaderText = "Processor Name/Version";
      col.Width = 200;
      gvTasks.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "TaskGroup";
      col.HeaderText = "Task Group";
      col.Width = 200;
      gvTasks.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "ProcessorType";
      col.HeaderText = "Processor Type";
      col.Width = 130;
      gvTasks.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "Start";
      col.HeaderText = "Start";
      col.Width = 70;
      gvTasks.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "End";
      col.HeaderText = "End";
      col.Width = 70;
      gvTasks.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "Freq";
      col.HeaderText = "Freq";
      col.Width = 70;
      gvTasks.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "Act";
      col.HeaderText = "Act";
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.Width = 70;
      gvTasks.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "Active Schedule";
      col.HeaderText = "Active Schedule";
      col.Width = 120;
      col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      gvTasks.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "ScheduledTaskID";
      col.HeaderText = String.Empty;
      col.Width = 0;
      col.Visible = false;
      gvTasks.Columns.Add(col);
    }

    private void cboTaskGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!_initializationComplete)
        return;

      LoadScheduledTasksToGrid();
    }

    private void gvTasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex != 0 || e.RowIndex < 0)
        return;

      DataGridViewRow row = gvTasks.Rows[e.RowIndex];

      bool checkedValue = Convert.ToBoolean(row.Cells[0].Value);
      checkedValue = !checkedValue;
      row.Cells[0].Value = checkedValue;

      int scheduledTaskID = row.Cells[10].Value.ToInt32();
      _gridAssignments[scheduledTaskID] = checkedValue;

      int gridAssignmentCount = _gridAssignments.Where(a => a.Value == true).Count();

      int updateCount = CheckForUpdates();

      btnSave.Enabled = updateCount > 0;

      lblAssignmentSummary.Text = _scheduledTasks.Count.ToString() + " total tasks listed, " + gridAssignmentCount.ToString() + " tasks assigned, " + 
        (updateCount == 0 ? " no updates" : " " + updateCount.ToString()) + " updates" + 
       (cboTaskGroup.Text.IsBlank() ? String.Empty : ", list is filtered");
    }

    private int CheckForUpdates()
    {
      int updateCount = 0;

      for (int i = 0; i < _gridAssignments.Count; i++)
      {
        int scheduledTaskId = _gridAssignments.Keys.ElementAt(i);

        if (_gridAssignments[scheduledTaskId] != _dbAssignments[scheduledTaskId])
          updateCount++;
      }

      return updateCount;
    }
  }
}
