using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.TsoControls.PanelDefs
{
  [XMap(XType=XType.Element, CollectionElements = "PanelLine", WrapperElement = "PanelLineElementSet")]
  public class PanelLineElementSet : List<PanelLineElement>
  {
  }
}
