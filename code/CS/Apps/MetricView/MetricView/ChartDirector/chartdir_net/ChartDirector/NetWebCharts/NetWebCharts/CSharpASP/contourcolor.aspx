<%@ Page Language="C#" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

//
// Create chart
//
protected void createChart(WebChartViewer viewer, int chartIndex)
{
    // The x and y coordinates of the grid
    double[] dataX = {-4, -3, -2, -1, 0, 1, 2, 3, 4};
    double[] dataY = {-4, -3, -2, -1, 0, 1, 2, 3, 4};

    // Use random numbers for the z values on the XY grid
    RanSeries r = new RanSeries(99);
    double[] dataZ = r.get2DSeries(dataX.Length, dataY.Length, -0.9, 0.9);

    // Create a XYChart object of size 420 x 360 pixels
    XYChart c = new XYChart(420, 360);

    // Set the plotarea at (30, 25) and of size 300 x 300 pixels. Use semi-transparent grey
    // (0xdd000000) horizontal and vertical grid lines
    c.setPlotArea(30, 25, 300, 300, -1, -1, -1, unchecked((int)0xdd000000), -1);

    // Set the x-axis and y-axis scale
    c.xAxis().setLinearScale(-4, 4, 1);
    c.yAxis().setLinearScale(-4, 4, 1);

    // Add a contour layer using the given data
    ContourLayer layer = c.addContourLayer(dataX, dataY, dataZ);

    // Move the grid lines in front of the contour layer
    c.getPlotArea().moveGridBefore(layer);

    // Add a color axis (the legend) in which the top left corner is anchored at (350, 25). Set the
    // length to 400 300 and the labels on the right side.
    ColorAxis cAxis = layer.setColorAxis(350, 25, Chart.TopLeft, 300, Chart.Right);

    if (chartIndex == 1) {
        // Speicify a color gradient as a list of colors, and use it in the color axis.
        int[] colorGradient = {0x0044cc, 0xffffff, 0x00aa00};
        cAxis.setColorGradient(false, colorGradient);
    } else if (chartIndex == 2) {
        // Specify the color scale to use in the color axis
        double[] colorScale = {-1.0, 0x1a9850, -0.75, 0x66bd63, -0.5, 0xa6d96a, -0.25, 0xd9ef8b, 0,
            0xfee08b, 0.25, 0xfdae61, 0.5, 0xf46d43, 0.75, 0xd73027, 1};
        cAxis.setColorScale(colorScale);
    } else if (chartIndex == 3) {
        // Specify the color scale to use in the color axis. Also specify an underflow color
        // 0x66ccff (blue) for regions that fall below the lower axis limit.
        double[] colorScale = {0, 0xffff99, 0.2, 0x80cdc1, 0.4, 0x35978f, 0.6, 0x01665e, 0.8,
            0x003c30, 1};
        cAxis.setColorScale(colorScale, 0x66ccff);
    }

    // Output the chart
    viewer.Image = c.makeWebImage(Chart.PNG);
}

//
// Page Load event handler
//
protected void Page_Load(object sender, EventArgs e)
{
    createChart(WebChartViewer0, 0);
    createChart(WebChartViewer1, 1);
    createChart(WebChartViewer2, 2);
    createChart(WebChartViewer3, 3);
}

</script>

<html>
<head>
    <title>Contour Color Scale</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Contour Color Scale
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request["SCRIPT_NAME"]%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer0" runat="server" />
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
    <chart:WebChartViewer id="WebChartViewer2" runat="server" />
    <chart:WebChartViewer id="WebChartViewer3" runat="server" />
</body>
</html>

