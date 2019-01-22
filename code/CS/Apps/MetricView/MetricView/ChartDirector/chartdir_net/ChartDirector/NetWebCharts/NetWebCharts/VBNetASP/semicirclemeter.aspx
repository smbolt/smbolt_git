<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The value to display on the meter
    Dim value As Double = 72.55

    ' Create an AngularMeter object of size 300 x 180 pixels with transparent background
    Dim m As AngularMeter = New AngularMeter(300, 180, Chart.Transparent)

    ' Center at (150, 150), scale radius = 128 pixels, scale angle -90 to +90 degrees
    m.setMeter(150, 150, 128, -90, 90)

    ' Add a pale grey (0xeeeeee) scale background of 148 pixels radius, with a 10 pixel thick light
    ' grey (0xcccccc) border
    m.addScaleBackground(148, &Heeeeee, 10, &Hcccccc)

    ' Meter scale is 0 - 100, with major tick every 20 units, minor tick every 10 units, and micro
    ' tick every 5 units
    m.setScale(0, 100, 20, 10, 5)

    ' Set the scale label style to 15pt Arial Italic. Set the major/minor/micro tick lengths to
    ' 16/16/10 pixels pointing inwards, and their widths to 2/1/1 pixels.
    m.setLabelStyle("Arial Italic", 16)
    m.setTickLength(-16, -16, -10)
    m.setLineWidth(0, 2, 1, 1)

    ' Add a smooth color scale to the meter
    Dim smoothColorScale() As Double = {0, &H3333ff, 25, &H0088ff, 50, &H00ff00, 75, &Hdddd00, _
        100, &Hff0000}
    m.addColorScale(smoothColorScale)

    ' Add a text label centered at (150, 125) with 15pt Arial Italic font
    m.addText(150, 125, "CPU", "Arial Italic", 15, Chart.TextColor, Chart.BottomCenter)

    ' Add a red (0xff0000) pointer at the specified value
    m.addPointer2(value, &Hff0000)

    ' Output the chart
    WebChartViewer1.Image = m.makeWebImage(Chart.PNG)

End Sub

</script>

<html>
<head>
    <title>Semicircle Meter</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Semicircle Meter
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

