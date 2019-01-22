using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
    public class TrackvlegendController : Controller
    {
        //
        // Default Action
        //
        public ActionResult Index()
        {
            createChart(ViewBag.Viewer = new RazorChartViewer(HttpContext, "chart1"));
            return View();
        }

        //
        // Create chart
        //
        private void createChart(RazorChartViewer viewer)
        {
            // In this example, we simply use random data for the 2 data series.
            RanSeries r = new RanSeries(127);
            double[] data0 = r.getSeries(180, 70, -5, 5);
            double[] data1 = r.getSeries(180, 150, -15, 15);
            DateTime[] timeStamps = r.getDateSeries(180, new DateTime(2014, 3, 1), 86400);

            // Create a XYChart object of size 640 x 420 pixels
            XYChart c = new XYChart(640, 420);

            // Add a title to the chart using 20pt Arial font
            c.addTitle("    Plasma Stabilizer Energy Usage", "Arial", 20);

            // Set the plotarea at (55, 60) and of size 560 x 330 pixels, with transparent background and
            // border and light grey (0xcccccc) horizontal grid lines
            c.setPlotArea(55, 60, 560, 330, -1, -1, Chart.Transparent, 0xcccccc);

            // Add a legend box at (55, 30) using horizontal layout, with 10pt Arial Bold as font and
            // transparent background and border.
            c.addLegend(55, 30, false, "Arial Bold", 10).setBackground(Chart.Transparent);

            // Set axis label style to 10pt Arial
            c.xAxis().setLabelStyle("Arial", 10);
            c.yAxis().setLabelStyle("Arial", 10);

            // Set the x and y axis stems to transparent, and the x-axis tick color to grey (0xcccccc)
            c.xAxis().setColors(Chart.Transparent, Chart.TextColor, Chart.TextColor, 0xcccccc);
            c.yAxis().setColors(Chart.Transparent);

            // Configure the x-axis tick lengtht to 10 pixels internal to the plot area
            c.xAxis().setTickLength(-10, 0);

            // With the ticks internal to the plot area, the x-axis labels will come very close to the
            // axis stem, so we configure a wider gap.
            c.xAxis().setLabelGap(10);

            // For the automatic axis labels, set the minimum spacing to 80/40 pixels for the x/y axis.
            c.xAxis().setTickDensity(80);
            c.yAxis().setTickDensity(40);

            // Use "mm/yyyy" as the x-axis label format for the first plotted month of a year, and "mm"
            // for other months
            c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "{value|mm/yyyy} ",
                Chart.StartOfMonthFilter(), "{value|mm}");

            // Add a title to the y axis using dark grey (0x555555) 12pt Arial Bold font
            c.yAxis().setTitle("Energy (kWh)", "Arial Bold", 14, 0x555555);

            // Add a line layer with 2-pixel line width
            LineLayer layer0 = c.addLineLayer(data0, 0xcc0000, "Power Usage");
            layer0.setXData(timeStamps);
            layer0.setLineWidth(2);

            // Add an area layer using semi-transparent blue (0x7f0044cc) as the fill color
            AreaLayer layer1 = c.addAreaLayer(data1, 0x7f0044cc, "Effective Load");
            layer1.setXData(timeStamps);
            layer1.setBorderColor(Chart.SameAsMainColor);

            // Output the chart
            viewer.Image = c.makeWebImage(Chart.PNG);

            // Output Javascript chart model to the browser to suppport tracking cursor
            viewer.ChartModel = c.getJsChartModel();
        }
    }
}


