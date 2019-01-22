using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GS.Code
{
  public class Migrator
  {
    public MigrationResult DeleteDirectoryContents(string path, StringBuilder sb, bool recursive, bool reportOnly)
    {
      MigrationResult migrationResult = new MigrationResult();

      Console.WriteLine("Deleting contents of directory: " + path);

      if (Directory.Exists(path))
      {
        List<string> fileNames = Directory.GetFiles(path).ToList();

        foreach (string fileName in fileNames)
        {
          migrationResult.DestFilesToBeDeleted++;
          Console.WriteLine("Deleting file: " + Path.GetFileName(fileName) + (reportOnly ? " (REPORT-ONLY)" : String.Empty));
          sb.Append("DELETING FILE      " + Path.GetFileName(fileName) + (reportOnly ? " (REPORT-ONLY)" : String.Empty) + g.crlf);
          if (!reportOnly)
          {
            migrationResult.DestFilesDeleted++;
            FileAttributes fa = File.GetAttributes(fileName);
            File.SetAttributes(fileName, FileAttributes.Normal);
            File.Delete(fileName);
          }
        }

        if (recursive)
        {
          Console.WriteLine("Recursive sub-directory processing");
          List<string> folders = Directory.GetDirectories(path).ToList();
          foreach (string folder in folders)
          {
            migrationResult = DeleteDirectoryRecursive(folder, sb, migrationResult, reportOnly);
          }
        }
      }
      else
      {
        Console.WriteLine("Destination directory '" + path + "' does not exist - nothing to delete");
      }

      return migrationResult;
    }

    private MigrationResult DeleteDirectoryRecursive(string path, StringBuilder sb, MigrationResult migrationResult, bool reportOnly)
    {
      Console.WriteLine("Deleting contents of directory: " + path);

      List<string> fileNames = Directory.GetFiles(path).ToList();

      foreach (string fileName in fileNames)
      {
        migrationResult.DestFilesToBeDeleted++;
        Console.WriteLine("Deleting file: " + Path.GetFileName(fileName) + (reportOnly ? " (REPORT-ONLY)" : String.Empty));
        sb.Append("DELETING FILE      " + Path.GetFileName(fileName) + (reportOnly ? " (REPORT-ONLY)" : String.Empty) + g.crlf);
        if (!reportOnly)
        {
          migrationResult.DestFilesDeleted++;
          FileAttributes fa = File.GetAttributes(fileName);
          File.SetAttributes(fileName, FileAttributes.Normal);
          File.Delete(fileName);
        }
      }

      List<string> folders = Directory.GetDirectories(path).ToList();
      foreach (string folder in folders)
      {
        migrationResult = DeleteDirectoryRecursive(folder, sb, migrationResult, reportOnly);
      }

      sb.Append("DELETING DIRECTORY  " + path + (reportOnly ? " (REPORT-ONLY)" : String.Empty) + g.crlf);
      migrationResult.DestFoldersToBeDeleted++;
      Console.WriteLine("Deleting directory " + path + (reportOnly ? " (REPORT-ONLY)" : String.Empty));

      if (!reportOnly)
      {
        migrationResult.DestFoldersDeleted++;
        Directory.Delete(path);
      }

      return migrationResult;
    }

    public MigrationResult CopyFoldersAndFiles(MappingControl c, string source, string destination, StringBuilder sb, MigrationResult migrationResult, bool recursive, bool reportOnly)
    {
      Console.WriteLine("Copying source '" + source + "' to destination '" + destination + (recursive ? " (RECURSIVE)" + (reportOnly ? " (REPORT ONLY)" : String.Empty) : String.Empty));
      List<string> filePaths = Directory.GetFiles(source).ToList();

      if (!Directory.Exists(destination))
      {
        migrationResult.DestFoldersToBeCreated++;
        if (!reportOnly)
        {
          Console.WriteLine("Creating destination directory '" + destination + "'");
          migrationResult.DestFoldersCreated++;
          Directory.CreateDirectory(destination);
        }
      }

      foreach (string filePath in filePaths)
      {
        string fileName = Path.GetFileName(filePath);
        migrationResult.SourceFiles++;

        string ext = Path.GetExtension(fileName);
        IncludedExtension ie = c.GetIncludedExtension(ext);

        InclusionResult exclusionResult = c.CheckFileInclusion(ie, filePath);

        switch (exclusionResult)
        {
          case InclusionResult.IncludedByDefault:
          case InclusionResult.IncludedFileMatch:

            if (!Directory.Exists(c.Destination))
              migrationResult.DestFoldersToBeCreated++;

            migrationResult.FilesToBeCopied++;
            string destFileName = c.Destination + @"\" + fileName;
            if (File.Exists(destFileName))
              migrationResult.FilesToBeReplaced++;

            if (!reportOnly)
            {
              if (!Directory.Exists(c.Destination))
              {
                Directory.CreateDirectory(c.Destination);
                migrationResult.DestFoldersCreated++;
              }

              if (File.Exists(destFileName))
                migrationResult.FilesReplaced++;

              migrationResult.FilesCopied++;

              File.Copy(filePath, destFileName, true);
            }
            break;

          case InclusionResult.ExcludedBySpec:
          case InclusionResult.IncludedExtensionExclusionSpec:
          case InclusionResult.ExcludedByExtension:
            // get a report written here...
            migrationResult.FilesToBeExcluded++;
            if (!reportOnly)
              migrationResult.FilesExcluded++;
            break;
        }
      }

      if (recursive)
      {
        List<string> sourceSubDirectories = Directory.GetDirectories(source).ToList();
        foreach (string sourceSubDirectory in sourceSubDirectories)
        {
          string newDirectoryName = Path.GetFileName(sourceSubDirectory);
          migrationResult = CopyFoldersAndFiles(c, sourceSubDirectory, destination + @"\" + newDirectoryName, sb, migrationResult, recursive, reportOnly);
        }
      }

      return migrationResult;
    }

  }
}
