using System;
using System.Windows.Forms;
using ChartDirector;

namespace CSharpChartExplorer
{
    public partial class FrmRealTimeZoomScroll : Form
    {
        // The data arrays that store the realtime data. The data arrays are updated in realtime. 
        // In this demo, we store at most 10000 values. 
        private const int sampleSize = 10000;
        private DateTime[] timeStamps = new DateTime[sampleSize];
        private double[] dataSeriesA = new double[sampleSize];
        private double[] dataSeriesB = new double[sampleSize];
        private double[] dataSeriesC = new double[sampleSize];

        // The index of the array position to which new data values are added.
        private int currentIndex = 0;

        // The full range is initialized to 60 seconds of data. It can be extended when more data
        // are available.
        private int initialFullRange = 60;

        // The maximum zoom in is 10 seconds.
        private int zoomInLimit = 10;

        // In this demo, we use a data generator driven by a timer to generate realtime data. The
        // nextDataTime is an internal variable used by the data generator to keep track of which
        // values to generate next.
        private DateTime nextDataTime = new DateTime(DateTime.Now.Ticks / 10000000 * 10000000);

        public FrmRealTimeZoomScroll()
        {
            InitializeComponent();
        }

        private void FrmRealtimeZoomScroll_Load(object sender, EventArgs e)
        {
            // Initialize the WinChartViewer
            initChartViewer(winChartViewer1);

            // Data generation rate
            dataRateTimer.Interval = 250;

            // Chart update rate, which can be different from the data generation rate.
            chartUpdateTimer.Interval = (int)samplePeriod.Value;

            // Now can start the timers for data collection and chart update
            dataRateTimer.Start();
            chartUpdateTimer.Start();
        }

        //
        // Initialize the WinChartViewer
        //
        private void initChartViewer(WinChartViewer viewer)
        {
            viewer.MouseWheelZoomRatio = 1.1;

            // Initially set the mouse usage to "Pointer" mode (Drag to Scroll mode)
            pointerPB.Checked = true;
        }

        //
        // The data update routine. In this demo, it is invoked every 250ms to get new data.
        //
        private void dataRateTimer_Tick(object sender, EventArgs e)
        {
            do
            {
                //
                // In this demo, we use some formulas to generate new values. In real applications,
                // it may be replaced by some data acquisition code.
                //
                double p = nextDataTime.Ticks / 10000000.0 * 4;
                double dataA = 20 + Math.Cos(p * 2.2) * 10 + 1 / (Math.Cos(p) * Math.Cos(p) + 0.01);
                double dataB = 150 + 100 * Math.Sin(p / 27.7) * Math.Sin(p / 10.1);
                double dataC = 150 + 100 * Math.Cos(p / 6.7) * Math.Cos(p / 11.9);

                // In this demo, if the data arrays are full, the oldest 5% of data are discarded.
                if (currentIndex >= timeStamps.Length)
                {
                    currentIndex = sampleSize * 95 / 100 - 1;

                    for (int i = 0; i < currentIndex; ++i)
                    {
                        int srcIndex = i + sampleSize - currentIndex;
                        timeStamps[i] = timeStamps[srcIndex];
                        dataSeriesA[i] = dataSeriesA[srcIndex];
                        dataSeriesB[i] = dataSeriesB[srcIndex];
                        dataSeriesC[i] = dataSeriesC[srcIndex];
                    }
                }

                // Store the new values in the current index position, and increment the index.
                timeStamps[currentIndex] = nextDataTime;
                dataSeriesA[currentIndex] = dataA;
                dataSeriesB[currentIndex] = dataB;
                dataSeriesC[currentIndex] = dataC;
                ++currentIndex; 
                
                nextDataTime = nextDataTime.AddMilliseconds(dataRateTimer.Interval);
            }
            while (nextDataTime < DateTime.Now);

            // We provide some visual feedback to the numbers generated, so you can see the
            // values being generated.
            valueA.Text = dataSeriesA[currentIndex - 1].ToString(".##");
            valueB.Text = dataSeriesB[currentIndex - 1].ToString(".##");
            valueC.Text = dataSeriesC[currentIndex - 1].ToString(".##");
        }

