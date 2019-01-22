using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.TsoControls.PanelDefs
{
  [XMap(XType=XType.Element, CollectionElements = "PanelLineElement")]
  public class PanelLine : List<PanelLineElement>
  {
    [XMap(IsKey = true)]
    public int LineNumber {
      get;
      set;
    }

    [XMap]
    public string SetName {
      get;
      set;
    }

    [XMap(DefaultValue = "1")]
    public int Repeat {
      get;
      set;
    }

    public PanelLine()
    {
      this.LineNumber = 0;
      this.SetName = String.Empty;
      this.Repeat = 1;
    }
  }
}
