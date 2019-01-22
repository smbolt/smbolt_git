Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class polarline
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Polar Line Chart"
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
        Dim data0() As Double = {90, 25, 40, 55, 68, 44, 79, 85, 50}
        Dim angles0() As Double = {15, 60, 110, 180, 230, 260, 260, 310, 340}

        Dim data1() As Double = {80, 91, 66, 80, 92, 87}
        Dim angles1() As Double = {40, 65, 88, 110, 150, 200}

        ' Create a PolarChart object of size 460 x 500 pixels, with a grey (e0e0e0) background and 1
        ' pixel 3D border
        Dim c As PolarChart = New PolarChart(460, 500, &He0e0e0, &H000000, 1)

        ' Add a title to the chart at the top left corner using 15pt Arial Bold Italic font. Use a
        ' wood pattern as the title background.
        c.addTitle("Polar Line Chart Demo", "Arial Bold Italic", 15).setBackground(c.patternColor( _
            "wood.png"))

        ' Set center of plot area at (230, 280) with radius 180 pixels, and white (ffffff)
        ' background.
        c.setPlotArea(230, 280, 180, &Hffffff)

        ' Set the grid style to circular grid, with grids below the chart layers
        c.setGridStyle(False, False)

        ' Add a legend box at top-center of plot area (230, 35) using horizontal layout. Use 10pt
        ' Arial Bold font, with 1 pixel 3D border effect.
        Dim b As LegendBox = c.addLegend(230, 35, False, "Arial Bold", 9)
        b.setAlignment(Chart.TopCenter)
        b.setBackground(Chart.Transparent, Chart.Transparent, 1)

        ' Set angular axis as 0 - 360, with a spoke every 30 units
        c.angularAxis().setLinearScale(0, 360, 30)

        ' Add a blue (0xff) line layer to the chart using (data0, angle0)
        Dim layer0 As PolarLineLayer = c.addLineLayer(data0, &H0000ff, "Close Loop Line")
        layer0.setAngles(angles0)

        ' Set the line width to 2 pixels
        layer0.setLineWidth(2)

        ' Use 11 pixel triangle symbols for the data points
        layer0.setDataSymbol(Chart.TriangleSymbol, 11)

        ' Enable data label and set its format
        layer0.setDataLabelFormat("({value},{angle})")

        ' Set the data label text box with light blue (0x9999ff) backgruond color and 1 pixel 3D
        ' border effect
        layer0.setDataLabelStyle().setBackground(&H9999ff, Chart.Transparent, 1)

        ' Add a red (0xff0000) line layer to the chart using (data1, angle1)
        Dim layer1 As PolarLineLayer = c.addLineLayer(data1, &Hff0000, "Open Loop Line")
        layer1.setAngles(angles1)

        ' Set the line width to 2 pixels
        layer1.setLineWidth(2)

        ' Use 11 pixel diamond symbols for the data points
        layer1.setDataSymbol(Chart.DiamondSymbol, 11)

        ' Set the line to open loop
        layer1.setCloseLoop(False)

        ' Enable data label and set its format
        layer1.setDataLabelFormat("({value},{angle})")

        ' Set the data label text box with light red (0xff9999) backgruond color and 1 pixel 3D
        ' border effect
        layer1.setDataLabelStyle().setBackground(&Hff9999, Chart.Transparent, 1)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='[{dataSetName}] ({radius}, {angle})'")

    End Sub

End Class

