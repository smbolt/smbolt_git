using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Configuration;

namespace Org.Notify
{
  public class NotifyConfigHelper
  {
    private static ConfigDbSpec _notifyDbSpec;

    public static NotifyConfigSets GetNotifyConfigs(NotifyConfigMode notifyConfigMode)
    {
      try
      {
        switch (notifyConfigMode)
        {
          case NotifyConfigMode.AppConfig:
            var ncs = g.AppConfig.GetNotifyConfigSet();
            if (ncs != null)
            {
              var notifyConfigs = new NotifyConfigSets();
              notifyConfigs.Add(ncs.Name, ncs);
              return notifyConfigs;
            }
            return null;

          case NotifyConfigMode.Database:
            return GetNotifyConfigsFromDb();

          default:
            return null;
        }
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the NotifyConfigs.", ex);
      }
    }

    private static NotifyConfigSets GetNotifyConfigsFromDb()
    {
      try
      {
        if (_notifyDbSpec == null)
          _notifyDbSpec = GetNotifyConfigDbSpec();

        var notifyConfigSets = new NotifyConfigSets();

        using (var repo = new NotifyRepository(_notifyDbSpec))
        {
          var notifyConfigSetList = repo.GetNotifyConfigSets(true);

          foreach (var notifyConfigSet in notifyConfigSetList)
          {
            if (!notifyConfigSets.ContainsKey(notifyConfigSet.Name))
              notifyConfigSets.Add(notifyConfigSet.Name, notifyConfigSet);
          }
        }

        return notifyConfigSets;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the NotifyConfigs from the database.", ex); 
      }
    }

    public static void SetNotifyConfigDbSpec(ConfigDbSpec notifyConfigDbSpec)
    {
      _notifyDbSpec = notifyConfigDbSpec;
    }

    private static ConfigDbSpec GetNotifyConfigDbSpec()
    {
      string prefix = g.CI("NotifyDbSpecPrefix");
      return g.GetDbSpec(prefix); 
    }
  }
}
