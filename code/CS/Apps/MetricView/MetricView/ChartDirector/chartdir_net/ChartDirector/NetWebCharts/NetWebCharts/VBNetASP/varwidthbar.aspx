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
    Dim data() As Double = {800, 600, 1000, 1400}
    Dim widths() As Double = {250, 500, 960, 460}
    Dim labels() As String = {"Wind", "Hydro", "Coal", "Natural Gas"}

    ' The colors to use
    Dim colors() As Integer = {&H00aa00, &H66aaee, &Hee6622, &Hffbb00}

    ' Create a XYChart object of size 500 x 350 pixels
    Dim c As XYChart = New XYChart(500, 350)

    ' Add a title to the chart using 15pt Arial Italic font
    c.addTitle("Energy Generation Breakdown", "Arial Italic", 15)

    ' Set the plotarea at (60, 60) and of (chart_width - 90) x (chart_height - 100) in size. Use a
    ' vertical gradient color from light blue (f9f9ff) to sky blue (aaccff) as background. Set grid
    ' lines to white (ffffff).
    Dim plotAreaBgColor As Integer = c.linearGradientColor(0, 60, 0, c.getHeight() - 40, &Haaccff, _
        &Hf9fcff)
    c.setPlotArea(60, 60, c.getWidth() - 90, c.getHeight() - 100, plotAreaBgColor, -1, -1, _
        &Hffffff)

    ' Add a legend box at (50, 30) using horizontal layout and transparent background.
    c.addLegend(55, 30, False).setBackground(Chart.Transparent)

    ' Add titles to x/y axes with 10 points Arial Bold font
    c.xAxis().setTitle("Mega Watts", "Arial Bold", 10)
    c.yAxis().setTitle("Cost per MWh (dollars)", "Arial Bold", 10)

    ' Set the x axis rounding to false, so that the x-axis will fit the data exactly
    c.xAxis().setRounding(False, False)

    ' In ChartDirector, there is no bar layer that can have variable bar widths, but you may create
    ' a bar using an area layer. (A bar can be considered as the area under a rectangular outline.)
    ' So by using a loop to create one bar per area layer, we can achieve a variable width bar
    ' chart.

    ' starting position of current bar
    Dim currentX As Double = 0

    For i As Integer = 0 To UBound(data)
        ' ending position of current bar
        Dim nextX As Double = currentX + widths(i)

        ' outline of the bar
        Dim dataX() As Double = {currentX, currentX, nextX, nextX}
        Dim dataY() As Double = {0, data(i), data(i), 0}

        ' create the area layer to fill the bar
        Dim layer As AreaLayer = c.addAreaLayer(dataY, colors(i), labels(i))
        layer.setXData(dataX)

        ' Tool tip for the layer
        layer.setHTMLImageMap("", "", "title='" & labels(i) & ": " & widths(i) & " MW at $" & _
            data(i) & " per MWh'")

        ' the ending position becomes the starting position of the next bar
        currentX = nextX
    Next

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("")

End Sub

</script>

<html>
<head>
    <title>Variable Width Bar Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Variable Width Bar Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

