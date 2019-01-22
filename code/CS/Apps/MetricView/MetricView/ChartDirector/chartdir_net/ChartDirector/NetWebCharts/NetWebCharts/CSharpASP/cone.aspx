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
    // The data for the pyramid chart
    double[] data = {156, 123, 211, 179};

    // The labels for the pyramid chart
    string[] labels = {"Funds", "Bonds", "Stocks", "Cash"};

    // The semi-transparent colors for the pyramid layers
    int[] colors = {0x60000088, 0x6066aaee, 0x60ffbb00, 0x60ee6622};

    // Create a PyramidChart object of size 480 x 400 pixels
    PyramidChart c = new PyramidChart(480, 400);

    // Set the cone center at (280, 180), and width x height to 150 x 300 pixels
    c.setConeSize(280, 180, 150, 300);

    // Set the elevation to 15 degrees
    c.setViewAngle(15);

    // Set the pyramid data and labels
    c.setData(data, labels);

    // Set the layer colors to the given colors
    c.setColors2(Chart.DataColor, colors);

    // Leave 1% gaps between layers
    c.setLayerGap(0.01);

    // Add labels at the left side of the pyramid layers using Arial Bold font. The labels will have
    // 3 lines showing the layer name, value and percentage.
    c.setLeftLabel("{label}\nUS ${value}K\n({percent}%)", "Arial Bold");

    // Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG);

    // Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "",
        "title='{label}: US$ {value}K ({percent}%)'");
}

</script>

<html>
<head>
    <title>Cone Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Cone Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request["SCRIPT_NAME"]%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

