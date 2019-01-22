Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class whitevlinearmeter
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "White Vertical Linear Meters"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 6
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The value to display on the meter
        Dim value As Double = 75.35

        ' Create a LinearMeter object of size 250 x 75 pixels with very light grey (0xeeeeee)
        ' backgruond and a light grey (0xccccccc) 3-pixel thick rounded frame
        Dim m As LinearMeter = New LinearMeter(70, 260, &Heeeeee, &Hcccccc)
        m.setRoundedFrame(Chart.Transparent)
        m.setThickFrame(3)

        ' Set the scale region top-left corner at (28, 30), with size of 20 x 196 pixels. The scale
        ' labels are located on the left (default - implies vertical meter)
        m.setMeter(28, 30, 20, 196)

        ' Set meter scale from 0 - 100, with a tick every 10 units
        m.setScale(0, 100, 10)

        ' Demostrate different types of color scales and putting them at different positions
        Dim smoothColorScale() As Double = {0, &H6666ff, 25, &H00bbbb, 50, &H00ff00, 75, &Hffff00, _
            100, &Hff0000}
        Dim stepColorScale() As Double = {0, &H33ff33, 50, &Hffff33, 80, &Hff3333, 100}
        Dim highLowColorScale() As Double = {0, &H6666ff, 70, Chart.Transparent, 100, &Hff0000}

        If chartIndex = 0 Then
            ' Add the smooth color scale at the default position
            m.addColorScale(smoothColorScale)
        ElseIf chartIndex = 1 Then
            ' Add the step color scale at the default position
            m.addColorScale(stepColorScale)
        ElseIf chartIndex = 2 Then
            ' Add the high low scale at the default position
            m.addColorScale(highLowColorScale)
        ElseIf chartIndex = 3 Then
            ' Add the smooth color scale starting at x = 28 (left of scale) with zero width and
            ' ending at x = 28 with 20 pixels width
            m.addColorScale(smoothColorScale, 28, 0, 28, 20)
        ElseIf chartIndex = 4 Then
            ' Add the smooth color scale starting at x = 38 (center of scale) with zero width and
            ' ending at x = 28 with 20 pixels width
            m.addColorScale(smoothColorScale, 38, 0, 28, 20)
        Else
            ' Add the smooth color scale starting at x = 48 (right of scale) with zero width and
            ' ending at x = 28 with 20 pixels width
            m.addColorScale(smoothColorScale, 48, 0, 28, 20)
        End If

        ' In this demo, we demostrate pointers of different shapes
        If chartIndex < 3 Then
            ' Add a blue (0x0000cc) pointer of default shape at the specified value
            m.addPointer(value, &H0000cc)
        Else
            ' Add a blue (0x0000cc) pointer of triangular shape the specified value
            m.addPointer(value, &H0000cc).setShape(Chart.TriangularPointer, 0.7, 0.5)
        End If

        ' Add a title using 8pt Arial Bold font with a border color background
        m.addTitle("Temp C", "Arial Bold", 8, Chart.TextColor).setBackground(&Hcccccc)

        ' Add a text box at the bottom-center. Display the value using white (0xffffff) 8pt Arial
        ' Bold font on a black (0x000000) background with rounded border.
        Dim t As ChartDirector.TextBox = m.addText(m.getWidth() / 2, m.getHeight() - 8, _
            m.formatValue(value, "2"), "Arial Bold", 8, &Hffffff, Chart.Bottom)
        t.setBackground(&H000000)
        t.setRoundedCorners(3)
        t.setMargin(5, 5, 2, 1)

        ' Output the chart
        viewer.Chart = m

    End Sub

End Class

