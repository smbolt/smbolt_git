Imports ChartDirector

Public Class FrmRealTimeZoomScroll

    ' The data arrays that store the visible data. The data arrays are updated in realtime. In
    ' this demo, we plot the last 240 samples.
    Private Const sampleSize As Integer = 10000
    Private dataSeriesA(sampleSize - 1) As Double
    Private dataSeriesB(sampleSize - 1) As Double
    Private dataSeriesC(sampleSize - 1) As Double
    Private timeStamps(sampleSize - 1) As Date

    ' The index of the array position to which new data values are added.
    Private currentIndex As Integer = 0

    ' The full range is initialized to 60 seconds of data. It can be extended when more data
    ' are available.
    Private initialFullRange As Integer = 60

    ' The maximum zoom in is 10 seconds.
    Private zoomInLimit As Integer = 10

    ' In this demo, we use a data generator driven by a timer to generate realtime data. The
    ' nextDataTime is an internal variable used by the data generator to keep track of which
    ' values to generate next.
    Private nextDataTime As DateTime = New DateTime((Now.Ticks \ 10000000) * 10000000)

    ' Flag to indicated if initialization has been completed. Prevents events from firing before 
    ' controls are properly initialized.
    Private hasFinishedInitialization As Boolean

    Private Sub FrmRealTimeZoomScroll_Load(ByVal sender As Object, ByVal e As EventArgs) _
        Handles MyBase.Load

        ' Initialize the WinChartViewer
        initChartViewer(winChartViewer1)

        ' Data generation rate
        dataRateTimer.Interval = 250

        ' Chart update rate
        chartUpdateTimer.Interval = CInt(samplePeriod.Value)

        ' Can handle events now
        hasFinishedInitialization = True

        ' Now can start the timers for data collection and chart update
        dataRateTimer.Start()
        chartUpdateTimer.Start()

    End Sub

    '
    ' Initialize the WinChartViewer
    '
    Private Sub initChartViewer(ByVal viewer As WinChartViewer)

        viewer.MouseWheelZoomRatio = 1.1

        ' Initially set the mouse usage to "Pointer" mode (Drag to Scroll mode)
        pointerPB.Checked = True

    End Sub


    '/ <summary>
    '/ The data generator - invoke once every 250ms
    '/ </summary>
    Private Sub dataRateTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles dataRateTimer.Tick

        Do While nextDataTime < DateTime.Now
            '
            ' In this demo, we use some formulas to generate new values. In real applications,
            ' it may be replaced by some data acquisition code.
            '
            Dim p As Double = nextDataTime.Ticks / 10000000.0 * 4
            Dim dataA As Double = 20 + Math.Cos(p * 2.2) * 10 + 1 / (Math.Cos(p) * Math.Cos(p) + 0.01)
            Dim dataB As Double = 150 + 100 * Math.Sin(p / 27.7) * Math.Sin(p / 10.1)
            Dim dataC As Double = 150 + 100 * Math.Cos(p / 6.7) * Math.Cos(p / 11.9)

            ' In this demo, if the data arrays are full, the oldest 5% of data are discarded.
            If currentIndex >= timeStamps.Length Then
                currentIndex = Int(sampleSize * 95 / 100) - 1
                For i As Integer = 0 To currentIndex - 1
                    Dim srcIndex As Integer = i + sampleSize - currentIndex
                    timeStamps(i) = timeStamps(srcIndex)
                    dataSeriesA(i) = dataSeriesA(srcIndex)
                    dataSeriesB(i) = dataSeriesB(srcIndex)
                    dataSeriesC(i) = dataSeriesC(srcIndex)
                Next
            End If

            ' Store the new values in the current index position, and increment the index.
            timeStamps(currentIndex) = nextDataTime
            dataSeriesA(currentIndex) = dataA
            dataSeriesB(currentIndex) = dataB
            dataSeriesC(currentIndex) = dataC
            currentIndex += 1

            ' Update nextDataTime. This is needed by our data generator. In real applications,
            ' you may not need this variable or the associated do/while loop.
            nextDataTime = nextDataTime.AddMilliseconds(dataRateTimer.Interval)
        Loop

        ' We provide some visual feedback to the numbers generated, so you can see the
        ' values being generated.
        valueA.Text = dataSeriesA(currentIndex - 1).ToString(".##")
        valueB.Text = dataSeriesB(currentIndex - 1).ToString(".##")
        valueC.Text = dataSeriesC(currentIndex - 1).ToString(".##")

    End Sub

    '
    ' Update the chart and the viewport periodically
    '
    Private Sub chartUpdateTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles chartUpdateTimer.Tick

        Dim viewer As WinChartViewer = winChartViewer1

        If currentIndex > 0 Then
            '
            ' As we added more data, we may need to update the full range. 
            '

            Dim startDate As DateTime = timeStamps(0)
            Dim endDate As DateTime = timeStamps(currentIndex - 1)

            ' Use the initialFullRange if this is sufficient.
            Dim duration As Double = endDate.Subtract(startDate).TotalSeconds
            If duration < initialFullRange Then
                endDate = startDate.AddSeconds(initialFullRange)
            End If

            ' Update the full range to reflect the actual duration of the data. In this case, 
            ' if the view port is viewing the latest data, we will scroll the view port as new
            ' data are added. If the view port is viewing historical data, we would keep the 
            ' axis scale unchanged to keep the chart stable.
            Dim updateType As Integer = Chart.ScrollWithMax
            If viewer.ViewPortLeft + viewer.ViewPortWidth < 0.999 Then
                updateType = Chart.KeepVisibleRange
            End If
            Dim axisScaleHasChanged As Boolean = viewer.updateFullRangeH("x", startDate, endDate, updateType)

            ' Set the zoom in limit as a ratio to the full range
            viewer.ZoomInWidthLimit = zoomInLimit / (viewer.getValueAtViewPort("x", 1) - _
                viewer.getValueAtViewPort("x", 0))

            ' Trigger the viewPortChanged event to update the display if the axis scale has 
            ' changed or if new data are added to the existing axis scale.
            If axisScaleHasChanged Or duration < initialFullRange Then
                viewer.updateViewPort(True, False)
            End If

        End If

    End Sub

    '
    ' The viewPortChanged event handler. In this example, it just updates the chart. If you
    ' have other controls to update, you may also put the update code here.
    '
    Private Sub winChartViewer1_ViewPortChanged(ByVal sender As Object, ByVal e As WinViewPortEventArgs) _
        Handles winChartViewer1.ViewPortChanged

        ' In addition to updating the chart, we may also need to update other controls that
        ' changes based on the view port.
        updateControls(winChartViewer1)

        ' Update the chart if necessary
        If e.NeedUpdateChart Then
            drawChart(winChartViewer1)
        End If

    End Sub

    '
    ' Update other controls when the view port changed
    '
    Private Sub updateControls(ByVal viewer As WinChartViewer)
        ' Update the scroll bar to reflect the view port position and width.           
        hScrollBar1.Enabled = winChartViewer1.ViewPortWidth < 1
        hScrollBar1.LargeChange = Math.Ceiling(winChartViewer1.ViewPortWidth * _
            (hScrollBar1.Maximum - hScrollBar1.Minimum))
        hScrollBar1.SmallChange = Math.Ceiling(hScrollBar1.LargeChange * 0.1)
        hScrollBar1.Value = Math.Round(winChartViewer1.ViewPortLeft * _
            (hScrollBar1.Maximum - hScrollBar1.Minimum)) + hScrollBar1.Minimum
    End Sub

    '
    ' Draw the chart
    '
    Private Sub drawChart(ByVal viewer As WinChartViewer)

        ' Get the start date and end date that are visible on the chart.
        Dim viewPortStartDate As DateTime = Chart.NTime(viewer.getValueAtViewPort("x", viewer.ViewPortLeft))
        Dim viewPortEndDate As DateTime = Chart.NTime(viewer.getValueAtViewPort("x", viewer.ViewPortLeft + _
            viewer.ViewPortWidth))

        ' Extract the part of the data arrays that are visible.
        Dim viewPortTimeStamps() As DateTime = Nothing
        Dim viewPortDataSeriesA() As Double = Nothing
        Dim viewPortDataSeriesB() As Double = Nothing
        Dim viewPortDataSeriesC() As Double = Nothing

        If currentIndex > 0 Then
            ' Get the array indexes that corresponds to the visible start and end dates
            Dim startIndex As Integer = Math.Floor(Chart.bSearch2(timeStamps, 0, currentIndex, viewPortStartDate))
            Dim endIndex As Integer = Math.Ceiling(Chart.bSearch2(timeStamps, 0, currentIndex, viewPortEndDate))
            Dim noOfPoints As Integer = endIndex - startIndex + 1

            ' Extract the visible data
            viewPortTimeStamps = Chart.arraySlice(timeStamps, startIndex, noOfPoints)
            viewPortDataSeriesA = Chart.arraySlice(dataSeriesA, startIndex, noOfPoints)
            viewPortDataSeriesB = Chart.arraySlice(dataSeriesB, startIndex, noOfPoints)
            viewPortDataSeriesC = Chart.arraySlice(dataSeriesC, startIndex, noOfPoints)
        End If

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
        c.setPlotArea(55, 50, c.getWidth() - 85, c.getHeight() - 80, c.linearGradientColor(0, 50, 0, _
            c.getHeight() - 35, &HF0F6FF, &HA0C0FF), -1, Chart.Transparent, &HFFFFFF, &HFFFFFF)

        ' As the data can lie outside the plotarea in a zoomed chart, we need enable clipping.
        c.setClipping()

        ' Add a title to the chart using 18 pts Times New Roman Bold Italic font
        c.addTitle("  Realtime Chart with Zoom/Scroll and Track Line", "Arial", 18)

        ' Add a legend box at (55, 25) using horizontal layout. Use 8pts Arial Bold as font. Set the
        ' background and border color to Transparent and use line style legend key.
        Dim b As LegendBox = c.addLegend(55, 25, False, "Arial Bold", 10)
        b.setBackground(Chart.Transparent)
        b.setLineStyleKey()

        ' Set the x and y axis stems to transparent and the label font to 10pt Arial
        c.xAxis().setColors(Chart.Transparent)
        c.yAxis().setColors(Chart.Transparent)
        c.xAxis().setLabelStyle("Arial", 10)
        c.yAxis().setLabelStyle("Arial", 10)

        ' Add axis title using 10pts Arial Bold Italic font
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
        layer.setFastLineMode()

        ' Now we add the 3 data series to a line layer, using the color red (ff0000), green (00cc00)
        ' and blue (0000ff)
        layer.setXData(viewPortTimeStamps)
        layer.addDataSet(viewPortDataSeriesA, &HFF0000, "Alpha")
        layer.addDataSet(viewPortDataSeriesB, &HCC00, "Beta")
        layer.addDataSet(viewPortDataSeriesC, &HFF, "Gamma")

        '================================================================================
        ' Configure axis scale and labelling
        '================================================================================

        If currentIndex > 0 Then
            c.xAxis().setDateScale(viewPortStartDate, viewPortEndDate)
        End If

        ' For the automatic axis labels, set the minimum spacing to 75/30 pixels for the x/y axis.
        c.xAxis().setTickDensity(75)
        c.yAxis().setTickDensity(30)

        '
        ' In a zoomable chart, the time range can be from a few years to a few seconds. We can need
        ' to define the date/time format the various cases. 
        '

        ' If all ticks are year aligned, we use "yyyy" as the label format.
        c.xAxis().setFormatCondition("align", 360 * 86400)
        c.xAxis().setLabelFormat("{value|yyyy}")

        ' If all ticks are month aligned, we use "mmm yyyy" in bold font as the first label of a year,
        ' and "mmm" for other labels.
        c.xAxis().setFormatCondition("align", 30 * 86400)
        c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "<*font=bold*>{value|mmm yyyy}", _
            Chart.AllPassFilter(), "{value|mmm}")

        ' If all ticks are day algined, we use "mmm dd<*br*>yyyy" in bold font as the first label of a
        ' year, and "mmm dd" in bold font as the first label of a month, and "dd" for other labels.
        c.xAxis().setFormatCondition("align", 86400)
        c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), _
            "<*block,halign=left*><*font=bold*>{value|mmm dd<*br*>yyyy}", Chart.StartOfMonthFilter(), _
            "<*font=bold*>{value|mmm dd}")
        c.xAxis().setMultiFormat2(Chart.AllPassFilter(), "{value|dd}")

        ' If all ticks are hour algined, we use "hh:nn<*br*>mmm dd" in bold font as the first label of 
        ' the Day, and "hh:nn" for other labels.
        c.xAxis().setFormatCondition("align", 3600)
        c.xAxis().setMultiFormat(Chart.StartOfDayFilter(), "<*font=bold*>{value|hh:nn<*br*>mmm dd}", _
            Chart.AllPassFilter(), "{value|hh:nn}")

        ' If all ticks are minute algined, then we use "hh:nn" as the label format.
        c.xAxis().setFormatCondition("align", 60)
        c.xAxis().setLabelFormat("{value|hh:nn}")

        ' If all other cases, we use "hh:nn:ss" as the label format.
        c.xAxis().setFormatCondition("else")
        c.xAxis().setLabelFormat("{value|hh:nn:ss}")

        ' We make sure the tick increment must be at least 1 second.
        c.xAxis().setMinTickInc(1)

        '================================================================================
        ' Output the chart
        '================================================================================

        ' We need to update the track line too. If the mouse is moving on the chart (eg. if 
        ' the user drags the mouse on the chart to scroll it), the track line will be updated
        ' in the MouseMovePlotArea event. Otherwise, we need to update the track line here.
        If Not viewer.IsInMouseMoveEvent Then
            trackLineLabel(c, IIf(IsNothing(viewer.Chart), c.getPlotArea().getRightX(), _
                viewer.PlotAreaMouseX))
        End If

        viewer.Chart = c

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
        If xCoor < plotArea.getLeftX() Then
            Return
        End If

        ' Draw a vertical track line at the x-position
        d.vline(plotArea.getTopY(), plotArea.getBottomY(), xCoor, &H888888)

        ' Draw a label on the x-axis to show the track line position.
        Dim xlabel As String = "<*font,bgColor=000000*> " & c.xAxis().getFormattedLabel(xValue, _
            "hh:nn:ss.ff") & " <*/font*>"
        Dim t As TTFText = d.text(xlabel, "Arial Bold", 10)

        ' Restrict the x-pixel position of the label to make sure it stays inside the chart image.
        Dim xLabelPos As Integer = Math.Max(0, Math.Min(xCoor - t.getWidth() / 2, _
            c.getWidth() - t.getWidth()))
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
                    Chart.Transparent) Then

                    d.circle(xCoor, yCoor, 4, 4, color, color)

                    Dim label As String = "<*font,bgColor=" & Hex(color) & "*> " & c.formatValue( _
                        dataSet.getValue(xIndex), "{value|P4}") & " <*/font*>"
                    t = d.text(label, "Arial Bold", 10)

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

    '
    ' Updates the chartUpdateTimer interval if the user selects another interval.
    '
    Private Sub samplePeriod_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles samplePeriod.ValueChanged

        chartUpdateTimer.Interval = CInt(samplePeriod.Value)

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
    ' Draw track cursor when mouse is moving over plotarea
    '
    Private Sub winChartViewer1_MouseMovePlotArea(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.MouseEventArgs) Handles winChartViewer1.MouseMovePlotArea

        Dim viewer As WinChartViewer = sender
        trackLineLabel(viewer.Chart, viewer.PlotAreaMouseX)
        viewer.updateDisplay()

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

End Class