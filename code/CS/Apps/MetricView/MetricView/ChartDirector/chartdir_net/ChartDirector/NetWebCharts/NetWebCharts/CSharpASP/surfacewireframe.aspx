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
    double[] dataX = {-2, -1, 0, 1, 2};
    double[] dataY = {-2, -1, 0, 1, 2};

    // The values at the grid points. In this example, we will compute the values using the formula
    // z = square_root(15 - x * x - y * y).
    double[] dataZ = new double[dataX.Length * dataY.Length];
    for(int yIndex = 0; yIndex < dataY.Length; ++yIndex) {
        double y = dataY[yIndex];
        for(int xIndex = 0; xIndex < dataX.Length; ++xIndex) {
            double x = dataX[xIndex];
            dataZ[yIndex * dataX.Length + xIndex] = Math.Sqrt(15 - x * x - y * y);
        }
    }

    // Create a SurfaceChart object of size 380 x 340 pixels, with white (ffffff) background and
    // grey (888888) border.
    SurfaceChart c = new SurfaceChart(380, 340, 0xffffff, 0x888888);

    // Demonstrate various wireframes with and without interpolation
    if (chartIndex == 0) {
        // Original data without interpolation
        c.addTitle("5 x 5 Data Points\nStandard Shading", "Arial Bold", 12);
        c.setContourColor(unchecked((int)0x80ffffff));
    } else if (chartIndex == 1) {
        // Original data, spline interpolated to 40 x 40 for smoothness
        c.addTitle("5 x 5 Points - Spline Fitted to 40 x 40\nStandard Shading", "Arial Bold", 12);
        c.setContourColor(unchecked((int)0x80ffffff));
        c.setInterpolation(40, 40);
    } else if (chartIndex == 2) {
        // Rectangular wireframe of original data
        c.addTitle("5 x 5 Data Points\nRectangular Wireframe");
        c.setShadingMode(Chart.RectangularFrame);
    } else if (chartIndex == 3) {
        // Rectangular wireframe of original data spline interpolated to 40 x 40
        c.addTitle("5 x 5 Points - Spline Fitted to 40 x 40\nRectangular Wireframe");
        c.setShadingMode(Chart.RectangularFrame);
        c.setInterpolation(40, 40);
    } else if (chartIndex == 4) {
        // Triangular wireframe of original data
        c.addTitle("5 x 5 Data Points\nTriangular Wireframe");
        c.setShadingMode(Chart.TriangularFrame);
    } else {
        // Triangular wireframe of original data spline interpolated to 40 x 40
        c.addTitle("5 x 5 Points - Spline Fitted to 40 x 40\nTriangular Wireframe");
        c.setShadingMode(Chart.TriangularFrame);
        c.setInterpolation(40, 40);
    }

    // Set the center of the plot region at (200, 170), and set width x depth x height to 200 x 200
    // x 150 pixels
    c.setPlotRegion(200, 170, 200, 200, 150);

    // Set the plot region wall thichness to 5 pixels
    c.setWallThickness(5);

    // Set the elevation and rotation angles to 20 and 30 degrees
    c.setViewAngle(20, 30);

    // Set the data to use to plot the chart
    c.setData(dataX, dataY, dataZ);

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
    createChart(WebChartViewer4, 4);
    createChart(WebChartViewer5, 5);
}

</script>

<html>
<head>
    <title>Surface Wireframe</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Surface Wireframe
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request["SCRIPT_NAME"]%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer0" runat="server" />
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
    <chart:WebChartViewer id="WebChartViewer2" runat="server" />
    <chart:WebChartViewer id="WebChartViewer3" runat="server" />
    <chart:WebChartViewer id="WebChartViewer4" runat="server" />
    <chart:WebChartViewer id="WebChartViewer5" runat="server" />
</body>
</html>

