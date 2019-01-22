using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Terminal.BMS
{
  public class BmsLineSet : SortedList<int, BmsLine>
  {
    public override string ToString()
    {
      if (this.Count == 0)
        return "NO LINES";

      StringBuilder sb = new StringBuilder();

      foreach (var bmsLine in this.Values)
      {
        sb.Append(bmsLine.LineText.TrimEnd() + " ");
      }

      return sb.ToString();
    }
  }
}
