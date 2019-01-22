using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.BMR.Common
{
  public enum AccountType
  {
    NotSet = -1,
    Administrative = 1,
    Standard = 2
  }

  public enum AccountStatus
  {
    NotSet = -1,
    Active = 1,
    Suspended = 2,
    Canceled = 3,
    Deleted = 4
  }

  public enum PersonStatus
  {
    NotSet = -1,
    Active = 1,
    Suspended = 2,
    Deleted = 3
  }

  public enum ResumeStatus
  {
    NotSet = -1,
    Incomplete = 1,
    Complete = 2,
    Deleted = 3
  }

  public enum ResponseAction
  {
    NotSet = -1,
    Created = 1,
    Updated = 2,
    Deleted = 3
  }
}
