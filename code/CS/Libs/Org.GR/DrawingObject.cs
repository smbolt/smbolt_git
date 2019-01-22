using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GR
{
  public class DrawingObject
  {
    public Point Location {
      get;
      set;
    }
    public Size Size {
      get;
      set;
    }

    public DrawingObjectSet DrawingObjectSet {
      get;
      set;
    }

    public DrawingObject()
    {
    }

    public virtual void DrawObject(Graphics gr, float scale, PointF origin)
    {
    }

  }
}
