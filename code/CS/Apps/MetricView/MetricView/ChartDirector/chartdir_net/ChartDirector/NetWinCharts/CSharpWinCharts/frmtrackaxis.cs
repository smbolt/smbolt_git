using System;
using System.Collections;
using System.Windows.Forms;
using ChartDirector;

namespace CSharpChartExplorer
{
    public partial class FrmTrackAxis : Form
    {
        public FrmTrackAxis()
        {
            InitializeComponent();
        }

        private void FrmTrackAxis_Load(object sender, EventArgs e)
        {
            // Data for the chart as 2 random data series
            RanSeries r = new RanSeries(127);
            double[] data0 = r.getSeries(180, 10, -1.5, 1.5);
            double[] data1 = r.getSeries(180, 150, -15, 15);
            DateTime[] timeStamps = r.getDateSeries(180, new DateTime(2011, 1, 1), 86400);

            // Create a XYChart object of size 670 x 400 pixels
            XYChart c = new XYChart(670, 400);

            // Add a title to the chart using 18pt Times New Roman Bold Italic font
            c.addTitle("Plasma Stabilizer Energy Usage", "Times New Roman Bold Italic", 18);

            // Set the plotarea at (50, 55) with width 100 pixels less than chart width, and height 90 pixels
            // less than chart height. Use a vertical gradient from light blue (f0f6ff) to sky blue (a0c0ff)
            // as background. Set border to transparent and grid lines to white (ffffff).
            c.setPlotArea(50, 55, c.getWidth() - 100, c.getHeight() - 90, c.linearGradientColor(0, 55, 0,
                c.getHeight() - 35, 0xf0f6ff, 0xa0c0ff), -1, Chart.Transparent, 0xffffff, 0xffffff);

            // Add a legend box at (50, 25) using horizontal layout. Use 10pt Arial Bold as font. Set the
            // background and border color to Transparent.
            c.addLegend(50, 25, false, "Arial Bold", 10).setBackground(Chart.Transparent);

            // Set axis label style to 8pt Arial Bold
            c.xAxis().setLabelStyle("Arial Bold", 8);
            c.yAxis().setLabelStyle("Arial Bold", 8);
            c.yAxis2().setLabelStyle("Arial Bold", 8);

            // Set the axis stem to transparent
            c.xAxis().setColors(Chart.Transparent);
            c.yAxis().setColors(Chart.Transparent);
            c.yAxis2().setColors(Chart.Transparent);

            // Configure x-axis label format
            c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "{value|mm/yyyy} ", Chart.StartOfMonthFilter(
                ), "{value|mm}");

            // Add axis title using 10pt Arial Bold Italic font
            c.yAxis().setTitle("Power Usage (Watt)", "Arial Bold Italic", 10);
            c.yAxis2().setTitle("Effective Load (kg)", "Arial Bold Italic", 10);

            // Add a line layer to the chart using a line width of 2 pixels.
            LineLayer layer = c.addLineLayer2();
            layer.setLineWidth(2);

            // Add 2 data series to the line layer
            layer.setXData(timeStamps);
            layer.addDataSet(data0, 0xcc0000, "Power Usage");
            layer.addDataSet(data1, 0x008800, "Effective Load").setUseYAxis2();

            // Assign the chart to the WinChartViewer
            winChartViewer1.Chart = c;
        }

        //
        // Draw track cursor when mouse is moving over plotarea
        //
        private void winChartViewer1_MouseMovePlotArea(object sender, MouseEventArgs e)
        {
            WinChartViewer viewer = (WinChartViewer)sender;
            trackLineAxis((XYChart)viewer.Chart, viewer.PlotAreaMouseX);
            viewer.updateDisplay();

            // Hide the track cursor when the mouse leaves the plot area
            viewer.removeDynamicLayer("MouseLeavePlotArea");
        }

