using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.VideoSeating.Business
{
  [XMap(XType=XType.Element, CollectionElements="SeatSpot", WrapperElement = "SeatSpotSet")]
  public class SeatSpotSet : Dictionary<string, SeatSpot>
  {
    public Rectangle TopArea {
      get;
      set;
    }
    public Rectangle RightArea {
      get;
      set;
    }
    public Rectangle BottomArea {
      get;
      set;
    }
    public Rectangle LeftArea {
      get;
      set;
    }
    public Rectangle Aisle {
      get;
      set;
    }
    public Training Training {
      get;
      set;
    }

    public bool NonSeatAreaContains(Point pt)
    {
      if (this.TopArea != null)
        if (this.TopArea.Contains(pt))
          return true;

      if (this.RightArea != null)
        if (this.RightArea.Contains(pt))
          return true;

      if (this.BottomArea != null)
        if (this.BottomArea.Contains(pt))
          return true;

      if (this.LeftArea != null)
        if (this.LeftArea.Contains(pt))
          return true;

      if (this.Aisle != null)
        if (this.Aisle.Contains(pt))
          return true;

      return false;
    }
  }
}
