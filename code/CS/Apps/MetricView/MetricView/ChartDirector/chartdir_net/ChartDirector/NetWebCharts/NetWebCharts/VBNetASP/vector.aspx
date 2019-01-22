<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The (x, y) coordinates of the starting point of the vectors
    Dim dataX() As Double = {20, 40, 60, 80, 100, 20, 40, 60, 80, 100, 20, 40, 58, 62, 80, 100, _
        20, 40, 60, 80, 100, 20, 40, 60, 80, 100}
    Dim dataY() As Double = {20, 20, 20, 20, 20, 40, 40, 40, 40, 40, 60, 60, 60, 60, 60, 60, 80, _
        80, 80, 80, 80, 100, 100, 100, 100, 100}

    ' The lengths (radii) and directions (angles) of the vectors
    Dim dataR() As Double = {6, 6, 9, 6, 6, 6, 9, 12, 9, 6, 9, 12, 12, 12, 12, 9, 6, 9, 12, 9, 6, _
        6, 6, 9, 6, 6}
    Dim dataA() As Double = {-45, -30, 0, 30, 45, -60, -45, 0, 45, 60, -90, -90, -90, 90, 90, 90, _
        -120, -135, 180, 135, 120, -135, -150, 180, 150, 135}

    ' Create a XYChart object of size 450 x 390 pixels
    Dim c As XYChart = New XYChart(450, 390)

    ' Set the plotarea at (55, 40) and of size 350 x 300 pixels, with a light grey border
    ' (0xc0c0c0). Turn on both horizontal and vertical grid lines with light grey color (0xc0c0c0)
    c.setPlotArea(55, 40, 350, 300, -1, -1, &Hc0c0c0, &Hc0c0c0, -1)

    ' Add a title to the chart using 18pt Times Bold Itatic font.
    c.addTitle("Vector Chart Demonstration", "Times New Roman Bold Italic", 18)

    ' Add a title to the y axis using 12pt Arial Bold Italic font
    c.yAxis().setTitle("Y Axis Title", "Arial Bold Italic", 12)

    ' Add a title to the x axis using 12pt Arial Bold Italic font
    c.xAxis().setTitle("X Axis Title", "Arial Bold Italic", 12)

    ' Set the axes line width to 3 pixels
    c.xAxis().setWidth(3)
    c.yAxis().setWidth(3)

    ' Add a vector layer to the chart using blue (0000CC) color, with vector arrow size set to 11
    ' pixels
    c.addVectorLayer(dataX, dataY, dataR, dataA, Chart.YAxisScale, &H0000cc).setArrowHead(11)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='Vector at ({x}, {value}): Length = {len}, Angle = {dir} deg'")

End Sub

</script>

<html>
<head>
    <title>Vector Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Vector Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

