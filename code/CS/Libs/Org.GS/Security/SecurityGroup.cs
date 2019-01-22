using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Security
{
  public class SecurityGroup
  {
    public string LogicalGroupName {
      get;
      set;
    }
    public List<string> IncludeGroups {
      get;
      set;
    }
    public List<string> ExcludeGroups {
      get;
      set;
    }
    public List<string> IncludeUsers {
      get;
      set;
    }
    public List<string> ExcludeUsers {
      get;
      set;
    }

    public SecurityGroup(string logicalGroupName)
    {
      this.LogicalGroupName = logicalGroupName;
      this.IncludeGroups = new List<string>();
      this.ExcludeGroups = new List<string>();
      this.IncludeUsers = new List<string>();
      this.ExcludeUsers = new List<string>();
    }
  }
}
