using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using Org.GS.Logging;
using Org.GS.TextProcessing;

namespace Org.GS
{
  public enum FileSystemItemType
  {
    Folder,
    File
  }

  public class FileSystemItem 
  {
    public static bool StaticCountersInitialized = InitializeStaticCounters();

    private string _path;

    private string _fullName;
    public string FullName { get { return _fullName; } }

    public static event Action<FileSystemItem> FileSystemItemAdded;

    public static event Action<string> FSNotification;

    private static SearchParms _searchParms;
    public static SearchParms SearchParms 
    { 
      get { return _searchParms; }
      set { _searchParms = value; }
    }


    private static Logger _logger;

    private FileSystemItemType _fileSystemItemType;
    public FileSystemItemType FileSystemItemType { get { return _fileSystemItemType; } }
    public bool IsFile { get { return Get_IsFile(); } }
    public bool IsFolder { get { return Get_IsFolder(); } }
    public bool IsLastFolderUnderParent { get { return Get_IsLastFolderUnderParent(); } }

    public static SortedList<string, int> Extensions;
    public int FolderCount { get { return Get_FolderCount(); } }
    public static int TotalFileCount { get; set; }
    public static int TotalFolderCount { get; set; }
    public static int FsiCount { get { return TotalFolderCount + TotalFileCount; } }
    public static bool IsRecursionFinished = false;

    private static int _processedCount = 0;
    public static int ProcessedCount { get { return _processedCount; } }

    private string _rootFolderPath;
    public string RootFolderPath { get { return _rootFolderPath; } }

    private bool _isRootFolder = false;
    public bool IsRootFolder { get { return _isRootFolder; } }

    public List<FileSystemItem> Folders { get { return Get_Folders(); } }
    public List<FileSystemItem> Files { get { return Get_Files(); } }

    private FileSystemItem _parent;
    public FileSystemItem Parent { get { return _parent; } }

    public FileSystemItem RootFolder { get { return Get_RootFolder(); } }
    public int Depth { get; set; }

    private bool _isArchiveFile;
    public bool IsArchiveFile { get { return _isArchiveFile; } }

    private bool _isArchiveFileExpanded;
    public bool IsArchiveFileExpanded { get { return _isArchiveFileExpanded; } }

    private bool _isArchivedItem;
    public bool IsArchivedItem { get { return _isArchivedItem; } }

    public FileSystemItemSet FileSystemItemSet { get; set; }

    private Dictionary<string, FileSystemItem> _fileList;
    public Dictionary<string, FileSystemItem> FileList { get { return Get_FileList(); } }

    private string _name;
    public string Name { get { return _name; } } 

    private string _extension;
    public string Extension { get { return _extension; } } 

    private string _fileNameWithoutExtension;
    public string FileNameWithoutExtension { get { return _fileNameWithoutExtension; } } 


    private long _size;
    public long Size { get { return _size; } }

    private long _sizeIncludingDescendants;
    public long SizeIncludingDepescendants { get { return Get_SizeIncludingDescendants(); } }

    public string LogicalPath { get; set; }

    public bool IsFileIncluded { get; set; }

    private DateTime? _lastChangeDateTime;
    public DateTime? LastChangedDateTime { get { return _lastChangeDateTime; } }

    public bool IsProcessed { get; set; }

    private bool _maxPathExceeded = false;
    public bool MaxPathExceeded { get { return _maxPathExceeded; } }


    // this is currently only used as a container for second-level FSIs - no initialization is done, several fields will be null.
    public FileSystemItem()
    {
      this.FileSystemItemSet = new FileSystemItemSet();
      if (FileSystemItem._searchParms == null)
        FileSystemItem._searchParms = new SearchParms();
    }
    
    public FileSystemItem(string path)
    {
      // try to make sure that all your properties that are not going to be populated via lower levels 
      // get populated before you begin the recursion.  Consider the Initialize method.

      this.Initialize(path);

      if (FileSystemItem._searchParms == null)
        FileSystemItem._searchParms = new SearchParms();

      _rootFolderPath = path;
      _isRootFolder = true;
      IsProcessed = false;
    }

    public FileSystemItem(string path, SearchParms searchParms)
    {
      if (FileSystemItem._searchParms == null)
        FileSystemItem._searchParms = new SearchParms();

      _fileSystemItemType = FileSystemItemType.Folder;

      this.Initialize(path);

      _rootFolderPath = path;
      _isRootFolder = true;
      _searchParms = searchParms;
    }

    public FileSystemItem(string path, FileSystemItem parent, FileSystemItemType fileSystemItemType)
    {
      if (FileSystemItem._searchParms == null)
        FileSystemItem._searchParms = new SearchParms();

      _fileSystemItemType = fileSystemItemType;
      this.Initialize(path);
      _parent = parent;
    }

    public void PopulateFileSystemHierarchy()
    {
      this.PopulateFileSystemHierarchy(this);

      IsRecursionFinished = true;
      FireFileSystemItemAddedEvent();
    }

    private void Initialize(string path)
    {
      _path = path;
      _fullName = path;

      this.FileSystemItemSet = new FileSystemItemSet();
      _fileList = new Dictionary<string, FileSystemItem>();


      //_fileSystemItemType = Get_FileSystemItemType();
      if(_fileSystemItemType  == FileSystemItemType.File)
      {
        var fi = new FileInfo(_path);
        _size = fi.Length;
        _lastChangeDateTime = fi.LastWriteTime;
        _name = fi.Name;
        _extension = fi.Extension;
        _fileNameWithoutExtension = Path.GetFileNameWithoutExtension(_name); 
        if(_fileSystemItemType == FileSystemItemType.File && _extension == ".zip")
        {
          _isArchiveFile = true;
          if(_searchParms.ArchiveFileExpanded)
          {
            _isArchiveFileExpanded = true;
          }
        }
      }
      this.Depth = Get_RootFolder().ToInt32();
    }

