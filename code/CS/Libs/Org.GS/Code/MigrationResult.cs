using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.Code
{
  public class MigrationResult
  {
    public int SourceFolders {
      get;
      set;
    }
    public int SourceFiles {
      get;
      set;
    }
    public int DestFoldersToBeDeleted {
      get;
      set;
    }
    public int DestFoldersDeleted {
      get;
      set;
    }
    public int DestFilesToBeDeleted {
      get;
      set;
    }
    public int DestFilesDeleted {
      get;
      set;
    }
    public int DestFoldersToBeCreated {
      get;
      set;
    }
    public int DestFoldersCreated {
      get;
      set;
    }
    public int FilesToBeCopied {
      get;
      set;
    }
    public int FilesCopied {
      get;
      set;
    }
    public int FilesToBeReplaced {
      get;
      set;
    }
    public int FilesReplaced {
      get;
      set;
    }
    public int FilesToBeExcluded {
      get;
      set;
    }
    public int FilesExcluded {
      get;
      set;
    }

    public MigrationResult()
    {
      this.SourceFolders = 0;
      this.SourceFiles = 0;
      this.DestFoldersToBeDeleted = 0;
      this.DestFoldersDeleted = 0;
      this.DestFilesToBeDeleted = 0;
      this.DestFilesDeleted = 0;
      this.DestFoldersToBeDeleted = 0;
      this.DestFoldersCreated = 0;
      this.FilesToBeCopied = 0;
      this.FilesCopied = 0;
      this.FilesToBeReplaced = 0;
      this.FilesReplaced = 0;
      this.FilesToBeExcluded = 0;
      this.FilesExcluded = 0;
    }
  }
}
