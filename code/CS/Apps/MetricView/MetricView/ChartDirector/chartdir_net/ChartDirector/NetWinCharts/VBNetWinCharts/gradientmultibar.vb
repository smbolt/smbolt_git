Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class gradientmultibar
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Gradient Multi-Bar Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the bar chart
        Dim data0() As Double = {100, 125, 245, 147, 67}
        Dim data1() As Double = {85, 156, 179, 211, 123}
        Dim data2() As Double = {97, 87, 56, 267, 157}
        Dim labels() As String = {"Mon", "Tue", "Wed", "Thur", "Fri"}

        ' Create a XYChart object of size 540 x 375 pixels
        Dim c As XYChart = New XYChart(540, 375)

        ' Add a title to the chart using 18pt Times Bold Italic font
        c.addTitle("Average Weekly Network Load", "Times New Roman Bold Italic", 18)

        ' Set the plotarea at (50, 55) and of 440 x 280 pixels in size. Use a vertical gradient
        ' color from grey (888888) to black (000000) as background. Set border and grid lines to
        ' white (ffffff).
        c.setPlotArea(50, 55, 440, 280, c.linearGradientColor(0, 55, 0, 335, &H888888, &H000000), _
            -1, &Hffffff, &Hffffff)

        ' Add a legend box at (50, 25) using horizontal layout. Use 10pt Arial Bold as font, with
        ' transparent background.
        c.addLegend(50, 25, False, "Arial Bold", 10).setBackground(Chart.Transparent)

        ' Set the x axis labels
        c.xAxis().setLabels(labels)

        ' Draw the ticks between label positions (instead of at label positions)
        c.xAxis().setTickOffset(0.5)

        ' Set axis label style to 8pt Arial Bold
        c.xAxis().setLabelStyle("Arial Bold", 8)
        c.yAxis().setLabelStyle("Arial Bold", 8)

        ' Set axis line width to 2 pixels
        c.xAxis().setWidth(2)
        c.yAxis().setWidth(2)

        ' Add axis title
        c.yAxis().setTitle("Throughput (MBytes Per Hour)")

        ' Add a multi-bar layer with 3 data sets and 4 pixels 3D depth
        Dim layer As BarLayer = c.addBarLayer2(Chart.Side, 4)
        layer.addDataSet(data0, &H66aaee, "Server #1")
        layer.addDataSet(data1, &Heebb22, "Server #2")
        layer.addDataSet(data2, &Hcc0000, "Server #3")

        ' Set bar border to transparent. Use bar gradient lighting with light intensity from 0.75 to
        ' 1.75.
        layer.setBorderColor(Chart.Transparent, Chart.barLighting(0.75, 1.75))

        ' Configure the bars within a group to touch each others (no gap)
        layer.setBarGap(0.2, Chart.TouchBar)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{dataSetName} on {xLabel}: {value} MBytes/hour'")

    End Sub

End Class

