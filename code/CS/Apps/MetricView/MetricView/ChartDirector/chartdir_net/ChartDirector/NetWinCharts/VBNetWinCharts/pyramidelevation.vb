Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class pyramidelevation
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Pyramid Elevation"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 7
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the pyramid chart
        Dim data() As Double = {156, 123, 211, 179}

        ' The colors for the pyramid layers
        Dim colors() As Integer = {&H66aaee, &Heebb22, &Hcccccc, &Hcc88ff}

        ' The elevation angle
        Dim angle As Integer = chartIndex * 15

        ' Create a PyramidChart object of size 200 x 200 pixels, with white (ffffff) background and
        ' grey (888888) border
        Dim c As PyramidChart = New PyramidChart(200, 200, &Hffffff, &H888888)

        ' Set the pyramid center at (100, 100), and width x height to 60 x 120 pixels
        c.setPyramidSize(100, 100, 60, 120)

        ' Set the elevation angle
        c.addTitle("Elevation = " & angle, "Arial Italic", 15)
        c.setViewAngle(angle)

        ' Set the pyramid data
        c.setData(data)

        ' Set the layer colors to the given colors
        c.setColors2(Chart.DataColor, colors)

        ' Leave 1% gaps between layers
        c.setLayerGap(0.01)

        ' Output the chart
        viewer.Chart = c

    End Sub

End Class

