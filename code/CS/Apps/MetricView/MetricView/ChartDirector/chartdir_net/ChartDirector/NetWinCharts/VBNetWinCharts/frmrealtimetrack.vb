Imports ChartDirector

Public Class FrmRealTimeTrack

    ' The data arrays that store the visible data. The data arrays are updated in realtime. In
    ' this demo, we plot the last 240 samples.
    Private Const sampleSize As Integer = 240
    Private dataSeriesA(sampleSize - 1) As Double
    Private dataSeriesB(sampleSize - 1) As Double
    Private dataSeriesC(sampleSize - 1) As Double
    Private timeStamps(sampleSize - 1) As Date

    ' The index of the array position to which new data values are added.
    Private currentIndex As Integer = 0

    ' In this demo, we use a data generator driven by a timer to generate realtime data. The
    ' nextDataTime is an internal variable used by the data generator to keep track of which
    ' values to generate next.
    Private nextDataTime As DateTime = New DateTime((Now.Ticks \ 10000000) * 10000000)

    Private Sub FrmZoomScrollTrack_Load(ByVal sender As Object, ByVal e As EventArgs) _
        Handles MyBase.Load

        ' Data generation rate
        dataRateTimer.Interval = 250

        ' Chart update rate
        chartUpdateTimer.Interval = CInt(samplePeriod.Value)

        ' Initialize data buffer. In this demo, we just set the initial state to no data.
        Dim i As Integer
        For i = 0 To UBound(timeStamps)
            timeStamps(i) = DateTime.MinValue
        Next

        ' Enable RunPB button
        runPB.Checked = True

        ' Now can start the timers for data collection and chart update
        dataRateTimer.Start()
        chartUpdateTimer.Start()

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

            ' We provide some visual feedback to the numbers generated, so you can see the
            ' values being generated.
            valueA.Text = dataA.ToString(".##")
            valueB.Text = dataB.ToString(".##")
            valueC.Text = dataC.ToString(".##")

            ' After obtaining the new values, we need to update the data arrays.
            If currentIndex < timeStamps.Length Then
                ' Store the new values in the current index position, and increment the index.
                dataSeriesA(currentIndex) = dataA
                dataSeriesB(currentIndex) = dataB
                dataSeriesC(currentIndex) = dataC
                timeStamps(currentIndex) = nextDataTime
                currentIndex += 1
            Else
                ' The data arrays are full. Shift the arrays and store the values at the end.
                shiftData(dataSeriesA, dataA)
                shiftData(dataSeriesB, dataB)
                shiftData(dataSeriesC, dataC)
                shiftData(timeStamps, nextDataTime)
            End If

            ' Update nextDataTime. This is needed by our data generator. In real applications,
            ' you may not need this variable or the associated do/while loop.
            nextDataTime = nextDataTime.AddMilliseconds(dataRateTimer.Interval)
        Loop

    End Sub

    '
    ' Utility to shift a value into an array
    '
    Private Sub shiftData(ByVal data As Object, ByVal newValue As Object)

        Dim i As Integer
        For i = 1 To UBound(data)
            data(i - 1) = data(i)
        Next
        data(UBound(data)) = newValue

    End Sub

    '
    ' Enable/disable chart update based on the state of the Run button.
    '
    Private Sub runPB_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles runPB.CheckedChanged

        chartUpdateTimer.Enabled = runPB.Checked

    End Sub

    '
    ' Updates the chartUpdateTimer interval if the user selects another interval.
    '
    Private Sub samplePeriod_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles samplePeriod.ValueChanged

        chartUpdateTimer.Interval = CInt(samplePeriod.Value)

    End Sub

    '
    ' The chartUpdateTimer Tick event - this updates the chart periodicially by raising
    ' viewPortChanged events.
    '
    Private Sub chartUpdateTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles chartUpdateTimer.Tick

        winChartViewer1.updateViewPort(True, False)

    End Sub

    '
    ' The viewPortChanged event handler. In this example, it just updates the chart. If you
    ' have other controls to update, you may also put the update code here.
    '
    Private Sub winChartViewer1_ViewPortChanged(ByVal sender As Object, ByVal e As WinViewPortEventArgs) _
        Handles winChartViewer1.ViewPortChanged

        drawChart(winChartViewer1)

    End Sub

    '
    ' Draw the chart and display it in the given viewer.
    '
    Private Sub drawChart(ByVal viewer As WinChartViewer)

        ' Create an XYChart object 600 x 270 pixels in size, with light grey (f4f4f4) 
        ' background, black (000000) border, 1 pixel raised effect, and with a rounded frame.
        Dim c As XYChart = New XYChart(600, 270, &HF4F4F4, &H0, 1)
        c.setRoundedFrame(Chart.CColor(BackColor))

        ' Set the plotarea at (55, 55) and of size 520 x 185 pixels. Use white (ffffff) 
        ' background. Enable both horizontal and vertical grids by setting their colors to 
        ' grey (cccccc). Set clipping mode to clip the data lines to the plot area.
        c.setPlotArea(55, 55, 520, 185, &HFFFFFF, -1, -1, &HCCCCCC, &HCCCCCC)
        c.setClipping()

        ' Add a title to the chart using 15 pts Times New Roman Bold Italic font, with a light
        ' grey (dddddd) background, black (000000) border, and a glass like raised effect.
        c.addTitle("Field Intensity at Observation Satellite", "Times New Roman Bold Italic", 15 _
            ).setBackground(&HDDDDDD, &H0, Chart.glassEffect())

        ' Set the reference font size of the legend box
        c.getLegend().setFontSize(8)

        ' Configure the y-axis with a 10pts Arial Bold axis title
        c.yAxis().setTitle("Intensity (V/m)", "Arial Bold", 10)

        ' Configure the x-axis to auto-scale with at least 75 pixels between major tick and 15 
        ' pixels between minor ticks. This shows more minor grid lines on the chart.
        c.xAxis().setTickDensity(75, 15)

        ' Set the axes width to 2 pixels
        c.xAxis().setWidth(2)
        c.yAxis().setWidth(2)

        ' Now we add the data to the chart
        Dim firstTime As DateTime = timeStamps(0)
        If firstTime <> DateTime.MinValue Then
            ' Set up the x-axis scale. In this demo, we set the x-axis to show the 240 samples,
            ' with 250ms per sample.
            c.xAxis().setDateScale(firstTime, firstTime.AddSeconds( _
                dataRateTimer.Interval * timeStamps.Length / 1000))

            ' Set the x-axis label format
            c.xAxis().setLabelFormat("{value|hh:nn:ss}")

            ' Create a line layer to plot the lines
            Dim layer As LineLayer = c.addLineLayer2()

            ' The x-coordinates are the timeStamps.
            layer.setXData(timeStamps)

            ' The 3 data series are used to draw 3 lines.
            layer.addDataSet(dataSeriesA, &HFF0000, "Alpha")
            layer.addDataSet(dataSeriesB, &H00CC00, "Beta")
            layer.addDataSet(dataSeriesC, &H0000FF, "Gamma")
        End If

        ' Include track line with legend. If the mouse is on the plot area, show the track 
        ' line with legend at the mouse position; otherwise, show them for the latest data
        ' values (that is, at the rightmost position).
        trackLineLegend(c, IIf(viewer.IsMouseOnPlotArea, viewer.PlotAreaMouseX, _
            c.getPlotArea().getRightX()))

        ' Assign the chart to the WinChartViewer
        viewer.Chart = c

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
        d.vline(plotArea.getTopY(), plotArea.getBottomY(), xCoor, d.dashLineColor(&H0, &H101))

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
            "*><*block*><*font=Arial Bold*>[" & c.xAxis().getFormattedLabel(xValue, "hh:nn:ss.ff") & _
            "]<*/*>        " & Join(CType(legendEntries.ToArray(GetType(String)), String()), "        ") & _
            "<*/*>"

        ' Display the legend on the top of the plot area
        Dim t As TTFText = d.text(legendText, "Arial", 8)
        t.draw(plotArea.getLeftX() + 5, plotArea.getTopY() - 3, &H0, Chart.BottomLeft)

    End Sub

End Class