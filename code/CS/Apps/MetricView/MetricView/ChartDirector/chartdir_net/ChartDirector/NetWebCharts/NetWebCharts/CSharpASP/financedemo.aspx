<%@ Page Language="C#" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<script runat="server">

// The timeStamps, volume, high, low, open and close data
DateTime[] timeStamps = null;
double[] volData = null;
double[] highData = null;
double[] lowData = null;
double[] openData = null;
double[] closeData = null;

// An extra data series to compare with the close data
double[] compareData = null;

// The resolution of the data in seconds. 1 day = 86400 seconds.
int resolution = 86400;

/// <summary>
/// Get the timeStamps, highData, lowData, openData, closeData and volData.
/// </summary>
/// <param name="ticker">The ticker symbol for the data series.</param>
/// <param name="startDate">The starting date/time for the data series.</param>
/// <param name="endDate">The ending date/time for the data series.</param>
/// <param name="durationInDays">The number of trading days to get.</param>
/// <param name="extraPoints">The extra leading data points needed in order to
/// compute moving averages.</param>
/// <returns>True if successfully obtain the data, otherwise false.</returns>
protected bool getData(string ticker, DateTime startDate, DateTime endDate, int durationInDays,
    int extraPoints)
{
    // This method should return false if the ticker symbol is invalid. In this sample code, as we
    // are using a random number generator for the data, all ticker symbol is allowed, but we still
    // assumed an empty symbol is invalid.
    if (ticker == "") {
        return false;
    }

    // In this demo, we can get 15 min, daily, weekly or monthly data depending on the time range.
    resolution = 86400;
    if (durationInDays <= 10) {
        // 10 days or less, we assume 15 minute data points are available
        resolution = 900;

        // We need to adjust the startDate backwards for the extraPoints. We assume 6.5 hours
        // trading time per day, and 5 trading days per week.
        double dataPointsPerDay = 6.5 * 3600 / resolution;
        DateTime adjustedStartDate = startDate.AddDays(-Math.Ceiling(extraPoints / dataPointsPerDay
             * 7 / 5) - 2);

        // Get the required 15 min data
        get15MinData(ticker, adjustedStartDate, endDate);

    } else if (durationInDays >= 4.5 * 360) {
        // 4 years or more - use monthly data points.
        resolution = 30 * 86400;

        // Adjust startDate backwards to cater for extraPoints
        DateTime adjustedStartDate = startDate.Date.AddMonths(-extraPoints);

        // Get the required monthly data
        getMonthlyData(ticker, adjustedStartDate, endDate);

    } else if (durationInDays >= 1.5 * 360) {
        // 1 year or more - use weekly points.
        resolution = 7 * 86400;

        // Adjust startDate backwards to cater for extraPoints
        DateTime adjustedStartDate = startDate.Date.AddDays(-extraPoints * 7 - 6);

        // Get the required weekly data
        getWeeklyData(ticker, adjustedStartDate, endDate);

    } else {
        // Default - use daily points
        resolution = 86400;

        // Adjust startDate backwards to cater for extraPoints. We multiply the days by 7/5 as we
        // assume 1 week has 5 trading days.
        DateTime adjustedStartDate = startDate.Date.AddDays(-Math.Ceiling(extraPoints * 7.0 / 5) - 2
            );

        // Get the required daily data
        getDailyData(ticker, adjustedStartDate, endDate);
    }

    return true;
}

/// <summary>
/// Get 15 minutes data series for timeStamps, highData, lowData, openData, closeData
/// and volData.
/// </summary>
/// <param name="ticker">The ticker symbol for the data series.</param>
/// <param name="startDate">The starting date/time for the data series.</param>
/// <param name="endDate">The ending date/time for the data series.</param>
protected void get15MinData(string ticker, DateTime startDate, DateTime endDate)
{
    //
    // In this demo, we use a random number generator to generate the data. In practice, you may get
    // the data from a database or by other means. If you do not have 15 minute data, you may modify
    // the "drawChart" method below to not using 15 minute data.
    //
    generateRandomData(ticker, startDate, endDate, 900);
}

