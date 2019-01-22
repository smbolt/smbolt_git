using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  public static class PdfImageExtractor
  {
    public static bool PageContainsImages(string filename, int pageNumber)
    {
      try
      {
        using (var reader = new PdfReader(filename))
        {
          var parser = new PdfReaderContentParser(reader);
          ImageRenderListener listener = null;
          parser.ProcessContent(pageNumber, (listener = new ImageRenderListener()));
          return listener.Images.Count > 0;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to determine if there is an image on page " + pageNumber.ToString() +
                            " of PDF file '" + filename + "'.", ex);
      }
    }

    public static Dictionary<string, System.Drawing.Image> ExtractImages(PdfReader reader)
    {
      int pageNumber = 0;

      try
      {
        var parser = new PdfReaderContentParser(reader);
        ImageRenderListener listener = null;

        for (var i = 1; i <= reader.NumberOfPages; i++)
        {
          pageNumber = i;
          parser.ProcessContent(i, (listener = new ImageRenderListener()));
        }

        return listener.Images;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to retrieve images from page " + pageNumber.ToString() +
                            " of  the PDF file '.", ex);
      }
    }

    public static Dictionary<string, System.Drawing.Image> ExtractImages(PdfReader reader, int pageNumber)
    {
      try
      {
        Dictionary<string, System.Drawing.Image> images = new Dictionary<string, System.Drawing.Image>();
        PdfReaderContentParser parser = new PdfReaderContentParser(reader);
        ImageRenderListener listener = null;

        parser.ProcessContent(pageNumber, (listener = new ImageRenderListener()));

        return listener.Images;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to extract images from page " + pageNumber.ToString() +
                            " of the PDF file.", ex);
      }
    }
  }
}
