using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace Org.GS.Configuration
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element, CollectionElements = "NotifyGroupReference")]
  public class NotifyEvent : List<NotifyGroupReference>
  {
    public int NotifyEventId {
      get;
      set;
    }
    public int NotifyConfigId {
      get;
      set;
    }

    [XMap(Name = "Name", IsKey = true)]
    public string Name {
      get;
      set;
    }

    [XMap(Name = "IsActive")]
    public bool IsActive {
      get;
      set;
    }

    [XMap]
    public string DefaultSubject {
      get;
      set;
    }

    public string CreatedBy {
      get;
      set;
    }
    public DateTime CreatedOn {
      get;
      set;
    }
    public string ModifiedBy {
      get;
      set;
    }
    public DateTime? ModifiedOn {
      get;
      set;
    }

    public NotifyEvent()
    {
      this.NotifyEventId = 0;
      this.NotifyConfigId = 0;
      this.Name = String.Empty;
      this.IsActive = false;
      this.DefaultSubject = String.Empty;
      this.CreatedBy = String.Empty;
      this.CreatedOn = DateTime.MinValue;
      this.ModifiedBy = null;
      this.ModifiedOn = null;
    }

    public List<string> GetAllGroupNames()
    {
      List<string> groupNames = new List<string>();

      foreach (NotifyGroupReference ngr in this)
      {
        if (ngr.IsActive)
          groupNames.Add(ngr.NotifyGroupName);
      }

      return groupNames;
    }
  }
}
