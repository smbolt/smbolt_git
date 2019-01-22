using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
    public class TracklegendController : Controller
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
            // Data for the chart as 3 random data series
            RanSeries r = new RanSeries(127);
            double[] data0 = r.getSeries(100, 100, -15, 15);
            double[] data1 = r.getSeries(100, 150, -15, 15);
            double[] data2 = r.getSeries(100, 200, -15, 15);
            DateTime[] timeStamps = r.getDateSeries(100, new DateTime(2011, 1, 1), 86400);

            // Create a XYChart object of size 640 x 400 pixels
            XYChart c = new XYChart(640, 400);

            // Add a title to the chart using 18pt Times New Roman Bold Italic font
            c.addTitle("    Product Line Global Revenue", "Times New Roman Bold Italic", 18);

            // Set the plotarea at (50, 55) with width 70 pixels less than chart width, and height 90
            // pixels less than chart height. Use a vertical gradient from light blue (f0f6ff) to sky
            // blue (a0c0ff) as background. Set border to transparent and grid lines to white (ffffff).
            c.setPlotArea(50, 55, c.getWidth() - 70, c.getHeight() - 90, c.linearGradientColor(0, 55, 0,
                c.getHeight() - 35, 0xf0f6ff, 0xa0c0ff), -1, Chart.Transparent, 0xffffff, 0xffffff);


            // Set axis label style to 8pt Arial Bold
            c.xAxis().setLabelStyle("Arial Bold", 8);
            c.yAxis().setLabelStyle("Arial Bold", 8);

            // Set the axis stem to transparent
            c.xAxis().setColors(Chart.Transparent);
            c.yAxis().setColors(Chart.Transparent);

            // Configure x-axis label format
            c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "{value|mm/yyyy} ",
                Chart.StartOfMonthFilter(), "{value|mm}");

            // Add axis title using 10pt Arial Bold Italic font
            c.yAxis().setTitle("USD millions", "Arial Bold Italic", 10);

            // Add a line layer to the chart using a line width of 2 pixels.
            LineLayer layer = c.addLineLayer2();
            layer.setLineWidth(2);

            // Add 3 data series to the line layer
            layer.setXData(timeStamps);
            layer.addDataSet(data0, 0xff3333, "Alpha");
            layer.addDataSet(data1, 0x008800, "Beta");
            layer.addDataSet(data2, 0x3333cc, "Gamma");

            // Output the chart
            viewer.Image = c.makeWebImage(Chart.PNG);

            // Output Javascript chart model to the browser to suppport tracking cursor
            viewer.ChartModel = c.getJsChartModel();
        }
    }
}


