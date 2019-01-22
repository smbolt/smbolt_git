using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Org.GS;

namespace Org.GraphicReports
{
  public class StringSizer
  {
    private RectangleF _drawingRectangle;
    private Font _font;
    private string _text;

    private string _stringToDraw;
    public string StringToDraw {
      get {
        return _stringToDraw;
      }
    }

    private RectangleF _stringRectangleF;
    public RectangleF StringRectangleF {
      get {
        return _stringRectangleF;
      }
    }
    public Rectangle StringRectangle {
      get {
        return _stringRectangleF.ToRectangle();
      }
    }

    private PointF _drawingPointF;
    public PointF DrawingPointF {
      get {
        return _drawingPointF;
      }
    }
    public Point DrawingPoint {
      get {
        return _drawingPointF.ToPoint();
      }
    }

    public StringSizer(Graphics gx, PointF origin, RectangleF drawingRectangle, Font font, string text)
    {
      _stringRectangleF = new RectangleF();
      _drawingPointF = new PointF();

      _drawingRectangle = drawingRectangle;
      _font = font;
      _text = text;

      string origString = _text.Trim();
      _stringToDraw = origString;

      float maxWidth = drawingRectangle.Width - 6;
      var tm = gx.MeasureString(_stringToDraw, font);
      var stringSize = new SizeF(tm.Width, tm.Height);

      if (tm.Width > maxWidth)
      {
        string ellipsisString = _stringToDraw + "...";
        string workString = _stringToDraw;
        while (tm.Width > maxWidth)
        {
          if (workString.Length > 0)
          {
            workString = workString.TrimLastChar();
            ellipsisString = workString + "...";
            tm = gx.MeasureString(ellipsisString, font);
            if (tm.Width <= maxWidth)
            {
              _stringToDraw = ellipsisString;
              stringSize = new SizeF(tm.Width, tm.Height);
            }
          }
          else
          {
            ellipsisString = ellipsisString.TrimLastChar();
            tm = gx.MeasureString(ellipsisString, font);
            if (tm.Width <= maxWidth)
            {
              stringSize = new SizeF(tm.Width, tm.Height);
            }
          }
        }
      }

      float xOffset = _drawingRectangle.Width / 2 - stringSize.Width / 2;
      float yOffset = _drawingRectangle.Height / 2 - stringSize.Height / 2;

      _stringRectangleF = new RectangleF(new PointF(xOffset, yOffset), stringSize);
      _drawingPointF = new PointF(origin.X + xOffset, origin.Y + yOffset);
    }
  }
}
