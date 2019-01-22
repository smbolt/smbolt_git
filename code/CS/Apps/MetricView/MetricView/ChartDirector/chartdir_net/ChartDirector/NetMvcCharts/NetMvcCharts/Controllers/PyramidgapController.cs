using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
    public class PyramidgapController : Controller
    {
        //
        // Default Action
        //
        public ActionResult Index()
        {
            ViewBag.Title = "Pyramid Gap";

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
            // The data for the pyramid chart
            double[] data = {156, 123, 211, 179};

            // The colors for the pyramid layers
            int[] colors = {0x66aaee, 0xeebb22, 0xcccccc, 0xcc88ff};

            // The layer gap
            double gap = chartIndex * 0.01;

            // Create a PyramidChart object of size 200 x 200 pixels, with white (ffffff) background and
            // grey (888888) border
            PyramidChart c = new PyramidChart(200, 200, 0xffffff, 0x888888);

            // Set the pyramid center at (100, 100), and width x height to 60 x 120 pixels
            c.setPyramidSize(100, 100, 60, 120);

            // Set the layer gap
            c.addTitle("Gap = " + gap, "Arial Italic", 15);
            c.setLayerGap(gap);

            // Set the elevation to 15 degrees
            c.setViewAngle(15);

            // Set the pyramid data
            c.setData(data);

            // Set the layer colors to the given colors
            c.setColors2(Chart.DataColor, colors);

            // Output the chart
            viewer.Image = c.makeWebImage(Chart.PNG);
        }
    }
}

