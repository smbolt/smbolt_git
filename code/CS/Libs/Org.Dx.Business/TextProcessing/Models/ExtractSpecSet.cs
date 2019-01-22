using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements = "ExtractSpec", XType = XType.Element)]
  public class ExtractSpecSet : Dictionary<string, ExtractSpec>
  {
    [XMap]
    public string ColumnIndexMapName {
      get;
      set;
    }

    public ExtractSpecSet()
    {
      this.ColumnIndexMapName = String.Empty;
    }

    public void AutoInit()
    {
      if (this.ColumnIndexMapName.IsBlank())
        this.ColumnIndexMapName = "ColumnIndexMap";
    }

    public void PopulateReferences()
    {
      foreach (var extractSpec in this.Values)
      {
        extractSpec.NumberLines();
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

      foreach (var cmd in tsd)
      {
        cmd.Parent = tsd;
      }

      foreach (var childTsd in tsd.TsdSet.Values)
      {
        PopulateTsdReferences(tsd, childTsd, extractSpec);
      }
    }

  }
}
