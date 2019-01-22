Imports ChartDirector
Imports System.Collections

Public Class FrmTrackBox

    Private Sub FrmTrackBox_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' The data for the bar chart
        Dim data0() As Double = {100, 125, 245, 147, 67}
        Dim data1() As Double = {85, 156, 179, 211, 123}
        Dim data2() As Double = {97, 87, 56, 267, 157}
        Dim labels() As String = {"Mon", "Tue", "Wed", "Thur", "Fri"}

        ' Create a XYChart object of size 540 x 375 pixels
        Dim c As XYChart = New XYChart(540, 375)

        ' Add a title to the chart using 18pt Times Bold Italic font
        c.addTitle("Average Weekly Network Load", "Times New Roman Bold Italic", 18)

        ' Set the plotarea at (50, 55) and of 440 x 280 pixels in size. Use a vertical gradient color from
        ' light blue (f9f9ff) to blue (6666ff) as background. Set border and grid lines to white (ffffff).
        c.setPlotArea(50, 55, 440, 280, c.linearGradientColor(0, 55, 0, 335, &Hf9f9ff, &H6666ff), -1, _
            &Hffffff, &Hffffff)

        ' Add a legend box at (50, 28) using horizontal layout. Use 10pt Arial Bold as font, with transparent
        ' background.
        c.addLegend(50, 28, False, "Arial Bold", 10).setBackground(Chart.Transparent)

        ' Set the x axis labels
        c.xAxis().setLabels(labels)

        ' Draw the ticks between label positions (instead of at label positions)
        c.xAxis().setTickOffset(0.5)

        ' Set axis label style to 8pt Arial Bold
        c.xAxis().setLabelStyle("Arial Bold", 8)
        c.yAxis().setLabelStyle("Arial Bold", 8)

        ' Set axis line width to 2 pixels
        c.xAxis().setWidth(2)
        c.yAxis().setWidth(2)

        ' Add axis title
        c.yAxis().setTitle("Throughput (MBytes Per Hour)")

        ' Add a multi-bar layer with 3 data sets
        Dim layer As BarLayer = c.addBarLayer2(Chart.Side)
        layer.addDataSet(data0, &Hff0000, "Server #1")
        layer.addDataSet(data1, &H00ff00, "Server #2")
        layer.addDataSet(data2, &Hff8800, "Server #3")

        ' Set bar border to transparent. Use glass lighting effect with light direction from left.
        layer.setBorderColor(Chart.Transparent, Chart.glassEffect(Chart.NormalGlare, Chart.Left))

        ' Configure the bars within a group to touch each others (no gap)
        layer.setBarGap(0.2, Chart.TouchBar)

        ' Assign the chart to the WinChartViewer
        winChartViewer1.Chart = c

    End Sub

    '
    ' Draw track cursor when mouse is moving over plotarea
    '
    Private Sub winChartViewer1_MouseMovePlotArea(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.MouseEventArgs) Handles winChartViewer1.MouseMovePlotArea

        Dim viewer As WinChartViewer = sender
        trackBoxLegend(viewer.Chart, viewer.PlotAreaMouseX, viewer.PlotAreaMouseY)
        viewer.updateDisplay()

    	' Hide the track cursor when the mouse leaves the plot area
    	viewer.removeDynamicLayer("MouseLeavePlotArea")

    End Sub

    '
    ' Draw the track box with legend
    '
    Private Sub trackBoxLegend(c As XYChart, mouseX As Integer, mouseY As Integer)

        ' Clear the current dynamic layer and get the DrawArea object to draw on it.
        Dim d As DrawArea = c.initDynamicLayer()

        ' The plot area object
        Dim plotArea As PlotArea = c.getPlotArea()

        ' Get the data x-value that is nearest to the mouse
        Dim xValue As Double = c.getNearestXValue(mouseX)

        ' Compute the position of the box. This example assumes a label based x-axis, in which the labeling
        ' spacing is one x-axis unit. So the left and right sides of the box is 0.5 unit from the central
        ' x-value.
        Dim boxLeft As Integer = c.getXCoor(xValue - 0.5)
        Dim boxRight As Integer = c.getXCoor(xValue + 0.5)
        Dim boxTop As Integer = plotArea.getTopY()
        Dim boxBottom As Integer = plotArea.getBottomY()

        ' Draw the track box
        d.rect(boxLeft, boxTop, boxRight, boxBottom, &H000000, Chart.Transparent)

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

                ' Build the legend entry, consist of the legend icon, the name and the data value.
                Dim dataValue As Double = dataSet.getValue(xIndex)
                If (dataValue <> Chart.NoValue) And (dataSet.getDataColor() <> Chart.Transparent) Then
                    legendEntries.Add(dataSet.getLegendIcon() & " " & dataSet.getDataName() & ": " & _
                        c.formatValue(dataValue, "{value|P4}"))
                End If
            Next
        Next

        ' Create the legend by joining the legend entries
        If legendEntries.Count > 0 Then
            legendEntries.Reverse()
            Dim legend As String = "<*block,bgColor=FFFFCC,edgeColor=000000,margin=5*><*font,underline=1*>" _
                 & c.xAxis().getFormattedLabel(xValue) & "<*/font*><*br*>" & Join(CType( _
                legendEntries.ToArray(GetType(String)), String()), "<*br*>") & "<*/*>"

            ' Display the legend at the bottom-right side of the mouse cursor, and make sure the legend will
            ' not go outside the chart image.
            Dim t As TTFText = d.text(legend, "Arial Bold", 8)
            t.draw(Math.Min(mouseX + 12, c.getWidth() - t.getWidth()), Math.Min(mouseY + 18, c.getHeight() - _
                t.getHeight()), &H000000, Chart.TopLeft)
        End If

    End Sub

End Class
