using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.GS.Configuration;
using Org.GS;

namespace Org.FTP
{
  public enum FtpStatus
  {
    NotSet,
    Initializing,
    Progressing,
    Suspended,
    Terminated,
    Completed,
    Error
  }    

  public class FtpProgress
  {
    public FtpStatus FtpStatus { get; set; }
    public long TotalBytes { get; set; }
    public long BytesTransferred { get; set; }
    public long BytesRemaining { get; set; }
    public decimal PercentageComplete { get; set; }
    public DateTime StartDT { get; set; }
    public DateTime FinishDT { get; set; }
    public TimeSpan Duration { get; set; }
    public ConfigFtpSpec ConfigFtpSpec { get; set; }
        

    public FtpProgress()
    {
      FtpStatus = FtpStatus.NotSet;
      TotalBytes = 0;
      BytesTransferred = 0;
      BytesRemaining = 0;
      PercentageComplete = 0.0m;
      StartDT = DateTime.MinValue;
      FinishDT = DateTime.MinValue;
      Duration = TimeSpan.MinValue;
      ConfigFtpSpec = null; 
    }

    public int GetCompletionPercentage()
    {
      try
      {
        if (this.TotalBytes == 0)
          return 0;

        float pct = (float)this.BytesTransferred / this.TotalBytes * 100;

        return (int)pct;
      }
      catch { return 0; }
    }

    public string GetCompletionPercentageString()
    {
      return GetCompletionPercentage().ToString() + " %";
    }
  }
}
