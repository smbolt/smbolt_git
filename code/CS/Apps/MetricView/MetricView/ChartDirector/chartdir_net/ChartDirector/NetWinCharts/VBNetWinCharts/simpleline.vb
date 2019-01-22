Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class simpleline
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Simple Line Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the line chart
        Dim data() As Double = {30, 28, 40, 55, 75, 68, 54, 60, 50, 62, 75, 65, 75, 91, 60, 55, _
            53, 35, 50, 66, 56, 48, 52, 65, 62}

        ' The labels for the line chart
        Dim labels() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", _
            "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24"}

        ' Create a XYChart object of size 250 x 250 pixels
        Dim c As XYChart = New XYChart(250, 250)

        ' Set the plotarea at (30, 20) and of size 200 x 200 pixels
        c.setPlotArea(30, 20, 200, 200)

        ' Add a line chart layer using the given data
        c.addLineLayer(data)

        ' Set the labels on the x axis.
        c.xAxis().setLabels(labels)

        ' Display 1 out of 3 labels on the x-axis.
        c.xAxis().setLabelStep(3)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='Hour {xLabel}: Traffic {value} GBytes'")

    End Sub

End Class

