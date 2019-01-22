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
    double[] data = {18, 45, 28};

    // The labels for the pie chart
    string[] labels = {"Marble", "Wood", "Granite"};

    // The icons for the sectors
    string[] texture = {"marble3.png", "wood.png", "rock.png"};

    // Create a PieChart object of size 400 x 330 pixels, with a metallic green (88EE88) background,
    // black border and 1 pixel 3D border effect
    PieChart c = new PieChart(400, 330, Chart.metalColor(0x88ee88), 0x000000, 1);

    //Set default directory for loading images from current script directory
    c.setSearchPath(Server.MapPath("."));

    // Set donut center at (200, 160), and outer/inner radii as 120/60 pixels
    c.setDonutSize(200, 160, 120, 60);

    // Add a title box using 15pt Times Bold Italic font and metallic deep green (008000) background
    // color
    c.addTitle("Material Composition", "Times New Roman Bold Italic", 15).setBackground(
        Chart.metalColor(0x008000));

    // Set the pie data and the pie labels
    c.setData(data, labels);

    // Set the colors of the sectors to the 3 texture patterns
    c.setColor(Chart.DataColor + 0, c.patternColor2(texture[0]));
    c.setColor(Chart.DataColor + 1, c.patternColor2(texture[1]));
    c.setColor(Chart.DataColor + 2, c.patternColor2(texture[2]));

    // Draw the pie in 3D with a 3D depth of 30 pixels
    c.set3D(30);

    // Use 12pt Arial Bold Italic as the sector label font
    c.setLabelStyle("Arial Bold Italic", 12);

    // Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG);

    // Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", "title='{label}: {value}kg ({percent}%)'");
}

</script>

<html>
<head>
    <title>Texture Donut Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Texture Donut Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request["SCRIPT_NAME"]%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

