<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' data for the gantt chart, representing the start date, end date and names for various
    ' activities
    Dim startDate() As Date = {DateSerial(2004, 8, 16), DateSerial(2004, 8, 30), DateSerial(2004, _
        9, 13), DateSerial(2004, 9, 20), DateSerial(2004, 9, 27), DateSerial(2004, 10, 4), _
        DateSerial(2004, 10, 25), DateSerial(2004, 11, 1), DateSerial(2004, 11, 8)}
    Dim endDate() As Date = {DateSerial(2004, 8, 30), DateSerial(2004, 9, 13), DateSerial(2004, 9, _
        27), DateSerial(2004, 10, 4), DateSerial(2004, 10, 11), DateSerial(2004, 11, 8), _
        DateSerial(2004, 11, 8), DateSerial(2004, 11, 22), DateSerial(2004, 11, 22)}
    Dim labels() As String = {"Market Research", "Define Specifications", "Overall Archiecture", _
        "Project Planning", "Detail Design", "Software Development", "Test Plan", _
        "Testing and QA", "User Documentation"}

    ' Create a XYChart object of size 620 x 280 pixels. Set background color to light blue (ccccff),
    ' with 1 pixel 3D border effect.
    Dim c As XYChart = New XYChart(620, 280, &Hccccff, &H000000, 1)

    ' Add a title to the chart using 15 points Times Bold Itatic font, with white (ffffff) text on a
    ' deep blue (000080) background
    c.addTitle("Simple Gantt Chart Demo", "Times New Roman Bold Italic", 15, &Hffffff _
        ).setBackground(&H000080)

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

    ' Add a green (33ff33) box-whisker layer showing the box only.
    c.addBoxWhiskerLayer(Chart.CTime(startDate), Chart.CTime(endDate), Nothing, Nothing, Nothing, _
        &H00cc00, Chart.SameAsMainColor, Chart.SameAsMainColor)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{xLabel}: {top|mmm dd, yyyy} to {bottom|mmm dd, yyyy}'")

End Sub

</script>

<html>
<head>
    <title>Simple Gantt Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Simple Gantt Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