        //
        // Update the chart and the viewport periodically
        //
        private void chartUpdateTimer_Tick(object sender, EventArgs e)
        {
            WinChartViewer viewer = winChartViewer1;

            if (currentIndex > 0)
            {
                //
                // As we added more data, we may need to update the full range. 
                //

                DateTime startDate = timeStamps[0];
                DateTime endDate = timeStamps[currentIndex - 1];

                // Use the initialFullRange if this is sufficient.
                double duration = endDate.Subtract(startDate).TotalSeconds;
                if (duration < initialFullRange)
                    endDate = startDate.AddSeconds(initialFullRange);

                // Update the full range to reflect the actual duration of the data. In this case, 
                // if the view port is viewing the latest data, we will scroll the view port as new
                // data are added. If the view port is viewing historical data, we would keep the 
                // axis scale unchanged to keep the chart stable.
                int updateType = Chart.ScrollWithMax;
                if (viewer.ViewPortLeft + viewer.ViewPortWidth < 0.999)
                    updateType = Chart.KeepVisibleRange;
                bool axisScaleHasChanged = viewer.updateFullRangeH("x", startDate, endDate, updateType);
                
                // Set the zoom in limit as a ratio to the full range
                viewer.ZoomInWidthLimit = zoomInLimit / (viewer.getValueAtViewPort("x", 1) - 
                    viewer.getValueAtViewPort("x", 0));
                
                // Trigger the viewPortChanged event to update the display if the axis scale has 
                // changed or if new data are added to the existing axis scale.
                if (axisScaleHasChanged || (duration < initialFullRange))
                    viewer.updateViewPort(true, false);
            }
        }

        //
        // The ViewPortChanged event handler. This event occurs if the user scrolls or zooms in
        // or out the chart by dragging or clicking on the chart. It can also be triggered by
        // calling WinChartViewer.updateViewPort.
        //
        private void winChartViewer1_ViewPortChanged(object sender, WinViewPortEventArgs e)
        {
            // In addition to updating the chart, we may also need to update other controls that
            // changes based on the view port.
            updateControls(winChartViewer1);

            // Update the chart if necessary
            if (e.NeedUpdateChart)
                drawChart(winChartViewer1);
        }

        //
        // Update other controls when the view port changed
        //
        private void updateControls(WinChartViewer viewer)
        {
            // Update the scroll bar to reflect the view port position and width.           
            hScrollBar1.Enabled = winChartViewer1.ViewPortWidth < 1;
            hScrollBar1.LargeChange = (int)Math.Ceiling(winChartViewer1.ViewPortWidth *
                (hScrollBar1.Maximum - hScrollBar1.Minimum));
            hScrollBar1.SmallChange = (int)Math.Ceiling(hScrollBar1.LargeChange * 0.1);
            hScrollBar1.Value = (int)Math.Round(winChartViewer1.ViewPortLeft *
                (hScrollBar1.Maximum - hScrollBar1.Minimum)) + hScrollBar1.Minimum;
        }

