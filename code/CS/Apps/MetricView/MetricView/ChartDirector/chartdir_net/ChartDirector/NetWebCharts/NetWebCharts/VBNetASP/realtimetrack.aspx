<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<script runat="server">

'
' Draw the chart
'
Private Sub drawChart(viewer As WebChartViewer)

    '
    ' Data to draw the chart. In this demo, the data buffer will be filled by a random data
    ' generator. In real life, the data is probably stored in a buffer (eg. a database table, a text
    ' file, or some global memory) and updated by other means.
    '

    ' We use a data buffer to emulate the last 240 samples.
    Dim sampleSize As Integer = 240
    Dim dataSeries1(sampleSize - 1) As Double
    Dim dataSeries2(sampleSize - 1) As Double
    Dim dataSeries3(sampleSize - 1) As Double
    Dim timeStamps(sampleSize - 1) As Date

    ' Our pseudo random number generator
    Dim firstDate As Date = DateTime.Now.AddSeconds(-timeStamps.Length)
    For i As Integer = 0 To UBound(timeStamps)
        timeStamps(i) = firstDate.AddSeconds(i)
        Dim p As Double = timeStamps(i).Ticks / 10000000
        dataSeries1(i) = Math.Cos(p * 2.1) * 10 + 1 / (Math.Cos(p) * Math.Cos(p) + 0.01) + 20
        dataSeries2(i) = 100 * Math.Sin(p / 27.7) * Math.Sin(p / 10.1) + 150
        dataSeries3(i) = 100 * Math.Cos(p / 6.7) * Math.Cos(p / 11.9) + 150
    Next

    ' Create an XYChart object 600 x 270 pixels in size, with light grey (f4f4f4) background, black
    ' (000000) border, 1 pixel raised effect, and with a rounded frame.
    Dim c As XYChart = New XYChart(600, 270, &Hf4f4f4, &H000000, 0)
    c.setRoundedFrame()

    ' Set the plotarea at (55, 57) and of size 520 x 185 pixels. Use white (ffffff) background.
    ' Enable both horizontal and vertical grids by setting their colors to grey (cccccc). Set
    ' clipping mode to clip the data lines to the plot area.
    c.setPlotArea(55, 57, 520, 185, &Hffffff, -1, -1, &Hcccccc, &Hcccccc)
    c.setClipping()

    ' Add a title to the chart using 15pt Times New Roman Bold Italic font, with a light grey
    ' (dddddd) background, black (000000) border, and a glass like raised effect.
    c.addTitle("Field Intensity at Observation Satellite", "Times New Roman Bold Italic", 15 _
        ).setBackground(&Hdddddd, &H000000, Chart.glassEffect())

    ' Configure the y-axis with a 10pt Arial Bold axis title
    c.yAxis().setTitle("Intensity (V/m)", "Arial Bold", 10)

    ' Configure the x-axis to auto-scale with at least 75 pixels between major tick and 15 pixels
    ' between minor ticks. This shows more minor grid lines on the chart.
    c.xAxis().setTickDensity(75, 15)

    ' Set the axes width to 2 pixels
    c.xAxis().setWidth(2)
    c.yAxis().setWidth(2)

    ' Set the x-axis label format
    c.xAxis().setLabelFormat("{value|hh:nn:ss}")

    ' Create a line layer to plot the lines
    Dim layer As LineLayer = c.addLineLayer2()

    ' The x-coordinates are the timeStamps.
    layer.setXData(timeStamps)

    ' The 3 data series are used to draw 3 lines. Here we put the latest data values as part of the
    ' data set name, so you can see them updated in the legend box.
    layer.addDataSet(dataSeries1, &Hff0000, "Alpha")
    layer.addDataSet(dataSeries2, &H00cc00, "Beta")
    layer.addDataSet(dataSeries3, &H0000ff, "Gamma")

    ' Output the chart
    viewer.Image = c.makeWebImage(Chart.PNG)

    ' Output Javascript chart model to the browser to suppport tracking cursor
    WebChartViewer1.ChartModel = c.getJsChartModel()

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

    drawChart(WebChartViewer1)

End Sub

</script>

<!DOCTYPE html>
<html>
<head>
    <title>Realtime Chart with Track Line</title>
    <script type="text/javascript" src="cdjcv.js"></script>
</head>
<body style="margin:0px">
<script type="text/javascript">

//
// Execute the following initialization code after the web page is loaded
//
JsChartViewer.addEventListener(window, 'load', function() {
    var viewer = JsChartViewer.get('<%=WebChartViewer1.ClientID%>');

    // Draw track cursor when mouse is moving over plotarea or if the chart is updated. In the latter case,
    // if the mouse is not on the plot area, we will update the legend to show the latest data values.
    viewer.attachHandler(["MouseMovePlotArea", "TouchStartPlotArea", "TouchMovePlotArea", "ChartMove",
        "PostUpdate", "Now"], function(e) {
        this.preventDefault(e);   // Prevent the browser from using touch events for other actions
        trackLineLegend(viewer, viewer.getPlotAreaMouseX());
    });

    // When the chart is being updated, by default, an "Updating" box will pop up. In this example, we
    // will disable this box.
    viewer.updatingMsg = "";
});

