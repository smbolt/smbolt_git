using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using Org.GS.Logging;
using Org.GS;

namespace Org.GS
{
  public enum FileSystemItemType
  {
    NotSet = 0,
    Folder,
    File
  }

  public class FileSystemItem : IDisposable
  {
    public static event Action<FileSystemItem> FileSystemItemAdded;
    public static event Action<string> FSNotification;
    private static Logger _logger;

    public static bool StaticCountersInitialized = InitializeStaticCounters();

    public static SearchParms SearchParms {
      get;
      private set;
    }
    private static string OriginalFullPath {
      get;
      set;
    }
    public static string RootFolderPath {
      get;
      private set;
    }
    public static FileSystemItem RootFolder {
      get;
      private set;
    }
    public bool IsRootFolder {
      get;
      private set;
    }

    public string Name {
      get;
      set;
    }
    public string Extension {
      get;
      private set;
    }
    private FileInfo _fileInfo;
    public FileSystemItemType FileSystemItemType  {
      get;
      private set;
    }

    public string FullPath {
      get {
        return Get_FullPath();
      }
    }
    public string PathToRoot {
      get {
        return Get_PathToRoot();
      }
    }
    public bool IsFile {
      get {
        return Get_IsFile();
      }
    }
    public bool IsFolder {
      get {
        return Get_IsFolder();
      }
    }
    public bool IsLastFolderUnderParent {
      get {
        return Get_IsLastFolderUnderParent();
      }
    }
    public int FolderCount {
      get {
        return Get_FolderCount();
      }
    }
    public long SizeIncludingDependants {
      get {
        return Get_SizeIncludingDescendants();
      }
    }

    public static int TotalFileCount {
      get;
      set;
    }
    public static int TotalFolderCount {
      get;
      set;
    }
    public static int FsiCount {
      get {
        return TotalFolderCount + TotalFileCount;
      }
    }
    public static int ProcessedCount {
      get;
      private set;
    }

    public static SortedList<string, int> Extensions;
    public static bool IsRecursionFinished;

    public FileSystemItem Parent {
      get;
      set;
    }
    public FileSystemItemSet FileSystemItemSet {
      get;
      set;
    }
    public List<FileSystemItem> Folders {
      get {
        return Get_Folders();
      }
    }
    public List<FileSystemItem> Files {
      get {
        return Get_Files();
      }
    }
    public Dictionary<string, FileSystemItem> FileList {
      get;
      private set;
    }


    public bool IsZipArchiveFile {
      get;
      private set;
    }
    public bool IsZipArchiveFilesExpanded {
      get;
      set;
    }
    public bool IsZipArchivedItem {
      get;
      set;
    }
    public string FileNameWithoutExtension {
      get;
      set;
    }
    private long _size;
    public long Size {
      get {
        return Get_Size();
      }
    }
    public int Depth {
      get {
        return Get_Depth();
      }
    }

    public string LogicalPath {
      get;
      set;
    }
    public bool IsFileIncluded {
      get;
      set;
    }

    public DateTime CreationTime {
      get {
        return _fileInfo.CreationTime;
      }
    }
    public DateTime LastWriteTime {
      get {
        return _fileInfo.LastWriteTime;
      }
    }
    public DateTime LastAccessTime {
      get {
        return _fileInfo.LastAccessTime;
      }
    }

    public bool IsProcessed {
      get;
      set;
    }
    public bool MaxPathExceeded {
      get;
      private set;
    }

    public string Report {
      get {
        return Get_Report();
      }
    }
    public string RootReport {
      get {
        return Get_RootReport();
      }
    }
    public static List<FileSystemItem> ItemsWithAccessViolations {
      get;
      set;
    }

    // constructor to be used for top level item with optional SearchParms
    public FileSystemItem(string path, SearchParms searchParms = null)
    {
      if (path.IsBlank())
        throw new Exception("The FileSystemItem constructor cannot be involved with a null or blank path parameter.");

      FileSystemItem.SearchParms = searchParms == null ? new SearchParms() : searchParms;

      ItemsWithAccessViolations = new List<FileSystemItem>();
      OriginalFullPath = path.Trim();
      RootFolderPath = OriginalFullPath;
      this.IsRootFolder = true;
      RootFolder = this;
      TotalFolderCount = 1;
      TotalFileCount = 0;

      this.Initialize(OriginalFullPath);
      IsProcessed = false;
    }

