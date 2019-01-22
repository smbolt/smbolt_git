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
    Dim value As Double = 4.75

    ' Create an AugularMeter object of size 110 x 110 pixels, using silver background color with a
    ' black 2 pixel 3D depressed border.
    Dim m As AngularMeter = New AngularMeter(110, 110, Chart.silverColor(), &H000000, -2)

    ' Set meter appearance according to a parameter
    If chartIndex = 0 Then
        ' Set the meter center at bottom left corner (15, 95), with radius 85 pixels. Meter spans
        ' from 90 - 0 degrees.
        m.setMeter(15, 95, 85, 90, 0)
        ' Add a label to the meter centered at (35, 75)
        m.addText(35, 75, "VDC", "Arial Bold", 12, Chart.TextColor, Chart.Center)
        ' Add a text box to show the value at top right corner (103, 7)
        m.addText(103, 7, m.formatValue(value, "2"), "Arial", 8, &Hffffff, Chart.TopRight _
            ).setBackground(0, 0, -1)
    ElseIf chartIndex = 1 Then
        ' Set the meter center at top left corner (15, 15), with radius 85 pixels. Meter spans from
        ' 90 - 180 degrees.
        m.setMeter(15, 15, 85, 90, 180)
        ' Add a label to the meter centered at (35, 35)
        m.addText(35, 35, "AMP", "Arial Bold", 12, Chart.TextColor, Chart.Center)
        ' Add a text box to show the value at bottom right corner (103, 103)
        m.addText(103, 103, m.formatValue(value, "2"), "Arial", 8, &Hffffff, Chart.BottomRight _
            ).setBackground(0, 0, -1)
    ElseIf chartIndex = 2 Then
        ' Set the meter center at top right corner (15, 95), with radius 85 pixels. Meter spans from
        ' 270 - 180 degrees.
        m.setMeter(95, 15, 85, 270, 180)
        ' Add a label to the meter centered at (75, 35)
        m.addText(75, 35, "KW", "Arial Bold", 12, Chart.TextColor, Chart.Center)
        ' Add a text box to show the value at bottom left corner (7, 103)
        m.addText(7, 103, m.formatValue(value, "2"), "Arial", 8, &Hffffff, Chart.BottomLeft _
            ).setBackground(0, 0, -1)
    Else
        ' Set the meter center at bottom right corner (95, 95), with radius 85 pixels. Meter spans
        ' from 270 - 360 degrees.
        m.setMeter(95, 95, 85, 270, 360)
        ' Add a label to the meter centered at (75, 75)
        m.addText(75, 75, "RPM", "Arial Bold", 12, Chart.TextColor, Chart.Center)
        ' Add a text box to show the value at top left corner (7, 7)
        m.addText(7, 7, m.formatValue(value, "2"), "Arial", 8, &Hffffff, Chart.TopLeft _
            ).setBackground(0, 0, -1)
    End If

    ' Meter scale is 0 - 10, with a major tick every 2 units, and minor tick every 1 unit
    m.setScale(0, 10, 2, 1)

    ' Set 0 - 6 as green (99ff99) zone, 6 - 8 as yellow (ffff00) zone, and 8 - 10 as red (ff3333)
    ' zone
    m.addZone(0, 6, &H99ff99, &H808080)
    m.addZone(6, 8, &Hffff00, &H808080)
    m.addZone(8, 10, &Hff3333, &H808080)

    ' Add a semi-transparent black (80000000) pointer at the specified value
    m.addPointer(value, &H80000000)

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

End Sub

</script>

<html>
<head>
    <title>Square Angular Meters</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Square Angular Meters
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer0" runat="server" />
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
    <chart:WebChartViewer id="WebChartViewer2" runat="server" />
    <chart:WebChartViewer id="WebChartViewer3" runat="server" />
</body>
</html>

