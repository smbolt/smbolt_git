using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.BMR.ApiModels
{
  public class UserProcessModel : ApiModelBase
  {
    public string ProcessName {
      get;
      set;
    }
    public string SubProcessName {
      get;
      set;
    }
    public int SubProcessStep {
      get;
      set;
    }
    public int ProcessEntityId {
      get;
      set;
    }

    public UserProcessModel()
    {
      this.ProcessName = String.Empty;
      this.SubProcessName = String.Empty;
      this.SubProcessStep = 0;
      this.ProcessEntityId = 0;
    }
  }
}
