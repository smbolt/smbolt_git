using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ChartDirector;

namespace Teleflora.Operations.MetricView
{
  public partial class MetricGraph : UserControl
  {
    public delegate void MenuActionEventHandler(string graphName, string menuAction);
    public event MenuActionEventHandler MenuAction;

    public delegate void SaveImageToFileEventHandler(string graphName, Image img);
    public event SaveImageToFileEventHandler SaveImageToFile;

    public delegate void UpdateConfigLocationAndSize(MetricGraphConfiguration metricGraphConfig);
    public event UpdateConfigLocationAndSize UpdateConfigLocAndSize;

    public delegate void WinHotSpotEventHandler(string graph, string metric, string time, string value);
    public event WinHotSpotEventHandler GraphValue;


    private MetricDataObjects _dataObjects;
    public MetricDataObjects DataObjects
    {
      get {
        return _dataObjects;
      }
      set {
        _dataObjects = value;
      }
    }

    private bool _isSuspended = false;
    public bool IsSuspended
    {
      get {
        return _isSuspended;
      }
      set {
        _isSuspended = value;
      }
    }

    private bool _isRollup;
    public bool IsRollup
    {
      get {
        return _isRollup;
      }
      set {
        _isRollup = value;
      }
    }

    private bool IsSQLConnectionLost = false;
    private bool IsMaximized = false;
    private bool IsFrontAndCenter = false;
    private bool UseSpline = false;
    private int restoreLeft = 0;
    private int restoreTop = 0;
    private int restoreWidth = 600;
    private int restoreHeight = 300;

    private int[] graphColors = new int[12];

    private bool IsResizing = false;
    private Timer timer;
    MetricObservationSetCollection msc;

    // forms controls
    Label lblTitle;
    //Pego g;

    ChartDirector.XYChart g;
    WinChartViewer viewer;

    private MetricGraphConfiguration _metricGraphConfig;
    public MetricGraphConfiguration MetricGraphConfig
    {
      get {
        return _metricGraphConfig;
      }
      set
      {
        _metricGraphConfig = value;
        this.Left = _metricGraphConfig.GraphLocation.X;
        this.Top = _metricGraphConfig.GraphLocation.Y;
        this.Width = _metricGraphConfig.GraphSize.Width;
        this.Height = _metricGraphConfig.GraphSize.Height;
        this._graphName = _metricGraphConfig.GraphName;
        this.lblTitle.Text = GeneralUtility.StripSeqFromName(_metricGraphConfig.GraphName);

        SetSelectionAndActivation();
      }
    }

    private bool IsMoveOrResizeStarted;
    private bool IsCursorPositionedForResizingWidth;
    private bool IsCursorPositionedForResizingHeight;
    private bool IsResizingWidth;
    private bool IsResizingHeight;
    private int prevMouseXPos;
    private int prevMouseYPos;
    private int currMouseXPos;
    private int currMouseYPos;

    private string _graphName;
    public string GraphName
    {
      get {
        return _graphName;
      }
      set
      {
        _graphName = value;
        viewer.Tag = _graphName;
      }
    }

    public MetricGraph(MetricDataObjects dataObjects)
    {
      _dataObjects = dataObjects;
      InitializeComponent();
      viewer = new WinChartViewer();
      InitializeControl();
    }

