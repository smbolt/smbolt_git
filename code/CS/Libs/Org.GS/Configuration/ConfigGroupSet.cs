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
  [XMap(XType = XType.Element, CollectionElements = "ConfigGroup")]
  public class ConfigGroupSet : SortedList<string, ConfigGroup>
  {
    public List<string> GroupNameList {
      get {
        return Get_GroupNameList();
      }
    }
    public ConfigSecurity ConfigSecurity {
      get;
      set;
    }

    public string GetNextID()
    {
      int highestID = 0;

      foreach (ConfigGroup g in this.Values)
      {
        if (g.GroupID.IsNumeric())
        {
          int id = Int32.Parse(g.GroupID);
          if (id > highestID)
          {
            highestID = id;
          }
        }
      }

      if (highestID < 999)
      {
        highestID++;
        return highestID.ToString("000");
      }

      bool[] numberUsed = new bool[1000];

      foreach (ConfigGroup g in this.Values)
      {
        if (g.GroupID.IsNumeric())
        {
          int id = Int32.Parse(g.GroupID);
          numberUsed[id] = true;
        }
      }

      for (int i = 1; i < 1000; i++)
      {
        if (!numberUsed[i])
          return i.ToString("000");
      }

      throw new Exception("All security group numbers from 1 to 999 are used.  Only 999 security groups are allowed.");
    }

    public string GetGroupIDFromName(string groupName)
    {
      foreach (ConfigGroup g in this.Values)
      {
        if (g.GroupName.ToLower() == groupName.ToLower())
          return g.GroupID;
      }

      throw new Exception("GroupID not found for group name '" + groupName + "'.");
    }

    public ConfigUserSet GetUsersByGroupID(string groupID)
    {
      ConfigUserSet us = new ConfigUserSet();

      foreach (ConfigUser u in g.AppConfig.ConfigSecurity.ConfigUserSet.Values)
      {
        foreach (var ga in u.ConfigGroupAssignments)
        {
          if (ga.GroupID == groupID)
          {
            if (!us.ContainsKey(u.UserID))
            {
              us.Add(u.UserID, u);
            }
          }
        }
      }

      return us;
    }

    public void DeleteGroupByID(string groupID)
    {
      if (!g.AppConfig.ConfigSecurity.ConfigGroupSet.ContainsKey(groupID))
        throw new Exception("Security configuration does not contain a group with ID = '" + groupID + "' - cannot delete group.");


      foreach (ConfigUser u in g.AppConfig.ConfigSecurity.ConfigUserSet.Values)
      {
        List<int> indiciesToDelete = new List<int>();
        for (int i = 0; i < u.ConfigGroupAssignments.Count; i++)
        {
          if (u.ConfigGroupAssignments.Where(e => e.GroupID == groupID).FirstOrDefault() != null)
            indiciesToDelete.Add(i);
        }

        foreach (int i in indiciesToDelete)
          u.ConfigGroupAssignments.RemoveAt(i);
      }

      g.AppConfig.ConfigSecurity.ConfigGroupSet.Remove(groupID);
    }

    public bool ContainsGroup(string groupName)
    {
      foreach (ConfigGroup g in this.Values)
      {
        if (g.GroupName == groupName)
          return true;
      }

      return false;
    }

    public void AddGroup(string groupName)
    {
      if (this.ContainsGroup(groupName))
        return;

      ProgramFunctionControl pfc = g.AppConfig.ProgramConfigSet[g.AppInfo.ConfigName].ProgramFunctionControl;
      ProgramRoleSet roleSet = pfc.ProgramRoleSet;

      string OrgRoleName = String.Empty;

      foreach (ProgramRole role in roleSet.Values)
      {
        if (role.OrgRoleName == groupName)
        {
          OrgRoleName = role.OrgRoleName;
          break;
        }
      }

      ConfigGroup gp = new ConfigGroup();
      gp.GroupName = OrgRoleName;

      int key = 0;
      while (this.ContainsKey(key.ToString().Trim()))
        key++;

      gp.GroupID = key.ToString();
      this.Add(gp.GroupID, gp);
    }

    public void RemoveGroup(string groupName)
    {
      int indexToRemove = -1;

      for (int i = 0; i < this.Count; i++)
      {
        if (this.Values[i].GroupName == groupName)
        {
          indexToRemove = i;
          break;
        }
      }

      if (indexToRemove == -1)
        return;

      this.RemoveAt(indexToRemove);
    }

    private List<string> Get_GroupNameList()
    {
      var groupNameList = new List<string>();

      foreach (var group in this.Values)
      {
        groupNameList.Add(group.GroupName);
      }

      return groupNameList;
    }
  }
}