    // this is a private constructor to be used only internally when creating hierarchies of FileSystemItems
    private FileSystemItem(string path, FileSystemItem parent, FileSystemItemType fileSystemItemType)
    {
      if (path.IsBlank())
        throw new Exception("The FileSystemItem constructor cannot be involved with a null or blank path parameter.");

      if (fileSystemItemType == GS.FileSystemItemType.NotSet)
        throw new Exception("The internal constructor of FileSystemItem cannot be invoked with the FileSysteItemType parameter equal to 'NotSet'.");

      this.FileSystemItemType = fileSystemItemType;

      this.Parent = parent;
      this.Initialize(path);
    }

    // CONSTRUCTOR COMMENTED OUT FOR NOW - ONLY USED WHEN PROCESSING ARCHIVE ITEMS - WILL WAIT UNTIL THAT CODE IS REVISITED
    // this is currently only used as a container for second-level FSIs - no initialization is done, several fields will be null.
    //public FileSystemItem()
    //{
    //  this.FileSystemItemSet = new FileSystemItemSet();
    //  if (FileSystemItem._searchParms == null)
    //    FileSystemItem._searchParms = new SearchParms();
    //}

    public void PopulateFileSystemHierarchy()
    {
      this.PopulateFileSystemHierarchy(this);

      IsRecursionFinished = true;
      FireFileSystemItemAddedEvent();
    }

