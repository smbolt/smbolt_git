using System;
using System.Collections;
using System.Windows;
using ChartDirector;


namespace CSharpWPFDemo
{
    public partial class ParamViewer : Window
    {
        public ParamViewer()
        {
            InitializeComponent();
        }

        public void Display(object sender, WPFHotSpotEventArgs e)
        {
            // Add the name of the ChartViewer control that is being clicked
            dataGrid.Items.Add(new DictionaryEntry("source", (sender as WPFChartViewer).Name));

            // List out the parameters of the hot spot
            foreach (var key in e.AttrValues)
                dataGrid.Items.Add(key);

            // Display the form
            ShowDialog();
        }
    }
}
