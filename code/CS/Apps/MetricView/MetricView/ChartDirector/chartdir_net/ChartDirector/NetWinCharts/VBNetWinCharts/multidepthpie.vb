Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class multidepthpie
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Multi-Depth Pie Chart"
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
        Dim data() As Double = {72, 18, 15, 12}

        ' The labels for the pie chart
        Dim labels() As String = {"Labor", "Machinery", "Facilities", "Computers"}

        ' The depths for the sectors
        Dim depths() As Double = {30, 20, 10, 10}

        ' Create a PieChart object of size 360 x 300 pixels, with a light blue (DDDDFF) background
        ' and a 1 pixel 3D border
        Dim c As PieChart = New PieChart(360, 300, &Hddddff, -1, 1)

        ' Set the center of the pie at (180, 175) and the radius to 100 pixels
        c.setPieSize(180, 175, 100)

        ' Add a title box using 15pt Times Bold Italic font and blue (AAAAFF) as background color
        c.addTitle("Project Cost Breakdown", "Times New Roman Bold Italic", 15).setBackground( _
            &Haaaaff)

        ' Set the pie data and the pie labels
        c.setData(data, labels)

        ' Draw the pie in 3D with variable 3D depths
        c.set3D2(depths)

        ' Set the start angle to 225 degrees may improve layout when the depths of the sector are
        ' sorted in descending order, because it ensures the tallest sector is at the back.
        c.setStartAngle(225)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{label}: US${value}K ({percent}%)'")

    End Sub

End Class

