Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class pieshading
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "2D Pie Shading"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 6
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the pie chart
        Dim data() As Double = {18, 30, 20, 15}

        ' The labels for the pie chart
        Dim labels() As String = {"Labor", "Licenses", "Facilities", "Production"}

        ' The colors to use for the sectors
        Dim colors() As Integer = {&H66aaee, &Heebb22, &Hbbbbbb, &H8844ff}

        ' Create a PieChart object of size 200 x 220 pixels. Use a vertical gradient color from blue
        ' (0000cc) to deep blue (000044) as background. Use rounded corners of 16 pixels radius.
        Dim c As PieChart = New PieChart(200, 220)
        c.setBackground(c.linearGradientColor(0, 0, 0, c.getHeight(), &H0000cc, &H000044))
        c.setRoundedFrame(&Hffffff, 16)

        ' Set the center of the pie at (100, 120) and the radius to 80 pixels
        c.setPieSize(100, 120, 80)

        ' Set the pie data
        c.setData(data, labels)

        ' Set the sector colors
        c.setColors2(Chart.DataColor, colors)

        ' Demonstrates various shading modes
        If chartIndex = 0 Then
            c.addTitle("Default Shading", "bold", 12, &Hffffff)
        ElseIf chartIndex = 1 Then
            c.addTitle("Local Gradient", "bold", 12, &Hffffff)
            c.setSectorStyle(Chart.LocalGradientShading)
        ElseIf chartIndex = 2 Then
            c.addTitle("Global Gradient", "bold", 12, &Hffffff)
            c.setSectorStyle(Chart.GlobalGradientShading)
        ElseIf chartIndex = 3 Then
            c.addTitle("Concave Shading", "bold", 12, &Hffffff)
            c.setSectorStyle(Chart.ConcaveShading)
        ElseIf chartIndex = 4 Then
            c.addTitle("Rounded Edge", "bold", 12, &Hffffff)
            c.setSectorStyle(Chart.RoundedEdgeShading)
        ElseIf chartIndex = 5 Then
            c.addTitle("Radial Gradient", "bold", 12, &Hffffff)
            c.setSectorStyle(Chart.RadialShading)
        End If

        ' Disable the sector labels by setting the color to Transparent
        c.setLabelStyle("", 8, Chart.Transparent)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{label}: US${value}K ({percent}%)'")

    End Sub

End Class

