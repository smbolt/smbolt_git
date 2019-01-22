using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class PerfEntry
  {
    public string Name { get; set; }
    public string Desc { get; set; }
    public DateTime StartDateTime { get; private set; }
    public DateTime EndDateTime { get; private set; }
    public TimeSpan Elapsed { get { return Get_Elapsed(); } }
    public string ElapsedFmt { get { return Get_ElapsedFmt(); } }
    public PerfEntryStatus PerfEntryStatus { get; private set; }
    public bool IsComplete { get { return this.PerfEntryStatus == PerfEntryStatus.Complete; } }

    public PerfEntry(string name, string desc = "")
    {
      this.Name = name;
      this.Desc = desc;
      this.StartDateTime = DateTime.Now;
      this.EndDateTime = DateTime.MinValue;
      this.PerfEntryStatus = PerfEntryStatus.InProgress;
    }

    public void End()
    {
      this.EndDateTime = DateTime.Now;
      this.PerfEntryStatus = PerfEntryStatus.Complete;
    }

    private TimeSpan Get_Elapsed()
    {
      if (!this.IsComplete)
        return new TimeSpan(0);

      return this.EndDateTime - this.StartDateTime;
    }

    private string Get_ElapsedFmt()
    {
      if (!this.IsComplete)
        return "IN-PROGRESS ";

      var elaspsed = Get_Elapsed();

      return elaspsed.ToString(@"hh\:mm\:ss\.fff");
    }
  }
}
