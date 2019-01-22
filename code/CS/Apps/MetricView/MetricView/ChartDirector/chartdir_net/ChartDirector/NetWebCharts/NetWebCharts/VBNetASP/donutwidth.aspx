<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Create chart
'
Protected Sub createChart(viewer As WebChartViewer, chartIndex As Integer)

    ' Determine the donut inner radius (as percentage of outer radius) based on input parameter
    Dim donutRadius As Integer = chartIndex * 25

    ' The data for the pie chart
    Dim data() As Double = {10, 10, 10, 10, 10}

    ' The labels for the pie chart
    Dim labels() As String = {"Marble", "Wood", "Granite", "Plastic", "Metal"}

    ' Create a PieChart object of size 150 x 120 pixels, with a grey (EEEEEE) background, black
    ' border and 1 pixel 3D border effect
    Dim c As PieChart = New PieChart(150, 120, &Heeeeee, &H000000, 1)

    ' Set donut center at (75, 65) and the outer radius to 50 pixels. Inner radius is computed
    ' according donutWidth
    c.setDonutSize(75, 60, 50, Int(50 * donutRadius / 100))

    ' Add a title to show the donut width
    c.addTitle("Inner Radius = " & donutRadius & " %", "Arial", 10).setBackground(&Hcccccc, 0)

    ' Draw the pie in 3D
    c.set3D(12)

    ' Set the pie data and the pie labels
    c.setData(data, labels)

    ' Disable the sector labels by setting the color to Transparent
    c.setLabelStyle("", 8, Chart.Transparent)

    ' Output the chart
    viewer.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    viewer.ImageMap = c.getHTMLImageMap("", "", "title='{label}: {value}kg ({percent}%)'")

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

End Sub

</script>

<html>
<head>
    <title>Donut Width</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Donut Width
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
</body>
</html>

