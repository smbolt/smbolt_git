Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class glassmultibar
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Glass Multi-Bar Chart"
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
        ' color from light blue (f9f9ff) to blue (6666ff) as background. Set border and grid lines
        ' to white (ffffff).
        c.setPlotArea(50, 55, 440, 280, c.linearGradientColor(0, 55, 0, 335, &Hf9f9ff, &H6666ff), _
            -1, &Hffffff, &Hffffff)

        ' Add a legend box at (50, 28) using horizontal layout. Use 10pt Arial Bold as font, with
        ' transparent background.
        c.addLegend(50, 28, False, "Arial Bold", 10).setBackground(Chart.Transparent)

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

        ' Add a multi-bar layer with 3 data sets
        Dim layer As BarLayer = c.addBarLayer2(Chart.Side)
        layer.addDataSet(data0, &Hff0000, "Server #1")
        layer.addDataSet(data1, &H00ff00, "Server #2")
        layer.addDataSet(data2, &Hff8800, "Server #3")

        ' Set bar border to transparent. Use glass lighting effect with light direction from left.
        layer.setBorderColor(Chart.Transparent, Chart.glassEffect(Chart.NormalGlare, Chart.Left))

        ' Configure the bars within a group to touch each others (no gap)
        layer.setBarGap(0.2, Chart.TouchBar)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{dataSetName} on {xLabel}: {value} MBytes/hour'")

    End Sub

End Class

