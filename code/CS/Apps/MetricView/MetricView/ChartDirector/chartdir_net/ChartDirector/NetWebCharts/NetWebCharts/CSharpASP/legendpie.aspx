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
    double[] data = {25, 18, 15, 12, 8, 30, 35};

    // The labels for the pie chart
    string[] labels = {"Labor", "Licenses", "Taxes", "Legal", "Insurance", "Facilities",
        "Production"};

    // Create a PieChart object of size 450 x 270 pixels
    PieChart c = new PieChart(450, 270);

    // Set the center of the pie at (150, 100) and the radius to 80 pixels
    c.setPieSize(150, 135, 100);

    // add a legend box where the top left corner is at (330, 50)
    c.addLegend(330, 60);

    // modify the sector label format to show percentages only
    c.setLabelFormat("{percent}%");

    // Set the pie data and the pie labels
    c.setData(data, labels);

    // Use rounded edge shading, with a 1 pixel white (FFFFFF) border
    c.setSectorStyle(Chart.RoundedEdgeShading, 0xffffff, 1);

    // Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG);

    // Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", "title='{label}: US${value}K ({percent}%)'"
        );
}

</script>

<html>
<head>
    <title>Pie Chart with Legend (1)</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Pie Chart with Legend (1)
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request["SCRIPT_NAME"]%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

