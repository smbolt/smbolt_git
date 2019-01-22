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

    ' Create an AngularMeter object of size 240 x 170 pixels with very light grey (0xeeeeee)
    ' background, and a rounded 4-pixel thick light grey (0xcccccc) border
    Dim m As AngularMeter = New AngularMeter(240, 170, &Heeeeee, &Hcccccc)
    m.setRoundedFrame(Chart.Transparent)
    m.setThickFrame(4)

    ' Set the default text and line colors to dark grey (0x222222)
    m.setColor(Chart.TextColor, &H222222)
    m.setColor(Chart.LineColor, &H222222)

    ' Center at (120, 145), scale radius = 128 pixels, scale angle -60 to +60 degrees
    m.setMeter(120, 145, 128, -60, 60)

    ' Meter scale is 0 - 100, with major/minor/micro ticks every 20/10/5 units
    m.setScale(0, 100, 20, 10, 5)

    ' Set the scale label style to 14pt Arial Italic. Set the major/minor/micro tick lengths to
    ' 16/16/10 pixels pointing inwards, and their widths to 2/1/1 pixels.
    m.setLabelStyle("Arial Italic", 14)
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

    ' Add a text label centered at (120, 120) with 15pt Arial Italic font
    m.addText(120, 120, "CPU", "Arial Italic", 15, Chart.TextColor, Chart.BottomCenter)

    ' Add a red (0xff0000) pointer at the specified value
    m.addPointer2(value, &Hff0000)

    ' Add a semi-transparent light grey (0x3fcccccc) rectangle at (0, 120) and of size 240 x 60
    ' pixels to cover the bottom part of the meter for decoration
    Dim cover As ChartDirector.TextBox = m.addText(0, 120, "")
    cover.setSize(240, 60)
    cover.setBackground(&H3fcccccc)

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
    <title>Rectangular Angular Meters</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Rectangular Angular Meters
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

