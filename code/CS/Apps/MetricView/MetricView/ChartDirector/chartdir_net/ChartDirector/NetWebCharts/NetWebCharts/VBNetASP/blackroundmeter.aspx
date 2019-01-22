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

    ' Create an AngularMeter object of size 250 x 250 pixels with transparent background
    Dim m As AngularMeter = New AngularMeter(250, 250, Chart.Transparent)

    ' Set the default text and line colors to white (0xffffff)
    m.setColor(Chart.TextColor, &Hffffff)
    m.setColor(Chart.LineColor, &Hffffff)

    ' Demonstration two different meter scale angles
    If chartIndex Mod 2 = 0 Then
        ' Center at (125, 125), scale radius = 111 pixels, scale angle -140 to +140 degrees
        m.setMeter(125, 125, 111, -140, 140)
    Else
        ' Center at (125, 125), scale radius = 111 pixels, scale angle -180 to +90 degrees
         m.setMeter(125, 125, 111, -180, 90)
    End If

    ' Add a black (0x000000) circle with radius 123 pixels as background
    m.addRing(0, 123, &H000000)

    ' Gradient color for the border to make it silver-like
    Dim ringGradient() As Double = {1, &H7f7f7f, 0.5, &Hd6d6d6, 0, &Hffffff, -0.5, &Hd6d6d6, -1, _
        &H7f7f7f}
    ' Add a ring between radii 116 and 122 pixels using the silver gradient as border
    m.addRing(116, 122, m.relativeLinearGradient(ringGradient, 45, 122))

    ' Meter scale is 0 - 100, with major/minor/micro ticks every 10/5/1 units
    m.setScale(0, 100, 10, 5, 1)

    ' Set the scale label style to 15pt Arial Italic. Set the major/minor/micro tick lengths to
    ' 12/9/6 pixels pointing inwards, and their widths to 2/1/1 pixels.
    m.setLabelStyle("Arial Italic", 15)
    m.setTickLength(-12, -9, -6)
    m.setLineWidth(0, 2, 1, 1)

    ' Demostrate different types of color scales and glare effects and putting them at different
    ' positions.
    Dim smoothColorScale() As Double = {0, &H0000ff, 25, &H0088ff, 50, &H00ff00, 75, &Hdddd00, _
        100, &Hff0000}
    Dim stepColorScale() As Double = {0, &H00aa00, 60, &Hddaa00, 80, &Hcc0000, 100}
    Dim highLowColorScale() As Double = {0, &H00ff00, 70, Chart.Transparent, 100, &Hff0000}

    If chartIndex = 0 Then
        ' Add the smooth color scale at the default position
        m.addColorScale(smoothColorScale)
        ' Add glare up to radius 116 (= region inside border)
        m.addGlare(116)
    ElseIf chartIndex = 1 Then
        ' Add the smooth color scale starting at radius 62 with zero width and ending at radius 40
        ' with 22 pixels outer width
        m.addColorScale(smoothColorScale, 62, 0, 40, 22)
        ' Add glare up to radius 116 (= region inside border), concave and spanning 190 degrees
        m.addGlare(116, -190)
    ElseIf chartIndex = 2 Then
        ' Add the smooth color scale starting at radius 111 with zero width and ending at radius 111
        ' with 12 pixels inner width
        m.addColorScale(smoothColorScale, 111, 0, 111, -12)
        ' Add glare up to radius 116 (= region inside border), concave and spanning 190 degrees and
        ' rotated by 45 degrees
        m.addGlare(116, -190, 45)
    ElseIf chartIndex = 3 Then
        ' Add the high/low color scale at the default position
        m.addColorScale(highLowColorScale)
    ElseIf chartIndex = 4 Then
        ' Add the smooth color scale at radius 44 with 16 pixels outer width
        m.addColorScale(smoothColorScale, 44, 16)
        ' Add glare up to radius 116 (= region inside border), concave and spanning 190 degrees and
        ' rotated by -45 degrees
        m.addGlare(116, -190, -45)
    Else
        ' Add the step color scale at the default position
        m.addColorScale(stepColorScale)
    End If

    ' Add a text label centered at (125, 175) with 15pt Arial Italic font
    m.addText(125, 175, "CPU", "Arial Italic", 15, Chart.TextColor, Chart.Center)

    ' Add a readout to some of the charts as demonstration
    If chartIndex = 0 Or chartIndex = 2 Then
        ' Put the value label center aligned at (125, 232), using black (0x000000) 14pt Arial font
        ' on a light blue (0x99ccff) background. Set box width to 50 pixels with 5 pixels rounded
        ' corners.
        Dim t As ChartDirector.TextBox = m.addText(125, 232, m.formatValue(value, _
            "<*block,width=50,halign=center*>{value|1}"), "Arial", 14, &H000000, _
            Chart.BottomCenter)
        t.setBackground(&H99ccff)
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
    <title>Black Round Meters</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Black Round Meters
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

