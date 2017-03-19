using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Logging;

namespace Org.GS.Code
{
  public class MigrationEngine : IDisposable
  {
    private Logger _logger;
    private ProfileSet _profileSet { get; set; }
    private string fileDeleteMessage = "DELETING FILE ";


    public MigrationEngine(string configPath)
    {
      _logger = new Logger();
      _profileSet = new ProfileSet();

      if (configPath.IsBlank())
        throw new Exception("Path to migration profiles configuration file is blank.");

      if (!File.Exists(configPath))
        throw new Exception("Path for migration profiles configuration does not exist '" + configPath + "'.");

      string profilesString = File.ReadAllText(configPath);
      XElement profilesXml = XElement.Parse(profilesString);

      var f = new ObjectFactory2(g.CI("InDiagnosticsMode").ToBoolean());
      f.LogToMemory = g.CI("LogToMemory").ToBoolean();
      _profileSet = f.Deserialize(profilesXml) as ProfileSet;
    }

    public string RunProfile(string profileName, bool reportOnly)
    {
      try
      {
        if (reportOnly)
          fileDeleteMessage = "FILE TO DELETE";

        _logger.Log("Running profile '" + profileName + "', reportOnly='" + reportOnly.ToString() + "'.");

        bool profileNameShown = false;

        int grandTotalSource = 0;
        int grandTotalMoved = 0;
        int grandTotalNameExcl = 0;
        int grandTotalExtExcl = 0;
        int grandTotalDeleted = 0;

        StringBuilder sb = new StringBuilder();

        Profile p = _profileSet.Values.Where(e => e.NameLower == profileName.ToLower().Trim()).FirstOrDefault();

        if (p == null)
          return "Profile '" + profileName + "' not found in migration configuration.";

        int filesPushed = 0;

        string reportOnlyText = String.Empty;
        if (reportOnly)
          reportOnlyText = "*** REPORT ONLY ***";

        sb.Append("Processing Beginning for Profile '" + p.Name + "'" + "  " + reportOnlyText + g.crlf2);

        foreach (MappingControl c in p.MappingControlSet.Values.Where(c => c.IsActive))
        {
          sb.Append("-----------------------------------------------------------------" + g.crlf);
          sb.Append("Mapping Control Element:  '" + c.Name + "'" + g.crlf);
          sb.Append("-----------------------------------------------------------------" + g.crlf);
          int totalSource = 0;
          int totalMoved = 0;
          int totalNameExcl = 0;
          int totalExtExcl = 0;
          int totalDeleted = 0;

          if (c.ClearDestination)
          {
            sb.Append("Deleting files in destination path '" + c.Destination + "'" + g.crlf);
            if (Directory.Exists(c.Destination))
            {
              string[] filesToDelete = Directory.GetFiles(c.Destination);
              foreach (string fileName in filesToDelete)
              {
                sb.Append(fileDeleteMessage + "     " + fileName + g.crlf);
                if (!reportOnly)
                {
                  File.Delete(fileName);
                  totalDeleted++;
                }
              }

              if (c.Recursive)
              {
                sb.Append("RECURSIVELY DELETING DIRECTORY  " + c.Destination + g.crlf);
                var fsu = new FileSystemUtility();
                totalDeleted += DeleteDirectoryContentsRecursive(c.Destination, sb, 0);
              }
            }
            sb.Append(g.crlf);
          }

          if (c.Recursive)
          {
            sb.Append("RECURSIVELY COPYING DIRECTORY  " + c.Source + " to " + c.Destination + g.crlf);
            var fsu = new FileSystemUtility();
            totalSource += CountFilesInSourceRecursive(c.Source, 0);
            totalMoved += CopyFoldersAndFiles(c.Source, c.Destination, sb, 0);
          }


          string[] files = Directory.GetFiles(c.Source);

          foreach (string file in files)
          {
            string fileName = Path.GetFileName(file);
            totalSource++;

            string ext = Path.GetExtension(file);
            IncludedExtension ie = c.GetIncludedExtension(ext);

            if (ie != null)
            {
              if (ie.IncludeFile(file))
              {
                bool includeFile = true;
                foreach (var fileToExclude in c.ExcludedFileSet)
                {
                  string fileNameMatch = fileToExclude.Replace("*", String.Empty).ToLower();
                  string fileNameLower = fileName.ToLower();
                  if (fileNameLower.Contains(fileNameMatch))
                  {
                    includeFile = false;
                    break;
                  }
                }

                if (includeFile)
                {
                  sb.Append("FILE MOVED         " + fileName + g.crlf);
                  if (!reportOnly)
                  {
                    filesPushed++;
                    string destFileName = c.Destination + @"\" + fileName;
                    if (!Directory.Exists(c.Destination))
                      Directory.CreateDirectory(c.Destination);
                    File.Copy(file, destFileName, true);
                    totalMoved++;
                  }
                }
                else
                {
                  sb.Append("EXCLUDED (NAME)    " + fileName + g.crlf);
                  totalNameExcl++;
                }
              }
              else
              {
                sb.Append("EXCLUDED (NAME)    " + fileName + g.crlf);
                totalNameExcl++;
              }
            }
            else
            {
              sb.Append("EXCLUDED (EXT)     " + fileName + g.crlf);
              totalExtExcl++;
            }
          }

          if (!profileNameShown)
          {
            sb.Append(g.crlf + "Subtotals for Mapping Control Element:  '" + c.Name + "'" + g.crlf2);
            profileNameShown = true;
          }

          sb.Append("  Source      : " + c.Source + g.crlf +
                    "  Destination : " + c.Destination + g.crlf +
                    "  Clear First : " + c.ClearDestination.ToString() + g.crlf);

          sb.Append("  Profile Destination Files Deleted         : " + totalDeleted.ToString("#,##0") + g.crlf +
                    "  Profile Total Source Files                : " + totalSource.ToString("#,##0") + g.crlf +
                    "  Profile Total Files Excluded (name)       : " + totalNameExcl.ToString("#,##0") + g.crlf +
                    "  Profile Total Files Excluded (ext)        : " + totalExtExcl.ToString("#,##0") + g.crlf +
                    "  Profile Total Files Moved                 : " + totalMoved.ToString("#,##0") + g.crlf2 + g.crlf);

          grandTotalDeleted += totalDeleted;
          grandTotalSource += totalSource;
          grandTotalNameExcl += totalNameExcl;
          grandTotalExtExcl += totalExtExcl;
          grandTotalMoved += totalMoved;
        }


        sb.Append("-----------------------------------------------------------------" + g.crlf);
        sb.Append("Grand Totals for profile '" + p.Name + "'" + g.crlf);
        sb.Append("  Grand Total Destination Files Deleted     : " + grandTotalDeleted.ToString("#,##0") + g.crlf +
                  "  Grand Total Source Files                  : " + grandTotalSource.ToString("#,##0") + g.crlf +
                  "  Grand Total Files Excluded (name)         : " + grandTotalNameExcl.ToString("#,##0") + g.crlf +
                  "  Grand Total Files Excluded (ext)          : " + grandTotalExtExcl.ToString("#,##0") + g.crlf +
                  "  Grand Total Files Moved                   : " + grandTotalMoved.ToString("#,##0") + g.crlf2);

        sb.Append("Code Pusher processing has completed successfully for Profile '" + p.Name + "'." + g.crlf);

        string report = sb.ToString();


        _logger.Log("Profile '" + profileName + "' is finished - report follows." + g.crlf + report);

        return report;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to run migration profile '" + profileName + "' reportOnly='" + reportOnly.ToString() + "'.", ex);
      }
    }

