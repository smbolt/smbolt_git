Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class multiradar
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Multi Radar Chart"
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
        Dim data0() As Double = {90, 60, 85, 75, 55}
        Dim data1() As Double = {60, 80, 70, 80, 85}

        ' The labels for the chart
        Dim labels() As String = {"Speed", "Reliability", "Comfort", "Safety", "Efficiency"}

        ' Create a PolarChart object of size 480 x 380 pixels. Set background color to gold, with 1
        ' pixel 3D border effect
        Dim c As PolarChart = New PolarChart(480, 380, Chart.goldColor(), &H000000, 1)

        ' Add a title to the chart using 15pt Times Bold Italic font. The title text is white
        ' (ffffff) on a deep blue (000080) background
        c.addTitle("Space Travel Vehicles Compared", "Times New Roman Bold Italic", 15, &Hffffff _
            ).setBackground(&H000080)

        ' Set plot area center at (240, 210), with 150 pixels radius, and a white (ffffff)
        ' background.
        c.setPlotArea(240, 210, 150, &Hffffff)

        ' Add a legend box at top right corner (470, 35) using 10pt Arial Bold font. Set the
        ' background to silver, with 1 pixel 3D border effect.
        Dim b As LegendBox = c.addLegend(470, 35, True, "Arial Bold", 10)
        b.setAlignment(Chart.TopRight)
        b.setBackground(Chart.silverColor(), Chart.Transparent, 1)

        ' Add an area layer to the chart using semi-transparent blue (0x806666cc). Add a blue
        ' (0x6666cc) line layer using the same data with 3 pixel line width to highlight the border
        ' of the area.
        c.addAreaLayer(data0, &H806666cc, "Model Saturn")
        c.addLineLayer(data0, &H6666cc).setLineWidth(3)

        ' Add an area layer to the chart using semi-transparent red (0x80cc6666). Add a red
        ' (0xcc6666) line layer using the same data with 3 pixel line width to highlight the border
        ' of the area.
        c.addAreaLayer(data1, &H80cc6666, "Model Jupiter")
        c.addLineLayer(data1, &Hcc6666).setLineWidth(3)

        ' Set the labels to the angular axis as spokes.
        c.angularAxis().setLabels(labels)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='[{dataSetName}] {label}: score = {value}'")

    End Sub

End Class

