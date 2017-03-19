using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS
{
  public class SearchParms
  {
    public string RootPath { get; set; }
    public DateTime SinceDate { get; set; }
    public List<string> Extensions { get; set; }
    public List<string> FolderNameIncludes { get; set; }
    public LogicOp ExtensionAndFileNameIncludeLogicOp { get; set; }
    public List<string> FolderNameExcludes { get; set; }
    public List<string> FileNameIncludes { get; set; }
    public List<string> FileNameExcludes { get; set; }
    public List<string> ContentIncludes { get; set; }
    public List<string> ContentExcludes { get; set; }
    public SearchResults SearchResults { get; set; }
    public bool ProcessChildFolders { get; set; }
    public bool BuildFileList { get; set; }
    public int FileCountLimit { get; set; }
    public bool LogPathTooLongExceptions { get; set; }
    public bool AllowMemoryUsageGrowth { get; set; }
    public bool ArchiveFileExpanded { get; set; }

    public SearchParms()
    {
      Initialize();
    }

    public SearchParms(List<string> extensions)
    {
      Initialize();
      this.Extensions = extensions;
    }

    private void Initialize()
    {
      this.RootPath = String.Empty;
      this.SinceDate = DateTime.Now;
      this.Extensions = new List<string>();
      this.FolderNameIncludes = new List<string>();
      this.FolderNameExcludes = new List<string>();
      this.FileNameIncludes = new List<string>();
      this.FileNameExcludes = new List<string>();
      this.ContentIncludes = new List<string>();
      this.ContentExcludes = new List<string>();
      this.SearchResults = new SearchResults();
      this.ProcessChildFolders = true;
      this.BuildFileList = true;
      this.FileCountLimit = 0;
      this.LogPathTooLongExceptions = false;
      this.AllowMemoryUsageGrowth = false;
    }
  }
}
