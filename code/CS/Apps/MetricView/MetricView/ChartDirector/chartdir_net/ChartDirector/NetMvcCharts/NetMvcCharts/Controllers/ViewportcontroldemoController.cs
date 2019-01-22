using System;
using System.Web.Mvc;
using ChartDirector;

namespace NetMvcCharts.Controllers
{
    public class ViewportcontroldemoController : Controller
    {
        //
        // Initialize the WebChartViewer when the page is first loaded
        //
        private void initViewer(RazorChartViewer viewer)
        {
            // The full x-axis range is from Jan 1, 2007 to Jan 1, 2012
            DateTime startDate = new DateTime(2010, 1, 1);
            DateTime endDate = new DateTime(2015, 1, 1);
            viewer.setFullRange("x", startDate, endDate);

            // Initialize the view port to show the last 366 days (out of 1826 days)
            viewer.ViewPortWidth = 366.0 / 1826;
            viewer.ViewPortLeft = 1 - viewer.ViewPortWidth;

            // Set the maximum zoom to 10 days (out of 1826 days)
            viewer.ZoomInWidthLimit = 10.0 / 1826;
        }

        //
        // Create a random table for demo purpose.
        //
        private RanTable getRandomTable()
        {
            RanTable r = new RanTable(127, 4, 1828);
            r.setDateCol(0, new DateTime(2010, 1, 1), 86400);
            r.setCol(1, 150, -10, 10);
            r.setCol(2, 200, -10, 10);
            r.setCol(3, 250, -8, 8);
            return r;
        }

