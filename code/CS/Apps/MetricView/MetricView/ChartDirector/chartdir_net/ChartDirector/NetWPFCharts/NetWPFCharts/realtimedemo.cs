using System;
using ChartDirector;

namespace CSharpWPFDemo
{
  class realtimedemo : DemoModule
  {
    //Name of demo module
    public string getName() {
      return "Simple Realtime Chart";
    }

    //Number of charts produced in this demo module
    public int getNoOfCharts()
    {
      return 1;
    }

    //Main code for creating chart.
    public void createChart(WPFChartViewer viewer, int chartIndex)
    {
      //This demo uses its own window. The viewer on the right pane is not used.
      viewer.Chart = null;
      new RealTimeDemoWindow().ShowDialog();
    }
  }
}
