Imports ChartDirector

Public Class FrmRealTimeDemo

    ' The data arrays that store the visible data. The data arrays are updated in realtime. In
    ' this demo, we plot the last 240 samples.
    Private Const sampleSize As Integer = 240
    Private dataSeriesA(sampleSize - 1) As Double
    Private dataSeriesB(sampleSize - 1) As Double
    Private dataSeriesC(sampleSize - 1) As Double
    Private timeStamps(sampleSize - 1) As Date

    ' In this demo, we use a data generator driven by a timer to generate realtime data. The
    ' nextDataTime is an internal variable used by the data generator to keep track of which
    ' values to generate next.
    Private nextDataTime As DateTime = DateTime.Now

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
            shiftData(dataSeriesA, dataA)
            shiftData(dataSeriesB, dataB)
            shiftData(dataSeriesC, dataC)
            shiftData(timeStamps, nextDataTime)

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
        Dim c As XYChart = New XYChart(600, 270, &HF4F4F4, &H000000, 1)
        c.setRoundedFrame(Chart.CColor(BackColor))

        ' Set the plotarea at (55, 62) and of size 520 x 175 pixels. Use white (ffffff) 
        ' background. Enable both horizontal and vertical grids by setting their colors to 
        ' grey (cccccc). Set clipping mode to clip the data lines to the plot area.
        c.setPlotArea(55, 62, 520, 175, &HFFFFFF, -1, -1, &HCCCCCC, &HCCCCCC)
        c.setClipping()

        ' Add a title to the chart using 15 pts Times New Roman Bold Italic font, with a light
        ' grey (dddddd) background, black (000000) border, and a glass like raised effect.
        c.addTitle("Field Intensity at Observation Satellite", "Times New Roman Bold Italic", 15 _
            ).setBackground(&HDDDDDD, &H0, Chart.glassEffect())

        ' Add a legend box at the top of the plot area with 9pts Arial Bold font. We set the 
        ' legend box to the same width as the plot area and use grid layout (as opposed to 
        ' flow or top/down layout). This distributes the 3 legend icons evenly on top of the 
        ' plot area.
        Dim b As LegendBox = c.addLegend2(55, 33, 3, "Arial Bold", 9)
        b.setBackground(Chart.Transparent, Chart.Transparent)
        b.setWidth(520)

        ' Configure the y-axis with a 10pts Arial Bold axis title
        c.yAxis().setTitle("Intensity (V/m)", "Arial Bold", 10)

        ' Configure the x-axis to auto-scale with at least 75 pixels between major tick and 15 
        ' pixels between minor ticks. This shows more minor grid lines on the chart.
        c.xAxis().setTickDensity(75, 15)

        ' Set the axes width to 2 pixels
        c.xAxis().setWidth(2)
        c.yAxis().setWidth(2)

        ' Now we add the data to the chart
        Dim lastTime As DateTime = timeStamps(timeStamps.Length - 1)
        If lastTime <> DateTime.MinValue Then
            ' Set up the x-axis scale. In this demo, we set the x-axis to show the last 240 
            ' samples, with 250ms per sample.
            c.xAxis().setDateScale(lastTime.AddSeconds( _
                -dataRateTimer.Interval * timeStamps.Length / 1000), lastTime)

            ' Set the x-axis label format
            c.xAxis().setLabelFormat("{value|hh:nn:ss}")

            ' Create a line layer to plot the lines
            Dim layer As LineLayer = c.addLineLayer2()

            ' The x-coordinates are the timeStamps.
            layer.setXData(timeStamps)

            ' The 3 data series are used to draw 3 lines. Here we put the latest data values
            ' as part of the data set name, so you can see them updated in the legend box.
            layer.addDataSet(dataSeriesA, &HFF0000, "Alpha: <*bgColor=FFCCCC*>" & _
                c.formatValue(dataSeriesA(dataSeriesA.Length - 1), " {value|2} "))
            layer.addDataSet(dataSeriesB, &H00CC00, "Beta: <*bgColor=CCFFCC*>" & _
                c.formatValue(dataSeriesB(dataSeriesB.Length - 1), " {value|2} "))
            layer.addDataSet(dataSeriesC, &H0000FF, "Gamma: <*bgColor=CCCCFF*>" & _
                c.formatValue(dataSeriesC(dataSeriesC.Length - 1), " {value|2} "))
        End If

        ' Assign the chart to the WinChartViewer
        viewer.Chart = c

    End Sub

End Class