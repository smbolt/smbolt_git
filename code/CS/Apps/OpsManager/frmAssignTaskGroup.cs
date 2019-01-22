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

namespace Org.OpsManager
{
  public partial class frmAssignTaskGroup : Form
  {
    private ScheduledTask _scheduleTask;
    private ScheduledTaskGroup _selectedTaskGroup;
    private SortedList<int, ScheduledTaskGroup> _scheduledTaskGroups;
    private ConfigDbSpec _configDbSpec;

    public frmAssignTaskGroup(ScheduledTask scheduledTask, SortedList<int, ScheduledTaskGroup> scheduledTaskGroups, ConfigDbSpec configDbSpec)
    {
      InitializeComponent();

      _scheduleTask = scheduledTask;
      _scheduledTaskGroups = scheduledTaskGroups;
      _configDbSpec = configDbSpec;

      InitializeForm();
    }


    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "OK":
          UpdateScheduledTask();
          this.DialogResult = DialogResult.OK;
          this.Close();
          break;

        case "Cancel":
          this.DialogResult = DialogResult.Cancel;
          this.Close();
          break;
      }
    }

    private void UpdateScheduledTask()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        int? selectedTaskGroupId =  _selectedTaskGroup?.TaskGroupId ?? null;

        _scheduleTask.TaskGroupId = selectedTaskGroupId;
        _scheduleTask.TaskGroupName = _selectedTaskGroup == null ? String.Empty : _selectedTaskGroup.TaskGroupName;

        using (var repo = new TaskRepository(_configDbSpec))
        {
          repo.SetTaskGroupForScheduledTask(_scheduleTask.ScheduledTaskId, selectedTaskGroupId);
        }

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to update the Task Group for the Scheduled Task." + g.crlf2 + ex.ToReport(),
                        "Assign Task Group - Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
      }
    }

    private void InitializeForm()
    {
      lblScheduledTaskNameValue.Text = _scheduleTask.TaskName;
      
      cboTaskGroups.Items.Clear();
      cboTaskGroups.Items.Add(String.Empty);
      foreach (var group in _scheduledTaskGroups.Values)
        cboTaskGroups.Items.Add(group.TaskGroupName);

      if (_scheduleTask.TaskGroupName.IsNotBlank())
      {
        for (int i = 0; i < cboTaskGroups.Items.Count; i++)
        {
          if (_scheduleTask.TaskGroupName == cboTaskGroups.Items[i].ToString())
          {
            cboTaskGroups.SelectedIndex = i;
            break;
          }
        }
      }
      else
      {
        _selectedTaskGroup = null;
        cboTaskGroups.SelectedIndex = -1;
      }

      btnOK.Enabled = false;
    }

    private void cboTaskGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
      string selectedGroupName = cboTaskGroups.Text;

      if (selectedGroupName.IsNotBlank())
      {
        foreach (var kvpGroup in _scheduledTaskGroups)
        {
          if (kvpGroup.Value.TaskGroupName == selectedGroupName)
          {
            _selectedTaskGroup = kvpGroup.Value;
            btnOK.Enabled = _selectedTaskGroup.TaskGroupId != _scheduleTask.TaskGroupId;
            break;
          }
        }
      }
      else
      {
        _selectedTaskGroup = null;
        btnOK.Enabled = _scheduleTask.TaskGroupId != 0;
      }
    }
  }
}
