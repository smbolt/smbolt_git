using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.ShareFileApiClient
{
  public class SFFolderContent
  {
    public SFFolderSet SFFolderSet {
      get;
      set;
    }
    public SFFileSet SFFileSet {
      get;
      set;
    }

    public SFFolderContent()
    {
      this.SFFolderSet = new SFFolderSet();
      this.SFFileSet = new SFFileSet();
    }
  }
}
