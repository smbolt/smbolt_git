<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<script runat="server">

'
' Initialize the WebChartViewer when the page is first loaded
'
Private Sub initViewer(viewer As WebChartViewer)

    '
    ' This example assumes the initial chart is the full chart and we can auto-detect the full data
    ' range in the drawChart code. So we do not need to configure the full range here.
    '

End Sub

'
' Draw the main chart
'
Private Sub drawChart(viewer As WebChartViewer)

    '
    ' For simplicity, in this demo, the data arrays are filled with hard coded data. In a real
    ' application, you may use a database or other data source to load up the arrays, and only
    ' visible data (data within the view port) need to be loaded.
    '
    Dim dataX0() As Double = {10, 15, 6, -12, 14, -8, 13, -3, 16, 12, 10.5, -7, 3, -10, -5, 2, 5}
    Dim dataY0() As Double = {130, 150, 80, 110, -110, -105, -130, -15, -170, 125, 125, 60, 25, _
        150, 150, 15, 120}
    Dim dataX1() As Double = {6, 7, -4, 3.5, 7, 8, -9, -10, -12, 11, 8, -3, -2, 8, 4, -15, 15}
    Dim dataY1() As Double = {65, -40, -40, 45, -70, -80, 80, 10, -100, 105, 60, 50, 20, 170, -25, _
        50, 75}
    Dim dataX2() As Double = {-10, -12, 11, 8, 6, 12, -4, 3.5, 7, 8, -9, 3, -13, 16, -7.5, -10, _
        -15}
    Dim dataY2() As Double = {65, -80, -40, 45, -70, -80, 80, 90, -100, 105, 60, -75, -150, -40, _
        120, -50, -30}

    ' Create an XYChart object 500 x 480 pixels in size, with light blue (c0c0ff) background
    Dim c As XYChart = New XYChart(500, 480, &Hc0c0ff)

    ' Set the plotarea at (50, 40) and of size 400 x 400 pixels. Use light grey (c0c0c0) horizontal
    ' and vertical grid lines. Set 4 quadrant coloring, where the colors alternate between lighter
    ' and deeper grey (dddddd/eeeeee).
    c.setPlotArea(50, 40, 400, 400, -1, -1, -1, &Hc0c0c0, &Hc0c0c0).set4QBgColor(&Hdddddd, _
        &Heeeeee, &Hdddddd, &Heeeeee, &H000000)

    ' As the data can lie outside the plotarea in a zoomed chart, we need enable clipping
    c.setClipping()

    ' Set 4 quadrant mode, with both x and y axes symetrical around the origin
    c.setAxisAtOrigin(Chart.XYAxisAtOrigin, Chart.XAxisSymmetric + Chart.YAxisSymmetric)

    ' Add a legend box at (450, 40) (top right corner of the chart) with vertical layout and 8pt
    ' Arial Bold font. Set the background color to semi-transparent grey (40dddddd).
    Dim legendBox As LegendBox = c.addLegend(450, 40, True, "Arial Bold", 8)
    legendBox.setAlignment(Chart.TopRight)
    legendBox.setBackground(&H40dddddd)

    ' Add titles to axes
    c.xAxis().setTitle("Alpha Index")
    c.yAxis().setTitle("Beta Index")

    ' Set axes line width to 2 pixels
    c.xAxis().setWidth(2)
    c.yAxis().setWidth(2)

    ' The default ChartDirector settings has a denser y-axis grid spacing and less-dense x-axis grid
    ' spacing. In this demo, we want the tick spacing to be symmetrical. We use around 40 pixels
    ' between major ticks and 20 pixels between minor ticks.
    c.xAxis().setTickDensity(40, 20)
    c.yAxis().setTickDensity(40, 20)

    '
    ' In this example, we represent the data by scatter points. You may modify the code below to use
    ' other layer types (lines, areas, etc).
    '

    ' Add scatter layer, using 11 pixels red (ff33333) X shape symbols
    c.addScatterLayer(dataX0, dataY0, "Group A", Chart.Cross2Shape(), 11, &Hff3333)

    ' Add scatter layer, using 11 pixels green (33ff33) circle symbols
    c.addScatterLayer(dataX1, dataY1, "Group B", Chart.CircleShape, 11, &H33ff33)

    ' Add scatter layer, using 11 pixels blue (3333ff) triangle symbols
    c.addScatterLayer(dataX2, dataY2, "Group C", Chart.TriangleSymbol, 11, &H3333ff)

    '
    ' In this example, we have not explicitly configured the full x and y range. In this case, the
    ' first time syncLinearAxisWithViewPort is called, ChartDirector will auto-scale the axis and
    ' assume the resulting range is the full range. In subsequent calls, ChartDirector will set the
    ' axis range based on the view port and the full range.
    '
    viewer.syncLinearAxisWithViewPort("x", c.xAxis())
    viewer.syncLinearAxisWithViewPort("y", c.yAxis())

    ' Output the chart
    viewer.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='[{dataSetName}] Alpha = {x|G}, Beta = {value|G}'")

