<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Create chart
'
Protected Sub createChart(viewer As WebChartViewer, chartIndex As Integer)

    ' The x and y coordinates of the grid
    Dim dataX() As Double = {-10, -8, -6, -4, -2, 0, 2, 4, 6, 8, 10}
    Dim dataY() As Double = {-10, -8, -6, -4, -2, 0, 2, 4, 6, 8, 10}

    ' The values at the grid points. In this example, we will compute the values using the formula z
    ' = x * sin(y) + y * sin(x).
    Dim dataZ((UBound(dataX) + 1) * (UBound(dataY) + 1) - 1) As Double
    For yIndex As Integer = 0 To UBound(dataY)
        Dim y As Double = dataY(yIndex)
        For xIndex As Integer = 0 To UBound(dataX)
            Dim x As Double = dataX(xIndex)
            dataZ(yIndex * (UBound(dataX) + 1) + xIndex) = x * Math.Sin(y) + y * Math.Sin(x)
        Next
    Next

    ' Create a SurfaceChart object of size 380 x 400 pixels, with white (ffffff) background and grey
    ' (888888) border.
    Dim c As SurfaceChart = New SurfaceChart(380, 400, &Hffffff, &H888888)

    ' Demonstrate various shading methods
    If chartIndex = 0 Then
        c.addTitle("11 x 11 Data Points<*br*>Smooth Shading")
    ElseIf chartIndex = 1 Then
        c.addTitle("11 x 11 Points - Spline Fitted to 50 x 50<*br*>Smooth Shading")
        c.setInterpolation(50, 50)
    ElseIf chartIndex = 2 Then
        c.addTitle("11 x 11 Data Points<*br*>Rectangular Shading")
        c.setShadingMode(Chart.RectangularShading)
    Else
        c.addTitle("11 x 11 Data Points<*br*>Triangular Shading")
        c.setShadingMode(Chart.TriangularShading)
    End If

    ' Set the center of the plot region at (175, 200), and set width x depth x height to 200 x 200 x
    ' 160 pixels
    c.setPlotRegion(175, 200, 200, 200, 160)

    ' Set the plot region wall thichness to 5 pixels
    c.setWallThickness(5)

    ' Set the elevation and rotation angles to 45 and 60 degrees
    c.setViewAngle(45, 60)

    ' Set the perspective level to 35
    c.setPerspective(35)

    ' Set the data to use to plot the chart
    c.setData(dataX, dataY, dataZ)

    ' Set contour lines to semi-transparent black (c0000000)
    c.setContourColor(&Hc0000000)

    ' Output the chart
    viewer.Image = c.makeWebImage(Chart.JPG)

End Sub

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    createChart(WebChartViewer0, 0)
    createChart(WebChartViewer1, 1)
    createChart(WebChartViewer2, 2)
    createChart(WebChartViewer3, 3)

End Sub

</script>

<html>
<head>
    <title>Surface Shading</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Surface Shading
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer0" runat="server" />
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
    <chart:WebChartViewer id="WebChartViewer2" runat="server" />
    <chart:WebChartViewer id="WebChartViewer3" runat="server" />
</body>
</html>

