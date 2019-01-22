using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Database
{
  public enum ConstraintType
  {
    NotSet,
    PRIMARY_KEY,
    FOREIGN_KEY,
    UNIQUE,
    UNKNOWN
  }

}
