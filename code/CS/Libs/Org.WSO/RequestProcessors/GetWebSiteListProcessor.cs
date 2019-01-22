using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Linq;
using Org.GS;
using Org.WSO.Transactions;

namespace Org.WSO
{
  public class GetWebSiteListProcessor : RequestProcessorBase, IRequestProcessor
  {
    public override XElement ProcessRequest()
    {
      base.Initialize(MethodBase.GetCurrentMethod());

      GetWebSiteListResponse getWebSiteListResponse = new GetWebSiteListResponse();

      using (var sm = new SiteManager())
      {
        getWebSiteListResponse.WebSiteSet = sm.GetWebSites();
      }

      getWebSiteListResponse.TransactionStatus = TransactionStatus.Success;
      //base.WriteSuccessLog("0000", "000");
      ObjectFactory2 f = new ObjectFactory2();
      XElement response = f.Serialize(getWebSiteListResponse);
      return response;
    }
  }
}
