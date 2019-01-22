using System;
using System.Collections;
using System.Windows.Forms;
using ChartDirector;

namespace CSharpChartExplorer
{
    public partial class FrmTrackLabel : Form
    {
        public FrmTrackLabel()
        {
            InitializeComponent();
        }

        private void FrmTrackLabel_Load(object sender, EventArgs e)
        {
            // Data for the chart as 3 random data series
            RanSeries r = new RanSeries(127);
            double[] data0 = r.getSeries(100, 100, -15, 15);
            double[] data1 = r.getSeries(100, 150, -15, 15);
            double[] data2 = r.getSeries(100, 200, -15, 15);
            DateTime[] timeStamps = r.getDateSeries(100, new DateTime(2011, 1, 1), 86400);

            // Create a XYChart object of size 640 x 400 pixels
            XYChart c = new XYChart(640, 400);

            // Add a title to the chart using 18pt Times New Roman Bold Italic font
            c.addTitle("    Product Line Global Revenue", "Times New Roman Bold Italic", 18);

            // Set the plotarea at (50, 55) with width 70 pixels less than chart width, and height 90 pixels
            // less than chart height. Use a vertical gradient from light blue (f0f6ff) to sky blue (a0c0ff)
            // as background. Set border to transparent and grid lines to white (ffffff).
            c.setPlotArea(50, 55, c.getWidth() - 70, c.getHeight() - 90, c.linearGradientColor(0, 55, 0,
                c.getHeight() - 35, 0xf0f6ff, 0xa0c0ff), -1, Chart.Transparent, 0xffffff, 0xffffff);

            // Add a legend box at (50, 25) using horizontal layout. Use 10pt Arial Bold as font. Set the
            // background and border color to Transparent.
            c.addLegend(50, 25, false, "Arial Bold", 10).setBackground(Chart.Transparent);

            // Set axis label style to 8pt Arial Bold
            c.xAxis().setLabelStyle("Arial Bold", 8);
            c.yAxis().setLabelStyle("Arial Bold", 8);

            // Set the axis stem to transparent
            c.xAxis().setColors(Chart.Transparent);
            c.yAxis().setColors(Chart.Transparent);

            // Configure x-axis label format
            c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "{value|mm/yyyy} ", Chart.StartOfMonthFilter(
                ), "{value|mm}");

            // Add axis title using 10pt Arial Bold Italic font
            c.yAxis().setTitle("USD millions", "Arial Bold Italic", 10);

            // Add a line layer to the chart using a line width of 2 pixels.
            LineLayer layer = c.addLineLayer2();
            layer.setLineWidth(2);

            // Add 3 data series to the line layer
            layer.setXData(timeStamps);
            layer.addDataSet(data0, 0xff3333, "Alpha");
            layer.addDataSet(data1, 0x008800, "Beta");
            layer.addDataSet(data2, 0x3333cc, "Gamma");

            // Assign the chart to the WinChartViewer
            winChartViewer1.Chart = c;
        }

        //
        // Draw track cursor when mouse is moving over plotarea
        //
        private void winChartViewer1_MouseMovePlotArea(object sender, MouseEventArgs e)
        {
            WinChartViewer viewer = (WinChartViewer)sender;
            trackLineLabel((XYChart)viewer.Chart, viewer.PlotAreaMouseX);
            viewer.updateDisplay();

            // Hide the track cursor when the mouse leaves the plot area
            viewer.removeDynamicLayer("MouseLeavePlotArea");
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

            // Draw a vertical track line at the x-position
            d.vline(plotArea.getTopY(), plotArea.getBottomY(), xCoor, d.dashLineColor(0x000000, 0x0101));

            // Draw a label on the x-axis to show the track line position.
            string xlabel = "<*font,bgColor=000000*> " + c.xAxis().getFormattedLabel(xValue, "mmm dd, yyyy") +
                " <*/font*>";
            TTFText t = d.text(xlabel, "Arial Bold", 8);

            // Restrict the x-pixel position of the label to make sure it stays inside the chart image.
            int xLabelPos = Math.Max(0, Math.Min(xCoor - t.getWidth() / 2, c.getWidth() - t.getWidth()));
            t.draw(xLabelPos, plotArea.getBottomY() + 6, 0xffffff);

            // Iterate through all layers to draw the data labels
            for(int i = 0; i < c.getLayerCount(); ++i) {
                Layer layer = c.getLayerByZ(i);

                // The data array index of the x-value
                int xIndex = layer.getXIndexOf(xValue);

                // Iterate through all the data sets in the layer
                for(int j = 0; j < layer.getDataSetCount(); ++j) {
                    ChartDirector.DataSet dataSet = layer.getDataSetByZ(j);

                    // Get the color and position of the data label
                    int color = dataSet.getDataColor();
                    int yCoor = c.getYCoor(dataSet.getPosition(xIndex), dataSet.getUseYAxis());

                    // Draw a track dot with a label next to it for visible data points in the plot area
                    if ((yCoor >= plotArea.getTopY()) && (yCoor <= plotArea.getBottomY()) && (color !=
                        Chart.Transparent)) {

                        d.circle(xCoor, yCoor, 4, 4, color, color);

                        string label = "<*font,bgColor=" + color.ToString("x") + "*> " + c.formatValue(
                            dataSet.getValue(xIndex), "{value|P4}") + " <*/font*>";
                        t = d.text(label, "Arial Bold", 8);

                        // Draw the label on the right side of the dot if the mouse is on the left side the
                        // chart, and vice versa. This ensures the label will not go outside the chart image.
                        if (xCoor <= (plotArea.getLeftX() + plotArea.getRightX()) / 2) {
                            t.draw(xCoor + 5, yCoor, 0xffffff, Chart.Left);
                        } else {
                            t.draw(xCoor - 5, yCoor, 0xffffff, Chart.Right);
                        }
                    }
                }
            }
        }
    }
}
