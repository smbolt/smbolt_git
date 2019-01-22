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
  [XMap(XType = XType.Element, CollectionElements = "ConfigUser")]
  public class ConfigUserSet : SortedList<string, ConfigUser>
  {
    public string GetUserIDByName(string userName)
    {
      foreach (ConfigUser u in this.Values)
      {
        if (u.UserName.ToLower() == userName.ToLower())
          return u.UserID;
      }

      throw new Exception("UserID not found for user name '" + userName + "'.");
    }

    public ConfigUser GetUserByName(string userName)
    {
      foreach (ConfigUser u in this.Values)
      {
        if (u.UserName.ToLower() == userName.ToLower())
          return u;
      }

      return null;
    }

    public string GetNextID()
    {
      int highestID = 0;

      foreach (ConfigUser u in this.Values)
      {
        if (u.UserID.IsNumeric())
        {
          int id = Int32.Parse(u.UserID);
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

      foreach (ConfigUser u in this.Values)
      {
        if (u.UserID.IsNumeric())
        {
          int id = Int32.Parse(u.UserID);
          numberUsed[id] = true;
        }
      }

      for (int i = 1; i < 1000; i++)
      {
        if (!numberUsed[i])
          return i.ToString("000");
      }

      throw new Exception("All user id numbers from 1 to 999 are used.  Only 999 users are allowed.");
    }

    public bool UserExists(string userName)
    {
      foreach (ConfigUser u in this.Values)
      {
        if (u.UserName.Trim().ToLower() == userName.Trim().ToLower())
          return true;
      }

      return false;
    }

    public string GetUserName(string userName)
    {
      foreach (ConfigUser u in this.Values)
      {
        if (u.UserName.Trim().ToLower() == userName.Trim().ToLower())
          return u.UserName.Trim();
      }

      return String.Empty;
    }

    public bool Login(string userName, string password)
    {
      ConfigUser user = null;

      foreach (ConfigUser u in this.Values)
      {
        if (u.UserName.Trim().ToLower() == userName.Trim().ToLower())
        {
          user = u;
        }
      }

      if (user == null)
        return false;

      if (user.Password == password.Trim())
        return true;

      return false;
    }

    public bool NamedPersonExists(string firstName, string lastName)
    {
      foreach (ConfigUser u in this.Values)
      {
        if (u.FirstName.Trim().ToLower() == firstName.Trim().ToLower()
            && u.LastName.Trim().ToLower() == lastName.Trim().ToLower())
          return true;
      }

      return false;
    }
  }
}
