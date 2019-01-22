using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Terminal.BMS
{
  public class Parenthetical
  {
    public string Name {
      get;
      set;
    }
    public List<string> Parms {
      get;
      set;
    }

    public Parenthetical(string name, string parms)
    {
      this.Name = name;
      this.Parms = parms.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
  }
}
