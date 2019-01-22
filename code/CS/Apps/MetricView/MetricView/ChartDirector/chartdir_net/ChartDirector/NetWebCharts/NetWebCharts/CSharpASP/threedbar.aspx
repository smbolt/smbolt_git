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
    double[] data = {85, 156, 179.5, 211, 123};

    // The labels for the bar chart
    string[] labels = {"Mon", "Tue", "Wed", "Thu", "Fri"};

    // Create a XYChart object of size 300 x 280 pixels
    XYChart c = new XYChart(300, 280);

    // Set the plotarea at (45, 30) and of size 200 x 200 pixels
    c.setPlotArea(45, 30, 200, 200);

    // Add a title to the chart
    c.addTitle("Weekly Server Load");

    // Add a title to the y axis
    c.yAxis().setTitle("MBytes");

    // Add a title to the x axis
    c.xAxis().setTitle("Work Week 25");

    // Add a bar chart layer with green (0x00ff00) bars using the given data
    c.addBarLayer(data, 0x00ff00).set3D();

    // Set the labels on the x axis.
    c.xAxis().setLabels(labels);

    // Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG);

    // Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", "title='{xLabel}: {value} MBytes'");
}

</script>

<html>
<head>
    <title>3D Bar Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        3D Bar Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request["SCRIPT_NAME"]%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