    private void Initialize(string path)
    {
      try
      {
        _fileInfo = new FileInfo(path);

        if ((_fileInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
          this.FileSystemItemType = FileSystemItemType.Folder;
        else
          this.FileSystemItemType = FileSystemItemType.File;

        this.Name = _fileInfo.Name;

        if (this.FileSystemItemType == FileSystemItemType.File)
        {
          _size = _fileInfo.Length;
          this.Extension = _fileInfo.Extension.Trim();
          this.FileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.Name);

          if (this.Extension.ToLower() == ".zip")
          {
            this.IsZipArchiveFile = true;
            this.IsZipArchiveFilesExpanded = FileSystemItem.SearchParms.ExpandZipArchives;
          }
        }
        else
        {
          _size = 0;
          this.Extension = String.Empty;
          this.FileNameWithoutExtension = this.Name;
        }

        this.FileSystemItemSet = new FileSystemItemSet();
        this.FileList = new Dictionary<string, FileSystemItem>();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to initialize the FileSystemItem object. The path is '" + path + "'.", ex);
      }
    }

    public void PopulateFileSystemHierarchy(FileSystemItem fsi)
    {
      try
      {
        if (fsi.FileSystemItemType == GS.FileSystemItemType.Folder)
        {
          var subFolders = Directory.GetDirectories(fsi.FullPath);
          foreach (var subFolder in subFolders)
          {
            if (!this.IncludeFolder(subFolder, fsi.Depth))
              continue;

            TotalFolderCount++;
            var childFolder = new FileSystemItem(subFolder, fsi, FileSystemItemType.Folder);
            fsi.FileSystemItemSet.Add(childFolder.Name, childFolder);
            FireFileSystemItemAddedEvent();
            PopulateFileSystemHierarchy(childFolder);
          }

          var files = Directory.GetFiles(fsi.FullPath);
          foreach (var file in files)
          {
            var childFile = new FileSystemItem(file, fsi, FileSystemItemType.File);

            //if(childFile.IsZipArchiveFile && childFile.IsZipArchiveFileExpanded)
            //{
            //  using (var zipToOpen = new FileStream(childFile.FullPath, FileMode.Open))
            //  {
            //    using(var ai = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
            //    {
            //      childFile.FileSystemItemSet = ai.CreateFSISet(childFile);
            //    }
            //  }
            //}

            fsi.FileSystemItemSet.Add(childFile.Name, childFile);
            FireFileSystemItemAddedEvent();
          }
        }
      }
      catch (Exception ex)
      {
        if (ex.GetType().Name.Contains("UnauthorizedAccessException"))
        {
          ItemsWithAccessViolations.Add(this);

          if (SearchParms.FailOnAccessViolations)
            throw new Exception("The PopulateFileSystemHierarchy (recursive) method failed with an access violation.");

          return;
        }

        throw new Exception("An exception occurred while attempting to build the folder list.", ex);
      }
    }

    //private Dictionary<string, FileSystemItem> Get_FileList(bool includeExpandedArchive = false)
    //{
    //  if (this.FileList != null)
    //    return this.FileList;

    //  var fileList = new Dictionary<string, FileSystemItem>();

    //  if (this.FileSystemItemType == FileSystemItemType.File)
    //  {
    //    if (includeExpandedArchive)
    //    {
    //      // build list from looping through the archive and populate '_fileList'
    //      return _fileList;
    //    }
    //    else
    //    {
    //      return null;
    //    }
    //  }

    //  // add the full list of all dependent files to "_fileList'
    //  AddFilesToList(this, fileList);

    //  this.FileList = fileList;
    //  return _fileList;
    //}

    private void AddFilesToList(FileSystemItem folder, Dictionary<string, FileSystemItem> fileList)
    {
      //foreach (var file in folder.FileSystemItemSet.Values)
      //{
      //  if (!fileList.ContainsKey(file.LogicalPath))
      //    fileList.Add(file.LogicalPath, file);
      //}

      //foreach (var childFolder in folder.FileSystemItemSet)
      //  AddFilesToList(childFolder, fileList);
    }

    private string Get_FullPath()
    {
      if (this.IsRootFolder)
        return OriginalFullPath;

      string pathToRoot = this.PathToRoot;

      return pathToRoot + @"\" + this.Name;
    }

    private string Get_LevelName()
    {
      if (this.Name.IsBlank())
        return "FSI NAME IS BLANK";

      return Path.GetFileName(this.Name);
    }

    private string Get_PathToRoot()
    {
      if (this.IsRootFolder || this.Parent == null)
        return String.Empty;

      string pathToRoot = String.Empty;
      var parent = this.Parent;

      while (true)
      {
        if (parent.IsRootFolder)
        {
          if (pathToRoot.IsBlank())
            pathToRoot = RootFolderPath;
          else
            pathToRoot = RootFolderPath + @"\" + pathToRoot;
          break;
        }
        else
        {
          if (pathToRoot.IsBlank())
          {
            pathToRoot = parent.Name.Trim();
          }
          else
          {
            pathToRoot = parent.Name.Trim() + @"\" + pathToRoot;
          }
        }

        parent = parent.Parent;
      }

      return pathToRoot;
    }

    private FileSystemItem Get_RootFolder()
    {
      if (this.Parent == null)
      {
        if (this.IsFolder)
          return this;
        else
          return null;
      }

      var rootFolder = this.Parent;
      while (rootFolder != null)
      {
        var f = rootFolder.Parent;
        if (f == null)
          break;
        else
          rootFolder = f;
      }

      return rootFolder;
    }

    private long Get_Size()
    {
      if (this.FileSystemItemType == FileSystemItemType.File)
        return _size;

      return Get_SizeIncludingDescendants();
    }

    private int Get_Depth()
    {
      if (this.IsRootFolder || this.Parent == null)
        return 0;

      int depth = 0;

      var parent = this.Parent;
      while (parent != null)
      {
        depth++;
        var f = parent.Parent;
        if (f == null)
          break;
        else
          parent = f;
      }

      return depth;
    }

    private long Get_SizeIncludingDescendants()
    {
      if (this.FileSystemItemType == FileSystemItemType.File)
        return this.Size;

      long size = 0; //  ComputeSize();
      return size;
    }

    private bool Get_IsFile()
    {
      if (this.FileSystemItemType == FileSystemItemType.File)
        return true;

      if (this.FileSystemItemType == FileSystemItemType.Folder)
        return false;

      throw new Exception("The FileSystemItemType property for this object has not been set.");
    }

    private bool Get_IsFolder()
    {
      if (this.FileSystemItemType == FileSystemItemType.Folder)
        return true;

      if (this.FileSystemItemType == FileSystemItemType.File)
        return false;

      throw new Exception("The FileSystemItemType property for this object has not been set.");
    }

    private bool Get_IsLastFolderUnderParent()
    {
      if (!this.IsFolder)
        return false;

      if (this.Parent == null)
        return false;

      var lastSibling = this.Parent.FileSystemItemSet.Values.Where(f => f.IsFolder).LastOrDefault();

      if (lastSibling == null)
        return false;

      return this.FullPath == lastSibling.FullPath;
    }

    private int Get_FolderCount()
    {
      if (this.FileSystemItemSet == null || this.FileSystemItemSet.Count == 0)
        return 0;

      return this.FileSystemItemSet.Values.Where(f => f.IsFolder).Count();
    }

    public void RemoveFirstFolder()
    {
      int indexOfFirstFolder = -1;

      for (int i = 0; i < this.FileSystemItemSet.Count; i++)
      {
        if (this.FileSystemItemSet.Values[i].IsFolder)
        {
          indexOfFirstFolder = i;
          break;
        }
      }

      if (indexOfFirstFolder > -1)
        this.FileSystemItemSet.RemoveAt(indexOfFirstFolder);
    }

    public List<FileSystemItem> Get_Folders()
    {
      if (this.FileSystemItemSet == null || this.FileSystemItemSet.Count == 0)
        return new List<FileSystemItem>();

      return this.FileSystemItemSet.Values.Where(f => f.IsFolder).ToList();
    }

    public List<FileSystemItem> Get_Files()
    {
      if (this.FileSystemItemSet == null || this.FileSystemItemSet.Count == 0)
        return new List<FileSystemItem>();

      return this.FileSystemItemSet.Values.Where(f => f.IsFile).ToList();
    }


    public bool IncludeFolder(string folderName, int folderDepth)
    {
      try
      {
        if (folderName.ToUpper().Contains("RECYCLE.BIN"))
          return false;

        if (SearchParms.FolderNameIncludes == null)
          SearchParms.FolderNameIncludes = new List<string>();

        if (SearchParms.FolderNameExcludes == null)
          SearchParms.FolderNameExcludes = new List<string>();

        if (SearchParms.FolderNameIncludes.Count == 0 && SearchParms.FolderNameExcludes.Count == 0)
          return true;

        // Folder inclusing logic is "Includes" prioritized - all folders are implicitly included unless there is an include spec.
        // When there is an include spec, then only those included in the spec are included.
        // Regardless of how a folder is included (implicitly, i.e. no include spec) or explicitly (via an include spec)
        // it can only be excluded via an exclude spec.
        // So we check the include spec first...

        string folderNameCompare = Path.GetFileName(folderName).Trim().ToLower();

        if (SearchParms.FolderNameIncludes.Count > 0)
        {
          bool excludedByOmissionInIncludeSpec = true;

          // folder is included by default - see if it is excluded via not being included in the include spec.

          int includeSpecsAtThisDepth = 0;

          foreach (var includeFolderSpec in SearchParms.FolderNameIncludes)
          {
            string includeSpec = includeFolderSpec.ToLower().Trim();

            if (includeSpec.StartsWith("["))
            {
              string depthString = includeSpec.GetTextBetweenBrackets();
              if (depthString.IsInteger())
              {
                int depth = depthString.ToInt32();
                if (depth != folderDepth)
                  continue;
                includeSpec = includeSpec.Substring(includeSpec.IndexOf(']') + 1);
              }
            }

            includeSpecsAtThisDepth++;
            if (folderNameCompare.MatchesPattern(includeSpec))
            {
              excludedByOmissionInIncludeSpec = false;
              break;
            }
          }

          // if there is an include spec that doesn't include the folder - then it's not included.
          if (includeSpecsAtThisDepth > 0 && excludedByOmissionInIncludeSpec)
            return false;
        }


        // If this point is reached, the folder is included implicitly or explicitly.
        // If will be included unless it is explicitly excluded by an exclude spec.
        if (SearchParms.FolderNameExcludes.Count > 0)
        {
          foreach (var excludeFolderSpec in SearchParms.FolderNameExcludes)
          {
            string excludeSpec = excludeFolderSpec.ToLower().Trim();

            if (excludeSpec.StartsWith("["))
            {
              string depthString = excludeSpec.GetTextBetweenBrackets();
              if (depthString.IsInteger())
              {
                int depth = depthString.ToInt32();
                if (depth != this.Depth)
                  continue;
                excludeSpec = excludeSpec.Substring(excludeSpec.IndexOf(']') + 1);
              }
            }

            if (folderNameCompare.MatchesPattern(excludeSpec))
              return false;
          }
        }

        return true;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to determine if the folder '" + folderName + "' should be included in " +
                            "processing based on the SearchParms.", ex);
      }
    }

