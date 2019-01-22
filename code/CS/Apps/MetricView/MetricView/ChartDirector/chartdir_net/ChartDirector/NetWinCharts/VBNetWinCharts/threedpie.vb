Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class threedpie
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "3D Pie Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the pie chart
        Dim data() As Double = {25, 18, 15, 12, 8, 30, 35}

        ' The labels for the pie chart
        Dim labels() As String = {"Labor", "Licenses", "Taxes", "Legal", "Insurance", _
            "Facilities", "Production"}

        ' Create a PieChart object of size 360 x 300 pixels
        Dim c As PieChart = New PieChart(360, 300)

        ' Set the center of the pie at (180, 140) and the radius to 100 pixels
        c.setPieSize(180, 140, 100)

        ' Add a title to the pie chart
        c.addTitle("Project Cost Breakdown")

        ' Draw the pie in 3D
        c.set3D()

        ' Set the pie data and the pie labels
        c.setData(data, labels)

        ' Explode the 1st sector (index = 0)
        c.setExplode(0)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{label}: US${value}K ({percent}%)'")

    End Sub

End Class

