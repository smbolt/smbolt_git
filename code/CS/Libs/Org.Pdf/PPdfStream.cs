using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using iText.Kernel;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Element;
using Org.GS;

namespace Org.Pdf
{
  public class PPdfStream : PObject
  {
    public int CompressionLevel {
      get;
      private set;
    }
    public int Length {
      get;
      private set;
    }
    private PdfStream _pdfStream;

    public PPdfStream(Document document, string name, PdfObject pdfObject, PObject parent = null, bool isPage = false)
      : base(document, name, pdfObject, parent, isPage)
    {
      try
      {
        this.CompressionLevel = 0;
        this.Length = 0;
        this.Bytes = new byte[0];

        _pdfStream = base.iPdfObject as PdfStream;

        if (_pdfStream == null)
          throw new Exception("The pdfObject parameter passed to the PPdfStream constructor cannot be cast to a PdfStream.");

        this.CompressionLevel = _pdfStream.GetCompressionLevel();
        this.Length = _pdfStream.GetLength();
        using (var ms = this.Document.PdfReader.ReadStream(_pdfStream, true))
        {
          this.Bytes = new byte[this.Length];
          ms.Read(this.Bytes, 0, this.Length);
          ms.Close();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the PPdfStream constructor.", ex);
      }
    }
  }
}
