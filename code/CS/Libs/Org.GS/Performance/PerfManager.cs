using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class PerfManager
  {
    public SortedList<string, PerfEntry> PerfEntries { get; private set; }
    public string Report { get { return Get_Report(); } }

    public PerfManager()
    {
      this.PerfEntries = new SortedList<string, PerfEntry>();
    }

    public void Clear()
    {
      this.PerfEntries = new SortedList<string, PerfEntry>();
    }

    public string Start(string name, string desc)
    {
      string keyPrefix = this.PerfEntries.Count.ToString("0000") + "_";
      string entryName = keyPrefix + name;

      int seq = 0;
      while (this.PerfEntries.ContainsKey(entryName))
      {
        seq++;
        entryName = keyPrefix + entryName + "_" + seq.ToString("000");
      }

      var perfEntry = new PerfEntry(entryName, desc);
      this.PerfEntries.Add(perfEntry.Name, perfEntry);

      return entryName;
    }

    public string End(string name)
    {
      if (this.PerfEntries.ContainsKey(name))
      {
        var perfEntry = this.PerfEntries[name];
        perfEntry.End();
      }

      return Get_Report();
    }

    private string Get_Report()
    {
      var sb = new StringBuilder();

      sb.Append("PERFORMANCE REPORT" + g.crlf2);

      sb.Append("ITEM    ELAPSED       STARTED        ENDED        NAME (DESCRIPTION)" + g.crlf);
      //         0001  00:00:01.123  00:00:00.000  00:00:00.000    LoadDxWorkbookFromFile (this is the description)

      foreach (var perf in this.PerfEntries.Values)
      {
        string fullName = perf.Name;
        string itemNbr = fullName.Substring(0, 4);
        string name = fullName.Substring(5);
        string elapsed = perf.ElapsedFmt;
        string started = perf.StartDateTime.ToString(@"hh\:mm\:ss\.fff");
        string ended = perf.IsComplete ? perf.EndDateTime.ToString(@"hh\:mm\:ss\.fff") : new string(' ', 12);

        sb.Append(itemNbr + "  " + elapsed + "  " + started + "  " + ended + "    " + name +
                  (perf.Desc.IsNotBlank() ? " (" + perf.Desc + ")" : String.Empty) + g.crlf);
      }

      string report = sb.ToString();
      return report;
    }
  }
}
