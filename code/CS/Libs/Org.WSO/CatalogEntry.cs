using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.WSO
{
  public class CatalogEntry
  {
    public string ComponentName {
      get;
      set;
    }
    public string AssemblyName {
      get;
      set;
    }
    public string ObjectTypeName {
      get;
      set;
    }

    public CatalogEntry(string entryConfig)
    {
      if (entryConfig == null)
        throw new Exception("The entryConfig parameter passed into the CatalogEntry constructor is null.");

      string[] tokens = entryConfig.Split(Constants.PipeDelimiter, StringSplitOptions.RemoveEmptyEntries);

      if (tokens.Length != 3)
        throw new Exception("The entryConfig parameter passed into the CatalogEntry constructor is invalid.  There must be three string components " +
                            "(component name, assembly name and object type name) separated by pipes ('|'), for example 'WSO|Org.WSO.dll|RequestProcessorFactory'.");

      this.ComponentName = tokens[0];
      this.AssemblyName = tokens[1];
      this.ObjectTypeName = tokens[2];
    }
  }
}
