Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class threedscatter
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "3D Scatter Chart (1)"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The XYZ data for the 3D scatter chart as 3 random data series
        Dim r As RanSeries = New RanSeries(0)
        Dim xData() As Double = r.getSeries2(100, 100, -10, 10)
        Dim yData() As Double = r.getSeries2(100, 0, 0, 20)
        Dim zData() As Double = r.getSeries2(100, 100, -10, 10)

        ' Create a ThreeDScatterChart object of size 720 x 600 pixels
        Dim c As ThreeDScatterChart = New ThreeDScatterChart(720, 600)

        ' Add a title to the chart using 20 points Times New Roman Italic font
        c.addTitle("3D Scatter Chart (1)  ", "Times New Roman Italic", 20)

        ' Set the center of the plot region at (350, 280), and set width x depth x height to 360 x
        ' 360 x 270 pixels
        c.setPlotRegion(350, 280, 360, 360, 270)

        ' Add a scatter group to the chart using 11 pixels glass sphere symbols, in which the color
        ' depends on the z value of the symbol
        c.addScatterGroup(xData, yData, zData, "", Chart.GlassSphere2Shape, 11, _
            Chart.SameAsMainColor)

        ' Add a color axis (the legend) in which the left center is anchored at (645, 270). Set the
        ' length to 200 pixels and the labels on the right side.
        c.setColorAxis(645, 270, Chart.Left, 200, Chart.Right)

        ' Set the x, y and z axis titles using 10 points Arial Bold font
        c.xAxis().setTitle("X-Axis Place Holder", "Arial Bold", 10)
        c.yAxis().setTitle("Y-Axis Place Holder", "Arial Bold", 10)
        c.zAxis().setTitle("Z-Axis Place Holder", "Arial Bold", 10)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", "title='(x={x|p}, y={y|p}, z={z|p}'")

    End Sub

End Class

