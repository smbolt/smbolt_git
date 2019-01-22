using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
    public class ClickpieController : Controller
    {
        //
        // Default Action
        //
        public ActionResult Index()
        {
            ViewBag.Title = "Simple Clickable Pie Chart";
            createChart(ViewBag.Viewer = new RazorChartViewer(HttpContext, "chart1"));
            return View("~/Views/Shared/ChartView.cshtml");
        }

        //
        // Create chart
        //
        private void createChart(RazorChartViewer viewer)
        {
            // Get the selected year and month
            int selectedYear = int.Parse(Request["year"]);
            int selectedMonth = int.Parse(Request["x"]) + 1;

            //
            // In real life, the data may come from a database based on selectedYear. In this example, we
            // just use a random number generator.
            //
            int seed = (selectedYear - 1992) * (100 + 3 * selectedMonth);
            RanTable rantable = new RanTable(seed, 1, 4);
            rantable.setCol(0, seed * 0.003, seed * 0.017);

            double[] data = rantable.getCol(0);

            // The labels for the pie chart
            string[] labels = {"Services", "Hardware", "Software", "Others"};

            // Create a PieChart object of size 600 x 240 pixels
            PieChart c = new PieChart(600, 280);

            // Set the center of the pie at (300, 140) and the radius to 120 pixels
            c.setPieSize(300, 140, 120);

            // Add a title to the pie chart using 18pt Times Bold Italic font
            c.addTitle("Revenue Breakdown for " + selectedMonth + "/" + selectedYear,
                "Times New Roman Bold Italic", 18);

            // Draw the pie in 3D with 20 pixels 3D depth
            c.set3D(20);

            // Set label format to display sector label, value and percentage in two lines
            c.setLabelFormat("{label}<*br*>${value|2}M ({percent}%)");

            // Set label style to 10pt Arial Bold Italic font. Set background color to the same as the
            // sector color, with reduced-glare glass effect and rounded corners.
            ChartDirector.TextBox t = c.setLabelStyle("Arial Bold Italic", 10);
            t.setBackground(Chart.SameAsMainColor, Chart.Transparent, Chart.glassEffect(
                Chart.ReducedGlare));
            t.setRoundedCorners();

            // Use side label layout method
            c.setLabelLayout(Chart.SideLayout);

            // Set the pie data and the pie labels
            c.setData(data, labels);

            // Create the image and save it in a temporary location
            viewer.Image = c.makeWebImage(Chart.PNG);

            // Create an image map for the chart
            viewer.ImageMap = c.getHTMLImageMap(Url.Action("", "piestub"), "",
                "title='{label}:US$ {value|2}M'");
        }
    }
}

