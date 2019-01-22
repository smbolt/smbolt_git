using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using Org.GS.Dynamic;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class AppConfig
  {
    private Encryptor encryptor;

    public ConfigurationType ConfigurationType {
      get;
      set;
    }
    public NotifyConfig NotifyConfig {
      get {
        return Get_NotifyConfig();
      }
    }
    public string ConfigName {
      get;
      set;
    }
    public string ModuleName {
      get;
      set;
    }
    public bool UseLocalConfig {
      get;
      set;
    }
    public string AppDataPath {
      get;
      set;
    }
    public bool IsUpdated {
      get {
        return Get_IsUpdated();
      }
    }
    public bool IsLoaded {
      get;
      set;
    }
    public string FullPath {
      get;
      set;
    }
    public string LoadedBy {
      get;
      set;
    }
    public string OriginalFormattedXml {
      get;
      set;
    }
    public string OriginalImage {
      get;
      set;
    }
    public string CurrentImage {
      get;
      set;
    }
    private bool _pcKeysSet = false;
    private List<string> _pcKeys;

    [XMap(CollectionElements = "ProgramConfig", WrapperElement = "ProgramConfigSet")]
    public ProgramConfigSet ProgramConfigSet {
      get;
      set;
    }

    [XMap(XType=XType.Element)]
    public ConfigSecurity ConfigSecurity {
      get;
      set;
    }

    public Dictionary<string, string> Variables {
      get;
      set;
    }

    public string ObjectFactoryDebugSerialize {
      get;
      set;
    }
    public string ObjectFactoryDebugDeserialize {
      get;
      set;
    }

    public AppConfig()
    {
      this.Initialize();
    }

    public AppConfig(string configName)
    {
      this.encryptor = new Encryptor();

      this.ConfigSecurity = new ConfigSecurity();
      this.ConfigurationType = ConfigurationType.LocalFile;
      this.ConfigName = configName;
      this.ModuleName = String.Empty;
      this.AppDataPath = String.Empty;
      this.UseLocalConfig = true;
      this.IsLoaded = false;
      this.FullPath = String.Empty;
      this.LoadedBy = String.Empty;

      this.ProgramConfigSet = new ProgramConfigSet();
      this.Variables = new Dictionary<string, string>();
      this.CurrentImage = String.Empty;
      this.OriginalImage = String.Empty;
    }

    public void Clear()
    {
      Initialize();
    }

    public void Initialize()
    {
      this.IsLoaded = false;
      this.FullPath = String.Empty;
      this.LoadedBy = String.Empty;
      this.Variables = new Dictionary<string, string>();
    }

    public void ResetPcKeys()
    {
      _pcKeysSet = false;
    }


    public void SetCI(string key, string value)
    {
      RefreshPcKeys();

      foreach (string pcKey in _pcKeys)
      {
        if (this.ProgramConfigSet.ContainsKey(pcKey))
        {
          if (this.ProgramConfigSet[pcKey].CISet != null)
          {
            foreach (KeyValuePair<string, CIGroup> kvpCIGroup in this.ProgramConfigSet[pcKey].CISet)
            {
              if (kvpCIGroup.Value.ContainsKey(key))
              {
                this.ProgramConfigSet[pcKey].CISet[kvpCIGroup.Value.Name][key].Value = value;
                return;
              }
            }
          }
        }
      }

      throw new Exception("Application configuration cannot update configuration item named '" + key + "' because it does not exist.");
    }


    public void SetCI(string program, string group, string key, string value)
    {
      if (this.ProgramConfigSet.ContainsKey(program))
      {
        ProgramConfig pc = this.ProgramConfigSet[program];
        if (pc.CISet != null)
        {
          if (pc.CISet.ContainsKey(group))
          {
            CIGroup ciGroup = pc.CISet[group];
            if (ciGroup.ContainsKey(key))
            {
              ciGroup[key].Value = value;
              return;
            }
            else
            {
              var ci = new CI(key, value, ciGroup);
              ciGroup.Add(key, ci);
              return;
            }
          }
        }
      }

      throw new Exception("Application configuration cannot update configuration item for program='" + program + "' " +
                          "group='" + group + "' key='" + key + "' because it does not exist.");
    }

    public string GetCI(string key)
    {
      try
      {
        if (!this.IsLoaded || key.IsBlank())
          return String.Empty;

        if (this.ProgramConfigSet == null)
          return String.Empty;

        RefreshPcKeys();

        foreach (string pcKey in _pcKeys)
        {
          if (this.ProgramConfigSet.ContainsKey(pcKey))
          {
            if (this.ProgramConfigSet[pcKey].CISet != null)
            {
              foreach (KeyValuePair<string, CIGroup> kvpCIGroup in this.ProgramConfigSet[pcKey].CISet)
              {
                if (kvpCIGroup.Value.ContainsKey(key))
                {
                  string value = kvpCIGroup.Value[key].Value;
                  string resolvedValue = ResolveVariables(value);
                  return resolvedValue;
                }
              }
            }
          }
        }

        return String.Empty;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to retrieve a CI with the key '" + key + "'.", ex);
      }
    }

    public string GetVariableValue(string variableName)
    {
      return this.ResolveVariables(variableName);
    }

    public bool ContainsList(string listName)
    {
      if (!this.IsLoaded || listName.IsBlank())
        return false;

      if (this.ProgramConfigSet == null)
        return false;

      RefreshPcKeys();

      foreach (string pcKey in _pcKeys)
      {
        if (this.ProgramConfigSet.ContainsKey(pcKey))
        {
          if (this.ProgramConfigSet[pcKey].ConfigListSet != null)
          {
            if (this.ProgramConfigSet[pcKey].ConfigListSet.ContainsKey(listName))
            {
              return true;
            }
          }
        }
      }

      return false;
    }

    public List<string> GetList(string listName)
    {
      List<string> list = new List<string>();

      if (!this.IsLoaded || listName.IsBlank())
        return list;

      if (this.ProgramConfigSet == null)
        return list;

      RefreshPcKeys();

      foreach (string pcKey in _pcKeys)
      {
        if (this.ProgramConfigSet.ContainsKey(pcKey))
        {
          if (this.ProgramConfigSet[pcKey].ConfigListSet != null)
          {
            if (this.ProgramConfigSet[pcKey].ConfigListSet.ContainsKey(listName))
            {
              foreach (LI li in this.ProgramConfigSet[pcKey].ConfigListSet[listName])
              {
                list.Add(ResolveVariables(li.Value));
              }

              return list;
            }
          }
        }
      }

      return list;
    }

    public Dictionary<string, string> GetDictionary(string dictName)
    {
      Dictionary<string, string> dict = new Dictionary<string, string>();

      if (!this.IsLoaded || dictName.IsBlank())
        return dict;

      if (this.ProgramConfigSet == null)
        return dict;

      RefreshPcKeys();

      foreach (string pcKey in _pcKeys)
      {
        if (this.ProgramConfigSet.ContainsKey(pcKey))
        {
          if (this.ProgramConfigSet[pcKey].ConfigDictionarySet != null)
          {
            if (this.ProgramConfigSet[pcKey].ConfigDictionarySet.ContainsKey(dictName))
            {
              foreach (DI di in this.ProgramConfigSet[pcKey].ConfigDictionarySet[dictName])
              {
                dict.Add(di.Key, ResolveVariables(di.Value));
              }

              return dict;
            }
          }
        }
      }

      return dict;
    }

    public bool ContainsDictionary(string dictName)
    {

      if (!this.IsLoaded || dictName.IsBlank())
        return false;

      if (this.ProgramConfigSet == null)
        return false;

      RefreshPcKeys();

      foreach (string pcKey in _pcKeys)
      {
        if (this.ProgramConfigSet.ContainsKey(pcKey))
        {
          if (this.ProgramConfigSet[pcKey].ConfigDictionarySet != null)
          {
            if (this.ProgramConfigSet[pcKey].ConfigDictionarySet.ContainsKey(dictName))
            {
              return true;
            }
          }
        }
      }

      return false;
    }

    public List<int> GetIntList(string listName)
    {
      List<int> list = new List<int>();

      if (!this.IsLoaded || listName.IsBlank())
        return list;

      if (this.ProgramConfigSet == null)
        return list;

      RefreshPcKeys();

      foreach (string pcKey in _pcKeys)
      {
        if (this.ProgramConfigSet.ContainsKey(pcKey))
        {
          if (this.ProgramConfigSet[pcKey].ConfigListSet != null)
          {
            if (this.ProgramConfigSet[pcKey].ConfigListSet.ContainsKey(listName))
            {
              foreach (LI li in this.ProgramConfigSet[pcKey].ConfigListSet[listName])
              {
                if (li.Value.IsNumeric())
                  list.Add(Int32.Parse(li.Value));
              }

              return list;
            }
          }
        }
      }

      return list;
    }

    public void UpdateList(string listName, List<string> updatedList)
    {
      RefreshPcKeys();

      foreach (string pcKey in _pcKeys)
      {
        if (this.ProgramConfigSet[pcKey].ConfigListSet != null)
        {
          if (this.ProgramConfigSet[pcKey].ConfigListSet.ContainsKey(listName))
          {
            updatedList.Sort();
            ConfigList newList = new ConfigList(this.ProgramConfigSet[pcKey].ConfigListSet);
            newList.Name = listName;
            foreach (string newListItem in updatedList)
            {
              LI li = new LI(newListItem, newList);
              newList.Add(li);
            }

            this.ProgramConfigSet[pcKey].ConfigListSet[listName] = newList;
            return;
          }
        }
      }

      throw new Exception("Application configuration cannot update configuration list named '" + listName + "' because no such list exists.");
    }

    public bool GetBoolean(string key, bool defaultValue)
    {
      if (this.ContainsKey(key))
      {
        string value = this.GetCI(key).Trim();
        bool returnValue = false;
        Boolean.TryParse(value, out returnValue);
        return returnValue;
      }

      return defaultValue;
    }

    public bool GetBoolean(string key)
    {
      if (this.ContainsKey(key))
      {
        string value = this.GetCI(key).Trim();
        bool returnValue = false;
        Boolean.TryParse(value, out returnValue);
        return returnValue;
      }

      return false;
    }

    public int GetInteger(string key)
    {
      if (this.ContainsKey(key))
      {
        string value = this.GetCI(key).Trim();
        int returnValue = 0;
        Int32.TryParse(value, out returnValue);
        return returnValue;
      }

      return 0;
    }

    public DateTime GetDate(string key)
    {
      if (this.ContainsKey(key))
      {
        string value = this.GetCI(key).Trim();
        DateTime returnValue = DateTime.MinValue;

        if (DateTime.TryParse(value, out returnValue))
          return returnValue;
      }

      return DateTime.MinValue;
    }

    public float GetFloat(string key)
    {
      if (this.ContainsKey(key))
      {
        string value = this.GetCI(key).Trim();
        decimal returnValue = 0;
        Decimal.TryParse(value, out returnValue);
        return (float)returnValue;
      }

      return 0;
    }

    public int? GetNullableInteger(string key)
    {
      if (this.ContainsKey(key))
      {
        string value = this.GetCI(key).Trim();
        int returnValue = 0;
        Int32.TryParse(value, out returnValue);
        return returnValue;
      }

      return null;
    }

    public void UpdateBoolean(string programName, string groupName, string key, bool value)
    {
      string boolValue = value.ToString();
      this.UpdateCI(programName, groupName, key, boolValue);
    }

    public void SetBoolean(string key, bool value)
    {
      string boolValue = value.ToString();
      this.SetCI(key, boolValue);
    }

    public object GetConfigSecurityForUser_LockObject = new object();
    public ConfigSecurity GetConfigSecurityForUser(string userName)
    {
      if (Monitor.TryEnter(GetConfigSecurityForUser_LockObject, 5000))
      {
        try
        {
          var configSecurity = new ConfigSecurity();
          configSecurity.PasswordMinLth = this.ConfigSecurity.PasswordMinLth;
          configSecurity.PasswordMaxLth = this.ConfigSecurity.PasswordMaxLth;
          configSecurity.PasswordReqMixCase = this.ConfigSecurity.PasswordReqMixCase;
          configSecurity.PasswordReqNbr = this.ConfigSecurity.PasswordReqNbr;
          configSecurity.PasswordReqChgOnFirstUse = this.ConfigSecurity.PasswordReqChgOnFirstUse;
          configSecurity.PasswordReqChgFreq = this.ConfigSecurity.PasswordReqChgFreq;

          var configUser = this.ConfigSecurity.ConfigUserSet.Values.Where(u => u.UserName.ToUpper().Trim() == userName.ToUpper().Trim()).FirstOrDefault();
          if (configUser != null)
          {
            var u = new ConfigUser();
            u.UserType = configUser.UserType;
            u.UserID = configUser.UserID;
            u.UserName = configUser.UserName;
            u.Password = configUser.Password;
            u.FirstName = configUser.FirstName;
            u.LastName = configUser.LastName;
            u.CompanyName = configUser.CompanyName;
            u.DepartmentName = configUser.DepartmentName;

            foreach (var groupAssignment in configUser.ConfigGroupAssignments)
            {
              var ga = new ConfigGroupAssignment();
              ga.GroupID = groupAssignment.GroupID;
              u.ConfigGroupAssignments.Add(ga);
            }
            configSecurity.ConfigUserSet.Add(u.UserID, u);
          }

          foreach (var configGroup in this.ConfigSecurity.ConfigGroupSet.Values)
          {
            var cg = new ConfigGroup();
            cg.GroupID = configGroup.GroupID;
            cg.GroupName = configGroup.GroupName;
            configSecurity.ConfigGroupSet.Add(cg.GroupID, cg);
          }

          return configSecurity;
        }
        catch(Exception ex)
        {
          throw new Exception("An exception occurred attempting to get ConfigSecurity by user in GetConfigSecurityForUser in AppConfig.", ex);
        }
        finally
        {
          Monitor.Exit(GetConfigSecurityForUser_LockObject);
        }
      }
      else
      {
        throw new Exception("Could not obtain a lock for getting ConfigSecurity by user in GetConfigSecurityForUser in AppConfig.");
      }
    }

    public bool ContainsKey(string key)
    {
      if (!this.IsLoaded || key.IsBlank())
        return false;

      if (this.ProgramConfigSet == null)
        return false;

      RefreshPcKeys();

      foreach (string pcKey in _pcKeys)
      {
        if (this.ProgramConfigSet.ContainsKey(pcKey))
        {
          if (this.ProgramConfigSet[pcKey].CISet != null)
          {
            foreach (KeyValuePair<string, CIGroup> kvpCIGroup in this.ProgramConfigSet[pcKey].CISet)
            {
              if (kvpCIGroup.Value.ContainsKey(key))
              {
                return true;
              }
            }
          }
        }
      }

      return false;
    }

    public bool ContainsGroup(string groupName)
    {
      if (!this.IsLoaded || groupName.IsBlank())
        return false;

      if (this.ProgramConfigSet == null)
        return false;

      RefreshPcKeys();

      foreach (string pcKey in _pcKeys)
      {
        if (this.ProgramConfigSet.ContainsKey(pcKey))
        {
          if (this.ProgramConfigSet[pcKey].CISet != null)
          {
            if (this.ProgramConfigSet[pcKey].CISet.ContainsKey(groupName))
              return true;
          }
        }
      }

      return false;
    }

    public string ResolveVariables(string value)
    {
      string originalConfigValue = value;

      if (value.IndexOf("$") < 0)
        return value;

      int variableCount = 0;

      do
      {
        int begPos = value.IndexOf("$");
        int endPos = value.IndexOf("$", begPos + 1);
        if (endPos == -1)
          throw new Exception("The configuration value '" + value + "' contains an unpaired dollar sign ($) - variable name cannot be determined.");

        int length = endPos - begPos - 1;
        if (length < 1)
          throw new Exception("The configuration value '" + value + "' contains a zero-length varaible name.");

        string variableName = value.Substring(begPos + 1, length);
        if (!this.Variables.ContainsKey(variableName))
          return value;

        value = value.Replace("$" + variableName + "$", this.Variables[variableName]);

        variableCount++;
        if (variableCount > 10)
          throw new Exception("Greater than 10 variables found in configuration value '" + originalConfigValue + "'.");

      } while (value.IndexOf("$") > -1);

      return value;
    }


    public void ReloadFromFile()
    {
      this.Variables.Clear();
      string configPath = g.AppConfigPath + @"\AppConfig.xmlx";
      LoadFromFile(configPath);
    }

    public void LoadFromFile()
    {
      string defaultConfigFilePath = g.AppConfigPath + @"\AppConfig.xmlx";
      this.LoadFromFile(defaultConfigFilePath);
    }

    public void LoadFromFile(string encryptedConfigPath)
    {
      try
      {
        this.FullPath = encryptedConfigPath;
        string unencryptedConfigPath = this.FullPath.Replace(".xmlx", ".xml");

        if (!File.Exists(encryptedConfigPath) && !File.Exists(unencryptedConfigPath))
          CreateDefaultAppConfig(unencryptedConfigPath);

        if (File.Exists(unencryptedConfigPath))
        {
          XElement unencryptedXml = XElement.Parse(File.ReadAllText(unencryptedConfigPath));
          string unencryptedFormattedXml = unencryptedXml.ToString();
          File.WriteAllText(encryptedConfigPath, encryptor.EncryptString(unencryptedFormattedXml));
        }

        if (!File.Exists(encryptedConfigPath))
          throw new Exception("Application configuration file '" + encryptedConfigPath + "' does not exist.");

        Decryptor decryptor = new Decryptor();
        string decryptedConfig = decryptor.DecryptString(File.ReadAllText(encryptedConfigPath));
        this.LoadFromString(decryptedConfig);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred loading the AppConfig from file.", ex);
      }
    }

    public void LoadFromString(string configString)
    {
      ObjectFactory2 f = null;

      try
      {
        StartupLogging.WriteStartupLog("Loading AppConfig file from string");

        XElement appConfig = XElement.Parse(configString);
        this.OriginalFormattedXml = appConfig.ToString();

        f = new ObjectFactory2();
        f.InDiagnosticsMode = true;

        if (this.ObjectFactoryDebugDeserialize != null)
        {
          f.DeserializeDebugBreak = this.ObjectFactoryDebugDeserialize;
          f.InDiagnosticsMode = true;
        }

        f.LogToMemory = true;
        XElement programConfigSetXml = appConfig.Element("ProgramConfigSet");
        if (programConfigSetXml != null)
          this.ProgramConfigSet = f.Deserialize(programConfigSetXml) as ProgramConfigSet;

        CheckForDuplicateCIs();
        LoadVariables();

        if (appConfig.Element("ConfigSecurity") != null)
          this.ConfigSecurity = f.Deserialize(this, appConfig.Element("ConfigSecurity"), null, null) as ConfigSecurity;

        if (!g.IsWebBasedApplication)
        {
          if (this.Variables.ContainsKey("EXEPATH"))
            this.Variables["EXEPATH"] = g.GetAppPath();
          else
            this.Variables.Add("EXEPATH", g.GetAppPath());
        }

        if (g.RuntimeEnvironment == RuntimeEnvironment.Dev && g.BranchName.IsNotBlank())
        {
          if (this.Variables.ContainsKey("BRANCH"))
            this.Variables["BRANCH"] = g.BranchName;
          else
            this.Variables.Add("BRANCH", g.BranchName);
        }

        this.CaptureCurrentImage();

        this.IsLoaded = true;
        bool skipAppConfigVerification = this.GetCI("SkipAppConfigVerification").ToBoolean();

        if (this.CurrentImage != this.OriginalFormattedXml && !skipAppConfigVerification)
        {
          string firstDiffLine = GetFirstConfigDifference(this.CurrentImage, this.OriginalFormattedXml);
          if (g.AppConfig.ConfigName != "AppConfigManager")
          {
            if (g.CI("UseConfigErrorForm").ToBoolean())
            {
              string configData = "ORIGINAL FORMATTED XML" + g.crlf2 +
                                  this.OriginalFormattedXml + g.crlf2 +
                                  "CURRENT PROCESSED XML" + g.crlf2 +
                                  this.CurrentImage + g.crlf2 +
                                  "FIRST LINE WITH DIFFERENCES" + g.crlf2 +
                                  firstDiffLine + g.crlf;

              using (var fConfigData = new frmConfigData(configData))
              {
                fConfigData.ShowDialog();
              }
            }
            throw new Exception("The AppConfig file does not serialize and deserialize to the same image." + g.crlf + firstDiffLine);
          }
        }

      }
      catch (Exception ex)
      {
        if (ex.GetType().Name == "ReflectionTypeLoadException")
        {
          StringBuilder sb = new StringBuilder();
          var rtlEx = (ReflectionTypeLoadException)ex;
          var loaderExceptions = rtlEx.LoaderExceptions;
          foreach (var loaderException in loaderExceptions)
            sb.Append(loaderException.Message + g.crlf);
          string message = "A ReflectionTypeLoadException occurred while attempting to load the AppConfigFile - individual loader exception messages follow." + g.crlf + sb.ToString();
          StartupLogging.WriteStartupLog(message);
          throw new Exception(message, ex);
        }
        else
        {
          throw new Exception("An exception occurred loading the AppConfig from file.", ex);
        }
      }
    }

    private void CreateDefaultAppConfig(string unencryptedConfigPath)
    {
      string importAppConfigPath = String.Empty;

      try
      {
        if (g.AttemptConfigImport)
        {
          frmLocateAppConfig fLocateAppConfig = new frmLocateAppConfig(g.UseInitialConfig);
          if (fLocateAppConfig.ShowDialog() == DialogResult.OK)
          {
            if (fLocateAppConfig.UseInitAppConfig)
            {
              if (g.InitConfig.IsNotBlank())
              {
                File.WriteAllText(g.AppConfigPath + @"\AppConfig.xml", g.InitConfig);
                File.WriteAllText(g.AppConfigPath + @"\AppConfig.xmlx", encryptor.EncryptString(g.InitConfig));
              }
              return;
            }
            else
            {
              importAppConfigPath = fLocateAppConfig.LocatedConfigFile;
              string extension = Path.GetExtension(importAppConfigPath);
              string encryptedAppConfigPath = importAppConfigPath;

              if (extension != ".xmlx")
                encryptedAppConfigPath = encryptedAppConfigPath.Replace(".xml", ".xmlx");
              string unencryptedAppConfigPath = encryptedAppConfigPath.Replace(".xmlx", ".xml");

              if (extension == ".xmlx")
              {
                File.Copy(encryptedAppConfigPath, g.AppConfigPath + @"\AppConfig.xmlx", true);
                File.WriteAllText(g.AppConfigPath + @"\AppConfig.xml", encryptor.DecryptString(File.ReadAllText(encryptedAppConfigPath)));
              }
              else
              {
                File.Copy(unencryptedAppConfigPath, g.AppConfigPath + @"\AppConfig.xml", true);
                File.WriteAllText(g.AppConfigPath + @"\AppConfig.xmlx", encryptor.EncryptString(File.ReadAllText(unencryptedAppConfigPath)));
              }

              return;
            }
          }
        }

        this.Clear();
        string configName = g.AppInfo.ConfigName;
        if (configName.IsBlank())
          configName = "ConfigName";
        this.ProgramConfigSet.Add(configName, new ProgramConfig(this.ProgramConfigSet, configName));
        var programConfig = this.ProgramConfigSet.Values.First();
        var ciSet = programConfig.CISet;
        ciSet.Add("Options", new CIGroup("Options", ciSet));
        var optionsCIGroup = ciSet["Options"];
        optionsCIGroup.Add("ConfigItem1", new CI("ConfigItem1", "ConfigValue1", optionsCIGroup));
        programConfig.ConfigListSet.Add("List1", new ConfigList("List1", programConfig.ConfigListSet));
        var list1 = programConfig.ConfigListSet["List1"];
        list1.Add(new LI("ListItem1", list1));
        programConfig.ConfigDictionarySet.Add("Dictionary1", new ConfigDictionary("Dictionary1", programConfig.ConfigDictionarySet));
        var dict1 = programConfig.ConfigDictionarySet["Dictionary1"];
        dict1.Add(new DI("Key1", "Value1", dict1));
        this.CaptureCurrentImage();
        File.WriteAllText(unencryptedConfigPath, this.CurrentImage);
      }
      catch (Exception ex)
      {
        string message = ex.Message;
      }
    }

    private void CaptureCurrentImage()
    {
      this.OriginalImage = this.Serialize().ToString();
      this.CurrentImage = this.OriginalImage;
    }

    private bool Get_IsUpdated()
    {
      this.CurrentImage = this.Serialize().ToString();
      if (this.CurrentImage != this.OriginalImage)
        return true;

      return false;
    }

    public string GetCurrentImage()
    {
      return this.Serialize().ToString();
    }

    public void CheckForDuplicateCIs()
    {
      if (this.ProgramConfigSet == null)
        return;

      List<string> keys = new List<string>();

      foreach (ProgramConfig pc in this.ProgramConfigSet.Values)
      {
        if (pc.CISet != null)
        {
          keys = new List<string>();
          foreach (CIGroup g in pc.CISet.Values)
          {
            foreach (CI ci in g.Values)
            {
              if (keys.Contains(ci.Key))
                throw new Exception("A duplicate configuration item key '" + ci.Key + "' was found in the CIs for ProgramConfig '" + pc.ConfigName +
                                    " in CIGroup '" + g.Name + "' in the application configuration file at '" + this.FullPath + "'.");

              keys.Add(ci.Key);
            }
          }
        }
      }
    }

    public void LoadVariables()
    {
      if (this.ProgramConfigSet == null)
        return;

      foreach (ProgramConfig pc in this.ProgramConfigSet.Values)
      {
        if (pc.CISet != null)
        {
          foreach (CIGroup gp in pc.CISet.Values)
          {
            foreach (CI ci in gp.Values)
            {
              if (ci.Key.Length > 2)
              {
                if (ci.Key[0] == '$' && ci.Key[ci.Key.Length - 1] == '$')
                {
                  string variableKey = ci.Key.Substring(1, ci.Key.Length - 2);
                  if (this.Variables.ContainsKey(ci.Key))
                    this.Variables[variableKey] = ci.Value;
                  else
                    this.Variables.Add(variableKey, ci.Value);
                }
              }
            }
          }
        }
      }

      if (g.AppInfo.OrgApplicationType != ApplicationType.WebSite)
      {
        if (!this.Variables.ContainsKey("APPDATAPATH"))
        {
          this.Variables.Add("APPDATAPATH", g.AppDataPath);
        }

        if (!this.Variables.ContainsKey("EXEPATH"))
        {
          this.Variables.Add("EXEPATH", g.GetAppPath());
        }

        if (!this.Variables.ContainsKey("IMPORTS"))
        {
          this.Variables.Add("IMPORTS", g.ImportsPath);
        }

        if (!this.Variables.ContainsKey("EXPORTS"))
        {
          this.Variables.Add("EXPORTS", g.ExportsPath);
        }

        if (!this.Variables.ContainsKey("REPORTS"))
        {
          this.Variables.Add("REPORTS", g.ReportsPath);
        }

        if (!this.Variables.ContainsKey("ERRORS"))
        {
          this.Variables.Add("ERRORS", g.ErrorsPath);
        }

        if (!this.Variables.ContainsKey("MEFCATALOG"))
        {
          this.Variables.Add("MEFCATALOG", g.MEFCatalog);
        }
      }

      if (this.Variables.ContainsKey("DEVPATH") && this.Variables.ContainsKey("ORGDEV") && this.Variables.ContainsKey("GPDEV"))
      {
        if (g.ExecutablePath.Contains("DEV-MAIN"))
          this.Variables["DEVPATH"] = this.Variables["GPDEV"];
        else
          this.Variables["DEVPATH"] = this.Variables["ORGDEV"];
      }

      if (!this.Variables.ContainsKey("HOST"))
        this.Variables.Add("HOST", Environment.MachineName.ToUpper());
    }

    public void Save()
    {
      if (!File.Exists(this.FullPath))
        throw new Exception("Configuration file path does not exist: '" + this.FullPath + "'.");

      string unencryptedConfig = this.Serialize().ToString();
      string encryptedConfig = encryptor.EncryptString(unencryptedConfig);
      File.WriteAllText(this.FullPath, encryptedConfig);

      string unencryptedFilePath = this.FullPath.Replace(".xmlx", ".xml");

      if (File.Exists(unencryptedFilePath))
        File.WriteAllText(unencryptedFilePath, unencryptedConfig);
    }

    public XElement Serialize()
    {
      ObjectFactory2 f = new ObjectFactory2();

      if (this.ObjectFactoryDebugSerialize != null)
        f.SerializeDebugBreak = this.ObjectFactoryDebugSerialize;

      return f.Serialize(this);
    }

    public void AddCI(string programName, string group, CI ci)
    {
      if (!this.ProgramConfigSet.ContainsKey(programName))
      {
        ProgramConfig pcNew = new ProgramConfig(this.ProgramConfigSet, programName);
        this.ProgramConfigSet.Add(programName, pcNew);
      }

      ProgramConfig pc = this.ProgramConfigSet[programName];

      if (pc.CISet == null)
        pc.CISet = new CISet(pc);

      if (pc.CISet.ContainsKey(group))
        pc.CISet[group].Add(ci.Key, ci);
      else
      {
        CIGroup ciGroup = new CIGroup(pc.CISet);
        ciGroup.Name = group;
        ciGroup.Add(ci.Key, ci);
        pc.CISet.Add(group, ciGroup);
      }
    }

    public void AddCI(string group, string key, string value)
    {
      if (group == null)
        group = "Default";

      if (this.ProgramConfigSet.ContainsKey(this.ConfigName))
      {
        if (key.Length > 1)
        {
          if (key[0] == '$' && key[key.Length - 1] == '$')
          {
            key = key.Replace("$", String.Empty);
            if (this.Variables.ContainsKey(key))
              this.Variables[key] = value;
            else
              this.Variables.Add(key, value);
          }
        }

        ProgramConfig pc = this.ProgramConfigSet[this.ConfigName];

        if (pc.CISet == null)
          pc.CISet = new CISet(pc);

        CIGroup g;
        if (!pc.CISet.ContainsKey(group))
        {
          g = new CIGroup(group, pc.CISet);
          pc.CISet.Add(g.Name, g);
        }

        g = pc.CISet[group];
        if (g.ContainsKey(key))
          g[key].Value = value;
        else
        {
          g.Add(key, new CI(key, value));
        }
      }
      else
        throw new Exception("Application configuration program config '" + this.ConfigName + "' does not exist.");
    }

    public void AddDynamic(string key, string value)
    {
      if (this.ProgramConfigSet.ContainsKey(this.ConfigName))
      {
        if (key.Length > 1)
        {
          if (key[0] == '$' && key[key.Length - 1] == '$')
          {
            key = key.Replace("$", String.Empty);
            if (this.Variables.ContainsKey(key))
              this.Variables[key] = value;
            else
              this.Variables.Add(key, value);
          }
        }

        ProgramConfig pc = this.ProgramConfigSet[this.ConfigName];

        if (pc.CISet == null)
          pc.CISet = new CISet(pc);

        CIGroup g;
        if (!pc.CISet.ContainsKey("Dynamic"))
        {
          g = new CIGroup("Dynamic", pc.CISet);
          pc.CISet.Add(g.Name, g);
        }

        g = pc.CISet["Dynamic"];
        if (g.ContainsKey(key))
          g[key].Value = value;
        else
        {
          g.Add(key, new CI(key, value));
        }
      }
      else
        throw new Exception("Application configuration program config '" + this.ConfigName + "' does not exist.");
    }

    public void UpdateCI(string programName, string groupName, string key, string value)
    {
      if (key.IsBlank())
        return;

      if (this.ProgramConfigSet.ContainsKey(programName))
      {
        ProgramConfig pc = this.ProgramConfigSet[programName];

        if (pc.CISet == null)
          pc.CISet = new CISet(pc);

        CIGroup group;

        if (!pc.CISet.ContainsKey(groupName))
          pc.CISet.Add(groupName, new CIGroup(groupName, pc.CISet));

        group = pc.CISet[groupName];
        if (group.ContainsKey(key))
        {
          group[key].Value = value;
          return;
        }
        else
        {
          CI updateCI = new CI(key, value, group);
          group.Add(updateCI.Key, updateCI);
          return;
        }
      }

      throw new Exception("Application configuration cannot update configuration item named '" + key + "' because no program configuration exists for program name '" + programName + "'.");
    }

    public void DeleteCI(string program, string group, string key)
    {
      if (program.IsBlank() || group.IsBlank() || key.IsBlank())
        return;

      if (!this.ProgramConfigSet.ContainsKey(program))
        return;

      if (!this.ProgramConfigSet[program].CISet.ContainsKey(group))
        return;

      this.ProgramConfigSet[program].CISet[group].Remove(key);
    }

    public void DeleteCI(CI ci)
    {
      string group = String.Empty;
      string program = String.Empty;

      if (ci.CIGroup != null)
      {
        if (ci.CIGroup.CISet != null)
        {
          if (ci.CIGroup.CISet.ProgramConfig != null)
            program = ci.CIGroup.CISet.ProgramConfig.ConfigName;
        }
      }

      if (group.IsNotBlank() && program.IsNotBlank())
      {
        this.ProgramConfigSet[program].CISet[group].Remove(ci.Key);
        return;
      }


      RefreshPcKeys();

      foreach (string pcKey in _pcKeys)
      {
        if (!this.ProgramConfigSet.ContainsKey(pcKey))
        {
          ProgramConfig pc = this.ProgramConfigSet[pcKey];

          if (pc.CISet != null)
          {
            foreach (CIGroup ciGroup in pc.CISet.Values)
            {
              if (ciGroup.ContainsKey(ci.Key))
              {
                ciGroup.Remove(ci.Key);
                return;
              }
            }
          }
        }
      }
    }

    public TaskConfigurations GetTaskConfigurations()
    {
      if (!this.ProgramConfigSet.ContainsKey(this.ConfigName))
        return null;

      ProgramConfig pc = this.ProgramConfigSet[this.ConfigName];
      return pc.TaskConfigurations;
    }

    public ConfigSmtpSpec GetSmtpSpec(string prefix)
    {
      return (ConfigSmtpSpec)GetCO<ConfigSmtpSpec>(prefix);
    }

    public ConfigSyncSpec GetSyncSpec(string prefix)
    {
      return (ConfigSyncSpec)GetCO<ConfigSyncSpec>(prefix);
    }

    public ConfigDbSpec GetDbSpec(string prefix)
    {
      return (ConfigDbSpec)GetCO<ConfigDbSpec>(prefix);
    }

    public ConfigWsSpec GetWsSpec(string prefix)
    {
      return (ConfigWsSpec)GetCO<ConfigWsSpec>(prefix);
    }

    public void UpdateCO<T>(ConfigObjectBase co)
    {
      Type t = co.GetType();
      ConfigItemPropertySet cips = g.GetConfigItemPropertySet(t);

      foreach (ConfigItemProperty cip in cips.Values)
      {
        string ciValue = String.Empty;
        PropertyInfo pi = t.GetProperty(cip.PropertyName);
        if (pi != null)
          ciValue = pi.GetValue(co, null).ToString().Trim();

        this.UpdateCI(co.ConfigProgram, co.ConfigGroup, co.NamingPrefix + cip.PropertyName, ciValue);
      }
    }

    public bool ContainsConfigDbSpec(string configDbSpecName)
    {
      ConfigDbSpec configDbSpec = (ConfigDbSpec) ConfigObjectFactory.CreateConfigObject(typeof(ConfigDbSpec), configDbSpecName);

      if (this.ContainsGroup(configDbSpec.ConfigGroup))
        return true;

      return false;
    }

    public void AddConfigDbSpec(ConfigDbSpec configDbSpec, string programConfigName, string configDbSpecName)
    {
      if (this.ContainsConfigDbSpec(configDbSpecName))
        return;

      if (this.IsLoaded == false)
        throw new Exception("AppConfig object is not loaded.");

      if (this.ProgramConfigSet == null)
        throw new Exception("AppConfig ProgramConfigSet object is null.");

      if (!this.ProgramConfigSet.ContainsKey(programConfigName))
        throw new Exception("AppConfig ProgramConfig object named '" + programConfigName + "' does not exist.");

      string configDbSpecGroupName = configDbSpecName + "ConfigDbSpec";

      ProgramConfig pc = this.ProgramConfigSet[programConfigName];

      if (pc.CISet.ContainsKey(configDbSpecName + "configDbSpecGroupName"))
        throw new Exception("ConfigDbSpec object named '" + configDbSpecName + "' already exists.");

      CIGroup ciGroup = new CIGroup(configDbSpecGroupName, pc.CISet);

      CI ciDbServer = new CI(configDbSpecName + "DbServer", configDbSpec.DbServer);
      ciGroup.Add(ciDbServer.Key, ciDbServer);

      CI ciDbDsn = new CI(configDbSpecName + "DbDsn", configDbSpec.DbDsn);
      ciGroup.Add(ciDbDsn.Key, ciDbDsn);

      CI ciDbName = new CI(configDbSpecName + "DbName", configDbSpec.DbName);
      ciGroup.Add(ciDbName.Key, ciDbName);

      CI ciDbUserId = new CI(configDbSpecName + "DbUserId", configDbSpec.DbUserId);
      ciGroup.Add(ciDbUserId.Key, ciDbUserId);

      CI ciDbPassword = new CI(configDbSpecName + "DbPassword", configDbSpec.DbPassword);
      ciGroup.Add(ciDbPassword.Key, ciDbPassword);

      CI ciDbType = new CI(configDbSpecName + "DbType", configDbSpec.DbType.ToString());
      ciGroup.Add(ciDbType.Key, ciDbType);

      CI ciDbUseWindowsAuth = new CI(configDbSpecName + "DbUseWindowsAuth", configDbSpec.DbUseWindowsAuth.ToString());
      ciGroup.Add(ciDbUseWindowsAuth.Key, ciDbUseWindowsAuth);

      pc.CISet.Add(ciGroup.Name, ciGroup);
    }

    public ConfigObjectBase GetCO<T>(string prefix)
    {
      if (prefix.EndsWith("$HOST$"))
        prefix = prefix.Replace("$HOST$", Environment.MachineName.ToUpper());

      ConfigObjectBase co = ConfigObjectFactory.CreateConfigObject(typeof(T), prefix);
      co.ConfigProgram = this.GetProgramForGroup(co.ConfigGroup);
      if (co.ConfigProgram.IsBlank())
        co.ConfigProgram = g.AppInfo.AppTitle;

      Type t = co.GetType();
      ConfigItemPropertySet cips = g.GetConfigItemPropertySet(t);

      foreach (ConfigItemProperty cip in cips.Values)
      {
        string ciName = prefix.Trim() + cip.PropertyName;
        PropertyInfo pi = t.GetProperty(cip.PropertyName);

        if (this.ContainsKey(ciName))
          g.SetConfigObjectPropertyValue(co, pi, cip.PropertyType.Name, this.GetCI(ciName));
      }

      if (co.GetType().Name == "ConfigDbSpec")
      {
        var dbSpec = (ConfigDbSpec)co;
        if (dbSpec.DbPasswordEncoded)
          dbSpec.DbPassword = TokenMaker.DecodeToken2(dbSpec.DbPassword);
      }

      return co;
    }

    public ConfigObjectBase GetCO<T>(string program, string prefix, ConfigCreateMode createMode)
    {
      ConfigObjectBase co = ConfigObjectFactory.CreateConfigObject(typeof(T), prefix);
      co.ConfigProgram = program;
      Type t = co.GetType();
      ConfigItemPropertySet cips = g.GetConfigItemPropertySet(t);

      foreach (ConfigItemProperty cip in cips.Values)
      {
        string ciName = prefix.Trim() + cip.PropertyName;
        PropertyInfo pi = t.GetProperty(cip.PropertyName);

        if (this.ContainsKey(ciName))
          g.SetConfigObjectPropertyValue(co, pi, cip.PropertyType.Name, this.GetCI(ciName));
        else
        {
          if (!this.ContainsKey(ciName))
          {
            switch (createMode)
            {
              case ConfigCreateMode.CreatePermanent:
                this.AddCI(program, co.ConfigGroup, new CI(ciName, String.Empty));
                break;

              case ConfigCreateMode.CreateAsUpdate:
                this.AddCI(program, co.ConfigGroup, new CI("!" + ciName, String.Empty));
                break;
            }
          }
        }
      }

      return co;
    }

    public void SetCO<T>(ConfigObjectBase co)
    {
      Type t = co.GetType();
      ConfigItemPropertySet cips = g.GetConfigItemPropertySet(t);

      foreach (ConfigItemProperty cip in cips.Values)
      {
        string ciValue = String.Empty;
        PropertyInfo pi = t.GetProperty(cip.PropertyName);
        if (pi != null)
          ciValue = pi.GetValue(co, null).ToString().Trim();

        this.SetCI(co.ConfigProgram, co.ConfigGroup, co.NamingPrefix + cip.PropertyName, ciValue);
      }
    }

    public string GetProgramForGroup(string group)
    {
      if (this.IsLoaded == false)
        return String.Empty;

      if (this.ProgramConfigSet == null)
        return String.Empty;

      if (this.ProgramConfigSet.ContainsKey("Global"))
      {
        ProgramConfig pc = this.ProgramConfigSet["Global"];

        if (pc.CISet != null)
        {
          if (this.ProgramConfigSet["Global"].CISet.ContainsKey(group))
            return "Global";
        }
      }

      if (this.ProgramConfigSet.ContainsKey(this.ConfigName))
      {
        ProgramConfig pc = this.ProgramConfigSet[this.ConfigName];

        if (pc.CISet != null)
        {
          if (this.ProgramConfigSet[this.ConfigName].CISet.ContainsKey(group))
            return this.ConfigName;
        }
      }

      if (this.ModuleName.IsNotBlank())
      {
        if (this.ProgramConfigSet.ContainsKey(this.ModuleName))
        {
          ProgramConfig pc = this.ProgramConfigSet[this.ModuleName];

          if (pc.CISet != null)
          {
            if (this.ProgramConfigSet[this.ModuleName].CISet.ContainsKey(group))
              return this.ModuleName;
          }

        }
      }

      return String.Empty;
    }

    public string GetProgramForGroupFullSearch(string group)
    {
      if (this.IsLoaded == false)
        return String.Empty;

      if (this.ProgramConfigSet == null)
        return String.Empty;

      foreach(var programConfig in this.ProgramConfigSet)
      {
        if (programConfig.Value.CISet.ContainsKey(group))
          return programConfig.Key;
      }

      return String.Empty;
    }

    //[DebuggerStepThrough]
    public void RefreshPcKeys()
    {
      // The "program config keys" (pcKeys) define the hierarchy of the
      // cascading configs Global/Program/Module for retrieving configurations.
      // They "should" only require building once.
      if (_pcKeysSet)
        return;

      _pcKeys = new List<string>();

      List<string> existingProgramConfigNames = new List<string>();
      foreach (var pc in this.ProgramConfigSet)
      {
        if (!_pcKeys.Contains(pc.Key))
        {
          _pcKeys.Add(pc.Key);
        }
      }

      if (this.ModuleName.IsNotBlank() && !_pcKeys.Contains(this.ModuleName))
        _pcKeys.Add(this.ModuleName);
      if (this.ConfigName.IsNotBlank() && !_pcKeys.Contains(this.ConfigName))
        _pcKeys.Add(this.ConfigName);

      _pcKeysSet = true;
    }

    private NotifyConfig GetNotifyConfig(string notificationConfigName)
    {
      if (!this.ProgramConfigSet.ContainsKey(this.ConfigName))
        return null;

      if (this.ProgramConfigSet[this.ConfigName].NotifyConfigSet == null)
        return null;

      if (!this.ProgramConfigSet[this.ConfigName].NotifyConfigSet.ContainsKey(notificationConfigName))
        return null;

      return this.ProgramConfigSet[this.ConfigName].NotifyConfigSet[notificationConfigName];
    }

    private string GetFirstConfigDifference(string currCfg, string origCfg)
    {
      string currCfgWork = currCfg.Replace(g.crlf, "|");
      string origCfgWork = origCfg.Replace(g.crlf, "|");

      string[] currCfgLines = currCfgWork.Split(Constants.PipeDelimiter);
      string[] origCfgLines = origCfgWork.Split(Constants.PipeDelimiter);

      int currLineCount = currCfgLines.Length;
      int origLineCount = origCfgLines.Length;

      int maxLinesToProcess = currLineCount < origLineCount ? currLineCount : origLineCount;

      for (int i = 0; i < maxLinesToProcess; i++)
      {
        if (currCfgLines[i].Trim() != origCfgLines[i].Trim())
          return "First difference on zero-based line " + i.ToString() + g.crlf +
                 "Curr Cfg: " + currCfgLines[i].Trim() + g.crlf +
                 "Orig Cfg: " + origCfgLines[i].Trim();
      }

      return "Total curr cfg lines is " + currLineCount.ToString() + g.crlf +
             "Total orig cfg lines is " + origLineCount.ToString();
    }

    public NotifyConfigSet GetNotifyConfigSet()
    {
      if (this.ProgramConfigSet == null)
        return null;

      if (this.ConfigName.IsBlank())
        return null;

      ProgramConfig globalConfig = null;
      ProgramConfig programConfig = null;
      NotifyConfigSet ncs = null;

      if (this.ProgramConfigSet.ContainsKey("Global"))
        globalConfig = this.ProgramConfigSet["Global"];

      if (this.ProgramConfigSet.ContainsKey(this.ConfigName))
        programConfig = this.ProgramConfigSet[this.ConfigName];

      if (globalConfig != null && globalConfig.NotifyConfigSet != null && globalConfig.NotifyConfigSet.Count > 0)
        ncs = globalConfig.NotifyConfigSet;

      if (programConfig != null)
      {
        if (programConfig.NotifyConfigSet != null && programConfig.NotifyConfigSet.Count > 0)
          ncs = programConfig.NotifyConfigSet;
      }

      return ncs;
    }

    public NotifyConfig Get_NotifyConfig()
    {
      var ncs = GetNotifyConfigSet();

      if (ncs == null)
        return null;

      string notifyConfigName = this.GetCI("NotifyConfigName");
      if (notifyConfigName.IsBlank())
        notifyConfigName = "Default";

      if (!ncs.ContainsKey(notifyConfigName))
        return null;

      return ncs[notifyConfigName];
    }
  }
}
