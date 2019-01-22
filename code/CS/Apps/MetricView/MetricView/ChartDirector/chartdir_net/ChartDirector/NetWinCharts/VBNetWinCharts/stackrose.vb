Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class stackrose
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Stacked Rose Chart"
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
        Dim data0() As Double = {5, 3, 10, 4, 3, 5, 2, 5}
        Dim data1() As Double = {12, 6, 17, 6, 7, 9, 4, 7}
        Dim data2() As Double = {17, 7, 22, 7, 18, 13, 5, 11}

        Dim angles() As Double = {0, 45, 90, 135, 180, 225, 270, 315}
        Dim labels() As String = {"North", "North<*br*>East", "East", "South<*br*>East", "South", _
            "South<*br*>West", "West", "North<*br*>West"}

        ' Create a PolarChart object of size 460 x 500 pixels, with a grey (e0e0e0) background and a
        ' 1 pixel 3D border
        Dim c As PolarChart = New PolarChart(460, 500, &He0e0e0, &H000000, 1)

        ' Add a title to the chart at the top left corner using 15pt Arial Bold Italic font. Use
        ' white text on deep blue background.
        c.addTitle("Wind Direction", "Arial Bold Italic", 15, &Hffffff).setBackground(&H000080)

        Dim legendBox As LegendBox = c.addLegend(230, 35, False, "Arial Bold", 9)
        legendBox.setAlignment(Chart.TopCenter)
        legendBox.setBackground(Chart.Transparent, Chart.Transparent, 1)

        legendBox.addKey("5 m/s or above", &Hff3333)
        legendBox.addKey("1 - 5 m/s", &H33ff33)
        legendBox.addKey("less than 1 m/s", &H3333ff)

        ' Set plot area center at (230, 280) with radius 180 pixels and white background
        c.setPlotArea(230, 280, 180, &Hffffff)

        ' Set the grid style to circular grid
        c.setGridStyle(False)

        ' Set angular axis as 0 - 360, with a spoke every 30 units
        c.angularAxis().setLinearScale2(0, 360, labels)

        For i As Integer = 0 To UBound(angles)
            c.angularAxis().addZone(angles(i) - 10, angles(i) + 10, 0, data0(i), &H3333ff, 0)
            c.angularAxis().addZone(angles(i) - 10, angles(i) + 10, data0(i), data1(i), &H33ff33,0)
            c.angularAxis().addZone(angles(i) - 10, angles(i) + 10, data1(i), data2(i), &Hff3333,0)
        Next

        ' Add an Transparent invisible layer to ensure the axis is auto-scaled using the data
        c.addLineLayer(data2, Chart.Transparent)

        ' Output the chart
        viewer.Chart = c

    End Sub

End Class

