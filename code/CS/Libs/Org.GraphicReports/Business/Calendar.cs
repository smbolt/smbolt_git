using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Resources;
using Org.GS;

namespace Org.GraphicReports.Business
{
  public class Calendar : SortedList<int, CalendarMonth>
  {
    private Image _logo;

    public DateTime StartDate {
      get;
      set;
    }
    public DateTime EndDate {
      get;
      set;
    }
    public DateTime FirstDateInView {
      get;
      set;
    }
    public DateTime LastDateInView {
      get;
      set;
    }

    public Font TitleFont {
      get;
      set;
    }
    public Font MonthFont {
      get;
      set;
    }
    public Font DayFont {
      get;
      set;
    }

    private int _titleHeight = 25;
    private int _monthBarHeight = 31;
    private int _dayHeight = 17;
    private int _dayWidth = 17;
    public int DayWidth {
      get {
        return _dayWidth;
      }
    }
    private int _rigColWidth = 90;

    public Calendar(DateSpan dateSpan)
    {
      this.StartDate = dateSpan.StartDateTime;
      this.EndDate = dateSpan.EndDateTime;
      this.FirstDateInView = this.StartDate;

      var dt = this.StartDate;
      while (dt <= dateSpan.EndDateTime)
      {
        var calendarMonth = new CalendarMonth(this, dt);
        this.Add(calendarMonth.CCYYMM, calendarMonth);
        dt = dt.AddMonths(1);
      }

      this.TitleFont = new Font("Calibri", 14.0F, FontStyle.Bold);
      this.MonthFont = new Font("Calibri", 10.5F);
      this.DayFont = new Font("Calibri", 9.0F);

      ResourceManager resourceManager = new ResourceManager("Org.GraphicReports.Resource1", Assembly.GetExecutingAssembly());
      _logo = (Bitmap)resourceManager.GetObject("logo");
    }

    public int Draw(Graphics gx, Point origin, Size sz)
    {
      int x = origin.X;
      int y = origin.Y;

      this.LastDateInView = ComputeDaysToDraw(this.FirstDateInView, x + _rigColWidth, sz.Width - _rigColWidth);

      var blueBrush = new SolidBrush(Color.FromArgb(100, 149, 237));

      gx.DrawImage(_logo, new Rectangle(x, y, 120, 56));

      y += 60;

      // what if the string is too long for the width?
      gx.DrawString("Rig Schedule: " + this.StartDate.ToString("MM/dd/yyyy") + " - " + this.EndDate.ToString("MM/dd/yyyy"),
                    this.TitleFont, Brushes.Green, new PointF(x, y));
      y += _titleHeight;

      gx.FillRectangle(blueBrush, new Rectangle(x, y, sz.Width, _monthBarHeight));
      gx.DrawRectangle(Pens.Black, new Rectangle(x, y, sz.Width, _monthBarHeight));
      gx.FillRectangle(Brushes.Black, new Rectangle(x, y, sz.Width, 3));

      // draw rig column
      gx.DrawRectangle(Pens.Black, new Rectangle(x, y, _rigColWidth, _monthBarHeight));
      gx.DrawRectangle(Pens.Black, new Rectangle(x, y + _monthBarHeight, _rigColWidth, _dayHeight));


      bool continueDrawing = true;

      foreach (var mth in this.Values)
      {
        int mthLeft = mth.DrawingLeft;
        int mthWidth = mth.DrawingWidth;
        if (mthLeft > 0)
        {
          gx.DrawRectangle(Pens.Black, new Rectangle(mthLeft, y, mthWidth, _monthBarHeight));
          string mthText = mth.GetMonthTextFit(gx, this.MonthFont);
          if (mthText.IsNotBlank())
          {
            SizeF mthTextSize = gx.MeasureString(mthText, this.DayFont);
            PointF mthTextPoint = new PointF(mthLeft + (mthWidth / 2) - (mthTextSize.Width / 2), y + (_monthBarHeight / 2) - (mthTextSize.Height / 2) + 1);
            gx.DrawString(mthText, this.MonthFont, Brushes.Black, mthTextPoint);
          }
        }

        int topOfDay = y + _monthBarHeight;
        foreach (var day in mth.Values)
        {
          if (day.DrawThisDay)
          {
            gx.DrawRectangle(Pens.Black, new Rectangle(day.Left, topOfDay, day.PixelsToDraw, _dayHeight));
            string dayText = day.DayOfMonth.ToString();
            SizeF dayTextSize = gx.MeasureString(dayText, this.DayFont);
            if (dayTextSize.Width < day.PixelsToDraw)
            {
              PointF dayTextPoint = new PointF(day.Left + (day.PixelsToDraw / 2) - (dayTextSize.Width / 2) + 1, topOfDay + (_dayHeight / 2) - (dayTextSize.Height / 2) + 1);
              gx.DrawString(dayText, this.DayFont, Brushes.Black, dayTextPoint);
            }
          }
          else
          {
            continueDrawing = false;
            break;
          }
        }
        if (!continueDrawing)
          break;
      }

      y += _monthBarHeight + _dayHeight;

      return y;
    }

    private DateTime ComputeDaysToDraw(DateTime firstDayInView, int left, int width)
    {
      int x = left;
      int remainingPixels = width;
      int monthIndex = 0;

      while(remainingPixels > 0 && monthIndex < this.Count)
      {
        CalendarMonth mth = this.Values.ElementAt(monthIndex);
        if (mth.FirstDayOfMonth >= firstDayInView)
          for (int i = 0; i < mth.Count; i++)
          {
            var day = mth.Values.ElementAt(i);
            day.Left = x;
            if (remainingPixels >= _dayWidth)
            {
              day.PixelsToDraw = _dayWidth;
              remainingPixels -= _dayWidth;
              x += _dayWidth;
            }
            else
            {
              day.PixelsToDraw = remainingPixels;
              x += remainingPixels;
              remainingPixels = 0;
              break;
            }
          }
        monthIndex++;
      }
      return DateTime.MinValue;
    }
  }
}
