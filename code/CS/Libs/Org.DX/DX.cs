using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DX
{
  [XMap(XType = XType.Element, KeyName="Name")]
  public class DX
  {
    [XMap(IsKey = true)]
    public string Name { get; set; }

    [XMap(XType = XType.Element, CollectionElements = "DXColumn")]
    public DXColumnSet DXColumnSet { get; set; }
    
    public DX()
    {
      this.Name = String.Empty;
    }
  }
}
