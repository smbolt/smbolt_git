using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using DevExpress.Pdf;


namespace Org.DxPdf
{
  public class PdfEngine : IDisposable
  {
    public string ExtractTextFromPDF(string filePath)
    {
      string documentText = "";
      
      try
      {
        using (FileStream documentStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
          using (PdfDocumentProcessor documentProcessor = new PdfDocumentProcessor())
          {
            documentProcessor.LoadDocument(documentStream);
            documentText = documentProcessor.Text;
          }
        }
        return documentText;
      }
      catch(Exception ex) 
      {
        throw new Exception("An exception occurred while attempting to extract the text from PDF document '" + filePath + "'", ex); 
      }
    }

    public void Dispose()
    {

    }


  }
}
