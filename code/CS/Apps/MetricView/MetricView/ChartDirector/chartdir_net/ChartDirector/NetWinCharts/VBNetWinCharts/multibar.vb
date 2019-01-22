Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class multibar
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Multi-Bar Chart"
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
        Dim labels() As String = {"Mon", "Tue", "Wed", "Thu", "Fri"}

        ' Create a XYChart object of size 400 x 240 pixels
        Dim c As XYChart = New XYChart(400, 240)

        ' Add a title to the chart using 10 pt Arial font
        c.addTitle("         Average Weekday Network Load", "", 10)

        ' Set the plot area at (50, 25) and of size 320 x 180. Use two alternative background colors
        ' (0xffffc0 and 0xffffe0)
        c.setPlotArea(50, 25, 320, 180, &Hffffc0, &Hffffe0)

        ' Add a legend box at (55, 18) using horizontal layout. Use 8 pt Arial font, with
        ' transparent background
        c.addLegend(55, 18, False, "", 8).setBackground(Chart.Transparent)

        ' Add a title to the y-axis
        c.yAxis().setTitle("Throughput (MBytes Per Hour)")

        ' Reserve 20 pixels at the top of the y-axis for the legend box
        c.yAxis().setTopMargin(20)

        ' Set the x axis labels
        c.xAxis().setLabels(labels)

        ' Add a multi-bar layer with 3 data sets and 3 pixels 3D depth
        Dim layer As BarLayer = c.addBarLayer2(Chart.Side, 3)
        layer.addDataSet(data0, &Hff8080, "Server #1")
        layer.addDataSet(data1, &H80ff80, "Server #2")
        layer.addDataSet(data2, &H8080ff, "Server #3")

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{dataSetName} on {xLabel}: {value} MBytes/hour'")

    End Sub

End Class

