using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using System.Collections;

namespace Org.GS.AppDomainManagement
{
  public class AppDomainObjectRegistry : MarshalByRefObject, ISponsor
  {

    private Dictionary<string, AppDomainObjectDescriptor> _registeredObjects {
      get;
      set;
    }
    public IReadOnlyDictionary<string, AppDomainObjectDescriptor> RegisteredObjects {
      get {
        return _registeredObjects;
      }
    }

    public AppDomainObjectRegistry()
    {
      _registeredObjects = new Dictionary<string, AppDomainObjectDescriptor>();
    }

    public void Clear()
    {
      _registeredObjects = new Dictionary<string, AppDomainObjectDescriptor>();
    }

    public void ValidateAppDomainObjectDescriptor(AppDomainObjectDescriptor appDomainObjectDescriptor, List<string> existingAppDomainNames)
    {
      if (appDomainObjectDescriptor == null)
        throw new Exception("The AppDomainObjectDescriptor is null.");

      if (appDomainObjectDescriptor.AppDomainSetup == null)
        throw new Exception("The AppDomainSetup property of the AppDomainObjectDescriptor is null.");

      if (appDomainObjectDescriptor.ObjectRegistryName.IsBlank())
        throw new Exception("The ObjectRegistryName property of the AppDomainObjectDescriptor is blank or null.");

      if (appDomainObjectDescriptor.ObjectAssemblyName.IsBlank())
        throw new Exception("The ObjectAssemblyName property of the AppDomainObjectDescriptor is blank or null.");

      if (appDomainObjectDescriptor.ObjectTypeName.IsBlank())
        throw new Exception("The ObjectTypeName property of the AppDomainObjectDescriptor is blank or null.");

      if (appDomainObjectDescriptor.AppDomainSetup.ApplicationBase == null)
        throw new Exception("The ApplicationBase property of the AppDomainObjectDescriptor.AppDomainSetup object is null.");

      // Ensure that the object registry name is unique within the object registry
      string objectRegistryKey = appDomainObjectDescriptor.ObjectRegistryName.ToUpper().Trim();
      if (this.RegisteredObjects.ContainsKey(objectRegistryKey))
        throw new Exception("An object named '" + appDomainObjectDescriptor.ObjectRegistryName + "' already exists in the object registry.");

      // Ensure that the specified friendly name for the AppDomain is unique within the process.
      if (existingAppDomainNames.ToLowerCaseAndTrim().Contains(appDomainObjectDescriptor.AppDomainFriendlyName.ToLower()))
        throw new Exception("The new AppDomain cannot be created with the friendly name '" + appDomainObjectDescriptor.AppDomainFriendlyName + "', " +
                            "the process already contains an AppDomain with the same name.");

      // Ensure that a valid ApplicationBase is configured - the AppDomain will look here for assemblies.
      if (!Directory.Exists(appDomainObjectDescriptor.AppDomainSetup.ApplicationBase))
        throw new Exception("The ApplicationBase (assembly path) configured in the AppDomainSetup object does not point to a valid directory - '" +
                            appDomainObjectDescriptor.AppDomainSetup.ApplicationBase + "'.");

      // Ensure that the specified assembly exists in the ApplicationBase folder
      string assemblyFullPath = appDomainObjectDescriptor.AppDomainSetup.ApplicationBase + @"\" + appDomainObjectDescriptor.ObjectAssemblyName + ".dll";
      if (!File.Exists(assemblyFullPath))
        throw new Exception("No file named '" + appDomainObjectDescriptor.ObjectAssemblyName + ".dll" + "' exists in the ApplicationBase path configured in the AppDomainSetup " +
                            "object which is '" + appDomainObjectDescriptor.AppDomainSetup.ApplicationBase + "'.");

      // Ensure that the Org.GS assembly exists in the ApplicationBase (assembly path).
      if (!File.Exists(appDomainObjectDescriptor.AppDomainSetup.ApplicationBase + @"\Org.GS.dll"))
        throw new Exception("The required 'Org.GS' assembly does not exist in the AppDomain.ApplicationBase folder '" + appDomainObjectDescriptor.AppDomainSetup.ApplicationBase + "'.");
    }

    ~AppDomainObjectRegistry()
    {
      UnregisterAll();
    }

    TimeSpan ISponsor.Renewal(ILease lease)
    {
      return TimeSpan.Zero;
    }

    public void Register(AppDomainObjectDescriptor adod)
    {
      try
      {
        if (adod.Object == null)
          throw new Exception("AppDomainObjectDescriptor's Object property is NULL.");

        var marshalObj = adod.Object as MarshalBase;

        if (marshalObj == null)
          throw new Exception("AppDomainObjectDescriptor's Object is not of type 'MarshalBase'.");

        adod.Lease = marshalObj.Lease;

        if (adod.Lease.CurrentState != LeaseState.Active)
          throw new Exception("Failed to register Object. The LeaseState for the provided object is not active.");

        adod.Lease.Register(this);

        if (this.RegisteredObjects.ContainsKey(adod.ObjectRegistryName.Trim().ToUpper()))
          throw new Exception("The ObjectRegistryName '" + adod.ObjectRegistryName + "' already exists in the object registry.");

        _registeredObjects.Add(adod.ObjectRegistryName.Trim().ToUpper(), adod);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to register the object named '" + adod.ObjectRegistryName +
                            "' in the AppDomainSupervisor object registry.", ex);
      }
    }

    //public void Unregister(MarshalByRefObject obj)
    //{
    //  ILease lease = (ILease)RemotingServices.GetLifetimeService(obj);

    //  if (lease.CurrentState != LeaseState.Active)
    //    throw new Exception("Failed to unregister Object. The Lease State for the provided object is not active.");

    //  lease.Unregister(this);
    //  lock (this)
    //  {
    //    LeaseList.Remove(lease);
    //  }
    //}

    public void UnregisterAll()
    {
      lock (this)
      {
        foreach (var obj in RegisteredObjects.Values)
          obj.Lease.Unregister(this);

        _registeredObjects.Clear();
      }
    }
  }
}
