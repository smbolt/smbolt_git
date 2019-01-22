using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business
{
  [XMap(XType = XType.Element)]
  public class ColumnMap
  {
    [XMap(IsKey = true)]
    public string Name { get; set; }

    [XMap]
    public int Index { get; set; }

    [XMap(DefaultValue = "False")]
    public bool IsRequired { get; set; }

    [XMap(DefaultValue = "String")]
    public DataType DataType { get; set; }
  }
}
