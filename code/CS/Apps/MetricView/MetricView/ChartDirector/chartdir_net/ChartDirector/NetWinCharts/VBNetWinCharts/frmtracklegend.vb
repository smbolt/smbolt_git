Imports ChartDirector
Imports System.Collections

Public Class FrmTrackLegend

    Private Sub FrmTrackLegend_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

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

        ' Set legend icon style to use line style icon, sized for 8pt font
        c.getLegend().setLineStyleKey()
        c.getLegend().setFontSize(8)

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

        ' Include track line with legend for the latest data values
        trackLineLegend(c, c.getPlotArea().getRightX())

        ' Assign the chart to the WinChartViewer
        winChartViewer1.Chart = c

    End Sub

    '
    ' Draw track cursor when mouse is moving over plotarea
    '
    Private Sub winChartViewer1_MouseMovePlotArea(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.MouseEventArgs) Handles winChartViewer1.MouseMovePlotArea

        Dim viewer As WinChartViewer = sender
        trackLineLegend(viewer.Chart, viewer.PlotAreaMouseX)
        viewer.updateDisplay()

    End Sub

    '
    ' Draw the track line with legend
    '
    Private Sub trackLineLegend(c As XYChart, mouseX As Integer)

        ' Clear the current dynamic layer and get the DrawArea object to draw on it.
        Dim d As DrawArea = c.initDynamicLayer()

        ' The plot area object
        Dim plotArea As PlotArea = c.getPlotArea()

        ' Get the data x-value that is nearest to the mouse, and find its pixel coordinate.
        Dim xValue As Double = c.getNearestXValue(mouseX)
        Dim xCoor As Integer = c.getXCoor(xValue)

        ' Draw a vertical track line at the x-position
        d.vline(plotArea.getTopY(), plotArea.getBottomY(), xCoor, d.dashLineColor(&H000000, &H0101))

        ' Container to hold the legend entries
        Dim legendEntries As ArrayList = New ArrayList()

        ' Iterate through all layers to build the legend array
        For i As Integer = 0 To c.getLayerCount() - 1
            Dim layer As Layer = c.getLayerByZ(i)

            ' The data array index of the x-value
            Dim xIndex As Integer = layer.getXIndexOf(xValue)

            ' Iterate through all the data sets in the layer
            For j As Integer = 0 To layer.getDataSetCount() - 1
                Dim dataSet As ChartDirector.DataSet = layer.getDataSetByZ(j)

                ' We are only interested in visible data sets with names
                Dim dataName As String = dataSet.getDataName()
                Dim color As Integer = dataSet.getDataColor()
                If (Not string.IsNullOrEmpty(dataName)) And (color <> Chart.Transparent) Then
                    ' Build the legend entry, consist of the legend icon, name and data value.
                    Dim dataValue As Double = dataSet.getValue(xIndex)
                    legendEntries.Add("<*block*>" & dataSet.getLegendIcon() & " " & dataName & ": " & IIf( _
                        dataValue = Chart.NoValue, "N/A", c.formatValue(dataValue, "{value|P4}")) & "<*/*>")

                    ' Draw a track dot for data points within the plot area
                    Dim yCoor As Integer = c.getYCoor(dataSet.getPosition(xIndex), dataSet.getUseYAxis())
                    If (yCoor >= plotArea.getTopY()) And (yCoor <= plotArea.getBottomY()) Then
                        d.circle(xCoor, yCoor, 4, 4, color, color)
                    End If
                End If
            Next
        Next

        ' Create the legend by joining the legend entries
        legendEntries.Reverse()
        Dim legendText As String = "<*block,maxWidth=" & plotArea.getWidth() & _
            "*><*block*><*font=Arial Bold*>[" & c.xAxis().getFormattedLabel(xValue, "mmm dd, yyyy") & _
            "]<*/*>        " & Join(CType(legendEntries.ToArray(GetType(String)), String()), "        ") & _
            "<*/*>"

        ' Display the legend on the top of the plot area
        Dim t As TTFText = d.text(legendText, "Arial", 8)
        t.draw(plotArea.getLeftX() + 5, plotArea.getTopY() - 3, &H000000, Chart.BottomLeft)

    End Sub

End Class
