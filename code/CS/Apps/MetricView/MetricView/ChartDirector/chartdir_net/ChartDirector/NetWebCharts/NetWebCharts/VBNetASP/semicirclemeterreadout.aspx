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
    Dim value As Double = 66

    ' The background and border colors of the meters
    Dim bgColor() As Integer = {&H88ccff, &Hffdddd}
    Dim borderColor() As Integer = {&H000077, &H880000}

    ' Create an AngularMeter object of size 300 x 200 pixels with transparent background
    Dim m As AngularMeter = New AngularMeter(300, 200, Chart.Transparent)

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

    If chartIndex = 0 Then
        ' Add the smooth color scale at the default position
        m.addColorScale(smoothColorScale)
        ' Add a red (0xff0000) triangular pointer starting from 38% and ending at 60% of scale
        ' radius, with a width 6 times the default
        m.addPointer2(value, &Hff0000, -1, Chart.TriangularPointer2, 0.38, 0.6, 6)
    Else
        ' Add the smooth color scale starting at radius 124 with zero width and ending at radius 124
        ' with 16 pixels inner width
        m.addColorScale(smoothColorScale, 124, 0, 124, -16)
        ' Add a red (0xff0000) pointer
        m.addPointer2(value, &Hff0000)
    End If

    ' Configure a large "pointer cap" to be used as the readout circle at the center. The cap radius
    ' and border width is set to 33% and 4% of the meter scale radius. The cap color is dark blue
    ' (0x000044). The border color is light blue (0x66bbff) with a 60% brightness gradient effect.
    m.setCap2(Chart.Transparent, &H000044, &H66bbff, 0.6, 0, 0.33, 0.04)

    ' Add value label at the center with light blue (0x66ddff) 28pt Arial Italic font
    m.addText(150, 150, m.formatValue(value, "{value|0}"), "Arial Italic", 28, &H66ddff, _
        Chart.Center).setMargin(0)

    ' Output the chart
    viewer.Image = m.makeWebImage(Chart.PNG)

End Sub

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    createChart(WebChartViewer0, 0)
    createChart(WebChartViewer1, 1)

End Sub

</script>

<html>
<head>
    <title>Semicircle Meter with Readout</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Semicircle Meter with Readout
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer0" runat="server" />
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

