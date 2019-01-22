Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class patternarea
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Pattern Area Chart"
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
        Dim data() As Double = {3.0, 2.8, 4.0, 5.5, 7.5, 6.8, 5.4, 6.0, 5.0, 6.2, 7.5, 6.5, 7.5, _
            8.1, 6.0, 5.5, 5.3, 3.5, 5.0, 6.6, 5.6, 4.8, 5.2, 6.5, 6.2}

        ' The labels for the area chart
        Dim labels() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", _
            "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24"}

        ' Create a XYChart object of size 300 x 180 pixels. Set the background to pale yellow
        ' (0xffffa0) with a black border (0x0)
        Dim c As XYChart = New XYChart(300, 180, &Hffffa0, &H000000)

        ' Set the plotarea at (45, 35) and of size 240 x 120 pixels. Set the background to white
        ' (0xffffff). Set both horizontal and vertical grid lines to black (&H0&) dotted lines
        ' (pattern code 0x0103)
        c.setPlotArea(45, 35, 240, 120, &Hffffff, -1, -1, c.dashLineColor(&H000000, &H000103), _
            c.dashLineColor(&H000000, &H000103))

        ' Add a title to the chart using 10pt Arial Bold font. Use a 1 x 2 bitmap pattern as the
        ' background. Set the border to black (0x0).
        c.addTitle("Snow Percipitation (Dec 12)", "Arial Bold", 10).setBackground(c.patternColor( _
            New Integer() {&Hb0b0f0, &He0e0ff}, 2), &H000000)

        ' Add a title to the y axis
        c.yAxis().setTitle("mm per hour")

        ' Set the labels on the x axis.
        c.xAxis().setLabels(labels)

        ' Display 1 out of 3 labels on the x-axis.
        c.xAxis().setLabelStep(3)

        ' Add an area layer to the chart
        Dim layer As AreaLayer = c.addAreaLayer()

        ' Load a snow pattern from an external file "snow.png".
        Dim snowPattern As Integer = c.patternColor2("snow.png")

        ' Add a data set to the area layer using the snow pattern as the fill color. Use deep blue
        ' (0x0000ff) as the area border line color (&H0000ff&)
        layer.addDataSet(data).setDataColor(snowPattern, &H0000ff)

        ' Set the line width to 2 pixels to highlight the line
        layer.setLineWidth(2)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{xLabel}:00 - {value} mm/hour'")

    End Sub

End Class

