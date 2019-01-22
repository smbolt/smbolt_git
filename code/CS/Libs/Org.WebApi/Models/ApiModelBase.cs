using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.WebApi.Models
{
  public class ApiModelBase
  {
    public int OrgId { get; set; }

    public ApiModelBase()
    {
      this.OrgId = -1;
    }
  }
}