        //
        // Draw the chart.
        //
        private void drawChart(WinChartViewer viewer)
        {
            // Get the start date and end date that are visible on the chart.
            DateTime viewPortStartDate = Chart.NTime(viewer.getValueAtViewPort("x", viewer.ViewPortLeft));
            DateTime viewPortEndDate = Chart.NTime(viewer.getValueAtViewPort("x", viewer.ViewPortLeft +
                viewer.ViewPortWidth));

            // Extract the part of the data arrays that are visible.
            DateTime[] viewPortTimeStamps = null;
            double[] viewPortDataSeriesA = null;
            double[] viewPortDataSeriesB = null;
            double[] viewPortDataSeriesC = null;

            if (currentIndex > 0)
            {
                // Get the array indexes that corresponds to the visible start and end dates
                int startIndex = (int)Math.Floor(Chart.bSearch2(timeStamps, 0, currentIndex, viewPortStartDate));
                int endIndex = (int)Math.Ceiling(Chart.bSearch2(timeStamps, 0, currentIndex, viewPortEndDate));
                int noOfPoints = endIndex - startIndex + 1;
                
                // Extract the visible data
                viewPortTimeStamps = (DateTime[])Chart.arraySlice(timeStamps, startIndex, noOfPoints);
                viewPortDataSeriesA = (double[])Chart.arraySlice(dataSeriesA, startIndex, noOfPoints);
                viewPortDataSeriesB = (double[])Chart.arraySlice(dataSeriesB, startIndex, noOfPoints);
                viewPortDataSeriesC = (double[])Chart.arraySlice(dataSeriesC, startIndex, noOfPoints);
            }

            //
            // At this stage, we have extracted the visible data. We can use those data to plot the chart.
            //

            //================================================================================
            // Configure overall chart appearance.
            //================================================================================

            // Create an XYChart object of size 640 x 350 pixels
            XYChart c = new XYChart(640, 350);

            // Set the plotarea at (55, 50) with width 80 pixels less than chart width, and height 85 pixels
            // less than chart height. Use a vertical gradient from light blue (f0f6ff) to sky blue (a0c0ff)
            // as background. Set border to transparent and grid lines to white (ffffff).
            c.setPlotArea(55, 50, c.getWidth() - 85, c.getHeight() - 80, c.linearGradientColor(0, 50, 0,
                c.getHeight() - 35, 0xf0f6ff, 0xa0c0ff), -1, Chart.Transparent, 0xffffff, 0xffffff);

            // As the data can lie outside the plotarea in a zoomed chart, we need enable clipping.
            c.setClipping();

            // Add a title to the chart using 18 pts Times New Roman Bold Italic font
            c.addTitle("  Realtime Chart with Zoom/Scroll and Track Line", "Arial", 18);

            // Add a legend box at (55, 25) using horizontal layout. Use 8pts Arial Bold as font. Set the
            // background and border color to Transparent and use line style legend key.
            LegendBox b = c.addLegend(55, 25, false, "Arial Bold", 10);
            b.setBackground(Chart.Transparent);
            b.setLineStyleKey();

            // Set the x and y axis stems to transparent and the label font to 10pt Arial
            c.xAxis().setColors(Chart.Transparent);
            c.yAxis().setColors(Chart.Transparent);
            c.xAxis().setLabelStyle("Arial", 10);
            c.yAxis().setLabelStyle("Arial", 10);

            // Add axis title using 10pts Arial Bold Italic font
            c.yAxis().setTitle("Ionic Temperature (C)", "Arial Bold", 10);

            //================================================================================
            // Add data to chart
            //================================================================================

            //
            // In this example, we represent the data by lines. You may modify the code below to use other
            // representations (areas, scatter plot, etc).
            //

            // Add a line layer for the lines, using a line width of 2 pixels
            LineLayer layer = c.addLineLayer2();
            layer.setLineWidth(2);
            layer.setFastLineMode();

            // Now we add the 3 data series to a line layer, using the color red (ff0000), green (00cc00)
            // and blue (0000ff)
            layer.setXData(viewPortTimeStamps);
            layer.addDataSet(viewPortDataSeriesA, 0xff0000, "Alpha");
            layer.addDataSet(viewPortDataSeriesB, 0x00cc00, "Beta");
            layer.addDataSet(viewPortDataSeriesC, 0x0000ff, "Gamma");

            //================================================================================
            // Configure axis scale and labelling
            //================================================================================

            if (currentIndex > 0)
                c.xAxis().setDateScale(viewPortStartDate, viewPortEndDate);

            // For the automatic axis labels, set the minimum spacing to 75/30 pixels for the x/y axis.
            c.xAxis().setTickDensity(75);
            c.yAxis().setTickDensity(30);

            //
            // In a zoomable chart, the time range can be from a few years to a few seconds. We can need
            // to define the date/time format the various cases. 
            //

            // If all ticks are year aligned, we use "yyyy" as the label format.
            c.xAxis().setFormatCondition("align", 360 * 86400);
            c.xAxis().setLabelFormat("{value|yyyy}");

            // If all ticks are month aligned, we use "mmm yyyy" in bold font as the first label of a year,
            // and "mmm" for other labels.
            c.xAxis().setFormatCondition("align", 30 * 86400);
            c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "<*font=bold*>{value|mmm yyyy}",
                Chart.AllPassFilter(), "{value|mmm}");

