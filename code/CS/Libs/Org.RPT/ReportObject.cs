using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using Org.GS;

namespace Org.RPT
{
  [XMap(XType = XType.Element, KeyName = "Name")]
  public class ReportObject
  {
    [XMap(IsKey = true, DefaultValue = "ReportObject")]
		public string Name { get; set; }

    [XMap(DefaultValue = "ReportText")]
    public ReportObjectType ReportObjectType { get; set; }

    [XMap(DefaultValue = "False")]
    public bool PerPage { get; set; }

    [XMap(XType = XType.Element)]
    public ReportText ReportText { get; set; }

    [XMap(XType = XType.Element)]
    public ReportObjectSize ReportObjectSize { get; set; }

    [XMap(XType = XType.Element)]
    public ReportObjectLocation ReportObjectLocation { get; set; }

    [XMap(DefaultValue = "")]
		public string FillColor { get; set; }

    [XMap(DefaultValue = "")]
    public string BorderColor { get; set; }

    [XMap(DefaultValue = "0")]
    public float BorderWidth { get; set; }

    [XMap(XType = XType.Element, WrapperElement = "ReportColumns", CollectionElements = "ReportColumn")]
    public ReportColumns ReportColumns { get; set; }

    public Dictionary<string, int> ColumnReferences { get; set; }

    [XMap(XType = XType.Element, CollectionElements = "ReportObject",  WrapperElement="ReportObjectSet", SequenceDuplicates = true, KeyName = "Name")]
    public ReportObjectSet ReportObjectSet { get; set; }
        
    [XMap(DefaultValue = "")]
		public string ImagePath { get; set; }

    public Image Image { get; set; }

    public ReportObject Parent { get; set; }
    public ReportDef RDef { get; set; }
    public PointF ActualLocation { get; set; }
    public SizeF ActualSize { get; set; }
    public int Level { get; set; }
    public float LeftEdge { get; set; }
    public float CurrX { get; set; }
    public float CurrY { get; set; }
    public ReportObject ParentReportObject { get { return this.Get_ParentReportObject(); } }

    public ReportObject()
		{
      this.Name = String.Empty;
      this.ReportObjectType = ReportObjectType.ReportText;
      this.PerPage = false;
      this.ReportText = new ReportText();
      this.ReportObjectSize = new ReportObjectSize();
      this.ReportObjectLocation = new ReportObjectLocation();
      this.FillColor = String.Empty;
      this.BorderColor = String.Empty;
      this.BorderWidth = 0.0F;
      this.ReportColumns = new ReportColumns();
      this.ColumnReferences = new Dictionary<string, int>();
      this.ReportObjectSet = new ReportObjectSet();
      this.ImagePath = String.Empty;
      this.Image = null;
      this.Parent = null;
      this.RDef = null;
      this.ActualLocation = new PointF(0, 0);
      this.ActualSize = new SizeF(0, 0);
      this.Level = 0;
      this.LeftEdge = 0.0F;
      this.CurrX = 0.0F;
      this.CurrY = 0.0F;
		}

