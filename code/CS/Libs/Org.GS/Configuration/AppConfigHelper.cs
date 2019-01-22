using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Org.GS.Configuration;

namespace Org.GS
{
  public static class AppConfigHelper
  {
    private static Encryptor encryptor = new Encryptor();

    public static List<string> GetCentralConfigProfilesSharedFile(string centralConfigPath, ConfigFileType configFileType)
    {
      string fullPath = String.Empty;

      switch (configFileType)
      {
        case ConfigFileType.AppConfig:
          fullPath = centralConfigPath + @"\APPCFG\PROFILES";
          break;
      }

      string[] profileNames = Directory.GetDirectories(fullPath);
      List<string> profiles = new List<string>();
      foreach (string profileName in profileNames)
        profiles.Add(Path.GetFileName(profileName));

      return profiles;
    }

    public static List<string> GetCentralConfigProfilesSharedService(ConfigWsSpec configWsSpec, int orgId, ConfigFileType configFileType)
    {
      List<string> profiles = new List<string>();
      //string endpoint = configWsSpec.WebServiceEndpoint;

      //WsMessage requestMessage = WsMessageFactory.BuildGetProfileListRequestMessage(MessagingParticipant.Sender, GetProfileListCommand.GetProfileList, configFileType);
      //requestMessage.MessageHeader.CustomerID = customerNumber;
      //requestMessage.MessageHeader.AppName = g.AppInfo.AppName;
      //requestMessage.MessageHeader.Version = g.AppInfo.AppVersion;
      //WsMessage responseMessage = SimpleServiceMessaging.SendMessage(requestMessage, endpoint);

      //foreach (XElement profile in responseMessage.TransactionBody.Elements("Profile"))
      //{
      //  profiles.Add(profile.Attribute("Name").Value.Trim());
      //}

      return profiles;
    }

    public static string GetCentralAppConfigFile(AppConfig c, string centralAppConfigProfile)
    {
      string centralConfigPath = g.RemoveTrailingSlash(c.GetCI("CentralConfigPath"));
      string fullPath = centralConfigPath + @"\APPCFG\PROFILES\" + centralAppConfigProfile + @"\CentralConfig.xmlx";
      string unencryptedFullPath = fullPath.Replace(".xmlx", ".xml");

      if (File.Exists(unencryptedFullPath))
        File.WriteAllText(fullPath, encryptor.EncryptString(File.ReadAllText(unencryptedFullPath)));

      if (!File.Exists(fullPath))
        return String.Empty;

      string encryptedXml = File.ReadAllText(fullPath);
      string unencryptedXml = encryptor.DecryptString(encryptedXml);
      return unencryptedXml;
    }

    public static AppConfig GetCentralAppConfig(ConfigurationType configurationType, ConfigWsSpec configWsSpec, string centralAppConfigPath, string centralAppConfigProfile)
    {
      AppConfig centralAppConfig = new AppConfig(g.AppInfo.ConfigName);

      XElement centralAppConfigXml = GetCentralAppConfigXml(configurationType, configWsSpec, centralAppConfigPath, centralAppConfigProfile);
      string cacheConfigPath = g.AppDataPath + @"\AppConfig\Cache";
      if (!Directory.Exists(cacheConfigPath))
        Directory.CreateDirectory(cacheConfigPath);

      if (centralAppConfigXml.Elements().Count() == 0)
      {
        string cachedCentralConfigFile = Directory.GetFiles(cacheConfigPath, "*.ccfx").SingleOrDefault();
        if (cachedCentralConfigFile != null)
        {
          if (File.Exists(cachedCentralConfigFile))
          {
            string xml = encryptor.DecryptString(File.ReadAllText(cachedCentralConfigFile));
            centralAppConfigXml = XElement.Parse(xml);
            throw new Exception(g.AppInfo.AppName + " was unable to reach the central configuration web service at '" + configWsSpec.WebServiceEndpoint + "'." +
                                g.crlf2 + "Cached central configuration elements will be loaded.");
          }
        }
      }
      else
      {
        string[] filesToClear = Directory.GetFiles(cacheConfigPath, "*.ccfx");
        foreach (string fileToClear in filesToClear)
          File.Delete(fileToClear);
        File.WriteAllText(cacheConfigPath + @"\Central.ccfx", encryptor.EncryptString(centralAppConfigXml.ToString()));
      }

      //centralAppConfig.LoadFromXml(centralAppConfigXml);
      return centralAppConfig;
    }

