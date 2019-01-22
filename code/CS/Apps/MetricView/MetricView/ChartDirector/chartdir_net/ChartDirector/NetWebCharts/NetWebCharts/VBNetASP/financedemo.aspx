<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<script runat="server">

' The timeStamps, volume, high, low, open and close data
Dim timeStamps() As Date = Nothing
Dim volData() As Double = Nothing
Dim highData() As Double = Nothing
Dim lowData() As Double = Nothing
Dim openData() As Double = Nothing
Dim closeData() As Double = Nothing

' An extra data series to compare with the close data
Dim compareData() As Double = Nothing

' The resolution of the data in seconds. 1 day = 86400 seconds.
Dim resolution As Integer = 86400

'/ <summary>
'/ Get the timeStamps, highData, lowData, openData, closeData and volData.
'/ </summary>
'/ <param name="ticker">The ticker symbol for the data series.</param>
'/ <param name="startDate">The starting date/time for the data series.</param>
'/ <param name="endDate">The ending date/time for the data series.</param>
'/ <param name="durationInDays">The number of trading days to get.</param>
'/ <param name="extraPoints">The extra leading data points needed in order to
'/ compute moving averages.</param>
'/ <returns>True if successfully obtain the data, otherwise false.</returns>
Protected Function getData(ticker As String, startDate As Date, endDate As Date, _
    durationInDays As Integer, extraPoints As Integer) As Boolean

    ' This method should return false if the ticker symbol is invalid. In this sample code, as we
    ' are using a random number generator for the data, all ticker symbol is allowed, but we still
    ' assumed an empty symbol is invalid.
    If ticker = "" Then
        Return False
    End If

    ' In this demo, we can get 15 min, daily, weekly or monthly data depending on the time range.
    resolution = 86400
    If durationInDays <= 10 Then
        ' 10 days or less, we assume 15 minute data points are available
        resolution = 900

        ' We need to adjust the startDate backwards for the extraPoints. We assume 6.5 hours trading
        ' time per day, and 5 trading days per week.
        Dim dataPointsPerDay As Double = 6.5 * 3600 / resolution
        Dim adjustedStartDate As Date = startDate.AddDays(-Math.Ceiling(extraPoints / _
            dataPointsPerDay * 7 / 5) - 2)

        ' Get the required 15 min data
        get15MinData(ticker, adjustedStartDate, endDate)

    ElseIf durationInDays >= 4.5 * 360 Then
        ' 4 years or more - use monthly data points.
        resolution = 30 * 86400

        ' Adjust startDate backwards to cater for extraPoints
        Dim adjustedStartDate As Date = startDate.Date.AddMonths(-extraPoints)

        ' Get the required monthly data
        getMonthlyData(ticker, adjustedStartDate, endDate)

    ElseIf durationInDays >= 1.5 * 360 Then
        ' 1 year or more - use weekly points.
        resolution = 7 * 86400

        ' Adjust startDate backwards to cater for extraPoints
        Dim adjustedStartDate As Date = startDate.Date.AddDays(-extraPoints * 7 - 6)

        ' Get the required weekly data
        getWeeklyData(ticker, adjustedStartDate, endDate)

    Else
        ' Default - use daily points
        resolution = 86400

        ' Adjust startDate backwards to cater for extraPoints. We multiply the days by 7/5 as we
        ' assume 1 week has 5 trading days.
        Dim adjustedStartDate As Date = startDate.Date.AddDays(-Math.Ceiling(extraPoints * 7.0 / 5 _
            ) - 2)

        ' Get the required daily data
        getDailyData(ticker, adjustedStartDate, endDate)
    End If

    Return True

End Function

'/ <summary>
'/ Get 15 minutes data series for timeStamps, highData, lowData, openData, closeData
'/ and volData.
'/ </summary>
'/ <param name="ticker">The ticker symbol for the data series.</param>
'/ <param name="startDate">The starting date/time for the data series.</param>
'/ <param name="endDate">The ending date/time for the data series.</param>
Protected Sub get15MinData(ticker As String, startDate As Date, endDate As Date)

    '
    ' In this demo, we use a random number generator to generate the data. In practice, you may get
    ' the data from a database or by other means. If you do not have 15 minute data, you may modify
    ' the "drawChart" method below to not using 15 minute data.
    '
    generateRandomData(ticker, startDate, endDate, 900)

End Sub

'/ <summary>
'/ Get daily data series for timeStamps, highData, lowData, openData, closeData
'/ and volData.
'/ </summary>
'/ <param name="ticker">The ticker symbol for the data series.</param>
'/ <param name="startDate">The starting date/time for the data series.</param>
'/ <param name="endDate">The ending date/time for the data series.</param>
Protected Sub getDailyData(ticker As String, startDate As Date, endDate As Date)

    '
    ' In this demo, we use a random number generator to generate the data. In practice, you may get
    ' the data from a database or by other means.
    '
    ' A typical database code example is like below. (This only shows a general idea. The exact
    ' details may differ depending on your database brand and schema. The SQL, in particular the
    ' date format, may be different depending on which brand of database you use.)
    '
    '    ' Open the database connection to MS SQL
    '    Dim dbconn As System.Data.IDbConnection = New System.Data.SqlClient.SqlConnection(
    '          "..... put your database connection string here .......")
    '   dbconn.Open()
    '
    '   ' SQL statement to get the data
    '   Dim sqlCmd As System.Data.IDbCommand = dbconn.CreateCommand()
    '   sqlCmd.CommandText = "Select recordDate, highData, lowData, openData, " & _
    '         "closeData, volData From dailyFinanceTable Where ticker = '" & ticker & _
    '         "' And recordDate >= '" & startDate.ToString("yyyyMMdd") & "' And " & _
    '         "recordDate <= '" & endDate.ToString("yyyyMMdd") & "' Order By recordDate"
    '
    '   ' The most convenient way to read the SQL result into arrays is to use the
    '   ' ChartDirector DBTable utility.
    '   Dim table As DBTable = New DBTable(sqlCmd.ExecuteReader())
    '   dbconn.Close()
    '
    '   ' Now get the data into arrays
    '   timeStamps = table.getColAsDateTime(0)
    '   highData = table.getCol(1)
    '   lowData = table.getCol(2)
    '   openData = table.getCol(3)
    '   closeData = table.getCol(4)
    '   volData = table.getCol(5)
    '
    generateRandomData(ticker, startDate, endDate, 86400)

