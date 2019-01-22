Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class contourcolor
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Contour Color Scale"
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

        ' Use random numbers for the z values on the XY grid
        Dim r As RanSeries = New RanSeries(99)
        Dim dataZ() As Double = r.get2DSeries(UBound(dataX) + 1, UBound(dataY) + 1, -0.9, 0.9)

        ' Create a XYChart object of size 420 x 360 pixels
        Dim c As XYChart = New XYChart(420, 360)

        ' Set the plotarea at (30, 25) and of size 300 x 300 pixels. Use semi-transparent grey
        ' (0xdd000000) horizontal and vertical grid lines
        c.setPlotArea(30, 25, 300, 300, -1, -1, -1, &Hdd000000, -1)

        ' Set the x-axis and y-axis scale
        c.xAxis().setLinearScale(-4, 4, 1)
        c.yAxis().setLinearScale(-4, 4, 1)

        ' Add a contour layer using the given data
        Dim layer As ContourLayer = c.addContourLayer(dataX, dataY, dataZ)

        ' Move the grid lines in front of the contour layer
        c.getPlotArea().moveGridBefore(layer)

        ' Add a color axis (the legend) in which the top left corner is anchored at (350, 25). Set
        ' the length to 400 300 and the labels on the right side.
        Dim cAxis As ColorAxis = layer.setColorAxis(350, 25, Chart.TopLeft, 300, Chart.Right)

        If chartIndex = 1 Then
            ' Speicify a color gradient as a list of colors, and use it in the color axis.
            Dim colorGradient() As Integer = {&H0044cc, &Hffffff, &H00aa00}
            cAxis.setColorGradient(False, colorGradient)
        ElseIf chartIndex = 2 Then
            ' Specify the color scale to use in the color axis
            Dim colorScale() As Double = {-1.0, &H1a9850, -0.75, &H66bd63, -0.5, &Ha6d96a, -0.25, _
                &Hd9ef8b, 0, &Hfee08b, 0.25, &Hfdae61, 0.5, &Hf46d43, 0.75, &Hd73027, 1}
            cAxis.setColorScale(colorScale)
        ElseIf chartIndex = 3 Then
            ' Specify the color scale to use in the color axis. Also specify an underflow color
            ' 0x66ccff (blue) for regions that fall below the lower axis limit.
            Dim colorScale() As Double = {0, &Hffff99, 0.2, &H80cdc1, 0.4, &H35978f, 0.6, _
                &H01665e, 0.8, &H003c30, 1}
            cAxis.setColorScale(colorScale, &H66ccff)
        End If

        ' Output the chart
        viewer.Chart = c

    End Sub

End Class

