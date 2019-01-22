Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class xzonecolor
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "X Zone Coloring"
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
        Dim data() As Double = {50, 55, 47, 34, 42, 49, 63, 62, 73, 59, 56, 50, 64, 60, 67, 67, _
            58, 59, 73, 77, 84, 82, 80, 84, 89}

        ' The error data representing the error band around the data points
        Dim errData() As Double = {5, 6, 5.1, 6.5, 6.6, 8, 5.4, 5.1, 4.6, 5.0, 5.2, 6.0, 4.9, 5.6, _
            4.8, 6.2, 7.4, 7.1, 6.5, 9.6, 12.1, 15.3, 18.5, 20.9, 24.1}

        ' The timestamps for the data
        Dim labels() As Date = {DateSerial(2001, 1, 1), DateSerial(2001, 2, 1), DateSerial(2001, _
            3, 1), DateSerial(2001, 4, 1), DateSerial(2001, 5, 1), DateSerial(2001, 6, 1), _
            DateSerial(2001, 7, 1), DateSerial(2001, 8, 1), DateSerial(2001, 9, 1), DateSerial( _
            2001, 10, 1), DateSerial(2001, 11, 1), DateSerial(2001, 12, 1), DateSerial(2002, 1, 1 _
            ), DateSerial(2002, 2, 1), DateSerial(2002, 3, 1), DateSerial(2002, 4, 1), DateSerial( _
            2002, 5, 1), DateSerial(2002, 6, 1), DateSerial(2002, 7, 1), DateSerial(2002, 8, 1), _
            DateSerial(2002, 9, 1), DateSerial(2002, 10, 1), DateSerial(2002, 11, 1), DateSerial( _
            2002, 12, 1), DateSerial(2003, 1, 1)}

        ' Create a XYChart object of size 550 x 220 pixels
        Dim c As XYChart = New XYChart(550, 220)

        ' Set the plot area at (50, 10) and of size 480 x 180 pixels. Enabled both vertical and
        ' horizontal grids by setting their colors to light grey (cccccc)
        c.setPlotArea(50, 10, 480, 180).setGridColor(&Hcccccc, &Hcccccc)

        ' Add a legend box (50, 10) (top of plot area) using horizontal layout. Use 8pt Arial font.
        ' Disable bounding box (set border to transparent).
        Dim legendBox As LegendBox = c.addLegend(50, 10, False, "", 8)
        legendBox.setBackground(Chart.Transparent)

        ' Add keys to the legend box to explain the color zones
        legendBox.addKey("Historical", &H9999ff)
        legendBox.addKey("Forecast", &Hff9966)

        ' Add a title to the y axis.
        c.yAxis().setTitle("Energy Consumption")

        ' Set the labels on the x axis
        c.xAxis().setLabels2(labels)

        ' Set multi-style axis label formatting. Use Arial Bold font for yearly labels and display
        ' them as "yyyy". Use default font for monthly labels and display them as "mmm". Replace
        ' some labels with minor ticks to ensure the labels are at least 3 units apart.
        c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "<*font=Arial Bold*>{value|yyyy}", _
            Chart.StartOfMonthFilter(), "{value|mmm}", 3)

        ' Add a line layer to the chart
        Dim layer As LineLayer = c.addLineLayer2()

        ' Create the color to draw the data line. The line is blue (0x333399) to the left of x = 18,
        ' and become a red (0xd04040) dash line to the right of x = 18.
        Dim lineColor As Integer = layer.xZoneColor(18, &H333399, c.dashLineColor(&Hd04040, _
            Chart.DashLine))

        ' Add the data line
        layer.addDataSet(data, lineColor, "Average")

        ' We are not showing the data set name in the legend box. The name is for showing in tool
        ' tips only.
        layer.setLegend(Chart.NoLegend)

        ' Create the color to draw the err zone. The color is semi-transparent blue (0x809999ff) to
        ' the left of x = 18, and become semi-transparent red (0x80ff9966) to the right of x = 18.
        Dim errColor As Integer = layer.xZoneColor(18, &H809999ff, &H80ff9966)

        ' Add the upper border of the err zone
        layer.addDataSet(New ArrayMath(data).add(errData).result(), errColor, "Upper bound")

        ' Add the lower border of the err zone
        layer.addDataSet(New ArrayMath(data).sub(errData).result(), errColor, "Lower bound")

        ' Set the default line width to 2 pixels
        layer.setLineWidth(2)

        ' In this example, we are not showing the data set name in the legend box
        layer.setLegend(Chart.NoLegend)

        ' Color the region between the err zone lines
        c.addInterLineLayer(layer.getLine(1), layer.getLine(2), errColor)

        ' Output the chart
        viewer.Chart = c

        ' Include tool tip for the chart.
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{dataSetName} on {xLabel|mmm yyyy}: {value} MJoule'")

    End Sub

End Class

