<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The data for the bar chart
    Dim data0() As Double = {100, 115, 165, 107, 67}
    Dim data1() As Double = {85, 106, 129, 161, 123}
    Dim data2() As Double = {67, 87, 86, 167, 157}

    ' The labels for the bar chart
    Dim labels() As String = {"Mon", "Tue", "Wed", "Thu", "Fri"}

    ' Create a XYChart object of size 600 x 360 pixels
    Dim c As XYChart = New XYChart(600, 360)

    ' Set default text color to dark grey (0x333333)
    c.setColor(Chart.TextColor, &H333333)

    ' Set the plotarea at (70, 20) and of size 400 x 300 pixels, with transparent background and
    ' border and light grey (0xcccccc) horizontal grid lines
    c.setPlotArea(70, 20, 400, 300, Chart.Transparent, -1, Chart.Transparent, &Hcccccc)

    ' Add a legend box at (480, 20) using vertical layout and 12pt Arial font. Set background and
    ' border to transparent and key icon border to the same as the fill color.
    Dim b As LegendBox = c.addLegend(480, 20, True, "Arial", 12)
    b.setBackground(Chart.Transparent, Chart.Transparent)
    b.setKeyBorder(Chart.SameAsMainColor)

    ' Set the x and y axis stems to transparent and the label font to 12pt Arial
    c.xAxis().setColors(Chart.Transparent)
    c.yAxis().setColors(Chart.Transparent)
    c.xAxis().setLabelStyle("Arial", 12)
    c.yAxis().setLabelStyle("Arial", 12)

    ' Add a stacked bar layer
    Dim layer As BarLayer = c.addBarLayer2(Chart.Stack)

    ' Add the three data sets to the bar layer
    layer.addDataSet(data0, &Haaccee, "Server # 1")
    layer.addDataSet(data1, &Hbbdd88, "Server # 2")
    layer.addDataSet(data2, &Heeaa66, "Server # 3")

    ' Set the bar border to transparent
    layer.setBorderColor(Chart.Transparent)

    ' Enable labelling for the entire bar and use 12pt Arial font
    layer.setAggregateLabelStyle("Arial", 12)

    ' Enable labelling for the bar segments and use 12pt Arial font with center alignment
    layer.setDataLabelStyle("Arial", 10).setAlignment(Chart.Center)

    ' For a vertical stacked bar with positive data, the first data set is at the bottom. For the
    ' legend box, by default, the first entry at the top. We can reverse the legend order to make
    ' the legend box consistent with the stacked bar.
    layer.setLegendOrder(Chart.ReverseLegend)

    ' Set the labels on the x axis.
    c.xAxis().setLabels(labels)

    ' For the automatic y-axis labels, set the minimum spacing to 40 pixels.
    c.yAxis().setTickDensity(40)

    ' Add a title to the y axis using dark grey (0x555555) 14pt Arial Bold font
    c.yAxis().setTitle("Y-Axis Title Placeholder", "Arial Bold", 14, &H555555)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{dataSetName} on {xLabel}: {value} MBytes/hour'")

End Sub

</script>

<html>
<head>
    <title>Stacked Bar Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Stacked Bar Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

