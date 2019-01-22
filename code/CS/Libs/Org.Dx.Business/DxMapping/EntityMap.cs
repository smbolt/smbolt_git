using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business
{
  [XMap (XType = XType.Element, CollectionElements="ColumnMap")]
  public class EntityMap : Dictionary<string, ColumnMap>
  {
    [XMap (IsKey = true)]
    public string Name {
      get;
      set;
    }
  }
}
