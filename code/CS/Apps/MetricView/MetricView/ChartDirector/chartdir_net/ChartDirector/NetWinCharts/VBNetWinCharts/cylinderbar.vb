Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class cylinderbar
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Cylinder Bar Shape"
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
        Dim data() As Double = {85, 156, 179.5, 211, 123}

        ' The labels for the bar chart
        Dim labels() As String = {"Mon", "Tue", "Wed", "Thu", "Fri"}

        ' Create a XYChart object of size 400 x 240 pixels.
        Dim c As XYChart = New XYChart(400, 240)

        ' Add a title to the chart using 14pt Times Bold Italic font
        c.addTitle("Weekly Server Load", "Times New Roman Bold Italic", 14)

        ' Set the plotarea at (45, 40) and of 300 x 160 pixels in size. Use alternating light grey
        ' (f8f8f8) / white (ffffff) background.
        c.setPlotArea(45, 40, 300, 160, &Hf8f8f8, &Hffffff)

        ' Add a multi-color bar chart layer
        Dim layer As BarLayer = c.addBarLayer3(data)

        ' Set layer to 3D with 10 pixels 3D depth
        layer.set3D(10)

        ' Set bar shape to circular (cylinder)
        layer.setBarShape(Chart.CircleShape)

        ' Set the labels on the x axis.
        c.xAxis().setLabels(labels)

        ' Add a title to the y axis
        c.yAxis().setTitle("MBytes")

        ' Add a title to the x axis
        c.xAxis().setTitle("Work Week 25")

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", "title='{xLabel}: {value} MBytes'")

    End Sub

End Class

