using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.ShareFileApiClient
{
  public class SFFile : SFBase
  {
    public long Size { get; set; }
    public override SFType SFType { get { return SFType.File; } }

    public SFFile(SFFolder parentFolder)
    {
      this.ParentFolder = parentFolder;
      this.Size = 0; 
    }
  }
}
