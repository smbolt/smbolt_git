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
  public class RigSet : Dictionary<string, Rig>
  {
    public Font PadNameFont {
      get;
      set;
    }
    public Calendar Calendar {
      get;
      set;
    }
    private ReportParms _reportParms;
    private ReportDiagnostics _diag;
    private float _x;
    private float _y;
    private float _w;
    private float _h;
    private float _t;
    private float _b;
    private float _l;
    private float _r;
    private float _ch;
    private float _cw;

    private float _rigColumnWidth = 90.0F;
    public float RigColumnWidth {
      get {
        return _rigColumnWidth;
      }
    }
    private float _rigRowHeight = 69.0F;
    public float RigRowHeight {
      get {
        return _rigRowHeight;
      }
    }
    public float ClientHeight {
      get {
        return _ch;
      }
    }
    public float ClientWidth {
      get {
        return _cw;
      }
    }

    public RigSet(ReportParms reportParms)
    {
      _reportParms = reportParms;
      _diag = _reportParms.ReportDiagnostics;
      _w = _reportParms.ActualPageSize.Width;
      _h = _reportParms.ActualPageSize.Height;
      _l = _reportParms.Margins.Left;
      _t = _reportParms.Margins.Top;
      _r = _w - _reportParms.Margins.Right;
      _b = _w - _reportParms.Margins.Bottom;
      _cw = _reportParms.Margins.GetClientWidth(_w);
      _ch = _reportParms.Margins.GetClientHeight(_h);

      this.Calendar = new Calendar(reportParms.DateSpan);

      this.PadNameFont = new Font("Calibri", 10.0F);
    }

    public void DrawReport(Graphics gx)
    {
      gx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
      gx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
      gx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
      gx.TextRenderingHint = TextRenderingHint.AntiAlias;

      gx.FillRectangle(Brushes.White, new Rectangle(0, 0, (int) _w, (int) _h));

      if (_diag.DiagnosticsActive)
      {
        DrawReportDiagnostics(gx);
      }

      _x = _l;
      _y = _t;

      if (_reportParms.PrintCalendar)
      {
        _y = (float) DrawCalendar(gx, new Point((int)_l, (int)_t));
      }

      float _calendarLeft = _x + _rigColumnWidth;

      foreach (var rig in this.Values)
      {
        if (_y > _b)
          break;

        rig.Draw(gx, new Point((int)_x, (int)_y), (int)_r);
        _y += _rigRowHeight + 6;

        //gx.DrawRectangle(Pens.Black, new Rectangle((int)_x, (int)_y, (int)_rigColumnWidth, (int)_rigRowHeight));
        //gx.DrawString(rig.Name, this.PadNameFont, Brushes.Black, new Point((int)_x + 10, (int)_y + 10));
        //gx.FillRectangle(Brushes.Gray, new Rectangle((int)_x, (int)(_y + _rigRowHeight), (int)_cw, 6));
        //gx.DrawRectangle(Pens.Black, new Rectangle((int)_x, (int)(_y + _rigRowHeight), (int)_cw, 6));


        //foreach (var pad in rig.PadSet.Values)
        //{
        //  foreach (var well in pad.WellSet.Values)
        //  {
        //    if (well.SpudDate >= this.Calendar.StartDate && well.CompletionDate <= this.Calendar.EndDate)
        //    {
        //      well.Draw(gx, new Point((int)_calendarLeft, (int)_y), (int)_r);

        //    }
        //  }

        //}
        //_y += _rigRowHeight + 6;
      }



      var blueBrush = new SolidBrush(Color.FromArgb(100, 149, 237));


      //gx.DrawString("Rig Schedule: 3/1/2016 - 6/1/2016", new Font("Calibri", 14.0F, FontStyle.Bold), Brushes.Green, new PointF(50.0F, 100.0F));
      //gx.DrawLine(new Pen(Brushes.Black, 2.5F), new Point(50, 124), new Point(1000, 124));

      //gx.FillRectangle(blueBrush, new Rectangle(50, 125, 75, 28));
      //gx.DrawRectangle(Pens.Black, new Rectangle(50, 125, 75, 28));

      //gx.FillRectangle(blueBrush, new Rectangle(125, 125, 50, 28));
      //gx.DrawRectangle(Pens.Black, new Rectangle(125, 125, 50, 28));
      //gx.DrawString("February", new Font("Calibri", 8.0F, FontStyle.Bold), Brushes.Black, new PointF(128, 132));

      //gx.FillRectangle(blueBrush, new Rectangle(175, 125, 350, 28));
      //gx.DrawRectangle(Pens.Black, new Rectangle(175, 125, 350, 28));
      //gx.DrawString("March 2016", new Font("Calibri", 8.0F, FontStyle.Bold), Brushes.Black, new PointF(310, 132));

    }

    private void DrawReportDiagnostics(Graphics gx)
    {
      if (_diag.ShowMargins)
        gx.DrawRectangle(Pens.LightGray, new Rectangle((int)_l, (int)_t, (int)_cw, (int)_ch));


    }

    private int DrawCalendar(Graphics gx, Point pt)
    {
      return this.Calendar.Draw(gx, pt, new Size((int)_cw, (int)_ch));
    }

    public RigSet GetForDateSpan(DateSpan dateSpan)
    {
      try
      {
        var rs = new RigSet(_reportParms);

        foreach (var rig in this.Values)
        {
          Rig rigForSpan = rig.GetForDateSpan(dateSpan);
          if (rigForSpan != null)
            rs.Add(rigForSpan.Name, rigForSpan);
        }

        return rs;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to create the DrawingObjectSet from the RigSet for the time span '" +
                            dateSpan.StartDateTime.ToString("MM/dd/yyyy") + "' to '" + dateSpan.EndDateTime.ToString("MM/dd/yyyy") + "'.", ex);
      }
    }

    public string GetReport()
    {
      StringBuilder sb = new StringBuilder();

      foreach (var rig in this.Values)
      {
        sb.Append("Rig: " + rig.Name + g.crlf);
        foreach (var pad in rig.PadSet.Values)
        {
          sb.Append("  Pad: " + pad.PadNumber.ToString().PadTo(8) + pad.PadName + g.crlf);
          foreach (var well in pad.WellSet.Values)
          {
            sb.Append("    Well " + well.WellOrdinal.ToString() + "  " +
                      well.WellNumber.ToString().PadTo(8) +
                      well.WellName.PadTo(30) + "  " +
                      well.SpudDate.Value.ToString("yyyy-MM-dd") + "  " +
                      well.CompletionDate.Value.ToString("yyyy-MM-dd") + "  " +
                      well.UnitNumber.ToString().PadTo(8) +
                      well.UnitName + g.crlf);

          }
        }
      }

      return sb.ToString();
    }
  }
}
