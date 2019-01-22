Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class surfaceaxis
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Surface Chart Axis Types"
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
        Dim dataX() As Date = {DateSerial(2008, 9, 1), DateSerial(2008, 9, 2), DateSerial(2008, 9, _
            3), DateSerial(2008, 9, 4), DateSerial(2008, 9, 5), DateSerial(2008, 9, 6)}
        Dim dataY() As String = {"Low", "Medium", "High"}

        ' The data series
        Dim lowData() As Double = {24, 38, 33, 25, 28, 36}
        Dim mediumData() As Double = {49, 42, 34, 47, 53, 50}
        Dim highData() As Double = {44, 51, 38, 33, 47, 42}

        ' Create a SurfaceChart object of size 760 x 500 pixels
        Dim c As SurfaceChart = New SurfaceChart(760, 500)

        ' Add a title to the chart using 18 points Arial font
        c.addTitle("Surface Chart Axis Types", "Arial", 18)

        ' Set the center of the plot region at (385, 240), and set width x depth x height to 480 x
        ' 240 x 240 pixels
        c.setPlotRegion(385, 240, 480, 240, 240)

        ' Set the elevation and rotation angles to 30 and -10 degrees
        c.setViewAngle(30, -10)

        ' Set the data to use to plot the chart. As the y-data are text strings (enumerated), we
        ' will use an empty array for the y-coordinates. For the z data series, they are just the
        ' concatenation of the individual data series.
        c.setData(Chart.CTime(dataX), Nothing, New ArrayMath(lowData).insert(mediumData).insert( _
            highData).result())

        ' Set the y-axis labels
        c.yAxis().setLabels(dataY)

        ' Set x-axis tick density to 75 pixels. ChartDirector auto-scaling will use this as the
        ' guideline when putting ticks on the x-axis.
        c.xAxis().setTickDensity(75)

        ' Spline interpolate data to a 80 x 40 grid for a smooth surface
        c.setInterpolation(80, 40)

        ' Set surface grid lines to semi-transparent black (cc000000).
        c.setSurfaceAxisGrid(&Hcc000000)

        ' Set contour lines to the same color as the fill color at the contour level
        c.setContourColor(Chart.SameAsMainColor)

        ' Add a color axis (the legend) in which the top right corner is anchored at (95, 100). Set
        ' the length to 160 pixels and the labels on the left side.
        Dim cAxis As ColorAxis = c.setColorAxis(95, 100, Chart.TopRight, 160, Chart.Left)

        ' Add a bounding box with light grey (eeeeee) background and grey (888888) border.
        cAxis.setBoundingBox(&Heeeeee, &H888888)

        ' Set label style to Arial bold for all axes
        c.xAxis().setLabelStyle("Arial Bold")
        c.yAxis().setLabelStyle("Arial Bold")
        c.zAxis().setLabelStyle("Arial Bold")
        c.colorAxis().setLabelStyle("Arial Bold")

        ' Set the x, y and z axis titles using deep blue (000088) 15 points Arial font
        c.xAxis().setTitle("Date/Time Axis", "Arial Italic", 15, &H000088)
        c.yAxis().setTitle("Label<*br*>Based<*br*>Axis", "Arial Italic", 15, &H000088)
        c.zAxis().setTitle("Numeric Axis", "Arial Italic", 15, &H000088)

        ' Output the chart
        viewer.Chart = c

    End Sub

End Class