		public void LayoutObject(Graphics gr, float scale)
		{
      ReportObjectType roType = this.ReportObjectType;
      string roName = this.Name;

      float left = 0.0F;
      float top = 0.0F;
      float width = 0.0F;
      float height = 0.0F;

      float currY = this.Parent.CurrY;
      this.CurrX = this.Parent.CurrX;
      this.LeftEdge = this.Parent.LeftEdge;

      HeightMode heightMode = HeightMode.Relative;

      if (roType == ReportObjectType.Rectangle)
      {
        left = this.Parent.LeftEdge;
        top = this.Parent.CurrY;
      }
      else
      {
        if (this.ReportObjectLocation != null)
        {
          heightMode = this.ReportObjectLocation.HeightMode;

          if (this.Parent.ColumnReferences.ContainsKey(this.ReportObjectLocation.ColRef))
            left = this.Parent.ColumnReferences[this.ReportObjectLocation.ColRef] * scale + this.LeftEdge * scale;
          else
          {
            // this probably needs to be looking at a "width control" not "heightMode"
            if (heightMode == HeightMode.Relative)
              left = this.ReportObjectLocation.Left * scale + this.LeftEdge * scale;
            else
              left = this.RDef.LeftMargin * scale + this.ReportObjectLocation.Left * scale;
          }

          if (heightMode == HeightMode.Relative)
            top = this.ReportObjectLocation.Top * scale + this.Parent.CurrY * scale;
          else
            top = this.RDef.TopMargin * scale + this.ReportObjectLocation.Top * scale;
        }
        else
        {
          top = this.Parent.CurrY;
        }
      }

      if (this.ReportObjectSize != null)
      {
        width = this.ReportObjectSize.Width * scale;
        height = this.ReportObjectSize.Height * scale;
      }

      float holdFontSize = 10.0F;
      string holdText = String.Empty;

      if (this.ReportText != null)
      {
        holdFontSize = this.ReportText.FontSize;
        holdText = this.ReportText.Text;
      }

			float textLeft = 0F;
			SizeF szText = new SizeF(0.0F, 0.0F);

      try
      {
        switch (roType)
        {
          case ReportObjectType.Rectangle:
            this.ActualLocation = new PointF(left, top);
            this.ActualSize = new SizeF(width, height);
            this.CurrY = top; 
            this.Parent.CurrY += height;
            break;

          case ReportObjectType.ReportText:
            this.ActualLocation = new PointF(left, top);
            this.ActualSize = new SizeF(width, height); 
            string text = this.ReportText.Text;

            if (text.Length > 0)
            {
              float availableWidth = this.RDef.PageWidth - (left + 3 + this.RDef.RightMargin + 3);
              float availableHeight = this.RDef.PageHeight - (top + 3 + this.RDef.BottomMargin + 3);
              Font f = new Font(this.ReportText.FontName, this.ReportText.FontSize * scale, this.ReportText.FontStyle.ToFontStyle());
              RectangleF textRect = new RectangleF(left + 3, top + 3, availableWidth, availableHeight);

              switch (this.ReportText.HorzAlign)
              {
                case HorizAlign.Left:
                  if (this.ReportText.WrapText)
                  {
                    szText = gr.MeasureString(text, f, Convert.ToInt32(availableWidth));
                    this.ActualLocation = new PointF(textRect.X, textRect.Y);
                    this.ActualSize = new SizeF(textRect.Width, textRect.Height); 

                    if (this.ParentReportObject != null)
                    {
                      if (this.ParentReportObject.ReportObjectSize.HeightSizeMode == HeightSizeMode.Grow)
                      {
                        if (textRect.Top + szText.Height > this.RDef.NextY)  // not sure if this is correct - clarify the intent and test (nextY is the prior Y? at this point)
                        {
                          this.RDef.NextY = textRect.Top + szText.Height + 10;
                          if (this.ParentReportObject.ReportObjectSize.Height < szText.Height)
                            this.ParentReportObject.ReportObjectSize.Height = szText.Height + 10;
                        }
                      }
                      else
                      {
                        // What if its not "grow" (then we only need to draw, not "layout" and we don't do anything here.
                        // This is probably not yet correct.
                      }
                    }
                  }
                  break;

                case HorizAlign.Right:
                  szText = gr.MeasureString(text, f);
                  textLeft = left + this.ReportObjectSize.Width * scale - szText.Width;
                  break;

                case HorizAlign.Center:
                  szText = gr.MeasureString(text, f);
                  textLeft = left + this.ReportObjectSize.Width * scale / 2 - szText.Width / 2;
                  break;
              }
            }
            break;

            case ReportObjectType.PageBorder:
              left = this.RDef.LeftMargin;
              top = this.RDef.TopMargin;
              width = this.RDef.PageWidth - (this.RDef.LeftMargin + this.RDef.RightMargin);
              height = this.RDef.PageHeight - (this.RDef.TopMargin + this.RDef.BottomMargin); 
              this.ActualLocation = new PointF(left, top);
              this.ActualSize = new SizeF(width, height);
              break;
        }

        if (this.PerPage)
          this.RDef.PerPage.Add(this.RDef.PerPage.Count.ToString(), this);
        else
          this.RDef.PrintSet.Add(this.RDef.PrintSet.Count.ToString(), this);

        if (this.RDef.Trace.Length == 0)
        {
          this.RDef.Trace.Append("REPORT TRACE" + g.crlf2);
          this.RDef.Trace.Append("LVL   TYPE                 Name                   X         Y         W         H            RdNextY        RosCurrY" + g.crlf); 
        }

        if (roType == ReportObjectType.Rectangle)
          this.RDef.Trace.Append(g.crlf);

        this.RDef.Trace.Append(this.Level.ToString("00") + "    " + roType.ToString().PadTo(18) + "   " + this.Name.PadTo(18) + " " + 
          left.ToString("0000.000") + "  " + top.ToString("0000.000") + "  " + width.ToString("0000.000") + "  " + height.ToString("0000.000") + "        " +
          this.RDef.NextY.ToString("0000.000") + "        " + this.Parent.CurrY.ToString("0000.000") + "  " + 
          holdText.TrimToMax(30).Replace("\n", String.Empty).Replace("\r", String.Empty) + g.crlf); 


        if (this.ReportObjectSet != null)
        {
          //this.Parent.CurrY = top;
          this.RDef.Trace.Append(this.Level.ToString("00") + "    " + ("ReportObjectSet").PadTo(18) + "   " + String.Empty.PadTo(18) + " " +
              "          " + this.Parent.CurrY.ToString("0000.000") + g.crlf); 

          foreach (ReportObject ro in this.ReportObjectSet.Values)
          {
            ro.LayoutObject(gr, scale);
          }
        }

      }
      catch (System.Exception e)
      {
        MessageBox.Show(e.Message, "Error in ReportObject.DrawObject");
      }

		}

