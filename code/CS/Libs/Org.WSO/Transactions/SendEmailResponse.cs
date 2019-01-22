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
  public class SendEmailResponse : TransactionBase
  {
    public SendEmailResponse()
    {
    }
  }
}
