Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class multipie
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Multi-Pie Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 3
    End Function

    'Main code for creating charts
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the pie chart
        Dim data0() As Double = {25, 18, 15}
        Dim data1() As Double = {14, 32, 24}
        Dim data2() As Double = {25, 23, 9}

        ' The labels for the pie chart
        Dim labels() As String = {"Software", "Hardware", "Services"}

        ' Create a PieChart object of size 180 x 160 pixels
        Dim c As PieChart = New PieChart(180, 160)

        ' Set the center of the pie at (90, 80) and the radius to 60 pixels
        c.setPieSize(90, 80, 60)

        ' Set the border color of the sectors to white (ffffff)
        c.setLineColor(&Hffffff)

        ' Set the background color of the sector label to pale yellow (ffffc0) with a black border
        ' (000000)
        c.setLabelStyle().setBackground(&Hffffc0, &H000000)

        ' Set the label to be slightly inside the perimeter of the circle
        c.setLabelLayout(Chart.CircleLayout, -10)

        ' Set the title, data and colors according to which pie to draw
        If chartIndex = 0 Then
            c.addTitle("Alpha Division", "Arial Bold", 8)
            c.setData(data0, labels)
            c.setColors2(Chart.DataColor, New Integer() {&Hff3333, &Hff9999, &Hffcccc})
        ElseIf chartIndex = 1 Then
            c.addTitle("Beta Division", "Arial Bold", 8)
            c.setData(data1, labels)
            c.setColors2(Chart.DataColor, New Integer() {&H33ff33, &H99ff99, &Hccffcc})
        Else
            c.addTitle("Gamma Division", "Arial Bold", 8)
            c.setData(data2, labels)
            c.setColors2(Chart.DataColor, New Integer() {&H3333ff, &H9999ff, &Hccccff})
        End If

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{label}: US${value}M ({percent}%)'")

    End Sub

End Class

