using System;
using ChartDirector;

namespace CSharpChartExplorer
{
    public class colorhbarmeter : DemoModule
    {
        //Name of demo module
        public string getName() { return "Color Horizontal Bar Meters"; }

        //Number of charts produced in this demo module
        public int getNoOfCharts() { return 6; }

        //Main code for creating charts
        public void createChart(WinChartViewer viewer, int chartIndex)
        {
            // The value to display on the meter
            double value = 75.35;

            // The background, border and bar colors of the meters
            int[] bgColor = {0xbbddff, 0xccffcc, 0xffddff, 0xffffaa, 0xffdddd, 0xeeeeee};
            int[] borderColor = {0x000088, 0x006600, 0x880088, 0xee6600, 0x880000, 0x666666};
            int[] barColor = {0x0088ff, 0x00cc00, 0x8833dd, 0xff8800, 0xee3333, 0x888888};

            // Create a LinearMeter object of size 260 x 80 pixels with a 3-pixel thick rounded
            // frame
            LinearMeter m = new LinearMeter(260, 80, bgColor[chartIndex], borderColor[chartIndex]);
            m.setRoundedFrame(Chart.Transparent);
            m.setThickFrame(3);

            // Set the scale region top-left corner at (18, 24), with size of 222 x 20 pixels. The
            // scale labels are located on the top (implies horizontal meter)
            m.setMeter(18, 24, 222, 20, Chart.Top);

            // Set meter scale from 0 - 100, with a tick every 10 units
            m.setScale(0, 100, 10);

            if (chartIndex % 4 == 0) {
                // Add a 5-pixel thick smooth color scale at y = 48 (below the meter scale)
                double[] smoothColorScale = {0, 0x0000ff, 25, 0x0088ff, 50, 0x00ff00, 75, 0xdddd00,
                    100, 0xff0000};
                m.addColorScale(smoothColorScale, 48, 5);
            } else if (chartIndex % 4 == 1) {
                // Add a 5-pixel thick step color scale at y = 48 (below the meter scale)
                double[] stepColorScale = {0, 0x00cc00, 50, 0xffdd00, 80, 0xff3333, 100};
                m.addColorScale(stepColorScale, 48, 5);
            } else if (chartIndex % 4 == 2) {
                // Add a 5-pixel thick high/low color scale at y = 48 (below the meter scale)
                double[] highLowColorScale = {0, 0x0000ff, 40, Chart.Transparent, 60,
                    Chart.Transparent, 100, 0xff0000};
                m.addColorScale(highLowColorScale, 48, 5);
            } else {
                // Add a 5-pixel thick high only color scale at y = 48 (below the meter scale)
                double[] highColorScale = {70, Chart.Transparent, 100, 0xff0000};
                m.addColorScale(highColorScale, 48, 0, 48, 8);
            }

            // Add a bar from 0 to value with glass effect and 4 pixel rounded corners
            m.addBar(0, value, barColor[chartIndex], Chart.glassEffect(Chart.NormalGlare, Chart.Top
                ), 4);

            // Add a label right aligned to (243, 65) using 8pt Arial Bold font
            m.addText(243, 65, "Temperature C", "Arial Bold", 8, Chart.TextColor, Chart.Right);

            // Add a text box left aligned to (18, 65). Display the value using white (0xffffff) 8pt
            // Arial Bold font on a black (0x000000) background with depressed rounded border.
            ChartDirector.TextBox t = m.addText(18, 65, m.formatValue(value, "2"), "Arial", 8,
                0xffffff, Chart.Left);
            t.setBackground(0x000000, 0x000000, -1);
            t.setRoundedCorners(3);

            // Output the chart
            viewer.Chart = m;
        }
    }
}

