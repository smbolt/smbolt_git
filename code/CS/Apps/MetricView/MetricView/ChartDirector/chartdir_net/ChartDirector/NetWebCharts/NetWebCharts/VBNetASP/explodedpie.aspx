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
    Dim data() As Double = {21, 18, 15, 12, 8, 24}

    ' The labels for the pie chart
    Dim labels() As String = {"Labor", "Licenses", "Taxes", "Legal", "Facilities", "Production"}

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
    c.addTitle("Exploded Pie Chart Demonstration", "Times New Roman Bold Italic", 18).setMargin2( _
        0, 0, 16, 0)

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

    ' Use 10pt Arial Bold as the default label font. Set the label box background color the same as
    ' the sector color. Use soft lighting effect with light direction from right. Use 8 pixels
    ' rounded corners.
    Dim t As ChartDirector.TextBox = c.setLabelStyle("Arial Bold", 10, &H000000)
    t.setBackground(Chart.SameAsMainColor, Chart.Transparent, Chart.softLighting(Chart.Right, 0))
    t.setRoundedCorners(8)

    ' Set the sector label format. The label is centered in a 110 pixels wide bounding box. It
    ' consists of two lines. The first line is the sector name. The second line shows the data value
    ' and percentage.
    c.setLabelFormat( _
        "<*block,halign=center,width=110*>{label}<*br*><*font=Arial,size=8*>US$ {value}M " & _
        "({percent}%)<*/*>")

    ' Explode all sectors 10 pixels from the center
    c.setExplode(-1, 10)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{label}: US${value}M ({percent}%)'")

End Sub

</script>

<html>
<head>
    <title>Exploded Pie Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Exploded Pie Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