            // If all ticks are day algined, we use "mmm dd<*br*>yyyy" in bold font as the first label of a
            // year, and "mmm dd" in bold font as the first label of a month, and "dd" for other labels.
            c.xAxis().setFormatCondition("align", 86400);
            c.xAxis().setMultiFormat(Chart.StartOfYearFilter(),
                "<*block,halign=left*><*font=bold*>{value|mmm dd<*br*>yyyy}", Chart.StartOfMonthFilter(),
                "<*font=bold*>{value|mmm dd}");
            c.xAxis().setMultiFormat2(Chart.AllPassFilter(), "{value|dd}");

            // If all ticks are hour algined, we use "hh:nn<*br*>mmm dd" in bold font as the first label of 
            // the Day, and "hh:nn" for other labels.
            c.xAxis().setFormatCondition("align", 3600);
            c.xAxis().setMultiFormat(Chart.StartOfDayFilter(), "<*font=bold*>{value|hh:nn<*br*>mmm dd}",
                Chart.AllPassFilter(), "{value|hh:nn}");

            // If all ticks are minute algined, then we use "hh:nn" as the label format.
            c.xAxis().setFormatCondition("align", 60);
            c.xAxis().setLabelFormat("{value|hh:nn}");

            // If all other cases, we use "hh:nn:ss" as the label format.
            c.xAxis().setFormatCondition("else");
            c.xAxis().setLabelFormat("{value|hh:nn:ss}");

            // We make sure the tick increment must be at least 1 second.
            c.xAxis().setMinTickInc(1);

            //================================================================================
            // Output the chart
            //================================================================================

            // We need to update the track line too. If the mouse is moving on the chart (eg. if 
            // the user drags the mouse on the chart to scroll it), the track line will be updated
            // in the MouseMovePlotArea event. Otherwise, we need to update the track line here.
            if (!winChartViewer1.IsInMouseMoveEvent)
            {
                trackLineLabel(c, (null == viewer.Chart) ? c.getPlotArea().getRightX() : 
                    viewer.PlotAreaMouseX);
            }
                        
            viewer.Chart = c;
        }

        //
        // Draw track line with data labels
        //
        private void trackLineLabel(XYChart c, int mouseX)
        {
            // Clear the current dynamic layer and get the DrawArea object to draw on it.
            DrawArea d = c.initDynamicLayer();

            // The plot area object
            PlotArea plotArea = c.getPlotArea();

            // Get the data x-value that is nearest to the mouse, and find its pixel coordinate.
            double xValue = c.getNearestXValue(mouseX);
            int xCoor = c.getXCoor(xValue);
            if (xCoor < plotArea.getLeftX())
                return;

            // Draw a vertical track line at the x-position
            d.vline(plotArea.getTopY(), plotArea.getBottomY(), xCoor, 0x888888);

            // Draw a label on the x-axis to show the track line position.
            string xlabel = "<*font,bgColor=000000*> " + c.xAxis().getFormattedLabel(xValue, "hh:nn:ss.ff") +
                " <*/font*>";
            TTFText t = d.text(xlabel, "Arial Bold", 10);

            // Restrict the x-pixel position of the label to make sure it stays inside the chart image.
            int xLabelPos = Math.Max(0, Math.Min(xCoor - t.getWidth() / 2, c.getWidth() - t.getWidth()));
            t.draw(xLabelPos, plotArea.getBottomY() + 6, 0xffffff);

            // Iterate through all layers to draw the data labels
            for (int i = 0; i < c.getLayerCount(); ++i)
            {
                Layer layer = c.getLayerByZ(i);

                // The data array index of the x-value
                int xIndex = layer.getXIndexOf(xValue);

                // Iterate through all the data sets in the layer
                for (int j = 0; j < layer.getDataSetCount(); ++j)
                {
                    ChartDirector.DataSet dataSet = layer.getDataSetByZ(j);

                    // Get the color and position of the data label
                    int color = dataSet.getDataColor();
                    int yCoor = c.getYCoor(dataSet.getPosition(xIndex), dataSet.getUseYAxis());

                    // Draw a track dot with a label next to it for visible data points in the plot area
                    if ((yCoor >= plotArea.getTopY()) && (yCoor <= plotArea.getBottomY()) && (color !=
                        Chart.Transparent) && (!string.IsNullOrEmpty(dataSet.getDataName())))
                    {
                        d.circle(xCoor, yCoor, 4, 4, color, color);

                        string label = "<*font,bgColor=" + color.ToString("x") + "*> " + c.formatValue(
                            dataSet.getValue(xIndex), "{value|P4}") + " <*/font*>";
                        t = d.text(label, "Arial Bold", 10);

                        // Draw the label on the right side of the dot if the mouse is on the left side the
                        // chart, and vice versa. This ensures the label will not go outside the chart image.
                        if (xCoor <= (plotArea.getLeftX() + plotArea.getRightX()) / 2)
                            t.draw(xCoor + 5, yCoor, 0xffffff, Chart.Left);
                        else
                            t.draw(xCoor - 5, yCoor, 0xffffff, Chart.Right);
                    }
                }
            }
        }

