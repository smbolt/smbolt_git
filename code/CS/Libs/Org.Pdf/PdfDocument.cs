using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Org.GS;

namespace Org.Pdf
{
  public class PdfDocument : IDisposable
  {
    public Document Document {
      get;
      private set;
    }
    public string DocumentPath {
      get;
      private set;
    }
    public int PageCount {
      get;
      private set;
    }
    public PdfPageSet PdfPageSet {
      get;
      private set;
    }

    public PdfDocument(string documentPath)
    {
      try
      {
        Initialize();

        this.DocumentPath = documentPath;
        if (!File.Exists(this.DocumentPath))
          throw new Exception("There is no file at the provided path for the document '" + this.DocumentPath + "'.");

        using (var pdfReader = new PdfReader(this.DocumentPath))
        {
          this.PageCount = pdfReader.NumberOfPages;

          //var catalog = pdfReader.Catalog as PdfDictionary;

          //foreach (var catalogKey in catalog.Keys)
          //{
          //  var catObj = catalog.Get(catalogKey);
          //  string catObjType = catObj.GetType().Name;
          //  switch (catObjType)
          //  {
          //    case "PRIndirectReference":
          //      var prIndirectReference = catObj as PRIndirectReference;
          //      var directObject = catalog.GetDirectObject(catalogKey) as PdfDictionary;

          //      break;

          //    case "PdfName":
          //      var pdfName = catObj as PdfName;
          //      break;

          //    case "PdfDictionary":
          //      var pdfDictionary = catObj as PdfDictionary;
          //      break;

          //    default:
          //      break;
          //  }
          //}


          for (int i = 1; i < this.PageCount; i++)
          {
            Rectangle r = new Rectangle(197,293,771,409);

            var filter = new RegionTextRenderFilter(r);
            var strategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), filter);
            string extractedText = PdfTextExtractor.GetTextFromPage(pdfReader, i, strategy);

            var pdfPage = new PdfPage(i);

            var pageN = pdfReader.GetPageN(i) as PdfDictionary;
            foreach (var key in pageN.Keys)
            {
              var obj = pageN.Get(key) as PdfObject;
              string typeName = obj.GetType().Name;
              switch (typeName)
              {
                case "PdfArray":
                  var pdfArray = obj as PdfArray;

                  break;

                case "PRIndirectReference":
                  var prIndirectReference = obj as PRIndirectReference;
                  var directObject = pageN.GetDirectObject(key);
                  break;

                default:

                  break;

              }

            }



            this.PdfPageSet.Add(pdfPage.PageNumber, pdfPage);
          }

          pdfReader.Close();
        }



      }
      catch (Exception ex)
      {
        throw new Exception("An exception was encountered attempting to create the iTextSharp.text.Document object using path '" +
                            this.DocumentPath + "'.", ex);
      }
    }

    private void Initialize()
    {
      this.PdfPageSet = new PdfPageSet();
    }

    public void Dispose()
    {

    }
  }
}
