Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class colorsemicirclemeter
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Color Semicircle Meters"
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

        ' The background and border colors of the meters
        Dim bgColor() As Integer = {&H88ccff, &Hffdddd, &Hffddaa, &Hffccff, &Hdddddd, &Hccffcc}
        Dim borderColor() As Integer = {&H000077, &H880000, &Hee6600, &H440088, &H000000, &H006000}

        ' Create an AngularMeter object of size 300 x 180 pixels with transparent background
        Dim m As AngularMeter = New AngularMeter(300, 180, Chart.Transparent)

        ' Center at (150, 150), scale radius = 124 pixels, scale angle -90 to +90 degrees
        m.setMeter(150, 150, 124, -90, 90)

        ' Background gradient color with brighter color at the center
        Dim bgGradient() As Double = {0, m.adjustBrightness(bgColor(chartIndex), 3), 0.75, _
            bgColor(chartIndex)}

        ' Add a scale background of 148 pixels radius using the background gradient, with a 13 pixel
        ' thick border
        m.addScaleBackground(148, m.relativeRadialGradient(bgGradient), 13, borderColor(chartIndex))

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
            ' Add the smooth color scale starting at radius 124 with zero width and ending at radius
            ' 124 with 16 pixels inner width
            m.addColorScale(smoothColorScale, 124, 0, 124, -16)
        ElseIf chartIndex = 2 Then
            ' Add the smooth color scale starting at radius 65 with zero width and ending at radius
            ' 55 with 20 pixels outer width
            m.addColorScale(smoothColorScale, 65, 0, 55, 20)
        ElseIf chartIndex = 3 Then
            ' Add the high/low color scale at the default position
            m.addColorScale(highLowColorScale)
        ElseIf chartIndex = 4 Then
            ' Add the step color scale at the default position
            m.addColorScale(stepColorScale)
        Else
            ' Add the smooth color scale at radius 55 with 20 pixels outer width
            m.addColorScale(smoothColorScale, 55, 20)
        End If

        ' Add a text label centered at (150, 125) with 15pt Arial Italic font
        m.addText(150, 125, "CPU", "Arial Italic", 15, Chart.TextColor, Chart.BottomCenter)

        ' Demonstrate two different types of pointers - thin triangular pointer (the default) and
        ' line pointer
        If chartIndex Mod 2 = 0 Then
            m.addPointer2(value, &Hff0000)
        Else
            m.addPointer2(value, &Hff0000, -1, Chart.LinePointer2)
        End If

        ' Output the chart
        viewer.Chart = m

    End Sub

End Class

