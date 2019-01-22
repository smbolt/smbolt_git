using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business
{ 
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "DxRegion")]
  public class DxRegionSet : Dictionary<string, DxRegion>
  {
    public int NextStartRowIndex { get; set; }

    public DxRegionSet()
    {
      this.NextStartRowIndex = 0;
    }
  }
}
