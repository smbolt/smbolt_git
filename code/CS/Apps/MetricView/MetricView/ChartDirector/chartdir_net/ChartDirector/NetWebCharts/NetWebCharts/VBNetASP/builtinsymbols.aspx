<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' Some ChartDirector built-in symbols
    Dim symbols() As Integer = {Chart.CircleShape, Chart.GlassSphereShape, _
        Chart.GlassSphere2Shape, Chart.SolidSphereShape, Chart.SquareShape, Chart.DiamondShape, _
        Chart.TriangleShape, Chart.RightTriangleShape, Chart.LeftTriangleShape, _
        Chart.InvertedTriangleShape, Chart.StarShape(3), Chart.StarShape(4), Chart.StarShape(5), _
        Chart.StarShape(6), Chart.StarShape(7), Chart.StarShape(8), Chart.StarShape(9), _
        Chart.StarShape(10), Chart.PolygonShape(5), Chart.Polygon2Shape(5), Chart.PolygonShape(6), _
        Chart.Polygon2Shape(6), Chart.Polygon2Shape(7), Chart.CrossShape(0.1), Chart.CrossShape( _
        0.2), Chart.CrossShape(0.3), Chart.CrossShape(0.4), Chart.CrossShape(0.5), _
        Chart.CrossShape(0.6), Chart.CrossShape(0.7), Chart.Cross2Shape(0.1), Chart.Cross2Shape( _
        0.2), Chart.Cross2Shape(0.3), Chart.Cross2Shape(0.4), Chart.Cross2Shape(0.5), _
        Chart.Cross2Shape(0.6), Chart.Cross2Shape(0.7), Chart.ArrowShape(), Chart.ArrowShape(45), _
        Chart.ArrowShape(90, 0.5), Chart.ArrowShape(135, 0.5, 0.2), Chart.ArrowShape(180, 0.3, _
        0.2, 0.3), Chart.ArrowShape(225, 1, 0.5, 0.7), Chart.ArrowShape(270, 1, 0.5, 0.25), _
        Chart.ArrowShape(315, 0.5, 0.5, 0), Chart.ArrowShape(30, 0.5, 0.1, 0.6), Chart.ArrowShape( _
        210, 0.5, 0.1, 0.6), Chart.ArrowShape(330, 0.7, 0.1), Chart.ArrowShape(150, 0.7, 0.1)}

    ' Create a XYChart object of size 500 x 450 pixels
    Dim c As XYChart = New XYChart(500, 450)

    ' Set the plotarea at (55, 40) and of size 400 x 350 pixels, with a light grey border
    ' (0xc0c0c0). Turn on both horizontal and vertical grid lines with light grey color (0xc0c0c0)
    c.setPlotArea(55, 40, 400, 350, -1, -1, &Hc0c0c0, &Hc0c0c0, -1)

    ' Add a title to the chart using 18pt Times Bold Itatic font.
    c.addTitle("Built-in Symbols", "Times New Roman Bold Italic", 18)

    ' Set the axes line width to 3 pixels
    c.xAxis().setWidth(3)
    c.yAxis().setWidth(3)

    ' Ensure the ticks are at least 1 unit part (integer ticks)
    c.xAxis().setMinTickInc(1)
    c.yAxis().setMinTickInc(1)

    ' Add each symbol as a separate scatter layer.
    For i As Integer = 0 To UBound(symbols)
        c.addScatterLayer(New Double() {i Mod 7 + 1.0}, New Double() {Int(i / 7 + 1.0)}, "", _
            symbols(i), 17)
    Next

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", "title='(x, y) = ({x}, {value})'")

End Sub

</script>

<html>
<head>
    <title>Built-in Symbols</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Built-in Symbols
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

