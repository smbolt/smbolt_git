Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class blackvbarmeter
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Black Vertical Bar Meters"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 6
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The value to display on the meter
        Dim value As Double = 66.77

        ' Create a LinearMeter object of size 70 x 260 pixels with black background and rounded
        ' corners
        Dim m As LinearMeter = New LinearMeter(70, 260, &H000000)
        m.setRoundedFrame(Chart.Transparent)

        ' Set the default text and line colors to white (0xffffff)
        m.setColor(Chart.TextColor, &Hffffff)
        m.setColor(Chart.LineColor, &Hffffff)

        ' Set the scale region top-left corner at (36, 30), with size of 20 x 196 pixels. The scale
        ' labels are located on the left (default - implies vertical meter).
        m.setMeter(36, 30, 20, 196)

        ' Set meter scale from 0 - 100, with a tick every 10 units
        m.setScale(0, 100, 10)

        ' Add the chart title at the top center
        Dim title As ChartDirector.TextBox = m.addText(m.getWidth() / 2, 5, "Temp C", _
            "Arial Bold", 8, Chart.TextColor, Chart.TopCenter)

        ' Move the scale labels 8 pixels from the meter scale to make room for the color scale
        m.setLabelPos(False, 8)

        ' Demostrate different types of color scales
        Dim smoothColorScale() As Double = {0, &H0000ff, 25, &H0088ff, 50, &H00ff00, 75, &Hdddd00, _
            100, &Hff0000}
        Dim stepColorScale() As Double = {0, &H00dd00, 50, &Hffff00, 80, &Hff0000, 100}
        Dim highLowColorScale() As Double = {0, &H0000ff, 40, Chart.Transparent, 60, _
            Chart.Transparent, 100, &Hff0000}
        Dim highColorScale() As Double = {70, Chart.Transparent, 100, &Hff0000}

        If chartIndex = 0 Then
            ' Add a blue (0x0088ff) bar from 0 to value with glass effect and 4 pixel rounded
            ' corners
            m.addBar(0, value, &H0088ff, Chart.glassEffect(Chart.NormalGlare, Chart.Left), 4)
            ' Add a 4-pixel thick smooth color scale at x = 29 (left of meter scale)
            m.addColorScale(smoothColorScale, 29, 4)
        ElseIf chartIndex = 1 Then
             ' Add a yellow (0xCC9922) bar from 0 to value with bar lighting effect
            m.addBar(0, value, &Hcc9922, Chart.barLighting())
            ' Add a 4-pixel thick smooth color scale at x = 29 (left of meter scale)
            m.addColorScale(smoothColorScale, 29, 4)

            ' Set the title style to black text on a yellow (0xcc9922) background with rounded
            ' corners
            title.setFontColor(&H000000)
            title.setBackground(&Hcc9922)
            title.setRoundedCorners(2)
        ElseIf chartIndex = 2 Then
             ' Add a grey (0xaaaaaa) bar from 0 to value with bar lighting effect
            m.addBar(0, value, &Haaaaaa, Chart.barLighting())
            ' Add a 4-pixel high/low color scale at x = 29 (left of meter scale)
            m.addColorScale(highLowColorScale, 29, 4)
        ElseIf chartIndex = 3 Then
             ' Add a brown (0xbb6622) bar from 0 to value with cylinder lighting effect
            m.addBar(0, value, &Hbb6622, Chart.cylinderEffect())
            ' Add a 4 pixel thick brown (0xbb6622) frame
            m.setThickFrame(4, &Hbb6622)
            ' Add a high only color scale at x = 29 (left of meter scale)
            m.addColorScale(highColorScale, 29, 4)
        ElseIf chartIndex = 4 Then
            ' Add a purple (0x7755ee) bar from 0 to value
            m.addBar(0, value, &H7755ee)
            ' Add a 4 pixel thick purple (0x880088) frame
            m.setThickFrame(4, &H880088)
            ' Add a 4-pixel high/low color scale at x = 29 (left of meter scale)
            m.addColorScale(highLowColorScale, 29, 4)
        Else
             ' Add a green (0x00bb00) bar from 0 to value with soft lighting effect and 4 pixel
             ' rounded corners
            m.addBar(0, value, &H00bb00, Chart.softLighting(), 4)
            ' Add a 4-pixel step color scale at x = 29 (left of meter scale)
            m.addColorScale(stepColorScale, 29, 4)
        End If

        ' Add a text box at the bottom-center. Display the value using white (0xffffff) 8pt Arial
        ' Bold font on a black (0x000000) background with depressed grey (0x333333) rounded border.
        Dim t As ChartDirector.TextBox = m.addText(m.getWidth() / 2, m.getHeight() - 7, _
            m.formatValue(value, "2"), "Arial Bold", 8, &Hffffff, Chart.BottomCenter)
        t.setBackground(&H000000, &H333333, -1)
        t.setRoundedCorners(3)

        ' Output the chart
        viewer.Chart = m

    End Sub

End Class

