using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.Software.Data.Entities;

namespace Org.Software.Business
{
  public class ReferenceManager
  {
    public Assembly BusinessObjectAssembly {
      get {
        return Get_BusinessObjectAssembly();
      }
    }
    public Assembly DataAssembly {
      get {
        return Get_DataAssembly();
      }
    }

    private Assembly Get_BusinessObjectAssembly()
    {
      return Assembly.GetExecutingAssembly();
    }

    private Assembly Get_DataAssembly()
    {
      return Assembly.GetAssembly(typeof(Org_SoftwareEntities));
    }
  }
}
