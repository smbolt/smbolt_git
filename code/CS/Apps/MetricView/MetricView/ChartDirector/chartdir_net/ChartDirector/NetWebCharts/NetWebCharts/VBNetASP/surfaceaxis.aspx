<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The x and y coordinates of the grid
    Dim dataX() As Date = {DateSerial(2008, 9, 1), DateSerial(2008, 9, 2), DateSerial(2008, 9, 3), _
        DateSerial(2008, 9, 4), DateSerial(2008, 9, 5), DateSerial(2008, 9, 6)}
    Dim dataY() As String = {"Low", "Medium", "High"}

    ' The data series
    Dim lowData() As Double = {24, 38, 33, 25, 28, 36}
    Dim mediumData() As Double = {49, 42, 34, 47, 53, 50}
    Dim highData() As Double = {44, 51, 38, 33, 47, 42}

    ' Create a SurfaceChart object of size 760 x 500 pixels
    Dim c As SurfaceChart = New SurfaceChart(760, 500)

    ' Add a title to the chart using 18 points Arial font
    c.addTitle("Surface Chart Axis Types", "Arial", 18)

    ' Set the center of the plot region at (385, 240), and set width x depth x height to 480 x 240 x
    ' 240 pixels
    c.setPlotRegion(385, 240, 480, 240, 240)

    ' Set the elevation and rotation angles to 30 and -10 degrees
    c.setViewAngle(30, -10)

    ' Set the data to use to plot the chart. As the y-data are text strings (enumerated), we will
    ' use an empty array for the y-coordinates. For the z data series, they are just the
    ' concatenation of the individual data series.
    c.setData(Chart.CTime(dataX), Nothing, New ArrayMath(lowData).insert(mediumData).insert( _
        highData).result())

    ' Set the y-axis labels
    c.yAxis().setLabels(dataY)

    ' Set x-axis tick density to 75 pixels. ChartDirector auto-scaling will use this as the
    ' guideline when putting ticks on the x-axis.
    c.xAxis().setTickDensity(75)

    ' Spline interpolate data to a 80 x 40 grid for a smooth surface
    c.setInterpolation(80, 40)

    ' Set surface grid lines to semi-transparent black (cc000000).
    c.setSurfaceAxisGrid(&Hcc000000)

    ' Set contour lines to the same color as the fill color at the contour level
    c.setContourColor(Chart.SameAsMainColor)

    ' Add a color axis (the legend) in which the top right corner is anchored at (95, 100). Set the
    ' length to 160 pixels and the labels on the left side.
    Dim cAxis As ColorAxis = c.setColorAxis(95, 100, Chart.TopRight, 160, Chart.Left)

    ' Add a bounding box with light grey (eeeeee) background and grey (888888) border.
    cAxis.setBoundingBox(&Heeeeee, &H888888)

    ' Set label style to Arial bold for all axes
    c.xAxis().setLabelStyle("Arial Bold")
    c.yAxis().setLabelStyle("Arial Bold")
    c.zAxis().setLabelStyle("Arial Bold")
    c.colorAxis().setLabelStyle("Arial Bold")

    ' Set the x, y and z axis titles using deep blue (000088) 15 points Arial font
    c.xAxis().setTitle("Date/Time Axis", "Arial Italic", 15, &H000088)
    c.yAxis().setTitle("Label<*br*>Based<*br*>Axis", "Arial Italic", 15, &H000088)
    c.zAxis().setTitle("Numeric Axis", "Arial Italic", 15, &H000088)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.JPG)

End Sub

</script>

<html>
<head>
    <title>Surface Chart Axis Types</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Surface Chart Axis Types
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