        //
        // Updates the chartUpdateTimer interval if the user selects another interval.
        //
        private void samplePeriod_ValueChanged(object sender, EventArgs e)
        {
            chartUpdateTimer.Interval = (int)samplePeriod.Value;
        }

        //
        // The scroll bar event handler
        //
        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            // When the view port is changed (user drags on the chart to scroll), the scroll bar will get
            // updated. When the scroll bar changes (eg. user drags on the scroll bar), the view port will
            // get updated. This creates an infinite loop. To avoid this, the scroll bar can update the 
            // view port only if the view port is not updating the scroll bar.
            if (!winChartViewer1.IsInViewPortChangedEvent)
            {
                winChartViewer1.ViewPortLeft = ((double)(hScrollBar1.Value - hScrollBar1.Minimum))
                    / (hScrollBar1.Maximum - hScrollBar1.Minimum);

                // Trigger a view port changed event to update the chart
                winChartViewer1.updateViewPort(true, false);
            }
        }

        //
        // Draw track cursor when mouse is moving over plotarea
        //
        private void winChartViewer1_MouseMovePlotArea(object sender, MouseEventArgs e)
        {
            WinChartViewer viewer = (WinChartViewer)sender;
            trackLineLabel((XYChart)viewer.Chart, viewer.PlotAreaMouseX);
            viewer.updateDisplay();
        }

        //
        // Pointer (Drag to Scroll) button event handler
        //
        private void pointerPB_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                winChartViewer1.MouseUsage = WinChartMouseUsage.ScrollOnDrag;
        }

        //
        // Zoom In button event handler
        //
        private void zoomInPB_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                winChartViewer1.MouseUsage = WinChartMouseUsage.ZoomIn;
        }

        //
        // Zoom Out button event handler
        //
        private void zoomOutPB_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                winChartViewer1.MouseUsage = WinChartMouseUsage.ZoomOut;
        }

        //
        // Save button event handler
        //
        private void savePB_Click(object sender, EventArgs e)
        {
            // The standard Save File dialog
            SaveFileDialog fileDlg = new SaveFileDialog();
            fileDlg.Filter = "PNG (*.png)|*.png|JPG (*.jpg)|*.jpg|GIF (*.gif)|*.gif|BMP (*.bmp)|*.bmp|" +
                "SVG (*.svg)|*.svg|PDF (*.pdf)|*.pdf";
            fileDlg.FileName = "chartdirector_demo";
            if (fileDlg.ShowDialog() != DialogResult.OK)
                return;

            // Save the chart
            if (null != winChartViewer1.Chart)
                winChartViewer1.Chart.makeChart(fileDlg.FileName);
        }
    }
}