//
// Draw track line with legend
//
function trackLineLegend(viewer, mouseX)
{
    // Remove all previously drawn tracking object
    viewer.hideObj("all");

    // The chart and its plot area
    var c = viewer.getChart();
    var plotArea = c.getPlotArea();

    // Get the data x-value that is nearest to the mouse, and find its pixel coordinate.
    var xValue = c.getNearestXValue(mouseX);
    var xCoor = c.getXCoor(xValue);
    if (xCoor == null)
        return;

    // Draw a vertical track line at the x-position
    viewer.drawVLine("trackLine", xCoor, plotArea.getTopY(), plotArea.getBottomY(), "black 1px dotted");

    // Array to hold the legend entries
    var legendEntries = [];

    // Iterate through all layers to build the legend array
    for (var i = 0; i < c.getLayerCount(); ++i)
    {
        var layer = c.getLayerByZ(i);

        // The data array index of the x-value
        var xIndex = layer.getXIndexOf(xValue);

        // Iterate through all the data sets in the layer
        for (var j = 0; j < layer.getDataSetCount(); ++j)
        {
            var dataSet = layer.getDataSetByZ(j);

            // We are only interested in visible data sets with names, as they are required for legend entries.
            var dataName = dataSet.getDataName();
            var color = dataSet.getDataColor();
            if ((!dataName) || (color == null))
                continue;

            // Build the legend entry, consist of a colored square box, the name and the data value.
            var dataValue = dataSet.getValue(xIndex);
            legendEntries.push("<nobr>" + viewer.htmlRect(7, 7, color) + " " + dataName + ": " +
                ((dataValue == null) ? "N/A" : dataValue.toPrecision(4)) + viewer.htmlRect(20, 0) + "</nobr> ");

            // Draw a track dot for data points within the plot area
            var yCoor = c.getYCoor(dataSet.getPosition(xIndex), dataSet.getUseYAxis());
            if ((yCoor != null) && (yCoor >= plotArea.getTopY()) && (yCoor <= plotArea.getBottomY()))
            {
                viewer.showTextBox("dataPoint" + i + "_" + j, xCoor, yCoor, JsChartViewer.Center,
                    viewer.htmlRect(7, 7, color));
            }
        }
    }

    // Create the legend by joining the legend entries.
    var legend = "<nobr>[" + c.xAxis().getFormattedLabel(xValue, "hh:nn:ss") + "]" + viewer.htmlRect(20, 0) +
        "</nobr> " + legendEntries.reverse().join("");

    // Display the legend on the top of the plot area
    viewer.showTextBox("legend", plotArea.getLeftX(), plotArea.getTopY(), JsChartViewer.BottomLeft, legend,
        "width:" + plotArea.getWidth() + "px;font:bold 11px Arial;padding:3px;-webkit-text-size-adjust:100%;");
}

//
// Executes once every second to update the countdown display. Updates the chart when the countdown reaches 0.
//
function timerTick()
{
    // Get the update period and the time left
    var updatePeriod = parseInt(document.getElementById("UpdatePeriod").value);
    var timeLeft = Math.min(parseInt(document.getElementById("TimeRemaining").innerHTML), updatePeriod) - 1;

    if (timeLeft == 0)
        // Can update the chart now
        JsChartViewer.get('<%=WebChartViewer1.ClientID%>').partialUpdate();
    else if (timeLeft < 0)
        // Reset the update period
        timeLeft += updatePeriod;

    // Update the countdown display
    document.getElementById("TimeRemaining").innerHTML = timeLeft;
}
window.setInterval("timerTick()", 1000);

</script>
<table cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td align="right" colspan="2" style="background:#000088; color:#ffff00; padding:0px 4px 2px 0px;">
            <a style="color:#FFFF00; font:italic bold 10pt Arial; text-decoration:none" href="http://www.advsofteng.com/">
                Advanced Software Engineering
            </a>
        </td>
    </tr>
    <tr valign="top">
        <td style="width:150px; background:#c0c0ff; border-right:black 1px solid; border-bottom:black 1px solid;">
            <br />
            <br />
            <div style="font: 9pt Verdana; padding:10px;">
                <b>Update Period</b><br />
                <select id="UpdatePeriod" style="width:130px">
                    <option value="5">5</option>
                    <option value="10" selected="selected">10</option>
                    <option value="20">20</option>
                    <option value="30">30</option>
                    <option value="60">60</option>
                </select>
            </div>
            <div style="font:9pt Verdana; padding:10px;">
                <b>Time Remaining</b><br />
                <div style="width:128px; border:#888888 1px inset;">
                    <div style="margin:3px" id="TimeRemaining">0</div>
                </div>
            </div>
        </td>
        <td>
            <div style="font: bold 20pt Arial; margin:5px 0px 0px 5px;">
                Realtime Chart with Track Line
            </div>
            <hr style="border:solid 1px #000080" />
            <div style="padding:0px 5px 5px 10px">
                <chart:WebChartViewer id="WebChartViewer1" runat="server" width="600px" height="270px" />
            </div>
        </td>
    </tr>
</table>
</body>
</html>
