Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class polarbubble
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Polar Bubble Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the chart
        Dim data0() As Double = {6, 12.5, 18.2, 15}
        Dim angles0() As Double = {45, 96, 169, 258}
        Dim size0() As Double = {41, 105, 12, 20}

        Dim data1() As Double = {18, 16, 11, 14}
        Dim angles1() As Double = {30, 210, 240, 310}
        Dim size1() As Double = {30, 45, 12, 90}

        ' Create a PolarChart object of size 460 x 460 pixels
        Dim c As PolarChart = New PolarChart(460, 460)

        ' Add a title to the chart at the top left corner using 15pt Arial Bold Italic font
        c.addTitle2(Chart.TopLeft, "<*underline=2*>EM Field Strength", "Arial Bold Italic", 15)

        ' Set center of plot area at (230, 240) with radius 180 pixels
        c.setPlotArea(230, 240, 180)

        ' Use alternative light grey/dark grey circular background color
        c.setPlotAreaBg(&Hdddddd, &Heeeeee)

        ' Set the grid style to circular grid
        c.setGridStyle(False)

        ' Add a legend box at the top right corner of the chart using 9pt Arial Bold font
        c.addLegend(459, 0, True, "Arial Bold", 9).setAlignment(Chart.TopRight)

        ' Set angular axis as 0 - 360, with a spoke every 30 units
        c.angularAxis().setLinearScale(0, 360, 30)

        ' Set the radial axis label format
        c.radialAxis().setLabelFormat("{value} km")

        ' Add a blue (0x9999ff) line layer to the chart using (data0, angle0)
        Dim layer0 As PolarLineLayer = c.addLineLayer(data0, &H9999ff, "Cold Spot")
        layer0.setAngles(angles0)

        ' Disable the line by setting its width to 0, so only the symbols are visible
        layer0.setLineWidth(0)

        ' Use a circular data point symbol
        layer0.setDataSymbol(Chart.CircleSymbol, 11)

        ' Modulate the symbol size by size0 to produce a bubble chart effect
        layer0.setSymbolScale(size0)

        ' Add a red (0xff9999) line layer to the chart using (data1, angle1)
        Dim layer1 As PolarLineLayer = c.addLineLayer(data1, &Hff9999, "Hot Spot")
        layer1.setAngles(angles1)

        ' Disable the line by setting its width to 0, so only the symbols are visible
        layer1.setLineWidth(0)

        ' Use a circular data point symbol
        layer1.setDataSymbol(Chart.CircleSymbol, 11)

        ' Modulate the symbol size by size1 to produce a bubble chart effect
        layer1.setSymbolScale(size1)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{dataSetName} at ({value} km, {angle} deg)" & vbLf & "Strength = {z} Watt'")

    End Sub

End Class

