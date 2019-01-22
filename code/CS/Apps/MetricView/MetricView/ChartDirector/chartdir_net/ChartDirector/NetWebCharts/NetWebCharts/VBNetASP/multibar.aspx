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
    Dim data0() As Double = {100, 125, 245, 147, 67}
    Dim data1() As Double = {85, 156, 179, 211, 123}
    Dim data2() As Double = {97, 87, 56, 267, 157}
    Dim labels() As String = {"Mon", "Tue", "Wed", "Thu", "Fri"}

    ' Create a XYChart object of size 400 x 240 pixels
    Dim c As XYChart = New XYChart(400, 240)

    ' Add a title to the chart using 10 pt Arial font
    c.addTitle("         Average Weekday Network Load", "", 10)

    ' Set the plot area at (50, 25) and of size 320 x 180. Use two alternative background colors
    ' (0xffffc0 and 0xffffe0)
    c.setPlotArea(50, 25, 320, 180, &Hffffc0, &Hffffe0)

    ' Add a legend box at (55, 18) using horizontal layout. Use 8 pt Arial font, with transparent
    ' background
    c.addLegend(55, 18, False, "", 8).setBackground(Chart.Transparent)

    ' Add a title to the y-axis
    c.yAxis().setTitle("Throughput (MBytes Per Hour)")

    ' Reserve 20 pixels at the top of the y-axis for the legend box
    c.yAxis().setTopMargin(20)

    ' Set the x axis labels
    c.xAxis().setLabels(labels)

    ' Add a multi-bar layer with 3 data sets and 3 pixels 3D depth
    Dim layer As BarLayer = c.addBarLayer2(Chart.Side, 3)
    layer.addDataSet(data0, &Hff8080, "Server #1")
    layer.addDataSet(data1, &H80ff80, "Server #2")
    layer.addDataSet(data2, &H8080ff, "Server #3")

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{dataSetName} on {xLabel}: {value} MBytes/hour'")

End Sub

</script>

<html>
<head>
    <title>Multi-Bar Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Multi-Bar Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

