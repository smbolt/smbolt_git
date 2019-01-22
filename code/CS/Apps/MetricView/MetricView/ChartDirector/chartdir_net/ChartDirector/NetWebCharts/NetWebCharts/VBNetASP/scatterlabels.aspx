<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The XY points for the scatter chart
    Dim dataX() As Double = {150, 400, 300, 1500, 800}
    Dim dataY() As Double = {0.6, 8, 5.4, 2, 4}

    ' The labels for the points
    Dim labels() As String = {"Nano<*br*>100", "SpeedTron<*br*>200 Lite", "SpeedTron<*br*>200", _
        "Marathon<*br*>Extra", "Marathon<*br*>2000"}

    ' Create a XYChart object of size 450 x 400 pixels
    Dim c As XYChart = New XYChart(450, 400)

    ' Set the plotarea at (55, 40) and of size 350 x 300 pixels, with a light grey border
    ' (0xc0c0c0). Turn on both horizontal and vertical grid lines with light grey color (0xc0c0c0)
    c.setPlotArea(55, 40, 350, 300, &Hffffff, -1, &Hc0c0c0, &Hc0c0c0, -1)

    ' Add a title to the chart using 18pt Times Bold Itatic font.
    c.addTitle("Product Comparison Chart", "Times New Roman Bold Italic", 18)

    ' Add a title to the y axis using 12pt Arial Bold Italic font
    c.yAxis().setTitle("Capacity (tons)", "Arial Bold Italic", 12)

    ' Add a title to the x axis using 12pt Arial Bold Italic font
    c.xAxis().setTitle("Range (miles)", "Arial Bold Italic", 12)

    ' Set the axes line width to 3 pixels
    c.xAxis().setWidth(3)
    c.yAxis().setWidth(3)

    ' Add the data as a scatter chart layer, using a 15 pixel circle as the symbol
    Dim layer As ScatterLayer = c.addScatterLayer(dataX, dataY, "", Chart.GlassSphereShape, 15, _
        &Hff3333, &Hff3333)

    ' Add labels to the chart as an extra field
    layer.addExtraField(labels)

    ' Set the data label format to display the extra field
    layer.setDataLabelFormat("{field0}")

    ' Use 8pt Arial Bold to display the labels
    Dim textbox As ChartDirector.TextBox = layer.setDataLabelStyle("Arial Bold", 8)

    ' Set the background to purple with a 1 pixel 3D border
    textbox.setBackground(&Hcc99ff, Chart.Transparent, 1)

    ' Put the text box 4 pixels to the right of the data point
    textbox.setAlignment(Chart.Left)
    textbox.setPos(4, 0)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='Range = {x} miles, Capacity = {value} tons'")

End Sub

</script>

<html>
<head>
    <title>Custom Scatter Labels</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Custom Scatter Labels
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

