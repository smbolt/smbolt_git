<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '
    '    We use a random table to simulate generating 12 months of data
    '

    ' Create the random table object with 4 cols * 12 rows, using 3 as seed
    Dim rantable As RanTable = New RanTable(3, 4, 12)

    ' Set the 1st column to be the 12 months of year 2002
    rantable.setDateCol(0, DateSerial(2002, 1, 1), 86400 * 30)

    ' Set the 2nd, 3rd and 4th columns to be random numbers starting from 125, 75, and 100
    ' respectively. The change between rows is set to -35 to + 35. The minimum value of any cell is
    ' 0.
    rantable.setCol(1, 125, -35, 35, 0)
    rantable.setCol(2, 75, -35, 35, 0)
    rantable.setCol(3, 100, -35, 35, 0)

    ' Get the 1st column (time) as the x data
    Dim dataX() As Double = rantable.getCol(0)

    ' Get the 2nd, 3rd and 4th columns as 3 data sets
    Dim dataY0() As Double = rantable.getCol(1)
    Dim dataY1() As Double = rantable.getCol(2)
    Dim dataY2() As Double = rantable.getCol(3)

    ' Create a XYChart object of size 360 x 400 pixels
    Dim c As XYChart = New XYChart(360, 400)

    ' Add a title to the chart
    c.addTitle("<*underline=2*>Rotated Line Chart Demo", "Times New Roman Bold Italic", 14)

    ' Set the plotarea at (60, 75) and of size 190 x 320 pixels. Turn on both horizontal and
    ' vertical grid lines with light grey color (0xc0c0c0)
    c.setPlotArea(60, 75, 190, 320).setGridColor(&Hc0c0c0, &Hc0c0c0)

    ' Add a legend box at (270, 75)
    c.addLegend(270, 75)

    ' Swap the x and y axis to become a rotated chart
    c.swapXY()

    ' Set the y axis on the top side (right + rotated = top)
    c.setYAxisOnRight()

    ' Add a title to the y axis
    c.yAxis().setTitle("Throughput (MBytes)")

    ' Reverse the x axis so it is pointing downwards
    c.xAxis().setReverse()

    ' Add a line chart layer using the given data
    Dim layer As LineLayer = c.addLineLayer2()
    layer.setXData(dataX)
    layer.addDataSet(dataY0, &Hff0000, "Server A")
    layer.addDataSet(dataY1, &H338033, "Server B")
    layer.addDataSet(dataY2, &H0000ff, "Server C")

    ' Set the line width to 2 pixels
    layer.setLineWidth(2)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='[{dataSetName}] {x|mm/yyyy}: {value|0} MByte'")

End Sub

</script>

<html>
<head>
    <title>Rotated Line Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Rotated Line Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

