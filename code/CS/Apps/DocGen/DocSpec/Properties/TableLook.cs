using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "tblLook", AutoMap = true, Abbr = "TblLk")]
  public class TableLook : DocumentElement
  {
    [Meta(XMatch = true)]
    public bool FirstRow {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public bool LastRow {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public bool FirstColumn {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public bool LastColumn {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public bool NoHorizontalBand {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public bool NoVerticalBand {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public string Val {
      get;
      set;
    }

    public TableLook() {}

    public TableLook(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
      {
        this.FirstRow = false;
        this.LastRow = false;
        this.FirstColumn = false;
        this.LastColumn = false;
        this.NoHorizontalBand = false;
        this.NoVerticalBand = false;
        this.Val = String.Empty;
        return;
      }

      this.FirstRow = xml.GetBooleanAttribute("firstRow");
      this.LastRow = xml.GetBooleanAttribute("lastRow");
      this.FirstColumn = xml.GetBooleanAttribute("firstColumn");
      this.LastColumn = xml.GetBooleanAttribute("lastColumn");
      this.NoHorizontalBand = xml.GetBooleanAttribute("noHBand");
      this.NoVerticalBand = xml.GetBooleanAttribute("noVBand");
      this.Val = xml.GetRequiredAttributeValue("val");
    }
  }
}
