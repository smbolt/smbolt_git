using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Org.Software.Business;
using Org.Software.Business.Models;
using Org.WSO;
using Org.WSO.Transactions;
using Org.Software.Tasks.Transactions;
using Org.GS;
using Org.GS.Configuration;

namespace Org.Software.Tasks.Concrete
{
  public class CheckForUpdatesProcessor : RequestProcessorBase, IRequestProcessor, IDisposable
  {
    public static bool _isMapped = false;
    public ConfigDbSpec _configDbSpec;

    public CheckForUpdatesProcessor()
    {
      g.LogToMemory("CheckForUpdatesProcessor Created");
      if (!_isMapped)
      {
        if (XmlMapper.AddAssembly(Assembly.GetExecutingAssembly()))
          g.LogToMemory("CheckForUpdatesProcessor MappingTypes");
        _isMapped = true;
      }
    }

    public override XElement ProcessRequest()
    {
      base.Initialize(MethodBase.GetCurrentMethod());
      XmlMapper.AddAssembly(Assembly.GetExecutingAssembly());
      base.TransactionEngine.MessageHeader.AddPerfInfoEntry("Start of ProcessRequest");
      var f = new ObjectFactory2();
      CheckForUpdatesRequest request = f.Deserialize(base.TransactionEngine.TransactionBody) as CheckForUpdatesRequest;
      CheckForUpdatesResponse response = new CheckForUpdatesResponse();
      response.CurrentVersion = request.CurrentVersion;
      XElement transactionBody = null;

      try
      {
        string dbSpecPrefix = g.CI("SoftwareDbSpecPrefix");
        _configDbSpec = g.GetDbSpec(dbSpecPrefix);

        using (var repository = new SoftwareDataRepository(_configDbSpec))
        {
          var softwareUpdatesForModuleVersion = repository.GetSoftwareUpdatesForModuleVersion(request.ModuleCode, request.CurrentVersion);
          var updateForModuleVersion = GetUpdateForModuleVersion(softwareUpdatesForModuleVersion, request);

          if (updateForModuleVersion == null)
          {
            response.UpgradeAvailable = false;
            response.UpgradeRequired = false;
            response.UpgradeVersion = String.Empty;
          }
          else
          {
            response.UpgradeAvailable = true;
            response.UpgradeRequired = false;
            response.UpgradeVersion = updateForModuleVersion.VersionValue;
            response.PlatformString = updateForModuleVersion.SoftwarePlatformString;
          }

          //IList<Bus.SoftwareModule> softwareModules = repository.GetList<Bus.SoftwareModule>("SoftwareModuleName");

          response.TransactionStatus = TransactionStatus.Success;
        }

        base.WriteSuccessLog("0000", "000");
        transactionBody = f.Serialize(response);
        base.TransactionEngine.MessageHeader.AddPerfInfoEntry("End of ProcessRequest");
        return transactionBody;
      }
      catch (Exception ex)
      {
        base.WriteErrorLog("0000", "000");

        var errorResponse = new ErrorResponse();
        errorResponse.TransactionStatus = TransactionStatus.Error;
        errorResponse.Message = "An exception occurred processing the CheckForUpdates web service request.";
        errorResponse.Exception = ex;
        transactionBody = f.Serialize(errorResponse);
        base.TransactionEngine.MessageHeader.AddPerfInfoEntry("End of ProcessRequest");
        return transactionBody;
      }
    }

