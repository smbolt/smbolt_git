Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class pyramidgap
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Pyramid Gap"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 6
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the pyramid chart
        Dim data() As Double = {156, 123, 211, 179}

        ' The colors for the pyramid layers
        Dim colors() As Integer = {&H66aaee, &Heebb22, &Hcccccc, &Hcc88ff}

        ' The layer gap
        Dim gap As Double = chartIndex * 0.01

        ' Create a PyramidChart object of size 200 x 200 pixels, with white (ffffff) background and
        ' grey (888888) border
        Dim c As PyramidChart = New PyramidChart(200, 200, &Hffffff, &H888888)

        ' Set the pyramid center at (100, 100), and width x height to 60 x 120 pixels
        c.setPyramidSize(100, 100, 60, 120)

        ' Set the layer gap
        c.addTitle("Gap = " & gap, "Arial Italic", 15)
        c.setLayerGap(gap)

        ' Set the elevation to 15 degrees
        c.setViewAngle(15)

        ' Set the pyramid data
        c.setData(data)

        ' Set the layer colors to the given colors
        c.setColors2(Chart.DataColor, colors)

        ' Output the chart
        viewer.Chart = c

    End Sub

End Class

