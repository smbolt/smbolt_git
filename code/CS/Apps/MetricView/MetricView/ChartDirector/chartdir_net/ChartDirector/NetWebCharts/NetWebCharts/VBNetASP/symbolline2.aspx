<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The data for the line chart
    Dim data0() As Double = {42, 49, 33, 38, 64, 56, 29, 41, 44, 57, 59, 42}
    Dim data1() As Double = {65, 75, 47, 34, 42, 49, 73, 62, 90, 69, 66, 78}
    Dim data2() As Double = {36, 28, 25, 28, 38, 20, 22, 30, 25, 33, 30, 24}
    Dim labels() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", _
        "Oct", "Nov", "Dec"}

    ' Create a XYChart object of size 600 x 375 pixels
    Dim c As XYChart = New XYChart(600, 375)

    ' Add a title to the chart using 18pt Times Bold Italic font
    c.addTitle("Product Line Global Revenue", "Times New Roman Bold Italic", 18)

    ' Set the plotarea at (50, 55) and of 500 x 280 pixels in size. Use a vertical gradient color
    ' from light blue (f9f9ff) to sky blue (aaccff) as background. Set border to transparent and
    ' grid lines to white (ffffff).
    c.setPlotArea(50, 55, 500, 280, c.linearGradientColor(0, 55, 0, 335, &Hf9fcff, &Haaccff), -1, _
        Chart.Transparent, &Hffffff)

    ' Add a legend box at (50, 28) using horizontal layout. Use 10pt Arial Bold as font, with
    ' transparent background.
    c.addLegend(50, 28, False, "Arial Bold", 10).setBackground(Chart.Transparent)

    ' Set the x axis labels
    c.xAxis().setLabels(labels)

    ' Set y-axis tick density to 30 pixels. ChartDirector auto-scaling will use this as the
    ' guideline when putting ticks on the y-axis.
    c.yAxis().setTickDensity(30)

    ' Set axis label style to 8pt Arial Bold
    c.xAxis().setLabelStyle("Arial Bold", 8)
    c.yAxis().setLabelStyle("Arial Bold", 8)

    ' Set axis line width to 2 pixels
    c.xAxis().setWidth(2)
    c.yAxis().setWidth(2)

    ' Add axis title using 10pt Arial Bold Italic font
    c.yAxis().setTitle("Revenue in USD millions", "Arial Bold Italic", 10)

    ' Add a line layer to the chart
    Dim layer As LineLayer = c.addLineLayer2()

    ' Set the line width to 3 pixels
    layer.setLineWidth(3)

    ' Add the three data sets to the line layer, using circles, diamands and X shapes as symbols
    layer.addDataSet(data0, &Hff0000, "Quantum Computer").setDataSymbol(Chart.CircleSymbol, 9)
    layer.addDataSet(data1, &H00ff00, "Atom Synthesizer").setDataSymbol(Chart.DiamondSymbol, 11)
    layer.addDataSet(data2, &Hff6600, "Proton Cannon").setDataSymbol(Chart.Cross2Shape(), 11)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='Revenue of {dataSetName} in {xLabel}: US$ {value}M'")

End Sub

</script>

<html>
<head>
    <title>Symbol Line Chart (2)</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Symbol Line Chart (2)
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

