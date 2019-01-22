using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class ProcessTimer
  {
    public DateTime? StartDateTime {
      get;
      private set;
    }
    public DateTime? EndDateTime {
      get;
      private set;
    }
    public decimal? Seconds {
      get {
        return Get_Seconds();
      }
    }

    public ProcessTimer()
    {
      Start();
    }

    public void Start()
    {
      this.StartDateTime = DateTime.Now;
    }

    public decimal? End()
    {
      return Get_Seconds();
    }

    public decimal? SecondsSoFar()
    {
      if (!this.StartDateTime.HasValue)
        return null;

      return Convert.ToDecimal((DateTime.Now - this.StartDateTime.Value).TotalMilliseconds / 1000);
    }

    private decimal? Get_Seconds()
    {
      if (!this.StartDateTime.HasValue || !this.EndDateTime.HasValue)
        return (decimal?) null;

      return Convert.ToDecimal((this.EndDateTime.Value - this.StartDateTime.Value).TotalMilliseconds / 1000);
    }
  }
}
