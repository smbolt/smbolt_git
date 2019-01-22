using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "tblCellMar", Abbr = "TCMgn")]
  public class TableCellMargin : DocumentElement
  {
    [Meta(XMatch = true)]
    public Margin TopMargin {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public Margin BottomMargin {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public Margin TableCellLeftMargin {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public Margin TableCellRightMargin {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public Margin StartMargin {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public Margin EndMargin {
      get;
      set;
    }
    public List<Margin> UsedMargins
    {
      get {
        return GetUsedMargins();
      }
    }

    public TableCellMargin() { }

    public TableCellMargin(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
        return;

      XElement topMarginElement = xml.GetElement("top");
      if (topMarginElement != null)
        this.TopMargin = new Margin(topMarginElement, doc, this);

      XElement bottomMarginElement = xml.GetElement("bottom");
      if (bottomMarginElement != null)
        this.BottomMargin = new Margin(bottomMarginElement, doc, this);

      XElement leftMarginElement = xml.GetElement("left");
      if (leftMarginElement != null)
        this.TableCellLeftMargin = new Margin(leftMarginElement, doc, this);

      XElement rightMarginElement = xml.GetElement("right");
      if (rightMarginElement != null)
        this.TableCellRightMargin = new Margin(rightMarginElement, doc, this);

      XElement startMarginElement = xml.GetElement("start");
      if (startMarginElement != null)
        this.StartMargin = new Margin(startMarginElement, doc, this);

      XElement endMarginElement = xml.GetElement("end");
      if (endMarginElement != null)
        this.EndMargin = new Margin(endMarginElement, doc, this);
    }

    private PageLayout ComputeArea()
    {

      return new PageLayout();
    }

    public List<Margin> GetUsedMargins()
    {
      List<Margin> usedMargins = new List<Margin>();

      if (this.TopMargin != null)
        usedMargins.Add(this.TopMargin);

      if (this.TableCellLeftMargin != null)
        usedMargins.Add(this.TableCellLeftMargin);

      if (this.BottomMargin != null)
        usedMargins.Add(this.BottomMargin);

      if (this.TableCellRightMargin != null)
        usedMargins.Add(this.TableCellRightMargin);

      if (this.StartMargin != null)
        usedMargins.Add(this.StartMargin);

      if (this.EndMargin != null)
        usedMargins.Add(this.EndMargin);

      return usedMargins;
    }
  }
}
