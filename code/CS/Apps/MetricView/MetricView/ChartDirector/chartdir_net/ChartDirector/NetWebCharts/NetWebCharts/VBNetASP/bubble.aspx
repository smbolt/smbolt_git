<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The XYZ points for the bubble chart
    Dim dataX0() As Double = {150, 300, 1000, 1700}
    Dim dataY0() As Double = {12, 60, 25, 65}
    Dim dataZ0() As Double = {20, 50, 50, 85}

    Dim dataX1() As Double = {500, 1000, 1300}
    Dim dataY1() As Double = {35, 50, 75}
    Dim dataZ1() As Double = {30, 55, 95}

    ' Create a XYChart object of size 450 x 420 pixels
    Dim c As XYChart = New XYChart(450, 420)

    ' Set the plotarea at (55, 65) and of size 350 x 300 pixels, with a light grey border
    ' (0xc0c0c0). Turn on both horizontal and vertical grid lines with light grey color (0xc0c0c0)
    c.setPlotArea(55, 65, 350, 300, -1, -1, &Hc0c0c0, &Hc0c0c0, -1)

    ' Add a legend box at (50, 30) (top of the chart) with horizontal layout. Use 12pt Times Bold
    ' Italic font. Set the background and border color to Transparent.
    c.addLegend(50, 30, False, "Times New Roman Bold Italic", 12).setBackground(Chart.Transparent)

    ' Add a title to the chart using 18pt Times Bold Itatic font.
    c.addTitle("Product Comparison Chart", "Times New Roman Bold Italic", 18)

    ' Add a title to the y axis using 12pt Arial Bold Italic font
    c.yAxis().setTitle("Capacity (tons)", "Arial Bold Italic", 12)

    ' Add a title to the x axis using 12pt Arial Bold Italic font
    c.xAxis().setTitle("Range (miles)", "Arial Bold Italic", 12)

    ' Set the axes line width to 3 pixels
    c.xAxis().setWidth(3)
    c.yAxis().setWidth(3)

    ' Add (dataX0, dataY0) as a scatter layer with semi-transparent red (0x80ff3333) circle symbols,
    ' where the circle size is modulated by dataZ0. This creates a bubble effect.
    c.addScatterLayer(dataX0, dataY0, "Technology AAA", Chart.CircleSymbol, 9, &H80ff3333, _
        &H80ff3333).setSymbolScale(dataZ0)

    ' Add (dataX1, dataY1) as a scatter layer with semi-transparent green (0x803333ff) circle
    ' symbols, where the circle size is modulated by dataZ1. This creates a bubble effect.
    c.addScatterLayer(dataX1, dataY1, "Technology BBB", Chart.CircleSymbol, 9, &H803333ff, _
        &H803333ff).setSymbolScale(dataZ1)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='[{dataSetName}] Range = {x} miles, Capacity = {value} tons, Length = {z} meters'")

End Sub

</script>

<html>
<head>
    <title>Bubble Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Bubble Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

