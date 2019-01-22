using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Org.GS;

namespace Org.GS.Code
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class IncludedExtension
  {
    [XMap]
    public string Extension {
      get;
      set;
    }

    [XMap(XType=XType.Element, WrapperElement="ExtensionExclusionSet", CollectionElements="ExtensionExclusion")]
    public ExtensionExclusionSet ExtensionExclusionSet {
      get;
      set;
    }

    public IncludedExtension()
    {
      this.Extension = String.Empty;
      this.ExtensionExclusionSet = new ExtensionExclusionSet();
    }

    public bool IncludeFile(string fileName)
    {
      string ext = System.IO.Path.GetExtension(fileName).Replace(".", String.Empty).ToLower();

      if (this.Extension == "*" || this.Extension.ToLower() == ext)
        return true;

      return false;
    }
  }
}
