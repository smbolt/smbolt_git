using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business
{
  [XMap (XType = XType.Element, CollectionElements = "DxRegionRow", WrapperElement = "DxRegionRowSet")]
  public class DxRegionRowSet : Dictionary<string, DxRegionRow>
  {
  }
}
