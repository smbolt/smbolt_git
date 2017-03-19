using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.GS.TextProcessing
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements = "ExtractSpec", XType = XType.Element)] 
  public class ExtractSpecSet : Dictionary<string, ExtractSpec>
  {
    public void PopulateReferences()
    {
      foreach (var extractSpec in this.Values)
      {
        extractSpec.ExtractSpecSet = this;

        foreach (var tsd in extractSpec.Values)
        {
          PopulateTsdReferences(null, tsd, extractSpec);
        }
      }
    }

    private void PopulateTsdReferences(Tsd parentTsd, Tsd tsd, ExtractSpec extractSpec)
    {
      tsd.Parent = parentTsd;
      tsd.ExtractSpec = extractSpec;

      foreach (var childTsd in tsd.TsdSet.Values)
        PopulateTsdReferences(tsd, childTsd, extractSpec); 
    }

  }
}
