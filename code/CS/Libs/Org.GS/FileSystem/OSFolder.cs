using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Org.GS.Logging;

namespace Org.GS
{
  public class OSFolder : FSBase
  {
    private static Func<OSFolder, bool, bool> FileLimitReached;
    public static event Action<string> FSNotification;
    private static Logger _logger;
    public static SortedList<string, int> Extensions;

    public static int FileCount {
      get;
      set;
    }
    public static int ProcessedFileCount {
      get;
      set;
    }
    public static int FolderCount {
      get;
      set;
    }
    public static bool StaticCountersInitialized = InitializeStaticCounters();

    public int? FolderID {
      get;
      set;
    }
    public int ProjectID {
      get;
      set;
    }
    public bool IsRootFolder {
      get;
      set;
    }
    public string RootFolderPath {
      get;
      set;
    }
    public string FolderName {
      get;
      set;
    }
    public OSFolder ParentFolder {
      get;
      set;
    }
    public OSFolder RootFolder {
      get {
        return Get_RootFolder();
      }
    }
    public int DepthFromRoot {
      get;
      set;
    }
    public OSFolderSet OSFolderSet {
      get;
      set;
    }
    public OSFileSet OSFileSet {
      get;
      set;
    }
    public SearchParms SearchParms {
      get;
      set;
    }
    private Dictionary<string, OSFile> _fileList;
    public Dictionary<string, OSFile> FileList {
      get {
        return Get_FileList();
      }
    }
    public bool IsProcessed {
      get;
      set;
    }

    private string _fullPath;
    public string FullPath
    {
      get {
        return _fullPath;
      }
      set
      {
        _fullPath = value;
        this.FolderName = Path.GetFileName(_fullPath);
      }
    }

    public OSFolder()
    {
      this.Initialize();
    }

    public OSFolder(string folderPath)
    {
      Initialize();
      this.FullPath = folderPath;
    }

    public OSFolder(SearchParms searchParms)
    {
      this.Initialize();
      this.SearchParms = searchParms;
      if (searchParms.RootPath.IsNotBlank())
      {
        this.RootFolderPath = searchParms.RootPath;
        this.FullPath = searchParms.RootPath;
      }
    }

    private void Initialize()
    {
      this.IsRootFolder = false;
      this.RootFolderPath = String.Empty;
      this.FolderName = String.Empty;
      this.FullPath = String.Empty;
      this.ParentFolder = null;
      this.DepthFromRoot = -1;
      this.OSFolderSet = new OSFolderSet();
      this.OSFileSet = new OSFileSet();
      this.SearchParms = new SearchParms();
      this.IsProcessed = false;
    }

    public static void SetLimitReachedFunction(Func<OSFolder, bool, bool> limitReached)
    {
      FileLimitReached = limitReached;
    }

    public void BuildFolderAndFileList()
    {
      this.OSFolderSet = new OSFolderSet();
      this.OSFileSet = new OSFileSet();
      Extensions = new SortedList<string, int>();

      BuildFolderList(this);

      if (this.SearchParms.BuildFileList)
        _fileList = Get_FileList();

      if (FileLimitReached != null)
      {
        FileLimitReached(this.RootFolder, true);
      }
    }

