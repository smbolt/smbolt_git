using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Software.Business.Models
{
  public class SoftwareRepository
  {
    public int RepositoryId {
      get;
      set;
    }
    public int SoftwareStatusId {
      get;
      set;
    }
    public string RepositoryRoot {
      get;
      set;
    }
  }
}
