using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.AX
{
  public class UnitOfWork
  {
    public string Src { get; set; }
    public string Tgt { get; set; }
    public AxionClass AxionClass { get; set; }

    public UnitOfWork()
    {
      this.Src = String.Empty;
      this.Tgt = String.Empty;
      this.AxionClass = AxionClass.NotSet;
    }

    public virtual TaskResult Run(bool isDryRun)
    {


      return null;
    }
  }
}
