using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Teleflora.Operations.MetricView
{
  public partial class frmGraphProperties : Form
  {
    private MetricGraphConfiguration _metricGraphConfig;
    private MetricDataObjects _metricDataObjects;
    private AvailableMetricSet ams;
    private AvailableMetricSet amsESQLMGMT;
    private AvailableMetricSet amsDisplay;
    private bool SuppressCkEvent = false;
    private int[] envID;
    private int[] systemID;
    private int[] applicationID;
    private int[] applicationTypeID;
    private List<string> listItemDesc = new List<string>();
    private List<int[]> listItemKeys = new List<int[]>();
    private List<int> availableMetricIndex = new List<int>();
    private List<string> listObsvServers = new List<string>();
    private string nameSeq = String.Empty;

    public frmGraphProperties(MetricGraphConfiguration config, MetricDataObjects data)
    {
      _metricGraphConfig = config;
      _metricDataObjects = data;

      InitializeComponent();

      InitializeForm();
    }

    private void InitializeForm()
    {
      Initialize_AvailableMetricsList();
      Initialize_IncludedMetricsList();
      LoadEnvironmentsCombo();
      LoadTargetSystemsCombo();
      LoadTargetApplicationsCombo();

      LoadIncludedMetrics();

      if (ckUseEsqlmgmt.Checked)
      {
        _metricGraphConfig.UseESQLMGMT = true;
        if (_metricDataObjects.AvailableMetrics != null)
          if(_metricDataObjects.AvailableMetrics.Count > 0)
            if (_metricDataObjects.AvailableMetrics[0].MetricSource == 1)
            {
              amsESQLMGMT = _metricDataObjects.AvailableMetrics;
              LoadAvailableMetrics();
            }
      }
      else
      {
        _metricGraphConfig.UseESQLMGMT = false;
        if (_metricDataObjects.AvailableMetrics != null)
          if(_metricDataObjects.AvailableMetrics.Count > 0)
            if (_metricDataObjects.AvailableMetrics[0].MetricSource == 0)
            {
              ams = _metricDataObjects.AvailableMetrics;
              LoadAvailableMetrics();
            }

        ckUseEsqlmgmt.Visible = false;
        ckUseEsqlmgmt.Checked = false;
      }

      nameSeq = GeneralUtility.GetSeqFromName(_metricGraphConfig.GraphName);
      txtGraphName.Text = GeneralUtility.StripSeqFromName(_metricGraphConfig.GraphName);
      txtRefreshInterval.Text = _metricGraphConfig.RefreshInterval.ToString();
      ckActive.Checked = _metricGraphConfig.IsActive;
      ckUseEsqlmgmt.Checked = _metricGraphConfig.UseESQLMGMT;
      if (_metricGraphConfig.IncludedMetrics.Count > 0)
        ckUseEsqlmgmt.Enabled = false;
      btnAddToGraph.Enabled = false;
      btnRemoveFromGraph.Enabled = false;
      mnuContextListViewRollUp.Visible = false;
    }


    private void Initialize_AvailableMetricsList()
    {
      lvAvailableMetrics.Clear();

      ColumnHeader ch = new ColumnHeader();
      ch.Text = "Env";
      ch.Width = 33;
      ch.TextAlign = HorizontalAlignment.Left;
      lvAvailableMetrics.Columns.Add(ch);

      ch = new ColumnHeader();
      ch.Text = "Typ";
      ch.Width = 33;
      ch.TextAlign = HorizontalAlignment.Left;
      lvAvailableMetrics.Columns.Add(ch);

      ch = new ColumnHeader();
      ch.Text = "Target Sys";
      ch.Width = 80;
      ch.TextAlign = HorizontalAlignment.Left;
      lvAvailableMetrics.Columns.Add(ch);

      ch = new ColumnHeader();
      ch.Text = "Target App";
      ch.Width = 120;
      ch.TextAlign = HorizontalAlignment.Left;
      lvAvailableMetrics.Columns.Add(ch);

      ch = new ColumnHeader();
      ch.Text = "Obsv Sys";
      ch.Width = 80;
      ch.TextAlign = HorizontalAlignment.Left;
      lvAvailableMetrics.Columns.Add(ch);

      ch = new ColumnHeader();
      ch.Text = "Obsv App";
      ch.Width = 120;
      ch.TextAlign = HorizontalAlignment.Left;
      lvAvailableMetrics.Columns.Add(ch);

      ch = new ColumnHeader();
      ch.Text = "Obsv Svr";
      ch.Width = 145;
      ch.TextAlign = HorizontalAlignment.Left;
      lvAvailableMetrics.Columns.Add(ch);

      ch = new ColumnHeader();
      ch.Text = "Desc (State/Metric/ValueType/Interval)";
      ch.Width = 235;
      ch.TextAlign = HorizontalAlignment.Left;
      lvAvailableMetrics.Columns.Add(ch);
    }


    private void Initialize_IncludedMetricsList()
    {
      lvIncludedMetrics.Clear();

      ColumnHeader ch = new ColumnHeader();
      ch.Text = "Env";
      ch.Width = 33;
      ch.TextAlign = HorizontalAlignment.Left;
      lvIncludedMetrics.Columns.Add(ch);

      ch = new ColumnHeader();
      ch.Text = "Typ";
      ch.Width = 33;
      ch.TextAlign = HorizontalAlignment.Left;
      lvIncludedMetrics.Columns.Add(ch);

      ch = new ColumnHeader();
      ch.Text = "Target Sys";
      ch.Width = 80;
      ch.TextAlign = HorizontalAlignment.Left;
      lvIncludedMetrics.Columns.Add(ch);

      ch = new ColumnHeader();
      ch.Text = "Target App";
      ch.Width = 120;
      ch.TextAlign = HorizontalAlignment.Left;
      lvIncludedMetrics.Columns.Add(ch);

      ch = new ColumnHeader();
      ch.Text = "Obsv Sys";
      ch.Width = 80;
      ch.TextAlign = HorizontalAlignment.Left;
      lvIncludedMetrics.Columns.Add(ch);

      ch = new ColumnHeader();
      ch.Text = "Obsv App";
      ch.Width = 120;
      ch.TextAlign = HorizontalAlignment.Left;
      lvIncludedMetrics.Columns.Add(ch);

      ch = new ColumnHeader();
      ch.Text = "Obsv Svr";
      ch.Width = 145;
      ch.TextAlign = HorizontalAlignment.Left;
      lvIncludedMetrics.Columns.Add(ch);

      ch = new ColumnHeader();
      ch.Text = "Desc (State/Metric/ValueType/Interval)";
      ch.Width = 235;
      ch.TextAlign = HorizontalAlignment.Left;
      lvIncludedMetrics.Columns.Add(ch);
    }


    private void LoadEnvironmentsCombo()
    {
      cboEnvironment.Items.Clear();
      cboEnvironment.Items.Add("--------");
      int count = 1;
      int selectIndex = -1;
      envID = new int[_metricDataObjects.Environments.Count + 1];
      foreach (KeyValuePair<int, Environment> envKVP in _metricDataObjects.Environments)
      {
        cboEnvironment.Items.Add(envKVP.Value.EnvironmentDesc);
        envID[count++] = envKVP.Value.EnvironmentID;
        if (envKVP.Value.EnvironmentDesc.CompareTo("Production") == 0)
          selectIndex = cboEnvironment.Items.Count - 1;
      }

      if (selectIndex > -1)
        cboEnvironment.SelectedIndex = selectIndex;

    }

    private void LoadTargetSystemsCombo()
    {
      cboTargetSystem.Items.Clear();
      cboTargetSystem.Items.Add("--------");
      int count = 1;
      systemID = new int[_metricDataObjects.Systems.Count + 1];
      foreach (KeyValuePair<int, TFSystem> systemKVP in _metricDataObjects.Systems)
      {
        cboTargetSystem.Items.Add(systemKVP.Value.SystemDesc);
        systemID[count++] = systemKVP.Value.SystemID;
      }
    }

    private void LoadTargetApplicationsCombo()
    {
      cboTargetApplication.Items.Clear();
      cboTargetApplication.Items.Add("--------");
      int count = 1;
      applicationID = new int[_metricDataObjects.Applications.Count + 1];
      applicationTypeID = new int[_metricDataObjects.Applications.Count + 1];
      foreach (KeyValuePair<int, TFApplication> applKVP in _metricDataObjects.Applications)
      {
        cboTargetApplication.Items.Add(applKVP.Value.ApplicationName);
        applicationID[count] = applKVP.Value.ApplicationID;
        applicationTypeID[count++] = applKVP.Value.ApplicationTypeID;
      }
    }
    private void btnUpdate_Click(object sender, EventArgs e)
    {
      if (IsInputValid())
      {
        UpdateConfig();
        this.Close();
      }
    }


    private void UpdateConfig()
    {
      _metricGraphConfig.GraphName = nameSeq + txtGraphName.Text.Trim();
      _metricGraphConfig.RefreshInterval = int.Parse(txtRefreshInterval.Text);

      _metricGraphConfig.IsActive = ckActive.Checked;

      if (optionLatestMetrics.Checked)
      {
        _metricGraphConfig.IncludedMetrics.UseMostCurrentMetric = true;
        switch (cboDataPoints.SelectedIndex)
        {
          case 0:
            _metricGraphConfig.IncludedMetrics.DataPoints = 100; // default - edits should prevent this value
            break;
          case 1:
            _metricGraphConfig.IncludedMetrics.DataPoints = 10;
            break;
          case 2:
            _metricGraphConfig.IncludedMetrics.DataPoints = 25;
            break;
          case 3:
            _metricGraphConfig.IncludedMetrics.DataPoints = 50;
            break;
          case 4:
            _metricGraphConfig.IncludedMetrics.DataPoints = 100;
            break;
          case 5:
            _metricGraphConfig.IncludedMetrics.DataPoints = 200;
            break;
          case 6:
            _metricGraphConfig.IncludedMetrics.DataPoints = 300;
            break;
          case 7:
            _metricGraphConfig.IncludedMetrics.DataPoints = 500;
            break;
          case 8:
            _metricGraphConfig.IncludedMetrics.DataPoints = 1000;
            break;
          case 9:
            _metricGraphConfig.IncludedMetrics.DataPoints = 2000;
            break;
          case 10:
            _metricGraphConfig.IncludedMetrics.DataPoints = 3000;
            break;
        }
        _metricGraphConfig.IncludedMetrics.FromDateTime = DateTime.MinValue; // default
        _metricGraphConfig.IncludedMetrics.ToDateTime = DateTime.MinValue; // default
      }
      else
      {
        _metricGraphConfig.IncludedMetrics.UseMostCurrentMetric = false;
        _metricGraphConfig.IncludedMetrics.DataPoints = 100; // default
        _metricGraphConfig.IncludedMetrics.FromDateTime = dtpFromDate.Value;
        _metricGraphConfig.IncludedMetrics.ToDateTime = dtpToDate.Value;
      }

    }


    private bool IsInputValid()
    {
      return true;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void txtRefreshInterval_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        e.Handled = true;
    }

    private void optionLatestMetrics_CheckedChanged(object sender, EventArgs e)
    {
      dtpFromDate.Enabled = false;
      dtpToDate.Enabled = false;
      cboDataPoints.Enabled = true;
    }

    private void optionFilterByDate_CheckedChanged(object sender, EventArgs e)
    {
      dtpFromDate.Enabled = true;
      dtpToDate.Enabled = true;
      dtpFromDate.Value = DateTime.Now.AddMinutes(-30);
      dtpToDate.Value = DateTime.Now;
      cboDataPoints.Enabled = false;
      cboDataPoints.SelectedIndex = 3;
    }

    private void btnGetMetrics_Click(object sender, EventArgs e)
    {
      if (ckUseEsqlmgmt.Checked)
      {
        GetAvaialbleESQLMGMT_Metrics();
      }
      else
      {
        GetAvailableMetrics();
      }
    }

    private void GetAvailableMetrics()
    {
      this.Cursor = Cursors.WaitCursor;
      lvAvailableMetrics.Items.Clear();
      MetricQueryParms parms = new MetricQueryParms();

      if (cboEnvironment.SelectedIndex > 0)
      {
        parms.IsEnvironmentIDSpecified = true;
        parms.EnvironmentID = envID[cboEnvironment.SelectedIndex];
      }

      if (cboTargetSystem.SelectedIndex > 0)
      {
        parms.IsTargetSystemIDSpecified = true;
        parms.TargetSystemID = systemID[cboTargetSystem.SelectedIndex];
      }

      if (cboTargetApplication.SelectedIndex > 0)
      {
        parms.IsTargetApplicationIDSpecified = true;
        parms.TargetApplicationID = applicationID[cboTargetApplication.SelectedIndex];
      }

      _metricDataObjects.GetAvailableMetrics(parms);
      ams = _metricDataObjects.AvailableMetrics;

      LoadAvailableMetrics();
      this.Cursor = Cursors.Arrow;

    }

    private void btnAddToGraph_Click(object sender, EventArgs e)
    {
      AddAvailableMetricToGraph();
    }

    private void AddAvailableMetricToGraph()
    {
      if(lvAvailableMetrics.SelectedIndices.Count == 0)
        return;

      int index = availableMetricIndex[lvAvailableMetrics.SelectedIndices[0]];

      int intervalTypeID = -1;

      if (_metricGraphConfig.IncludedMetrics.Count > 0)
      {
        intervalTypeID = _metricGraphConfig.IncludedMetrics[0].IntervalID;
      }



      if (ckUseEsqlmgmt.Checked)
      {
        if (intervalTypeID > -1)
        {
          if (amsESQLMGMT[index].IntervalID != intervalTypeID)
          {
            MessageBox.Show("MetricView 1.0 does not allow the mixing of interval types in a single graph.",
                            "Error - Cannot include selected metric", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }
        }

        ams[index].LegendLabelNumber = 2;
        _metricGraphConfig.IncludedMetrics.Add(_metricGraphConfig.IncludedMetrics.Count, CloneSpecificMetric(amsESQLMGMT[index]));
      }
      else
      {
        if (intervalTypeID > -1)
        {
          if (ams[index].IntervalID != intervalTypeID)
          {
            MessageBox.Show("MetricView 1.0 does not allow the mixing of interval types in a single graph.",
                            "Error - Cannot include selected metric", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }
        }

        ams[index].LegendLabelNumber = 2;
        _metricGraphConfig.IncludedMetrics.Add(_metricGraphConfig.IncludedMetrics.Count, CloneSpecificMetric(ams[index]));
      }

      SetYAxisLabel();

      LoadIncludedMetrics();
    }

    private SpecificMetric CloneSpecificMetric(SpecificMetric sm)
    {
      SpecificMetric clone = new SpecificMetric();
      clone.AggregateTypeID = sm.AggregateTypeID;
      clone.EnvironmentID = sm.EnvironmentID;
      clone.IntervalID = sm.IntervalID;
      clone.LegendLabel = sm.LegendLabel;
      clone.LegendLabelNumber = sm.LegendLabelNumber;
      clone.MetricFileName = String.Empty;
      clone.MetricID = sm.MetricID;
      clone.MetricSource = sm.MetricSource;
      clone.MetricStateID = sm.MetricStateID;
      clone.MetricTypeID = sm.MetricTypeID;
      clone.MetricValueTypeID = sm.MetricValueTypeID;
      clone.ObserverApplicationID = sm.ObserverApplicationID;
      clone.ObserverServerID = sm.ObserverServerID;
      clone.ObserverSystemID = sm.ObserverSystemID;
      clone.RollUpToHourly = false;
      clone.TargetApplicationID = sm.TargetApplicationID;
      clone.TargetSystemID = sm.TargetSystemID;
      clone.Count = 0;
      return clone;
    }


    private void SetYAxisLabel()
    {
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < _metricGraphConfig.IncludedMetrics.Count; i++)
      {
        sb.Append(Convert.ToString(i + 1) + "-" +
                  _metricDataObjects.Metrics[_metricGraphConfig.IncludedMetrics[i].MetricID].MetricDesc + "  ");
      }

      _metricGraphConfig.IncludedMetrics.YAxisLabel = sb.ToString();
    }

    private void btnRemoveFromGraph_Click(object sender, EventArgs e)
    {
      if (lvIncludedMetrics.SelectedIndices.Count == 0)
        return;
      _metricGraphConfig.IncludedMetrics.Remove(lvIncludedMetrics.SelectedIndices[0]);
      IncludedMetricSet ms = new IncludedMetricSet();
      foreach(KeyValuePair<int, SpecificMetric> amKVP in _metricGraphConfig.IncludedMetrics)
      {
        ms.Add(ms.Count, amKVP.Value);
      }
      _metricGraphConfig.IncludedMetrics = ms;

      SetYAxisLabel();
      LoadIncludedMetrics();
    }

    private void LoadIncludedMetrics()
    {
      if (_metricGraphConfig.IncludedMetrics.Count > 0)
      {
        if (_metricGraphConfig.IncludedMetrics[0].MetricSource == 1)
        {
          SuppressCkEvent = true;
          ckUseEsqlmgmt.Checked = true;
          ckUseEsqlmgmt.Visible = true;
          SuppressCkEvent = false;
        }


        if (_metricGraphConfig.IncludedMetrics.UseMostCurrentMetric)
        {
          optionLatestMetrics.Checked = true;
          optionFilterByDate.Checked = false;
          dtpFromDate.Enabled = false;
          dtpToDate.Enabled = false;

          switch (_metricGraphConfig.IncludedMetrics.DataPoints)
          {
            case 10:
              cboDataPoints.SelectedIndex = 1;
              break;
            case 25:
              cboDataPoints.SelectedIndex = 2;
              break;
            case 50:
              cboDataPoints.SelectedIndex = 3;
              break;
            case 100:
              cboDataPoints.SelectedIndex = 4;
              break;
            case 200:
              cboDataPoints.SelectedIndex = 5;
              break;
            case 300:
              cboDataPoints.SelectedIndex = 6;
              break;
            case 500:
              cboDataPoints.SelectedIndex = 7;
              break;
            case 1000:
              cboDataPoints.SelectedIndex = 8;
              break;
          }
        }
        else
        {
          cboDataPoints.SelectedIndex = 0;
          cboDataPoints.Enabled = false;
          optionLatestMetrics.Checked = false;
          optionFilterByDate.Checked = true;
          dtpFromDate.Enabled = true;
          dtpToDate.Enabled = true;
          dtpFromDate.Value = _metricGraphConfig.IncludedMetrics.FromDateTime;
          dtpToDate.Value = _metricGraphConfig.IncludedMetrics.ToDateTime;
        }
      }
      else
      {
        ckUseEsqlmgmt.Enabled = true;
        dtpFromDate.MinDate = DateTime.Now.AddYears(-5);
        dtpFromDate.MaxDate = DateTime.Now.AddDays(10);
        dtpToDate.MinDate = DateTime.Now.AddYears(-5);
        dtpToDate.MaxDate = DateTime.Now.AddDays(10);
        dtpFromDate.Value = DateTime.Now.AddHours(-1);
        dtpToDate.Value = DateTime.Now;
        dtpToDate.Enabled = false;
        dtpFromDate.Enabled = false;
        optionFilterByDate.Checked = false;
        cboDataPoints.Enabled = true;
        cboDataPoints.SelectedIndex = 3;
      }

      lvIncludedMetrics.Items.Clear();

      foreach (KeyValuePair<int, SpecificMetric> amKVP in _metricGraphConfig.IncludedMetrics)
      {
        ListViewItem i = new ListViewItem();
        i.Text = _metricDataObjects.Environments[amKVP.Value.EnvironmentID].EnvironmentDesc.Substring(0, 4);
        i.SubItems.Add(_metricDataObjects.MetricTypes[amKVP.Value.MetricTypeID].MetricTypeDesc.Substring(0, 3));
        i.SubItems.Add(_metricDataObjects.Systems[amKVP.Value.TargetSystemID].SystemDesc);
        i.SubItems.Add(_metricDataObjects.Applications[amKVP.Value.TargetApplicationID].ApplicationName);
        i.SubItems.Add(_metricDataObjects.Systems[amKVP.Value.ObserverSystemID].SystemDesc);
        i.SubItems.Add(_metricDataObjects.Applications[amKVP.Value.ObserverApplicationID].ApplicationName);
        i.SubItems.Add(_metricDataObjects.Servers[amKVP.Value.ObserverServerID].ServerDesc);
        i.SubItems.Add(_metricDataObjects.MetricStates[amKVP.Value.MetricStateID].MetricStateDesc + "." +
                       _metricDataObjects.Metrics[amKVP.Value.MetricID].MetricDesc + "." +
                       _metricDataObjects.MetricValueTypes[amKVP.Value.MetricValueTypeID].MetricValueTypeDesc + "." +
                       _metricDataObjects.Intervals[amKVP.Value.IntervalID].IntervalDesc);
        lvIncludedMetrics.Items.Add(i);
      }

      if (lvIncludedMetrics.Items.Count > 0)
        btnRemoveAllFromGraph.Enabled = true;
      else
        btnRemoveAllFromGraph.Enabled = false;

    }

    private void lvAvailableMetrics_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lvAvailableMetrics.SelectedIndices.Count > 0)
      {
        lvIncludedMetrics.SelectedIndices.Clear();
        btnAddToGraph.Enabled = true;
      }
      else
        btnAddToGraph.Enabled = false;
    }

    private void lvIncludedMetrics_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lvIncludedMetrics.SelectedIndices.Count > 0)
      {
        lvAvailableMetrics.SelectedIndices.Clear();
        btnRemoveFromGraph.Enabled = true;
      }
      else
        btnRemoveFromGraph.Enabled = false;

      mnuContextListViewRollUp.Visible = false;

      if (lvIncludedMetrics.SelectedIndices.Count > 0)
      {
        int key = lvIncludedMetrics.SelectedIndices[0];
        SpecificMetric sm = _metricGraphConfig.IncludedMetrics[key];
        if (sm.IntervalID == 3)
        {
          if (sm.RollUpToHourly)
          {
            mnuContextListViewRollUp.Text = "Cancel Rollup to Hourly";
            mnuContextListViewRollUp.Visible = true;
          }
          else
          {
            mnuContextListViewRollUp.Text = "Rollup to Hourly";
            mnuContextListViewRollUp.Visible = true;
          }
        }
      }

    }

    private void lvAvailableMetrics_DoubleClick(object sender, EventArgs e)
    {
      AddAvailableMetricToGraph();
    }

    private void mnuContextListViewMetricProperties_Click(object sender, EventArgs e)
    {
      SpecificMetric sm = new SpecificMetric();
      bool IsSpecificMetricFound = false;

      if (lvAvailableMetrics.SelectedIndices.Count > 0)
      {
        int key = lvAvailableMetrics.SelectedIndices[0];
        sm = _metricDataObjects.AvailableMetrics[key];
        IsSpecificMetricFound = true;
      }
      else
      {
        if (lvIncludedMetrics.SelectedIndices.Count > 0)
        {
          int key = lvIncludedMetrics.SelectedIndices[0];
          sm = _metricGraphConfig.IncludedMetrics[key];
          IsSpecificMetricFound = true;
        }
      }

      if (IsSpecificMetricFound)
      {
        MetricQueryParms parms = new MetricQueryParms();

        if (cboEnvironment.SelectedIndex > 0)
        {
          parms.IsEnvironmentIDSpecified = true;
          parms.EnvironmentID = envID[cboEnvironment.SelectedIndex];
        }

        if (cboTargetSystem.SelectedIndex > 0)
        {
          parms.IsTargetSystemIDSpecified = true;
          parms.TargetSystemID = systemID[cboTargetSystem.SelectedIndex];
        }

        if (cboTargetApplication.SelectedIndex > 0)
        {
          parms.IsTargetApplicationIDSpecified = true;
          parms.TargetApplicationID = applicationID[cboTargetApplication.SelectedIndex];
        }

        int dataPoints = 0;

        switch (cboDataPoints.SelectedIndex)
        {
          case 0:
            dataPoints = 100;
            break;
          case 1:
            dataPoints = 10;
            break;
          case 2:
            dataPoints = 25;
            break;
          case 3:
            dataPoints = 50;
            break;
          case 4:
            dataPoints = 100;
            break;
          case 5:
            dataPoints = 200;
            break;
          case 6:
            dataPoints = 300;
            break;
          case 7:
            dataPoints = 500;
            break;
          case 8:
            dataPoints = 1000;
            break;
        }


        if (optionFilterByDate.Checked)
        {
          parms.UseMostCurrentMetric = false;
          parms.FromDateTime = dtpFromDate.Value;
          parms.ToDateTime = dtpToDate.Value;
          parms.DataPoints = 0;
        }
        else
        {
          parms.UseMostCurrentMetric = true;
          parms.FromDateTime = DateTime.MinValue;
          parms.ToDateTime = DateTime.MinValue;
          parms.DataPoints = dataPoints;
        }

        frmMetricProperties fMetricProperties = new frmMetricProperties(sm, _metricDataObjects, parms);
        fMetricProperties.ShowDialog();
      }
    }

    private void ckUseEsqlmgmt_CheckedChanged(object sender, EventArgs e)
    {
      _metricGraphConfig.UseESQLMGMT = ckUseEsqlmgmt.Checked;

      if (ckUseEsqlmgmt.Checked)
      { if (SuppressCkEvent)
          return;
        lvAvailableMetrics.Items.Clear();
        GetAvaialbleESQLMGMT_Metrics();
      }
    }

    private void frmGraphProperties_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F12:
          this.ckUseEsqlmgmt.Visible = !this.ckUseEsqlmgmt.Visible;
          break;
      }
    }

    private void GetAvaialbleESQLMGMT_Metrics()
    {
      this.Cursor = Cursors.WaitCursor;
      lvAvailableMetrics.Items.Clear();
      MetricQueryParms parms = new MetricQueryParms();

      if (cboEnvironment.SelectedIndex > 0)
      {
        parms.IsEnvironmentIDSpecified = true;
        parms.EnvironmentID = envID[cboEnvironment.SelectedIndex];
      }

      if (cboTargetSystem.SelectedIndex > 0)
      {
        parms.IsTargetSystemIDSpecified = true;
        parms.TargetSystemID = systemID[cboTargetSystem.SelectedIndex];
      }

      if (cboTargetApplication.SelectedIndex > 0)
      {
        parms.IsTargetApplicationIDSpecified = true;
        parms.TargetApplicationID = applicationID[cboTargetApplication.SelectedIndex];
      }

      if(_metricDataObjects.AvailableMetrics != null)
        _metricDataObjects.AvailableMetrics.Clear();

      _metricDataObjects.GetAvailableESQLMGMT_Metrics(parms);
      amsESQLMGMT = _metricDataObjects.AvailableMetrics;

      LoadAvailableMetrics();
      this.Cursor = Cursors.Arrow;
    }

    private void LoadAvailableMetrics()
    {
      BuildAvailableMetricsList();
      LoadAvailableMetricsDisplayList();
    }

    private void LoadAvailableMetricsDisplayList()
    {
      lvAvailableMetrics.Items.Clear();

      foreach (KeyValuePair<int, SpecificMetric> amKVP in amsDisplay)
      {
        ListViewItem i = new ListViewItem();
        i.Text = _metricDataObjects.Environments[amKVP.Value.EnvironmentID].EnvironmentDesc.Substring(0, 4);
        i.SubItems.Add(_metricDataObjects.MetricTypes[amKVP.Value.MetricTypeID].MetricTypeDesc.Substring(0, 3));
        i.SubItems.Add(_metricDataObjects.Systems[amKVP.Value.TargetSystemID].SystemDesc);
        i.SubItems.Add(_metricDataObjects.Applications[amKVP.Value.TargetApplicationID].ApplicationName);
        i.SubItems.Add(_metricDataObjects.Systems[amKVP.Value.ObserverSystemID].SystemDesc);
        i.SubItems.Add(_metricDataObjects.Applications[amKVP.Value.ObserverApplicationID].ApplicationName);
        i.SubItems.Add(_metricDataObjects.Servers[amKVP.Value.ObserverServerID].ServerDesc);
        i.SubItems.Add(_metricDataObjects.MetricStates[amKVP.Value.MetricStateID].MetricStateDesc + "." +
                       _metricDataObjects.Metrics[amKVP.Value.MetricID].MetricDesc + "." +
                       _metricDataObjects.MetricValueTypes[amKVP.Value.MetricValueTypeID].MetricValueTypeDesc + "." +
                       _metricDataObjects.Intervals[amKVP.Value.IntervalID].IntervalDesc);
        lvAvailableMetrics.Items.Add(i);
      }
    }

    private void BuildAvailableMetricsList()
    {
      string filterByServer = String.Empty;

      amsDisplay = new AvailableMetricSet(_metricDataObjects.Data);
      AvailableMetricSet amsTemp = new AvailableMetricSet(_metricDataObjects.Data);
      if (ckUseEsqlmgmt.Checked)
        amsTemp = amsESQLMGMT;
      else
        amsTemp = ams;

      int[] filterItem = new int[0];
      availableMetricIndex = new List<int>();

      // if we are using a filter - get the filter (itemFilter) and filter the list
      if (cboListFilterByMetric.SelectedIndex > 0)
      {
        filterItem = listItemKeys[cboListFilterByMetric.SelectedIndex - 1];
      }
      else // else recreate the list of filters...
      {
        listItemDesc = new List<string>();
        listItemKeys = new List<int[]>();
      }

      // if we are using a filter on the obsv server desc - get the filter and filter the list
      if (cboListFilterObsvSvr.SelectedIndex > 0)
        filterByServer = cboListFilterObsvSvr.SelectedItem.ToString();

      int index = -1;
      foreach (KeyValuePair<int, SpecificMetric> amKVP in amsTemp)
      {
        index++;
        // if we are using a metric filter, filter the list
        if (cboListFilterByMetric.SelectedIndex > 0)
        {
          int metricStateID = amKVP.Value.MetricStateID;
          int metricID = amKVP.Value.MetricID;
          int metricValueTypeID = amKVP.Value.MetricValueTypeID;
          int intervalID = amKVP.Value.IntervalID;
          // if we are also filtering by server filter by both metric and server
          if (cboListFilterObsvSvr.SelectedIndex > 0)
          {
            if (metricStateID == filterItem[0] && metricID == filterItem[1]
                && metricValueTypeID == filterItem[2] && intervalID == filterItem[3]
                && _metricDataObjects.Servers[amKVP.Value.ObserverServerID].ServerDesc == filterByServer)
            {
              availableMetricIndex.Add(index);
              amsDisplay.Add(amKVP.Key, amKVP.Value);
            }
          }
          else // we're just filtering by metric
          {
            if (metricStateID == filterItem[0] && metricID == filterItem[1]
                && metricValueTypeID == filterItem[2] && intervalID == filterItem[3])
            {
              availableMetricIndex.Add(index);
              amsDisplay.Add(amKVP.Key, amKVP.Value);
            }
          }
        }
        else // if not filtering by metric
        { // we may be filtering by server
          if (cboListFilterObsvSvr.SelectedIndex > 0)
          {
            if (_metricDataObjects.Servers[amKVP.Value.ObserverServerID].ServerDesc == filterByServer)
            {
              availableMetricIndex.Add(index);
              amsDisplay.Add(amKVP.Key, amKVP.Value);
            }
          }
          else  // no list filters... just add it in
          {
            availableMetricIndex.Add(index);
            amsDisplay.Add(amKVP.Key, amKVP.Value);
          }
        }

        // if we are not using a filter - build the data for the list filter combo boxes
        if (cboListFilterByMetric.SelectedIndex < 1)
        {
          int[] entry = new int[4];
          entry[0] = amKVP.Value.MetricStateID;
          entry[1] = amKVP.Value.MetricID;
          entry[2] = amKVP.Value.MetricValueTypeID;
          entry[3] = amKVP.Value.IntervalID;

          if (!IsAlreadyInListFilter(entry))
          {
            listItemDesc.Add(_metricDataObjects.MetricStates[entry[0]].MetricStateDesc + "." +
                             _metricDataObjects.Metrics[entry[1]].MetricDesc + "." +
                             _metricDataObjects.MetricValueTypes[entry[2]].MetricValueTypeDesc + "." +
                             _metricDataObjects.Intervals[entry[3]].IntervalDesc);

            listItemKeys.Add(entry);
          }

          string obsvServer = _metricDataObjects.Servers[amKVP.Value.ObserverServerID].ServerDesc;
          if (!listObsvServers.Contains(obsvServer))
            listObsvServers.Add(obsvServer);
        }
      }

      // if we are not using a filter - update the filter list
      if (cboListFilterByMetric.SelectedIndex < 1 && cboListFilterObsvSvr.SelectedIndex < 1)
      {
        cboListFilterByMetric.Items.Clear();
        cboListFilterByMetric.Items.Add("--------");
        foreach (string desc in listItemDesc)
        {
          cboListFilterByMetric.Items.Add(desc);
        }

        cboListFilterObsvSvr.Items.Clear();
        cboListFilterObsvSvr.Items.Add("--------");
        foreach (string server in listObsvServers)
        {
          cboListFilterObsvSvr.Items.Add(server);
        }
      }

    }

    private bool IsAlreadyInListFilter(int[] newEntry)
    {
      foreach(int[] entry in listItemKeys)
      {
        if (newEntry[0] == entry[0] && newEntry[1] == entry[1] && newEntry[2] == entry[2] && newEntry[3] == entry[3])
          return true;
      }
      return false;
    }

    private void cboListFilterByMetric_SelectedIndexChanged(object sender, EventArgs e)
    {
      LoadAvailableMetrics();
    }

    private void btnRemoveAllFromGraph_Click(object sender, EventArgs e)
    {
      IncludedMetricSet ms = new IncludedMetricSet();
      _metricGraphConfig.IncludedMetrics = ms;

      lvIncludedMetrics.Items.Clear();

      SetYAxisLabel();
      LoadIncludedMetrics();
    }

    private void mnuContextListViewRollUp_Click(object sender, EventArgs e)
    {
      SpecificMetric sm = new SpecificMetric();

      if (lvIncludedMetrics.SelectedIndices.Count > 0)
      {
        int key = lvIncludedMetrics.SelectedIndices[0];
        sm = _metricGraphConfig.IncludedMetrics[key];
      }
      else
        return;

      if (mnuContextListViewRollUp.Text == "Cancel Rollup to Hourly")
      {
        sm.RollUpToHourly = false;
        mnuContextListViewRollUp.Text = "Rollup to Hourly";
      }
      else
      {
        sm.RollUpToHourly = true;
        mnuContextListViewRollUp.Text = "Cancel Rollup to Hourly";
      }
    }
  }
}