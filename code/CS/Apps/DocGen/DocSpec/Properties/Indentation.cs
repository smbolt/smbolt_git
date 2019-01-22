using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.DocGen.DocSpec
{
  [Meta(OxName = "ind", Abbr = "Ind", IsAttribute = true, AutoMap = true)]
  public class Indentation : DocumentElement
  {
    [Meta(XMatch = true)]
    public string Start {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public int? StartCharacters {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public string End {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public int? EndCharacters {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public string Hanging {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public int? HangingCharacters {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public string FirstLine {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public int? FirstLineCharacters {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public string Left {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public int? LeftCharacters {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public string Right {
      get;
      set;
    }
    [Meta(XMatch = true)]
    public int? RightCharacters {
      get;
      set;
    }


    public Indentation() { }

    public Indentation(XElement xml, Doc doc, DocumentElement parent)
    {
      base.Initialize(xml, doc, parent);

      if (xml == null)
        return;

      this.Start = xml.GetAttributeValue("start");
      this.StartCharacters = xml.GetInt32AttributeValueOrNull("startChars");
      this.End = xml.GetAttributeValue("end");
      this.EndCharacters = xml.GetInt32AttributeValueOrNull("endChars");
      this.Hanging = xml.GetAttributeValue("hanging");
      this.HangingCharacters = xml.GetInt32AttributeValueOrNull("hangingChars");
      this.FirstLine = xml.GetAttributeValue("firstLine");
      this.FirstLineCharacters = xml.GetInt32AttributeValueOrNull("firstLineChars");
      this.Left = xml.GetAttributeValue("left");
      this.LeftCharacters = xml.GetInt32AttributeValueOrNull("leftChars");
      this.Right = xml.GetAttributeValue("right");
      this.RightCharacters = xml.GetInt32AttributeValueOrNull("right");
    }
  }
}