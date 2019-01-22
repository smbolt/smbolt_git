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
    double value = 74.25;

    // Create a LinearMeter object of size 70 x 260 pixels with very light grey (0xeeeeee)
    // backgruond and a grey (0xbbbbbb) 3-pixel thick rounded frame
    LinearMeter m = new LinearMeter(70, 260, 0xeeeeee, 0xbbbbbb);
    m.setRoundedFrame(Chart.Transparent);
    m.setThickFrame(3);

    // Set the scale region top-left corner at (28, 33), with size of 20 x 194 pixels. The scale
    // labels are located on the left (default - implies vertical meter).
    m.setMeter(28, 33, 20, 194);

    // Set meter scale from 0 - 100, with a tick every 10 units
    m.setScale(0, 100, 10);

    // Demostrate different types of color scales
    double[] smoothColorScale = {0, 0x0000ff, 25, 0x0088ff, 50, 0x00ff00, 75, 0xdddd00, 100,
        0xff0000};
    double[] stepColorScale = {0, 0x00dd00, 50, 0xffff00, 80, 0xff0000, 100};
    double[] highColorScale = {70, Chart.Transparent, 100, 0xff0000};
    double[] lowColorScale = {0, 0x0000ff, 30, Chart.Transparent};

    if (chartIndex == 0) {
        // Add a blue (0x0088ff) bar from 0 to value with glass effect and 4 pixel rounded corners
        m.addBar(0, value, 0x0088ff, Chart.glassEffect(Chart.NormalGlare, Chart.Left), 4);
        // Add a 6-pixel thick smooth color scale at x = 53 (right of meter scale)
        m.addColorScale(smoothColorScale, 53, 6);
    } else if (chartIndex == 1) {
         // Add a green (0x00cc00) bar from 0 to value with bar lighting effect and 4 pixel rounded
         // corners
        m.addBar(0, value, 0x00cc00, Chart.barLighting(), 4);
        // Add a high only color scale at x = 52 (right of meter scale) with thickness varying from
        // 0 to 8
        m.addColorScale(highColorScale, 52, 0, 52, 8);
        // Add a low only color scale at x = 52 (right of meter scale) with thickness varying from 8
        // to 0
        m.addColorScale(lowColorScale, 52, 8, 52, 0);
    } else if (chartIndex == 2) {
        // Add a purple (0x0088ff) bar from 0 to value with glass effect and 4 pixel rounded corners
        m.addBar(0, value, 0x8833dd, Chart.glassEffect(Chart.NormalGlare, Chart.Left), 4);
        // Add a high only color scale at x = 52 (right of meter scale) with thickness varying from
        // 0 to 8
        m.addColorScale(highColorScale, 52, 0, 52, 8);
    } else if (chartIndex == 3) {
         // Add a orange (0xff8800) bar from 0 to value with cylinder lighting effect
        m.addBar(0, value, 0xff8800, Chart.cylinderEffect());
        // Add a high only color scale at x = 53 (right of meter scale)
        m.addColorScale(highColorScale, 53, 6);
    } else if (chartIndex == 4) {
        // Add a red (0xee3333) bar from 0 to value with glass effect and 4 pixel rounded corners
        m.addBar(0, value, 0xee3333, Chart.glassEffect(Chart.NormalGlare, Chart.Left), 4);
        // Add a step color scale at x = 53 (right of meter scale)
        m.addColorScale(stepColorScale, 53, 6);
    } else {
         // Add a grey (0xaaaaaa) bar from 0 to value
        m.addBar(0, value, 0xaaaaaa);
        // Add a smooth color scale at x = 52 (right of meter scale) with thickness varying from 0
        // to 8
        m.addColorScale(smoothColorScale, 52, 0, 52, 8);
    }

    // Add a title using 8pt Arial Bold font with grey (0xbbbbbb) background
    m.addTitle("Temp C", "Arial Bold", 8, Chart.TextColor).setBackground(0xbbbbbb);

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
    <title>White Vertical Bar Meters</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        White Vertical Bar Meters
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

