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
  public partial class NavButton : UserControl
  {
    public NavSection NavSection { get; set; }

    private bool _isSelected = false;
    public bool IsSelected 
    { 
      get { return _isSelected; }
      set 
      {
        _isSelected = value;
        if (_isSelected)
        {
          this.Refresh();
          foreach(NavSection s in this.NavSection.Navigator.NavSections)
          {
            foreach(NavButton b in s.NavButtons)
            {
              if (b.Name != this.Name)
              {
                if (b.IsSelected)
                {
                  b.IsSelected = false;
                  b.Refresh();
                }
              }
            }
          }
        }
      }
    }
    
    private bool _mouseIn = false;

    public NavButton()
    {
      InitializeComponent();
      InitializeControl();
    }

    public void InitializeControl()
    {

    }

    private void NavButton_Paint(object sender, PaintEventArgs e)
    {
      if (_mouseIn)
      {
        e.Graphics.FillRectangle(UIBase.BlueBrush, 0, 0, this.Width, this.Height); 
        if (this.IsSelected)
          e.Graphics.DrawString(this.Text, new Font("Tahoma", 10.0F, FontStyle.Bold), Brushes.White, new Point(20, 4)); 
        else
          e.Graphics.DrawString(this.Text, new Font("Tahoma", 10.0F, FontStyle.Bold), Brushes.White, new Point(20, 4)); 
      }
      else
      {
        if (this.IsSelected)
        {
          e.Graphics.FillRectangle(Brushes.LightGray, 0, 0, this.Width, this.Height); 
          e.Graphics.DrawString(this.Text, new Font("Tahoma", 10.0F, FontStyle.Bold), UIBase.DarkBlueBrush, new Point(20, 4)); 
        }
        else
        {
          e.Graphics.DrawString(this.Text, new Font("Tahoma", 10.0F, FontStyle.Bold), UIBase.BlueBrush, new Point(20, 4));
        }
      }
    }

    private void NavButton_MouseEnter(object sender, EventArgs e)
    {
      _mouseIn = true;
      this.Invalidate();
    }

    private void NavButton_MouseLeave(object sender, EventArgs e)
    {
      _mouseIn = false;
      this.Invalidate();
    }
  }
}
