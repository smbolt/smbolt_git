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
    // The data for the pie chart
    double[] data = {72, 18, 15, 12};

    // The depths for the sectors
    double[] depths = {30, 20, 10, 10};

    // The labels for the pie chart
    string[] labels = {"Sunny", "Cloudy", "Rainy", "Snowy"};

    // The icons for the sectors
    string[] icons = {"sun.png", "cloud.png", "rain.png", "snowy.png"};

    // Create a PieChart object of size 400 x 300 pixels
    PieChart c = new PieChart(400, 300);

    // Use the semi-transparent palette for this chart
    c.setColors(Chart.transparentPalette);

    // Set the background to metallic light blue (CCFFFF), with a black border and 1 pixel 3D border
    // effect,
    c.setBackground(Chart.metalColor(0xccccff), 0x000000, 1);

    //Set default directory for loading images from current script directory
    c.setSearchPath(Server.MapPath("."));

    // Set donut center at (200, 175), and outer/inner radii as 100/50 pixels
    c.setDonutSize(200, 175, 100, 50);

    // Add a title box using 15pt Times Bold Italic font and metallic blue (8888FF) background color
    c.addTitle("Weather Profile in Wonderland", "Times New Roman Bold Italic", 15).setBackground(
        Chart.metalColor(0x8888ff));

    // Set the pie data and the pie labels
    c.setData(data, labels);

    // Add icons to the chart as a custom field
    c.addExtraField(icons);

    // Configure the sector labels using CDML to include the icon images
    c.setLabelFormat(
        "<*block,valign=absmiddle*><*img={field0}*> <*block*>{label}\n{percent}%<*/*><*/*>");

    // Draw the pie in 3D with variable 3D depths
    c.set3D2(depths);

    // Set the start angle to 225 degrees may improve layout when the depths of the sector are
    // sorted in descending order, because it ensures the tallest sector is at the back.
    c.setStartAngle(225);

    // Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG);

    // Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "",
        "title='{label}: {value} days ({percent}%)'");
}

</script>

<html>
<head>
    <title>Icon Donut Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Icon Donut Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request["SCRIPT_NAME"]%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

