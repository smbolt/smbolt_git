Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class linearzonemeter
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Linear Zone Meter"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The value to display on the meter
        Dim value As Double = 85

        ' Create an LinearMeter object of size 210 x 45 pixels, using silver background with a 2
        ' pixel black 3D depressed border.
        Dim m As LinearMeter = New LinearMeter(210, 45, Chart.silverColor(), 0, -2)

        ' Set the scale region top-left corner at (5, 5), with size of 200 x 20 pixels. The scale
        ' labels are located on the bottom (implies horizontal meter)
        m.setMeter(5, 5, 200, 20, Chart.Bottom)

        ' Set meter scale from 0 - 100
        m.setScale(0, 100)

        ' Add a title at the bottom of the meter with a 1 pixel raised 3D border
        m.addTitle2(Chart.Bottom, "Battery Level", "Arial Bold", 8).setBackground( _
            Chart.Transparent, -1, 1)

        ' Set 3 zones of different colors to represent Good/Weak/Bad data ranges
        m.addZone(50, 100, &H99ff99, "Good")
        m.addZone(20, 50, &Hffff66, "Weak")
        m.addZone(0, 20, &Hffcccc, "Bad")

        ' Add empty labels (just need the ticks) at 0/20/50/80 as separators for zones
        m.addLabel(0, " ")
        m.addLabel(20, " ")
        m.addLabel(50, " ")
        m.addLabel(100, " ")

        ' Add a semi-transparent blue (800000ff) pointer at the specified value, using triangular
        ' pointer shape
        m.addPointer(value, &H800000ff).setShape(Chart.TriangularPointer)

        ' Output the chart
        viewer.Chart = m

    End Sub

End Class

