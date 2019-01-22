Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class icondonut
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Icon Donut Chart"
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
        Dim data() As Double = {72, 18, 15, 12}

        ' The depths for the sectors
        Dim depths() As Double = {30, 20, 10, 10}

        ' The labels for the pie chart
        Dim labels() As String = {"Sunny", "Cloudy", "Rainy", "Snowy"}

        ' The icons for the sectors
        Dim icons() As String = {"sun.png", "cloud.png", "rain.png", "snowy.png"}

        ' Create a PieChart object of size 400 x 300 pixels
        Dim c As PieChart = New PieChart(400, 300)

        ' Use the semi-transparent palette for this chart
        c.setColors(Chart.transparentPalette)

        ' Set the background to metallic light blue (CCFFFF), with a black border and 1 pixel 3D
        ' border effect,
        c.setBackground(Chart.metalColor(&Hccccff), &H000000, 1)

        ' Set donut center at (200, 175), and outer/inner radii as 100/50 pixels
        c.setDonutSize(200, 175, 100, 50)

        ' Add a title box using 15pt Times Bold Italic font and metallic blue (8888FF) background
        ' color
        c.addTitle("Weather Profile in Wonderland", "Times New Roman Bold Italic", 15 _
            ).setBackground(Chart.metalColor(&H8888ff))

        ' Set the pie data and the pie labels
        c.setData(data, labels)

        ' Add icons to the chart as a custom field
        c.addExtraField(icons)

        ' Configure the sector labels using CDML to include the icon images
        c.setLabelFormat( _
            "<*block,valign=absmiddle*><*img={field0}*> <*block*>{label}<*br*>{percent}%<*/*><*/*>")

        ' Draw the pie in 3D with variable 3D depths
        c.set3D2(depths)

        ' Set the start angle to 225 degrees may improve layout when the depths of the sector are
        ' sorted in descending order, because it ensures the tallest sector is at the back.
        c.setStartAngle(225)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{label}: {value} days ({percent}%)'")

    End Sub

End Class

