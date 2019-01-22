using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Terminal.Screen
{
  [XMap(XType = XType.Element, KeyName = "Name")]
  public class ScreenSpec
  {
    [XMap(IsKey = true)]
    public string Name { get; set; }

    [XMap]
    public int Rows { get; set; }

    [XMap]
    public int Columns { get; set; }

    [XMap]
    public bool FixedWidth { get; set; }

    [XMap]
    public bool FixedHeight { get; set; }

    [XMap(XType = XType.Element, CollectionElements = "FieldSpec", WrapperElement = "FieldSpecSet")]
    public FieldSpecSet FieldSpecSet { get; set; }

    public ScreenSpec()
    {
      this.Name = String.Empty;
      this.FieldSpecSet = new FieldSpecSet();
    }
  }
}