    private void InitializeControl()
    {
      lblTitle = new Label();
      lblTitle.Size = new Size(200, 14);
      lblTitle.BackColor = Color.Transparent;
      lblTitle.Font = new Font("Tahoma", 8F, FontStyle.Bold);
      lblTitle.ForeColor = Color.White;
      lblTitle.TextAlign = ContentAlignment.TopLeft;
      this.Controls.Add(lblTitle);

      lblTitle.Dock = DockStyle.Top;
      lblTitle.Text = "MetricGraph";
      lblTitle.MouseDown += new MouseEventHandler(MetricGraph_MouseDown);
      lblTitle.MouseUp += new MouseEventHandler(MetricGraph_MouseUp);
      lblTitle.MouseMove += new MouseEventHandler(MetricGraph_MouseMove);
      lblTitle.DoubleClick += new EventHandler(MetricGraph_DoubleClick);
      viewer.MouseEnterHotSpot += new ChartDirector.WinHotSpotEventHandler(viewer_MouseEnterHotSpot);
      viewer.MouseLeaveHotSpot += new ChartDirector.WinHotSpotEventHandler(viewer_MouseLeaveHotSpot);
      viewer.MouseDownHotSpot += new ChartDirector.WinHotSpotEventHandler(viewer_MouseDownHotSpot);
      viewer.MouseUpHotSpot += new ChartDirector.WinHotSpotEventHandler(viewer_MouseUpHotSpot);
      viewer.Click += new EventHandler(viewer_Click);

      graphColors[0] = Chart.CColor(Color.Red);
      graphColors[1] = Chart.CColor(Color.Blue);
      graphColors[2] = Chart.CColor(Color.DarkGreen);
      graphColors[3] = Chart.CColor(Color.Magenta);
      graphColors[4] = Chart.CColor(Color.DarkBlue);
      graphColors[5] = Chart.CColor(Color.Lime);
      graphColors[6] = Chart.CColor(Color.Purple);
      graphColors[7] = Chart.CColor(Color.Pink);
      graphColors[8] = Chart.CColor(Color.Black);
      graphColors[9] = Chart.CColor(Color.Aquamarine);
      graphColors[10] = Chart.CColor(Color.SteelBlue);
      graphColors[11] = Chart.CColor(Color.SpringGreen);

      mnuContextRestorePosition.Visible = false;
      mnuContextMaximize.Visible = true;
      mnuContextFrontAndCenter.Visible = true;

      this.Controls.Add(viewer);
      viewer.Size = new Size(100, 100);
      viewer.Location = new Point(0, 20);
      viewer.Dock = DockStyle.Fill;
      viewer.BorderStyle = BorderStyle.FixedSingle;
      viewer.BringToFront();

      _metricGraphConfig = new MetricGraphConfiguration();

      timer = new Timer();
      timer.Interval = _metricGraphConfig.RefreshInterval;
      timer.Tick +=new EventHandler(timer_Tick);
      SetSelectionAndActivation();
    }



    void viewer_MouseDownHotSpot(object sender, WinHotSpotEventArgs e)
    {
      if (e.AttrValues["title"].ToString().CompareTo("ToolButton") == 0)
      {
        Graphics gr = Graphics.FromImage(viewer.Image);
        gr.DrawImage(Image.FromFile(@"C:\_projects\MetricView\MetricView\images\refresh_down.bmp"), viewer.Width - 19, 1);
        viewer.Invalidate(new Rectangle(viewer.Width - 19, 1, 16, 16));
        gr.Dispose();
        return;
      }
    }

    void viewer_MouseLeaveHotSpot(object sender, WinHotSpotEventArgs e)
    {
      if (e.AttrValues["title"].ToString().CompareTo("ToolButton") == 0)
      {
        Graphics gr = Graphics.FromImage(viewer.Image);
        gr.DrawImage(Image.FromFile(@"C:\_projects\MetricView\MetricView\images\refresh.bmp"), viewer.Width - 19, 1);
        viewer.Invalidate(new Rectangle(viewer.Width - 19, 1, 16, 16));
        gr.Dispose();
        return;
      }
    }

