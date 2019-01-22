<%@ Page Language="C#" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

//
// Page Load event handler
//
protected void Page_Load(object sender, EventArgs e)
{
    // The data for the bar chart
    double[] data0 = {100, 125, 245, 147, 67};
    double[] data1 = {85, 156, 179, 211, 123};
    double[] data2 = {97, 87, 56, 267, 157};

    // The labels for the bar chart
    string[] labels = {"Mon", "Tue", "Wed", "Thu", "Fri"};

    // Create a XYChart object of size 500 x 320 pixels
    XYChart c = new XYChart(500, 320);

    // Set the plotarea at (100, 40) and of size 280 x 240 pixels
    c.setPlotArea(100, 40, 280, 240);

    // Add a legend box at (405, 100)
    c.addLegend(405, 100);

    // Add a title to the chart
    c.addTitle("Weekday Network Load");

    // Add a title to the y axis. Draw the title upright (font angle = 0)
    c.yAxis().setTitle("Average\nWorkload\n(MBytes\nPer Hour)").setFontAngle(0);

    // Set the labels on the x axis
    c.xAxis().setLabels(labels);

    // Add three bar layers, each representing one data set. The bars are drawn in semi-transparent
    // colors.
    c.addBarLayer(data0, unchecked((int)0x808080ff), "Server # 1", 5);
    c.addBarLayer(data1, unchecked((int)0x80ff0000), "Server # 2", 5);
    c.addBarLayer(data2, unchecked((int)0x8000ff00), "Server # 3", 5);

    // Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG);

    // Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "",
        "title='{dataSetName} on {xLabel}: {value} MBytes/hour'");
}

</script>

<html>
<head>
    <title>Depth Bar Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Depth Bar Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request["SCRIPT_NAME"]%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

