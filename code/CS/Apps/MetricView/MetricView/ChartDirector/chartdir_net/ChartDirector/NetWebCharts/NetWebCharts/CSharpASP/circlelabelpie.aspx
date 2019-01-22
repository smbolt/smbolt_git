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
    double[] data = {42, 18, 8};

    // The labels for the pie chart
    string[] labels = {"Agree", "Disagree", "Not Sure"};

    // The colors to use for the sectors
    int[] colors = {0x66ff66, 0xff6666, 0xffff00};

    // Create a PieChart object of size 300 x 300 pixels. Set the background to a gradient color
    // from blue (aaccff) to sky blue (ffffff), with a grey (888888) border. Use rounded corners and
    // soft drop shadow.
    PieChart c = new PieChart(300, 300);
    c.setBackground(c.linearGradientColor(0, 0, 0, c.getHeight() / 2, 0xaaccff, 0xffffff), 0x888888)
        ;
    c.setRoundedFrame();
    c.setDropShadow();

    if (chartIndex == 0) {
    //============================================================
    //    Draw a pie chart where the label is on top of the pie
    //============================================================

        // Set the center of the pie at (150, 150) and the radius to 120 pixels
        c.setPieSize(150, 150, 120);

        // Set the label position to -40 pixels from the perimeter of the pie (-ve means label is
        // inside the pie)
        c.setLabelPos(-40);

    } else {
    //============================================================
    //    Draw a pie chart where the label is outside the pie
    //============================================================

        // Set the center of the pie at (150, 150) and the radius to 80 pixels
        c.setPieSize(150, 150, 80);

        // Set the sector label position to be 20 pixels from the pie. Use a join line to connect
        // the labels to the sectors.
        c.setLabelPos(20, Chart.LineColor);

    }

    // Set the pie data and the pie labels
    c.setData(data, labels);

    // Set the sector colors
    c.setColors2(Chart.DataColor, colors);

    // Use local gradient shading, with a 1 pixel semi-transparent black (bb000000) border
    c.setSectorStyle(Chart.LocalGradientShading, unchecked((int)0xbb000000), 1);

    // Output the chart
    viewer.Image = c.makeWebImage(Chart.PNG);

    // Include tool tip for the chart
    viewer.ImageMap = c.getHTMLImageMap("", "", "title='{label}: {value} responses ({percent}%)'");
}

//
// Page Load event handler
//
protected void Page_Load(object sender, EventArgs e)
{
    createChart(WebChartViewer0, 0);
    createChart(WebChartViewer1, 1);
}

</script>

<html>
<head>
    <title>Circular Label Layout</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Circular Label Layout
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request["SCRIPT_NAME"]%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer0" runat="server" />
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

