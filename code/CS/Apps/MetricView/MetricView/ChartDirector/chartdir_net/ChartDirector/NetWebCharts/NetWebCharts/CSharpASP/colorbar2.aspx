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
    double[] data = {85, 156, 179, 211, 123, 189, 166};

    // The labels for the bar chart
    string[] labels = {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"};

    // The colors for the bars
    int[] colors = {0x5588bb, 0x66bbbb, 0xaa6644, 0x99bb55, 0xee9944, 0x444466, 0xbb5555};

    // Create a XYChart object of size 600 x 360 pixels
    XYChart c = new XYChart(600, 360);

    // Set default text color to dark grey (0x333333)
    c.setColor(Chart.TextColor, 0x333333);

    // Set the plotarea at (70, 20) and of size 500 x 300 pixels, with transparent background and
    // border and light grey (0xcccccc) horizontal grid lines
    c.setPlotArea(70, 20, 500, 300, Chart.Transparent, -1, Chart.Transparent, 0xcccccc);

    // Set the x and y axis stems to transparent and the label font to 12pt Arial
    c.xAxis().setColors(Chart.Transparent);
    c.yAxis().setColors(Chart.Transparent);
    c.xAxis().setLabelStyle("Arial", 12);
    c.yAxis().setLabelStyle("Arial", 12);

    // Add a multi-color bar chart layer using the given data
    BarLayer layer = c.addBarLayer3(data, colors);

    // Use bar gradient lighting with the light intensity from 0.8 to 1.15
    layer.setBorderColor(Chart.Transparent, Chart.barLighting(0.8, 1.15));

    // Set rounded corners for bars
    layer.setRoundedCorners();

    // Set the labels on the x axis.
    c.xAxis().setLabels(labels);

    // For the automatic y-axis labels, set the minimum spacing to 40 pixels.
    c.yAxis().setTickDensity(40);

    // Add a title to the y axis using dark grey (0x555555) 14pt Arial font
    c.yAxis().setTitle("Y-Axis Title Placeholder", "Arial", 14, 0x555555);

    // Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG);

    // Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", "title='{xLabel}: ${value}M'");
}

</script>

<html>
<head>
    <title>Multi-Color Bar Chart (2)</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Multi-Color Bar Chart (2)
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request["SCRIPT_NAME"]%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

