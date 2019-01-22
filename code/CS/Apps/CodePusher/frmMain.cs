using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Org.GS;
using Org.GS.Code;

namespace Org.CodePusher
{
  public partial class frmMain : Form
  {
    private a a;

    private string _source;
    private string _dest;
    private ProfileSet _profiles;
    private bool _isFirstShowing;
    private string _appConfigPath;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void InitializeForm()
    {
      try
      {
        a = new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during application initialization." + g.crlf2 + ex.ToReport(), "Code Pusher Initialization Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      ReloadCodePusherConfig();

      _isFirstShowing = true;
      _appConfigPath = String.Empty;
      lblStatus.Text = String.Empty;
      ckClearDestination.Enabled = false;

      txtSource.Text = _source;
      txtDestination.Text = _dest;
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "PushCode":
          PushCode();
          break;

        case "ReloadCodePusherConfig":
          ReloadCodePusherConfig();
          break;
      }
    }

    private void PushCode()
    {
      try
      {
        bool profileNameShown = false;

        int grandTotalSource = 0;
        int grandTotalMoved = 0;
        int grandTotalNameExcl = 0;
        int grandTotalExtExcl = 0;
        int grandTotalDeleted = 0;

        this.Cursor = Cursors.WaitCursor;
        StringBuilder sb = new StringBuilder();

        string profileName = cboProfile.Text.Trim();
        if (profileName.IndexOf("-") > -1)
          profileName = profileName.Split('-').First().Trim();

        Profile p = _profiles[profileName];

        txtResults.Clear();
        Application.DoEvents();
        int filesPushed = 0;

        string reportOnlyText = String.Empty;
        if (ckReportOnly.Checked)
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
                lblStatus.Text = "Deleting file: " + fileName;
                Application.DoEvents();
                sb.Append("DELETING FILE      " + fileName + g.crlf);
                if (!ckReportOnly.Checked)
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
                  if (!ckReportOnly.Checked)
                  {
                    filesPushed++;
                    string destFileName = c.Destination + @"\" + fileName;
                    if (!Directory.Exists(c.Destination))
                      Directory.CreateDirectory(c.Destination);
                    lblStatus.Text = "Copying file: " + file + "   (Count:" + filesPushed.ToString() + ")";
                    Application.DoEvents();
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

          sb.Append("  Source      : " + c.Source +  g.crlf +
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

        txtResults.Text = sb.ToString();

        lblStatus.Text = "Files copied: " + filesPushed.ToString();
        Application.DoEvents();

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to push code." + g.crlf2 +
                        ex.ToReport(), "Adsdi Code Pusher - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        this.Cursor = Cursors.Default;
      }
    }

    private int DeleteDirectoryRecursive(string path, StringBuilder sb, int filesDeleted)
    {
      List<string> fileNames = Directory.GetFiles(path).ToList();

      foreach (string fileName in fileNames)
      {
        lblStatus.Text = "Deleting file: " + fileName;
        Application.DoEvents();
        sb.Append("DELETING FILE      " + fileName + g.crlf);
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

    private int DeleteDirectoryContentsRecursive(string path, StringBuilder sb, int filesDeleted)
    {
      List<string> fileNames = Directory.GetFiles(path).ToList();

      foreach (string fileName in fileNames)
      {
        lblStatus.Text = "Deleting file: " + fileName;
        Application.DoEvents();
        sb.Append("DELETING FILE      " + fileName + g.crlf);
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
        lblStatus.Text = "Copying file: " + fileName + "   (Count:" + filesPushed.ToString() + ")";
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

    private void ReloadCodePusherConfig()
    {
      try
      {
        string profilesPath = g.CI("ProfilesPath");
        XElement cfg = XElement.Parse(File.ReadAllText(profilesPath));

        var f = new ObjectFactory2(g.CI("InDiagnosticsMode").ToBoolean());
        f.LogToMemory = g.CI("LogToMemory").ToBoolean();
        _profiles = f.Deserialize(cfg) as ProfileSet;

        _profiles.ResolveVariables();

        cboProfile.Items.Clear();
        cboProfile.Items.Add(String.Empty);
        foreach (Profile p in _profiles.Values)
        {
          string profileName = p.Name;
          string profileStatus = String.Empty;
          if (p.ProfileStatus == ProfileStatus.Active || p.ProfileStatus == ProfileStatus.Disabled)
          {
            profileStatus = p.ProfileStatus.ToString();
            cboProfile.Items.Add(p.Name + " - " + profileStatus);
          }
          else
            cboProfile.Items.Add(p.Name);
        }

        cboProfile.SelectedIndex = 0;
      }
      catch (Exception ex)
      {
        txtResults.Text = "An exception occurred loading code pusher profiles." + g.crlf2 + ex.ToReport() + g.crlf2 +
                          "MEMORY LOG" + g.crlf + g.MemoryLog;
        return;
      }
    }

    private void cboProfile_SelectedIndexChanged(object sender, EventArgs e)
    {
      btnPushCode.Enabled = cboProfile.SelectedIndex > 0;

      if (cboProfile.Text.IsBlank())
      {
        cboMappingControlElements.Items.Clear();
        txtSource.Clear();
        txtDestination.Clear();
        ckClearDestination.Checked = false;
        ckReportOnly.Checked = false;
        return;
      }

      string profileName = cboProfile.Text.Trim();
      if (profileName.IndexOf("-") > -1)
        profileName = profileName.Split('-').First().Trim();

      if (_profiles.ContainsKey(profileName))
      {
        Profile p = _profiles[profileName];

        if (p.ProfileStatus == ProfileStatus.Active)
          btnPushCode.Enabled = true;
        else
          btnPushCode.Enabled = false;
      }

      txtSource.Clear();
      txtDestination.Clear();
      ckClearDestination.Checked = false;
      ckClearDestination.Enabled = false;
      Load_cboMappingControlElements(profileName);

      if (cboMappingControlElements.Items.Count > 0)
        cboMappingControlElements.SelectedIndex = 0;
    }

    private void Load_cboMappingControlElements(string profileName)
    {
      cboMappingControlElements.Items.Clear();

      if (_profiles.ContainsKey(profileName))
      {
        Profile p = _profiles[profileName];
        foreach (MappingControl mc in p.MappingControlSet.Values)
        {
          cboMappingControlElements.Items.Add(mc.Name);
        }
      }
    }

    private bool CheckPathAccessibility()
    {
      _source = txtSource.Text.Trim();
      _dest = txtDestination.Text.Trim();

      if (!Directory.Exists(_source))
      {
        MessageBox.Show("The source directory '" + _source + "' is not accessible.");
        return false;
      }

      if (!Directory.Exists(_dest))
      {
        MessageBox.Show("The destination directory '" + _dest + "' is not accessible.");
        return false;
      }

      return true;
    }

    private void frmMain_Shown(object sender, EventArgs e)
    {
      if (!_isFirstShowing)
        return;

      _isFirstShowing = false;
    }

    private void cboMappingControlElements_SelectedIndexChanged(object sender, EventArgs e)
    {
      string profileName = cboProfile.Text.Trim();
      if (profileName.IndexOf("-") > -1)
        profileName = profileName.Split('-').First().Trim();

      string mappingControlName = cboMappingControlElements.Text.Trim();
      if (mappingControlName.IsBlank())
      {
        txtSource.Clear();
        txtDestination.Clear();
        ckClearDestination.Checked = false;
        ckClearDestination.Enabled = false;
        return;
      }

      if (_profiles.ContainsKey(profileName))
      {
        Profile p = _profiles[profileName];
        if (p.MappingControlSet.ContainsKey(mappingControlName))
        {
          MappingControl mc = p.MappingControlSet[mappingControlName];
          txtSource.Text = mc.Source;
          txtDestination.Text = mc.Destination;
          ckClearDestination.Checked = mc.ClearDestination;
          ckClearDestination.Enabled = true;
          return;
        }
      }
    }

    private void ckClearDestination_CheckedChanged(object sender, EventArgs e)
    {
      if (cboProfile.Text.IsBlank())
        return;

      if (cboMappingControlElements.Text.IsBlank())
        return;

      string profileName = cboProfile.Text.Trim();
      if (profileName.IndexOf("-") > -1)
        profileName = profileName.Split('-').First().Trim();

      string mappingControlName = cboMappingControlElements.Text.Trim();

      if (_profiles.ContainsKey(profileName))
      {
        Profile p = _profiles[profileName];
        if (p.MappingControlSet.ContainsKey(mappingControlName))
        {
          MappingControl mc = p.MappingControlSet[mappingControlName];
          mc.ClearDestination = ckClearDestination.Checked;
          return;
        }
      }

    }

  }
}