    public bool ContainsProfile(string profileName)
    {
      if (_profileSet == null || profileName.IsBlank())
        return false;

      var profile = _profileSet.Values.Where(p => p.NameLower == profileName.ToLower().Trim()).FirstOrDefault();
      if (profile == null)
        return false;

      return true;
    }

    private int DeleteDirectoryContentsRecursive(string path, StringBuilder sb, int filesDeleted)
    {
      List<string> fileNames = Directory.GetFiles(path).ToList();

      foreach (string fileName in fileNames)
      {
        sb.Append(fileDeleteMessage + "     " + fileName + g.crlf);
        filesDeleted++;
        FileAttributes fa = File.GetAttributes(fileName);
        File.SetAttributes(fileName, FileAttributes.Normal);
        File.Delete(fileName);
      }

      List<string> folders = Directory.GetDirectories(path).ToList();
      foreach (string folder in folders)
      {
        filesDeleted += DeleteDirectoryRecursive(folder, sb, 0);
      }

      return filesDeleted;
    }

    private int DeleteDirectoryRecursive(string path, StringBuilder sb, int filesDeleted)
    {
      List<string> fileNames = Directory.GetFiles(path).ToList();

      foreach (string fileName in fileNames)
      {
        sb.Append(fileDeleteMessage + "     " + fileName + g.crlf);
        filesDeleted++;
        FileAttributes fa = File.GetAttributes(fileName);
        File.SetAttributes(fileName, FileAttributes.Normal);
        File.Delete(fileName);
      }

      List<string> folders = Directory.GetDirectories(path).ToList();
      foreach (string folder in folders)
      {
        filesDeleted += DeleteDirectoryRecursive(folder, sb, 0);
      }

      Directory.Delete(path);

      return filesDeleted;
    }

    private int CountFilesInSourceRecursive(string sourcePath, int count)
    {
      List<string> fileNames = Directory.GetFiles(sourcePath).ToList();

      foreach (string fileName in fileNames)
      {
        count++;
      }

      List<string> directories = Directory.GetDirectories(sourcePath).ToList();
      foreach (string directory in directories)
      {
        string newDirectoryName = Path.GetFileName(directory);
        count += CountFilesInSourceRecursive(directory, 0);
      }

      return count;
    }

    private int CopyFoldersAndFiles(string sourcePath, string destPath, StringBuilder sb, int filesPushed)
    {
      List<string> fileNames = Directory.GetFiles(sourcePath).ToList();

      if (!Directory.Exists(destPath))
      {
        Directory.CreateDirectory(destPath);
      }

      foreach (string fileName in fileNames)
      {
        sb.Append("FILE MOVED         " + fileName + g.crlf);
        filesPushed++;
        File.Copy(fileName, destPath + @"\" + Path.GetFileName(fileName));
      }

      List<string> directories = Directory.GetDirectories(sourcePath).ToList();
      foreach (string directory in directories)
      {
        string newDirectoryName = Path.GetFileName(directory);
        if (!Directory.Exists(destPath + @"\" + newDirectoryName))
        {
          Directory.CreateDirectory(destPath + @"\" + newDirectoryName);
        }
        filesPushed += CopyFoldersAndFiles(directory, destPath + @"\" + newDirectoryName, sb, 0);
      }

      return filesPushed;
    }

    public void Dispose()
    {
    }
  }
}
