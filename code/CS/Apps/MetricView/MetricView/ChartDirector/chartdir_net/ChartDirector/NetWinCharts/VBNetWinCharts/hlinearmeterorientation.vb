Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class hlinearmeterorientation
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "H-Linear Meter Orientation"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 4
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The value to display on the meter
        Dim value As Double = 74.25

        ' Create a LinearMeter object of size 250 x 75 pixels with very light grey (0xeeeeee)
        ' backgruond and a light grey (0xccccccc) 3-pixel thick rounded frame
        Dim m As LinearMeter = New LinearMeter(250, 75, &Heeeeee, &Hcccccc)
        m.setRoundedFrame(Chart.Transparent)
        m.setThickFrame(3)

        ' This example demonstrates putting the text labels at the top or bottom. This is by setting
        ' the label alignment, scale position and label position.
        Dim alignment() As Integer = {Chart.Top, Chart.Top, Chart.Bottom, Chart.Bottom}
        Dim meterYPos() As Integer = {23, 23, 34, 34}
        Dim labelYPos() As Integer = {61, 61, 15, 15}

        ' Set the scale region
        m.setMeter(14, meterYPos(chartIndex), 218, 20, alignment(chartIndex))

        ' Set meter scale from 0 - 100, with a tick every 10 units
        m.setScale(0, 100, 10)

        ' Add a smooth color scale at the default position
        Dim smoothColorScale() As Double = {0, &H6666ff, 25, &H00bbbb, 50, &H00ff00, 75, &Hffff00, _
            100, &Hff0000}
        m.addColorScale(smoothColorScale)

        ' Add a blue (0x0000cc) pointer at the specified value
        m.addPointer(value, &H0000cc)

        '
        ' In this example, some charts have the "Temperauture" label on the left side and the value
        ' readout on the right side, and some charts have the reverse
        '

        If chartIndex Mod 2 = 0 Then
            ' Add a label on the left side using 8pt Arial Bold font
            m.addText(10, labelYPos(chartIndex), "Temperature C", "Arial Bold", 8, _
                Chart.TextColor, Chart.Left)

            ' Add a text box on the right side. Display the value using white (0xffffff) 8pt Arial
            ' Bold font on a black (0x000000) background with depressed rounded border.
            Dim t As ChartDirector.TextBox = m.addText(235, labelYPos(chartIndex), m.formatValue( _
                value, "2"), "Arial Bold", 8, &Hffffff, Chart.Right)
            t.setBackground(&H000000, &H000000, -1)
            t.setRoundedCorners(3)
        Else
            ' Add a label on the right side using 8pt Arial Bold font
            m.addText(237, labelYPos(chartIndex), "Temperature C", "Arial Bold", 8, _
                Chart.TextColor, Chart.Right)

            ' Add a text box on the left side. Display the value using white (0xffffff) 8pt Arial
            ' Bold font on a black (0x000000) background with depressed rounded border.
            Dim t As ChartDirector.TextBox = m.addText(11, labelYPos(chartIndex), m.formatValue( _
                value, "2"), "Arial Bold", 8, &Hffffff, Chart.Left)
            t.setBackground(&H000000, &H000000, -1)
            t.setRoundedCorners(3)
        End If

        ' Output the chart
        viewer.Chart = m

    End Sub

End Class

