using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.MX.Model
{
  public class MetricObjectSet<T> : SortedList<int, T>
  {
    public string Report { get { return Get_Report(); } }

    private string Get_Report()
    {
      var sb = new StringBuilder();
      sb.Append(typeof(T).ToString().Replace("Org.MX.Model.", String.Empty) + "s" + g.crlf);

      if (this.Count == 0)
      {
        sb.Append("  Collection is empty" + g.crlf2);
        return sb.ToString();
      }

      sb.Append("  ID      Code    Name                            Description" + g.crlf);
      sb.Append("  -----   -----   ------------------------------  --------------------------------------------------" + g.crlf);

      foreach (var item in this.Values)
      {
        var metricObject = item as MetricObject;
        sb.Append("  " + metricObject.Report + g.crlf);
      }

      sb.Append(g.crlf);

      string report = sb.ToString();
      return report;
    }
  }
}
