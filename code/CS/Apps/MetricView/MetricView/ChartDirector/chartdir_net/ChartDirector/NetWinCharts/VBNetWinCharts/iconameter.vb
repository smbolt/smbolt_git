Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class iconameter
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Icon Angular Meter"
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

        ' Create an AugularMeter object of size 70 x 90 pixels, using black background with a 2
        ' pixel 3D depressed border.
        Dim m As AngularMeter = New AngularMeter(70, 90, 0, 0, -2)

        ' Use white on black color palette for default text and line colors
        m.setColors(Chart.whiteOnBlackPalette)

        ' Set the meter center at (10, 45), with radius 50 pixels, and span from 135 to 45 degrees
        m.setMeter(10, 45, 50, 135, 45)

        ' Set meter scale from 0 - 100, with the specified labels
        m.setScale2(0, 100, New String() {"E", " ", " ", " ", "F"})

        ' Set the angular arc and major tick width to 2 pixels
        m.setLineWidth(2, 2)

        ' Add a red zone at 0 - 15
        m.addZone(0, 15, &Hff3333)

        ' Add an icon at (25, 35)
        m.addText(25, 35, "<*img=gas.gif*>")

        ' Add a yellow (ffff00) pointer at the specified value
        m.addPointer(value, &Hffff00)

        ' Output the chart
        viewer.Chart = m

    End Sub

End Class

