<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The data for the chart
    Dim data0() As Double = {5.1, 2.6, 1.5, 2.2, 5.1, 4.3, 4.0, 9.0, 1.7, 8.8, 9.9, 9.5, 9.4, 1.8, _
        2.1, 2.3, 3.5, 7.7, 8.8, 6.1, 5.0, 3.1, 6.0, 4.3}
    Dim angles0() As Double = {0, 15, 30, 45, 60, 75, 90, 105, 120, 135, 150, 165, 180, 195, 210, _
        225, 240, 255, 270, 285, 300, 315, 330, 345}

    Dim data1() As Double = {8.1, 2.5, 5, 5.2, 6.5, 8.5, 9, 7.6, 8.7, 6.4, 5.5, 5.4, 3.0, 8.7, _
        7.1, 8.8, 7.9, 2.2, 5.0, 4.0, 1.5, 7.5, 8.3, 9.0}
    Dim angles1() As Double = {0, 15, 30, 45, 60, 75, 90, 105, 120, 135, 150, 165, 180, 195, 210, _
        225, 240, 255, 270, 285, 300, 315, 330, 345}

    ' Create a PolarChart object of size 460 x 460 pixels
    Dim c As PolarChart = New PolarChart(460, 460)

    ' Add a title to the chart at the top left corner using 15pt Arial Bold Italic font
    c.addTitle2(Chart.TopLeft, "<*underline=2*>EM Field Strength", "Arial Bold Italic", 15)

    ' Set center of plot area at (230, 240) with radius 180 pixels
    c.setPlotArea(230, 240, 180)

    ' Set the grid style to circular grid
    c.setGridStyle(False)

    ' Add a legend box at the top right corner of the chart using 9pt Arial Bold font
    c.addLegend(459, 0, True, "Arial Bold", 9).setAlignment(Chart.TopRight)

    ' Set angular axis as 0 - 360, with a spoke every 30 units
    c.angularAxis().setLinearScale(0, 360, 30)

    ' Add a red (0xff9999) spline area layer to the chart using (data0, angles0)
    c.addSplineAreaLayer(data0, &Hff9999, "Above 100MHz").setAngles(angles0)

    ' Add a blue (0xff) spline line layer to the chart using (data1, angle1)
    Dim layer1 As PolarSplineLineLayer = c.addSplineLineLayer(data1, &H0000ff, "Below 100MHz")
    layer1.setAngles(angles1)

    ' Set the line width to 3 pixels
    layer1.setLineWidth(3)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{dataSetName} EM field at {angle} deg: {value} Watt'")

End Sub

</script>

<html>
<head>
    <title>Polar Spline Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Polar Spline Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

