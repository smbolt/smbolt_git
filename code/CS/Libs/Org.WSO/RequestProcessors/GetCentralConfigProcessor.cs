using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Hosting;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;
using Org.GS;
using Org.GS.Logging;
using Org.WSO;
using Org.WSO.Transactions;

namespace Org.WSO.RequestProcessors
{
  public class GetCentralConfigProcessor : RequestProcessorBase, IRequestProcessor
  {
    public override XElement ProcessRequest()
    {
      base.Initialize(MethodBase.GetCurrentMethod());

      ObjectFactory2 f = new ObjectFactory2();
      GetCentralConfigRequest getCentralConfigRequest = f.Deserialize(base.TransactionEngine.TransactionBody) as GetCentralConfigRequest;

      if (HostingEnvironment.Cache["CentralConfig"] == null)
        PopulateCentralConfiguration();

      GetCentralConfigResponse getCentralConfigResponse = new GetCentralConfigResponse();

      XElement centralConfig = (XElement)HostingEnvironment.Cache["CentralConfig"];

      if (centralConfig.Element("GlobalConfigs") != null)
        getCentralConfigResponse.GlobalConfigs = centralConfig.Element("GlobalConfigs");
      else
        getCentralConfigResponse.GlobalConfigs = new XElement("GlobalConfigs");

      if (centralConfig.Element("AppSpecificConfigs") != null)
      {
        if (centralConfig.Element("AppSpecificConfigs").Element(getCentralConfigRequest.AppName) != null)
          getCentralConfigResponse.AppSpecificConfigs = centralConfig.Element("AppSpecificConfigs").Element(getCentralConfigRequest.AppName);
        else
          getCentralConfigResponse.AppSpecificConfigs = new XElement(getCentralConfigRequest.AppName);
      }

      getCentralConfigResponse.TransactionStatus = TransactionStatus.Success;

      XElement response = f.Serialize(getCentralConfigResponse);
      return response;
    }

    private void PopulateCentralConfiguration()
    {
      if (!g.AppConfig.ContainsKey("DB"))
        throw new Exception("Configuration entry 'DB' not found in ApplicationConfiguration - central configuraton load failed.");

      string folder = g.AppConfig.GetCI("DB");
      if (!Directory.Exists(folder))
        throw new Exception("Folder does not exist '" + folder + "' cannot load central configuration data into HostingEnvironment.Cache.");

      string centralConfigPath = folder + @"\CentralConfig.xml";
      if (!File.Exists(centralConfigPath))
        throw new Exception("Path '" + centralConfigPath + "' does not exist - cannot load central configuration data into HostingEnvironment.Cache.");

      XElement centralConfiguration = XElement.Parse(File.ReadAllText(centralConfigPath));

      HostingEnvironment.Cache["CentralConfig"] = centralConfiguration;

      base.ServiceBase.Logger.Log("Population of CentralConfig successful from Org '" + base.TransactionEngine.MessageHeader.OrgId.ToString() + "' from Application:"
                                  + base.TransactionEngine.MessageHeader.AppName + " Version:" + base.TransactionEngine.MessageHeader.AppVersion + "'.", 0);
    }

  }
}
