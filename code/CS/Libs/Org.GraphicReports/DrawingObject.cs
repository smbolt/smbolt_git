using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GraphicReports
{
  public delegate void SelectionChangedHandler();
  public delegate void SetDrawingObjectsDirtyHandler();

  [Serializable]
  public class DrawingObject
  {
    public event SelectionChangedHandler SelectionChanged;
    public event SetDrawingObjectsDirtyHandler SetDrawingObjectsDirty;

    protected virtual void OnSelectionChanged()
    {
      SelectionChanged();
    }

    protected virtual void OnSetDrawingObjectsDirty()
    {
      SetDrawingObjectsDirty();
    }

    private string _name;
    public string Name
    {
      get {
        return _name;
      }
      set
      {
        _name = value;
        OnSetDrawingObjectsDirty();
      }
    }

    public ObjectType ObjType
    {
      get {
        return _type;
      }
      set
      {
        _type = value;
        OnSetDrawingObjectsDirty();
      }
    }

    private ObjectType _type;
    public ObjectType Type
    {
      get {
        return _type;
      }
      set
      {
        _type = value;
        OnSetDrawingObjectsDirty();
      }
    }

    private string _text;
    public string Text
    {
      get {
        return _text;
      }
      set
      {
        _text = value;
        OnSetDrawingObjectsDirty();
      }
    }

    private HorizontalAlignment _textHorzAlign;
    public HorizontalAlignment TextHorizAlign
    {
      get {
        return _textHorzAlign;
      }
      set
      {
        _textHorzAlign = value;
        OnSetDrawingObjectsDirty();
      }
    }

    private System.Drawing.Font _font;
    public System.Drawing.Font TextFont
    {
      get {
        return _font;
      }
      set
      {
        _font = value;
        OnSetDrawingObjectsDirty();
      }
    }

    private Image _img;

    private System.Drawing.Color _fillColor;
    public System.Drawing.Color FillColor
    {
      get {
        return _fillColor;
      }
      set
      {
        _fillColor = value;
        OnSetDrawingObjectsDirty();
      }
    }

    private bool _hasFillColor;
    public bool HasFillColor
    {
      get {
        return _hasFillColor;
      }
      set
      {
        _hasFillColor = value;
        OnSetDrawingObjectsDirty();
      }
    }

    private bool _isUpperCase;
    public bool IsUpperCase
    {
      get {
        return _isUpperCase;
      }
      set {
        _isUpperCase = value;
      }
    }

    private bool _isLowerCase;
    public bool IsLowerCase
    {
      get {
        return _isLowerCase;
      }
      set {
        _isLowerCase = value;
      }
    }

    private string _graphicsPath;
    public string GraphicsPath
    {
      get {
        return _graphicsPath;
      }
      set
      {
        _graphicsPath = value;
        if (_graphicsPath.Length > 0)
        {
          _img = Image.FromFile(_graphicsPath);
        }
        OnSetDrawingObjectsDirty();
      }
    }

    private float _left;
    public float Left
    {
      get {
        return _left;
      }
      set
      {
        _left = value;
        OnSetDrawingObjectsDirty();
      }
    }

    private float _top;
    public float Top
    {
      get {
        return _top;
      }
      set
      {
        _top = value;
        OnSetDrawingObjectsDirty();
      }
    }

    private float _width;
    public float Width
    {
      get {
        return _width;
      }
      set
      {
        _width = value;
        OnSetDrawingObjectsDirty();
      }
    }

    private float _height;
    public float Height
    {
      get {
        return _height;
      }
      set
      {
        _height = value;
        OnSetDrawingObjectsDirty();
      }
    }

    private int _borderWidth;
    public int BorderWidth
    {
      get {
        return _borderWidth;
      }
      set
      {
        _borderWidth = value;
        OnSetDrawingObjectsDirty();
      }
    }

    private System.Drawing.Color _borderColor;
    public System.Drawing.Color BorderColor
    {
      get {
        return _borderColor;
      }
      set
      {
        _borderColor = value;
        OnSetDrawingObjectsDirty();
      }
    }

    private bool _selected;
    public bool Selected
    {
      get {
        return _selected;
      }
      set
      {
        _selected = value;
        OnSelectionChanged();
        OnSetDrawingObjectsDirty();
      }
    }

    private bool _isLocked;
    public bool IsLocked
    {
      get {
        return _isLocked;
      }
      set
      {
        _isLocked = value;
        OnSetDrawingObjectsDirty();
      }
    }

    public DrawingObjectSet DrawingObjectSet {
      get;
      set;
    }

    public DrawingObject()
    {
      _name = "unnamed";
      _text = "";
      _graphicsPath = "";
      _left = 5.0F;
      _top = 5.0F;
      _width = 20.0F;
      _height = 20.0F;
      _selected = false;
      _font = new Font("Arial", 12);
      _fillColor = Color.LightSkyBlue;
      _hasFillColor = true;
      _isUpperCase = false;
      _isLowerCase = false;
      _borderWidth = 0;
      _borderColor = Color.Black;
      _textHorzAlign = HorizontalAlignment.Left;
      _isLocked = false;
      this.DrawingObjectSet = new DrawingObjectSet();
    }


    public void DrawObject(Graphics gr, float scale, PointF origin, PointF adjust, DisplayMode mode, Dictionary<string, string> dictionary)
    {
      float left = _left * scale + origin.X * scale;
      float top = _top * scale + origin.Y * scale;
      float width = _width * scale;
      float height = _height * scale;
      float holdFontSize = _font.Size;
      float textLeft = 0F;
      string holdText = this._text;
      SizeF szText = new SizeF(0.0F, 0.0F);

      System.Drawing.Brush _fillBrush = new System.Drawing.SolidBrush(_fillColor);

      try
      {

        switch (this._type)
        {
          case ObjectType.TextObject:
            bool IsFirstName = false;

            if (mode != DisplayMode.Designer)
            {
              //if (this._text == "@firstname")
              //    IsFirstName = true;

              if (dictionary.ContainsKey(this._text))
                this._text = dictionary[this._text];
            }

            string text = _text;

            if (IsFirstName)
              text = String.Empty;

            if (IsUpperCase)
              text = _text.ToUpper();
            if (IsLowerCase)
              text = _text.ToLower();


            if (this._text.Length > 0)
            {
              Font f = new Font(_font.Name, _font.Size * scale, _font.Style);
              switch (this._textHorzAlign)
              {
                case HorizontalAlignment.Right:
                  szText = gr.MeasureString(text, f);
                  textLeft = left + this.Width * scale - szText.Width;
                  gr.DrawString(text, f, _fillBrush, textLeft, top + 3);
                  break;

                case HorizontalAlignment.Center:
                  szText = gr.MeasureString(text, f);
                  textLeft = left + this._width * scale / 2 - szText.Width / 2;
                  gr.DrawString(text, f, _fillBrush, textLeft, top + 3);
                  break;

                default:
                  szText = gr.MeasureString(text, f);
                  gr.DrawString(text, f, _fillBrush, left + 3, top + 3);
                  break;
              }
            }

            if (mode != DisplayMode.Designer)
              this._text = holdText;

            if (_borderWidth > 0)
              gr.DrawRectangle(new Pen(_borderColor, _borderWidth), left, top, width, height);

            break;

          case ObjectType.RectangleObject:
            if (_hasFillColor)
            {
              gr.FillRectangle(_fillBrush, left + 1, top + 1, width - 1, height - 1);
            }
            if (_borderWidth > 0)
            {
              gr.DrawRectangle(new Pen(_borderColor, _borderWidth), left, top, width, height);
            }
            break;

          case ObjectType.EllipseObject:
            if (_hasFillColor)
            {
              gr.FillEllipse(_fillBrush, left + 1, top + 1, width - 1, height - 1);
            }
            if (_borderWidth > 0)
            {
              gr.DrawEllipse(new Pen(_borderColor, _borderWidth), left, top, width, height);
            }
            break;

          case ObjectType.GraphicsObject:
            gr.DrawImage(_img, new RectangleF(left + 1, top + 1, width - 1, height - 1));
            if (_borderWidth > 0)
            {
              gr.DrawRectangle(new Pen(_borderColor, _borderWidth), left, top, width, height);
            }

            break;

            int pageWidth = 1100 * (int)scale;
            int pageHeight = 850 * (int)scale;  // do something different for getting these
            int shapeWidth = pageWidth / 6 * 4;
            int shapeHeight = 30;
            int picWidth = 250; // Convert.ToInt32(pic.Width / pic.HorizontalResolution * 100);
            int picHeight = 250; // Convert.ToInt32(pic.Height / pic.VerticalResolution * 100);

            int x = (int)(adjust.X * scale) + (pageWidth / 6 + 14);  // the addition of 14 helps to center the objects
            int y = (int)(adjust.Y * scale) + (pageHeight / 2 - 95);

            gr.FillRectangle(Brushes.Black, new RectangleF(x * scale, (y - 47) * scale, shapeWidth * scale, shapeHeight * scale));
            gr.FillRectangle(Brushes.Blue, new RectangleF(x * scale, (y - 50) * scale, shapeWidth * scale, shapeHeight * scale));

            gr.FillRectangle(Brushes.Black, new RectangleF(x * scale, (y + 3) * scale, shapeWidth * scale, shapeHeight * scale));
            gr.FillRectangle(Brushes.Red, new RectangleF(x * scale, (y * scale), shapeWidth * scale, shapeHeight * scale));

            gr.FillRectangle(Brushes.Black, new RectangleF(x * scale, (y + 53) * scale, shapeWidth * scale, shapeHeight * scale));
            gr.FillRectangle(Brushes.SpringGreen, new RectangleF(x * scale, (y + 50) * scale, shapeWidth * scale, shapeHeight * scale));


            gr.FillPolygon(Brushes.White, new PointF[] {new PointF(x * scale, (y - 50) * scale),
                             new PointF((x + 133) * scale, (y - 50) * scale),
                             new PointF(x * scale, (y + 83) * scale),
                             new PointF(x * scale, (y - 50) * scale)
            });

            gr.FillPolygon(Brushes.White, new PointF[] {new PointF((x + shapeWidth) * scale, (y - 50) * scale),
                             new PointF((x + shapeWidth) * scale, (y + 83) * scale),
                             new PointF((x + shapeWidth - 133) * scale, (y + 83) * scale),
                             new PointF((x + shapeWidth) * scale, (y - 50) * scale)
            });


            x = (int)(adjust.X * scale) + ((pageWidth / 2) - (picWidth / 2) + 14);  // the addition of 14 helps to center the objects
            y = (int)(adjust.Y * scale) + ((pageHeight / 2) - (picHeight / 2) - 80);

            //x = ((pageWidth / 2) - (picWidth / 2));
            //y = ((pageHeight / 2) - (picHeight / 2));

            //gr.DrawImage(pic, new RectangleF(x * scale, y * scale, 250 * scale, 250 * scale));
            gr.DrawEllipse(new Pen(Color.White, 30), new RectangleF((x - 16) * scale, (y - 16) * scale, 281 * scale, 281 * scale));
            gr.FillRectangle(Brushes.White, new RectangleF((x - 1) * scale, (y - 1) * scale, 35 * scale, 35 * scale));
            gr.FillRectangle(Brushes.White, new RectangleF((x + 216) * scale, (y - 1) * scale, 35 * scale, 35 * scale));
            gr.FillRectangle(Brushes.White, new RectangleF((x - 1) * scale, (y + 216) * scale, 35 * scale, 35 * scale));
            gr.FillRectangle(Brushes.White, new RectangleF((x + 216) * scale, (y + 216) * scale, 35 * scale, 35 * scale));
            gr.DrawEllipse(new Pen(Color.Red, 2), new RectangleF((x - 1) * scale, (y - 1) * scale, 251 * scale, 251 * scale));
            gr.DrawEllipse(new Pen(Color.Blue, 1), new RectangleF((x - 7) * scale, (y - 7) * scale, 263 * scale, 263 * scale));

            GraphicsPath pa = new GraphicsPath();
            pa.AddEllipse(820, 603, 250, 150);
            PathGradientBrush br = new PathGradientBrush(pa);
            br.CenterColor = Color.SpringGreen;
            Color[] colors = new Color[] { Color.White };
            br.SurroundColors = colors;
            gr.FillPath(br, pa);
            br.Dispose();
            pa.Dispose();

            break;


          default:
            break;
        }


        if (mode == DisplayMode.Designer)
        {
          if (_selected)
          {
            gr.DrawRectangle(Pens.Blue, left, top, width, height);
          }
          else
          {
            if (_borderWidth == 0)
            {
              gr.DrawRectangle(Pens.LightGray, left, top, width, height);
            }
          }
        }


        if (_selected & mode == DisplayMode.Designer)
        {
          if (_isLocked)
          {
            gr.FillRectangle(Brushes.DarkGray,
                             left + (width / 2) - 3, top - 3, 7, 7);
            gr.FillRectangle(Brushes.DarkGray,
                             left + (width / 2) - 3, top + height - 3, 7, 7);
            gr.FillRectangle(Brushes.DarkGray,
                             left - 3, top + (height / 2) - 3, 7, 7);
            gr.FillRectangle(Brushes.DarkGray,
                             left + width - 3, top + (height / 2) - 3, 7, 7);
          }
          else
          {
            gr.FillRectangle(Brushes.LightGreen,
                             left + (width / 2) - 3, top - 3, 7, 7);
            gr.FillRectangle(Brushes.LightGreen,
                             left + (width / 2) - 3, top + height - 3, 7, 7);
            gr.FillRectangle(Brushes.LightGreen,
                             left - 3, top + (height / 2) - 3, 7, 7);
            gr.FillRectangle(Brushes.LightGreen,
                             left + width - 3, top + (height / 2) - 3, 7, 7);
          }
        }

      }
      catch (System.Exception ex)
      {
        throw new Exception("An exception has occurred doing what?", ex);
      }

    }

    public void Select()
    {
      _selected = true;
      if (SelectionChanged != null)
        SelectionChanged();

      OnSetDrawingObjectsDirty();
    }

    public void Unselect()
    {
      _selected = false;
      if (SelectionChanged != null)
        SelectionChanged();

      OnSetDrawingObjectsDirty();
    }

    public bool SelectAtXY(float x, float y, float scale)
    {
      bool select = false;

      float left = _left * scale;
      float top = _top * scale;
      float width = _width * scale;
      float height = _height * scale;

      RectangleF rect = new RectangleF(left, top, width, height);
      if (rect.Contains(x, y))
      {
        select = true;
      }

      return select;
    }
  }
}
