using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.BMR.ApiModels
{
  public class SecurityQuestionModel : ApiModelBase
  {
    public int SecurityQuestionId {
      get;
      set;
    }
    public string SecurityQuestionText {
      get;
      set;
    }
  }
}
