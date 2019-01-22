using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DocGen.DocSpec
{
  public class Match
  {
    public string PropertyName {
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
    public bool MapAsAttribute {
      get;
      set;
    }

    public Match(string propertyName, string altName, string abbr, bool mapAsAttribute)
    {
      this.PropertyName = propertyName;
      this.AltName = altName;
      this.Abbr = abbr;
      this.MapAsAttribute = mapAsAttribute;
    }
  }
}
