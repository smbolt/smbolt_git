using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.AX
{
  public class AxProfileParms
  {
    public bool IsDryRun { get; set; }
    public ParmSet ParmSet { get; set; }

    public AxProfileParms()
    {
      this.IsDryRun = false;
      this.ParmSet = new ParmSet(); 
    }
  }
}
