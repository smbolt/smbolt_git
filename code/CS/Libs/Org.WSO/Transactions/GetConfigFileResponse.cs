using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.GS;

namespace Org.WSO.Transactions
{
  [XMap(XType = XType.Element)]
  public class GetConfigFileResponse : TransactionBase
  {
    [XMap(XType=XType.Element)]
    public string ConfigFile {
      get;
      set;
    }

    public GetConfigFileResponse()
    {
      this.ConfigFile = String.Empty;
    }
  }
}
