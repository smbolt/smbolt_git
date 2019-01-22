Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class multisymbolline
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Multi-Symbol Line Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' In this example, the data points are unevenly spaced on the x-axis
        Dim dataY() As Double = {4.7, 4.7, 6.6, 2.2, 4.7, 4.0, 4.0, 5.1, 4.5, 4.5, 6.8, 4.5, 4, _
            2.1, 3, 2.5, 2.5, 3.1}
        Dim dataX() As Date = {DateSerial(1999, 7, 1), DateSerial(2000, 1, 1), DateSerial(2000, 2, _
            1), DateSerial(2000, 4, 1), DateSerial(2000, 5, 8), DateSerial(2000, 7, 5), _
            DateSerial(2001, 3, 5), DateSerial(2001, 4, 7), DateSerial(2001, 5, 9), DateSerial( _
            2002, 2, 4), DateSerial(2002, 4, 4), DateSerial(2002, 5, 8), DateSerial(2002, 7, 7), _
            DateSerial(2002, 8, 30), DateSerial(2003, 1, 2), DateSerial(2003, 2, 16), DateSerial( _
            2003, 11, 6), DateSerial(2004, 1, 4)}

        ' Data points are assigned different symbols based on point type
        Dim pointType() As Double = {0, 1, 0, 1, 2, 1, 0, 0, 1, 1, 2, 2, 1, 0, 2, 1, 2, 0}

        ' Create a XYChart object of size 480 x 320 pixels. Use a vertical gradient color from pale
        ' blue (e8f0f8) to sky blue (aaccff) spanning half the chart height as background. Set
        ' border to blue (88aaee). Use rounded corners. Enable soft drop shadow.
        Dim c As XYChart = New XYChart(480, 320)
        c.setBackground(c.linearGradientColor(0, 0, 0, c.getHeight() / 2, &He8f0f8, &Haaccff), _
            &H88aaee)
        c.setRoundedFrame()
        c.setDropShadow()

        ' Add a title to the chart using 15 points Arial Italic font. Set top/bottom margins to 12
        ' pixels.
        Dim title As ChartDirector.TextBox = c.addTitle("Multi-Symbol Line Chart Demo", _
            "Arial Italic", 15)
        title.setMargin2(0, 0, 12, 12)

        ' Tentatively set the plotarea to 50 pixels from the left edge to allow for the y-axis, and
        ' to just under the title. Set the width to 65 pixels less than the chart width, and the
        ' height to reserve 90 pixels at the bottom for the x-axis and the legend box. Use pale blue
        ' (e8f0f8) background, transparent border, and grey (888888) dotted horizontal and vertical
        ' grid lines.
        c.setPlotArea(50, title.getHeight(), c.getWidth() - 65, c.getHeight() - title.getHeight() _
             - 90, &He8f0f8, -1, Chart.Transparent, c.dashLineColor(&H888888, Chart.DotLine), -1)

        ' Add a legend box where the bottom-center is anchored to the 12 pixels above the
        ' bottom-center of the chart. Use horizontal layout and 8 points Arial font.
        Dim legendBox As LegendBox = c.addLegend(c.getWidth() / 2, c.getHeight() - 12, False, _
            "Arial Bold", 8)
        legendBox.setAlignment(Chart.BottomCenter)

        ' Set the legend box background and border to pale blue (e8f0f8) and bluish grey (445566)
        legendBox.setBackground(&He8f0f8, &H445566)

        ' Use rounded corners of 5 pixel radius for the legend box
        legendBox.setRoundedCorners(5)

        ' Set the y axis label format to display a percentage sign
        c.yAxis().setLabelFormat("{value}%")

        ' Set y-axis title to use 10 points Arial Bold Italic font
        c.yAxis().setTitle("Axis Title Placeholder", "Arial Bold Italic", 10)

        ' Set axis labels to use Arial Bold font
        c.yAxis().setLabelStyle("Arial Bold")
        c.xAxis().setLabelStyle("Arial Bold")

        ' We add the different data symbols using scatter layers. The scatter layers are added
        ' before the line layer to make sure the data symbols stay on top of the line layer.

        ' We select the points with pointType = 0 (the non-selected points will be set to NoValue),
        ' and use yellow (ffff00) 15 pixels high 5 pointed star shape symbols for the points. (This
        ' example uses both x and y coordinates. For charts that have no x explicitly coordinates,
        ' use an empty array as dataX.)
        c.addScatterLayer(Chart.CTime(dataX), New ArrayMath(dataY).selectEQZ(pointType, _
            Chart.NoValue).result(), "Point Type 0", Chart.StarShape(5), 15, &Hffff00)

        ' Similar to above, we select the points with pointType - 1 = 0 and use green (ff00) 13
        ' pixels high six-sided polygon as symbols.
        c.addScatterLayer(Chart.CTime(dataX), New ArrayMath(dataY).selectEQZ(New ArrayMath( _
            pointType).sub(1).result(), Chart.NoValue).result(), "Point Type 1", _
            Chart.PolygonShape(6), 13, &H00ff00)

        ' Similar to above, we select the points with pointType - 2 = 0 and use red (ff0000) 13
        ' pixels high X shape as symbols.
        c.addScatterLayer(Chart.CTime(dataX), New ArrayMath(dataY).selectEQZ(New ArrayMath( _
            pointType).sub(2).result(), Chart.NoValue).result(), "Point Type 2", _
            Chart.Cross2Shape(), 13, &Hff0000)

        ' Finally, add a blue (0000ff) line layer with line width of 2 pixels
        Dim layer As LineLayer = c.addLineLayer(dataY, &H0000ff)
        layer.setXData(Chart.CTime(dataX))
        layer.setLineWidth(2)

        ' Adjust the plot area size, such that the bounding box (inclusive of axes) is 10 pixels
        ' from the left edge, just below the title, 25 pixels from the right edge, and 8 pixels
        ' above the legend box.
        c.packPlotArea(10, title.getHeight(), c.getWidth() - 25, c.layoutLegend().getTopY() - 8)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", "title='{x|mmm dd, yyyy}: {value}%'")

    End Sub

End Class

