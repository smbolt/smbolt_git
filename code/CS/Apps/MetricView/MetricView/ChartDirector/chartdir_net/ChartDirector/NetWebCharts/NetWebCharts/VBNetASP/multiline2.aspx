<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' In this example, we simply use random data for the 3 data series.
    Dim r As RanSeries = New RanSeries(129)
    Dim data0() As Double = r.getSeries(100, 100, -15, 15)
    Dim data1() As Double = r.getSeries(100, 160, -15, 15)
    Dim data2() As Double = r.getSeries(100, 220, -15, 15)
    Dim timeStamps() As Date = r.getDateSeries(100, DateSerial(2014, 1, 1), 86400)

    ' Create a XYChart object of size 600 x 400 pixels
    Dim c As XYChart = New XYChart(600, 400)

    ' Set default text color to dark grey (0x333333)
    c.setColor(Chart.TextColor, &H333333)

    ' Add a title box using grey (0x555555) 20pt Arial font
    c.addTitle("    Multi-Line Chart Demonstration", "Arial", 20, &H555555)

    ' Set the plotarea at (70, 70) and of size 500 x 300 pixels, with transparent background and
    ' border and light grey (0xcccccc) horizontal grid lines
    c.setPlotArea(70, 70, 500, 300, Chart.Transparent, -1, Chart.Transparent, &Hcccccc)

    ' Add a legend box with horizontal layout above the plot area at (70, 35). Use 12pt Arial font,
    ' transparent background and border, and line style legend icon.
    Dim b As LegendBox = c.addLegend(70, 35, False, "Arial", 12)
    b.setBackground(Chart.Transparent, Chart.Transparent)
    b.setLineStyleKey()

    ' Set axis label font to 12pt Arial
    c.xAxis().setLabelStyle("Arial", 12)
    c.yAxis().setLabelStyle("Arial", 12)

    ' Set the x and y axis stems to transparent, and the x-axis tick color to grey (0xaaaaaa)
    c.xAxis().setColors(Chart.Transparent, Chart.TextColor, Chart.TextColor, &Haaaaaa)
    c.yAxis().setColors(Chart.Transparent)

    ' Set the major/minor tick lengths for the x-axis to 10 and 0.
    c.xAxis().setTickLength(10, 0)

    ' For the automatic axis labels, set the minimum spacing to 80/40 pixels for the x/y axis.
    c.xAxis().setTickDensity(80)
    c.yAxis().setTickDensity(40)

    ' Add a title to the y axis using dark grey (0x555555) 14pt Arial font
    c.yAxis().setTitle("Y-Axis Title Placeholder", "Arial", 14, &H555555)

    ' Add a line layer to the chart with 3-pixel line width
    Dim layer As LineLayer = c.addLineLayer2()
    layer.setLineWidth(3)

    ' Add 3 data series to the line layer
    layer.addDataSet(data0, &H5588cc, "Alpha")
    layer.addDataSet(data1, &Hee9944, "Beta")
    layer.addDataSet(data2, &H99bb55, "Gamma")

    ' The x-coordinates for the line layer
    layer.setXData(timeStamps)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='[{x|mm/dd/yyyy}] {dataSetName}: {value}'")

End Sub

</script>

<html>
<head>
    <title>Multi-Line Chart (2)</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Multi-Line Chart (2)
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