    public void BuildFolderList(OSFolder folder)
    {
      try
      {
        FolderCount++;
        if (FolderCount % 10 == 0 && this.SearchParms.RootPath.IsNotBlank())
          NotifyHost("FLDR", folder.FolderName.Replace(this.SearchParms.RootPath, String.Empty));
        FileCount++;

        if (SearchParms.FileCountLimit > 0 && FileCount > SearchParms.FileCountLimit)
        {
          ProcessedFileCount += FileCount;
          if (FileLimitReached != null)
          {
            bool filesProcessed = FileLimitReached(this.RootFolder, false);

            FileCount = 0;
            if (filesProcessed)
            {
              int foldersToRemove = 0;
              foreach (var fldr in this.RootFolder.OSFolderSet)
              {
                if (fldr.IsProcessed)
                  foldersToRemove++;
              }

              while (foldersToRemove > 0)
              {
                this.RootFolder.OSFolderSet.RemoveAt(0);
                foldersToRemove--;
              }
            }


          }
        }

        string[] subFolders = null;
        try
        {
          subFolders = Directory.GetDirectories(folder._fullPath);
        }
        catch (Exception ex)
        {
          if (ex.GetType().Name == "PathTooLongException")
          {
            if (SearchParms.LogPathTooLongExceptions)
            {
              if (_logger == null)
                _logger = new Logger();
              _logger.Log(folder.FullPath);
            }
            return;
          }
          else
            throw new Exception("An exception occurred while building the folder list.", ex);
        }

        foreach (string subFolder in subFolders)
        {
          FolderCount++;
          if (FolderCount % 10 == 0 && this.SearchParms.RootPath.IsNotBlank())
            NotifyHost("FLDR", folder.FolderName.Replace(this.SearchParms.RootPath, String.Empty));
          if (IncludeFolder(subFolder))
          {
            OSFolder f = null;

            try
            {
              f = new OSFolder(folder.SearchParms);
            }
            catch (Exception ex)
            {
              if (ex.GetType().Name == "PathTooLongException")
              {
                if (SearchParms.LogPathTooLongExceptions)
                {
                  if (_logger == null)
                    _logger = new Logger();
                  _logger.Log(folder.FullPath);
                }
                return;
              }
              else
                throw new Exception("An exception occurred while building the folder list.", ex);
            }

            f.FullPath = subFolder;
            f.ProjectID = folder.ProjectID;
            f.FolderName = Path.GetFileName(f._fullPath);
            f.IsRootFolder = false;
            f.DepthFromRoot = folder.DepthFromRoot + 1;
            f.RootFolderPath = folder.RootFolderPath;
            f.ParentFolder = folder;
            f.SearchParms = folder.SearchParms;
            folder.OSFolderSet.Add(f);
            if (folder.SearchParms.ProcessChildFolders)
              f.BuildFolderList(f);
          }
        }

        BuildFileList(folder);
      }
      catch (Exception ex)
      {
        if (ex.GetType().Name.Contains("UnauthorizedAccessException"))
          return;
        throw new Exception("An exception occurred while attempting to build the folder list.", ex);
      }
    }

    public bool IncludeFolder(string folderName)
    {
      if (folderName.ToUpper().Contains("RECYCLE.BIN"))
        return false;

      if (this.SearchParms.FolderNameExcludes.Count == 0)
        return true;

      string folderCompare = Path.GetFileName(folderName).Trim().ToLower();

      foreach (string excludeFolder in this.SearchParms.FolderNameExcludes)
      {
        string exclude = excludeFolder.Trim().ToLower();

        if (exclude.StartsWith("*"))
        {
          if (exclude.EndsWith("*"))
          {
            if (folderCompare.Contains(exclude.Substring(1).TrimEnd(Constants.AsteriskDelimiter)))
            {
              return false;
            }
          }
          else
          {
            if (folderCompare.EndsWith(exclude.Substring(1)))
              return false;
          }
        }
        else
        {
          if (exclude.EndsWith("*"))
          {
            if (folderCompare.StartsWith(exclude.TrimEnd(Constants.AsteriskDelimiter)))
              return false;
          }
          else
          {
            if (folderCompare == exclude)
              return false;
          }
        }
      }

      return true;
    }

    public List<string> GetLeafFolders()
    {
      BuildFolderList(this);
      return GetLeafFolders(this);
    }

    public List<string> GetLeafFolders(OSFolder folder)
    {
      List<string> leafFolders = new List<string>();
      foreach (var childFolder in folder.OSFolderSet)
      {
        if (childFolder.OSFolderSet.Count == 0)
          leafFolders.Add(childFolder.FullPath);
        else
          leafFolders.AddRange(GetLeafFolders(childFolder));
      }
      return leafFolders;
    }

