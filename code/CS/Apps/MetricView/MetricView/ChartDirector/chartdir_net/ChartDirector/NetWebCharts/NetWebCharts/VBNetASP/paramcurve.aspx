<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The XY data of the first data series
    Dim dataX0() As Double = {10, 35, 17, 4, 22, 29, 45, 52, 63, 39}
    Dim dataY0() As Double = {2.0, 3.2, 2.7, 1.2, 2.8, 2.9, 3.1, 3.0, 2.3, 3.3}

    ' The XY data of the second data series
    Dim dataX1() As Double = {30, 35, 17, 4, 22, 59, 43, 52, 63, 39}
    Dim dataY1() As Double = {1.0, 1.3, 0.7, 0.6, 0.8, 3.0, 1.8, 2.3, 3.4, 1.5}

    ' The XY data of the third data series
    Dim dataX2() As Double = {28, 35, 15, 10, 22, 60, 46, 64, 39}
    Dim dataY2() As Double = {2.0, 2.2, 1.2, 0.4, 1.8, 2.7, 2.4, 2.8, 2.4}

    ' Create a XYChart object of size 540 x 480 pixels
    Dim c As XYChart = New XYChart(540, 480)

    ' Set the plotarea at (70, 65) and of size 400 x 350 pixels, with white background and a light
    ' grey border (0xc0c0c0). Turn on both horizontal and vertical grid lines with light grey color
    ' (0xc0c0c0)
    c.setPlotArea(70, 65, 400, 350, &Hffffff, -1, &Hc0c0c0, &Hc0c0c0, -1)

    ' Add a legend box with the top center point anchored at (270, 30). Use horizontal layout. Use
    ' 10pt Arial Bold Italic font. Set the background and border color to Transparent.
    Dim legendBox As LegendBox = c.addLegend(270, 30, False, "Arial Bold Italic", 10)
    legendBox.setAlignment(Chart.TopCenter)
    legendBox.setBackground(Chart.Transparent, Chart.Transparent)

    ' Add a title to the chart using 18 point Times Bold Itatic font.
    c.addTitle("Parametric Curve Fitting", "Times New Roman Bold Italic", 18)

    ' Add titles to the axes using 12pt Arial Bold Italic font
    c.yAxis().setTitle("Axis Title Placeholder", "Arial Bold Italic", 12)
    c.xAxis().setTitle("Axis Title Placeholder", "Arial Bold Italic", 12)

    ' Set the axes line width to 3 pixels
    c.yAxis().setWidth(3)
    c.xAxis().setWidth(3)

    ' Add a scatter layer using (dataX0, dataY0)
    c.addScatterLayer(dataX0, dataY0, "Polynomial", Chart.GlassSphere2Shape, 11, &Hff0000)

    ' Add a degree 2 polynomial trend line layer for (dataX0, dataY0)
    Dim trend0 As TrendLayer = c.addTrendLayer2(dataX0, dataY0, &Hff0000)
    trend0.setLineWidth(3)
    trend0.setRegressionType(Chart.PolynomialRegression(2))
    trend0.setHTMLImageMap("{disable}")

    ' Add a scatter layer for (dataX1, dataY1)
    c.addScatterLayer(dataX1, dataY1, "Exponential", Chart.GlassSphere2Shape, 11, &H00aa00)

    ' Add an exponential trend line layer for (dataX1, dataY1)
    Dim trend1 As TrendLayer = c.addTrendLayer2(dataX1, dataY1, &H00aa00)
    trend1.setLineWidth(3)
    trend1.setRegressionType(Chart.ExponentialRegression)
    trend1.setHTMLImageMap("{disable}")

    ' Add a scatter layer using (dataX2, dataY2)
    c.addScatterLayer(dataX2, dataY2, "Logarithmic", Chart.GlassSphere2Shape, 11, &H0000ff)

    ' Add a logarithmic trend line layer for (dataX2, dataY2)
    Dim trend2 As TrendLayer = c.addTrendLayer2(dataX2, dataY2, &H0000ff)
    trend2.setLineWidth(3)
    trend2.setRegressionType(Chart.LogarithmicRegression)
    trend2.setHTMLImageMap("{disable}")

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", "title='[{dataSetName}] ({x}, {value})'")

End Sub

</script>

<html>
<head>
    <title>Parametric Curve Fitting</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Parametric Curve Fitting
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

