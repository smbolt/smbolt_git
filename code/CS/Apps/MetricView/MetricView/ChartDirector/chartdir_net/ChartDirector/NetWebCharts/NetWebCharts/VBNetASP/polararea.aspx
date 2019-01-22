<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' Data for the chart
    Dim data0() As Double = {5, 3, 10, 4, 3, 5, 2, 5}
    Dim data1() As Double = {12, 6, 17, 6, 7, 9, 4, 7}
    Dim data2() As Double = {17, 7, 22, 7, 18, 13, 5, 11}

    Dim labels() As String = {"North", "North<*br*>East", "East", "South<*br*>East", "South", _
        "South<*br*>West", "West", "North<*br*>West"}

    ' Create a PolarChart object of size 460 x 500 pixels, with a grey (e0e0e0) background and 1
    ' pixel 3D border
    Dim c As PolarChart = New PolarChart(460, 500, &He0e0e0, &H000000, 1)

    ' Add a title to the chart at the top left corner using 15pt Arial Bold Italic font. Use a wood
    ' pattern as the title background.
    c.addTitle("Polar Area Chart Demo", "Arial Bold Italic", 15).setBackground(c.patternColor( _
        Server.MapPath("wood.png")))

    ' Set center of plot area at (230, 280) with radius 180 pixels, and white (ffffff) background.
    c.setPlotArea(230, 280, 180, &Hffffff)

    ' Set the grid style to circular grid
    c.setGridStyle(False)

    ' Add a legend box at top-center of plot area (230, 35) using horizontal layout. Use 10pt Arial
    ' Bold font, with 1 pixel 3D border effect.
    Dim b As LegendBox = c.addLegend(230, 35, False, "Arial Bold", 9)
    b.setAlignment(Chart.TopCenter)
    b.setBackground(Chart.Transparent, Chart.Transparent, 1)

    ' Set angular axis using the given labels
    c.angularAxis().setLabels(labels)

    ' Specify the label format for the radial axis
    c.radialAxis().setLabelFormat("{value}%")

    ' Set radial axis label background to semi-transparent grey (40cccccc)
    c.radialAxis().setLabelStyle().setBackground(&H40cccccc, 0)

    ' Add the data as area layers
    c.addAreaLayer(data2, -1, "5 m/s or above")
    c.addAreaLayer(data1, -1, "1 - 5 m/s")
    c.addAreaLayer(data0, -1, "less than 1 m/s")

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='[{label}] {dataSetName}: {value}%'")

End Sub

</script>

<html>
<head>
    <title>Polar Area Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Polar Area Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