    private void BuildFileList(OSFolder folder)
    {
      string[] files = Directory.GetFiles(folder._fullPath);

      foreach (string file in files)
      {
        FileCount++;
        if (FileCount % 500 == 0 && this.SearchParms.RootPath.IsNotBlank())
          NotifyHost("FILE", file.Replace(this.SearchParms.RootPath, String.Empty));

        string extension = Path.GetExtension(file).ToLower();//.Replace(".", String.Empty);
        if (extension.IsNotBlank() && !Extensions.ContainsKey(extension))
          Extensions.Add(extension, 0);
        if (Extensions.ContainsKey(extension))
          Extensions[extension]++;

        if (IncludeFile(file))
        {
          OSFile f = null;
          try
          {
            f = new OSFile(folder);
          }
          catch (Exception ex)
          {
            if (ex.GetType().Name == "PathTooLongException")
            {
              if (SearchParms.LogPathTooLongExceptions)
              {
                if (_logger == null)
                  _logger = new Logger();
                _logger.Log(folder.FullPath);
              }
              return;
            }
            else
              throw new Exception("An exception occurred while building the folder list.", ex);
          }



          f.FullPath = file;
          f.FileName = Path.GetFileName(file);
          f.SetFileProperties();
          folder.OSFileSet.Add(f.FileName, f);
          if (!this.SearchParms.SearchResults.OSFileList.ContainsKey(f.FileName.ToLower()))
            this.SearchParms.SearchResults.OSFileList.Add(f.FileName.ToLower(), f);
        }
      }



      //if (folder.SearchParms.Extensions.Count == 0)
      //{
      //  string[] files = Directory.GetFiles(folder._fullPath);
      //  foreach (string file in files)
      //  {
      //    if (IncludeFile(file))
      //    {
      //      OSFile f = new OSFile();
      //      f.FullPath = file;
      //      f.FileName = Path.GetFileName(file);
      //      f.SetLastChangedDateTime();
      //      folder.OSFileSet.Add(f.FileName, f);
      //      if (!this.SearchParms.SearchResults.OSFileList.ContainsKey(f.FileName.ToLower()))
      //        this.SearchParms.SearchResults.OSFileList.Add(f.FileName.ToLower(), f);
      //    }
      //  }
      //}
      //else
      //{
      //  foreach(string filter in folder.SearchParms.Extensions)
      //  {
      //    string extensionFilter = "*." + filter.Trim();
      //    string[] files = Directory.GetFiles(folder._fullPath, extensionFilter);
      //    foreach (string file in files)
      //    {
      //      string extension = Path.GetExtension(file);
      //      if (extension == "." + filter)
      //      {
      //        OSFile f = new OSFile();
      //        f.FullPath = file;
      //        f.FileName = Path.GetFileName(file);
      //        f.SetLastChangedDateTime();
      //        folder.OSFileSet.Add(f.FileName, f);
      //        if (!this.SearchParms.SearchResults.OSFileList.ContainsKey(f.FileName.ToLower()))
      //          this.SearchParms.SearchResults.OSFileList.Add(f.FileName.ToLower(), f);
      //      }
      //    }
      //  }
      //}
    }

    public bool IncludeFile(string fileName)
    {
      string fileNameCompare = Path.GetFileNameWithoutExtension(fileName).ToLower().Trim();
      string extCompare = Path.GetExtension(fileName).Replace(".", String.Empty).ToLower().Trim();

      if (extCompare.IsNotBlank() && this.SearchParms.Extensions.Count > 0)
      {
        bool includeByExtension = false;
        foreach(string ext in this.SearchParms.Extensions)
        {
          if (ext == "*.*" || ext == ".*")
          {
            includeByExtension = true;
            break;
          }

          string extNoPeriod = ext.Replace(".", String.Empty);
          if (extNoPeriod.Length > 1)
          {
            if (extNoPeriod.StartsWith("*"))
            {
              if (extCompare == extNoPeriod.Substring(1))
              {
                includeByExtension = true;
                break;
              }
            }
            else
            {
              if (extNoPeriod.EndsWith("*"))
              {
                if (extCompare == extNoPeriod.TrimEnd(Constants.AsteriskDelimiter))
                {
                  includeByExtension = true;
                  break;
                }
              }
              else
              {
                if (extCompare == extNoPeriod)
                {
                  includeByExtension = true;
                  break;
                }
              }
            }
          }
          else
          {

          }
        }

        if (!includeByExtension)
          return false;
      }

      // if we are including by extension and we want to filter out any files that do not match a file name pattern
      // if we are including it on this basis, it bypasses the following exclusion logic - for now...
      if (this.SearchParms.FileNameIncludes.Count > 0 && this.SearchParms.ExtensionAndFileNameIncludeLogicOp == LogicOp.AND)
      {
        foreach (var fileInclude in this.SearchParms.FileNameIncludes)
        {
          string fileIncludeLower = fileInclude.ToLower().Trim();
          if (fileNameCompare.Contains(fileIncludeLower))
            return true;
        }
        return false;
      }

      foreach (string excludeFileName in this.SearchParms.FileNameExcludes)
      {
        string exclude = excludeFileName.Trim().ToLower();

        if (exclude.StartsWith("*"))
        {
          if (fileNameCompare.EndsWith(exclude.Substring(1)))
            return false;
        }
        else
        {
          if (exclude.EndsWith("*"))
          {
            if (fileNameCompare.StartsWith(exclude.TrimEnd(Constants.AsteriskDelimiter)))
              return false;
          }
          else
          {
            if (fileNameCompare == exclude)
              return false;
          }
        }
      }

      return true;
    }

    public string GetFileListReport()
    {
      StringBuilder sb = new StringBuilder();

      sb.Append(BuildFileListReport(this));
      return sb.ToString();
    }

    public string GetFileListFromSearchResults()
    {
      StringBuilder sb = new StringBuilder();

      foreach (OSFile f in this.SearchParms.SearchResults.OSFileList.Values)
        sb.Append(PadTo(f.FileName, 30) + "    " + f.FullPath + g.crlf);

      return sb.ToString();
    }

