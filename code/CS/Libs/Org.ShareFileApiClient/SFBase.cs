using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.ShareFileApiClient
{
  public enum SFType
  {
    Folder,
    File,
    Base
  }


  public class SFBase : IDisposable
  {
    public static SFFolder RootFolder { get; set; }
    public SFFolder ParentFolder { get; set; }
    public string Name { get; set; }
    public string Id { get; set; }
    public int Depth { get { return Get_Depth(); } }
    public string FullPath { get { return Get_FullPath(); } }
    public virtual SFType SFType { get { return SFType.Base; } }

    public SFBase()
    {
      this.Name = String.Empty;
      this.Id = String.Empty;
    }

    private int Get_Depth()
    {
      int depth = 0;
      var parent = this.ParentFolder;

      while (parent != null)
      {
        parent = parent.ParentFolder;
        depth++; 
      }

      return depth;
    }

    private string Get_FullPath()
    {
      string fullPath = this.Name;

      var parent = this.ParentFolder;
      while (parent != null)
      {
        fullPath = parent.Name + @"\" + fullPath;
        parent = parent.ParentFolder;
      }

      return fullPath;
    }

    public void Dispose()
    {
    }
  }
}
