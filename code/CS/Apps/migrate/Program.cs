using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Code;

namespace Org.Migrate
{
  class Program
  {
    private static a a;
    private static List<string> _profilePaths;
    private static ProfileSet _profileSet;
    private static bool _confirmClose;
    private static bool _reportOnly;

    static void Main(string[] args)
    {
      if (!Initialize())
      {
        EndProgram();
        return;
      }

      args = UpdateArgsFromConfig(args);

      if (!LoadProfiles())
      {
        EndProgram();
        return;
      }

      RunFunctions(args);
    }

    private static void RunFunctions(string[] args)
    {
      List<string> parms = args.ToList();

      if (parms.Count == 0)
      {
        DisplayHelp(false);
        return;
      }

      string formattedParms = FormatParms(parms);
      Console.WriteLine(formattedParms); 


      if (parms.Contains("-cc"))
        _confirmClose = true;

      if (parms.Contains("-ro"))
      {
        _reportOnly = true;
        Console.WriteLine("MIGRATE WILL BE RUN IN REPORT-ONLY MODE - NO CHANGES WILL BE MADE"); 
      }

      string profileName = String.Empty;
      foreach (string parm in parms)
      {
        if (parm.ToLower().Contains("p=") || parm.ToLower().Contains("profile="))
        {
          profileName = parm.ToTokenArray(Constants.EqualsDelimiter).Last().Trim();
          break;
        }
      }

      if (profileName.IsNotBlank())
      {
        Migrate(profileName); 
      }
      else
      {
        switch (parms.First().ToLower())
        {
          case "list":
            ListProfiles();
            break;

          case "help":
            DisplayHelp(true);
            break;

          default:
            DisplayHelp(false);
            break;
        }
      }

      EndProgram();
    }

