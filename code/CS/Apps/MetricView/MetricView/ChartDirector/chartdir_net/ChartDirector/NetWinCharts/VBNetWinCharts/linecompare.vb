Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class linecompare
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Line with Target Zone"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the upper and lower bounding lines
        Dim upperY() As Double = {60, 60, 100, 100, 60, 60}
        Dim lowerY() As Double = {40, 40, 80, 80, 40, 40}
        Dim zoneX() As Double = {0, 2.5, 3.5, 5.5, 6.5, 10}

        ' The data for the spline curve
        Dim curveY() As Double = {50, 44, 54, 48, 58, 50, 90, 85, 104, 82, 96, 90, 74, 52, 35, 58, _
            46, 54, 48, 52, 50}
        Dim curveX() As Double = {0, 0.5, 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6, 6.5, 7, 7.5, _
            8, 8.5, 9, 9.5, 10}

        ' Create a XYChart object of size 600 x 300 pixels, with a light grey (cccccc) background,
        ' black border, and 1 pixel 3D border effect.
        Dim c As XYChart = New XYChart(600, 300, &Hcccccc, &H000000, 1)

        ' Set the plotarea at (55, 58) and of size 520 x 195 pixels, with white background. Turn on
        ' both horizontal and vertical grid lines with light grey color (cccccc)
        c.setPlotArea(55, 58, 520, 195, &Hffffff, -1, -1, &Hcccccc, &Hcccccc)

        ' Add a legend box at (55, 32) (top of the chart) with horizontal layout. Use 9pt Arial Bold
        ' font. Set the background and border color to Transparent.
        c.addLegend(55, 32, False, "Arial Bold", 9).setBackground(Chart.Transparent)

        ' Add a title box to the chart using 15pt Times Bold Italic font. The title is in CDML and
        ' includes embedded images for highlight. The text is white (ffffff) on a black background,
        ' with a 1 pixel 3D border.
        c.addTitle( _
            "<*block,valign=absmiddle*><*img=star.png*><*img=star.png*> Performance Enhancer " & _
            "<*img=star.png*><*img=star.png*><*/*>", "Times New Roman Bold Italic", 15, &Hffffff _
            ).setBackground(&H000000, -1, 1)

        ' Add a title to the y axis
        c.yAxis().setTitle("Temperature")

        ' Add a title to the x axis using CMDL
        c.xAxis().setTitle("<*block,valign=absmiddle*><*img=clock.png*>  Elapsed Time (hour)<*/*>")

        ' Set the axes width to 2 pixels
        c.xAxis().setWidth(2)
        c.yAxis().setWidth(2)

        ' Add a purple (800080) spline layer to the chart with a line width of 2 pixels
        Dim splineLayer As SplineLayer = c.addSplineLayer(curveY, &H800080, _
            "Molecular Temperature")
        splineLayer.setXData(curveX)
        splineLayer.setLineWidth(2)

        ' Add a line layer to the chart with two dark green (338033) data sets, and a line width of
        ' 2 pixels
        Dim lineLayer As LineLayer = c.addLineLayer2()
        lineLayer.addDataSet(upperY, &H338033, "Target Zone")
        lineLayer.addDataSet(lowerY, &H338033)
        lineLayer.setXData(zoneX)
        lineLayer.setLineWidth(2)

        ' Color the zone between the upper zone line and lower zone line as semi-transparent light
        ' green (8099ff99)
        c.addInterLineLayer(lineLayer.getLine(0), lineLayer.getLine(1), &H8099ff99, &H8099ff99)

        ' If the spline line gets above the upper zone line, color to area between the lines red
        ' (ff0000)
        c.addInterLineLayer(splineLayer.getLine(0), lineLayer.getLine(0), &Hff0000, _
            Chart.Transparent)

        ' If the spline line gets below the lower zone line, color to area between the lines blue
        ' (0000ff)
        c.addInterLineLayer(splineLayer.getLine(0), lineLayer.getLine(1), Chart.Transparent, _
            &H0000ff)

        ' Add a custom CDML text at the bottom right of the plot area as the logo
        c.addText(575, 250, _
            "<*block,valign=absmiddle*><*img=small_molecule.png*> <*block*><*font=Times New " & _
            "Roman Bold Italic,size=10,color=804040*>Molecular<*br*>Engineering<*/*>" _
            ).setAlignment(Chart.BottomRight)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='Temperature at hour {x}: {value} C'")

    End Sub

End Class