    void viewer_MouseUpHotSpot(object sender, WinHotSpotEventArgs e)
    {
      if (e.AttrValues["title"].ToString().CompareTo("ToolButton") != 0)
        return;



      switch (e.AttrValues["path"].ToString())
      {
        case "BACK":
          break;

        case "BACK_ALL":
          break;

        case "FWD":
          break;

        case "FWD_ALL":
          break;

        case "REFRESH":
          this.RefreshGraph(true);
          break;
      }

      if (e.AttrValues["title"].ToString().CompareTo("ToolButton") == 0)
      {
        Graphics gr = Graphics.FromImage(viewer.Image);
        gr.DrawImage(Image.FromFile(@"C:\_projects\MetricView\MetricView\images\refresh_hot.bmp"), viewer.Width - 19, 1);
        gr.Dispose();
        viewer.Invalidate(new Rectangle(viewer.Width - 19, 1, 16, 16));
      }
    }

    void viewer_MouseEnterHotSpot(object sender, WinHotSpotEventArgs e)
    {
      if (e.AttrValues["title"].ToString().CompareTo("ToolButton") == 0)
      {
        Graphics gr = Graphics.FromImage(viewer.Image);
        gr.DrawImage(Image.FromFile(@"C:\_projects\MetricView\MetricView\images\refresh_hot.bmp"), viewer.Width - 19, 1);
        gr.Dispose();
        viewer.Invalidate(new Rectangle(viewer.Width - 19, 1, 16, 16));
        return;
      }


      GraphValue(this.lblTitle.Text, e.AttrValues["dataSetName"].ToString(),
                 e.AttrValues["xLabel"].ToString(), e.AttrValues["value"].ToString());
    }

    private void viewer_Click(object sender, EventArgs e)
    {
      this.BringToFront();
      MenuAction(this.GraphName, @"SELECT");
    }

    public void SetSelectionAndActivation()
    {
      if (_metricGraphConfig.IsActive)
      {
        timer.Interval = _metricGraphConfig.RefreshInterval;
        bool wasTimerEnabled = timer.Enabled;
        timer.Enabled = true;
        if (!wasTimerEnabled)
        {
          DrawGraph(true);
        }
        this.mnuContextActivateGraph.Visible = false;
        this.mnuContextDeactivateGraph.Visible = true;

        if (_metricGraphConfig.IsSelected)
          this.BackColor = Color.Blue;
        else
          this.BackColor = Color.DarkBlue;
      }
      else
      {
        timer.Enabled = false;
        this.mnuContextActivateGraph.Visible = true;
        this.mnuContextDeactivateGraph.Visible = false;

        if (_metricGraphConfig.IsSelected)
          this.BackColor = Color.DarkGray;
        else
          this.BackColor = Color.Gray;
      }
    }

    public void Reconfigure()
    {
      if (timer.Enabled)
      {

        timer.Enabled = false;
        timer.Interval = _metricGraphConfig.RefreshInterval;
        timer.Enabled = true;
      }

      this.GraphName = _metricGraphConfig.GraphName;
      this.lblTitle.Text = GeneralUtility.StripSeqFromName(this.GraphName);
      SetSelectionAndActivation();

    }

    public void RefreshGraph(bool UpdateData)
    {
      if (_metricGraphConfig.IsActive)
      {
        this.Cursor = Cursors.WaitCursor;
        DrawGraph(UpdateData);
        this.Cursor = Cursors.Arrow;
      }
    }

    public void Nudge(string direction)
    {
      switch (direction)
      {
        case "RIGHT":
          this.Left += 1;
          break;

        case "LEFT":
          this.Left -= 1;
          break;

        case "UP":
          this.Top -= 1;
          break;

        case "DOWN":
          this.Top += 1;
          break;

        case "INCREASE_WIDTH":
          this.Width += 1;
          break;

        case "DECREASE_WIDTH":
          this.Width -= 1;
          break;

        case "INCREASE_HEIGHT":
          this.Height += 1;
          break;

        case "DECREASE_HEIGHT":
          this.Height -= 1;
          break;
      }
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      if (!_metricGraphConfig.IncludedMetrics.UseMostCurrentMetric)
        return;

      if (IsSuspended)
        return;

      string holdTitle = this.lblTitle.Text;
      this.lblTitle.Text += " - Refreshing Graph";
      Application.DoEvents();
      //System.Threading.Thread.Sleep(100);
      DrawGraph(true);
      this.lblTitle.Text = holdTitle;
      Application.DoEvents();
    }

