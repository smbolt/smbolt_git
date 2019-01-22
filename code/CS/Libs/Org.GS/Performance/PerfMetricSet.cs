using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Org.GS.Performance
{
  public class PerfMetricSet : SortedList<DateTime, PerfMetrics>
  {
    public bool IsActive {
      get;
      set;
    }
    public string PerfReport {
      get {
        return Get_PerfReport();
      }
    }

    private string Get_PerfReport()
    {
      if (!IsActive)
        return "PerfMetricSet is not active.";

      if (Monitor.TryEnter(this, 2000))
      {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < this.Count; i++)
        {
          var perfMetricEntry = this.ElementAt(i);
          DateTime dt = perfMetricEntry.Key;
          var perfMetrics = perfMetricEntry.Value;

          // first item
          if (i == 0)
          {
            sb.Append("PERF METRICS SET REPORT" + g.crlf +
                      "-----------------------------------------------------------------------" + g.crlf);
            sb.Append(dt.ToString("yyyy-MM-dd HH:mm:ss.fff") + g.crlf + perfMetrics.PerfReport);
            continue;
          }

          DateTime prevDt = this.ElementAt(i - 1).Key;
          TimeSpan ts = dt - prevDt;

          sb.Append(dt.ToString("yyyy-MM-dd HH:mm:ss.fff") + " - " + g.crlf +
                    perfMetrics.PerfReport + g.crlf +
                    ts.TotalMilliseconds.ToString() + " seconds" + g.crlf);

          // last item
          if (i == this.Count - 1)
          {
            DateTime dtFirst = this.ElementAt(0).Key;
            ts = dt - dtFirst;
            sb.Append("Total Elapsed Time: " + ts.TotalMilliseconds.ToString() + " seconds" + g.crlf);
          }
        }

        string report = sb.ToString();
        Monitor.Exit(this);
        return report;
      }
      else
      {
        return "Unable to produce report due to thread contention.";
      }
    }

    public void AddPerfMetrics(PerfMetrics perfMetrics)
    {
      if (!this.IsActive)
        return;

      if (Monitor.TryEnter(this, 2000))
      {
        DateTime dt = DateTime.Now;
        while (this.ContainsKey(dt))
          dt = DateTime.Now;
        this.Add(dt, perfMetrics);
        Monitor.Exit(this);
      }
    }

    public PerfMetrics GetLatest()
    {
      if (!this.IsActive)
        return null;

      if (this.Count == 0)
        return null;

      return this.Last().Value;
    }
  }
}
