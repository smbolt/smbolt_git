<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Create chart
'
Protected Sub createChart(viewer As WebChartViewer, chartIndex As Integer)

    ' The data for the pie chart
    Dim data() As Double = {18, 30, 20, 15}

    ' The labels for the pie chart
    Dim labels() As String = {"Labor", "Licenses", "Facilities", "Production"}

    ' The colors to use for the sectors
    Dim colors() As Integer = {&H66aaee, &Heebb22, &Hbbbbbb, &H8844ff}

    ' Create a PieChart object of size 200 x 220 pixels. Use a vertical gradient color from blue
    ' (0000cc) to deep blue (000044) as background. Use rounded corners of 16 pixels radius.
    Dim c As PieChart = New PieChart(200, 220)
    c.setBackground(c.linearGradientColor(0, 0, 0, c.getHeight(), &H0000cc, &H000044))
    c.setRoundedFrame(&Hffffff, 16)

    ' Set donut center at (100, 120), and outer/inner radii as 80/40 pixels
    c.setDonutSize(100, 120, 80, 40)

    ' Set the pie data
    c.setData(data, labels)

    ' Set the sector colors
    c.setColors2(Chart.DataColor, colors)

    ' Demonstrates various shading modes
    If chartIndex = 0 Then
        c.addTitle("Default Shading", "bold", 12, &Hffffff)
    ElseIf chartIndex = 1 Then
        c.addTitle("Local Gradient", "bold", 12, &Hffffff)
        c.setSectorStyle(Chart.LocalGradientShading)
    ElseIf chartIndex = 2 Then
        c.addTitle("Global Gradient", "bold", 12, &Hffffff)
        c.setSectorStyle(Chart.GlobalGradientShading)
    ElseIf chartIndex = 3 Then
        c.addTitle("Concave Shading", "bold", 12, &Hffffff)
        c.setSectorStyle(Chart.ConcaveShading)
    ElseIf chartIndex = 4 Then
        c.addTitle("Rounded Edge", "bold", 12, &Hffffff)
        c.setSectorStyle(Chart.RoundedEdgeShading)
    ElseIf chartIndex = 5 Then
        c.addTitle("Radial Gradient", "bold", 12, &Hffffff)
        c.setSectorStyle(Chart.RadialShading)
    ElseIf chartIndex = 6 Then
        c.addTitle("Ring Shading", "bold", 12, &Hffffff)
        c.setSectorStyle(Chart.RingShading)
    End If

    ' Disable the sector labels by setting the color to Transparent
    c.setLabelStyle("", 8, Chart.Transparent)

    ' Output the chart
    viewer.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    viewer.ImageMap = c.getHTMLImageMap("", "", "title='{label}: US${value}K ({percent}%)'")

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
    createChart(WebChartViewer6, 6)

End Sub

</script>

<html>
<head>
    <title>2D Donut Shading</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        2D Donut Shading
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
    <chart:WebChartViewer id="WebChartViewer6" runat="server" />
</body>
</html>

