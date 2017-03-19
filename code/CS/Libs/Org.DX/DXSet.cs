using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DX
{
  [XMap(XType = XType.Element, CollectionElements = "DX")]
  public class DXSet : Dictionary<string, DX>
  {
  }
}
