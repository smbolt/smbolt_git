using System;
using System.Windows.Forms;
using System.Collections;

namespace CSharpChartExplorer
{
	/// <summary>
	/// A simple form to list out all parameters passed to ClickHotSpot event
	/// handler for demo purposes.
	/// </summary>
	public partial class ParamViewer : Form
	{
		/// <summary>
		/// ParamViewer Constructor
		/// </summary>
		public ParamViewer()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// ParamViewer Constructor
		/// </summary>
		public void Display(object sender, ChartDirector.WinHotSpotEventArgs e)
		{
			// Add the name of the ChartViewer control that is being clicked
			listView.Items.Add(new ListViewItem(new string[] {"source", 
				((ChartDirector.WinChartViewer)sender).Name}));

			// List out the parameters of the hot spot
			foreach (DictionaryEntry key in e.GetAttrValues())
			{
				listView.Items.Add(new ListViewItem(
					new string[] {(string)key.Key, (string)key.Value}));
			}

			// Display the form
			ShowDialog();
		}
		
		/// <summary>
		/// Handler for the OK button
		/// </summary>
		private void OKPB_Click(object sender, System.EventArgs e)
		{
			// Just close the Form
			Close();
		}
	}
}
