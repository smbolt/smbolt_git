using ChartDirector;
using System;

namespace Custom
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    // Copyright 2012 Advanced Software Engineering Limited
    //
    // ChartDirector FinanceChart class library
    //     - Requires ChartDirector Ver 5.1 or above
    //
    // You may use and modify the code in this file in your application, provided the code and
    // its modifications are used only in conjunction with ChartDirector. Usage of this software
    // is subjected to the terms and condition of the ChartDirector license.
    ///////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Represents a Financial Chart
    /// </summary>
    public class FinanceChart : MultiChart
    {
        int m_totalWidth = 0;
        int m_totalHeight = 0;
        bool m_antiAlias = true;
        bool m_logScale = false;
        bool m_axisOnRight = true;

        int m_leftMargin = 40;
        int m_rightMargin = 40;
        int m_topMargin = 30;
        int m_bottomMargin = 30;

        int m_plotAreaBgColor = 0xffffff;
        int m_plotAreaBorder = 0x888888;
        int m_plotAreaGap = 2;

        int m_majorHGridColor = 0xdddddd;
        int m_minorHGridColor = 0xdddddd;
        int m_majorVGridColor = 0xdddddd;
        int m_minorVGridColor = 0xdddddd;

        string m_legendFont = "normal";
        double m_legendFontSize = 8;
        int m_legendFontColor = Chart.TextColor;
        int m_legendBgColor = unchecked((int)0x80cccccc);

        string m_yAxisFont = "normal";
        double m_yAxisFontSize = 8;
        int m_yAxisFontColor = Chart.TextColor;
        int m_yAxisMargin = 14;

        string m_xAxisFont = "normal";
        double m_xAxisFontSize = 8;
        int m_xAxisFontColor = Chart.TextColor;
        double m_xAxisFontAngle = 0;

        double[] m_timeStamps = null;
        double[] m_highData = null;
        double[] m_lowData = null;
        double[] m_openData = null;
        double[] m_closeData = null;
        double[] m_volData = null;
        string m_volUnit = "";
        int m_extraPoints = 0;

        string m_yearFormat = "{value|yyyy}";
        string m_firstMonthFormat = "<*font=bold*>{value|mmm yy}";
        string m_otherMonthFormat = "{value|mmm}";
        string m_firstDayFormat = "<*font=bold*>{value|d mmm}";
        string m_otherDayFormat = "{value|d}";
        string m_firstHourFormat = "<*font=bold*>{value|d mmm\nh:nna}";
        string m_otherHourFormat = "{value|h:nna}";
        int m_timeLabelSpacing = 50;

        string m_generalFormat = "P3";

        string m_toolTipMonthFormat = "[{xLabel|mmm yyyy}]";
        string m_toolTipDayFormat = "[{xLabel|mmm d, yyyy}]";
        string m_toolTipHourFormat = "[{xLabel|mmm d, yyyy hh:nn:ss}]";

        XYChart m_mainChart = null;
        XYChart m_currentChart = null;

        /// <summary>
        /// Create a FinanceChart with a given width. The height will be automatically determined
        /// as the chart is built.
        /// </summary>
        /// <param name="width">Width of the chart in pixels</param>
        public FinanceChart(int width) : base(width, 1)
        {
            m_totalWidth = width;
            setMainChart(this);
        }

        /// <summary>
        /// Enable/Disable anti-alias. Enabling anti-alias makes the line smoother. Disabling
        /// anti-alias make the chart file size smaller, and so can be downloaded faster
        /// through the Internet. The default is to enable anti-alias.
        /// </summary>
        /// <param name="antiAlias">True to enable anti-alias. False to disable anti-alias.</param>
        public void enableAntiAlias(bool antiAlias)
        {
            m_antiAlias = antiAlias;
        }

        /// <summary>
        /// Set the margins around the plot area.
        /// </summary>
        /// <param name="m_leftMargin">The distance between the plot area and the chart left edge.</param>
        /// <param name="m_topMargin">The distance between the plot area and the chart top edge.</param>
        /// <param name="m_rightMargin">The distance between the plot area and the chart right edge.</param>
        /// <param name="m_bottomMargin">The distance between the plot area and the chart bottom edge.</param>
        public void setMargins(int leftMargin, int topMargin, int rightMargin, int bottomMargin)
        {
            m_leftMargin = leftMargin;
            m_rightMargin = rightMargin;
            m_topMargin = topMargin;
            m_bottomMargin = bottomMargin;
        }

        /// <summary>
        /// Add a text title above the plot area. You may add multiple title above the plot area by
        /// calling this method multiple times.
        /// </summary>
        /// <param name="alignment">The alignment with respect to the region that is on top of the
        /// plot area.</param>
        /// <param name="text">The text to add.</param>
        /// <returns>The TextBox object representing the text box above the plot area.</returns>
        public ChartDirector.TextBox addPlotAreaTitle(int alignment, string text)
        {
            ChartDirector.TextBox ret = addText(m_leftMargin, 0, text, "bold", 10, Chart.TextColor,
                alignment);
            ret.setSize(m_totalWidth - m_leftMargin - m_rightMargin + 1, m_topMargin - 1);
            ret.setMargin(0);
            return ret;
        }

        /// <summary>
        /// Set the plot area style. The default is to use pale yellow 0xfffff0 as the background,
        /// and light grey 0xdddddd as the grid lines.
        /// </summary>
        /// <param name="bgColor">The plot area background color.</param>
        /// <param name="majorHGridColor">Major horizontal grid color.</param>
        /// <param name="majorVGridColor">Major vertical grid color.</param>
        /// <param name="minorHGridColor">Minor horizontal grid color. In current version, minor
        /// horizontal grid is not used.</param>
        /// <param name="minorVGridColor">Minor vertical grid color.</param>
        public void setPlotAreaStyle(int bgColor, int majorHGridColor, int majorVGridColor,
            int minorHGridColor, int minorVGridColor)
        {
            m_plotAreaBgColor = bgColor;
            m_majorHGridColor = majorHGridColor;
            m_majorVGridColor = majorVGridColor;
            m_minorHGridColor = minorHGridColor;
            m_minorVGridColor = minorVGridColor;
        }

        /// <summary>
        /// Set the plot area border style. The default is grey color (888888), with a gap
        /// of 2 pixels between charts.
        /// </summary>
        /// <param name="borderColor">The color of the border.</param>
        /// <param name="borderGap">The gap between two charts.</param>
        public void setPlotAreaBorder(int borderColor, int borderGap)
        {
            m_plotAreaBorder = borderColor;
            m_plotAreaGap = borderGap;
        }

        /// <summary>
        /// Set legend style. The default is Arial 8 pt black color, with light grey background.
        /// </summary>
        /// <param name="font">The font of the legend text.</param>
        /// <param name="fontSize">The font size of the legend text in points.</param>
        /// <param name="fontColor">The color of the legend text.</param>
        /// <param name="bgColor">The background color of the legend box.</param>
        public void setLegendStyle(string font, double fontSize, int fontColor, int bgColor)
        {
            m_legendFont = font;
            m_legendFontSize = fontSize;
            m_legendFontColor = fontColor;
            m_legendBgColor = bgColor;
        }

        /// <summary>
        /// Set x-axis label style. The default is Arial 8 pt black color no rotation.
        /// </summary>
        /// <param name="font">The font of the axis labels.</param>
        /// <param name="fontSize">The font size of the axis labels in points.</param>
        /// <param name="fontColor">The color of the axis labels.</param>
        /// <param name="fontAngle">The rotation of the axis labels.</param>
        public void setXAxisStyle(string font, double fontSize, int fontColor, double fontAngle)
        {
            m_xAxisFont = font;
            m_xAxisFontSize = fontSize;
            m_xAxisFontColor = fontColor;
            m_xAxisFontAngle = fontAngle;
        }

        /// <summary>
        /// Set y-axis label style. The default is Arial 8 pt black color, with 13 pixels margin.
        /// </summary>
        /// <param name="font">The font of the axis labels.</param>
        /// <param name="fontSize">The font size of the axis labels in points.</param>
        /// <param name="fontColor">The color of the axis labels.</param>
        /// <param name="axisMargin">The margin at the top of the y-axis in pixels (to leave
        /// space for the legend box).</param>
        public void setYAxisStyle(string font, double fontSize, int fontColor, int axisMargin)
        {
            m_yAxisFont = font;
            m_yAxisFontSize = fontSize;
            m_yAxisFontColor = fontColor;
            m_yAxisMargin = axisMargin;
        }

        /// <summary>
        /// Set whether the main y-axis is on right of left side of the plot area. The default is
        /// on right.
        /// </summary>
        /// <param name="b">True if the y-axis is on right. False if the y-axis is on left.</param>
        public void setAxisOnRight(bool b)
        {
            m_axisOnRight = b;
        }

        /// <summary>
        /// Determines if log scale should be used for the main chart. The default is linear scale.
        /// </summary>
        /// <param name="b">True for using log scale. False for using linear scale.</param>
        public void setLogScale(bool b)
        {
            m_logScale = b;
            if (m_mainChart != null) {
                if (m_logScale) {
                    m_mainChart.yAxis().setLogScale();
                } else {
                    m_mainChart.yAxis().setLinearScale();
                }
            }
        }

        /// <summary>
        /// Set the date/time formats to use for the x-axis labels under various cases.
        /// </summary>
        /// <param name="yearFormat">The format for displaying labels on an axis with yearly ticks. The
        /// default is "yyyy".</param>
        /// <param name="firstMonthFormat">The format for displaying labels on an axis with monthly ticks.
        /// This parameter applies to the first available month of a year (usually January) only, so it can
        /// be formatted differently from the other labels.</param>
        /// <param name="otherMonthFormat">The format for displaying labels on an axis with monthly ticks.
        /// This parameter applies to months other than the first available month of a year.</param>
        /// <param name="firstDayFormat">The format for displaying labels on an axis with daily ticks.
        /// This parameter applies to the first available day of a month only, so it can be formatted
        /// differently from the other labels.</param>
        /// <param name="otherDayFormat">The format for displaying labels on an axis with daily ticks.
        /// This parameter applies to days other than the first available day of a month.</param>
        /// <param name="firstHourFormat">The format for displaying labels on an axis with hourly
        /// resolution. This parameter applies to the first tick of a day only, so it can be formatted
        /// differently from the other labels.</param>
        /// <param name="otherHourFormat">The format for displaying labels on an axis with hourly.
        /// resolution. This parameter applies to ticks at hourly boundaries, except the first tick
        /// of a day.</param>
        public void setDateLabelFormat(string yearFormat, string firstMonthFormat,
            string otherMonthFormat, string firstDayFormat, string otherDayFormat,
            string firstHourFormat, string otherHourFormat)
        {
            if (yearFormat != null) {
                m_yearFormat = yearFormat;
            }
            if (firstMonthFormat != null) {
                m_firstMonthFormat = firstMonthFormat;
            }
            if (otherMonthFormat != null) {
                m_otherMonthFormat = otherMonthFormat;
            }
            if (firstDayFormat != null) {
                m_firstDayFormat = firstDayFormat;
            }
            if (otherDayFormat != null) {
                m_otherDayFormat = otherDayFormat;
            }
            if (firstHourFormat != null) {
                m_firstHourFormat = firstHourFormat;
            }
            if (otherHourFormat != null) {
                m_otherHourFormat = otherHourFormat;
            }
        }

        /// <summary>
        /// This method is for backward compatibility - use setDataLabelFormat instead.
        /// </summary>
        public void setTimeLabelFormats(string yearFormat, string firstMonthFormat,
            string otherMonthFormat, string firstDayFormat, string otherDayFormat,
            string firstHourFormat, string otherHourFormat)
        {
            setDateLabelFormat(yearFormat, firstMonthFormat, otherMonthFormat, firstDayFormat,
                otherDayFormat, firstHourFormat, otherHourFormat);
        }

        /// <summary>
        /// Set the minimum label spacing between two labels on the time axis
        /// </summary>
        /// <param name="labelSpacing">The minimum label spacing in pixels.</param>
        public void setDateLabelSpacing(int labelSpacing)
        {
            if (labelSpacing > 0) {
                m_timeLabelSpacing = labelSpacing;
            } else {
                 m_timeLabelSpacing = 0;
            }
        }

        /// <summary>
        /// This function is for backward compatibility. It has no purpose.
        /// </summary>
        public void enableToolTips(bool b, string dateTimeFormat)
        {
            //do nothing
        }

        /// <summary>
        /// Set the tool tip formats for display date/time
        /// </summary>
        /// <param name="monthFormat">The tool tip format to use if the data point spacing is one
        /// or more months (more than 30 days).</param>
        /// <param name="dayFormat">The tool tip format to use if the data point spacing is 1 day
        /// to less than 30 days.</param>
        /// <param name="hourFormat">The tool tip format to use if the data point spacing is less
        /// than 1 day.</param>
        public void setToolTipDateFormat(string monthFormat, string dayFormat, string hourFormat)
        {
            if (monthFormat != null) {
                m_toolTipMonthFormat = monthFormat;
            }
            if (dayFormat != null) {
                m_toolTipDayFormat = dayFormat;
            }
            if (hourFormat != null) {
                m_toolTipHourFormat = hourFormat;
            }
        }

        /// <summary>
        /// Get the tool tip format for display date/time
        /// </summary>
        /// <returns>The tool tip format string.</returns>
        public string getToolTipDateFormat()
        {
            if (m_timeStamps == null) {
                return m_toolTipHourFormat;
            }
            if (m_timeStamps.Length <= m_extraPoints) {
                return m_toolTipHourFormat;
            }
            double resolution = (m_timeStamps[m_timeStamps.Length - 1] - m_timeStamps[0]) / (
                m_timeStamps.Length);
            if (resolution >= 30 * 86400) {
                return m_toolTipMonthFormat;
            } else if (resolution >= 86400) {
                return m_toolTipDayFormat;
            } else {
                return m_toolTipHourFormat;
            }
        }

        /// <summary>
        /// Set the number format for use in displaying values in legend keys and tool tips.
        /// </summary>
        /// <param name="formatString">The default number format.</param>
        public void setNumberLabelFormat(string formatString)
        {
            if (formatString != null) {
                m_generalFormat = formatString;
            }
        }

        /// <summary>
        /// A utility function to compute triangular moving averages
        /// </summary>
        /// <param name="data">An array of numbers as input.</param>
        /// <param name="period">The moving average period.</param>
        /// <returns>An array representing the triangular moving average of the input array.</returns>
        private double[] computeTriMovingAvg(double[] data, int period)
        {
            int p = period / 2 + 1;
            return new ArrayMath(data).movAvg(p).movAvg(p).result();
        }

        /// <summary>
        /// A utility function to compute weighted moving averages
        /// </summary>
        /// <param name="data">An array of numbers as input.</param>
        /// <param name="period">The moving average period.</param>
        /// <returns>An array representing the weighted moving average of the input array.</returns>
        private double[] computeWeightedMovingAvg(double[] data, int period)
        {
            ArrayMath acc = new ArrayMath(data);
            for(int i = 2; i < period + 1; ++i) {
                acc.add(new ArrayMath(data).movAvg(i).mul(i).result());
            }
            return acc.div((1 + period) * period / 2).result();
        }

        /// <summary>
        /// A utility function to obtain the first visible closing price.
        /// </summary>
        /// <returns>The first closing price.
        /// are cd.NoValue.</returns>
        private double firstCloseValue()
        {
            for(int i = m_extraPoints; i < m_closeData.Length; ++i) {
                if ((m_closeData[i] != Chart.NoValue) && (m_closeData[i] != 0)) {
                    return m_closeData[i];
                }
            }
            return Chart.NoValue;
        }

        /// <summary>
        /// A utility function to obtain the last valid position (that is, position not
        /// containing cd.NoValue) of a data series.
        /// </summary>
        /// <param name="data">An array of numbers as input.</param>
        /// <returns>The last valid position in the input array, or -1 if all positions
        /// are cd.NoValue.</returns>
        private int lastIndex(double[] data)
        {
            int i = data.Length - 1;
            while (i >= 0) {
                if (data[i] != Chart.NoValue) {
                    break;
                }
                i = i - 1;
            }
            return i;
        }

        /// <summary>
        /// Set the data used in the chart. If some of the data are not available, some artifical
        /// values should be used. For example, if the high and low values are not available, you
        /// may use closeData as highData and lowData.
        /// </summary>
        /// <param name="timeStamps">An array of dates/times for the time intervals.</param>
        /// <param name="highData">The high values in the time intervals.</param>
        /// <param name="lowData">The low values in the time intervals.</param>
        /// <param name="openData">The open values in the time intervals.</param>
        /// <param name="closeData">The close values in the time intervals.</param>
        /// <param name="volData">The volume values in the time intervals.</param>
        /// <param name="extraPoints">The number of leading time intervals that are not
        /// displayed in the chart. These intervals are typically used for computing
        /// indicators that require extra leading data, such as moving averages.</param>
        public void setData(DateTime[] timeStamps, double[] highData, double[] lowData,
            double[] openData, double[] closeData, double[] volData, int extraPoints)
        {
            setData(Chart.CTime(timeStamps), highData, lowData, openData, closeData, volData,
                extraPoints);
        }

        /// <summary>
        /// Set the data used in the chart. If some of the data are not available, some artifical
        /// values should be used. For example, if the high and low values are not available, you
        /// may use closeData as highData and lowData.
        /// </summary>
        /// <param name="timeStamps">An array of dates/times for the time intervals.</param>
        /// <param name="highData">The high values in the time intervals.</param>
        /// <param name="lowData">The low values in the time intervals.</param>
        /// <param name="openData">The open values in the time intervals.</param>
        /// <param name="closeData">The close values in the time intervals.</param>
        /// <param name="volData">The volume values in the time intervals.</param>
        /// <param name="extraPoints">The number of leading time intervals that are not
        /// displayed in the chart. These intervals are typically used for computing
        /// indicators that require extra leading data, such as moving averages.</param>
        public void setData(double[] timeStamps, double[] highData, double[] lowData,
            double[] openData, double[] closeData, double[] volData, int extraPoints)
        {
            m_timeStamps = timeStamps;
            m_highData = highData;
            m_lowData = lowData;
            m_openData = openData;
            m_closeData = closeData;
            if (extraPoints > 0) {
                m_extraPoints = extraPoints;
            } else {
                m_extraPoints = 0;
            }

            /////////////////////////////////////////////////////////////////////////
            // Auto-detect volume units
            /////////////////////////////////////////////////////////////////////////
            double maxVol = new ArrayMath(volData).max();
            string[] units = {"", "K", "M", "B"};
            int unitIndex = units.Length - 1;
            while ((unitIndex > 0) && (maxVol < Math.Pow(1000, unitIndex))) {
                unitIndex = unitIndex - 1;
            }

            m_volData = new ArrayMath(volData).div(Math.Pow(1000, unitIndex)).result();
            m_volUnit = units[unitIndex];
        }

        //////////////////////////////////////////////////////////////////////////////
        // Format x-axis labels
        //////////////////////////////////////////////////////////////////////////////
        private void setXLabels(Axis a)
        {
            a.setLabels2(m_timeStamps);
            if (m_extraPoints < m_timeStamps.Length) {
                int tickStep = (int)((m_timeStamps.Length - m_extraPoints) * m_timeLabelSpacing / (
                    m_totalWidth - m_leftMargin - m_rightMargin)) + 1;
                double timeRangeInSeconds = m_timeStamps[m_timeStamps.Length - 1] - m_timeStamps[
                    m_extraPoints];
                double secondsBetweenTicks = timeRangeInSeconds / (m_totalWidth - m_leftMargin -
                    m_rightMargin) * m_timeLabelSpacing;

                if (secondsBetweenTicks * (m_timeStamps.Length - m_extraPoints) <=
                    timeRangeInSeconds) {
                    tickStep = 1;
                    if (m_timeStamps.Length > 1) {
                        secondsBetweenTicks = m_timeStamps[m_timeStamps.Length - 1] - m_timeStamps[
                            m_timeStamps.Length - 2];
                    } else {
                        secondsBetweenTicks = 86400;
                    }
                }

                if ((secondsBetweenTicks > 360 * 86400) || ((secondsBetweenTicks > 90 * 86400) && (
                    timeRangeInSeconds >= 720 * 86400))) {
                    //yearly ticks
                    a.setMultiFormat2(Chart.StartOfYearFilter(), m_yearFormat, tickStep);
                } else if ((secondsBetweenTicks >= 30 * 86400) || ((secondsBetweenTicks > 7 * 86400)
                     && (timeRangeInSeconds >= 60 * 86400))) {
                    //monthly ticks
                    int monthBetweenTicks = (int)(secondsBetweenTicks / 31 / 86400) + 1;
                    a.setMultiFormat(Chart.StartOfYearFilter(), m_firstMonthFormat,
                        Chart.StartOfMonthFilter(monthBetweenTicks), m_otherMonthFormat);
                    a.setMultiFormat2(Chart.StartOfMonthFilter(), "-", 1, false);
                } else if ((secondsBetweenTicks >= 86400) || ((secondsBetweenTicks > 6 * 3600) && (
                    timeRangeInSeconds >= 86400))) {
                    //daily ticks
                    a.setMultiFormat(Chart.StartOfMonthFilter(), m_firstDayFormat,
                        Chart.StartOfDayFilter(1, 0.5), m_otherDayFormat, tickStep);
                } else {
                    //hourly ticks
                    a.setMultiFormat(Chart.StartOfDayFilter(1, 0.5), m_firstHourFormat,
                        Chart.StartOfHourFilter(1, 0.5), m_otherHourFormat, tickStep);
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////
        // Create tool tip format string for showing OHLC data
        //////////////////////////////////////////////////////////////////////////////
        private string getHLOCToolTipFormat()
        {
            return "title='" + getToolTipDateFormat() + " Op:{open|" + m_generalFormat +
                "}, Hi:{high|" + m_generalFormat + "}, Lo:{low|" + m_generalFormat + "}, Cl:{close|"
                 + m_generalFormat + "}'";
        }

        /// <summary>
        /// Add the main chart - the chart that shows the HLOC data.
        /// </summary>
        /// <param name="height">The height of the main chart in pixels.</param>
        /// <returns>An XYChart object representing the main chart created.</returns>
        public XYChart addMainChart(int height)
        {
            m_mainChart = addIndicator(height);
            m_mainChart.yAxis().setMargin(2 * m_yAxisMargin);
            if (m_logScale) {
                m_mainChart.yAxis().setLogScale();
            } else {
                m_mainChart.yAxis().setLinearScale();
            }
            return m_mainChart;
        }

        /// <summary>
        /// Add a candlestick layer to the main chart.
        /// </summary>
        /// <param name="upColor">The candle color for an up day.</param>
        /// <param name="downColor">The candle color for a down day.</param>
        /// <returns>The CandleStickLayer created.</returns>
        public CandleStickLayer addCandleStick(int upColor, int downColor)
        {
            addOHLCLabel(upColor, downColor, true);
            CandleStickLayer ret = m_mainChart.addCandleStickLayer(m_highData, m_lowData,
                m_openData, m_closeData, upColor, downColor);
            ret.setHTMLImageMap("", "", getHLOCToolTipFormat());
            if (m_highData.Length - m_extraPoints > 60) {
                ret.setDataGap(0);
            }

            if (m_highData.Length > m_extraPoints) {
                int expectedWidth = (int)((m_totalWidth - m_leftMargin - m_rightMargin) / (
                    m_highData.Length - m_extraPoints));
                if (expectedWidth <= 5) {
                    ret.setDataWidth(expectedWidth + 1 - expectedWidth % 2);
                }
            }

            return ret;
        }

        /// <summary>
        /// Add a HLOC layer to the main chart.
        /// </summary>
        /// <param name="color">The color of the HLOC symbol.</param>
        /// <returns>The HLOCLayer created.</returns>
        public HLOCLayer addHLOC(int color)
        {
            return addHLOC(color, color);
        }

        /// <summary>
        /// Add a HLOC layer to the main chart.
        /// </summary>
        /// <param name="upColor">The color of the HLOC symbol for an up day.</param>
        /// <param name="downColor">The color of the HLOC symbol for a down day.</param>
        /// <returns>The HLOCLayer created.</returns>
        public HLOCLayer addHLOC(int upColor, int downColor)
        {
            addOHLCLabel(upColor, downColor, false);
            HLOCLayer ret = m_mainChart.addHLOCLayer(m_highData, m_lowData, m_openData, m_closeData)
                ;
            ret.setColorMethod(Chart.HLOCUpDown, upColor, downColor);
            ret.setHTMLImageMap("", "", getHLOCToolTipFormat());
            ret.setDataGap(0);
            return ret;
        }

        private void addOHLCLabel(int upColor, int downColor, bool candleStickMode)
        {
            int i = lastIndex(m_closeData);
            if (i >= 0) {
                double openValue = Chart.NoValue;
                double closeValue = Chart.NoValue;
                double highValue = Chart.NoValue;
                double lowValue = Chart.NoValue;

                if (i < m_openData.Length) {
                    openValue = m_openData[i];
                }
                if (i < m_closeData.Length) {
                    closeValue = m_closeData[i];
                }
                if (i < m_highData.Length) {
                    highValue = m_highData[i];
                }
                if (i < m_lowData.Length) {
                    lowValue = m_lowData[i];
                }

                string openLabel = "";
                string closeLabel = "";
                string highLabel = "";
                string lowLabel = "";
                string delim = "";
                if (openValue != Chart.NoValue) {
                    openLabel = "Op:" + formatValue(openValue, m_generalFormat);
                    delim = ", ";
                }
                if (highValue != Chart.NoValue) {
                    highLabel = delim + "Hi:" + formatValue(highValue, m_generalFormat);
                    delim = ", ";
                }
                if (lowValue != Chart.NoValue) {
                    lowLabel = delim + "Lo:" + formatValue(lowValue, m_generalFormat);
                    delim = ", ";
                }
                if (closeValue != Chart.NoValue) {
                    closeLabel = delim + "Cl:" + formatValue(closeValue, m_generalFormat);
                    delim = ", ";
                }
                string label = openLabel + highLabel + lowLabel + closeLabel;

                bool useUpColor = (closeValue >= openValue);
                if (candleStickMode != true) {
                    double[] closeChanges = new ArrayMath(m_closeData).delta().result();
                    int lastChangeIndex = lastIndex(closeChanges);
                    useUpColor = (lastChangeIndex < 0);
                    if (useUpColor != true) {
                        useUpColor = (closeChanges[lastChangeIndex] >= 0);
                    }
                }

                int udcolor = downColor;
                if (useUpColor) {
                    udcolor = upColor;
                }
                m_mainChart.getLegend().addKey(label, udcolor);
            }
        }

        /// <summary>
        /// Add a closing price line on the main chart.
        /// </summary>
        /// <param name="color">The color of the line.</param>
        /// <returns>The LineLayer object representing the line created.</returns>
        public LineLayer addCloseLine(int color)
        {
            return addLineIndicator2(m_mainChart, m_closeData, color, "Closing Price");
        }

        /// <summary>
        /// Add a weight close line on the main chart.
        /// </summary>
        /// <param name="color">The color of the line.</param>
        /// <returns>The LineLayer object representing the line created.</returns>
        public LineLayer addWeightedClose(int color)
        {
            return addLineIndicator2(m_mainChart, new ArrayMath(m_highData).add(m_lowData).add(
                m_closeData).add(m_closeData).div(4).result(), color, "Weighted Close");
        }

        /// <summary>
        /// Add a typical price line on the main chart.
        /// </summary>
        /// <param name="color">The color of the line.</param>
        /// <returns>The LineLayer object representing the line created.</returns>
        public LineLayer addTypicalPrice(int color)
        {
            return addLineIndicator2(m_mainChart, new ArrayMath(m_highData).add(m_lowData).add(
                m_closeData).div(3).result(), color, "Typical Price");
        }

        /// <summary>
        /// Add a median price line on the main chart.
        /// </summary>
        /// <param name="color">The color of the line.</param>
        /// <returns>The LineLayer object representing the line created.</returns>
        public LineLayer addMedianPrice(int color)
        {
            return addLineIndicator2(m_mainChart, new ArrayMath(m_highData).add(m_lowData).div(2
                ).result(), color, "Median Price");
        }

        /// <summary>
        /// Add a simple moving average line on the main chart.
        /// </summary>
        /// <param name="period">The moving average period</param>
        /// <param name="color">The color of the line.</param>
        /// <returns>The LineLayer object representing the line created.</returns>
        public LineLayer addSimpleMovingAvg(int period, int color)
        {
            string label = "SMA (" + period + ")";
            return addLineIndicator2(m_mainChart, new ArrayMath(m_closeData).movAvg(period).result(
                ), color, label);
        }

        /// <summary>
        /// Add an exponential moving average line on the main chart.
        /// </summary>
        /// <param name="period">The moving average period</param>
        /// <param name="color">The color of the line.</param>
        /// <returns>The LineLayer object representing the line created.</returns>
        public LineLayer addExpMovingAvg(int period, int color)
        {
            string label = "EMA (" + period + ")";
            return addLineIndicator2(m_mainChart, new ArrayMath(m_closeData).expAvg(2.0 / (period +
                1)).result(), color, label);
        }

        /// <summary>
        /// Add a triangular moving average line on the main chart.
        /// </summary>
        /// <param name="period">The moving average period</param>
        /// <param name="color">The color of the line.</param>
        /// <returns>The LineLayer object representing the line created.</returns>
        public LineLayer addTriMovingAvg(int period, int color)
        {
            string label = "TMA (" + period + ")";
            return addLineIndicator2(m_mainChart, new ArrayMath(computeTriMovingAvg(m_closeData,
                period)).result(), color, label);
        }

        /// <summary>
        /// Add a weighted moving average line on the main chart.
        /// </summary>
        /// <param name="period">The moving average period</param>
        /// <param name="color">The color of the line.</param>
        /// <returns>The LineLayer object representing the line created.</returns>
        public LineLayer addWeightedMovingAvg(int period, int color)
        {
            string label = "WMA (" + period + ")";
            return addLineIndicator2(m_mainChart, new ArrayMath(computeWeightedMovingAvg(
                m_closeData, period)).result(), color, label);
        }

        /// <summary>
        /// Add a parabolic SAR indicator to the main chart.
        /// </summary>
        /// <param name="accInitial">Initial acceleration factor</param>
        /// <param name="accIncrement">Acceleration factor increment</param>
        /// <param name="accMaximum">Maximum acceleration factor</param>
        /// <param name="symbolType">The symbol used to plot the parabolic SAR</param>
        /// <param name="symbolSize">The symbol size in pixels</param>
        /// <param name="fillColor">The fill color of the symbol</param>
        /// <param name="edgeColor">The edge color of the symbol</param>
        /// <returns>The LineLayer object representing the layer created.</returns>
        public LineLayer addParabolicSAR(double accInitial, double accIncrement, double accMaximum,
            int symbolType, int symbolSize, int fillColor, int edgeColor)
        {
            bool isLong = true;
            double acc = accInitial;
            double extremePoint = 0;
            double[] psar = new double[m_lowData.Length];

            int i_1 = -1;
            int i_2 = -1;

            for(int i = 0; i < m_lowData.Length; ++i) {
                psar[i] = Chart.NoValue;
                if ((m_lowData[i] != Chart.NoValue) && (m_highData[i] != Chart.NoValue)) {
                    if ((i_1 >= 0) && (i_2 < 0)) {
                        if (m_lowData[i_1] <= m_lowData[i]) {
                            psar[i] = m_lowData[i_1];
                            isLong = true;
                            if (m_highData[i_1] > m_highData[i]) {
                                extremePoint = m_highData[i_1];
                            } else {
                                extremePoint = m_highData[i];
                            }
                        } else {
                            extremePoint = m_lowData[i];
                            isLong = false;
                            if (m_highData[i_1] > m_highData[i]) {
                                psar[i] = m_highData[i_1];
                            } else {
                                psar[i] = m_highData[i];
                            }
                        }
                    } else if ((i_1 >= 0) && (i_2 >= 0)) {
                        if (acc > accMaximum) {
                            acc = accMaximum;
                        }

                        psar[i] = psar[i_1] + acc * (extremePoint - psar[i_1]);

                        if (isLong) {
                            if (m_lowData[i] < psar[i]) {
                                isLong = false;
                                psar[i] = extremePoint;
                                extremePoint = m_lowData[i];
                                acc = accInitial;
                            } else {
                                if (m_highData[i] > extremePoint) {
                                    extremePoint = m_highData[i];
                                    acc = acc + accIncrement;
                                }

                                if (m_lowData[i_1] < psar[i]) {
                                    psar[i] = m_lowData[i_1];
                                }
                                if (m_lowData[i_2] < psar[i]) {
                                    psar[i] = m_lowData[i_2];
                                }
                            }
                        } else {
                            if (m_highData[i] > psar[i]) {
                                isLong = true;
                                psar[i] = extremePoint;
                                extremePoint = m_highData[i];
                                acc = accInitial;
                            } else {
                                if (m_lowData[i] < extremePoint) {
                                    extremePoint = m_lowData[i];
                                    acc = acc + accIncrement;
                                }

                                if (m_highData[i_1] > psar[i]) {
                                    psar[i] = m_highData[i_1];
                                }
                                if (m_highData[i_2] > psar[i]) {
                                    psar[i] = m_highData[i_2];
                                }
                            }
                        }
                    }

                    i_2 = i_1;
                    i_1 = i;
                }
            }

            LineLayer ret = addLineIndicator2(m_mainChart, psar, fillColor, "Parabolic SAR");
            ret.setLineWidth(0);

            ret = addLineIndicator2(m_mainChart, psar, fillColor, "");
            ret.setLineWidth(0);
            ret.getDataSet(0).setDataSymbol(symbolType, symbolSize, fillColor, edgeColor);
            return ret;
        }

        /// <summary>
        /// Add a comparison line to the main price chart.
        /// </summary>
        /// <param name="data">The data series to compare to</param>
        /// <param name="color">The color of the comparison line</param>
        /// <param name="name">The name of the comparison line</param>
        /// <returns>The LineLayer object representing the line layer created.</returns>
        public LineLayer addComparison(double[] data, int color, string name)
        {
            int firstIndex = m_extraPoints;
            while ((firstIndex < data.Length) && (firstIndex < m_closeData.Length)) {
                if ((data[firstIndex] != Chart.NoValue) && (m_closeData[firstIndex] != Chart.NoValue
                    ) && (data[firstIndex] != 0) && (m_closeData[firstIndex] != 0)) {
                    break;
                }
                firstIndex = firstIndex + 1;
            }
            if ((firstIndex >= data.Length) || (firstIndex >= m_closeData.Length)) {
                return null;
            }

            double scaleFactor = m_closeData[firstIndex] / data[firstIndex];
            LineLayer layer = m_mainChart.addLineLayer(new ArrayMath(data).mul(scaleFactor).result(
                ), Chart.Transparent);
            layer.setHTMLImageMap("{disable}");

            Axis a = m_mainChart.addAxis(Chart.Right, 0);
            a.setColors(Chart.Transparent, Chart.Transparent);
            a.syncAxis(m_mainChart.yAxis(), 1 / scaleFactor, 0);

            LineLayer ret = addLineIndicator2(m_mainChart, data, color, name);
            ret.setUseYAxis(a);
            return ret;
        }

        /// <summary>
        /// Display percentage axis scale
        /// </summary>
        /// <returns>The Axis object representing the percentage axis.</returns>
        public Axis setPercentageAxis()
        {
            double firstClose = firstCloseValue();
            if (firstClose == Chart.NoValue) {
                return null;
            }

            int axisAlign = Chart.Left;
            if (m_axisOnRight) {
                axisAlign = Chart.Right;
            }

            Axis ret = m_mainChart.addAxis(axisAlign, 0);
            configureYAxis(ret, 300);
            ret.syncAxis(m_mainChart.yAxis(), 100 / firstClose);
            ret.setRounding(false, false);
            ret.setLabelFormat("{={value}-100|@}%");
            m_mainChart.yAxis().setColors(Chart.Transparent, Chart.Transparent);
            m_mainChart.getPlotArea().setGridAxis(null, ret);
            return ret;
        }

        /// <summary>
        /// Add a generic band to the main finance chart. This method is used internally by other methods to add
        /// various bands (eg. Bollinger band, Donchian channels, etc).
        /// </summary>
        /// <param name="upperLine">The data series for the upper band line.</param>
        /// <param name="lowerLine">The data series for the lower band line.</param>
        /// <param name="lineColor">The color of the upper and lower band line.</param>
        /// <param name="fillColor">The color to fill the region between the upper and lower band lines.</param>
        /// <param name="name">The name of the band.</param>
        /// <returns>An InterLineLayer object representing the filled region.</returns>
        public InterLineLayer addBand(double[] upperLine, double[] lowerLine, int lineColor,
            int fillColor, string name)
        {
            int i = upperLine.Length - 1;
            if (i >= lowerLine.Length) {
                i = lowerLine.Length - 1;
            }

            while (i >= 0) {
                if ((upperLine[i] != Chart.NoValue) && (lowerLine[i] != Chart.NoValue)) {
                    name = name + ": " + formatValue(lowerLine[i], m_generalFormat) + " - " +
                        formatValue(upperLine[i], m_generalFormat);
                    break;
                }
                i = i - 1;
            }

            LineLayer layer = m_mainChart.addLineLayer2();
            layer.addDataSet(upperLine, lineColor, name);
            layer.addDataSet(lowerLine, lineColor);
            return m_mainChart.addInterLineLayer(layer.getLine(0), layer.getLine(1), fillColor);
        }

        /// <summary>
        /// This method is for backward compatibility.
        /// - use addBand(double[], double[], int, int, string) instead.
        /// </summary>
        public InterLineLayer addBand(ArrayMath upperLine, ArrayMath lowerLine, int lineColor,
            int fillColor, string name)
        {
            return addBand(upperLine.result(), lowerLine.result(), lineColor, fillColor, name);
        }

        /// <summary>
        /// Add a Bollinger band on the main chart.
        /// </summary>
        /// <param name="period">The period to compute the band.</param>
        /// <param name="bandWidth">The half-width of the band in terms multiples of standard deviation. Typically 2 is used.</param>
        /// <param name="lineColor">The color of the lines defining the upper and lower limits.</param>
        /// <param name="fillColor">The color to fill the regional within the band.</param>
        /// <returns>The InterLineLayer object representing the band created.</returns>
        public InterLineLayer addBollingerBand(int period, double bandWidth, int lineColor,
            int fillColor)
        {
            //Bollinger Band is moving avg +/- (width * moving std deviation)
            double[] stdDev = new ArrayMath(m_closeData).movStdDev(period).mul(bandWidth).result();
            double[] movAvg = new ArrayMath(m_closeData).movAvg(period).result();
            string label = "Bollinger (" + period + ", " + bandWidth + ")";
            return addBand(new ArrayMath(movAvg).add(stdDev).result(), new ArrayMath(movAvg).sub(
                stdDev).selectGTZ(null, 0).result(), lineColor, fillColor, label);
        }

        /// <summary>
        /// Add a Donchian channel on the main chart.
        /// </summary>
        /// <param name="period">The period to compute the band.</param>
        /// <param name="lineColor">The color of the lines defining the upper and lower limits.</param>
        /// <param name="fillColor">The color to fill the regional within the band.</param>
        /// <returns>The InterLineLayer object representing the band created.</returns>
        public InterLineLayer addDonchianChannel(int period, int lineColor, int fillColor)
        {
            //Donchian Channel is the zone between the moving max and moving min
            string label = "Donchian (" + period + ")";
            return addBand(new ArrayMath(m_highData).movMax(period).result(), new ArrayMath(
                m_lowData).movMin(period).result(), lineColor, fillColor, label);
        }

        /// <summary>
        /// Add a price envelop on the main chart. The price envelop is a defined as a ratio around a
        /// moving average. For example, a ratio of 0.2 means 20% above and below the moving average.
        /// </summary>
        /// <param name="period">The period for the moving average.</param>
        /// <param name="range">The ratio above and below the moving average.</param>
        /// <param name="lineColor">The color of the lines defining the upper and lower limits.</param>
        /// <param name="fillColor">The color to fill the regional within the band.</param>
        /// <returns>The InterLineLayer object representing the band created.</returns>
        public InterLineLayer addEnvelop(int period, double range, int lineColor, int fillColor)
        {
            //Envelop is moving avg +/- percentage
            double[] movAvg = new ArrayMath(m_closeData).movAvg(period).result();
            string label = "Envelop (SMA " + period + " +/- " + (int)(range * 100) + "%)";
            return addBand(new ArrayMath(movAvg).mul(1 + range).result(), new ArrayMath(movAvg).mul(
                1 - range).result(), lineColor, fillColor, label);
        }

        /// <summary>
        /// Add a volume bar chart layer on the main chart.
        /// </summary>
        /// <param name="height">The height of the bar chart layer in pixels.</param>
        /// <param name="upColor">The color to used on an 'up' day. An 'up' day is a day where
        /// the closing price is higher than that of the previous day.</param>
        /// <param name="downColor">The color to used on a 'down' day. A 'down' day is a day
        /// where the closing price is lower than that of the previous day.</param>
        /// <param name="flatColor">The color to used on a 'flat' day. A 'flat' day is a day
        /// where the closing price is the same as that of the previous day.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public BarLayer addVolBars(int height, int upColor, int downColor, int flatColor)
        {
            return addVolBars2(m_mainChart, height, upColor, downColor, flatColor);
        }

        private BarLayer addVolBars2(XYChart c, int height, int upColor, int downColor,
            int flatColor)
        {
            BarLayer barLayer = c.addBarLayer2(Chart.Overlay);
            barLayer.setBorderColor(Chart.Transparent);

            if (c == m_mainChart) {
                configureYAxis(c.yAxis2(), height);
                int topMargin = c.getDrawArea().getHeight() - m_topMargin - m_bottomMargin - height
                     + m_yAxisMargin;
                if (topMargin < 0) {
                    topMargin = 0;
                }
                c.yAxis2().setTopMargin(topMargin);
                barLayer.setUseYAxis2();
            }

            Axis a = c.yAxis2();
            if (c != m_mainChart) {
                a = c.yAxis();
            }
            if (new ArrayMath(m_volData).max() < 10) {
                a.setLabelFormat("{value|1}" + m_volUnit);
            } else {
                a.setLabelFormat("{value}" + m_volUnit);
            }

            double[] closeChange = new ArrayMath(m_closeData).delta().replace(Chart.NoValue, 0
                ).result();
            int i = lastIndex(m_volData);
            string label = "Vol";
            if (i >= 0) {
                label = label + ": " + formatValue(m_volData[i], m_generalFormat) + m_volUnit;
            }

            DataSet upDS = barLayer.addDataSet(new ArrayMath(m_volData).selectGTZ(closeChange
                ).result(), upColor);
            DataSet dnDS = barLayer.addDataSet(new ArrayMath(m_volData).selectLTZ(closeChange
                ).result(), downColor);
            DataSet flatDS = barLayer.addDataSet(new ArrayMath(m_volData).selectEQZ(closeChange
                ).result(), flatColor);

            if ((i < 0) || (closeChange[i] == 0) || (closeChange[i] == Chart.NoValue)) {
                flatDS.setDataName(label);
            } else if (closeChange[i] > 0) {
                upDS.setDataName(label);
            } else {
                dnDS.setDataName(label);
            }

            return barLayer;
        }

        /// <summary>
        /// Add a blank indicator chart to the finance chart. Used internally to add other indicators.
        /// Override to change the default formatting (eg. axis fonts, etc.) of the various indicators.
        /// </summary>
        /// <param name="height">The height of the chart in pixels.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addIndicator(int height)
        {
            //create a new chart object
            XYChart ret = new XYChart(m_totalWidth, height + m_topMargin + m_bottomMargin,
                Chart.Transparent);
            ret.setTrimData(m_extraPoints);

            if (m_currentChart != null) {
                //if there is a chart before the newly created chart, disable its x-axis, and copy
                //its x-axis labels to the new chart
                m_currentChart.xAxis().setColors(Chart.Transparent, Chart.Transparent);
                ret.xAxis().copyAxis(m_currentChart.xAxis());

                //add chart to MultiChart and update the total height
                addChart(0, m_totalHeight + m_plotAreaGap, ret);
                m_totalHeight = m_totalHeight + height + 1 + m_plotAreaGap;
            } else {
                //no existing chart - create the x-axis labels from scratch
                setXLabels(ret.xAxis());

                //add chart to MultiChart and update the total height
                addChart(0, m_totalHeight, ret);
                m_totalHeight = m_totalHeight + height + 1;
            }

            //the newly created chart becomes the current chart
            m_currentChart = ret;

            //update the size
            setSize(m_totalWidth, m_totalHeight + m_topMargin + m_bottomMargin);

            //configure the plot area
            ret.setPlotArea(m_leftMargin, m_topMargin, m_totalWidth - m_leftMargin - m_rightMargin,
                height, m_plotAreaBgColor, -1, m_plotAreaBorder).setGridColor(m_majorHGridColor,
                m_majorVGridColor, m_minorHGridColor, m_minorVGridColor);
            ret.setAntiAlias(m_antiAlias);

            //configure legend box
            if (m_legendFontColor != Chart.Transparent) {
                LegendBox box = ret.addLegend(m_leftMargin, m_topMargin, false, m_legendFont,
                    m_legendFontSize);
                box.setFontColor(m_legendFontColor);
                box.setBackground(m_legendBgColor);
                box.setMargin2(5, 0, 2, 1);
                box.setSize(m_totalWidth - m_leftMargin - m_rightMargin + 1, 0);
            }

            //configure x-axis
            Axis a = ret.xAxis();
            a.setIndent(true);
            a.setTickLength(2, 0);
            a.setColors(Chart.Transparent, m_xAxisFontColor, m_xAxisFontColor, m_xAxisFontColor);
            a.setLabelStyle(m_xAxisFont, m_xAxisFontSize, m_xAxisFontColor, m_xAxisFontAngle);

            //configure y-axis
            ret.setYAxisOnRight(m_axisOnRight);
            configureYAxis(ret.yAxis(), height);

            return ret;
        }

        private void configureYAxis(Axis a, int height)
        {
            a.setAutoScale(0, 0.05, 0);
            if (height < 100) {
                a.setTickDensity(15);
            }
            a.setMargin(m_yAxisMargin);
            a.setLabelStyle(m_yAxisFont, m_yAxisFontSize, m_yAxisFontColor, 0);
            a.setTickLength(-4, -2);
            a.setColors(Chart.Transparent, m_yAxisFontColor, m_yAxisFontColor, m_yAxisFontColor);
        }

        /// <summary>
        /// Add a generic line indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="data">The data series of the indicator line.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <param name="name">The name of the indicator.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addLineIndicator(int height, double[] data, int color, string name)
        {
            XYChart c = addIndicator(height);
            addLineIndicator2(c, data, color, name);
            return c;
        }

        /// <summary>
        /// This method is for backward compatibility.
        // - use addLineIndicator(int, double[], int, string) instead.
        /// </summary>
        public XYChart addLineIndicator(int height, ArrayMath data, int color, string name)
        {
            return addLineIndicator(height, data.result(), color, name);
        }

        /// <summary>
        /// Add a line to an existing indicator chart.
        /// </summary>
        /// <param name="c">The indicator chart to add the line to.</param>
        /// <param name="data">The data series of the indicator line.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <param name="name">The name of the indicator.</param>
        /// <returns>The LineLayer object representing the line created.</returns>
        public LineLayer addLineIndicator2(XYChart c, double[] data, int color, string name)
        {
            return c.addLineLayer(data, color, formatIndicatorLabel(name, data));
        }

        /// <summary>
        /// This method is for backward compatibility.
        // - use addLineIndicator2(XYChart c, double[], int, string) instead.
        /// </summary>
        public LineLayer addLineIndicator2(XYChart c, ArrayMath data, int color, string name)
        {
            return addLineIndicator2(c, data.result(), color, name);
        }

        /// <summary>
        /// Add a generic bar indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="data">The data series of the indicator bars.</param>
        /// <param name="color">The color of the indicator bars.</param>
        /// <param name="name">The name of the indicator.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addBarIndicator(int height, double[] data, int color, string name)
        {
            XYChart c = addIndicator(height);
            addBarIndicator2(c, data, color, name);
            return c;
        }

        /// <summary>
        /// This method is for backward compatibility.
        // - use addBarIndicator(int, double[], int, string) instead.
        /// </summary>
        public XYChart addBarIndicator(int height, ArrayMath data, int color, string name)
        {
            return addBarIndicator(height, data.result(), color, name);
        }

        /// <summary>
        /// Add a bar layer to an existing indicator chart.
        /// </summary>
        /// <param name="c">The indicator chart to add the bar layer to.</param>
        /// <param name="data">The data series of the indicator bars.</param>
        /// <param name="color">The color of the indicator bars.</param>
        /// <param name="name">The name of the indicator.</param>
        /// <returns>The BarLayer object representing the bar layer created.</returns>
        public BarLayer addBarIndicator2(XYChart c, double[] data, int color, string name)
        {
            BarLayer layer = c.addBarLayer(data, color, formatIndicatorLabel(name, data));
            layer.setBorderColor(Chart.Transparent);
            return layer;
        }

        /// <summary>
        /// This method is for backward compatibility.
        // - use addBarIndicator2(XYChart c, double[], int, string) instead.
        /// </summary>
        public BarLayer addBarIndicator2(XYChart c, ArrayMath data, int color, string name)
        {
            return addBarIndicator2(c, data.result(), color, name);
        }

        /// <summary>
        /// Add an upper/lower threshold range to an existing indicator chart.
        /// </summary>
        /// <param name="c">The indicator chart to add the threshold range to.</param>
        /// <param name="layer">The line layer that the threshold range applies to.</param>
        /// <param name="topRange">The upper threshold.</param>
        /// <param name="topColor">The color to fill the region of the line that is above the
        /// upper threshold.</param>
        /// <param name="bottomRange">The lower threshold.</param>
        /// <param name="bottomColor">The color to fill the region of the line that is below
        /// the lower threshold.</param>
        public void addThreshold(XYChart c, LineLayer layer, double topRange, int topColor,
            double bottomRange, int bottomColor)
        {
            Mark topMark = c.yAxis().addMark(topRange, topColor, formatValue(topRange,
                m_generalFormat));
            Mark bottomMark = c.yAxis().addMark(bottomRange, bottomColor, formatValue(bottomRange,
                m_generalFormat));

            c.addInterLineLayer(layer.getLine(), topMark.getLine(), topColor, Chart.Transparent);
            c.addInterLineLayer(layer.getLine(), bottomMark.getLine(), Chart.Transparent,
                bottomColor);
        }

        private string formatIndicatorLabel(string name, double[] data)
        {
            int i = lastIndex(data);
            if (name == null) {
                return name;
            }
            if ((name == "") || (i < 0)) {
                return name;
            }
            string ret = name + ": " + formatValue(data[i], m_generalFormat);
            return ret;
        }

        /// <summary>
        /// Add an Accumulation/Distribution indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addAccDist(int height, int color)
        {
            //Close Location Value = ((C - L) - (H - C)) / (H - L)
            //Accumulation Distribution Line = Accumulation of CLV * volume
            double[] range = new ArrayMath(m_highData).sub(m_lowData).result();
            return addLineIndicator(height, new ArrayMath(m_closeData).mul(2).sub(m_lowData).sub(
                m_highData).mul(m_volData).financeDiv(range, 0).acc().result(), color,
                "Accumulation/Distribution");
        }

        private double[] computeAroonUp(int period)
        {
            double[] aroonUp = new double[m_highData.Length];
            for(int i = 0; i < m_highData.Length; ++i) {
                double highValue = m_highData[i];
                if (highValue == Chart.NoValue) {
                    aroonUp[i] = Chart.NoValue;
                } else {
                    int currentIndex = i;
                    int highCount = period;
                    int count = period;

                    while ((count > 0) && (currentIndex >= count)) {
                        currentIndex = currentIndex - 1;
                        double currentValue = m_highData[currentIndex];
                        if (currentValue != Chart.NoValue) {
                            count = count - 1;
                            if (currentValue > highValue) {
                                highValue = currentValue;
                                highCount = count;
                            }
                        }
                    }

                    if (count > 0) {
                        aroonUp[i] = Chart.NoValue;
                    } else {
                        aroonUp[i] = highCount * 100.0 / period;
                    }
                }
            }

            return aroonUp;
        }

        private double[] computeAroonDn(int period)
        {
            double[] aroonDn = new double[m_lowData.Length];
            for(int i = 0; i < m_lowData.Length; ++i) {
                double lowValue = m_lowData[i];
                if (lowValue == Chart.NoValue) {
                    aroonDn[i] = Chart.NoValue;
                } else {
                    int currentIndex = i;
                    int lowCount = period;
                    int count = period;

                    while ((count > 0) && (currentIndex >= count)) {
                        currentIndex = currentIndex - 1;
                        double currentValue = m_lowData[currentIndex];
                        if (currentValue != Chart.NoValue) {
                            count = count - 1;
                            if (currentValue < lowValue) {
                                lowValue = currentValue;
                                lowCount = count;
                            }
                        }
                    }

                    if (count > 0) {
                        aroonDn[i] = Chart.NoValue;
                    } else {
                        aroonDn[i] = lowCount * 100.0 / period;
                    }
                }
            }

            return aroonDn;
        }

        /// <summary>
        /// Add an Aroon Up/Down indicators chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the indicators.</param>
        /// <param name="upColor">The color of the Aroon Up indicator line.</param>
        /// <param name="downColor">The color of the Aroon Down indicator line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addAroon(int height, int period, int upColor, int downColor)
        {
            XYChart c = addIndicator(height);
            addLineIndicator2(c, computeAroonUp(period), upColor, "Aroon Up");
            addLineIndicator2(c, computeAroonDn(period), downColor, "Aroon Down");
            c.yAxis().setLinearScale(0, 100);
            return c;
        }

        /// <summary>
        /// Add an Aroon Oscillator indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the indicator.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addAroonOsc(int height, int period, int color)
        {
            string label = "Aroon Oscillator (" + period + ")";
            XYChart c = addLineIndicator(height, new ArrayMath(computeAroonUp(period)).sub(
                computeAroonDn(period)).result(), color, label);
            c.yAxis().setLinearScale(-100, 100);
            return c;
        }

        private double[] computeTrueRange()
        {
            double[] previousClose = new ArrayMath(m_closeData).shift().result();
            double[] ret = new ArrayMath(m_highData).sub(m_lowData).result();
            double temp = 0;

            for(int i = 0; i < m_highData.Length; ++i) {
                if ((ret[i] != Chart.NoValue) && (previousClose[i] != Chart.NoValue)) {
                    temp = Math.Abs(m_highData[i] - previousClose[i]);
                    if (temp > ret[i]) {
                        ret[i] = temp;
                    }
                    temp = Math.Abs(previousClose[i] - m_lowData[i]);
                    if (temp > ret[i]) {
                        ret[i] = temp;
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Add an Average Directional Index indicators chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the indicator.</param>
        /// <param name="posColor">The color of the Positive Directional Index line.</param>
        /// <param name="negColor">The color of the Negatuve Directional Index line.</param>
        /// <param name="color">The color of the Average Directional Index line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addADX(int height, int period, int posColor, int negColor, int color)
        {
            //pos/neg directional movement
            ArrayMath pos = new ArrayMath(m_highData).delta().selectGTZ();
            ArrayMath neg = new ArrayMath(m_lowData).delta().mul(-1).selectGTZ();
            double[] delta = new ArrayMath(pos.result()).sub(neg.result()).result();
            pos.selectGTZ(delta);
            neg.selectLTZ(delta);

            //initial value
            double[] posData = pos.result();
            double[] negData = neg.result();
            if ((posData.Length > 1) && (posData[1] != Chart.NoValue) && (negData[1] !=
                Chart.NoValue)) {
                posData[1] = (posData[1] * 2 + negData[1]) / 3;
                negData[1] = (negData[1] + posData[1]) / 2;
                pos = new ArrayMath(posData);
                neg = new ArrayMath(negData);
            }

            //pos/neg directional index
            double[] tr = computeTrueRange();
            tr = new ArrayMath(tr).expAvg(1.0 / period).result();
            pos.expAvg(1.0 / period).financeDiv(tr, 0).mul(100);
            neg.expAvg(1.0 / period).financeDiv(tr, 0).mul(100);

            //directional movement index ??? what happen if division by zero???
            double[] totalDM = new ArrayMath(pos.result()).add(neg.result()).result();
            ArrayMath dx = new ArrayMath(pos.result()).sub(neg.result()).abs().financeDiv(totalDM, 0
                ).mul(100).expAvg(1.0 / period);

            XYChart c = addIndicator(height);
            string label1 = "+DI (" + period + ")";
            string label2 = "-DI (" + period + ")";
            string label3 = "ADX (" + period + ")";
            addLineIndicator2(c, pos.result(), posColor, label1);
            addLineIndicator2(c, neg.result(), negColor, label2);
            addLineIndicator2(c, dx.result(), color, label3);
            return c;
        }

        /// <summary>
        /// Add an Average True Range indicators chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the indicator.</param>
        /// <param name="color1">The color of the True Range line.</param>
        /// <param name="color2">The color of the Average True Range line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addATR(int height, int period, int color1, int color2)
        {
            double[] trueRange = computeTrueRange();
            XYChart c = addLineIndicator(height, trueRange, color1, "True Range");
            string label = "Average True Range (" + period + ")";
            addLineIndicator2(c, new ArrayMath(trueRange).expAvg(2.0 / (period + 1)).result(),
                color2, label);
            return c;
        }

        /// <summary>
        /// Add a Bollinger Band Width indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the indicator.</param>
        /// <param name="width">The band width to compute the indicator.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addBollingerWidth(int height, int period, double width, int color)
        {
            string label = "Bollinger Width (" + period + ", " + width + ")";
            return addLineIndicator(height, new ArrayMath(m_closeData).movStdDev(period).mul(width *
                2).result(), color, label);
        }

        /// <summary>
        /// Add a Community Channel Index indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the indicator.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <param name="deviation">The distance beween the middle line and the upper and lower threshold lines.</param>
        /// <param name="upColor">The fill color when the indicator exceeds the upper threshold line.</param>
        /// <param name="downColor">The fill color when the indicator falls below the lower threshold line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addCCI(int height, int period, int color, double deviation, int upColor,
            int downColor)
        {
            //typical price
            double[] tp = new ArrayMath(m_highData).add(m_lowData).add(m_closeData).div(3).result();

            //simple moving average of typical price
            double[] smvtp = new ArrayMath(tp).movAvg(period).result();

            //compute mean deviation
            double[] movMeanDev = new double[smvtp.Length];
            for(int i = 0; i < smvtp.Length; ++i) {
                double avg = smvtp[i];
                if (avg == Chart.NoValue) {
                    movMeanDev[i] = Chart.NoValue;
                } else {
                    int currentIndex = i;
                    int count = period - 1;
                    double acc = 0;

                    while ((count >= 0) && (currentIndex >= count)) {
                        double currentValue = tp[currentIndex];
                        currentIndex = currentIndex - 1;
                        if (currentValue != Chart.NoValue) {
                            count = count - 1;
                            acc = acc + Math.Abs(avg - currentValue);
                        }
                    }

                    if (count > 0) {
                        movMeanDev[i] = Chart.NoValue;
                    } else {
                        movMeanDev[i] = acc / period;
                    }
                }
            }

            XYChart c = addIndicator(height);
            string label = "CCI (" + period + ")";
            LineLayer layer = addLineIndicator2(c, new ArrayMath(tp).sub(smvtp).financeDiv(
                movMeanDev, 0).div(0.015).result(), color, label);
            addThreshold(c, layer, deviation, upColor, -deviation, downColor);
            return c;
        }

        /// <summary>
        /// Add a Chaikin Money Flow indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the indicator.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addChaikinMoneyFlow(int height, int period, int color)
        {
            double[] range = new ArrayMath(m_highData).sub(m_lowData).result();
            double[] volAvg = new ArrayMath(m_volData).movAvg(period).result();
            string label = "Chaikin Money Flow (" + period + ")";
            return addBarIndicator(height, new ArrayMath(m_closeData).mul(2).sub(m_lowData).sub(
                m_highData).mul(m_volData).financeDiv(range, 0).movAvg(period).financeDiv(volAvg, 0
                ).result(), color, label);
        }

        /// <summary>
        /// Add a Chaikin Oscillator indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addChaikinOscillator(int height, int color)
        {
            //first compute acc/dist line
            double[] range = new ArrayMath(m_highData).sub(m_lowData).result();
            double[] accdist = new ArrayMath(m_closeData).mul(2).sub(m_lowData).sub(m_highData).mul(
                m_volData).financeDiv(range, 0).acc().result();

            //chaikin osc = exp3(accdist) - exp10(accdist)
            double[] expAvg10 = new ArrayMath(accdist).expAvg(2.0 / (10 + 1)).result();
            return addLineIndicator(height, new ArrayMath(accdist).expAvg(2.0 / (3 + 1)).sub(
                expAvg10).result(), color, "Chaikin Oscillator");
        }

        /// <summary>
        /// Add a Chaikin Volatility indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period1">The period to smooth the range.</param>
        /// <param name="period2">The period to compute the rate of change of the smoothed range.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addChaikinVolatility(int height, int period1, int period2, int color)
        {
            string label = "Chaikin Volatility (" + period1 + ", " + period2 + ")";
            return addLineIndicator(height, new ArrayMath(m_highData).sub(m_lowData).expAvg(2.0 / (
                period1 + 1)).rate(period2).sub(1).mul(100).result(), color, label);
        }

        /// <summary>
        /// Add a Close Location Value indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addCLV(int height, int color)
        {
            //Close Location Value = ((C - L) - (H - C)) / (H - L)
            double[] range = new ArrayMath(m_highData).sub(m_lowData).result();
            return addLineIndicator(height, new ArrayMath(m_closeData).mul(2).sub(m_lowData).sub(
                m_highData).financeDiv(range, 0).result(), color, "Close Location Value");
        }

        /// <summary>
        /// Add a Detrended Price Oscillator indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the indicator.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addDPO(int height, int period, int color)
        {
            string label = "DPO (" + period + ")";
            return addLineIndicator(height, new ArrayMath(m_closeData).movAvg(period).shift(period /
                2 + 1).sub(m_closeData).mul(-1).result(), color, label);
        }

        /// <summary>
        /// Add a Donchian Channel Width indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the indicator.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addDonchianWidth(int height, int period, int color)
        {
            string label = "Donchian Width (" + period + ")";
            return addLineIndicator(height, new ArrayMath(m_highData).movMax(period).sub(
                new ArrayMath(m_lowData).movMin(period).result()).result(), color, label);
        }

        /// <summary>
        /// Add a Ease of Movement indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to smooth the indicator.</param>
        /// <param name="color1">The color of the indicator line.</param>
        /// <param name="color2">The color of the smoothed indicator line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addEaseOfMovement(int height, int period, int color1, int color2)
        {
            double[] boxRatioInverted = new ArrayMath(m_highData).sub(m_lowData).financeDiv(
                m_volData, 0).result();
            double[] result = new ArrayMath(m_highData).add(m_lowData).div(2).delta().mul(
                boxRatioInverted).result();

            XYChart c = addLineIndicator(height, result, color1, "EMV");
            string label = "EMV EMA (" + period + ")";
            addLineIndicator2(c, new ArrayMath(result).movAvg(period).result(), color2, label);
            return c;
        }

        /// <summary>
        /// Add a Fast Stochastic indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period1">The period to compute the %K line.</param>
        /// <param name="period2">The period to compute the %D line.</param>
        /// <param name="color1">The color of the %K line.</param>
        /// <param name="color2">The color of the %D line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addFastStochastic(int height, int period1, int period2, int color1,
            int color2)
        {
            double[] movLow = new ArrayMath(m_lowData).movMin(period1).result();
            double[] movRange = new ArrayMath(m_highData).movMax(period1).sub(movLow).result();
            double[] stochastic = new ArrayMath(m_closeData).sub(movLow).financeDiv(movRange, 0.5
                ).mul(100).result();

            string label1 = "Fast Stochastic %K (" + period1 + ")";
            XYChart c = addLineIndicator(height, stochastic, color1, label1);
            string label2 = "%D (" + period2 + ")";
            addLineIndicator2(c, new ArrayMath(stochastic).movAvg(period2).result(), color2, label2)
                ;

            c.yAxis().setLinearScale(0, 100);
            return c;
        }

        /// <summary>
        /// Add a MACD indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period1">The first moving average period to compute the indicator.</param>
        /// <param name="period2">The second moving average period to compute the indicator.</param>
        /// <param name="period3">The moving average period of the signal line.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <param name="signalColor">The color of the signal line.</param>
        /// <param name="divColor">The color of the divergent bars.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addMACD(int height, int period1, int period2, int period3, int color,
            int signalColor, int divColor)
        {
            XYChart c = addIndicator(height);

            //MACD is defined as the difference between two exponential averages (typically 12/26 days)
            double[] expAvg1 = new ArrayMath(m_closeData).expAvg(2.0 / (period1 + 1)).result();
            double[] macd = new ArrayMath(m_closeData).expAvg(2.0 / (period2 + 1)).sub(expAvg1
                ).result();

            //Add the MACD line
            string label1 = "MACD (" + period1 + ", " + period2 + ")";
            addLineIndicator2(c, macd, color, label1);

            //MACD signal line
            double[] macdSignal = new ArrayMath(macd).expAvg(2.0 / (period3 + 1)).result();
            string label2 = "EXP (" + period3 + ")";
            addLineIndicator2(c, macdSignal, signalColor, label2);

            //Divergence
            addBarIndicator2(c, new ArrayMath(macd).sub(macdSignal).result(), divColor, "Divergence"
                );

            return c;
        }

        /// <summary>
        /// Add a Mass Index indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <param name="upColor">The fill color when the indicator exceeds the upper threshold line.</param>
        /// <param name="downColor">The fill color when the indicator falls below the lower threshold line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addMassIndex(int height, int color, int upColor, int downColor)
        {
            //Mass Index
            double f = 2.0 / (10);
            double[] exp9 = new ArrayMath(m_highData).sub(m_lowData).expAvg(f).result();
            double[] exp99 = new ArrayMath(exp9).expAvg(f).result();

            XYChart c = addLineIndicator(height, new ArrayMath(exp9).financeDiv(exp99, 1).movAvg(25
                ).mul(25).result(), color, "Mass Index");
            c.yAxis().addMark(27, upColor);
            c.yAxis().addMark(26.5, downColor);
            return c;
        }

        /// <summary>
        /// Add a Money Flow Index indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the indicator.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <param name="range">The distance beween the middle line and the upper and lower threshold lines.</param>
        /// <param name="upColor">The fill color when the indicator exceeds the upper threshold line.</param>
        /// <param name="downColor">The fill color when the indicator falls below the lower threshold line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addMFI(int height, int period, int color, double range, int upColor,
            int downColor)
        {
            //Money Flow Index
            double[] typicalPrice = new ArrayMath(m_highData).add(m_lowData).add(m_closeData).div(3
                ).result();
            double[] moneyFlow = new ArrayMath(typicalPrice).mul(m_volData).result();

            double[] selector = new ArrayMath(typicalPrice).delta().result();
            double[] posMoneyFlow = new ArrayMath(moneyFlow).selectGTZ(selector).movAvg(period
                ).result();
            double[] posNegMoneyFlow = new ArrayMath(moneyFlow).selectLTZ(selector).movAvg(period
                ).add(posMoneyFlow).result();

            XYChart c = addIndicator(height);
            string label = "Money Flow Index (" + period + ")";
            LineLayer layer = addLineIndicator2(c, new ArrayMath(posMoneyFlow).financeDiv(
                posNegMoneyFlow, 0.5).mul(100).result(), color, label);
            addThreshold(c, layer, 50 + range, upColor, 50 - range, downColor);

            c.yAxis().setLinearScale(0, 100);
            return c;
        }

        /// <summary>
        /// Add a Momentum indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the indicator.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addMomentum(int height, int period, int color)
        {
            string label = "Momentum (" + period + ")";
            return addLineIndicator(height, new ArrayMath(m_closeData).delta(period).result(),
                color, label);
        }

        /// <summary>
        /// Add a Negative Volume Index indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the signal line.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <param name="signalColor">The color of the signal line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addNVI(int height, int period, int color, int signalColor)
        {
            double[] nvi = new double[m_volData.Length];

            double previousNVI = 100;
            double previousVol = Chart.NoValue;
            double previousClose = Chart.NoValue;
            for(int i = 0; i < m_volData.Length; ++i) {
                if (m_volData[i] == Chart.NoValue) {
                    nvi[i] = Chart.NoValue;
                } else {
                    if ((previousVol != Chart.NoValue) && (m_volData[i] < previousVol) && (
                        previousClose != Chart.NoValue) && (m_closeData[i] != Chart.NoValue)) {
                        nvi[i] = previousNVI + previousNVI * (m_closeData[i] - previousClose) /
                            previousClose;
                    } else {
                        nvi[i] = previousNVI;
                    }

                    previousNVI = nvi[i];
                    previousVol = m_volData[i];
                    previousClose = m_closeData[i];
                }
            }

            XYChart c = addLineIndicator(height, nvi, color, "NVI");
            if (nvi.Length > period) {
                string label = "NVI SMA (" + period + ")";
                addLineIndicator2(c, new ArrayMath(nvi).movAvg(period).result(), signalColor, label)
                    ;
            }
            return c;
        }

        /// <summary>
        /// Add an On Balance Volume indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addOBV(int height, int color)
        {
            double[] closeChange = new ArrayMath(m_closeData).delta().result();
            double[] upVolume = new ArrayMath(m_volData).selectGTZ(closeChange).result();
            double[] downVolume = new ArrayMath(m_volData).selectLTZ(closeChange).result();

            return addLineIndicator(height, new ArrayMath(upVolume).sub(downVolume).acc().result(),
                color, "OBV");
        }

        /// <summary>
        /// Add a Performance indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addPerformance(int height, int color)
        {
            double closeValue = firstCloseValue();
            if (closeValue != Chart.NoValue) {
                return addLineIndicator(height, new ArrayMath(m_closeData).mul(100 / closeValue
                    ).sub(100).result(), color, "Performance");
            } else {
                //chart is empty !!!
                return addIndicator(height);
            }
        }

        /// <summary>
        /// Add a Percentage Price Oscillator indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period1">The first moving average period to compute the indicator.</param>
        /// <param name="period2">The second moving average period to compute the indicator.</param>
        /// <param name="period3">The moving average period of the signal line.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <param name="signalColor">The color of the signal line.</param>
        /// <param name="divColor">The color of the divergent bars.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addPPO(int height, int period1, int period2, int period3, int color,
            int signalColor, int divColor)
        {
            double[] expAvg1 = new ArrayMath(m_closeData).expAvg(2.0 / (period1 + 1)).result();
            double[] expAvg2 = new ArrayMath(m_closeData).expAvg(2.0 / (period2 + 1)).result();
            ArrayMath ppo = new ArrayMath(expAvg2).sub(expAvg1).financeDiv(expAvg2, 0).mul(100);
            double[] ppoSignal = new ArrayMath(ppo.result()).expAvg(2.0 / (period3 + 1)).result();

            string label1 = "PPO (" + period1 + ", " + period2 + ")";
            string label2 = "EMA (" + period3 + ")";
            XYChart c = addLineIndicator(height, ppo.result(), color, label1);
            addLineIndicator2(c, ppoSignal, signalColor, label2);
            addBarIndicator2(c, ppo.sub(ppoSignal).result(), divColor, "Divergence");
            return c;
        }

        /// <summary>
        /// Add a Positive Volume Index indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the signal line.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <param name="signalColor">The color of the signal line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addPVI(int height, int period, int color, int signalColor)
        {
            //Positive Volume Index
            double[] pvi = new double[m_volData.Length];

            double previousPVI = 100;
            double previousVol = Chart.NoValue;
            double previousClose = Chart.NoValue;
            for(int i = 0; i < m_volData.Length; ++i) {
                if (m_volData[i] == Chart.NoValue) {
                    pvi[i] = Chart.NoValue;
                } else {
                    if ((previousVol != Chart.NoValue) && (m_volData[i] > previousVol) && (
                        previousClose != Chart.NoValue) && (m_closeData[i] != Chart.NoValue)) {
                        pvi[i] = previousPVI + previousPVI * (m_closeData[i] - previousClose) /
                            previousClose;
                    } else {
                        pvi[i] = previousPVI;
                    }

                    previousPVI = pvi[i];
                    previousVol = m_volData[i];
                    previousClose = m_closeData[i];
                }
            }

            XYChart c = addLineIndicator(height, pvi, color, "PVI");
            if (pvi.Length > period) {
                string label = "PVI SMA (" + period + ")";
                addLineIndicator2(c, new ArrayMath(pvi).movAvg(period).result(), signalColor, label)
                    ;
            }
            return c;
        }

        /// <summary>
        /// Add a Percentage Volume Oscillator indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period1">The first moving average period to compute the indicator.</param>
        /// <param name="period2">The second moving average period to compute the indicator.</param>
        /// <param name="period3">The moving average period of the signal line.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <param name="signalColor">The color of the signal line.</param>
        /// <param name="divColor">The color of the divergent bars.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addPVO(int height, int period1, int period2, int period3, int color,
            int signalColor, int divColor)
        {
            double[] expAvg1 = new ArrayMath(m_volData).expAvg(2.0 / (period1 + 1)).result();
            double[] expAvg2 = new ArrayMath(m_volData).expAvg(2.0 / (period2 + 1)).result();
            ArrayMath pvo = new ArrayMath(expAvg2).sub(expAvg1).financeDiv(expAvg2, 0).mul(100);
            double[] pvoSignal = new ArrayMath(pvo.result()).expAvg(2.0 / (period3 + 1)).result();

            string label1 = "PVO (" + period1 + ", " + period2 + ")";
            string label2 = "EMA (" + period3 + ")";
            XYChart c = addLineIndicator(height, pvo.result(), color, label1);
            addLineIndicator2(c, pvoSignal, signalColor, label2);
            addBarIndicator2(c, pvo.sub(pvoSignal).result(), divColor, "Divergence");
            return c;
        }

        /// <summary>
        /// Add a Price Volumne Trend indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addPVT(int height, int color)
        {
            return addLineIndicator(height, new ArrayMath(m_closeData).rate().sub(1).mul(m_volData
                ).acc().result(), color, "PVT");
        }

        /// <summary>
        /// Add a Rate of Change indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the indicator.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addROC(int height, int period, int color)
        {
            string label = "ROC (" + period + ")";
            return addLineIndicator(height, new ArrayMath(m_closeData).rate(period).sub(1).mul(100
                ).result(), color, label);
        }

        private double[] RSIMovAvg(double[] data, int period)
        {
            //The "moving average" in classical RSI is based on a formula that mixes simple
            //and exponential moving averages.

            if (period <= 0) {
                period = 1;
            }

            int count = 0;
            double acc = 0;

            for(int i = 0; i < data.Length; ++i) {
                if (Math.Abs(data[i] / Chart.NoValue - 1) > 1e-005) {
                    count = count + 1;
                    acc = acc + data[i];
                    if (count < period) {
                        data[i] = Chart.NoValue;
                    } else {
                        data[i] = acc / period;
                        acc = data[i] * (period - 1);
                    }
                }
            }

            return data;
        }

        private double[] computeRSI(int period)
        {
            //RSI is defined as the average up changes for the last 14 days, divided by the
            //average absolute changes for the last 14 days, expressed as a percentage.

            double[] absChange = RSIMovAvg(new ArrayMath(m_closeData).delta().abs().result(), period
                );
            double[] absUpChange = RSIMovAvg(new ArrayMath(m_closeData).delta().selectGTZ().result(
                ), period);
            return new ArrayMath(absUpChange).financeDiv(absChange, 0.5).mul(100).result();
        }

        /// <summary>
        /// Add a Relative Strength Index indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the indicator.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <param name="range">The distance beween the middle line and the upper and lower threshold lines.</param>
        /// <param name="upColor">The fill color when the indicator exceeds the upper threshold line.</param>
        /// <param name="downColor">The fill color when the indicator falls below the lower threshold line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addRSI(int height, int period, int color, double range, int upColor,
            int downColor)
        {
            XYChart c = addIndicator(height);
            string label = "RSI (" + period + ")";
            LineLayer layer = addLineIndicator2(c, computeRSI(period), color, label);

            //Add range if given
            if ((range > 0) && (range < 50)) {
                addThreshold(c, layer, 50 + range, upColor, 50 - range, downColor);
            }
            c.yAxis().setLinearScale(0, 100);
            return c;
        }

        /// <summary>
        /// Add a Slow Stochastic indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period1">The period to compute the %K line.</param>
        /// <param name="period2">The period to compute the %D line.</param>
        /// <param name="color1">The color of the %K line.</param>
        /// <param name="color2">The color of the %D line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addSlowStochastic(int height, int period1, int period2, int color1,
            int color2)
        {
            double[] movLow = new ArrayMath(m_lowData).movMin(period1).result();
            double[] movRange = new ArrayMath(m_highData).movMax(period1).sub(movLow).result();
            ArrayMath stochastic = new ArrayMath(m_closeData).sub(movLow).financeDiv(movRange, 0.5
                ).mul(100).movAvg(3);

            string label1 = "Slow Stochastic %K (" + period1 + ")";
            string label2 = "%D (" + period2 + ")";
            XYChart c = addLineIndicator(height, stochastic.result(), color1, label1);
            addLineIndicator2(c, stochastic.movAvg(period2).result(), color2, label2);

            c.yAxis().setLinearScale(0, 100);
            return c;
        }

        /// <summary>
        /// Add a Moving Standard Deviation indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the indicator.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addStdDev(int height, int period, int color)
        {
            string label = "Moving StdDev (" + period + ")";
            return addLineIndicator(height, new ArrayMath(m_closeData).movStdDev(period).result(),
                color, label);
        }

        /// <summary>
        /// Add a Stochastic RSI indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the indicator.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <param name="range">The distance beween the middle line and the upper and lower threshold lines.</param>
        /// <param name="upColor">The fill color when the indicator exceeds the upper threshold line.</param>
        /// <param name="downColor">The fill color when the indicator falls below the lower threshold line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addStochRSI(int height, int period, int color, double range, int upColor,
            int downColor)
        {
            double[] rsi = computeRSI(period);
            double[] movLow = new ArrayMath(rsi).movMin(period).result();
            double[] movRange = new ArrayMath(rsi).movMax(period).sub(movLow).result();

            XYChart c = addIndicator(height);
            string label = "StochRSI (" + period + ")";
            LineLayer layer = addLineIndicator2(c, new ArrayMath(rsi).sub(movLow).financeDiv(
                movRange, 0.5).mul(100).result(), color, label);

            //Add range if given
            if ((range > 0) && (range < 50)) {
                addThreshold(c, layer, 50 + range, upColor, 50 - range, downColor);
            }
            c.yAxis().setLinearScale(0, 100);
            return c;
        }

        /// <summary>
        /// Add a TRIX indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the indicator.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addTRIX(int height, int period, int color)
        {
            double f = 2.0 / (period + 1);
            string label = "TRIX (" + period + ")";
            return addLineIndicator(height, new ArrayMath(m_closeData).expAvg(f).expAvg(f).expAvg(f
                ).rate().sub(1).mul(100).result(), color, label);
        }

        private double[] computeTrueLow()
        {
            //the lower of today's low or yesterday's close.
            double[] previousClose = new ArrayMath(m_closeData).shift().result();
            double[] ret = new double[m_lowData.Length];
            for(int i = 0; i < m_lowData.Length; ++i) {
                if ((m_lowData[i] != Chart.NoValue) && (previousClose[i] != Chart.NoValue)) {
                    if (m_lowData[i] < previousClose[i]) {
                        ret[i] = m_lowData[i];
                    } else {
                        ret[i] = previousClose[i];
                    }
                } else {
                    ret[i] = Chart.NoValue;
                }
            }

            return ret;
        }

        /// <summary>
        /// Add an Ultimate Oscillator indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period1">The first moving average period to compute the indicator.</param>
        /// <param name="period2">The second moving average period to compute the indicator.</param>
        /// <param name="period3">The third moving average period to compute the indicator.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <param name="range">The distance beween the middle line and the upper and lower threshold lines.</param>
        /// <param name="upColor">The fill color when the indicator exceeds the upper threshold line.</param>
        /// <param name="downColor">The fill color when the indicator falls below the lower threshold line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addUltimateOscillator(int height, int period1, int period2, int period3,
            int color, double range, int upColor, int downColor)
        {
            double[] trueLow = computeTrueLow();
            double[] buyingPressure = new ArrayMath(m_closeData).sub(trueLow).result();
            double[] trueRange = computeTrueRange();

            double[] rawUO1 = new ArrayMath(buyingPressure).movAvg(period1).financeDiv(
                new ArrayMath(trueRange).movAvg(period1).result(), 0.5).mul(4).result();
            double[] rawUO2 = new ArrayMath(buyingPressure).movAvg(period2).financeDiv(
                new ArrayMath(trueRange).movAvg(period2).result(), 0.5).mul(2).result();
            double[] rawUO3 = new ArrayMath(buyingPressure).movAvg(period3).financeDiv(
                new ArrayMath(trueRange).movAvg(period3).result(), 0.5).mul(1).result();

            XYChart c = addIndicator(height);
            string label = "Ultimate Oscillator (" + period1 + ", " + period2 + ", " + period3 + ")"
                ;
            LineLayer layer = addLineIndicator2(c, new ArrayMath(rawUO1).add(rawUO2).add(rawUO3
                ).mul(100.0 / 7).result(), color, label);
            addThreshold(c, layer, 50 + range, upColor, 50 - range, downColor);

            c.yAxis().setLinearScale(0, 100);
            return c;
        }

        /// <summary>
        /// Add a Volume indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="upColor">The color to used on an 'up' day. An 'up' day is a day where
        /// the closing price is higher than that of the previous day.</param>
        /// <param name="downColor">The color to used on a 'down' day. A 'down' day is a day
        /// where the closing price is lower than that of the previous day.</param>
        /// <param name="flatColor">The color to used on a 'flat' day. A 'flat' day is a day
        /// where the closing price is the same as that of the previous day.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addVolIndicator(int height, int upColor, int downColor, int flatColor)
        {
            XYChart c = addIndicator(height);
            addVolBars2(c, height, upColor, downColor, flatColor);
            return c;
        }

        /// <summary>
        /// Add a William %R indicator chart.
        /// </summary>
        /// <param name="height">The height of the indicator chart in pixels.</param>
        /// <param name="period">The period to compute the indicator.</param>
        /// <param name="color">The color of the indicator line.</param>
        /// <param name="range">The distance beween the middle line and the upper and lower threshold lines.</param>
        /// <param name="upColor">The fill color when the indicator exceeds the upper threshold line.</param>
        /// <param name="downColor">The fill color when the indicator falls below the lower threshold line.</param>
        /// <returns>The XYChart object representing the chart created.</returns>
        public XYChart addWilliamR(int height, int period, int color, double range, int upColor,
            int downColor)
        {
            double[] movLow = new ArrayMath(m_lowData).movMin(period).result();
            double[] movHigh = new ArrayMath(m_highData).movMax(period).result();
            double[] movRange = new ArrayMath(movHigh).sub(movLow).result();

            XYChart c = addIndicator(height);
            LineLayer layer = addLineIndicator2(c, new ArrayMath(movHigh).sub(m_closeData
                ).financeDiv(movRange, 0.5).mul(-100).result(), color, "William %R");
            addThreshold(c, layer, -50 + range, upColor, -50 - range, downColor);
            c.yAxis().setLinearScale(-100, 0);
            return c;
        }
        /// <summary>
        /// This method is for backward compatibility. It has no purpose.
        /// </summary>
        public void layoutChart()
        {
            //do nothing
        }
    }
}