    private static void Migrate(string profileName)
    {
      var migrator = new Migrator();

      Console.WriteLine(g.crlf + "*** MIGRATE FUNCTION BEGINNING ***"); 
      
      if (!_profileSet.ContainsKey(profileName))
      {
        Console.WriteLine(g.crlf + "*** ERROR ***  PROFILE '" + profileName + "' NOT FOUND");
        Console.Out.Flush();
        if (!_confirmClose)
          System.Threading.Thread.Sleep(2000);
        Console.WriteLine(g.crlf + "*** MIGRATE FUNCTION ENDING ***"); 
        return;
      }

      Console.WriteLine("Migration beginning for profile: " + profileName);

      try
      {
        _profileSet.ResolveVariables(profileName); 
        Profile p = _profileSet[profileName];

        bool profileNameShown = false;
        MigrationResult gtResult = new MigrationResult();

        StringBuilder sb = new StringBuilder();

        string reportOnlyText = String.Empty;
        if (_reportOnly)
          reportOnlyText = "*** REPORT ONLY ***";

        Console.WriteLine("Profile contains " + p.MappingControlSet.Count().ToString() + " MappingControl elements");

        sb.Append("Processing Beginning for Profile '" + p.Name + "'" + "  " + reportOnlyText + g.crlf2);

        foreach (MappingControl c in p.MappingControlSet.Values.Where(c => c.IsActive))
        {
          Console.WriteLine(g.crlf + "Processing started for MappingControl element '" + c.Name + "'");
          Console.WriteLine("Source directory: " + c.Source);
          Console.WriteLine("Destination directory: " + c.Destination);
          if (c.ClearDestination)
            Console.WriteLine("All existing files in destination will be deleted" + (c.Recursive ? " recursively" : String.Empty));
          if (c.Recursive)
            Console.WriteLine("File copy functions will occur recursively"); 

          sb.Append("-----------------------------------------------------------------" + g.crlf);
          sb.Append("Mapping Control Element:  '" + c.Name + "'" + g.crlf);
          sb.Append("-----------------------------------------------------------------" + g.crlf);

          MigrationResult mcResult = new MigrationResult();

          if (c.ClearDestination)
          {
            mcResult = migrator.DeleteDirectoryContents(c.Destination, sb, c.Recursive, _reportOnly);
          }

          mcResult = migrator.CopyFoldersAndFiles(c, c.Source, c.Destination, sb, mcResult, c.Recursive, _reportOnly);

          gtResult.SourceFolders += mcResult.SourceFolders;
          gtResult.SourceFiles += mcResult.SourceFiles;
          gtResult.DestFoldersToBeDeleted += mcResult.DestFoldersToBeDeleted;
          gtResult.DestFoldersDeleted += mcResult.DestFoldersDeleted;
          gtResult.DestFilesToBeDeleted += mcResult.DestFilesToBeDeleted;
          gtResult.DestFilesDeleted += mcResult.DestFilesDeleted;
          gtResult.DestFoldersToBeCreated += mcResult.DestFoldersToBeCreated;
          gtResult.DestFoldersCreated += mcResult.DestFoldersCreated;
          gtResult.FilesToBeCopied += mcResult.FilesToBeCopied;
          gtResult.FilesCopied += mcResult.FilesCopied;
          gtResult.FilesToBeReplaced += mcResult.FilesToBeReplaced;
          gtResult.FilesReplaced += mcResult.FilesReplaced;
          gtResult.FilesToBeExcluded += mcResult.FilesToBeExcluded;
          gtResult.FilesExcluded += mcResult.FilesExcluded;
        }

        sb.Append("-----------------------------------------------------------------" + g.crlf);
        sb.Append("Grand Totals for profile '" + p.Name + "'" + g.crlf);
        sb.Append("  Grand Total Source Folders                : " + gtResult.SourceFolders.ToString("#,##0") + g.crlf +
        sb.Append("  Grand Total Source Files                  : " + gtResult.SourceFiles.ToString("#,##0") + g.crlf +
                  "  Grand Total Dest Folders to be Deleted    : " + gtResult.DestFoldersToBeDeleted.ToString("#,##0") + g.crlf +
                  "  Grand Total Dest Folders Deleted          : " + gtResult.DestFoldersDeleted.ToString("#,##0") + g.crlf +
                  "  Grand Total Dest Files to be Deleted      : " + gtResult.DestFilesToBeDeleted.ToString("#,##0") + g.crlf +
                  "  Grand Total Dest Files Deleted            : " + gtResult.DestFilesDeleted.ToString("#,##0") + g.crlf +
                  "  Grand Total Dest Folders to be Created    : " + gtResult.DestFoldersToBeCreated.ToString("#,##0") + g.crlf +
                  "  Grand Total Dest Folders Created          : " + gtResult.DestFoldersCreated.ToString("#,##0") + g.crlf +
                  "  Grand Total Files to be Copied            : " + gtResult.FilesToBeCopied.ToString("#,##0") + g.crlf +
                  "  Grand Total Files Copied                  : " + gtResult.FilesCopied.ToString("#,##0") + g.crlf +
                  "  Grand Total Files to be Replaced          : " + gtResult.FilesToBeReplaced.ToString("#,##0") + g.crlf +
                  "  Grand Total Files Replaced                : " + gtResult.FilesReplaced.ToString("#,##0") + g.crlf +
                  "  Grand Total Files to be Excluded          : " + gtResult.FilesToBeExcluded.ToString("#,##0") + g.crlf +
                  "  Grand Total Files Excluded                : " + gtResult.FilesExcluded.ToString("#,##0") + g.crlf2));

        sb.Append("Code Pusher processing has completed successfully for Profile '" + p.Name + "'." + g.crlf);
        string report = sb.ToString();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred during the execution of migration profile '" + profileName + "'.", ex);
      }

      Console.WriteLine(g.crlf + "*** MIGRATE FUNCTION ENDING ***"); 
    }


    private static int CountFilesInSourceRecursive(string sourcePath, int count)
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


    private static string FormatParms(List<string> parms)
    {
      string formattedParms = "MIGRATE ARGS: ";
      foreach (string parm in parms)
      {
        formattedParms += " " + parm;
      }
      return formattedParms; 
    }

    private static void ListProfiles()
    {      
      Console.WriteLine(g.crlf + "*** LIST FUNCTION BEGINNING ***"); 
      Console.WriteLine(g.crlf + "Profile Directories Searched");

      foreach (string profilePath in _profilePaths)
      {
        string path = g.AppConfig.ResolveVariables(profilePath);
        Console.WriteLine("  Path: " + path); 
      }

      Console.WriteLine(g.crlf + "Profiles Loaded");
      foreach (var profile in _profileSet.Values)
      {
        Console.WriteLine("  Profile: " + profile.Name); 
      }
      Console.WriteLine(g.crlf + "*** LIST FUNCTION ENDING ***"); 
    }

