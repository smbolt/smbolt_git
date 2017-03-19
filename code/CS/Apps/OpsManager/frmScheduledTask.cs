using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;
using Org.GS.Configuration;
using Org.TSK.Business;
using Org.TSK.Business.Models;
using tsk = Org.TSK.Business.Models;

namespace Org.OpsManager
{
  public partial class frmScheduledTask : Form
  {
    private SortedList<int, tsk.TaskSchedule> _taskSchedules;
    private OpsData _opsData;
    private bool _isInitLoad = true;
    private bool _isNewTask;

    public frmScheduledTask(OpsData opsData, bool isNewTask)
    {
      InitializeComponent();
      _opsData = opsData;
      _isNewTask = isNewTask;
      InitializeTaskSchedulesGridAndFields();
      if (_isNewTask)
      {
        this.Text = "New Scheduled Task";
        btnViewTaskParameters.Visible = false;
        btnNewTaskSchedule.Visible = false;
      }
      else
      {
        this.Text = "Edit Scheduled Task";
        SetScheduledTaskData();
      }
      this.ActiveControl = txtTaskName;
      _isInitLoad = false;
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "Save":
          SaveToDb();
          break;

        case "EditTaskSchedule":
          if (IsValidDoubleLeftClick((MouseEventArgs)e))
            EditTaskSchedule();
          break;

        case "NewTaskSchedule":
          NewTaskSchedule();
          break;

        case "DeleteTaskSchedule":
          DeleteTaskSchedule();
          break;

        case "ViewTaskParameters":
          ViewTaskParameters();
          break;

        case "PropertyChange":
          CheckforChanges();
          break;

        case "RunUntilTaskChange":
          UpdateRunUntilFields();
          CheckforChanges();
          break;

        case "Cancel":
          this.DialogResult = DialogResult.OK;
          break;

        case "Exit":
          Environment.Exit(0);
          break;
      }
    }

