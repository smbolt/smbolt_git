using System;
using System.Drawing;

namespace NameTags
{
  /// <summary>
  /// Summary description for NameTag.
  /// </summary>
  public class NameTag
  {
    private float _height;
    private float _width;
    private float _left;
    private float _top;
    private string _name;
    private Color _nameColor;
    private Font _nameFont;
    private Color _otherColor;
    private Font _otherFont;
    private Bitmap _picture;


    public NameTag()
    {
      //
      // TODO: Add constructor logic here
      //
    }

    public void Draw(System.Drawing.Graphics gr, float scale)
    {
      SizeF nameSize;
      SizeF topTextSize;
      SizeF bottomTextSize;
      string topText = "Church in Oklahoma City";
      string bottomText = "Truth School Jr. - 2004";

//			gr.DrawRectangle(System.Drawing.Pens.Black,
//				_left * scale / 100,
//				_top * scale / 100,
//				_width * scale / 100,
//				_height * scale / 100);

      gr.DrawImage(_picture, new RectangleF(_left * scale,
                                            _top * scale,
                                            _width * scale,
                                            _height * scale));

      Font nameFont = new Font(NameFont.FontFamily, NameFont.Size * scale, NameFont.Style);
      Font otherFont = new Font(OtherFont.FontFamily, OtherFont.Size * scale, OtherFont.Style);

      ////////////////////////////////////////////
      // place the top text
      ////////////////////////////////////////////
      topTextSize = gr.MeasureString(topText, otherFont);
      float topTextWidth = topTextSize.Width;

      gr.DrawString(topText,
                    otherFont,
                    Brushes.Black,
                    (_left + (_width / 2) - (topTextWidth / 2)) * scale,
                    (_top + 29) * scale);

      gr.DrawString(topText,
                    otherFont,
                    (Brush) new System.Drawing.SolidBrush(OtherColor),
                    (((_left + (_width / 2) - (topTextWidth / 2))) * scale) + 1,
                    ((_top + 28) * scale));

      ////////////////////////////////////////////
      // place the name
      ////////////////////////////////////////////
      nameSize = gr.MeasureString(_name, NameFont);
      float nameWidth = nameSize.Width;

      gr.DrawString(_name,
                    nameFont,
                    Brushes.Black,
                    (_left + (_width / 2) - (nameWidth / 2)) * scale,
                    (_top + 72) * scale);

      gr.DrawString(_name,
                    nameFont,
                    (Brush) new System.Drawing.SolidBrush(NameColor),
                    (((_left + (_width / 2) - (nameWidth / 2))) * scale) + 1,
                    ((_top + 71) * scale));

      ////////////////////////////////////////////
      // place the bottom text
      ////////////////////////////////////////////
      bottomTextSize = gr.MeasureString(bottomText, otherFont);
      float bottomTextWidth = bottomTextSize.Width;

      gr.DrawString(bottomText,
                    otherFont,
                    Brushes.Black,
                    (_left + (_width / 2) - (bottomTextWidth / 2)) * scale,
                    (_top + 145) * scale);

      gr.DrawString(bottomText,
                    otherFont,
                    (Brush) new System.Drawing.SolidBrush(OtherColor),
                    (((_left + (_width / 2) - (bottomTextWidth / 2))) * scale) + 1,
                    ((_top + 144) * scale));
    }


    public float Top
    {
      get
      {
        return _top;
      }
      set
      {
        _top = value;
      }
    }

    public float Left
    {
      get
      {
        return _left;
      }
      set
      {
        _left = value;
      }
    }

    public float Height
    {
      get
      {
        return _height;
      }
      set
      {
        _height = value;
      }
    }

    public float Width
    {
      get
      {
        return _width;
      }
      set
      {
        _width = value;
      }
    }

    public string Name
    {
      get
      {
        return _name;
      }
      set
      {
        _name = value;
      }
    }

    public Color NameColor
    {
      get
      {
        return _nameColor;
      }
      set
      {
        _nameColor = value;
      }
    }

    public Font NameFont
    {
      get
      {
        return _nameFont;
      }
      set
      {
        _nameFont = value;
      }
    }

    public Color OtherColor
    {
      get
      {
        return _otherColor;
      }
      set
      {
        _otherColor = value;
      }
    }

    public Font OtherFont
    {
      get
      {
        return _otherFont;
      }
      set
      {
        _otherFont = value;
      }
    }

    public Bitmap Picture
    {
      get
      {
        return _picture;
      }
      set
      {
        _picture = value;
      }
    }
  }
}
