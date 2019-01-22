Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class contourinterpolate
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Contour Interpolation"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 4
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The x and y coordinates of the grid
        Dim dataX() As Double = {-4, -3, -2, -1, 0, 1, 2, 3, 4}
        Dim dataY() As Double = {-4, -3, -2, -1, 0, 1, 2, 3, 4}

        ' The values at the grid points. In this example, we will compute the values using the
        ' formula z = Sin(x * pi / 3) * Sin(y * pi / 3).
        Dim dataZ((UBound(dataX) + 1) * (UBound(dataY) + 1) - 1) As Double
        For yIndex As Integer = 0 To UBound(dataY)
            Dim y As Double = dataY(yIndex)
            For xIndex As Integer = 0 To UBound(dataX)
                Dim x As Double = dataX(xIndex)
                dataZ(yIndex * (UBound(dataX) + 1) + xIndex) = Math.Sin(x * 3.1416 / 3) * _
                    Math.Sin(y * 3.1416 / 3)
            Next
        Next

        ' Create a XYChart object of size 360 x 360 pixels
        Dim c As XYChart = New XYChart(360, 360)

        ' Set the plotarea at (30, 25) and of size 300 x 300 pixels. Use semi-transparent black
        ' (c0000000) for both horizontal and vertical grid lines
        c.setPlotArea(30, 25, 300, 300, -1, -1, -1, &Hc0000000, -1)

        ' Add a contour layer using the given data
        Dim layer As ContourLayer = c.addContourLayer(dataX, dataY, dataZ)

        ' Set the x-axis and y-axis scale
        c.xAxis().setLinearScale(-4, 4, 1)
        c.yAxis().setLinearScale(-4, 4, 1)

        If chartIndex = 0 Then
            ' Discrete coloring, spline surface interpolation
            c.addTitle("Spline Surface - Discrete Coloring", "Arial Bold Italic", 12)
        ElseIf chartIndex = 1 Then
            ' Discrete coloring, linear surface interpolation
            c.addTitle("Linear Surface - Discrete Coloring", "Arial Bold Italic", 12)
            layer.setSmoothInterpolation(False)
        ElseIf chartIndex = 2 Then
            ' Smooth coloring, spline surface interpolation
            c.addTitle("Spline Surface - Continuous Coloring", "Arial Bold Italic", 12)
            layer.setContourColor(Chart.Transparent)
            layer.colorAxis().setColorGradient(True)
        Else
            ' Discrete coloring, linear surface interpolation
            c.addTitle("Linear Surface - Continuous Coloring", "Arial Bold Italic", 12)
            layer.setSmoothInterpolation(False)
            layer.setContourColor(Chart.Transparent)
            layer.colorAxis().setColorGradient(True)
        End If

        ' Output the chart
        viewer.Chart = c

    End Sub

End Class