    private void SaveToDb()
    {
      DialogResult result = DialogResult.Yes;

      if (cbRunUntilTask.Checked && cboPeriod.Text == "NULL")
      {
        MessageBox.Show("Run Until Tasks must have a Period Context.", "OpsManager - Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      if (_opsData.Environment == "Prod")
        result = MessageBox.Show("Save these changes to the ScheduledTasks table in the Task Scheduling database?",
                                            "Ops Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      if (result == DialogResult.No)
        return;

      int? userDefinedPeriodContextID = null;
      if (cboPeriod.Text != "NULL")
        userDefinedPeriodContextID = cboPeriod.Text.ToEnum<PeriodContexts>(PeriodContexts.NotSet).ToInt32();

      int? userDefinedActiveScheduleID = null;
      foreach (DataGridViewRow row in gvTaskSchedules.Rows)
        if ((bool)row.Cells[0].Value == true)
          userDefinedActiveScheduleID = row.Cells[1].Value.ToInt32();

      int? userDefinedRunUntilOffsetMintues = null;
      if (txtRunUntilOffsetMinutes.Text.IsNotBlank())
        userDefinedRunUntilOffsetMintues = txtRunUntilOffsetMinutes.Text.ToInt32();

      if (_isNewTask)
      {
         _opsData.CurrentScheduledTask.CreatedBy = g.SystemInfo.DomainAndUser;
         _opsData.CurrentScheduledTask.CreatedDate = DateTime.Now;
      }
      else
      {
         _opsData.CurrentScheduledTask.ScheduledTaskId = _opsData.CurrentScheduledTask.ScheduledTaskId;
         _opsData.CurrentScheduledTask.ModifiedBy = g.SystemInfo.DomainAndUser;
         _opsData.CurrentScheduledTask.ModifiedDate = DateTime.Now;
      }
       _opsData.CurrentScheduledTask.TaskName = txtTaskName.Text;
       _opsData.CurrentScheduledTask.ProcessorTypeId = cboProcessorType.Text.ToEnum<ProcessorType>(ProcessorType.NotSet).ToInt32();
       _opsData.CurrentScheduledTask.ProcessorName = txtProcessorName.Text.GetStringValueOrNull();
       _opsData.CurrentScheduledTask.ProcessorVersion = txtProcessorVersion.Text.GetStringValueOrNull();
       _opsData.CurrentScheduledTask.AssemblyLocation = txtAssemblyLocation.Text.GetStringValueOrNull();
       _opsData.CurrentScheduledTask.StoredProcedureName = txtStoredProcedureName.Text.GetStringValueOrNull();
       _opsData.CurrentScheduledTask.IsActive = chkIsActive.Checked;
       _opsData.CurrentScheduledTask.RunUntilTask = cbRunUntilTask.Checked;
       _opsData.CurrentScheduledTask.RunUntilPeriodContextID = userDefinedPeriodContextID;
       _opsData.CurrentScheduledTask.RunUntilOverride = cbRunUntilOverride.Checked;
       _opsData.CurrentScheduledTask.RunUntilOffsetMinutes = userDefinedRunUntilOffsetMintues;
       _opsData.CurrentScheduledTask.IsLongRunning = chkIsLongRunning.Checked;
       _opsData.CurrentScheduledTask.TrackHistory = chkTrackHistory.Checked;
       _opsData.CurrentScheduledTask.SuppressNotificationsOnSuccess = chkSuppressNotificationsOnSuccess.Checked;
       _opsData.CurrentScheduledTask.ActiveScheduleId = userDefinedActiveScheduleID;
        

      using (var repo = new TaskRepository(_opsData.TasksDbSpec))
      {
        if (_isNewTask)
          repo.AddScheduledTask( _opsData.CurrentScheduledTask);
        else
          repo.UpdateScheduledTask( _opsData.CurrentScheduledTask);
      }
      this.DialogResult = DialogResult.None;
      btnSave.Enabled = false;
    }

    private void NewTaskSchedule()
    {
      _opsData.CurrentTaskSchedule = new tsk.TaskSchedule();
      bool isNewSchedule = true;
      frmTaskSchedule frm = new frmTaskSchedule(_opsData, isNewSchedule);
      frm.ShowDialog();
      ListTaskSchedules();
      if (!_opsData.CurrentScheduledTask.ActiveScheduleId.HasValue && gvTaskSchedules.RowCount == 1)
      {
        int activeScheduleID = gvTaskSchedules.Rows[0].Cells[1].Value.ToInt32();
        _opsData.CurrentScheduledTask.ActiveScheduleId = activeScheduleID;
        using (var repo = new TaskRepository(_opsData.TasksDbSpec))
        {
          repo.UpdateActiveScheduleID(activeScheduleID, _opsData.CurrentScheduledTask.ScheduledTaskId);
        }
        ListTaskSchedules();
      }
    }

    private void EditTaskSchedule()
    {
      try
      {
        int taskScheduleID = gvTaskSchedules.SelectedRows[0].Cells[1].Value.ToInt32();
        _opsData.CurrentTaskSchedule = _taskSchedules[taskScheduleID];
        bool isNewSchedule = false;
        frmTaskSchedule frm = new frmTaskSchedule(_opsData, isNewSchedule);
        frm.ShowDialog();
        _opsData.CurrentTaskSchedule = new tsk.TaskSchedule();
        ListTaskSchedules();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to edit the selected scheduled task." + g.crlf2 + ex.ToReport(),
                        "Ops Manager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
      }
    }

    private void DeleteTaskSchedule()
    {
      int scheduleID = gvTaskSchedules.SelectedRows[0].Cells[1].Value.ToInt32();
      DialogResult result = DialogResult.Yes;

      if (_opsData.Environment == "Prod")
        result = MessageBox.Show("Are you sure you want to permanently delete Task Schedule with ID: " + scheduleID.ToString() +
                                            " from the Task Scheduling Database?", "Ops Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
      if (result == DialogResult.Yes)
      {
        using (var repo = new TaskRepository(_opsData.TasksDbSpec))
        {
          repo.DeleteTaskSchedule(scheduleID);
        }
        if (_opsData.CurrentScheduledTask.ActiveScheduleId == scheduleID)
        {
          using (var repo = new TaskRepository(_opsData.TasksDbSpec))
          {
            repo.UpdateActiveScheduleID(null, _opsData.CurrentScheduledTask.ScheduledTaskId);
          }
          _opsData.CurrentScheduledTask.ActiveScheduleId = null;
        }
        ListTaskSchedules();
      }
    }

    private void ViewTaskParameters()
    {
      bool isSpecificTask = true;
      frmTaskParameters frm = new frmTaskParameters(_opsData, isSpecificTask);
      frm.ShowDialog();
    }

    private void SetScheduledTaskData()
    {
      try
      {
        cboPeriod.Text = _opsData.CurrentScheduledTask.RunUntilPeriodContextID.ToEnum<PeriodContexts>(PeriodContexts.NotSet).ToString();
        cboProcessorType.Text = _opsData.CurrentScheduledTask.ProcessorTypeId.ToEnum<ProcessorType>(ProcessorType.NotSet).ToString();

        txtTaskName.Text = _opsData.CurrentScheduledTask.TaskName;
        txtProcessorName.Text = _opsData.CurrentScheduledTask.ProcessorName;
        txtProcessorVersion.Text = _opsData.CurrentScheduledTask.ProcessorVersion;
        txtAssemblyLocation.Text = _opsData.CurrentScheduledTask.AssemblyLocation;
        txtStoredProcedureName.Text = _opsData.CurrentScheduledTask.StoredProcedureName;
        txtRunUntilOffsetMinutes.Text = _opsData.CurrentScheduledTask.RunUntilOffsetMinutes.ToString();

        chkIsActive.Checked = _opsData.CurrentScheduledTask.IsActive;
        cbRunUntilTask.Checked = _opsData.CurrentScheduledTask.RunUntilTask;
        cbRunUntilOverride.Checked = _opsData.CurrentScheduledTask.RunUntilOverride;
        chkIsLongRunning.Checked = _opsData.CurrentScheduledTask.IsLongRunning;
        chkTrackHistory.Checked = _opsData.CurrentScheduledTask.TrackHistory;

        ListTaskSchedules();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when trying to set Scheduled Task info", ex);
      }
    }

    private void ListTaskSchedules()
    {
      try
      {
        gvTaskSchedules.Rows.Clear();
        _taskSchedules = new SortedList<int, tsk.TaskSchedule>();

        using (var taskData = new TaskRepository(_opsData.TasksDbSpec))
        {
          _taskSchedules = taskData.GetTaskSchedules(_opsData.CurrentScheduledTask.ScheduledTaskId);
        }

        bool activeSchedule = false;
        foreach (var ts in _taskSchedules.Values)
        {
          activeSchedule = false;
          if (ts.TaskScheduleId == _opsData.CurrentScheduledTask.ActiveScheduleId)
            activeSchedule = true;
          gvTaskSchedules.Rows.Add(activeSchedule, ts.TaskScheduleId, ts.ScheduleName, ts.IsActive);
        }
        lblStatus.Text = "Loaded " + _taskSchedules.Count.ToString() + " Task Schedule(s)";
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurren when trying to list Task Schedules", ex);
      }
    }

    private void CheckforChanges()
    {
      if (_isInitLoad)
        return;

      if (txtTaskName.Text.IsBlank())
      {
        btnSave.Enabled = false;
        return;
      }

      int? periodID = _opsData.CurrentScheduledTask.RunUntilPeriodContextID;
      string originalPeriod = periodID.HasValue ? periodID.ToEnum<PeriodContexts>(PeriodContexts.NotSet).ToString() : "NULL";
      int userDefinedActiveScheduleID = 0;

      foreach (DataGridViewRow row in gvTaskSchedules.Rows)
        if ((bool)row.Cells[0].Value == true)
          userDefinedActiveScheduleID = row.Cells[1].Value.ToInt32();

      var test = _opsData.CurrentScheduledTask.RunUntilOffsetMinutes.ToString();
      if (txtTaskName.Text.Trim() != _opsData.CurrentScheduledTask.TaskName ||
          txtProcessorName.Text.GetStringValueOrNull() != _opsData.CurrentScheduledTask.ProcessorName ||
          txtProcessorVersion.Text.GetStringValueOrNull() != _opsData.CurrentScheduledTask.ProcessorVersion ||
          txtAssemblyLocation.Text.GetStringValueOrNull() != _opsData.CurrentScheduledTask.AssemblyLocation ||
          txtStoredProcedureName.Text.GetStringValueOrNull() != _opsData.CurrentScheduledTask.StoredProcedureName ||
          txtRunUntilOffsetMinutes.Text.Trim() != _opsData.CurrentScheduledTask.RunUntilOffsetMinutes.ToString() ||
          chkIsActive.Checked != _opsData.CurrentScheduledTask.IsActive ||
          cbRunUntilTask.Checked != _opsData.CurrentScheduledTask.RunUntilTask ||
          cbRunUntilOverride.Checked != _opsData.CurrentScheduledTask.RunUntilOverride ||
          chkIsLongRunning.Checked != _opsData.CurrentScheduledTask.IsLongRunning ||
          chkTrackHistory.Checked != _opsData.CurrentScheduledTask.TrackHistory ||
          chkSuppressNotificationsOnSuccess.Checked != _opsData.CurrentScheduledTask.SuppressNotificationsOnSuccess ||
          cboPeriod.Text != originalPeriod ||
          cboProcessorType.Text != _opsData.CurrentScheduledTask.ProcessorTypeId.ToEnum<ProcessorType>(ProcessorType.NotSet).ToString() ||
          userDefinedActiveScheduleID != _opsData.CurrentScheduledTask.ActiveScheduleId)
        btnSave.Enabled = true;
      else btnSave.Enabled = false;
    }

    private void UpdateRunUntilFields()
    {
      if (cbRunUntilTask.Checked)
      {
        cbRunUntilOverride.Enabled = true;
        cboPeriod.Enabled = true;
        txtRunUntilOffsetMinutes.Enabled = true;
        chkTrackHistory.Checked = true;
        chkTrackHistory.Enabled = false;
      }
      else
      {
        cbRunUntilOverride.Enabled = false;
        cboPeriod.Enabled = false;
        txtRunUntilOffsetMinutes.Enabled = false;
        chkTrackHistory.Enabled = true;
      }
    }

    private bool IsValidDoubleLeftClick(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        DataGridView.HitTestInfo hit = gvTaskSchedules.HitTest(e.X, e.Y);
        if (hit.Type == DataGridViewHitTestType.Cell)
          return true;
      }

      return false;
    }

    private void gvTaskSchedules_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex != 0 || e.RowIndex < 0)
        return;

      bool isChecked = (bool)gvTaskSchedules.Rows[e.RowIndex].Cells[0].Value;

      if (isChecked)
        return;

      foreach (DataGridViewRow row in gvTaskSchedules.Rows)
      {
        if ((bool)row.Cells[0].Value == true)
          row.Cells[0].Value = false;
      }

      gvTaskSchedules.Rows[e.RowIndex].Cells[0].Value = true;

      CheckforChanges();
    }

    private void txtRunUntilOffsetMinutes_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
        e.Handled = true;

      if (e.KeyChar == '.' && ((sender as TextBox).Text.IndexOf('.') > -1))
        e.Handled = true;
    }

