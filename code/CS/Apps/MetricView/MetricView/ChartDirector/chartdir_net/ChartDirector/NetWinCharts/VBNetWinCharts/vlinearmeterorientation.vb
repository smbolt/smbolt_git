Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class vlinearmeterorientation
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "V-Linear Meter Orientation"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 2
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The value to display on the meter
        Dim value As Double = 75.35

        ' Create a LinearMeter object of size 70 x 240 pixels with very light grey (0xeeeeee)
        ' backgruond and a light grey (0xccccccc) 3-pixel thick rounded frame
        Dim m As LinearMeter = New LinearMeter(70, 240, &Heeeeee, &Hcccccc)
        m.setRoundedFrame(Chart.Transparent)
        m.setThickFrame(3)

        ' This example demonstrates putting the text labels at the left or right side by setting the
        ' label alignment and scale position.
        If chartIndex = 0 Then
            m.setMeter(28, 18, 20, 205, Chart.Left)
        Else
            m.setMeter(20, 18, 20, 205, Chart.Right)
        End If

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