        //
        // Draw the chart
        //
        private void drawChart(RazorChartViewer viewer)
        {
            // Determine the visible x-axis range
            DateTime viewPortStartDate = Chart.NTime(viewer.getValueAtViewPort("x", viewer.ViewPortLeft))
                ;
            DateTime viewPortEndDate = Chart.NTime(viewer.getValueAtViewPort("x", viewer.ViewPortLeft +
                viewer.ViewPortWidth));

            // We need to get the data within the visible x-axis range. In real code, this can be by
            // using a database query or some other means as specific to the application. In this demo,
            // we just generate a random data table, and then select the data within the table.
            RanTable r = getRandomTable();

            // Select the data for the visible date range viewPortStartDate to viewPortEndDate. It is
            // possible there is no data point at exactly viewPortStartDate or viewPortEndDate. In this
            // case, we also need the data points that are just outside the visible date range to
            // "overdraw" the line a little bit (the "overdrawn" part will be clipped to the plot area)
            // In this demo, we do this by adding a one day margin to the date range when selecting the
            // data.
            r.selectDate(0, viewPortStartDate.AddDays(-1), viewPortEndDate.AddDays(1));

            // The selected data from the random data table
            DateTime[] timeStamps = Chart.NTime(r.getCol(0));
            double[] dataSeriesA = r.getCol(1);
            double[] dataSeriesB = r.getCol(2);
            double[] dataSeriesC = r.getCol(3);

            //
            // Now we have obtained the data, we can plot the chart.
            //

            //================================================================================
            // Configure overall chart appearance.
            //================================================================================

            // Create an XYChart object of size 640 x 400 pixels
            XYChart c = new XYChart(640, 400);

            // Set the plotarea at (55, 55) with width 80 pixels less than chart width, and height 90
            // pixels less than chart height. Use a vertical gradient from light blue (f0f6ff) to sky
            // blue (a0c0ff) as background. Set border to transparent and grid lines to white (ffffff).
            c.setPlotArea(55, 55, c.getWidth() - 80, c.getHeight() - 90, c.linearGradientColor(0, 55, 0,
                c.getHeight() - 35, 0xf0f6ff, 0xa0c0ff), -1, Chart.Transparent, 0xffffff, 0xffffff);

            // As the data can lie outside the plotarea in a zoomed chart, we need to enable clipping.
            c.setClipping();

            // Add a title box using dark grey (0x333333) 18pt Arial Bold font
            c.addTitle("   Zooming and Scrolling with Viewport Control", "Arial Bold", 15, 0x333333);

            if (viewer.IsAttachmentRequest()) {
                LegendBox b = c.addLegend(55, 28, false, "Arial Bold", 10);
                b.setBackground(Chart.Transparent, Chart.Transparent);
                b.setLineStyleKey();
            }

            // Set the x and y axis stems to transparent and the label font to 10pt Arial
            c.xAxis().setColors(Chart.Transparent);
            c.yAxis().setColors(Chart.Transparent);
            c.xAxis().setLabelStyle("Arial", 10);
            c.yAxis().setLabelStyle("Arial", 10);

            // Add axis title using 10pt Arial Bold font
            c.yAxis().setTitle("Ionic Temperature (C)", "Arial Bold", 10);

            //================================================================================
            // Add data to chart
            //================================================================================

            //
            // In this example, we represent the data by lines. You may modify the code below to use
            // other layer types (areas, scatter plot, etc).
            //

            // Add a line layer for the lines, using a line width of 2 pixels
            LineLayer layer = c.addLineLayer2();
            layer.setLineWidth(2);

            // In this demo, we do not have too many data points. In real code, the chart may contain a
            // lot of data points when fully zoomed out - much more than the number of horizontal pixels
            // in this plot area. So it is a good idea to use fast line mode.
            layer.setFastLineMode();

            // Now we add the 3 data series to a line layer, using the color red (0xff3333), green
            // (0x008800) and blue (0x3333cc)
            layer.setXData(timeStamps);
            layer.addDataSet(dataSeriesA, 0xff3333, "Alpha");
            layer.addDataSet(dataSeriesB, 0x008800, "Beta");
            layer.addDataSet(dataSeriesC, 0x3333cc, "Gamma");

            //================================================================================
            // Configure axis scale and labelling
            //================================================================================

            // Set the x-axis as a date/time axis with the scale according to the view port x range.
            viewer.syncDateAxisWithViewPort("x", c.xAxis());

            // For the automatic y-axis labels, set the minimum spacing to 30 pixels.
            c.yAxis().setTickDensity(30);

            //
            // In this demo, the time range can be from a few years to a few days. We demonstrate how to
            // set up different date/time format based on the time range.
            //

            // If all ticks are yearly aligned, then we use "yyyy" as the label format.
            c.xAxis().setFormatCondition("align", 360 * 86400);
            c.xAxis().setLabelFormat("{value|yyyy}");

            // If all ticks are monthly aligned, then we use "mmm yyyy" in bold font as the first label
            // of a year, and "mmm" for other labels.
            c.xAxis().setFormatCondition("align", 30 * 86400);
            c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "<*font=bold*>{value|mmm<*br*>yyyy}",
                Chart.AllPassFilter(), "{value|mmm}");

            // If all ticks are daily algined, then we use "mmm dd<*br*>yyyy" in bold font as the first
            // label of a year, and "mmm dd" in bold font as the first label of a month, and "dd" for
            // other labels.
            c.xAxis().setFormatCondition("align", 86400);
            c.xAxis().setMultiFormat(Chart.StartOfYearFilter(),
                "<*block,halign=left*><*font=bold*>{value|mmm dd<*br*>yyyy}", Chart.StartOfMonthFilter(),
                "<*font=bold*>{value|mmm dd}");
            c.xAxis().setMultiFormat2(Chart.AllPassFilter(), "{value|dd}");

            // For all other cases (sub-daily ticks), use "hh:nn<*br*>mmm dd" for the first label of a
            // day, and "hh:nn" for other labels.
            c.xAxis().setFormatCondition("else");
            c.xAxis().setMultiFormat(Chart.StartOfDayFilter(), "<*font=bold*>{value|hh:nn<*br*>mmm dd}",
                Chart.AllPassFilter(), "{value|hh:nn}");

            //================================================================================
            // Step 5 - Output the chart
            //================================================================================

            if (viewer.IsAttachmentRequest()) {
                // Output as PDF attachment
                viewer.Image = c.makeWebImage(Chart.PDF);
            } else {
                // Output the chart
                viewer.Image = c.makeWebImage(Chart.PNG);

                // Output Javascript chart model to the browser to suppport tracking cursor
                viewer.ChartModel = c.getJsChartModel();
            }
        }

