Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class concentric
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Concentric Donut Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' Data for outer ring
        Dim data() As Double = {88, 124, 96}

        ' Data for inner ring
        Dim data2() As Double = {77, 87, 45}

        ' Labels for the sectors
        Dim labels() As String = {"Hardware", "Software", "Services"}

        ' Colors for the sectors
        Dim colors() As Integer = {&Hff9999, &H9999ff, &H66ff66}

        '
        ' Create the main chart, which contains the chart title, the outer ring, and the legend box
        '

        ' Create a PieChart object of size 450 x 360 pixels, with transparent background
        Dim c As PieChart = New PieChart(450, 360)

        ' Add a title to the chart with 18pt Times Bold Italic font
        c.addTitle("Concentric Donut Chart", "Times New Roman Bold Italic", 18)

        ' Set donut center at (160, 200), and outer/inner radii as 150/100 pixels
        c.setDonutSize(160, 200, 150, 100)

        ' Add a label at the bottom-right corner of the ring to label the outer ring Use 12pt Arial
        ' Bold Italic font in white (ffffff) color, on a green (008800) background, with soft
        ' lighting effect and 5 pixels rounded corners
        Dim t As ChartDirector.TextBox = c.addText(260, 300, " Year 2006 ", "Arial Bold Italic", _
            12, &Hffffff)
        t.setBackground(&H008800, Chart.Transparent, Chart.softLighting())
        t.setRoundedCorners(5)

        ' Set the legend box at (320, 50) with 12pt Arial Bold Italic font, with no border
        c.addLegend(320, 50, True, "Arial Bold Italic", 13).setBackground(Chart.Transparent, _
            Chart.Transparent)

        ' Set the pie data and the pie labels
        c.setData(data, labels)

        ' Set the pie colors
        c.setColors2(Chart.DataColor, colors)

        ' Set pie border color to white (ffffff)
        c.setLineColor(&Hffffff)

        ' Set pie label to value in $###M format, percentage in (##.#%) format, in two lines.
        c.setLabelFormat("${value}M<*br*>({percent|1}%)")

        ' Use 10pt Airal Bold for the sector labels
        c.setLabelStyle("Arial Bold", 10)

        ' Set the label position to -25 pixels from the sector (which would be internal to the
        ' sector)
        c.setLabelPos(-25)

        '
        ' Create the inner ring.
        '

        ' Create a PieChart object of size 280 x 320 pixels, with transparent background
        Dim c2 As PieChart = New PieChart(280, 320, Chart.Transparent)

        ' Set donut center at (110, 110), and outer/inner radii as 100/50 pixels
        c2.setDonutSize(110, 110, 100, 50)

        ' Add a label at the center of the ring to label the inner ring. Use 12pt Arial Bold Italic
        ' font in white (ffffff) color, on a deep blue (0000cc) background, with soft lighting
        ' effect and 5 pixels rounded corners
        Dim t2 As ChartDirector.TextBox = c2.addText(110, 110, " Year 2005 ", "Arial Bold Italic", _
            12, &Hffffff, Chart.Center)
        t2.setBackground(&H0000cc, Chart.Transparent, Chart.softLighting())
        t2.setRoundedCorners(5)

        ' Set the pie data and the pie labels
        c2.setData(data2, labels)

        ' Set the pie colors
        c2.setColors2(Chart.DataColor, colors)

        ' Set pie border color to white (ffffff)
        c2.setLineColor(&Hffffff)

        ' Set pie label to value in $###M format, percentage in (##.#%) format, in two lines.
        c2.setLabelFormat("${value}M<*br*>({percent|1}%)")

        ' Use 10pt Airal Bold for the sector labels
        c2.setLabelStyle("Arial Bold", 10)

        ' Set the label position to -25 pixels from the sector (which would be internal to the
        ' sector)
        c2.setLabelPos(-25)

        ' merge the inner ring into the outer ring at (50, 90)
        c.makeChart3().merge(c2.makeChart3(), 50, 90, Chart.TopLeft, 0)

        ' Output the chart
        viewer.Chart = c

        ' include tool tip for the chart
        viewer.ImageMap = c2.getHTMLImageMap("clickable", "", _
            "title='{label} revenue for 2005: US${value}M ({percent}%)'", 50, 90) + _
            c.getHTMLImageMap("clickable", "", _
            "title='{label} revenue for 2006: US${value}M ({percent}%)'")

    End Sub

End Class

