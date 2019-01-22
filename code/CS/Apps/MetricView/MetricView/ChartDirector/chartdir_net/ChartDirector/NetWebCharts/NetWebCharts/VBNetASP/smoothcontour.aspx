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
    ' = Sin(x / 2) * Sin(y / 2).
    Dim dataZ((UBound(dataX) + 1) * (UBound(dataY) + 1) - 1) As Double
    For yIndex As Integer = 0 To UBound(dataY)
        Dim y As Double = dataY(yIndex)
        For xIndex As Integer = 0 To UBound(dataX)
            Dim x As Double = dataX(xIndex)
            dataZ(yIndex * (UBound(dataX) + 1) + xIndex) = Math.Sin(x / 2.0) * Math.Sin(y / 2.0)
        Next
    Next

    ' Create a XYChart object of size 600 x 500 pixels
    Dim c As XYChart = New XYChart(600, 500)

    ' Add a title to the chart using 18 points Times New Roman Bold Italic font
    c.addTitle("Nano Lattice Twister Field Intensity        ", "Times New Roman Bold Italic", 18)

    ' Set the plotarea at (75, 40) and of size 400 x 400 pixels. Use semi-transparent black
    ' (80000000) dotted lines for both horizontal and vertical grid lines
    c.setPlotArea(75, 40, 400, 400, -1, -1, -1, c.dashLineColor(&H80000000, Chart.DotLine), -1)

    ' Set x-axis and y-axis title using 12 points Arial Bold Italic font
    c.xAxis().setTitle("Lattice X Direction (nm)", "Arial Bold Italic", 12)
    c.yAxis().setTitle("Lattice Y Direction (nm)", "Arial Bold Italic", 12)

    ' Set x-axis and y-axis labels to use Arial Bold font
    c.xAxis().setLabelStyle("Arial Bold")
    c.yAxis().setLabelStyle("Arial Bold")

    ' When auto-scaling, use tick spacing of 40 pixels as a guideline
    c.yAxis().setTickDensity(40)
    c.xAxis().setTickDensity(40)

    ' Add a contour layer using the given data
    Dim layer As ContourLayer = c.addContourLayer(dataX, dataY, dataZ)

    ' Set the contour color to transparent
    layer.setContourColor(Chart.Transparent)

    ' Move the grid lines in front of the contour layer
    c.getPlotArea().moveGridBefore(layer)

    ' Add a color axis (the legend) in which the left center point is anchored at (495, 240). Set
    ' the length to 370 pixels and the labels on the right side.
    Dim cAxis As ColorAxis = layer.setColorAxis(495, 240, Chart.Left, 370, Chart.Right)

    ' Add a bounding box to the color axis using light grey (eeeeee) as the background and dark grey
    ' (444444) as the border.
    cAxis.setBoundingBox(&Heeeeee, &H444444)

    ' Add a title to the color axis using 12 points Arial Bold Italic font
    cAxis.setTitle("Twist Pressure (Twist-Volt)", "Arial Bold Italic", 12)

    ' Set color axis labels to use Arial Bold font
    cAxis.setLabelStyle("Arial Bold")

    ' Use smooth gradient coloring
    cAxis.setColorGradient(True)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.JPG)

End Sub

</script>

<html>
<head>
    <title>Continuous Contour Coloring</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Continuous Contour Coloring
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

