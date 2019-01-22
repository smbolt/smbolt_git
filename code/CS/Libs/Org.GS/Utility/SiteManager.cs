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
  [Serializable]
  public class SiteManager : IDisposable
  {
    private bool _isDryRun;

    public SiteManager(bool isDryRun = false)
    {
      _isDryRun = isDryRun;
    }

    public WebSiteSet GetWebSites()
    {
      try
      {
        WebSiteSet webSiteSet = new WebSiteSet();
        ManagementObjectCollection moSet = GetWebSiteList();

        foreach (ManagementObject mo in moSet)
        {
          WebSite webSite = new WebSite();
          webSite.Name = mo.Properties["Name"].Value.ToString();
          webSite.Id = Convert.ToInt32(mo.Properties["Id"].Value);
          UInt32 state = (UInt32)mo.InvokeMethod("GetState", new object[] { });
          webSite.WebSiteStatus = state.ToEnum<WebSiteStatus>(WebSiteStatus.Unknown);
          webSiteSet.Add(webSite);
        }

        return webSiteSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while trying to get a list of Web Sites.", ex);
      }
    }

    public WebSite GetWebSite(string siteName)
    {
      try
      {
        WebSite webSite = new WebSite();
        ManagementObject mo = GetWebSiteByName(siteName);

        if (mo == null)
          return null;

        webSite.Name = mo.Properties["Name"].Value.ToString();
        webSite.Id = Convert.ToInt32(mo.Properties["Id"].Value);
        UInt32 state = (UInt32)mo.InvokeMethod("GetState", new object[] { });
        webSite.WebSiteStatus = state.ToEnum<WebSiteStatus>(WebSiteStatus.Unknown);
        return webSite;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while trying to get Web Site '" + siteName + "'.", ex);
      }
    }

    public TaskResult StartStopWebSite(string siteName, SiteCommand command)
    {
      TaskResult taskResult = new TaskResult(command.ToString() + "WebSite");
      try
      {
        WebSiteStatus desiredStatus = command == SiteCommand.Start ? WebSiteStatus.Started : WebSiteStatus.Stopped;

        ManagementObject mo = GetWebSiteByName(siteName);

        if (mo == null)
          return taskResult.Failed("Failed to locate the web site based on the name '" + siteName + "'.", 6103);

        WebSite webSite = new WebSite();
        webSite.Name = mo.Properties["Name"].Value.ToString();
        webSite.Id = Convert.ToInt32(mo.Properties["Id"].Value);
        UInt32 state = (UInt32)mo.InvokeMethod("GetState", new object[] { });
        webSite.WebSiteStatus = state.ToEnum<WebSiteStatus>(WebSiteStatus.Unknown);
        taskResult.Object = webSite;

        if (desiredStatus == webSite.WebSiteStatus)
          return taskResult.Success("Web site '" + siteName + "' is already " + desiredStatus.ToString() + ".");

        if (_isDryRun)
          return taskResult.Success("Web Site '" + siteName + "' was ready to " + command.ToString() + ", but returned because the command was a DryRun.");
        mo.InvokeMethod(command.ToString(), new object[] {});

        webSite.WebSiteStatus = ((UInt32)mo.InvokeMethod("GetState", new object[] { })).ToEnum<WebSiteStatus>(WebSiteStatus.Unknown);

        if (desiredStatus != webSite.WebSiteStatus)
          return taskResult.Failed("Failed to " + command.ToString() + " web site '" + siteName + "'.", 6104);

        return taskResult.Success("Web site '" + siteName + "' was succesfully " + webSite.WebSiteStatus.ToString() + ".");
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred while attempting to " + command.ToString() + " the web site named '" + siteName + "'.", ex);
      }
    }

    public TaskResult GetWebSiteStatus(string siteName)
    {
      TaskResult taskResult = new TaskResult("GetWebSiteStatus");
      try
      {
        ManagementObject mo = GetWebSiteByName(siteName);

        if (mo == null)
          return taskResult.Failed("Web Site '" + siteName + "' does not exist.");

        WebSite webSite = new WebSite();
        webSite.Name = mo.Properties["Name"].Value.ToString();
        webSite.Id = Convert.ToInt32(mo.Properties["Id"].Value);
        UInt32 state = (UInt32)mo.InvokeMethod("GetState", new object[] { });
        webSite.WebSiteStatus = state.ToEnum<WebSiteStatus>(WebSiteStatus.Unknown);
        taskResult.Object = webSite;

        if (webSite.WebSiteStatus == WebSiteStatus.NotSet || webSite.WebSiteStatus == WebSiteStatus.Unknown)
          return taskResult.Failed("Could no determine status for Web Site '" + siteName + "'.");

        return taskResult.Success("Web Site '" + siteName + "' is '" + webSite.WebSiteStatus + "'.");
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the status of the web site named '" + siteName + "'.", ex);
      }
    }

    private ManagementObject GetWebSiteByName(string siteName)
    {
      try
      {
        ManagementScope scope = new ManagementScope(@"\\.\root\WebAdministration");
        SelectQuery query = new SelectQuery("select * from Site where Name = '" + siteName + "'");

        using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
        {
          scope.Connect();
          ManagementObjectCollection moSet = searcher.Get();

          if (moSet.Count == 0)
            return null;
          else if (moSet.Count > 1)
            throw new Exception("More than one WebSite has the name '" + siteName + "'.");

          return moSet.OfType<ManagementObject>().FirstOrDefault();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get a WMI ManagementObject for web site by name for '" + siteName + "'.", ex);
      }
    }

    private ManagementObject GetWebSiteById(int siteId)
    {
      try
      {
        ManagementObjectCollection moSet = GetWebSiteList();
        foreach (ManagementObject mo in moSet)
        {
          if (mo.Properties["Id"].Value.ToInt32() == siteId)
            return mo;
        }
        return null;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get a WMI ManagementObject for web site by Id for '" + siteId.ToString() + "'.", ex);
      }
    }

    private ManagementObjectCollection GetWebSiteList()
    {
      try
      {
        ManagementScope scope = new ManagementScope(@"\\.\root\WebAdministration");
        SelectQuery query = new SelectQuery("select * from Site");

        using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
        {
          scope.Connect();
          ManagementObjectCollection moSet = searcher.Get();
          return moSet;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to get a WMI ManagementObjectCollection of web sites.", ex);
      }
    }

    public void Dispose()
    {
    }
  }
}
