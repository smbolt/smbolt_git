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

    ' Create a LinearMeter object of size 260 x 80 pixels with black background and rounded corners
    Dim m As LinearMeter = New LinearMeter(260, 80, &H000000)
    m.setRoundedFrame(Chart.Transparent)

    ' Set the default text and line colors to white (0xffffff)
    m.setColor(Chart.TextColor, &Hffffff)
    m.setColor(Chart.LineColor, &Hffffff)

    ' Set the scale region top-left corner at (15, 24), with size of 228 x 20 pixels. The scale
    ' labels are located on the top (implies horizontal meter)
    m.setMeter(15, 24, 228, 20, Chart.Top)

    ' Set meter scale from 0 - 100, with a tick every 10 units
    m.setScale(0, 100, 10)

    ' Demostrate different types of color scales
    Dim smoothColorScale() As Double = {0, &H0000ff, 25, &H0088ff, 50, &H00ff00, 75, &Hdddd00, _
        100, &Hff0000}
    Dim stepColorScale() As Double = {0, &H00dd00, 50, &Hffff00, 80, &Hff3333, 100}
    Dim highLowColorScale() As Double = {0, &H0000ff, 40, Chart.Transparent, 60, _
        Chart.Transparent, 100, &Hff0000}
    Dim highColorScale() As Double = {70, Chart.Transparent, 100, &Hff0000}

    If chartIndex = 0 Then
        ' Add a blue (0x0088ff) bar from 0 to value with glass effect and 4 pixel rounded corners
        m.addBar(0, value, &H0088ff, Chart.glassEffect(Chart.NormalGlare, Chart.Top), 4)
        ' Add a 5-pixel thick smooth color scale at y = 48 (below the meter scale)
        m.addColorScale(smoothColorScale, 48, 5)
    ElseIf chartIndex = 1 Then
        ' Add a purple (0xaa66ff) bar from 0 to value
        m.addBar(0, value, &Haa66ff)
        ' Add a 4 pixel thick purple (0x880088) frame
        m.setThickFrame(4, &H880088)
        ' Add a 5-pixel thick high/low color scale at y = 48 (below the meter scale)
        m.addColorScale(highLowColorScale, 48, 5)
    ElseIf chartIndex = 2 Then
         ' Add a green (0x00ee00) bar from 0 to value with right side soft lighting effect and 4
         ' pixel rounded corners
        m.addBar(0, value, &H00ee00, Chart.softLighting(Chart.Right), 4)
        ' Add a 5-pixel thick step color scale at y = 48 (below the meter scale)
        m.addColorScale(stepColorScale, 48, 5)
    ElseIf chartIndex = 3 Then
          ' Add an orange (0xff8800) bar from 0 to value with cylinder lighting effect
        m.addBar(0, value, &Hff8800, Chart.cylinderEffect())
        ' Add a 4 pixel thick brown (0xbb5500) frame
        m.setThickFrame(4, &Hbb5500)
        ' Add a 5-pixel thick high only color scale at y = 48 (below the meter scale)
        m.addColorScale(highColorScale, 48, 5)
    ElseIf chartIndex = 4 Then
         ' Add a magneta (0xdd00dd) bar from 0 to value with top side soft lighting effect and 4
         ' pixel rounded corners
        m.addBar(0, value, &Hdd00dd, Chart.softLighting(Chart.Top), 4)
        ' Add a 5-pixel thick smooth color scale at y = 48 (below the meter scale)
        m.addColorScale(smoothColorScale, 48, 5)
    Else
         ' Add a red (0xff0000) bar from 0 to value with bar lighting effect
        m.addBar(0, value, &Hff0000, Chart.barLighting())
        ' Add a 4 pixel thick red (0xaa0000) frame
        m.setThickFrame(4, &Haa0000)
           ' Add a 5-pixel thick high/low color scale at y = 48 (below the meter scale)
        m.addColorScale(highLowColorScale, 48, 5)
    End If

    ' Add a label left aligned to (12, 65) using 8pt Arial Bold font
    m.addText(12, 65, "Temperature C", "Arial Bold", 8, Chart.TextColor, Chart.Left)

    ' Add a text box right aligned to (243, 65). Display the value using white (0xffffff) 8pt Arial
    ' Bold font on a black (0x000000) background with depressed dark grey (0x333333) rounded border.
    Dim t As ChartDirector.TextBox = m.addText(243, 65, m.formatValue(value, "2"), "Arial", 8, _
        &Hffffff, Chart.Right)
    t.setBackground(&H000000, &H333333, -1)
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
    <title>Black Horizontal Bar Meters</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Black Horizontal Bar Meters
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

