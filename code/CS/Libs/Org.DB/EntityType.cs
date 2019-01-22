using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Org.DB
{
  public class EntityType
  {
    public Type TypeOfEntity {
      get;
      set;
    }
    public PropertyInfo DbSetPI {
      get;
      set;
    }
    public MethodInfo DbSetMI {
      get;
      set;
    }
  }
}
