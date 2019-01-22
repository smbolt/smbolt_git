<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The data for the area chart
    Dim data0() As Double = {42, 49, 33, 38, 51, 46, 29, 41, 44, 57, 59, 52, 37, 34, 51, 56, 56, _
        60, 70, 76, 63, 67, 75, 64, 51}
    Dim data1() As Double = {50, 45, 47, 34, 42, 49, 63, 62, 73, 59, 56, 50, 64, 60, 67, 67, 58, _
        59, 73, 77, 84, 82, 80, 84, 89}
    Dim data2() As Double = {61, 79, 85, 66, 53, 39, 24, 21, 37, 56, 37, 22, 21, 33, 13, 17, 4, _
        23, 16, 25, 9, 10, 5, 7, 16}
    Dim labels() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", _
        "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24"}

    ' Create a XYChart object of size 300 x 210 pixels. Set the background to pale yellow (0xffffc0)
    ' with a black border (0x0)
    Dim c As XYChart = New XYChart(300, 210, &Hffffc0, &H000000)

    ' Set the plotarea at (50, 30) and of size 240 x 140 pixels. Use white (0xffffff) background.
    c.setPlotArea(50, 30, 240, 140).setBackground(&Hffffff)

    ' Add a legend box at (50, 185) (below of plot area) using horizontal layout. Use 8pt Arial font
    ' with Transparent background.
    c.addLegend(50, 185, False, "", 8).setBackground(Chart.Transparent)

    ' Add a title box to the chart using 8pt Arial Bold font, with yellow (0xffff40) background and
    ' a black border (0x0)
    c.addTitle("Sales Volume", "Arial Bold", 8).setBackground(&Hffff40, 0)

    ' Set the y axis label format to US$nnnn
    c.yAxis().setLabelFormat("US${value}")

    ' Set the labels on the x axis.
    c.xAxis().setLabels(labels)

    ' Display 1 out of 2 labels on the x-axis. Show minor ticks for remaining labels.
    c.xAxis().setLabelStep(2, 1)

    ' Add an stack area layer with three data sets
    Dim layer As AreaLayer = c.addAreaLayer2(Chart.Stack)
    layer.addDataSet(data0, &H4040ff, "Store #1")
    layer.addDataSet(data1, &Hff4040, "Store #2")
    layer.addDataSet(data2, &H40ff40, "Store #3")

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{dataSetName} sales at hour {xLabel}: US${value}K'")

End Sub

</script>

<html>
<head>
    <title>Stacked Area Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Stacked Area Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