/// <summary>
/// Get daily data series for timeStamps, highData, lowData, openData, closeData
/// and volData.
/// </summary>
/// <param name="ticker">The ticker symbol for the data series.</param>
/// <param name="startDate">The starting date/time for the data series.</param>
/// <param name="endDate">The ending date/time for the data series.</param>
protected void getDailyData(string ticker, DateTime startDate, DateTime endDate)
{
    //
    // In this demo, we use a random number generator to generate the data. In practice, you may get
    // the data from a database or by other means.
    //
    // A typical database code example is like below. (This only shows a general idea. The exact
    // details may differ depending on your database brand and schema. The SQL, in particular the
    // date format, may be different depending on which brand of database you use.)
    //
    //    // Open the database connection to MS SQL
    //    System.Data.IDbConnection dbconn = new System.Data.SqlClient.SqlConnection(
    //          "..... put your database connection string here .......");
    //   dbconn.Open();
    //
    //   // SQL statement to get the data
    //   System.Data.IDbCommand sqlCmd = dbconn.CreateCommand();
    //   sqlCmd.CommandText = "Select recordDate, highData, lowData, openData, " +
    //         "closeData, volData From dailyFinanceTable Where ticker = '" + ticker +
    //         "' And recordDate >= '" + startDate.ToString("yyyyMMdd") + "' And " +
    //         "recordDate <= '" + endDate.ToString("yyyyMMdd") + "' Order By recordDate";
    //
    //   // The most convenient way to read the SQL result into arrays is to use the
    //   // ChartDirector DBTable utility.
    //   DBTable table = new DBTable(sqlCmd.ExecuteReader());
    //   dbconn.Close();
    //
    //   // Now get the data into arrays
    //   timeStamps = table.getColAsDateTime(0);
    //   highData = table.getCol(1);
    //   lowData = table.getCol(2);
    //   openData = table.getCol(3);
    //   closeData = table.getCol(4);
    //   volData = table.getCol(5);
    //
    generateRandomData(ticker, startDate, endDate, 86400);
}

/// <summary>
/// Get weekly data series for timeStamps, highData, lowData, openData, closeData
/// and volData.
/// </summary>
/// <param name="ticker">The ticker symbol for the data series.</param>
/// <param name="startDate">The starting date/time for the data series.</param>
/// <param name="endDate">The ending date/time for the data series.</param>
protected void getWeeklyData(string ticker, DateTime startDate, DateTime endDate)
{
    //
    // If you do not have weekly data, you may call "getDailyData(startDate, endDate)" to get daily
    // data, then call "convertDailyToWeeklyData()" to convert to weekly data.
    //
    generateRandomData(ticker, startDate, endDate, 86400 * 7);
}

/// <summary>
/// Get monthly data series for timeStamps, highData, lowData, openData, closeData
/// and volData.
/// </summary>
/// <param name="ticker">The ticker symbol for the data series.</param>
/// <param name="startDate">The starting date/time for the data series.</param>
/// <param name="endDate">The ending date/time for the data series.</param>
protected void getMonthlyData(string ticker, DateTime startDate, DateTime endDate)
{
    //
    // If you do not have weekly data, you may call "getDailyData(startDate, endDate)" to get daily
    // data, then call "convertDailyToMonthlyData()" to convert to monthly data.
    //
    generateRandomData(ticker, startDate, endDate, 86400 * 30);
}

/// <summary>
/// A random number generator designed to generate realistic financial data.
/// </summary>
/// <param name="ticker">The ticker symbol for the data series.</param>
/// <param name="startDate">The starting date/time for the data series.</param>
/// <param name="endDate">The ending date/time for the data series.</param>
/// <param name="resolution">The period of the data series.</param>
protected void generateRandomData(string ticker, DateTime startDate, DateTime endDate,
    int resolution)
{
    FinanceSimulator db = new FinanceSimulator(ticker, startDate, endDate, resolution);
    timeStamps = db.getTimeStamps();
    highData = db.getHighData();
    lowData = db.getLowData();
    openData = db.getOpenData();
    closeData = db.getCloseData();
    volData = db.getVolData();
}

/// <summary>
/// A utility to convert daily to weekly data.
/// </summary>
protected void convertDailyToWeeklyData()
{
    aggregateData(new ArrayMath(timeStamps).selectStartOfWeek());
}

