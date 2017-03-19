using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Org.GS;

namespace Org.RPT
{
  public enum HorizAlign
  {
    NotSet,
    Left,
    Right,
    Center
  }

  public enum FontStyle
  {
    Regular,
    Bold,
    BoldItalic,
    Italic
  }

  public enum TextCase
  {
    NotSet,
    Upper,
    Lower
  }


  [XMap(XType = XType.Element)]
  public class ReportText
  {
    [XMap]
    public string Text 
    {
      get
      {
        return Get_Text(); 
      }
      set
      {
        this._rawText = value;
      }
    }

    [XMap]
    public string DataSource { get; set; }

    [XMap(DefaultValue = "NotSet")]
    public TextCase TextCase { get; set; }

    [XMap(DefaultValue = "Left")]
    public HorizAlign HorzAlign { get; set; }

    [XMap(DefaultValue = "Tahoma")]
    public string FontName { get; set; }

    [XMap(DefaultValue = "10")]
    public float FontSize { get; set; }

    [XMap(DefaultValue = "Regular")]
    public FontStyle FontStyle { get; set; }

    [XMap(DefaultValue = "")]
    public string TextColor { get; set; }

    [XMap(DefaultValue = "False")]
    public bool WrapText { get; set; }

    private string _rawText;
    public string RawText
    {
      get { return _rawText; }
    }

    public ReportText()
    {
      this.Text = String.Empty;
      this.DataSource = String.Empty;
      this.TextCase = TextCase.NotSet;
      this.HorzAlign = HorizAlign.Left;
      this.FontName = "Tahoma";
      this.FontSize = 9.0F;
      this.FontStyle = FontStyle.Regular;
      this.TextColor = "#000000";
      this.WrapText = false;
    }

    private string Get_Text()
    {
      switch (this.TextCase)
      {
        case TextCase.Upper:
          return _rawText.ToUpper();

        case TextCase.Lower:
          return _rawText.ToLower();
      }

      return _rawText;
    }
  }

  public static class ReportExtensionMethods
  {
    public static System.Drawing.FontStyle ToFontStyle(this Org.RPT.FontStyle styleIn)
    {
      switch (styleIn)
      {
        case FontStyle.Bold:
          return System.Drawing.FontStyle.Bold;
        case FontStyle.Italic:
          return System.Drawing.FontStyle.Italic;
        case FontStyle.BoldItalic:
          return System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic;
      }
                    
      return System.Drawing.FontStyle.Regular;
    }


  }
}
