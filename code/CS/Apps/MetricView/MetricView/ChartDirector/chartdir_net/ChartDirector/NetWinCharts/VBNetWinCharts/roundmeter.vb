Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class roundmeter
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Round Meter"
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
        Dim value As Double = 72.3

        ' Create an AngularMeter object of size 250 x 250 pixels with transparent background
        Dim m As AngularMeter = New AngularMeter(250, 250, Chart.Transparent)

        ' Center at (125, 125), scale radius = 111 pixels, scale angle -145 to +145 degrees
        m.setMeter(125, 125, 111, -145, 145)

        ' Add a very light grey (0xeeeeee) circle with radius 123 pixels as background
        m.addRing(0, 123, &Heeeeee)
        ' Add a grey (0xcccccc) ring between radii 116 and 123 pixels as border
        m.addRing(116, 123, &Hcccccc)

        ' Meter scale is 0 - 100, with major/minor/micro ticks every 10/5/1 units
        m.setScale(0, 100, 10, 5, 1)

        ' Set the scale label style to 15pt Arial Italic. Set the major/minor/micro tick lengths to
        ' 12/9/6 pixels pointing inwards, and their widths to 2/1/1 pixels.
        m.setLabelStyle("Arial Italic", 15)
        m.setTickLength(-12, -9, -6)
        m.setLineWidth(0, 2, 1, 1)

        ' Add a smooth color scale to the meter
        Dim smoothColorScale() As Double = {0, &H3333ff, 25, &H0088ff, 50, &H00ff00, 75, &Hdddd00, _
            100, &Hff0000}
        m.addColorScale(smoothColorScale)

        ' Add a text label centered at (125, 175) with 15pt Arial Italic font
        m.addText(125, 175, "CPU", "Arial Italic", 15, Chart.TextColor, Chart.Center)

        ' Add a red (0xff0000) pointer at the specified value
        m.addPointer2(value, &Hff0000)

        ' Output the chart
        viewer.Chart = m

    End Sub

End Class