    public static AppConfig GetCentralAppConfigXmlFromCache()
    {
      AppConfig appConfig = new AppConfig(g.AppInfo.ConfigName);
      XElement centralAppConfig = new XElement("ApplicationConfiguration");
      //appConfig.LoadFromXml(centralAppConfig);

      string cacheConfigPath = g.AppDataPath + @"\AppConfig\Cache";
      if (!Directory.Exists(cacheConfigPath))
        Directory.CreateDirectory(cacheConfigPath);

      string cachedCentralConfigFile = Directory.GetFiles(cacheConfigPath, "*.ccfx").SingleOrDefault();
      if (cachedCentralConfigFile != null)
      {
        if (File.Exists(cachedCentralConfigFile))
        {
          string xml = encryptor.DecryptString(File.ReadAllText(cachedCentralConfigFile));
          centralAppConfig = XElement.Parse(xml);
          appConfig = new AppConfig(g.AppInfo.ConfigName);
          //appConfig.LoadFromXml(centralAppConfig);
        }
      }

      return appConfig;
    }

    public static XElement GetCentralAppConfigXml(ConfigurationType configurationType, ConfigWsSpec configWsSpec, string centralAppConfigPath, string centralAppConfigProfile)
    {
      XElement centralAppConfigXml = new XElement("ApplicationConfiguration");
      string xml = String.Empty;

      switch (configurationType)
      {
        case ConfigurationType.SharedFile:
          if (!Directory.Exists(centralAppConfigPath))
          {
            //MessageBox.Show("This program is configured to use 'file-based' Central Configuration, but no path has been provided to the Central Configuration profiles." +
            //    g.crlf2 + "No central configuration items will be loaded.",
            //    "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            xml = @"<ApplicationConfiguration/>";
            break;
          }

          if (centralAppConfigProfile.IsBlank())
          {
            //MessageBox.Show("This program is configured to use 'file-based' Central Configuration, but no configuration profile has been specified." +
            //    g.crlf2 + "No central configuration items will be loaded.",
            //    "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            xml = @"<ApplicationConfiguration/>";
            break;
          }

          string centralConfigProfileFolder = g.RemoveTrailingSlash(centralAppConfigPath) + @"\APPCFG\PROFILES\" + centralAppConfigProfile;
          if (!Directory.Exists(centralConfigProfileFolder))
          {
            //MessageBox.Show("This program is configured to use 'file-based' Central Configuration, but the specified profile '" +
            //    centralAppConfigProfile + "' cannot be located." +
            //    g.crlf2 + "No central configuration items will be loaded.",
            //    "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            xml = @"<ApplicationConfiguration/>";
            break;
          }

          string centralConfigFile = g.RemoveTrailingSlash(centralAppConfigPath) + @"\APPCFG\PROFILES\" + centralAppConfigProfile + @"\CentralConfig.xmlx";
          string unencryptedCentralConfigFile = centralConfigFile.Replace(".xmlx", ".xml");
          if (!File.Exists(centralConfigFile) && !File.Exists(unencryptedCentralConfigFile))
          {
            //MessageBox.Show("This program is configured to use 'file-based' Central Configuration, but no central configuration file exists at path '" +
            //    centralConfigFile + "'." +
            //    g.crlf2 + "No central configuration items will be loaded.",
            //    "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            xml = @"<ApplicationConfiguration/>";
            break;
          }

          xml = AppConfigHelper.GetCentralAppConfigFile(g.AppConfig, centralAppConfigProfile);
          break;

        case ConfigurationType.SharedService:

          if (!configWsSpec.IsReadyToConnect())
          {
            //MessageBox.Show("This program is configured to use 'service-based' Central Configuration, but central configuration service specifications are incomplete or missing." + PLGlobal.crlf2 +
            //    "Web Service Binding: " + configWsSpec.WsBinding + g.crlf +
            //    "Web Service Host: " + configWsSpec.WsHost + g.crlf +
            //    "Web Service Port: " + configWsSpec.WsPort + g.crlf2 +
            //    "No central configuration items will be loaded.",
            //    "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            xml = @"<ApplicationConfiguration/>";
            break;
          }

          if (centralAppConfigProfile.IsBlank())
          {
            //MessageBox.Show("This program is configured to use 'service-based' Central Configuration, but no configuration profile has been specified." +
            //    g.crlf2 + "No central configuration items will be loaded.",
            //    "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            xml = @"<ApplicationConfiguration/>";
            break;
          }

          //string endpoint = configWsSpec.WebServiceEndpoint;

          //WsMessage requestMessage = WsMessageFactory.BuildGetConfigFileRequestMessage(MessagingParticipant.Sender, GetConfigFileCommand.GetConfigFile,
          //        ConfigFileType.AppConfig, centralAppConfigProfile, "CentralConfig.xmlx");
          //requestMessage.MessageHeader.AppName = g.AppInfo.AppName;
          //requestMessage.MessageHeader.Version = g.AppInfo.AppVersion;
          //WsMessage responseMessage = SimpleServiceMessaging.SendMessage(requestMessage, endpoint);

          //switch (responseMessage.MessageBody.Transaction.TransactionHeader.TransactionStatus)
          //{
          //  case WsTransactionStatus.Success:
          //    xml = responseMessage.TransactionBody.ToString();
          //    break;

          //  case WsTransactionStatus.Error:
          //    ErrorResponse errorResponse = new ErrorResponse();
          //    //errorResponse.LoadFromXml(responseMessage.TransactionBody);
          //    xml = @"<ApplicationConfiguration/>";
          //    break;
          //}

          break;
      }

      try
      {
        centralAppConfigXml = XElement.Parse(xml);
        return centralAppConfigXml;
      }
      catch
      {
        return new XElement("ApplicationConfiguration");
      }
    }

