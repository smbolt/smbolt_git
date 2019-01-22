using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.BMR.ApiModels
{
  public class ApiInput
  {
    public string ApiAction {
      get;
      set;
    }
    public int OrgId {
      get;
      set;
    }
    public int AccountId {
      get;
      set;
    }
    public DateTime SentDateTime {
      get;
      set;
    }
    public int Code {
      get;
      set;
    }
    public string Command {
      get;
      set;
    }
    public object Value {
      get;
      set;
    }

    public ApiInput()
    {
      this.ApiAction = String.Empty;
      this.OrgId = -1;
      this.AccountId = -1;
      this.SentDateTime = DateTime.Now;
      this.Code = -1;
      this.Command = String.Empty;
      this.Value = null;
    }
  }
}