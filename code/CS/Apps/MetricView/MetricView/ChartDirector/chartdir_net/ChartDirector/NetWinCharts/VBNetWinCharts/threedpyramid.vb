Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class threedpyramid
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "3D Pyramid Chart"
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
        Dim labels() As String = {"Corporate Tax", "Working Capital", "Re-investment", "Dividend"}

        ' The colors for the pyramid layers
        Dim colors() As Integer = {&H66aaee, &Heebb22, &Hcccccc, &Hcc88ff}

        ' Create a PyramidChart object of size 500 x 400 pixels
        Dim c As PyramidChart = New PyramidChart(500, 400)

        ' Set the pyramid center at (200, 180), and width x height to 150 x 300 pixels
        c.setPyramidSize(200, 180, 150, 300)

        ' Set the elevation to 15 degrees
        c.setViewAngle(15)

        ' Set the pyramid data and labels
        c.setData(data, labels)

        ' Set the layer colors to the given colors
        c.setColors2(Chart.DataColor, colors)

        ' Leave 1% gaps between layers
        c.setLayerGap(0.01)

        ' Add labels at the center of the pyramid layers using Arial Bold font. The labels will show
        ' the percentage of the layers.
        c.setCenterLabel("{percent}%", "Arial Bold")

        ' Add labels at the right side of the pyramid layers using Arial Bold font. The labels will
        ' have two lines showing the layer name and value.
        c.setRightLabel("{label}<*br*>US$ {value}M", "Arial Bold")

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{label}: US$ {value}M ({percent}%)'")

    End Sub

End Class