End Sub

'
' Draw the thumbnail chart in the WebViewPortControl
'
Private Sub drawFullChart(vp As WebViewPortControl, viewer As WebChartViewer)

    '
    ' For simplicity, in this demo, the data arrays are filled with hard coded data. In a real
    ' application, you may use a database or other data source to load up the arrays. As this is a
    ' small thumbnail chart, complete data may not be needed. For exmaple, if there are a million
    ' points, a random sample may already be sufficient for the thumbnail chart.
    '
    Dim dataX0() As Double = {10, 15, 6, -12, 14, -8, 13, -3, 16, 12, 10.5, -7, 3, -10, -5, 2, 5}
    Dim dataY0() As Double = {130, 150, 80, 110, -110, -105, -130, -15, -170, 125, 125, 60, 25, _
        150, 150, 15, 120}
    Dim dataX1() As Double = {6, 7, -4, 3.5, 7, 8, -9, -10, -12, 11, 8, -3, -2, 8, 4, -15, 15}
    Dim dataY1() As Double = {65, -40, -40, 45, -70, -80, 80, 10, -100, 105, 60, 50, 20, 170, -25, _
        50, 75}
    Dim dataX2() As Double = {-10, -12, 11, 8, 6, 12, -4, 3.5, 7, 8, -9, 3, -13, 16, -7.5, -10, _
        -15}
    Dim dataY2() As Double = {65, -80, -40, 45, -70, -80, 80, 90, -100, 105, 60, -75, -150, -40, _
        120, -50, -30}

    ' Create an XYChart object 120 x 120 pixels in size
    Dim c As XYChart = New XYChart(120, 120)

    ' Set the plotarea to cover the entire chart and with no grid lines. Set 4 quadrant coloring,
    ' where the colors alternate between lighter and deeper grey (d8d8d8/eeeeee).
    c.setPlotArea(0, 0, c.getWidth() - 1, c.getHeight() - 1, -1, -1, -1, Chart.Transparent _
        ).set4QBgColor(&Hd8d8d8, &Heeeeee, &Hd8d8d8, &Heeeeee, &H000000)

    ' Set 4 quadrant mode, with both x and y axes symetrical around the origin
    c.setAxisAtOrigin(Chart.XYAxisAtOrigin, Chart.XAxisSymmetric + Chart.YAxisSymmetric)

    ' The x and y axis scales reflect the full range of the view port
    c.xAxis().setLinearScale(viewer.getValueAtViewPort("x", 0), viewer.getValueAtViewPort("x", 1), _
        Chart.NoValue)
    c.yAxis().setLinearScale(viewer.getValueAtViewPort("y", 0), viewer.getValueAtViewPort("y", 1), _
        Chart.NoValue)

    ' Add scatter layer, using 5 pixels red (ff33333) X shape symbols
    c.addScatterLayer(dataX0, dataY0, "Group A", Chart.Cross2Shape(), 5, &Hff3333)

    ' Add scatter layer, using 5 pixels green (33ff33) circle symbols
    c.addScatterLayer(dataX1, dataY1, "Group B", Chart.CircleShape, 5, &H33ff33)

    ' Add scatter layer, using 5 pixels blue (3333ff) triangle symbols
    c.addScatterLayer(dataX2, dataY2, "Group C", Chart.TriangleSymbol, 5, &H3333ff)

    ' Output the chart
    vp.Image = c.makeWebImage(Chart.PNG)

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

    ' Initialize the WebChartViewer and draw the chart.
    initViewer(WebChartViewer1)
    drawChart(WebChartViewer1)

    ' Draw a thumbnail chart representing the full range in the WebViewPortControl
    drawFullChart(WebViewPortControl1, WebChartViewer1)

End Sub

</script>

