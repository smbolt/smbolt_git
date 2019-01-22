using System;
using System.Collections;
using System.Windows.Forms;
using ChartDirector;

namespace CSharpChartExplorer
{
    public partial class FrmZoomScrollTrack : Form
    {
        // Data arrays
        private DateTime[] timeStamps;
        private double[] dataSeriesA;
        private double[] dataSeriesB;
        private double[] dataSeriesC;
        
        // Flag to indicated if initialization has been completed. Prevents events from firing before 
        // controls are properly initialized.
        private bool hasFinishedInitialization;

        public FrmZoomScrollTrack()
        {
            InitializeComponent();
        }

        private void FrmZoomScrollTrack_Load(object sender, EventArgs e)
        {
            // Load the data
            loadData();

            // Initialize the WinChartViewer
            initChartViewer(winChartViewer1);

            // Can handle events now
            hasFinishedInitialization = true;

            // Trigger the ViewPortChanged event to draw the chart
            winChartViewer1.updateViewPort(true, true);
        }

        //
        // Load the data
        //
        private void loadData()
        {
            // In this example, we just use random numbers as data.
            RanSeries r = new RanSeries(127);
            timeStamps = r.getDateSeries(1827, new DateTime(2007, 1, 1), 86400);
            dataSeriesA = r.getSeries2(1827, 150, -10, 10);
            dataSeriesB = r.getSeries2(1827, 200, -10, 10);
            dataSeriesC = r.getSeries2(1827, 250, -8, 8);
        }

