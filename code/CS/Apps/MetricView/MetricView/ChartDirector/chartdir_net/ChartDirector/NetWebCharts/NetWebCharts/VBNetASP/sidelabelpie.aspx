<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The data for the pie chart
    Dim data() As Double = {35, 30, 25, 7, 6, 5, 4, 3, 2, 1}

    ' The labels for the pie chart
    Dim labels() As String = {"Labor", "Production", "Facilities", "Taxes", "Misc", "Legal", _
        "Insurance", "Licenses", "Transport", "Interest"}

    ' Create a PieChart object of size 560 x 270 pixels, with a golden background and a 1 pixel 3D
    ' border
    Dim c As PieChart = New PieChart(560, 270, Chart.goldColor(), -1, 1)

    ' Add a title box using 15pt Times Bold Italic font and metallic pink background color
    c.addTitle("Project Cost Breakdown", "Times New Roman Bold Italic", 15).setBackground( _
        Chart.metalColor(&Hff9999))

    ' Set the center of the pie at (280, 135) and the radius to 110 pixels
    c.setPieSize(280, 135, 110)

    ' Draw the pie in 3D with 20 pixels 3D depth
    c.set3D(20)

    ' Use the side label layout method
    c.setLabelLayout(Chart.SideLayout)

    ' Set the label box background color the same as the sector color, with glass effect, and with 5
    ' pixels rounded corners
    Dim t As ChartDirector.TextBox = c.setLabelStyle()
    t.setBackground(Chart.SameAsMainColor, Chart.Transparent, Chart.glassEffect())
    t.setRoundedCorners(5)

    ' Set the border color of the sector the same color as the fill color. Set the line color of the
    ' join line to black (0x0)
    c.setLineColor(Chart.SameAsMainColor, &H000000)

    ' Set the start angle to 135 degrees may improve layout when there are many small sectors at the
    ' end of the data array (that is, data sorted in descending order). It is because this makes the
    ' small sectors position near the horizontal axis, where the text label has the least tendency
    ' to overlap. For data sorted in ascending order, a start angle of 45 degrees can be used
    ' instead.
    c.setStartAngle(135)

    ' Set the pie data and the pie labels
    c.setData(data, labels)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{label}: US${value}K ({percent}%)'")

End Sub

</script>

<html>
<head>
    <title>Side Label Layout</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Side Label Layout
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

