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
    Dim dataX() As Double = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
    Dim dataY() As Double = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10}

    ' Use random numbers for the z values on the XY grid
    Dim r As RanSeries = New RanSeries(999)
    Dim dataZ() As Double = r.get2DSeries(UBound(dataX) + 1, UBound(dataY) + 1, -0.9, 1.15)

    ' Create a XYChart object of size 640 x 460 pixels
    Dim c As XYChart = New XYChart(640, 460)

    ' Set default text color to dark grey (0x333333)
    c.setColor(Chart.TextColor, &H333333)

    ' Set the plotarea at (30, 25) and of size 400 x 400 pixels. Use semi-transparent grey
    ' (0xdd000000) horizontal and vertical grid lines
    c.setPlotArea(50, 25, 400, 400, -1, -1, Chart.Transparent, &Hdd000000, -1)

    ' Set the x and y axis stems to transparent and the label font to 12pt Arial
    c.xAxis().setColors(Chart.Transparent)
    c.yAxis().setColors(Chart.Transparent)
    c.xAxis().setLabelStyle("Arial", 12)
    c.yAxis().setLabelStyle("Arial", 12)

    ' Set the x-axis and y-axis scale
    c.xAxis().setLinearScale(0, 10, 1)
    c.yAxis().setLinearScale(0, 10, 1)

    ' Add a contour layer using the given data
    Dim layer As ContourLayer = c.addContourLayer(dataX, dataY, dataZ)

    ' Move the grid lines in front of the contour layer
    c.getPlotArea().moveGridBefore(layer)

    ' Define the color scale
    Dim colorScale() As Double = {-0.8, &H0066ff, -0.5, &H66ccff, -0.3, &H66ffff, 0, &H88ff88, _
        0.4, &H00ff00, 0.7, &Hffff00, 0.9, &Hff6600, 1.0, &Hcc0000, 1.1}
    ' Apply the color scale, and specify the underflow and overflow colors for regions exceeding the
    ' color scale
    layer.colorAxis().setColorScale(colorScale, &H0000cc, &H000000)

    '
    ' Instead of displaying the color axis, we use a legend box to display the colors. This is
    ' useful for colors that are unevenly spaced on the color axis.
    '

    ' Add a legend box at (460, 25) with vertical layout, with 12pt Arial font, transparent
    ' background and border, icon size of 15 x 15 pixels, and line spacing of 8 pixels.
    Dim b As LegendBox = c.addLegend(460, 25, True, "Arial", 12)
    b.setBackground(Chart.Transparent, Chart.Transparent)
    b.setKeySize(15, 15)
    b.setKeySpacing(0, 8)

    ' Add the legend box entries
    b.addKey("> 1.1 (Critical)", &H000000)
    b.addKey("1.0 to 1.1 (Alert)", &Hcc0000)
    b.addKey("0.9 to 1.0", &Hff6600)
    b.addKey("0.7 to 0.9", &Hffff00)
    b.addKey("0.4 to 0.7", &H00ff00)
    b.addKey("0.0 to 0.4", &H88ff88)
    b.addKey("-0.3 to 0.0", &H66ffff)
    b.addKey("-0.5 to -0.3", &H66ccff)
    b.addKey("-0.8 to -0.5", &H0066ff)
    b.addKey("< -0.8", &H0000cc)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

End Sub

</script>

<html>
<head>
    <title>Contour Color Legend</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Contour Color Legend
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

