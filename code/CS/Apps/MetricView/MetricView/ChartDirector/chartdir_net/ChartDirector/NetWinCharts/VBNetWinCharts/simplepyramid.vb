Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class simplepyramid
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Simple Pyramid Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the pyramid chart
        Dim data() As Double = {156, 123, 211, 179}

        ' The labels for the pyramid chart
        Dim labels() As String = {"Funds", "Bonds", "Stocks", "Cash"}

        ' Create a PyramidChart object of size 360 x 360 pixels
        Dim c As PyramidChart = New PyramidChart(360, 360)

        ' Set the pyramid center at (180, 180), and width x height to 150 x 180 pixels
        c.setPyramidSize(180, 180, 150, 300)

        ' Set the pyramid data and labels
        c.setData(data, labels)

        ' Add labels at the center of the pyramid layers using Arial Bold font. The labels will have
        ' two lines showing the layer name and percentage.
        c.setCenterLabel("{label}<*br*>{percent}%", "Arial Bold")

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{label}: US$ {value}M ({percent}%)'")

    End Sub

End Class

