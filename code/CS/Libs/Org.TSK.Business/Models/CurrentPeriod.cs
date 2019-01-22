using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS.Configuration;
using Org.GS;

namespace Org.TSK.Business.Models
{
  public class CurrentPeriod
  {
    private DateTime? _startDateTime;
    public DateTime? StartDateTime {
      get {
        return _startDateTime;
      }
    }

    private DateTime? _endDateTime;
    public DateTime? EndDateTime {
      get {
        return _endDateTime;
      }
    }

    public CurrentPeriod(PeriodContexts periodContext, int offsetMinutes)
    {
      var now = DateTime.Now;

      switch (periodContext)
      {
        case PeriodContexts.Year:
          _startDateTime = now.ToBeginOfYear().AddMinutes(offsetMinutes);
          _endDateTime = now.ToEndOfYear().AddMinutes(offsetMinutes);

          if (now < _startDateTime)
          {
            var d = now;
            while (now < _startDateTime)
            {
              d = d.AddYears(-1);
              _startDateTime = d.ToBeginOfYear().AddMinutes(offsetMinutes);
              _endDateTime = d.ToEndOfYear().AddMinutes(offsetMinutes);
            }
            return;
          }

          if (now > _endDateTime)
          {
            var d = now;
            while (now > _endDateTime)
            {
              d = d.AddYears(1);
              _startDateTime = d.ToBeginOfYear().AddMinutes(offsetMinutes);
              _endDateTime = d.ToEndOfYear().AddMinutes(offsetMinutes);
            }
          }
          break;

        case PeriodContexts.Quarter:
          _startDateTime = now.ToBeginOfQuarter().AddMinutes(offsetMinutes);
          _endDateTime = now.ToEndOfQuarter().AddMinutes(offsetMinutes);

          if (now < _startDateTime)
          {
            var d = now;
            while (now < _startDateTime)
            {
              d = d.AddMonths(-3);
              _startDateTime = d.ToBeginOfQuarter().AddMinutes(offsetMinutes);
              _endDateTime = d.ToEndOfQuarter().AddMinutes(offsetMinutes);
            }
            return;
          }

          if (now > _endDateTime)
          {
            var d = now;
            while (now > _endDateTime)
            {
              d = d.AddMonths(3);
              _startDateTime = d.ToBeginOfQuarter().AddMinutes(offsetMinutes);
              _endDateTime = d.ToEndOfQuarter().AddMinutes(offsetMinutes);
            }
          }
          break;

        case PeriodContexts.Month:
          _startDateTime = now.ToBeginOfMonth().AddMinutes(offsetMinutes);
          _endDateTime = now.ToEndOfMonth().AddMinutes(offsetMinutes);

          if (now < _startDateTime)
          {
            var d = now;
            while (now < _startDateTime)
            {
              d = d.AddMonths(-1);
              _startDateTime = d.ToBeginOfMonth().AddMinutes(offsetMinutes);
              _endDateTime = d.ToEndOfMonth().AddMinutes(offsetMinutes);
            }
            return;
          }

          if (now > _endDateTime)
          {
            var d = now;
            while (now > _endDateTime)
            {
              d = d.AddMonths(1);
              _startDateTime = d.ToBeginOfMonth().AddMinutes(offsetMinutes);
              _endDateTime = d.ToEndOfMonth().AddMinutes(offsetMinutes);
            }
          }
          break;

        case PeriodContexts.Week:
          _startDateTime = now.ToBeginOfWeek().AddMinutes(offsetMinutes);
          _endDateTime = _startDateTime.Value.AddDays(7);

          if (now < _startDateTime)
          {
            var d = now;
            while (now < _startDateTime)
            {
              d = d.AddDays(-7);
              _startDateTime = d.ToBeginOfWeek().AddMinutes(offsetMinutes);
              _endDateTime = _startDateTime.Value.AddDays(7);
            }
            return;
          }

          if (now > _endDateTime)
          {
            var d = now;
            while (now > _endDateTime)
            {
              d = d.AddDays(7);
              _startDateTime = d.ToBeginOfWeek().AddMinutes(offsetMinutes);
              _endDateTime = _startDateTime.Value.AddDays(7);
            }
          }
          break;

        case PeriodContexts.Day:
          _startDateTime = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0).AddMinutes(offsetMinutes);
          _endDateTime = now.ToEndOfDate().AddMinutes(offsetMinutes);

          if (now < _startDateTime)
          {
            var d = now;
            while (now < _startDateTime)
            {
              d = d.AddDays(-1);
              _startDateTime = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0).AddMinutes(offsetMinutes);
              _endDateTime = d.ToEndOfDate().AddMinutes(offsetMinutes);
            }
            return;
          }

          if (now > _endDateTime)
          {
            var d = now;
            while (now > _endDateTime)
            {
              d = d.AddDays(1);
              _startDateTime = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0).AddMinutes(offsetMinutes);
              _endDateTime = d.ToEndOfDate().AddMinutes(offsetMinutes);
            }
          }
          break;

        default:
          throw new Exception("An invalid value was passed to the CurrentPeriod constructor in the 'periodContext' parameter. The value received is '" +
                              periodContext.ToString() + "'.");
      }
    }
  }
}
