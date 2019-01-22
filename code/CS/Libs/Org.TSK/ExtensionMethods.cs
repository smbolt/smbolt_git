using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Text;
using System.Threading.Tasks;
using Org.TP;
using Org.GS;

namespace Org.TSK 
{
  public static class ExtensionMethods 
  {
    
    public static List<string> GetRunnableTaskNames(this CompositionContainer container,
                               IEnumerable<Lazy<ITaskProcessorFactory, ITaskProcessorMetadata>> taskProcessorFactories)
    {
      List<string> runnableTaskNames = new List<string>();

      if (container == null)
        return runnableTaskNames; 
                  
      foreach (Lazy<ITaskProcessorFactory, ITaskProcessorMetadata> taskProcessorFactory in taskProcessorFactories)
      {
        if (taskProcessorFactory.Metadata.Processors != null)
        {
          string[] processors  = taskProcessorFactory.Metadata.Processors.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries); 
          foreach (var processor in processors)
          {
            if (!runnableTaskNames.Contains(processor))
              runnableTaskNames.Add(processor);
          }
        }
      }

      return runnableTaskNames;
    }

  }
}
