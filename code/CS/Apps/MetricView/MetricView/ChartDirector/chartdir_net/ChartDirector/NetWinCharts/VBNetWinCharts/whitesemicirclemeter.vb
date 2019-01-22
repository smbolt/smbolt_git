Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class whitesemicirclemeter
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "White Semicircle Meters"
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

        ' Set the default text and line colors to very dark grey (0x222222)
        m.setColor(Chart.TextColor, &H222222)
        m.setColor(Chart.LineColor, &H222222)

        ' Center at (150, 150), scale radius = 128 pixels, scale angle -90 to +90 degrees
        m.setMeter(150, 150, 128, -90, 90)

        ' Gradient color for the border to make it silver-like
        Dim ringGradient() As Double = {1, &H999999, 0.5, &Hdddddd, 0, &Hf8f8f8, -0.5, &Hdddddd, _
            -1, &H999999}

        ' Background gradient color from white (0xffffff) at the center to light grey (0xdddddd) at
        ' the border
        Dim bgGradient() As Double = {0, &Hffffff, 0.75, &Heeeeee, 1, &Hdddddd}

        ' Add a scale background of 148 pixels radius using the gradient background, with a 10 pixel
        ' thick silver border
        m.addScaleBackground(148, m.relativeRadialGradient(bgGradient, 148), 10, _
            m.relativeLinearGradient(ringGradient, 45, 148))

        ' Add a 1 pixel light grey (0xbbbbbb) line at the inner edge of the thick silver border
        ' (radius = 138) to enhance its contrast with the background gradient
        m.addScaleBackground(138, Chart.Transparent, 1, &Hbbbbbb)

        ' Meter scale is 0 - 100, with major tick every 20 units, minor tick every 10 units, and
        ' micro tick every 5 units
        m.setScale(0, 100, 20, 10, 5)

        ' Set the scale label style to 15pt Arial Italic. Set the major/minor/micro tick lengths to
        ' 16/16/10 pixels pointing inwards, and their widths to 2/1/1 pixels.
        m.setLabelStyle("Arial Italic", 16)
        m.setTickLength(-16, -16, -10)
        m.setLineWidth(0, 2, 1, 1)

        ' Demostrate different types of color scales and putting them at different positions
        Dim smoothColorScale() As Double = {0, &H3333ff, 25, &H0088ff, 50, &H00ff00, 75, &Hdddd00, _
            100, &Hff0000}
        Dim stepColorScale() As Double = {0, &H00cc00, 60, &Hffdd00, 80, &Hee0000, 100}
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

        ' Output the chart
        viewer.Chart = m

    End Sub

End Class