    private void gvTaskSchedules_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == MouseButtons.Right)
      {
        DataGridView.HitTestInfo hit = gvTaskSchedules.HitTest(e.X, e.Y);
        DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];
        if (!row.Selected)
        {
          gvTaskSchedules.ClearSelection();
          gvTaskSchedules.Rows[e.RowIndex].Selected = true;
        }
      }
    }

    private void ctxMenuScheduledTask_Opening(object sender, CancelEventArgs e)
    {
      ContextMenuStrip ctxMenu = sender as ContextMenuStrip;
      if (ctxMenu != null)
      {
        var mousePos = ctxMenu.PointToClient(Control.MousePosition);
        if (ctxMenu.ClientRectangle.Contains(mousePos))
        {
          var gvMousePos = gvTaskSchedules.PointToClient(Control.MousePosition);
          var hitTestIndex = gvTaskSchedules.HitTest(gvMousePos.X, gvMousePos.Y);
          if (hitTestIndex.RowIndex == -1)
          {
            e.Cancel = true;
          }
        }
        else e.Cancel = true;
      }
    }

    private void InitializeTaskSchedulesGridAndFields()
    {
      gvTaskSchedules.Columns.Clear();

      try
      {
        if (_isNewTask)
        {
          gvTaskSchedules.Visible = false;
          this.Size = new Size(this.Size.Width, this.Size.Height - gvTaskSchedules.Size.Height);
        }
        else
        {
          GridView view = _opsData.GridViewSet["taskSchedules"];
          view.SetColumnWidths(gvTaskSchedules.ClientSize.Width);

          DataGridViewCheckBoxColumn checkBoxCol = new DataGridViewCheckBoxColumn();
          checkBoxCol.Name = "ActiveSchedule";
          checkBoxCol.HeaderText = "Active Schedule";
          checkBoxCol.Width = 100;
          checkBoxCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
          checkBoxCol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
          checkBoxCol.ReadOnly = false;

          gvTaskSchedules.Columns.Add(checkBoxCol);

          foreach (GridColumn gc in view)
          {
            DataGridViewColumn col = new DataGridViewTextBoxColumn();
            col.Name = gc.Name;
            col.HeaderText = gc.Text;

            col.Width = gc.WidthPixels;

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
            gvTaskSchedules.Columns.Add(col);
          }
        }

        //Fill ComboBoxes
        foreach (PeriodContexts period in Enum.GetValues(typeof(PeriodContexts)))
          if (period != PeriodContexts.NotSet)
            cboPeriod.Items.Add(period.ToString());
        cboPeriod.SelectedIndex = 0;

        foreach (ProcessorType processorType in Enum.GetValues(typeof(ProcessorType)))
          if (processorType != ProcessorType.NotSet)
            cboProcessorType.Items.Add(processorType);
        cboProcessorType.SelectedIndex = 0;

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the Database grid using view 'taskSchedules'." + g.crlf2 +
                        "Exception:" + g.crlf + ex.ToReport(), "Ops Manager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
