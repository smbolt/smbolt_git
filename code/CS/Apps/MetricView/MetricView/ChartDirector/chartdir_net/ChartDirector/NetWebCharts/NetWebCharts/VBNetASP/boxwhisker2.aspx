<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' Sample data for the Box-Whisker chart. Represents the minimum, 1st quartile, medium, 3rd
    ' quartile and maximum values of some quantities
    Dim Q0Data() As Double = {40, 45, 40, 30, 20, 50, 25, 44}
    Dim Q1Data() As Double = {55, 60, 50, 40, 38, 60, 51, 60}
    Dim Q2Data() As Double = {62, 70, 60, 50, 48, 70, 62, 70}
    Dim Q3Data() As Double = {70, 80, 65, 60, 53, 78, 69, 76}
    Dim Q4Data() As Double = {80, 90, 75, 70, 60, 85, 80, 84}

    ' The labels for the chart
    Dim labels() As String = {"A", "B", "C", "D", "E", "F", "G", "H"}

    ' Create a XYChart object of size 450 x 400 pixels
    Dim c As XYChart = New XYChart(450, 400)

    ' Set the plotarea at (50, 30) and of size 380 x 340 pixels, with transparent background and
    ' border and light grey (0xcccccc) horizontal grid lines
    c.setPlotArea(50, 30, 380, 340, Chart.Transparent, -1, Chart.Transparent, &Hcccccc)

    ' Add a title box using grey (0x555555) 18pt Arial font
    Dim title As ChartDirector.TextBox = c.addTitle("     Pattern Recognition Accuracy", "Arial", _
        18, &H555555)

    ' Set the x and y axis stems to transparent and the label font to 12pt Arial
    c.xAxis().setColors(Chart.Transparent)
    c.yAxis().setColors(Chart.Transparent)
    c.xAxis().setLabelStyle("Arial", 12)
    c.yAxis().setLabelStyle("Arial", 12)

    ' Set the labels on the x axis
    c.xAxis().setLabels(labels)

    ' For the automatic y-axis labels, set the minimum spacing to 30 pixels.
    c.yAxis().setTickDensity(30)

    ' Add a box whisker layer using light blue (0x99ccee) for the fill color and blue (0x6688aa) for
    ' the whisker color. Set line width to 2 pixels. Use rounded corners and bar lighting effect.
    Dim b As BoxWhiskerLayer = c.addBoxWhiskerLayer(Q3Data, Q1Data, Q4Data, Q0Data, Q2Data, _
        &H99ccee, &H6688aa)
    b.setLineWidth(2)
    b.setRoundedCorners()
    b.setBorderColor(Chart.Transparent, Chart.barLighting())

    ' Adjust the plot area to fit under the title with 10-pixel margin on the other three sides.
    c.packPlotArea(10, title.getHeight(), c.getWidth() - 10, c.getHeight() - 10)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='[{xLabel}] min/med/max = {min}/{med}/{max}" & vbLf & _
        "Inter-quartile range: {bottom} to {top}'")

End Sub

</script>

<html>
<head>
    <title>Box-Whisker Chart (2)</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Box-Whisker Chart (2)
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

