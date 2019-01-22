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
    Dim value As Double = 66.77

    ' The background, border and bar colors of the meters
    Dim bgColor() As Integer = {&Hcce4ff, &Hccffcc, &Hffdddd, &Hffffaa, &Hffccff, &Heeeeee}
    Dim borderColor() As Integer = {&H000088, &H006600, &H880000, &Hee6600, &H6600aa, &H666666}
    Dim barColor() As Integer = {&H2299ff, &H00ee00, &Hee4455, &Hff8800, &H8833dd, &H888888}

    ' Create a LinearMeter object of size 70 x 260 pixels with a 3-pixel thick rounded frame
    Dim m As LinearMeter = New LinearMeter(70, 260, bgColor(chartIndex), borderColor(chartIndex))
    m.setRoundedFrame(Chart.Transparent)
    m.setThickFrame(3)

    ' Set the scale region top-left corner at (28, 33), with size of 20 x 194 pixels. The scale
    ' labels are located on the left (default - implies vertical meter).
    m.setMeter(28, 33, 20, 194)

    ' Set meter scale from 0 - 100, with a tick every 10 units
    m.setScale(0, 100, 10)

    ' Demostrate different types of color scales
    Dim smoothColorScale() As Double = {0, &H0000ff, 25, &H0088ff, 50, &H00ff00, 75, &Hdddd00, _
        100, &Hff0000}
    Dim stepColorScale() As Double = {0, &H33ff33, 50, &Hffff00, 80, &Hff3333, 100}
    Dim highLowColorScale() As Double = {0, &H0000ff, 40, Chart.Transparent, 60, _
        Chart.Transparent, 100, &Hff0000}
    Dim highColorScale() As Double = {70, Chart.Transparent, 100, &Hff0000}
    Dim lowColorScale() As Double = {0, &H0000ff, 30, Chart.Transparent}

    If chartIndex = 0 Then
        ' Add a 6-pixel thick smooth color scale at x = 53 (right of meter scale)
        m.addColorScale(smoothColorScale, 53, 6)
    ElseIf chartIndex = 1 Then
        ' Add a high only color scale at x = 52 (right of meter scale) with thickness varying from 0
        ' to 8
        m.addColorScale(highColorScale, 52, 0, 52, 8)
        ' Add a low only color scale at x = 52 (right of meter scale) with thickness varying from 8
        ' to 0
        m.addColorScale(lowColorScale, 52, 8, 52, 0)
    ElseIf chartIndex = 2 Then
        ' Add a high only color scale at x = 52 (right of meter scale) with thickness varying from 0
        ' to 8
        m.addColorScale(highColorScale, 52, 0, 52, 8)
    ElseIf chartIndex = 3 Then
        ' Add a smooth color scale at x = 52 (right of meter scale) with thickness varying from 0 to
        ' 8
        m.addColorScale(smoothColorScale, 52, 0, 52, 8)
    ElseIf chartIndex = 4 Then
        ' Add a 6-pixel thick high/low color scale at x = 53 (right of meter scale)
        m.addColorScale(highLowColorScale, 53, 6)
    Else
        ' Add a 6-pixel thick step color scale at x = 53 (right of meter scale)
        m.addColorScale(stepColorScale, 53, 6)
    End If

    ' Add a bar from 0 to value with glass effect and 4 pixel rounded corners
    m.addBar(0, value, barColor(chartIndex), Chart.glassEffect(Chart.NormalGlare, Chart.Left), 4)

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
    <title>Color Vertical Bar Meters</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Color Vertical Bar Meters
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

