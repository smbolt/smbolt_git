Imports ChartDirector

Public Class FrmXYZoomScroll

    ' Data arrays
    Dim dataX0 As Double()
    Dim dataY0 As Double()
    Dim dataX1 As Double()
    Dim dataY1 As Double()
    Dim dataX2 As Double()
    Dim dataY2 As Double()

    Private Sub FrmXYZoomScroll_Load(ByVal sender As Object, ByVal e As EventArgs) _
        Handles MyBase.Load

        ' Load the data
        loadData()

        ' Trigger the ViewPortChanged event to draw the chart
        winChartViewer1.updateViewPort(True, True)

        ' Draw the full thumbnail chart for the ViewPortControl
        drawFullChart(viewPortControl1, winChartViewer1)

    End Sub

    '
    ' Load the data
    '
    Private Sub loadData()

        '
        ' For simplicity, in this demo, we just use hard coded data.
        '
        dataX0 = New Double() {10, 15, 6, -12, 14, -8, 13, -3, 16, 12, 10.5, -7, 3, -10, -5, 2, 5}
        dataY0 = New Double() {130, 150, 80, 110, -110, -105, -130, -15, -170, 125, 125, 60, 25, 150, _
            150, 15, 120}
        dataX1 = New Double() {6, 7, -4, 3.5, 7, 8, -9, -10, -12, 11, 8, -3, -2, 8, 4, -15, 15}
        dataY1 = New Double() {65, -40, -40, 45, -70, -80, 80, 10, -100, 105, 60, 50, 20, 170, -25, 50, 75}
        dataX2 = New Double() {-10, -12, 11, 8, 6, 12, -4, 3.5, 7, 8, -9, 3, -13, 16, -7.5, -10, -15}
        dataY2 = New Double() {65, -80, -40, 45, -70, -80, 80, 90, -100, 105, 60, -75, -150, -40, 120, _
            -50, -30}

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

        ' Update the image map if necessary
        If e.NeedUpdateImageMap Then
            updateImageMap(winChartViewer1)
        End If

    End Sub

    '
    ' Update controls when the view port changed
    '
    Private Sub updateControls(ByVal viewer As WinChartViewer)

        ' Synchronize the zoom bar value with the view port width/height
        zoomBar.Value = Math.Round(Math.Min(viewer.ViewPortWidth, viewer.ViewPortHeight) * zoomBar.Maximum)

    End Sub

    '
    ' Draw the chart.
    '
    Private Sub drawChart(ByVal viewer As WinChartViewer)

        ' Create an XYChart object 500 x 480 pixels in size, with the same background color
        ' as the container
        Dim c As XYChart = New XYChart(500, 480, Chart.CColor(BackColor))

        ' Set the plotarea at (50, 40) and of size 400 x 400 pixels. Use light grey (c0c0c0)
        ' horizontal and vertical grid lines. Set 4 quadrant coloring, where the colors of 
        ' the quadrants alternate between lighter and deeper grey (dddddd/eeeeee). 
        c.setPlotArea(50, 40, 400, 400, -1, -1, -1, &HC0C0C0, &HC0C0C0 _
            ).set4QBgColor(&HDDDDDD, &HEEEEEE, &HDDDDDD, &HEEEEEE, &H0)

        ' Enable clipping mode to clip the part of the data that is outside the plot area.
        c.setClipping()

        ' Set 4 quadrant mode, with both x and y axes symetrical around the origin
        c.setAxisAtOrigin(Chart.XYAxisAtOrigin, Chart.XAxisSymmetric + Chart.YAxisSymmetric)

        ' Add a legend box at (450, 40) (top right corner of the chart) with vertical layout
        ' and 8 pts Arial Bold font. Set the background color to semi-transparent grey.
        Dim b As LegendBox = c.addLegend(450, 40, True, "Arial Bold", 8)
        b.setAlignment(Chart.TopRight)
        b.setBackground(&H40DDDDDD)

        ' Add a titles to axes
        c.xAxis().setTitle("Alpha Index")
        c.yAxis().setTitle("Beta Index")

        ' Set axes width to 2 pixels
        c.xAxis().setWidth(2)
        c.yAxis().setWidth(2)

        ' The default ChartDirector settings has a denser y-axis grid spacing and less-dense
        ' x-axis grid spacing. In this demo, we want the tick spacing to be symmetrical.
        ' We use around 50 pixels between major ticks and 25 pixels between minor ticks.
        c.xAxis().setTickDensity(50, 25)
        c.yAxis().setTickDensity(50, 25)

        '
        ' In this example, we represent the data by scatter points. If you want to represent
        ' the data by somethings else (lines, bars, areas, floating boxes, etc), just modify
        ' the code below to use the layer type of your choice. 
        '

        ' Add scatter layer, using 11 pixels red (ff33333) X shape symbols
        c.addScatterLayer(dataX0, dataY0, "Group A", Chart.Cross2Shape(), 11, &HFF3333)

        ' Add scatter layer, using 11 pixels green (33ff33) circle symbols
        c.addScatterLayer(dataX1, dataY1, "Group B", Chart.CircleShape, 11, &H33FF33)

        ' Add scatter layer, using 11 pixels blue (3333ff) triangle symbols
        c.addScatterLayer(dataX2, dataY2, "Group C", Chart.TriangleSymbol, 11, &H3333FF)

        '
        ' In this example, we have not explicitly configured the full x and y range. In this case, the
        ' first time syncLinearAxisWithViewPort is called, ChartDirector will auto-scale the axis and
        ' assume the resulting range is the full range. In subsequent calls, ChartDirector will set the
        ' axis range based on the view port and the full range.
        '
        viewer.SyncLinearAxisWithViewPort("x", c.xAxis())
        viewer.SyncLinearAxisWithViewPort("y", c.yAxis())

        ' We need to update the track line too. If the mouse is moving on the chart (eg. if 
        ' the user drags the mouse on the chart to scroll it), the track line will be updated
        ' in the MouseMovePlotArea event. Otherwise, we need to update the track line here.
        If (Not viewer.IsInMouseMoveEvent) And viewer.IsMouseOnPlotArea Then
            crossHair(c, viewer.PlotAreaMouseX, viewer.PlotAreaMouseY)
        End If

        ' Set the chart image to the WinChartViewer
        viewer.Chart = c

    End Sub

    '
    ' Draw the full thumbnail chart and display it in the given ViewPortControl
    '
    Private Sub drawFullChart(ByVal vpc As WinViewPortControl, ByVal viewer As WinChartViewer)

        ' Create an XYChart object of the same size as the Viewport Control
        Dim c As XYChart = New XYChart(viewPortControl1.ClientSize.Width, viewPortControl1.ClientSize.Height)

        ' Set the plotarea to cover the entire chart. Disable grid lines by setting their colors
        ' to transparent. Set 4 quadrant coloring, where the colors of the quadrants alternate 
        ' between lighter and deeper grey (dddddd/eeeeee). 
        c.setPlotArea(0, 0, c.getWidth() - 1, c.getHeight() - 1, -1, -1, &HFF0000, Chart.Transparent, _
            Chart.Transparent).set4QBgColor(&HDDDDDD, &HEEEEEE, &HDDDDDD, &HEEEEEE, &H0)

        ' Set 4 quadrant mode, with both x and y axes symetrical around the origin
        c.setAxisAtOrigin(Chart.XYAxisAtOrigin, Chart.XAxisSymmetric + Chart.YAxisSymmetric)

        ' The x and y axis scales reflect the full range of the view port
        c.xAxis().setLinearScale(viewer.getValueAtViewPort("x", 0), viewer.getValueAtViewPort("x", 1), _
            Chart.NoValue)
        c.yAxis().setLinearScale(viewer.getValueAtViewPort("y", 0), viewer.getValueAtViewPort("y", 1), _
            Chart.NoValue)

        ' Add scatter layer, using 3 pixels red (ff33333) X shape symbols
        c.addScatterLayer(dataX0, dataY0, "Group A", Chart.Cross2Shape(), 3, &HFF3333, &HFF3333)

        ' Add scatter layer, using 3 pixels green (33ff33) circle symbols
        c.addScatterLayer(dataX1, dataY1, "Group B", Chart.CircleShape, 3, &H33FF33, &H33FF33)

        ' Add scatter layer, using 3 pixels blue (3333ff) triangle symbols
        c.addScatterLayer(dataX2, dataY2, "Group C", Chart.TriangleSymbol, 3, &H3333FF, &H3333FF)

        ' Set the chart image to the WinChartViewer
        vpc.Chart = c

    End Sub

    '
    ' Update the image map
    '
    Private Sub updateImageMap(ByVal viewer As WinChartViewer)

        ' Include tool tip for the chart
        If IsNothing(viewer.ImageMap) Then
            viewer.ImageMap = viewer.Chart.getHTMLImageMap("clickable", "", _
                "title='[{dataSetName}] Alpha = {x}, Beta = {value}'")
        End If

    End Sub

    '
    ' ClickHotSpot event handler. In this demo, we just display the hot spot parameters in a pop up 
    ' dialog.
    '
    Private Sub winChartViewer1_ClickHotSpot(ByVal sender As Object, ByVal e As WinHotSpotEventArgs) _
        Handles winChartViewer1.ClickHotSpot

        ' We show the pop up dialog only when the mouse action is not in zoom-in or zoom-out mode.
        If winChartViewer1.MouseUsage <> WinChartMouseUsage.ZoomIn And _
            winChartViewer1.MouseUsage <> WinChartMouseUsage.ZoomOut Then
            Dim f As New ParamViewer()
            f.Display(sender, e)
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
    ' ValueChanged event handler for zoomBar. Zoom in around the center point and try to 
    ' maintain the aspect ratio
    '
    Private Sub zoomBar_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles zoomBar.ValueChanged

        If Not winChartViewer1.IsInViewPortChangedEvent Then
            'Remember the center point
            Dim centerX As Double = winChartViewer1.ViewPortLeft + winChartViewer1.ViewPortWidth / 2
            Dim centerY As Double = winChartViewer1.ViewPortTop + winChartViewer1.ViewPortHeight / 2

            'Aspect ratio and zoom factor
            Dim aspectRatio As Double = winChartViewer1.ViewPortWidth / winChartViewer1.ViewPortHeight
            Dim zoomTo As Double = CDbl(zoomBar.Value) / zoomBar.Maximum

            'Zoom while respecting the aspect ratio
            winChartViewer1.ViewPortWidth = zoomTo * Math.Max(1, aspectRatio)
            winChartViewer1.ViewPortHeight = zoomTo * Math.Max(1, 1 / aspectRatio)

            'Adjust ViewPortLeft and ViewPortTop to keep center point unchanged
            winChartViewer1.ViewPortLeft = centerX - winChartViewer1.ViewPortWidth / 2
            winChartViewer1.ViewPortTop = centerY - winChartViewer1.ViewPortHeight / 2

            'update the chart, but no need to update the image map yet, as the chart is still 
            'zooming and is unstable
            winChartViewer1.updateViewPort(True, False)
        End If

    End Sub

    '
    ' Draw track cursor when mouse is moving over plotarea
    '
    Private Sub winChartViewer1_MouseMovePlotArea(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.MouseEventArgs) Handles winChartViewer1.MouseMovePlotArea

        Dim viewer As WinChartViewer = sender
        crossHair(viewer.Chart, viewer.PlotAreaMouseX, viewer.PlotAreaMouseY)
        viewer.updateDisplay()

        ' Hide the track cursor when the mouse leaves the plot area
        viewer.removeDynamicLayer("MouseLeavePlotArea")

        ' Update image map if necessary. If the mouse is still dragging, the chart is still 
        ' updating and not confirmed, so there is no need to set up the image map.
        If Not viewer.IsMouseDragging Then
            updateImageMap(viewer)
        End If

    End Sub

    '
    ' Draw cross hair cursor with axis labels
    '
    Private Sub crossHair(ByVal c As XYChart, ByVal mouseX As Integer, ByVal mouseY As Integer)

        ' Clear the current dynamic layer and get the DrawArea object to draw on it.
        Dim d As DrawArea = c.initDynamicLayer()

        ' The plot area object
        Dim plotArea As PlotArea = c.getPlotArea()

        ' Draw a vertical line and a horizontal line as the cross hair
        d.vline(plotArea.getTopY(), plotArea.getBottomY(), mouseX, d.dashLineColor(&H0, &H101))
        d.hline(plotArea.getLeftX(), plotArea.getRightX(), mouseY, d.dashLineColor(&H0, &H101))

        ' Draw y-axis label
        Dim label As String = "<*block,bgColor=FFFFDD,margin=3,edgeColor=000000*>" & c.formatValue( _
            c.getYValue(mouseY, c.yAxis()), "{value|P4}") & "<*/*>"
        Dim t As TTFText = d.text(label, "Arial Bold", 8)
        t.draw(plotArea.getLeftX() - 5, mouseY, &H0, Chart.Right)

        ' Draw x-axis label
        label = "<*block,bgColor=FFFFDD,margin=3,edgeColor=000000*>" & c.formatValue(c.getXValue(mouseX), _
            "{value|P4}") & "<*/*>"
        t = d.text(label, "Arial Bold", 8)
        t.draw(mouseX, plotArea.getBottomY() + 5, &H0, Chart.Top)

    End Sub

End Class