    private void MetricGraph_MouseDown(object sender, MouseEventArgs e)
    {
      this.BringToFront();
      MenuAction(this.GraphName, @"SELECT");

      IsMoveOrResizeStarted = false;
      IsResizingHeight = false;
      IsResizingWidth = false;

      prevMouseXPos = this.Left + e.Location.X;
      prevMouseYPos = this.Top + e.Location.Y;

      if (IsCursorPositionedForResizingWidth)
      {
        IsResizingWidth = true;
        IsMoveOrResizeStarted = true;
      }
      else
      {
        if (IsCursorPositionedForResizingHeight)
        {
          IsResizingHeight = true;
          IsMoveOrResizeStarted = true;
        }
        else
        {
          IsMoveOrResizeStarted = true;
        }
      }
    }

    private void MetricGraph_DoubleClick(object sender, EventArgs e)
    {
      UseSpline = !UseSpline;
      RefreshGraph(true);
      return;
    }

    public void RestorePosition()
    {
      this.Visible = false;

      this.Left = restoreLeft;
      this.Top = restoreTop;
      this.Width = restoreWidth;
      this.Height = restoreHeight;
      this.BringToFront();
      IsMaximized = false;
      IsFrontAndCenter = false;
      mnuContextMaximize.Visible = true;
      mnuContextRestorePosition.Visible = false;
      mnuContextFrontAndCenter.Visible = true;
      ResizeGraph(false);

      this.Visible = true;
    }

    public void MaximizeGraph()
    {
      this.Visible = false;

      if (!IsFrontAndCenter)
      {
        restoreLeft = this.Left;
        restoreTop = this.Top;
        restoreWidth = this.Width;
        restoreHeight = this.Height;
      }
      this.Left = 4;
      this.Top = 4;
      this.Width = this.Parent.Width - 10;
      this.Height = this.Parent.Height - 10;
      IsMaximized = true;
      IsFrontAndCenter = false;
      this.BringToFront();
      mnuContextMaximize.Visible = false;
      mnuContextRestorePosition.Visible = true;
      mnuContextFrontAndCenter.Visible = false;
      ResizeGraph(false);

      this.Visible = true;
    }

    public void FrontAndCenter()
    {
      this.Visible = false;

      if (!IsMaximized)
      {
        restoreLeft = this.Left;
        restoreTop = this.Top;
        restoreWidth = this.Width;
        restoreHeight = this.Height;
      }

      int width = this.Parent.Width * 2 / 3;
      int height = this.Parent.Height * 2 / 3;
      int left = (this.Parent.Width / 2) - (width / 2);
      int top = (this.Parent.Height / 2) - (height / 2);

      this.Left = left;
      this.Top = top;
      this.Width = width;
      this.Height = height;
      IsMaximized = false;
      IsFrontAndCenter = true;
      this.BringToFront();
      mnuContextMaximize.Visible = false;
      mnuContextRestorePosition.Visible = true;
      mnuContextFrontAndCenter.Visible = false;
      ResizeGraph(false);

      this.Visible = true;
    }

    public void Arrange()
    {
      this.Left = _metricGraphConfig.GraphLocation.X;
      this.Top = _metricGraphConfig.GraphLocation.Y;
      this.Width = _metricGraphConfig.GraphSize.Width;
      this.Height = _metricGraphConfig.GraphSize.Height;
      IsMaximized = false;
      IsFrontAndCenter = false;
    }