    private SoftwareUpdatesForModuleVersion GetUpdateForModuleVersion(List<SoftwareUpdatesForModuleVersion> list, CheckForUpdatesRequest request)
    {
      Dictionary<int, SoftwareUpdatesForModuleVersion> softwareUpdates = new Dictionary<int, SoftwareUpdatesForModuleVersion>();
      foreach(var softwareUpdate in list)
        softwareUpdates.Add(softwareUpdates.Count, softwareUpdate);

      List<int> keysToRemove = new List<int>();
      string requestOsToken = request.CurrentPlatformString.ToTokenArray(Constants.CaretDelimiter).ElementAtOrDefault(0);
      string requestBitsToken = request.CurrentPlatformString.ToTokenArray(Constants.CaretDelimiter).ElementAtOrDefault(1);
      string requestSpToken = request.CurrentPlatformString.ToTokenArray(Constants.CaretDelimiter).ElementAtOrDefault(2);
      string requestFxToken = request.CurrentPlatformString.ToTokenArray(Constants.CaretDelimiter).ElementAtOrDefault(3);

      if (requestOsToken.IsNotBlank())
        requestOsToken = requestOsToken.ToUpper();
      if (requestBitsToken.IsNotBlank())
        requestBitsToken = requestBitsToken.ToUpper();
      if (requestSpToken.IsNotBlank())
        requestSpToken = requestSpToken.ToUpper();
      if (requestFxToken.IsNotBlank())
        requestFxToken = requestFxToken.ToUpper();

      // remove by OS
      foreach(var kvp in softwareUpdates)
      {
        string itemPlatformString = kvp.Value.SoftwarePlatformString;
        string osToken = itemPlatformString.ToTokenArray(Constants.CaretDelimiter).ElementAtOrDefault(0);
        if (osToken.IsNotBlank())
        {
          if (osToken.ToUpper() != requestOsToken)
            keysToRemove.Add(kvp.Key);
        }
      }

      foreach(int keyToRemove in keysToRemove)
        softwareUpdates.Remove(keyToRemove);

      if (softwareUpdates.Count == 0)
        return null;

      // reinitialize and remove elements based on os bits
      keysToRemove.Clear();

      foreach(var kvp in softwareUpdates)
      {
        string itemPlatformString = kvp.Value.SoftwarePlatformString;
        string bitsToken = itemPlatformString.ToTokenArray(Constants.CaretDelimiter).ElementAtOrDefault(1);
        if (bitsToken.IsNotBlank())
        {
          bitsToken = bitsToken.ToUpper();
          if (bitsToken != requestBitsToken && bitsToken != "#")
            keysToRemove.Add(kvp.Key);
        }
      }

      foreach(int keyToRemove in keysToRemove)
        softwareUpdates.Remove(keyToRemove);

      if (softwareUpdates.Count == 0)
        return null;

      // reinitialize and remove elements based on service pack
      keysToRemove.Clear();

      foreach(var kvp in softwareUpdates)
      {
        string itemPlatformString = kvp.Value.SoftwarePlatformString;
        string spToken = itemPlatformString.ToTokenArray(Constants.CaretDelimiter).ElementAtOrDefault(2);
        if (spToken.IsNotBlank())
        {
          spToken = spToken.ToUpper();
          if (spToken != requestSpToken && spToken != "#")
            keysToRemove.Add(kvp.Key);
        }
      }

      foreach(int keyToRemove in keysToRemove)
        softwareUpdates.Remove(keyToRemove);

      if (softwareUpdates.Count == 0)
        return null;

      // reinitialize and remove elements based on .net framework version
      keysToRemove.Clear();

      foreach(var kvp in softwareUpdates)
      {
        string itemPlatformString = kvp.Value.SoftwarePlatformString;
        string fxToken = itemPlatformString.ToTokenArray(Constants.CaretDelimiter).ElementAtOrDefault(3);
        if (fxToken.IsNotBlank())
        {
          fxToken = fxToken.ToUpper();
          if (fxToken != requestFxToken && fxToken != "#")
            keysToRemove.Add(kvp.Key);
        }
      }

      foreach(int keyToRemove in keysToRemove)
        softwareUpdates.Remove(keyToRemove);

      if (softwareUpdates.Count == 0)
        return null;

      return softwareUpdates.Values.FirstOrDefault();
    }

    ~CheckForUpdatesProcessor()
    {
      g.LogToMemory("CheckForUpdatesProcessor Destructor");
      Dispose(false);
    }
  }
}
