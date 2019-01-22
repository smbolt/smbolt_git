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

    // The values at the grid points. In this example, we will compute the values using the formula
    // z = Sin(x * pi / 3) * Sin(y * pi / 3).
    double[] dataZ = new double[dataX.Length * dataY.Length];
    for(int yIndex = 0; yIndex < dataY.Length; ++yIndex) {
        double y = dataY[yIndex];
        for(int xIndex = 0; xIndex < dataX.Length; ++xIndex) {
            double x = dataX[xIndex];
            dataZ[yIndex * dataX.Length + xIndex] = Math.Sin(x * 3.1416 / 3) * Math.Sin(y * 3.1416 /
                3);
        }
    }

    // Create a XYChart object of size 360 x 360 pixels
    XYChart c = new XYChart(360, 360);

    // Set the plotarea at (30, 25) and of size 300 x 300 pixels. Use semi-transparent black
    // (c0000000) for both horizontal and vertical grid lines
    c.setPlotArea(30, 25, 300, 300, -1, -1, -1, unchecked((int)0xc0000000), -1);

    // Add a contour layer using the given data
    ContourLayer layer = c.addContourLayer(dataX, dataY, dataZ);

    // Set the x-axis and y-axis scale
    c.xAxis().setLinearScale(-4, 4, 1);
    c.yAxis().setLinearScale(-4, 4, 1);

    if (chartIndex == 0) {
        // Discrete coloring, spline surface interpolation
        c.addTitle("Spline Surface - Discrete Coloring", "Arial Bold Italic", 12);
    } else if (chartIndex == 1) {
        // Discrete coloring, linear surface interpolation
        c.addTitle("Linear Surface - Discrete Coloring", "Arial Bold Italic", 12);
        layer.setSmoothInterpolation(false);
    } else if (chartIndex == 2) {
        // Smooth coloring, spline surface interpolation
        c.addTitle("Spline Surface - Continuous Coloring", "Arial Bold Italic", 12);
        layer.setContourColor(Chart.Transparent);
        layer.colorAxis().setColorGradient(true);
    } else {
        // Discrete coloring, linear surface interpolation
        c.addTitle("Linear Surface - Continuous Coloring", "Arial Bold Italic", 12);
        layer.setSmoothInterpolation(false);
        layer.setContourColor(Chart.Transparent);
        layer.colorAxis().setColorGradient(true);
    }

    // Output the chart
    viewer.Image = c.makeWebImage(Chart.JPG);
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
    <title>Contour Interpolation</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Contour Interpolation
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

