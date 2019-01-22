using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Teleflora.Operations.MetricView
{
    public partial class frmMetricProperties : Form
    {
        private SpecificMetric _sm;
        private MetricDataObjects _dataObjects;
        private string holdLegendLabel = String.Empty;
        private bool IsSQLCommandShown = false;
        private MetricQueryParms _parms;

        public frmMetricProperties(SpecificMetric sm, MetricDataObjects dataObjects, MetricQueryParms parms)
        {
            _sm = sm;
            _dataObjects = dataObjects;
            _parms = parms;

            InitializeComponent();
            InitializeForm();

            txtGraphLegendLabel.ReadOnly = true;
        }

        private void InitializeForm()
        {
            lblEnvironmentID.Text = _sm.EnvironmentID.ToString();
            lblEnvironmentDesc.Text = _dataObjects.Environments[_sm.EnvironmentID].EnvironmentDesc;

            lblMetricTypeID.Text = _sm.MetricTypeID.ToString();
            lblMetricTypeDesc.Text = _dataObjects.MetricTypes[_sm.MetricTypeID].MetricTypeDesc;

            lblTargetSystemID.Text = _sm.TargetSystemID.ToString();
            lblTargetSystemDesc.Text = _dataObjects.Systems[_sm.TargetSystemID].SystemDesc;

            lblTargetApplicationID.Text = _sm.TargetApplicationID.ToString();
            lblTargetApplicationDesc.Text = _dataObjects.Applications[_sm.TargetApplicationID].ApplicationName;

            lblMetricStateID.Text = _sm.MetricStateID.ToString();
            lblMetricStateDesc.Text = _dataObjects.MetricStates[_sm.MetricStateID].MetricStateDesc;

            lblMetricID.Text = _sm.MetricID.ToString();
            lblMetricDesc.Text = _dataObjects.Metrics[_sm.MetricID].MetricDesc;

            lblAggregateTypeID.Text = _sm.AggregateTypeID.ToString();
            lblAggregateTypeDesc.Text = _dataObjects.AggregateTypes[_sm.AggregateTypeID].AggregateTypeDesc;

            lblMetricValueTypeID.Text = _sm.MetricValueTypeID.ToString();
            lblMetricValueTypeDesc.Text = _dataObjects.MetricValueTypes[_sm.MetricValueTypeID].MetricValueTypeDesc;

            lblIntervalTypeID.Text = _sm.IntervalID.ToString();
            lblIntervalTypeDesc.Text = _dataObjects.Intervals[_sm.IntervalID].IntervalDesc;

            lblObserverSystemID.Text = _sm.ObserverSystemID.ToString();
            lblObserverSystemDesc.Text = _dataObjects.Systems[_sm.ObserverSystemID].SystemDesc;

            lblObserverApplicationID.Text = _sm.ObserverApplicationID.ToString();
            lblObserverApplicationDesc.Text = _dataObjects.Applications[_sm.ObserverApplicationID].ApplicationName;

            lblObserverServerID.Text = _sm.ObserverServerID.ToString();
            lblObserverServerDesc.Text = _dataObjects.Servers[_sm.ObserverServerID].ServerDesc;

            holdLegendLabel = _sm.LegendLabel;
            txtGraphLegendLabel.Text = _sm.LegendLabel;

            cboGraphLabelStyles.SelectedIndex = _sm.LegendLabelNumber;

            ckForecastFromFile.Checked = _sm.IsMetricFromFile;
            ckUseYOYData.Checked = _sm.UseYOYData;
            lblForecastFileName.Text = _sm.MetricFileName;
            txtMultiplier.Text = _sm.Multiplier.ToString();

            btnGetFile.Enabled = ckForecastFromFile.Checked;
            

            this.Size = new Size(542, 684);
            txtSQLCommand.Text = String.Empty;
            txtSQLCommand.Visible = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidInput())
            {
                UpdateProperties();
                this.Close();
            }
        }

        private bool ValidInput()
        {
            if (txtGraphLegendLabel.Text.Trim().Length == 0)
            {
                MessageBox.Show("Legend label cannot be blank - please coorect.", "Metric Properties",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (ckForecastFromFile.Checked && lblForecastFileName.Text.Trim().Length < 1)
            {
                MessageBox.Show("A file must be selected as a source for this forecast metric.", "Metric Properties",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!GeneralUtility.IsNumeric(txtMultiplier.Text, true))
            {
                MessageBox.Show("The multiplier must be a valid number.  It may contain a decimal point.", "Metric Properties",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMultiplier.Select();
                txtMultiplier.Focus();
                return false;
            }
            
            return true;
        }

        private void UpdateProperties()
        {
            _sm.LegendLabel = txtGraphLegendLabel.Text.Trim();
            _sm.LegendLabelNumber = cboGraphLabelStyles.SelectedIndex;

            _sm.IsMetricFromFile = ckForecastFromFile.Checked;
            _sm.MetricFileName = lblForecastFileName.Text;
            _sm.UseYOYData = ckUseYOYData.Checked;
            _sm.Multiplier = (float) Decimal.Parse(txtMultiplier.Text);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboGraphLabelStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboGraphLabelStyles.SelectedIndex)
            {
                case 0:
                    txtGraphLegendLabel.Text = holdLegendLabel;
                    txtGraphLegendLabel.ReadOnly = false;
                    break;

                case 1:
                    txtGraphLegendLabel.Text = _dataObjects.Systems[_sm.TargetSystemID].SystemDesc + " - " +
                            _dataObjects.Applications[_sm.TargetApplicationID].ApplicationName + " - " +
                            _dataObjects.Metrics[_sm.MetricID].MetricDesc;
                    txtGraphLegendLabel.ReadOnly = false;
                    break;

                case 2:
                    txtGraphLegendLabel.Text = _dataObjects.Systems[_sm.TargetSystemID].SystemDesc + " - " +
                            _dataObjects.Servers[_sm.ObserverServerID].ServerDesc + " - " +
                            _dataObjects.Metrics[_sm.MetricID].MetricDesc;
                    txtGraphLegendLabel.ReadOnly = false;
                    break;

                case 3:
                    txtGraphLegendLabel.Text = _dataObjects.Applications[_sm.TargetApplicationID].ApplicationName + " - " +
                            _dataObjects.Metrics[_sm.MetricID].MetricDesc;
                    txtGraphLegendLabel.ReadOnly = false;
                    break;

                case 4:
                    txtGraphLegendLabel.ReadOnly = false;
                    txtGraphLegendLabel.Select();
                    txtGraphLegendLabel.Focus();
                    break;

            }         
        }

        private void frmMetricProperties_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    if (IsSQLCommandShown)
                    {
                        this.Size = new Size(542, 684);
                        txtSQLCommand.Text = String.Empty;
                        txtSQLCommand.Visible = false;
                        IsSQLCommandShown = false;
                    }
                    else
                    {
                        this.Size = new Size(850, 684);
                        txtSQLCommand.Text = GetSQLStatement();
                        txtSQLCommand.Visible = true;
                        IsSQLCommandShown = true;
                    }
                    break;
            }
        }

        private string GetSQLStatement()
        {
            StringBuilder sb = new StringBuilder();

            if (_parms.UseMostCurrentMetric)
            {
                sb.Append(" select top " + _parms.DataPoints.ToString().Trim() + " ");
            }
            else
            {
                sb.Append(" select ");
            }

            sb.Append("MetricObservedID, " +
                    "ReceivedFromObserverDateTime, " +
                    "Observer_SystemID, " +
                    "Observer_ApplicationID, " +
                    "Observer_ServerID, " +
                    "Target_SystemID, " +
                    "Target_ApplicationID, " +
                    "EnvironmentID, " +
                    "AggregateTypeID, " +
                    "MetricID, " +
                    "MetricStateID, " +
                    "MetricTypeID, " +
                    "MetricValueTypeID, " +
                    "IntervalID, " +
                    "MetricValue, " +
                    "MetricCapturedDateTime, " +
                    "MetricDuration " +
                "from tblMetricObserved " +
                "where " +
                    "EnvironmentID = " + _sm.EnvironmentID.ToString().Trim() + " and " +
                    "Target_SystemID = " + _sm.TargetSystemID.ToString().Trim() + " and " +
                    "Target_ApplicationID = " + _sm.TargetApplicationID.ToString().Trim() + " and " +
                    "Observer_SystemID = " + _sm.ObserverSystemID.ToString().Trim() + " and " +
                    "Observer_ApplicationID = " + _sm.ObserverApplicationID.ToString().Trim() + " and " +
                    "Observer_ServerID = " + _sm.ObserverServerID.ToString().Trim() + " and " +
                    "MetricTypeID = " + _sm.MetricTypeID.ToString().Trim() + " and " +
                    "MetricStateID = " + _sm.MetricStateID.ToString().Trim() + " and " +
                    "MetricID = " + _sm.MetricID.ToString().Trim() + " and " +
                    "AggregateTypeID = " + _sm.AggregateTypeID.ToString().Trim() + " and " +
                    "MetricValueTypeID = " + _sm.MetricValueTypeID.ToString().Trim() + " and " +
                    "IntervalID = " + _sm.IntervalID.ToString().Trim() + " ");

            if (!_parms.UseMostCurrentMetric)
            {
                sb.Append(" and MetricCapturedDateTime >= '" +
                    _parms.FromDateTime.ToString("yyyy-MM-dd") + "T" + _parms.FromDateTime.ToString("HH:mm:ss.fff") +
                    "' and MetricCapturedDateTime <= '" +
                    _parms.ToDateTime.ToString("yyyy-MM-dd") + "T" + _parms.ToDateTime.ToString("HH:mm:ss.fff") + "' ");
                sb.Append(" order by MetricCapturedDateTime ");
            }
            else
            {
                sb.Append(" order by MetricCapturedDateTime desc ");
            }


            return sb.ToString();
        }

        private void ckForecastFromFile_CheckedChanged(object sender, EventArgs e)
        {
            if (ckForecastFromFile.Checked)
            {
                btnGetFile.Enabled = true;
            }
            else
            {
                btnGetFile.Enabled = false;
                lblForecastFileName.Text = String.Empty;
            }
        }

        private void btnGetFile_Click(object sender, EventArgs e)
        {
            dlgFileOpen.InitialDirectory = @"C:\Program Files\MetricView1.0";
            if (dlgFileOpen.ShowDialog() == DialogResult.OK)
                lblForecastFileName.Text = dlgFileOpen.FileName;
        }
    }
}