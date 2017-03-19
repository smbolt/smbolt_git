using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Org.GS;

namespace Org.GS
{
    public partial class frmDisplayText : Form
    {
        public frmDisplayText(string text)
        {
            InitializeComponent();
            txtOut.Text = text;
        }

        private void Action(object sender, EventArgs e)
        {
            string action = g.GetActionFromEvent(sender);
            
            switch (action)
            {
                case "ShowAssemblies":
                    ShowAssemblies();
                    break;

                case "Exit":
                    this.Close();
                    break;
            }
        }

        private void ShowAssemblies()
        {
            txtOut.Text = "In VS Designer : " + g.IsInVisualStudioDesigner.ToString() + g.crlf2 +
                AssemblyHelper.GetAssembliesInAppDomain() + g.crlf2; 
        }

        public void SetText(string text)
        {
            txtOut.Text = text;
            txtOut.SelectionStart = 0;
            txtOut.SelectionLength = 0;
            txtOut.ScrollToCaret();
        }

        public void AppendText(string text)
        {
            txtOut.Text += text;
            txtOut.SelectionStart = 0;
            txtOut.SelectionLength = 0;
            txtOut.ScrollToCaret();
        }
    }
}
