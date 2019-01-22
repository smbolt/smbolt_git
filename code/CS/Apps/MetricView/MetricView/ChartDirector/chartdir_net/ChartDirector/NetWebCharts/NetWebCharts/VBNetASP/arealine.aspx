<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' In this example, we simply use random data for the 2 data series.
    Dim r As RanSeries = New RanSeries(127)
    Dim data0() As Double = r.getSeries(180, 70, -5, 5)
    Dim data1() As Double = r.getSeries(180, 150, -15, 15)
    Dim timeStamps() As Date = r.getDateSeries(180, DateSerial(2014, 3, 1), 86400)

    ' Create a XYChart object of size 640 x 420 pixels
    Dim c As XYChart = New XYChart(640, 420)

    ' Set default text color to dark grey (0x333333)
    c.setColor(Chart.TextColor, &H333333)

    ' Add a title box using grey (0x555555) 20pt Arial Bold font
    c.addTitle("   Plasma Stabilizer Energy Usage", "Arial Bold", 20, &H555555)

    ' Set the plotarea at (70, 70) and of size 540 x 320 pixels, with transparent background and
    ' border and light grey (0xcccccc) horizontal grid lines
    c.setPlotArea(70, 70, 540, 320, -1, -1, Chart.Transparent, &Hcccccc)

    ' Add a legend box with horizontal layout above the plot area at (70, 32). Use 12pt Arial Bold
    ' dark grey (0x555555) font, transparent background and border, and line style legend icon.
    Dim b As LegendBox = c.addLegend(70, 32, False, "Arial Bold", 12)
    b.setFontColor(&H555555)
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

    ' Use "mm/yyyy" as the x-axis label format for the first plotted month of a year, and "mm" for
    ' other months
    c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "{value|mm/yyyy} ", _
        Chart.StartOfMonthFilter(), "{value|mm}")

    ' Add a title to the y axis using dark grey (0x555555) 12pt Arial Bold font
    c.yAxis().setTitle("Energy (kWh)", "Arial Bold", 14, &H555555)

    ' Add a line layer with 2-pixel line width
    Dim layer0 As LineLayer = c.addLineLayer(data0, &Hcc0000, "Power Usage")
    layer0.setXData(timeStamps)
    layer0.setLineWidth(2)

    ' Add an area layer using semi-transparent blue (0x7f0044cc) as the fill color
    Dim layer1 As AreaLayer = c.addAreaLayer(data1, &H7f0044cc, "Effective Load")
    layer1.setXData(timeStamps)
    layer1.setBorderColor(Chart.SameAsMainColor)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", "title='[{x|mm dd, yyyy}] {value} kWh'")

End Sub

</script>

<html>
<head>
    <title>Area Line Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Area Line Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

