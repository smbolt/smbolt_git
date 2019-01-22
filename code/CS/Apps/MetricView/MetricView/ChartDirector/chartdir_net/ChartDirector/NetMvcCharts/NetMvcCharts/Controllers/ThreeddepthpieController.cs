using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
    public class ThreeddepthpieController : Controller
    {
        //
        // Default Action
        //
        public ActionResult Index()
        {
            ViewBag.Title = "3D Depth";

            // This example contains 5 charts.
            ViewBag.Viewer = new RazorChartViewer[5];
            for (int i = 0; i < ViewBag.Viewer.Length; ++i)
                createChart(ViewBag.Viewer[i] = new RazorChartViewer(HttpContext, "chart" + i), i);

            return View("~/Views/Shared/ChartView.cshtml");
        }

        //
        // Create chart
        //
        private void createChart(RazorChartViewer viewer, int chartIndex)
        {
            // the tilt angle of the pie
            int depth = chartIndex * 5 + 5;

            // The data for the pie chart
            double[] data = {25, 18, 15, 12, 8, 30, 35};

            // The labels for the pie chart
            string[] labels = {"Labor", "Licenses", "Taxes", "Legal", "Insurance", "Facilities",
                "Production"};

            // Create a PieChart object of size 100 x 110 pixels
            PieChart c = new PieChart(100, 110);

            // Set the center of the pie at (50, 55) and the radius to 38 pixels
            c.setPieSize(50, 55, 38);

            // Set the depth of the 3D pie
            c.set3D(depth);

            // Add a title showing the depth
            c.addTitle("Depth = " + depth + " pixels", "Arial", 8);

            // Set the pie data
            c.setData(data, labels);

            // Disable the sector labels by setting the color to Transparent
            c.setLabelStyle("", 8, Chart.Transparent);

            // Output the chart
            viewer.Image = c.makeWebImage(Chart.PNG);

            // Include tool tip for the chart
            viewer.ImageMap = c.getHTMLImageMap("", "", "title='{label}: US${value}K ({percent}%)'");
        }
    }
}

