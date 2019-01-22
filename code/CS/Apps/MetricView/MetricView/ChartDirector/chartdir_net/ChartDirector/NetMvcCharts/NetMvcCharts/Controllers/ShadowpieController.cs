using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
    public class ShadowpieController : Controller
    {
        //
        // Default Action
        //
        public ActionResult Index()
        {
            ViewBag.Title = "3D Shadow Mode";

            // This example contains 4 charts.
            ViewBag.Viewer = new RazorChartViewer[4];
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
            int angle = chartIndex * 90 + 45;

            // The data for the pie chart
            double[] data = {25, 18, 15, 12, 8, 30, 35};

            // The labels for the pie chart
            string[] labels = {"Labor", "Licenses", "Taxes", "Legal", "Insurance", "Facilities",
                "Production"};

            // Create a PieChart object of size 110 x 110 pixels
            PieChart c = new PieChart(110, 110);

            // Set the center of the pie at (50, 55) and the radius to 36 pixels
            c.setPieSize(55, 55, 36);

            // Set the depth, tilt angle and 3D mode of the 3D pie (-1 means auto depth, "true" means the
            // 3D effect is in shadow mode)
            c.set3D(-1, angle, true);

            // Add a title showing the shadow angle
            c.addTitle("Shadow @ " + angle + " deg", "Arial", 8);

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

