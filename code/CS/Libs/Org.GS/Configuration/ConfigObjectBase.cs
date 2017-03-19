using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS.Configuration
{
  public class ConfigObjectBase
  {
    public string ConfigProgram { get; set; }
    public string NamingPrefix { get; set; }
    public virtual bool IsUpdated { get { return false; } }

    public string ConfigGroup
    {
      get { return this.NamingPrefix.Trim() + this.GetType().Name; }
    }

    public ConfigObjectBase() 
    {
      this.ConfigProgram = String.Empty;
      this.NamingPrefix = String.Empty;
    }

    public ConfigObjectBase(string namingPrefix)
    {
      this.ConfigProgram = String.Empty;
      this.NamingPrefix = namingPrefix;
    }

    public virtual void SetOriginalProperties() { }
  }
}
