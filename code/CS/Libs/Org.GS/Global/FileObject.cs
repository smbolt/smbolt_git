using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace Org.GS
{
  [XMap(XType = XType.Element)]
  public class FileObject
  {
    [XMap]
    public string FileName { get; set; }

    [XMap(XType= XType.Element)]
    public string Data { get; set; }

    public FileObject()
    {
      this.FileName = String.Empty;
      this.Data = String.Empty;
    }
  }
}
