Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class simplebar
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Simple Bar Chart (1)"
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
        Dim data() As Double = {85, 156, 179.5, 211, 123}

        ' The labels for the bar chart
        Dim labels() As String = {"Mon", "Tue", "Wed", "Thu", "Fri"}

        ' Create a XYChart object of size 250 x 250 pixels
        Dim c As XYChart = New XYChart(250, 250)

        ' Set the plotarea at (30, 20) and of size 200 x 200 pixels
        c.setPlotArea(30, 20, 200, 200)

        ' Add a bar chart layer using the given data
        c.addBarLayer(data)

        ' Set the labels on the x axis.
        c.xAxis().setLabels(labels)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", "title='{xLabel}: {value} GBytes'")

    End Sub

End Class

