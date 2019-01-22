using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace Org.GS.AppDomainManagement
{
  public interface IAppDomainUtility
  {
    string AppDomainFriendlyName { get; }

    string GetAssemblyReport();
    Assembly GetRootAssembly();
  }
}
