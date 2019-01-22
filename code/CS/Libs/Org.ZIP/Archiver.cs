using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Org.GS;

namespace Org.ZIP
{
  public class Archiver
  {
    public void CreateArchive(string sourceDirectory, string targetPath)
    {
      try
      {
        if (!Directory.Exists(sourceDirectory))
          throw new Exception("Archive source directory '" + sourceDirectory + "' does not exist.");

        string targetFolder = Path.GetDirectoryName(targetPath);
        if (!Directory.Exists(targetFolder))
          Directory.CreateDirectory(targetFolder);

        ZipFile.CreateFromDirectory(sourceDirectory, targetPath);
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting to create archive file from contents of directory '" +
                            sourceDirectory + "'.  Target file to create is '" + targetPath + "'.", ex);
      }
    }

    public void ExtractArchive(string sourcePath, string targetDirectory)
    {
      try
      {
        if (!File.Exists(sourcePath))
          throw new Exception("Archive source path '" + sourcePath + "' does not exist.");

        ZipFile.ExtractToDirectory(sourcePath, targetDirectory);
      }
      catch(Exception ex)
      {
        throw(new Exception("An exception occurred during the archival extract process.", ex));
      }
    }

    public FileSystemItem ProcessArchive(string path)
    {
      var fileSystemItemArchive = new FileSystemItem(path);
      using(FileStream zipToOpen = new FileStream(path, FileMode.Open))
      {
        using(ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
        {
          fileSystemItemArchive.LogicalPath = zipToOpen.Name.ToString();
          foreach (var zipArchiveEntry in archive.Entries)
          {
            var fileSystemItem = new FileSystemItem(zipArchiveEntry.FullName);
            fileSystemItem.LogicalPath = zipArchiveEntry.Name;
            fileSystemItemArchive.FileSystemItemSet.Add(zipArchiveEntry.Name, fileSystemItem);
          }
        }
      }

      return fileSystemItemArchive;
    }
  }
}
