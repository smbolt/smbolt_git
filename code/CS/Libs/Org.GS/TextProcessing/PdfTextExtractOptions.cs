using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.TextProcessing
{
  public class PdfTextExtractOptions
  {
    public bool ThrowExceptions { get; set; }

    public PdfTextExtractOptions()
    {
      this.ThrowExceptions = true;
    }
  }
}
