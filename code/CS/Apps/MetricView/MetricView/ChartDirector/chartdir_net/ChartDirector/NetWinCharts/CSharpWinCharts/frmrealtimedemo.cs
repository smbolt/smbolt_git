using System;
using System.Windows.Forms;
using System.Collections;
using ChartDirector;

namespace CSharpChartExplorer
{
    public partial class FrmRealTimeDemo : Form
    {
        // The data arrays that store the visible data. The data arrays are updated in realtime. In
        // this demo, we plot the last 240 samples.
        private const int sampleSize = 240;
        private DateTime[] timeStamps = new DateTime[sampleSize];
        private double[] dataSeriesA = new double[sampleSize];
        private double[] dataSeriesB = new double[sampleSize];
        private double[] dataSeriesC = new double[sampleSize];

        // In this demo, we use a data generator driven by a timer to generate realtime data. The
        // nextDataTime is an internal variable used by the data generator to keep track of which
        // values to generate next.
        private DateTime nextDataTime = DateTime.Now;

        public FrmRealTimeDemo()
        {
            InitializeComponent();
        }

        private void FrmRealTimeDemo_Load(object sender, EventArgs e)
        {
            // Data generation rate
            dataRateTimer.Interval = 250;

            // Chart update rate, which can be different from the data generation rate.
            chartUpdateTimer.Interval = (int)samplePeriod.Value;

            // Initialize data buffer to no data.
            for (int i = 0; i < timeStamps.Length; ++i)
                timeStamps[i] = DateTime.MinValue;

            // Enable RunPB button
            runPB.Checked = true;

            // Now can start the timers for data collection and chart update
            dataRateTimer.Start();
            chartUpdateTimer.Start();
        }

        //
        // The data update routine. In this demo, it is invoked every 250ms to get new data.
        //
        private void dataRateTimer_Tick(object sender, EventArgs e)
        {
            do
            {
                //
                // In this demo, we use some formulas to generate new values. In real applications,
                // it may be replaced by some data acquisition code.
                //
                double p = nextDataTime.Ticks / 10000000.0 * 4;
                double dataA = 20 + Math.Cos(p * 2.2) * 10 + 1 / (Math.Cos(p) * Math.Cos(p) + 0.01);
                double dataB = 150 + 100 * Math.Sin(p / 27.7) * Math.Sin(p / 10.1);
                double dataC = 150 + 100 * Math.Cos(p / 6.7) * Math.Cos(p / 11.9);

                // After obtaining the new values, we need to update the data arrays.
                shiftData(dataSeriesA, dataA);
                shiftData(dataSeriesB, dataB);
                shiftData(dataSeriesC, dataC);
                shiftData(timeStamps, nextDataTime);

                // Update nextDataTime. This is needed by our data generator. In real applications,
                // you may not need this variable or the associated do/while loop.
                nextDataTime = nextDataTime.AddMilliseconds(dataRateTimer.Interval);
            }
            while (nextDataTime < DateTime.Now);

            // We provide some visual feedback to the numbers generated, so you can see the
            // values being generated.
            valueA.Text = dataSeriesA[dataSeriesA.Length - 1].ToString(".##");
            valueB.Text = dataSeriesB[dataSeriesB.Length - 1].ToString(".##");
            valueC.Text = dataSeriesC[dataSeriesC.Length - 1].ToString(".##");
        }

        //
        // Utility to shift a double value into an array
        //
        private void shiftData(double[] data, double newValue)
        {
            for (int i = 1; i < data.Length; ++i)
                data[i - 1] = data[i];
            data[data.Length - 1] = newValue;
        }

        //
        // Utility to shift a DataTime value into an array
        //
        private void shiftData(DateTime[] data, DateTime newValue)
        {
            for (int i = 1; i < data.Length; ++i)
                data[i - 1] = data[i];
            data[data.Length - 1] = newValue;
        }
        
        //
        // Enable/disable chart update based on the state of the Run button.
        //
        private void runPB_CheckedChanged(object sender, EventArgs e)
        {
            chartUpdateTimer.Enabled = runPB.Checked;
        }

        //
        // Updates the chartUpdateTimer interval if the user selects another interval.
        //
        private void samplePeriod_ValueChanged(object sender, EventArgs e)
        {
            chartUpdateTimer.Interval = (int)samplePeriod.Value;
        }