    private string PadTo(string stringIn, int totalLength)
    {
      string stringWork = stringIn.Trim();

      while (stringWork.Length < totalLength)
        stringWork += "          ";

      return stringWork.Substring(0, totalLength);
    }

    public string BuildFileListReport()
    {
      return BuildFileListReport(this);
    }

    public string BuildFileListReport(OSFolder folder)
    {
      StringBuilder sb = new StringBuilder();

      if (folder.OSFileSet.Count > 0)
      {
        sb.Append(g.crlf + folder.DepthFromRoot.ToString("000") + " Folder: " + folder._fullPath + g.crlf);
        foreach (KeyValuePair<string, OSFile> kvp in folder.OSFileSet)
        {
          sb.Append("       " + kvp.Value.FileName + g.crlf);
        }
      }

      foreach (OSFolder f in folder.OSFolderSet)
      {
        sb.Append(BuildFileListReport(f));
      }

      return sb.ToString();
    }

    public string RunSearch()
    {
      StringBuilder sb = new StringBuilder();

      sb.Append(GetSearchResults(this));

      return sb.ToString();
    }

    public string GetSearchResults(OSFolder folder)
    {
      StringBuilder sb = new StringBuilder();

      bool folderHeaderWritten = false;

      foreach (KeyValuePair<string, OSFile> kvp in folder.OSFileSet)
      {
        string report = kvp.Value.RunSearch(folder.SearchParms);
        if (report.Trim().Length > 0)
        {
          if (!folderHeaderWritten)
          {
            sb.Append(g.crlf + folder.DepthFromRoot.ToString("000") + " Folder: " + folder.FullPath + g.crlf);
            folderHeaderWritten = true;
          }
          sb.Append("      File: " + kvp.Value.FileName + g.crlf + report);
        }
      }

      foreach (OSFolder f in folder.OSFolderSet)
      {
        sb.Append(GetSearchResults(f));
      }

      return sb.ToString();
    }

    public string GetChangedFiles()
    {
      StringBuilder sb = new StringBuilder();

      sb.Append(GetChangedSince(this));

      return sb.ToString();
    }

    public string GetChangedSince(OSFolder folder)
    {
      StringBuilder sb = new StringBuilder();

      bool folderHeaderWritten = false;

      foreach (KeyValuePair<string, OSFile> kvp in folder.OSFileSet)
      {
        if (kvp.Value.LastChangedDateTime > this.SearchParms.SinceDate)
        {
          if (!folderHeaderWritten)
          {
            sb.Append(g.crlf + folder.DepthFromRoot.ToString("000") + " Folder: " + folder.FullPath + g.crlf);
            folderHeaderWritten = true;
          }
          sb.Append("      Updated: " + kvp.Value.LastChangedDateTime.ToString("yyyy/MM/dd HH:mm:ss") + "   File: " + kvp.Value.FileName + g.crlf);
        }
      }

      foreach (OSFolder f in folder.OSFolderSet)
      {
        sb.Append(GetChangedSince(f));
      }

      return sb.ToString();
    }

    private Dictionary<string, OSFile> Get_FileList()
    {
      if (_fileList != null)
        return _fileList;

      var fileList = new Dictionary<string, OSFile>();
      AddFilesToList(this, fileList);
      _fileList = fileList;
      return fileList;
    }

    private void AddFilesToList(OSFolder folder, Dictionary<string, OSFile> fileList)
    {
      foreach (var file in folder.OSFileSet.Values)
      {
        if (!fileList.ContainsKey(file.LogicalPath))
          fileList.Add(file.LogicalPath, file);
      }

      foreach (var childFolder in folder.OSFolderSet)
        AddFilesToList(childFolder, fileList);
    }

    public override string ToString()
    {
      if (_fullPath != null)
        return _fullPath;

      return "NULL";
    }

    private OSFolder Get_RootFolder()
    {
      if (this.ParentFolder == null)
        return this;

      var rootFolder = this.ParentFolder;
      while (rootFolder != null)
      {
        var f = rootFolder.ParentFolder;
        if (f == null)
          break;
        else
          rootFolder = f;
      }

      return rootFolder;
    }

    private void NotifyHost(string objectType, string notificationMessage)
    {
      if (FSNotification == null)
        return;

      string counts = "[" + FolderCount.ToString("000000") + "|" + FileCount.ToString("000000") + "|" + ProcessedFileCount.ToString("000000") + "]";

      FSNotification(objectType + " " + counts + " " + notificationMessage);
    }

    private static bool InitializeStaticCounters()
    {
      FolderCount = 0;
      FileCount = 0;
      ProcessedFileCount = 0;
      Extensions = new SortedList<string, int>();
      return true;
    }

  }
}
