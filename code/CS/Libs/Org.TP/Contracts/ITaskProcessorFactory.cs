using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.TP
{
  public interface ITaskProcessorFactory : IDisposable
  {
    ITaskProcessor CreateTaskProcessor(string nameAndVersion);
    void Dispose();
  }
}
