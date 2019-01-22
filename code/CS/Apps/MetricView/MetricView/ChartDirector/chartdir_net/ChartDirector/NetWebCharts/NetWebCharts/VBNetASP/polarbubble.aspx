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
    Dim data0() As Double = {6, 12.5, 18.2, 15}
    Dim angles0() As Double = {45, 96, 169, 258}
    Dim size0() As Double = {41, 105, 12, 20}

    Dim data1() As Double = {18, 16, 11, 14}
    Dim angles1() As Double = {30, 210, 240, 310}
    Dim size1() As Double = {30, 45, 12, 90}

    ' Create a PolarChart object of size 460 x 460 pixels
    Dim c As PolarChart = New PolarChart(460, 460)

    ' Add a title to the chart at the top left corner using 15pt Arial Bold Italic font
    c.addTitle2(Chart.TopLeft, "<*underline=2*>EM Field Strength", "Arial Bold Italic", 15)

    ' Set center of plot area at (230, 240) with radius 180 pixels
    c.setPlotArea(230, 240, 180)

    ' Use alternative light grey/dark grey circular background color
    c.setPlotAreaBg(&Hdddddd, &Heeeeee)

    ' Set the grid style to circular grid
    c.setGridStyle(False)

    ' Add a legend box at the top right corner of the chart using 9pt Arial Bold font
    c.addLegend(459, 0, True, "Arial Bold", 9).setAlignment(Chart.TopRight)

    ' Set angular axis as 0 - 360, with a spoke every 30 units
    c.angularAxis().setLinearScale(0, 360, 30)

    ' Set the radial axis label format
    c.radialAxis().setLabelFormat("{value} km")

    ' Add a blue (0x9999ff) line layer to the chart using (data0, angle0)
    Dim layer0 As PolarLineLayer = c.addLineLayer(data0, &H9999ff, "Cold Spot")
    layer0.setAngles(angles0)

    ' Disable the line by setting its width to 0, so only the symbols are visible
    layer0.setLineWidth(0)

    ' Use a circular data point symbol
    layer0.setDataSymbol(Chart.CircleSymbol, 11)

    ' Modulate the symbol size by size0 to produce a bubble chart effect
    layer0.setSymbolScale(size0)

    ' Add a red (0xff9999) line layer to the chart using (data1, angle1)
    Dim layer1 As PolarLineLayer = c.addLineLayer(data1, &Hff9999, "Hot Spot")
    layer1.setAngles(angles1)

    ' Disable the line by setting its width to 0, so only the symbols are visible
    layer1.setLineWidth(0)

    ' Use a circular data point symbol
    layer1.setDataSymbol(Chart.CircleSymbol, 11)

    ' Modulate the symbol size by size1 to produce a bubble chart effect
    layer1.setSymbolScale(size1)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{dataSetName} at ({value} km, {angle} deg)" & vbLf & "Strength = {z} Watt'")

End Sub

</script>

<html>
<head>
    <title>Polar Bubble Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Polar Bubble Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

