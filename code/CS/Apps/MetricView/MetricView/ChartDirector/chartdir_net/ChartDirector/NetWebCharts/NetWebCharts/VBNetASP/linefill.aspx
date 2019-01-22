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
    Dim data0() As Double = {70, 73, 80, 90, 95, 93, 82, 77, 82, 101, 111, 115}
    Dim data1() As Double = {90, 96, 89, 77, 82, 96, 109, 109, 99, 108, 96, 91}
    Dim data2() As Double = {58, 34, 25, 49, 64, 10, 16, 40, 25, 49, 40, 22}

    ' The labels for the line chart
    Dim labels() As String = {"2008-01", "2008-02", "2008-03", "2008-04", "2008-05", "2008-06", _
        "2008-07", "2008-08", "2008-09", "2008-10", "2008-11", "2008-12"}

    ' Create a XYChart object of size 450 x 450 pixels
    Dim c As XYChart = New XYChart(450, 450)

    ' Add a title to the chart using 15pt Arial Italic font.
    Dim title As ChartDirector.TextBox = c.addTitle("Inter-line Coloring", "Arial Italic", 15)

    ' Add a legend box where the top-center is anchored to the horizontal center of the chart, just
    ' under the title. Use horizontal layout and 10 points Arial Bold font, and transparent
    ' background and border. Use line style legend key.
    Dim legendBox As LegendBox = c.addLegend(c.getWidth() / 2, title.getHeight(), False, _
        "Arial Bold Italic", 10)
    legendBox.setBackground(Chart.Transparent, Chart.Transparent)
    legendBox.setAlignment(Chart.TopCenter)
    legendBox.setLineStyleKey()

    ' Tentatively set the plotarea at (70, 65) and of (chart_width - 100) x (chart_height - 110) in
    ' size. Use light grey (c0c0c0) border and horizontal and vertical grid lines.
    Dim plotArea As PlotArea = c.setPlotArea(70, 65, c.getWidth() - 100, c.getHeight() - 110, -1, _
        -1, &Hc0c0c0, &Hc0c0c0, -1)

    ' Add a title to the y axis using 12pt Arial Bold Italic font
    c.yAxis().setTitle("Axis Title Placeholder", "Arial Bold Italic", 12)

    ' Add a title to the x axis using 12pt Arial Bold Italic font
    c.xAxis().setTitle("Axis Title Placeholder", "Arial Bold Italic", 12)

    ' Set the axes line width to 3 pixels
    c.xAxis().setWidth(3)
    c.yAxis().setWidth(3)

    ' Set the labels on the x axis.
    c.xAxis().setLabels(labels)

    ' Use 8 points Arial rotated by 90 degrees as the x-axis label font
    c.xAxis().setLabelStyle("Arial", 8, Chart.TextColor, 90)

    ' Add a spline curve to the chart
    Dim layer0 As SplineLayer = c.addSplineLayer(data0, &Hff0000, "Data Set 0")
    layer0.setLineWidth(2)

    ' Add a normal line to the chart
    Dim layer1 As LineLayer = c.addLineLayer(data1, &H008800, "Data Set 1")
    layer1.setLineWidth(2)

    ' Color the region between the above spline curve and normal line. Use the semi-transparent red
    ' (80ff000000) if the spline curve is higher than the normal line, otherwise use
    ' semi-transparent green (80008800)
    c.addInterLineLayer(layer0.getLine(), layer1.getLine(), &H80ff0000, &H80008800)

    ' Add another normal line to the chart
    Dim layer2 As LineLayer = c.addLineLayer(data2, &H0000ff, "Data Set 2")
    layer2.setLineWidth(2)

    ' Add a horizontal mark line to the chart at y = 40
    Dim mark As Mark = c.yAxis().addMark(40, -1, "Threshold")
    mark.setLineWidth(2)

    ' Set the mark line to purple (880088) dash line. Use white (ffffff) for the mark label.
    mark.setMarkColor(c.dashLineColor(&H880088), &Hffffff)

    ' Put the mark label at the left side of the mark, with a purple (880088) background.
    mark.setAlignment(Chart.Left)
    mark.setBackground(&H880088)

    ' Color the region between the above normal line and mark line. Use the semi-transparent blue
    ' (800000ff) if the normal line is higher than the mark line, otherwise use semi-transparent
    ' purple (80880088)
    c.addInterLineLayer(layer2.getLine(), mark.getLine(), &H800000ff, &H80880088)

    ' Layout the legend box, so we can get its height
    c.layoutLegend()

    ' Adjust the plot area size, such that the bounding box (inclusive of axes) is 10 pixels from
    ' the left edge, just under the legend box, 25 pixels from the right edge, and 10 pixels from
    ' the bottom edge.
    c.packPlotArea(10, legendBox.getTopY() + legendBox.getHeight(), c.getWidth() - 25, _
        c.getHeight() - 10)

    ' After determining the exact plot area position, we may adjust the legend box and the title
    ' positions so that they are centered relative to the plot area (instead of the chart)
    legendBox.setPos(plotArea.getLeftX() + (plotArea.getWidth() - legendBox.getWidth()) / 2, _
        legendBox.getTopY())
    title.setPos(plotArea.getLeftX() + (plotArea.getWidth() - title.getWidth()) / 2, _
        title.getTopY())

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{dataSetName} in {xLabel}: {value}'")

End Sub

</script>

<html>
<head>
    <title>Inter-Line Coloring</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Inter-Line Coloring
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

