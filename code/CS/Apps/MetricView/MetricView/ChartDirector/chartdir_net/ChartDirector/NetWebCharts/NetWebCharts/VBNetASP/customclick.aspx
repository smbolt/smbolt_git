<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The data for the line chart
    Dim data0() As Double = {50, 55, 47, 36, 42, 49, 63, 62, 73, 59, 56, 50, 64, 60, 67, 67, 58, _
        59, 73, 77, 84, 82, 80, 84}
    Dim data1() As Double = {36, 28, 25, 33, 38, 20, 22, 30, 25, 33, 30, 24, 28, 36, 30, 45, 46, _
        42, 48, 45, 43, 52, 64, 70}

    ' The labels for the line chart
    Dim labels() As String = {"Jan-04", "Feb-04", "Mar-04", "Apr-04", "May-04", "Jun-04", _
        "Jul-04", "Aug-04", "Sep-04", "Oct-04", "Nov-04", "Dec-04", "Jan-05", "Feb-05", "Mar-05", _
        "Apr-05", "May-05", "Jun-05", "Jul-05", "Aug-05", "Sep-05", "Oct-05", "Nov-05", "Dec-05"}

    ' Create an XYChart object of size 600 x 360 pixels, with a light blue (EEEEFF) background,
    ' black border, 1 pxiel 3D border effect and rounded corners
    Dim c As XYChart = New XYChart(600, 360, &Heeeeff, &H000000, 1)
    c.setRoundedFrame()

    ' Set plotarea at (55, 60) with size of 520 x 240 pixels. Use white (ffffff) as background and
    ' grey (cccccc) for grid lines
    c.setPlotArea(55, 60, 520, 240, &Hffffff, -1, -1, &Hcccccc, &Hcccccc)

    ' Add a legend box at (55, 58) (top of plot area) using 9pt Arial Bold font with horizontal
    ' layout Set border and background colors of the legend box to Transparent
    Dim legendBox As LegendBox = c.addLegend(55, 58, False, "Arial Bold", 9)
    legendBox.setBackground(Chart.Transparent)

    ' Reserve 10% margin at the top of the plot area during auto-scaling to leave space for the
    ' legends.
    c.yAxis().setAutoScale(0.1)

    ' Add a title to the chart using 15pt Times Bold Italic font. The text is white (ffffff) on a
    ' blue (0000cc) background, with glass effect.
    Dim title As ChartDirector.TextBox = c.addTitle("Monthly Revenue for Year 2000/2001", _
        "Times New Roman Bold Italic", 15, &Hffffff)
    title.setBackground(&H0000cc, &H000000, Chart.glassEffect(Chart.ReducedGlare))

    ' Add a title to the y axis
    c.yAxis().setTitle("Month Revenue (USD millions)")

    ' Set the labels on the x axis. Draw the labels vertical (angle = 90)
    c.xAxis().setLabels(labels).setFontAngle(90)

    ' Add a vertical mark at x = 17 using a semi-transparent purple (809933ff) color and Arial Bold
    ' font. Attached the mark (and therefore its label) to the top x axis.
    Dim mark As Mark = c.xAxis2().addMark(17, &H809933ff, "Merge with Star Tech", "Arial Bold")

    ' Set the mark line width to 2 pixels
    mark.setLineWidth(2)

    ' Set the mark label font color to purple (0x9933ff)
    mark.setFontColor(&H9933ff)

    ' Add a copyright message at (575, 295) (bottom right of plot area) using Arial Bold font
    Dim copyRight As ChartDirector.TextBox = c.addText(575, 295, "(c) Copyright Space Travel Ltd", _
        "Arial Bold")
    copyRight.setAlignment(Chart.BottomRight)

    ' Add a line layer to the chart
    Dim layer As LineLayer = c.addLineLayer()

    ' Set the default line width to 3 pixels
    layer.setLineWidth(3)

    ' Add the data sets to the line layer
    layer.addDataSet(data0, -1, "Enterprise")
    layer.addDataSet(data1, -1, "Consumer")

    ' Create the image
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Create an image map for the chart
    Dim chartImageMap As String = c.getHTMLImageMap("xystub.aspx", "", _
        "title='{dataSetName} @ {xLabel} = USD {value|0} millions'")

    ' Create an image map for the legend box
    Dim legendImageMap As String = legendBox.getHTMLImageMap( _
        "javascript:popMsg('the legend key [{dataSetName}]');", " ", _
        "title='This legend key is clickable!'")

    ' Obtain the image map for the title
    Dim titleImageMap As String = "<area " + title.getImageCoor() + _
        " href='javascript:popMsg(""the chart title"");' title='The title is clickable!'>"

    ' Obtain the image map for the mark
    Dim markImageMap As String = "<area " + mark.getImageCoor() + _
        " href='javascript:popMsg(""the Merge with Star Tech mark"");' title='The Merge with " & _
        "Star Tech text is clickable!'>"

    ' Obtain the image map for the copyright message
    Dim copyRightImageMap As String = "<area " + copyRight.getImageCoor() + _
        " href='javascript:popMsg(""the copyright message"");' title='The copyright text is " & _
        "clickable!'>"

    ' Get the combined image map
    WebChartViewer1.ImageMap = chartImageMap + legendImageMap + titleImageMap + markImageMap + _
        copyRightImageMap

End Sub

</script>

<!DOCTYPE html>
<html>
<head>
    <title>Custom Clickable Objects</title>
</head>
<body style="margin:5px 0px 0px 5px">
<script type="text/javascript">
function popMsg(msg) {
    alert("You have clicked on " + msg + ".");
}
</script>
<div style="font:bold 18pt verdana;">
    Custom Clickable Objects
</div>
<hr style="border:solid 1px #000080" />
<div style="font:10pt verdana; margin-bottom:20px">
    <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
</div>
<div style="font:10pt verdana; width:600px; margin-bottom:20px">
    In the following chart, the lines, legend keys, title, copyright, and the "Merge
    with Star Tech" text are all clickable!
</div>
<chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>
