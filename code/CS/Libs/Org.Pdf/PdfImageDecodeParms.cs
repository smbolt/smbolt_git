using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Pdf
{
  public class PdfImageDecodeParms
  {
    public int Columns {
      get;
      private set;
    }
    public int Rows {
      get;
      private set;
    }
    public int K {
      get;
      private set;
    }

    public PdfImageDecodeParms(int columns, int rows, int k)
    {
      this.Columns = columns;
      this.Rows = rows;
      this.K = k;
    }
  }
}
