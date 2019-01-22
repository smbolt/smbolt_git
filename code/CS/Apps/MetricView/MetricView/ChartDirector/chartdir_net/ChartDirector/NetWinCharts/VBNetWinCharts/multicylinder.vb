Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class multicylinder
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Multi-Cylinder Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' Data for the chart
        Dim data0() As Double = {100, 125, 245, 147, 67, 96, 160, 145, 97, 167, 220, 125}
        Dim data1() As Double = {85, 156, 179, 211, 123, 225, 127, 99, 111, 260, 175, 156}
        Dim data2() As Double = {97, 87, 56, 267, 157, 157, 67, 156, 77, 87, 197, 87}
        Dim labels() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", _
            "Oct", "Nov", "Dec"}

        ' Create a XYChart object of size 560 x 280 pixels.
        Dim c As XYChart = New XYChart(560, 280)

        ' Add a title to the chart using 14pt Arial Bold Italic font
        c.addTitle("     Average Weekly Network Load", "Arial Bold Italic", 14)

        ' Set the plotarea at (50, 50) and of 500 x 200 pixels in size. Use alternating light grey
        ' (f8f8f8) / white (ffffff) background. Set border to transparent and use grey (CCCCCC)
        ' dotted lines as horizontal and vertical grid lines
        c.setPlotArea(50, 50, 500, 200, &Hffffff, &Hf8f8f8, Chart.Transparent, c.dashLineColor( _
            &Hcccccc, Chart.DotLine), c.dashLineColor(&Hcccccc, Chart.DotLine))

        ' Add a legend box at (50, 22) using horizontal layout. Use 10 pt Arial Bold Italic font,
        ' with transparent background
        c.addLegend(50, 22, False, "Arial Bold Italic", 10).setBackground(Chart.Transparent)

        ' Set the x axis labels
        c.xAxis().setLabels(labels)

        ' Draw the ticks between label positions (instead of at label positions)
        c.xAxis().setTickOffset(0.5)

        ' Add axis title
        c.yAxis().setTitle("Throughput (MBytes Per Hour)")

        ' Set axis line width to 2 pixels
        c.xAxis().setWidth(2)
        c.yAxis().setWidth(2)

        ' Add a multi-bar layer with 3 data sets
        Dim layer As BarLayer = c.addBarLayer2(Chart.Side)
        layer.addDataSet(data0, &Hff0000, "Server #1")
        layer.addDataSet(data1, &H00ff00, "Server #2")
        layer.addDataSet(data2, &H0000ff, "Server #3")

        ' Set bar shape to circular (cylinder)
        layer.setBarShape(Chart.CircleShape)

        ' Configure the bars within a group to touch each others (no gap)
        layer.setBarGap(0.2, Chart.TouchBar)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{dataSetName} on {xLabel}: {value} MBytes/hour'")

    End Sub

End Class

