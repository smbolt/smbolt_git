using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.GS;

namespace Org.WSO.Transactions
{
  [XMap(XType = XType.Element)]
  public class SendFilesRequest : TransactionBase
  {
    [XMap(XType = XType.Element, CollectionElements = "FileObject")]
    public List<FileObject> FileObjects { get; set; }

    public SendFilesRequest()
    {
      this.FileObjects = new List<FileObject>();
    }
  }
}
