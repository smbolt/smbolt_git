using System;
using ChartDirector;

namespace CSharpChartExplorer
{
    public class barlabel : DemoModule
    {
        //Name of demo module
        public string getName() { return "Bar Labels"; }

        //Number of charts produced in this demo module
        public int getNoOfCharts() { return 1; }

        //Main code for creating chart.
        //Note: the argument chartIndex is unused because this demo only has 1 chart.
        public void createChart(WinChartViewer viewer, int chartIndex)
        {
            // The data for the bar chart
            double[] data = {85, 156, 179, 211, 123, 189, 166};

            // The labels for the bar chart
            string[] labels = {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"};

            // Create a XYChart object of size 600 x 360 pixels
            XYChart c = new XYChart(600, 360);

            // Set default text color to dark grey (0x333333)
            c.setColor(Chart.TextColor, 0x333333);

            // Set the plotarea at (70, 20) and of size 500 x 300 pixels, with transparent
            // background and border and light grey (0xcccccc) horizontal grid lines
            c.setPlotArea(70, 20, 500, 300, Chart.Transparent, -1, Chart.Transparent, 0xcccccc);

            // Set the x and y axis stems to transparent and the label font to 12pt Arial
            c.xAxis().setColors(Chart.Transparent);
            c.yAxis().setColors(Chart.Transparent);
            c.xAxis().setLabelStyle("Arial", 12);
            c.yAxis().setLabelStyle("Arial", 12);

            // Add a blue (0x6699bb) bar chart layer using the given data
            BarLayer layer = c.addBarLayer(data, 0x6699bb);

            // Use bar gradient lighting with the light intensity from 0.8 to 1.3
            layer.setBorderColor(Chart.Transparent, Chart.barLighting(0.8, 1.3));

            // Set rounded corners for bars
            layer.setRoundedCorners();

            // Display labela on top of bars using 12pt Arial font
            layer.setAggregateLabelStyle("Arial", 12);

            // Set the labels on the x axis.
            c.xAxis().setLabels(labels);

            // For the automatic y-axis labels, set the minimum spacing to 40 pixels.
            c.yAxis().setTickDensity(40);

            // Add a title to the y axis using dark grey (0x555555) 14pt Arial Bold font
            c.yAxis().setTitle("Y-Axis Title Placeholder", "Arial Bold", 14, 0x555555);

            // Output the chart
            viewer.Chart = c;

            //include tool tip for the chart
            viewer.ImageMap = c.getHTMLImageMap("clickable", "", "title='{xLabel}: ${value}M'");
        }
    }
}

