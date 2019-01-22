<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Create chart
'
Protected Sub createChart(viewer As WebChartViewer, chartIndex As Integer)

    ' The data for the pyramid chart
    Dim data() As Double = {156, 123, 211, 179}

    ' The colors for the pyramid layers
    Dim colors() As Integer = {&H66aaee, &Heebb22, &Hcccccc, &Hcc88ff}

    ' The layer gap
    Dim gap As Double = chartIndex * 0.01

    ' Create a PyramidChart object of size 200 x 200 pixels, with white (ffffff) background and grey
    ' (888888) border
    Dim c As PyramidChart = New PyramidChart(200, 200, &Hffffff, &H888888)

    ' Set the pyramid center at (100, 100), and width x height to 60 x 120 pixels
    c.setPyramidSize(100, 100, 60, 120)

    ' Set the layer gap
    c.addTitle("Gap = " & gap, "Arial Italic", 15)
    c.setLayerGap(gap)

    ' Set the elevation to 15 degrees
    c.setViewAngle(15)

    ' Set the pyramid data
    c.setData(data)

    ' Set the layer colors to the given colors
    c.setColors2(Chart.DataColor, colors)

    ' Output the chart
    viewer.Image = c.makeWebImage(Chart.PNG)

End Sub

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    createChart(WebChartViewer0, 0)
    createChart(WebChartViewer1, 1)
    createChart(WebChartViewer2, 2)
    createChart(WebChartViewer3, 3)
    createChart(WebChartViewer4, 4)
    createChart(WebChartViewer5, 5)

End Sub

</script>

<html>
<head>
    <title>Pyramid Gap</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Pyramid Gap
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer0" runat="server" />
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
    <chart:WebChartViewer id="WebChartViewer2" runat="server" />
    <chart:WebChartViewer id="WebChartViewer3" runat="server" />
    <chart:WebChartViewer id="WebChartViewer4" runat="server" />
    <chart:WebChartViewer id="WebChartViewer5" runat="server" />
</body>
</html>

