using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.TextUtility
{
  public class WellSet : List<Well>
  {
    public string WellNameReport { get { return Get_WellNameReport(); } }

    private string Get_WellNameReport()
    {
      var sb = new StringBuilder();

      foreach (var well in this)
      {
        sb.Append(well.WellName.Trim() + g.crlf);
      }

      return sb.ToString().Trim();
    }
  }
}
