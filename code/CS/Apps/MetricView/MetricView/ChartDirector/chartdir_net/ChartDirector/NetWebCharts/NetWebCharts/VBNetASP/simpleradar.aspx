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
    Dim data() As Double = {90, 60, 65, 75, 40}

    ' The labels for the chart
    Dim labels() As String = {"Speed", "Reliability", "Comfort", "Safety", "Efficiency"}

    ' Create a PolarChart object of size 450 x 350 pixels
    Dim c As PolarChart = New PolarChart(450, 350)

    ' Set center of plot area at (225, 185) with radius 150 pixels
    c.setPlotArea(225, 185, 150)

    ' Add an area layer to the polar chart
    c.addAreaLayer(data, &H9999ff)

    ' Set the labels to the angular axis as spokes
    c.angularAxis().setLabels(labels)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", "title='{label}: score = {value}'")

End Sub

</script>

<html>
<head>
    <title>Simple Radar Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Simple Radar Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

