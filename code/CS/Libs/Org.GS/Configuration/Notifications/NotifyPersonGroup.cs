using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Configuration
{
  public class NotifyPersonGroup
  {
    public int NotifyPersonGroupId {
      get;
      set;
    }
    public int NotifyGroupId {
      get;
      set;
    }
    public int NotifyPersonId {
      get;
      set;
    }
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

    public NotifyPersonGroup()
    {
      this.NotifyPersonGroupId = 0;
      this.NotifyGroupId = 0;
      this.NotifyPersonId = 0;
      this.IsActive = true;
      this.CreatedBy = String.Empty;
      this.CreatedOn = DateTime.MinValue;
      this.ModifiedBy = null;
      this.ModifiedOn = null;
    }
  }
}
