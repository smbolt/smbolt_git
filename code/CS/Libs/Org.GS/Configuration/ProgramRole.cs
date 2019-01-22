using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class ProgramRole
  {
    [XMap(IsRequired = true, IsExplicit = true, IsKey = true)]
    public int RoleNumber { get; set; }

    [XMap(IsRequired = true, IsExplicit = true)]
    public string OrgRoleName { get; set; }

    [XMap(IsRequired = true, IsExplicit = true)]
    public string ClientRoleName { get; set; }

    private string _functionsAllowed;
    [XMap(IsRequired = true, IsExplicit = true)]
    public string FunctionsAllowed 
    { 
      get { return this._functionsAllowed; }
      set 
      {
        this._functionsAllowed = value;
        Set_FunctionsAllowedList(value); 
      }
    }

    private List<int> _functionsAllowedList;
    public List<int> FunctionsAllowedList
    {
      get { return this._functionsAllowedList; }
    }

    private string _inheritedRoles;
    [XMap(IsExplicit = true)]
    public string InheritedRoles
    { 
      get { return this._inheritedRoles; }
      set 
      {
        this._inheritedRoles = value;
        Set_InheritedRolesList(value); 
      }
    }

    private List<int> _inheritedRolesList;
    public List<int> InheritedRolesList 
    {
      get { return this._inheritedRolesList; } 
    }

    [XMap(IsRequired = true, IsExplicit = true)]
    public bool CLSecurity { get; set; }

    [XMap(IsRequired = true, IsExplicit = true)]
    public bool Hide { get; set; }

    public ProgramRole()
    {
      this.RoleNumber = -1;
      this.OrgRoleName = String.Empty;
      this.ClientRoleName = String.Empty;
      this.FunctionsAllowed = String.Empty;
      this._functionsAllowedList = new List<int>();
      this.InheritedRoles = String.Empty;
      this._inheritedRolesList = new List<int>();
      this.CLSecurity = false;
      this.Hide = false;
    }

    public ProgramRole(int roleNumber, string roleName, string clientRoleName, string functionsAllowed, string inheritRoles, bool clSecurity, bool hide)
    {
      this.RoleNumber = roleNumber;
      this.OrgRoleName = roleName;
      this.ClientRoleName = clientRoleName;
      this.FunctionsAllowed = functionsAllowed;
      this.InheritedRoles = inheritRoles;
      this.CLSecurity = clSecurity;
      this.Hide = hide;
    }

    private void Set_FunctionsAllowedList(string functionsAllowed)
    {
      this._functionsAllowedList = new List<int>();
      if (functionsAllowed == null)
        return;

      List<string> functionsAllowedList = functionsAllowed.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries).ToList();
      foreach(var functionAllowed in functionsAllowedList.Where(s => s.IsNumeric()))
      {
        int fa = functionAllowed.ToInt32();
        if (!this._functionsAllowedList.Contains(fa))
          this._functionsAllowedList.Add(fa);
      }
    }

    private void Set_InheritedRolesList(string inheritedRoles)
    {
      this._inheritedRolesList = new List<int>();
      if (inheritedRoles == null)
        return;

      List<string> inheritedRolesList = inheritedRoles.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries).ToList();
      foreach(var inheritedRole in inheritedRolesList.Where(s => s.IsNumeric()))
      {
        int ir = inheritedRole.ToInt32();
        if (!this._inheritedRolesList.Contains(ir))
          this._inheritedRolesList.Add(ir);
      }
    }
  }
}