        //
        // Draw track line with axis labels
        //
        private void trackLineAxis(XYChart c, int mouseX)
        {
            // Clear the current dynamic layer and get the DrawArea object to draw on it.
            DrawArea d = c.initDynamicLayer();

            // The plot area object
            PlotArea plotArea = c.getPlotArea();

            // Get the data x-value that is nearest to the mouse, and find its pixel coordinate.
            double xValue = c.getNearestXValue(mouseX);
            int xCoor = c.getXCoor(xValue);

            // The vertical track line is drawn up to the highest data point (the point with smallest
            // y-coordinate). We need to iterate all datasets in all layers to determine where it is.
            int minY = plotArea.getBottomY();

            // Iterate through all layers to find the highest data point
            for(int i = 0; i < c.getLayerCount(); ++i) {
                Layer layer = c.getLayerByZ(i);

                // The data array index of the x-value
                int xIndex = layer.getXIndexOf(xValue);

                // Iterate through all the data sets in the layer
                for(int j = 0; j < layer.getDataSetCount(); ++j) {
                    ChartDirector.DataSet dataSet = layer.getDataSetByZ(j);

                    double dataPoint = dataSet.getPosition(xIndex);
                    if ((dataPoint != Chart.NoValue) && (dataSet.getDataColor() != Chart.Transparent)) {
                        minY = Math.Min(minY, c.getYCoor(dataPoint, dataSet.getUseYAxis()));
                    }
                }
            }

            // Draw a vertical track line at the x-position up to the highest data point.
            d.vline(Math.Max(minY, plotArea.getTopY()), plotArea.getBottomY() + 6, xCoor, d.dashLineColor(
                0x000000, 0x0101));

            // Draw a label on the x-axis to show the track line position
            d.text("<*font,bgColor=000000*> " + c.xAxis().getFormattedLabel(xValue, "mmm dd, yyyy") +
                " <*/font*>", "Arial Bold", 8).draw(xCoor, plotArea.getBottomY() + 6, 0xffffff, Chart.Top);

            // Iterate through all layers to build the legend array
            for(int i = 0; i < c.getLayerCount(); ++i) {
                Layer layer = c.getLayerByZ(i);

                // The data array index of the x-value
                int xIndex = layer.getXIndexOf(xValue);

                // Iterate through all the data sets in the layer
                for(int j = 0; j < layer.getDataSetCount(); ++j) {
                    ChartDirector.DataSet dataSet = layer.getDataSetByZ(j);

                    // The positional value, axis binding, pixel coordinate and color of the data point.
                    double dataPoint = dataSet.getPosition(xIndex);
                    Axis yAxis = dataSet.getUseYAxis();
                    int yCoor = c.getYCoor(dataPoint, yAxis);
                    int color = dataSet.getDataColor();

                    // Draw the axis label only for visible data points of named data sets
                    if ((dataPoint != Chart.NoValue) && (color != Chart.Transparent) && (yCoor >=
                        plotArea.getTopY()) && (yCoor <= plotArea.getBottomY())) {
                        // The axis label consists of 3 parts - a track dot for the data point, an axis label,
                        // and a line joining the track dot to the axis label.

                        // Draw the line first. The end point of the line at the axis label side depends on
                        // whether the label is at the left or right side of the axis (that is, on whether the
                        // axis is on the left or right side of the plot area).
                        int xPos = yAxis.getX() + ((yAxis.getAlignment() == Chart.Left) ? -4 : 4);
                        d.hline(xCoor, xPos, yCoor, d.dashLineColor(color, 0x0101));

                        // Draw the track dot
                        d.circle(xCoor, yCoor, 4, 4, color, color);

                        // Draw the axis label. If the axis is on the left side of the plot area, the labels
                        // should right aligned to the axis, and vice versa.
                        d.text("<*font,bgColor=" + color.ToString("x") + "*> " + c.formatValue(dataPoint,
                            "{value|P4}") + " <*/font*>", "Arial Bold", 8).draw(xPos, yCoor, 0xffffff, ((
                            yAxis.getAlignment() == Chart.Left) ? Chart.Right : Chart.Left));
                    }
                }
            }
        }
    }
}
