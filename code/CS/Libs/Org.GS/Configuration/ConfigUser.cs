using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  public enum UserType
  {
    NotSet,
    SecurityClass,
    Person
  }

  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class ConfigUser
  {
    [XMap]
    public UserType UserType { get; set; }

    [XMap(IsKey = true)]
    public string UserID { get; set; }

    [XMap]
    public string UserName { get; set; }

    [XMap]
    public string Password { get; set; }

    [XMap]
    public string FirstName { get; set; }

    [XMap]
    public string LastName { get; set; }

    [XMap]
    public string CompanyName { get; set; }

    [XMap]
    public string DepartmentName { get; set; }

    [XMap(XType = XType.Element, CollectionElements = "ConfigGroupAssignment", WrapperElement = "ConfigGroupAssignments")]
    public ConfigGroupAssignments ConfigGroupAssignments { get; set; }

    public ConfigUser()
    {
      this.UserType = UserType.NotSet;
      this.UserID = String.Empty;
      this.UserName = String.Empty;
      this.Password = String.Empty;
      this.FirstName = String.Empty;
      this.LastName = String.Empty;
      this.CompanyName = String.Empty;
      this.DepartmentName = String.Empty;
      this.ConfigGroupAssignments = new ConfigGroupAssignments();
    }
    
    public bool IsInGroup(string groupID)
    {
      ConfigGroupAssignment group = this.ConfigGroupAssignments.Where(e => e.GroupID == groupID).FirstOrDefault();
      return group != null ? true : false;
    }
  }
}
