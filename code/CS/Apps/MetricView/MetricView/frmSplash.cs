using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Teleflora.Operations.MetricView
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();

            this.Cursor = Cursors.AppStarting;
        }

        public void SetMessage(string message)
        {
            this.lblMessage.Text = message;
        }

        public void SetBuildString(string buildString)
        {
            this.lblBuild.Text = buildString;
        }

        private void frmSplash_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }


    }
}