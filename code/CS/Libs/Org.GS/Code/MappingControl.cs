using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Org.GS;

namespace Org.GS.Code
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class MappingControl
  {
    [XMap(IsKey=true)]
    public string Name {
      get;
      set;
    }

    [XMap(DefaultValue="True")]
    public bool IsActive {
      get;
      set;
    }

    [XMap]
    public string Source {
      get;
      set;
    }

    [XMap]
    public string Destination {
      get;
      set;
    }

    [XMap(DefaultValue="False")]
    public bool ClearDestination {
      get;
      set;
    }

    [XMap(XType = XType.Element, WrapperElement="IncludedExtensionSet", CollectionElements = "IncludedExtension")]
    public IncludedExtensionSet IncludedExtensionSet {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public IncludedFileSet IncludedFileSet {
      get;
      set;
    }

    [XMap(XType = XType.Element)]
    public ExcludedFileSet ExcludedFileSet {
      get;
      set;
    }

    [XMap(DefaultValue="False")]
    public bool Recursive {
      get;
      set;
    }

    public MappingControl()
    {
      this.Name = String.Empty;
      this.IsActive = true;
      this.Source = String.Empty;
      this.Destination = String.Empty;
      this.ClearDestination = false;
      this.IncludedExtensionSet = new IncludedExtensionSet();
      this.IncludedFileSet = new IncludedFileSet();
      this.ExcludedFileSet = new ExcludedFileSet();
      this.Recursive = false;
    }

    public IncludedExtension GetIncludedExtension(string ext)
    {
      ext = ext.Replace(".", String.Empty);

      foreach (IncludedExtension ie in this.IncludedExtensionSet)
      {
        if (ie.Extension == "*")
          return ie;

        if (ie.Extension == ext)
          return ie;
      }

      return null;
    }

    public InclusionResult CheckFileInclusion(IncludedExtension ie, string fileName)
    {
      // check for specific rule for including by file name or pattern (overriding)
      foreach (string includedFile in this.IncludedFileSet)
      {
        string fileNameMatch = includedFile.Replace("*", String.Empty).ToLower();
        string fileNameLower = fileName.ToLower();
        if (fileNameLower.Contains(fileNameMatch))
          return InclusionResult.IncludedFileMatch;
      }

      // check for specification of extensions to include (and exclude if not specified)
      if (ie != null)
      {
        string ext = Path.GetExtension(fileName).ToLower();
        if (ext == ie.Extension.ToLower())
        {
          // check if some files in this extension need to be excluded
          // either by matching or including a pattern
          foreach (ExtensionExclusion ee in ie.ExtensionExclusionSet)
          {
            switch (ee.ExclusionControl)
            {
              case ExclusionControl.IfIncludes:
                if (fileName.ToLower().IndexOf(ee.Value.ToLower()) != -1)
                  return InclusionResult.IncludedExtensionExclusionSpec;
                break;

              case ExclusionControl.IfMatches:
                if (fileName.ToLower() == ee.Value)
                  return InclusionResult.IncludedExtensionExclusionSpec;
                break;
            }
          }

          // files with matching extensions are included at this point
          // and fall through to see if they are explicitly excluded below

        }
        else
        {
          return InclusionResult.ExcludedByExtension;
        }
      }

      // check to see if the file is explicitly excluded
      foreach (var fileToExclude in this.ExcludedFileSet)
      {
        string fileNameMatch = fileToExclude.Replace("*", String.Empty).ToLower();
        string fileNameLower = fileName.ToLower();
        if (fileNameLower.Contains(fileNameMatch))
          return InclusionResult.ExcludedBySpec;
      }

      if (ie == null)
        return InclusionResult.IncludedByDefault;

      return InclusionResult.IncludedByExtension;
    }
  }
}
