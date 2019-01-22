Imports ChartDirector

Public Class FrmViewPortControlDemo

    ' Data arrays
    Dim timeStamps As DateTime()
    Dim dataSeriesA As Double()
    Dim dataSeriesB As Double()
    Dim dataSeriesC As Double()

    ' Flag to indicated if initialization has been completed. Prevents events from firing before 
    ' controls are properly initialized.
    Private hasFinishedInitialization As Boolean

    Private Sub FrmViewPortControlDemo_Load(ByVal sender As Object, ByVal e As EventArgs) _
        Handles MyBase.Load

        ' Load the data
        loadData()

        ' Initialize the WinChartViewer
        initChartViewer(winChartViewer1)

        ' Can handle events now
        hasFinishedInitialization = True

        ' Trigger the ViewPortChanged event to draw the chart
        winChartViewer1.updateViewPort(True, True)

        ' Draw the full thumbnail chart for the ViewPortControl
        drawFullChart(viewPortControl1, winChartViewer1)

    End Sub

    '
    ' Load the data
    '
    Private Sub loadData()

        ' In this example, we just use random numbers as data.
        Dim r As RanSeries = New RanSeries(127)
        timeStamps = r.getDateSeries(1827, New DateTime(2010, 1, 1), 86400)
        dataSeriesA = r.getSeries2(1827, 150, -10, 10)
        dataSeriesB = r.getSeries2(1827, 200, -10, 10)
        dataSeriesC = r.getSeries2(1827, 250, -8, 8)

    End Sub

    '
    ' Initialize the WinChartViewer
    '
    Private Sub initChartViewer(ByVal viewer As WinChartViewer)

        ' Set the full x range to be the duration of the data
        viewer.SetFullRange("x", timeStamps(0), timeStamps(timeStamps.Length - 1))

        ' Initialize the view port to show the latest 20% of the time range
        viewer.ViewPortWidth = 0.2
        viewer.ViewPortLeft = 1 - viewer.ViewPortWidth

        ' Set the maximum zoom to 10 points
        viewer.ZoomInWidthLimit = 10.0 / timeStamps.Length

        ' Enable mouse wheel zooming by setting the zoom ratio to 1.1 per wheel event
        viewer.MouseWheelZoomRatio = 1.1

        ' Initially set the mouse usage to "Pointer" mode (Drag to Scroll mode)
        pointerPB.Checked = True

    End Sub

    '
    ' The ViewPortChanged event handler. This event occurs if the user scrolls or zooms in
    ' or out the chart by dragging or clicking on the chart. It can also be triggered by
    ' calling WinChartViewer.updateViewPort.
    '
    Private Sub winChartViewer1_ViewPortChanged(ByVal sender As Object, _
        ByVal e As WinViewPortEventArgs) Handles winChartViewer1.ViewPortChanged

        ' Update the chart if necessary
        If e.NeedUpdateChart Then
            drawChart(winChartViewer1)
        End If

    End Sub

    '
    ' Draw the chart.
    '
    Private Sub drawChart(ByVal viewer As WinChartViewer)

        ' Get the start date and end date that are visible on the chart.
        Dim viewPortStartDate As DateTime = Chart.NTime(viewer.GetValueAtViewPort("x", viewer.ViewPortLeft))
        Dim viewPortEndDate As DateTime = Chart.NTime(viewer.GetValueAtViewPort("x", viewer.ViewPortLeft + _
            viewer.ViewPortWidth))

        ' Get the array indexes that corresponds to the visible start and end dates
        Dim startIndex As Integer = Math.Floor(Chart.bSearch(timeStamps, viewPortStartDate))
        Dim endIndex As Integer = Math.Ceiling(Chart.bSearch(timeStamps, viewPortEndDate))
        Dim noOfPoints As Integer = endIndex - startIndex + 1

        ' Extract the part of the data array that are visible.
        Dim viewPortTimeStamps As DateTime() = Chart.arraySlice(timeStamps, startIndex, noOfPoints)
        Dim viewPortDataSeriesA As Double() = Chart.arraySlice(dataSeriesA, startIndex, noOfPoints)
        Dim viewPortDataSeriesB As Double() = Chart.arraySlice(dataSeriesB, startIndex, noOfPoints)
        Dim viewPortDataSeriesC As Double() = Chart.arraySlice(dataSeriesC, startIndex, noOfPoints)

        '
        ' At this stage, we have extracted the visible data. We can use those data to plot the chart.
        '

        '================================================================================
        ' Configure overall chart appearance.
        '================================================================================

        ' Create an XYChart object of size 640 x 400 pixels
        Dim c As XYChart = New XYChart(640, 400)

        ' Set the plotarea at (55, 55) with width 80 pixels less than chart width, and height 90 pixels
        ' less than chart height. Use a vertical gradient from light blue (f0f6ff) to sky blue (a0c0ff)
        ' as background. Set border to transparent and grid lines to white (ffffff).
        c.setPlotArea(55, 55, c.getWidth() - 80, c.getHeight() - 90, c.linearGradientColor(0, 55, 0, _
            c.getHeight() - 35, &HF0F6FF, &HA0C0FF), -1, Chart.Transparent, &HFFFFFF, &HFFFFFF)

        ' As the data can lie outside the plotarea in a zoomed chart, we need enable clipping.
        c.setClipping()

        ' Add a title to the chart using 15pt Arial Bold font
        c.addTitle("   Zooming and Scrolling with Viewport Control", "Arial Bold", 15)

        ' Set legend icon style to use line style icon, sized for 8pt font
        c.getLegend().setLineStyleKey()
        c.getLegend().setFontSize(8)

        ' Set the x and y axis stems to transparent and the label font to 10pt Arial
        c.xAxis().setColors(Chart.Transparent)
        c.yAxis().setColors(Chart.Transparent)
        c.xAxis().setLabelStyle("Arial", 10)
        c.yAxis().setLabelStyle("Arial", 10)

        ' Add axis title using 10pt Arial Bold font
        c.yAxis().setTitle("Ionic Temperature (C)", "Arial Bold", 10)

        '================================================================================
        ' Add data to chart
        '================================================================================

        '
        ' In this example, we represent the data by lines. You may modify the code below to use other
        ' representations (areas, scatter plot, etc).
        '

        ' Add a line layer for the lines, using a line width of 2 pixels
        Dim layer As LineLayer = c.addLineLayer2()
        layer.setLineWidth(2)

        ' In this demo, we do not have too many data points. In real code, the chart may contain a lot
        ' of data points when fully zoomed out - much more than the number of horizontal pixels in this
        ' plot area. So it is a good idea to use fast line mode.
        layer.setFastLineMode()

        ' Now we add the 3 data series to a line layer, using the color red (ff33333), green (008800)
        ' and blue (3333cc)
        layer.setXData(viewPortTimeStamps)
        layer.addDataSet(viewPortDataSeriesA, &HFF3333, "Alpha")
        layer.addDataSet(viewPortDataSeriesB, &H8800, "Beta")
        layer.addDataSet(viewPortDataSeriesC, &H3333CC, "Gamma")

        '================================================================================
        ' Configure axis scale and labelling
        '================================================================================

        ' Set the x-axis as a date/time axis with the scale according to the view port x range.
        viewer.SyncDateAxisWithViewPort("x", c.xAxis())

        ' For the automatic y-axis labels, set the minimum spacing to 30 pixels.
        c.yAxis().setTickDensity(30)

        '
        ' In this demo, the time range can be from a few years to a few days. We demonstrate how to set
        ' up different date/time format based on the time range.
        '

        ' If all ticks are yearly aligned, then we use "yyyy" as the label format.
        c.xAxis().setFormatCondition("align", 360 * 86400)
        c.xAxis().setLabelFormat("{value|yyyy}")

        ' If all ticks are monthly aligned, then we use "mmm yyyy" in bold font as the first label of a
        ' year, and "mmm" for other labels.
        c.xAxis().setFormatCondition("align", 30 * 86400)
        c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "<*font=bold*>{value|mmm<*br*>yyyy}", _
            Chart.AllPassFilter(), "{value|mmm}")

        ' If all ticks are daily algined, then we use "mmm dd<*br*>yyyy" in bold font as the first
        ' label of a year, and "mmm dd" in bold font as the first label of a month, and "dd" for other
        ' labels.
        c.xAxis().setFormatCondition("align", 86400)
        c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), _
            "<*block,halign=left*><*font=bold*>{value|mmm dd<*br*>yyyy}", Chart.StartOfMonthFilter(), _
            "<*font=bold*>{value|mmm dd}")
        c.xAxis().setMultiFormat2(Chart.AllPassFilter(), "{value|dd}")

        ' For all other cases (sub-daily ticks), use "hh:nn<*br*>mmm dd" for the first label of a day,
        ' and "hh:nn" for other labels.
        c.xAxis().setFormatCondition("else")
        c.xAxis().setMultiFormat(Chart.StartOfDayFilter(), "<*font=bold*>{value|hh:nn<*br*>mmm dd}", _
            Chart.AllPassFilter(), "{value|hh:nn}")

        '================================================================================
        ' Output the chart
        '================================================================================

        ' We need to update the track line too. If the mouse is moving on the chart (eg. if 
        ' the user drags the mouse on the chart to scroll it), the track line will be updated
        ' in the MouseMovePlotArea event. Otherwise, we need to update the track line here.
        If Not viewer.IsInMouseMoveEvent Then
            trackLineLegend(c, IIf(IsNothing(viewer.Chart), c.getPlotArea().getRightX(), _
                viewer.PlotAreaMouseX))
        End If

        viewer.Chart = c

    End Sub

    Private Sub drawFullChart(ByVal vpc As WinViewPortControl, ByVal viewer As WinChartViewer)

        ' Create an XYChart object of size 640 x 60 pixels   
        Dim c As XYChart = New XYChart(640, 60)

        ' Set the plotarea with the same horizontal position as that in the main chart for alignment.
        c.setPlotArea(55, 0, c.getWidth() - 80, c.getHeight() - 1, &HC0D8FF, -1, &H888888, _
            Chart.Transparent, &HFFFFFF)

        ' Set the x axis stem to transparent and the label font to 10pt Arial
        c.xAxis().setColors(Chart.Transparent)
        c.xAxis().setLabelStyle("Arial", 10)

        ' Put the x-axis labels inside the plot area by setting a negative label gap. Use
        ' setLabelAlignment to put the label at the right side of the tick.
        c.xAxis().setLabelGap(-1)
        c.xAxis().setLabelAlignment(1)

        ' Set the y axis stem and labels to transparent (that is, hide the labels)
        c.yAxis().setColors(Chart.Transparent, Chart.Transparent)

        ' Add a line layer for the lines with fast line mode enabled
        Dim layer As LineLayer = c.addLineLayer()
        layer.setFastLineMode()

        ' Now we add the 3 data series to a line layer, using the color red (&Hff3333), green
        ' (&H008800) and blue (&H3333cc)
        layer.setXData(timeStamps)
        layer.addDataSet(dataSeriesA, &HFF3333)
        layer.addDataSet(dataSeriesB, &H8800)
        layer.addDataSet(dataSeriesC, &H3333CC)

        ' The x axis scales should reflect the full range of the view port
        c.xAxis().setDateScale(viewer.getValueAtViewPort("x", 0), viewer.getValueAtViewPort("x", 1))

        ' For the automatic x-axis labels, set the minimum spacing to 75 pixels.
        c.xAxis().setTickDensity(75)

        ' For the auto-scaled y-axis, as we hide the labels, we can disable axis rounding. This can
        ' make the axis scale fit the data tighter.
        c.yAxis().setRounding(False, False)

        ' Output the chart
        vpc.Chart = c

    End Sub

    '
    ' Pointer (Drag to Scroll) button event handler
    '
    Private Sub pointerPB_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles pointerPB.CheckedChanged

        If sender.Checked Then
            winChartViewer1.MouseUsage = WinChartMouseUsage.ScrollOnDrag
        End If

    End Sub

    '
    ' Zoom In button event handler
    '
    Private Sub zoomInPB_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles zoomInPB.CheckedChanged

        If sender.Checked Then
            winChartViewer1.MouseUsage = WinChartMouseUsage.ZoomIn
        End If

    End Sub

    '
    ' Zoom Out button event handler
    '
    Private Sub zoomOutPB_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles zoomOutPB.CheckedChanged

        If sender.Checked Then
            winChartViewer1.MouseUsage = WinChartMouseUsage.ZoomOut
        End If

    End Sub

    '
    ' Save button event handler
    '
    Private Sub savePB_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles savePB.Click

        ' The standard Save File dialog
        Dim fileDlg As SaveFileDialog = New SaveFileDialog()
        fileDlg.Filter = "PNG (*.png)|*.png|JPG (*.jpg)|*.jpg|GIF (*.gif)|*.gif|BMP (*.bmp)|*.bmp|" & _
            "SVG (*.svg)|*.svg|PDF (*.pdf)|*.pdf"
        fileDlg.FileName = "chartdirector_demo"
        If fileDlg.ShowDialog() <> DialogResult.OK Then
            Return
        End If
        ' Save the chart
        If Not IsNothing(winChartViewer1.Chart) Then
            winChartViewer1.Chart.makeChart(fileDlg.FileName)
        End If

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
    Private Sub trackLineLegend(ByVal c As XYChart, ByVal mouseX As Integer)

        ' Clear the current dynamic layer and get the DrawArea object to draw on it.
        Dim d As DrawArea = c.initDynamicLayer()

        ' The plot area object
        Dim plotArea As PlotArea = c.getPlotArea()

        ' Get the data x-value that is nearest to the mouse, and find its pixel coordinate.
        Dim xValue As Double = c.getNearestXValue(mouseX)
        Dim xCoor As Integer = c.getXCoor(xValue)

        ' Draw a vertical track line at the x-position
        d.vline(plotArea.getTopY(), plotArea.getBottomY(), xCoor, &HAAAAAA)

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
                If (Not String.IsNullOrEmpty(dataName)) And (color <> Chart.Transparent) Then
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
        Dim t As TTFText = d.text(legendText, "Arial Bold", 10)
        t.draw(plotArea.getLeftX() + 5, plotArea.getTopY() - 3, &H0, Chart.BottomLeft)

    End Sub

End Class