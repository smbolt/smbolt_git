using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS.Configuration
{
  public class ConfigItemProperty
  {
    public string PropertyName {
      get;
      set;
    }
    public Type PropertyType {
      get;
      set;
    }

    public ConfigItemProperty()
    {
      PropertyName = String.Empty;
      PropertyType = Type.GetType("System.Object");
    }
  }
}