End Sub

'/ <summary>
'/ Get weekly data series for timeStamps, highData, lowData, openData, closeData
'/ and volData.
'/ </summary>
'/ <param name="ticker">The ticker symbol for the data series.</param>
'/ <param name="startDate">The starting date/time for the data series.</param>
'/ <param name="endDate">The ending date/time for the data series.</param>
Protected Sub getWeeklyData(ticker As String, startDate As Date, endDate As Date)

    '
    ' If you do not have weekly data, you may call "getDailyData(startDate, endDate)" to get daily
    ' data, then call "convertDailyToWeeklyData()" to convert to weekly data.
    '
    generateRandomData(ticker, startDate, endDate, 86400 * 7)

End Sub

'/ <summary>
'/ Get monthly data series for timeStamps, highData, lowData, openData, closeData
'/ and volData.
'/ </summary>
'/ <param name="ticker">The ticker symbol for the data series.</param>
'/ <param name="startDate">The starting date/time for the data series.</param>
'/ <param name="endDate">The ending date/time for the data series.</param>
Protected Sub getMonthlyData(ticker As String, startDate As Date, endDate As Date)

    '
    ' If you do not have weekly data, you may call "getDailyData(startDate, endDate)" to get daily
    ' data, then call "convertDailyToMonthlyData()" to convert to monthly data.
    '
    generateRandomData(ticker, startDate, endDate, 86400 * 30)

End Sub

'/ <summary>
'/ A random number generator designed to generate realistic financial data.
'/ </summary>
'/ <param name="ticker">The ticker symbol for the data series.</param>
'/ <param name="startDate">The starting date/time for the data series.</param>
'/ <param name="endDate">The ending date/time for the data series.</param>
'/ <param name="resolution">The period of the data series.</param>
Protected Sub generateRandomData(ticker As String, startDate As Date, endDate As Date, _
    resolution As Integer)

    Dim db As FinanceSimulator = New FinanceSimulator(ticker, startDate, endDate, resolution)
    timeStamps = db.getTimeStamps()
    highData = db.getHighData()
    lowData = db.getLowData()
    openData = db.getOpenData()
    closeData = db.getCloseData()
    volData = db.getVolData()

End Sub

'/ <summary>
'/ A utility to convert daily to weekly data.
'/ </summary>
Protected Sub convertDailyToWeeklyData()

    aggregateData(New ArrayMath(timeStamps).selectStartOfWeek())

End Sub

'/ <summary>
'/ A utility to convert daily to monthly data.
'/ </summary>
Protected Sub convertDailyToMonthlyData()

    aggregateData(New ArrayMath(timeStamps).selectStartOfMonth())

End Sub

'/ <summary>
'/ An internal method used to aggregate daily data.
'/ </summary>
Protected Sub aggregateData(aggregator As ArrayMath)

    timeStamps = Chart.NTime(aggregator.aggregate(Chart.CTime(timeStamps), Chart.AggregateFirst))
    highData = aggregator.aggregate(highData, Chart.AggregateMax)
    lowData = aggregator.aggregate(lowData, Chart.AggregateMin)
    openData = aggregator.aggregate(openData, Chart.AggregateFirst)
    closeData = aggregator.aggregate(closeData, Chart.AggregateLast)
    volData = aggregator.aggregate(volData, Chart.AggregateSum)

End Sub

