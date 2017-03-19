using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.Management;
using System.Threading;

namespace Org.GS 
{
  public class AppPoolManager : IDisposable
  {
    private bool _isDryRun;

    public AppPoolManager(bool isDryRun = false)
    {
      _isDryRun = isDryRun;
    }

    public AppPoolSet GetAppPools()
    {
      try
      {
        AppPoolSet appPoolSet = new AppPoolSet();

        ManagementScope scope = new ManagementScope(@"\\.\root\WebAdministration");
        SelectQuery query = new SelectQuery("select * from ApplicationPool");

        using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
        {
          scope.Connect();
          ManagementObjectCollection moSet = searcher.Get();
          foreach (ManagementObject mo in moSet)
          {
            AppPool appPool = new AppPool();
            appPool.Name = mo.Properties["Name"].Value.ToString();
            appPool.AutoStart = mo.Properties["AutoStart"].Value.ToBoolean();
            appPool.Enable32BitAppOnWin64 = mo.Properties["Enable32BitAppOnWin64"].Value.ToBoolean();
            UInt32 state = (UInt32)mo.InvokeMethod("GetState", new object[] { });
            appPool.AppPoolStatus = state.ToEnum<AppPoolStatus>(AppPoolStatus.Unknown);
            appPoolSet.Add(appPool);
          }
        }

        return appPoolSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while trying to get a list of App Pools.", ex);
      }
    }

    public AppPool GetAppPool(string appPoolName)
    {
      try
      {
        AppPool appPool = new AppPool();
        ManagementObject mo = GetAppPoolByName(appPoolName);

        if (mo == null)
          return null;

        appPool.Name = mo.Properties["Name"].Value.ToString();
        appPool.AutoStart = mo.Properties["AutoStart"].Value.ToBoolean();
        appPool.Enable32BitAppOnWin64 = mo.Properties["Enable32BitAppOnWin64"].Value.ToBoolean();
        UInt32 state = (UInt32)mo.InvokeMethod("GetState", new object[] { });
        appPool.AppPoolStatus = state.ToEnum<AppPoolStatus>(AppPoolStatus.Unknown);
        return appPool;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while trying to get App Pool '" + appPoolName + "'.", ex);
      }
    }

    public TaskResult StartStopAppPool(string appPoolName, AppPoolCommand command)
    {
      TaskResult taskResult = new TaskResult("StartStopAppPool");
      try
      {
        AppPoolStatus desiredStatus = command == AppPoolCommand.Start ? AppPoolStatus.Started : AppPoolStatus.Stopped;

        ManagementObject mo = GetAppPoolByName(appPoolName);

        if(mo == null)
          return taskResult.Failed("Failed to locate the app pool based on the name '" + appPoolName + "'.", 6103);

        AppPool appPool = new AppPool();
        appPool.Name = mo.Properties["Name"].Value.ToString();
        appPool.AutoStart = mo.Properties["AutoStart"].Value.ToBoolean();
        appPool.Enable32BitAppOnWin64 = mo.Properties["Enable32BitAppOnWin64"].Value.ToBoolean();
        UInt32 state = (UInt32)mo.InvokeMethod("GetState", new object[] { });
        appPool.AppPoolStatus = state.ToEnum<AppPoolStatus>(AppPoolStatus.Unknown);
        taskResult.Object = appPool;

        if (desiredStatus == appPool.AppPoolStatus)
          return taskResult.Success("App pool '" + appPoolName + "' is already " + desiredStatus.ToString() + ".");

        if (_isDryRun)
          return taskResult.Success("App pool '" + appPoolName + "' was ready to " + command.ToString() + ", but returned because the command was a DryRun.");
        mo.InvokeMethod(command.ToString(), new object[] {});

        appPool.AppPoolStatus = ((UInt32)mo.InvokeMethod("GetState", new object[] { })).ToEnum<AppPoolStatus>(AppPoolStatus.Unknown);

        if (desiredStatus != appPool.AppPoolStatus)
          return taskResult.Failed("Failed to " + command.ToString() + " app pool '" + appPoolName + "'.", 6104);

        return taskResult.Success("App pool '" + appPoolName + "' was successfully " + appPool.AppPoolStatus.ToString() + ".");
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting  to " + command.ToString() + " the app pool named " + appPoolName + "'.", ex);
      }
    }

    public TaskResult GetAppPoolStatus(string appPoolName)
    {
      TaskResult taskResult = new TaskResult("GetAppPoolStatus");
      try
      {
        ManagementObject mo = GetAppPoolByName(appPoolName);

        if (mo == null)
          return taskResult.Failed("Failed to locate the app pool based on the name '" + appPoolName + "'.", 6103);

        AppPool appPool = new AppPool();
        appPool.Name = mo.Properties["Name"].Value.ToString();
        appPool.AutoStart = mo.Properties["AutoStart"].Value.ToBoolean();
        appPool.Enable32BitAppOnWin64 = mo.Properties["Enable32BitAppOnWin64"].Value.ToBoolean();
        UInt32 state = (UInt32)mo.InvokeMethod("GetState", new object[] { });
        appPool.AppPoolStatus = state.ToEnum<AppPoolStatus>(AppPoolStatus.Unknown);
        taskResult.Object = appPool;

        if (appPool.AppPoolStatus == AppPoolStatus.NotSet || appPool.AppPoolStatus == AppPoolStatus.Unknown)
          return taskResult.Failed("Could no determine status for App Pool '" + appPoolName + "'.");

        return taskResult.Success("App Pool '" + appPoolName + "' is '" + appPool.AppPoolStatus + "'.");
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting  to get status of App Pool '" + appPoolName + "'.", ex);
      }
    }

    private ManagementObject GetAppPoolByName(string appPoolName)
    {
      try
      {
        ManagementScope scope = new ManagementScope(@"\\.\root\WebAdministration");
        SelectQuery query = new SelectQuery("select * from ApplicationPool where Name = '" + appPoolName + "'");

        using(ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
        {
          scope.Connect();
          ManagementObjectCollection moSet = searcher.Get();

          if (moSet.Count == 0)
            return null;
          else if (moSet.Count > 1)
            throw new Exception("More than one App Pool has the name '" + appPoolName + "'.");

          return moSet.OfType<ManagementObject>().FirstOrDefault();
        }
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting to get a WMI ManagementObject for app pool by name for '" + appPoolName + "'.", ex);
      }
    }

    public void Dispose()
    {
    }
  }
}
