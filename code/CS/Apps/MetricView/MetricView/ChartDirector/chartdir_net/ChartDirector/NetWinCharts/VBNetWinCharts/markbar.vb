Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class markbar
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Bars with Marks"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the bars and the marks
        Dim barData() As Double = {100, 125, 245, 147, 67, 96, 160, 145, 97, 167, 220, 125}
        Dim markData() As Double = {85, 156, 220, 120, 80, 110, 140, 130, 111, 180, 175, 100}

        ' The labels for the bar chart
        Dim labels() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", _
            "Oct", "Nov", "Dec"}

        ' Create a XYChart object of size 480 x 360 pixels. Use a vertical gradient color from pale
        ' blue (e8f0f8) to sky blue (aaccff) spanning half the chart height as background. Set
        ' border to blue (88aaee). Use rounded corners. Enable soft drop shadow.
        Dim c As XYChart = New XYChart(480, 360)
        c.setBackground(c.linearGradientColor(0, 0, 0, c.getHeight() / 2, &He8f0f8, &Haaccff), _
            &H88aaee)
        c.setRoundedFrame()
        c.setDropShadow()

        ' Add a title to the chart using 15 points Arial Italic font. Set top/bottom margins to 12
        ' pixels.
        Dim title As ChartDirector.TextBox = c.addTitle("Bars with Marks Demonstration", _
            "Arial Italic", 15)
        title.setMargin2(0, 0, 12, 12)

        ' Tentatively set the plotarea to 50 pixels from the left edge to allow for the y-axis, and
        ' to just under the title. Set the width to 65 pixels less than the chart width, and the
        ' height to reserve 90 pixels at the bottom for the x-axis and the legend box. Use pale blue
        ' (e8f0f8) background, transparent border, and grey (888888) dotted horizontal grid lines.
        c.setPlotArea(50, title.getHeight(), c.getWidth() - 65, c.getHeight() - title.getHeight() _
             - 90, &He8f0f8, -1, Chart.Transparent, c.dashLineColor(&H888888, Chart.DotLine))

        ' Add a legend box where the bottom-center is anchored to the 15 pixels above the
        ' bottom-center of the chart. Use horizontal layout and 8 points Arial font.
        Dim legendBox As LegendBox = c.addLegend(c.getWidth() / 2, c.getHeight() - 15, False, _
            "Arial", 8)
        legendBox.setAlignment(Chart.BottomCenter)

        ' Set the legend box background and border to pale blue (e8f0f8) and bluish grey (445566)
        legendBox.setBackground(&He8f0f8, &H445566)

        ' Use rounded corners of 5 pixel radius for the legend box
        legendBox.setRoundedCorners(5)

        ' Use line style legend key
        legendBox.setLineStyleKey()

        ' Set axes to transparent
        c.xAxis().setColors(Chart.Transparent)
        c.yAxis().setColors(Chart.Transparent)

        ' Set the labels on the x axis
        c.xAxis().setLabels(labels)

        ' Add a box-whisker layer with just the middle mark visible for the marks. Use red (ff0000)
        ' color for the mark, with a line width of 2 pixels and 10% horizontal gap
        Dim markLayer As BoxWhiskerLayer = c.addBoxWhiskerLayer(Nothing, Nothing, Nothing, _
            Nothing, markData, -1, &Hff0000)
        markLayer.setLineWidth(2)
        markLayer.setDataGap(0.1)

        ' Add the legend key for the mark line
        legendBox.addKey("Target", &Hff0000, 2)

        ' Tool tip for the mark layer
        markLayer.setHTMLImageMap("", "", "title='Target at {xLabel}: {med}'")

        ' Add a blue (0066cc) bar layer using the given data.
        Dim barLayer As BarLayer = c.addBarLayer(barData, &H0066cc, "Actual")

        ' Use soft lighting effect for the bars with light direction from left.
        barLayer.setBorderColor(Chart.Transparent, Chart.softLighting(Chart.Left))

        ' Tool tip for the bar layer
        barLayer.setHTMLImageMap("", "", "title='{dataSetName} at {xLabel}: {value}'")

        ' Adjust the plot area size, such that the bounding box (inclusive of axes) is 10 pixels
        ' from the left edge, just below the title, 15 pixels from the right edge, and 10 pixels
        ' above the legend box.
        c.packPlotArea(10, title.getHeight(), c.getWidth() - 15, c.layoutLegend().getTopY() - 10)

        ' Output the chart
        viewer.Chart = c

        ' Include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable")

    End Sub

End Class

