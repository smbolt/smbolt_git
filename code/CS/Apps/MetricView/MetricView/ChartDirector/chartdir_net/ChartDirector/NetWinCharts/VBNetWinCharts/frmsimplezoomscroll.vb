Imports ChartDirector

Public Class FrmSimpleZoomScroll

    ' Data arrays
    Dim timeStamps As DateTime()
    Dim dataSeriesA As Double()
    Dim dataSeriesB As Double()
    Dim dataSeriesC As Double()

    Private Sub FrmSimpleZoomScroll_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        ' Load the data
        loadData()

        ' Initialize the WinChartViewer
        initChartViewer(winChartViewer1)

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
        viewer.setFullRange("x", timeStamps(0), timeStamps(timeStamps.Length - 1))

        ' Initialize the view port to show the latest 20% of the time range
        viewer.ViewPortWidth = 0.2
        viewer.ViewPortLeft = 1 - viewer.ViewPortWidth

        ' Set the maximum zoom to 10 points
        viewer.ZoomInWidthLimit = 10.0 / timeStamps.Length

        '/ Initially set the mouse usage to "Pointer" mode (Drag to Scroll mode)
        pointerPB.Checked = True

    End Sub

    '
    ' The ViewPortChanged event handler. This event occurs if the user scrolls or zooms in
    ' or out the chart by dragging or clicking on the chart. It can also be triggered by
    ' calling WinChartViewer.updateViewPort.
    '
    Private Sub winChartViewer1_ViewPortChanged(ByVal sender As Object, ByVal e As WinViewPortEventArgs) _
        Handles winChartViewer1.ViewPortChanged

        If e.NeedUpdateChart Then
            drawChart(winChartViewer1)
        End If
        If e.NeedUpdateImageMap Then
            updateImageMap(winChartViewer1)
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

        ' Create an XYChart object 600 x 300 pixels in size, with pale blue (f0f0ff) 
        ' background, black (000000) border, 1 pixel raised effect, and with a rounded frame.
        Dim c As XYChart = New XYChart(600, 300, &HF0F0FF, 0, 1)
        c.setRoundedFrame(Chart.CColor(BackColor))

        ' Set the plotarea at (52, 60) and of size 520 x 205 pixels. Use white (ffffff) background.
        ' Enable both horizontal and vertical grids by setting their colors to grey (cccccc). Set
        ' clipping mode to clip the data lines to the plot area.
        c.setPlotArea(52, 60, 520, 205, &HFFFFFF, -1, -1, &HCCCCCC, &HCCCCCC)
        ' As the data can lie outside the plotarea in a zoomed chart, we need to enable clipping.
        c.setClipping()

        ' Add a top title to the chart using 15 pts Times New Roman Bold Italic font, with a light blue
        ' (ccccff) background, black (000000) border, and a glass like raised effect.
        c.addTitle("Simple Zooming and Scrolling", "Times New Roman Bold Italic", 15 _
                ).setBackground(&HCCCCFF, &H0, Chart.glassEffect())

        ' Add a legend box at the top of the plot area with 9pts Arial Bold font with flow layout.
        c.addLegend(50, 33, False, "Arial Bold", 9).setBackground(Chart.Transparent, Chart.Transparent)

        ' Set axes width to 2 pixels
        c.yAxis().setWidth(2)
        c.xAxis().setWidth(2)

        ' Add a title to the y-axis
        c.yAxis().setTitle("Price (USD)", "Arial Bold", 9)

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

        ' Now we add the 3 data series to a line layer, using the color red (ff0000), green (00cc00)
        ' and blue (0000ff)
        layer.setXData(viewPortTimeStamps)
        layer.addDataSet(viewPortDataSeriesA, &Hff0000, "Product Alpha")
        layer.addDataSet(viewPortDataSeriesB, &H00cc00, "Product Beta")
        layer.addDataSet(viewPortDataSeriesC, &H0000ff, "Product Gamma")

        '================================================================================
        ' Configure axis scale and labelling
        '================================================================================

        ' Set the x-axis as a date/time axis with the scale according to the view port x range.
        viewer.syncDateAxisWithViewPort("x", c.xAxis())

        ' In this demo, we rely on ChartDirector to auto-label the axis. We ask ChartDirector to ensure
        ' the x-axis labels are at least 75 pixels apart to avoid too many labels.
        c.xAxis().setTickDensity(75)

        '================================================================================
        ' Output the chart
        '================================================================================

        viewer.Chart = c

    End Sub

    '
    ' Update the image map
    '
    Private Sub updateImageMap(ByVal viewer As WinChartViewer)

        ' Include tool tip for the chart
        If IsNothing(winChartViewer1.ImageMap) Then
            winChartViewer1.ImageMap = winChartViewer1.Chart.getHTMLImageMap("", "", _
                "title='[{dataSetName}] {x|mmm dd, yyyy}: USD {value|2}'")
        End If

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

End Class