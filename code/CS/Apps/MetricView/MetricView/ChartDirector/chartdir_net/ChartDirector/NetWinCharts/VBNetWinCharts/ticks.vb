Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class ticks
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Tick Density"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 2
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the chart
        Dim data() As Double = {100, 125, 265, 147, 67, 105}
        Dim labels() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun"}

        ' Create a XYChart object of size 250 x 250 pixels
        Dim c As XYChart = New XYChart(250, 250)

        ' Set the plot area at (27, 25) and of size 200 x 200 pixels
        c.setPlotArea(27, 25, 200, 200)

        If chartIndex = 1 Then
            ' High tick density, uses 10 pixels as tick spacing
            c.addTitle("Tick Density = 10 pixels")
            c.yAxis().setTickDensity(10)
        Else
            ' Normal tick density, just use the default setting
            c.addTitle("Default Tick Density")
        End If

        ' Set the labels on the x axis
        c.xAxis().setLabels(labels)

        ' Add a color bar layer using the given data. Use a 1 pixel 3D border for the bars.
        c.addBarLayer3(data).setBorderColor(-1, 1)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='Revenue for {xLabel}: US${value}M'")

    End Sub

End Class

