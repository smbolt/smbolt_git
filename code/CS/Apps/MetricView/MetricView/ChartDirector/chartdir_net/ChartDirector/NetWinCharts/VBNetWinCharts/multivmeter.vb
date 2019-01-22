Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class multivmeter
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Multi-Pointer Vertical Meter"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The values to display on the meter
        Dim value0 As Double = 30.99
        Dim value1 As Double = 45.35
        Dim value2 As Double = 77.64

        ' Create an LinearMeter object of size 60 x 245 pixels, using silver background with a 2
        ' pixel black 3D depressed border.
        Dim m As LinearMeter = New LinearMeter(60, 245, Chart.silverColor(), 0, -2)

        ' Set the scale region top-left corner at (25, 30), with size of 20 x 200 pixels. The scale
        ' labels are located on the left (default - implies vertical meter)
        m.setMeter(25, 30, 20, 200)

        ' Set meter scale from 0 - 100, with a tick every 10 units
        m.setScale(0, 100, 10)

        ' Set 0 - 50 as green (99ff99) zone, 50 - 80 as yellow (ffff66) zone, and 80 - 100 as red
        ' (ffcccc) zone
        m.addZone(0, 50, &H99ff99)
        m.addZone(50, 80, &Hffff66)
        m.addZone(80, 100, &Hffcccc)

        ' Add deep red (000080), deep green (008000) and deep blue (800000) pointers to reflect the
        ' values
        m.addPointer(value0, &H000080)
        m.addPointer(value1, &H008000)
        m.addPointer(value2, &H800000)

        ' Add a text box label at top-center (30, 5) using Arial Bold/8pt/deep blue (000088), with a
        ' light blue (9999ff) background
        m.addText(30, 5, "Temp C", "Arial Bold", 8, &H000088, Chart.TopCenter).setBackground( _
            &H9999ff)

        ' Output the chart
        viewer.Chart = m

    End Sub

End Class

