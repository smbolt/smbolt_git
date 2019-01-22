using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
    public class LogaxisController : Controller
    {
        //
        // Default Action
        //
        public ActionResult Index()
        {
            ViewBag.Title = "Log Scale Axis";

            // This example contains 2 charts.
            ViewBag.Viewer = new RazorChartViewer[2];
            for (int i = 0; i < ViewBag.Viewer.Length; ++i)
                createChart(ViewBag.Viewer[i] = new RazorChartViewer(HttpContext, "chart" + i), i);

            return View("~/Views/Shared/ChartView.cshtml");
        }

        //
        // Create chart
        //
        private void createChart(RazorChartViewer viewer, int chartIndex)
        {
            // The data for the chart
            double[] data = {100, 125, 260, 147, 67};
            string[] labels = {"Mon", "Tue", "Wed", "Thu", "Fri"};

            // Create a XYChart object of size 200 x 180 pixels
            XYChart c = new XYChart(200, 180);

            // Set the plot area at (30, 10) and of size 140 x 130 pixels
            c.setPlotArea(30, 10, 140, 130);

            // Ise log scale axis if required
            if (chartIndex == 1) {
                c.yAxis().setLogScale3();
            }

            // Set the labels on the x axis
            c.xAxis().setLabels(labels);

            // Add a color bar layer using the given data. Use a 1 pixel 3D border for the bars.
            c.addBarLayer3(data).setBorderColor(-1, 1);

            // Output the chart
            viewer.Image = c.makeWebImage(Chart.PNG);

            // Include tool tip for the chart
            viewer.ImageMap = c.getHTMLImageMap("", "", "title='Mileage on {xLabel}: {value} miles'");
        }
    }
}

