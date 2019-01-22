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
  public partial class frmParameterSet : Form
  {
    OpsData _opsData;
    bool _isNewSet;
    int? _editRowIndex;

    public frmParameterSet(OpsData opsData, bool isNewSet)
    {
      InitializeComponent();
      _opsData = opsData;
      _isNewSet = isNewSet;
      InitializeParametersInSetGrid();
      btnSave.Enabled = false;

      if (_isNewSet)
        this.Text = "New Parameter Set";
      else
      {
        this.Text = "Edit Parameter Set";
        ListParametersInSet();
        txtParameterSetName.Text = _opsData.CurrentTaskParameterSet.ParameterSetName;
      }
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "Save":
          SaveToDb();
          break;

        case "Add":
          AddTaskParameterToGrid();
          break;

        case "Clear":
          ClearParameterInfo();
          break;

        case "Edit":
          SetParameterData();
          break;

        case "Remove":
          RemoveParameterFromSet();
          break;

        case "PropertyChange":
          CheckForChanges();
          break;

        case "Cancel":
          this.DialogResult = DialogResult.Yes;
          break;

        case "Exit":
          Environment.Exit(0);
          break;
      }
    }

    private void SaveToDb()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        if (MessageBox.Show("Are you sure you want to save these changes to the ScheduledTaskParameters table in the Task Scheduling database?",
                            "Ops Manager - Confirm Parameter Set Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
          DateTime currentTime = DateTime.Now;
          TaskParameterSet parmSet = new TaskParameterSet();
          parmSet.ParameterSetName = txtParameterSetName.Text;

          if (gvParametersInSet.Rows.Count > 0)
          {
            foreach (DataGridViewRow row in gvParametersInSet.Rows)
            {
              var parm = new TaskParameter();

              if (row.Cells[0].Value.ToInt32() != 0)
              {
                parm.ParameterID = row.Cells[0].Value.ToInt32();
                parm.ModifiedBy = g.SystemInfo.DomainAndUser;
                parm.ModifiedDate = currentTime;
              }
              else
              {
                parm.CreatedBy = g.SystemInfo.DomainAndUser;
                parm.CreatedDate = currentTime;
              }

              parm.ParameterSetName = txtParameterSetName.Text;
              parm.ParameterName = row.Cells[1].Value.ToString();
              if (row.Cells[2].Value.ToString().IsNotBlank())
                parm.ParameterValue = row.Cells[2].Value.ToString();
              parm.DataType = row.Cells[3].Value.ToString();

              parmSet.TaskParameters.Add(parm);
            }

            using (var repo = new TaskRepository(_opsData.TasksDbSpec))
            {
              foreach (var taskParameter in parmSet.TaskParameters)
              {
                if (taskParameter.ParameterID == 0)
                  repo.InsertTaskParameter(taskParameter);
                else
                  repo.UpdateTaskParameter(taskParameter);
              }
            }
            this.DialogResult = DialogResult.None;
            btnSave.Enabled = false;
          }
        }

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to save the parameter set to the database." + g.crlf2 + ex.ToReport()); 
      }
    }

    private void AddTaskParameterToGrid()
    {
      if (_editRowIndex.HasValue)
      {
        gvParametersInSet.Rows[_editRowIndex.ToInt32()].Cells[1].Value = txtParameterName.Text;
        gvParametersInSet.Rows[_editRowIndex.ToInt32()].Cells[2].Value = txtParameterValue.Text;
        gvParametersInSet.Rows[_editRowIndex.ToInt32()].Cells[3].Value = txtDataType.Text;
        btnAdd.Text = "Add";
      }
      else
        gvParametersInSet.Rows.Add(0, txtParameterName.Text, txtParameterValue.Text, txtDataType.Text);

      ClearParameterInfo();
    }

    private void SetParameterData()
    {
      if (gvParametersInSet.SelectedRows.Count == 1)
      {
        txtParameterName.Text = gvParametersInSet.SelectedCells[1].Value.ToString();
        txtParameterValue.Text = gvParametersInSet.SelectedCells[2].Value.ToString();
        txtDataType.Text = gvParametersInSet.SelectedCells[3].Value.ToString();

        _editRowIndex = gvParametersInSet.SelectedRows[0].Index;
        btnAdd.Text = "Update";
        btnAdd.Enabled = false;
      }
      else
        MessageBox.Show("Please select 1 Parameter to edit", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private void RemoveParameterFromSet()
    {
      if (gvParametersInSet.SelectedRows.Count == 1)
      {
        if (gvParametersInSet.SelectedCells[0].Value.ToInt32() == 0)
          gvParametersInSet.Rows.Remove(gvParametersInSet.SelectedRows[0]);
        else
          MessageBox.Show("Cannot delete previously existing parameter from the set", "Ops Manager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      else
        MessageBox.Show("Please select 1 Parameter to edit", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private void ClearParameterInfo()
    {
      txtParameterName.Text = "";
      txtParameterValue.Text = "";
      txtDataType.Text = "";
      _editRowIndex = null;
      btnAdd.Text = "Add";
      txtParameterName.Focus();
    }

    private void ListParametersInSet()
    {
      try
      {
        gvParametersInSet.Rows.Clear();

        foreach (var tp in _opsData.CurrentTaskParameterSet.TaskParameters)
        {
          gvParametersInSet.Rows.Add(tp.ParameterID,tp.ParameterName, tp.ParameterValue, tp.DataType);
        }
        lblStatus.Text = "Loaded " + _opsData.CurrentTaskParameterSet.TaskParameters.Count.ToString() + " Parameter(s)";
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurren when trying to list Parameters in Parameter Set", ex);
      }
    }

    private void CheckForChanges()
    {
      if (txtParameterSetName.Text.IsNotBlank() && txtParameterName.Text.IsNotBlank() && txtDataType.Text.IsNotBlank())
      {
        if (_editRowIndex.HasValue)
        {
          if (txtParameterName.Text != gvParametersInSet.Rows[_editRowIndex.Value].Cells[1].Value.ToString() ||
              txtParameterValue.Text != gvParametersInSet.Rows[_editRowIndex.Value].Cells[2].Value.ToString() ||
              txtDataType.Text != gvParametersInSet.Rows[_editRowIndex.Value].Cells[3].Value.ToString())
          {
            btnAdd.Enabled = true;
            btnSave.Enabled = true;
          }
          else
            btnAdd.Enabled = false;
        }
        else
          btnAdd.Enabled = true;
          btnSave.Enabled = true;
      }
      else
        btnAdd.Enabled = false;

      if (txtParameterSetName.Text.IsNotBlank() && txtParameterSetName.Text != _opsData.CurrentTaskParameterSet.ParameterSetName)
        btnSave.Enabled = true;
    }

    private void InitializeParametersInSetGrid()
    {
      try
      {
        gvParametersInSet.Columns.Clear();
        var test = _opsData.GridViewSet;
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

    private void ctxMenuParametersInSet_Opening(object sender, CancelEventArgs e)
    {
      ContextMenuStrip ctxMenu = sender as ContextMenuStrip;
      if (ctxMenu != null)
      {
        var mousePos = ctxMenu.PointToClient(Control.MousePosition);
        if (ctxMenu.ClientRectangle.Contains(mousePos))
        {
          var gvMousePos = gvParametersInSet.PointToClient(Control.MousePosition);
          var hitTestIndex = gvParametersInSet.HitTest(gvMousePos.X, gvMousePos.Y);
          if (hitTestIndex.RowIndex == -1)
          {
            e.Cancel = true;
          }
          else
          {
            gvParametersInSet.ClearSelection();
            gvParametersInSet.Rows[hitTestIndex.RowIndex].Selected = true;
          }
        }
        else e.Cancel = true;
      }
    }
  }
}
