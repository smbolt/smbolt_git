using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.GS;

namespace Org.RPT
{
  [XMap(XType = XType.Element, CollectionElements = "ReportDef")]
  public class ReportDefs : Dictionary<string, ReportDef>
  {
    public ReportDefs()
    {
    }

    public void AutoInit()
    {
      foreach (ReportDef r in this.Values)
      {
        r.SetReferences();
      }
    }
  }
}
