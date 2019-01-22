using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
    public class PolarvectorController : Controller
    {
        //
        // Default Action
        //
        public ActionResult Index()
        {
            ViewBag.Title = "Polar Vector Chart";
            createChart(ViewBag.Viewer = new RazorChartViewer(HttpContext, "chart1"));
            return View("~/Views/Shared/ChartView.cshtml");
        }

        //
        // Create chart
        //
        private void createChart(RazorChartViewer viewer)
        {
            // Coordinates of the starting points of the vectors
            double[] radius = {5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 10, 10, 10, 10, 10, 10, 10, 10, 10,
                10, 10, 10, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 20, 20, 20, 20, 20, 20, 20,
                20, 20, 20, 20, 20, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25};
            double[] angle = {0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 0, 30, 60, 90, 120,
                150, 180, 210, 240, 270, 300, 330, 0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330,
                0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 0, 30, 60, 90, 120, 150, 180, 210,
                240, 270, 300, 330};

            // Magnitude and direction of the vectors
            double[] magnitude = {5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
                3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1};
            double[] direction = {60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 0, 30, 60, 90, 120,
                150, 180, 210, 240, 270, 300, 330, 0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330,
                0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 0, 30, 60, 90, 120, 150, 180, 210,
                240, 270, 300, 330, 0, 30};

            // Create a PolarChart object of size 460 x 460 pixels
            PolarChart c = new PolarChart(460, 460);

            // Add a title to the chart at the top left corner using 15pt Arial Bold Italic font
            c.addTitle("Polar Vector Chart Demonstration", "Arial Bold Italic", 15);

            // Set center of plot area at (230, 240) with radius 180 pixels
            c.setPlotArea(230, 240, 180);

            // Set the grid style to circular grid
            c.setGridStyle(false);

            // Set angular axis as 0 - 360, with a spoke every 30 units
            c.angularAxis().setLinearScale(0, 360, 30);

            // Add a polar vector layer to the chart with blue (0000ff) vectors
            c.addVectorLayer(radius, angle, magnitude, direction, Chart.RadialAxisScale, 0x0000ff);

            // Output the chart
            viewer.Image = c.makeWebImage(Chart.PNG);

            // Include tool tip for the chart
            viewer.ImageMap = c.getHTMLImageMap("", "",
                "title='Vector at ({value}, {angle} deg): Length = {len}, Angle = {dir} deg'");
        }
    }
}

