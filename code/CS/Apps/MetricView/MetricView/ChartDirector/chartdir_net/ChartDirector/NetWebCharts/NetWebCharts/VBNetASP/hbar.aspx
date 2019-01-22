<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The data for the bar chart
    Dim data() As Double = {3.9, 8.1, 10.9, 14.2, 18.1, 19.0, 21.2, 23.2, 25.7, 36}

    ' The labels for the bar chart
    Dim labels() As String = {"Bastic Group", "Simpa", "YG Super", "CID", "Giga Tech", _
        "Indo Digital", "Supreme", "Electech", "THP Thunder", "Flash Light"}

    ' Create a XYChart object of size 600 x 250 pixels
    Dim c As XYChart = New XYChart(600, 250)

    ' Add a title to the chart using Arial Bold Italic font
    c.addTitle("Revenue Estimation - Year 2002", "Arial Bold Italic")

    ' Set the plotarea at (100, 30) and of size 400 x 200 pixels. Set the plotarea border,
    ' background and grid lines to Transparent
    c.setPlotArea(100, 30, 400, 200, Chart.Transparent, Chart.Transparent, Chart.Transparent, _
        Chart.Transparent, Chart.Transparent)

    ' Add a bar chart layer using the given data. Use a gradient color for the bars, where the
    ' gradient is from dark green (0x008000) to white (0xffffff)
    Dim layer As BarLayer = c.addBarLayer(data, c.gradientColor(100, 0, 500, 0, &H008000, &Hffffff))

    ' Swap the axis so that the bars are drawn horizontally
    c.swapXY(True)

    ' Set the bar gap to 10%
    layer.setBarGap(0.1)

    ' Use the format "US$ xxx millions" as the bar label
    layer.setAggregateLabelFormat("US$ {value} millions")

    ' Set the bar label font to 10pt Times Bold Italic/dark red (0x663300)
    layer.setAggregateLabelStyle("Times New Roman Bold Italic", 10, &H663300)

    ' Set the labels on the x axis
    Dim textbox As ChartDirector.TextBox = c.xAxis().setLabels(labels)

    ' Set the x axis label font to 10pt Arial Bold Italic
    textbox.setFontStyle("Arial Bold Italic")
    textbox.setFontSize(10)

    ' Set the x axis to Transparent, with labels in dark red (0x663300)
    c.xAxis().setColors(Chart.Transparent, &H663300)

    ' Set the y axis and labels to Transparent
    c.yAxis().setColors(Chart.Transparent, Chart.Transparent)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", "title='{xLabel}: US${value} millions'")

End Sub

</script>

<html>
<head>
    <title>Borderless Bar Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Borderless Bar Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

