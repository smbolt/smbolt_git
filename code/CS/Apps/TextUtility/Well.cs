using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.TextUtility
{
  public class Well
  {
    public string WellName {
      get;
      set;
    }
    public int GPWellNo {
      get;
      set;
    }
    public string Active {
      get;
      set;
    }
    public string Operated {
      get;
      set;
    }
    public string CountyName {
      get;
      set;
    }
    public string StateName {
      get;
      set;
    }
    public string FieldName {
      get;
      set;
    }
    public string API {
      get;
      set;
    }

    public Well()
    {
      this.WellName = String.Empty;
      this.GPWellNo = 0;
      this.Active = String.Empty;
      this.Operated = String.Empty;
      this.CountyName = String.Empty;
      this.StateName = String.Empty;
      this.FieldName = String.Empty;
      this.API = String.Empty;
    }
  }
}
