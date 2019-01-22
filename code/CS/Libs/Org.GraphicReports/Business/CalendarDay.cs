using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GraphicReports.Business
{
  public class CalendarDay
  {
    private Calendar _calendar;
    private CalendarMonth _calendarMonth;
    private DateTime _day;
    public int DayOfMonth { get { return _day.Day; } }
    public int Left { get; set; }
    public int PixelsToDraw { get; set; }
    public bool DrawThisDay { get { return this.PixelsToDraw > 0; } }

    public CalendarDay(CalendarMonth calendarMonth, DateTime dt)
    {
      _calendarMonth = calendarMonth;
      _calendar = calendarMonth.Calendar;
      _day = new DateTime(dt.Year, dt.Month, dt.Day);
      this.Left = -1;
      this.PixelsToDraw = -1; 
    }
  }
}
