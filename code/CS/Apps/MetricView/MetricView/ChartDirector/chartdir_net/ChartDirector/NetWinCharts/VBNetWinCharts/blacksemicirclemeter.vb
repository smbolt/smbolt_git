Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class blacksemicirclemeter
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Black Semicircle Meters"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 6
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The value to display on the meter
        Dim value As Double = 72.55

        ' Create an AngularMeter object of size 300 x 180 pixels with transparent background
        Dim m As AngularMeter = New AngularMeter(300, 180, Chart.Transparent)

        ' Set the default text and line colors to white (0xffffff)
        m.setColor(Chart.TextColor, &Hffffff)
        m.setColor(Chart.LineColor, &Hffffff)

        ' Center at (150, 150), scale radius = 128 pixels, scale angle -90 to +90 degrees
        m.setMeter(150, 150, 128, -90, 90)

        ' Gradient color for the border to make it silver-like
        Dim ringGradient() As Double = {1, &H909090, 0.5, &Hd6d6d6, 0, &Heeeeee, -0.5, &Hd6d6d6, _
            -1, &H909090}

        ' Add a black (0x000000) scale background of 148 pixels radius with a 10 pixel thick silver
        ' border
        m.addScaleBackground(148, 0, 10, m.relativeLinearGradient(ringGradient, 45, 148))

        ' Meter scale is 0 - 100, with major tick every 20 units, minor tick every 10 units, and
        ' micro tick every 5 units
        m.setScale(0, 100, 20, 10, 5)

        ' Set the scale label style to 15pt Arial Italic. Set the major/minor/micro tick lengths to
        ' 16/16/10 pixels pointing inwards, and their widths to 2/1/1 pixels.
        m.setLabelStyle("Arial Italic", 16)
        m.setTickLength(-16, -16, -10)
        m.setLineWidth(0, 2, 1, 1)

        ' Demostrate different types of color scales and putting them at different positions
        Dim smoothColorScale() As Double = {0, &H0000ff, 25, &H0088ff, 50, &H00ff00, 75, &Hdddd00, _
            100, &Hff0000}
        Dim stepColorScale() As Double = {0, &H00aa00, 60, &Hddaa00, 80, &Hcc0000, 100}
        Dim highLowColorScale() As Double = {0, &H00ff00, 70, Chart.Transparent, 100, &Hff0000}

        If chartIndex = 0 Then
            ' Add the smooth color scale at the default position
            m.addColorScale(smoothColorScale)
        ElseIf chartIndex = 1 Then
            ' Add the smooth color scale starting at radius 128 with zero width and ending at radius
            ' 128 with 16 pixels inner width
            m.addColorScale(smoothColorScale, 128, 0, 128, -16)
        ElseIf chartIndex = 2 Then
            ' Add the smooth color scale starting at radius 70 with zero width and ending at radius
            ' 60 with 20 pixels outer width
            m.addColorScale(smoothColorScale, 70, 0, 60, 20)
        ElseIf chartIndex = 3 Then
            ' Add the high/low color scale at the default position
            m.addColorScale(highLowColorScale)
        ElseIf chartIndex = 4 Then
            ' Add the step color scale at the default position
            m.addColorScale(stepColorScale)
        Else
            ' Add the smooth color scale at radius 60 with 15 pixels outer width
            m.addColorScale(smoothColorScale, 60, 15)
        End If

        ' Add a text label centered at (150, 125) with 15pt Arial Italic font
        m.addText(150, 125, "CPU", "Arial Italic", 15, Chart.TextColor, Chart.BottomCenter)

        ' Add a red (0xff0000) pointer at the specified value
        m.addPointer2(value, &Hff0000)

        ' Add glare up to radius 138 (= region inside border)
        If chartIndex Mod 2 = 0 Then
            m.addGlare(138)
        End If

        ' Output the chart
        viewer.Chart = m

    End Sub

End Class

