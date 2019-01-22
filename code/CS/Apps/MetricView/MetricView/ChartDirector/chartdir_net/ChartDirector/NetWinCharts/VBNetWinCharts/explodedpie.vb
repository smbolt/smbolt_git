Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class explodedpie
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Exploded Pie Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the pie chart
        Dim data() As Double = {21, 18, 15, 12, 8, 24}

        ' The labels for the pie chart
        Dim labels() As String = {"Labor", "Licenses", "Taxes", "Legal", "Facilities", _
            "Production"}

        ' The colors to use for the sectors
        Dim colors() As Integer = {&H66aaee, &Heebb22, &Hbbbbbb, &H8844ff, &Hdd2222, &H009900}

        ' Create a PieChart object of size 600 x 360 pixels.
        Dim c As PieChart = New PieChart(600, 360)

        ' Use the white on black palette, which means the default text and line colors are white
        c.setColors(Chart.whiteOnBlackPalette)

        ' Use a vertical gradient color from deep blue (000066) to blue (0000cc) as background. Use
        ' rounded corners of 20 pixels radius. Enable soft drop shadow.
        c.setBackground(c.linearGradientColor(0, 0, 0, c.getHeight(), &H000066, &H0000cc))
        c.setRoundedFrame(&Hffffff, 20)
        c.setDropShadow()

        ' Add a title using 18pt Times New Roman Bold Italic font. Add 16 pixels top margin to the
        ' title.
        c.addTitle("Exploded Pie Chart Demonstration", "Times New Roman Bold Italic", 18 _
            ).setMargin2(0, 0, 16, 0)

        ' Set the center of the pie at (300, 195) and the radius to 110 pixels
        c.setPieSize(300, 195, 110)

        ' Set the pie data and the pie labels
        c.setData(data, labels)

        ' Set the sector colors
        c.setColors2(Chart.DataColor, colors)

        ' Use local gradient shading for the sectors, with 5 pixels wide semi-transparent white
        ' (bbffffff) borders
        c.setSectorStyle(Chart.LocalGradientShading, &Hbbffffff, 5)

        ' Use the side label layout method
        c.setLabelLayout(Chart.SideLayout)

        ' Use 10pt Arial Bold as the default label font. Set the label box background color the same
        ' as the sector color. Use soft lighting effect with light direction from right. Use 8
        ' pixels rounded corners.
        Dim t As ChartDirector.TextBox = c.setLabelStyle("Arial Bold", 10, &H000000)
        t.setBackground(Chart.SameAsMainColor, Chart.Transparent, Chart.softLighting(Chart.Right, _
            0))
        t.setRoundedCorners(8)

        ' Set the sector label format. The label is centered in a 110 pixels wide bounding box. It
        ' consists of two lines. The first line is the sector name. The second line shows the data
        ' value and percentage.
        c.setLabelFormat( _
            "<*block,halign=center,width=110*>{label}<*br*><*font=Arial,size=8*>US$ {value}M " & _
            "({percent}%)<*/*>")

        ' Explode all sectors 10 pixels from the center
        c.setExplode(-1, 10)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{label}: US${value}M ({percent}%)'")

    End Sub

End Class

