Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class rose
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Simple Rose Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' Data for the chart
        Dim data() As Double = {9.4, 1.8, 2.1, 2.3, 3.5, 7.7, 8.8, 6.1, 5.0, 3.1, 6.0, 4.3, 5.1, _
            2.6, 1.5, 2.2, 5.1, 4.3, 4.0, 9.0, 1.7, 8.8, 9.9, 9.5}
        Dim angles() As Double = {0, 15, 30, 45, 60, 75, 90, 105, 120, 135, 150, 165, 180, 195, _
            210, 225, 240, 255, 270, 285, 300, 315, 330, 345}

        ' Create a PolarChart object of size 460 x 460 pixels, with a silver background and a 1
        ' pixel 3D border
        Dim c As PolarChart = New PolarChart(460, 460, Chart.silverColor(), &H000000, 1)

        ' Add a title to the chart at the top left corner using 15pt Arial Bold Italic font. Use
        ' white text on deep blue background.
        c.addTitle("Polar Vector Chart Demonstration", "Arial Bold Italic", 15, &Hffffff _
            ).setBackground(&H000080)

        ' Set plot area center at (230, 240) with radius 180 pixels and white background
        c.setPlotArea(230, 240, 180, &Hffffff)

        ' Set the grid style to circular grid
        c.setGridStyle(False)

        ' Set angular axis as 0 - 360, with a spoke every 30 units
        c.angularAxis().setLinearScale(0, 360, 30)

        ' Add sectors to the chart as sector zones
        For i As Integer = 0 To UBound(data)
            c.angularAxis().addZone(angles(i), angles(i) + 15, 0, data(i), &H33ff33, &H008000)
        Next

        ' Add an Transparent invisible layer to ensure the axis is auto-scaled using the data
        c.addLineLayer(data, Chart.Transparent)

        ' Output the chart
        viewer.Chart = c

    End Sub

End Class

