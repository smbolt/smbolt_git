Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class finance2
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Finance Chart (2)"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' Create a finance chart demo containing 100 days of data
        Dim noOfDays As Integer = 100

        ' To compute moving averages starting from the first day, we need to get extra data points
        ' before the first day
        Dim extraDays As Integer = 30

        ' In this exammple, we use a random number generator utility to simulate the data. We set up
        ' the random table to create 6 cols x (noOfDays + extraDays) rows, using 9 as the seed.
        Dim rantable As RanTable = New RanTable(9, 6, noOfDays + extraDays)

        ' Set the 1st col to be the timeStamp, starting from Sep 4, 2002, with each row representing
        ' one day, and counting week days only (jump over Sat and Sun)
        rantable.setDateCol(0, DateSerial(2002, 9, 4), 86400, True)

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

        ' Create a FinanceChart object of width 640 pixels
        Dim c As FinanceChart = New FinanceChart(640)

        ' Add a title to the chart
        c.addTitle("Finance Chart Demonstration")

        ' Set the data into the finance chart object
        c.setData(timeStamps, highData, lowData, openData, closeData, volData, extraDays)

        ' Add a slow stochastic chart (75 pixels high) with %K = 14 and %D = 3
        c.addSlowStochastic(75, 14, 3, &H006060, &H606000)

        ' Add the main chart with 240 pixels in height
        c.addMainChart(240)

        ' Add a 10 period simple moving average to the main chart, using brown color
        c.addSimpleMovingAvg(10, &H663300)

        ' Add a 20 period simple moving average to the main chart, using purple color
        c.addSimpleMovingAvg(20, &H9900ff)

        ' Add candlestick symbols to the main chart, using green/red for up/down days
        c.addCandleStick(&H00ff00, &Hff0000)

        ' Add 20 days donchian channel to the main chart, using light blue (9999ff) as the border
        ' and semi-transparent blue (c06666ff) as the fill color
        c.addDonchianChannel(20, &H9999ff, &Hc06666ff)

        ' Add a 75 pixels volume bars sub-chart to the bottom of the main chart, using
        ' green/red/grey for up/down/flat days
        c.addVolBars(75, &H99ff99, &Hff9999, &H808080)

        ' Append a MACD(26, 12) indicator chart (75 pixels high) after the main chart, using 9 days
        ' for computing divergence.
        c.addMACD(75, 26, 12, 9, &H0000ff, &Hff00ff, &H008000)

        ' Output the chart
        viewer.Chart = c

    End Sub

End Class

