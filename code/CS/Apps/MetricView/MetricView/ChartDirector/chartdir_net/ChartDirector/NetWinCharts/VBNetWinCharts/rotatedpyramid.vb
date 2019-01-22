Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class rotatedpyramid
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Rotated Pyramid Chart"
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

        ' The semi-transparent colors for the pyramid layers
        Dim colors() As Integer = {&H400000cc, &H4066aaee, &H40ffbb00, &H40ee6622}

        ' Create a PyramidChart object of size 450 x 400 pixels
        Dim c As PyramidChart = New PyramidChart(450, 400)

        ' Set the pyramid center at (220, 180), and width x height to 150 x 300 pixels
        c.setPyramidSize(220, 180, 150, 300)

        ' Set the elevation to 15 degrees and rotation to 75 degrees
        c.setViewAngle(15, 75)

        ' Set the pyramid data and labels
        c.setData(data, labels)

        ' Set the layer colors to the given colors
        c.setColors2(Chart.DataColor, colors)

        ' Leave 1% gaps between layers
        c.setLayerGap(0.01)

        ' Add a legend box at (320, 60), with light grey (eeeeee) background and grey (888888)
        ' border. Set the top-left and bottom-right corners to rounded corners of 10 pixels radius.
        Dim legendBox As LegendBox = c.addLegend(320, 60)
        legendBox.setBackground(&Heeeeee, &H888888)
        legendBox.setRoundedCorners(10, 0, 10, 0)

        ' Add labels at the center of the pyramid layers using Arial Bold font. The labels will show
        ' the percentage of the layers.
        c.setCenterLabel("{percent}%", "Arial Bold")

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{label}: US$ {value}M ({percent}%)'")

    End Sub

End Class

