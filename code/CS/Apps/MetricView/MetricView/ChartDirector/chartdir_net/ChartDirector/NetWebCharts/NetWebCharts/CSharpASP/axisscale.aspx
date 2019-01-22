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
    // The data for the chart
    double[] data = {5.5, 3.5, -3.7, 1.7, -1.4, 3.3};
    string[] labels = {"Jan", "Feb", "Mar", "Apr", "May", "Jun"};

    // Create a XYChart object of size 200 x 190 pixels
    XYChart c = new XYChart(200, 190);

    // Set the plot area at (30, 20) and of size 140 x 140 pixels
    c.setPlotArea(30, 20, 140, 140);

    // Configure the axis as according to the input parameter
    if (chartIndex == 0) {
        c.addTitle("No Axis Extension", "Arial", 8);
    } else if (chartIndex == 1) {
        c.addTitle("Top/Bottom Extensions = 0/0", "Arial", 8);
        // Reserve 20% margin at top of plot area when auto-scaling
        c.yAxis().setAutoScale(0, 0);
    } else if (chartIndex == 2) {
        c.addTitle("Top/Bottom Extensions = 0.2/0.2", "Arial", 8);
        // Reserve 20% margin at top and bottom of plot area when auto-scaling
        c.yAxis().setAutoScale(0.2, 0.2);
    } else if (chartIndex == 3) {
        c.addTitle("Axis Top Margin = 15", "Arial", 8);
        // Reserve 15 pixels at top of plot area
        c.yAxis().setMargin(15);
    } else {
        c.addTitle("Manual Scale -5 to 10", "Arial", 8);
        // Set the y axis to scale from -5 to 10, with ticks every 5 units
        c.yAxis().setLinearScale(-5, 10, 5);
    }

    // Set the labels on the x axis
    c.xAxis().setLabels(labels);

    // Add a color bar layer using the given data. Use a 1 pixel 3D border for the bars.
    c.addBarLayer3(data).setBorderColor(-1, 1);

    // Output the chart
    viewer.Image = c.makeWebImage(Chart.PNG);

    // Include tool tip for the chart
    viewer.ImageMap = c.getHTMLImageMap("", "", "title='ROI for {xLabel}: {value}%'");
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
}

</script>

<html>
<head>
    <title>Y-Axis Scaling</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Y-Axis Scaling
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
</body>
</html>

