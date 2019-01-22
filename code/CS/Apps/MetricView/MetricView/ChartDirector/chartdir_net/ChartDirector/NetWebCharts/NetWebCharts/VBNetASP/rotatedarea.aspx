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
    Dim data() As Double = {30, 28, 40, 55, 75, 68, 54, 60, 50, 62, 75, 65, 75, 89, 60, 55, 53, _
        35, 50, 66, 56, 48, 52, 65, 62}

    ' The labels for the area chart
    Dim labels() As Double = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, _
        19, 20, 21, 22, 23, 24}

    ' Create a XYChart object of size 320 x 320 pixels
    Dim c As XYChart = New XYChart(320, 320)

    ' Swap the x and y axis to become a rotated chart
    c.swapXY()

    ' Set the y axis on the top side (right + rotated = top)
    c.setYAxisOnRight()

    ' Reverse the x axis so it is pointing downwards
    c.xAxis().setReverse()

    ' Set the plotarea at (50, 50) and of size 200 x 200 pixels. Enable horizontal and vertical
    ' grids by setting their colors to grey (0xc0c0c0).
    c.setPlotArea(50, 50, 250, 250).setGridColor(&Hc0c0c0, &Hc0c0c0)

    ' Add a line chart layer using the given data
    c.addAreaLayer(data, c.gradientColor(50, 0, 300, 0, &Hffffff, &H0000ff))

    ' Set the labels on the x axis. Append "m" after the value to show the unit.
    c.xAxis().setLabels2(labels, "{value} m")

    ' Display 1 out of 3 labels.
    c.xAxis().setLabelStep(3)

    ' Add a title to the x axis
    c.xAxis().setTitle("Depth")

    ' Add a title to the y axis
    c.yAxis().setTitle("Carbon Dioxide Concentration (ppm)")

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='Carbon dioxide concentration at {xLabel}: {value} ppm'")

End Sub

</script>

<html>
<head>
    <title>Rotated Area Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Rotated Area Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

