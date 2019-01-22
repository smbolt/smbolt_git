<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The data for the bar chart
    Dim data() As Double = {85, 156, 179.5, 211, 123}

    ' The labels for the bar chart
    Dim labels() As String = {"Mon", "Tue", "Wed", "Thu", "Fri"}

    ' Create a XYChart object of size 250 x 250 pixels
    Dim c As XYChart = New XYChart(250, 250)

    ' Set the plotarea at (30, 20) and of size 200 x 200 pixels
    c.setPlotArea(30, 20, 200, 200)

    ' Add a bar chart layer using the given data
    c.addBarLayer(data)

    ' Set the labels on the x axis.
    c.xAxis().setLabels(labels)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", "title='{xLabel}: {value} GBytes'")

End Sub

</script>

<html>
<head>
    <title>Simple Bar Chart (1)</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Simple Bar Chart (1)
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

