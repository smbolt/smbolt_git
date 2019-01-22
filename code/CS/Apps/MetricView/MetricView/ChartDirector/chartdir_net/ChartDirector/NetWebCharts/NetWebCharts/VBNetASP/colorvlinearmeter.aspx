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

    ' The background and border colors of the meters
    Dim bgColor() As Integer = {&Hbbddff, &Hccffcc, &Hffccff, &Hffffaa, &Hffdddd, &Heeeeee}
    Dim borderColor() As Integer = {&H000088, &H006600, &H6600aa, &Hee6600, &H880000, &H666666}

    ' Create a LinearMeter object of size 70 x 260 pixels with a 3-pixel thick rounded frame
    Dim m As LinearMeter = New LinearMeter(70, 260, bgColor(chartIndex), borderColor(chartIndex))
    m.setRoundedFrame(Chart.Transparent)
    m.setThickFrame(3)

    ' Set the scale region top-left corner at (28, 30), with size of 20 x 200 pixels. The scale
    ' labels are located on the left (default - implies vertical meter)
    m.setMeter(28, 30, 20, 200)

    ' Set meter scale from 0 - 100, with a tick every 10 units
    m.setScale(0, 100, 10)

    ' Demostrate different types of color scales and putting them at different positions
    Dim smoothColorScale() As Double = {0, &H6666ff, 25, &H00bbbb, 50, &H00ff00, 75, &Hffff00, _
        100, &Hff0000}
    Dim stepColorScale() As Double = {0, &H33ff33, 50, &Hffff33, 80, &Hff3333, 100}
    Dim highLowColorScale() As Double = {0, &H6666ff, 70, Chart.Transparent, 100, &Hff0000}

    If chartIndex = 0 Then
        ' Add the smooth color scale at the default position
        m.addColorScale(smoothColorScale)
    ElseIf chartIndex = 1 Then
        ' Add the step color scale at the default position
        m.addColorScale(stepColorScale)
    ElseIf chartIndex = 2 Then
        ' Add the high low scale at the default position
        m.addColorScale(highLowColorScale)
    ElseIf chartIndex = 3 Then
        ' Add the smooth color scale starting at x = 28 (left of scale) with zero width and ending
        ' at x = 28 with 20 pixels width
        m.addColorScale(smoothColorScale, 28, 0, 28, 20)
    ElseIf chartIndex = 4 Then
        ' Add the smooth color scale starting at x = 38 (center of scale) with zero width and ending
        ' at x = 28 with 20 pixels width
        m.addColorScale(smoothColorScale, 38, 0, 28, 20)
    Else
        ' Add the smooth color scale starting at x = 48 (right of scale) with zero width and ending
        ' at x = 28 with 20 pixels width
        m.addColorScale(smoothColorScale, 48, 0, 28, 20)
    End If

    ' Add a blue (0x0000cc) pointer at the specified value
    m.addPointer(value, &H0000cc)

    ' Add a title using white (0xffffff) 8pt Arial Bold font with a border color background
    m.addTitle("Temp C", "Arial Bold", 8, &Hffffff).setBackground(borderColor(chartIndex))

    ' Add a bottom title using white (0xffffff) 8pt Arial Bold font with a border color background
    ' to display the value
    m.addTitle2(Chart.Bottom, m.formatValue(value, "2"), "Arial Bold", 8, &Hffffff).setBackground( _
        borderColor(chartIndex))

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
    createChart(WebChartViewer4, 4)
    createChart(WebChartViewer5, 5)

End Sub

</script>

<html>
<head>
    <title>Color Vertical Linear Meters</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Color Vertical Linear Meters
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer0" runat="server" style='margin-right:25px;' />
    <chart:WebChartViewer id="WebChartViewer1" runat="server" style='margin-right:25px;' />
    <chart:WebChartViewer id="WebChartViewer2" runat="server" style='margin-right:25px;' />
    <chart:WebChartViewer id="WebChartViewer3" runat="server" style='margin-right:25px;' />
    <chart:WebChartViewer id="WebChartViewer4" runat="server" style='margin-right:25px;' />
    <chart:WebChartViewer id="WebChartViewer5" runat="server" />
</body>
</html>

