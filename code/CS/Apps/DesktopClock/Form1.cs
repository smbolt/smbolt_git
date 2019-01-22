using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopClock
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void InitializeForm()
    {
      UpdateClock();
      timer1.Interval = 500;
      timer1.Tick += timer1_Tick;
      timer1.Enabled = true;
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      UpdateClock();
    }

    private void UpdateClock()
    {
      lblCalendar.Text = DateTime.Now.ToString("MMM dd, yyyy");
      lblClock.Text = DateTime.Now.ToString("hh:mm:ss tt");
      Application.DoEvents();
    }

    private void lblClock_Click(object sender, EventArgs e)
    {
      if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None)
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      else
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
    }
  }
}
