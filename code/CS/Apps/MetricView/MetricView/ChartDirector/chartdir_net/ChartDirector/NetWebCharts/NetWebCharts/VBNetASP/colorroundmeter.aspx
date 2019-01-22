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
    Dim value As Double = 72.3

    ' The background and border colors of the meters
    Dim bgColor() As Integer = {&H88ccff, &Hffdddd, &Hffddaa, &Hffccff, &Hdddddd, &Hccffcc}
    Dim borderColor() As Integer = {&H000077, &H880000, &Hee6600, &H440088, &H000000, &H006000}

    ' Create an AngularMeter object of size 250 x 250 pixels with transparent background
    Dim m As AngularMeter = New AngularMeter(250, 250, Chart.Transparent)

    ' Demonstration two different meter scale angles
    If chartIndex Mod 2 = 0 Then
        ' Center at (125, 125), scale radius = 111 pixels, scale angle -140 to +140 degrees
        m.setMeter(125, 125, 111, -140, 140)
    Else
        ' Center at (125, 125), scale radius = 111 pixels, scale angle -180 to +90 degrees
         m.setMeter(125, 125, 111, -180, 90)
    End If

    ' Background gradient color with brighter color at the center
    Dim bgGradient() As Double = {0, m.adjustBrightness(bgColor(chartIndex), 3), 0.75, bgColor( _
        chartIndex)}
    ' Add circle with radius 123 pixels as background using the background gradient
    m.addRing(0, 123, m.relativeRadialGradient(bgGradient))
    ' Add a ring between radii 116 and 123 pixels as border
    m.addRing(116, 123, borderColor(chartIndex))

    ' Meter scale is 0 - 100, with major/minor/micro ticks every 10/5/1 units
    m.setScale(0, 100, 10, 5, 1)

    ' Set the scale label style to 15pt Arial Italic. Set the major/minor/micro tick lengths to
    ' 12/9/6 pixels pointing inwards, and their widths to 2/1/1 pixels.
    m.setLabelStyle("Arial Italic", 15)
    m.setTickLength(-12, -9, -6)
    m.setLineWidth(0, 2, 1, 1)

    ' Demostrate different types of color scales and putting them at different positions
    Dim smoothColorScale() As Double = {0, &H3333ff, 25, &H0088ff, 50, &H00ff00, 75, &Hdddd00, _
        100, &Hff0000}
    Dim stepColorScale() As Double = {0, &H00cc00, 60, &Hffdd00, 80, &Hee0000, 100}
    Dim highLowColorScale() As Double = {0, &H00ff00, 70, Chart.Transparent, 100, &Hff0000}

    If chartIndex = 0 Then
        ' Add the smooth color scale at the default position
        m.addColorScale(smoothColorScale)
    ElseIf chartIndex = 1 Then
        ' Add the smooth color scale starting at radius 62 with zero width and ending at radius 40
        ' with 22 pixels outer width
        m.addColorScale(smoothColorScale, 62, 0, 40, 22)
    ElseIf chartIndex = 2 Then
        ' Add green, yellow and red zones between radii 44 and 60
        m.addZone(0, 60, 44, 60, &H00dd00)
        m.addZone(60, 80, 44, 60, &Hffff00)
        m.addZone(80, 100, 44, 60, &Hff0000)
    ElseIf chartIndex = 3 Then
        ' Add the high/low color scale at the default position
        m.addColorScale(highLowColorScale)
    ElseIf chartIndex = 4 Then
        ' Add the smooth color scale at radius 44 with 16 pixels outer width
        m.addColorScale(smoothColorScale, 44, 16)
    Else
        ' Add the step color scale at the default position
        m.addColorScale(stepColorScale)
    End If

    ' Add a text label centered at (125, 175) with 15pt Arial Italic font
    m.addText(125, 175, "CPU", "Arial Italic", 15, Chart.TextColor, Chart.Center)

    ' Add a readout to some of the charts as demonstration
    If chartIndex = 0 Or chartIndex = 2 Then
        ' Put the value label center aligned at (125, 232), using white (0xffffff) 14pt Arial font
        ' on a black (0x000000) background. Set box width to 50 pixels with 5 pixels rounded
        ' corners.
        Dim t As ChartDirector.TextBox = m.addText(125, 232, m.formatValue(value, _
            "<*block,width=50,halign=center*>{value|1}"), "Arial", 14, &Hffffff, _
            Chart.BottomCenter)
        t.setBackground(&H000000)
        t.setRoundedCorners(5)
    End If

    ' Add a red (0xff0000) pointer at the specified value
    m.addPointer2(value, &Hff0000)

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
    <title>Color Round Meters</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Color Round Meters
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

