using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
    public class DepthbarController : Controller
    {
        //
        // Default Action
        //
        public ActionResult Index()
        {
            ViewBag.Title = "Depth Bar Chart";
            createChart(ViewBag.Viewer = new RazorChartViewer(HttpContext, "chart1"));
            return View("~/Views/Shared/ChartView.cshtml");
        }

        //
        // Create chart
        //
        private void createChart(RazorChartViewer viewer)
        {
            // The data for the bar chart
            double[] data0 = {100, 125, 245, 147, 67};
            double[] data1 = {85, 156, 179, 211, 123};
            double[] data2 = {97, 87, 56, 267, 157};

            // The labels for the bar chart
            string[] labels = {"Mon", "Tue", "Wed", "Thu", "Fri"};

            // Create a XYChart object of size 500 x 320 pixels
            XYChart c = new XYChart(500, 320);

            // Set the plotarea at (100, 40) and of size 280 x 240 pixels
            c.setPlotArea(100, 40, 280, 240);

            // Add a legend box at (405, 100)
            c.addLegend(405, 100);

            // Add a title to the chart
            c.addTitle("Weekday Network Load");

            // Add a title to the y axis. Draw the title upright (font angle = 0)
            c.yAxis().setTitle("Average\nWorkload\n(MBytes\nPer Hour)").setFontAngle(0);

            // Set the labels on the x axis
            c.xAxis().setLabels(labels);

            // Add three bar layers, each representing one data set. The bars are drawn in
            // semi-transparent colors.
            c.addBarLayer(data0, unchecked((int)0x808080ff), "Server # 1", 5);
            c.addBarLayer(data1, unchecked((int)0x80ff0000), "Server # 2", 5);
            c.addBarLayer(data2, unchecked((int)0x8000ff00), "Server # 3", 5);

            // Output the chart
            viewer.Image = c.makeWebImage(Chart.PNG);

            // Include tool tip for the chart
            viewer.ImageMap = c.getHTMLImageMap("", "",
                "title='{dataSetName} on {xLabel}: {value} MBytes/hour'");
        }
    }
}