    private void FireFileSystemItemAddedEvent()
    {
      if (FileSystemItemAdded == null)
        return;

      FileSystemItemAdded(RootFolder);
    }

    private static bool InitializeStaticCounters()
    {
      if (StaticCountersInitialized)
        return true;

      IsRecursionFinished = false;
      TotalFolderCount = 0;
      TotalFileCount = 0;
      Extensions = new SortedList<string, int>();
      return true;
    }

    private string Get_Report()
    {
      StringBuilder sb = new StringBuilder();
      BuildReport(sb, this);
      string report = sb.ToString();
      return report;
    }

    private void BuildReport(StringBuilder sb, FileSystemItem fsi)
    {
      string indent = g.BlankString(2 * fsi.Depth);

      if (fsi.Depth == 0)
      {
        sb.Append("+ " + RootFolderPath + g.crlf);
      }
      else
      {
        sb.Append(indent + (fsi.IsFile ?
                            ("\xB7").ToString() + " "+ fsi.Name + g.crlf :
                            "+ " +  fsi.Name + g.crlf));
      }

      if (fsi.IsFolder)
      {
        foreach (var folder in fsi.Folders)
        {
          BuildReport(sb, folder);
        }

        foreach (var file in fsi.Files)
        {
          BuildReport(sb, file);
        }
      }
    }

    private string Get_RootReport()
    {
      return RootFolder.Report;
    }

    public void Dispose()
    {

    }

  }
}
