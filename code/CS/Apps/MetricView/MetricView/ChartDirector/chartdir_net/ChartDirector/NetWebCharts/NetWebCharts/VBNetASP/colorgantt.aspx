<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The tasks for the gantt chart
    Dim labels() As String = {"Market Research", "Define Specifications", "Overall Archiecture", _
        "Project Planning", "Detail Design", "Software Development", "Test Plan", _
        "Testing and QA", "User Documentation"}

    ' The task index, start date, end date and color for each bar
    Dim taskNo() As Double = {0, 0, 1, 2, 3, 4, 5, 6, 6, 7, 8, 8}
    Dim startDate() As Date = {DateSerial(2004, 8, 16), DateSerial(2004, 10, 4), DateSerial(2004, _
        8, 30), DateSerial(2004, 9, 13), DateSerial(2004, 9, 20), DateSerial(2004, 9, 27), _
        DateSerial(2004, 10, 4), DateSerial(2004, 10, 4), DateSerial(2004, 10, 25), DateSerial( _
        2004, 11, 1), DateSerial(2004, 10, 18), DateSerial(2004, 11, 8)}
    Dim endDate() As Date = {DateSerial(2004, 8, 30), DateSerial(2004, 10, 18), DateSerial(2004, _
        9, 13), DateSerial(2004, 9, 27), DateSerial(2004, 10, 4), DateSerial(2004, 10, 11), _
        DateSerial(2004, 11, 8), DateSerial(2004, 10, 18), DateSerial(2004, 11, 8), DateSerial( _
        2004, 11, 22), DateSerial(2004, 11, 1), DateSerial(2004, 11, 22)}
    Dim colors() As Integer = {&H00cc00, &H00cc00, &H00cc00, &H0000cc, &H0000cc, &Hcc0000, _
        &Hcc0000, &H0000cc, &Hcc0000, &Hcc0000, &H00cc00, &Hcc0000}

    ' Create a XYChart object of size 620 x 325 pixels. Set background color to light red
    ' (0xffcccc), with 1 pixel 3D border effect.
    Dim c As XYChart = New XYChart(620, 325, &Hffcccc, &H000000, 1)

    ' Add a title to the chart using 15 points Times Bold Itatic font, with white (ffffff) text on a
    ' dark red (800000) background
    c.addTitle("Mutli-Color Gantt Chart Demo", "Times New Roman Bold Italic", 15, &Hffffff _
        ).setBackground(&H800000)

    ' Set the plotarea at (140, 55) and of size 460 x 200 pixels. Use alternative white/grey
    ' background. Enable both horizontal and vertical grids by setting their colors to grey
    ' (c0c0c0). Set vertical major grid (represents month boundaries) 2 pixels in width
    c.setPlotArea(140, 55, 460, 200, &Hffffff, &Heeeeee, Chart.LineColor, &Hc0c0c0, &Hc0c0c0 _
        ).setGridWidth(2, 1, 1, 1)

    ' swap the x and y axes to create a horziontal box-whisker chart
    c.swapXY()

    ' Set the y-axis scale to be date scale from Aug 16, 2004 to Nov 22, 2004, with ticks every 7
    ' days (1 week)
    c.yAxis().setDateScale(DateSerial(2004, 8, 16), DateSerial(2004, 11, 22), 86400 * 7)

    ' Set multi-style axis label formatting. Month labels are in Arial Bold font in "mmm d" format.
    ' Weekly labels just show the day of month and use minor tick (by using '-' as first character
    ' of format string).
    c.yAxis().setMultiFormat(Chart.StartOfMonthFilter(), "<*font=Arial Bold*>{value|mmm d}", _
        Chart.StartOfDayFilter(), "-{value|d}")

    ' Set the y-axis to shown on the top (right + swapXY = top)
    c.setYAxisOnRight()

    ' Set the labels on the x axis
    c.xAxis().setLabels(labels)

    ' Reverse the x-axis scale so that it points downwards.
    c.xAxis().setReverse()

    ' Set the horizontal ticks and grid lines to be between the bars
    c.xAxis().setTickOffset(0.5)

    ' Add some symbols to the chart to represent milestones. The symbols are added using scatter
    ' layers. We need to specify the task index, date, name, symbol shape, size and color.
    c.addScatterLayer(New Double() {1}, Chart.CTime(New Date() {DateSerial(2004, 9, 13)}), _
        "Milestone 1", Chart.Cross2Shape(), 13, &Hffff00).setHTMLImageMap("{disable}")
    c.addScatterLayer(New Double() {3}, Chart.CTime(New Date() {DateSerial(2004, 10, 4)}), _
        "Milestone 2", Chart.StarShape(5), 15, &Hff00ff).setHTMLImageMap("{disable}")
    c.addScatterLayer(New Double() {5}, Chart.CTime(New Date() {DateSerial(2004, 11, 8)}), _
        "Milestone 3", Chart.TriangleSymbol, 13, &Hff9933).setHTMLImageMap("{disable}")

    ' Add a multi-color box-whisker layer to represent the gantt bars
    Dim layer As BoxWhiskerLayer = c.addBoxWhiskerLayer2(Chart.CTime(startDate), _
        Chart.CTime(endDate), Nothing, Nothing, Nothing, colors)
    layer.setXData(taskNo)
    layer.setBorderColor(Chart.SameAsMainColor)

    ' Divide the plot area height ( = 200 in this chart) by the number of tasks to get the height of
    ' each slot. Use 80% of that as the bar height.
    layer.setDataWidth(Int(200 * 4 / 5 / (UBound(labels) + 1)))

    ' Add a legend box at (140, 265) - bottom of the plot area. Use 8pt Arial Bold as the font with
    ' auto-grid layout. Set the width to the same width as the plot area. Set the backgorund to grey
    ' (dddddd).
    Dim legendBox As LegendBox = c.addLegend2(140, 265, Chart.AutoGrid, "Arial Bold", 8)
    legendBox.setWidth(461)
    legendBox.setBackground(&Hdddddd)

    ' The keys for the scatter layers (milestone symbols) will automatically be added to the legend
    ' box. We just need to add keys to show the meanings of the bar colors.
    legendBox.addKey("Market Team", &H00cc00)
    legendBox.addKey("Planning Team", &H0000cc)
    legendBox.addKey("Development Team", &Hcc0000)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{xLabel}: {top|mmm dd, yyyy} to {bottom|mmm dd, yyyy}'")

End Sub

</script>

<html>
<head>
    <title>Multi-Color Gantt Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Multi-Color Gantt Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

