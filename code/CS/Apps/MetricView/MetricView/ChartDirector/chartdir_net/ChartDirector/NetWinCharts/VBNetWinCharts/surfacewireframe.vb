Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class surfacewireframe
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Surface Wireframe"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 6
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The x and y coordinates of the grid
        Dim dataX() As Double = {-2, -1, 0, 1, 2}
        Dim dataY() As Double = {-2, -1, 0, 1, 2}

        ' The values at the grid points. In this example, we will compute the values using the
        ' formula z = square_root(15 - x * x - y * y).
        Dim dataZ((UBound(dataX) + 1) * (UBound(dataY) + 1) - 1) As Double
        For yIndex As Integer = 0 To UBound(dataY)
            Dim y As Double = dataY(yIndex)
            For xIndex As Integer = 0 To UBound(dataX)
                Dim x As Double = dataX(xIndex)
                dataZ(yIndex * (UBound(dataX) + 1) + xIndex) = Math.Sqrt(15 - x * x - y * y)
            Next
        Next

        ' Create a SurfaceChart object of size 380 x 340 pixels, with white (ffffff) background and
        ' grey (888888) border.
        Dim c As SurfaceChart = New SurfaceChart(380, 340, &Hffffff, &H888888)

        ' Demonstrate various wireframes with and without interpolation
        If chartIndex = 0 Then
            ' Original data without interpolation
            c.addTitle("5 x 5 Data Points<*br*>Standard Shading", "Arial Bold", 12)
            c.setContourColor(&H80ffffff)
        ElseIf chartIndex = 1 Then
            ' Original data, spline interpolated to 40 x 40 for smoothness
            c.addTitle("5 x 5 Points - Spline Fitted to 40 x 40<*br*>Standard Shading", _
                "Arial Bold", 12)
            c.setContourColor(&H80ffffff)
            c.setInterpolation(40, 40)
        ElseIf chartIndex = 2 Then
            ' Rectangular wireframe of original data
            c.addTitle("5 x 5 Data Points<*br*>Rectangular Wireframe")
            c.setShadingMode(Chart.RectangularFrame)
        ElseIf chartIndex = 3 Then
            ' Rectangular wireframe of original data spline interpolated to 40 x 40
            c.addTitle("5 x 5 Points - Spline Fitted to 40 x 40<*br*>Rectangular Wireframe")
            c.setShadingMode(Chart.RectangularFrame)
            c.setInterpolation(40, 40)
        ElseIf chartIndex = 4 Then
            ' Triangular wireframe of original data
            c.addTitle("5 x 5 Data Points<*br*>Triangular Wireframe")
            c.setShadingMode(Chart.TriangularFrame)
        Else
            ' Triangular wireframe of original data spline interpolated to 40 x 40
            c.addTitle("5 x 5 Points - Spline Fitted to 40 x 40<*br*>Triangular Wireframe")
            c.setShadingMode(Chart.TriangularFrame)
            c.setInterpolation(40, 40)
        End If

        ' Set the center of the plot region at (200, 170), and set width x depth x height to 200 x
        ' 200 x 150 pixels
        c.setPlotRegion(200, 170, 200, 200, 150)

        ' Set the plot region wall thichness to 5 pixels
        c.setWallThickness(5)

        ' Set the elevation and rotation angles to 20 and 30 degrees
        c.setViewAngle(20, 30)

        ' Set the data to use to plot the chart
        c.setData(dataX, dataY, dataZ)

        ' Output the chart
        viewer.Chart = c

    End Sub

End Class

