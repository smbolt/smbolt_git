using System;

namespace CSharpWPFDemo
{
	/// <summary>
	/// Represents the function each demo chart module must provide
	/// </summary>
	public interface DemoModule
	{
		/// <summary>
		/// A human readable name for the module
		/// </summary>
		string getName();

		/// <summary>
		/// The number of demo charts generated by this module
		/// </summary>
		int getNoOfCharts();

		/// <summary>
		/// Generate a demo chart and display it in the given WinChartViewer. The chartIndex 
		/// argument indicate which demo chart to generate. It must be a number from 0 to (n - 1).
		/// </summary>
		void createChart(ChartDirector.WPFChartViewer viewer, int chartIndex);
	}
}
