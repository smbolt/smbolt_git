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

    // Create a LinearMeter object of size 250 x 75 pixels with very light grey (0xeeeeee)
    // backgruond and a light grey (0xccccccc) 3-pixel thick rounded frame
    LinearMeter m = new LinearMeter(70, 260, 0xeeeeee, 0xcccccc);
    m.setRoundedFrame(Chart.Transparent);
    m.setThickFrame(3);

    // Set the scale region top-left corner at (28, 30), with size of 20 x 196 pixels. The scale
    // labels are located on the left (default - implies vertical meter)
    m.setMeter(28, 30, 20, 196);

    // Set meter scale from 0 - 100, with a tick every 10 units
    m.setScale(0, 100, 10);

    // Demostrate different types of color scales and putting them at different positions
    double[] smoothColorScale = {0, 0x6666ff, 25, 0x00bbbb, 50, 0x00ff00, 75, 0xffff00, 100,
        0xff0000};
    double[] stepColorScale = {0, 0x33ff33, 50, 0xffff33, 80, 0xff3333, 100};
    double[] highLowColorScale = {0, 0x6666ff, 70, Chart.Transparent, 100, 0xff0000};

    if (chartIndex == 0) {
        // Add the smooth color scale at the default position
        m.addColorScale(smoothColorScale);
    } else if (chartIndex == 1) {
        // Add the step color scale at the default position
        m.addColorScale(stepColorScale);
    } else if (chartIndex == 2) {
        // Add the high low scale at the default position
        m.addColorScale(highLowColorScale);
    } else if (chartIndex == 3) {
        // Add the smooth color scale starting at x = 28 (left of scale) with zero width and ending
        // at x = 28 with 20 pixels width
        m.addColorScale(smoothColorScale, 28, 0, 28, 20);
    } else if (chartIndex == 4) {
        // Add the smooth color scale starting at x = 38 (center of scale) with zero width and
        // ending at x = 28 with 20 pixels width
        m.addColorScale(smoothColorScale, 38, 0, 28, 20);
    } else {
        // Add the smooth color scale starting at x = 48 (right of scale) with zero width and ending
        // at x = 28 with 20 pixels width
        m.addColorScale(smoothColorScale, 48, 0, 28, 20);
    }

    // In this demo, we demostrate pointers of different shapes
    if (chartIndex < 3) {
        // Add a blue (0x0000cc) pointer of default shape at the specified value
        m.addPointer(value, 0x0000cc);
    } else {
        // Add a blue (0x0000cc) pointer of triangular shape the specified value
        m.addPointer(value, 0x0000cc).setShape(Chart.TriangularPointer, 0.7, 0.5);
    }

    // Add a title using 8pt Arial Bold font with a border color background
    m.addTitle("Temp C", "Arial Bold", 8, Chart.TextColor).setBackground(0xcccccc);

    // Add a text box at the bottom-center. Display the value using white (0xffffff) 8pt Arial Bold
    // font on a black (0x000000) background with rounded border.
    ChartDirector.TextBox t = m.addText(m.getWidth() / 2, m.getHeight() - 8, m.formatValue(value,
        "2"), "Arial Bold", 8, 0xffffff, Chart.Bottom);
    t.setBackground(0x000000);
    t.setRoundedCorners(3);
    t.setMargin2(5, 5, 2, 1);

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
    <title>White Vertical Linear Meters</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        White Vertical Linear Meters
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request["SCRIPT_NAME"]%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer0" runat="server" style='margin-right:25px;' />
    <chart:WebChartViewer id="WebChartViewer1" runat="server" style='margin-right:25px;' />
    <chart:WebChartViewer id="WebChartViewer2" runat="server" style='margin-right:25px;' />
    <chart:WebChartViewer id="WebChartViewer3" runat="server" style='margin-right:25px;' />
    <chart:WebChartViewer id="WebChartViewer4" runat="server" style='margin-right:25px;' />
    <chart:WebChartViewer id="WebChartViewer5" runat="server" />
</body>
</html>

