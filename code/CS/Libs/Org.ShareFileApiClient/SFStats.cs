using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.ShareFileApiClient
{
  public class SFStats
  {
    public long FolderCount { get; set; }
    public long FileCount { get; set; }
    public long TotalBytes { get; set; }

    public SFStats()
    {
      this.FolderCount = 0;
      this.FileCount = 0;
      this.TotalBytes = 0;
    }
  }
}
