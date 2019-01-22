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

    ' Create a LinearMeter object of size 260 x 80 pixels with very light grey (0xeeeeee) backgruond
    ' and a light grey (0xccccccc) 3-pixel thick rounded frame
    Dim m As LinearMeter = New LinearMeter(260, 80, &Heeeeee, &Haaaaaa)
    m.setRoundedFrame(Chart.Transparent)
    m.setThickFrame(3)

    ' Set the scale region top-left corner at (18, 24), with size of 222 x 20 pixels. The scale
    ' labels are located on the top (implies horizontal meter)
    m.setMeter(18, 24, 222, 20, Chart.Top)

    ' Set meter scale from 0 - 100, with a tick every 10 units
    m.setScale(0, 100, 10)

    ' Demostrate different types of color scales
    Dim smoothColorScale() As Double = {0, &H0000ff, 25, &H0088ff, 50, &H00ff00, 75, &Hdddd00, _
        100, &Hff0000}
    Dim stepColorScale() As Double = {0, &H00dd00, 50, &Hffff00, 80, &Hff0000, 100}
    Dim highLowColorScale() As Double = {0, &H0000ff, 40, Chart.Transparent, 60, _
        Chart.Transparent, 100, &Hff0000}
    Dim highColorScale() As Double = {70, Chart.Transparent, 100, &Hff0000}

    If chartIndex = 0 Then
        ' Add a blue (0x0088ff) bar from 0 to value with glass effect and 4 pixel rounded corners
        m.addBar(0, value, &H0088ff, Chart.glassEffect(Chart.NormalGlare, Chart.Top), 4)
        ' Add a 5-pixel thick smooth color scale at y = 48 (below the meter scale)
        m.addColorScale(smoothColorScale, 48, 5)
    ElseIf chartIndex = 1 Then
         ' Add a green (0x00cc00) bar from 0 to value with bar lighting effect
        m.addBar(0, value, &H00cc00, Chart.barLighting())
        ' Add a 5-pixel thick step color scale at y = 48 (below the meter scale)
        m.addColorScale(stepColorScale, 48, 5)
    ElseIf chartIndex = 2 Then
        ' Add a purple (0x8833dd) bar from 0 to value with glass effect and 4 pixel rounded corners
        m.addBar(0, value, &H8833dd, Chart.glassEffect(Chart.NormalGlare, Chart.Top), 4)
        ' Add a 5-pixel thick high/low color scale at y = 48 (below the meter scale)
        m.addColorScale(highLowColorScale, 48, 5)
    ElseIf chartIndex = 3 Then
          ' Add an orange (0xff8800) bar from 0 to value with cylinder lighting effect
        m.addBar(0, value, &Hff8800, Chart.cylinderEffect())
        ' Add a high only color scale at y = 48 (below the meter scale) with thickness varying from
        ' 0 to 8
        m.addColorScale(highColorScale, 48, 0, 48, 8)
    ElseIf chartIndex = 4 Then
        ' Add a red (0xee3333) bar from 0 to value with glass effect and 4 pixel rounded corners
        m.addBar(0, value, &Hee3333, Chart.glassEffect(Chart.NormalGlare, Chart.Top), 4)
        ' Add a 5-pixel thick smooth color scale at y = 48 (below the meter scale)
        m.addColorScale(smoothColorScale, 48, 5)
    Else
        ' Add a grey (0xaaaaaa) bar from 0 to value
        m.addBar(0, value, &Haaaaaa)
        ' Add a 5-pixel thick step color scale at y = 48 (below the meter scale)
        m.addColorScale(stepColorScale, 48, 5)
    End If

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
    <title>White Horizontal Bar Meters</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        White Horizontal Bar Meters
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

