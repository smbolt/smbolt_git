using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Org.GS.PlugIn;
using Org.GS;
using mscoree;

namespace Org.GS.AppDomainManagement
{
  public class AppDomainSupervisor : IDisposable
  {
    public AppDomainInfo RootAppDomain {
      get;
      private set;
    }
    public List<string> AppDomainNames {
      get {
        return Get_AppDomainNames();
      }
    }
    public AppDomainObjectRegistry _objectRegistry;
    public string Report {
      get {
        return Get_Report();
      }
    }

    public AppDomainSupervisor()
    {
      try
      {
        _objectRegistry = new AppDomainObjectRegistry();

        this.RootAppDomain = GetRootAppDomain();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the constuctor of AppDomainManager.", ex);
      }
    }

    public object GetObject(AppDomainObjectDescriptor adod)
    {
      MarshalBase theObject = null;
      System.Runtime.Remoting.Lifetime.ILease lease = null;

      try
      {
        string objectRegistryKey = adod.ObjectRegistryName.Trim().ToUpper();

        AppDomain appDomain = this.GetAppDomain(adod.AppDomainFriendlyName);

        if (appDomain == null)
          appDomain = CreateAppDomain(adod);

        if (_objectRegistry.RegisteredObjects.ContainsKey(objectRegistryKey))
        {
          if (_objectRegistry.RegisteredObjects[objectRegistryKey] == null)
            throw new Exception("The Object for Registry Key '" + objectRegistryKey + "' is NULL.");

          theObject = _objectRegistry.RegisteredObjects[objectRegistryKey].Object as MarshalBase;

          try
          {
            theObject.TestExistence();
            return theObject;
          }
          catch (Exception ex)
          {
            if (ex.Message.Contains("has been disconnected"))
              return appDomain.CreateInstanceAndUnwrap(adod.ObjectAssemblyName, adod.ObjectTypeName);

            throw new Exception("An exception occurred while attempting to retrieve an object of type '" + adod.ObjectTypeName +
                                " from the AppDomain named '" + adod.AppDomainFriendlyName + "'.");
          }
        }

        theObject = appDomain.CreateInstanceAndUnwrap(adod.ObjectAssemblyName, adod.ObjectTypeName) as MarshalBase;
        lease = theObject.Lease;

        return appDomain.CreateInstanceAndUnwrap(adod.ObjectAssemblyName, adod.ObjectTypeName);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get an object from the AppDomainSupervisor. The AppDomainObjectDescriptorValues are " +
                            adod.ToReport() + ".", ex);
      }
    }

    public AppDomain GetAppDomain(string friendlyName)
    {
      try
      {
        foreach (AppDomainInfo appDomainInfo in this.RootAppDomain.AppDomainInfoSet.Values)
        {
          if (appDomainInfo.AppDomain.FriendlyName.Trim().ToUpper() == friendlyName.Trim().ToUpper())
            return appDomainInfo.AppDomain;
        }

        return null;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while trying to retrieve the AppDomain named '" + friendlyName + "'.", ex);
      }
    }

