<%@ Page Language="C#" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

//
// Create chart
//
protected void createChart(WebChartViewer viewer, int chartIndex)
{
    // The data for the pie chart
    double[] data = {18, 30, 20, 15};

    // The labels for the pie chart
    string[] labels = {"Labor", "Licenses", "Facilities", "Production"};

    // The colors to use for the sectors
    int[] colors = {0x66aaee, 0xeebb22, 0xbbbbbb, 0x8844ff};

    // Create a PieChart object of size 200 x 220 pixels. Use a vertical gradient color from blue
    // (0000cc) to deep blue (000044) as background. Use rounded corners of 16 pixels radius.
    PieChart c = new PieChart(200, 220);
    c.setBackground(c.linearGradientColor(0, 0, 0, c.getHeight(), 0x0000cc, 0x000044));
    c.setRoundedFrame(0xffffff, 16);

    // Set donut center at (100, 120), and outer/inner radii as 80/40 pixels
    c.setDonutSize(100, 120, 80, 40);

    // Set the pie data
    c.setData(data, labels);

    // Set the sector colors
    c.setColors2(Chart.DataColor, colors);

    // Demonstrates various shading modes
    if (chartIndex == 0) {
        c.addTitle("Default Shading", "bold", 12, 0xffffff);
    } else if (chartIndex == 1) {
        c.addTitle("Local Gradient", "bold", 12, 0xffffff);
        c.setSectorStyle(Chart.LocalGradientShading);
    } else if (chartIndex == 2) {
        c.addTitle("Global Gradient", "bold", 12, 0xffffff);
        c.setSectorStyle(Chart.GlobalGradientShading);
    } else if (chartIndex == 3) {
        c.addTitle("Concave Shading", "bold", 12, 0xffffff);
        c.setSectorStyle(Chart.ConcaveShading);
    } else if (chartIndex == 4) {
        c.addTitle("Rounded Edge", "bold", 12, 0xffffff);
        c.setSectorStyle(Chart.RoundedEdgeShading);
    } else if (chartIndex == 5) {
        c.addTitle("Radial Gradient", "bold", 12, 0xffffff);
        c.setSectorStyle(Chart.RadialShading);
    } else if (chartIndex == 6) {
        c.addTitle("Ring Shading", "bold", 12, 0xffffff);
        c.setSectorStyle(Chart.RingShading);
    }

    // Disable the sector labels by setting the color to Transparent
    c.setLabelStyle("", 8, Chart.Transparent);

    // Output the chart
    viewer.Image = c.makeWebImage(Chart.PNG);

    // Include tool tip for the chart
    viewer.ImageMap = c.getHTMLImageMap("", "", "title='{label}: US${value}K ({percent}%)'");
}

//
// Page Load event handler
//
protected void Page_Load(object sender, EventArgs e)
{
    createChart(WebChartViewer0, 0);
    createChart(WebChartViewer1, 1);
    createChart(WebChartViewer2, 2);
    createChart(WebChartViewer3, 3);
    createChart(WebChartViewer4, 4);
    createChart(WebChartViewer5, 5);
    createChart(WebChartViewer6, 6);
}

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
        <a href='viewsource.aspx?file=<%=Request["SCRIPT_NAME"]%>'>View Source Code</a>
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

