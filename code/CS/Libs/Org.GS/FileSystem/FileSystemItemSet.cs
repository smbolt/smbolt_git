using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS
{
  public class FileSystemItemSet : List<FileSystemItem>
  {
    public List<FileSystemItem> Folders { get { return Get_Folders(); } }
    public List<FileSystemItem> Files { get { return Get_Files(); } }

    private List<FileSystemItem> Get_Folders()
    {
      return this.Where(f => f.FileSystemItemType == FileSystemItemType.Folder).ToList();
    }

    private List<FileSystemItem> Get_Files()
    {
      return this.Where(f => f.FileSystemItemType == FileSystemItemType.File).ToList();
    }

    public FileSystemItem EnsureFolder(string path)
    {
      string[] pathNodes = path.Split(Constants.FSlashDelimiter, StringSplitOptions.RemoveEmptyEntries);
      FileSystemItemSet fsiSet = this;
      int pathNodeIndex = -1;
      return EnsureFolder(pathNodes, pathNodeIndex, fsiSet);
    }

    private FileSystemItem EnsureFolder(string[] pathNodes, int pathNodeIndex, FileSystemItemSet fsiSet)
    {
      pathNodeIndex++;

      string folderName = pathNodes[pathNodeIndex];
      var folder = fsiSet.GetFolder(folderName);

      if (folder == null)
      {
        folder = new FileSystemItem(folderName, null, FileSystemItemType.Folder);
        fsiSet.Add(folder); 
      }

      if (pathNodeIndex >= pathNodes.Length - 1)
        return folder;


      return EnsureFolder(pathNodes, pathNodeIndex, folder.FileSystemItemSet);
    }

    private FileSystemItem GetFolder(string folderName)
    {
      return this.Where(f => f.FullName.ToLower() == folderName.ToLower()).FirstOrDefault();
    }

  }
}
