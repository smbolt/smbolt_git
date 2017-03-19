using Microsoft.Win32;
using System.Collections.Generic;
using System;
using System.Security;
using System.Security.Principal;

namespace Org.GS
{
  public enum RegistryHive
  {
    HKEY_CLASSES_ROOT,
    HKEY_CURRENT_USER,
    HKEY_CURRENT_CONFIG,
    HKEY_LOCAL_MACHINE
  }

  public enum EnumDataType
  {
    String,
    Boolean,
    Integer
  }
  public class RegistryHelper
  {
    public static bool RegistryKeyExists(RegistryHive hive, string key)
    {
      RegistryKey regKey = GetRegistyKey(hive, key);
      if (regKey == null)
        return false;

      regKey.Close();
      return true;
    }

    public static bool RegistryValueExists(RegistryHive hive, string key, string valueName)
    {
      RegistryKey regKey = GetRegistyKey(hive, key);
      if (regKey == null)
          return false;

      string[] valueNames = GetValueNames(hive, key);
      foreach (string name in valueNames)
      {
        if (name == valueName.Trim())
        {
          regKey.Close();
          return true;
        }
      }

      regKey.Close();
      return false;
    }

    public static string[] GetSubKeyNames(RegistryHive hive, string key)
    {
      string[] subKeys = new string[0];

      RegistryKey regKey = GetRegistyKey(hive, key);
      if (regKey != null)
      {
        subKeys = regKey.GetSubKeyNames();
        regKey.Close();
      }

      return subKeys;
    }

    public static string[] GetValueNames(RegistryHive hive, string key)
    {
      string[] valueNames = new string[0];

      RegistryKey regKey = GetRegistyKey(hive, key);
      if (regKey != null)
      {
        valueNames = regKey.GetValueNames();
        regKey.Close();
      }

      return valueNames;
    }

    public static string GetSubKeyValue(RegistryHive hive, string key)
    {
      string value = String.Empty;

      RegistryKey regKey = GetRegistyKey(hive, key);
      if (regKey != null)
      {
        value = regKey.GetValue("").ToString();
        regKey.Close();
      }

      return value;
    }

    public static string GetValueNameValue(RegistryHive hive, string key, string valueName)
    {
      string value = String.Empty;

      RegistryKey regKey = GetRegistyKey(hive, key);
      if (regKey != null)
      {
        string[] valueNames = regKey.GetValueNames();
        for (int i = 0; i < valueNames.Length; i++)
        {
          if (valueNames[i] == valueName)
          {
            value = regKey.GetValue(valueName).ToString();
            break;
          }
        }

        regKey.Close();
      }

      return value;
    }

    public static string GetValueNameValue(RegistryHive hive, string key, string valueName, string defaultValue, EnumDataType dataType)
    {
      string returnVal = GetValueNameValue(hive, key, valueName);
      if (string.IsNullOrEmpty(returnVal)) //if key value is empty
      {
        SaveSetting(hive, key, valueName, defaultValue); //save the default setting
        returnVal = defaultValue; //return the default setting
      }
      else //key value is not empty
      {
        bool blResult;
        int intResult;

        if (dataType == EnumDataType.Boolean)
        {
          if (!bool.TryParse(returnVal, out blResult)) //we couldn't parse a bool
          {
            SaveSetting(hive, key, valueName, defaultValue); //save the default setting instead
            returnVal = defaultValue;
          }
        }
        else if (dataType == EnumDataType.Integer)
        {
          if (!int.TryParse(returnVal, out intResult)) //couldn't parse to integer
          {
            SaveSetting(hive, key, valueName, defaultValue); //save default value intstead
            returnVal = defaultValue;
          }
        }

                
      }
      return returnVal;
    }

    public static SortedList<string, string> GetAllValueNameValues(RegistryHive hive, string key)
    {
      SortedList<string, string> allValues = new SortedList<string, string>();

      RegistryKey regKey = GetRegistyKey(hive, key);
      if (regKey != null)
      {
        string[] valueNames = regKey.GetValueNames();
        for (int i = 0; i < valueNames.Length; i++)
        {
          allValues.Add(valueNames[i], regKey.GetValue(valueNames[i]).ToString());
        }

        regKey.Close();
      }

      return allValues;
    }

    private static RegistryKey GetRegistyKey(RegistryHive hive, string key)
    {
      RegistryKey regKey = null;
      switch (hive)
      {
        case RegistryHive.HKEY_CLASSES_ROOT:
          regKey = Registry.ClassesRoot.OpenSubKey(key, true);
          break;

        case RegistryHive.HKEY_LOCAL_MACHINE:
          regKey = Registry.LocalMachine.OpenSubKey(key, true);
          break;

        case RegistryHive.HKEY_CURRENT_USER:
          regKey = Registry.CurrentUser.OpenSubKey(key, true);
          break;

        case RegistryHive.HKEY_CURRENT_CONFIG:
          regKey = Registry.CurrentConfig.OpenSubKey(key, true);
          break;
      }

      return regKey;
    }

    public static void CreateSubKey(RegistryHive hive, string key)
    {           
      switch (hive)
      {
        case RegistryHive.HKEY_CURRENT_USER:
          Registry.CurrentUser.CreateSubKey(key);
          break;
        case RegistryHive.HKEY_CLASSES_ROOT:
          Registry.ClassesRoot.CreateSubKey(key);
          break;
        case RegistryHive.HKEY_CURRENT_CONFIG:
          Registry.CurrentConfig.CreateSubKey(key);   
          break; 
        case RegistryHive.HKEY_LOCAL_MACHINE:
          Registry.LocalMachine.CreateSubKey(key); 
          break;
      }
    }

    public static void DeleteSubKey(RegistryHive hive, string key)
    {    
      RegistryKey regKey = null;    
   
      regKey = RegistryHelper.GetRegistyKey(hive, key);
      if (regKey == null)
        return;

      switch (hive)
      {
        case RegistryHive.HKEY_CURRENT_USER:
          Registry.CurrentUser.DeleteSubKeyTree(key);
          break;
        case RegistryHive.HKEY_CLASSES_ROOT:
          Registry.ClassesRoot.DeleteSubKeyTree(key);
          break;
        case RegistryHive.HKEY_CURRENT_CONFIG:
          Registry.CurrentConfig.DeleteSubKeyTree(key);
          break;
        case RegistryHive.HKEY_LOCAL_MACHINE:
          Registry.LocalMachine.DeleteSubKeyTree(key);
          break;
      }
    }

    public static void DeleteValue(RegistryHive hive, string key, string valueName)
    {
      RegistryKey regKey = null;

      regKey = RegistryHelper.GetRegistyKey(hive, key);
      if (regKey != null)
      {
        string[] valueNames = regKey.GetValueNames();
        for (int i = 0; i < valueNames.Length; i++)
        {
          if (valueNames[i] == valueName)
          {
            regKey.DeleteValue(valueName);
            break;
          }
        }
        regKey.Close();
      }
    }

    public static void SaveSetting(RegistryHive hive, string key, string valueName, string valueNameValue)
    {
      RegistryKey regKey;

      if (RegistryKeyExists(hive, key))
      {
        regKey = GetRegistyKey(hive, key);
        regKey.SetValue(valueName, valueNameValue);
        regKey.Close();
      }
      else
      {
        CreateSubKey(hive, key);
        if (RegistryKeyExists(hive, key))
        {
          regKey = GetRegistyKey(hive, key);
          regKey.SetValue(valueName, valueNameValue, RegistryValueKind.String);
          regKey.Close();
        }
      }
    }
  }
}
