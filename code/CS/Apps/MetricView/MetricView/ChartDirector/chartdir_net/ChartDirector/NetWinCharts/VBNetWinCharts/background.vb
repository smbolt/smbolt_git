Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class background
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Background and Wallpaper"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 4
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the chart
        Dim data() As Double = {85, 156, 179.5, 211, 123}
        Dim labels() As String = {"Mon", "Tue", "Wed", "Thu", "Fri"}

        ' Create a XYChart object of size 270 x 270 pixels
        Dim c As XYChart = New XYChart(270, 270)

        ' Set the plot area at (40, 32) and of size 200 x 200 pixels
        Dim plotarea As PlotArea = c.setPlotArea(40, 32, 200, 200)

        ' Set the background style based on the input parameter
        If chartIndex = 0 Then
            ' Has wallpaper image
            c.setWallpaper("tile.gif")
        ElseIf chartIndex = 1 Then
            ' Use a background image as the plot area background
            plotarea.setBackground2("bg.png")
        ElseIf chartIndex = 2 Then
            ' Use white (0xffffff) and grey (0xe0e0e0) as two alternate plotarea background colors
            plotarea.setBackground(&Hffffff, &He0e0e0)
        Else
            ' Use a dark background palette
            c.setColors(Chart.whiteOnBlackPalette)
        End If

        ' Set the labels on the x axis
        c.xAxis().setLabels(labels)

        ' Add a color bar layer using the given data. Use a 1 pixel 3D border for the bars.
        c.addBarLayer3(data).setBorderColor(-1, 1)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='Revenue for {xLabel}: US${value}K'")

    End Sub

End Class

