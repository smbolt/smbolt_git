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
using Microsoft.VisualBasic;

namespace Org.OpsManager
{
  public partial class frmTaskParameters : Form
  {
    OpsData _opsData;
    SortedList<int, TaskParameter> _taskParameters;
    SortedList<string, TaskParameterSet> _parameterSets;
    bool _isSpecificTask;
    bool _clearingParameterSets = false;

    public frmTaskParameters(OpsData opsData, bool isSpecificTask)
    {
      InitializeComponent();
      _opsData = opsData;
      _isSpecificTask = isSpecificTask;
      if (_isSpecificTask)
      {
        this.Text = "Task Parameters for " + _opsData.CurrentScheduledTask.TaskName;
        btnNewParameterSet.Visible = true;
        gvParameterSets.DoubleClick -= Action;
      }
      else
      {
        this.Text = "Maintain Task Parameters";
        btnNewTaskParameter.Text = "New Parameter Variable";
      }
      InitializeTaskParametersGrid();
      InitializeParameterSetsGrid();
      InitializeParametersInSetGrid();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "EditTaskParameter":
          if (IsValidDoubleLeftClick((MouseEventArgs)e))
            EditTaskParameter();
          break;

        case "EditParameterSet":
          if (IsValidDoubleLeftClick((MouseEventArgs)e))
            EditParameterSet();
          break;

        case "NewTaskParameter":
          NewTaskParameter();
          break;

        case "NewParameterSet":
          NewParameterSet();
          break;

        case "AddSetToTaskParameters":
          AddSetToTaskParameters();
          break;

        case "DeleteTaskParameter":
          DeleteParameter();
          ListTaskParameters();
          break;

        case "MigrateVariable":
          MigrateVariable();
          break;

        case "MigrateParameterSet":
          MigrateParameterSet();
          break;

        case "Cancel":
          this.DialogResult = DialogResult.OK;
          break;

        case "Exit":
          Environment.Exit(0);
          break;
      }
    }

    private void EditTaskParameter()
    {
      int parameterID = gvTaskParameters.SelectedRows[0].Cells[0].Value.ToInt32();
      _opsData.CurrentTaskParameter = _taskParameters[parameterID];
      bool isNewParameter = false;
      frmEditTaskParameter frm = new frmEditTaskParameter(_opsData, isNewParameter);
      frm.ShowDialog();
      _opsData.CurrentTaskParameter = new TaskParameter();
      ListTaskParameters();
    }

    private void NewTaskParameter()
    {
      _opsData.CurrentTaskParameter = new TaskParameter();
      bool isNewParameter = true;
      frmEditTaskParameter frm = new frmEditTaskParameter(_opsData, isNewParameter);
      frm.ShowDialog();
      ListTaskParameters();
    }

    private void AddSetToTaskParameters()
    {
      bool isNewParameter = true;
      string setName = gvParameterSets.SelectedRows[0].Cells[0].Value.ToString();
      _opsData.CurrentTaskParameter.ParameterValue = "ParmSet=" + setName;
      frmEditTaskParameter frm = new frmEditTaskParameter(_opsData, isNewParameter);
      frm.ShowDialog();
      _opsData.CurrentTaskParameter = new TaskParameter();
      ListTaskParameters();
    }

    private void NewParameterSet()
    {
      _opsData.CurrentTaskParameterSet = new TaskParameterSet();
      bool isNewSet = true;
      frmParameterSet frm = new frmParameterSet(_opsData, isNewSet);
      frm.ShowDialog();
      ListParameterSets();
    }

    private void EditParameterSet()
    {
      string setName = gvParameterSets.SelectedCells[0].Value.ToString();
      _opsData.CurrentTaskParameterSet = _parameterSets[setName];
      bool isNewSet = false;
      frmParameterSet frm = new frmParameterSet(_opsData, isNewSet);
      frm.ShowDialog();
      ListParameterSets();

      foreach (DataGridViewRow row in gvParameterSets.Rows)
      {
        if (row.Cells[0].Value.ToString() == setName)
        {
          row.Selected = true;
          ListParametersInSet();
        }
      }
    }

