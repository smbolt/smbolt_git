Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class depthbar
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Depth Bar Chart"
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

        ' The labels for the bar chart
        Dim labels() As String = {"Mon", "Tue", "Wed", "Thu", "Fri"}

        ' Create a XYChart object of size 500 x 320 pixels
        Dim c As XYChart = New XYChart(500, 320)

        ' Set the plotarea at (100, 40) and of size 280 x 240 pixels
        c.setPlotArea(100, 40, 280, 240)

        ' Add a legend box at (405, 100)
        c.addLegend(405, 100)

        ' Add a title to the chart
        c.addTitle("Weekday Network Load")

        ' Add a title to the y axis. Draw the title upright (font angle = 0)
        c.yAxis().setTitle("Average<*br*>Workload<*br*>(MBytes<*br*>Per Hour)").setFontAngle(0)

        ' Set the labels on the x axis
        c.xAxis().setLabels(labels)

        ' Add three bar layers, each representing one data set. The bars are drawn in
        ' semi-transparent colors.
        c.addBarLayer(data0, &H808080ff, "Server # 1", 5)
        c.addBarLayer(data1, &H80ff0000, "Server # 2", 5)
        c.addBarLayer(data2, &H8000ff00, "Server # 3", 5)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{dataSetName} on {xLabel}: {value} MBytes/hour'")

    End Sub

End Class

