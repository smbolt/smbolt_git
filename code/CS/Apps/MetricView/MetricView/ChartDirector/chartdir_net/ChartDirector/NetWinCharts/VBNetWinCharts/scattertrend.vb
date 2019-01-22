Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class scattertrend
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Scatter Trend Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The XY data of the first data series
        Dim dataX0() As Double = {50, 55, 37, 24, 42, 49, 63, 72, 83, 59}
        Dim dataY0() As Double = {3.6, 2.8, 2.5, 2.3, 3.8, 3.0, 3.8, 5.0, 6.0, 3.3}

        ' The XY data of the second data series
        Dim dataX1() As Double = {50, 55, 37, 24, 42, 49, 63, 72, 83, 59}
        Dim dataY1() As Double = {1.6, 1.8, 0.8, 0.5, 1.3, 1.5, 2.3, 2.4, 2.9, 1.5}

        ' Tool tip formats for data points and trend lines
        Dim scatterToolTip As String = _
            "title='{dataSetName}: Response time at {x} TPS: {value} sec'"
        Dim trendToolTip As String = _
            "title='Slope = {slope|4} sec/TPS; Intercept = {intercept|4} sec'"

        ' Create a XYChart object of size 450 x 420 pixels
        Dim c As XYChart = New XYChart(450, 420)

        ' Set the plotarea at (55, 65) and of size 350 x 300 pixels, with white background and a
        ' light grey border (0xc0c0c0). Turn on both horizontal and vertical grid lines with light
        ' grey color (0xc0c0c0)
        c.setPlotArea(55, 65, 350, 300, &Hffffff, -1, &Hc0c0c0, &Hc0c0c0, -1)

        ' Add a legend box at (50, 30) (top of the chart) with horizontal layout. Use 12pt Times
        ' Bold Italic font. Set the background and border color to Transparent.
        c.addLegend(50, 30, False, "Times New Roman Bold Italic", 12).setBackground( _
            Chart.Transparent)

        ' Add a title to the chart using 18 point Times Bold Itatic font.
        c.addTitle("Server Performance", "Times New Roman Bold Italic", 18)

        ' Add titles to the axes using 12pt Arial Bold Italic font
        c.yAxis().setTitle("Response Time (sec)", "Arial Bold Italic", 12)
        c.xAxis().setTitle("Server Load (TPS)", "Arial Bold Italic", 12)

        ' Set the axes line width to 3 pixels
        c.yAxis().setWidth(3)
        c.xAxis().setWidth(3)

        ' Add a scatter layer using (dataX0, dataY0)
        Dim scatter1 As ScatterLayer = c.addScatterLayer(dataX0, dataY0, "Server AAA", _
            Chart.DiamondSymbol, 11, &H008000)
        scatter1.setHTMLImageMap("", "", scatterToolTip)

        ' Add a trend line layer for (dataX0, dataY0)
        Dim trend1 As TrendLayer = c.addTrendLayer2(dataX0, dataY0, &H008000)
        trend1.setLineWidth(3)
        trend1.setHTMLImageMap("", "", trendToolTip)

        ' Add a scatter layer for (dataX1, dataY1)
        Dim scatter2 As ScatterLayer = c.addScatterLayer(dataX1, dataY1, "Server BBB", _
            Chart.TriangleSymbol, 9, &H6666ff)
        scatter2.setHTMLImageMap("", "", scatterToolTip)

        ' Add a trend line layer for (dataX1, dataY1)
        Dim trend2 As TrendLayer = c.addTrendLayer2(dataX1, dataY1, &H6666ff)
        trend2.setLineWidth(3)
        trend2.setHTMLImageMap("", "", trendToolTip)

        ' Output the chart
        viewer.Chart = c

        ' include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable")

    End Sub

End Class

