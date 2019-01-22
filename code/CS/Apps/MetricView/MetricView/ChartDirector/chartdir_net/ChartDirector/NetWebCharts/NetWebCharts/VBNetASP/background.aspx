<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Create chart
'
Protected Sub createChart(viewer As WebChartViewer, chartIndex As Integer)

    ' The data for the chart
    Dim data() As Double = {85, 156, 179.5, 211, 123}
    Dim labels() As String = {"Mon", "Tue", "Wed", "Thu", "Fri"}

    ' Create a XYChart object of size 270 x 270 pixels
    Dim c As XYChart = New XYChart(270, 270)

    ' Set the plot area at (40, 32) and of size 200 x 200 pixels
    Dim plotarea As PlotArea = c.setPlotArea(40, 32, 200, 200)

    ' Set the background style based on the input parameter
    If chartIndex = 0 Then
        ' Has wallpaper image
        c.setWallpaper(Server.MapPath("tile.gif"))
    ElseIf chartIndex = 1 Then
        ' Use a background image as the plot area background
        plotarea.setBackground2(Server.MapPath("bg.png"))
    ElseIf chartIndex = 2 Then
        ' Use white (0xffffff) and grey (0xe0e0e0) as two alternate plotarea background colors
        plotarea.setBackground(&Hffffff, &He0e0e0)
    Else
        ' Use a dark background palette
        c.setColors(Chart.whiteOnBlackPalette)
    End If

    ' Set the labels on the x axis
    c.xAxis().setLabels(labels)

    ' Add a color bar layer using the given data. Use a 1 pixel 3D border for the bars.
    c.addBarLayer3(data).setBorderColor(-1, 1)

    ' Output the chart
    viewer.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    viewer.ImageMap = c.getHTMLImageMap("", "", "title='Revenue for {xLabel}: US${value}K'")

End Sub

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    createChart(WebChartViewer0, 0)
    createChart(WebChartViewer1, 1)
    createChart(WebChartViewer2, 2)
    createChart(WebChartViewer3, 3)

End Sub

</script>

<html>
<head>
    <title>Background and Wallpaper</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Background and Wallpaper
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer0" runat="server" />
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
    <chart:WebChartViewer id="WebChartViewer2" runat="server" />
    <chart:WebChartViewer id="WebChartViewer3" runat="server" />
</body>
</html>

