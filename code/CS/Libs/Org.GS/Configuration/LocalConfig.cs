using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.Configuration
{
  public class LocalConfig
  {
    private XElement lcElement;
    private Dictionary<string, string> lc;

    private bool _isLoaded = false;
    public bool IsLoaded { get { return _isLoaded; } }

    public LocalConfig()
    {
      try
      {
        g.ExecutablePath = g.GetAppPath();
        g.AppInfo.MainExecutablePath = g.ExecutablePath;

        switch (g.AppInfo.OrgApplicationType)
        {
          case ApplicationType.WebSite:
          case ApplicationType.WcfService:
          case ApplicationType.WebApi:
            g.ExecutablePath = System.Web.Hosting.HostingEnvironment.MapPath("~");
            break;

          case ApplicationType.WinApp:
          case ApplicationType.WinService:
            g.ExecutablePath = g.GetAppPath();
            break;

            case ApplicationType.PlugInModule:
            case ApplicationType.MEFModule:
            case ApplicationType.OrgGS:
              if (g.AppInfo.RunningInNonDefaultAppDomain)
                g.ExecutablePath = g.AppInfo.ModulePath;
              break;

          default:
            break;
        }

        g.LocalConfigPath = g.ExecutablePath.RemoveTrailingSlash() + @"\LocalConfig.xml";

        if (g.AlternateAppConfigPath.IsNotBlank())
          g.LocalConfigPath = g.AlternateAppConfigPath + @"\LocalConfig.xml";

        string encryptedLocalConfigPath = g.LocalConfigPath + "x";

        if (!File.Exists(g.LocalConfigPath) && !File.Exists(encryptedLocalConfigPath))
        {
          CreateDefaultLocalConfig(g.LocalConfigPath);
        }

        if (File.Exists(g.LocalConfigPath))
          File.WriteAllText(encryptedLocalConfigPath, TokenMaker.GenerateToken(File.ReadAllText(g.LocalConfigPath)));

        if (!File.Exists(encryptedLocalConfigPath))
          throw new Exception("LocalConfig.xmlx file does not exist at '" + encryptedLocalConfigPath + "'.");

        lcElement = XElement.Parse(TokenMaker.DecodeToken(File.ReadAllText(encryptedLocalConfigPath)));
        LoadDictionary();

        if (this.ContainsKey("UseLocalAppData"))
          g.UseLocalAppData = this.GetBoolean("UseLocalAppData");

        if (this.ContainsKey("UseStartupLogging"))
          g.UseStartupLogging = this.GetBoolean("UseStartupLogging"); 

        _isLoaded = true;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the constructor of the LocalConfig object - AppDomain name is '" + AppDomain.CurrentDomain.FriendlyName + "'.", ex); 
      }
    }

    public LocalConfig(string localConfigPath)
    {
      string unencryptedLocalConfigPath = localConfigPath + @"\LocalConfig.xml";
      string encryptedLocalConfigPath = unencryptedLocalConfigPath + "x";

      if (File.Exists(unencryptedLocalConfigPath))
        File.WriteAllText(encryptedLocalConfigPath, TokenMaker.GenerateToken(File.ReadAllText(unencryptedLocalConfigPath)));

      if (!File.Exists(encryptedLocalConfigPath))
        throw new Exception("LocalConfig.xmlx file does not exist at '" + encryptedLocalConfigPath + "'.");

      lcElement = XElement.Parse(TokenMaker.DecodeToken(File.ReadAllText(encryptedLocalConfigPath)));
      LoadDictionary();

      _isLoaded = true;
    }

    private void CreateDefaultLocalConfig(string localConfigPath)
    {
      XElement lc = new XElement("LocalConfig");
      lc.Add(new XElement("CI", new XAttribute("K", "Vendor"), new XAttribute("V", g.AppInfo.Vendor)));
      lc.Add(new XElement("CI", new XAttribute("K", "AppType"), new XAttribute("V", g.AppInfo.OrgApplicationType.ToString())));
      lc.Add(new XElement("CI", new XAttribute("K", "AppName"), new XAttribute("V", g.AppInfo.AppName)));
      lc.Add(new XElement("CI", new XAttribute("K", "UseLocalAppData"), new XAttribute("V", "True")));
      lc.Add(new XElement("CI", new XAttribute("K", "UseStartupLogging"), new XAttribute("V", "True")));
      lc.Add(new XElement("CI", new XAttribute("K", "UseInitialConfig"), new XAttribute("V", "False")));
      lc.Add(new XElement("CI", new XAttribute("K", "AttemptConfigImport"), new XAttribute("V", "False")));
      string lcString = lc.ToString();
      File.WriteAllText(localConfigPath, lcString);
    }

    private void LoadDictionary()
    {
      lc = new Dictionary<string,string>();

      IEnumerable<XElement> ciElements = lcElement.Elements("CI");
      foreach (XElement ciElement in ciElements)
      {
        string k = ciElement.Attribute("K").Value;
        string v = ciElement.Attribute("V").Value;

        if (lc.ContainsKey(k))
          throw new Exception("Duplicate key '" + k + "' found in LocalConfig file.");

        lc.Add(k, v); 
      }
    }

    public string GetCI(string key)
    {
      if (lc.ContainsKey(key))
        return lc[key];

      return String.Empty;
    }

    public void AddCI(string key, string value)
    {
      if (lcElement == null)
        return;

      XElement ciElement = XmlHelper.GetElementByAttributeValue(lcElement, "CI", "K", key);
      if (ciElement == null)
        lcElement.Add(new XElement("CI", new XAttribute("K", key), new XAttribute("V", value)));
      else
        ciElement.Attribute("V").Value = value;

      if (!lc.ContainsKey(key))
        lc.Add(key, value); 
    }

    public bool GetBoolean(string key)
    {
      if (!lc.ContainsKey(key))
        return false;

      return Boolean.Parse(lc[key]); 
    }

    public int GetInteger(string key)
    {
      if (!lc.ContainsKey(key))
        return 0;

      return Int32.Parse(lc[key]); 
    }

    public bool ContainsKey(string key)
    {
      return lc.ContainsKey(key); 
    }

    public string GetXml()
    {
      if (lc == null)
        return String.Empty;

      string lcXml =  this.lcElement.ToString();

      return lcXml;
    }

    public void Save()
    {
      string xml = this.GetXml();

      string plainTextPath = g.LocalConfigPath;
      string obfuscatedPath = plainTextPath + "x";

      if (File.Exists(plainTextPath))
        File.WriteAllText(plainTextPath, xml);

      if (File.Exists(obfuscatedPath))
      {
        string encodedXml = TokenMaker.GenerateToken(xml);
        File.WriteAllText(obfuscatedPath, encodedXml);
      }
    }
  }
}
