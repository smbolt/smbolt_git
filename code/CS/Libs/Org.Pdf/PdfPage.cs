using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.GS;

namespace Org.Pdf
{
  public class PdfPage
  {
    public int PageNumber  {
      get;
      private set;
    }

    public PdfPage(int pageNumber)
    {
      this.PageNumber = pageNumber;
    }
  }
}
