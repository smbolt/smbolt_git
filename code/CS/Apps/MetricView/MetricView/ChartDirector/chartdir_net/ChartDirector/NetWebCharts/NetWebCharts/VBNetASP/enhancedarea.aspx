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

    ' Create a XYChart object of size 600 x 360 pixels, with a brushed silver background, 1 pixel 3D
    ' border effect, rounded corners and soft drop shadow.
    Dim c As XYChart = New XYChart(600, 360, Chart.brushedSilverColor(), Chart.Transparent, 1)
    c.setRoundedFrame()
    c.setDropShadow()

    ' Set default directory for loading images from current script directory
    Call c.setSearchPath(Server.MapPath("."))

    ' Add a title box to the chart using 18pt Times Bold Italic font.
    Dim title As ChartDirector.TextBox = c.addTitle( _
        "<*block,valign=absmiddle*><*img=star.png*><*img=star.png*> Performance Enhancer " & _
        "<*img=star.png*><*img=star.png*><*/*>", "Times New Roman Bold Italic", 18)

    '
    ' Use a text box with a depressed 3D border to create the inner depressed region
    '

    ' The width of the frame border
    Dim frameWidth As Integer = 5

    ' Set the depressed region position
    Dim contentBox As ChartDirector.TextBox = c.addText(frameWidth, title.getHeight(), "")
    contentBox.setSize(c.getDrawArea().getWidth() - 1 - frameWidth * 2, c.getDrawArea().getHeight( _
        ) - title.getHeight() - frameWidth - 1)

    ' Use -1 as the rasied effect to create a depressed region
    contentBox.setBackground(Chart.Transparent, Chart.Transparent, -1)

    ' Set rounded corners, and put the text box at the back of the chart
    contentBox.setRoundedCorners(10)
    contentBox.setZOrder(Chart.ChartBackZ)

    ' Tentatively set the plotarea to 50 pixels from the left depressed edge, and 25 pixels under
    ' the top depressed edge. Set the width to 75 pixels less than the depressed region width, and
    ' the height to 75 pixels less than the depressed region height. Use white (ffffff) background,
    ' transparent border, and grey (cccccc) horizontal and vertical grid lines.
    Dim plotArea As PlotArea = c.setPlotArea(50 + contentBox.getLeftX(), contentBox.getTopY() + _
        25, contentBox.getWidth() - 75, contentBox.getHeight() - 75, &Hffffff, -1, -1, &Hcccccc, _
        -1)

    ' Add a title to the y axis
    c.yAxis().setTitle("Energy Concentration (KJ per liter)")

    ' Set the labels on the x axis.
    c.xAxis().setLabels(labels)

    ' Display 1 out of 3 labels on the x-axis.
    c.xAxis().setLabelStep(3)

    ' Add a title to the x axis using CDML
    c.xAxis().setTitle("<*block,valign=absmiddle*><*img=clock.png*>  Elapsed Time (hour)<*/*>")

    ' Set the axes width to 2 pixels
    c.xAxis().setWidth(2)
    c.yAxis().setWidth(2)

    ' Add an area layer to the chart using a gradient color that changes vertically from
    ' semi-transparent red (60ff0000) to semi-transparent white (60ffffff)
    c.addAreaLayer(data, c.linearGradientColor(0, contentBox.getTopY() + 20, 0, _
        contentBox.getTopY() + contentBox.getHeight() - 50, &H60ff0000, &H60ffffff))

    ' Adjust the plot area size, such that the bounding box (inclusive of axes) is 15 pixels from
    ' the left depressed edge, 25 pixels below the top depressed edge, 25 pixels from the right
    ' depressed edge, and 15 pixels above the bottom depressed edge.
    c.packPlotArea(contentBox.getLeftX() + 15, contentBox.getTopY() + 25, contentBox.getLeftX() + _
        contentBox.getWidth() - 25, contentBox.getTopY() + contentBox.getHeight() - 15)

    ' Add a custom CDML text with the bottom right corner is anchored to the bootom right corner of
    ' the plot area, with 5 pixels margin.
    c.addText(plotArea.getLeftX() + plotArea.getWidth() - 5, plotArea.getTopY() + _
        plotArea.getHeight() - 5, _
        "<*block,valign=absmiddle*><*img=small_molecule.png*> <*block*><*font=Times New Roman " & _
        "Bold Italic,size=10,color=804040*>Molecular<*br*>Engineering<*/*>").setAlignment( _
        Chart.BottomRight)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.JPG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", "title='Hour {xLabel}: {value} KJ/liter'")

End Sub

</script>

<html>
<head>
    <title>Enhanced Area Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Enhanced Area Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

