using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.TsoControls.PanelDefs
{
  [XMap(XType=XType.Element, CollectionElements = "PanelDef", WrapperElement = "PanelDefSet")]
  public class PanelDefSet : Dictionary<string, PanelDef>
  {
  }
}

