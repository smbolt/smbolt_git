using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.TSK.Business.Models
{
  public class TimeIntervalSet : List<TimeInterval>
  {
    public bool Contains(DateTime dt)
    {
      return this.Any(i => i.StartDateTime < dt && i.EndDateTime > dt);
    }
  }
}
