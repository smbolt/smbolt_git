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
    Dim data0() As Double = {100, 100, 100, 100, 100}
    Dim data1() As Double = {90, 85, 85, 80, 70}
    Dim data2() As Double = {80, 65, 65, 75, 45}

    ' The labels for the chart
    Dim labels() As String = {"Population<*br*><*font=Arial*>6 millions", _
        "GDP<*br*><*font=Arial*>120 billions", "Export<*br*><*font=Arial*>25 billions", _
        "Import<*br*><*font=Arial*>24 billions", "Investments<*br*><*font=Arial*>20 billions"}

    ' Create a PolarChart object of size 480 x 460 pixels. Set background color to silver, with 1
    ' pixel 3D border effect
    Dim c As PolarChart = New PolarChart(480, 460, Chart.silverColor(), &H000000, 1)

    ' Add a title to the chart using 15pt Times Bold Italic font. The title text is white (ffffff)
    ' on a deep green (008000) background
    c.addTitle("Economic Growth", "Times New Roman Bold Italic", 15, &Hffffff).setBackground( _
        &H008000)

    ' Set plot area center at (240, 270), with 150 pixels radius
    c.setPlotArea(240, 270, 150)

    ' Use 1 pixel width semi-transparent black (c0000000) lines as grid lines
    c.setGridColor(&Hc0000000, 1, &Hc0000000, 1)

    ' Add a legend box at top-center of plot area (240, 35) using horizontal layout. Use 10pt Arial
    ' Bold font, with silver background and 1 pixel 3D border effect.
    Dim b As LegendBox = c.addLegend(240, 35, False, "Arial Bold", 10)
    b.setAlignment(Chart.TopCenter)
    b.setBackground(Chart.silverColor(), Chart.Transparent, 1)

    ' Add area layers of different colors to represent the data
    c.addAreaLayer(data0, &Hcc8880, "Year 2004")
    c.addAreaLayer(data1, &Hffd080, "Year 1994")
    c.addAreaLayer(data2, &Ha0bce0, "Year 1984")

    ' Set the labels to the angular axis as spokes.
    c.angularAxis().setLabels(labels)

    ' Set radial axis from 0 - 100 with a tick every 20 units
    c.radialAxis().setLinearScale(0, 100, 20)

    ' Just show the radial axis as a grid line. Hide the axis labels by setting the label color to
    ' Transparent
    c.radialAxis().setColors(&Hc0000000, Chart.Transparent)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='Current {label}: {value}% in {dataSetName}'")

End Sub

</script>

<html>
<head>
    <title>Stacked Radar Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Stacked Radar Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