    private void DeleteParameter()
    {
      int parameterID = gvTaskParameters.SelectedRows[0].Cells[0].Value.ToInt32();
      DialogResult result = DialogResult.Yes;

      if (_opsData.Environment == "Prod")
        result = MessageBox.Show("Are you sure you want to permanently delete Task Parameter with ID: " + parameterID.ToString() +
                                      " from the Task Scheduling Database?", "Ops Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
      if (result == DialogResult.Yes)
      {
        using (var repo = new TaskRepository(_opsData.TasksDbSpec))
        {
          repo.DeleteTaskParameter(parameterID);
        }
      }
    }

    private void ListTaskParameters()
    {
      try
      {
        lblStatus.Text = "Loading parameters...";

        _taskParameters = new SortedList<int, TaskParameter>();

        using (var repo = new TaskRepository(_opsData.TasksDbSpec))
        {
          if (_isSpecificTask)
            _taskParameters = repo.GetTaskParameters(_opsData.CurrentScheduledTask.ScheduledTaskId);
          else
            _taskParameters = repo.GetTaskParameters(null);
        }

        gvTaskParameters.Rows.Clear();
        foreach (var tp in _taskParameters.Values)
        {
          gvTaskParameters.Rows.Add(tp.ParameterID, tp.ParameterName, tp.ParameterValue, tp.DataType);
        }
        lblStatus.Text = "Loaded " + _taskParameters.Count.ToString() + " Task Parameter(s)";
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when trying to list Task Parameters", ex);
      }
    }

    private void ListParameterSets()
    {
      try
      {
        _clearingParameterSets = true;
        gvParameterSets.Rows.Clear();
        _clearingParameterSets = false;
        _parameterSets = new SortedList<string, TaskParameterSet>();

        using (var repo = new TaskRepository(_opsData.TasksDbSpec))
        {
          _parameterSets = repo.GetParameterSets();
        }

        foreach (var parameterSet in _parameterSets.Values)
        {
          gvParameterSets.Rows.Add(parameterSet.ParameterSetName);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when trying to list Parameter Sets", ex);
      }
    }

    private void ListParametersInSet()
    {
      try
      {
        gvParametersInSet.Rows.Clear();
        string parameterSetName = gvParameterSets.SelectedCells[0].Value.ToString();
        
        foreach (var taskParameter in _parameterSets[parameterSetName].TaskParameters)
        {
          gvParametersInSet.Rows.Add(taskParameter.ParameterID, taskParameter.ParameterName, taskParameter.ParameterValue, taskParameter.DataType);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when trying to list Parameters in the Set", ex);
      }
    }

    private void MigrateVariable()
    {
      try
      {
        TaskParameter parameterVariable = new TaskParameter();

        var selectedCells = gvTaskParameters.SelectedRows[0].Cells;

        parameterVariable.ParameterID = selectedCells[0].Value.ToInt32();
        parameterVariable.ParameterName = selectedCells[1].Value.ToString();
        parameterVariable.ParameterValue = selectedCells[2].Value.ToString();
        parameterVariable.DataType = selectedCells[3].Value.ToString();
        parameterVariable.CreatedBy = g.SystemInfo.DomainAndUser;
        parameterVariable.CreatedDate = DateTime.Now;

        string destinationEnv;
        if (_opsData.Environment == "Test")
          destinationEnv = "Prod";
        else
          destinationEnv = "Test";

        var result = MessageBox.Show("Are you sure you want to migrate Parameter '" + parameterVariable.ParameterName + "' to " + destinationEnv + "?",
                                     "OpsManager - Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result == DialogResult.No)
          return;

        var destinationDbSpec = g.GetDbSpec("TaskScheduling" + destinationEnv);
        using (var taskRepo = new TaskRepository(destinationDbSpec))
        {
          taskRepo.MigrateVariable(parameterVariable);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to migrate parameter variable." + g.crlf2 + ex.ToReport(),
                        "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void MigrateParameterSet()
    {
      try
      {
        var parameterSet = new TaskParameterSet();
        parameterSet.ParameterSetName = gvParameterSets.SelectedRows[0].Cells[0].Value.ToString();

        var currentDate = DateTime.Now;
        foreach (var parameter in _parameterSets[parameterSet.ParameterSetName].TaskParameters)
        {
          parameter.CreatedBy = g.SystemInfo.DomainAndUser;
          parameter.CreatedDate = currentDate;
          parameter.ModifiedBy = null;
          parameter.ModifiedDate = null;
          parameterSet.TaskParameters.Add(parameter);
        }

        string destinationEnv;
        if (_opsData.Environment == "Test")
          destinationEnv = "Prod";
        else
          destinationEnv = "Test";

        var result = MessageBox.Show("Are you sure you want to migrate Parameter Set '" + parameterSet.ParameterSetName + "' to " + destinationEnv + "?",
                                     "OpsManager - Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result == DialogResult.No)
          return;

        var destinationDbSpec = g.GetDbSpec("TaskScheduling" + destinationEnv);
        using (var taskRepo = new TaskRepository(destinationDbSpec))
          taskRepo.MigrateParameterSet(parameterSet);

        ListParameterSets();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to migrate parameter set '" + gvParameterSets.SelectedRows[0].Cells[0].Value.ToString() + 
                        "'." + g.crlf2 + ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private bool IsValidDoubleLeftClick(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        DataGridView.HitTestInfo hit = gvTaskParameters.HitTest(e.X, e.Y);
        if (hit.Type == DataGridViewHitTestType.Cell)
          return true;

        hit = gvParameterSets.HitTest(e.X, e.Y);
        if (hit.Type == DataGridViewHitTestType.Cell)
          return true;

        hit = gvParametersInSet.HitTest(e.X, e.Y);
        if (hit.Type == DataGridViewHitTestType.Cell)
          return true;
      }
      return false;
    }

    private void ctxMenu_Opening(object sender, CancelEventArgs e)
    {
      ContextMenuStrip ctxMenu = sender as ContextMenuStrip;
      DataGridView gridview = new DataGridView();

      string migrateToEnv;
      if (_opsData.Environment == "Test")
        migrateToEnv = "Prod";
      else
        migrateToEnv = "Test";

      if (ctxMenu.Name == "ctxMenuTaskParameters")
      {
        gridview = gvTaskParameters;
        
        ToolStripMenuItem menuItem = new ToolStripMenuItem();
        menuItem.Visible = true;
        ctxMenu.Items.Clear();
        if (_isSpecificTask)
        {
          menuItem.Name = "ctxMenuTaskParametersDelete";
          menuItem.Text = "Delete";
          menuItem.Tag = "DeleteTaskParameter";     
        }
        else
        {
          menuItem.Name = "ctxMenuTaskParametersMigrate";
          menuItem.Text = "Migrate To " + migrateToEnv;
          menuItem.Tag = "MigrateVariable";
        }
        menuItem.Click += new System.EventHandler(this.Action);
        ctxMenu.Items.Add(menuItem);
      }
      else if (ctxMenu.Name == "ctxMenuParameterSets")
      {
        gridview = gvParameterSets;

        if (!_isSpecificTask)
          ctxMenuParameterSets.Items[0].Visible = false;

        ctxMenuParameterSets.Items[1].Text = "Migrate To " + migrateToEnv;
      }
      else
      {
        e.Cancel = true;
        return;
      }

      var mousePos = ctxMenu.PointToClient(Control.MousePosition);
      if (ctxMenu.ClientRectangle.Contains(mousePos))
      {
        var gvMousePos = gridview.PointToClient(Control.MousePosition);
        var hitTestIndex = gridview.HitTest(gvMousePos.X, gvMousePos.Y);
        if (hitTestIndex.RowIndex == -1)
        {
          e.Cancel = true;
          return;
        }
        else
        {
          gridview.Rows[hitTestIndex.RowIndex].Selected = true;
        }
      }
      else e.Cancel = true;
    }

    private void gvParameterSets_SelectionChange(object sender, EventArgs e)
    {
      if (!_clearingParameterSets)
        ListParametersInSet();
    }

    private void InitializeTaskParametersGrid()
    {
      try
      {
        gvTaskParameters.Columns.Clear();
        GridView view = _opsData.GridViewSet["taskParameters"];
        view.SetColumnWidths(gvTaskParameters.ClientSize.Width);

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
          gvTaskParameters.Columns.Add(col);
        }

        gvTaskParameters.Columns[0].Visible = false;

        ListTaskParameters();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the TaskParameters grid using view 'taskParameters' " +
                        g.crlf2 + ex.ToReport(), "OpsManager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void InitializeParameterSetsGrid()
    {
      try
      {
        gvParameterSets.Columns.Clear();
        GridView view = _opsData.GridViewSet["parameterSets"];
        view.SetColumnWidths(gvParameterSets.ClientSize.Width);

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
          gvParameterSets.Columns.Add(col);
        }

        ListParameterSets();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the ParameterSets grid using view 'parameterSets' " +
                        g.crlf2 + ex.ToReport(), "OpsManager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void InitializeParametersInSetGrid()
    {
      try
      {
        gvParametersInSet.Columns.Clear();
        GridView view = _opsData.GridViewSet["parametersInSet"];
        view.SetColumnWidths(gvParametersInSet.ClientSize.Width);

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
          gvParametersInSet.Columns.Add(col);
        }

        gvParametersInSet.Columns[0].Visible = false;

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to initialize the ParametersInSet grid using view 'parametersInSet' " +
                        g.crlf2 + ex.ToReport(), "OpsManager - Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
