using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using ChartDirector;

namespace CSharpWPFDemo
{
    /// <summary>
    /// Interaction logic for XYZoomScroll.xaml
    /// </summary>
    public partial class XYZoomScrollWindow : Window
    {
        // XY data points for the chart
        private double[] dataX0;
        private double[] dataY0;
        private double[] dataX1;
        private double[] dataY1;
        private double[] dataX2;
        private double[] dataY2;

        public XYZoomScrollWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Connect the WPFViewPortControl to the WPfChartViewer
            ViewPortControl1.Viewer = WPFChartViewer1;

            // Load the data
            loadData();

            // Trigger the ViewPortChanged event to draw the chart
            WPFChartViewer1.updateViewPort(true, true);

            // Draw the full thumbnail chart for the ViewPortControl
            drawFullChart(ViewPortControl1, WPFChartViewer1);
        }

        //
        // Load the data
        //
        private void loadData()
        {
            //
            // For simplicity, in this demo, we just use hard coded data.
            //
            dataX0 = new double[] { 10, 15, 6, -12, 14, -8, 13, -3, 16, 12, 10.5, -7, 3, -10, -5, 2, 5 };
            dataY0 = new double[] {130, 150, 80, 110, -110, -105, -130, -15, -170, 125,  125, 60, 25, 150,
                150, 15, 120};
            dataX1 = new double[] { 6, 7, -4, 3.5, 7, 8, -9, -10, -12, 11, 8, -3, -2, 8, 4, -15, 15 };
            dataY1 = new double[] {65, -40, -40, 45, -70, -80, 80, 10, -100, 105, 60, 50, 20, 170, -25, 50,
                75};
            dataX2 = new double[] { -10, -12, 11, 8, 6, 12, -4, 3.5, 7, 8, -9, 3, -13, 16, -7.5, -10, -15 };
            dataY2 = new double[] {65, -80, -40, 45, -70, -80, 80, 90, -100, 105, 60, -75, -150, -40, 120,
                -50, -30};
        }

        //
        // The ViewPortChanged event handler. This event occurs if the user scrolls or zooms in
        // or out the chart by dragging or clicking on the chart. It can also be triggered by
        // calling WinChartViewer.updateViewPort.
        //
        private void WPFChartViewer1_ViewPortChanged(object sender, WPFViewPortEventArgs e)
        {
            var viewer = sender as WPFChartViewer;

            // In addition to updating the chart, we may also need to update other controls that
            // changes based on the view port.
            updateControls(viewer);

            // Update the chart if necessary
            if (e.NeedUpdateChart)
                drawChart(viewer);

            // Update the image map if necessary
            if (e.NeedUpdateImageMap)
                updateImageMap(viewer);
        }

        //
        // Update controls when the view port changed
        //
        private void updateControls(WPFChartViewer viewer)
        {
            // Synchronize the zoom bar value with the view port width/height
            zoomBar.Value = Math.Min(viewer.ViewPortWidth, viewer.ViewPortHeight) * zoomBar.Maximum;
        }

