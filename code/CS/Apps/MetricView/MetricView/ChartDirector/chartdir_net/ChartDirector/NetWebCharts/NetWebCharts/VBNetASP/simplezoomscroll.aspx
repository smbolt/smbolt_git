<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<script runat="server">

'
' Initialize the WebChartViewer when the page is first loaded
'
Private Sub initViewer(viewer As WebChartViewer)

    ' The full x-axis range is from Jan 1, 2007 to Jan 1, 2012
    Dim startDate As Date = DateSerial(2010, 1, 1)
    Dim endDate As Date = DateSerial(2015, 1, 1)
    viewer.setFullRange("x", startDate, endDate)

    ' Initialize the view port to show the last 366 days (out of 1826 days)
    viewer.ViewPortWidth = 366.0 / 1826
    viewer.ViewPortLeft = 1 - viewer.ViewPortWidth

    ' Set the maximum zoom to 10 days (out of 1826 days)
    viewer.ZoomInWidthLimit = 10.0 / 1826

End Sub

'
' Create a random table for demo purpose.
'
Private Function getRandomTable() As RanTable

    Dim r As RanTable = New RanTable(127, 4, 1828)
    r.setDateCol(0, DateSerial(2010, 1, 1), 86400)
    r.setCol(1, 150, -10, 10)
    r.setCol(2, 200, -10, 10)
    r.setCol(3, 250, -8, 8)
    Return r

End Function

'
' Draw the chart
'
Private Sub drawChart(viewer As WebChartViewer)

    ' Determine the visible x-axis range
    Dim viewPortStartDate As Date = Chart.NTime(viewer.getValueAtViewPort("x", viewer.ViewPortLeft))
    Dim viewPortEndDate As Date = Chart.NTime(viewer.getValueAtViewPort("x", viewer.ViewPortLeft + _
        viewer.ViewPortWidth))

    ' We need to get the data within the visible x-axis range. In real code, this can be by using a
    ' database query or some other means as specific to the application. In this demo, we just
    ' generate a random data table, and then select the data within the table.
    Dim r As RanTable = getRandomTable()

    ' Select the data for the visible date range viewPortStartDate to viewPortEndDate. It is
    ' possible there is no data point at exactly viewPortStartDate or viewPortEndDate. In this case,
    ' we also need the data points that are just outside the visible date range to "overdraw" the
    ' line a little bit (the "overdrawn" part will be clipped to the plot area) In this demo, we do
    ' this by adding a one day margin to the date range when selecting the data.
    r.selectDate(0, viewPortStartDate.AddDays(-1), viewPortEndDate.AddDays(1))

    ' The selected data from the random data table
    Dim timeStamps() As Date = Chart.NTime(r.getCol(0))
    Dim dataSeriesA() As Double = r.getCol(1)
    Dim dataSeriesB() As Double = r.getCol(2)
    Dim dataSeriesC() As Double = r.getCol(3)

    '
    ' Now we have obtained the data, we can plot the chart.
    '

    '================================================================================
    ' Configure overall chart appearance.
    '================================================================================

    ' Create an XYChart object 600 x 300 pixels in size, with pale blue (f0f0ff) background, black
    ' (000000) rounded border, 1 pixel raised effect.
    Dim c As XYChart = New XYChart(600, 300, &Hf0f0ff, &H000000)
    c.setRoundedFrame()

    ' Set the plotarea at (52, 60) and of size 520 x 205 pixels. Use white (ffffff) background.
    ' Enable both horizontal and vertical grids by setting their colors to grey (cccccc). Set
    ' clipping mode to clip the data lines to the plot area.
    c.setPlotArea(55, 60, 520, 205, &Hffffff, -1, -1, &Hcccccc, &Hcccccc)

    ' As the data can lie outside the plotarea in a zoomed chart, we need to enable clipping.
    c.setClipping()

    ' Add a top title to the chart using 15pt Times New Roman Bold Italic font, with a light blue
    ' (ccccff) background, black (000000) border, and a glass like raised effect.
    c.addTitle("Product Line International Market Price", "Times New Roman Bold Italic", 15 _
        ).setBackground(&Hccccff, &H000000, Chart.glassEffect())

    ' Add a legend box at the top of the plot area with 9pt Arial Bold font with flow layout.
    c.addLegend(50, 33, False, "Arial Bold", 9).setBackground(Chart.Transparent, Chart.Transparent)

    ' Set axes width to 2 pixels
    c.xAxis().setWidth(2)
    c.yAxis().setWidth(2)

    ' Add a title to the y-axis
    c.yAxis().setTitle("Price (USD)", "Arial Bold", 10)

    '================================================================================
    ' Add data to chart
    '================================================================================

    '
    ' In this example, we represent the data by lines. You may modify the code below to use other
    ' representations (areas, scatter plot, etc).
    '

    ' Add a line layer for the lines, using a line width of 2 pixels
    Dim layer As LineLayer = c.addLineLayer2()
    layer.setLineWidth(2)

    ' In this demo, we do not have too many data points. In real code, the chart may contain a lot
    ' of data points when fully zoomed out - much more than the number of horizontal pixels in this
    ' plot area. So it is a good idea to use fast line mode.
    layer.setFastLineMode()

    ' Now we add the 3 data series to a line layer, using the color red (ff0000), green (00cc00) and
    ' blue (0000ff)
    layer.setXData(timeStamps)
    layer.addDataSet(dataSeriesA, &Hff0000, "Product Alpha")
    layer.addDataSet(dataSeriesB, &H00cc00, "Product Beta")
    layer.addDataSet(dataSeriesC, &H0000ff, "Product Gamma")

    '================================================================================
    ' Configure axis scale and labelling
    '================================================================================

    ' Set the x-axis as a date/time axis with the scale according to the view port x range.
    viewer.syncDateAxisWithViewPort("x", c.xAxis())

    ' In this demo, we rely on ChartDirector to auto-label the axis. We ask ChartDirector to ensure
    ' the x-axis labels are at least 75 pixels apart to avoid too many labels.
    c.xAxis().setTickDensity(75)

    '================================================================================
    ' Output the chart
    '================================================================================

    ' Output the chart
    viewer.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='[{dataSetName}] {x|mmm dd, yyyy}: USD {value|2}'")