'/ <summary>
'/ Create a financial chart according to user selections. The user selections are
'/ encoded in the query parameters.
'/ </summary>
Public Function drawChart() As BaseChart

    ' In this demo, we just assume we plot up to the latest time. So end date is now.
    Dim endDate As Date = Now

    ' If the trading day has not yet started (before 9:30am), or if the end date is on on Sat or
    ' Sun, we set the end date to 4:00pm of the last trading day
    Do While (endDate.TimeOfDay.CompareTo(New TimeSpan(9, 30, 0)) < 0) Or (endDate.DayOfWeek = _
        DayOfWeek.Sunday) Or (endDate.DayOfWeek = DayOfWeek.Saturday)
        endDate = endDate.Date.AddDays(-1).Add(New TimeSpan(16, 0, 0))
    Loop

    ' The duration selected by the user
    Dim durationInDays As Integer = CInt(TimeRange.SelectedItem.Value)

    ' Compute the start date by subtracting the duration from the end date.
    Dim startDate As Date = endDate
    If durationInDays >= 30 Then
        ' More or equal to 30 days - so we use months as the unit
        startDate = New DateTime(endDate.Year, endDate.Month, 1).AddMonths(-durationInDays / 30)
    Else
        ' Less than 30 days - use day as the unit. The starting point of the axis is always at the
        ' start of the day (9:30am). Note that we use trading days, so we skip Sat and Sun in
        ' counting the days.
        startDate = endDate.Date
        For i As Integer = 1 To durationInDays - 1
            If startDate.DayOfWeek = DayOfWeek.Monday Then
                startDate = startDate.AddDays(-3)
            Else
                startDate = startDate.AddDays(-1)
            End If
        Next
    End If

    ' The moving average periods selected by the user.
    Dim avgPeriod1 As Integer = 0
    If isNumeric(movAvg1.Text) Then
        avgPeriod1 = CInt(movAvg1.Text)
    Else
        avgPeriod1 = 0
    End If
    Dim avgPeriod2 As Integer = 0
    If isNumeric(movAvg2.Text) Then
        avgPeriod2 = CInt(movAvg2.Text)
    Else
        avgPeriod2 = 0
    End If

    If avgPeriod1 < 0 Then
        avgPeriod1 = 0
    ElseIf avgPeriod1 > 300 Then
        avgPeriod1 = 300
    End If

    If avgPeriod2 < 0 Then
        avgPeriod2 = 0
    ElseIf avgPeriod2 > 300 Then
        avgPeriod2 = 300
    End If

    ' We need extra leading data points in order to compute moving averages.
    Dim extraPoints As Integer = 20
    If avgPeriod1 > extraPoints Then
        extraPoints = avgPeriod1
    End If
    If avgPeriod2 > extraPoints Then
        extraPoints = avgPeriod2
    End If

    ' Get the data series to compare with, if any.
    Dim compareKey As String = Trim(CompareWith.Text)
    compareData = Nothing
    If getData(compareKey, startDate, endDate, durationInDays, extraPoints) Then
          compareData = closeData
    End If

    ' The data series we want to get.
    Dim tickerKey As String = Trim(TickerSymbol.Text)
    If Not getData(tickerKey, startDate, endDate, durationInDays, extraPoints) Then
        Return errMsg("Please enter a valid ticker symbol")
    End If

    ' We now confirm the actual number of extra points (data points that are before the start date)
    ' as inferred using actual data from the database.
    extraPoints = UBound(timeStamps) + 1
    For i As Integer = 0 To UBound(timeStamps)
        If timeStamps(i) >= startDate Then
            extraPoints = i
            Exit For
        End If
    Next

    ' Check if there is any valid data
    If extraPoints >= UBound(timeStamps) + 1 Then
        ' No data - just display the no data message.
        Return errMsg("No data available for the specified time period")
    End If

    ' In some finance chart presentation style, even if the data for the latest day is not fully
    ' available, the axis for the entire day will still be drawn, where no data will appear near the
    ' end of the axis.
    If resolution < 86400 Then
        ' Add extra points to the axis until it reaches the end of the day. The end of day is
        ' assumed to be 16:00 (it depends on the stock exchange).
        Dim lastTime As Date = timeStamps(UBound(timeStamps))
        Dim extraTrailingPoints As Integer = Int(New TimeSpan(16, 0, 0).Subtract( _
            lastTime.TimeOfDay).TotalSeconds / resolution)
        If extraTrailingPoints > 0 Then
            Dim extendedTimeStamps(timeStamps.Length + extraTrailingPoints - 1) As Date
            Array.Copy(timeStamps, 0, extendedTimeStamps, 0, timeStamps.Length)
            For i As Integer = 0 To extraTrailingPoints - 1
                extendedTimeStamps(i + timeStamps.Length) = lastTime.AddSeconds(resolution * (i + _
                    1))
            Next
            timeStamps = extendedTimeStamps
        End If
    End If

    '
    ' At this stage, all data are available. We can draw the chart as according to user input.
    '

    '
    ' Determine the chart size. In this demo, user can select 4 different chart sizes. Default is
    ' the large chart size.
    '
    Dim width As Integer = 780
    Dim mainHeight As Integer = 255
    Dim indicatorHeight As Integer = 80

    Dim size As String = ChartSize.SelectedItem.Value
    If size = "S" Then
        ' Small chart size
        width = 450
        mainHeight = 160
        indicatorHeight = 60
    ElseIf size = "M" Then
        ' Medium chart size
        width = 620
        mainHeight = 215
        indicatorHeight = 70
    ElseIf size = "H" Then
        ' Huge chart size
        width = 1000
        mainHeight = 320
        indicatorHeight = 90
    End If

    ' Create the chart object using the selected size
    Dim m As FinanceChart = New FinanceChart(width)

    ' Set the data into the chart object
    m.setData(timeStamps, highData, lowData, openData, closeData, volData, extraPoints)

    '
    ' We configure the title of the chart. In this demo chart design, we put the company name as the
    ' top line of the title with left alignment.
    '
    m.addPlotAreaTitle(Chart.TopLeft, tickerKey)

    ' We displays the current date as well as the data resolution on the next line.
    Dim resolutionText As String = ""
    If resolution = 30 * 86400 Then
        resolutionText = "Monthly"
    ElseIf resolution = 7 * 86400 Then
        resolutionText = "Weekly"
    ElseIf resolution = 86400 Then
        resolutionText = "Daily"
    ElseIf resolution = 900 Then
        resolutionText = "15-min"
    End If

    m.addPlotAreaTitle(Chart.BottomLeft, "<*font=Arial,size=8*>" & m.formatValue(Now, _
        "mmm dd, yyyy") & " - " & resolutionText & " chart")

    ' A copyright message at the bottom left corner the title area
    m.addPlotAreaTitle(Chart.BottomRight, "<*font=Arial,size=8*>(c) Advanced Software Engineering")

    '
    ' Add the first techical indicator according. In this demo, we draw the first indicator on top
    ' of the main chart.
    '
    addIndicator(m, Indicator1.SelectedItem.Value, indicatorHeight)

    '
    ' Add the main chart
    '
    m.addMainChart(mainHeight)

    '
    ' Set log or linear scale according to user preference
    '
    If LogScale.Checked Then
        m.setLogScale(True)
    End If

    '
    ' Set axis labels to show data values or percentage change to user preference
    '
    If PercentageScale.Checked Then
        m.setPercentageAxis()
    End If

    '
    ' Draw any price line the user has selected
    '
    Dim mainType As String = ChartType.SelectedItem.Value
    If mainType = "Close" Then
        m.addCloseLine(&H000040)
    ElseIf mainType = "TP" Then
        m.addTypicalPrice(&H000040)
    ElseIf mainType = "WC" Then
        m.addWeightedClose(&H000040)
    ElseIf mainType = "Median" Then
        m.addMedianPrice(&H000040)
    End If

    '
    ' Add comparison line if there is data for comparison
    '
    If Not compareData Is Nothing Then
        If UBound(compareData) + 1 > extraPoints Then
            m.addComparison(compareData, &H0000ff, compareKey)
        End If
    End If

    '
    ' Add moving average lines.
    '
    addMovingAvg(m, avgType1.SelectedItem.Value, avgPeriod1, &H663300)
    addMovingAvg(m, avgType2.SelectedItem.Value, avgPeriod2, &H9900ff)

    '
    ' Draw candlesticks or OHLC symbols if the user has selected them.
    '
    If mainType = "CandleStick" Then
        m.addCandleStick(&H33ff33, &Hff3333)
    ElseIf mainType = "OHLC" Then
        m.addHLOC(&H008800, &Hcc0000)
    End If

    '
    ' Add parabolic SAR if necessary
    '
    If ParabolicSAR.Checked Then
        m.addParabolicSAR(0.02, 0.02, 0.2, Chart.DiamondShape, 5, &H008800, &H000000)
    End If

    '
    ' Add price band/channel/envelop to the chart according to user selection
    '
    Dim bandType As String = Band.SelectedItem.Value
    If bandType = "BB" Then
        m.addBollingerBand(20, 2, &H9999ff, &Hc06666ff)
    ElseIf bandType = "DC" Then
        m.addDonchianChannel(20, &H9999ff, &Hc06666ff)
    ElseIf bandType = "Envelop" Then
        m.addEnvelop(20, 0.1, &H9999ff, &Hc06666ff)
    End If

    '
    ' Add volume bars to the main chart if necessary
    '
    If Volume.Checked Then
        m.addVolBars(indicatorHeight, &H99ff99, &Hff9999, &Hc0c0c0)
    End If

    '
    ' Add additional indicators as according to user selection.
    '
    addIndicator(m, Indicator2.SelectedItem.Value, indicatorHeight)
    addIndicator(m, Indicator3.SelectedItem.Value, indicatorHeight)
    addIndicator(m, Indicator4.SelectedItem.Value, indicatorHeight)

    Return m

