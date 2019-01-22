Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class splineline
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Spline Line Chart"
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
        Dim data0() As Double = {32, 39, 23, 28, 41, 38, 26, 35, 29}
        Dim data1() As Double = {50, 55, 47, 34, 47, 53, 38, 40, 51}

        ' The labels for the chart
        Dim labels() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8"}

        ' Create a XYChart object of size 600 x 300 pixels, with a pale red (ffdddd) background,
        ' black border, 1 pixel 3D border effect and rounded corners.
        Dim c As XYChart = New XYChart(600, 300, &Hffdddd, &H000000, 1)
        c.setRoundedFrame()

        ' Set the plotarea at (55, 58) and of size 520 x 195 pixels, with white (ffffff) background.
        ' Set horizontal and vertical grid lines to grey (cccccc).
        c.setPlotArea(55, 58, 520, 195, &Hffffff, -1, -1, &Hcccccc, &Hcccccc)

        ' Add a legend box at (55, 32) (top of the chart) with horizontal layout. Use 9pt Arial Bold
        ' font. Set the background and border color to Transparent.
        c.addLegend(55, 32, False, "Arial Bold", 9).setBackground(Chart.Transparent)

        ' Add a title box to the chart using 15pt Times Bold Italic font. The title is in CDML and
        ' includes embedded images for highlight. The text is white (ffffff) on a dark red (880000)
        ' background, with soft lighting effect from the right side.
        c.addTitle( _
            "<*block,valign=absmiddle*><*img=star.png*><*img=star.png*> Performance Enhancer " & _
            "<*img=star.png*><*img=star.png*><*/*>", "Times New Roman Bold Italic", 15, &Hffffff _
            ).setBackground(&H880000, -1, Chart.softLighting(Chart.Right))

        ' Add a title to the y axis
        c.yAxis().setTitle("Energy Concentration (KJ per liter)")

        ' Set the labels on the x axis
        c.xAxis().setLabels(labels)

        ' Add a title to the x axis using CMDL
        c.xAxis().setTitle("<*block,valign=absmiddle*><*img=clock.png*>  Elapsed Time (hour)<*/*>")

        ' Set the axes width to 2 pixels
        c.xAxis().setWidth(2)
        c.yAxis().setWidth(2)

        ' Add a spline layer to the chart
        Dim layer As SplineLayer = c.addSplineLayer()

        ' Set the default line width to 2 pixels
        layer.setLineWidth(2)

        ' Add a data set to the spline layer, using blue (0000c0) as the line color, with yellow
        ' (ffff00) circle symbols.
        layer.addDataSet(data1, &H0000c0, "Target Group").setDataSymbol(Chart.CircleSymbol, 9, _
            &Hffff00)

        ' Add a data set to the spline layer, using brown (982810) as the line color, with pink
        ' (f040f0) diamond symbols.
        layer.addDataSet(data0, &H982810, "Control Group").setDataSymbol(Chart.DiamondSymbol, 11, _
            &Hf040f0)

        ' Add a custom CDML text at the bottom right of the plot area as the logo
        c.addText(575, 250, _
            "<*block,valign=absmiddle*><*img=small_molecule.png*> <*block*><*font=Times New " & _
            "Roman Bold Italic,size=10,color=804040*>Molecular<*br*>Engineering<*/*>" _
            ).setAlignment(Chart.BottomRight)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{dataSetName} at t = {xLabel} hour: {value} KJ/liter'")

    End Sub

End Class

