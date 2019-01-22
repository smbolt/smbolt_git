Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class surface
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Surface Chart (1)"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The x and y coordinates of the grid
        Dim dataX() As Double = {-10, -9, -8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, _
            8, 9, 10}
        Dim dataY() As Double = {-10, -9, -8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, _
            8, 9, 10}

        ' The values at the grid points. In this example, we will compute the values using the
        ' formula z = x * sin(y) + y * sin(x).
        Dim dataZ((UBound(dataX) + 1) * (UBound(dataY) + 1) - 1) As Double
        For yIndex As Integer = 0 To UBound(dataY)
            Dim y As Double = dataY(yIndex)
            For xIndex As Integer = 0 To UBound(dataX)
                Dim x As Double = dataX(xIndex)
                dataZ(yIndex * (UBound(dataX) + 1) + xIndex) = x * Math.Sin(y) + y * Math.Sin(x)
            Next
        Next

        ' Create a SurfaceChart object of size 720 x 600 pixels
        Dim c As SurfaceChart = New SurfaceChart(720, 600)

        ' Add a title to the chart using 20 points Times New Roman Italic font
        c.addTitle("Surface Energy Density   ", "Times New Roman Italic", 20)

        ' Set the center of the plot region at (350, 280), and set width x depth x height to 360 x
        ' 360 x 270 pixels
        c.setPlotRegion(350, 280, 360, 360, 270)

        ' Set the data to use to plot the chart
        c.setData(dataX, dataY, dataZ)

        ' Spline interpolate data to a 80 x 80 grid for a smooth surface
        c.setInterpolation(80, 80)

        ' Add a color axis (the legend) in which the left center is anchored at (645, 270). Set the
        ' length to 200 pixels and the labels on the right side.
        c.setColorAxis(645, 270, Chart.Left, 200, Chart.Right)

        ' Set the x, y and z axis titles using 10 points Arial Bold font
        c.xAxis().setTitle("X (nm)", "Arial Bold", 10)
        c.yAxis().setTitle("Y (nm)", "Arial Bold", 10)
        c.zAxis().setTitle("Energy Density (J/m<*font,super*>2<*/font*>)", "Arial Bold", 10)

        ' Output the chart
        viewer.Chart = c

    End Sub

End Class

