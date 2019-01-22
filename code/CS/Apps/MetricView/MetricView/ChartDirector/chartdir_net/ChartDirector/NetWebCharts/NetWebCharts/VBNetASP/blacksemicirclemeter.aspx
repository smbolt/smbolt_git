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
    Dim value As Double = 72.55

    ' Create an AngularMeter object of size 300 x 180 pixels with transparent background
    Dim m As AngularMeter = New AngularMeter(300, 180, Chart.Transparent)

    ' Set the default text and line colors to white (0xffffff)
    m.setColor(Chart.TextColor, &Hffffff)
    m.setColor(Chart.LineColor, &Hffffff)

    ' Center at (150, 150), scale radius = 128 pixels, scale angle -90 to +90 degrees
    m.setMeter(150, 150, 128, -90, 90)

    ' Gradient color for the border to make it silver-like
    Dim ringGradient() As Double = {1, &H909090, 0.5, &Hd6d6d6, 0, &Heeeeee, -0.5, &Hd6d6d6, -1, _
        &H909090}

    ' Add a black (0x000000) scale background of 148 pixels radius with a 10 pixel thick silver
    ' border
    m.addScaleBackground(148, 0, 10, m.relativeLinearGradient(ringGradient, 45, 148))

    ' Meter scale is 0 - 100, with major tick every 20 units, minor tick every 10 units, and micro
    ' tick every 5 units
    m.setScale(0, 100, 20, 10, 5)

    ' Set the scale label style to 15pt Arial Italic. Set the major/minor/micro tick lengths to
    ' 16/16/10 pixels pointing inwards, and their widths to 2/1/1 pixels.
    m.setLabelStyle("Arial Italic", 16)
    m.setTickLength(-16, -16, -10)
    m.setLineWidth(0, 2, 1, 1)

    ' Demostrate different types of color scales and putting them at different positions
    Dim smoothColorScale() As Double = {0, &H0000ff, 25, &H0088ff, 50, &H00ff00, 75, &Hdddd00, _
        100, &Hff0000}
    Dim stepColorScale() As Double = {0, &H00aa00, 60, &Hddaa00, 80, &Hcc0000, 100}
    Dim highLowColorScale() As Double = {0, &H00ff00, 70, Chart.Transparent, 100, &Hff0000}

    If chartIndex = 0 Then
        ' Add the smooth color scale at the default position
        m.addColorScale(smoothColorScale)
    ElseIf chartIndex = 1 Then
        ' Add the smooth color scale starting at radius 128 with zero width and ending at radius 128
        ' with 16 pixels inner width
        m.addColorScale(smoothColorScale, 128, 0, 128, -16)
    ElseIf chartIndex = 2 Then
        ' Add the smooth color scale starting at radius 70 with zero width and ending at radius 60
        ' with 20 pixels outer width
        m.addColorScale(smoothColorScale, 70, 0, 60, 20)
    ElseIf chartIndex = 3 Then
        ' Add the high/low color scale at the default position
        m.addColorScale(highLowColorScale)
    ElseIf chartIndex = 4 Then
        ' Add the step color scale at the default position
        m.addColorScale(stepColorScale)
    Else
        ' Add the smooth color scale at radius 60 with 15 pixels outer width
        m.addColorScale(smoothColorScale, 60, 15)
    End If

    ' Add a text label centered at (150, 125) with 15pt Arial Italic font
    m.addText(150, 125, "CPU", "Arial Italic", 15, Chart.TextColor, Chart.BottomCenter)

    ' Add a red (0xff0000) pointer at the specified value
    m.addPointer2(value, &Hff0000)

    ' Add glare up to radius 138 (= region inside border)
    If chartIndex Mod 2 = 0 Then
        m.addGlare(138)
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
    createChart(WebChartViewer4, 4)
    createChart(WebChartViewer5, 5)

End Sub

</script>

<html>
<head>
    <title>Black Semicircle Meters</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Black Semicircle Meters
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

