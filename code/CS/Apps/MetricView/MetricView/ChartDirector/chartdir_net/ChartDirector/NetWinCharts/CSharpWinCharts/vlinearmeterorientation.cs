using System;
using ChartDirector;

namespace CSharpChartExplorer
{
    public class vlinearmeterorientation : DemoModule
    {
        //Name of demo module
        public string getName() { return "V-Linear Meter Orientation"; }

        //Number of charts produced in this demo module
        public int getNoOfCharts() { return 2; }

        //Main code for creating charts
        public void createChart(WinChartViewer viewer, int chartIndex)
        {
            // The value to display on the meter
            double value = 75.35;

            // Create a LinearMeter object of size 70 x 240 pixels with very light grey (0xeeeeee)
            // backgruond and a light grey (0xccccccc) 3-pixel thick rounded frame
            LinearMeter m = new LinearMeter(70, 240, 0xeeeeee, 0xcccccc);
            m.setRoundedFrame(Chart.Transparent);
            m.setThickFrame(3);

            // This example demonstrates putting the text labels at the left or right side by
            // setting the label alignment and scale position.
            if (chartIndex == 0) {
                m.setMeter(28, 18, 20, 205, Chart.Left);
            } else {
                m.setMeter(20, 18, 20, 205, Chart.Right);
            }

            // Set meter scale from 0 - 100, with a tick every 10 units
            m.setScale(0, 100, 10);

            // Add a smooth color scale to the meter
            double[] smoothColorScale = {0, 0x6666ff, 25, 0x00bbbb, 50, 0x00ff00, 75, 0xffff00, 100,
                0xff0000};
            m.addColorScale(smoothColorScale);

            // Add a blue (0x0000cc) pointer at the specified value
            m.addPointer(value, 0x0000cc);

            // Output the chart
            viewer.Chart = m;
        }
    }
}

