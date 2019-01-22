using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
    public class IconameterController : Controller
    {
        //
        // Default Action
        //
        public ActionResult Index()
        {
            ViewBag.Title = "Icon Angular Meter";
            createChart(ViewBag.Viewer = new RazorChartViewer(HttpContext, "chart1"));
            return View("~/Views/Shared/ChartView.cshtml");
        }

        //
        // Create chart
        //
        private void createChart(RazorChartViewer viewer)
        {
            // The value to display on the meter
            double value = 85;

            // Create an AugularMeter object of size 70 x 90 pixels, using black background with a 2
            // pixel 3D depressed border.
            AngularMeter m = new AngularMeter(70, 90, 0, 0, -2);

            //Set default directory for loading images
            m.setSearchPath(Url.Content("~/Content"));

            // Use white on black color palette for default text and line colors
            m.setColors(Chart.whiteOnBlackPalette);

            // Set the meter center at (10, 45), with radius 50 pixels, and span from 135 to 45 degrees
            m.setMeter(10, 45, 50, 135, 45);

            // Set meter scale from 0 - 100, with the specified labels
            m.setScale2(0, 100, new string[] {"E", " ", " ", " ", "F"});

            // Set the angular arc and major tick width to 2 pixels
            m.setLineWidth(2, 2);

            // Add a red zone at 0 - 15
            m.addZone(0, 15, 0xff3333);

            // Add an icon at (25, 35)
            m.addText(25, 35, "<*img=gas.gif*>");

            // Add a yellow (ffff00) pointer at the specified value
            m.addPointer(value, 0xffff00);

            // Output the chart
            viewer.Image = m.makeWebImage(Chart.PNG);
        }
    }
}