End Sub

'
' Page Load event handler
'
Private Sub Page_Load(sender As System.Object, e As System.EventArgs)

    '
    ' This script handles both the full page request, as well as the subsequent partial updates
    ' (AJAX chart updates). We need to determine the type of request first before we processing it.
    '
    If WebChartViewer.IsPartialUpdateRequest(Page) Then
        ' Is a partial update request.

        ' The .NET platform will not restore the states of the controls before or during Page_Load,
        ' so we need to restore the state ourselves
        WebChartViewer1.LoadViewerState()

        ' Draw the chart in partial update mode
        drawChart(WebChartViewer1)

        ' Output the chart immediately and then terminate the page life cycle (PartialUpdateChart
        ' will cause Page_Load to terminate immediately without running the following code).
        WebChartViewer1.PartialUpdateChart()
    End If

    '
    ' If the code reaches here, it is a full page request.
    '

    ' In this exapmle, we just need to initialize the WebChartViewer and draw the chart.
    initViewer(WebChartViewer1)
    drawChart(WebChartViewer1)

End Sub

</script>

<!DOCTYPE html>
<html>
<head>
    <title>Simple Zooming and Scrolling</title>
    <script type="text/javascript" src="cdjcv.js"></script>
</head>
<body style="margin:0px;">
<script type="text/javascript">

//
// Execute the following initialization code after the web page is loaded
//
JsChartViewer.addEventListener(window, 'load', function() {
    // Update the chart when the view port has changed (eg. when the user zooms in using the mouse)
    var viewer = JsChartViewer.get('<%=WebChartViewer1.ClientID%>');
    viewer.attachHandler("ViewPortChanged", viewer.partialUpdate);

    // Set the initial mouse usage to "scroll"
    viewer.setMouseUsage(JsChartViewer.Scroll);
    document.getElementById("scrollChart").checked = true;
});

</script>
<form method="post" id="SimpleZoomScroll" runat="server">
<table cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td align="right" colspan="2" style="background:#000088">
            <div style="padding:0px 3px 2px 0px; font:italic bold 10pt Arial;">
                <a style="color:#FFFF00; text-decoration:none" href="http://www.advsofteng.com/">Advanced Software Engineering</a>
            </div>
        </td>
    </tr>
    <tr valign="top">
        <td style="width:127px; background:#c0c0ff; border-right:black 1px solid; border-bottom:black 1px solid;">
        <div style="font:9pt Verdana; padding:10px 0px 0px 3px; line-height:1.5; width:127px">
            <!-- The onclick handler of the following radio buttons sets the mouse usage mode. -->
            <input name="mouseUsage" id="scrollChart" type="radio"
                onclick="JsChartViewer.get('<%=WebChartViewer1.ClientID%>').setMouseUsage(JsChartViewer.Scroll)" />
            Drag To Scroll<br />
            <input name="mouseUsage" id="zoomInChart" type="radio"
                onclick="JsChartViewer.get('<%=WebChartViewer1.ClientID%>').setMouseUsage(JsChartViewer.ZoomIn)" />
            Zoom In<br />
            <input name="mouseUsage" id="zoomOutChart" type="radio"
                onclick="JsChartViewer.get('<%=WebChartViewer1.ClientID%>').setMouseUsage(JsChartViewer.ZoomOut)" />
            Zoom Out<br />
        </div>
        </td>
        <td>
            <div style="font-weight:bold; font-size:20pt; margin:5px 0px 0px 5px; font-family:Arial">
                Simple Zooming and Scrolling
            </div>
            <hr style="border:solid 1px #000080" />
            <div style="padding:0px 5px 5px 10px">
                <chart:WebChartViewer id="WebChartViewer1" runat="server" width="600px" height="300px" />
            </div>
        </td>
    </tr>
</table>
</form>
</body>
</html>
