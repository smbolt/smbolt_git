using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.GS;

namespace Org.RPT
{
  [XMap(XType = XType.Element, KeyName = "ColumnId")]
  public class ReportColumn
  {
    [XMap(IsKey = true, IsRequired = true)]
    public string ColumnId { get; set; }

    [XMap(IsRequired = true)]
    public int ColumnPos { get; set; }

    public ReportColumn()
    {
      this.ColumnId = String.Empty;
      this.ColumnPos = 0; 
    }
  }
}
