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
  [XMap(XType = XType.Element, CollectionElements = "NotifyPerson")]
  public class NotifyGroup : List<NotifyPerson>
  {
    public int NotifyGroupId {
      get;
      set;
    }

    [XMap(Name = "Name", IsKey = true)]
    public string Name {
      get;
      set;
    }

    [XMap(Name = "IsActive", DefaultValue = "True")]
    public bool IsActive {
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

    public NotifyGroup()
    {
      this.NotifyGroupId = 0;
      this.Name = String.Empty;
      this.CreatedBy = String.Empty;
      this.CreatedOn = DateTime.MinValue;
      this.ModifiedBy = null;
      this.ModifiedOn = null;
    }
  }
}
