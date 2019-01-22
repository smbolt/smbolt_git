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
    Dim dataX() As Double = {-2, -1, 0, 1, 2}
    Dim dataY() As Double = {-2, -1, 0, 1, 2}

    ' The values at the grid points. In this example, we will compute the values using the formula z
    ' = square_root(15 - x * x - y * y).
    Dim dataZ((UBound(dataX) + 1) * (UBound(dataY) + 1) - 1) As Double
    For yIndex As Integer = 0 To UBound(dataY)
        Dim y As Double = dataY(yIndex)
        For xIndex As Integer = 0 To UBound(dataX)
            Dim x As Double = dataX(xIndex)
            dataZ(yIndex * (UBound(dataX) + 1) + xIndex) = Math.Sqrt(15 - x * x - y * y)
        Next
    Next

    ' Create a SurfaceChart object of size 380 x 340 pixels, with white (ffffff) background and grey
    ' (888888) border.
    Dim c As SurfaceChart = New SurfaceChart(380, 340, &Hffffff, &H888888)

    ' Demonstrate various wireframes with and without interpolation
    If chartIndex = 0 Then
        ' Original data without interpolation
        c.addTitle("5 x 5 Data Points<*br*>Standard Shading", "Arial Bold", 12)
        c.setContourColor(&H80ffffff)
    ElseIf chartIndex = 1 Then
        ' Original data, spline interpolated to 40 x 40 for smoothness
        c.addTitle("5 x 5 Points - Spline Fitted to 40 x 40<*br*>Standard Shading", "Arial Bold", _
            12)
        c.setContourColor(&H80ffffff)
        c.setInterpolation(40, 40)
    ElseIf chartIndex = 2 Then
        ' Rectangular wireframe of original data
        c.addTitle("5 x 5 Data Points<*br*>Rectangular Wireframe")
        c.setShadingMode(Chart.RectangularFrame)
    ElseIf chartIndex = 3 Then
        ' Rectangular wireframe of original data spline interpolated to 40 x 40
        c.addTitle("5 x 5 Points - Spline Fitted to 40 x 40<*br*>Rectangular Wireframe")
        c.setShadingMode(Chart.RectangularFrame)
        c.setInterpolation(40, 40)
    ElseIf chartIndex = 4 Then
        ' Triangular wireframe of original data
        c.addTitle("5 x 5 Data Points<*br*>Triangular Wireframe")
        c.setShadingMode(Chart.TriangularFrame)
    Else
        ' Triangular wireframe of original data spline interpolated to 40 x 40
        c.addTitle("5 x 5 Points - Spline Fitted to 40 x 40<*br*>Triangular Wireframe")
        c.setShadingMode(Chart.TriangularFrame)
        c.setInterpolation(40, 40)
    End If

    ' Set the center of the plot region at (200, 170), and set width x depth x height to 200 x 200 x
    ' 150 pixels
    c.setPlotRegion(200, 170, 200, 200, 150)

    ' Set the plot region wall thichness to 5 pixels
    c.setWallThickness(5)

    ' Set the elevation and rotation angles to 20 and 30 degrees
    c.setViewAngle(20, 30)

    ' Set the data to use to plot the chart
    c.setData(dataX, dataY, dataZ)

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
    createChart(WebChartViewer4, 4)
    createChart(WebChartViewer5, 5)

End Sub

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
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer0" runat="server" />
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
    <chart:WebChartViewer id="WebChartViewer2" runat="server" />
    <chart:WebChartViewer id="WebChartViewer3" runat="server" />
    <chart:WebChartViewer id="WebChartViewer4" runat="server" />
    <chart:WebChartViewer id="WebChartViewer5" runat="server" />
</body>
</html>

