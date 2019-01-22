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

    ' Create a LinearMeter object of size 250 x 75 pixels with very light grey (0xeeeeee) backgruond
    ' and a light grey (0xccccccc) 3-pixel thick rounded frame
    Dim m As LinearMeter = New LinearMeter(250, 75, &Heeeeee, &Hcccccc)
    m.setRoundedFrame(Chart.Transparent)
    m.setThickFrame(3)

    ' This example demonstrates putting the text labels at the top or bottom. This is by setting the
    ' label alignment, scale position and label position.
    Dim alignment() As Integer = {Chart.Top, Chart.Top, Chart.Bottom, Chart.Bottom}
    Dim meterYPos() As Integer = {23, 23, 34, 34}
    Dim labelYPos() As Integer = {61, 61, 15, 15}

    ' Set the scale region
    m.setMeter(14, meterYPos(chartIndex), 218, 20, alignment(chartIndex))

    ' Set meter scale from 0 - 100, with a tick every 10 units
    m.setScale(0, 100, 10)

    ' Add a smooth color scale at the default position
    Dim smoothColorScale() As Double = {0, &H6666ff, 25, &H00bbbb, 50, &H00ff00, 75, &Hffff00, _
        100, &Hff0000}
    m.addColorScale(smoothColorScale)

    ' Add a blue (0x0000cc) pointer at the specified value
    m.addPointer(value, &H0000cc)

    '
    ' In this example, some charts have the "Temperauture" label on the left side and the value
    ' readout on the right side, and some charts have the reverse
    '

    If chartIndex Mod 2 = 0 Then
        ' Add a label on the left side using 8pt Arial Bold font
        m.addText(10, labelYPos(chartIndex), "Temperature C", "Arial Bold", 8, Chart.TextColor, _
            Chart.Left)

        ' Add a text box on the right side. Display the value using white (0xffffff) 8pt Arial Bold
        ' font on a black (0x000000) background with depressed rounded border.
        Dim t As ChartDirector.TextBox = m.addText(235, labelYPos(chartIndex), m.formatValue( _
            value, "2"), "Arial Bold", 8, &Hffffff, Chart.Right)
        t.setBackground(&H000000, &H000000, -1)
        t.setRoundedCorners(3)
    Else
        ' Add a label on the right side using 8pt Arial Bold font
        m.addText(237, labelYPos(chartIndex), "Temperature C", "Arial Bold", 8, Chart.TextColor, _
            Chart.Right)

        ' Add a text box on the left side. Display the value using white (0xffffff) 8pt Arial Bold
        ' font on a black (0x000000) background with depressed rounded border.
        Dim t As ChartDirector.TextBox = m.addText(11, labelYPos(chartIndex), m.formatValue(value, _
            "2"), "Arial Bold", 8, &Hffffff, Chart.Left)
        t.setBackground(&H000000, &H000000, -1)
        t.setRoundedCorners(3)
    End If

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
    <title>H-Linear Meter Orientation</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        H-Linear Meter Orientation
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer0" runat="server" />
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
    <chart:WebChartViewer id="WebChartViewer2" runat="server" />
    <chart:WebChartViewer id="WebChartViewer3" runat="server" />
</body>
</html>

