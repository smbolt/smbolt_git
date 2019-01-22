Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class hbar
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Borderless Bar Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the bar chart
        Dim data() As Double = {3.9, 8.1, 10.9, 14.2, 18.1, 19.0, 21.2, 23.2, 25.7, 36}

        ' The labels for the bar chart
        Dim labels() As String = {"Bastic Group", "Simpa", "YG Super", "CID", "Giga Tech", _
            "Indo Digital", "Supreme", "Electech", "THP Thunder", "Flash Light"}

        ' Create a XYChart object of size 600 x 250 pixels
        Dim c As XYChart = New XYChart(600, 250)

        ' Add a title to the chart using Arial Bold Italic font
        c.addTitle("Revenue Estimation - Year 2002", "Arial Bold Italic")

        ' Set the plotarea at (100, 30) and of size 400 x 200 pixels. Set the plotarea border,
        ' background and grid lines to Transparent
        c.setPlotArea(100, 30, 400, 200, Chart.Transparent, Chart.Transparent, Chart.Transparent, _
            Chart.Transparent, Chart.Transparent)

        ' Add a bar chart layer using the given data. Use a gradient color for the bars, where the
        ' gradient is from dark green (0x008000) to white (0xffffff)
        Dim layer As BarLayer = c.addBarLayer(data, c.gradientColor(100, 0, 500, 0, &H008000, _
            &Hffffff))

        ' Swap the axis so that the bars are drawn horizontally
        c.swapXY(True)

        ' Set the bar gap to 10%
        layer.setBarGap(0.1)

        ' Use the format "US$ xxx millions" as the bar label
        layer.setAggregateLabelFormat("US$ {value} millions")

        ' Set the bar label font to 10pt Times Bold Italic/dark red (0x663300)
        layer.setAggregateLabelStyle("Times New Roman Bold Italic", 10, &H663300)

        ' Set the labels on the x axis
        Dim textbox As ChartDirector.TextBox = c.xAxis().setLabels(labels)

        ' Set the x axis label font to 10pt Arial Bold Italic
        textbox.setFontStyle("Arial Bold Italic")
        textbox.setFontSize(10)

        ' Set the x axis to Transparent, with labels in dark red (0x663300)
        c.xAxis().setColors(Chart.Transparent, &H663300)

        ' Set the y axis and labels to Transparent
        c.yAxis().setColors(Chart.Transparent, Chart.Transparent)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{xLabel}: US${value} millions'")

    End Sub

End Class