End Function

'/ <summary>
'/ Add a moving average line to the FinanceChart object.
'/ </summary>
'/ <param name="m">The FinanceChart object to add the line to.</param>
'/ <param name="avgType">The moving average type (SMA/EMA/TMA/WMA).</param>
'/ <param name="avgPeriod">The moving average period.</param>
'/ <param name="color">The color of the line.</param>
'/ <returns>The LineLayer object representing line layer created.</returns>
Protected Function addMovingAvg(m As FinanceChart, avgType As String, avgPeriod As Integer, _
    color As Integer) As LineLayer

    If avgPeriod > 1 Then
        If avgType = "SMA" Then
            Return m.addSimpleMovingAvg(avgPeriod, color)
        ElseIf avgType = "EMA" Then
            Return m.addExpMovingAvg(avgPeriod, color)
        ElseIf avgType = "TMA" Then
            Return m.addTriMovingAvg(avgPeriod, color)
        ElseIf avgType = "WMA" Then
            Return m.addWeightedMovingAvg(avgPeriod, color)
        End If
    End If
    Return Nothing

End Function

'/ <summary>
'/ Add an indicator chart to the FinanceChart object. In this demo example, the
'/ indicator parameters (such as the period used to compute RSI, colors of the lines,
'/ etc.) are hard coded to commonly used values. You are welcome to design a more
'/ complex user interface to allow users to set the parameters.
'/ </summary>
'/ <param name="m">The FinanceChart object to add the line to.</param>
'/ <param name="indicator">The selected indicator.</param>
'/ <param name="height">Height of the chart in pixels</param>
'/ <returns>The XYChart object representing indicator chart.</returns>
Protected Function addIndicator(m As FinanceChart, indicator As String, height As Integer) As _
    XYChart

    If indicator = "RSI" Then
        Return m.addRSI(height, 14, &H800080, 20, &Hff6666, &H6666ff)
    ElseIf indicator = "StochRSI" Then
        Return m.addStochRSI(height, 14, &H800080, 30, &Hff6666, &H6666ff)
    ElseIf indicator = "MACD" Then
        Return m.addMACD(height, 26, 12, 9, &H0000ff, &Hff00ff, &H008000)
    ElseIf indicator = "FStoch" Then
        Return m.addFastStochastic(height, 14, 3, &H006060, &H606000)
    ElseIf indicator = "SStoch" Then
        Return m.addSlowStochastic(height, 14, 3, &H006060, &H606000)
    ElseIf indicator = "ATR" Then
        Return m.addATR(height, 14, &H808080, &H0000ff)
    ElseIf indicator = "ADX" Then
        Return m.addADX(height, 14, &H008000, &H800000, &H000080)
    ElseIf indicator = "DCW" Then
        Return m.addDonchianWidth(height, 20, &H0000ff)
    ElseIf indicator = "BBW" Then
        Return m.addBollingerWidth(height, 20, 2, &H0000ff)
    ElseIf indicator = "DPO" Then
        Return m.addDPO(height, 20, &H0000ff)
    ElseIf indicator = "PVT" Then
        Return m.addPVT(height, &H0000ff)
    ElseIf indicator = "Momentum" Then
        Return m.addMomentum(height, 12, &H0000ff)
    ElseIf indicator = "Performance" Then
        Return m.addPerformance(height, &H0000ff)
    ElseIf indicator = "ROC" Then
        Return m.addROC(height, 12, &H0000ff)
    ElseIf indicator = "OBV" Then
        Return m.addOBV(height, &H0000ff)
    ElseIf indicator = "AccDist" Then
        Return m.addAccDist(height, &H0000ff)
    ElseIf indicator = "CLV" Then
        Return m.addCLV(height, &H0000ff)
    ElseIf indicator = "WilliamR" Then
        Return m.addWilliamR(height, 14, &H800080, 30, &Hff6666, &H6666ff)
    ElseIf indicator = "Aroon" Then
        Return m.addAroon(height, 14, &H339933, &H333399)
    ElseIf indicator = "AroonOsc" Then
        Return m.addAroonOsc(height, 14, &H0000ff)
    ElseIf indicator = "CCI" Then
        Return m.addCCI(height, 20, &H800080, 100, &Hff6666, &H6666ff)
    ElseIf indicator = "EMV" Then
        Return m.addEaseOfMovement(height, 9, &H006060, &H606000)
    ElseIf indicator = "MDX" Then
        Return m.addMassIndex(height, &H800080, &Hff6666, &H6666ff)
    ElseIf indicator = "CVolatility" Then
        Return m.addChaikinVolatility(height, 10, 10, &H0000ff)
    ElseIf indicator = "COscillator" Then
        Return m.addChaikinOscillator(height, &H0000ff)
    ElseIf indicator = "CMF" Then
        Return m.addChaikinMoneyFlow(height, 21, &H008000)
    ElseIf indicator = "NVI" Then
        Return m.addNVI(height, 255, &H0000ff, &H883333)
    ElseIf indicator = "PVI" Then
        Return m.addPVI(height, 255, &H0000ff, &H883333)
    ElseIf indicator = "MFI" Then
        Return m.addMFI(height, 14, &H800080, 30, &Hff6666, &H6666ff)
    ElseIf indicator = "PVO" Then
        Return m.addPVO(height, 26, 12, 9, &H0000ff, &Hff00ff, &H008000)
    ElseIf indicator = "PPO" Then
        Return m.addPPO(height, 26, 12, 9, &H0000ff, &Hff00ff, &H008000)
    ElseIf indicator = "UO" Then
        Return m.addUltimateOscillator(height, 7, 14, 28, &H800080, 20, &Hff6666, &H6666ff)
    ElseIf indicator = "Vol" Then
        Return m.addVolIndicator(height, &H99ff99, &Hff9999, &Hc0c0c0)
    ElseIf indicator = "TRIX" Then
        Return m.addTRIX(height, 12, &H0000ff)
    End If
    Return Nothing

