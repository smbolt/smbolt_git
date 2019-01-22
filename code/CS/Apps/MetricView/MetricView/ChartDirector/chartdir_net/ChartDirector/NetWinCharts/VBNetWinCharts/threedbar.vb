Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class threedbar
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "3D Bar Chart"
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

        ' Create a XYChart object of size 300 x 280 pixels
        Dim c As XYChart = New XYChart(300, 280)

        ' Set the plotarea at (45, 30) and of size 200 x 200 pixels
        c.setPlotArea(45, 30, 200, 200)

        ' Add a title to the chart
        c.addTitle("Weekly Server Load")

        ' Add a title to the y axis
        c.yAxis().setTitle("MBytes")

        ' Add a title to the x axis
        c.xAxis().setTitle("Work Week 25")

        ' Add a bar chart layer with green (0x00ff00) bars using the given data
        c.addBarLayer(data, &H00ff00).set3D()

        ' Set the labels on the x axis.
        c.xAxis().setLabels(labels)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", "title='{xLabel}: {value} MBytes'")

    End Sub

End Class

