Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class squareameter
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Square Angular Meters"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 4
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The value to display on the meter
        Dim value As Double = 4.75

        ' Create an AugularMeter object of size 110 x 110 pixels, using silver background color with
        ' a black 2 pixel 3D depressed border.
        Dim m As AngularMeter = New AngularMeter(110, 110, Chart.silverColor(), &H000000, -2)

        ' Set meter appearance according to a parameter
        If chartIndex = 0 Then
            ' Set the meter center at bottom left corner (15, 95), with radius 85 pixels. Meter
            ' spans from 90 - 0 degrees.
            m.setMeter(15, 95, 85, 90, 0)
            ' Add a label to the meter centered at (35, 75)
            m.addText(35, 75, "VDC", "Arial Bold", 12, Chart.TextColor, Chart.Center)
            ' Add a text box to show the value at top right corner (103, 7)
            m.addText(103, 7, m.formatValue(value, "2"), "Arial", 8, &Hffffff, Chart.TopRight _
                ).setBackground(0, 0, -1)
        ElseIf chartIndex = 1 Then
            ' Set the meter center at top left corner (15, 15), with radius 85 pixels. Meter spans
            ' from 90 - 180 degrees.
            m.setMeter(15, 15, 85, 90, 180)
            ' Add a label to the meter centered at (35, 35)
            m.addText(35, 35, "AMP", "Arial Bold", 12, Chart.TextColor, Chart.Center)
            ' Add a text box to show the value at bottom right corner (103, 103)
            m.addText(103, 103, m.formatValue(value, "2"), "Arial", 8, &Hffffff, Chart.BottomRight _
                ).setBackground(0, 0, -1)
        ElseIf chartIndex = 2 Then
            ' Set the meter center at top right corner (15, 95), with radius 85 pixels. Meter spans
            ' from 270 - 180 degrees.
            m.setMeter(95, 15, 85, 270, 180)
            ' Add a label to the meter centered at (75, 35)
            m.addText(75, 35, "KW", "Arial Bold", 12, Chart.TextColor, Chart.Center)
            ' Add a text box to show the value at bottom left corner (7, 103)
            m.addText(7, 103, m.formatValue(value, "2"), "Arial", 8, &Hffffff, Chart.BottomLeft _
                ).setBackground(0, 0, -1)
        Else
            ' Set the meter center at bottom right corner (95, 95), with radius 85 pixels. Meter
            ' spans from 270 - 360 degrees.
            m.setMeter(95, 95, 85, 270, 360)
            ' Add a label to the meter centered at (75, 75)
            m.addText(75, 75, "RPM", "Arial Bold", 12, Chart.TextColor, Chart.Center)
            ' Add a text box to show the value at top left corner (7, 7)
            m.addText(7, 7, m.formatValue(value, "2"), "Arial", 8, &Hffffff, Chart.TopLeft _
                ).setBackground(0, 0, -1)
        End If

        ' Meter scale is 0 - 10, with a major tick every 2 units, and minor tick every 1 unit
        m.setScale(0, 10, 2, 1)

        ' Set 0 - 6 as green (99ff99) zone, 6 - 8 as yellow (ffff00) zone, and 8 - 10 as red
        ' (ff3333) zone
        m.addZone(0, 6, &H99ff99, &H808080)
        m.addZone(6, 8, &Hffff00, &H808080)
        m.addZone(8, 10, &Hff3333, &H808080)

        ' Add a semi-transparent black (80000000) pointer at the specified value
        m.addPointer(value, &H80000000)

        ' Output the chart
        viewer.Chart = m

    End Sub

End Class

