using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class FileFormat
  {
    public string Name {
      get;
      set;
    }
    public List<OSFile> Files {
      get;
      set;
    }

    public FileFormat()
    {
      this.Name = String.Empty;
      this.Files = new List<OSFile>();
    }
  }
}
