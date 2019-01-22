using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BMR.Common;

namespace Org.BMR.ApiModels
{
  public class ResponseModel : ApiModelBase
  {
    public int EntityId { get; set; }
    public string EntityName { get; set; }
    public ResponseAction ResponseAction { get; set; }
    public string Code { get; set; }
    public string Message { get; set; }

    public ResponseModel()
    {
      this.EntityId = -1;
      this.EntityName = String.Empty;
      this.ResponseAction = ResponseAction.NotSet;
      this.Code = String.Empty;
      this.Message = String.Empty;
    }
  }
}
