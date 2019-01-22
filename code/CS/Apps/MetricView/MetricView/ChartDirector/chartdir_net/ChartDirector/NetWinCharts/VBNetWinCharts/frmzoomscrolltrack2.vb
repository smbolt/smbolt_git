Imports ChartDirector

Public Class FrmZoomScrollTrack2

    ' Data arrays
    Dim timeStamps As DateTime()
    Dim dataSeriesA As Double()
    Dim dataSeriesB As Double()
    Dim dataSeriesC As Double()

    ' Flag to indicated if initialization has been completed. Prevents events from firing before 
    ' controls are properly initialized.
    Private hasFinishedInitialization As Boolean

    Private Sub FrmZoomScrollTrack2_Load(ByVal sender As Object, ByVal e As EventArgs) _
        Handles MyBase.Load

        ' Load the data
        loadData()

        ' Initialize the WinChartViewer
        initChartViewer(winChartViewer1)

        ' Can handle events now
        hasFinishedInitialization = True

        ' Trigger the ViewPortChanged event to draw the chart
        winChartViewer1.updateViewPort(True, True)

    End Sub

    '
    ' Load the data
    '
    Private Sub loadData()

        ' In this example, we just use random numbers as data.
        Dim r As RanSeries = New RanSeries(127)
        timeStamps = r.getDateSeries(1827, New DateTime(2007, 1, 1), 86400)
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

        ' In addition to updating the chart, we may also need to update other controls that
        ' changes based on the view port.
        updateControls(winChartViewer1)

        ' Update the chart if necessary
        If e.NeedUpdateChart Then
            drawChart(winChartViewer1)
        End If

    End Sub

    '
    ' Update controls when the view port changed
    '
    Private Sub updateControls(ByVal viewer As WinChartViewer)

        ' Update the start date and end date control to reflect the view port.
        startDateCtrl.Value = Chart.NTime(viewer.GetValueAtViewPort("x", viewer.ViewPortLeft))
        endDateCtrl.Value = Chart.NTime(viewer.GetValueAtViewPort("x", viewer.ViewPortLeft + _
            viewer.ViewPortWidth))

        ' Update the scroll bar to reflect the view port position and width of the view port.
        hScrollBar1.Enabled = winChartViewer1.ViewPortWidth < 1
        hScrollBar1.LargeChange = Math.Ceiling(winChartViewer1.ViewPortWidth * _
            (hScrollBar1.Maximum - hScrollBar1.Minimum))
        hScrollBar1.SmallChange = Math.Ceiling(hScrollBar1.LargeChange * 0.1)
        hScrollBar1.Value = Math.Round(winChartViewer1.ViewPortLeft * _
            (hScrollBar1.Maximum - hScrollBar1.Minimum)) + hScrollBar1.Minimum

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

        ' Create an XYChart object of size 640 x 350 pixels
        Dim c As XYChart = New XYChart(640, 350)

        ' Set the plotarea at (55, 50) with width 80 pixels less than chart width, and height 85 pixels
        ' less than chart height. Use a vertical gradient from light blue (f0f6ff) to sky blue (a0c0ff)
        ' as background. Set border to transparent and grid lines to white (ffffff).
        c.setPlotArea(55, 50, c.getWidth() - 80, c.getHeight() - 85, c.linearGradientColor(0, 50, 0, _
            c.getHeight() - 35, &HF0F6FF, &HA0C0FF), -1, Chart.Transparent, &HFFFFFF, &HFFFFFF)

        ' As the data can lie outside the plotarea in a zoomed chart, we need enable clipping.
        c.setClipping()

        ' Add a title to the chart using 18 pts Times New Roman Bold Italic font
        c.addTitle("   Zooming and Scrolling with Track Line (2)", "Times New Roman Bold Italic", 18)

        ' Add a legend box at (55, 25) using horizontal layout. Use 8pts Arial Bold as font. Set the
        ' background and border color to Transparent and use line style legend key.
        Dim b As LegendBox = c.addLegend(55, 25, False, "Arial Bold", 8)
        b.setBackground(Chart.Transparent)
        b.setLineStyleKey()

        ' Set the axis stem to transparent
        c.xAxis().setColors(Chart.Transparent)
        c.yAxis().setColors(Chart.Transparent)

        ' Add axis title using 10pts Arial Bold Italic font
        c.yAxis().setTitle("Ionic Temperature (C)", "Arial Bold Italic", 10)

        '================================================================================
        ' Add data to chart
        '================================================================================

        '
        ' In this example, we represent the data by lines. You may modify the code below to use other
        ' representations (areas, scatter plot, etc).
        '

        ' Add a line layer for the lines, using a line width of 2 pixels
        Dim layer As LineLayer = c.addLineLayer2()
        Layer.setLineWidth(2)

        ' In this demo, we do not have too many data points. In real code, the chart may contain a lot
        ' of data points when fully zoomed out - much more than the number of horizontal pixels in this
        ' plot area. So it is a good idea to use fast line mode.
        Layer.setFastLineMode()

        ' Now we add the 3 data series to a line layer, using the color red (ff33333), green (008800)
        ' and blue (3333cc)
        Layer.setXData(viewPortTimeStamps)
        Layer.addDataSet(viewPortDataSeriesA, &HFF3333, "Alpha")
        Layer.addDataSet(viewPortDataSeriesB, &H008800, "Beta")
        Layer.addDataSet(viewPortDataSeriesC, &H3333CC, "Gamma")

        '================================================================================
        ' Configure axis scale and labelling
        '================================================================================

        ' Set the x-axis as a date/time axis with the scale according to the view port x range.
        viewer.SyncDateAxisWithViewPort("x", c.xAxis())

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
        c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "<*font=bold*>{value|mmm yyyy}", _
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
        If (Not viewer.IsInMouseMoveEvent) And viewer.IsMouseOnPlotArea Then
            trackLineLabel(c, viewer.PlotAreaMouseX)
        End If

        viewer.Chart = c

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
    ' The scroll bar event handler
    '
    Private Sub hScrollBar1_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles hScrollBar1.ValueChanged

        ' When the view port is changed (user drags on the chart to scroll), the scroll bar will get
        ' updated. When the scroll bar changes (eg. user drags on the scroll bar), the view port will
        ' get updated. This creates an infinite loop. To avoid this, the scroll bar can update the 
        ' view port only if the view port is not updating the scroll bar.
        If hasFinishedInitialization And Not winChartViewer1.IsInViewPortChangedEvent Then
            ' Set the view port based on the scroll bar
            winChartViewer1.ViewPortLeft = (hScrollBar1.Value - hScrollBar1.Minimum) / _
                (hScrollBar1.Maximum - hScrollBar1.Minimum)

            ' Trigger a view port changed event to update the chart
            winChartViewer1.updateViewPort(True, False)
        End If

    End Sub

    '
    ' Start Date control event handler
    '
    Private Sub startDateCtrl_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles startDateCtrl.ValueChanged

        ' The date control can update the view port only if it is not currently being updated in the
        ' view port changed event (that is, only if the date is changed due to user action).
        If hasFinishedInitialization And Not winChartViewer1.IsInViewPortChangedEvent Then
            ' The updated view port width
            Dim vpWidth As Double = winChartViewer1.ViewPortLeft + winChartViewer1.ViewPortWidth - _
                winChartViewer1.GetViewPortAtValue("x", Chart.CTime(startDateCtrl.Value))

            ' Make sure the updated view port width is within bounds
            vpWidth = Math.Max(winChartViewer1.ZoomInWidthLimit, Math.Min(vpWidth, _
                winChartViewer1.ViewPortLeft + winChartViewer1.ViewPortWidth))

            ' Update view port and trigger a view port changed event to update the chart
            winChartViewer1.ViewPortLeft += winChartViewer1.ViewPortWidth - vpWidth
            winChartViewer1.ViewPortWidth = vpWidth
            winChartViewer1.updateViewPort(True, False)
        End If

    End Sub

    '
    ' End Date control event handler
    '
    Private Sub endDateCtrl_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles endDateCtrl.ValueChanged

        ' The date control can update the view port only if it is not currently being updated in the
        ' view port changed event (that is, only if the date is changed due to user action).
        If hasFinishedInitialization And Not winChartViewer1.IsInViewPortChangedEvent Then
            ' The updated view port width
            Dim vpWidth As Double = winChartViewer1.GetViewPortAtValue("x", _
                Chart.CTime(endDateCtrl.Value)) - winChartViewer1.ViewPortLeft

            ' Make sure the updated view port width is within bounds
            vpWidth = Math.Max(winChartViewer1.ZoomInWidthLimit, Math.Min(vpWidth, _
                1 - winChartViewer1.ViewPortLeft))

            ' Update view port and trigger a view port changed event to update the chart
            winChartViewer1.ViewPortWidth = vpWidth
            winChartViewer1.updateViewPort(True, False)
        End If

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
    Private Sub trackLineLabel(ByVal c As XYChart, ByVal mouseX As Integer)

        ' Clear the current dynamic layer and get the DrawArea object to draw on it.
        Dim d As DrawArea = c.initDynamicLayer()

        ' The plot area object
        Dim plotArea As PlotArea = c.getPlotArea()

        ' Get the data x-value that is nearest to the mouse, and find its pixel coordinate.
        Dim xValue As Double = c.getNearestXValue(mouseX)
        Dim xCoor As Integer = c.getXCoor(xValue)

        ' Draw a vertical track line at the x-position
        d.vline(plotArea.getTopY(), plotArea.getBottomY(), xCoor, d.dashLineColor(&H0, &H101))

        ' Draw a label on the x-axis to show the track line position.
        Dim xlabel As String = "<*font,bgColor=000000*> " & c.xAxis().getFormattedLabel(xValue, _
            "mmm dd, yyyy") & " <*/font*>"
        Dim t As TTFText = d.text(xlabel, "Arial Bold", 8)

        ' Restrict the x-pixel position of the label to make sure it stays inside the chart image.
        Dim xLabelPos As Integer = Math.Max(0, Math.Min(xCoor - t.getWidth() / 2, c.getWidth() - t.getWidth( _
            )))
        t.draw(xLabelPos, plotArea.getBottomY() + 6, &HFFFFFF)

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
                    Chart.Transparent) And (Not String.IsNullOrEmpty(dataSet.getDataName())) Then

                    d.circle(xCoor, yCoor, 4, 4, color, color)

                    Dim label As String = "<*font,bgColor=" & Hex(color) & "*> " & c.formatValue( _
                        dataSet.getValue(xIndex), "{value|P4}") & " <*/font*>"
                    t = d.text(label, "Arial Bold", 8)

                    ' Draw the label on the right side of the dot if the mouse is on the left side the chart,
                    ' and vice versa. This ensures the label will not go outside the chart image.
                    If xCoor <= (plotArea.getLeftX() + plotArea.getRightX()) / 2 Then
                        t.draw(xCoor + 5, yCoor, &HFFFFFF, Chart.Left)
                    Else
                        t.draw(xCoor - 5, yCoor, &HFFFFFF, Chart.Right)
                    End If
                End If
            Next
        Next

    End Sub

End Class