        //
        // Draw the chart and display it in the given viewer
        //
        private void drawChart(WPFChartViewer viewer)
        {
            // Create an XYChart object 500 x 480 pixels in size, with the same background color
            // as the container
            XYChart c = new XYChart(500, 480, 0xccccff);

            // Set the plotarea at (50, 40) and of size 400 x 400 pixels. Use light grey (c0c0c0)
            // horizontal and vertical grid lines. Set 4 quadrant coloring, where the colors of 
            // the quadrants alternate between lighter and deeper grey (dddddd/eeeeee). 
            c.setPlotArea(50, 40, 400, 400, -1, -1, -1, 0xc0c0c0, 0xc0c0c0
                ).set4QBgColor(0xdddddd, 0xeeeeee, 0xdddddd, 0xeeeeee, 0x000000);

            // Enable clipping mode to clip the part of the data that is outside the plot area.
            c.setClipping();

            // Set 4 quadrant mode, with both x and y axes symetrical around the origin
            c.setAxisAtOrigin(Chart.XYAxisAtOrigin, Chart.XAxisSymmetric + Chart.YAxisSymmetric);

            // Add a legend box at (450, 40) (top right corner of the chart) with vertical layout
            // and 8 pts Arial Bold font. Set the background color to semi-transparent grey.
            LegendBox b = c.addLegend(450, 40, true, "Arial Bold", 8);
            b.setAlignment(Chart.TopRight);
            b.setBackground(0x40dddddd);

            // Add a titles to axes
            c.xAxis().setTitle("Alpha Index");
            c.yAxis().setTitle("Beta Index");

            // Set axes width to 2 pixels
            c.xAxis().setWidth(2);
            c.yAxis().setWidth(2);

            // The default ChartDirector settings has a denser y-axis grid spacing and less-dense
            // x-axis grid spacing. In this demo, we want the tick spacing to be symmetrical.
            // We use around 50 pixels between major ticks and 25 pixels between minor ticks.
            c.xAxis().setTickDensity(50, 25);
            c.yAxis().setTickDensity(50, 25);

            //
            // In this example, we represent the data by scatter points. If you want to represent
            // the data by somethings else (lines, bars, areas, floating boxes, etc), just modify
            // the code below to use the layer type of your choice. 
            //

            // Add scatter layer, using 11 pixels red (ff33333) X shape symbols
            c.addScatterLayer(dataX0, dataY0, "Group A", Chart.Cross2Shape(), 11, 0xff3333);

            // Add scatter layer, using 11 pixels green (33ff33) circle symbols
            c.addScatterLayer(dataX1, dataY1, "Group B", Chart.CircleShape, 11, 0x33ff33);

            // Add scatter layer, using 11 pixels blue (3333ff) triangle symbols
            c.addScatterLayer(dataX2, dataY2, "Group C", Chart.TriangleSymbol, 11, 0x3333ff);

            //
            // In this example, we have not explicitly configured the full x and y range. In this case, the
            // first time syncLinearAxisWithViewPort is called, ChartDirector will auto-scale the axis and
            // assume the resulting range is the full range. In subsequent calls, ChartDirector will set the
            // axis range based on the view port and the full range.
            //
            viewer.syncLinearAxisWithViewPort("x", c.xAxis());
            viewer.syncLinearAxisWithViewPort("y", c.yAxis());

            // We need to update the track line too. If the mouse is moving on the chart (eg. if 
            // the user drags the mouse on the chart to scroll it), the track line will be updated
            // in the MouseMovePlotArea event. Otherwise, we need to update the track line here.
            if ((!viewer.IsInMouseMoveEvent) && viewer.IsMouseOnPlotArea)
                crossHair(c, viewer.PlotAreaMouseX, viewer.PlotAreaMouseY);

            // Set the chart image to the ChartViewer
            viewer.Chart = c;
        }

        //
        // Draw the full thumbnail chart and display it in the given ViewPortControl
        //
        private void drawFullChart(WPFViewPortControl vpc, WPFChartViewer viewer)
        {
            // Create an XYChart object of the same size as the Viewport Control
            XYChart c = new XYChart((int)vpc.ActualWidth, (int)vpc.ActualHeight);

            // Set the plotarea to cover the entire chart. Disable grid lines by setting their colors
            // to transparent. Set 4 quadrant coloring, where the colors of the quadrants alternate 
            // between lighter and deeper grey (dddddd/eeeeee). 
            c.setPlotArea(0, 0, c.getWidth() - 1, c.getHeight() - 1, -1, -1, 0xff0000, Chart.Transparent,
                Chart.Transparent).set4QBgColor(0xdddddd, 0xeeeeee, 0xdddddd, 0xeeeeee, 0x000000);

            // Set 4 quadrant mode, with both x and y axes symetrical around the origin
            c.setAxisAtOrigin(Chart.XYAxisAtOrigin, Chart.XAxisSymmetric + Chart.YAxisSymmetric);

            // The x and y axis scales reflect the full range of the view port
            c.xAxis().setLinearScale(viewer.getValueAtViewPort("x", 0), viewer.getValueAtViewPort("x", 1),
                Chart.NoValue);
            c.yAxis().setLinearScale(viewer.getValueAtViewPort("y", 0), viewer.getValueAtViewPort("y", 1),
                Chart.NoValue);

            // Add scatter layer, using 3 pixels red (ff33333) X shape symbols
            c.addScatterLayer(dataX0, dataY0, "Group A", Chart.Cross2Shape(), 3, 0xff3333, 0xff3333);

            // Add scatter layer, using 3 pixels green (33ff33) circle symbols
            c.addScatterLayer(dataX1, dataY1, "Group B", Chart.CircleShape, 3, 0x33ff33, 0x33ff33);

            // Add scatter layer, using 3 pixels blue (3333ff) triangle symbols
            c.addScatterLayer(dataX2, dataY2, "Group C", Chart.TriangleSymbol, 3, 0x3333ff, 0x3333ff);

            // Set the chart image to the ViewPortControl
            vpc.Chart = c;
        }

        //
        // Update the image map
        //
        private void updateImageMap(WPFChartViewer viewer)
        {
            // Include tool tip for the chart
            if ((viewer.ImageMap == null) && (viewer.Chart != null))
            {
                viewer.ImageMap = viewer.Chart.getHTMLImageMap("clickable", "",
                    "title='[{dataSetName}] Alpha = {x}, Beta = {value}'");
            }
        }

