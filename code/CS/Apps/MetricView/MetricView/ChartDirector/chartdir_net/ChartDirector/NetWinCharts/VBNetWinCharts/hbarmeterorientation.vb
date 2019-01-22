Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class hbarmeterorientation
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "H-Bar Meter Orientation"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 4
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The value to display on the meter
        Dim value As Double = 75.35

        ' The bar colors for the meters
        Dim barColor() As Integer = {&H0088ff, &H00cc00, &H8833dd, &Hff8800}

        ' Create a LinearMeter object of size 260 x 80 pixels with very light grey (0xeeeeee)
        ' backgruond and a light grey (0xccccccc) 3-pixel thick rounded frame
        Dim m As LinearMeter = New LinearMeter(260, 80, &Heeeeee, &Hcccccc)
        m.setRoundedFrame(Chart.Transparent)
        m.setThickFrame(3)

        ' This example demonstrates putting the text labels at the top or bottom. This is by setting
        ' the label alignment, as well as the scale, color scale and label positions.
        Dim alignment() As Integer = {Chart.Top, Chart.Top, Chart.Bottom, Chart.Bottom}
        Dim meterYPos() As Integer = {24, 24, 36, 36}
        Dim labelYPos() As Integer = {65, 65, 16, 16}
        Dim colorScalePos() As Integer = {48, 48, 27, 27}

        ' Set the scale region
        m.setMeter(18, meterYPos(chartIndex), 222, 20, alignment(chartIndex))

        ' Set meter scale from 0 - 100, with a tick every 10 units
        m.setScale(0, 100, 10)

        ' Add a 5-pixel thick smooth color scale
        Dim smoothColorScale() As Double = {0, &H0000ff, 25, &H0088ff, 50, &H00ff00, 75, &Hdddd00, _
            100, &Hff0000}
        m.addColorScale(smoothColorScale, colorScalePos(chartIndex), 5)

        ' Add a bar from 0 to value with glass effect and 4 pixel rounded corners
        m.addBar(0, value, barColor(chartIndex), Chart.glassEffect(Chart.NormalGlare, Chart.Top),4)

        '
        ' In this example, some charts have the "Temperauture" label on the left side and the value
        ' readout on the right side, and some charts have the reverse
        '

        If chartIndex Mod 2 = 0 Then
            ' Add a label on the left side using 8pt Arial Bold font
            m.addText(15, labelYPos(chartIndex), "Temperature C", "Arial Bold", 8, _
                Chart.TextColor, Chart.Left)

            ' Add a text box on the right side. Display the value using white (0xffffff) 8pt Arial
            ' Bold font on a black (0x000000) background with depressed rounded border.
            Dim t As ChartDirector.TextBox = m.addText(240, labelYPos(chartIndex), m.formatValue( _
                value, "2"), "Arial", 8, &Hffffff, Chart.Right)
            t.setBackground(&H000000, &H000000, -1)
            t.setRoundedCorners(3)
        Else
            ' Add a label on the right side using 8pt Arial Bold font
            m.addText(243, labelYPos(chartIndex), "Temperature C", "Arial Bold", 8, _
                Chart.TextColor, Chart.Right)

            ' Add a text box on the left side. Display the value using white (0xffffff) 8pt Arial
            ' Bold font on a black (0x000000) background with depressed rounded border.
            Dim t As ChartDirector.TextBox = m.addText(18, labelYPos(chartIndex), m.formatValue( _
                value, "2"), "Arial", 8, &Hffffff, Chart.Left)
            t.setBackground(&H000000, &H000000, -1)
            t.setRoundedCorners(3)
        End If

        ' Output the chart
        viewer.Chart = m

    End Sub

End Class