    public void FrontAndCenterNew()
    {
      this.Visible = false;

      int width = this.Parent.Width * 2 / 3;
      int height = this.Parent.Height * 2 / 3;
      int left = (this.Parent.Width / 2) - (width / 2);
      int top = (this.Parent.Height / 2) - (height / 2);

      this.Left = left;
      this.Top = top;
      this.Width = width;
      this.Height = height;
      IsMaximized = false;
      IsFrontAndCenter = false;
      this.BringToFront();
      mnuContextMaximize.Visible = false;
      mnuContextRestorePosition.Visible = false;
      mnuContextFrontAndCenter.Visible = true;
      ResizeGraph(true);

      this.Visible = true;
    }
    private void MetricGraph_MouseUp(object sender, MouseEventArgs e)
    {
      IsMoveOrResizeStarted = false;
      IsResizingWidth = false;
      IsResizingHeight = false;
      if (IsResizing)
      {
        DrawGraph(false);
        IsResizing = false;
      }
    }

    private void MetricGraph_MouseMove(object sender, MouseEventArgs e)
    {
      currMouseXPos = this.Left + e.Location.X;
      currMouseYPos = this.Top + e.Location.Y;

      if (IsMoveOrResizeStarted)
      {
        if (IsResizingWidth)
        {
          this.Width += currMouseXPos - prevMouseXPos;
          IsResizing = true;
        }
        else
        {
          if (IsResizingHeight)
          {
            this.Height += currMouseYPos - prevMouseYPos;
            IsResizing = true;
          }
          else
          {
            this.Left += currMouseXPos - prevMouseXPos;
            this.Top += currMouseYPos - prevMouseYPos;
            IsCursorPositionedForResizingHeight = false;
            IsCursorPositionedForResizingWidth = false;
          }
        }
        _metricGraphConfig.GraphLocation = new Point(this.Left, this.Top);
        _metricGraphConfig.GraphSize = new Size(this.Width, this.Height);
        _metricGraphConfig.GraphName = _graphName;

        UpdateConfigLocAndSize(_metricGraphConfig);
      }
      else
      {
        if (e.Location.X > this.Width - 5)
        {
          this.Cursor = Cursors.SizeWE;
          IsCursorPositionedForResizingWidth = true;
          IsCursorPositionedForResizingHeight = false;
        }
        else
        {
          if (e.Location.Y > this.Height - 5)
          {
            this.Cursor = Cursors.SizeNS;
            IsCursorPositionedForResizingHeight = true;
            IsCursorPositionedForResizingWidth = false;
          }
          else
          {
            IsCursorPositionedForResizingHeight = false;
            IsCursorPositionedForResizingWidth = false;
            this.Cursor = Cursors.Arrow;
          }
        }

      }

      prevMouseXPos = currMouseXPos;
      prevMouseYPos = currMouseYPos;
    }

    private void mnuContextMenuAction_Click(object sender, EventArgs e)
    {
      ToolStripMenuItem item = (ToolStripMenuItem)sender;
      MenuAction(this.GraphName, item.Tag.ToString());
    }

    public void ResizeGraph(bool UpdateData)
    {
      DrawGraph(UpdateData);
    }

    public void InitializeGraph()
    {
      g = new XYChart(viewer.Width, viewer.Height, 0xeeeeff, 0xeeeeff, 0);

      g.addTitle(GeneralUtility.StripSeqFromName(this.GraphName), "Tahoma Bold", 9.5, Chart.CColor(Color.DarkBlue));
      g.setPlotArea(45, 50, viewer.Width - 75, viewer.Height - 92, 0xffffff, -1, -1, 0xcccccc, 0xcccccc);
      g.addLegend(45, 25, false, "Tahoma", 7).setBackground(Chart.Transparent);

      g.yAxis().setTitle("No Metrics");
      g.yAxis().setLabelStyle("Tahoma", 7);
      g.xAxis().setTitle("No Metrics", "Tahoma", 7);
      g.xAxis().setLabelStyle("Tahoma", 7);

      viewer.Image = g.makeImage();
    }

    public void SendChartImageToClipboard()
    {
      Clipboard.SetDataObject(viewer.Image, true);
    }

