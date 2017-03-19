using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.GS;

namespace Org.RPT
{
  [XMap(XType = XType.Element, CollectionElements = "ReportColumn", KeyName = "ColumnId")]
  public class ReportColumns : Dictionary<string, ReportColumn>
  {
  }
}
