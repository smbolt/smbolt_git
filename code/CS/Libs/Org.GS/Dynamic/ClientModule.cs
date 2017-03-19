using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using Org.GS;

namespace Org.GS.Dynamic
{
  public class ClientModule
  {
    public string ModuleName { get; set; }
    public string CurrentVersion { get; set; }
    public List<string> Versions;

    public ClientModule()
    {
      this.ModuleName = string.Empty;
      this.CurrentVersion = string.Empty;
      this.Versions = new List<string>();
    }

    public XElement GetXml()
    {
      XElement xml = new XElement("ClientModule");
      xml.Add(new XAttribute("ModuleName", this.ModuleName.ToString()));
      xml.Add(new XAttribute("CurrentVersion", this.CurrentVersion.ToString()));

      XElement versions = new XElement("Versions");
      foreach (string version in this.Versions)
        versions.Add(new XElement("Version", version));
      xml.Add(versions);

      return xml;
    }

    public void LoadFromXml(XElement xElement)
    {
      this.ModuleName = (string) xElement.GetRequiredElementValue("ModuleName");
      this.CurrentVersion = (string)xElement.GetRequiredElementValue("CurrentVersion");

      IEnumerable<XElement> versions = xElement.Element("Versions").Elements("Version");

      foreach (XElement version in versions)
        this.Versions.Add(version.Value);
    }
  }
}
