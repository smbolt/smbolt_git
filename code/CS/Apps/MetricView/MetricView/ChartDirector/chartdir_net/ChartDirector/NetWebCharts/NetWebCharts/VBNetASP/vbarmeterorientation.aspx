<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Create chart
'
Protected Sub createChart(viewer As WebChartViewer, chartIndex As Integer)

    ' The value to display on the meter
    Dim value As Double = 74.25

    ' Bar colors of the meters
    Dim barColor() As Integer = {&H2299ff, &H00ee00, &Haa66ee, &Hff7711}

    ' Create a LinearMeter object of size 70 x 240 pixels with very light grey (0xeeeeee) backgruond
    ' and a grey (0xaaaaaa) 3-pixel thick rounded frame
    Dim m As LinearMeter = New LinearMeter(70, 240, &Heeeeee, &Haaaaaa)
    m.setRoundedFrame(Chart.Transparent)
    m.setThickFrame(3)

    ' This example demonstrates putting the text labels at the left or right side of the meter
    ' scale, and putting the color scale on the same side as the labels or on opposite side.
    Dim alignment() As Integer = {Chart.Left, Chart.Left, Chart.Right, Chart.Right}
    Dim meterXPos() As Integer = {28, 38, 12, 21}
    Dim labelGap() As Integer = {2, 12, 10, 2}
    Dim colorScalePos() As Integer = {53, 28, 36, 10}

    ' Configure the position of the meter scale and which side to put the text labels
    m.setMeter(meterXPos(chartIndex), 18, 20, 205, alignment(chartIndex))

    ' Set meter scale from 0 - 100, with a tick every 10 units
    m.setScale(0, 100, 10)

    ' To put the color scale on the same side as the text labels, we need to increase the gap
    ' between the labels and the meter scale to make room for the color scale
    m.setLabelPos(False, labelGap(chartIndex))

    ' Add a smooth color scale to the meter
    Dim smoothColorScale() As Double = {0, &H0000ff, 25, &H0088ff, 50, &H00ff00, 75, &Hdddd00, _
        100, &Hff0000}
    m.addColorScale(smoothColorScale, colorScalePos(chartIndex), 6)

    ' Add a bar from 0 to value with glass effect and 4 pixel rounded corners
    m.addBar(0, value, barColor(chartIndex), Chart.glassEffect(Chart.NormalGlare, Chart.Left), 4)

    ' Output the chart
    viewer.Image = m.makeWebImage(Chart.PNG)

End Sub

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    createChart(WebChartViewer0, 0)
    createChart(WebChartViewer1, 1)
    createChart(WebChartViewer2, 2)
    createChart(WebChartViewer3, 3)

End Sub

</script>

<html>
<head>
    <title>V-Bar Meter Orientation</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        V-Bar Meter Orientation
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer0" runat="server" style='margin-right:25px;' />
    <chart:WebChartViewer id="WebChartViewer1" runat="server" style='margin-right:25px;' />
    <chart:WebChartViewer id="WebChartViewer2" runat="server" style='margin-right:25px;' />
    <chart:WebChartViewer id="WebChartViewer3" runat="server" />
</body>
</html>