    private static void DisplayHelp(bool confirmClose)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append(g.crlf + "When no arguments are entered with the migrate command this program information is displayed." + g.crlf);

      sb.Append(g.crlf + "Parameters: " + g.crlf2);

      if (confirmClose)
      {
        _confirmClose = true;
        return;
      }

      Console.WriteLine(sb.ToString()); 
      Console.Out.Flush();
      
      System.Threading.Thread.Sleep(2000);
    }

    private static void EndProgram()
    {
      if (_confirmClose)
      {
        Console.WriteLine(g.crlf + "PROGRAM MIGRATE IS ENDING - PRESS ANY KEY TO CLOSE");
        Console.ReadLine();
      }
    }



    private static bool Initialize()
    {
      Console.WriteLine(g.crlf + "MIGRATE INITIALIZING");
      _profilePaths = new List<string>();
      _profileSet = new ProfileSet();
      _reportOnly = false;

      try
      {
        a = new a();

        _confirmClose = false;
        if (g.CI("ConfirmClose").ToBoolean())
          _confirmClose = true;

        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine(g.crlf + "*** ERROR ***" +
          g.crlf + "An exception occurred during the startup of the migrate application - see details below." + g.crlf + ex.MessageReport());
        return false;
      }
    }

    private static bool LoadProfiles()
    {
      try
      {
        _profilePaths = g.GetList("ProfilePaths");

        foreach (string profilePath in _profilePaths)
        {
          string path = g.AppConfig.ResolveVariables(profilePath);
          List<string> profileFiles = Directory.GetFiles(path, "*.xml").ToList();
          foreach (string profileFile in profileFiles)
          {
            var profileSet = GetProfiles(profileFile);
            foreach (var kvp in profileSet)
            {
              if (_profileSet.ContainsKey(kvp.Key))
              {
                Console.WriteLine("Duplicate profile '" + kvp.Key + "' discarded - read from '" + profileFile + "'.");
              }
              else
              {
                _profileSet.Add(kvp.Key, kvp.Value);
              }
            }

            foreach(var kvp in profileSet.VariableSet)
            {
              string keyName = kvp.Key.Replace("$", String.Empty);

              if (!_profileSet.VariableSet.ContainsKey(keyName))
              {
                _profileSet.VariableSet.Add(keyName, kvp.Value); 
              }
            }
          }
        }

        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine(g.crlf + "*** ERROR ***" +
          g.crlf + "An exception occurred during the loading of migration profiles - see details below." + g.crlf + ex.MessageReport());
        return false;
      }
    }

    private static string[] UpdateArgsFromConfig(string[] args)
    {
      string configArgs = g.GetCI("ConfigArgs");
      if (configArgs.IsNotBlank())
      {
        args = configArgs.ToTokenArray(Constants.SpaceDelimiter);
      }
      return args;
    }

    private static ProfileSet GetProfiles(string profileFile)
    {
      try
      {
        string profileString = File.ReadAllText(profileFile);
        XElement profileSetXml = XElement.Parse(profileString);
        string profileSetXmlBeforeFormatted = profileSetXml.ToString();

        var f = new ObjectFactory2();
        f.LogToMemory = true;
        var profileSet = f.Deserialize(profileSetXml) as ProfileSet;
        
        XElement profileSetAfterXml = f.Serialize(profileSet); 
        string profileXmlAfterFormatted = profileSetAfterXml.ToString();

        if (profileXmlAfterFormatted != profileSetXmlBeforeFormatted)
        {
          throw new Exception("Before and after xml values for migration profiles do not match."); 
        }

        return profileSet; 
      }
      catch (Exception ex)
      {
        string memoryLog = g.MemoryLog; 

        throw new Exception("An exception occurred attempting to deserialize the migration profiles in file '" +
                            profileFile + "'." + g.crlf + "Exception messages follow:" + g.crlf + ex.ToReport()); 
      }
    }
  }
}
