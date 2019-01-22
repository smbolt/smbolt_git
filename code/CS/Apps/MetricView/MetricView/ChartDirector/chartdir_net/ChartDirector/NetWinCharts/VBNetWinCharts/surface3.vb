Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class surface3
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Surface Chart (3)"
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
        ' formula z = Sin(x * x / 128 - y * y / 256 + 3) * Cos(x / 4 + 1 - Exp(y / 8))
        Dim dataZ((UBound(dataX) + 1) * (UBound(dataY) + 1) - 1) As Double
        For yIndex As Integer = 0 To UBound(dataY)
            Dim y As Double = dataY(yIndex)
            For xIndex As Integer = 0 To UBound(dataX)
                Dim x As Double = dataX(xIndex)
                dataZ(yIndex * (UBound(dataX) + 1) + xIndex) = Math.Sin(x * x / 128.0 - y * y / _
                    256.0 + 3) * Math.Cos(x / 4.0 + 1 - Math.Exp(y / 8.0))
            Next
        Next

        ' Create a SurfaceChart object of size 750 x 600 pixels
        Dim c As SurfaceChart = New SurfaceChart(750, 600)

        ' Add a title to the chart using 20 points Times New Roman Italic font
        c.addTitle("Surface Energy Density       ", "Times New Roman Italic", 20)

        ' Set the center of the plot region at (380, 260), and set width x depth x height to 360 x
        ' 360 x 270 pixels
        c.setPlotRegion(380, 260, 360, 360, 270)

        ' Set the elevation and rotation angles to 30 and 210 degrees
        c.setViewAngle(30, 210)

        ' Set the perspective level to 60
        c.setPerspective(60)

        ' Set the data to use to plot the chart
        c.setData(dataX, dataY, dataZ)

        ' Spline interpolate data to a 80 x 80 grid for a smooth surface
        c.setInterpolation(80, 80)

        ' Use semi-transparent black (c0000000) for x and y major surface grid lines. Use dotted
        ' style for x and y minor surface grid lines.
        Dim majorGridColor As Integer = &Hc0000000
        Dim minorGridColor As Integer = c.dashLineColor(majorGridColor, Chart.DotLine)
        c.setSurfaceAxisGrid(majorGridColor, majorGridColor, minorGridColor, minorGridColor)

        ' Set contour lines to semi-transparent white (80ffffff)
        c.setContourColor(&H80ffffff)

        ' Add a color axis (the legend) in which the left center is anchored at (665, 280). Set the
        ' length to 200 pixels and the labels on the right side.
        c.setColorAxis(665, 280, Chart.Left, 200, Chart.Right)

        ' Set the x, y and z axis titles using 12 points Arial Bold font
        c.xAxis().setTitle("X Title<*br*>Placeholder", "Arial Bold", 12)
        c.yAxis().setTitle("Y Title<*br*>Placeholder", "Arial Bold", 12)
        c.zAxis().setTitle("Z Title Placeholder", "Arial Bold", 12)

        ' Output the chart
        viewer.Chart = c

    End Sub

End Class

