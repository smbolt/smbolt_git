Imports ChartDirector
Imports System.Collections

Public Class FrmTrackAxis

    Private Sub FrmTrackAxis_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Data for the chart as 2 random data series
        Dim r As RanSeries = New RanSeries(127)
        Dim data0() As Double = r.getSeries(180, 10, -1.5, 1.5)
        Dim data1() As Double = r.getSeries(180, 150, -15, 15)
        Dim timeStamps() As Date = r.getDateSeries(180, DateSerial(2011, 1, 1), 86400)

        ' Create a XYChart object of size 670 x 400 pixels
        Dim c As XYChart = New XYChart(670, 400)

        ' Add a title to the chart using 18pt Times New Roman Bold Italic font
        c.addTitle("Plasma Stabilizer Energy Usage", "Times New Roman Bold Italic", 18)

        ' Set the plotarea at (50, 55) with width 100 pixels less than chart width, and height 90 pixels less
        ' than chart height. Use a vertical gradient from light blue (f0f6ff) to sky blue (a0c0ff) as
        ' background. Set border to transparent and grid lines to white (ffffff).
        c.setPlotArea(50, 55, c.getWidth() - 100, c.getHeight() - 90, c.linearGradientColor(0, 55, 0, _
            c.getHeight() - 35, &Hf0f6ff, &Ha0c0ff), -1, Chart.Transparent, &Hffffff, &Hffffff)

        ' Add a legend box at (50, 25) using horizontal layout. Use 10pt Arial Bold as font. Set the
        ' background and border color to Transparent.
        c.addLegend(50, 25, False, "Arial Bold", 10).setBackground(Chart.Transparent)

        ' Set axis label style to 8pt Arial Bold
        c.xAxis().setLabelStyle("Arial Bold", 8)
        c.yAxis().setLabelStyle("Arial Bold", 8)
        c.yAxis2().setLabelStyle("Arial Bold", 8)

        ' Set the axis stem to transparent
        c.xAxis().setColors(Chart.Transparent)
        c.yAxis().setColors(Chart.Transparent)
        c.yAxis2().setColors(Chart.Transparent)

        ' Configure x-axis label format
        c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "{value|mm/yyyy} ", Chart.StartOfMonthFilter(), _
            "{value|mm}")

        ' Add axis title using 10pt Arial Bold Italic font
        c.yAxis().setTitle("Power Usage (Watt)", "Arial Bold Italic", 10)
        c.yAxis2().setTitle("Effective Load (kg)", "Arial Bold Italic", 10)

        ' Add a line layer to the chart using a line width of 2 pixels.
        Dim layer As LineLayer = c.addLineLayer2()
        layer.setLineWidth(2)

        ' Add 2 data series to the line layer
        layer.setXData(timeStamps)
        layer.addDataSet(data0, &Hcc0000, "Power Usage")
        layer.addDataSet(data1, &H008800, "Effective Load").setUseYAxis2()

        ' Assign the chart to the WinChartViewer
        winChartViewer1.Chart = c

    End Sub

    '
    ' Draw track cursor when mouse is moving over plotarea
    '
    Private Sub winChartViewer1_MouseMovePlotArea(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.MouseEventArgs) Handles winChartViewer1.MouseMovePlotArea

        Dim viewer As WinChartViewer = sender
        trackLineAxis(viewer.Chart, viewer.PlotAreaMouseX)
        viewer.updateDisplay()

    	' Hide the track cursor when the mouse leaves the plot area
    	viewer.removeDynamicLayer("MouseLeavePlotArea")

    End Sub

    '
    ' Draw track line with axis labels
    '
    Private Sub trackLineAxis(c As XYChart, mouseX As Integer)

        ' Clear the current dynamic layer and get the DrawArea object to draw on it.
        Dim d As DrawArea = c.initDynamicLayer()

        ' The plot area object
        Dim plotArea As PlotArea = c.getPlotArea()

        ' Get the data x-value that is nearest to the mouse, and find its pixel coordinate.
        Dim xValue As Double = c.getNearestXValue(mouseX)
        Dim xCoor As Integer = c.getXCoor(xValue)

        ' The vertical track line is drawn up to the highest data point (the point with smallest
        ' y-coordinate). We need to iterate all datasets in all layers to determine where it is.
        Dim minY As Integer = plotArea.getBottomY()

        ' Iterate through all layers to find the highest data point
        For i As Integer = 0 To c.getLayerCount() - 1
            Dim layer As Layer = c.getLayerByZ(i)

            ' The data array index of the x-value
            Dim xIndex As Integer = layer.getXIndexOf(xValue)

            ' Iterate through all the data sets in the layer
            For j As Integer = 0 To layer.getDataSetCount() - 1
                Dim dataSet As ChartDirector.DataSet = layer.getDataSetByZ(j)

                Dim dataPoint As Double = dataSet.getPosition(xIndex)
                If (dataPoint <> Chart.NoValue) And (dataSet.getDataColor() <> Chart.Transparent) Then
                    minY = Math.Min(minY, c.getYCoor(dataPoint, dataSet.getUseYAxis()))
                End If
            Next
        Next

        ' Draw a vertical track line at the x-position up to the highest data point.
        d.vline(Math.Max(minY, plotArea.getTopY()), plotArea.getBottomY() + 6, xCoor, d.dashLineColor( _
            &H000000, &H0101))

        ' Draw a label on the x-axis to show the track line position
        d.text("<*font,bgColor=000000*> " & c.xAxis().getFormattedLabel(xValue, "mmm dd, yyyy") & _
            " <*/font*>", "Arial Bold", 8).draw(xCoor, plotArea.getBottomY() + 6, &Hffffff, Chart.Top)

        ' Iterate through all layers to build the legend array
        For i As Integer = 0 To c.getLayerCount() - 1
            Dim layer As Layer = c.getLayerByZ(i)

            ' The data array index of the x-value
            Dim xIndex As Integer = layer.getXIndexOf(xValue)

            ' Iterate through all the data sets in the layer
            For j As Integer = 0 To layer.getDataSetCount() - 1
                Dim dataSet As ChartDirector.DataSet = layer.getDataSetByZ(j)

                ' The positional value, axis binding, pixel coordinate and color of the data point.
                Dim dataPoint As Double = dataSet.getPosition(xIndex)
                Dim yAxis As Axis = dataSet.getUseYAxis()
                Dim yCoor As Integer = c.getYCoor(dataPoint, yAxis)
                Dim color As Integer = dataSet.getDataColor()

                ' Draw the axis label only for visible data points of named data sets
                If (dataPoint <> Chart.NoValue) And (color <> Chart.Transparent) And (yCoor >= _
                    plotArea.getTopY()) And (yCoor <= plotArea.getBottomY()) Then
                    ' The axis label consists of 3 parts - a track dot for the data point, an axis label, and
                    ' a line joining the track dot to the axis label.

                    ' Draw the line first. The end point of the line at the axis label side depends on whether
                    ' the label is at the left or right side of the axis (that is, on whether the axis is on
                    ' the left or right side of the plot area).
                    Dim xPos As Integer = yAxis.getX() + IIf(yAxis.getAlignment() = Chart.Left, -4, 4)
                    d.hline(xCoor, xPos, yCoor, d.dashLineColor(color, &H0101))

                    ' Draw the track dot
                    d.circle(xCoor, yCoor, 4, 4, color, color)

                    ' Draw the axis label. If the axis is on the left side of the plot area, the labels should
                    ' right aligned to the axis, and vice versa.
                    d.text("<*font,bgColor=" & Hex(color) & "*> " & c.formatValue(dataPoint, "{value|P4}") & _
                        " <*/font*>", "Arial Bold", 8).draw(xPos, yCoor, &Hffffff, IIf(yAxis.getAlignment() _
                         = Chart.Left, Chart.Right, Chart.Left))
                End If
            Next
        Next

    End Sub

End Class