    public AppDomain CreateAppDomain(AppDomainObjectDescriptor appDomainObjectDescriptor)
    {
      try
      {
        var currentAppDomainNameList = this.Get_AppDomainNames();
        _objectRegistry.ValidateAppDomainObjectDescriptor(appDomainObjectDescriptor, currentAppDomainNameList);

        string parentAppDomainFriendlyName = AppDomain.CurrentDomain.FriendlyName;
        AppDomain appDomain = AppDomain.CreateDomain(appDomainObjectDescriptor.AppDomainFriendlyName, null, appDomainObjectDescriptor.AppDomainSetup);
        appDomain.SetData("ParentAppDomainAppType", g.AppInfo.OrgApplicationType);
        appDomain.SetData("ParentAppDomainConfigName", g.AppInfo.ConfigName);
        var theObject = appDomain.CreateInstanceAndUnwrap(appDomainObjectDescriptor.ObjectAssemblyName, appDomainObjectDescriptor.ObjectTypeName);

        if (theObject == null)
          throw new Exception("Could not create object of type + '" + appDomainObjectDescriptor.ObjectTypeName + "' from assembly '" + appDomainObjectDescriptor.ObjectAssemblyName + "'.");

        appDomainObjectDescriptor.AppDomain = appDomain;
        appDomainObjectDescriptor.Object = theObject;

        _objectRegistry.Register(appDomainObjectDescriptor);

        var appDomainUtility = (IAppDomainUtility)(appDomain.CreateInstanceAndUnwrap("Org.GS", "Org.GS.AppDomainManagement.AppDomainUtility"));

        string appDomainFriendlyName = appDomainUtility.AppDomainFriendlyName;
        appDomain.SetData("ParentAppDomainFriendlyName", parentAppDomainFriendlyName);
        appDomain.SetData("AppDomainObjectDescriptor", appDomainObjectDescriptor);

        string assemblyReport = appDomainUtility.GetAssemblyReport();

        this.Refresh();
        return appDomain;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create a new AppDomain named '" +
                            appDomainObjectDescriptor.AppDomainFriendlyName + "'.", ex);
      }
    }

    public void FlushAppDomains()
    {
      try
      {
        if (this.RootAppDomain == null)
          throw new Exception("The RootAppDomain object is null.");

        var unloadedAppDomainKeys = new List<string>();

        foreach (var childAppDomain in this.RootAppDomain.AppDomainInfoSet)
        {
          if (childAppDomain.Value.AppDomain != null && !childAppDomain.Value.AppDomain.FriendlyName.ToUpper().Contains("/W3SVC/"))
          {
            AppDomain.Unload(childAppDomain.Value.AppDomain);
            unloadedAppDomainKeys.Add(childAppDomain.Key);
          }
        }

        foreach (string unloadedAppDomainKey in unloadedAppDomainKeys)
        {
          this.RootAppDomain.AppDomainInfoSet.Remove(unloadedAppDomainKey);
        }

        this._objectRegistry.Clear();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to flush all AppDomains.", ex);
      }
    }

    public void Refresh()
    {
      try
      {
        this.RootAppDomain = GetRootAppDomain();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to refresh the AppDomainManager.", ex);
      }
    }

    private AppDomainInfo GetRootAppDomain()
    {
      try
      {
        AppDomainInfo appDomainInfo = null;

        var appDomains = GetAppDomains();

        if (appDomains.Count == 0)
          return null;

        foreach (AppDomain appDomain in appDomains)
        {
          if (appDomain.IsTheDefaultAppDomain())
          {
            appDomainInfo = new AppDomainInfo(appDomain, null);
            appDomainInfo.FriendlyName = appDomain.FriendlyName;
            break;
          }
        }

        appDomains.Remove(appDomainInfo.AppDomain);

        foreach (var childAppDomain in appDomains)
        {
          var childAppDomainInfo = new AppDomainInfo(childAppDomain, appDomainInfo);
          childAppDomainInfo.FriendlyName = childAppDomain.FriendlyName;

          int seq = 0;
          string appDomainKey = childAppDomainInfo.FriendlyName + "-" + seq.ToString("000");

          while (appDomainInfo.AppDomainInfoSet.ContainsKey(appDomainKey))
          {
            seq++;
            appDomainKey = childAppDomainInfo.FriendlyName + "-" + seq.ToString("000");
          }

          foreach (var registeredObject in _objectRegistry.RegisteredObjects.Values)
          {
            if (registeredObject.AppDomainFriendlyName.Trim().ToUpper() == childAppDomainInfo.FriendlyName.Trim().ToUpper())
              childAppDomainInfo.RegisteredObjects.Add(registeredObject.ObjectRegistryName, registeredObject);
          }

          appDomainInfo.AppDomainInfoSet.Add(appDomainKey, childAppDomainInfo);
        }

        return appDomainInfo;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to retrieve AppDomain information.", ex);
      }
    }

    private List<AppDomain> GetAppDomains()
    {
      List<AppDomain> appDomains = new List<AppDomain>();
      IntPtr enumHandle = IntPtr.Zero;

      ICorRuntimeHost host = new mscoree.CorRuntimeHost();

      try
      {
        host.EnumDomains(out enumHandle);
        object domain = null;
        while (true)
        {
          host.NextDomain(enumHandle, out domain);
          if (domain == null) break;
          AppDomain appDomain = (AppDomain)domain;
          appDomains.Add(appDomain);
        }

        return appDomains;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to retrieve a list of AppDomains.", ex);
      }
      finally
      {
        host.CloseEnum(enumHandle);
        Marshal.ReleaseComObject(host);
      }
    }

    private List<string> Get_AppDomainNames()
    {
      try
      {
        var appDomainNames = new List<string>();

        if (this.RootAppDomain == null)
          this.RootAppDomain = GetRootAppDomain();

        if (this.RootAppDomain == null)
          return appDomainNames;

        appDomainNames.Add(this.RootAppDomain.FriendlyName);

        foreach (var appDomainInfo in this.RootAppDomain.AppDomainInfoSet.Values)
        {
          appDomainNames.Add(appDomainInfo.AppDomain.FriendlyName);
        }

        return appDomainNames;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to retrieve a list of AppDomain names.", ex);
      }
    }

    private string Get_Report()
    {
      try
      {
        this.Refresh();

        StringBuilder sb = new StringBuilder();

        sb.Append("AppDomain Report" + g.crlf2);
        sb.Append("*******************************************************************************************************" + g.crlf);
        sb.Append("Root AppDomain  Friendly Name: " + this.RootAppDomain.FriendlyName + g.crlf);
        sb.Append("*******************************************************************************************************" + g.crlf);
        sb.Append(this.RootAppDomain.Report + g.crlf2);

        if (this.RootAppDomain.AppDomainInfoSet.Count == 0)
          sb.Append(g.crlf2 + "No child AppDomains exist." + g.crlf2);

        foreach (var kvpChildAppDomain in this.RootAppDomain.AppDomainInfoSet)
        {
          string key = kvpChildAppDomain.Key;
          var childAppDomainInfo = kvpChildAppDomain.Value;

          sb.Append("*******************************************************************************************************" + g.crlf);
          sb.Append("Child AppDomain  Friendly Name: " + childAppDomainInfo.FriendlyName + g.crlf);
          sb.Append("*******************************************************************************************************" + g.crlf);
          sb.Append(childAppDomainInfo.Report + g.crlf2);
        }

        string report = sb.ToString();
        return report;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create the AppDomainSupervisor.Report.", ex);
      }
    }

    public void Dispose()
    {
    }
  }
}
