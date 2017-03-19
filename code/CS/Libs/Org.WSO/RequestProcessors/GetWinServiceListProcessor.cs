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
  public class GetWinServiceListProcessor : RequestProcessorBase, IRequestProcessor
  {
    public override XElement ProcessRequest()
    {
      var serviceManager = new ServiceManager();

      base.Initialize(MethodBase.GetCurrentMethod());

      GetWinServiceListResponse getWinServiceListResponse = new GetWinServiceListResponse();

      using (var sm = new ServiceManager())
      {
        getWinServiceListResponse.WinServiceSet = sm.GetWinServices();
      }

      getWinServiceListResponse.TransactionStatus = TransactionStatus.Success;
      //base.WriteSuccessLog("0000", "000");
      ObjectFactory2 f = new ObjectFactory2();
      XElement response = f.Serialize(getWinServiceListResponse);
      return response;
    }
  }
}
