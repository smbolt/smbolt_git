using System;
using System.Windows;
using ChartDirector;

namespace CSharpWPFDemo
{
    public partial class SimpleZoomScrollWindow : Window
    {
        // Data arrays
        private DateTime[] timeStamps;
        private double[] dataSeriesA;
        private double[] dataSeriesB;
        private double[] dataSeriesC;

        public SimpleZoomScrollWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Load the data
            loadData();

            // Initialize the WinChartViewer
            initChartViewer(WPFChartViewer1);

            // Trigger the ViewPortChanged event to draw the chart
            WPFChartViewer1.updateViewPort(true, true);
        }

        //
        // Load the data
        //
        private void loadData()
        {
            // In this example, we just use random numbers as data.
            RanSeries r = new RanSeries(127);
            timeStamps = r.getDateSeries(1827, new DateTime(2007, 1, 1), 86400);
            dataSeriesA = r.getSeries2(1827, 150, -10, 10);
            dataSeriesB = r.getSeries2(1827, 200, -10, 10);
            dataSeriesC = r.getSeries2(1827, 250, -8, 8);
        }

        //
        // Initialize the WinChartViewer
        //
        private void initChartViewer(WPFChartViewer viewer)
        {
            // Set the full x range to be the duration of the data
            viewer.setFullRange("x", timeStamps[0], timeStamps[timeStamps.Length - 1]);

            // Initialize the view port to show the latest 20% of the time range
            viewer.ViewPortWidth = 0.2;
            viewer.ViewPortLeft = 1 - viewer.ViewPortWidth;

            // Set the maximum zoom to 10 points
            viewer.ZoomInWidthLimit = 10.0 / timeStamps.Length;

            // Enable mouse wheel zooming by setting the zoom ratio to 1.1 per wheel event
            viewer.MouseWheelZoomRatio = 1.1;

            // Initially set the mouse usage to "Pointer" mode (Drag to Scroll mode)
            pointerPB.IsChecked = true;
        }

        //
        // The ViewPortChanged event handler. This event occurs if the user scrolls or zooms in
        // or out the chart by dragging or clicking on the chart. It can also be triggered by
        // calling WinChartViewer.updateViewPort.
        //
        private void WPFChartViewer1_ViewPortChanged(object sender, WPFViewPortEventArgs e)
        {
            var viewer = sender as WPFChartViewer;

            if (e.NeedUpdateChart)
                drawChart(viewer);
            if (e.NeedUpdateImageMap)
                updateImageMap(viewer);
        }

        //
        // Update the image map
        //
        private void updateImageMap(WPFChartViewer viewer)
        {
            // Include tool tip for the chart
            if (viewer.ImageMap == null)
            {
                viewer.ImageMap = viewer.Chart.getHTMLImageMap("", "",
                    "title='[{dataSetName}] {x|mmm dd, yyyy}: USD {value|2}'");
            }
        }

