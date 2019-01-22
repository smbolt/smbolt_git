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

namespace Org.OpsManager
{
  public partial class frmEditTaskParameter : Form
  {
    OpsData _opsData;
    bool _isInitLoad = true;
    bool _isNewParameter;

    public frmEditTaskParameter(OpsData opsData, bool isNewParameter)
    {
      InitializeComponent();
      _opsData = opsData;
      _isNewParameter = isNewParameter;
      btnSave.Enabled = false;
      if (_isNewParameter)
      {
        this.Text = "New Task Parameter";
        if (_opsData.CurrentTaskParameter.ParameterValue.IsNotBlank())
        {
          txtParameterValue.Text = _opsData.CurrentTaskParameter.ParameterValue;
          txtParameterValue.Enabled = false;
        }
      }
      else 
      {
        this.Text = "Edit Task Parameter";
        SetTaskParameterData();
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
        result = MessageBox.Show("Save these changes to the ScheduledTaskParameters table in the Task Scheduling database?",
                                 "Ops Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      if (result == DialogResult.Yes)
      {
        if (_isNewParameter)
        {
          _opsData.CurrentTaskParameter.CreatedBy = g.SystemInfo.DomainAndUser;
          _opsData.CurrentTaskParameter.CreatedDate = DateTime.Now;
        }
        else
        {
          _opsData.CurrentTaskParameter.ParameterID = _opsData.CurrentTaskParameter.ParameterID;
          _opsData.CurrentTaskParameter.ModifiedBy = g.SystemInfo.DomainAndUser;
          _opsData.CurrentTaskParameter.ModifiedDate = DateTime.Now;
        }

        _opsData.CurrentTaskParameter.ParameterName = txtParameterName.Text.Trim();
        _opsData.CurrentTaskParameter.ParameterValue = txtParameterValue.Text.Trim();
        _opsData.CurrentTaskParameter.DataType = txtDataType.Text.Trim();

        if (_opsData.CurrentTaskParameter.ParameterSetName.IsNotBlank())
          _opsData.CurrentTaskParameter.ParameterSetName = _opsData.CurrentTaskParameter.ParameterSetName;
        else if (_opsData.CurrentScheduledTask != null)
          _opsData.CurrentTaskParameter.ScheduledTaskID = _opsData.CurrentScheduledTask.ScheduledTaskId;

        using (var repo = new TaskRepository(_opsData.TasksDbSpec))
        {
          if (_isNewParameter)
            repo.InsertTaskParameter(_opsData.CurrentTaskParameter);
          else
            repo.UpdateTaskParameter(_opsData.CurrentTaskParameter);
        }
        this.DialogResult = DialogResult.None;
        btnSave.Enabled = false;
      }
    }
        
    private void SetTaskParameterData()
    {
      try
      {
        txtParameterName.Text = _opsData.CurrentTaskParameter.ParameterName;
        txtParameterValue.Text = _opsData.CurrentTaskParameter.ParameterValue;
        txtDataType.Text = _opsData.CurrentTaskParameter.DataType;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when trying to set Task Parameter info", ex);
      }
    }

    private void CheckForChanges()
    {
      if (_isInitLoad)
        return;

      if (txtParameterName.Text.Trim() != _opsData.CurrentTaskParameter.ParameterName ||
          txtParameterValue.Text.Trim() != _opsData.CurrentTaskParameter.ParameterValue ||
          txtDataType.Text.Trim() != _opsData.CurrentTaskParameter.DataType
         )
      {
        btnSave.Enabled = true;
      }
      else btnSave.Enabled = false;
    }
  }
}