    private void DrawGraph(bool UpdateData)
    {
      DateTime beginRefreshDT = DateTime.Now;
      if (UpdateData || msc == null || msc.Count == 0)
        msc = this.GetMetricObservationSetCollection();

      if (IsRollup)
      {
        msc.Rollup();
      }

      if (IsSQLConnectionLost)
        MenuAction("ALL", "SQL_CONNECTION_LOST");

      if (msc.Count == 0)
      {
        InitializeGraph();
        return;
      }

      MetricObservationSet ms = msc.Values[0];

      g = new XYChart(viewer.Width, viewer.Height, 0xeeeeff, 0xeeeeff, 0);

      g.addTitle(GeneralUtility.StripSeqFromName(this.GraphName), "Tahoma Bold", 9.5, Chart.CColor(Color.DarkBlue));
      g.setPlotArea(45, 50, viewer.Width - 75, viewer.Height - 92, 0xffffff, -1, -1, 0xcccccc, 0xcccccc);
      g.addLegend(45, 25, false, "Tahoma", 7).setBackground(Chart.Transparent);

      if (ms.Count == 0)
      {
        g.yAxis().setTitle("No Metrics Found for Specified Criteria");
        g.yAxis().setTitle("No Metrics");
        g.yAxis().setLabelStyle("Tahoma", 7);
        g.xAxis().setTitle("No Metrics", "Tahoma", 7);
        g.xAxis().setLabelStyle("Tahoma", 7);
        g.addText(55, 75, "No metrics found for specified criteria", "Tahoma", 9, 0x000000);
        viewer.Image = g.makeImage();
        return;
      }


      string dateTimeRange = "Metrics from " + msc[0].Values[0].MetricCapturedDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "  to  " +
                             msc[0].Values[msc[0].Values.Count - 1].MetricCapturedDateTime.ToString("yyyy-MM-dd HH:mm:ss");

      g.yAxis().setTitle(_dataObjects.Metrics[msc[0].Values[0].MetricID].MetricDesc);
      g.yAxis().setLabelStyle("Tahoma", 7);
      g.xAxis().setTitle(dateTimeRange, "Tahoma",7);
      g.xAxis().setLabelStyle("Tahoma", 7);

      LineLayer layer = g.addLineLayer2();
      layer.setLineWidth(1);
      if (UseSpline)
        layer.setLineWidth(0);

      if (msc.UseMostCurrentMetric)
      {
        string text = msc.DataPoints.ToString() + " Most Current Observations as of " +
                      DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + " - Refresh Interval is " + msc.RefreshInterval.ToString() + " milliseconds";
        g.addText(10, 15, text, "Tahoma", 7, 0x000000);
      }
      else
      {
        string text = "From " + msc.FromDateTime.ToString("MM/dd/yyyy HH:mm:ss") + " to " +
                      msc.ToDateTime.ToString() + " - Graph is static, use Refresh context menu to get latest metrics";
        g.addText(10, 15, text, "Tahoma", 7, 0x000000);
      }

      // until we do the alignment of metric sets, we must be sure the points array is not jagged
      int numberOfPoints = 999999;
      for (int mscPtr = 0; mscPtr < msc.Count; mscPtr++)
      {
        if (msc[mscPtr].Count < numberOfPoints)
          numberOfPoints = msc[mscPtr].Count;
      }

      double[,] points = new double[msc.Count, numberOfPoints];
      string[] labels = new string[numberOfPoints];

      for (int mscPtr = 0; mscPtr < msc.Count; mscPtr++)
      {
        ms = msc.Values[mscPtr];

        for (int msPtr = 0; msPtr < numberOfPoints; msPtr++)
        {
          MetricObservation m = ms.Values[msPtr];
          DateTime metricDateTime = m.MetricCapturedDateTime;
          float metricValue = m.MetricValue;

          if (_dataObjects.Metrics[msc[0].Values[0].MetricID].MetricDesc == "Available Bytes")
            metricValue = metricValue / 1000000;

          //if (m.AggregateTypeID == 1)
          //    metricValue = metricValue * 10;  //(need to consider putting a multiplier factor into the graph properties or
          ////                                     // to investigate using multiple scales)

          points[mscPtr, msPtr] = metricValue;
          if(mscPtr == 0)
            labels[msPtr] = metricDateTime.ToString("HH:mm");
        }

        double[] pointSet = new double[msc[0].Count];
        for (int a = 0; a < numberOfPoints; a++)
        {
          pointSet[a] = points[mscPtr, a];
        }
        layer.addDataSet(pointSet, graphColors[mscPtr], ms.LegendLabel);
        if (UseSpline)
          g.addSplineLayer(new ArrayMath(pointSet).lowess(.2).result(), graphColors[mscPtr]).setLineWidth(2);
      }

      g.xAxis().setLabels(labels);
      g.xAxis().setLabelStep(msc[0].Count / (((viewer.Width + 1) / 60) + 1));  // display every nth label

      DateTime endRefreshDT = DateTime.Now;
      TimeSpan refreshTime = endRefreshDT.Subtract(beginRefreshDT);

      string refreshTimeDisplay = "   Refresh time = " + refreshTime.TotalSeconds.ToString() +
                                  refreshTime.Milliseconds.ToString("00") + " seconds";
      if(UpdateData)
        g.xAxis().setTitle(dateTimeRange + " " + refreshTimeDisplay, "Tahoma", 7);
      else
        g.xAxis().setTitle(dateTimeRange, "Tahoma", 7);

      Image img = g.makeImage();
      Size imageSize = img.Size;
      //Graphics gr = Graphics.FromImage(img);
      //gr.DrawImage(Image.FromFile(@"C:\_projects\MetricView\MetricView\images\back_all.bmp"), viewer.Width - 83, 1);
      //gr.DrawImage(Image.FromFile(@"C:\_projects\MetricView\MetricView\images\back.bmp"), viewer.Width - 67, 1);
      //gr.DrawImage(Image.FromFile(@"C:\_projects\MetricView\MetricView\images\fwd.bmp"), viewer.Width - 51, 1);
      //gr.DrawImage(Image.FromFile(@"C:\_projects\MetricView\MetricView\images\fwd_all.bmp"), viewer.Width - 35, 1);
      //gr.DrawImage(Image.FromFile(@"C:\_projects\MetricView\MetricView\images\refresh.bmp"), viewer.Width - 19, 1);
      //gr.Dispose();

      //string backAllImageMap = "<area " + "shape=\"rect\" coords=\"" + Convert.ToString(viewer.Width - 83).Trim() + ",1," +
      //    Convert.ToString(viewer.Width - 83 + 16).Trim() + ",17\" href=\"BACK_ALL\" title=\"ToolButton\">\n";

      //string backImageMap = "<area " + "shape=\"rect\" coords=\"" + Convert.ToString(viewer.Width - 67).Trim() + ",1," +
      //    Convert.ToString(viewer.Width - 67 + 16).Trim() + ",17\" href=\"BACK\" title=\"ToolButton\">\n";

      //string fwdImageMap = "<area " + "shape=\"rect\" coords=\"" + Convert.ToString(viewer.Width - 51).Trim() + ",1," +
      //    Convert.ToString(viewer.Width - 51 + 16).Trim() + ",17\" href=\"FWD\" title=\"ToolButton\">\n";

      //string fwdAllImageMap = "<area " + "shape=\"rect\" coords=\"" + Convert.ToString(viewer.Width - 35).Trim() + ",1," +
      //    Convert.ToString(viewer.Width - 35 + 16).Trim() + ",17\" href=\"FWD_ALL\" title=\"ToolButton\">\n";

      //string refreshImageMap = "<area " + "shape=\"rect\" coords=\"" + Convert.ToString(viewer.Width - 19).Trim() + ",1," +
      //    Convert.ToString(viewer.Width - 19 + 16).Trim() + ",17\" href=\"REFRESH\" title=\"ToolButton\">\n";

      viewer.Image = img;
      viewer.ImageMap = g.getHTMLImageMap("test"); // +backAllImageMap + backImageMap + fwdImageMap + fwdAllImageMap + refreshImageMap;
    }