        //
        // Draw the chart.
        //
        private void drawChart(WPFChartViewer viewer)
        {
            // Get the start date and end date that are visible on the chart.
            DateTime viewPortStartDate = Chart.NTime(viewer.getValueAtViewPort("x", viewer.ViewPortLeft));
            DateTime viewPortEndDate = Chart.NTime(viewer.getValueAtViewPort("x", viewer.ViewPortLeft +
                viewer.ViewPortWidth));

            // Get the array indexes that corresponds to the visible start and end dates
            int startIndex = (int)Math.Floor(Chart.bSearch(timeStamps, viewPortStartDate));
            int endIndex = (int)Math.Ceiling(Chart.bSearch(timeStamps, viewPortEndDate));
            int noOfPoints = endIndex - startIndex + 1;

            // Extract the part of the data array that are visible.
            DateTime[] viewPortTimeStamps = (DateTime[])Chart.arraySlice(timeStamps, startIndex, noOfPoints);
            double[] viewPortDataSeriesA = (double[])Chart.arraySlice(dataSeriesA, startIndex, noOfPoints);
            double[] viewPortDataSeriesB = (double[])Chart.arraySlice(dataSeriesB, startIndex, noOfPoints);
            double[] viewPortDataSeriesC = (double[])Chart.arraySlice(dataSeriesC, startIndex, noOfPoints);

            //
            // At this stage, we have extracted the visible data. We can use those data to plot the chart.
            //

            //================================================================================
            // Configure overall chart appearance.
            //================================================================================

            // Create an XYChart object 600 x 300 pixels in size, with pale blue (0xf0f0ff) background,
            // black (000000) rounded border, 1 pixel raised effect.
            XYChart c = new XYChart(600, 300, 0xf0f0ff, 0, 1);
            c.setRoundedFrame(0xffffff);

            // Set the plotarea at (52, 60) and of size 520 x 205 pixels. Use white (ffffff) background.
            // Enable both horizontal and vertical grids by setting their colors to grey (cccccc). Set
            // clipping mode to clip the data lines to the plot area.
            c.setPlotArea(52, 60, 520, 205, 0xffffff, -1, -1, 0xcccccc, 0xcccccc);

            // As the data can lie outside the plotarea in a zoomed chart, we need to enable clipping.
            c.setClipping();

            // Add a top title to the chart using 15 pts Times New Roman Bold Italic font, with a light blue
            // (ccccff) background, black (000000) border, and a glass like raised effect.
            c.addTitle("Simple Zooming and Scrolling", "Times New Roman Bold Italic", 15
                ).setBackground(0xccccff, 0x0, Chart.glassEffect());

            // Add a legend box at the top of the plot area with 9pts Arial Bold font with flow layout.
            c.addLegend(50, 33, false, "Arial Bold", 9).setBackground(Chart.Transparent, Chart.Transparent);

            // Set axes width to 2 pixels
            c.yAxis().setWidth(2);
            c.xAxis().setWidth(2);

            // Add a title to the y-axis
            c.yAxis().setTitle("Price (USD)", "Arial Bold", 9);

            //================================================================================
            // Add data to chart
            //================================================================================

            //
            // In this example, we represent the data by lines. You may modify the code below to use other
            // representations (areas, scatter plot, etc).
            //

            // Add a line layer for the lines, using a line width of 2 pixels
            LineLayer layer = c.addLineLayer2();
            layer.setLineWidth(2);

            // In this demo, we do not have too many data points. In real code, the chart may contain a lot
            // of data points when fully zoomed out - much more than the number of horizontal pixels in this
            // plot area. So it is a good idea to use fast line mode.
            layer.setFastLineMode();

            // Now we add the 3 data series to a line layer, using the color red (ff0000), green (00cc00)
            // and blue (0000ff)
            layer.setXData(viewPortTimeStamps);
            layer.addDataSet(viewPortDataSeriesA, 0xff0000, "Product Alpha");
            layer.addDataSet(viewPortDataSeriesB, 0x00cc00, "Product Beta");
            layer.addDataSet(viewPortDataSeriesC, 0x0000ff, "Product Gamma");

            //================================================================================
            // Configure axis scale and labelling
            //================================================================================

            // Set the x-axis as a date/time axis with the scale according to the view port x range.
            viewer.syncDateAxisWithViewPort("x", c.xAxis());

            // In this demo, we rely on ChartDirector to auto-label the axis. We ask ChartDirector to ensure
            // the x-axis labels are at least 75 pixels apart to avoid too many labels.
            c.xAxis().setTickDensity(75);

            //================================================================================
            // Output the chart
            //================================================================================

            viewer.Chart = c;
        }

        //
        // Pointer (Drag to Scroll) button event handler
        //
        private void pointerPB_Checked(object sender, RoutedEventArgs e)
        {
            WPFChartViewer1.MouseUsage = WinChartMouseUsage.ScrollOnDrag;
        }

        //
        // Zoom In button event handler
        //
        private void zoomInPB_Checked(object sender, RoutedEventArgs e)
        {
            WPFChartViewer1.MouseUsage = WinChartMouseUsage.ZoomIn;
        }

        //
        // Zoom Out button event handler
        //
        private void zoomOutPB_Checked(object sender, RoutedEventArgs e)
        {
            WPFChartViewer1.MouseUsage = WinChartMouseUsage.ZoomOut;
        }
    }
}
