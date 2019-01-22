using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class RecogSpec
  {
    [XMap(IsKey = true)]
    public string Name {
      get;
      set;
    }

    [XMap(DefaultValue = "True")]
    public string Desc {
      get;
      set;
    }

    [XMap]
    public string Vendor {
      get;
      set;
    }

    [XMap(Name = "Type")]
    public FormatSpecType FormatSpecType {
      get;
      set;
    }

    [XMap]
    public bool IsActive {
      get;
      set;
    }

    [XMap(XType = XType.Element, CollectionElements = "RecogLine", WrapperElement = "RecogLineSet")]
    public RecogLineSet RecogLineSet {
      get;
      set;
    }

    public RecogSpec()
    {
      this.RecogLineSet = new RecogLineSet();
    }

    public bool IsMatch(string text)
    {
      if (text == null || text.IsBlank())
        return false;

      int prevIdx = 0;

      // Many possible optimizations and options here...
      // 1.  Currently just looking at whether the match string exist in the correct order, but matches could be found in
      //     subsequent level 1 sections.  May want to see if the first match string exists a second time (which would be
      //     possibly the beginning of the second level 1 section) and then ensure that there rest of the match strings
      //     occur in between the two occurrences of the first match string.
      // 2.  Need to consider the need for case sensitivity.
      // 3.  Need to consider matching with variable substitution (ie. "Page 1 of 65" would match "Page 9 of 9")
      // 4.  May want to consider specific offsets from a given location, requiring a particular character or word
      // 5.  May want to consider data patterns
      // 6.  May want to consider "queries" based on entity identification (probably for extaction purposes)
      //


      foreach (var recogLine in this.RecogLineSet)
      {
        if (recogLine.Use.Contains("R"))
        {
          int idx = text.IndexOf(recogLine.Text.ToLower(), prevIdx);

          // if the recognition text is not found the match fails
          if (idx == -1)
            return false;

          prevIdx = idx;
        }
      }


      return true;
    }
  }
}
