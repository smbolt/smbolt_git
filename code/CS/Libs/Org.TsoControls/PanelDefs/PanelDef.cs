using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.TsoControls.PanelDefs
{
  [XMap(XType = XType.Element, CollectionElements = "PanelLine", WrapperElement = "PanelLineSet")]
  public class PanelDef : SortedList<int, PanelLine>
  {
    [XMap(IsKey = true)]
    public string Name {
      get;
      set;
    }

    public PanelDef()
    {
      this.Name = String.Empty;
    }
  }
}
