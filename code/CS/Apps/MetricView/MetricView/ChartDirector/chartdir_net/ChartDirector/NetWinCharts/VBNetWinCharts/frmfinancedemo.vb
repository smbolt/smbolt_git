Imports ChartDirector

Public Class FrmFinanceDemo

    ' <summary>
    ' A utility class for adding items to ComboBox
    ' </summary>
    Class ListItem
        Dim m_key As String
        Dim m_value As String

        Public Sub New(ByVal k As String, ByVal v As String)
            m_key = k
            m_value = v
        End Sub

        Public ReadOnly Property Key() As String
            Get
                Return m_key
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return m_value
        End Function
    End Class

    ' Will set to true at the end of initialization - prevents events from firing before the
    ' controls are properly initialized.
    Private hasFinishedInitialization As Boolean

    ' The ticker symbol, timeStamps, volume, high, low, open and close data    
    Private tickerKey As String
    Private timeStamps As Date()
    Private volData As Double()
    Private highData As Double()
    Private lowData As Double()
    Private openData As Double()
    Private closeData As Double()

    ' An extra data series to compare with the close data
    Private compareKey As String
    Private compareData As Double()

    ' The resolution of the data in seconds. 1 day = 86400 seconds.
    Private resolution As Integer = 86400

    ' <summary>
    ' Form Load event handler - initialize the form
    ' </summary>
    Private Sub FrmFinanceDemo_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        timeRange.Items.Add(New ListItem("1", "1 day"))
        timeRange.Items.Add(New ListItem("2", "2 days"))
        timeRange.Items.Add(New ListItem("5", "5 days"))
        timeRange.Items.Add(New ListItem("10", "10 days"))
        timeRange.Items.Add(New ListItem("30", "1 month"))
        timeRange.Items.Add(New ListItem("60", "2 months"))
        timeRange.Items.Add(New ListItem("90", "3 months"))
        timeRange.SelectedIndex = timeRange.Items.Add(New ListItem("180", "6 months"))
        timeRange.Items.Add(New ListItem("360", "1 year"))
        timeRange.Items.Add(New ListItem("720", "2 years"))
        timeRange.Items.Add(New ListItem("1080", "3 years"))
        timeRange.Items.Add(New ListItem("1440", "4 years"))
        timeRange.Items.Add(New ListItem("1800", "5 years"))
        timeRange.Items.Add(New ListItem("3600", "10 years"))

        chartSize.Items.Add(New ListItem("S", "Small"))
        chartSize.Items.Add(New ListItem("M", "Medium"))
        chartSize.SelectedIndex = chartSize.Items.Add(New ListItem("L", "Large"))
        chartSize.Items.Add(New ListItem("H", "Huge"))

        chartType.Items.Add(New ListItem("None", "None"))
        chartType.SelectedIndex = chartType.Items.Add(New ListItem("CandleStick", "CandleStick"))
        chartType.Items.Add(New ListItem("Close", "Closing Price"))
        chartType.Items.Add(New ListItem("Median", "Median Price"))
        chartType.Items.Add(New ListItem("OHLC", "OHLC"))
        chartType.Items.Add(New ListItem("TP", "Typical Price"))
        chartType.Items.Add(New ListItem("WC", "Weighted Close"))

        priceBand.Items.Add(New ListItem("None", "None"))
        priceBand.SelectedIndex = priceBand.Items.Add(New ListItem("BB", "Bollinger Band"))
        priceBand.Items.Add(New ListItem("DC", "Donchain Channel"))
        priceBand.Items.Add(New ListItem("Envelop", "Envelop (SMA 20 +/- 10%)"))

        avgType1.Items.Add(New ListItem("None", "None"))
        avgType1.SelectedIndex = avgType1.Items.Add(New ListItem("SMA", "Simple"))
        avgType1.Items.Add(New ListItem("EMA", "Exponential"))
        avgType1.Items.Add(New ListItem("TMA", "Triangular"))
        avgType1.Items.Add(New ListItem("WMA", "Weighted"))

        avgType2.Items.Add(New ListItem("None", "None"))
        avgType2.SelectedIndex = avgType2.Items.Add(New ListItem("SMA", "Simple"))
        avgType2.Items.Add(New ListItem("EMA", "Exponential"))
        avgType2.Items.Add(New ListItem("TMA", "Triangular"))
        avgType2.Items.Add(New ListItem("WMA", "Weighted"))

        Dim indicators() As ListItem = _
        { _
            New ListItem("None", "None"), _
            New ListItem("AccDist", "Accumulation/Distribution"), _
            New ListItem("AroonOsc", "Aroon Oscillator"), _
            New ListItem("Aroon", "Aroon Up/Down"), _
            New ListItem("ADX", "Avg Directional Index"), _
            New ListItem("ATR", "Avg True Range"), _
            New ListItem("BBW", "Bollinger Band Width"), _
            New ListItem("CMF", "Chaikin Money Flow"), _
            New ListItem("COscillator", "Chaikin Oscillator"), _
            New ListItem("CVolatility", "Chaikin Volatility"), _
            New ListItem("CLV", "Close Location Value"), _
            New ListItem("CCI", "Commodity Channel Index"), _
            New ListItem("DPO", "Detrended Price Osc"), _
            New ListItem("DCW", "Donchian Channel Width"), _
            New ListItem("EMV", "Ease of Movement"), _
            New ListItem("FStoch", "Fast Stochastic"), _
            New ListItem("MACD", "MACD"), _
            New ListItem("MDX", "Mass Index"), _
            New ListItem("Momentum", "Momentum"), _
            New ListItem("MFI", "Money Flow Index"), _
            New ListItem("NVI", "Neg Volume Index"), _
            New ListItem("OBV", "On Balance Volume"), _
            New ListItem("Performance", "Performance"), _
            New ListItem("PPO", "% Price Oscillator"), _
            New ListItem("PVO", "% Volume Oscillator"), _
            New ListItem("PVI", "Pos Volume Index"), _
            New ListItem("PVT", "Price Volume Trend"), _
            New ListItem("ROC", "Rate of Change"), _
            New ListItem("RSI", "RSI"), _
            New ListItem("SStoch", "Slow Stochastic"), _
            New ListItem("StochRSI", "StochRSI"), _
            New ListItem("TRIX", "TRIX"), _
            New ListItem("UO", "Ultimate Oscillator"), _
            New ListItem("Vol", "Volume"), _
            New ListItem("WilliamR", "William's %R") _
        }

        indicator1.Items.AddRange(indicators)
        indicator2.Items.AddRange(indicators)
        indicator3.Items.AddRange(indicators)
        indicator4.Items.AddRange(indicators)

        Dim i As Integer
        For i = 0 To UBound(indicators)
            If indicators(i).Key = "RSI" Then
                indicator1.SelectedIndex = i
            ElseIf indicators(i).Key = "MACD" Then
                indicator2.SelectedIndex = i
            End If
        Next

        indicator3.SelectedIndex = 0
        indicator4.SelectedIndex = 0

        hasFinishedInitialization = True
        drawChart(winChartViewer1)

    End Sub

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
    Protected Function getData(ByVal ticker As String, ByVal startDate As Date, ByVal endDate As Date, _
        ByVal durationInDays As Integer, ByVal extraPoints As Integer) As Boolean

        ' This method should return false if the ticker symbol is invalid. In this sample
        ' code, as we are using a random number generator for the data, all ticker symbol
        ' is allowed, but we still assumed an empty symbol is invalid.
        If ticker = "" Then
            Return False
        End If

        ' In this demo, we can get 15 min, daily, weekly or monthly data depending on the
        ' time range.
        resolution = 86400
        If durationInDays <= 10 Then
            ' 10 days or less, we assume 15 minute data points are available
            resolution = 900

            ' We need to adjust the startDate backwards for the extraPoints. We assume
            ' 6.5 hours trading time per day, and 5 trading days per week.
            Dim dataPointsPerDay As Double = 6.5 * 3600 / resolution
            Dim adjustedStartDate As Date = startDate.AddDays(-Math.Ceiling(extraPoints _
                 / dataPointsPerDay * 7 / 5) - 2)

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

            ' Adjust startDate backwards to cater for extraPoints. We multiply the days
            ' by 7/5 as we assume 1 week has 5 trading days.
            Dim adjustedStartDate As Date = startDate.Date.AddDays(-Math.Ceiling( _
                extraPoints * 7.0 / 5) - 2)

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
    Protected Sub get15MinData(ByVal ticker As String, ByVal startDate As Date, ByVal endDate As Date)

        '
        ' In this demo, we use a random number generator to generate the data. In
        ' practice, you may get the data from a database or by other means. If you do not
        ' have 15 minute data, you may modify the "drawChart" method below to not using
        ' 15 minute data.
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
    Protected Sub getDailyData(ByVal ticker As String, ByVal startDate As Date, ByVal endDate As Date)

        '
        ' In this demo, we use a random number generator to generate the data. In
        ' practice, you may get the data from a database or by other means.
        '
        ' A typical database code example is like below. (This only shows a general idea.
        ' The exact details may differ depending on your database brand and schema. The
        ' SQL, in particular the date format, may be different depending on which brand
        ' of database you use.)
        '
        '	' Open the database connection to MS SQL
        '	Dim dbconn As System.Data.IDbConnection = New System.Data.SqlClient.SqlConnection(
        '	      "..... put your database connection string here .......")
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
    Protected Sub getWeeklyData(ByVal ticker As String, ByVal startDate As Date, ByVal endDate As Date)

        '
        ' If you do not have weekly data, you may call "getDailyData(startDate, endDate)"
        ' to get daily data, then call "convertDailyToWeeklyData()" to convert to weekly
        ' data.
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
    Protected Sub getMonthlyData(ByVal ticker As String, ByVal startDate As Date, ByVal endDate As Date)

        '
        ' If you do not have weekly data, you may call "getDailyData(startDate, endDate)"
        ' to get daily data, then call "convertDailyToMonthlyData()" to convert to
        ' monthly data.
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
    Protected Sub generateRandomData(ByVal ticker As String, ByVal startDate As Date, _
        ByVal endDate As Date, ByVal resolution As Integer)

        Dim db As FinanceSimulator = New FinanceSimulator(ticker, startDate, endDate, _
            resolution)
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
    Protected Sub aggregateData(ByVal aggregator As ArrayMath)

        timeStamps = Chart.NTime(aggregator.aggregate(Chart.CTime(timeStamps), _
            Chart.AggregateFirst))
        highData = aggregator.aggregate(highData, Chart.AggregateMax)
        lowData = aggregator.aggregate(lowData, Chart.AggregateMin)
        openData = aggregator.aggregate(openData, Chart.AggregateFirst)
        closeData = aggregator.aggregate(closeData, Chart.AggregateLast)
        volData = aggregator.aggregate(volData, Chart.AggregateSum)

    End Sub

    ' <summary>
    ' In this sample code, the chart updates when the user selection changes. You may 
    ' modify the code to update the data and chart periodically for real time charts.
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles timeRange.SelectedIndexChanged, chartSize.SelectedIndexChanged, _
        volumeBars.CheckedChanged, parabolicSAR.CheckedChanged, logScale.CheckedChanged, _
        percentageScale.CheckedChanged, chartType.SelectedIndexChanged, _
        priceBand.SelectedIndexChanged, avgType1.SelectedIndexChanged, movAvg1.TextChanged, _
        avgType2.SelectedIndexChanged, movAvg2.TextChanged, indicator1.SelectedIndexChanged, _
        indicator2.SelectedIndexChanged, indicator3.SelectedIndexChanged, _
        indicator4.SelectedIndexChanged

        If hasFinishedInitialization Then
            drawChart(winChartViewer1)
        End If

    End Sub

    '
    ' For the ticker symbols, the chart will update when the user enters a new symbol,
    ' and then press the enter button or leave the text box.
    '

    Private Sub tickerSymbol_Leave(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles tickerSymbol.Leave

        'User leave ticker symbol text box - redraw chart if symbol has changed
        If tickerSymbol.Text <> tickerKey Then
            drawChart(winChartViewer1)
        End If

    End Sub

    Private Sub tickerSymbol_KeyPress(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tickerSymbol.KeyPress

        ' User press enter key - same action as leaving the text box.
        If e.KeyChar = vbCr Then
            tickerSymbol_Leave(sender, e)
        End If

    End Sub

    Private Sub compareWith_Leave(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles compareWith.Leave

        ' User leave compare symbol text box - redraw chart if symbol has changed
        If compareWith.Text <> compareKey Then
            drawChart(winChartViewer1)
        End If

    End Sub

    Private Sub compareWith_KeyPress(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles compareWith.KeyPress

        ' User press enter key - same action as leaving the text box.
        If e.KeyChar = vbCr Then
            compareWith_Leave(sender, e)
        End If

    End Sub

    ' <summary>
    ' Draw the chart according to user selection and display it in the WebChartViewer.
    ' </summary>
    ' <param name="viewer">The WebChartViewer object to display the chart.</param>
    Private Sub drawChart(ByVal viewer As WinChartViewer)

        ' Use InvariantCulture to draw the chart. This ensures the chart will look the
        ' same on any computer.
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            System.Globalization.CultureInfo.InvariantCulture

        ' General index variable used in various For/Next loop
        Dim i As Integer = 0

        ' In this demo, we just assume we plot up to the latest time. So end date is now.
        Dim endDate As Date = Now

        ' If the trading day has not yet started (before 9:30am), or if the end date is
        ' on on Sat or Sun, we set the end date to 4:00pm of the last trading day
        Do While (endDate.TimeOfDay.CompareTo(New TimeSpan(9, 30, 0)) < 0) Or ( _
            endDate.DayOfWeek = DayOfWeek.Sunday) Or (endDate.DayOfWeek = _
            DayOfWeek.Saturday)
            endDate = endDate.Date.AddDays(-1).Add(New TimeSpan(16, 0, 0))
        Loop

        ' The duration selected by the user
        Dim durationInDays As Integer = CInt(timeRange.SelectedItem.Key)

        ' Compute the start date by subtracting the duration from the end date.
        Dim startDate As Date = endDate
        If durationInDays >= 30 Then
            ' More or equal to 30 days - so we use months as the unit
            startDate = New DateTime(endDate.Year, endDate.Month, 1).AddMonths( _
                -durationInDays / 30)
        Else
            ' Less than 30 days - use day as the unit. The starting point of the axis is
            ' always at the start of the day (9:30am). Note that we use trading days, so
            ' we skip Sat and Sun in counting the days.
            startDate = endDate.Date
            For i = 1 To durationInDays - 1
                If startDate.DayOfWeek = DayOfWeek.Monday Then
                    startDate = startDate.AddDays(-3)
                Else
                    startDate = startDate.AddDays(-1)
                End If
            Next
        End If

        ' The moving average periods selected by the user.
        On Error Resume Next
        Dim avgPeriod1 As Integer = CInt(movAvg1.Text)
        Dim avgPeriod2 As Integer = CInt(movAvg2.Text)
        On Error GoTo 0

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
        Dim extraPoints As Integer = Math.Max(20, Math.Max(avgPeriod1, avgPeriod2))

        ' Get the data series to compare with, if any.
        compareKey = Trim(compareWith.Text)
        compareData = Nothing
        If getData(compareKey, startDate, endDate, durationInDays, extraPoints) Then
            compareData = closeData
        End If

        ' The data series we want to get.
        tickerKey = Trim(tickerSymbol.Text)
        If Not getData(tickerKey, startDate, endDate, durationInDays, extraPoints) Then
            errMsg(viewer, "Please enter a valid ticker symbol")
            Return
        End If

        ' We now confirm the actual number of extra points (data points that are before
        ' the start date) as inferred using actual data from the database.
        extraPoints = timeStamps.Length
        For i = 0 To UBound(timeStamps)
            If (timeStamps(i) >= startDate) Then
                extraPoints = i
                Exit For
            End If
        Next

        ' Check if there is any valid data
        If extraPoints >= timeStamps.Length Then
            ' No data - just display the no data message.
            errMsg(viewer, "No data available for the specified time period")
            Return
        End If

        ' In some finance chart presentation style, even if the data for the latest day 
        ' is not fully available, the axis for the entire day will still be drawn, where
        ' no data will appear near the end of the axis.
        If (resolution < 86400) Then
            ' Add extra points to the axis until it reaches the end of the day. The end
            ' of day is assumed to be 4:00pm (it depends on the stock exchange).
            Dim lastTime As Date = timeStamps(UBound(timeStamps))
            Dim extraTrailingPoints As Integer = New TimeSpan(16, 0, 0).Subtract( _
                lastTime.TimeOfDay).TotalSeconds / resolution

            If extraTrailingPoints > 0 Then
                Dim extendedTimeStamps(timeStamps.Length + extraTrailingPoints - 1) As Date
                Array.Copy(timeStamps, 0, extendedTimeStamps, 0, timeStamps.Length)
                For i = 0 To extraTrailingPoints - 1
                    extendedTimeStamps(i + timeStamps.Length) = lastTime.AddSeconds(resolution * i)
                Next
                timeStamps = extendedTimeStamps
            End If
        End If

        '
        ' At this stage, all data is available. We can draw the chart as according to 
        ' user input.
        '

        '
        ' Determine the chart size. In this demo, user can select 4 different chart sizes.
        ' Default is the large chart size.
        '
        Dim width As Integer = 780
        Dim mainHeight As Integer = 255
        Dim indicatorHeight As Integer = 80

        If chartSize.SelectedItem.Key = "S" Then
            ' Small chart size
            width = 450
            mainHeight = 160
            indicatorHeight = 60
        ElseIf chartSize.SelectedItem.Key = "M" Then
            ' Medium chart size
            width = 620
            mainHeight = 215
            indicatorHeight = 70
        ElseIf chartSize.SelectedItem.Key = "H" Then
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
        ' We configure the title of the chart. In this demo chart design, we put the 
        ' company name as the top line of the title with left alignment.
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

        m.addPlotAreaTitle(Chart.BottomLeft, "<*font=Arial,size=8*>" & m.formatValue( _
            Now, "mmm dd, yyyy") & " - " & resolutionText & " chart")

        ' A copyright message at the bottom left corner the title area
        m.addPlotAreaTitle(Chart.BottomRight, _
            "<*font=arial.ttf,size=8*>(c) Advanced Software Engineering")

        '
        ' Add the first techical indicator according. In this demo, we draw the first
        ' indicator on top of the main chart.
        '
        addIndicator(m, indicator1.SelectedItem.Key, indicatorHeight)

        '
        ' Add the main chart
        '
        m.addMainChart(mainHeight)

        '
        ' Set log or linear scale according to user preference
        '
        m.setLogScale(logScale.Checked)

        '
        ' Set axis labels to show data values or percentage change to user preference
        '
        If percentageScale.Checked Then
            m.setPercentageAxis()
        End If

        '
        ' Draw any price line the user has selected
        '
        Dim mainType As String = chartType.SelectedItem.Key
        If mainType = "Close" Then
            m.addCloseLine(&H40)
        ElseIf mainType = "TP" Then
            m.addTypicalPrice(&H40)
        ElseIf mainType = "WC" Then
            m.addWeightedClose(&H40)
        ElseIf mainType = "Median" Then
            m.addMedianPrice(&H40)
        End If

        '
        ' Add comparison line if there is data for comparison
        '
        If Not compareData Is Nothing Then
            If UBound(compareData) + 1 > extraPoints Then
                m.addComparison(compareData, &HFF, compareKey)
            End If
        End If

        '
        ' Add moving average lines.
        '
        addMovingAvg(m, avgType1.SelectedItem.Key, avgPeriod1, &H663300)
        addMovingAvg(m, avgType2.SelectedItem.Key, avgPeriod2, &H9900FF)

        '
        ' Draw candlesticks or OHLC symbols if the user has selected them.
        '
        If mainType = "CandleStick" Then
            m.addCandleStick(&H33FF33, &HFF3333)
        ElseIf mainType = "OHLC" Then
            m.addHLOC(&H8800, &HCC0000)
        End If

        '
        ' Add parabolic SAR if necessary
        '
        If parabolicSAR.Checked Then
            m.addParabolicSAR(0.02, 0.02, 0.2, Chart.DiamondShape, 5, &H8800, &H0)
        End If

        '
        ' Add price band/channel/envelop to the chart according to user selection
        '
        If priceBand.SelectedItem.Key = "BB" Then
            m.addBollingerBand(20, 2, &H9999FF, &HC06666FF)
        ElseIf priceBand.SelectedItem.Key = "DC" Then
            m.addDonchianChannel(20, &H9999FF, &HC06666FF)
        ElseIf priceBand.SelectedItem.Key = "Envelop" Then
            m.addEnvelop(20, 0.1, &H9999FF, &HC06666FF)
        End If

        '
        ' Add volume bars to the main chart if necessary
        '
        If volumeBars.Checked Then
            m.addVolBars(indicatorHeight, &H99FF99, &HFF9999, &HC0C0C0)
        End If

        '
        ' Add additional indicators as according to user selection.
        '
        addIndicator(m, indicator2.SelectedItem.Key, indicatorHeight)
        addIndicator(m, indicator3.SelectedItem.Key, indicatorHeight)
        addIndicator(m, indicator4.SelectedItem.Key, indicatorHeight)

        '
        ' output the chart
        '
        viewer.Chart = m

        '
        ' tooltips for the chart
        '
        viewer.ImageMap = m.getHTMLImageMap("", "", "title='" & m.getToolTipDateFormat() & _
            " {value|P}'")

    End Sub

    '/ <summary>
    '/ Add a moving average line to the FinanceChart object.
    '/ </summary>
    '/ <param name="m">The FinanceChart object to add the line to.</param>
    '/ <param name="avgType">The moving average type (SMA/EMA/TMA/WMA).</param>
    '/ <param name="avgPeriod">The moving average period.</param>
    '/ <param name="color">The color of the line.</param>
    '/ <returns>The LineLayer object representing line layer created.</returns>
    Protected Function addMovingAvg(ByVal m As FinanceChart, ByVal avgType As String, _
        ByVal avgPeriod As Integer, ByVal color As Integer) As LineLayer

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
    Protected Function addIndicator(ByVal m As FinanceChart, ByVal indicator As String, _
        ByVal height As Integer) As XYChart

        If indicator = "RSI" Then
            Return m.addRSI(height, 14, &H800080, 20, &HFF6666, &H6666FF)
        ElseIf indicator = "StochRSI" Then
            Return m.addStochRSI(height, 14, &H800080, 30, &HFF6666, &H6666FF)
        ElseIf indicator = "MACD" Then
            Return m.addMACD(height, 26, 12, 9, &HFF, &HFF00FF, &H8000)
        ElseIf indicator = "FStoch" Then
            Return m.addFastStochastic(height, 14, 3, &H6060, &H606000)
        ElseIf indicator = "SStoch" Then
            Return m.addSlowStochastic(height, 14, 3, &H6060, &H606000)
        ElseIf indicator = "ATR" Then
            Return m.addATR(height, 14, &H808080, &HFF)
        ElseIf indicator = "ADX" Then
            Return m.addADX(height, 14, &H8000, &H800000, &H80)
        ElseIf indicator = "DCW" Then
            Return m.addDonchianWidth(height, 20, &HFF)
        ElseIf indicator = "BBW" Then
            Return m.addBollingerWidth(height, 20, 2, &HFF)
        ElseIf indicator = "DPO" Then
            Return m.addDPO(height, 20, &HFF)
        ElseIf indicator = "PVT" Then
            Return m.addPVT(height, &HFF)
        ElseIf indicator = "Momentum" Then
            Return m.addMomentum(height, 12, &HFF)
        ElseIf indicator = "Performance" Then
            Return m.addPerformance(height, &HFF)
        ElseIf indicator = "ROC" Then
            Return m.addROC(height, 12, &HFF)
        ElseIf indicator = "OBV" Then
            Return m.addOBV(height, &HFF)
        ElseIf indicator = "AccDist" Then
            Return m.addAccDist(height, &HFF)
        ElseIf indicator = "CLV" Then
            Return m.addCLV(height, &HFF)
        ElseIf indicator = "WilliamR" Then
            Return m.addWilliamR(height, 14, &H800080, 30, &HFF6666, &H6666FF)
        ElseIf indicator = "Aroon" Then
            Return m.addAroon(height, 14, &H339933, &H333399)
        ElseIf indicator = "AroonOsc" Then
            Return m.addAroonOsc(height, 14, &HFF)
        ElseIf indicator = "CCI" Then
            Return m.addCCI(height, 20, &H800080, 100, &HFF6666, &H6666FF)
        ElseIf indicator = "EMV" Then
            Return m.addEaseOfMovement(height, 9, &H6060, &H606000)
        ElseIf indicator = "MDX" Then
            Return m.addMassIndex(height, &H800080, &HFF6666, &H6666FF)
        ElseIf indicator = "CVolatility" Then
            Return m.addChaikinVolatility(height, 10, 10, &HFF)
        ElseIf indicator = "COscillator" Then
            Return m.addChaikinOscillator(height, &HFF)
        ElseIf indicator = "CMF" Then
            Return m.addChaikinMoneyFlow(height, 21, &H8000)
        ElseIf indicator = "NVI" Then
            Return m.addNVI(height, 255, &HFF, &H883333)
        ElseIf indicator = "PVI" Then
            Return m.addPVI(height, 255, &HFF, &H883333)
        ElseIf indicator = "MFI" Then
            Return m.addMFI(height, 14, &H800080, 30, &HFF6666, &H6666FF)
        ElseIf indicator = "PVO" Then
            Return m.addPVO(height, 26, 12, 9, &HFF, &HFF00FF, &H8000)
        ElseIf indicator = "PPO" Then
            Return m.addPPO(height, 26, 12, 9, &HFF, &HFF00FF, &H8000)
        ElseIf indicator = "UO" Then
            Return m.addUltimateOscillator(height, 7, 14, 28, &H800080, 20, &HFF6666, _
                &H6666FF)
        ElseIf indicator = "Vol" Then
            Return m.addVolIndicator(height, &H99FF99, &HFF9999, &HC0C0C0)
        ElseIf indicator = "TRIX" Then
            Return m.addTRIX(height, 12, &HFF)
        End If
        Return Nothing

    End Function

    '/ <summary>
    '/ Creates a dummy chart to show an error message.
    '/ </summary>
    '/ <param name="msg">The error message.
    '/ <returns>The BaseChart object containing the error message.</returns>
    Protected Sub errMsg(ByVal viewer As WinChartViewer, ByVal msg As String)

        Dim m As MultiChart = New MultiChart(400, 200)
        m.addTitle2(Chart.Center, msg, "Arial", 10).setMaxWidth(m.getWidth())
        viewer.Image = m.makeImage()

    End Sub

End Class