/// <summary>
/// A utility to convert daily to monthly data.
/// </summary>
protected void convertDailyToMonthlyData()
{
    aggregateData(new ArrayMath(timeStamps).selectStartOfMonth());
}

/// <summary>
/// An internal method used to aggregate daily data.
/// </summary>
protected void aggregateData(ArrayMath aggregator)
{
    timeStamps = Chart.NTime(aggregator.aggregate(Chart.CTime(timeStamps), Chart.AggregateFirst));
    highData = aggregator.aggregate(highData, Chart.AggregateMax);
    lowData = aggregator.aggregate(lowData, Chart.AggregateMin);
    openData = aggregator.aggregate(openData, Chart.AggregateFirst);
    closeData = aggregator.aggregate(closeData, Chart.AggregateLast);
    volData = aggregator.aggregate(volData, Chart.AggregateSum);
}

/// <summary>
/// Create a financial chart according to user selections. The user selections are
/// encoded in the query parameters.
/// </summary>
public BaseChart drawChart()
{
    // In this demo, we just assume we plot up to the latest time. So end date is now.
    DateTime endDate = DateTime.Now;

    // If the trading day has not yet started (before 9:30am), or if the end date is on on Sat or
    // Sun, we set the end date to 4:00pm of the last trading day
    while ((endDate.TimeOfDay.CompareTo(new TimeSpan(9, 30, 0)) < 0) || (endDate.DayOfWeek ==
        DayOfWeek.Sunday) || (endDate.DayOfWeek == DayOfWeek.Saturday)) {
        endDate = endDate.Date.AddDays(-1).Add(new TimeSpan(16, 0, 0));
    }

    // The duration selected by the user
    int durationInDays = int.Parse(TimeRange.SelectedItem.Value);

    // Compute the start date by subtracting the duration from the end date.
    DateTime startDate = endDate;
    if (durationInDays >= 30) {
        // More or equal to 30 days - so we use months as the unit
        startDate = new DateTime(endDate.Year, endDate.Month, 1).AddMonths(-durationInDays / 30);
    } else {
        // Less than 30 days - use day as the unit. The starting point of the axis is always at the
        // start of the day (9:30am). Note that we use trading days, so we skip Sat and Sun in
        // counting the days.
        startDate = endDate.Date;
        for(int i = 1; i < durationInDays; ++i) {
            if (startDate.DayOfWeek == DayOfWeek.Monday) {
                startDate = startDate.AddDays(-3);
            } else {
                startDate = startDate.AddDays(-1);
            }
        }
    }

    // The moving average periods selected by the user.
    int avgPeriod1 = 0;
    try {avgPeriod1 = int.Parse(movAvg1.Text);; }
    catch {avgPeriod1 = 0;; }
    int avgPeriod2 = 0;
    try {avgPeriod2 = int.Parse(movAvg2.Text);; }
    catch {avgPeriod2 = 0;; }

    if (avgPeriod1 < 0) {
        avgPeriod1 = 0;
    } else if (avgPeriod1 > 300) {
        avgPeriod1 = 300;
    }

    if (avgPeriod2 < 0) {
        avgPeriod2 = 0;
    } else if (avgPeriod2 > 300) {
        avgPeriod2 = 300;
    }

    // We need extra leading data points in order to compute moving averages.
    int extraPoints = 20;
    if (avgPeriod1 > extraPoints) {
        extraPoints = avgPeriod1;
    }
    if (avgPeriod2 > extraPoints) {
        extraPoints = avgPeriod2;
    }

    // Get the data series to compare with, if any.
    string compareKey = CompareWith.Text.Trim();
    compareData = null;
    if (getData(compareKey, startDate, endDate, durationInDays, extraPoints)) {
          compareData = closeData;
    }

    // The data series we want to get.
    string tickerKey = TickerSymbol.Text.Trim();
    if (!getData(tickerKey, startDate, endDate, durationInDays, extraPoints)) {
        return errMsg("Please enter a valid ticker symbol");
    }

    // We now confirm the actual number of extra points (data points that are before the start date)
    // as inferred using actual data from the database.
    extraPoints = timeStamps.Length;
    for(int i = 0; i < timeStamps.Length; ++i) {
        if (timeStamps[i] >= startDate) {
            extraPoints = i;
            break;
        }
    }

    // Check if there is any valid data
    if (extraPoints >= timeStamps.Length) {
        // No data - just display the no data message.
        return errMsg("No data available for the specified time period");
    }

    // In some finance chart presentation style, even if the data for the latest day is not fully
    // available, the axis for the entire day will still be drawn, where no data will appear near
    // the end of the axis.
    if (resolution < 86400) {
        // Add extra points to the axis until it reaches the end of the day. The end of day is
        // assumed to be 16:00 (it depends on the stock exchange).
        DateTime lastTime = timeStamps[timeStamps.Length - 1];
        int extraTrailingPoints = (int)(new TimeSpan(16, 0, 0).Subtract(lastTime.TimeOfDay
            ).TotalSeconds / resolution);
        if (extraTrailingPoints > 0) {
            DateTime[] extendedTimeStamps = new DateTime[timeStamps.Length + extraTrailingPoints];
            Array.Copy(timeStamps, 0, extendedTimeStamps, 0, timeStamps.Length);
            for(int i = 0; i < extraTrailingPoints; ++i) {
                extendedTimeStamps[i + timeStamps.Length] = lastTime.AddSeconds(resolution * (i + 1)
                    );
            }
            timeStamps = extendedTimeStamps;
        }
    }

    //
    // At this stage, all data are available. We can draw the chart as according to user input.
    //

    //
    // Determine the chart size. In this demo, user can select 4 different chart sizes. Default is
    // the large chart size.
    //
    int width = 780;
    int mainHeight = 255;
    int indicatorHeight = 80;

    string size = ChartSize.SelectedItem.Value;
    if (size == "S") {
        // Small chart size
        width = 450;
        mainHeight = 160;
        indicatorHeight = 60;
    } else if (size == "M") {
        // Medium chart size
        width = 620;
        mainHeight = 215;
        indicatorHeight = 70;
    } else if (size == "H") {
        // Huge chart size
        width = 1000;
        mainHeight = 320;
        indicatorHeight = 90;
    }

    // Create the chart object using the selected size
    FinanceChart m = new FinanceChart(width);

    // Set the data into the chart object
    m.setData(timeStamps, highData, lowData, openData, closeData, volData, extraPoints);

    //
    // We configure the title of the chart. In this demo chart design, we put the company name as
    // the top line of the title with left alignment.
    //
    m.addPlotAreaTitle(Chart.TopLeft, tickerKey);

    // We displays the current date as well as the data resolution on the next line.
    string resolutionText = "";
    if (resolution == 30 * 86400) {
        resolutionText = "Monthly";
    } else if (resolution == 7 * 86400) {
        resolutionText = "Weekly";
    } else if (resolution == 86400) {
        resolutionText = "Daily";
    } else if (resolution == 900) {
        resolutionText = "15-min";
    }

    m.addPlotAreaTitle(Chart.BottomLeft, "<*font=Arial,size=8*>" + m.formatValue(DateTime.Now,
        "mmm dd, yyyy") + " - " + resolutionText + " chart");

    // A copyright message at the bottom left corner the title area
    m.addPlotAreaTitle(Chart.BottomRight, "<*font=Arial,size=8*>(c) Advanced Software Engineering");

    //
    // Add the first techical indicator according. In this demo, we draw the first indicator on top
    // of the main chart.
    //
    addIndicator(m, Indicator1.SelectedItem.Value, indicatorHeight);

    //
    // Add the main chart
    //
    m.addMainChart(mainHeight);

    //
    // Set log or linear scale according to user preference
    //
    if (LogScale.Checked) {
        m.setLogScale(true);
    }

    //
    // Set axis labels to show data values or percentage change to user preference
    //
    if (PercentageScale.Checked) {
        m.setPercentageAxis();
    }

    //
    // Draw any price line the user has selected
    //
    string mainType = ChartType.SelectedItem.Value;
    if (mainType == "Close") {
        m.addCloseLine(0x000040);
    } else if (mainType == "TP") {
        m.addTypicalPrice(0x000040);
    } else if (mainType == "WC") {
        m.addWeightedClose(0x000040);
    } else if (mainType == "Median") {
        m.addMedianPrice(0x000040);
    }

    //
    // Add comparison line if there is data for comparison
    //
    if (compareData != null) {
        if (compareData.Length > extraPoints) {
            m.addComparison(compareData, 0x0000ff, compareKey);
        }
    }

    //
    // Add moving average lines.
    //
    addMovingAvg(m, avgType1.SelectedItem.Value, avgPeriod1, 0x663300);
    addMovingAvg(m, avgType2.SelectedItem.Value, avgPeriod2, 0x9900ff);

    //
    // Draw candlesticks or OHLC symbols if the user has selected them.
    //
    if (mainType == "CandleStick") {
        m.addCandleStick(0x33ff33, 0xff3333);
    } else if (mainType == "OHLC") {
        m.addHLOC(0x008800, 0xcc0000);
    }

    //
    // Add parabolic SAR if necessary
    //
    if (ParabolicSAR.Checked) {
        m.addParabolicSAR(0.02, 0.02, 0.2, Chart.DiamondShape, 5, 0x008800, 0x000000);
    }

    //
    // Add price band/channel/envelop to the chart according to user selection
    //
    string bandType = Band.SelectedItem.Value;
    if (bandType == "BB") {
        m.addBollingerBand(20, 2, 0x9999ff, unchecked((int)0xc06666ff));
    } else if (bandType == "DC") {
        m.addDonchianChannel(20, 0x9999ff, unchecked((int)0xc06666ff));
    } else if (bandType == "Envelop") {
        m.addEnvelop(20, 0.1, 0x9999ff, unchecked((int)0xc06666ff));
    }

    //
    // Add volume bars to the main chart if necessary
    //
    if (Volume.Checked) {
        m.addVolBars(indicatorHeight, 0x99ff99, 0xff9999, 0xc0c0c0);
    }

    //
    // Add additional indicators as according to user selection.
    //
    addIndicator(m, Indicator2.SelectedItem.Value, indicatorHeight);
    addIndicator(m, Indicator3.SelectedItem.Value, indicatorHeight);
    addIndicator(m, Indicator4.SelectedItem.Value, indicatorHeight);

    return m;
}

