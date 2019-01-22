using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Org.Dx.Business.TextProcessing;
using Org.GS;

namespace Org.Dx.Business
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "DxMapItem")]
  public class DxRegionRow : Dictionary<string, DxMapItem>
  {
    [XMap(IsKey = true)]
    public string Cond { get; set; }

    [XMap (DefaultValue = "DefaultToRegion")]
    public DxRegionExtractMethod DxRegionExtractMethod { get; set; }    

    [XMap]
    public string Include { get; set; }

    [XMap(DefaultValue = "False")]
    public bool Optional { get; set; }

    public CellDefinition RegionRowDefinition { get; set; }

    public DxRegion DxRegion { get; set; }

    public DxRegionRow()
    {
    }
  }
}
