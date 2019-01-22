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
  public class Rig
  {
    public string Name {
      get;
      set;
    }
    public PadSet PadSet {
      get;
      set;
    }
    public RigSet RigSet {
      get;
      set;
    }
    private Calendar _calendar;

    public Font RigNameFont {
      get;
      set;
    }
    private float _rigColumnWidth;
    private float _rigRowHeight;
    private float _ch;
    private float _cw;

    public Rig(RigSet rigSet)
    {
      this.RigNameFont = new Font("Calibri", 10.0F);
      this.Name = String.Empty;
      this.PadSet = new PadSet();
      this.RigSet = rigSet;
      _rigColumnWidth = rigSet.RigColumnWidth;
      _rigRowHeight = rigSet.RigRowHeight;
      _ch = rigSet.ClientHeight;
      _cw = rigSet.ClientWidth;
      _calendar = rigSet.Calendar;
    }

    public Rig GetForDateSpan(DateSpan dateSpan)
    {
      Rig rigForSpan = null;

      foreach (var pad in this.PadSet.Values)
      {
        Pad padForSpan = pad.GetForDateSpan(dateSpan);
        if (padForSpan != null)
        {
          if (rigForSpan == null)
          {
            rigForSpan = new Rig(this.RigSet);
            rigForSpan.Name = this.Name;
          }

          if (!rigForSpan.PadSet.ContainsKey(padForSpan.PadNumber))
            rigForSpan.PadSet.Add(padForSpan.PadNumber, padForSpan);
        }
      }

      return rigForSpan;
    }

    public void Draw(Graphics gx, Point origin, int rightLimit)
    {
      var rigNameRectangle = new Rectangle(origin.X, origin.Y, _rigColumnWidth.ToInt32(), _rigRowHeight.ToInt32());
      gx.DrawRectangle(Pens.Black, rigNameRectangle);
      var ss = new StringSizer(gx, origin, rigNameRectangle, this.RigNameFont, this.Name.Trim());
      gx.DrawString(ss.StringToDraw, this.RigNameFont, Brushes.Black, ss.DrawingPoint);


      gx.FillRectangle(Brushes.Gray, new Rectangle(origin.X, (int)(origin.Y + _rigRowHeight), (int)_cw, 6));
      gx.DrawRectangle(Pens.Black, new Rectangle(origin.X, (int)(origin.Y + _rigRowHeight), (int)_cw, 6));


      foreach (var pad in this.PadSet.Values)
      {
        foreach (var well in pad.WellSet.Values)
        {
          if (well.SpudDate >= _calendar.StartDate && well.CompletionDate <= _calendar.EndDate)
          {
            well.Draw(gx, new Point(origin.X + (int)_rigColumnWidth, origin.Y), rightLimit);
          }
        }
      }
    }
  }
}
