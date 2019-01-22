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
    // The value to display on the meter
    double value = 75.35;

    // Create a LinearMeter object of size 250 x 75 pixels with black background and rounded corners
    LinearMeter m = new LinearMeter(250, 75, 0x000000);
    m.setRoundedFrame(Chart.Transparent);

    // Set the default text and line colors to white (0xffffff)
    m.setColor(Chart.TextColor, 0xffffff);
    m.setColor(Chart.LineColor, 0xffffff);

    // Set the scale region top-left corner at (14, 23), with size of 218 x 20 pixels. The scale
    // labels are located on the top (implies horizontal meter)
    m.setMeter(14, 23, 218, 20, Chart.Top);

    // Set meter scale from 0 - 100, with a tick every 10 units
    m.setScale(0, 100, 10);

    // The tick line width to 1 pixel
    m.setLineWidth(0, 1);

    // Demostrate different types of color scales and putting them at different positions
    double[] smoothColorScale = {0, 0x0000ff, 25, 0x0088ff, 50, 0x00ff00, 75, 0xdddd00, 100,
        0xff0000};
    double[] stepColorScale = {0, 0x00cc00, 50, 0xeecc00, 80, 0xdd0000, 100};
    double[] highLowColorScale = {0, 0x0000ff, 70, Chart.Transparent, 100, 0xff0000};

    if (chartIndex == 0) {
        // Add the smooth color scale at the default position
        m.addColorScale(smoothColorScale);
    } else if (chartIndex == 1) {
        // Add the smooth color scale starting at y = 23 (top of scale) with zero width and ending
        // at y = 23 with 20 pixels width
        m.addColorScale(smoothColorScale, 23, 0, 23, 20);
    } else if (chartIndex == 2) {
        // Add the high low scale at the default position
        m.addColorScale(highLowColorScale);
    } else if (chartIndex == 3) {
        // Add the smooth color scale starting at y = 33 (center of scale) with zero width and
        // ending at y = 23 with 20 pixels width
        m.addColorScale(smoothColorScale, 33, 0, 23, 20);
    } else if (chartIndex == 4) {
        // Add the step color scale at the default position
        m.addColorScale(stepColorScale);
    } else {
        // Add the smooth color scale starting at y = 43 (bottom of scale) with zero width and
        // ending at y = 23 with 20 pixels width
        m.addColorScale(smoothColorScale, 43, 0, 23, 20);
    }

    // Add a blue (0x0000cc) pointer with white (0xffffff) border at the specified value
    m.addPointer(value, 0x0000cc, 0xffffff);

    // Add a label left aligned to (10, 61) using 8pt Arial Bold font
    m.addText(10, 61, "Temperature C", "Arial Bold", 8, Chart.TextColor, Chart.Left);

    // Add a text box right aligned to (235, 61). Display the value using white (0xffffff) 8pt Arial
    // Bold font on a black (0x000000) background with depressed grey (0x444444) rounded border.
    ChartDirector.TextBox t = m.addText(235, 61, m.formatValue(value, "2"), "Arial Bold", 8,
        0xffffff, Chart.Right);
    t.setBackground(0x000000, 0x444444, -1);
    t.setRoundedCorners(3);

    // Output the chart
    viewer.Image = m.makeWebImage(Chart.PNG);
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
}

</script>

<html>
<head>
    <title>Black Horizontal Linear Meters</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Black Horizontal Linear Meters
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
</body>
</html>

