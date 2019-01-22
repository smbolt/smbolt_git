<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The x and y coordinates of the grid
    Dim dataX() As Double = {-10, -9, -8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, _
        9, 10}
    Dim dataY() As Double = {-10, -9, -8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, _
        9, 10}

    ' The values at the grid points. In this example, we will compute the values using the formula z
    ' = Sin(x * x / 128 - y * y / 256 + 3) * Cos(x / 4 + 1 - Exp(y / 8))
    Dim dataZ((UBound(dataX) + 1) * (UBound(dataY) + 1) - 1) As Double
    For yIndex As Integer = 0 To UBound(dataY)
        Dim y As Double = dataY(yIndex)
        For xIndex As Integer = 0 To UBound(dataX)
            Dim x As Double = dataX(xIndex)
            dataZ(yIndex * (UBound(dataX) + 1) + xIndex) = Math.Sin(x * x / 128.0 - y * y / 256.0 _
                 + 3) * Math.Cos(x / 4.0 + 1 - Math.Exp(y / 8.0))
        Next
    Next

    ' Create a SurfaceChart object of size 750 x 600 pixels
    Dim c As SurfaceChart = New SurfaceChart(750, 600)

    ' Add a title to the chart using 20 points Times New Roman Italic font
    c.addTitle("Surface Energy Density       ", "Times New Roman Italic", 20)

    ' Set the center of the plot region at (380, 260), and set width x depth x height to 360 x 360 x
    ' 270 pixels
    c.setPlotRegion(380, 260, 360, 360, 270)

    ' Set the elevation and rotation angles to 30 and 210 degrees
    c.setViewAngle(30, 210)

    ' Set the perspective level to 60
    c.setPerspective(60)

    ' Set the data to use to plot the chart
    c.setData(dataX, dataY, dataZ)

    ' Spline interpolate data to a 80 x 80 grid for a smooth surface
    c.setInterpolation(80, 80)

    ' Use semi-transparent black (c0000000) for x and y major surface grid lines. Use dotted style
    ' for x and y minor surface grid lines.
    Dim majorGridColor As Integer = &Hc0000000
    Dim minorGridColor As Integer = c.dashLineColor(majorGridColor, Chart.DotLine)
    c.setSurfaceAxisGrid(majorGridColor, majorGridColor, minorGridColor, minorGridColor)

    ' Set contour lines to semi-transparent white (80ffffff)
    c.setContourColor(&H80ffffff)

    ' Add a color axis (the legend) in which the left center is anchored at (665, 280). Set the
    ' length to 200 pixels and the labels on the right side.
    c.setColorAxis(665, 280, Chart.Left, 200, Chart.Right)

    ' Set the x, y and z axis titles using 12 points Arial Bold font
    c.xAxis().setTitle("X Title<*br*>Placeholder", "Arial Bold", 12)
    c.yAxis().setTitle("Y Title<*br*>Placeholder", "Arial Bold", 12)
    c.zAxis().setTitle("Z Title Placeholder", "Arial Bold", 12)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.JPG)

End Sub

</script>

<html>
<head>
    <title>Surface Chart (3)</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Surface Chart (3)
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

