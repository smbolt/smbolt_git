Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class circlelabelpie
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Circular Label Layout"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 2
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the pie chart
        Dim data() As Double = {42, 18, 8}

        ' The labels for the pie chart
        Dim labels() As String = {"Agree", "Disagree", "Not Sure"}

        ' The colors to use for the sectors
        Dim colors() As Integer = {&H66ff66, &Hff6666, &Hffff00}

        ' Create a PieChart object of size 300 x 300 pixels. Set the background to a gradient color
        ' from blue (aaccff) to sky blue (ffffff), with a grey (888888) border. Use rounded corners
        ' and soft drop shadow.
        Dim c As PieChart = New PieChart(300, 300)
        c.setBackground(c.linearGradientColor(0, 0, 0, c.getHeight() / 2, &Haaccff, &Hffffff), _
            &H888888)
        c.setRoundedFrame()
        c.setDropShadow()

        If chartIndex = 0 Then
        '============================================================
        '    Draw a pie chart where the label is on top of the pie
        '============================================================

            ' Set the center of the pie at (150, 150) and the radius to 120 pixels
            c.setPieSize(150, 150, 120)

            ' Set the label position to -40 pixels from the perimeter of the pie (-ve means label is
            ' inside the pie)
            c.setLabelPos(-40)

        Else
        '============================================================
        '    Draw a pie chart where the label is outside the pie
        '============================================================

            ' Set the center of the pie at (150, 150) and the radius to 80 pixels
            c.setPieSize(150, 150, 80)

            ' Set the sector label position to be 20 pixels from the pie. Use a join line to connect
            ' the labels to the sectors.
            c.setLabelPos(20, Chart.LineColor)

        End If

        ' Set the pie data and the pie labels
        c.setData(data, labels)

        ' Set the sector colors
        c.setColors2(Chart.DataColor, colors)

        ' Use local gradient shading, with a 1 pixel semi-transparent black (bb000000) border
        c.setSectorStyle(Chart.LocalGradientShading, &Hbb000000, 1)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{label}: {value} responses ({percent}%)'")

    End Sub

End Class

