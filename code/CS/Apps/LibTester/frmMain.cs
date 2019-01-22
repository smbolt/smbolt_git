using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO.Compression;
using System.Data.Common;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using Org.FSO;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS;
using Org.GS.Performance;
using Org.ZIP;

namespace Org.LibTester
{
  public partial class frmMain : Form
  {
    private a a;
    private string _path;
    private int _limit;
    private int _originalLimit;
    private bool hasParentBeenInserted = false;
    public int RootFolderId;
    public int ProjectId;
    private ConfigDbSpec _dbSpec;

    private FileSystemItem _rootFolder;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "ProcessArchive":
          ProcessArchive();
          break;

        case "ProcessRootFolder":
          ProcessRootFolder();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void ProcessArchive()
    {
      //var archiver = new Archiver();
      //archiver.ProcessArchive(_path, _limit);
    }

    private void ProcessRootFolder()
    {
      _rootFolder = new FileSystemItem(_path);
      FileSystemItem.SearchParms.AllowMemoryUsageGrowth = g.CI("AllowMemoryUsageGrowth").ToBoolean();
      FileSystemItem.FileSystemItemAdded += FileSystemItem_FileSystemItemAdded;
      _rootFolder.PopulateFileSystemHierarchy();
      txtOut.Text = "File count is " + FileSystemItem.TotalFileCount.ToString() + g.crlf +
                    "Folder count is " + FileSystemItem.TotalFolderCount.ToString() + g.crlf +
                    "Total Folder and File count is " + FileSystemItem.FsiCount.ToString();
    }

    public void FileSystemItem_FileSystemItemAdded(FileSystemItem fsiRootFolder)
    {
      if (FileSystemItem.FsiCount > _limit || FileSystemItem.IsRecursionFinished)
      {
        int secondLevelFolderCount = fsiRootFolder.FolderCount;

        FsiLimitReached(fsiRootFolder);

        if (fsiRootFolder.FolderCount == secondLevelFolderCount)
        {
          if (!FileSystemItem.IsRecursionFinished && !FileSystemItem.SearchParms.AllowMemoryUsageGrowth)
            throw new Exception("Exception thrown to avoid unending loop when recursion is not finished and there is only one second level FileSystemItem.");

          _limit += _originalLimit;
        }
        else
        {
          _limit = _originalLimit;
        }
      }
    }

    private void FsiLimitReached(FileSystemItem fsi)
    {

      //_limit = _additionalLimit;

      //if (!FileSystemItem.IsRecursionFinished && fsi.FolderCount < 2)
      //  return;

      // need to figure out how to not end up in a loop when we do no work


      if (!FileSystemItem.IsRecursionFinished && fsi.FolderCount == 1 && !FileSystemItem.SearchParms.AllowMemoryUsageGrowth)
        throw new Exception("Exception thrown to avoid unending loop when recursion is not finished and there is only one second level FileSystemItem.");
      // end of problem to resolve


      if (FileSystemItem.IsRecursionFinished || fsi.FolderCount > 1)
      {
        using (var repo = new FsoRepository(_dbSpec))
        {
          RootFolderId = repo.GetRootFolderIDFromPath(_path);
          ProjectId = repo.GetProjectIDFromPath(_path);

          int parentId = 0;

          if(fsi.Parent == null && !hasParentBeenInserted)
          {
            hasParentBeenInserted = true;
            parentId = repo.LoadFileSystemRoot(fsi, ProjectId, RootFolderId, null);
          }

          foreach(var fsi2 in fsi.Folders)
          {
            if(fsi.FolderCount > 1)
              repo.LoadFileSystem(fsi2, ProjectId, RootFolderId, parentId);
          }
        }
        if(FileSystemItem.IsRecursionFinished)
          hasParentBeenInserted = false;
      }

      while (fsi.FolderCount > 0)
      {
        if (!FileSystemItem.IsRecursionFinished && fsi.FolderCount == 1)
          break;

        fsi.RemoveFirstFolder();
      }
    }

    private void InitializeForm()
    {
      try
      {
        a = new a();
        txtArchivePath.Text = g.GetCI("InputArchiveFile");
        txtFileCountLimit.Text = g.GetCI("FileCountLimit");
        _dbSpec = g.GetDbSpec("Test");
        _path = txtArchivePath.Text.ToString();
        _limit = txtFileCountLimit.Text.ToInt32();
        _originalLimit = _limit;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 + ex.ToReport(),
                        "Library Tester - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }
  }
}
