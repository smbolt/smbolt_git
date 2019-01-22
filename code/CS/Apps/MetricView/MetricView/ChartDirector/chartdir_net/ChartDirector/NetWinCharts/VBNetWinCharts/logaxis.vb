Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class logaxis
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Log Scale Axis"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 2
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the chart
        Dim data() As Double = {100, 125, 260, 147, 67}
        Dim labels() As String = {"Mon", "Tue", "Wed", "Thu", "Fri"}

        ' Create a XYChart object of size 200 x 180 pixels
        Dim c As XYChart = New XYChart(200, 180)

        ' Set the plot area at (30, 10) and of size 140 x 130 pixels
        c.setPlotArea(30, 10, 140, 130)

        ' Ise log scale axis if required
        If chartIndex = 1 Then
            c.yAxis().setLogScale3()
        End If

        ' Set the labels on the x axis
        c.xAxis().setLabels(labels)

        ' Add a color bar layer using the given data. Use a 1 pixel 3D border for the bars.
        c.addBarLayer3(data).setBorderColor(-1, 1)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='Mileage on {xLabel}: {value} miles'")

    End Sub

End Class

