using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.WSO.Transactions
{
  [XMap(XType = XType.Element)]
  public class GetWebSiteListResponse : TransactionBase
  {
    [XMap(XType = XType.Element, CollectionElements = "WebSite", WrapperElement = "WebSiteList")]
    public WebSiteSet WebSiteSet { get; set; }

    public GetWebSiteListResponse()
    {
    }
  }
}
