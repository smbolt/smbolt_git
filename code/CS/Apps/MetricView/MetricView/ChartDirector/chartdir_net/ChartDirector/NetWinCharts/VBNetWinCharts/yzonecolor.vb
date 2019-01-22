Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class yzonecolor
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Y Zone Coloring"
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
        Dim data() As Double = {30, 28, 40, 55, 75, 68, 54, 60, 50, 62, 75, 65, 75, 89, 60, 55, _
            53, 35, 50, 66, 56, 48, 52, 65, 62}

        ' The labels for the chart
        Dim labels() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", _
            "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24"}

        ' Create a XYChart object of size 500 x 300 pixels, with a pale yellow (0xffffc0)
        ' background, a black border, and 1 pixel 3D border effect
        Dim c As XYChart = New XYChart(500, 300, &Hffffc0, &H000000, 1)

        ' Set the plotarea at (55, 50) and of size 420 x 205 pixels, with white background. Turn on
        ' both horizontal and vertical grid lines with light grey color (0xc0c0c0)
        c.setPlotArea(55, 50, 420, 205, &Hffffff).setGridColor(&Hc0c0c0, &Hc0c0c0)

        ' Add a legend box at (55, 25) (top of the chart) with horizontal layout. Use 8pt Arial
        ' font. Set the background and border color to Transparent.
        Dim legendBox As LegendBox = c.addLegend(55, 25, False, "", 8)
        legendBox.setBackground(Chart.Transparent)

        ' Add keys to the legend box to explain the color zones
        legendBox.addKey("Normal Zone", &H8033ff33)
        legendBox.addKey("Alert Zone", &H80ff3333)

        ' Add a title box to the chart using 13pt Arial Bold Italic font. The title is in CDML and
        ' includes embedded images for highlight. The text is white (0xffffff) on a black
        ' background, with a 1 pixel 3D border.
        c.addTitle( _
            "<*block,valign=absmiddle*><*img=star.png*><*img=star.png*> Y Zone Color Demo " & _
            "<*img=star.png*><*img=star.png*><*/*>", "Arial Bold Italic", 13, &Hffffff _
            ).setBackground(&H000000, -1, 1)

        ' Add a title to the y axis
        c.yAxis().setTitle("Energy Concentration (KJ per liter)")

        ' Set the labels on the x axis.
        c.xAxis().setLabels(labels)

        ' Display 1 out of 3 labels on the x-axis.
        c.xAxis().setLabelStep(3)

        ' Add a title to the x axis using CDML
        c.xAxis().setTitle("<*block,valign=absmiddle*><*img=clock.png*>  Elapsed Time (hour)<*/*>")

        ' Set the axes width to 2 pixels
        c.xAxis().setWidth(2)
        c.yAxis().setWidth(2)

        ' Add an area layer to the chart. The area is using a y zone color, where the color is
        ' semi-transparent green below 60, and semi-transparent red above 60.
        c.addAreaLayer(data, c.yZoneColor(60, &H8033ff33, &H80ff3333))

        ' Add a custom CDML text at the bottom right of the plot area as the logo
        c.addText(475, 255, _
            "<*block,valign=absmiddle*><*img=small_molecule.png*> <*block*><*font=Times New " & _
            "Roman Bold Italic,size=10,color=804040*>Molecular<*br*>Engineering<*/*>" _
            ).setAlignment(Chart.BottomRight)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='Hour {xLabel}: {value} KJ/liter'")

    End Sub

End Class

