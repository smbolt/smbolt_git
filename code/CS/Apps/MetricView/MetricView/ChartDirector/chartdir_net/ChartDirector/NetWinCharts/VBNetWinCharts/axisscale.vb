Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class axisscale
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Y-Axis Scaling"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 5
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the chart
        Dim data() As Double = {5.5, 3.5, -3.7, 1.7, -1.4, 3.3}
        Dim labels() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun"}

        ' Create a XYChart object of size 200 x 190 pixels
        Dim c As XYChart = New XYChart(200, 190)

        ' Set the plot area at (30, 20) and of size 140 x 140 pixels
        c.setPlotArea(30, 20, 140, 140)

        ' Configure the axis as according to the input parameter
        If chartIndex = 0 Then
            c.addTitle("No Axis Extension", "Arial", 8)
        ElseIf chartIndex = 1 Then
            c.addTitle("Top/Bottom Extensions = 0/0", "Arial", 8)
            ' Reserve 20% margin at top of plot area when auto-scaling
            c.yAxis().setAutoScale(0, 0)
        ElseIf chartIndex = 2 Then
            c.addTitle("Top/Bottom Extensions = 0.2/0.2", "Arial", 8)
            ' Reserve 20% margin at top and bottom of plot area when auto-scaling
            c.yAxis().setAutoScale(0.2, 0.2)
        ElseIf chartIndex = 3 Then
            c.addTitle("Axis Top Margin = 15", "Arial", 8)
            ' Reserve 15 pixels at top of plot area
            c.yAxis().setMargin(15)
        Else
            c.addTitle("Manual Scale -5 to 10", "Arial", 8)
            ' Set the y axis to scale from -5 to 10, with ticks every 5 units
            c.yAxis().setLinearScale(-5, 10, 5)
        End If

        ' Set the labels on the x axis
        c.xAxis().setLabels(labels)

        ' Add a color bar layer using the given data. Use a 1 pixel 3D border for the bars.
        c.addBarLayer3(data).setBorderColor(-1, 1)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", "title='ROI for {xLabel}: {value}%'")

    End Sub

End Class