    public void PopulateFileSystemHierarchy(FileSystemItem parent)
    {
      try
      {
        if (parent.FileSystemItemType == GS.FileSystemItemType.Folder)
        {
          var subFolders = Directory.GetDirectories(parent.FullName);
          foreach (var subFolder in subFolders)
          {
            TotalFolderCount++;
            var childFolder = new FileSystemItem(subFolder, parent, FileSystemItemType.Folder);
            parent.FileSystemItemSet.Add(childFolder);
            FireFileSystemItemAddedEvent();
            PopulateFileSystemHierarchy(childFolder);
          }

          var files = Directory.GetFiles(parent.FullName);
          foreach (var file in files)
          {
            var childFile = new FileSystemItem(file, parent, FileSystemItemType.File);
            if(childFile.IsArchiveFile && childFile.IsArchiveFileExpanded)
            {
              using (var zipToOpen = new FileStream(childFile.FullName, FileMode.Open))
              {
                using(var ai = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                  childFile.FileSystemItemSet = ai.CreateFSISet(childFile);
                }
              }
            }
            parent.FileSystemItemSet.Add(childFile);
            FireFileSystemItemAddedEvent();
            PopulateFileSystemHierarchy(childFile);
          }
        }
      }
      catch (Exception ex)
      {
        if (ex.GetType().Name.Contains("UnauthorizedAccessException"))
          return;
        throw new Exception("An exception occurred while attempting to build the folder list.", ex); 
      }
    }

    private Dictionary<string, FileSystemItem> Get_FileList(bool includeExpandedArchive = false)
    {
      if (_fileList != null)
        return _fileList;

      if (this.FileSystemItemType == null)
        throw new Exception("The FileSystemItemType property for this object has not been set.");

      var fileList = new Dictionary<string, FileSystemItem>();

      if (this.FileSystemItemType == FileSystemItemType.File)
      {
        if (includeExpandedArchive)
        {
          // build list from looping through the archive and populate '_fileList'
          return _fileList;
        }
        else
        {
          return null;
        }
      }
      
      // add the full list of all dependent files to "_fileList'

      AddFilesToList(this, fileList);
      _fileList = fileList;
      return _fileList;
    }

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
    
    private FileSystemItem Get_RootFolder()
    {
      if (this.Parent == null)
        return this;

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

    private long Get_SizeIncludingDescendants()
    {
      if (this.FileSystemItemType == null)
        throw new Exception("The FileSystemItemType property for this object has not been set.");

      if (this.FileSystemItemType == FileSystemItemType.File)
        return _size;

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

      var lastSibling = this.Parent.FileSystemItemSet.Where(f => f.IsFolder).LastOrDefault();

      if (lastSibling == null)
        return false;

      return this.FullName == lastSibling.FullName;
    }

    private int Get_FolderCount()
    {
      if (this.FileSystemItemSet == null || this.FileSystemItemSet.Count == 0)
        return 0; 

      return this.FileSystemItemSet.Where(f => f.IsFolder).Count();
    }

    public void RemoveFirstFolder()
    {
      int indexOfFirstFolder = -1;

      for (int i = 0; i < this.FileSystemItemSet.Count; i++)
      {
        if (this.FileSystemItemSet[i].IsFolder)
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

      return this.FileSystemItemSet.Where(f => f.IsFolder).ToList();
    }

    public List<FileSystemItem> Get_Files()
    {
      if (this.FileSystemItemSet == null || this.FileSystemItemSet.Count == 0)
        return new List<FileSystemItem>();

      return this.FileSystemItemSet.Where(f => f.IsFile).ToList();
    }


    public bool IncludeFolder(string folderName)
    {
			if (folderName.ToUpper().Contains("RECYCLE.BIN"))
				return false; 

      //if (this.SearchParms.FolderNameExcludes.Count == 0)
      //  return true;

      string folderCompare = Path.GetFileName(folderName).Trim().ToLower();

      //foreach (string excludeFolder in this.SearchParms.FolderNameExcludes)
      //{
      //  string exclude = excludeFolder.Trim().ToLower();

      //  if (exclude.StartsWith("*"))
      //  {
      //    if (exclude.EndsWith("*"))
      //    {
      //      if (folderCompare.Contains(exclude.Substring(1).TrimEnd(Constants.AsteriskDelimiter)))
      //      {
      //        return false;
      //      }
      //    }
      //    else
      //    {
      //      if (folderCompare.EndsWith(exclude.Substring(1)))
      //        return false;
      //    }
      //  }
      //  else
      //  {
      //    if (exclude.EndsWith("*"))
      //    {
      //      if (folderCompare.StartsWith(exclude.TrimEnd(Constants.AsteriskDelimiter)))
      //        return false;
      //    }
      //    else
      //    {
      //      if (folderCompare == exclude)
      //        return false;
      //    }
      //  }
      //}
      
      return true;
    }

    private void FireFileSystemItemAddedEvent()
    {
      if (FileSystemItemAdded == null)
        return;

      FileSystemItemAdded(RootFolder);
    }
    
    private static bool InitializeStaticCounters()
    {
      TotalFolderCount = 0;
      TotalFileCount = 0;
      Extensions = new SortedList<string, int>();
      return true;
    }

  }
}
