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

namespace Org.WinServiceHost
{
  public partial class frmTaskCheck : Form
  {
    private AppConfig _appConfig;
    private AppInfo _appInfo;

    public frmTaskCheck(AppInfo appInfo, AppConfig appConfig)
    {
      InitializeComponent();

      _appInfo = appInfo;
      _appConfig = appConfig;
      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "OK":
          this.DialogResult = DialogResult.OK;
          this.Close();
          break;

        case "Cancel":
          this.DialogResult = DialogResult.OK;
          this.Close();
          break;
      }
    }

    private void InitializeForm()
    {
      AssertRequiredContext();
      Load_cboProfiles();
      LoadConfigToGrid();

      this.Text = this.Text.Replace("$AppName", _appInfo.AppName); 
    }

    private void AssertRequiredContext()
    {
      if (g.AppConfig.ProgramConfigSet == null)
      {
        MessageBox.Show("AppConfig ProgramConfigSet is null.", _appInfo.AppName + " Host - Error");
        throw new Exception("AppConfig ProgramConfigSet is null.");
      }

      if (!g.AppConfig.ProgramConfigSet.ContainsKey("WinServiceHost"))
      {
        MessageBox.Show("AppConfig ProgramConfigSet does not contain a ProgramConfig object for '" + _appInfo.ConfigName + "'.", _appInfo.AppName + " Host - Error");
        throw new Exception("AppConfig ProgramConfigSet does not contain a ProgramConfig object for '" + _appInfo.ConfigName + "'.");
      }
    }

    private void Load_cboProfiles()
    {
      cboProfile.Items.Clear();

      int idx = -1;

      int count = 0;
      foreach (KeyValuePair<string, TaskConfigSet> kvpTcs in g.AppConfig.ProgramConfigSet[_appInfo.ConfigName].TaskConfigurations)
      {
        if (kvpTcs.Key == "FastTest")
          idx = count;
        cboProfile.Items.Add(kvpTcs.Value.Mode);
        count++;
      }

      if (idx != -1)
        cboProfile.SelectedIndex = idx; 
    }

    private void LoadConfigToGrid()
    {
      Initialize_gvTasks();

      ProgramConfig pc = _appConfig.ProgramConfigSet["WinServiceHost"];
      string taskConfigProfile = _appConfig.GetCI("WSHTaskProfile");
      TaskConfigSet tcs = pc.TaskConfigurations[taskConfigProfile];

      foreach (TaskConfig taskConfig in tcs.Values)
      {
        gvTasks.Rows.Add(new object[] { taskConfig.IsActive, taskConfig.TaskName, "Immediate?", "Frequency"});
      }
    }

    private void Initialize_gvTasks()
    {
      gvTasks.Columns.Clear();
      gvTasks.Rows.Clear();

      DataGridViewColumn ckCol = new DataGridViewCheckBoxColumn();
      ckCol.Name = "Active";
      ckCol.HeaderText = "Active";
      ckCol.Width = 80;
      ckCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvTasks.Columns.Add(ckCol);

      DataGridViewColumn c = new DataGridViewTextBoxColumn();
      c.Name = "TaskName";
      c.HeaderText = "Task Name";
      c.Width = 160;
      gvTasks.Columns.Add(c); 

      ckCol = new DataGridViewCheckBoxColumn();
      ckCol.Name = "Immediate";
      ckCol.HeaderText = "Immediate";
      ckCol.Width = 80;
      ckCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvTasks.Columns.Add(ckCol);

      c = new DataGridViewTextBoxColumn();
      c.Name = "Frequency";
      c.HeaderText = "Frequency";
      c.Width = 80;
      c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      gvTasks.Columns.Add(c);

      c = new DataGridViewTextBoxColumn();
      c.Name = "Empty";
      c.HeaderText = "";
      c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      c.Width = 200;
      gvTasks.Columns.Add(c);
    }
  }
}
