using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
    public class BackgroundController : Controller
    {
        //
        // Default Action
        //
        public ActionResult Index()
        {
            ViewBag.Title = "Background and Wallpaper";

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
            // The data for the chart
            double[] data = {85, 156, 179.5, 211, 123};
            string[] labels = {"Mon", "Tue", "Wed", "Thu", "Fri"};

            // Create a XYChart object of size 270 x 270 pixels
            XYChart c = new XYChart(270, 270);

            // Set the plot area at (40, 32) and of size 200 x 200 pixels
            PlotArea plotarea = c.setPlotArea(40, 32, 200, 200);

            // Set the background style based on the input parameter
            if (chartIndex == 0) {
                // Has wallpaper image
                c.setWallpaper(Url.Content("~/Content/tile.gif"));
            } else if (chartIndex == 1) {
                // Use a background image as the plot area background
                plotarea.setBackground2(Url.Content("~/Content/bg.png"));
            } else if (chartIndex == 2) {
                // Use white (0xffffff) and grey (0xe0e0e0) as two alternate plotarea background colors
                plotarea.setBackground(0xffffff, 0xe0e0e0);
            } else {
                // Use a dark background palette
                c.setColors(Chart.whiteOnBlackPalette);
            }

            // Set the labels on the x axis
            c.xAxis().setLabels(labels);

            // Add a color bar layer using the given data. Use a 1 pixel 3D border for the bars.
            c.addBarLayer3(data).setBorderColor(-1, 1);

            // Output the chart
            viewer.Image = c.makeWebImage(Chart.PNG);

            // Include tool tip for the chart
            viewer.ImageMap = c.getHTMLImageMap("", "", "title='Revenue for {xLabel}: US${value}K'");
        }
    }
}

