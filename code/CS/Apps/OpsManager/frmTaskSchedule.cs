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
  public partial class frmTaskSchedule : Form
  {
    SortedList<int, tsk.TaskScheduleElement> _scheduleElements;
    OpsData _opsData;
    bool _isNewSchedule;
    bool _isInitLoad;

    public frmTaskSchedule(OpsData opsData, bool isNewSchedule)
    {
      InitializeComponent();
      _opsData = opsData;
      _isNewSchedule = isNewSchedule;
      InitializeElementsGrid();

      if (_isNewSchedule)
      {
        this.Text = "New Task Schedule";
        btnNewScheduleElement.Visible = false;
      }
      else
      {
        this.Text = "Edit Task Schedule";
        SetTaskScheduleData();
      }
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

        case "EditScheduleElement":
          if (IsValidDoubleLeftClick((MouseEventArgs)e))
            EditScheduleElement();
          break;

        case "NewScheduleElement":
          NewScheduleElement();
          break;

        case "DeleteScheduleElement":
          DeleteScheduleElement();
          break;

        case "PropertyChange":
          CheckForChanges();
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

      if (_opsData.Environment == "Prod")
        result = MessageBox.Show("Apply these changes to the TaskSchedules table in the Task Scheduling database?",
                                            "Ops Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      if (result == DialogResult.Yes)
      {
        if (_isNewSchedule)
        {
          _opsData.CurrentTaskSchedule.CreatedBy = g.SystemInfo.DomainAndUser;
          _opsData.CurrentTaskSchedule.CreatedDate = DateTime.Now;
        }
        else
        {
          
          _opsData.CurrentTaskSchedule.TaskScheduleId = _opsData.CurrentTaskSchedule.TaskScheduleId;
          _opsData.CurrentTaskSchedule.ModifiedBy = g.SystemInfo.DomainAndUser;
          _opsData.CurrentTaskSchedule.ModifiedDate = DateTime.Now;
        }
        _opsData.CurrentTaskSchedule.ScheduledTaskId = _opsData.CurrentScheduledTask.ScheduledTaskId;
        _opsData.CurrentTaskSchedule.ScheduleName = txtScheduleName.Text;
        _opsData.CurrentTaskSchedule.IsActive = cbIsActive.Checked;

        using (var repo = new TaskRepository(_opsData.TasksDbSpec))
        {
          if (_isNewSchedule)
            repo.AddTaskSchedule(_opsData.CurrentTaskSchedule);
          else
            repo.UpdateTaskSchedule(_opsData.CurrentTaskSchedule);
        }
        this.DialogResult = DialogResult.None;
        btnSave.Enabled = false;
      }
    }

    private void NewScheduleElement()
    {
      _opsData.CurrentScheduleElement = new tsk.TaskScheduleElement();
      bool isNewElement = true;
      frmScheduleElement frm = new frmScheduleElement(_opsData, isNewElement);
      frm.ShowDialog();
      ListScheduleElements();
    }

    private void EditScheduleElement()
    {
      int scheduleElementID = gvScheduleElements.SelectedRows[0].Cells[0].Value.ToInt32();
      _opsData.CurrentScheduleElement = _scheduleElements[scheduleElementID];
      bool isNewElement = false;
      frmScheduleElement frm = new frmScheduleElement(_opsData, isNewElement);
      frm.ShowDialog();
      _opsData.CurrentScheduleElement  = new tsk.TaskScheduleElement();
      ListScheduleElements();
    }

    private void DeleteScheduleElement()
    {
      int elementID = gvScheduleElements.SelectedRows[0].Cells[0].Value.ToInt32();
      DialogResult result = DialogResult.Yes;

      if (_opsData.Environment == "Prod")
        result = MessageBox.Show("Are you sure you want to permanently delete Schedule Element with ID:" + elementID.ToString() +
                                 ") from the Task Scheduling Database?", "Ops Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
      if (result == DialogResult.Yes)
      {
        using (var repo = new TaskRepository(_opsData.TasksDbSpec))
        {
          repo.DeleteTaskScheduleElement(elementID);
        }
        ListScheduleElements();
      }
    }

    private void SetTaskScheduleData()
    {
      try
      {
        txtScheduleName.Text = _opsData.CurrentTaskSchedule.ScheduleName;
        cbIsActive.Checked = _opsData.CurrentTaskSchedule.IsActive;

        ListScheduleElements();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when trying to set Task Schedule info", ex);
      }
    }

    private void ListScheduleElements()
    {
      try
      {
        gvScheduleElements.Rows.Clear();

        _scheduleElements = new SortedList<int, tsk.TaskScheduleElement>();

        using (var taskData = new TaskRepository(_opsData.TasksDbSpec))
        {
          _scheduleElements = taskData.GetScheduleElements(_opsData.CurrentTaskSchedule.TaskScheduleId);
        }

        foreach (var se in _scheduleElements.Values)
        {
          string executionType = se.TaskExecutionType.ToString();
          string intervalType = se.IntervalType == IntervalType.NotSet ? "" : se.IntervalType.ToString();
          gvScheduleElements.Rows.Add(se.TaskScheduleElementId, se.IsActive, executionType, se.FrequencySeconds, intervalType,
                                      se.StartDate.HasValue ? se.StartDate.Value.Date.ToString("MM/dd/yyyy") : se.StartDate.ToString(), se.StartTime,
                                      se.EndDate.HasValue ? se.EndDate.Value.Date.ToString("MM/dd/yyyy") : se.EndDate.ToString(), se.EndTime);
        }
        lblStatus.Text = "Loaded " + _scheduleElements.Count.ToString() + " Schedule Element(s)";
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurren when trying to list Schedule Elements", ex);
      }
    }

    private void CheckForChanges()
    {
      if (_isInitLoad)
        return;

      if ((txtScheduleName.Text.Trim() != _opsData.CurrentTaskSchedule.ScheduleName 
              && txtScheduleName.Text.IsNotBlank())
          ||
          cbIsActive.Checked != _opsData.CurrentTaskSchedule.IsActive)
        btnSave.Enabled = true;
      else btnSave.Enabled = false;
    }

    private bool IsValidDoubleLeftClick(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        DataGridView.HitTestInfo hit = gvScheduleElements.HitTest(e.X, e.Y);
        if (hit.Type == DataGridViewHitTestType.Cell)
          return true;
      }

      return false;
    }

    private void gvScheduleElements_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == MouseButtons.Right)
      {
        DataGridView.HitTestInfo hit = gvScheduleElements.HitTest(e.X, e.Y);
        DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];
        if (!row.Selected)
        {
          gvScheduleElements.ClearSelection();
          gvScheduleElements.Rows[e.RowIndex].Selected = true;
        }
      }
    }

    private void ctxMenuScheduleElements_Opening(object sender, CancelEventArgs e)
    {
      ContextMenuStrip ctxMenu = sender as ContextMenuStrip;
      if (ctxMenu != null)
      {
        var mousePos = ctxMenu.PointToClient(Control.MousePosition);
        if (ctxMenu.ClientRectangle.Contains(mousePos))
        {
          var gvMousePos = gvScheduleElements.PointToClient(Control.MousePosition);
          var hitTestIndex = gvScheduleElements.HitTest(gvMousePos.X, gvMousePos.Y);
          if (hitTestIndex.RowIndex == -1)
          {
            e.Cancel = true;
          }
        }
        else e.Cancel = true;
      }
    }

    private void InitializeElementsGrid()
    {
      gvScheduleElements.ClearSelection();

      try
      {
        if (_isNewSchedule)
        {
          gvScheduleElements.Visible = false;
          this.Size = new Size(325, this.Size.Height - 140);
          cbIsActive.Location = new Point(cbIsActive.Location.X - 470, cbIsActive.Location.Y);
        }
        else
        {
          GridView view = _opsData.GridViewSet["scheduleElements"];
          view.SetColumnWidths(gvScheduleElements.ClientSize.Width);

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
            gvScheduleElements.Columns.Add(col);
          }
          gvScheduleElements.Columns[0].Visible = false;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the Database grid using view 'scheduleElements'." + g.crlf2 +
                        "Exception:" + g.crlf + ex.ToReport(), "Ops Manager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
