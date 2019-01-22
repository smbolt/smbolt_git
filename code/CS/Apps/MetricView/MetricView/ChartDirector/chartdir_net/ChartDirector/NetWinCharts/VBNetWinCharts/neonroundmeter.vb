Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class neonroundmeter
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Neon Round Meters"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 4
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The value to display on the meter
        Dim value As Double = 50

        ' The main color of the four meters in this example. The other colors and gradients are
        ' derived from the main color.
        Dim colorList() As Integer = {&H007700, &H770077, &H0033dd, &H880000}
        Dim mainColor As Integer = colorList(chartIndex)

        '
        ' In this example, we demonstrate how to parameterized by size, so that the chart size can
        ' be changed by changing just one variable.
        '
        Dim size As Integer = 300

        ' The radius of the entire meter, which is size / 2, minus 2 pixels for margin
        Dim outerRadius As Integer = Int(size / 2 - 2)

        ' The radius of the meter scale
        Dim scaleRadius As Integer = Int(outerRadius * 92 / 100)

        ' The radius of the inner decorative circle
        Dim innerRadius As Integer = Int(scaleRadius * 40 / 100)

        ' The width of the color scale
        Dim colorScaleWidth As Integer = Int(scaleRadius * 10 / 100)

        ' Major tick length
        Dim tickLength As Integer = Int(scaleRadius * 10 / 100)

        ' Major tick width
        Dim tickWidth As Integer = Int(scaleRadius * 1 / 100 + 1)

        ' Label font size
        Dim fontSize As Integer = Int(scaleRadius * 13 / 100)

        '
        ' Create an angular meter based on the above parameters
        '

        ' Create an AngularMeter object of the specified size. In this demo, we use black (0x000000)
        ' as the background color. You can also use transparent or other colors.
        Dim m As AngularMeter = New AngularMeter(size, size, &H000000)

        ' Set the default text and line colors to white (0xffffff)
        m.setColor(Chart.TextColor, &Hffffff)
        m.setColor(Chart.LineColor, &Hffffff)

        ' Set meter center and scale radius, and set the scale angle from -180 to +90 degrees
        m.setMeter(size / 2, size / 2, scaleRadius, -180, 90)

        ' Background gradient with the mainColor at the center and become darker near the border
        Dim bgGradient() As Double = {0, mainColor, 0.5, m.adjustBrightness(mainColor, 0.75), 1, _
            m.adjustBrightness(mainColor, 0.15)}

        ' Fill the meter background with the background gradient
        m.addRing(0, outerRadius, m.relativeRadialGradient(bgGradient, outerRadius * 0.66))

        ' Fill the inner circle with the same background gradient for decoration
        m.addRing(0, innerRadius, m.relativeRadialGradient(bgGradient, innerRadius * 0.8))

        ' Gradient for the neon backlight, with the main color at the scale radius fading to
        ' transparent
        Dim neonGradient() As Double = {0.89, Chart.Transparent, 1, mainColor, 1.07, _
            Chart.Transparent}
        m.addRing(Int(scaleRadius * 85 / 100), outerRadius, m.relativeRadialGradient(neonGradient))

        ' The neon ring at the scale radius with width equal to 1/80 of the scale radius, creating
        ' using a brighter version of the main color
        m.addRing(scaleRadius, Int(scaleRadius + scaleRadius / 80), m.adjustBrightness(mainColor, _
            2))

        ' Meter scale is 0 - 100, with major/minor/micro ticks every 10/5/1 units
        m.setScale(0, 100, 10, 5, 1)

        ' Set the scale label style, tick length and tick width. The minor and micro tick lengths
        ' are 80% and 60% of the major tick length, and their widths are around half of the major
        ' tick width.
        m.setLabelStyle("Arial Italic", fontSize)
        m.setTickLength(-tickLength, -Int(tickLength * 80 / 100), -Int(tickLength * 60 / 100))
        m.setLineWidth(0, tickWidth, Int(tickWidth + 1 / 2), Int(tickWidth + 1 / 2))

        ' Demostrate different types of color scales and glare effects and putting them at different
        ' positions.
        Dim smoothColorScale() As Double = {0, &H0000ff, 25, &H0088ff, 50, &H00ff00, 75, &Hdddd00, _
            100, &Hff0000}
        Dim stepColorScale() As Double = {0, &H00dd00, 60, &Hddaa00, 80, &Hdd0000, 100}
        Dim highColorScale() As Double = {70, Chart.Transparent, 100, &Hff0000}

        If chartIndex = 1 Then
            ' Add the smooth color scale just outside the inner circle
            m.addColorScale(smoothColorScale, innerRadius + 1, colorScaleWidth)
            ' Add glare up to the scale radius, concave and spanning 190 degrees
            m.addGlare(scaleRadius, -190)
        ElseIf chartIndex = 2 Then
            ' Add the high color scale at the default position
            m.addColorScale(highColorScale)
            ' Add glare up to the scale radius
            m.addGlare(scaleRadius)
        Else
            ' Add the step color scale just outside the inner circle
            m.addColorScale(stepColorScale, innerRadius + 1, colorScaleWidth)
            ' Add glare up to the scale radius, concave and spanning 190 degrees and rotated by -45
            ' degrees
            m.addGlare(scaleRadius, -190, -45)
        End If

        ' Add a red (0xff0000) pointer at the specified value
        m.addPointer2(value, &Hff0000)

        ' Set the cap background to a brighter version of the mainColor, and using black (0x000000)
        ' for the cap and grey (0x999999) for the cap border
        m.setCap2(m.adjustBrightness(mainColor, 1.1), &H000000, &H999999)

        ' Output the chart
        viewer.Chart = m

    End Sub

End Class