        //
        // Initialize the WinChartViewer
        //
        private void initChartViewer(WinChartViewer viewer)
        {
            // Set the full x range to be the duration of the data
            viewer.setFullRange("x", timeStamps[0], timeStamps[timeStamps.Length - 1]);

            // Initialize the view port to show the latest 20% of the time range
            viewer.ViewPortWidth = 0.2;
            viewer.ViewPortLeft = 1 - viewer.ViewPortWidth;

            // Set the maximum zoom to 10 points
            viewer.ZoomInWidthLimit = 10.0 / timeStamps.Length;

            // Enable mouse wheel zooming by setting the zoom ratio to 1.1 per wheel event
            viewer.MouseWheelZoomRatio = 1.1;
            
            // Initially set the mouse usage to "Pointer" mode (Drag to Scroll mode)
            pointerPB.Checked = true;
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
        // Update controls when the view port changed
        //
        private void updateControls(WinChartViewer viewer)
        {
            // In this demo, we need to update the scroll bar to reflect the view port position and
            // width of the view port.
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

            // Get the array indexes that corresponds to the visible start and end dates
            int startIndex = (int)Math.Floor(Chart.bSearch(timeStamps, viewPortStartDate));
            int endIndex = (int)Math.Ceiling(Chart.bSearch(timeStamps, viewPortEndDate));
            int noOfPoints = endIndex - startIndex + 1;

            // Extract the part of the data array that are visible.
            DateTime[] viewPortTimeStamps = (DateTime[])Chart.arraySlice(timeStamps, startIndex, noOfPoints);
            double[] viewPortDataSeriesA = (double[])Chart.arraySlice(dataSeriesA, startIndex, noOfPoints);
            double[] viewPortDataSeriesB = (double[])Chart.arraySlice(dataSeriesB, startIndex, noOfPoints);
            double[] viewPortDataSeriesC = (double[])Chart.arraySlice(dataSeriesC, startIndex, noOfPoints);

            //
            // At this stage, we have extracted the visible data. We can use those data to plot the chart.
            //

            //================================================================================
            // Configure overall chart appearance.
            //================================================================================

            // Create an XYChart object of size 640 x 350 pixels
            XYChart c = new XYChart(640, 350);

            // Set the plotarea at (55, 55) with width 80 pixels less than chart width, and height 90 pixels
            // less than chart height. Use a vertical gradient from light blue (f0f6ff) to sky blue (a0c0ff)
            // as background. Set border to transparent and grid lines to white (ffffff).
            c.setPlotArea(55, 55, c.getWidth() - 80, c.getHeight() - 90, c.linearGradientColor(0, 55, 0,
                c.getHeight() - 35, 0xf0f6ff, 0xa0c0ff), -1, Chart.Transparent, 0xffffff, 0xffffff);

            // As the data can lie outside the plotarea in a zoomed chart, we need enable clipping.
            c.setClipping();

            // Add a title to the chart using 18 pts Times New Roman Bold Italic font
            c.addTitle("   Zooming and Scrolling with Track Line (1)", "Times New Roman Bold Italic", 18);

            // Set legend icon style to use line style icon, sized for 8pt font
            c.getLegend().setLineStyleKey();
            c.getLegend().setFontSize(8);

            // Set the axis stem to transparent
            c.xAxis().setColors(Chart.Transparent);
            c.yAxis().setColors(Chart.Transparent);

            // Add axis title using 10pts Arial Bold Italic font
            c.yAxis().setTitle("Ionic Temperature (C)", "Arial Bold Italic", 10);

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

            // In this demo, we do not have too many data points. In real code, the chart may contain a lot
            // of data points when fully zoomed out - much more than the number of horizontal pixels in this
            // plot area. So it is a good idea to use fast line mode.
            layer.setFastLineMode();

            // Now we add the 3 data series to a line layer, using the color red (ff33333), green (008800)
            // and blue (3333cc)
            layer.setXData(viewPortTimeStamps);
            layer.addDataSet(viewPortDataSeriesA, 0xff3333, "Alpha");
            layer.addDataSet(viewPortDataSeriesB, 0x008800, "Beta");
            layer.addDataSet(viewPortDataSeriesC, 0x3333cc, "Gamma");

            //================================================================================
            // Configure axis scale and labelling
            //================================================================================

            // Set the x-axis as a date/time axis with the scale according to the view port x range.
            viewer.syncDateAxisWithViewPort("x", c.xAxis());

            //
            // In this demo, the time range can be from a few years to a few days. We demonstrate how to set
            // up different date/time format based on the time range.
            //

            // If all ticks are yearly aligned, then we use "yyyy" as the label format.
            c.xAxis().setFormatCondition("align", 360 * 86400);
            c.xAxis().setLabelFormat("{value|yyyy}");

            // If all ticks are monthly aligned, then we use "mmm yyyy" in bold font as the first label of a
            // year, and "mmm" for other labels.
            c.xAxis().setFormatCondition("align", 30 * 86400);
            c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "<*font=bold*>{value|mmm yyyy}",
                Chart.AllPassFilter(), "{value|mmm}");

            // If all ticks are daily algined, then we use "mmm dd<*br*>yyyy" in bold font as the first
            // label of a year, and "mmm dd" in bold font as the first label of a month, and "dd" for other
            // labels.
            c.xAxis().setFormatCondition("align", 86400);
            c.xAxis().setMultiFormat(Chart.StartOfYearFilter(),
                "<*block,halign=left*><*font=bold*>{value|mmm dd<*br*>yyyy}", Chart.StartOfMonthFilter(),
                "<*font=bold*>{value|mmm dd}");
            c.xAxis().setMultiFormat2(Chart.AllPassFilter(), "{value|dd}");

            // For all other cases (sub-daily ticks), use "hh:nn<*br*>mmm dd" for the first label of a day,
            // and "hh:nn" for other labels.
            c.xAxis().setFormatCondition("else");
            c.xAxis().setMultiFormat(Chart.StartOfDayFilter(), "<*font=bold*>{value|hh:nn<*br*>mmm dd}",
                Chart.AllPassFilter(), "{value|hh:nn}");

            //================================================================================
            // Output the chart
            //================================================================================

            // We need to update the track line too. If the mouse is moving on the chart (eg. if 
            // the user drags the mouse on the chart to scroll it), the track line will be updated
            // in the MouseMovePlotArea event. Otherwise, we need to update the track line here.
            if (!viewer.IsInMouseMoveEvent)
            {
                trackLineLegend(c, (null == viewer.Chart) ? c.getPlotArea().getRightX() : 
                    viewer.PlotAreaMouseX);
            }
            
            viewer.Chart = c;
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
        // The scroll bar event handler
        //
        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            // When the view port is changed (user drags on the chart to scroll), the scroll bar will get
            // updated. When the scroll bar changes (eg. user drags on the scroll bar), the view port will
            // get updated. This creates an infinite loop. To avoid this, the scroll bar can update the 
            // view port only if the view port is not updating the scroll bar.
            if (hasFinishedInitialization && !winChartViewer1.IsInViewPortChangedEvent)
            {
                // Set the view port based on the scroll bar
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
            trackLineLegend((XYChart)viewer.Chart, viewer.PlotAreaMouseX);
            viewer.updateDisplay();
        }

        //
        // Draw the track line with legend
        //
        private void trackLineLegend(XYChart c, int mouseX)
        {
            // Clear the current dynamic layer and get the DrawArea object to draw on it.
            DrawArea d = c.initDynamicLayer();

            // The plot area object
            PlotArea plotArea = c.getPlotArea();

            // Get the data x-value that is nearest to the mouse, and find its pixel coordinate.
            double xValue = c.getNearestXValue(mouseX);
            int xCoor = c.getXCoor(xValue);

            // Draw a vertical track line at the x-position
            d.vline(plotArea.getTopY(), plotArea.getBottomY(), xCoor, d.dashLineColor(0x000000, 0x0101));

            // Container to hold the legend entries
            ArrayList legendEntries = new ArrayList();

            // Iterate through all layers to build the legend array
            for (int i = 0; i < c.getLayerCount(); ++i) {
                Layer layer = c.getLayerByZ(i);

                // The data array index of the x-value
                int xIndex = layer.getXIndexOf(xValue);

                // Iterate through all the data sets in the layer
                for (int j = 0; j < layer.getDataSetCount(); ++j) {
                    ChartDirector.DataSet dataSet = layer.getDataSetByZ(j);

                    // We are only interested in visible data sets with names
                    string dataName = dataSet.getDataName();
                    int color = dataSet.getDataColor();
                    if ((!string.IsNullOrEmpty(dataName)) && (color != Chart.Transparent)) {
                        // Build the legend entry, consist of the legend icon, name and data value.
                        double dataValue = dataSet.getValue(xIndex);
                        legendEntries.Add("<*block*>" + dataSet.getLegendIcon() + " " + dataName + ": " + ((
                            dataValue == Chart.NoValue) ? "N/A" : c.formatValue(dataValue, "{value|P4}")) +
                            "<*/*>");

                        // Draw a track dot for data points within the plot area
                        int yCoor = c.getYCoor(dataSet.getPosition(xIndex), dataSet.getUseYAxis());
                        if ((yCoor >= plotArea.getTopY()) && (yCoor <= plotArea.getBottomY())) {
                            d.circle(xCoor, yCoor, 4, 4, color, color);
                        }
                    }
                }
            }

            // Create the legend by joining the legend entries
            legendEntries.Reverse();
            string legendText = "<*block,maxWidth=" + plotArea.getWidth() + "*><*block*><*font=Arial Bold*>["
                 + c.xAxis().getFormattedLabel(xValue, "mmm dd, yyyy") + "]<*/*>        " + String.Join(
                "        ", (string[])legendEntries.ToArray(typeof(string))) + "<*/*>";

            // Display the legend on the top of the plot area
            TTFText t = d.text(legendText, "Arial", 8);
            t.draw(plotArea.getLeftX() + 5, plotArea.getTopY() - 3, 0x000000, Chart.BottomLeft);
        }
    }
}