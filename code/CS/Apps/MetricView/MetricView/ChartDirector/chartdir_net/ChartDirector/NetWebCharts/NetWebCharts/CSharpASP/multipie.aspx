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
    double[] data0 = {25, 18, 15};
    double[] data1 = {14, 32, 24};
    double[] data2 = {25, 23, 9};

    // The labels for the pie chart
    string[] labels = {"Software", "Hardware", "Services"};

    // Create a PieChart object of size 180 x 160 pixels
    PieChart c = new PieChart(180, 160);

    // Set the center of the pie at (90, 80) and the radius to 60 pixels
    c.setPieSize(90, 80, 60);

    // Set the border color of the sectors to white (ffffff)
    c.setLineColor(0xffffff);

    // Set the background color of the sector label to pale yellow (ffffc0) with a black border
    // (000000)
    c.setLabelStyle().setBackground(0xffffc0, 0x000000);

    // Set the label to be slightly inside the perimeter of the circle
    c.setLabelLayout(Chart.CircleLayout, -10);

    // Set the title, data and colors according to which pie to draw
    if (chartIndex == 0) {
        c.addTitle("Alpha Division", "Arial Bold", 8);
        c.setData(data0, labels);
        c.setColors2(Chart.DataColor, new int[] {0xff3333, 0xff9999, 0xffcccc});
    } else if (chartIndex == 1) {
        c.addTitle("Beta Division", "Arial Bold", 8);
        c.setData(data1, labels);
        c.setColors2(Chart.DataColor, new int[] {0x33ff33, 0x99ff99, 0xccffcc});
    } else {
        c.addTitle("Gamma Division", "Arial Bold", 8);
        c.setData(data2, labels);
        c.setColors2(Chart.DataColor, new int[] {0x3333ff, 0x9999ff, 0xccccff});
    }

    // Output the chart
    viewer.Image = c.makeWebImage(Chart.PNG);

    // Include tool tip for the chart
    viewer.ImageMap = c.getHTMLImageMap("", "", "title='{label}: US${value}M ({percent}%)'");
}

//
// Page Load event handler
//
protected void Page_Load(object sender, EventArgs e)
{
    createChart(WebChartViewer0, 0);
    createChart(WebChartViewer1, 1);
    createChart(WebChartViewer2, 2);
}

</script>

<html>
<head>
    <title>Multi-Pie Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Multi-Pie Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request["SCRIPT_NAME"]%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer0" runat="server" />
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
    <chart:WebChartViewer id="WebChartViewer2" runat="server" />
</body>
</html>

