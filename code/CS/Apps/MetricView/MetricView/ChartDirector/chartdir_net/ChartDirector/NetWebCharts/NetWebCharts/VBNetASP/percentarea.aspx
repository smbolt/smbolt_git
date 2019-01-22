<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The data for the area chart
    Dim data0() As Double = {42, 49, 33, 38, 51, 46, 29, 41, 44, 57, 59, 52, 37, 34, 51, 56, 56, _
        60, 70, 76, 63, 67, 75, 64, 51}
    Dim data1() As Double = {50, 55, 47, 34, 42, 49, 63, 62, 73, 59, 56, 50, 64, 60, 67, 67, 58, _
        59, 73, 77, 84, 82, 80, 84, 98}
    Dim data2() As Double = {87, 89, 85, 66, 53, 39, 24, 21, 37, 56, 37, 23, 21, 33, 13, 17, 14, _
        23, 16, 25, 29, 30, 45, 47, 46}

    ' The timestamps on the x-axis
    Dim labels() As Date = {DateSerial(1996, 1, 1), DateSerial(1996, 4, 1), DateSerial(1996, 7, 1 _
        ), DateSerial(1996, 10, 1), DateSerial(1997, 1, 1), DateSerial(1997, 4, 1), DateSerial( _
        1997, 7, 1), DateSerial(1997, 10, 1), DateSerial(1998, 1, 1), DateSerial(1998, 4, 1), _
        DateSerial(1998, 7, 1), DateSerial(1998, 10, 1), DateSerial(1999, 1, 1), DateSerial(1999, _
        4, 1), DateSerial(1999, 7, 1), DateSerial(1999, 10, 1), DateSerial(2000, 1, 1), _
        DateSerial(2000, 4, 1), DateSerial(2000, 7, 1), DateSerial(2000, 10, 1), DateSerial(2001, _
        1, 1), DateSerial(2001, 4, 1), DateSerial(2001, 7, 1), DateSerial(2001, 10, 1), _
        DateSerial(2002, 1, 1)}

    ' Create a XYChart object of size 500 x 280 pixels, using 0xffffcc as background color, with a
    ' black border, and 1 pixel 3D border effect
    Dim c As XYChart = New XYChart(500, 280, &Hffffcc, 0, 1)

    ' Set default directory for loading images from current script directory
    Call c.setSearchPath(Server.MapPath("."))

    ' Set the plotarea at (50, 45) and of size 320 x 200 pixels with white background. Enable
    ' horizontal and vertical grid lines using the grey (0xc0c0c0) color.
    c.setPlotArea(50, 45, 320, 200, &Hffffff).setGridColor(&Hc0c0c0, &Hc0c0c0)

    ' Add a legend box at (370, 45) using vertical layout and 8 points Arial Bold font.
    Dim legendBox As LegendBox = c.addLegend(370, 45, True, "Arial Bold", 8)

    ' Set the legend box background and border to transparent
    legendBox.setBackground(Chart.Transparent, Chart.Transparent)

    ' Set the legend box icon size to 16 x 32 pixels to match with custom icon size
    legendBox.setKeySize(16, 32)

    ' Add a title to the chart using 14 points Times Bold Itatic font and white font color, and
    ' 0x804020 as the background color
    c.addTitle("Quarterly Product Sales", "Times New Roman Bold Italic", 14, &Hffffff _
        ).setBackground(&H804020)

    ' Set the labels on the x axis.
    c.xAxis().setLabels2(labels)

    ' Set multi-style axis label formatting. Start of year labels are displayed as yyyy. For other
    ' labels, just show minor tick.
    c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "{value|yyyy}", Chart.AllPassFilter(), "-")

    ' Add a percentage area layer to the chart
    Dim layer As AreaLayer = c.addAreaLayer2(Chart.Percentage)

    ' Add the three data sets to the area layer, using icons images with labels as data set names
    layer.addDataSet(data0, &H40ddaa77, _
        "<*block,valign=absmiddle*><*img=service.png*> Service<*/*>")
    layer.addDataSet(data1, &H40aadd77, _
        "<*block,valign=absmiddle*><*img=software.png*> Software<*/*>")
    layer.addDataSet(data2, &H40aa77dd, _
        "<*block,valign=absmiddle*><*img=computer.png*> Hardware<*/*>")

    ' For a vertical stacked chart with positive data only, the last data set is always on top.
    ' However, in a vertical legend box, the last data set is at the bottom. This can be reversed by
    ' using the setLegend method.
    layer.setLegend(Chart.ReverseLegend)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{dataSetName} sales at {xLabel|yyyy} Q{=({xLabel|m}+2)/3|0}: US${value}K " & _
        "({percent}%)'")

End Sub

</script>

<html>
<head>
    <title>Percentage Area Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Percentage Area Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

