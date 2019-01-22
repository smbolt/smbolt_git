using System;
using System.Collections;
using System.Windows.Forms;
using ChartDirector;

namespace CSharpChartExplorer
{
    public partial class FrmTrackBox : Form
    {
        public FrmTrackBox()
        {
            InitializeComponent();
        }

        private void FrmTrackBox_Load(object sender, EventArgs e)
        {
            // The data for the bar chart
            double[] data0 = {100, 125, 245, 147, 67};
            double[] data1 = {85, 156, 179, 211, 123};
            double[] data2 = {97, 87, 56, 267, 157};
            string[] labels = {"Mon", "Tue", "Wed", "Thur", "Fri"};

            // Create a XYChart object of size 540 x 375 pixels
            XYChart c = new XYChart(540, 375);

            // Add a title to the chart using 18pt Times Bold Italic font
            c.addTitle("Average Weekly Network Load", "Times New Roman Bold Italic", 18);

            // Set the plotarea at (50, 55) and of 440 x 280 pixels in size. Use a vertical gradient color
            // from light blue (f9f9ff) to blue (6666ff) as background. Set border and grid lines to white
            // (ffffff).
            c.setPlotArea(50, 55, 440, 280, c.linearGradientColor(0, 55, 0, 335, 0xf9f9ff, 0x6666ff), -1,
                0xffffff, 0xffffff);

            // Add a legend box at (50, 28) using horizontal layout. Use 10pt Arial Bold as font, with
            // transparent background.
            c.addLegend(50, 28, false, "Arial Bold", 10).setBackground(Chart.Transparent);

            // Set the x axis labels
            c.xAxis().setLabels(labels);

            // Draw the ticks between label positions (instead of at label positions)
            c.xAxis().setTickOffset(0.5);

            // Set axis label style to 8pt Arial Bold
            c.xAxis().setLabelStyle("Arial Bold", 8);
            c.yAxis().setLabelStyle("Arial Bold", 8);

            // Set axis line width to 2 pixels
            c.xAxis().setWidth(2);
            c.yAxis().setWidth(2);

            // Add axis title
            c.yAxis().setTitle("Throughput (MBytes Per Hour)");

            // Add a multi-bar layer with 3 data sets
            BarLayer layer = c.addBarLayer2(Chart.Side);
            layer.addDataSet(data0, 0xff0000, "Server #1");
            layer.addDataSet(data1, 0x00ff00, "Server #2");
            layer.addDataSet(data2, 0xff8800, "Server #3");

            // Set bar border to transparent. Use glass lighting effect with light direction from left.
            layer.setBorderColor(Chart.Transparent, Chart.glassEffect(Chart.NormalGlare, Chart.Left));

            // Configure the bars within a group to touch each others (no gap)
            layer.setBarGap(0.2, Chart.TouchBar);

            // Assign the chart to the WinChartViewer
            winChartViewer1.Chart = c;
        }

        //
        // Draw track cursor when mouse is moving over plotarea
        //
        private void winChartViewer1_MouseMovePlotArea(object sender, MouseEventArgs e)
        {
            WinChartViewer viewer = (WinChartViewer)sender;
            trackBoxLegend((XYChart)viewer.Chart, viewer.PlotAreaMouseX, viewer.PlotAreaMouseY);
            viewer.updateDisplay();

            // Hide the track cursor when the mouse leaves the plot area
            viewer.removeDynamicLayer("MouseLeavePlotArea");
        }

        //
        // Draw the track box with legend
        //
        private void trackBoxLegend(XYChart c, int mouseX, int mouseY)
        {
            // Clear the current dynamic layer and get the DrawArea object to draw on it.
            DrawArea d = c.initDynamicLayer();

            // The plot area object
            PlotArea plotArea = c.getPlotArea();

            // Get the data x-value that is nearest to the mouse
            double xValue = c.getNearestXValue(mouseX);

            // Compute the position of the box. This example assumes a label based x-axis, in which the
            // labeling spacing is one x-axis unit. So the left and right sides of the box is 0.5 unit from
            // the central x-value.
            int boxLeft = c.getXCoor(xValue - 0.5);
            int boxRight = c.getXCoor(xValue + 0.5);
            int boxTop = plotArea.getTopY();
            int boxBottom = plotArea.getBottomY();

            // Draw the track box
            d.rect(boxLeft, boxTop, boxRight, boxBottom, 0x000000, Chart.Transparent);

            // Container to hold the legend entries
            ArrayList legendEntries = new ArrayList();

            // Iterate through all layers to build the legend array
            for(int i = 0; i < c.getLayerCount(); ++i) {
                Layer layer = c.getLayerByZ(i);

                // The data array index of the x-value
                int xIndex = layer.getXIndexOf(xValue);

                // Iterate through all the data sets in the layer
                for(int j = 0; j < layer.getDataSetCount(); ++j) {
                    ChartDirector.DataSet dataSet = layer.getDataSetByZ(j);

                    // Build the legend entry, consist of the legend icon, the name and the data value.
                    double dataValue = dataSet.getValue(xIndex);
                    if ((dataValue != Chart.NoValue) && (dataSet.getDataColor() != Chart.Transparent)) {
                        legendEntries.Add(dataSet.getLegendIcon() + " " + dataSet.getDataName() + ": " +
                            c.formatValue(dataValue, "{value|P4}"));
                    }
                }
            }

            // Create the legend by joining the legend entries
            if (legendEntries.Count > 0) {
                legendEntries.Reverse();
                string legend = "<*block,bgColor=FFFFCC,edgeColor=000000,margin=5*><*font,underline=1*>" +
                    c.xAxis().getFormattedLabel(xValue) + "<*/font*><*br*>" + String.Join("<*br*>", (string[])
                    legendEntries.ToArray(typeof(string))) + "<*/*>";

                // Display the legend at the bottom-right side of the mouse cursor, and make sure the legend
                // will not go outside the chart image.
                TTFText t = d.text(legend, "Arial Bold", 8);
                t.draw(Math.Min(mouseX + 12, c.getWidth() - t.getWidth()), Math.Min(mouseY + 18, c.getHeight()
                     - t.getHeight()), 0x000000, Chart.TopLeft);
            }
        }
    }
}
