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
    Dim data0() As Double = {43, 89, 76, 64, 48, 18, 92, 68, 44, 79, 71, 85}
    Dim angles0() As Double = {45, 96, 169, 258, 15, 30, 330, 260, 60, 75, 110, 140}

    Dim data1() As Double = {50, 91, 26, 29, 80, 53, 62, 87, 19, 40}
    Dim angles1() As Double = {230, 210, 240, 310, 179, 250, 244, 199, 89, 160}

    Dim data2() As Double = {88, 65, 76, 49, 80, 53}
    Dim angles2() As Double = {340, 310, 340, 210, 30, 300}

    ' The labels on the angular axis (spokes)
    Dim labels() As String = {"North", "North<*br*>East", "East", "South<*br*>East", "South", _
        "South<*br*>West", "West", "North<*br*>West"}

    ' Create a PolarChart object of size 460 x 460 pixels
    Dim c As PolarChart = New PolarChart(460, 460)

    ' Add a title to the chart at the top left corner using 15pt Arial Bold Italic font
    c.addTitle2(Chart.TopLeft, "<*underline=2*>Plants in Wonderland", "Arial Bold Italic", 15)

    ' Set center of plot area at (230, 240) with radius 180 pixels
    c.setPlotArea(230, 240, 180)

    ' Use alternative light grey/dark grey sector background color
    c.setPlotAreaBg(&Hdddddd, &Heeeeee, False)

    ' Set the grid style to circular grid
    c.setGridStyle(False)

    ' Add a legend box at the top right corner of the chart using 9pt Arial Bold font
    c.addLegend(459, 0, True, "Arial Bold", 9).setAlignment(Chart.TopRight)

    ' Set angular axis as 0 - 360, either 8 spokes
    c.angularAxis().setLinearScale2(0, 360, labels)

    ' Set the radial axis label format
    c.radialAxis().setLabelFormat("{value} km")

    ' Add a blue (0xff) polar line layer to the chart using (data0, angle0)
    Dim layer0 As PolarLineLayer = c.addLineLayer(data0, &H0000ff, "Immortal Weed")
    layer0.setAngles(angles0)

    layer0.setLineWidth(0)
    layer0.setDataSymbol(Chart.TriangleSymbol, 11)

    ' Add a red (0xff0000) polar line layer to the chart using (data1, angles1)
    Dim layer1 As PolarLineLayer = c.addLineLayer(data1, &Hff0000, "Precious Flower")
    layer1.setAngles(angles1)

    ' Disable the line by setting its width to 0, so only the symbols are visible
    layer1.setLineWidth(0)

    ' Use a 11 pixel diamond data point symbol
    layer1.setDataSymbol(Chart.DiamondSymbol, 11)

    ' Add a green (0x00ff00) polar line layer to the chart using (data2, angles2)
    Dim layer2 As PolarLineLayer = c.addLineLayer(data2, &H00ff00, "Magical Tree")
    layer2.setAngles(angles2)

    ' Disable the line by setting its width to 0, so only the symbols are visible
    layer2.setLineWidth(0)

    ' Use a 9 pixel square data point symbol
    layer2.setDataSymbol(Chart.SquareSymbol, 9)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{dataSetName} at ({value} km, {angle} deg)'")

End Sub

</script>

<html>
<head>
    <title>Polar Scatter Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Polar Scatter Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

