using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public interface IModuleMetadata
  {
    string Name { get; }
    string Version { get; }
  }

  public interface IRequestProcessorMetadata
  {
    string Name { get; }
    string Version { get; }
    string Processors { get; }
  }

  public interface ITaskProcessorMetadata
  {
    string Name { get; }
    string Version { get; }
    string Processors { get; }
  }

  public interface IMessageFactoryMetadata
  {
    string Name { get; }
    string Version { get; }
    string Transactions { get; }
  }
}
