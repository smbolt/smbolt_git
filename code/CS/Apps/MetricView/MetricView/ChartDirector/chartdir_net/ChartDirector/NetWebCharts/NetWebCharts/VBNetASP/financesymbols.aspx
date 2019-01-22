<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' Create a finance chart demo containing 100 days of data
    Dim noOfDays As Integer = 100

    ' To compute moving averages starting from the first day, we need to get extra data points
    ' before the first day
    Dim extraDays As Integer = 30

    ' In this exammple, we use a random number generator utility to simulate the data. We set up the
    ' random table to create 6 cols x (noOfDays + extraDays) rows, using 9 as the seed.
    Dim rantable As RanTable = New RanTable(9, 6, noOfDays + extraDays)

    ' Set the 1st col to be the timeStamp, starting from Sep 4, 2014, with each row representing one
    ' day, and counting week days only (jump over Sat and Sun)
    rantable.setDateCol(0, DateSerial(2014, 9, 4), 86400, True)

    ' Set the 2nd, 3rd, 4th and 5th columns to be high, low, open and close data. The open value
    ' starts from 100, and the daily change is random from -5 to 5.
    rantable.setHLOCCols(1, 100, -5, 5)

    ' Set the 6th column as the vol data from 5 to 25 million
    rantable.setCol(5, 50000000, 250000000)

    ' Now we read the data from the table into arrays
    Dim timeStamps() As Double = rantable.getCol(0)
    Dim highData() As Double = rantable.getCol(1)
    Dim lowData() As Double = rantable.getCol(2)
    Dim openData() As Double = rantable.getCol(3)
    Dim closeData() As Double = rantable.getCol(4)
    Dim volData() As Double = rantable.getCol(5)

    ' Custom data series should be of the same length as the OHLC data series
    Dim buySignal(UBound(closeData)) As Double
    Dim sellSignal(UBound(closeData)) As Double

    '
    ' The following is just an arbitrary algorithm to create some meaningless buySignal and
    ' sellSignal. They are just for demonstrating the charting engine. Please do not use them for
    ' actual trading.
    '

    Dim sma5() As Double = New ArrayMath(closeData).movAvg(5).result()
    Dim sma20() As Double = New ArrayMath(closeData).movAvg(20).result()

    For i As Integer = 0 To UBound(sma5)
        buySignal(i) = Chart.NoValue
        sellSignal(i) = Chart.NoValue
        If i > 0 Then
            If (sma5(i - 1) <= sma20(i - 1)) And (sma5(i) > sma20(i)) Then
                buySignal(i) = lowData(i)
            End If
            If (sma5(i - 1) >= sma20(i - 1)) And (sma5(i) < sma20(i)) Then
                sellSignal(i) = highData(i)
            End If
        End If
    Next

    ' Create a FinanceChart object of width 640 pixels
    Dim c As FinanceChart = New FinanceChart(640)

    ' Add a title to the chart
    c.addTitle("Finance Chart with Custom Symbols")

    ' Set the data into the finance chart object
    c.setData(timeStamps, highData, lowData, openData, closeData, volData, extraDays)

    ' Add the main chart with 240 pixels in height
    Dim mainChart As XYChart = c.addMainChart(240)

    ' Add buy signal symbols to the main chart, using cyan (0x00ffff) upward pointing arrows as
    ' symbols
    Dim buyLayer As ScatterLayer = mainChart.addScatterLayer(Nothing, buySignal, "Buy", _
        Chart.ArrowShape(0, 1, 0.4, 0.4), 11, &H00ffff&)
    ' Shift the symbol lower by 20 pixels
    buyLayer.getDataSet(0).setSymbolOffset(0, 20)

    ' Add sell signal symbols to the main chart, using purple (0x9900cc) downward pointing arrows as
    ' symbols
    Dim sellLayer As ScatterLayer = mainChart.addScatterLayer(Nothing, sellSignal, "Sell", _
        Chart.ArrowShape(180, 1, 0.4, 0.4), 11, &H9900cc)
    ' Shift the symbol higher by 20 pixels
    sellLayer.getDataSet(0).setSymbolOffset(0, -20)

    ' Add a 5 period simple moving average to the main chart, using brown color
    c.addSimpleMovingAvg(5, &H663300)

    ' Add a 20 period simple moving average to the main chart, using purple color
    c.addSimpleMovingAvg(20, &H9900ff)

    ' Add candlestick symbols to the main chart, using green/red for up/down days
    c.addCandleStick(&H66ff66, &Hff6666)

    ' Add a volume indicator chart (75 pixels high) after the main chart, using green/red/grey for
    ' up/down/flat days
    c.addVolIndicator(75, &H99ff99, &Hff9999, &H808080)

    ' Append a 14-days RSI indicator chart (75 pixels high) after the main chart. The main RSI line
    ' is purple (800080). Set threshold region to +/- 20 (that is, RSI = 50 +/- 25). The upper/lower
    ' threshold regions will be filled with red (ff0000)/blue (0000ff).
    c.addRSI(75, 14, &H800080, 20, &Hff0000, &H0000ff)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

End Sub

</script>

<html>
<head>
    <title>Finance Chart Custom Symbols</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Finance Chart Custom Symbols
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

