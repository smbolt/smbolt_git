Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class simpleradar
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Simple Radar Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the chart
        Dim data() As Double = {90, 60, 65, 75, 40}

        ' The labels for the chart
        Dim labels() As String = {"Speed", "Reliability", "Comfort", "Safety", "Efficiency"}

        ' Create a PolarChart object of size 450 x 350 pixels
        Dim c As PolarChart = New PolarChart(450, 350)

        ' Set center of plot area at (225, 185) with radius 150 pixels
        c.setPlotArea(225, 185, 150)

        ' Add an area layer to the polar chart
        c.addAreaLayer(data, &H9999ff)

        ' Set the labels to the angular axis as spokes
        c.angularAxis().setLabels(labels)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", "title='{label}: score = {value}'")

    End Sub

End Class

