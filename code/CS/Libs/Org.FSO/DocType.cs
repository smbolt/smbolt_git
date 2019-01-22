using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.FSO 
{
  public class DocType 
  {
    public int DocTypeID { get; set; }
    public int ProjectID { get; set; }
    public string ProjectName { get; set; }
    public string DocName { get; set; }
  }
}
