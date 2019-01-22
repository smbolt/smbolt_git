using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DocGen.DocSpec
{
  [AttributeUsage(AttributeTargets.Class|AttributeTargets.Property)]
  public class Meta : Attribute
  {
    public string OxName {
      get;
      set;
    }
    public string AltName {
      get;
      set;
    }
    public string Abbr {
      get;
      set;
    }
    public bool AutoMap {
      get;
      set;
    }
    public bool XMatch {
      get;
      set;
    }
    public bool IsAttribute {
      get;
      set;
    }
    public string ParentSet {
      get;
      set;
    }
    public string ChildOfSet {
      get;
      set;
    }

    public Meta()
    {
      this.OxName = String.Empty;
      this.AltName = String.Empty;
      this.Abbr = String.Empty;
      this.AutoMap = false;
      this.XMatch = false;
      this.IsAttribute = false;
      this.ParentSet = String.Empty;
      this.ChildOfSet = String.Empty;
    }
  }


  [AttributeUsage(AttributeTargets.Field)]
  public class EnumString : Attribute
  {
    public string Value {
      get;
      set;
    }

    public EnumString(string value)
    {
      this.Value = value;
    }
  }

}
