using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Teleflora.Operations.MetricView
{
  public partial class frmMetrics : Form
  {
    private MetricDataObjects _metricDataObjects;
    AvailableMetricSet ams;
    MetricObservationSet mos;
    DateTime beginDateTime = DateTime.MinValue;
    DateTime endDateTime = DateTime.MaxValue;

    struct MetricObsv
    {
      public DateTime IntervalDT;
      public bool IsPresent;
      public Rectangle rect;
      public string Text;
    }

    SortedList<DateTime, MetricObsv> metrics;

    private Image graphImage = new Bitmap(1400, 1850);
    private int countPerSecond = 20;
    private float lm = 25F;
    private float tm = 25F;
    private float bm = 25F;
    private float boxInterval = 37F;
    private float boxHeight;

    private int[] defaultServerIDs = new int[] { 1, 2, 3, 4, 5, 6, 10, 11, 35, 40,
        41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 57, 58,
        59, 61, 62, 63, 64, 65, 66, 77, 78, 82, 83, 84, 85,
        86, 87, 91, 92, 93, 94
                                               };

    private bool SuppressStatusUpdate = false;

    public frmMetrics(MetricDataObjects dataObjects)
    {
      InitializeComponent();

      _metricDataObjects = dataObjects;
      InitializeForm();
    }

    private void InitializeForm()
    {
      LoadServers();
      InitializeDates();
      pnlMetricsDetail.Visible = false;
    }

    private void LoadServers()
    {
      lvServers.Clear();
      lvServers.CheckBoxes = true;

      ColumnHeader ch = new ColumnHeader();
      ch.Text = "Server";
      ch.Width = 200;
      ch.Name = "Server";
      lvServers.Columns.Add(ch);

      ch = new ColumnHeader();
      ch.Text = "ID";
      ch.Width = 30;
      ch.Name = "ID";
      lvServers.Columns.Add(ch);

      foreach(KeyValuePair<int, Server> kvpServers in _metricDataObjects.Servers)
      {
        ListViewItem i = new ListViewItem();
        i.Text = kvpServers.Value.ServerDesc;
        i.SubItems.Add(kvpServers.Value.ServerID.ToString());
        for (int j = 0; j < defaultServerIDs.Length; j++)
          if (kvpServers.Value.ServerID == defaultServerIDs[j])
            i.Checked = true;
        lvServers.Items.Add(i);
      }

      UpdateServerStatus();
    }

    private void InitializeDates()
    {
      dtpToDate.Value = DateTime.Parse(DateTime.Now.AddDays(1).ToString("MM/dd/yyyy"));
      dtpFromDate.Value = dtpToDate.Value.AddDays(-1);
      beginDateTime = dtpFromDate.Value;
      endDateTime = dtpToDate.Value;
    }


    private void mnuFileExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void lvServers_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
      if (SuppressStatusUpdate)
        return;
      UpdateServerStatus();
    }

    private void btnCheckAll_Click(object sender, EventArgs e)
    {
      SuppressStatusUpdate = true;
      foreach (ListViewItem i in lvServers.Items)
        i.Checked = true;
      SuppressStatusUpdate = false;
      UpdateServerStatus();
    }

    private void btnClearAll_Click(object sender, EventArgs e)
    {
      SuppressStatusUpdate = true;
      foreach (ListViewItem i in lvServers.Items)
        i.Checked = false;
      SuppressStatusUpdate = false;
      UpdateServerStatus();
    }

    private void UpdateServerStatus()
    {
      if (lvServers.CheckedItems.Count == 1)
        lblServersIncludedStatus.Text = "1 Server Included";
      else
        lblServersIncludedStatus.Text = lvServers.CheckedItems.Count.ToString() + " Servers Included";

      if (lvServers.CheckedItems.Count == 0)
        btnGetMetrics.Enabled = false;
      else
        btnGetMetrics.Enabled = true;
    }

    private void frmMetrics_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.D:
          if (e.Control)
            pnlMetricsDetail.Visible = !pnlMetricsDetail.Visible;
          break;

        case Keys.A:
          pnlDetailMain.Size = new Size(2500, 2500);
          break;

        case Keys.B:
          pbMetricDetail.Size = new Size(2300, 2300);
          break;


      }
    }

    private void btnGetMetrics_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      ams = new AvailableMetricSet(_metricDataObjects.Data);
      MetricQueryParms parms = new MetricQueryParms();
      parms.EnvironmentID = 1;
      parms.IsEnvironmentIDSpecified = true;
      parms.FromDateTime = dtpFromDate.Value;
      parms.ToDateTime = dtpToDate.Value;
      parms.IsLimitedByDates = true;
      parms.ServerList = GetServerList();
      parms.IsServerListSpecified = true;
      ams.PopulateAvailableMetricsList(parms);

      lvMetrics.Clear();
      lvMetrics.View = View.Details;

      ColumnHeader ch = new ColumnHeader();
      ch.Name = "Server";
      ch.Text = "Server";
      ch.Width = 105;
      lvMetrics.Columns.Add(ch);

      ch = new ColumnHeader();
      ch.Name = "Metric";
      ch.Text = "State/Metric/ValueType/Interval";
      ch.Width = 300;
      lvMetrics.Columns.Add(ch);

      ch = new ColumnHeader();
      ch.Name = "Count";
      ch.Text = "Count";
      ch.Width = 65;
      ch.TextAlign = HorizontalAlignment.Right;
      lvMetrics.Columns.Add(ch);

      int count = 0;
      foreach (KeyValuePair<int, SpecificMetric> kvpSM in ams)
      {
        ListViewItem i = new ListViewItem();
        i.Text = _metricDataObjects.Servers[kvpSM.Value.ObserverServerID].ServerDesc;
        i.SubItems.Add(_metricDataObjects.MetricStates[kvpSM.Value.MetricStateID].MetricStateDesc + "." +
                       _metricDataObjects.Metrics[kvpSM.Value.MetricID].MetricDesc + "." +
                       _metricDataObjects.MetricValueTypes[kvpSM.Value.MetricValueTypeID].MetricValueTypeDesc + "." +
                       _metricDataObjects.Intervals[kvpSM.Value.IntervalID].IntervalDesc);
        i.SubItems.Add(kvpSM.Value.Count.ToString("##,###,##0"));
        i.Tag = count++;
        lvMetrics.Items.Add(i);
      }

      this.Cursor = Cursors.Arrow;
    }

    private string GetServerList()
    {
      StringBuilder sb = new StringBuilder();
      foreach (ListViewItem i in lvServers.CheckedItems)
      {
        if (sb.ToString().Length > 0)
        {
          sb.Append(",");
        }
        sb.Append(i.SubItems[1].Text);
      }
      return sb.ToString();
    }

    private void lvMetrics_DoubleClick(object sender, EventArgs e)
    {
      if (lvMetrics.SelectedItems.Count < 1)
        return;

      int index = Convert.ToInt32(lvMetrics.SelectedItems[0].Tag);
      ShowMetricDetail(index);
      lblMetricDescription.Text = "Metric number " + index.ToString();
      lblMetricDetail.Text = "Metric Detail";
      tabMain.SelectedTab = tabMetricDetail;
    }

    private void ShowMetricDetail(int index)
    {
      SpecificMetric sm = ams.Values[index];

      lblMetricDetailHeader.Text =
        "Server:    " + _metricDataObjects.Servers[sm.ObserverServerID].ServerDesc + "  Metric: " +
        _metricDataObjects.MetricStates[ams.Values[index].MetricStateID].MetricStateDesc + "." +
        _metricDataObjects.Metrics[ams.Values[index].MetricID].MetricDesc + "." +
        _metricDataObjects.MetricValueTypes[ams.Values[index].MetricValueTypeID].MetricValueTypeDesc + "." +
        _metricDataObjects.Intervals[ams.Values[index].IntervalID].IntervalDesc;

      mos = new MetricObservationSet(_metricDataObjects.Data);
      MetricQueryParms parms = new MetricQueryParms();
      parms.EnvironmentID = sm.EnvironmentID;
      parms.FromDateTime = dtpFromDate.Value;
      parms.ToDateTime = dtpToDate.Value;
      parms.IsLimitedByDates = true;

      mos.PopulateMetricObservations(sm, parms);

      PopulateMetrics();

      lblMetricDetailHeader.Text += "\r\nRecords = " + mos.Count.ToString();

      Size graphSize = new Size(1400, Convert.ToInt32(tm + (6 * (boxInterval + (5 * countPerSecond))) + bm));

      DrawImage(graphSize);
      pbMetricDetail.Size = graphSize;
      pbMetricDetail.Invalidate();

      pbMetricDetail.Invalidate();
    }

    private void DrawImage(Size graphSize)
    {
      graphImage = new Bitmap(graphSize.Width, graphSize.Height);
      Graphics gr = Graphics.FromImage(graphImage);
      DrawGrid(gr);
      //DrawMetrics(gr);
      gr.Dispose();
    }

    private void DrawGrid(Graphics gr)
    {
      boxHeight = 5F * countPerSecond;

      Font smallFont = new Font("Arial", 8, FontStyle.Bold);
      Font smallerFont = new Font("Arial", 7);
      Pen redPen = new Pen(Brushes.Red, 1F);
      Pen bluePen = new Pen(Brushes.Blue, 1F);

      for (int i = 0; i < 60; i++)
      {
        gr.DrawRectangle(Pens.Black, lm + i * 15, tm, 10, 10);
      }


    }


    //private void DrawMetrics(Graphics gr)
    //{
    //    StringBuilder sb = new StringBuilder();

    //    boxHeight = 5F * countPerSecond;

    //    Font smallFont = new Font("Arial", 8, FontStyle.Bold);
    //    Font smallerFont = new Font("Arial", 7);
    //    Pen redPen = new Pen(Brushes.Red, 1F);
    //    Pen bluePen = new Pen(Brushes.Blue, 1F);
    //    Pen grayPen = new Pen(Brushes.DarkGray, 1F);
    //    Pen magentaPen = new Pen(Brushes.Magenta, 1F);

    //    foreach (KeyValuePair<int, Plot> kvpPlot in ps)
    //    {
    //        int timeInteger = kvpPlot.Value.TimeInteger;
    //        int minutes = timeInteger / (60 * countPerSecond);
    //        int seconds = (timeInteger - (minutes * (60 * countPerSecond))) / countPerSecond;
    //        int seq = (timeInteger - ((minutes * (60 * countPerSecond)) + (seconds * countPerSecond)));

    //        int boxNumber = (minutes / 10);
    //        int minuteBox = minutes - (boxNumber * 10);
    //        int quarterMinuteBox = seconds / 15;

    //        float plotBottom = tm + ((boxNumber + 1) * boxHeight) + (boxNumber * boxInterval) - 2 - (seq * 5);
    //        float plotTop = plotBottom - plotHeight;
    //        float plotX = lm + (minuteBox * 128) + (quarterMinuteBox * 32) + (seconds - (quarterMinuteBox * 15) + 1) * 2;

    //        switch (kvpPlot.Value.PlotCode)
    //        {
    //            case 400:
    //                gr.DrawLine(redPen, plotX, plotBottom, plotX, plotTop);
    //                break;

    //            case 500:
    //                gr.DrawLine(bluePen, plotX, plotBottom, plotX, plotTop);
    //                break;

    //            case 200:
    //                gr.DrawLine(grayPen, plotX, plotBottom, plotX, plotTop);
    //                break;

    //            default:
    //                gr.DrawLine(magentaPen, plotX, plotBottom, plotX, plotTop);
    //                break;
    //        }

    //        sb.Append("\r\n" + kvpPlot.Value.PlotDateTime.ToString("yyyy-MM-dd HH:mm:ss") + " duration=" + kvpPlot.Value.PlotResponseTime.ToString("0.000"));

    //    }

    //    txtOut.Text = sb.ToString();
    //}

    private void pbMetricDetail_Paint(object sender, PaintEventArgs e)
    {
      Graphics gr = e.Graphics;
      gr.DrawImage(graphImage, 0, 0);
    }

    private void PopulateMetrics()
    {

      metrics = new SortedList<DateTime, MetricObsv>();

      if (ams.Count < 1)
        return;

      switch (ams[0].IntervalID)
      {
        case 10:
          DateTime workBeginDate = DateTime.Parse(beginDateTime.ToString(@"MM/dd/yy HH"));
          DateTime workEndDate = DateTime.Parse(endDateTime.ToString(@"MM/dd/yy HH")).AddHours(1);
          DateTime workDate = workBeginDate;
          while (workDate <= workEndDate)
          {
            MetricObsv mo = new MetricObsv();
            mo.IntervalDT = workDate;
            mo.IsPresent = false;
            metrics.Add(mo.IntervalDT, mo);
            workDate.AddSeconds(15);
          }
          break;

      }



      foreach (KeyValuePair<int, SpecificMetric> kvpMO in ams)
      {


      }

    }

  }
}