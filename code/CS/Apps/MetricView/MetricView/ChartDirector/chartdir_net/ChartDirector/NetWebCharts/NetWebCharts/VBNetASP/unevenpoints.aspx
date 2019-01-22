<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' Data points which more unevenly spaced in time
    Dim data0Y() As Double = {62, 69, 53, 58, 84, 76, 49, 61, 64, 77, 79}
    Dim data0X() As Date = {DateSerial(2007, 1, 1), DateSerial(2007, 1, 2), DateSerial(2007, 1, 5 _
        ), DateSerial(2007, 1, 7), DateSerial(2007, 1, 10), DateSerial(2007, 1, 14), DateSerial( _
        2007, 1, 17), DateSerial(2007, 1, 18), DateSerial(2007, 1, 19), DateSerial(2007, 1, 20), _
        DateSerial(2007, 1, 21)}

    ' Data points which are evenly spaced in a certain time range
    Dim data1Y() As Double = {36, 25, 28, 38, 20, 30, 27, 35, 65, 60, 40, 73, 62, 90, 75, 72}
    Dim data1Start As Date = DateSerial(2007, 1, 1)
    Dim data1End As Date = DateSerial(2007, 1, 16)

    ' Data points which are evenly spaced in another time range, in which the spacing is different
    ' from the above series
    Dim data2Y() As Double = {25, 15, 30, 23, 32, 55, 45}
    Dim data2Start As Date = DateSerial(2007, 1, 9)
    Dim data2End As Date = DateSerial(2007, 1, 21)

    ' Create a XYChart object of size 600 x 400 pixels. Use a vertical gradient color from light
    ' blue (99ccff) to white (ffffff) spanning the top 100 pixels as background. Set border to grey
    ' (888888). Use rounded corners. Enable soft drop shadow.
    Dim c As XYChart = New XYChart(600, 400)
    c.setBackground(c.linearGradientColor(0, 0, 0, 100, &H99ccff, &Hffffff), &H888888)
    c.setRoundedFrame()
    c.setDropShadow()

    ' Add a title using 18pt Times New Roman Bold Italic font. Set top margin to 16 pixels.
    c.addTitle("Product Line Order Backlog", "Times New Roman Bold Italic", 18).setMargin2(0, 0, _
        16, 0)

    ' Set the plotarea at (60, 80) and of 510 x 275 pixels in size. Use transparent border and dark
    ' grey (444444) dotted grid lines
    Dim plotArea As PlotArea = c.setPlotArea(60, 80, 510, 275, -1, -1, Chart.Transparent, _
        c.dashLineColor(&H444444, &H0101), -1)

    ' Add a legend box where the top-center is anchored to the horizontal center of the plot area at
    ' y = 45. Use horizontal layout and 10 points Arial Bold font, and transparent background and
    ' border.
    Dim legendBox As LegendBox = c.addLegend(plotArea.getLeftX() + plotArea.getWidth() / 2, 45, _
        False, "Arial Bold", 10)
    legendBox.setAlignment(Chart.TopCenter)
    legendBox.setBackground(Chart.Transparent, Chart.Transparent)

    ' Set x-axis tick density to 75 pixels and y-axis tick density to 30 pixels. ChartDirector
    ' auto-scaling will use this as the guidelines when putting ticks on the x-axis and y-axis.
    c.yAxis().setTickDensity(30)
    c.xAxis().setTickDensity(75)

    ' Set all axes to transparent
    c.xAxis().setColors(Chart.Transparent)
    c.yAxis().setColors(Chart.Transparent)

    ' Set the x-axis margins to 15 pixels, so that the horizontal grid lines can extend beyond the
    ' leftmost and rightmost vertical grid lines
    c.xAxis().setMargin(15, 15)

    ' Set axis label style to 8pt Arial Bold
    c.xAxis().setLabelStyle("Arial Bold", 8)
    c.yAxis().setLabelStyle("Arial Bold", 8)
    c.yAxis2().setLabelStyle("Arial Bold", 8)

    ' Add axis title using 10pt Arial Bold Italic font
    c.yAxis().setTitle("Backlog in USD millions", "Arial Bold Italic", 10)

    ' Add the first data series
    Dim layer0 As LineLayer = c.addLineLayer2()
    layer0.addDataSet(data0Y, &Hff0000, "Quantum Computer").setDataSymbol(Chart.GlassSphere2Shape, _
        11)
    layer0.setXData(data0X)
    layer0.setLineWidth(3)

    ' Add the second data series
    Dim layer1 As LineLayer = c.addLineLayer2()
    layer1.addDataSet(data1Y, &H00ff00, "Atom Synthesizer").setDataSymbol(Chart.GlassSphere2Shape, _
        11)
    layer1.setXData2(data1Start, data1End)
    layer1.setLineWidth(3)

    ' Add the third data series
    Dim layer2 As LineLayer = c.addLineLayer2()
    layer2.addDataSet(data2Y, &Hff6600, "Proton Cannon").setDataSymbol(Chart.GlassSphere2Shape, 11)
    layer2.setXData2(data2Start, data2End)
    layer2.setLineWidth(3)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='Backlog of {dataSetName} at {x|mm/dd/yyyy}: US$ {value}M'")

End Sub

</script>

<html>
<head>
    <title>Uneven Data Points </title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Uneven Data Points 
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

