using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
    public class CrosshairController : Controller
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
            // The XY data of the first data series
            double[] dataX = {50, 55, 37, 24, 42, 49, 63, 72, 83, 59};
            double[] dataY = {3.6, 2.8, 2.5, 2.3, 3.8, 3.0, 3.8, 5.0, 6.0, 3.3};

            // Create a XYChart object of size 520 x 490 pixels
            XYChart c = new XYChart(520, 490);

            // Set the plotarea at (60, 40) and of size 450 x 400 pixels, with white background and a
            // light grey border (0xc0c0c0). Turn on both horizontal and vertical grid lines with light
            // grey color (0xc0c0c0)
            c.setPlotArea(60, 40, 450, 400, 0xffffff, -1, 0xc0c0c0, 0xc0c0c0, -1);

            // Add a title to the chart using 18 point Times Bold Itatic font.
            c.addTitle("      Chemical X Thermal Conductivity", "Times New Roman Bold Italic", 18);

            // Add titles to the axes using 12pt Arial Bold Italic font
            c.yAxis().setTitle("Thermal Conductivity (W/K)", "Arial Bold Italic", 12);
            c.xAxis().setTitle("Concentration (g/liter)", "Arial Bold Italic", 12);

            // Set the axes line width to 3 pixels
            c.yAxis().setWidth(3);
            c.xAxis().setWidth(3);

            // Add a scatter layer using (dataX, dataY)
            ScatterLayer scatterLayer = c.addScatterLayer(dataX, dataY, "", Chart.GlassSphereShape, 13,
                0xcc0000);

            // Show custom Javascript tooltip for the scatter layer
            scatterLayer.setHTMLImageMap("", "",
                "onmouseover='showDataPointToolTip({x}, {value})' onmouseout='hideToolTip()'");

            // Add a trend line layer for (dataX, dataY)
            TrendLayer trendLayer = c.addTrendLayer2(dataX, dataY, 0xcc0000);

            // Set the line width to 3 pixels
            trendLayer.setLineWidth(3);

            // Add a 95% confidence band for the line
            trendLayer.addConfidenceBand(0.95, unchecked((int)0x806666ff));

            // Add a 95% confidence band (prediction band) for the points
            trendLayer.addPredictionBand(0.95, unchecked((int)0x8066ff66));

            // Show custom Javascript tooltip for the trend layer
            trendLayer.setHTMLImageMap("", "",
                "onmouseover='showTrendLineToolTip({slope}, {intercept})' onmouseout='hideToolTip()'");

            // Add a legend box at (60, 35) (top of the chart) with horizontal layout. Use 10pt Arial
            // Bold Italic font. Set the background and border color to Transparent and use line style
            // legend icons.
            LegendBox legendBox = c.addLegend(60, 35, false, "Arial Bold Italic", 9);
            legendBox.setBackground(Chart.Transparent);
            legendBox.setLineStyleKey(true);

            // Add entries to the legend box
            legendBox.addKey("95% Line Confidence", unchecked((int)0x806666ff));
            legendBox.addKey("95% Point Confidence", unchecked((int)0x8066ff66));
            legendBox.addKey(String.Format("Trend Line: y = {0:0.0000} x + {1:0.0000}",
                trendLayer.getSlope(), trendLayer.getIntercept()), 0xcc0000, 3);

            // Output the chart
            viewer.Image = c.makeWebImage(Chart.PNG);

            // Include tool tip for the chart
            viewer.ImageMap = c.getHTMLImageMap("");

            // Output Javascript chart model to the browser to suppport tracking cursor
            viewer.ChartModel = c.getJsChartModel();
        }
    }
}

