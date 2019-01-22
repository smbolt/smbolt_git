using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GraphicReports.Business
{
  public class CalendarMonth : SortedList<int, CalendarDay>
  {
    public Calendar Calendar;
    private DateTime _firstOfMonth {
      get;
      set;
    }
    public DateTime FirstDayOfMonth {
      get {
        return _firstOfMonth;
      }
    }
    public DateTime LastDayOfMonth {
      get {
        return new DateTime(this.FirstDayOfMonth.Year, this.FirstDayOfMonth.Month, this.FirstDayOfMonth.LastDayOfMonth());
      }
    }
    public string MonthText {
      get {
        return _firstOfMonth.ToString("MMMM");
      }
    }
    public string YearText {
      get {
        return _firstOfMonth.ToString("yyyy");
      }
    }
    public int CCYYMM {
      get {
        return _firstOfMonth.ToString("yyyyMM").ToInt32();
      }
    }
    public string MonthYearLong {
      get {
        return this.MonthText + " " + this.YearText;
      }
    }
    public string MonthYearMid {
      get {
        return _firstOfMonth.ToString("MMM") + " " + this.YearText;
      }
    }
    public string MonthYearShort {
      get {
        return _firstOfMonth.ToString("MM/yy");
      }
    }
    public int DrawingLeft {
      get {
        return Get_DrawingLeft();
      }
    }
    public int DrawingWidth {
      get {
        return Get_DrawingWidth();
      }
    }

    public CalendarMonth(Calendar calendar, DateTime dt)
    {
      this.Calendar = calendar;
      _firstOfMonth = new DateTime(dt.Year, dt.Month, 1);

      var d = _firstOfMonth;
      var m = d.Month;
      while (d.Month == m)
      {
        var calendarDay = new CalendarDay(this, d);
        this.Add(calendarDay.DayOfMonth, calendarDay);
        d = d.AddDays(1);
      }
    }

    private int Get_DrawingWidth()
    {
      int drawingWidth = 0;
      foreach (var day in this.Values)
      {
        if (day.DrawThisDay)
          drawingWidth += day.PixelsToDraw;
        else
          break;
      }
      return drawingWidth;
    }

    private int Get_DrawingLeft()
    {
      if (this.Values.Count == 0)
        return -1;

      if (!this.Values.First().DrawThisDay)
        return -1;

      return this.Values.First().Left;
    }

    public string GetMonthTextFit(Graphics gx, Font font)
    {
      int pixels = Get_DrawingWidth();

      if (gx.MeasureString(this.MonthYearLong, font).Width < pixels)
        return this.MonthYearLong;

      if (gx.MeasureString(this.MonthYearMid, font).Width < pixels)
        return this.MonthYearMid;

      if (gx.MeasureString(this.MonthYearShort, font).Width < pixels)
        return this.MonthYearShort;

      return String.Empty;
    }
  }
}
