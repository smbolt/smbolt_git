using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.QB.QBXML
{
  public class QueryMeta
  {
    public MetaData MetaData { get; set; }
    public Iterator Iterator { get; set; }
    public string IteratorId { get; set; }
  }
}
