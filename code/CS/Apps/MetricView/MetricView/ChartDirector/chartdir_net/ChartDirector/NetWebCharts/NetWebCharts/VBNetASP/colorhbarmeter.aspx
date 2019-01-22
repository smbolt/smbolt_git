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
    Dim value As Double = 75.35

    ' The background, border and bar colors of the meters
    Dim bgColor() As Integer = {&Hbbddff, &Hccffcc, &Hffddff, &Hffffaa, &Hffdddd, &Heeeeee}
    Dim borderColor() As Integer = {&H000088, &H006600, &H880088, &Hee6600, &H880000, &H666666}
    Dim barColor() As Integer = {&H0088ff, &H00cc00, &H8833dd, &Hff8800, &Hee3333, &H888888}

    ' Create a LinearMeter object of size 260 x 80 pixels with a 3-pixel thick rounded frame
    Dim m As LinearMeter = New LinearMeter(260, 80, bgColor(chartIndex), borderColor(chartIndex))
    m.setRoundedFrame(Chart.Transparent)
    m.setThickFrame(3)

    ' Set the scale region top-left corner at (18, 24), with size of 222 x 20 pixels. The scale
    ' labels are located on the top (implies horizontal meter)
    m.setMeter(18, 24, 222, 20, Chart.Top)

    ' Set meter scale from 0 - 100, with a tick every 10 units
    m.setScale(0, 100, 10)

    If chartIndex Mod 4 = 0 Then
        ' Add a 5-pixel thick smooth color scale at y = 48 (below the meter scale)
        Dim smoothColorScale() As Double = {0, &H0000ff, 25, &H0088ff, 50, &H00ff00, 75, &Hdddd00, _
            100, &Hff0000}
        m.addColorScale(smoothColorScale, 48, 5)
    ElseIf chartIndex Mod 4 = 1 Then
        ' Add a 5-pixel thick step color scale at y = 48 (below the meter scale)
        Dim stepColorScale() As Double = {0, &H00cc00, 50, &Hffdd00, 80, &Hff3333, 100}
        m.addColorScale(stepColorScale, 48, 5)
    ElseIf chartIndex Mod 4 = 2 Then
        ' Add a 5-pixel thick high/low color scale at y = 48 (below the meter scale)
        Dim highLowColorScale() As Double = {0, &H0000ff, 40, Chart.Transparent, 60, _
            Chart.Transparent, 100, &Hff0000}
        m.addColorScale(highLowColorScale, 48, 5)
    Else
        ' Add a 5-pixel thick high only color scale at y = 48 (below the meter scale)
        Dim highColorScale() As Double = {70, Chart.Transparent, 100, &Hff0000}
        m.addColorScale(highColorScale, 48, 0, 48, 8)
    End If

    ' Add a bar from 0 to value with glass effect and 4 pixel rounded corners
    m.addBar(0, value, barColor(chartIndex), Chart.glassEffect(Chart.NormalGlare, Chart.Top), 4)

    ' Add a label right aligned to (243, 65) using 8pt Arial Bold font
    m.addText(243, 65, "Temperature C", "Arial Bold", 8, Chart.TextColor, Chart.Right)

    ' Add a text box left aligned to (18, 65). Display the value using white (0xffffff) 8pt Arial
    ' Bold font on a black (0x000000) background with depressed rounded border.
    Dim t As ChartDirector.TextBox = m.addText(18, 65, m.formatValue(value, "2"), "Arial", 8, _
        &Hffffff, Chart.Left)
    t.setBackground(&H000000, &H000000, -1)
    t.setRoundedCorners(3)

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
    <title>Color Horizontal Bar Meters</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Color Horizontal Bar Meters
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer0" runat="server" />
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
    <chart:WebChartViewer id="WebChartViewer2" runat="server" />
    <chart:WebChartViewer id="WebChartViewer3" runat="server" />
    <chart:WebChartViewer id="WebChartViewer4" runat="server" />
    <chart:WebChartViewer id="WebChartViewer5" runat="server" />
</body>
</html>

