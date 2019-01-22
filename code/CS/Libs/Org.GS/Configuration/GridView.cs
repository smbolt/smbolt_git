using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.Configuration
{
  public enum ColumnWidthMode
  {
    Relative,
    Fixed
  }

  [XMap(XType = XType.Element, CollectionElements="GridColumn")]
  public class GridView : List<GridColumn>
  {
    [XMap(IsKey = true)]
    public string ViewName { get; set; }

    [XMap(DefaultValue="False")]
    public bool IsDefault { get; set; }

    [XMap(DefaultValue="Relative")]
    public ColumnWidthMode ColumnWidthMode { get; set; }

    public GridView()
    {
      this.ViewName = String.Empty;
      this.IsDefault = false;
      this.ColumnWidthMode = ColumnWidthMode.Relative;
    }   

    public void AutoInit()
    {
      if (this.ColumnWidthMode == ColumnWidthMode.Relative)
      {
        int columnCount = this.Count();
        float remainingPct = 1.0F; 
        int totalWidth = this.Sum(x => x.Width); 
        foreach(var col in this)
        {
          float pct = (float)col.Width / totalWidth;
          if (pct > remainingPct)
            remainingPct = pct;
          col.WidthPct = pct;
          remainingPct -= pct;
        }
      }
      else
      {
        foreach(var col in this)
          col.WidthPixels = col.Width;
      }
    }

    public void SetColumnWidths(int totalPixels)
    {
      int totalWidths = 0;
      int remainingPixels = totalPixels;

      // get the total configured widths
      foreach (GridColumn col in this)
        totalWidths += col.Width;

      // allocate pixels to the columns proportionate to the configured widths
      foreach (GridColumn col in this)
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
