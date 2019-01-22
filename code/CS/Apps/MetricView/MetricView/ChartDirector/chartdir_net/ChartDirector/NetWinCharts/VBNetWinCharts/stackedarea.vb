Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class stackedarea
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Stacked Area Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the area chart
        Dim data0() As Double = {42, 49, 33, 38, 51, 46, 29, 41, 44, 57, 59, 52, 37, 34, 51, 56, _
            56, 60, 70, 76, 63, 67, 75, 64, 51}
        Dim data1() As Double = {50, 45, 47, 34, 42, 49, 63, 62, 73, 59, 56, 50, 64, 60, 67, 67, _
            58, 59, 73, 77, 84, 82, 80, 84, 89}
        Dim data2() As Double = {61, 79, 85, 66, 53, 39, 24, 21, 37, 56, 37, 22, 21, 33, 13, 17, _
            4, 23, 16, 25, 9, 10, 5, 7, 16}
        Dim labels() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", _
            "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24"}

        ' Create a XYChart object of size 300 x 210 pixels. Set the background to pale yellow
        ' (0xffffc0) with a black border (0x0)
        Dim c As XYChart = New XYChart(300, 210, &Hffffc0, &H000000)

        ' Set the plotarea at (50, 30) and of size 240 x 140 pixels. Use white (0xffffff)
        ' background.
        c.setPlotArea(50, 30, 240, 140).setBackground(&Hffffff)

        ' Add a legend box at (50, 185) (below of plot area) using horizontal layout. Use 8pt Arial
        ' font with Transparent background.
        c.addLegend(50, 185, False, "", 8).setBackground(Chart.Transparent)

        ' Add a title box to the chart using 8pt Arial Bold font, with yellow (0xffff40) background
        ' and a black border (0x0)
        c.addTitle("Sales Volume", "Arial Bold", 8).setBackground(&Hffff40, 0)

        ' Set the y axis label format to US$nnnn
        c.yAxis().setLabelFormat("US${value}")

        ' Set the labels on the x axis.
        c.xAxis().setLabels(labels)

        ' Display 1 out of 2 labels on the x-axis. Show minor ticks for remaining labels.
        c.xAxis().setLabelStep(2, 1)

        ' Add an stack area layer with three data sets
        Dim layer As AreaLayer = c.addAreaLayer2(Chart.Stack)
        layer.addDataSet(data0, &H4040ff, "Store #1")
        layer.addDataSet(data1, &Hff4040, "Store #2")
        layer.addDataSet(data2, &H40ff40, "Store #3")

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{dataSetName} sales at hour {xLabel}: US${value}K'")

    End Sub

End Class

