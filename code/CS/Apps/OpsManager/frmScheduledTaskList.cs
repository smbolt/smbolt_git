using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.TSK.Business;
using Org.TSK.Business.Models;
using Org.GS.Configuration;
using Org.GS;

namespace Org.OpsManager
{
  public partial class frmScheduledTaskList : Form
  {
    private int _copyToTaskId;
    private int _copyFromTaskId;
    private SortedList<int, ScheduledTask> _scheduledTasks;
    private ConfigDbSpec _configDbSpec;

    public string Result { get; private set; }

    public frmScheduledTaskList(int copyToTaskId, SortedList<int, ScheduledTask> scheduledTasks, ConfigDbSpec configDbSpec)
    {
      InitializeComponent();

      _copyToTaskId = copyToTaskId;
      _scheduledTasks = scheduledTasks;
      _configDbSpec = configDbSpec;

      InitializeForm();
    }


    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "OK":
          if (UpdateScheduledTask())
          {
            this.DialogResult = DialogResult.OK;
            this.Close();
          }
          break;

        case "Cancel":
          this.DialogResult = DialogResult.Cancel;
          this.Result = "No updates have been made.";
          this.Close();
          break;
      }
    }

    private bool UpdateScheduledTask()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        using (var repo = new TaskRepository(_configDbSpec))
        {
          repo.CopyScheduleAndParameters(_copyFromTaskId, _copyToTaskId, ckCopySchedule.Checked, ckCopyParameters.Checked);

          if (ckCopyParameters.Checked)
          {
            if (ckCopySchedule.Checked)
              this.Result = "Task schedule and parameters copied.";
            else
              this.Result = "Task parameters copied.";
          }
          else
          {
            this.Result = "Task schedule copied.";
          }
        }
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception has occurred while attempting to update the Scheduled Task." + g.crlf2 + ex.ToReport(),
                        "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        return false;
      }

      this.Cursor = Cursors.Default;
      return true;
    }

    private void InitializeForm()
    {
      cboScheduledTasks.Items.Clear();

      var scheduledTaskNames = new List<string>();

      foreach (var scheduledTask in _scheduledTasks.Values)
      {
        if (scheduledTask.ScheduledTaskId == _copyToTaskId)
          continue;

        scheduledTaskNames.Add(scheduledTask.TaskName); 
      }

      scheduledTaskNames.Sort();
      cboScheduledTasks.Items.AddRange(scheduledTaskNames.ToArray());

      ckCopyParameters.Enabled = false;
      ckCopySchedule.Enabled = false;

      btnOK.Enabled = false;
    }

    private void cboScheduledTasks_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cboScheduledTasks.Text.IsBlank())
      {
        _copyFromTaskId = -1;
        btnOK.Enabled = false;
        ckCopyParameters.Enabled = false;
        ckCopySchedule.Enabled = false;
      }
      else
      {
        var copyFromTask = _scheduledTasks.Values.Where(t => t.TaskName == cboScheduledTasks.Text).FirstOrDefault();
        if (copyFromTask == null)
          return;

        _copyFromTaskId = copyFromTask.ScheduledTaskId;
        btnOK.Enabled = true;
        ckCopyParameters.Enabled = true;
        ckCopySchedule.Enabled = true;
      }
    }

    private void CheckedChanged(object sender, EventArgs e)
    {
      btnOK.Enabled = ckCopyParameters.Checked || ckCopySchedule.Checked;
    }
  }
}
