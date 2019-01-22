using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.Configuration
{
  [XMap(XType = XType.Element, CollectionElements = "GridView")]
  public class GridViewSet : Dictionary<string, GridView>
  {
    [XMap]
    public string DefaultViewName {
      get;
      set;
    }

    public GridViewSet()
    {
      this.DefaultViewName = String.Empty;
    }
  }
}
