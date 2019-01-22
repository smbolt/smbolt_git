Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class percentbar
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Percentage Bar Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the bar chart
        Dim data0() As Double = {100, 125, 245, 147, 67}
        Dim data1() As Double = {85, 156, 179, 211, 123}
        Dim data2() As Double = {97, 87, 56, 267, 157}

        ' The labels for the bar chart
        Dim labels() As String = {"Mon", "Tue", "Wed", "Thu", "Fri"}

        ' Create a XYChart object of size 500 x 320 pixels. Use a vertical gradient color from pale
        ' blue (e8f0f8) to sky blue (aaccff) spanning half the chart height as background. Set
        ' border to blue (88aaee). Use rounded corners. Enable soft drop shadow.
        Dim c As XYChart = New XYChart(500, 320)
        c.setBackground(c.linearGradientColor(0, 0, 0, c.getHeight() / 2, &He8f0f8, &Haaccff), _
            &H88aaee)
        c.setRoundedFrame()
        c.setDropShadow()

        ' Add a title to the chart using 15 points Arial Italic. Set top/bottom margins to 15
        ' pixels.
        Dim title As ChartDirector.TextBox = c.addTitle("Weekly Product Sales", "Arial Italic", 15)
        title.setMargin2(0, 0, 15, 15)

        ' Tentatively set the plotarea to 50 pixels from the left edge, and to just under the title.
        ' Set the width to 60% of the chart width, and the height to 50 pixels from the bottom edge.
        ' Use pale blue (e8f0f8) background, transparent border, and grey (aaaaaa) grid lines.
        c.setPlotArea(50, title.getHeight(), c.getWidth() * 6 / 10, c.getHeight() - _
            title.getHeight() - 50, &He8f0f8, -1, Chart.Transparent, &Haaaaaa)

        ' Add a legend box where the top-right corner is anchored at 10 pixels from the right edge,
        ' and just under the title. Use vertical layout and 8 points Arial font.
        Dim legendBox As LegendBox = c.addLegend(c.getWidth() - 10, title.getHeight(), True, _
            "Arial", 8)
        legendBox.setAlignment(Chart.TopRight)

        ' Set the legend box background and border to transparent
        legendBox.setBackground(Chart.Transparent, Chart.Transparent)

        ' Set the legend box icon size to 16 x 32 pixels to match with custom icon size
        legendBox.setKeySize(16, 32)

        ' Set axes to transparent
        c.xAxis().setColors(Chart.Transparent)
        c.yAxis().setColors(Chart.Transparent)

        ' Set the labels on the x axis
        c.xAxis().setLabels(labels)

        ' Add a percentage bar layer
        Dim layer As BarLayer = c.addBarLayer2(Chart.Percentage)

        ' Add the three data sets to the bar layer, using icons images with labels as data set names
        layer.addDataSet(data0, &H66aaee, _
            "<*block,valign=absmiddle*><*img=service.png*> Service<*/*>")
        layer.addDataSet(data1, &Heebb22, _
            "<*block,valign=absmiddle*><*img=software.png*> Software<*/*>")
        layer.addDataSet(data2, &Hcc88ff, _
            "<*block,valign=absmiddle*><*img=computer.png*> Hardware<*/*>")

        ' Use soft lighting effect with light direction from top
        layer.setBorderColor(Chart.Transparent, Chart.softLighting(Chart.Top))

        ' Enable data label at the middle of the the bar
        layer.setDataLabelStyle().setAlignment(Chart.Center)

        ' For a vertical stacked chart with positive data only, the last data set is always on top.
        ' However, in a vertical legend box, the last data set is at the bottom. This can be
        ' reversed by using the setLegend method.
        layer.setLegend(Chart.ReverseLegend)

        ' Adjust the plot area size, such that the bounding box (inclusive of axes) is 15 pixels
        ' from the left edge, just below the title, 10 pixels to the right of the legend box, and 15
        ' pixels from the bottom edge.
        c.packPlotArea(15, title.getHeight(), c.layoutLegend().getLeftX() - 10, c.getHeight() - 15)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{dataSetName} revenue on {xLabel}: US${value}K ({percent}%)'")

    End Sub

End Class

