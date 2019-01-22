using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.WebApi.Models
{
  public class ApiModelSet
  {
    public List<ApiModelBase> ModelList { get; set; }
    public int TotalEntityCount { get; set; }
    public int SubsetEntityCount { get { return this.ModelList != null ? this.ModelList.Count : 0; } }

    public ApiModelSet()
    {
      this.ModelList = new List<ApiModelBase>();
      this.TotalEntityCount = 0;
    }
  }
}
