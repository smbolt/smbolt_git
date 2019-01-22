using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using Org.GS;

namespace Org.WSO.Transactions
{
  [XMap(XType = XType.Element)]
  public class GetCentralConfigResponse : TransactionBase
  {
    [XMap(XType = XType.Element)]
    public XElement GlobalConfigs {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public XElement AppSpecificConfigs {
      get;
      set;
    }

    public GetCentralConfigResponse()
    {
      this.GlobalConfigs = new XElement("GlobalConfigs");
      this.AppSpecificConfigs = new XElement("AppSpecificConfigs");
    }
  }
}
