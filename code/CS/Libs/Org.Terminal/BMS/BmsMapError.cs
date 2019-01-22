using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Terminal.BMS
{
  public class BmsMapError
  {
    public int Code {
      get;
      set;
    }
    public string ErrorMessage {
      get;
      set;
    }
    public BmsMapErrorLevel BmsMapErrorLevel {
      get;
      set;
    }

    public BmsMapError()
    {
      this.Code = 0;
      this.ErrorMessage = String.Empty;
      this.BmsMapErrorLevel = BmsMapErrorLevel.NoError;
    }
  }
}
