using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class PerformanceInfoSet : List<PerformanceInfo>
  {
    public string GetReport()
    {
      if (this.Count == 0)
        return "No performance data.";

      StringBuilder sb = new StringBuilder();

      sb.Append("Performance Report" + g.crlf2);

      PerformanceInfo piFirst = this.FirstOrDefault();
      PerformanceInfo piLast = this.LastOrDefault();

      DateTime priorDt = DateTime.MinValue;
      foreach (var pi in this)
      {
        string timeStamp = pi.DateTime.ToString("yyyyMMdd-HHmmss.fff");
        if (priorDt == DateTime.MinValue)
          priorDt = pi.DateTime;
        TimeSpan ts = pi.DateTime - priorDt;
        sb.Append(timeStamp + "    " + ts.TotalSeconds.ToString("000") + "." + ts.Milliseconds.ToString("000") +
                  "    " + pi.Label + g.crlf);
        priorDt = pi.DateTime;
      }

      TimeSpan tsAll = piLast.DateTime - piFirst.DateTime;
      sb.Append(g.crlf + "                       " + tsAll.TotalSeconds.ToString("000") + "." + tsAll.Milliseconds.ToString("000") +
                "    Total Elapsed Time" + g.crlf);

      string report = sb.ToString();
      return report;
    }

    public void AddEntry(string entry)
    {
      this.Add(new PerformanceInfo(entry));
    }
  }
}
