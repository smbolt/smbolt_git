using System;
using ChartDirector;

namespace CSharpChartExplorer
{
    class tracklabel : DemoModule
    {
        //Name of demo module
        public string getName() { return "Track Line with Data Labels"; }

        //Number of charts produced in this demo module
        public int getNoOfCharts()
        {
            return 1;
        }

        //Main code for creating chart.
        public void createChart(WinChartViewer viewer, int chartIndex)
        {
            //This demo uses its own form. The viewer on the right pane is not used.
            viewer.Image = null;
            System.Windows.Forms.Form f = new FrmTrackLabel();
            f.ShowDialog();
            f.Dispose();
        }
    }
}
