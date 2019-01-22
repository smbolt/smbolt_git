using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.AppDomainManagement
{
  public static class ExtensionMethods
  {

    public static bool IsTheDefaultAppDomain(this AppDomain appDomain)
    {
      if (appDomain.IsDefaultAppDomain())
        return true;

      if (appDomain?.SetupInformation?.ConfigurationFile == null)
        return false;

      if (appDomain.SetupInformation.ConfigurationFile.ToLower().EndsWith("web.config"))
        return true;

      return false;
    }
  }
}