        private void drawFullChart(RazorViewPortControl vp, RazorChartViewer viewer)
        {
            // We need to draw a small thumbnail chart for the full data range. The simplest method is to
            // simply get the full data to draw the chart. If the full data are very large (eg. millions
            // of points), for such a small thumbnail chart, it is often acceptable to just retreive a
            // small sample of the data.
            //
            // In this example, there are only around 5500 points for the 3 data series. This amount is
            // not large to ChartDirector, so we simply pass all the data to ChartDirector.
            RanTable r = getRandomTable();

            // Get all the data from the random table
            DateTime[] timeStamps = Chart.NTime(r.getCol(0));
            double[] dataSeriesA = r.getCol(1);
            double[] dataSeriesB = r.getCol(2);
            double[] dataSeriesC = r.getCol(3);

            // Create an XYChart object of size 640 x 60 pixels
            XYChart c = new XYChart(640, 60);

            // Set the plotarea with the same horizontal position as that in the main chart for
            // alignment. The vertical position is set to equal to the chart height.
            c.setPlotArea(55, 0, c.getWidth() - 80, c.getHeight() - 1, 0xc0d8ff, -1, 0x888888,
                Chart.Transparent, 0xffffff);

            // Set the x axis stem to transparent and the label font to 10pt Arial
            c.xAxis().setColors(Chart.Transparent);
            c.xAxis().setLabelStyle("Arial", 10);

            // Put the x-axis labels inside the plot area by setting a negative label gap. Use
            // setLabelAlignment to put the label at the right side of the tick.
            c.xAxis().setLabelGap(-1);
            c.xAxis().setLabelAlignment(1);

            // Set the y axis stem and labels to transparent (that is, hide the labels)
            c.yAxis().setColors(Chart.Transparent, Chart.Transparent);

            // Add a line layer for the lines with fast line mode enabled
            LineLayer layer = c.addLineLayer2();
            layer.setFastLineMode();

            // Now we add the 3 data series to a line layer, using the color red (0xff3333), green
            // (0x008800) and blue (0x3333cc)
            layer.setXData(timeStamps);
            layer.addDataSet(dataSeriesA, 0xff3333);
            layer.addDataSet(dataSeriesB, 0x008800);
            layer.addDataSet(dataSeriesC, 0x3333cc);

            // The x axis scales should reflect the full range of the view port
            c.xAxis().setDateScale(viewer.getValueAtViewPort("x", 0), viewer.getValueAtViewPort("x", 1));

            // For the automatic x-axis labels, set the minimum spacing to 75 pixels.
            c.xAxis().setTickDensity(75);

            // For the auto-scaled y-axis, as we hide the labels, we can disable axis rounding. This can
            // make the axis scale fit the data tighter.
            c.yAxis().setRounding(false, false);

            // Output the chart
            vp.Image = c.makeWebImage(Chart.PNG);
        }

        public ActionResult Index()
        {
            RazorChartViewer viewer = ViewBag.Viewer = new RazorChartViewer(HttpContext, "chart1");
            RazorViewPortControl viewPortCtrl = ViewBag.ViewPortControl = new RazorViewPortControl(HttpContext, "chart2");

            //
            // This script handles both the full page request, as well as the subsequent partial updates
            // (AJAX chart updates). We need to determine the type of request first before we processing
            // it.
            //
            if (RazorChartViewer.IsPartialUpdateRequest(Request)) {
                // Is a partial update request.
                drawChart(viewer);

                if (viewer.IsAttachmentRequest()) {
                    return File(viewer.StreamChart(), Response.ContentType, "demochart.pdf");
                } else {
                    return Content(viewer.PartialUpdateChart());
                }
            }

            //
            // If the code reaches here, it is a full page request.
            //

            // Initialize the WebChartViewer and draw the chart.
            initViewer(viewer);
            drawChart(viewer);

            // Draw a thumbnail chart representing the full range in the WebViewPortControl
            drawFullChart(viewPortCtrl, viewer);
            return View();
        }
    }
}

