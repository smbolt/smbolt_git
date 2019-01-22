Imports ChartDirector
Imports System.Collections

Public Class FrmTrackLabel

    Private Sub FrmTrackLabel_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Data for the chart as 3 random data series
        Dim r As RanSeries = New RanSeries(127)
        Dim data0() As Double = r.getSeries(100, 100, -15, 15)
        Dim data1() As Double = r.getSeries(100, 150, -15, 15)
        Dim data2() As Double = r.getSeries(100, 200, -15, 15)
        Dim timeStamps() As Date = r.getDateSeries(100, DateSerial(2011, 1, 1), 86400)

        ' Create a XYChart object of size 640 x 400 pixels
        Dim c As XYChart = New XYChart(640, 400)

        ' Add a title to the chart using 18pt Times New Roman Bold Italic font
        c.addTitle("    Product Line Global Revenue", "Times New Roman Bold Italic", 18)

        ' Set the plotarea at (50, 55) with width 70 pixels less than chart width, and height 90 pixels less
        ' than chart height. Use a vertical gradient from light blue (f0f6ff) to sky blue (a0c0ff) as
        ' background. Set border to transparent and grid lines to white (ffffff).
        c.setPlotArea(50, 55, c.getWidth() - 70, c.getHeight() - 90, c.linearGradientColor(0, 55, 0, _
            c.getHeight() - 35, &Hf0f6ff, &Ha0c0ff), -1, Chart.Transparent, &Hffffff, &Hffffff)

        ' Add a legend box at (50, 25) using horizontal layout. Use 10pt Arial Bold as font. Set the
        ' background and border color to Transparent.
        c.addLegend(50, 25, False, "Arial Bold", 10).setBackground(Chart.Transparent)

        ' Set axis label style to 8pt Arial Bold
        c.xAxis().setLabelStyle("Arial Bold", 8)
        c.yAxis().setLabelStyle("Arial Bold", 8)

        ' Set the axis stem to transparent
        c.xAxis().setColors(Chart.Transparent)
        c.yAxis().setColors(Chart.Transparent)

        ' Configure x-axis label format
        c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "{value|mm/yyyy} ", Chart.StartOfMonthFilter(), _
            "{value|mm}")

        ' Add axis title using 10pt Arial Bold Italic font
        c.yAxis().setTitle("USD millions", "Arial Bold Italic", 10)

        ' Add a line layer to the chart using a line width of 2 pixels.
        Dim layer As LineLayer = c.addLineLayer2()
        layer.setLineWidth(2)

        ' Add 3 data series to the line layer
        layer.setXData(timeStamps)
        layer.addDataSet(data0, &Hff3333, "Alpha")
        layer.addDataSet(data1, &H008800, "Beta")
        layer.addDataSet(data2, &H3333cc, "Gamma")

        ' Assign the chart to the WinChartViewer
        winChartViewer1.Chart = c

    End Sub

    '
    ' Draw track cursor when mouse is moving over plotarea
    '
    Private Sub winChartViewer1_MouseMovePlotArea(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.MouseEventArgs) Handles winChartViewer1.MouseMovePlotArea

        Dim viewer As WinChartViewer = sender
        trackLineLabel(viewer.Chart, viewer.PlotAreaMouseX)
        viewer.updateDisplay()

    	' Hide the track cursor when the mouse leaves the plot area
    	viewer.removeDynamicLayer("MouseLeavePlotArea")

    End Sub

    '
    ' Draw track line with data labels
    '
    Private Sub trackLineLabel(c As XYChart, mouseX As Integer)

        ' Clear the current dynamic layer and get the DrawArea object to draw on it.
        Dim d As DrawArea = c.initDynamicLayer()

        ' The plot area object
        Dim plotArea As PlotArea = c.getPlotArea()

        ' Get the data x-value that is nearest to the mouse, and find its pixel coordinate.
        Dim xValue As Double = c.getNearestXValue(mouseX)
        Dim xCoor As Integer = c.getXCoor(xValue)

        ' Draw a vertical track line at the x-position
        d.vline(plotArea.getTopY(), plotArea.getBottomY(), xCoor, d.dashLineColor(&H000000, &H0101))

        ' Draw a label on the x-axis to show the track line position.
        Dim xlabel As String = "<*font,bgColor=000000*> " & c.xAxis().getFormattedLabel(xValue, _
            "mmm dd, yyyy") & " <*/font*>"
        Dim t As TTFText = d.text(xlabel, "Arial Bold", 8)

        ' Restrict the x-pixel position of the label to make sure it stays inside the chart image.
        Dim xLabelPos As Integer = Math.Max(0, Math.Min(xCoor - t.getWidth() / 2, c.getWidth() - t.getWidth( _
            )))
        t.draw(xLabelPos, plotArea.getBottomY() + 6, &Hffffff)

        ' Iterate through all layers to draw the data labels
        For i As Integer = 0 To c.getLayerCount() - 1
            Dim layer As Layer = c.getLayerByZ(i)

            ' The data array index of the x-value
            Dim xIndex As Integer = layer.getXIndexOf(xValue)

            ' Iterate through all the data sets in the layer
            For j As Integer = 0 To layer.getDataSetCount() - 1
                Dim dataSet As ChartDirector.DataSet = layer.getDataSetByZ(j)

                ' Get the color and position of the data label
                Dim color As Integer = dataSet.getDataColor()
                Dim yCoor As Integer = c.getYCoor(dataSet.getPosition(xIndex), dataSet.getUseYAxis())

                ' Draw a track dot with a label next to it for visible data points in the plot area
                If (yCoor >= plotArea.getTopY()) And (yCoor <= plotArea.getBottomY()) And (color <> _
                    Chart.Transparent) Then

                    d.circle(xCoor, yCoor, 4, 4, color, color)

                    Dim label As String = "<*font,bgColor=" & Hex(color) & "*> " & c.formatValue( _
                        dataSet.getValue(xIndex), "{value|P4}") & " <*/font*>"
                    t = d.text(label, "Arial Bold", 8)

                    ' Draw the label on the right side of the dot if the mouse is on the left side the chart,
                    ' and vice versa. This ensures the label will not go outside the chart image.
                    If xCoor <= (plotArea.getLeftX() + plotArea.getRightX()) / 2 Then
                        t.draw(xCoor + 5, yCoor, &Hffffff, Chart.Left)
                    Else
                        t.draw(xCoor - 5, yCoor, &Hffffff, Chart.Right)
                    End If
                End If
            Next
        Next

    End Sub

End Class
