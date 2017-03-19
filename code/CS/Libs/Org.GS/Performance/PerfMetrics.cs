using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Org.GS.Performance
{
  public class PerfMetrics
  {
    private SortedDictionary<DateTime, string> PerfEvents;

    public string PerfReport { get { return Get_PerfReport(); } }

    public PerfMetrics()
    {
      this.PerfEvents = new SortedDictionary<DateTime, string>();
    }

    public void RecordEvent(string eventDescription)
    {
      if (Monitor.TryEnter(this, 2000))
      {
        var d = DateTime.Now;

        while (this.PerfEvents.ContainsKey(d))
          d = DateTime.Now;

        this.PerfEvents.Add(d, eventDescription);

        Monitor.Exit(this);
      }
    }

    private string Get_PerfReport()
    {
      if (Monitor.TryEnter(this, 2000))
      {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < this.PerfEvents.Count; i++)
        {
          var perfEvent = this.PerfEvents.ElementAt(i);
          DateTime dt = perfEvent.Key;
          string desc = perfEvent.Value;

          // first item
          if (i == 0)
          {
            sb.Append("  PerfMetrics Report" + g.crlf +
                      "  -----------------------------------------------------------------------" + g.crlf);
            sb.Append("  " + dt.ToString("yyyy-MM-dd HH:mm:ss.fff") + " - " + desc.PadTo(25) + g.crlf);
            continue;
          }

          DateTime prevDt = this.PerfEvents.ElementAt(i - 1).Key;
          TimeSpan ts = dt - prevDt;

          decimal seconds = (decimal) ts.TotalMilliseconds / 1000; 

          sb.Append("  " + dt.ToString("yyyy-MM-dd HH:mm:ss.fff") + " - " + desc.PadTo(25) + "   " + 
                    seconds.ToString("0000.000") + " seconds" + g.crlf);

          // last item
          if (i == this.PerfEvents.Count - 1)
          {
            DateTime dtFirst = this.PerfEvents.ElementAt(0).Key;
            ts = dt - dtFirst;
            decimal totalSeconds = (decimal) ts.TotalMilliseconds / 1000;
            sb.Append("  Total Elapsed Time -------------------------------    " + totalSeconds.ToString("0000.000") + " seconds" + g.crlf);
          }
        }

        string report = sb.ToString();
        Monitor.Exit(this);
        return report;
      }
      else
      {
        return "Report could not be created due to thread contention.";
      }
    }
  }
}
