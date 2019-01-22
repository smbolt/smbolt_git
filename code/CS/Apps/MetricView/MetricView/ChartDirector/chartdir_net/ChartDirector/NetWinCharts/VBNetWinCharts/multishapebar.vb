Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class multishapebar
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Multi-Shape Bar Chart"
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
        Dim data0() As Double = {100, 125, 245, 147}
        Dim data1() As Double = {85, 156, 179, 211}
        Dim data2() As Double = {97, 87, 56, 267}
        Dim labels() As String = {"1st Quarter", "2nd Quarter", "3rd Quarter", "4th Quarter"}

        ' Create a XYChart object of size 600 x 350 pixels
        Dim c As XYChart = New XYChart(600, 350)

        ' Add a title to the chart using 14pt Arial Bold Italic font
        c.addTitle("Annual Product Revenue", "Arial Bold Italic", 14)

        ' Set the plot area at (50, 60) and of size 500 x 240. Use two alternative background colors
        ' (f8f8f8 and ffffff)
        c.setPlotArea(50, 60, 500, 240, &Hf8f8f8, &Hffffff)

        ' Add a legend box at (55, 22) using horizontal layout, with transparent background
        c.addLegend(55, 22, False).setBackground(Chart.Transparent)

        ' Set the x axis labels
        c.xAxis().setLabels(labels)

        ' Draw the ticks between label positions (instead of at label positions)
        c.xAxis().setTickOffset(0.5)

        ' Add a multi-bar layer with 3 data sets and 9 pixels 3D depth
        Dim layer As BarLayer = c.addBarLayer2(Chart.Side, 9)
        layer.addDataSet(data0, -1, "Product A")
        layer.addDataSet(data1, -1, "Product B")
        layer.addDataSet(data2, -1, "Product C")

        ' Set data set 1 to use a bar shape of a 6-pointed star
        layer.setBarShape(Chart.StarShape(6), 0)

        ' Set data set 2 to use a bar shapre of a 6-sided polygon
        layer.setBarShape(Chart.PolygonShape(6), 1)

        ' Set data set 3 to use an X bar shape
        layer.setBarShape(Chart.Cross2Shape(), 2)

        ' Add a title to the y-axis
        c.yAxis().setTitle("Revenue (USD in millions)")

        ' Add a title to the x axis
        c.xAxis().setTitle("Year 2005")

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{dataSetName}: {xLabel} Revenue = {value} millions'")

    End Sub

End Class

