using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.MOD.Contracts
{
  public interface IModule : IDisposable
  {
    void Run();
  }
}
