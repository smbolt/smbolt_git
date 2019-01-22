using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  public class Page
  {
    public int PageNumber {
      get;
      set;
    }
    public float Scale {
      get;
      set;
    }
    public Size Size {
      get;
      set;
    }

    public PageLayout PageLayout {
      get;
      set;
    }
    public Image Image {
      get;
      set;
    }


    public Page(Size pageSize, float scale)
    {
      this.PageNumber = 0;
      this.Scale = scale;
      this.Size = pageSize;
      this.PageLayout = new PageLayout();
      int width = Convert.ToInt32((float)this.Size.Width * scale);
      int height = Convert.ToInt32((float)this.Size.Height * scale);

      this.Image = new Bitmap(width, height);
    }

    public void DrawLayout(Graphics g)
    {
      Pen p = new Pen(Brushes.LightGray, 0.5F);

      g.DrawRectangle(p, this.PageLayout.Header);
      g.DrawRectangle(p, this.PageLayout.Footer);
      g.DrawRectangle(p, this.PageLayout.Main);

      p.Dispose();
    }
  }
}
