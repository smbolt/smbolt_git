using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Org.WebApi.ApiManagement
{
  public class PagingControl
  {
    public bool IsPaging {
      get;
      set;
    }
    public int TotalEntityCount {
      get;
      set;
    }
    public int SubsetEntityCount {
      get;
      set;
    }

    public PagingControl()
    {
      this.IsPaging = false;
      this.TotalEntityCount = 0;
      this.SubsetEntityCount = 0;
    }
  }
}