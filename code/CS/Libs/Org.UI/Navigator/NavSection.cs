using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Org.UI
{
  public partial class NavSection : UserControl
  {
    public UIPanel Navigator { get; set; }
    public List<NavButton> NavButtons;

    private int _configuredHeight; 

    private bool _isCollapsed;
    public bool IsCollapsed
    {
      get { return _isCollapsed; }
      set 
      {
        _isCollapsed = value;
        if (_isCollapsed)
        {
          this.Height = 36;
          this.Padding = new Padding(3, 37, 3, 3);
        }
        else
        {
          this.Height = 36 + this.NavButtons.Count * 24;
          this.Padding = new Padding(3, 28, 3, 3);
        }
      }
    }

    public NavSection()
    {
      InitializeComponent();
      InitializeControl();
    }

    private void InitializeControl()
    {
      this.NavButtons = new List<NavButton>();

      this.Text = "NavSection";
      this.Height = 120;
      _configuredHeight = 120; 
      this.IsCollapsed = true; 
    }

    private void NavSection_Paint(object sender, PaintEventArgs e)
    {
      e.Graphics.DrawString(this.Text, new Font("Tahoma", 10.0F, FontStyle.Bold), UIBase.BlueBrush, new Point(6, 8)); 
    }

    private void NavSection_Click(object sender, EventArgs e)
    {
      this.IsCollapsed = !this.IsCollapsed; 
    }
  }
}
