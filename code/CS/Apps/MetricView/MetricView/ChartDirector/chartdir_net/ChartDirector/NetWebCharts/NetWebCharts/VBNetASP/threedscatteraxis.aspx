<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)


    ' The x coordinates for the 2 scatter groups
    Dim dataX() As Date = {DateSerial(2011, 9, 1), DateSerial(2011, 9, 2), DateSerial(2011, 9, 3), _
        DateSerial(2011, 9, 4), DateSerial(2011, 9, 5), DateSerial(2011, 9, 6), DateSerial(2011, _
        9, 7), DateSerial(2011, 9, 8), DateSerial(2011, 9, 9), DateSerial(2011, 9, 10), _
        DateSerial(2011, 9, 11)}

    ' The y and z coordinates for the first scatter group
    Dim dataY0() As Double = {0.4, 0.2, 0.5, 0.4, 0.7, 1.3, 1.1, 1.0, 0.6, 0.4, 0.5}
    Dim dataZ0() As Double = {43, 38, 33, 23.4, 28, 36, 34, 47, 53, 45, 40}

    ' The y and z coordinates for the second scatter group
    Dim dataY1() As Double = {1.4, 1.0, 1.8, 1.9, 1.5, 1.0, 0.6, 0.7, 1.2, 1.7, 1.5}
    Dim dataZ1() As Double = {46, 41, 33, 37, 28, 29, 34, 37, 41, 52, 50}

    ' Instead of displaying numeric values, labels are used for the y-axis
    Dim labelsY() As String = {"Low", "Medium", "High"}

    ' Create a ThreeDScatterChart object of size 760 x 520 pixels
    Dim c As ThreeDScatterChart = New ThreeDScatterChart(760, 520)

    ' Add a title to the chart using 18 points Arial font
    c.addTitle("3D Scatter Chart Axis Types", "Arial", 18)

    ' Set the center of the plot region at (385, 270), and set width x depth x height to 480 x 240 x
    ' 240 pixels
    c.setPlotRegion(385, 270, 480, 240, 240)

    ' Set the elevation and rotation angles to 30 and -10 degrees
    c.setViewAngle(30, -10)

    ' Add a legend box at (380, 40) with horizontal layout. Use 9pt Arial Bold font.
    Dim b As LegendBox = c.addLegend(380, 40, False, "Arial Bold", 9)
    b.setAlignment(Chart.TopCenter)
    b.setRoundedCorners()

    ' Add a scatter group to the chart using 13 pixels red (ff0000) glass sphere symbols, with
    ' dotted drop lines
    Dim g0 As ThreeDScatterGroup = c.addScatterGroup(Chart.CTime(dataX), dataY0, dataZ0, _
        "Alpha Series", Chart.GlassSphere2Shape, 13, &Hff0000)
    g0.setDropLine(c.dashLineColor(Chart.SameAsMainColor, Chart.DotLine))

    ' Add a scatter group to the chart using 13 pixels blue (00cc00) cross symbols, with dotted drop
    ' lines
    Dim g1 As ThreeDScatterGroup = c.addScatterGroup(Chart.CTime(dataX), dataY1, dataZ1, _
        "Beta Series", Chart.Cross2Shape(), 13, &H00cc00)
    g1.setDropLine(c.dashLineColor(Chart.SameAsMainColor, Chart.DotLine))

    ' Set x-axis tick density to 50 pixels. ChartDirector auto-scaling will use this as the
    ' guideline when putting ticks on the x-axis.
    c.xAxis().setTickDensity(50)

    ' Set the y-axis labels
    c.yAxis().setLabels(labelsY)

    ' Set label style to Arial bold for all axes
    c.xAxis().setLabelStyle("Arial Bold")
    c.yAxis().setLabelStyle("Arial Bold")
    c.zAxis().setLabelStyle("Arial Bold")

    ' Set the x, y and z axis titles using deep blue (000088) 15 points Arial font
    c.xAxis().setTitle("Date/Time Axis", "Arial Italic", 15, &H000088)
    c.yAxis().setTitle("Label<*br*>Based<*br*>Axis", "Arial Italic", 15, &H000088)
    c.zAxis().setTitle("Numeric Axis", "Arial Italic", 15, &H000088)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

End Sub

</script>

<html>
<head>
    <title>3D Scatter Axis Types</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        3D Scatter Axis Types
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

