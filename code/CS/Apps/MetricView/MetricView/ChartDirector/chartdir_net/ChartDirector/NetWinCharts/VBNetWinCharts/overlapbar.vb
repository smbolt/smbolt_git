Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class overlapbar
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Overlapping Bar Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the bar chart
        Dim data0() As Double = {100, 125, 156, 147, 87, 124, 178, 109, 140, 106, 192, 122}
        Dim data1() As Double = {122, 156, 179, 211, 198, 177, 160, 220, 190, 188, 220, 270}
        Dim data2() As Double = {167, 190, 213, 267, 250, 320, 212, 199, 245, 267, 240, 310}
        Dim labels() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", _
            "Oct", "Nov", "Dec"}

        ' Create a XYChart object of size 580 x 280 pixels
        Dim c As XYChart = New XYChart(580, 280)

        ' Add a title to the chart using 14pt Arial Bold Italic font
        c.addTitle("Product Revenue For Last 3 Years", "Arial Bold Italic", 14)

        ' Set the plot area at (50, 50) and of size 500 x 200. Use two alternative background colors
        ' (f8f8f8 and ffffff)
        c.setPlotArea(50, 50, 500, 200, &Hf8f8f8, &Hffffff)

        ' Add a legend box at (50, 25) using horizontal layout. Use 8pt Arial as font, with
        ' transparent background.
        c.addLegend(50, 25, False, "Arial", 8).setBackground(Chart.Transparent)

        ' Set the x axis labels
        c.xAxis().setLabels(labels)

        ' Draw the ticks between label positions (instead of at label positions)
        c.xAxis().setTickOffset(0.5)

        ' Add a multi-bar layer with 3 data sets
        Dim layer As BarLayer = c.addBarLayer2(Chart.Side)
        layer.addDataSet(data0, &Hff8080, "Year 2003")
        layer.addDataSet(data1, &H80ff80, "Year 2004")
        layer.addDataSet(data2, &H8080ff, "Year 2005")

        ' Set 50% overlap between bars
        layer.setOverlapRatio(0.5)

        ' Add a title to the y-axis
        c.yAxis().setTitle("Revenue (USD in millions)")

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{xLabel} Revenue on {dataSetName}: {value} millions'")

    End Sub

End Class

