<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The data for the pyramid chart
    Dim data() As Double = {156, 123, 211, 179}

    ' The labels for the pyramid chart
    Dim labels() As String = {"Funds", "Bonds", "Stocks", "Cash"}

    ' The semi-transparent colors for the pyramid layers
    Dim colors() As Integer = {&H60000088, &H6066aaee, &H60ffbb00, &H60ee6622}

    ' Create a PyramidChart object of size 480 x 400 pixels
    Dim c As PyramidChart = New PyramidChart(480, 400)

    ' Set the cone center at (280, 180), and width x height to 150 x 300 pixels
    c.setConeSize(280, 180, 150, 300)

    ' Set the elevation to 15 degrees
    c.setViewAngle(15)

    ' Set the pyramid data and labels
    c.setData(data, labels)

    ' Set the layer colors to the given colors
    c.setColors2(Chart.DataColor, colors)

    ' Leave 1% gaps between layers
    c.setLayerGap(0.01)

    ' Add labels at the left side of the pyramid layers using Arial Bold font. The labels will have
    ' 3 lines showing the layer name, value and percentage.
    c.setLeftLabel("{label}<*br*>US ${value}K<*br*>({percent}%)", "Arial Bold")

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{label}: US$ {value}K ({percent}%)'")

End Sub

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
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