        //
        // The chartUpdateTimer Tick event - this updates the chart periodicially by raising
        // viewPortChanged events.
        //
        private void chartUpdateTimer_Tick(object sender, EventArgs e)
        {
            winChartViewer1.updateViewPort(true, false);
        }

        //
        // The viewPortChanged event handler. In this example, it just updates the chart. If you
        // have other controls to update, you may also put the update code here.
        //
        private void winChartViewer1_ViewPortChanged(object sender, WinViewPortEventArgs e)
        {
            drawChart(winChartViewer1);
        }

        //
        // Draw the chart and display it in the given viewer.
        //
        private void drawChart(WinChartViewer viewer)
        {
            // Create an XYChart object 600 x 270 pixels in size, with light grey (f4f4f4) 
            // background, black (000000) border, 1 pixel raised effect, and with a rounded frame.
            XYChart c = new XYChart(600, 270, 0xf4f4f4, 0x000000, 1);
            c.setRoundedFrame(Chart.CColor(BackColor));

            // Set the plotarea at (55, 62) and of size 520 x 175 pixels. Use white (ffffff) 
            // background. Enable both horizontal and vertical grids by setting their colors to 
            // grey (cccccc). Set clipping mode to clip the data lines to the plot area.
            c.setPlotArea(55, 62, 520, 175, 0xffffff, -1, -1, 0xcccccc, 0xcccccc);
            c.setClipping();

            // Add a title to the chart using 15 pts Times New Roman Bold Italic font, with a light
            // grey (dddddd) background, black (000000) border, and a glass like raised effect.
            c.addTitle("Field Intensity at Observation Satellite", "Times New Roman Bold Italic", 15
                ).setBackground(0xdddddd, 0x000000, Chart.glassEffect());

            // Add a legend box at the top of the plot area with 9pts Arial Bold font. We set the 
            // legend box to the same width as the plot area and use grid layout (as opposed to 
            // flow or top/down layout). This distributes the 3 legend icons evenly on top of the 
            // plot area.
            LegendBox b = c.addLegend2(55, 33, 3, "Arial Bold", 9);
            b.setBackground(Chart.Transparent, Chart.Transparent);
            b.setWidth(520);

            // Configure the y-axis with a 10pts Arial Bold axis title
            c.yAxis().setTitle("Intensity (V/m)", "Arial Bold", 10);

            // Configure the x-axis to auto-scale with at least 75 pixels between major tick and 15 
            // pixels between minor ticks. This shows more minor grid lines on the chart.
            c.xAxis().setTickDensity(75, 15);

            // Set the axes width to 2 pixels
            c.xAxis().setWidth(2);
            c.yAxis().setWidth(2);

            // Now we add the data to the chart
            DateTime lastTime = timeStamps[timeStamps.Length - 1];
            if (lastTime != DateTime.MinValue)
            {
                // Set up the x-axis scale. In this demo, we set the x-axis to show the last 240 
                // samples, with 250ms per sample.
                c.xAxis().setDateScale(lastTime.AddSeconds(
                    -dataRateTimer.Interval * timeStamps.Length / 1000), lastTime);

                // Set the x-axis label format
                c.xAxis().setLabelFormat("{value|hh:nn:ss}");

                // Create a line layer to plot the lines
                LineLayer layer = c.addLineLayer2();

                // The x-coordinates are the timeStamps.
                layer.setXData(timeStamps);

                // The 3 data series are used to draw 3 lines. Here we put the latest data values
                // as part of the data set name, so you can see them updated in the legend box.
                layer.addDataSet(dataSeriesA, 0xff0000, "Alpha: <*bgColor=FFCCCC*>" +
                    c.formatValue(dataSeriesA[dataSeriesA.Length - 1], " {value|2} "));
                layer.addDataSet(dataSeriesB, 0x00cc00, "Beta: <*bgColor=CCFFCC*>" +
                    c.formatValue(dataSeriesB[dataSeriesB.Length - 1], " {value|2} "));
                layer.addDataSet(dataSeriesC, 0x0000ff, "Gamma: <*bgColor=CCCCFF*>" +
                    c.formatValue(dataSeriesC[dataSeriesC.Length - 1], " {value|2} "));

            }

            // Assign the chart to the WinChartViewer
            viewer.Chart = c;
        }
    }
}