    //public static bool PingCentralConfigService(ConfigWsSpec configWsSpec)
    //{
    //  if (!configWsSpec.IsReadyToConnect())
    //    return false;

    //  string endpoint = configWsSpec.WebServiceEndpoint;

    //  WsMessage requestMessage = WsMessageFactory.BuildPingRequestMessage(MessagingParticipant.Sender);
    //  requestMessage.MessageHeader.AppName = g.AppInfo.AppName;
    //  requestMessage.MessageHeader.Version = g.AppInfo.AppVersion;
    //  WsMessage responseMessage = SimpleServiceMessaging.SendMessage(requestMessage, endpoint);

    //  switch (responseMessage.MessageBody.Transaction.TransactionHeader.TransactionStatus)
    //  {
    //    case WsTransactionStatus.Success:
    //      return true;
    //  }

    //  return false;
    //}

    public static string GetConfigReport(AppConfig c)
    {
      StringBuilder sb = new StringBuilder();

      //sb.Append("A D S D I     A P P L I C A T I O N     C O N F I G U R A T I O N     R E P O R T" + g.crlf2);
      //sb.Append("AppConfig Path : " + c.FullPath + g.crlf);
      //sb.Append("UFS Path       : " + c.AppDataPath + g.crlf2);

      //sb.Append("APPLICATION CONFIGURATION PROPERTIES" + g.crlf2);
      //sb.Append("    InDiagnosticsMode:    " + c.InDiagnosticsMode.ToString().PadTo(37) + "  ConfigurationType:     " + c.ConfigurationType.ToString() + g.crlf);
      //sb.Append("    TargetSystem:         " + c.TargetSystem.ToString().PadTo(37) + "  IsLoaded:              " + c.IsLoaded.ToString() + g.crlf);
      //sb.Append("    IsCentralAppConfig:   " + c.IsCentralAppConfig.ToString().PadTo(37) + "  IsCentralConfigLoaded: " + c.IsCentralConfigLoaded.ToString() + g.crlf);
      //sb.Append("    CentralConfigProfile: " + c.LoadedCentralConfigProfile.PadTo(37) + "  IsInUpdateMode:        " + c.InUpdateMode.ToString() + g.crlf);
      //sb.Append("    UseLocalConfig:       " + c.UseLocalConfig.ToString().PadTo(37) + "  LoadedBy:              " + c.LoadedBy + g.crlf2);

      //sb.Append("CONFIGURATION ITEMS                      STATUS                  LOCAL CONFIG VALUE                                            CENTRAL CONFIG VALUE" + g.crlf2);

      //foreach (ProgramConfig pc in c.ProgramConfigSet.Values)
      //{
      //    sb.Append("CONFIG NAME: " + pc.ConfigName.PadTo(25) + "   " + pc.ConfigStatus.ToString() + g.crlf);

      //    foreach (CIGroup g in pc.CISet.Values)
      //    {
      //        sb.Append(g.crlf + "  GROUP: " + g.Name.PadTo(25) + "       " + g.ConfigStatus.ToString().PadTo(22) +
      //            "  -----------------------------------------------------------------------------------------------------------------------------------------" + g.crlf);
      //        {
      //            foreach (CI ci in g.Values)
      //            {
      //                string defaultIndicator = String.Empty;
      //                if (ci.ConfigItemSource == "Default")
      //                    defaultIndicator = " (D)";
      //                if (ci.ConfigItemSource == "DefaultNoOverride")
      //                    defaultIndicator = " (DX)";

      //                string localValue = ci.Value.Trim();
      //                string centralValue = ci.CentralValue.Trim();

      //                if (ci.Key.ToLower().IndexOf("password") > -1 || ci.Key.ToLower().IndexOf("scannerencryptedserialnumber") > -1)
      //                {
      //                    if (localValue.IsNotBlank())
      //                        localValue = "**********";
      //                    if (centralValue.IsNotBlank())
      //                        centralValue = "**********";
      //                }

      //                if (localValue.IsBlank())
      //                    localValue = "[no local value]";
      //                if (centralValue.IsBlank())
      //                    centralValue = "[no central value]";

      //                sb.Append("    " + ci.Key.PadTo(35) + "  " + (ci.ConfigStatus.ToString() + defaultIndicator).PadTo(22) + "  " +
      //                    localValue.PadTo(60) + "  " + centralValue.PadTo(60) + g.crlf);
      //            }
      //        }
      //    }
      //    sb.Append(g.crlf);
      //}

      string configReport = sb.ToString();
      return configReport;
    }


  }
}