<!DOCTYPE html>
<html>
<head>
    <title>XY Zooming and Scrolling</title>
    <script type="text/javascript" src="cdjcv.js"></script>
    <style type="text/css">
        .chartButton { font:12px Verdana; border-bottom:#000000 1px solid; padding:5px; cursor:pointer;}
        .chartButtonSpacer { font:12px Verdana; border-bottom:#000000 1px solid; padding:5px;}
        .chartButton:hover { box-shadow:inset 0px 0px 0px 2px #444488; }
        .chartButtonPressed { background-color: #CCFFCC; }
    </style>
</head>
<body style="margin:0px">
<script type="text/javascript">

//
// Execute the following initialization code after the web page is loaded
//
JsChartViewer.addEventListener(window, 'load', function() {
    // Update the chart when the view port has changed (eg. when the user zooms in using the mouse)
    var viewer = JsChartViewer.get('<%=WebChartViewer1.ClientID%>');
    viewer.attachHandler("ViewPortChanged", viewer.partialUpdate);

    // Set the zoom and scroll mode to bi-directional
    viewer.setScrollDirection(JsChartViewer.HorizontalVertical);
    viewer.setZoomDirection(JsChartViewer.HorizontalVertical);

    // Set the initial mouse usage to "zoom in"
    setMouseMode(JsChartViewer.ZoomIn);

    // Initialize the view port control
    var viewPortCtrl = JsViewPortControl.get('<%=WebViewPortControl1.ClientID%>');
    // Set the mask color to semi-transparent black
    viewPortCtrl.setViewPortExternalColor("#80000000");
    // Set the selection rectangle border to white
    viewPortCtrl.setSelectionBorderStyle("1px solid white");
    // Bind the view port control to the chart viewer
    viewPortCtrl.setViewer(viewer);
});

//
// This method is called when the user clicks on the Pointer, Zoom In or Zoom Out buttons
//
function setMouseMode(mode)
{
    var viewer = JsChartViewer.get('<%=WebChartViewer1.ClientID%>');
    if (mode == viewer.getMouseUsage())
        mode = JsChartViewer.Default;

    // Set the button color based on the selected mouse mode
    document.getElementById("scrollButton").className = "chartButton" +
        ((mode  == JsChartViewer.Scroll) ? " chartButtonPressed" : "");
    document.getElementById("zoomInButton").className = "chartButton" +
        ((mode  == JsChartViewer.ZoomIn) ? " chartButtonPressed" : "");
    document.getElementById("zoomOutButton").className = "chartButton" +
        ((mode  == JsChartViewer.ZoomOut) ? " chartButtonPressed" : "");

    // Set the mouse mode
    viewer.setMouseUsage(mode);
}

</script>
<form method="post" id="XYZoomScroll" runat="server">
<table cellspacing="0" cellpadding="0" style="border:black 1px solid;">
    <tr>
        <td align="right" colspan="2" style="background:#000088; color:#ffff00; padding:0px 4px 2px 0px;">
            <a style="color:#FFFF00; font:italic bold 10pt Arial; text-decoration:none" href="http://www.advsofteng.com/">
                Advanced Software Engineering
            </a>
        </td>
    </tr>
    <tr valign="top">
        <td style="width:130px; background:#e0e0e0;">
            <!-- The following table is to create 3 cells for 3 buttons to control the mouse usage mode. -->
            <table style="width:100%; padding:0px; border:0px; border-spacing:0px;">
                <tr>
                    <td class="chartButton" id="scrollButton" onclick="setMouseMode(JsChartViewer.Scroll)"
                        ontouchstart="this.onclick(event); event.preventDefault();">
                        <img src="scrollnesw.gif" style="vertical-align:middle" alt="Drag" />&nbsp;&nbsp;Drag to Scroll
                    </td>
                </tr>
                <tr>
                    <td class="chartButton" id="zoomInButton" onclick="setMouseMode(JsChartViewer.ZoomIn)"
                        ontouchstart="this.onclick(event); event.preventDefault();">
                        <img src="zoomInIcon.gif" style="vertical-align:middle" alt="Zoom In" />&nbsp;&nbsp;Zoom In
                    </td>
                </tr>
                <tr>
                    <td class="chartButton" id="zoomOutButton" onclick="setMouseMode(JsChartViewer.ZoomOut)"
                        ontouchstart="this.onclick(event); event.preventDefault();">
                        <img src="zoomOutIcon.gif" style="vertical-align:middle" alt="Zoom Out" />&nbsp;&nbsp;Zoom Out
                    </td>
                </tr>
            </table>
            <br /><br /><br /><br /><br />
            <div style="text-align:center;">
                <chart:WebViewPortControl id="WebViewPortControl1" runat="server" width="120px" height="120px" />
            </div>
        </td>
        <td style="border-left: black 1px solid; background-color: #c0c0ff; padding:5px">
            <chart:WebChartViewer id="WebChartViewer1" runat="server" width="500px" height="480px" />
        </td>
    </tr>
</table>
</form>
</body>
</html>
