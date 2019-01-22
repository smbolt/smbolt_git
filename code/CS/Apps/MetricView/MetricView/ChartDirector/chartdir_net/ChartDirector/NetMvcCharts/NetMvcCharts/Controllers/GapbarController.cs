using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
    public class GapbarController : Controller
    {
        //
        // Default Action
        //
        public ActionResult Index()
        {
            ViewBag.Title = "Bar Gap";

            // This example contains 6 charts.
            ViewBag.Viewer = new RazorChartViewer[6];
            for (int i = 0; i < ViewBag.Viewer.Length; ++i)
                createChart(ViewBag.Viewer[i] = new RazorChartViewer(HttpContext, "chart" + i), i);

            return View("~/Views/Shared/ChartView.cshtml");
        }

        //
        // Create chart
        //
        private void createChart(RazorChartViewer viewer, int chartIndex)
        {
            double bargap = chartIndex * 0.25 - 0.25;

            // The data for the bar chart
            double[] data = {100, 125, 245, 147, 67};

            // The labels for the bar chart
            string[] labels = {"Mon", "Tue", "Wed", "Thu", "Fri"};

            // Create a XYChart object of size 150 x 150 pixels
            XYChart c = new XYChart(150, 150);

            // Set the plotarea at (27, 20) and of size 120 x 100 pixels
            c.setPlotArea(27, 20, 120, 100);

            // Set the labels on the x axis
            c.xAxis().setLabels(labels);

            if (bargap >= 0) {
                // Add a title to display to bar gap using 8pt Arial font
                c.addTitle("      Bar Gap = " + bargap, "Arial", 8);
            } else {
                // Use negative value to mean TouchBar
                c.addTitle("      Bar Gap = TouchBar", "Arial", 8);
                bargap = Chart.TouchBar;
            }

            // Add a bar chart layer using the given data and set the bar gap
            c.addBarLayer(data).setBarGap(bargap);

            // Output the chart
            viewer.Image = c.makeWebImage(Chart.PNG);

            // Include tool tip for the chart
            viewer.ImageMap = c.getHTMLImageMap("", "", "title='Production on {xLabel}: {value} kg'");
        }
    }
}

