<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The data for the area chart
    Dim data() As Double = {30, 28, 40, 55, 75, 68, 54, 60, 50, 62, 75, 65, 75, 89, 60, 55, 53, _
        35, 50, 66, 56, 48, 52, 65, 62}

    ' The labels for the area chart
    Dim labels() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", _
        "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24"}

    ' Create a XYChart object of size 300 x 300 pixels
    Dim c As XYChart = New XYChart(300, 300)

    ' Set the plotarea at (45, 30) and of size 200 x 200 pixels
    c.setPlotArea(45, 30, 200, 200)

    ' Add a title to the chart using 12pt Arial Bold Italic font
    c.addTitle("Daily Server Utilization", "Arial Bold Italic", 12)

    ' Add a title to the y axis
    c.yAxis().setTitle("MBytes")

    ' Add a title to the x axis
    c.xAxis().setTitle("June 12, 2001")

    ' Add a green (0x00ff00) 3D area chart layer using the give data
    c.addAreaLayer(data, &H00ff00).set3D()

    ' Set the labels on the x axis.
    c.xAxis().setLabels(labels)

    ' Display 1 out of 3 labels on the x-axis.
    c.xAxis().setLabelStep(3)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='Hour {xLabel}: Traffic {value} MBytes'")

End Sub

</script>

<html>
<head>
    <title>3D Area Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        3D Area Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

