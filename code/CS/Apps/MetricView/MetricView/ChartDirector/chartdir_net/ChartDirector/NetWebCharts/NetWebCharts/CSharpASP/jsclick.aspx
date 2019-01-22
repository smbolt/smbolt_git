<%@ Page Language="C#" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<script runat="server">

//
// Page Load event handler
//
protected void Page_Load(object sender, EventArgs e)
{
    //
    // For demo purpose, we use hard coded data. In real life, the following data could come from a
    // database.
    //
    double[] revenue = {4500, 5600, 6300, 8000, 12000, 14000, 16000, 20000, 24000, 28000};
    double[] grossMargin = {62, 65, 63, 60, 55, 56, 57, 53, 52, 50};
    double[] backLog = {563, 683, 788, 941, 1334, 1522, 1644, 1905, 2222, 2544};
    string[] labels = {"1996", "1997", "1998", "1999", "2000", "2001", "2002", "2003", "2004",
        "2005"};

    // Create a XYChart object of size 600 x 360 pixels
    XYChart c = new XYChart(600, 360);

    // Add a title to the chart using 18pt Times Bold Italic font
    c.addTitle("Annual Revenue for Star Tech", "Times New Roman Bold Italic", 18);

    // Set the plotarea at (60, 40) and of size 480 x 280 pixels. Use a vertical gradient color from
    // light green (eeffee) to dark green (008800) as background. Set border and grid lines to white
    // (ffffff).
    c.setPlotArea(60, 40, 480, 280, c.linearGradientColor(60, 40, 60, 280, 0xeeffee, 0x008800), -1,
        0xffffff, 0xffffff);

    // Add a multi-color bar chart layer using the revenue data.
    BarLayer layer = c.addBarLayer3(revenue);

    // Set cylinder bar shape
    layer.setBarShape(Chart.CircleShape);

    // Add extra field to the bars. These fields are used for showing additional information.
    layer.addExtraField2(grossMargin);
    layer.addExtraField2(backLog);

    // Set the labels on the x axis.
    c.xAxis().setLabels(labels);

    // In this example, we show the same scale using both axes
    c.syncYAxis();

    // Set the axis line to transparent
    c.xAxis().setColors(Chart.Transparent);
    c.yAxis().setColors(Chart.Transparent);
    c.yAxis2().setColors(Chart.Transparent);

    // Set the axis label to using 8pt Arial Bold as font
    c.yAxis().setLabelStyle("Arial Bold", 8);
    c.yAxis2().setLabelStyle("Arial Bold", 8);
    c.xAxis().setLabelStyle("Arial Bold", 8);

    // Add title to the y axes
    c.yAxis().setTitle("USD (millions)", "Arial Bold", 10);
    c.yAxis2().setTitle("USD (millions)", "Arial Bold", 10);

    // Create the image and save it in a temporary location
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG);

    // Client side Javascript to show detail information "onmouseover"
    string showText = "onmouseover='showInfo(\"{xLabel}\", {value}, {field0}, {field1});' ";

    // Client side Javascript to hide detail information "onmouseout"
    string hideText = "onmouseout='showInfo(null);' ";

    // "title" attribute to show tool tip
    string toolTip = "title='{xLabel}: US$ {value|0}M'";

    // Create an image map for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("xystub.aspx", "", showText + hideText + toolTip);
}

</script>

<!DOCTYPE html>
<html>
<head>
    <title>Javascript Clickable Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
<script type="text/javascript">
//
//Client side script function to show and hide detail information.
//
function showInfo(year, revenue, grossMargin, backLog)
{
    var obj = document.getElementById('detailInfo');

    if (!year)
    {
        obj.style.visibility = "hidden";
        return;
    }

    var content = "<table border='1' cellpadding='3' style='font:10pt verdana; " +
        "background-color:#CCCCFF' width='480px'>";
    content += "<tr><td><b>Year</b></td><td width='300px'>" + year + "</td></tr>";
    content += "<tr><td><b>Revenue</b></td><td>US$ " + revenue + "M</td></tr>";
    content += "<tr><td><b>Gross Margin</b></td><td>" + grossMargin + "%</td></tr>";
    content += "<tr><td><b>Back Log</b></td><td>US$ " + backLog + "M</td></tr>";
    content += "</table>";

    obj.innerHTML = content;
    obj.style.visibility = "visible";
}
</script>
<div style="font:bold 18pt verdana;">
    Javascript Clickable Chart
</div>
<hr style="border:solid 1px #000080" />
<div style="font:10pt verdana; margin-bottom:20px">
    <a href='viewsource.aspx?file=<%=Request["SCRIPT_NAME"]%>'>View Source Code</a>
</div>
<chart:WebChartViewer id="WebChartViewer1" runat="server" />
<div id="detailInfo" style="margin-left:60px"></div>
</body>
</html>
