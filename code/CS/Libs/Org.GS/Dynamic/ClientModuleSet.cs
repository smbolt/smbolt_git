using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Org.GS;

namespace Org.GS.Dynamic
{
  public class ClientModuleSet : Dictionary<string, ClientModule>
  {
    public string MainModule { get; set; }

    public ClientModuleSet()
    {
      this.MainModule = String.Empty;
    }

    public XElement GetXml()
    {
      XElement xml = new XElement("ClientModuleSet");
      xml.Add(new XAttribute("MainModule", this.MainModule));

      foreach (KeyValuePair<string, ClientModule> clientMods in this)
      {
        xml.Add(clientMods.Value.GetXml());
      }

      return xml;
    }

    public void LoadFromXml(XElement xElement)
    {
      this.Clear();

      this.MainModule = xElement.GetAttributeValue("MainModule");

      IEnumerable<XElement> clientModules = xElement.Elements("ClientModule");  
      foreach (XElement clientModule in clientModules)
      {
        ClientModule cm = new ClientModule();
        cm.LoadFromXml(clientModule);
        string key = cm.ModuleName + ":" + cm.CurrentVersion;
        if (!this.ContainsKey(key))
          this.Add(key, cm);
      }
    }
  }
}
