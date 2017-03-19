using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.Controls
{
  [XMap(XType=XType.Element, CollectionElements="ControlSpec")]
  public class ControlSpecSet : Dictionary<string, ControlSpecBase>
  {
  }
}
