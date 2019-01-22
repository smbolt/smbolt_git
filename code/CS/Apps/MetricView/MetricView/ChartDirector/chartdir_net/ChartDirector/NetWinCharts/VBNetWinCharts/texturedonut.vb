Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class texturedonut
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Texture Donut Chart"
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
        Dim data() As Double = {18, 45, 28}

        ' The labels for the pie chart
        Dim labels() As String = {"Marble", "Wood", "Granite"}

        ' The icons for the sectors
        Dim texture() As String = {"marble3.png", "wood.png", "rock.png"}

        ' Create a PieChart object of size 400 x 330 pixels, with a metallic green (88EE88)
        ' background, black border and 1 pixel 3D border effect
        Dim c As PieChart = New PieChart(400, 330, Chart.metalColor(&H88ee88), &H000000, 1)

        ' Set donut center at (200, 160), and outer/inner radii as 120/60 pixels
        c.setDonutSize(200, 160, 120, 60)

        ' Add a title box using 15pt Times Bold Italic font and metallic deep green (008000)
        ' background color
        c.addTitle("Material Composition", "Times New Roman Bold Italic", 15).setBackground( _
            Chart.metalColor(&H008000))

        ' Set the pie data and the pie labels
        c.setData(data, labels)

        ' Set the colors of the sectors to the 3 texture patterns
        c.setColor(Chart.DataColor + 0, c.patternColor2(texture(0)))
        c.setColor(Chart.DataColor + 1, c.patternColor2(texture(1)))
        c.setColor(Chart.DataColor + 2, c.patternColor2(texture(2)))

        ' Draw the pie in 3D with a 3D depth of 30 pixels
        c.set3D(30)

        ' Use 12pt Arial Bold Italic as the sector label font
        c.setLabelStyle("Arial Bold Italic", 12)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{label}: {value}kg ({percent}%)'")

    End Sub

End Class

