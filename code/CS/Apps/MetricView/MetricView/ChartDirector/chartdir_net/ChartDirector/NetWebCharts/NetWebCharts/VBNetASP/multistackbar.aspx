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
    Dim data0() As Double = {44, 55, 100}
    Dim data1() As Double = {97, 87, 167}
    Dim data2() As Double = {156, 78, 147}
    Dim data3() As Double = {125, 118, 211}

    ' The labels for the bar chart. The labels contains embedded images as icons.
    Dim labels() As String = {"<*img=service.png*><*br*>Service", _
        "<*img=software.png*><*br*>Software", "<*img=computer.png*><*br*>Hardware"}

    ' Create a XYChart object of size 600 x 350 pixels, using 0xe0e0ff as the background color,
    ' 0xccccff as the border color, with 1 pixel 3D border effect.
    Dim c As XYChart = New XYChart(600, 350, &He0e0ff, &Hccccff, 1)

    ' Set default directory for loading images from current script directory
    Call c.setSearchPath(Server.MapPath("."))

    ' Add a title to the chart using 14 points Times Bold Itatic font and light blue (0x9999ff) as
    ' the background color
    c.addTitle("Business Results 2001 vs 2002", "Times New Roman Bold Italic", 14).setBackground( _
        &H9999ff)

    ' Set the plotarea at (60, 45) and of size 500 x 210 pixels, using white (0xffffff) as the
    ' background
    c.setPlotArea(60, 45, 500, 210, &Hffffff)

    ' Swap the x and y axes to create a horizontal bar chart
    c.swapXY()

    ' Add a title to the y axis using 11 pt Times Bold Italic as font
    c.yAxis().setTitle("Revenue (millions)", "Times New Roman Bold Italic", 11)

    ' Set the labels on the x axis
    c.xAxis().setLabels(labels)

    ' Disable x-axis ticks by setting the tick length to 0
    c.xAxis().setTickLength(0)

    ' Add a stacked bar layer to the chart
    Dim layer As BarLayer = c.addBarLayer2(Chart.Stack)

    ' Add the first two data sets to the chart as a stacked bar group
    layer.addDataGroup("2001")
    layer.addDataSet(data0, &Haaaaff, "Local")
    layer.addDataSet(data1, &H6666ff, "International")

    ' Add the remaining data sets to the chart as another stacked bar group
    layer.addDataGroup("2002")
    layer.addDataSet(data2, &Hffaaaa, "Local")
    layer.addDataSet(data3, &Hff6666, "International")

    ' Set the sub-bar gap to 0, so there is no gap between stacked bars with a group
    layer.setBarGap(0.2, 0)

    ' Set the bar border to transparent
    layer.setBorderColor(Chart.Transparent)

    ' Set the aggregate label format
    layer.setAggregateLabelFormat("Year {dataGroupName}<*br*>{value} millions")

    ' Set the aggregate label font to 8 point Arial Bold Italic
    layer.setAggregateLabelStyle("Arial Bold Italic", 8)

    ' Reverse 20% space at the right during auto-scaling to allow space for the aggregate bar labels
    c.yAxis().setAutoScale(0.2)

    ' Add a legend box at (310, 300) using TopCenter alignment, with 2 column grid layout, and use
    ' 8pt Arial Bold Italic as font
    Dim legendBox As LegendBox = c.addLegend2(310, 300, 2, "Arial Bold Italic", 8)
    legendBox.setAlignment(Chart.TopCenter)

    ' Set the format of the text displayed in the legend box
    legendBox.setText("Year {dataGroupName} {dataSetName} Revenue")

    ' Set the background and border of the legend box to transparent
    legendBox.setBackground(Chart.Transparent, Chart.Transparent)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='Year {dataGroupName} {dataSetName} {xLabel} Revenue: {value} millions'")

End Sub

</script>

<html>
<head>
    <title>Multi-Stacked Bar Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Multi-Stacked Bar Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

