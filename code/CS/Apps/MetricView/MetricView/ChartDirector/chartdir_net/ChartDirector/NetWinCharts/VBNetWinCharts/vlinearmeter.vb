Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class vlinearmeter
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Vertical Linear Meter"
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
        Dim value As Double = 74.35

        ' Create an LinearMeter object of size 70 x 240 pixels with a very light grey (0xeeeeee)
        ' background, and a rounded 3-pixel thick light grey (0xcccccc) border
        Dim m As LinearMeter = New LinearMeter(70, 240, &Heeeeee, &Hcccccc)
        m.setRoundedFrame(Chart.Transparent)
        m.setThickFrame(3)

        ' Set the scale region top-left corner at (28, 18), with size of 20 x 205 pixels. The scale
        ' labels are located on the left (default - implies vertical meter).
        m.setMeter(28, 18, 20, 205)

        ' Set meter scale from 0 - 100, with a tick every 10 units
        m.setScale(0, 100, 10)

        ' Add a smooth color scale to the meter
        Dim smoothColorScale() As Double = {0, &H6666ff, 25, &H00bbbb, 50, &H00ff00, 75, &Hffff00, _
            100, &Hff0000}
        m.addColorScale(smoothColorScale)

        ' Add a blue (0x0000cc) pointer at the specified value
        m.addPointer(value, &H0000cc)

        ' Output the chart
        viewer.Chart = m

    End Sub

End Class

