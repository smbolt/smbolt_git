using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "ProgramRole")]
  public class ProgramRoleSet : SortedList<int, ProgramRole>
  {
    public ProgramRole GetRoleByName(string name)
    {
      foreach (var role in this.Values)
      {
        if (role.OrgRoleName == name)
          return role;
      }

      return null;
    }
  }
}
