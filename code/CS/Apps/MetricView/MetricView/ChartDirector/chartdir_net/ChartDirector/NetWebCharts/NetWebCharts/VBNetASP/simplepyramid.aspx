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

    ' Create a PyramidChart object of size 360 x 360 pixels
    Dim c As PyramidChart = New PyramidChart(360, 360)

    ' Set the pyramid center at (180, 180), and width x height to 150 x 180 pixels
    c.setPyramidSize(180, 180, 150, 300)

    ' Set the pyramid data and labels
    c.setData(data, labels)

    ' Add labels at the center of the pyramid layers using Arial Bold font. The labels will have two
    ' lines showing the layer name and percentage.
    c.setCenterLabel("{label}<*br*>{percent}%", "Arial Bold")

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{label}: US$ {value}M ({percent}%)'")

End Sub

</script>

<html>
<head>
    <title>Simple Pyramid Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Simple Pyramid Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

