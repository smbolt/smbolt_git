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
using Org.GS;

namespace Org.SvcManager
{
  public partial class frmTaskServices : Form
  {
    private ConfigDbSpec _prodDbSpec;
    private ConfigDbSpec _testDbSpec;
    private TaskServiceSet _taskServiceSet;


    public frmTaskServices(ConfigDbSpec prodDbSpec, ConfigDbSpec testDbSpec)
    {
      InitializeComponent();
      _prodDbSpec = prodDbSpec;
      _testDbSpec = testDbSpec;

      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "AddNew":
          AddNewTaskService();
          break;

        case "Close":
          this.DialogResult = DialogResult.OK;
          this.Close();
          break;
      }
    }

    private void AddNewTaskService()
    {
      List<string> hostNames = new List<string>();
      hostNames.Add("OKC1WEB1001"); 

      //using (var fAddOrUpdateTaskService = new frmAddOrUpdateTaskService(true, hostNames, _taskServiceSet))
      //{
      //  if (fAddOrUpdateTaskService.ShowDialog() == DialogResult.OK)
      //  {
      //    // get the new object, put it in the database and update the grid

      //  }
      //}
    }

    private void LoadTaskServiceGrid()
    {
      try
      {
        gvTaskServices.Rows.Clear();

        if (cboEnvironment.Text.IsBlank())
          return;

        this.Cursor = Cursors.WaitCursor;
        var dbSpec = cboEnvironment.Text == "Prod" ? _prodDbSpec : _testDbSpec;
        
        using (var taskRepo = new TaskRepository(dbSpec))
        {
          _taskServiceSet = taskRepo.GetTaskServiceSet();
        }

        foreach (var taskService in _taskServiceSet.OrderBy(t => t.HostName).ThenBy(t => t.TaskServiceName))
        {
          gvTaskServices.Rows.Add(
            taskService.HostName,
            taskService.TaskServiceName,
            taskService.TaskServiceID);
        }

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        throw new Exception("An exception occurred when attempting to get the TaskServiceSet.", ex);
      }
    }

    private void InitializeForm()
    {
      InitializeGrid();
    }

    private void InitializeGrid()
    {
      gvTaskServices.Columns.Clear();

      DataGridViewColumn col = new DataGridViewTextBoxColumn();
      col.Name = "TaskServiceHost";
      col.HeaderText = "Task Service Host";
      col.Width = 250;
      gvTaskServices.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "TaskServiceName";
      col.HeaderText = "Task Service Name";
      col.Width = 250;
      col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      gvTaskServices.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "TaskServiceID";
      col.HeaderText = String.Empty;
      col.Width = 0;
      col.Visible = false;
      gvTaskServices.Columns.Add(col);
    }

    private void cboEnvironment_SelectedIndexChanged(object sender, EventArgs e)
    {
      LoadTaskServiceGrid();
    }
  }
}
