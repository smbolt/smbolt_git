using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.TSK.Business.Models
{
  public class RunDay
  {
    private DateTime _runDate;
    private Dictionary<int, int> _dateDays;

    public int DayOfWeekInt { get { return (int) _runDate.DayOfWeek; } }
    public DayOfWeek DayOfWeek { get { return _runDate.DayOfWeek; } }
    public int WeekOrdinal { get; private set; }
    public bool IsWorkDay { get { return Get_IsWorkDay(); } }
    public bool IsEven { get; private set;  }
    public bool IsOdd { get; private set; }
    public bool IsLast { get; private set; }

    public RunDay(DateTime runDate)
    {
      _runDate = runDate;
      Initialize();
    }

    private bool Get_IsWorkDay()
    {
      int dow = (int)_runDate.DayOfWeek;
      if (dow == 0 || dow == 6)
        return false;

      return true;
    }

    public void Initialize()
    {
      // Populate table of days and day of week intergers for the month
      _dateDays = new Dictionary<int, int>();

      int day = _runDate.Day;
      int dow = (int) _runDate.DayOfWeek;

      while (day != 1)
      {
        day--;
        if (dow == 0)
          dow = 6;
        else
          dow--;
      }

      int daysInOMonth = _runDate.DaysInMonth();

      for (int i = 1; i < daysInOMonth + 1; i++)
      {
        _dateDays.Add(i, dow);
        dow++;
        if (dow > 6)
          dow = 0;
      }

      // Populate IsEven and IsOdd properties
      this.IsEven = _runDate.Day % 2 == 0;
      this.IsOdd = _runDate.Day % 2 != 0;

      // Populate WeekOrdinal and IsLast properties
      day = _runDate.Day;
      dow = (int)_runDate.DayOfWeek;

      int ordinal = 0;
      bool isLast = true;

      foreach (var d in _dateDays)
      {
        if (d.Key <= day)
        {
          if (d.Value == dow)
            ordinal++;
        }
        else
        {
          if (d.Value == dow)
            isLast = false;
        }
      }

      this.WeekOrdinal = ordinal;
      this.IsLast = isLast;
    }
  }
}
