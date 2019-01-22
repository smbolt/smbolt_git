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
    Dim dataX0() As Double = {170, 300, 1000, 1700}
    Dim dataY0() As Double = {16, 69, 16, 75}
    Dim dataZ0() As Double = {52, 105, 88, 140}

    Dim dataX1() As Double = {500, 1000, 1300}
    Dim dataY1() As Double = {40, 58, 85}
    Dim dataZ1() As Double = {140, 202, 84}

    ' Create a XYChart object of size 540 x 480 pixels
    Dim c As XYChart = New XYChart(540, 480)

    ' Set the plotarea at (70, 65) and of size 400 x 350 pixels. Turn on both horizontal and
    ' vertical grid lines with light grey color (0xc0c0c0)
    c.setPlotArea(70, 65, 400, 350, -1, -1, Chart.Transparent, &Hc0c0c0, -1)

    ' Add a legend box at (70, 30) (top of the chart) with horizontal layout. Use 12pt Times Bold
    ' Italic font. Set the background and border color to Transparent.
    c.addLegend(70, 30, False, "Times New Roman Bold Italic", 12).setBackground(Chart.Transparent)

    ' Add a title to the chart using 18pt Times Bold Itatic font.
    c.addTitle("Product Comparison Chart", "Times New Roman Bold Italic", 18)

    ' Add titles to the axes using 12pt Arial Bold Italic font
    c.yAxis().setTitle("Capacity (tons)", "Arial Bold Italic", 12)
    c.xAxis().setTitle("Range (miles)", "Arial Bold Italic", 12)

    ' Set the axes line width to 3 pixels
    c.xAxis().setWidth(3)
    c.yAxis().setWidth(3)

    ' Add (dataX0, dataY0) as a scatter layer with red (ff3333) glass spheres, where the sphere size
    ' is modulated by dataZ0. This creates a bubble effect.
    c.addScatterLayer(dataX0, dataY0, "Technology AAA", Chart.GlassSphere2Shape, 15, &Hff3333 _
        ).setSymbolScale(dataZ0)

    ' Add (dataX1, dataY1) as a scatter layer with blue (0000ff) glass spheres, where the sphere
    ' size is modulated by dataZ1. This creates a bubble effect.
    c.addScatterLayer(dataX1, dataY1, "Technology BBB", Chart.GlassSphere2Shape, 15, &H0000ff _
        ).setSymbolScale(dataZ1)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='[{dataSetName}] Range = {x} miles, Capacity = {value} tons, Length = {z} meters'")

End Sub

</script>

<html>
<head>
    <title>3D Bubble Chart (2)</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        3D Bubble Chart (2)
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

