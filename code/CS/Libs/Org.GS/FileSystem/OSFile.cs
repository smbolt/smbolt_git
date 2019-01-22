using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Org.GS
{
  public class OSFile :FSBase
  {
    public SearchParms SearchParms { get; set; }

    public int FileID { get; set; }
    public string FileName { get; set; }
    public string FullPath { get; set; }
    public long FileSize { get; set; }
    public int ExtensionID { get; set; }
    public string LogicalPath { get { return Get_LogicalPath(); } }
    public string FileExtension { get; set; }
    public bool IsFileIncluded { get; set; }
    public DateTime LastChangedDateTime { get; set; }
    public DateTime CompareLastChangeDateTime { get; set; }
    public List<string> ContainedControls { get; set; }
    public FileCompareStatus FileCompareStatus { get; set; }
    public OSFolder ParentFolder { get; set; }
    public bool IsProcessed { get; set; }
    public bool MaxPathExceeded { get; set; }

    public OSFile(OSFolder parentFolder)
    {
      this.ParentFolder = parentFolder;
      this.FileName = String.Empty;
      this.FullPath = String.Empty;
      this.FileSize = 0;
      this.FileExtension = String.Empty;
      this.IsFileIncluded = false;
      this.LastChangedDateTime = DateTime.MinValue;
      this.ContainedControls = new List<string>();
      this.FileCompareStatus = FileCompareStatus.NotSet;
      this.IsProcessed = false;
      this.MaxPathExceeded = false;
    }

    public OSFile(OSFolder parentFolder, string fullPath)
    {
      this.ParentFolder = parentFolder;
      this.FullPath = fullPath;
      this.FileSize = 0;
      this.FileExtension = String.Empty;
      this.FileName = Path.GetFileName(this.FullPath); 
      this.IsFileIncluded = false;
      this.ContainedControls = new List<string>();
      this.IsProcessed = false; 
      this.MaxPathExceeded = false;
    }

    public string RunSearch(SearchParms searchParms)
    {
      try
      {
        this.SearchParms = searchParms;

        StringBuilder sb = new StringBuilder();
        StreamReader sr = new StreamReader(this.FullPath);

        int lineNumber = 0;
        while (!sr.EndOfStream)
        {
          string line = sr.ReadLine();
          lineNumber++;

          foreach (string searchPatternInclude in this.SearchParms.ContentIncludes)
          {
            if (line.IndexOf(searchPatternInclude) > -1)
            {
              if (this.SearchParms.ContentExcludes.Count == 0)
              {
                sb.Append("            " + lineNumber.ToString("0000") + "   " + line + Environment.NewLine);
                if (!this.SearchParms.SearchResults.OSFileList.ContainsKey(this.FileName.ToLower()))
                {
                  this.SearchParms.SearchResults.OSFileList.Add(this.FileName.ToLower(), this);
                  this.IsFileIncluded = true;
                }
                break;
              }
              else
              {
                bool exclude = false;
                foreach (string searchPatternExclude in this.SearchParms.ContentExcludes)
                {
                  if (line.IndexOf(searchPatternExclude) > -1)
                  {
                    exclude = true;
                    break;
                  }
                }
                if (!exclude)
                {
                  sb.Append("            " + lineNumber.ToString("0000") + "   " + line + Environment.NewLine);
                  if (!this.SearchParms.SearchResults.OSFileList.ContainsKey(this.FileName.ToLower()))
                  {
                    this.SearchParms.SearchResults.OSFileList.Add(this.FileName.ToLower(), this);
                    this.IsFileIncluded = true;
                  }
                  break;
                }
              }
            }
          }
        }

        sr.Close();

        return sb.ToString();
      }
      catch (Exception ex)
      {


        return String.Empty;
      }
    }
    
    public void SetFileProperties()
    {
      if (this.FullPath.Length > 260)
      {
        this.MaxPathExceeded = true;
        string[] tokens = this.FullPath.Split(Constants.PeriodDelimiter, StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length > 0)
          this.FileExtension = "." + tokens[tokens.Length - 1];
      }
      else
      {
        if (File.Exists(this.FullPath))
        {
          FileInfo fileInfo = new FileInfo(this.FullPath);
          this.LastChangedDateTime = fileInfo.LastWriteTime;
          this.FileSize = fileInfo.Length;
          this.FileExtension = fileInfo.Extension;
        }
      }
    }

    public string Get_LogicalPath()
    {
      if (this.ParentFolder == null || this.ParentFolder.RootFolderPath == null)
        throw new Exception("ParentFolder or ParentFolder.RootFolder is null for file '" + this.FullPath + "'.");

      return this.FullPath.Replace(this.ParentFolder.RootFolderPath, "$CompareRoot$"); 
    }
  }
}