End Function

'/ <summary>
'/ Creates a dummy chart to show an error message.
'/ </summary>
'/ <param name="msg">The error message.
'/ <returns>The BaseChart object containing the error message.</returns>
Protected Function errMsg(msg As String) As BaseChart

    Dim m As MultiChart = New MultiChart(400, 200)
    m.addTitle2(Chart.Center, msg, "Arial", 10).setMaxWidth(m.getWidth())
    Return m

End Function

'
' Page Load event handler
'
Private Sub Page_Load(sender As System.Object, e As System.EventArgs)

    ' Create the finance chart
    Dim c As BaseChart = drawChart()

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

End Sub

</script>

<!DOCTYPE html>
<html>
<head>
<title>ChartDirector Financial Chart Demonstration</title>
<style>
.inputtitle {font:11px verdana; margin:10px 5px;}
.input {font:11px verdana}
</style>
</head>
<body style="margin:0px">
<table style="border:0px; padding:0px; border-spacing:0px;">
    <tr>
        <td align="right" colspan="2" style="background:#000088">
            <div style="padding:0px 3px 2px 0px; font:bold italic 10pt arial;">
                <a style="color:#ffff00; text-decoration:none" href="http://www.advsofteng.com">
                    Advanced Software Engineering
                </a>
            </div>
        </td>
    </tr>
    <tr valign="top">
        <td style="width:150px; background:#bbddff">
            <form id="Form1" method="post" runat="server">
            <div class="inputtitle">
                <b>Ticker Symbol</b><br />
                <asp:TextBox runat="server" id="TickerSymbol" class="input" style="width:140px;">ASE.SYMBOL</asp:TextBox>
            </div>
            <div class="inputtitle">
                <b>Compare With</b><br />
                <asp:TextBox runat="server" id="CompareWith" class="input" style="width:140px;"></asp:TextBox>
            </div>
            <div class="inputtitle">
                <b>Time Period</b><br />
                <asp:DropDownList runat="server" id="TimeRange" class="input" style="width:140px;">
                    <asp:ListItem value="1">1 day</asp:ListItem>
                    <asp:ListItem value="2">2 days</asp:ListItem>
                    <asp:ListItem value="5">5 days</asp:ListItem>
                    <asp:ListItem value="10">10 days</asp:ListItem>
                    <asp:ListItem value="30">1 month</asp:ListItem>
                    <asp:ListItem value="60">2 months</asp:ListItem>
                    <asp:ListItem value="90">3 months</asp:ListItem>
                    <asp:ListItem value="180" Selected="True">6 months</asp:ListItem>
                    <asp:ListItem value="360">1 year</asp:ListItem>
                    <asp:ListItem value="720">2 years</asp:ListItem>
                    <asp:ListItem value="1080">3 years</asp:ListItem>
                    <asp:ListItem value="1440">4 years</asp:ListItem>
                    <asp:ListItem value="1800">5 years</asp:ListItem>
                    <asp:ListItem value="3600">10 years</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="inputtitle">
                <b>Chart Size</b><br />
                <asp:DropDownList runat="server" id="ChartSize" class="input" style="width:140px;">
                    <asp:ListItem value="S">Small</asp:ListItem>
                    <asp:ListItem value="M">Medium</asp:ListItem>
                    <asp:ListItem value="L" Selected="True">Large</asp:ListItem>
                    <asp:ListItem value="H">Huge</asp:ListItem>
                </asp:DropDownList><br />
            </div>
            <div class="inputtitle">
                <asp:CheckBox runat="server" Text="Show Volume Bars" id="Volume" Checked="True"></asp:CheckBox><br />
                <asp:CheckBox runat="server" Text="Parabolic SAR" id="ParabolicSAR"></asp:CheckBox><br />
                <asp:CheckBox runat="server" Text="Log Scale" id="LogScale"></asp:CheckBox><br />
                <asp:CheckBox runat="server" Text="Percentage Scale" id="PercentageScale"></asp:CheckBox><br />
            </div>
            <div class="inputtitle">
                <b>Chart Type</b><br />
                <asp:DropDownList runat="server" id="ChartType" class="input" style="width:140px;">
                    <asp:ListItem value="None">None</asp:ListItem>
                    <asp:ListItem value="CandleStick" Selected="True">CandleStick</asp:ListItem>
                    <asp:ListItem value="Close">Closing Price</asp:ListItem>
                    <asp:ListItem value="Median">Median Price</asp:ListItem>
                    <asp:ListItem value="OHLC">OHLC</asp:ListItem>
                    <asp:ListItem value="TP">Typical Price</asp:ListItem>
                    <asp:ListItem value="WC">Weighted Close</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="inputtitle">
                <b>Price Band</b><br />
                <asp:DropDownList runat="server" id="Band" class="input" style="width:140px;">
                    <asp:ListItem value="None">None</asp:ListItem>
                    <asp:ListItem value="BB" Selected="True">Bollinger Band</asp:ListItem>
                    <asp:ListItem value="DC">Donchian Channel</asp:ListItem>
                    <asp:ListItem value="Envelop">Envelop (SMA 20 +/- 10%)</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="inputtitle">
                <b>Moving Averages</b><br />
                <asp:DropDownList runat="server" id="avgType1" class="input" style="width:105px;">
                    <asp:ListItem value="None">None</asp:ListItem>
                    <asp:ListItem value="SMA" Selected="True">Simple</asp:ListItem>
                    <asp:ListItem value="EMA">Exponential</asp:ListItem>
                    <asp:ListItem value="TMA">Triangular</asp:ListItem>
                    <asp:ListItem value="WMA">Weighted</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox runat="server" id="movAvg1" class="input" style="width:30px;">10</asp:TextBox><br />
                <asp:DropDownList runat="server" id="avgType2" class="input" style="width:105px;">
                    <asp:ListItem value="None">None</asp:ListItem>
                    <asp:ListItem value="SMA" Selected="True">Simple</asp:ListItem>
                    <asp:ListItem value="EMA">Exponential</asp:ListItem>
                    <asp:ListItem value="TMA">Triangular</asp:ListItem>
                    <asp:ListItem value="WMA">Weighted</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox runat="server" id="movAvg2" class="input" style="width:30px;">25</asp:TextBox><br />
            </div>
            <div class="inputtitle">
                <b>Technical Indicators</b><br />
                <asp:DropDownList runat="server" id="Indicator1" class="input" style="width:140px;">
                    <asp:ListItem value="None">None</asp:ListItem>
                    <asp:ListItem value="AccDist">Accumulation/Distribution</asp:ListItem>
                    <asp:ListItem value="AroonOsc">Aroon Oscillator</asp:ListItem>
                    <asp:ListItem value="Aroon">Aroon Up/Down</asp:ListItem>
                    <asp:ListItem value="ADX">Avg Directional Index</asp:ListItem>
                    <asp:ListItem value="ATR">Avg True Range</asp:ListItem>
                    <asp:ListItem value="BBW">Bollinger Band Width</asp:ListItem>
                    <asp:ListItem value="CMF">Chaikin Money Flow</asp:ListItem>
                    <asp:ListItem value="COscillator">Chaikin Oscillator</asp:ListItem>
                    <asp:ListItem value="CVolatility">Chaikin Volatility</asp:ListItem>
                    <asp:ListItem value="CLV">Close Location Value</asp:ListItem>
                    <asp:ListItem value="CCI">Commodity Channel Index</asp:ListItem>
                    <asp:ListItem value="DPO">Detrended Price Osc</asp:ListItem>
                    <asp:ListItem value="DCW">Donchian Channel Width</asp:ListItem>
                    <asp:ListItem value="EMV">Ease of Movement</asp:ListItem>
                    <asp:ListItem value="FStoch">Fast Stochastic</asp:ListItem>
                    <asp:ListItem value="MACD">MACD</asp:ListItem>
                    <asp:ListItem value="MDX">Mass Index</asp:ListItem>
                    <asp:ListItem value="Momentum">Momentum</asp:ListItem>
                    <asp:ListItem value="MFI">Money Flow Index</asp:ListItem>
                    <asp:ListItem value="NVI">Neg Volume Index</asp:ListItem>
                    <asp:ListItem value="OBV">On Balance Volume</asp:ListItem>
                    <asp:ListItem value="Performance">Performance</asp:ListItem>
                    <asp:ListItem value="PPO">% Price Oscillator</asp:ListItem>
                    <asp:ListItem value="PVO">% Volume Oscillator</asp:ListItem>
                    <asp:ListItem value="PVI">Pos Volume Index</asp:ListItem>
                    <asp:ListItem value="PVT">Price Volume Trend</asp:ListItem>
                    <asp:ListItem value="ROC">Rate of Change</asp:ListItem>
                    <asp:ListItem value="RSI" Selected="True">RSI</asp:ListItem>
                    <asp:ListItem value="SStoch">Slow Stochastic</asp:ListItem>
                    <asp:ListItem value="StochRSI">StochRSI</asp:ListItem>
                    <asp:ListItem value="TRIX">TRIX</asp:ListItem>
                    <asp:ListItem value="UO">Ultimate Oscillator</asp:ListItem>
                    <asp:ListItem value="Vol">Volume</asp:ListItem>
                    <asp:ListItem value="WilliamR">William's %R</asp:ListItem>
                </asp:DropDownList><br />
                <asp:DropDownList runat="server" id="Indicator2" class="input" style="width:140px;">
                    <asp:ListItem value="None">None</asp:ListItem>
                    <asp:ListItem value="AccDist">Accumulation/Distribution</asp:ListItem>
                    <asp:ListItem value="AroonOsc">Aroon Oscillator</asp:ListItem>
                    <asp:ListItem value="Aroon">Aroon Up/Down</asp:ListItem>
                    <asp:ListItem value="ADX">Avg Directional Index</asp:ListItem>
                    <asp:ListItem value="ATR">Avg True Range</asp:ListItem>
                    <asp:ListItem value="BBW">Bollinger Band Width</asp:ListItem>
                    <asp:ListItem value="CMF">Chaikin Money Flow</asp:ListItem>
                    <asp:ListItem value="COscillator">Chaikin Oscillator</asp:ListItem>
                    <asp:ListItem value="CVolatility">Chaikin Volatility</asp:ListItem>
                    <asp:ListItem value="CLV">Close Location Value</asp:ListItem>
                    <asp:ListItem value="CCI">Commodity Channel Index</asp:ListItem>
                    <asp:ListItem value="DPO">Detrended Price Osc</asp:ListItem>
                    <asp:ListItem value="DCW">Donchian Channel Width</asp:ListItem>
                    <asp:ListItem value="EMV">Ease of Movement</asp:ListItem>
                    <asp:ListItem value="FStoch">Fast Stochastic</asp:ListItem>
                    <asp:ListItem value="MACD" Selected="True">MACD</asp:ListItem>
                    <asp:ListItem value="MDX">Mass Index</asp:ListItem>
                    <asp:ListItem value="Momentum">Momentum</asp:ListItem>
                    <asp:ListItem value="MFI">Money Flow Index</asp:ListItem>
                    <asp:ListItem value="NVI">Neg Volume Index</asp:ListItem>
                    <asp:ListItem value="OBV">On Balance Volume</asp:ListItem>
                    <asp:ListItem value="Performance">Performance</asp:ListItem>
                    <asp:ListItem value="PPO">% Price Oscillator</asp:ListItem>
                    <asp:ListItem value="PVO">% Volume Oscillator</asp:ListItem>
                    <asp:ListItem value="PVI">Pos Volume Index</asp:ListItem>
                    <asp:ListItem value="PVT">Price Volume Trend</asp:ListItem>
                    <asp:ListItem value="ROC">Rate of Change</asp:ListItem>
                    <asp:ListItem value="RSI">RSI</asp:ListItem>
                    <asp:ListItem value="SStoch">Slow Stochastic</asp:ListItem>
                    <asp:ListItem value="StochRSI">StochRSI</asp:ListItem>
                    <asp:ListItem value="TRIX">TRIX</asp:ListItem>
                    <asp:ListItem value="UO">Ultimate Oscillator</asp:ListItem>
                    <asp:ListItem value="Vol">Volume</asp:ListItem>
                    <asp:ListItem value="WilliamR">William's %R</asp:ListItem>
                </asp:DropDownList><br />
                <asp:DropDownList runat="server" id="Indicator3" class="input" style="width:140px;">
                    <asp:ListItem value="None" Selected="True">None</asp:ListItem>
                    <asp:ListItem value="AccDist">Accumulation/Distribution</asp:ListItem>
                    <asp:ListItem value="AroonOsc">Aroon Oscillator</asp:ListItem>
                    <asp:ListItem value="Aroon">Aroon Up/Down</asp:ListItem>
                    <asp:ListItem value="ADX">Avg Directional Index</asp:ListItem>
                    <asp:ListItem value="ATR">Avg True Range</asp:ListItem>
                    <asp:ListItem value="BBW">Bollinger Band Width</asp:ListItem>
                    <asp:ListItem value="CMF">Chaikin Money Flow</asp:ListItem>
                    <asp:ListItem value="COscillator">Chaikin Oscillator</asp:ListItem>
                    <asp:ListItem value="CVolatility">Chaikin Volatility</asp:ListItem>
                    <asp:ListItem value="CLV">Close Location Value</asp:ListItem>
                    <asp:ListItem value="CCI">Commodity Channel Index</asp:ListItem>
                    <asp:ListItem value="DPO">Detrended Price Osc</asp:ListItem>
                    <asp:ListItem value="DCW">Donchian Channel Width</asp:ListItem>
                    <asp:ListItem value="EMV">Ease of Movement</asp:ListItem>
                    <asp:ListItem value="FStoch">Fast Stochastic</asp:ListItem>
                    <asp:ListItem value="MACD">MACD</asp:ListItem>
                    <asp:ListItem value="MDX">Mass Index</asp:ListItem>
                    <asp:ListItem value="Momentum">Momentum</asp:ListItem>
                    <asp:ListItem value="MFI">Money Flow Index</asp:ListItem>
                    <asp:ListItem value="NVI">Neg Volume Index</asp:ListItem>
                    <asp:ListItem value="OBV">On Balance Volume</asp:ListItem>
                    <asp:ListItem value="Performance">Performance</asp:ListItem>
                    <asp:ListItem value="PPO">% Price Oscillator</asp:ListItem>
                    <asp:ListItem value="PVO">% Volume Oscillator</asp:ListItem>
                    <asp:ListItem value="PVI">Pos Volume Index</asp:ListItem>
                    <asp:ListItem value="PVT">Price Volume Trend</asp:ListItem>
                    <asp:ListItem value="ROC">Rate of Change</asp:ListItem>
                    <asp:ListItem value="RSI">RSI</asp:ListItem>
                    <asp:ListItem value="SStoch">Slow Stochastic</asp:ListItem>
                    <asp:ListItem value="StochRSI">StochRSI</asp:ListItem>
                    <asp:ListItem value="TRIX">TRIX</asp:ListItem>
                    <asp:ListItem value="UO">Ultimate Oscillator</asp:ListItem>
                    <asp:ListItem value="Vol">Volume</asp:ListItem>
                    <asp:ListItem value="WilliamR">William's %R</asp:ListItem>
                </asp:DropDownList><br />
                <asp:DropDownList runat="server" id="Indicator4" class="input" style="width:140px;">
                    <asp:ListItem value="None" Selected="True">None</asp:ListItem>
                    <asp:ListItem value="AccDist">Accumulation/Distribution</asp:ListItem>
                    <asp:ListItem value="AroonOsc">Aroon Oscillator</asp:ListItem>
                    <asp:ListItem value="Aroon">Aroon Up/Down</asp:ListItem>
                    <asp:ListItem value="ADX">Avg Directional Index</asp:ListItem>
                    <asp:ListItem value="ATR">Avg True Range</asp:ListItem>
                    <asp:ListItem value="BBW">Bollinger Band Width</asp:ListItem>
                    <asp:ListItem value="CMF">Chaikin Money Flow</asp:ListItem>
                    <asp:ListItem value="COscillator">Chaikin Oscillator</asp:ListItem>
                    <asp:ListItem value="CVolatility">Chaikin Volatility</asp:ListItem>
                    <asp:ListItem value="CLV">Close Location Value</asp:ListItem>
                    <asp:ListItem value="CCI">Commodity Channel Index</asp:ListItem>
                    <asp:ListItem value="DPO">Detrended Price Osc</asp:ListItem>
                    <asp:ListItem value="DCW">Donchian Channel Width</asp:ListItem>
                    <asp:ListItem value="EMV">Ease of Movement</asp:ListItem>
                    <asp:ListItem value="FStoch">Fast Stochastic</asp:ListItem>
                    <asp:ListItem value="MACD">MACD</asp:ListItem>
                    <asp:ListItem value="MDX">Mass Index</asp:ListItem>
                    <asp:ListItem value="Momentum">Momentum</asp:ListItem>
                    <asp:ListItem value="MFI">Money Flow Index</asp:ListItem>
                    <asp:ListItem value="NVI">Neg Volume Index</asp:ListItem>
                    <asp:ListItem value="OBV">On Balance Volume</asp:ListItem>
                    <asp:ListItem value="Performance">Performance</asp:ListItem>
                    <asp:ListItem value="PPO">% Price Oscillator</asp:ListItem>
                    <asp:ListItem value="PVO">% Volume Oscillator</asp:ListItem>
                    <asp:ListItem value="PVI">Pos Volume Index</asp:ListItem>
                    <asp:ListItem value="PVT">Price Volume Trend</asp:ListItem>
                    <asp:ListItem value="ROC">Rate of Change</asp:ListItem>
                    <asp:ListItem value="RSI">RSI</asp:ListItem>
                    <asp:ListItem value="SStoch">Slow Stochastic</asp:ListItem>
                    <asp:ListItem value="StochRSI">StochRSI</asp:ListItem>
                    <asp:ListItem value="TRIX">TRIX</asp:ListItem>
                    <asp:ListItem value="UO">Ultimate Oscillator</asp:ListItem>
                    <asp:ListItem value="Vol">Volume</asp:ListItem>
                    <asp:ListItem value="WilliamR">William's %R</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="inputtitle" style="text-align:center">
                <asp:Button runat="server" id="Button1" class="input" Text="Update Chart"></asp:Button>
            </div>
            </form>
        </td>
        <td>
            <div style="font:bold 20pt arial; margin:5px 0px 0px 5px">
                ChartDirector Financial Chart Demonstration
            </div>
            <hr style="border:solid 1px #000080" />
            <br />
            <chart:webchartviewer id="WebChartViewer1" runat="server" ImageAlign="Top" />
        </td>
    </tr>
</table>
</body>
</html>
