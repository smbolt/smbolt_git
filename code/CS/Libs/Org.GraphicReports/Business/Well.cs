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
  public class Well
	{
		private static Font _wellNameFont = new Font("Calibri", 9.0F);

		public int WellNumber { get; set; }
		public int WellOrdinal { get; set; }
		public string WellName { get; set; }
		public DateTime? SpudDate { get; set; }
		public DateTime? CompletionDate { get; set; }
		public DateTime? AFEDate { get; set; }
		public int UnitNumber { get; set; }
		public string UnitName { get; set; }
		public bool IsUsed { get; set; }

    public static Font _smallFont;
    public static Font _largeFont;
    public static Color _colorGreen = Color.FromArgb(0, 128, 57);
    public static Color _colorRed = Color.FromArgb(255, 70, 35);
    public static Color _colorYellow = Color.FromArgb(240, 230, 140);
    public static Color _color2 = Color.Black;
    public static Brush _brushGreen;
    public static Brush _brushRed;
    public static Brush _brushYellow;
    public static Pen _pen1;

		private Pad _pad;
		private Rig _rig;
		private RigSet _rigSet;
		private Calendar _calendar;
		private int _dayWidth;
		private float _rigColumnWidth;
		private float _rigRowHeight;

		public Well(Pad pad)
		{
			_pad = pad;
			_rig = _pad.Rig;
			_rigSet = _rig.RigSet;
			_calendar = _rigSet.Calendar;
			_dayWidth = _calendar.DayWidth;
			_rigColumnWidth = _rigSet.RigColumnWidth;
			_rigRowHeight = _rigSet.RigRowHeight;

			this.WellNumber = 0;
			this.WellOrdinal = 0;
			this.WellName = String.Empty;
			this.SpudDate = null;
			this.CompletionDate = null;
			this.AFEDate = null;
			this.UnitNumber = 0;
			this.UnitName = String.Empty;
			this.IsUsed = false;

      if (_smallFont == null)
        _smallFont = new Font("Calibri", 8.0F);

      if (_largeFont == null)
        _largeFont = new Font("Calibri", 11.0F);

      if (_brushGreen == null)
        _brushGreen = new SolidBrush(_colorGreen); 

      if (_brushRed == null)
        _brushRed = new SolidBrush(_colorRed); 

      if (_brushYellow == null)
        _brushYellow = new SolidBrush(_colorYellow); 

      if (_pen1 == null)
        _pen1 = new Pen(_color2, 1.0F);
		}

		public void Draw(Graphics gx, Point origin, int rightLimit)
		{
			int x = origin.X;
			int y = origin.Y;

			int skipDays = (this.SpudDate - _calendar.StartDate).Value.Days;
			x += skipDays * _dayWidth;
			int wellWidthDays = (this.CompletionDate - this.SpudDate).Value.Days;
			int wellWidth = wellWidthDays * _dayWidth;
      int firstSecondWellRowWidth = wellWidth / 3;
      int thirdWellRowWidth = wellWidth - (firstSecondWellRowWidth * 2); 
      int wellNumberHeight = ((int)_rigRowHeight / 3) - 3;
      int padNumberHeight = (int)_rigRowHeight - (wellNumberHeight * 2);

      var wellRect = new Rectangle(x, y, wellWidth, (int)_rigRowHeight);

      gx.DrawRectangle(Pens.Black, wellRect);

      var rectTop1 = new Rectangle(x, y, firstSecondWellRowWidth, wellNumberHeight);
      string topText1 = "CellTop1";
      var ss1 = new StringSizer(gx, new Point(rectTop1.X, rectTop1.Y), rectTop1, _smallFont, topText1.Trim());
      gx.FillRectangle(Brushes.White, rectTop1);
      gx.DrawRectangle(Pens.Black, rectTop1);
      gx.DrawString(ss1.StringToDraw, _smallFont, _brushGreen, ss1.DrawingPoint);

      var rectTop2 = new Rectangle(x + firstSecondWellRowWidth, y, firstSecondWellRowWidth, wellNumberHeight);
      string topText2 = "CellTop2";
      var ss2 = new StringSizer(gx, new Point(rectTop2.X, rectTop2.Y), rectTop2, _smallFont, topText2.Trim());
      gx.FillRectangle(Brushes.White,rectTop2);
      gx.DrawRectangle(_pen1, rectTop2);
      gx.DrawString(ss2.StringToDraw, _smallFont, _brushGreen, ss2.DrawingPoint);

      var rectTop3 = new Rectangle(x + (firstSecondWellRowWidth * 2), y, thirdWellRowWidth, wellNumberHeight);
      string topText3 = "CellTop3";
      var ss3 = new StringSizer(gx, new Point(rectTop3.X, rectTop3.Y), rectTop3, _smallFont, topText3.Trim());
      gx.FillRectangle(Brushes.White, rectTop3);
      gx.DrawRectangle(Pens.Black, rectTop3);
      gx.DrawString(ss3.StringToDraw, _smallFont, _brushRed, ss3.DrawingPoint);

      var rectCenter1 = new Rectangle(x, y + wellNumberHeight, wellWidth, padNumberHeight);
      string CenterText4 = "CellCenter4";
      var ss4 = new StringSizer(gx, new Point(rectCenter1.X, rectCenter1.Y), rectCenter1, _smallFont, CenterText4.Trim());
      gx.FillRectangle(_brushYellow, rectCenter1);
      gx.DrawRectangle(Pens.Black, rectCenter1);
      gx.DrawString(ss4.StringToDraw, _largeFont, Brushes.Black, ss4.DrawingPoint);

      var rectBottom1 = new Rectangle(x, y + wellNumberHeight + padNumberHeight, firstSecondWellRowWidth, wellNumberHeight);
      string bottomText1 = "CellBottom1";
      var ss5 = new StringSizer(gx, new Point(rectBottom1.X, rectBottom1.Y), rectBottom1, _smallFont, bottomText1.Trim());
      gx.FillRectangle(Brushes.White, rectBottom1);
      gx.DrawRectangle(Pens.Black, rectBottom1);
      gx.DrawString(ss5.StringToDraw, _smallFont, _brushRed, ss5.DrawingPoint);
      
      var rectBottom2 = new Rectangle(x + firstSecondWellRowWidth, y + wellNumberHeight + padNumberHeight, firstSecondWellRowWidth, wellNumberHeight);
      string bottomText2 = "CellBottom2";
      var ss6 = new StringSizer(gx, new Point(rectBottom2.X, rectBottom2.Y), rectBottom2, _smallFont, bottomText2.Trim());
      gx.FillRectangle(Brushes.White, rectBottom2);
      gx.DrawRectangle(Pens.Black, rectBottom2);
      gx.DrawString(ss6.StringToDraw, _smallFont, _brushRed, ss6.DrawingPoint);

      var rectBottom3 = new Rectangle(x + (firstSecondWellRowWidth * 2), y + wellNumberHeight + padNumberHeight, thirdWellRowWidth, wellNumberHeight);
      string bottomText3 = "CellBottom3";
      var ss7 = new StringSizer(gx, new Point (rectBottom3.X, rectBottom3.Y), rectBottom3, _smallFont, bottomText3.Trim());
      gx.FillRectangle(Brushes.White, rectBottom3);
      gx.DrawRectangle(Pens.Black, rectBottom3);
      gx.DrawString(ss7.StringToDraw, _smallFont, _brushRed, ss7.DrawingPoint);
		}
	}
}
