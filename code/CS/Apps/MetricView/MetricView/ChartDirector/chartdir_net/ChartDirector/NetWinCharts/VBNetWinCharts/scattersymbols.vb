Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class scattersymbols
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Custom Scatter Symbols"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The XY points for the scatter chart
        Dim dataX() As Double = {200, 400, 300, 250, 500}
        Dim dataY() As Double = {40, 100, 50, 150, 250}

        ' The custom symbols for the points
        Dim symbols() As String = {"robot1.png", "robot2.png", "robot3.png", "robot4.png", _
            "robot5.png"}

        ' Create a XYChart object of size 450 x 400 pixels
        Dim c As XYChart = New XYChart(450, 400)

        ' Set the plotarea at (55, 40) and of size 350 x 300 pixels, with a light grey border
        ' (0xc0c0c0). Turn on both horizontal and vertical grid lines with light grey color
        ' (0xc0c0c0)
        c.setPlotArea(55, 40, 350, 300, -1, -1, &Hc0c0c0, &Hc0c0c0, -1)

        ' Add a title to the chart using 18pt Times Bold Itatic font.
        c.addTitle("Battle Robots", "Times New Roman Bold Italic", 18)

        ' Add a title to the y axis using 12pt Arial Bold Italic font
        c.yAxis().setTitle("Speed (km/s)", "Arial Bold Italic", 12)

        ' Add a title to the y axis using 12pt Arial Bold Italic font
        c.xAxis().setTitle("Range (km)", "Arial Bold Italic", 12)

        ' Set the axes line width to 3 pixels
        c.xAxis().setWidth(3)
        c.yAxis().setWidth(3)

        ' Add each point of the data as a separate scatter layer, so that they can have a different
        ' symbol
        For i As Integer = 0 To UBound(dataX)
            c.addScatterLayer(New Double() {dataX(i)}, New Double() {dataY(i)}).getDataSet(0 _
                ).setDataSymbol2(symbols(i))
        Next

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='Range = {x} km, Speed = {value} km/s'")

    End Sub

End Class

