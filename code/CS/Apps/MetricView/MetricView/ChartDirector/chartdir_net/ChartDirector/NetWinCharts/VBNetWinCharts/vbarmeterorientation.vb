Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class vbarmeterorientation
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "V-Bar Meter Orientation"
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

        ' Bar colors of the meters
        Dim barColor() As Integer = {&H2299ff, &H00ee00, &Haa66ee, &Hff7711}

        ' Create a LinearMeter object of size 70 x 240 pixels with very light grey (0xeeeeee)
        ' backgruond and a grey (0xaaaaaa) 3-pixel thick rounded frame
        Dim m As LinearMeter = New LinearMeter(70, 240, &Heeeeee, &Haaaaaa)
        m.setRoundedFrame(Chart.Transparent)
        m.setThickFrame(3)

        ' This example demonstrates putting the text labels at the left or right side of the meter
        ' scale, and putting the color scale on the same side as the labels or on opposite side.
        Dim alignment() As Integer = {Chart.Left, Chart.Left, Chart.Right, Chart.Right}
        Dim meterXPos() As Integer = {28, 38, 12, 21}
        Dim labelGap() As Integer = {2, 12, 10, 2}
        Dim colorScalePos() As Integer = {53, 28, 36, 10}

        ' Configure the position of the meter scale and which side to put the text labels
        m.setMeter(meterXPos(chartIndex), 18, 20, 205, alignment(chartIndex))

        ' Set meter scale from 0 - 100, with a tick every 10 units
        m.setScale(0, 100, 10)

        ' To put the color scale on the same side as the text labels, we need to increase the gap
        ' between the labels and the meter scale to make room for the color scale
        m.setLabelPos(False, labelGap(chartIndex))

        ' Add a smooth color scale to the meter
        Dim smoothColorScale() As Double = {0, &H0000ff, 25, &H0088ff, 50, &H00ff00, 75, &Hdddd00, _
            100, &Hff0000}
        m.addColorScale(smoothColorScale, colorScalePos(chartIndex), 6)

        ' Add a bar from 0 to value with glass effect and 4 pixel rounded corners
        m.addBar(0, value, barColor(chartIndex), Chart.glassEffect(Chart.NormalGlare, Chart.Left),4)

        ' Output the chart
        viewer.Chart = m

    End Sub

End Class

