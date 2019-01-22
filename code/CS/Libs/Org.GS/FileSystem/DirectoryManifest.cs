using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  [XMap (XType = XType.Element, CollectionElements = "FileManifest")]
  public class DirectoryManifest : List<FileManifest>
  {
    public FileManifest SelectedFileManifest { get; set; }
    public bool ReadyForProcessing { get; set; }

    public DirectoryManifest()
    {
      this.SelectedFileManifest = null;
      this.ReadyForProcessing = false;
    }
  }
}
