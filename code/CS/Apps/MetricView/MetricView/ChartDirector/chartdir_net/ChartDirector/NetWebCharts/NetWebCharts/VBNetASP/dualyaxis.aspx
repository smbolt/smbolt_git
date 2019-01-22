<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The data for the chart
    Dim data0() As Double = {0.05, 0.06, 0.48, 0.1, 0.01, 0.05}
    Dim data1() As Double = {100, 125, 265, 147, 67, 105}
    Dim labels() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun"}

    ' Create a XYChart object of size 300 x 180 pixels
    Dim c As XYChart = New XYChart(300, 180)

    ' Set the plot area at (50, 20) and of size 200 x 130 pixels
    c.setPlotArea(50, 20, 200, 130)

    ' Add a title to the chart using 8pt Arial Bold font
    c.addTitle("Independent Y-Axis Demo", "Arial Bold", 8)

    ' Set the labels on the x axis.
    c.xAxis().setLabels(labels)

    ' Add a title to the primary (left) y axis
    c.yAxis().setTitle("Packet Drop Rate (pps)")

    ' Set the axis, label and title colors for the primary y axis to red (0xc00000) to match the
    ' first data set
    c.yAxis().setColors(&Hc00000, &Hc00000, &Hc00000)

    ' Add a title to the secondary (right) y axis
    c.yAxis2().setTitle("Throughtput (MBytes)")

    ' set the axis, label and title colors for the primary y axis to green (0x008000) to match the
    ' second data set
    c.yAxis2().setColors(&H008000, &H008000, &H008000)

    ' Add a line layer to for the first data set using red (0xc00000) color with a line width to 3
    ' pixels
    Dim lineLayer As LineLayer = c.addLineLayer(data0, &Hc00000)
    lineLayer.setLineWidth(3)

    ' tool tip for the line layer
    lineLayer.setHTMLImageMap("", "", "title='Packet Drop Rate on {xLabel}: {value} pps'")

    ' Add a bar layer to for the second data set using green (0x00C000) color. Bind the second data
    ' set to the secondary (right) y axis
    Dim barLayer As BarLayer = c.addBarLayer(data1, &H00c000)
    barLayer.setUseYAxis2()

    ' tool tip for the bar layer
    barLayer.setHTMLImageMap("", "", "title='Throughput on {xLabel}: {value} MBytes'")

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("")

End Sub

</script>

<html>
<head>
    <title>Dual Y-Axis</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Dual Y-Axis
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

