using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Org.ChartViewer
{
  public partial class frmDataDisplay : Form
  {
    private string _text;
    private string _csv;
    private bool _showText = true; 

    public frmDataDisplay()
    {
      InitializeComponent();
    }

    private void btnHide_Click(object sender, EventArgs e)
    {
      this.Visible = false;
    }

    public void SetText(string text, string csv)
    {
      _text = text;
      _csv = csv; 
      UpdateDisplay();
    }

    private void ckCsv_CheckedChanged(object sender, EventArgs e)
    {
      _showText = !ckCsv.Checked;
      UpdateDisplay();
    }

    private void UpdateDisplay()
    {
      if (_showText)
        txtOut.Text = _text;
      else
        txtOut.Text = _csv; 
    }
  }
}
