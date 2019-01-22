using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
    public class LegendpieController : Controller
    {
        //
        // Default Action
        //
        public ActionResult Index()
        {
            ViewBag.Title = "Pie Chart with Legend (1)";
            createChart(ViewBag.Viewer = new RazorChartViewer(HttpContext, "chart1"));
            return View("~/Views/Shared/ChartView.cshtml");
        }

        //
        // Create chart
        //
        private void createChart(RazorChartViewer viewer)
        {
            // The data for the pie chart
            double[] data = {25, 18, 15, 12, 8, 30, 35};

            // The labels for the pie chart
            string[] labels = {"Labor", "Licenses", "Taxes", "Legal", "Insurance", "Facilities",
                "Production"};

            // Create a PieChart object of size 450 x 270 pixels
            PieChart c = new PieChart(450, 270);

            // Set the center of the pie at (150, 100) and the radius to 80 pixels
            c.setPieSize(150, 135, 100);

            // add a legend box where the top left corner is at (330, 50)
            c.addLegend(330, 60);

            // modify the sector label format to show percentages only
            c.setLabelFormat("{percent}%");

            // Set the pie data and the pie labels
            c.setData(data, labels);

            // Use rounded edge shading, with a 1 pixel white (FFFFFF) border
            c.setSectorStyle(Chart.RoundedEdgeShading, 0xffffff, 1);

            // Output the chart
            viewer.Image = c.makeWebImage(Chart.PNG);

            // Include tool tip for the chart
            viewer.ImageMap = c.getHTMLImageMap("", "", "title='{label}: US${value}K ({percent}%)'");
        }
    }
}

