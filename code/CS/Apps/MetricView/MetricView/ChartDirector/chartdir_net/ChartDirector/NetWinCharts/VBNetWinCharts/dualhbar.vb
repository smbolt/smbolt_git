Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class dualhbar
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Dual Horizontal Bar Charts"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The age groups
        Dim labels() As String = {"0 - 4", "5 - 9", "10 - 14", "15 - 19", "20 - 24", "24 - 29", _
            "30 - 34", "35 - 39", "40 - 44", "44 - 49", "50 - 54", "55 - 59", "60 - 64", _
            "65 - 69", "70 - 74", "75 - 79", "80+"}

        ' The male population (in thousands)
        Dim male() As Double = {215, 238, 225, 236, 235, 260, 286, 340, 363, 305, 259, 164, 135, _
            127, 102, 68, 66}

        ' The female population (in thousands)
        Dim female() As Double = {194, 203, 201, 220, 228, 271, 339, 401, 384, 304, 236, 137, 116, _
            122, 112, 85, 110}


        '=============================================================
        '    Draw the right bar chart
        '=============================================================

        ' Create a XYChart object of size 320 x 300 pixels
        Dim c As XYChart = New XYChart(320, 300)

        ' Set the plotarea at (50, 0) and of size 250 x 255 pixels. Use pink (0xffdddd) as the
        ' background.
        c.setPlotArea(50, 0, 250, 255, &Hffdddd)

        ' Add a custom text label at the top right corner of the right bar chart
        c.addText(300, 0, "Female", "Times New Roman Bold Italic", 12, &Ha07070).setAlignment( _
            Chart.TopRight)

        ' Add the pink (0xf0c0c0) bar chart layer using the female data
        Dim femaleLayer As BarLayer = c.addBarLayer(female, &Hf0c0c0, "Female")

        ' Swap the axis so that the bars are drawn horizontally
        c.swapXY(True)

        ' Set the bar to touch each others
        femaleLayer.setBarGap(Chart.TouchBar)

        ' Set the border style of the bars to 1 pixel 3D border
        femaleLayer.setBorderColor(-1, 1)

        ' Add a Transparent line layer to the chart using the male data. As it is Transparent, only
        ' the female bar chart can be seen. We need to put both male and female data in both left
        ' and right charts, because we want auto-scaling to produce the same scale for both chart.
        c.addLineLayer(male, Chart.Transparent)

        ' Set the y axis label font to Arial Bold
        c.yAxis().setLabelStyle("Arial Bold")

        ' Set the labels between the two bar charts, which can be considered as the x-axis labels
        ' for the right chart
        Dim tb As ChartDirector.TextBox = c.xAxis().setLabels(labels)

        ' Use a fix width of 50 for the labels (height = automatic) with center alignment
        tb.setSize(50, 0)
        tb.setAlignment(Chart.Center)

        ' Set the label font to Arial Bold
        tb.setFontStyle("Arial Bold")

        ' Disable ticks on the x-axis by setting the tick length to 0
        c.xAxis().setTickLength(0)

        '=============================================================
        '    Draw the left bar chart
        '=============================================================

        ' Create a XYChart object of size 280 x 300 pixels with a transparent background.
        Dim c2 As XYChart = New XYChart(280, 300, Chart.Transparent)

        ' Set the plotarea at (20, 0) and of size 250 x 255 pixels. Use pale blue (0xddddff) as the
        ' background.
        c2.setPlotArea(20, 0, 250, 255, &Hddddff)

        ' Add a custom text label at the top left corner of the left bar chart
        c2.addText(20, 0, "Male", "Times New Roman Bold Italic", 12, &H7070a0)

        ' Add the pale blue (0xaaaaff) bar chart layer using the male data
        Dim maleLayer As BarLayer = c2.addBarLayer(male, &Haaaaff, "Male")

        ' Swap the axis so that the bars are drawn horizontally
        c2.swapXY(True)

        ' Reverse the direction of the y-axis so it runs from right to left
        c2.yAxis().setReverse()

        ' Set the bar to touch each others
        maleLayer.setBarGap(Chart.TouchBar)

        ' Set the border style of the bars to 1 pixel 3D border
        maleLayer.setBorderColor(-1, 1)

        ' Add a Transparent line layer to the chart using the female data. As it is Transparent,
        ' only the male bar chart can be seen. We need to put both male and female data in both left
        ' and right charts, because we want auto-scaling to produce the same scale for both chart.
        c2.addLineLayer(female, Chart.Transparent)

        ' Set the y axis label font to Arial Bold
        c2.yAxis().setLabelStyle("Arial Bold")

        ' Set the x-axis labels for tool tip purposes.
        c2.xAxis().setLabels(labels)

        ' Hide the x-axis labels by setting them to Transparent. We only need to display the x-axis
        ' labels for the right chart.
        c2.xAxis().setColors(&H000000, Chart.Transparent, -1, Chart.Transparent)

        '=============================================================
        '    Use a MultiChart to contain both bar charts
        '=============================================================

        ' Create a MultiChart object of size 590 x 320 pixels.
        Dim m As MultiChart = New MultiChart(590, 320)

        ' Add a title to the chart using Arial Bold Italic font
        m.addTitle("Demographics Hong Kong Year 2002", "Arial Bold Italic")

        ' Add another title at the bottom using Arial Bold Italic font
        m.addTitle2(Chart.Bottom, "Population (in thousands)", "Arial Bold Italic", 10)

        ' Put the right chart at (270, 25)
        m.addChart(270, 25, c)

        ' Put the left chart at (0, 25)
        m.addChart(0, 25, c2)

        ' Output the chart
        viewer.Chart = m

        'include tool tip for the chart
        viewer.ImageMap = m.getHTMLImageMap("clickable", "", _
            "title='{dataSetName} (Age {xLabel}): Population {value}K'")

    End Sub

End Class

