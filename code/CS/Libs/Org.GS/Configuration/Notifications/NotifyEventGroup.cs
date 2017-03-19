using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Configuration
{
  public class NotifyEventGroup
  {
    public int NotifyEventGroupID { get; set; }
    public int NotifyEventID { get; set; }
    public int NotifyGroupID { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }

    public NotifyEventGroup()
    {
      this.NotifyEventGroupID = 0;
      this.NotifyEventID = 0;
      this.NotifyGroupID = 0;
      this.IsActive = true;
      this.CreatedBy = String.Empty;
      this.CreatedOn = DateTime.MinValue;
      this.ModifiedBy = null;
      this.ModifiedOn = null; 
    }
    
  }
}
