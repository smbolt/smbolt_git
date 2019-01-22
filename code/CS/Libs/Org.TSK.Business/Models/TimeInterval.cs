using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.TSK.Business.Models
{
  public class TimeInterval
  {
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }

    public TimeInterval(DateTime startDateTime, DateTime endDateTime)
    {
      this.StartDateTime = startDateTime;
      this.EndDateTime = endDateTime;
    }

    public bool Contains(DateTime dt)
    {
      return (this.StartDateTime < dt && this.EndDateTime > dt);
    }
  }
}
