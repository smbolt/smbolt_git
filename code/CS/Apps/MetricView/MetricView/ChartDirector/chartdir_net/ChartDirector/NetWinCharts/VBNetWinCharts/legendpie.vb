Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class legendpie
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Pie Chart with Legend (1)"
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

        ' Create a PieChart object of size 450 x 270 pixels
        Dim c As PieChart = New PieChart(450, 270)

        ' Set the center of the pie at (150, 100) and the radius to 80 pixels
        c.setPieSize(150, 135, 100)

        ' add a legend box where the top left corner is at (330, 50)
        c.addLegend(330, 60)

        ' modify the sector label format to show percentages only
        c.setLabelFormat("{percent}%")

        ' Set the pie data and the pie labels
        c.setData(data, labels)

        ' Use rounded edge shading, with a 1 pixel white (FFFFFF) border
        c.setSectorStyle(Chart.RoundedEdgeShading, &Hffffff, 1)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{label}: US${value}K ({percent}%)'")

    End Sub

End Class

