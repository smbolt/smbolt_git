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
    Dim colors() As Integer = {&H400000cc, &H4066aaee, &H40ffbb00, &H40ee6622}

    ' Create a PyramidChart object of size 450 x 400 pixels
    Dim c As PyramidChart = New PyramidChart(450, 400)

    ' Set the pyramid center at (220, 180), and width x height to 150 x 300 pixels
    c.setPyramidSize(220, 180, 150, 300)

    ' Set the elevation to 15 degrees and rotation to 75 degrees
    c.setViewAngle(15, 75)

    ' Set the pyramid data and labels
    c.setData(data, labels)

    ' Set the layer colors to the given colors
    c.setColors2(Chart.DataColor, colors)

    ' Leave 1% gaps between layers
    c.setLayerGap(0.01)

    ' Add a legend box at (320, 60), with light grey (eeeeee) background and grey (888888) border.
    ' Set the top-left and bottom-right corners to rounded corners of 10 pixels radius.
    Dim legendBox As LegendBox = c.addLegend(320, 60)
    legendBox.setBackground(&Heeeeee, &H888888)
    legendBox.setRoundedCorners(10, 0, 10, 0)

    ' Add labels at the center of the pyramid layers using Arial Bold font. The labels will show the
    ' percentage of the layers.
    c.setCenterLabel("{percent}%", "Arial Bold")

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{label}: US$ {value}M ({percent}%)'")

End Sub

</script>

<html>
<head>
    <title>Rotated Pyramid Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Rotated Pyramid Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

