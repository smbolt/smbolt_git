using System;
using ChartDirector;

namespace CSharpChartExplorer
{
    public class contourcolor : DemoModule
    {
        //Name of demo module
        public string getName() { return "Contour Color Scale"; }

        //Number of charts produced in this demo module
        public int getNoOfCharts() { return 4; }

        //Main code for creating charts
        public void createChart(WinChartViewer viewer, int chartIndex)
        {
            // The x and y coordinates of the grid
            double[] dataX = {-4, -3, -2, -1, 0, 1, 2, 3, 4};
            double[] dataY = {-4, -3, -2, -1, 0, 1, 2, 3, 4};

            // Use random numbers for the z values on the XY grid
            RanSeries r = new RanSeries(99);
            double[] dataZ = r.get2DSeries(dataX.Length, dataY.Length, -0.9, 0.9);

            // Create a XYChart object of size 420 x 360 pixels
            XYChart c = new XYChart(420, 360);

            // Set the plotarea at (30, 25) and of size 300 x 300 pixels. Use semi-transparent grey
            // (0xdd000000) horizontal and vertical grid lines
            c.setPlotArea(30, 25, 300, 300, -1, -1, -1, unchecked((int)0xdd000000), -1);

            // Set the x-axis and y-axis scale
            c.xAxis().setLinearScale(-4, 4, 1);
            c.yAxis().setLinearScale(-4, 4, 1);

            // Add a contour layer using the given data
            ContourLayer layer = c.addContourLayer(dataX, dataY, dataZ);

            // Move the grid lines in front of the contour layer
            c.getPlotArea().moveGridBefore(layer);

            // Add a color axis (the legend) in which the top left corner is anchored at (350, 25).
            // Set the length to 400 300 and the labels on the right side.
            ColorAxis cAxis = layer.setColorAxis(350, 25, Chart.TopLeft, 300, Chart.Right);

            if (chartIndex == 1) {
                // Speicify a color gradient as a list of colors, and use it in the color axis.
                int[] colorGradient = {0x0044cc, 0xffffff, 0x00aa00};
                cAxis.setColorGradient(false, colorGradient);
            } else if (chartIndex == 2) {
                // Specify the color scale to use in the color axis
                double[] colorScale = {-1.0, 0x1a9850, -0.75, 0x66bd63, -0.5, 0xa6d96a, -0.25,
                    0xd9ef8b, 0, 0xfee08b, 0.25, 0xfdae61, 0.5, 0xf46d43, 0.75, 0xd73027, 1};
                cAxis.setColorScale(colorScale);
            } else if (chartIndex == 3) {
                // Specify the color scale to use in the color axis. Also specify an underflow color
                // 0x66ccff (blue) for regions that fall below the lower axis limit.
                double[] colorScale = {0, 0xffff99, 0.2, 0x80cdc1, 0.4, 0x35978f, 0.6, 0x01665e,
                    0.8, 0x003c30, 1};
                cAxis.setColorScale(colorScale, 0x66ccff);
            }

            // Output the chart
            viewer.Chart = c;
        }
    }
}

