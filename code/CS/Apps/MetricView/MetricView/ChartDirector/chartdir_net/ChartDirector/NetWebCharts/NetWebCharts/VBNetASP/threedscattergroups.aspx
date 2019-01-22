<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The random XYZ data for the first 3D scatter group
    Dim r0 As RanSeries = New RanSeries(7)
    Dim xData0() As Double = r0.getSeries2(100, 100, -10, 10)
    Dim yData0() As Double = r0.getSeries2(100, 0, 0, 20)
    Dim zData0() As Double = r0.getSeries2(100, 100, -10, 10)

    ' The random XYZ data for the second 3D scatter group
    Dim r1 As RanSeries = New RanSeries(4)
    Dim xData1() As Double = r1.getSeries2(100, 100, -10, 10)
    Dim yData1() As Double = r1.getSeries2(100, 0, 0, 20)
    Dim zData1() As Double = r1.getSeries2(100, 100, -10, 10)

    ' The random XYZ data for the third 3D scatter group
    Dim r2 As RanSeries = New RanSeries(8)
    Dim xData2() As Double = r2.getSeries2(100, 100, -10, 10)
    Dim yData2() As Double = r2.getSeries2(100, 0, 0, 20)
    Dim zData2() As Double = r2.getSeries2(100, 100, -10, 10)

    ' Create a ThreeDScatterChart object of size 800 x 520 pixels
    Dim c As ThreeDScatterChart = New ThreeDScatterChart(800, 520)

    ' Add a title to the chart using 20 points Times New Roman Italic font
    c.addTitle("3D Scatter Groups                    ", "Times New Roman Italic", 20)

    ' Set the center of the plot region at (350, 240), and set width x depth x height to 360 x 360 x
    ' 270 pixels
    c.setPlotRegion(350, 240, 360, 360, 270)

    ' Set the elevation and rotation angles to 15 and 30 degrees
    c.setViewAngle(15, 30)

    ' Add a legend box at (640, 180)
    c.addLegend(640, 180)

    ' Add 3 scatter groups to the chart with 9 pixels glass sphere symbols of red (ff0000), green
    ' (00ff00) and blue (0000ff) colors
    c.addScatterGroup(xData0, yData0, zData0, "Alpha", Chart.GlassSphere2Shape, 9, &Hff0000)
    c.addScatterGroup(xData1, yData1, zData1, "Beta", Chart.GlassSphere2Shape, 9, &H00ff00)
    c.addScatterGroup(xData2, yData2, zData2, "Gamma", Chart.GlassSphere2Shape, 9, &H0000ff)

    ' Set the x, y and z axis titles
    c.xAxis().setTitle("X-Axis Place Holder")
    c.yAxis().setTitle("Y-Axis Place Holder")
    c.zAxis().setTitle("Z-Axis Place Holder")

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

End Sub

</script>

<html>
<head>
    <title>3D Scatter Groups</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        3D Scatter Groups
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

