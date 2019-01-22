Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class threedanglepie
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "3D Angle"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 7
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' the tilt angle of the pie
        Dim angle As Integer = chartIndex * 15

        ' The data for the pie chart
        Dim data() As Double = {25, 18, 15, 12, 8, 30, 35}

        ' The labels for the pie chart
        Dim labels() As String = {"Labor", "Licenses", "Taxes", "Legal", "Insurance", _
            "Facilities", "Production"}

        ' Create a PieChart object of size 100 x 110 pixels
        Dim c As PieChart = New PieChart(100, 110)

        ' Set the center of the pie at (50, 55) and the radius to 38 pixels
        c.setPieSize(50, 55, 38)

        ' Set the depth and tilt angle of the 3D pie (-1 means auto depth)
        c.set3D(-1, angle)

        ' Add a title showing the tilt angle
        c.addTitle("Tilt = " & angle & " deg", "Arial", 8)

        ' Set the pie data
        c.setData(data, labels)

        ' Disable the sector labels by setting the color to Transparent
        c.setLabelStyle("", 8, Chart.Transparent)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{label}: US${value}K ({percent}%)'")

    End Sub

End Class

