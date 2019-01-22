Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class blackhlinearmeter
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Black Horizontal Linear Meters"
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

        ' Create a LinearMeter object of size 250 x 75 pixels with black background and rounded
        ' corners
        Dim m As LinearMeter = New LinearMeter(250, 75, &H000000)
        m.setRoundedFrame(Chart.Transparent)

        ' Set the default text and line colors to white (0xffffff)
        m.setColor(Chart.TextColor, &Hffffff)
        m.setColor(Chart.LineColor, &Hffffff)

        ' Set the scale region top-left corner at (14, 23), with size of 218 x 20 pixels. The scale
        ' labels are located on the top (implies horizontal meter)
        m.setMeter(14, 23, 218, 20, Chart.Top)

        ' Set meter scale from 0 - 100, with a tick every 10 units
        m.setScale(0, 100, 10)

        ' The tick line width to 1 pixel
        m.setLineWidth(0, 1)

        ' Demostrate different types of color scales and putting them at different positions
        Dim smoothColorScale() As Double = {0, &H0000ff, 25, &H0088ff, 50, &H00ff00, 75, &Hdddd00, _
            100, &Hff0000}
        Dim stepColorScale() As Double = {0, &H00cc00, 50, &Heecc00, 80, &Hdd0000, 100}
        Dim highLowColorScale() As Double = {0, &H0000ff, 70, Chart.Transparent, 100, &Hff0000}

        If chartIndex = 0 Then
            ' Add the smooth color scale at the default position
            m.addColorScale(smoothColorScale)
        ElseIf chartIndex = 1 Then
            ' Add the smooth color scale starting at y = 23 (top of scale) with zero width and
            ' ending at y = 23 with 20 pixels width
            m.addColorScale(smoothColorScale, 23, 0, 23, 20)
        ElseIf chartIndex = 2 Then
            ' Add the high low scale at the default position
            m.addColorScale(highLowColorScale)
        ElseIf chartIndex = 3 Then
            ' Add the smooth color scale starting at y = 33 (center of scale) with zero width and
            ' ending at y = 23 with 20 pixels width
            m.addColorScale(smoothColorScale, 33, 0, 23, 20)
        ElseIf chartIndex = 4 Then
            ' Add the step color scale at the default position
            m.addColorScale(stepColorScale)
        Else
            ' Add the smooth color scale starting at y = 43 (bottom of scale) with zero width and
            ' ending at y = 23 with 20 pixels width
            m.addColorScale(smoothColorScale, 43, 0, 23, 20)
        End If

        ' Add a blue (0x0000cc) pointer with white (0xffffff) border at the specified value
        m.addPointer(value, &H0000cc, &Hffffff)

        ' Add a label left aligned to (10, 61) using 8pt Arial Bold font
        m.addText(10, 61, "Temperature C", "Arial Bold", 8, Chart.TextColor, Chart.Left)

        ' Add a text box right aligned to (235, 61). Display the value using white (0xffffff) 8pt
        ' Arial Bold font on a black (0x000000) background with depressed grey (0x444444) rounded
        ' border.
        Dim t As ChartDirector.TextBox = m.addText(235, 61, m.formatValue(value, "2"), _
            "Arial Bold", 8, &Hffffff, Chart.Right)
        t.setBackground(&H000000, &H444444, -1)
        t.setRoundedCorners(3)

        ' Output the chart
        viewer.Chart = m

    End Sub

End Class

