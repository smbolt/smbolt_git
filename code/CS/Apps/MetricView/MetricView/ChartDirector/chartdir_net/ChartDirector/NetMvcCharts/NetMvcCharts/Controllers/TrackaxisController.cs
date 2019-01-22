using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
    public class TrackaxisController : Controller
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
            // Data for the chart as 2 random data series
            RanSeries r = new RanSeries(127);
            double[] data0 = r.getSeries(180, 10, -1.5, 1.5);
            double[] data1 = r.getSeries(180, 150, -15, 15);
            DateTime[] timeStamps = r.getDateSeries(180, new DateTime(2011, 1, 1), 86400);

            // Create a XYChart object of size 670 x 400 pixels
            XYChart c = new XYChart(670, 400);

            // Add a title to the chart using 18pt Times New Roman Bold Italic font
            c.addTitle("Plasma Stabilizer Energy Usage", "Times New Roman Bold Italic", 18);

            // Set the plotarea at (50, 55) with width 100 pixels less than chart width, and height 90
            // pixels less than chart height. Use a vertical gradient from light blue (f0f6ff) to sky
            // blue (a0c0ff) as background. Set border to transparent and grid lines to white (ffffff).
            c.setPlotArea(50, 55, c.getWidth() - 100, c.getHeight() - 90, c.linearGradientColor(0, 55, 0,
                c.getHeight() - 35, 0xf0f6ff, 0xa0c0ff), -1, Chart.Transparent, 0xffffff, 0xffffff);

            // Add a legend box at (50, 25) using horizontal layout. Use 10pt Arial Bold as font. Set the
            // background and border color to Transparent.
            c.addLegend(50, 25, false, "Arial Bold", 10).setBackground(Chart.Transparent);

            // Set axis label style to 8pt Arial Bold
            c.xAxis().setLabelStyle("Arial Bold", 8);
            c.yAxis().setLabelStyle("Arial Bold", 8);
            c.yAxis2().setLabelStyle("Arial Bold", 8);

            // Set the axis stem to transparent
            c.xAxis().setColors(Chart.Transparent);
            c.yAxis().setColors(Chart.Transparent);
            c.yAxis2().setColors(Chart.Transparent);

            // Configure x-axis label format
            c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "{value|mm/yyyy} ",
                Chart.StartOfMonthFilter(), "{value|mm}");

            // Add axis title using 10pt Arial Bold Italic font
            c.yAxis().setTitle("Power Usage (Watt)", "Arial Bold Italic", 10);
            c.yAxis2().setTitle("Effective Load (kg)", "Arial Bold Italic", 10);

            // Add a line layer to the chart using a line width of 2 pixels.
            LineLayer layer = c.addLineLayer2();
            layer.setLineWidth(2);

            // Add 2 data series to the line layer
            layer.setXData(timeStamps);
            layer.addDataSet(data0, 0xcc0000, "Power Usage");
            layer.addDataSet(data1, 0x008800, "Effective Load").setUseYAxis2();

            // Output the chart
            viewer.Image = c.makeWebImage(Chart.PNG);

            // Output Javascript chart model to the browser to suppport tracking cursor
            viewer.ChartModel = c.getJsChartModel();
        }
    }
}

