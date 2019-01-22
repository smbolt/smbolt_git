using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.DB
{
  public class EntityModelMap
  {
    public string Name {
      get;
      set;
    }
    public Type EntityType {
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
    public Type ModelType {
      get;
      set;
    }
    public PropertyInfoPairSet PropertyInfoPairSet {
      get;
      set;
    }
    public bool PropertiesLoaded {
      get;
      set;
    }

    public EntityModelMap(string name, Type entityType, Type modelType, PropertyInfo dbSetPI, MethodInfo dbSetMI)
    {
      this.Name = name;
      this.EntityType = entityType;
      this.DbSetPI = dbSetPI;
      this.DbSetMI = dbSetMI;
      this.ModelType = modelType;
      this.PropertyInfoPairSet = new PropertyInfoPairSet();
      this.PropertiesLoaded = false;
    }
  }
}