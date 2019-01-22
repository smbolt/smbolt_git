Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class polarvector
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Polar Vector Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' Coordinates of the starting points of the vectors
        Dim radius() As Double = {5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 10, 10, 10, 10, 10, 10, 10, _
            10, 10, 10, 10, 10, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 20, 20, 20, 20, _
            20, 20, 20, 20, 20, 20, 20, 20, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25}
        Dim angle() As Double = {0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 0, 30, 60, _
            90, 120, 150, 180, 210, 240, 270, 300, 330, 0, 30, 60, 90, 120, 150, 180, 210, 240, _
            270, 300, 330, 0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 0, 30, 60, 90, _
            120, 150, 180, 210, 240, 270, 300, 330}

        ' Magnitude and direction of the vectors
        Dim magnitude() As Double = {5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 4, 4, 4, _
            4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, _
            1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
        Dim direction() As Double = {60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 0, 30, 60, _
            90, 120, 150, 180, 210, 240, 270, 300, 330, 0, 30, 60, 90, 120, 150, 180, 210, 240, _
            270, 300, 330, 0, 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 0, 30, 60, 90, _
            120, 150, 180, 210, 240, 270, 300, 330, 0, 30}

        ' Create a PolarChart object of size 460 x 460 pixels
        Dim c As PolarChart = New PolarChart(460, 460)

        ' Add a title to the chart at the top left corner using 15pt Arial Bold Italic font
        c.addTitle("Polar Vector Chart Demonstration", "Arial Bold Italic", 15)

        ' Set center of plot area at (230, 240) with radius 180 pixels
        c.setPlotArea(230, 240, 180)

        ' Set the grid style to circular grid
        c.setGridStyle(False)

        ' Set angular axis as 0 - 360, with a spoke every 30 units
        c.angularAxis().setLinearScale(0, 360, 30)

        ' Add a polar vector layer to the chart with blue (0000ff) vectors
        c.addVectorLayer(radius, angle, magnitude, direction, Chart.RadialAxisScale, &H0000ff)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='Vector at ({value}, {angle} deg): Length = {len}, Angle = {dir} deg'")

    End Sub

End Class