/// <summary>
/// Add a moving average line to the FinanceChart object.
/// </summary>
/// <param name="m">The FinanceChart object to add the line to.</param>
/// <param name="avgType">The moving average type (SMA/EMA/TMA/WMA).</param>
/// <param name="avgPeriod">The moving average period.</param>
/// <param name="color">The color of the line.</param>
/// <returns>The LineLayer object representing line layer created.</returns>
protected LineLayer addMovingAvg(FinanceChart m, string avgType, int avgPeriod, int color)
{
    if (avgPeriod > 1) {
        if (avgType == "SMA") {
            return m.addSimpleMovingAvg(avgPeriod, color);
        } else if (avgType == "EMA") {
            return m.addExpMovingAvg(avgPeriod, color);
        } else if (avgType == "TMA") {
            return m.addTriMovingAvg(avgPeriod, color);
        } else if (avgType == "WMA") {
            return m.addWeightedMovingAvg(avgPeriod, color);
        }
    }
    return null;
}

/// <summary>
/// Add an indicator chart to the FinanceChart object. In this demo example, the
/// indicator parameters (such as the period used to compute RSI, colors of the lines,
/// etc.) are hard coded to commonly used values. You are welcome to design a more
/// complex user interface to allow users to set the parameters.
/// </summary>
/// <param name="m">The FinanceChart object to add the line to.</param>
/// <param name="indicator">The selected indicator.</param>
/// <param name="height">Height of the chart in pixels</param>
/// <returns>The XYChart object representing indicator chart.</returns>
protected XYChart addIndicator(FinanceChart m, string indicator, int height)
{
    if (indicator == "RSI") {
        return m.addRSI(height, 14, 0x800080, 20, 0xff6666, 0x6666ff);
    } else if (indicator == "StochRSI") {
        return m.addStochRSI(height, 14, 0x800080, 30, 0xff6666, 0x6666ff);
    } else if (indicator == "MACD") {
        return m.addMACD(height, 26, 12, 9, 0x0000ff, 0xff00ff, 0x008000);
    } else if (indicator == "FStoch") {
        return m.addFastStochastic(height, 14, 3, 0x006060, 0x606000);
    } else if (indicator == "SStoch") {
        return m.addSlowStochastic(height, 14, 3, 0x006060, 0x606000);
    } else if (indicator == "ATR") {
        return m.addATR(height, 14, 0x808080, 0x0000ff);
    } else if (indicator == "ADX") {
        return m.addADX(height, 14, 0x008000, 0x800000, 0x000080);
    } else if (indicator == "DCW") {
        return m.addDonchianWidth(height, 20, 0x0000ff);
    } else if (indicator == "BBW") {
        return m.addBollingerWidth(height, 20, 2, 0x0000ff);
    } else if (indicator == "DPO") {
        return m.addDPO(height, 20, 0x0000ff);
    } else if (indicator == "PVT") {
        return m.addPVT(height, 0x0000ff);
    } else if (indicator == "Momentum") {
        return m.addMomentum(height, 12, 0x0000ff);
    } else if (indicator == "Performance") {
        return m.addPerformance(height, 0x0000ff);
    } else if (indicator == "ROC") {
        return m.addROC(height, 12, 0x0000ff);
    } else if (indicator == "OBV") {
        return m.addOBV(height, 0x0000ff);
    } else if (indicator == "AccDist") {
        return m.addAccDist(height, 0x0000ff);
    } else if (indicator == "CLV") {
        return m.addCLV(height, 0x0000ff);
    } else if (indicator == "WilliamR") {
        return m.addWilliamR(height, 14, 0x800080, 30, 0xff6666, 0x6666ff);
    } else if (indicator == "Aroon") {
        return m.addAroon(height, 14, 0x339933, 0x333399);
    } else if (indicator == "AroonOsc") {
        return m.addAroonOsc(height, 14, 0x0000ff);
    } else if (indicator == "CCI") {
        return m.addCCI(height, 20, 0x800080, 100, 0xff6666, 0x6666ff);
    } else if (indicator == "EMV") {
        return m.addEaseOfMovement(height, 9, 0x006060, 0x606000);
    } else if (indicator == "MDX") {
        return m.addMassIndex(height, 0x800080, 0xff6666, 0x6666ff);
    } else if (indicator == "CVolatility") {
        return m.addChaikinVolatility(height, 10, 10, 0x0000ff);
    } else if (indicator == "COscillator") {
        return m.addChaikinOscillator(height, 0x0000ff);
    } else if (indicator == "CMF") {
        return m.addChaikinMoneyFlow(height, 21, 0x008000);
    } else if (indicator == "NVI") {
        return m.addNVI(height, 255, 0x0000ff, 0x883333);
    } else if (indicator == "PVI") {
        return m.addPVI(height, 255, 0x0000ff, 0x883333);
    } else if (indicator == "MFI") {
        return m.addMFI(height, 14, 0x800080, 30, 0xff6666, 0x6666ff);
    } else if (indicator == "PVO") {
        return m.addPVO(height, 26, 12, 9, 0x0000ff, 0xff00ff, 0x008000);
    } else if (indicator == "PPO") {
        return m.addPPO(height, 26, 12, 9, 0x0000ff, 0xff00ff, 0x008000);
    } else if (indicator == "UO") {
        return m.addUltimateOscillator(height, 7, 14, 28, 0x800080, 20, 0xff6666, 0x6666ff);
    } else if (indicator == "Vol") {
        return m.addVolIndicator(height, 0x99ff99, 0xff9999, 0xc0c0c0);
    } else if (indicator == "TRIX") {
        return m.addTRIX(height, 12, 0x0000ff);
    }
    return null;
}

/// <summary>
/// Creates a dummy chart to show an error message.
/// </summary>
/// <param name="msg">The error message.
/// <returns>The BaseChart object containing the error message.</returns>
protected BaseChart errMsg(string msg)
{
    MultiChart m = new MultiChart(400, 200);
    m.addTitle2(Chart.Center, msg, "Arial", 10).setMaxWidth(m.getWidth());
    return m;
}

//
// Page Load event handler
//
protected void Page_Load(object sender, EventArgs e)
{
    // Create the finance chart
    BaseChart c = drawChart();

    // Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG);
}

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
