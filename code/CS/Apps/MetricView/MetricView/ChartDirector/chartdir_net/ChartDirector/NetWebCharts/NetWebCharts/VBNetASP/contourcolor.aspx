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
    Dim dataX() As Double = {-4, -3, -2, -1, 0, 1, 2, 3, 4}
    Dim dataY() As Double = {-4, -3, -2, -1, 0, 1, 2, 3, 4}

    ' Use random numbers for the z values on the XY grid
    Dim r As RanSeries = New RanSeries(99)
    Dim dataZ() As Double = r.get2DSeries(UBound(dataX) + 1, UBound(dataY) + 1, -0.9, 0.9)

    ' Create a XYChart object of size 420 x 360 pixels
    Dim c As XYChart = New XYChart(420, 360)

    ' Set the plotarea at (30, 25) and of size 300 x 300 pixels. Use semi-transparent grey
    ' (0xdd000000) horizontal and vertical grid lines
    c.setPlotArea(30, 25, 300, 300, -1, -1, -1, &Hdd000000, -1)

    ' Set the x-axis and y-axis scale
    c.xAxis().setLinearScale(-4, 4, 1)
    c.yAxis().setLinearScale(-4, 4, 1)

    ' Add a contour layer using the given data
    Dim layer As ContourLayer = c.addContourLayer(dataX, dataY, dataZ)

    ' Move the grid lines in front of the contour layer
    c.getPlotArea().moveGridBefore(layer)

    ' Add a color axis (the legend) in which the top left corner is anchored at (350, 25). Set the
    ' length to 400 300 and the labels on the right side.
    Dim cAxis As ColorAxis = layer.setColorAxis(350, 25, Chart.TopLeft, 300, Chart.Right)

    If chartIndex = 1 Then
        ' Speicify a color gradient as a list of colors, and use it in the color axis.
        Dim colorGradient() As Integer = {&H0044cc, &Hffffff, &H00aa00}
        cAxis.setColorGradient(False, colorGradient)
    ElseIf chartIndex = 2 Then
        ' Specify the color scale to use in the color axis
        Dim colorScale() As Double = {-1.0, &H1a9850, -0.75, &H66bd63, -0.5, &Ha6d96a, -0.25, _
            &Hd9ef8b, 0, &Hfee08b, 0.25, &Hfdae61, 0.5, &Hf46d43, 0.75, &Hd73027, 1}
        cAxis.setColorScale(colorScale)
    ElseIf chartIndex = 3 Then
        ' Specify the color scale to use in the color axis. Also specify an underflow color 0x66ccff
        ' (blue) for regions that fall below the lower axis limit.
        Dim colorScale() As Double = {0, &Hffff99, 0.2, &H80cdc1, 0.4, &H35978f, 0.6, &H01665e, _
            0.8, &H003c30, 1}
        cAxis.setColorScale(colorScale, &H66ccff)
    End If

    ' Output the chart
    viewer.Image = c.makeWebImage(Chart.PNG)

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
    <title>Contour Color Scale</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Contour Color Scale
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

