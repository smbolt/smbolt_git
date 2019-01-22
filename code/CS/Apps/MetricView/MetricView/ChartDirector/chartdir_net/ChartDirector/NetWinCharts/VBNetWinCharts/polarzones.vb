Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class polarzones
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Circular Zones"
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
        Dim data() As Double = {51, 15, 51, 40, 17, 87, 94, 21, 35, 88, 50, 60}

        ' The labels for the chart
        Dim labels() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", _
            "Oct", "Nov", "Dec"}

        ' Create a PolarChart object of size 400 x 420 pixels
        Dim c As PolarChart = New PolarChart(400, 420)

        ' Set background color to a 2 pixel pattern color, with a black border and 1 pixel 3D border
        ' effect
        c.setBackground(c.patternColor(New Integer() {&Hffffff, &He0e0e0}, 2), 0, 1)

        ' Add a title to the chart using 16pt Arial Bold Italic font. The title text is white
        ' (0xffffff) on 2 pixel pattern background
        c.addTitle("Chemical Concentration", "Arial Bold Italic", 16, &Hffffff).setBackground( _
            c.patternColor(New Integer() {&H000000, &H000080}, 2))

        ' Set center of plot area at (200, 240) with radius 145 pixels. Set background color to blue
        ' (9999ff)
        c.setPlotArea(200, 240, 145, &H9999ff)

        ' Color the region between radius = 40 to 80 as green (99ff99)
        c.radialAxis().addZone(40, 80, &H99ff99)

        ' Color the region with radius > 80 as red (ff9999)
        c.radialAxis().addZone(80, 999, &Hff9999)

        ' Set the grid style to circular grid
        c.setGridStyle(False)

        ' Set the radial axis label format
        c.radialAxis().setLabelFormat("{value} ppm")

        ' Use semi-transparent (40ffffff) label background so as not to block the data
        c.radialAxis().setLabelStyle().setBackground(&H40ffffff, &H40000000)

        ' Add a legend box at (200, 30) top center aligned, using 9pt Arial Bold font. with a black
        ' border.
        Dim legendBox As LegendBox = c.addLegend(200, 30, False, "Arial Bold", 9)
        legendBox.setAlignment(Chart.TopCenter)

        ' Add legend keys to represent the red/green/blue zones
        legendBox.addKey("Under-Absorp", &H9999ff)
        legendBox.addKey("Normal", &H99ff99)
        legendBox.addKey("Over-Absorp", &Hff9999)

        ' Add a blue (000080) spline line layer with line width set to 3 pixels and using yellow
        ' (ffff00) circles to represent the data points
        Dim layer As PolarSplineLineLayer = c.addSplineLineLayer(data, &H000080)
        layer.setLineWidth(3)
        layer.setDataSymbol(Chart.CircleShape, 11, &Hffff00)

        ' Set the labels to the angular axis as spokes.
        c.angularAxis().setLabels(labels)

        ' Output the chart
        viewer.Chart = c

        ' Include tool tip for the chart.
        viewer.ImageMap = layer.getHTMLImageMap("clickable", "", _
            "title='Concentration on {label}: {value} ppm'")

    End Sub

End Class

