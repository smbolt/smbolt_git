using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
    public class PieshadingController : Controller
    {
        //
        // Default Action
        //
        public ActionResult Index()
        {
            ViewBag.Title = "2D Pie Shading";

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
            // The data for the pie chart
            double[] data = {18, 30, 20, 15};

            // The labels for the pie chart
            string[] labels = {"Labor", "Licenses", "Facilities", "Production"};

            // The colors to use for the sectors
            int[] colors = {0x66aaee, 0xeebb22, 0xbbbbbb, 0x8844ff};

            // Create a PieChart object of size 200 x 220 pixels. Use a vertical gradient color from blue
            // (0000cc) to deep blue (000044) as background. Use rounded corners of 16 pixels radius.
            PieChart c = new PieChart(200, 220);
            c.setBackground(c.linearGradientColor(0, 0, 0, c.getHeight(), 0x0000cc, 0x000044));
            c.setRoundedFrame(0xffffff, 16);

            // Set the center of the pie at (100, 120) and the radius to 80 pixels
            c.setPieSize(100, 120, 80);

            // Set the pie data
            c.setData(data, labels);

            // Set the sector colors
            c.setColors2(Chart.DataColor, colors);

            // Demonstrates various shading modes
            if (chartIndex == 0) {
                c.addTitle("Default Shading", "bold", 12, 0xffffff);
            } else if (chartIndex == 1) {
                c.addTitle("Local Gradient", "bold", 12, 0xffffff);
                c.setSectorStyle(Chart.LocalGradientShading);
            } else if (chartIndex == 2) {
                c.addTitle("Global Gradient", "bold", 12, 0xffffff);
                c.setSectorStyle(Chart.GlobalGradientShading);
            } else if (chartIndex == 3) {
                c.addTitle("Concave Shading", "bold", 12, 0xffffff);
                c.setSectorStyle(Chart.ConcaveShading);
            } else if (chartIndex == 4) {
                c.addTitle("Rounded Edge", "bold", 12, 0xffffff);
                c.setSectorStyle(Chart.RoundedEdgeShading);
            } else if (chartIndex == 5) {
                c.addTitle("Radial Gradient", "bold", 12, 0xffffff);
                c.setSectorStyle(Chart.RadialShading);
            }

            // Disable the sector labels by setting the color to Transparent
            c.setLabelStyle("", 8, Chart.Transparent);

            // Output the chart
            viewer.Image = c.makeWebImage(Chart.PNG);

            // Include tool tip for the chart
            viewer.ImageMap = c.getHTMLImageMap("", "", "title='{label}: US${value}K ({percent}%)'");
        }
    }
}