		public void DrawObject(Graphics gr, float scale)
		{
      bool diagMode = this.RDef.DiagMode;
      Font diagFont = this.RDef.DiagFont;

      ReportObjectType roType = this.ReportObjectType;
      string roName = this.Name;

      float left = this.ActualLocation.X;
      float top = this.ActualLocation.Y;
      float width = this.ActualSize.Width;
      float height = this.ActualSize.Height;            

      float holdFontSize = 10.0F;
      string holdText = String.Empty;
      System.Drawing.Brush _fillBrush = Brushes.White;
      System.Drawing.Brush _textBrush = Brushes.Black;

      if (this.ReportText != null)
      {
        holdFontSize = this.ReportText.FontSize;
        holdText = this.ReportText.Text;

        if (this.ReportText.TextColor.IsNotBlank())
          _textBrush = new System.Drawing.SolidBrush(System.Drawing.ColorTranslator.FromHtml(this.ReportText.TextColor));
      }

			float textLeft = 0F;
			SizeF szText = new SizeF(0.0F, 0.0F);

      Color borderColor = Color.Black;
      if (this.BorderColor.IsNotBlank())
        borderColor = System.Drawing.ColorTranslator.FromHtml(this.BorderColor);

      bool hasFillColor = false;
      if (this.FillColor.IsNotBlank())
      {
        hasFillColor = true;
        _fillBrush = new System.Drawing.SolidBrush(System.Drawing.ColorTranslator.FromHtml(this.FillColor));
      }

      try
      {
        switch (roType)
        {
          case ReportObjectType.Rectangle:
            if (hasFillColor)
            {
              gr.FillRectangle(_fillBrush, left + 1, top + 1, width - 1, height - 1);
            }
            if (this.BorderWidth > 0)
            {
              gr.DrawRectangle(new Pen(borderColor, this.BorderWidth), left, top, width, height);
            }

            if (diagMode)
            {
              gr.DrawString(roName + " [" + this.Level.ToString() + "] " + left.ToString() + "  " + top.ToString() + "  " + width.ToString() + "  " + height.ToString(), diagFont, Brushes.Black, new PointF(left + 2, top + 2)); 
            }
            break;

          case ReportObjectType.ReportText:
            string text = this.ReportText.Text;

            if (text.Length > 0)
            {
              float availableWidth = this.RDef.PageWidth - (left + 3 + this.RDef.RightMargin + 3);
              float availableHeight = this.RDef.PageHeight - (top + 3 + this.RDef.BottomMargin + 3);
              Font f = new Font(this.ReportText.FontName, this.ReportText.FontSize * scale, this.ReportText.FontStyle.ToFontStyle());
              RectangleF textRect = new RectangleF(left + 3, top + 3, availableWidth, availableHeight); 
                            
              float fontHeight = f.GetHeight();
              szText = gr.MeasureString(text, f, Convert.ToInt32(availableWidth));
              bool multiLine = false;
              if (szText.Height > fontHeight * 1.4)
                multiLine = true;

              switch (this.ReportText.HorzAlign)
              {
                case HorizAlign.Right:
                  szText = gr.MeasureString(text, f);
                  textLeft = left + this.ReportObjectSize.Width * scale - szText.Width;
                  gr.DrawString(text, f, _textBrush, textLeft, top + 3);
                  break;

                case HorizAlign.Center:
                  szText = gr.MeasureString(text, f);
                  textLeft = left + this.ReportObjectSize.Width * scale / 2 - szText.Width / 2;
                  gr.DrawString(text, f, _textBrush, textLeft, top + 3);
                  break;

                default:
                  if (this.ReportText.WrapText)
                  {
                    gr.DrawString(text, f, _textBrush, textRect);
                    if (this.ParentReportObject != null)
                    {
                      if (this.ParentReportObject.ReportObjectSize.HeightSizeMode == HeightSizeMode.Grow)
                      {
                        if (textRect.Top + szText.Height > this.RDef.NextY)
                        {
                          this.RDef.NextY = textRect.Top + szText.Height + 10;
                          if (this.ParentReportObject.ReportObjectSize.Height < szText.Height)
                            this.ParentReportObject.ReportObjectSize.Height = szText.Height + 10; 
                        }
                      }
                    }
                  }
                  else
                  {
                    if (multiLine)
                    {
                      textRect.Height = fontHeight;
                      gr.DrawString(text, f, _textBrush, textRect, new StringFormat() { Trimming = StringTrimming.EllipsisCharacter } );
                    }
                    else
                    {
                      gr.DrawString(text, f, _textBrush, left + 3, top + 3);
                    }
                  }
                  break;
              }
            }

            if (this.BorderWidth > 0)
            {
              if (left > 0 && top > 0 && width > 0 && height > 0)
                gr.DrawRectangle(new Pen(borderColor, this.BorderWidth), left, top, width, height);
            }
            break;

          case ReportObjectType.PageBorder:
            if (hasFillColor)
            {
              gr.FillRectangle(_fillBrush, left + 1, top + 1, width - 1, height - 1);
            }
            if (this.BorderWidth > 0)
            {
              gr.DrawRectangle(new Pen(borderColor, this.BorderWidth), left, top, width, height);
            }
            break;

          default:
            break;
        }
      }
      catch (System.Exception e)
      {
        MessageBox.Show(e.Message, "Error in ReportObject.DrawObject");
      }

		}

    public void SetReferences()
    {
      this.ColumnReferences = null;
      if (this.Parent != null)
      {
        if (this.Parent.Parent != null)
          if (this.Parent.Parent.ColumnReferences != null)
            this.ColumnReferences = this.Parent.Parent.ColumnReferences;
      }

      if (this.ReportColumns != null)
      {
        if (this.ColumnReferences == null)
          this.ColumnReferences = new Dictionary<string, int>();

        foreach (ReportColumn rc in this.ReportColumns.Values)
        {
          string colId = rc.ColumnId;
          int colPos = rc.ColumnPos;

          if (this.ColumnReferences.ContainsKey(colId))
            this.ColumnReferences[colId] = colPos;
          else
          {
            this.ColumnReferences.Add(colId, colPos);
          }
        }
      }

      if (this.ReportObjectSet == null)
          return;

      this.ReportObjectSet.Parent = this;
      this.ReportObjectSet.Level = this.Level + 1;
      this.ReportObjectSet.RDef = this.RDef;
      this.ReportObjectSet.SetReferences();
    }

    private ReportObject Get_ParentReportObject()
    {
      if (this.Parent == null)
        return null;

      if (this.Parent.Parent == null)
        return null;

      return this.Parent.Parent;
    }
  }
}
