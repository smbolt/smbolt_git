<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The XY points for the bubble chart. The bubble chart has independent bubble size on the X and
    ' Y direction.
    Dim dataX0() As Double = {1000, 1500, 1700}
    Dim dataY0() As Double = {25, 20, 65}
    Dim dataZX0() As Double = {500, 200, 600}
    Dim dataZY0() As Double = {15, 30, 20}

    Dim dataX1() As Double = {500, 1000, 1300}
    Dim dataY1() As Double = {35, 50, 75}
    Dim dataZX1() As Double = {800, 300, 500}
    Dim dataZY1() As Double = {8, 27, 25}

    Dim dataX2() As Double = {150, 300}
    Dim dataY2() As Double = {20, 60}
    Dim dataZX2() As Double = {160, 400}
    Dim dataZY2() As Double = {30, 20}

    ' Create a XYChart object of size 450 x 420 pixels
    Dim c As XYChart = New XYChart(450, 420)

    ' Set the plotarea at (55, 65) and of size 350 x 300 pixels, with a light grey border
    ' (0xc0c0c0). Turn on both horizontal and vertical grid lines with light grey color (0xc0c0c0)
    c.setPlotArea(55, 65, 350, 300, -1, -1, &Hc0c0c0, &Hc0c0c0, -1)

    ' Add a legend box at (50, 30) (top of the chart) with horizontal layout. Use 12pt Times Bold
    ' Italic font. Set the background and border color to Transparent.
    c.addLegend(50, 30, False, "Times New Roman Bold Italic", 12).setBackground(Chart.Transparent)

    ' Add a title to the chart using 18pt Times Bold Itatic font.
    c.addTitle("Plasma Battery Comparison", "Times New Roman Bold Italic", 18)

    ' Add titles to the axes using 12pt Arial Bold Italic font
    c.yAxis().setTitle("Operating Current", "Arial Bold Italic", 12)
    c.xAxis().setTitle("Operating Voltage", "Arial Bold Italic", 12)

    ' Set the axes line width to 3 pixels
    c.xAxis().setWidth(3)
    c.yAxis().setWidth(3)

    ' Add (dataX0, dataY0) as a standard scatter layer, and also as a "bubble" scatter layer, using
    ' circles as symbols. The "bubble" scatter layer has symbol size modulated by (dataZX0, dataZY0)
    ' using the scale on the x and y axes.
    c.addScatterLayer(dataX0, dataY0, "Vendor A", Chart.CircleSymbol, 9, &Hff3333, &Hff3333)
    c.addScatterLayer(dataX0, dataY0, "", Chart.CircleSymbol, 9, &H80ff3333, &H80ff3333 _
        ).setSymbolScale(dataZX0, Chart.XAxisScale, dataZY0, Chart.YAxisScale)

    ' Add (dataX1, dataY1) as a standard scatter layer, and also as a "bubble" scatter layer, using
    ' squares as symbols. The "bubble" scatter layer has symbol size modulated by (dataZX1, dataZY1)
    ' using the scale on the x and y axes.
    c.addScatterLayer(dataX1, dataY1, "Vendor B", Chart.SquareSymbol, 7, &H3333ff, &H3333ff)
    c.addScatterLayer(dataX1, dataY1, "", Chart.SquareSymbol, 9, &H803333ff, &H803333ff _
        ).setSymbolScale(dataZX1, Chart.XAxisScale, dataZY1, Chart.YAxisScale)

    ' Add (dataX2, dataY2) as a standard scatter layer, and also as a "bubble" scatter layer, using
    ' diamonds as symbols. The "bubble" scatter layer has symbol size modulated by (dataZX2,
    ' dataZY2) using the scale on the x and y axes.
    c.addScatterLayer(dataX2, dataY2, "Vendor C", Chart.DiamondSymbol, 9, &H00ff00, &H00ff00)
    c.addScatterLayer(dataX2, dataY2, "", Chart.DiamondSymbol, 9, &H8033ff33, &H8033ff33 _
        ).setSymbolScale(dataZX2, Chart.XAxisScale, dataZY2, Chart.YAxisScale)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='Voltage = {x} +/- {={zx}/2} V, Current = {value} +/- {={zy}/2} A'")

End Sub

</script>

<html>
<head>
    <title>Bubble XY Scaling</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Bubble XY Scaling
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