        //
        // ClickHotSpot event handler. In this demo, we just display the hot spot parameters in a pop up 
        // dialog.
        //
        private void WPFChartViewer1_ClickHotSpot(object sender, WPFHotSpotEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("w=" + Width + ". h=" + Height);
            var viewer = sender as WPFChartViewer;

            // We show the pop up dialog only when the mouse action is not in zoom-in or zoom-out mode.
            if ((viewer.MouseUsage != WinChartMouseUsage.ZoomIn)
                && (viewer.MouseUsage != WinChartMouseUsage.ZoomOut))
                new ParamViewer().Display(sender, e);
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

        //
        // Save button event handler
        //
        private void savePB_Click(object sender, RoutedEventArgs e)
        {
            // The standard Save File dialog
            SaveFileDialog fileDlg = new SaveFileDialog();
            fileDlg.Filter = "PNG (*.png)|*.png|JPG (*.jpg)|*.jpg|GIF (*.gif)|*.gif|BMP (*.bmp)|*.bmp|" +
                "SVG (*.svg)|*.svg|PDF (*.pdf)|*.pdf";
            fileDlg.FileName = "chartdirector_demo";
            var ret = fileDlg.ShowDialog(this);
            if (!(ret.HasValue && ret.Value))
                return;

            // Save the chart
            if (null != WPFChartViewer1.Chart)
                WPFChartViewer1.Chart.makeChart(fileDlg.FileName);
        }

        //
        // ValueChanged event handler for zoomBar. Zoom in around the center point and try to 
        // maintain the aspect ratio
        //
        private void zoomBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var viewer = WPFChartViewer1;

            if (!viewer.IsInViewPortChangedEvent)
            {
                //Remember the center point
                double centerX = viewer.ViewPortLeft + viewer.ViewPortWidth / 2;
                double centerY = viewer.ViewPortTop + viewer.ViewPortHeight / 2;

                //Aspect ratio and zoom factor
                double aspectRatio = viewer.ViewPortWidth / viewer.ViewPortHeight;
                double zoomTo = ((double)zoomBar.Value) / zoomBar.Maximum;
                zoomTo = Math.Max(zoomTo, Math.Min(viewer.ZoomInWidthLimit, viewer.ZoomInHeightLimit));

                //Zoom while respecting the aspect ratio
                viewer.ViewPortWidth = zoomTo * Math.Max(1, aspectRatio);
                viewer.ViewPortHeight = zoomTo * Math.Max(1, 1 / aspectRatio);

                //Adjust ViewPortLeft and ViewPortTop to keep center point unchanged
                viewer.ViewPortLeft = centerX - viewer.ViewPortWidth / 2;
                viewer.ViewPortTop = centerY - viewer.ViewPortHeight / 2;

                //update the chart, but no need to update the image map yet, as the chart is still
                //zooming and is unstable
                viewer.updateViewPort(true, false);
            }
        }

        //
        // Draw track cursor when mouse is moving over plotarea, and update image map if necessary
        //
        private void WPFChartViewer1_MouseMovePlotArea(object sender, MouseEventArgs e)
        {
            var viewer = sender as WPFChartViewer;

            // Draw crosshair track cursor
            crossHair((XYChart)viewer.Chart, viewer.PlotAreaMouseX, viewer.PlotAreaMouseY);
            viewer.updateDisplay();

            // Hide the track cursor when the mouse leaves the plot area
            viewer.removeDynamicLayer("MouseLeavePlotArea");

            // Update image map if necessary. If the mouse is still dragging, the chart is still 
            // updating and not confirmed, so there is no need to set up the image map.
            if (!viewer.IsMouseDragging)
                updateImageMap(viewer);
        }

        //
        // Draw cross hair cursor with axis labels
        //
        private void crossHair(XYChart c, int mouseX, int mouseY)
        {
            // Clear the current dynamic layer and get the DrawArea object to draw on it.
            DrawArea d = c.initDynamicLayer();

            // The plot area object
            PlotArea plotArea = c.getPlotArea();

            // Draw a vertical line and a horizontal line as the cross hair
            d.vline(plotArea.getTopY(), plotArea.getBottomY(), mouseX, d.dashLineColor(0x000000, 0x0101));
            d.hline(plotArea.getLeftX(), plotArea.getRightX(), mouseY, d.dashLineColor(0x000000, 0x0101));

            // Draw y-axis label
            string label = "<*block,bgColor=FFFFDD,margin=3,edgeColor=000000*>" + c.formatValue(c.getYValue(
                mouseY, c.yAxis()), "{value|P4}") + "<*/*>";
            TTFText t = d.text(label, "Arial Bold", 8);
            t.draw(plotArea.getLeftX() - 5, mouseY, 0x000000, Chart.Right);

            // Draw x-axis label
            label = "<*block,bgColor=FFFFDD,margin=3,edgeColor=000000*>" + c.formatValue(c.getXValue(mouseX),
                "{value|P4}") + "<*/*>";
            t = d.text(label, "Arial Bold", 8);
            t.draw(mouseX, plotArea.getBottomY() + 5, 0x000000, Chart.Top);
        }
    }
}
