using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Terminal.Screen
{
  [XMap(XType = XType.Element, CollectionElements = "ScreenSpec")]
  public class ScreenSpecSet : Dictionary<string, ScreenSpec>
  {
    [XMap(IsKey = true)]
    public string Name { get; set; }

    public ScreenSpecSet()
    {
      this.Name = String.Empty;
    }
  }
}
