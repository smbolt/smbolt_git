using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.Dx.Business
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements = "DxWorksheet", XType = XType.Element)] 
  public class DxWorkbook : Dictionary<string, DxWorksheet>
  {
    [XMap]
    public string FilePath { get; set; }
  }
}
