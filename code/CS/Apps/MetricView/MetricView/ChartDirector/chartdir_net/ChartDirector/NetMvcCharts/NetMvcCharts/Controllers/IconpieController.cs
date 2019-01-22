using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
    public class IconpieController : Controller
    {
        //
        // Default Action
        //
        public ActionResult Index()
        {
            ViewBag.Title = "Icon Pie Chart (1)";
            createChart(ViewBag.Viewer = new RazorChartViewer(HttpContext, "chart1"));
            return View("~/Views/Shared/ChartView.cshtml");
        }

        //
        // Create chart
        //
        private void createChart(RazorChartViewer viewer)
        {
            // The data for the pie chart
            double[] data = {72, 18, 15, 12};

            // The depths for the sectors
            double[] depths = {30, 20, 10, 10};

            // The labels for the pie chart
            string[] labels = {"Sunny", "Cloudy", "Rainy", "Snowy"};

            // The icons for the sectors
            string[] icons = {"sun.png", "cloud.png", "rain.png", "snowy.png"};

            // Create a PieChart object of size 400 x 310 pixels, with a blue (CCCCFF) vertical metal
            // gradient background, black border, 1 pixel 3D border effect and rounded corners
            PieChart c = new PieChart(400, 310, Chart.metalColor(0xccccff, 0), 0x000000, 1);
            c.setRoundedFrame();

            //Set default directory for loading images
            c.setSearchPath(Url.Content("~/Content"));

            // Set the center of the pie at (200, 180) and the radius to 100 pixels
            c.setPieSize(200, 180, 100);

            // Add a title box using 15pt Times Bold Italic font, on a blue (CCCCFF) background with
            // glass effect
            c.addTitle("Weather Profile in Wonderland", "Times New Roman Bold Italic", 15).setBackground(
                0xccccff, 0x000000, Chart.glassEffect());

            // Set the pie data and the pie labels
            c.setData(data, labels);

            // Add icons to the chart as a custom field
            c.addExtraField(icons);

            // Configure the sector labels using CDML to include the icon images
            c.setLabelFormat(
                "<*block,valign=absmiddle*><*img={field0}*> <*block*>{label}\n{percent}%<*/*><*/*>");

            // Draw the pie in 3D with variable 3D depths
            c.set3D2(depths);

            // Set the start angle to 225 degrees may improve layout when the depths of the sector are
            // sorted in descending order, because it ensures the tallest sector is at the back.
            c.setStartAngle(225);

            // Output the chart
            viewer.Image = c.makeWebImage(Chart.PNG);

            // Include tool tip for the chart
            viewer.ImageMap = c.getHTMLImageMap("", "", "title='{label}: {value} days ({percent}%)'");
        }
    }
}

