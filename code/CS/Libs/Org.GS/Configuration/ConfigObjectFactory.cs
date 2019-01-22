using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS.Configuration
{
  public class ConfigObjectFactory
  {
    public static ConfigObjectBase CreateConfigObject(Type objectType)
    {
      switch (objectType.Name)
      {
        case "ConfigWsSpec":
          return new ConfigWsSpec();

        case "ConfigDbSpec":
          return new ConfigDbSpec();

        case "ConfigSmtpSpec":
          return new ConfigSmtpSpec();

        case "ConfigSyncSpec":
          return new ConfigSyncSpec();

        case "ConfigLogSpec":
          return new ConfigLogSpec();

        case "ConfigNotifySpec":
          return new ConfigNotifySpec();
      }

      throw new Exception("ConfigObjectFactory cannot create an object of type '" + objectType.Name + "'.");
    }

    public static ConfigObjectBase CreateConfigObject(Type objectType, string namingPrefix)
    {
      switch (objectType.Name)
      {
        case "ConfigWsSpec":
          return new ConfigWsSpec(namingPrefix);

        case "ConfigDbSpec":
          return new ConfigDbSpec(namingPrefix);

        case "ConfigSmtpSpec":
          return new ConfigSmtpSpec(namingPrefix);

        case "ConfigSyncSpec":
          return new ConfigSyncSpec(namingPrefix);

        case "ConfigLogSpec":
          return new ConfigLogSpec(namingPrefix);

        case "ConfigNotifySpec":
          return new ConfigNotifySpec(namingPrefix);

        case "ConfigFtpSpec":
          return new ConfigFtpSpec(namingPrefix);
      }

      throw new Exception("ConfigObjectFactory cannot create an object of type '" + objectType.Name + "'.");
    }
  }
}
