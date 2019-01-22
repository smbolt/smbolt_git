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

    ' The background and border colors of the meters
    Dim bgColor() As Integer = {&H88ccff, &Hffdddd, &Hffddaa, &Hffccff, &Hdddddd, &Hccffcc}
    Dim borderColor() As Integer = {&H000077, &H880000, &Hee6600, &H440088, &H000000, &H006000}

    ' Create an AngularMeter object of size 300 x 180 pixels with transparent background
    Dim m As AngularMeter = New AngularMeter(300, 180, Chart.Transparent)

    ' Center at (150, 150), scale radius = 124 pixels, scale angle -90 to +90 degrees
    m.setMeter(150, 150, 124, -90, 90)

    ' Background gradient color with brighter color at the center
    Dim bgGradient() As Double = {0, m.adjustBrightness(bgColor(chartIndex), 3), 0.75, bgColor( _
        chartIndex)}

    ' Add a scale background of 148 pixels radius using the background gradient, with a 13 pixel
    ' thick border
    m.addScaleBackground(148, m.relativeRadialGradient(bgGradient), 13, borderColor(chartIndex))

    ' Meter scale is 0 - 100, with major tick every 20 units, minor tick every 10 units, and micro
    ' tick every 5 units
    m.setScale(0, 100, 20, 10, 5)

    ' Set the scale label style to 15pt Arial Italic. Set the major/minor/micro tick lengths to
    ' 16/16/10 pixels pointing inwards, and their widths to 2/1/1 pixels.
    m.setLabelStyle("Arial Italic", 16)
    m.setTickLength(-16, -16, -10)
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
        ' Add the smooth color scale starting at radius 124 with zero width and ending at radius 124
        ' with 16 pixels inner width
        m.addColorScale(smoothColorScale, 124, 0, 124, -16)
    ElseIf chartIndex = 2 Then
        ' Add the smooth color scale starting at radius 65 with zero width and ending at radius 55
        ' with 20 pixels outer width
        m.addColorScale(smoothColorScale, 65, 0, 55, 20)
    ElseIf chartIndex = 3 Then
        ' Add the high/low color scale at the default position
        m.addColorScale(highLowColorScale)
    ElseIf chartIndex = 4 Then
        ' Add the step color scale at the default position
        m.addColorScale(stepColorScale)
    Else
        ' Add the smooth color scale at radius 55 with 20 pixels outer width
        m.addColorScale(smoothColorScale, 55, 20)
    End If

    ' Add a text label centered at (150, 125) with 15pt Arial Italic font
    m.addText(150, 125, "CPU", "Arial Italic", 15, Chart.TextColor, Chart.BottomCenter)

    ' Demonstrate two different types of pointers - thin triangular pointer (the default) and line
    ' pointer
    If chartIndex Mod 2 = 0 Then
        m.addPointer2(value, &Hff0000)
    Else
        m.addPointer2(value, &Hff0000, -1, Chart.LinePointer2)
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
    <title>Color Semicircle Meters</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Color Semicircle Meters
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

