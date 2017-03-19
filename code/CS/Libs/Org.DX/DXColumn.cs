using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.DX
{
  [XMap(XType = XType.Element, KeyName="Name")]
  public class DXColumn
  {
    [XMap(IsKey = true)]
    public string Name { get; set; }

    [XMap]
    public string Tag { get; set; }

    [XMap]
    public HorizontalAlignment HorizontalAlignment { get; set; }

    public DXColumn()
    {
      this.Name = String.Empty;
      this.Tag = String.Empty;
    }

    public void AutoInit()
    {
      if (this.Tag.IsBlank())
        this.Tag = this.Name;
    }
  }
}
