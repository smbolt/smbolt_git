using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.GS
{
  public class PropertyInfoPair
  {
    public PropertyInfo EntityPropertyInfo {
      get;
      set;
    }
    public PropertyInfo ModelPropertyInfo {
      get;
      set;
    }
    public MappingRule MappingRule {
      get;
      set;
    }
    public bool IsDbNullable {
      get;
      set;
    }
    public bool IsPrimaryKey {
      get;
      set;
    }

    public PropertyInfoPair(PropertyInfo entityPropertyInfo, PropertyInfo modelPropertyInfo, bool isDbNullable, bool isPrimaryKey, MappingRule mappingRule)
    {
      this.EntityPropertyInfo = entityPropertyInfo;
      this.ModelPropertyInfo = modelPropertyInfo;
      this.MappingRule = mappingRule;
      this.IsDbNullable = isDbNullable;
      this.IsPrimaryKey = isPrimaryKey;
    }
  }
}
