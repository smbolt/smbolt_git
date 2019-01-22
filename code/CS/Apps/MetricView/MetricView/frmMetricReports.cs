using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Teleflora.Operations.MetricView
{
    public partial class frmMetricReports : Form
    {
        private MetricDataObjects _dataObjects;
        private bool IsFirstShowing = true;

        public frmMetricReports(MetricDataObjects dataObjects)
        {
            InitializeComponent();
            _dataObjects = dataObjects;   
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMetricReports_Shown(object sender, EventArgs e)
        {
            if (IsFirstShowing)
            {
                txtMain.Text = "Report creation in process.";
                Application.DoEvents();
                RunReport();
            }

            IsFirstShowing = false;
        }

        private void RunReport()
        {
            this.Cursor = Cursors.WaitCursor;
            timer1.Interval = 200;
            timer1.Enabled = true;
            AvailableMetricsReport report = new AvailableMetricsReport();
            string reportText = report.GetReport(_dataObjects);
            timer1.Enabled = false;
            txtMain.Text = reportText;
            txtMain.SelectionStart = 0;
            txtMain.SelectionLength = 0;
            this.Cursor = Cursors.Default;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (txtMain.Text.Length > 37)
                txtMain.Text = "Report creation in process.";
            else
                txtMain.Text += " .";
            Application.DoEvents();
        }
    }
}