    private MetricObservationSetCollection GetMetricObservationSetCollection()
    {
      IsSQLConnectionLost = false;

      MetricObservationSetCollection msc = new MetricObservationSetCollection();
      msc.UseMostCurrentMetric = _metricGraphConfig.IncludedMetrics.UseMostCurrentMetric;
      msc.DataPoints = _metricGraphConfig.IncludedMetrics.DataPoints;
      msc.FromDateTime = _metricGraphConfig.IncludedMetrics.FromDateTime;
      msc.ToDateTime = _metricGraphConfig.IncludedMetrics.ToDateTime;
      msc.RefreshInterval = _metricGraphConfig.RefreshInterval;
      msc.YAxisLabel = _metricGraphConfig.IncludedMetrics.YAxisLabel;

      MetricQueryParms parms = new MetricQueryParms();
      parms.UseMostCurrentMetric = _metricGraphConfig.IncludedMetrics.UseMostCurrentMetric;
      parms.DataPoints = _metricGraphConfig.IncludedMetrics.DataPoints;
      parms.FromDateTime = _metricGraphConfig.IncludedMetrics.FromDateTime;
      parms.ToDateTime = _metricGraphConfig.IncludedMetrics.ToDateTime;

      //if (_metricGraphConfig.IncludedMetrics.UseMostCurrentMetric)
      //{
      //    MetricObservationSet[] mosSet = DataObjects.GetMetricObsevationSets(_metricGraphConfig.IncludedMetrics);
      //    for (int i = 0; i < mosSet.Length; i++)
      //    {
      //        msc.Add(msc.Count, mosSet[i]);
      //    }
      //}
      //else
      //{
      foreach (KeyValuePair<int, SpecificMetric> smKVP in _metricGraphConfig.IncludedMetrics)
      {
        if (smKVP.Value.RollUpToHourly)
          IsRollup = true;

        long duration = DataObjects.GetMetricObservationSet(smKVP.Value, parms);
        if (duration == -1)
        {
          IsSQLConnectionLost = true;
          return msc;
        }

        MetricObservationSet mos = DataObjects.MetricObservations;
        msc.Add(msc.Count, mos);
        if (mos.HighMetricValue > msc.HighMetricValue)
          msc.HighMetricValue = mos.HighMetricValue;
        if (mos.LowMetricValue < msc.LowMetricValue)
          msc.LowMetricValue = mos.LowMetricValue;
      }
      //}


      //msc.AlignMetrics();

      return msc;
    }

    private void mnuContextRefreshGraph_Click(object sender, EventArgs e)
    {
      if (_metricGraphConfig.IsActive)
        DrawGraph(true);
    }

    private void mnuContextGetGraphData_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      frmMetricData fMetricData = new frmMetricData(msc, _dataObjects, _metricGraphConfig);

      fMetricData.ShowDialog();
      this.Cursor = Cursors.Arrow;
    }

    private void mnuCopyGraphConfig_Click(object sender, EventArgs e)
    {
      Clipboard.SetData("GraphConfig", _metricGraphConfig);
    }

    private void mnuSaveGraphToFile_Click(object sender, EventArgs e)
    {
      MenuAction(this.GraphName, "SAVE_TO_FILE");
    }

    private void mnuSaveImageToFile_Click(object sender, EventArgs e)
    {
      Image img = viewer.Image;
      SaveImageToFile(this.GraphName, img);
    }


  }
}
