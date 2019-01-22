<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The data for the pie chart
    Dim data() As Double = {72, 18, 15, 12}

    ' The labels for the pie chart
    Dim labels() As String = {"Labor", "Machinery", "Facilities", "Computers"}

    ' The depths for the sectors
    Dim depths() As Double = {30, 20, 10, 10}

    ' Create a PieChart object of size 360 x 300 pixels, with a light blue (DDDDFF) background and a
    ' 1 pixel 3D border
    Dim c As PieChart = New PieChart(360, 300, &Hddddff, -1, 1)

    ' Set the center of the pie at (180, 175) and the radius to 100 pixels
    c.setPieSize(180, 175, 100)

    ' Add a title box using 15pt Times Bold Italic font and blue (AAAAFF) as background color
    c.addTitle("Project Cost Breakdown", "Times New Roman Bold Italic", 15).setBackground(&Haaaaff)

    ' Set the pie data and the pie labels
    c.setData(data, labels)

    ' Draw the pie in 3D with variable 3D depths
    c.set3D2(depths)

    ' Set the start angle to 225 degrees may improve layout when the depths of the sector are sorted
    ' in descending order, because it ensures the tallest sector is at the back.
    c.setStartAngle(225)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{label}: US${value}K ({percent}%)'")

End Sub

</script>

<html>
<head>
    <title>Multi-Depth Pie Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Multi-Depth Pie Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

