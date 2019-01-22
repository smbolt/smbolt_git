Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class gapbar
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Bar Gap"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 6
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        Dim bargap As Double = chartIndex * 0.25 - 0.25

        ' The data for the bar chart
        Dim data() As Double = {100, 125, 245, 147, 67}

        ' The labels for the bar chart
        Dim labels() As String = {"Mon", "Tue", "Wed", "Thu", "Fri"}

        ' Create a XYChart object of size 150 x 150 pixels
        Dim c As XYChart = New XYChart(150, 150)

        ' Set the plotarea at (27, 20) and of size 120 x 100 pixels
        c.setPlotArea(27, 20, 120, 100)

        ' Set the labels on the x axis
        c.xAxis().setLabels(labels)

        If bargap >= 0 Then
            ' Add a title to display to bar gap using 8pt Arial font
            c.addTitle("      Bar Gap = " & bargap, "Arial", 8)
        Else
            ' Use negative value to mean TouchBar
            c.addTitle("      Bar Gap = TouchBar", "Arial", 8)
            bargap = Chart.TouchBar
        End If

        ' Add a bar chart layer using the given data and set the bar gap
        c.addBarLayer(data).setBarGap(bargap)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='Production on {xLabel}: {value} kg'")

    End Sub

End Class

