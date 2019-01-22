using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Pipes
{
  public class NamedPipe
  {
    public string PipeName { get; set; }
    public int Instances { get; set; }
    public int MaxInstances { get; set; }

    public NamedPipe(string pipeName, int instances, int maxInstances)
    {
      this.PipeName = pipeName;
      this.Instances = instances;
      this.MaxInstances = maxInstances;
    }
  }
}
