Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class whitevbarmeter
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "White Vertical Bar Meters"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 6
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The value to display on the meter
        Dim value As Double = 74.25

        ' Create a LinearMeter object of size 70 x 260 pixels with very light grey (0xeeeeee)
        ' backgruond and a grey (0xbbbbbb) 3-pixel thick rounded frame
        Dim m As LinearMeter = New LinearMeter(70, 260, &Heeeeee, &Hbbbbbb)
        m.setRoundedFrame(Chart.Transparent)
        m.setThickFrame(3)

        ' Set the scale region top-left corner at (28, 33), with size of 20 x 194 pixels. The scale
        ' labels are located on the left (default - implies vertical meter).
        m.setMeter(28, 33, 20, 194)

        ' Set meter scale from 0 - 100, with a tick every 10 units
        m.setScale(0, 100, 10)

        ' Demostrate different types of color scales
        Dim smoothColorScale() As Double = {0, &H0000ff, 25, &H0088ff, 50, &H00ff00, 75, &Hdddd00, _
            100, &Hff0000}
        Dim stepColorScale() As Double = {0, &H00dd00, 50, &Hffff00, 80, &Hff0000, 100}
        Dim highColorScale() As Double = {70, Chart.Transparent, 100, &Hff0000}
        Dim lowColorScale() As Double = {0, &H0000ff, 30, Chart.Transparent}

        If chartIndex = 0 Then
            ' Add a blue (0x0088ff) bar from 0 to value with glass effect and 4 pixel rounded
            ' corners
            m.addBar(0, value, &H0088ff, Chart.glassEffect(Chart.NormalGlare, Chart.Left), 4)
            ' Add a 6-pixel thick smooth color scale at x = 53 (right of meter scale)
            m.addColorScale(smoothColorScale, 53, 6)
        ElseIf chartIndex = 1 Then
             ' Add a green (0x00cc00) bar from 0 to value with bar lighting effect and 4 pixel
             ' rounded corners
            m.addBar(0, value, &H00cc00, Chart.barLighting(), 4)
            ' Add a high only color scale at x = 52 (right of meter scale) with thickness varying
            ' from 0 to 8
            m.addColorScale(highColorScale, 52, 0, 52, 8)
            ' Add a low only color scale at x = 52 (right of meter scale) with thickness varying
            ' from 8 to 0
            m.addColorScale(lowColorScale, 52, 8, 52, 0)
        ElseIf chartIndex = 2 Then
            ' Add a purple (0x0088ff) bar from 0 to value with glass effect and 4 pixel rounded
            ' corners
            m.addBar(0, value, &H8833dd, Chart.glassEffect(Chart.NormalGlare, Chart.Left), 4)
            ' Add a high only color scale at x = 52 (right of meter scale) with thickness varying
            ' from 0 to 8
            m.addColorScale(highColorScale, 52, 0, 52, 8)
        ElseIf chartIndex = 3 Then
             ' Add a orange (0xff8800) bar from 0 to value with cylinder lighting effect
            m.addBar(0, value, &Hff8800, Chart.cylinderEffect())
            ' Add a high only color scale at x = 53 (right of meter scale)
            m.addColorScale(highColorScale, 53, 6)
        ElseIf chartIndex = 4 Then
            ' Add a red (0xee3333) bar from 0 to value with glass effect and 4 pixel rounded corners
            m.addBar(0, value, &Hee3333, Chart.glassEffect(Chart.NormalGlare, Chart.Left), 4)
            ' Add a step color scale at x = 53 (right of meter scale)
            m.addColorScale(stepColorScale, 53, 6)
        Else
             ' Add a grey (0xaaaaaa) bar from 0 to value
            m.addBar(0, value, &Haaaaaa)
            ' Add a smooth color scale at x = 52 (right of meter scale) with thickness varying from
            ' 0 to 8
            m.addColorScale(smoothColorScale, 52, 0, 52, 8)
        End If

        ' Add a title using 8pt Arial Bold font with grey (0xbbbbbb) background
        m.addTitle("Temp C", "Arial Bold", 8, Chart.TextColor).setBackground(&Hbbbbbb)

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

