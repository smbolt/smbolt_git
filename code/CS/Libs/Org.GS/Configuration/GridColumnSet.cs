using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.Configuration
{
  [XMap(XType = XType.Element, CollectionElements = "GridColumn")]
  public class GridColumnSet : Dictionary<string, GridColumn>
  {
    public void SetColumnWidths(int totalPixels)
    {
      int totalWidths = 0;
      int remainingPixels = totalPixels;

      // get the total configured widths
      foreach (GridColumn col in this.Values)
        totalWidths += col.Width;

      // allocate pixels to the columns proportionate to the configured widths
      foreach (GridColumn col in this.Values)
      {
        float pct = (float)col.Width / totalWidths;
        col.WidthPixels = (int)(pct * totalPixels);
        if (col.WidthPixels > remainingPixels)
          col.WidthPixels = remainingPixels;
        remainingPixels -= col.WidthPixels;
      }
    }
  }
}
