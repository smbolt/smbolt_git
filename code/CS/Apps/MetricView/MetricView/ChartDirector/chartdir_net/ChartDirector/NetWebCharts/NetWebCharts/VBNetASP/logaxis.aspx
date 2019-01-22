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
    Dim data() As Double = {100, 125, 260, 147, 67}
    Dim labels() As String = {"Mon", "Tue", "Wed", "Thu", "Fri"}

    ' Create a XYChart object of size 200 x 180 pixels
    Dim c As XYChart = New XYChart(200, 180)

    ' Set the plot area at (30, 10) and of size 140 x 130 pixels
    c.setPlotArea(30, 10, 140, 130)

    ' Ise log scale axis if required
    If chartIndex = 1 Then
        c.yAxis().setLogScale3()
    End If

    ' Set the labels on the x axis
    c.xAxis().setLabels(labels)

    ' Add a color bar layer using the given data. Use a 1 pixel 3D border for the bars.
    c.addBarLayer3(data).setBorderColor(-1, 1)

    ' Output the chart
    viewer.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    viewer.ImageMap = c.getHTMLImageMap("", "", "title='Mileage on {xLabel}: {value} miles'")

End Sub

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    createChart(WebChartViewer0, 0)
    createChart(WebChartViewer1, 1)

End Sub

</script>

<html>
<head>
    <title>Log Scale Axis</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Log Scale Axis
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer0" runat="server" />
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

