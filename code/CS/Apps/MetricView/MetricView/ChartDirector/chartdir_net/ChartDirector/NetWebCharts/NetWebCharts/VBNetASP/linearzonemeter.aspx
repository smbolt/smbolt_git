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
    Dim value As Double = 85

    ' Create an LinearMeter object of size 210 x 45 pixels, using silver background with a 2 pixel
    ' black 3D depressed border.
    Dim m As LinearMeter = New LinearMeter(210, 45, Chart.silverColor(), 0, -2)

    ' Set the scale region top-left corner at (5, 5), with size of 200 x 20 pixels. The scale labels
    ' are located on the bottom (implies horizontal meter)
    m.setMeter(5, 5, 200, 20, Chart.Bottom)

    ' Set meter scale from 0 - 100
    m.setScale(0, 100)

    ' Add a title at the bottom of the meter with a 1 pixel raised 3D border
    m.addTitle2(Chart.Bottom, "Battery Level", "Arial Bold", 8).setBackground(Chart.Transparent, _
        -1, 1)

    ' Set 3 zones of different colors to represent Good/Weak/Bad data ranges
    m.addZone(50, 100, &H99ff99, "Good")
    m.addZone(20, 50, &Hffff66, "Weak")
    m.addZone(0, 20, &Hffcccc, "Bad")

    ' Add empty labels (just need the ticks) at 0/20/50/80 as separators for zones
    m.addLabel(0, " ")
    m.addLabel(20, " ")
    m.addLabel(50, " ")
    m.addLabel(100, " ")

    ' Add a semi-transparent blue (800000ff) pointer at the specified value, using triangular
    ' pointer shape
    m.addPointer(value, &H800000ff).setShape(Chart.TriangularPointer)

    ' Output the chart
    WebChartViewer1.Image = m.makeWebImage(Chart.PNG)

End Sub

</script>

<html>
<head>
    <title>Linear Zone Meter</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Linear Zone Meter
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

