Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class multiline2
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Multi-Line Chart (2)"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' In this example, we simply use random data for the 3 data series.
        Dim r As RanSeries = New RanSeries(129)
        Dim data0() As Double = r.getSeries(100, 100, -15, 15)
        Dim data1() As Double = r.getSeries(100, 160, -15, 15)
        Dim data2() As Double = r.getSeries(100, 220, -15, 15)
        Dim timeStamps() As Date = r.getDateSeries(100, DateSerial(2014, 1, 1), 86400)

        ' Create a XYChart object of size 600 x 400 pixels
        Dim c As XYChart = New XYChart(600, 400)

        ' Set default text color to dark grey (0x333333)
        c.setColor(Chart.TextColor, &H333333)

        ' Add a title box using grey (0x555555) 20pt Arial font
        c.addTitle("    Multi-Line Chart Demonstration", "Arial", 20, &H555555)

        ' Set the plotarea at (70, 70) and of size 500 x 300 pixels, with transparent background and
        ' border and light grey (0xcccccc) horizontal grid lines
        c.setPlotArea(70, 70, 500, 300, Chart.Transparent, -1, Chart.Transparent, &Hcccccc)

        ' Add a legend box with horizontal layout above the plot area at (70, 35). Use 12pt Arial
        ' font, transparent background and border, and line style legend icon.
        Dim b As LegendBox = c.addLegend(70, 35, False, "Arial", 12)
        b.setBackground(Chart.Transparent, Chart.Transparent)
        b.setLineStyleKey()

        ' Set axis label font to 12pt Arial
        c.xAxis().setLabelStyle("Arial", 12)
        c.yAxis().setLabelStyle("Arial", 12)

        ' Set the x and y axis stems to transparent, and the x-axis tick color to grey (0xaaaaaa)
        c.xAxis().setColors(Chart.Transparent, Chart.TextColor, Chart.TextColor, &Haaaaaa)
        c.yAxis().setColors(Chart.Transparent)

        ' Set the major/minor tick lengths for the x-axis to 10 and 0.
        c.xAxis().setTickLength(10, 0)

        ' For the automatic axis labels, set the minimum spacing to 80/40 pixels for the x/y axis.
        c.xAxis().setTickDensity(80)
        c.yAxis().setTickDensity(40)

        ' Add a title to the y axis using dark grey (0x555555) 14pt Arial font
        c.yAxis().setTitle("Y-Axis Title Placeholder", "Arial", 14, &H555555)

        ' Add a line layer to the chart with 3-pixel line width
        Dim layer As LineLayer = c.addLineLayer2()
        layer.setLineWidth(3)

        ' Add 3 data series to the line layer
        layer.addDataSet(data0, &H5588cc, "Alpha")
        layer.addDataSet(data1, &Hee9944, "Beta")
        layer.addDataSet(data2, &H99bb55, "Gamma")

        ' The x-coordinates for the line layer
        layer.setXData(timeStamps)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='[{x|mm/dd/yyyy}] {dataSetName}: {value}'")

    End Sub

End Class

