using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Teleflora.Operations.MetricView
{
  public partial class frmMetricData : Form
  {
    MetricObservationSetCollection _msc;
    MetricGraphConfiguration _metricGraphConfig;
    MetricDataObjects _dataObjects;

    public frmMetricData(MetricObservationSetCollection msc, MetricDataObjects dataObjects, MetricGraphConfiguration metricGraphConfig)
    {
      this.Cursor = Cursors.Arrow;
      InitializeComponent();
      _msc = msc;
      _metricGraphConfig = metricGraphConfig;
      _dataObjects = dataObjects;

      btnTextReport.Enabled = false;
      btnCommaSeparated.Enabled = true;
      txtGraphData.Text = GetGraphData();
      txtGraphData.Select(0, 0);
    }


    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnCommaSeparated_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      txtGraphData.Text = GetGraphDataCSV();
      txtGraphData.Select(0, 0);
      this.Cursor = Cursors.Arrow;

      btnTextReport.Enabled = true;
      btnCommaSeparated.Enabled = false;
    }

    private string GetGraphData()
    {
      StringBuilder sb = new StringBuilder();

      if (_msc.Count == 0)
        return "No metric data";

      MetricObservationSet ms = _msc.Values[0];
      if (ms.Count == 0)
        return "No metric data";

      sb.Append(GeneralUtility.StripSeqFromName(_metricGraphConfig.GraphName) + "\r\n");
      sb.Append("Metrics from " + _msc[0].Values[0].MetricCapturedDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "  to  " +
                _msc[0].Values[_msc[0].Values.Count - 1].MetricCapturedDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
      sb.Append(_dataObjects.Metrics[_msc[0].Values[0].MetricID].MetricDesc + "\r\n");

      if (_msc.UseMostCurrentMetric)
        sb.Append(_msc.DataPoints.ToString() + " Most Current Observations as of " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
      else
        sb.Append("From " + _msc.FromDateTime.ToString("MM/dd/yyyy HH:mm:ss") + " to " + _msc.ToDateTime.ToString());

      sb.Append("\r\n\r\n");

      // until we do the alignment of metric sets, we must be sure the points array is not jagged
      int numberOfPoints = 999999;
      for (int mscPtr = 0; mscPtr < _msc.Count; mscPtr++)
      {
        if (_msc[mscPtr].Count < numberOfPoints)
          numberOfPoints = _msc[mscPtr].Count;
      }

      sb.Append("Columns\r\n\r\n");
      for (int j = 0; j < _msc.Count; j++)
      {
        sb.Append((j + 1).ToString() + " - " + _msc[j].LegendLabel + "\r\n");
      }
      sb.Append("\r\n");
      sb.Append("        ");

      for (int j = 0; j < _msc.Count; j++)
      {
        sb.Append("-------------(" + (j + 1).ToString() + ")-------------      ");
      }
      sb.Append("\r\n");

      for (int i = 0; i < numberOfPoints; i++)
      {
        sb.Append("[" + (i + 1).ToString("0000") + "]- ");
        for (int j = 0; j < _msc.Count; j++)
        {
          DateTime metricDateTime = _msc[j].Values[i].MetricCapturedDateTime;
          float metricValue = _msc[j].Values[i].MetricValue;
          sb.Append(metricDateTime.ToString("yyyyMMdd HH:mm:ss") + "  ");
          string value = "            " + metricValue.ToString("0000.0000");
          sb.Append(value.Substring(value.Length - 10, 10) + "      ");
        }
        sb.Append("\r\n");
      }
      return sb.ToString();
    }

    private string GetGraphDataCSV()
    {
      StringBuilder sb = new StringBuilder();

      if (_msc.Count == 0)
        return "No metric data";

      MetricObservationSet ms = _msc.Values[0];
      if (ms.Count == 0)
        return "No metric data";

      // until we do the alignment of metric sets, we must be sure the points array is not jagged
      int numberOfPoints = 999999;
      for (int mscPtr = 0; mscPtr < _msc.Count; mscPtr++)
      {
        if (_msc[mscPtr].Count < numberOfPoints)
          numberOfPoints = _msc[mscPtr].Count;
      }

      for (int j = 0; j < _msc.Count; j++)
      {
        if (j == 0)
          sb.Append("\"Observation\",");
        if (j > 0)
          sb.Append(",");
        sb.Append("\"" + _msc[j].LegendLabel + "\"");
      }
      sb.Append("\r\n");

      for (int i = 0; i < numberOfPoints; i++)
      {
        sb.Append("\"" + (i + 1).ToString("0000") + "\",");
        for (int j = 0; j < _msc.Count; j++)
        {
          if (j > 0)
            sb.Append(",");
          DateTime metricDateTime = _msc[j].Values[i].MetricCapturedDateTime;
          float metricValue = _msc[j].Values[i].MetricValue;
          sb.Append("\"" + metricDateTime.ToString("yyyyMMdd HH:mm:ss") + "\",");
          string value = metricValue.ToString("0000.0000");
          sb.Append("\"" + value + "\"");
        }
        sb.Append("\r\n");
      }
      return sb.ToString();
    }

    private void btnTextReport_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      txtGraphData.Text = GetGraphData();
      txtGraphData.Select(0, 0);
      this.Cursor = Cursors.Arrow;
      btnTextReport.Enabled = false;
      btnCommaSeparated.Enabled = true;
    }

  }
}