<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Create chart
'
Protected Sub createChart(viewer As WebChartViewer, chartIndex As Integer)

    Dim bargap As Double = chartIndex * 0.25 - 0.25

    ' The data for the bar chart
    Dim data() As Double = {100, 125, 245, 147, 67}

    ' The labels for the bar chart
    Dim labels() As String = {"Mon", "Tue", "Wed", "Thu", "Fri"}

    ' Create a XYChart object of size 150 x 150 pixels
    Dim c As XYChart = New XYChart(150, 150)

    ' Set the plotarea at (27, 20) and of size 120 x 100 pixels
    c.setPlotArea(27, 20, 120, 100)

    ' Set the labels on the x axis
    c.xAxis().setLabels(labels)

    If bargap >= 0 Then
        ' Add a title to display to bar gap using 8pt Arial font
        c.addTitle("      Bar Gap = " & bargap, "Arial", 8)
    Else
        ' Use negative value to mean TouchBar
        c.addTitle("      Bar Gap = TouchBar", "Arial", 8)
        bargap = Chart.TouchBar
    End If

    ' Add a bar chart layer using the given data and set the bar gap
    c.addBarLayer(data).setBarGap(bargap)

    ' Output the chart
    viewer.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    viewer.ImageMap = c.getHTMLImageMap("", "", "title='Production on {xLabel}: {value} kg'")

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
    <title>Bar Gap</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Bar Gap
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

