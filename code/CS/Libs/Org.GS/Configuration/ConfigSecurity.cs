using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.Threading;
using Org.GS;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class ConfigSecurity
  {
    [XMap(DefaultValue="0")]
    public int PasswordMinLth { get; set; }

    [XMap(DefaultValue="0")]
    public int PasswordMaxLth { get; set; }

    [XMap(DefaultValue="False")]
    public bool PasswordReqMixCase { get; set; }

    [XMap(DefaultValue="False")]
    public bool PasswordReqNbr { get; set; }

    [XMap(DefaultValue="False")]
    public bool PasswordReqChgOnFirstUse { get; set; }

    [XMap(DefaultValue="0")]
    public int PasswordReqChgFreq { get; set; }

    [XMap(XType = XType.Element, CollectionElements = "ConfigUser", WrapperElement = "ConfigUserSet")]
    public ConfigUserSet ConfigUserSet { get; set; }

    [XMap(XType = XType.Element, CollectionElements = "ConfigGroup", WrapperElement = "ConfigGroupSet")]
    public ConfigGroupSet ConfigGroupSet { get; set; }

    public string LoggedInUser { get; set; }
    public bool IsLoggedIn { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string SecurityToken { get; set; }
    public ConfigGroupSet LoggedInUserGroups { get; set; }
    public List<string> LoggedInUserAllowedFunctions { get; set; }
    public SecurityModel SecurityModel { get; set; }

    private bool _userFunctionsAlreadySet;
    private bool _groupMembershipAlreadySet;

    private bool _inClientAdminMode;
    public bool InClientAdminMode { get { return _inClientAdminMode; } }
    private bool _inOrgAdminMode;
    public bool InOrgAdminMode { get { return _inOrgAdminMode; } }

    public ConfigSecurity()
    {
      this.LoggedInUser = String.Empty;
      this.IsLoggedIn = false;
      this.UserName = String.Empty;
      this.UserId = -1; 
      this.Password = String.Empty;
      this.SecurityToken = String.Empty;
      this.LoggedInUserGroups = new ConfigGroupSet();
      this.LoggedInUserGroups.ConfigSecurity = this;
      this.LoggedInUserAllowedFunctions = new List<string>();
      this.PasswordMinLth = 0;
      this.PasswordMaxLth = 0;
      this.PasswordReqMixCase = false;
      this.PasswordReqNbr = false;
      this.PasswordReqChgOnFirstUse = false;
      this.PasswordReqChgFreq = 0;

      this.ConfigUserSet = new ConfigUserSet();
      this.ConfigGroupSet = new ConfigGroupSet();

      this.SecurityModel = GS.SecurityModel.None;

      _userFunctionsAlreadySet = false;
      _groupMembershipAlreadySet = false;
      _inClientAdminMode = false;
      _inOrgAdminMode = false;
    }

    public void Initialize(ProgramFunctionControl programFunctionControl, string userName, SecurityModel securityModel)
    {
      switch (securityModel)
      {
        case SecurityModel.Config:
          InitializeConfigSecurity(programFunctionControl, userName, securityModel);
          break;
      }
    }

    private void InitializeConfigSecurity(ProgramFunctionControl programFunctionControl, string userName, SecurityModel securityModel)
    {
      this.LoggedInUser = userName;
      this.SecurityModel = securityModel;

      this.LoggedInUserGroups.Clear();
      this.LoggedInUserAllowedFunctions.Clear();
      this.SetGroupMembershipForUser(this.LoggedInUser);
      this.SetUserFunctions(programFunctionControl);

      if (this.LoggedInUserGroups.ContainsGroup("OrgClientSuperAdmin"))
        _inClientAdminMode = true;
      else
        _inClientAdminMode = false;

      if (this.LoggedInUserGroups.ContainsGroup("OrgDeveloper") || this.LoggedInUserGroups.ContainsGroup("OrgSuperAdmin"))
        _inOrgAdminMode = true;
      else
        _inOrgAdminMode = false;
    }

    public ConfigGroupSet GetAssignedGroupsByUserID(string userID)
    {
      ConfigGroupSet gs = new ConfigGroupSet();

      if (g.AppConfig.ConfigSecurity.ConfigUserSet.ContainsKey(userID))
      {
        ConfigUser u = g.AppConfig.ConfigSecurity.ConfigUserSet[userID];
        foreach (var ga in u.ConfigGroupAssignments)
        {
          if (g.AppConfig.ConfigSecurity.ConfigGroupSet.ContainsKey(ga.GroupID))
          {
            ConfigGroup gp = g.AppConfig.ConfigSecurity.ConfigGroupSet[ga.GroupID];
            if (!gs.ContainsKey(gp.GroupID))
              gs.Add(gp.GroupID, gp);
          }
        }
      }

      return gs;
    }

    public void ValidatePasswordRules(string password)
    {
      int passwordLength = password.Trim().Length;

      if (this.PasswordMinLth != 0)
      {
        if (passwordLength < this.PasswordMinLth)
          throw new Exception("Password length is shorter than the minimum allowed length of " + this.PasswordMinLth.ToString() + ".");
      }

      if (this.PasswordMaxLth != 0)
      {
        if (passwordLength > this.PasswordMaxLth)
          throw new Exception("Password length is longer than the maximum allowed length of " + this.PasswordMaxLth.ToString() + ".");
      }

      if (this.PasswordReqNbr && !g.IncludesNumber(password))
          throw new Exception("Password must contain at least one number.");

      if (this.PasswordReqMixCase)
      {
        if (!g.IncludesLowerCaseLetter(password))
          throw new Exception("Password must contain at least one lower case letter.");

        if (!g.IncludesUpperCaseLetter(password))
          throw new Exception("Password must contain at least one upper case letter.");
      }
    }


    private object SetGroupMembershipForUser_LockObject = new object();
    public void SetGroupMembershipForUser(string userName)
    {
      if (Monitor.TryEnter(SetGroupMembershipForUser_LockObject, 2000))
      {
        try
        {
          if (_groupMembershipAlreadySet)
            return;

          if (userName.IsBlank())
            return;

          ConfigUser u = this.ConfigUserSet.GetUserByName(userName);
          if (u == null)
            return;

          this.LoggedInUserGroups = GetAssignedGroupsByUserID(u.UserID);

          _groupMembershipAlreadySet = true;
        }
        catch (Exception ex)
        {
          throw new Exception("An exception occurred attemptoing to set group membership for user in 'ConfigSecurity.SetGroupMembershipForUser' " +
                              "for user '" + userName + "'.", ex); 
        }
        finally
        {
          Monitor.Exit(SetGroupMembershipForUser_LockObject);
        }
      }
    }

    private object SetUserFunctions_LockObject = new object();
    public void SetUserFunctions(ProgramFunctionControl programFunctionControl)
    {
      if (_userFunctionsAlreadySet)
        return;

      if (Monitor.TryEnter(SetUserFunctions_LockObject, 2000))
      {
        try
        {
          this.LoggedInUserAllowedFunctions = new List<string>();

          foreach (ConfigGroup gp in this.LoggedInUserGroups.Values)
          {
            string groupName = gp.GroupName;

            foreach (ProgramRole role in programFunctionControl.ProgramRoleSet.Values)
            {
              if (role.OrgRoleName == gp.GroupName)
              {
                foreach (int i in role.FunctionsAllowedList)
                {
                  if (i == 999)
                  {
                    foreach (ProgramFunction f in programFunctionControl.ProgramFunctionSet.Values)
                    {
                      if (!this.LoggedInUserAllowedFunctions.Contains(f.FunctionName))
                        this.LoggedInUserAllowedFunctions.Add(f.FunctionName);
                    }
                  }
                  else
                  {
                    ProgramFunction f = programFunctionControl.ProgramFunctionSet[i];
                    if (!this.LoggedInUserAllowedFunctions.Contains(f.FunctionName))
                      this.LoggedInUserAllowedFunctions.Add(f.FunctionName);
                  }
                }

                foreach (int i in role.InheritedRolesList)
                {
                  ProgramRole inheritedRole = programFunctionControl.ProgramRoleSet[i];
                  AddInheritedFunctions(inheritedRole, this.LoggedInUserAllowedFunctions, 
                                        programFunctionControl.ProgramRoleSet, programFunctionControl.ProgramFunctionSet);
                }
                
                _userFunctionsAlreadySet = true;
              }
            }
          }
        }
        catch(Exception ex)
        {
          throw new Exception("An exception occurred attemptoing to set user functions in 'ConfigSecurity.SetUserFunctions'. ", ex);
        }
        finally
        {
          Monitor.Exit(SetUserFunctions_LockObject);
        }
      }
    }

    private void AddInheritedFunctions(ProgramRole r, List<string> allowedFunctions, ProgramRoleSet rs, ProgramFunctionSet fs)
    {
      foreach (int i in r.FunctionsAllowedList)
      {
        ProgramFunction f = fs[i];
        if (!allowedFunctions.Contains(f.FunctionName))
          allowedFunctions.Add(f.FunctionName);
      }

      foreach (int i in r.InheritedRolesList)
      {
        ProgramRole inheritedRole = rs[i];
        foreach (int j in inheritedRole.FunctionsAllowedList)
        {
          ProgramFunction f = fs[j];
          if (!allowedFunctions.Contains(f.FunctionName))
            allowedFunctions.Add(f.FunctionName);
        }

        foreach (int k in inheritedRole.InheritedRolesList)
        {
          ProgramRole innerInheritedRole = rs[k];
          AddInheritedFunctions(innerInheritedRole, allowedFunctions, rs, fs);
        }
      }
    }

    public string GetUserGroupAndFunctionSummary()
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("User:" + g.crlf + "  " + this.LoggedInUser + g.crlf);

      sb.Append(g.crlf2); 
      sb.Append("Groups:");
      foreach (ConfigGroup gp in this.LoggedInUserGroups.Values)
      {
        sb.Append(g.crlf + "  " + gp.GroupName);
      }

      sb.Append(g.crlf2); 
      sb.Append("Program Functions:");
      foreach (string function in this.LoggedInUserAllowedFunctions)
      {
        sb.Append(g.crlf + "  " + function);
      }

      return sb.ToString();
    }

    public bool AllowFunctionForUser(string requestedFunction)
    {
      return this.LoggedInUserAllowedFunctions.Contains(requestedFunction); 
    }

    public List<string> GetGroupsAllowedFunction(string programFunction)
    {
      var groupsAllowedFunction = new List<string>();


      return groupsAllowedFunction;
    }
  }
}
