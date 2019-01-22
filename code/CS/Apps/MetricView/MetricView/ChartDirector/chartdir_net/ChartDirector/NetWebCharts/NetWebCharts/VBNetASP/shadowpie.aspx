<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Create chart
'
Protected Sub createChart(viewer As WebChartViewer, chartIndex As Integer)

    ' the tilt angle of the pie
    Dim angle As Integer = chartIndex * 90 + 45

    ' The data for the pie chart
    Dim data() As Double = {25, 18, 15, 12, 8, 30, 35}

    ' The labels for the pie chart
    Dim labels() As String = {"Labor", "Licenses", "Taxes", "Legal", "Insurance", "Facilities", _
        "Production"}

    ' Create a PieChart object of size 110 x 110 pixels
    Dim c As PieChart = New PieChart(110, 110)

    ' Set the center of the pie at (50, 55) and the radius to 36 pixels
    c.setPieSize(55, 55, 36)

    ' Set the depth, tilt angle and 3D mode of the 3D pie (-1 means auto depth, "true" means the 3D
    ' effect is in shadow mode)
    c.set3D(-1, angle, True)

    ' Add a title showing the shadow angle
    c.addTitle("Shadow @ " & angle & " deg", "Arial", 8)

    ' Set the pie data
    c.setData(data, labels)

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

End Sub

</script>

<html>
<head>
    <title>3D Shadow Mode</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        3D Shadow Mode
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

