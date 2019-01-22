using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public enum DateSpanType
  {
    DateRange,
    DateTimeRange
  }

  public class DateSpan
  {
    public DateTime StartDateTime {
      get;
      private set;
    }
    public DateTime EndDateTime {
      get;
      private set;
    }
    public DateSpanType DateSpanType {
      get;
      private set;
    }

    public DateSpan(DateTime startDate, DateTime endDate)
    {
      this.StartDateTime = startDate;
      this.EndDateTime = endDate;
      this.DateSpanType = DateSpanType.DateRange;
    }

    public DateSpan(DateTime startDate, DateTime endDate, DateSpanType dateSpanType)
    {
      this.StartDateTime = startDate;
      this.EndDateTime = endDate;
      this.DateSpanType = dateSpanType;
    }

    public bool IsDateTimeInRange(DateTime dateTime)
    {
      if (dateTime > this.EndDateTime)
        return false;

      if (dateTime < this.StartDateTime)
        return false;

      return true;
    }

    public bool SpansIntersect(DateSpan dateSpan)
    {
      if (dateSpan.StartDateTime <= this.EndDateTime &&
          dateSpan.EndDateTime >= this.StartDateTime)
        return true;

      return false;
    }
  }
}
