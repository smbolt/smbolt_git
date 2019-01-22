Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class donutwidth
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Donut Width"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 5
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' Determine the donut inner radius (as percentage of outer radius) based on input parameter
        Dim donutRadius As Integer = chartIndex * 25

        ' The data for the pie chart
        Dim data() As Double = {10, 10, 10, 10, 10}

        ' The labels for the pie chart
        Dim labels() As String = {"Marble", "Wood", "Granite", "Plastic", "Metal"}

        ' Create a PieChart object of size 150 x 120 pixels, with a grey (EEEEEE) background, black
        ' border and 1 pixel 3D border effect
        Dim c As PieChart = New PieChart(150, 120, &Heeeeee, &H000000, 1)

        ' Set donut center at (75, 65) and the outer radius to 50 pixels. Inner radius is computed
        ' according donutWidth
        c.setDonutSize(75, 60, 50, Int(50 * donutRadius / 100))

        ' Add a title to show the donut width
        c.addTitle("Inner Radius = " & donutRadius & " %", "Arial", 10).setBackground(&Hcccccc, 0)

        ' Draw the pie in 3D
        c.set3D(12)

        ' Set the pie data and the pie labels
        c.setData(data, labels)

        ' Disable the sector labels by setting the color to Transparent
        c.setLabelStyle("", 8, Chart.Transparent)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{label}: {value}kg ({percent}%)'")

    End Sub

End Class

