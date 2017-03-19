using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;
using Org.WSO;
using Org.SoftwareTasks;

namespace Org.AppLauncher
{
  public class OrgExtention
  {
    public static void Extend()
    {
      XmlMapper.AddAssembly(Assembly.GetAssembly(typeof(Org.SoftwareTasks.Concrete.CheckForUpdatesProcessor)));      
    }

		//public static void ExtendWCFTransMap(WCFTransMap wcfTransMap)
		//{
		//	wcfTransMap.AddAssembly(Assembly.GetAssembly(typeof(Org.SoftwareTasks.Concrete.CheckForUpdatesProcessor))); 
		//}
  }
}
