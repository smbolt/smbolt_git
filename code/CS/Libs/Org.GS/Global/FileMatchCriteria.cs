using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class FileMatchCriteria
  {
    public FileSet IncludedFileSet { get; set; }
    public FileSet ExcludedFileSet { get; set; }

    public string Report { get { return Get_Report(); } }

    public FileMatchCriteria(string includePattern)
    {
      this.IncludedFileSet = new FileSet();
      this.IncludedFileSet.Add(new FileSpec(includePattern));
      this.ExcludedFileSet = new FileSet();
    }

    public FileMatchCriteria(FileSet includedFileSet, FileSet excludedFileSet)
    {
      this.IncludedFileSet = includedFileSet;
      this.ExcludedFileSet = excludedFileSet;
    }

    public bool IncludeThisFile(string fullFilePath)
    {
      if (this.IncludedFileSet.Count == 0 && this.ExcludedFileSet.Count == 0)
        return true; 

      string fileName = Path.GetFileName(fullFilePath);

      foreach (var excludedFile in this.ExcludedFileSet)
      {
        bool match = CheckForMatch(excludedFile.Name, fileName);

        if (match)
          return false;
      }

      foreach (var includedFile in this.IncludedFileSet)
      {
        bool match = CheckForMatch(includedFile.Name, fileName);

        if (match)
          return true;
      }

      return false;
    }

    private bool CheckForMatch(string pattern, string file)
    {
      pattern = pattern.ToLower();
      file = file.ToLower();
      string searchStr = pattern.Replace("*", String.Empty);

      if (pattern == "*")
        return true;
      if (file == searchStr)
        return true;
      if (pattern.StartsWith("*") && pattern.EndsWith("*") && file.Contains(searchStr))
        return true;
      if (pattern.StartsWith("*") && file.EndsWith(searchStr))
        return true;
      if (pattern.EndsWith("*") && file.StartsWith(searchStr))
        return true;

      return false;
    }

    private string Get_Report()
    {
      return "FileMatchCriteria report is not yet implemented.";
    }
  }
}
