Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class posnegbar
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Positive Negative Bars"
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
        Dim data() As Double = {-6.3, 2.3, 0.7, -3.4, 2.2, -2.9, -0.1, -0.1, 3.3, 6.2, 4.3, 1.6}

        ' The labels for the bar chart
        Dim labels() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", _
            "Oct", "Nov", "Dec"}

        ' Create a XYChart object of size 500 x 320 pixels
        Dim c As XYChart = New XYChart(500, 320)

        ' Add a title to the chart using Arial Bold Italic font
        c.addTitle("Productivity Change - Year 2005", "Arial Bold Italic")

        ' Set the plotarea at (50, 30) and of size 400 x 250 pixels
        c.setPlotArea(50, 30, 400, 250)

        ' Add a bar layer to the chart using the Overlay data combine method
        Dim layer As BarLayer = c.addBarLayer2(Chart.Overlay)

        ' Select positive data and add it as data set with blue (6666ff) color
        layer.addDataSet(New ArrayMath(data).selectGEZ(Nothing, Chart.NoValue).result(), &H6666ff)

        ' Select negative data and add it as data set with orange (ff6600) color
        layer.addDataSet(New ArrayMath(data).selectLTZ(Nothing, Chart.NoValue).result(), &Hff6600)

        ' Add labels to the top of the bar using 8 pt Arial Bold font. The font color is configured
        ' to be red (0xcc3300) below zero, and blue (0x3333ff) above zero.
        layer.setAggregateLabelStyle("Arial Bold", 8, layer.yZoneColor(0, &Hcc3300, &H3333ff))

        ' Set the labels on the x axis and use Arial Bold as the label font
        c.xAxis().setLabels(labels).setFontStyle("Arial Bold")

        ' Draw the y axis on the right of the plot area
        c.setYAxisOnRight(True)

        ' Use Arial Bold as the y axis label font
        c.yAxis().setLabelStyle("Arial Bold")

        ' Add a title to the y axis
        c.yAxis().setTitle("Percentage")

        ' Add a light blue (0xccccff) zone for positive part of the plot area
        c.yAxis().addZone(0, 9999, &Hccccff)

        ' Add a pink (0xffffcc) zone for negative part of the plot area
        c.yAxis().addZone(-9999, 0, &Hffcccc)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", "title='{xLabel}: {value}%'")

    End Sub

End Class

