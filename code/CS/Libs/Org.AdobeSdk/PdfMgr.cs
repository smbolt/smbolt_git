using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Acrobat;


namespace Org.AdobeSdk
{
  public class PdfMgr : IDisposable
  {
    private string _fileName;

    public PdfMgr(string fileName)
    {
      _fileName = fileName;
    }

    public string GetDocumentText()
    {
      try
      {
        StringBuilder sb = new StringBuilder();
        var _avDoc = new AcroAVDoc();
        _avDoc.Open(_fileName, String.Empty);

        var pdDoc = (CAcroPDDoc)_avDoc.GetPDDoc();
        int pageCount = pdDoc.GetNumPages();

        for (int i = 0; i < pageCount; i++)
        {
          var pdPage = (CAcroPDPage)pdDoc.AcquirePage(0);
          var pageSize = (CAcroPoint)pdPage.GetSize();
          var rect = (CAcroRect)Microsoft.VisualBasic.Interaction.CreateObject("AcroExch.Rect", "");
          rect.Left = 0;
          rect.right = pageSize.x;
          rect.Top = pageSize.y;
          rect.bottom = 0;
          var pdTextSelect = (CAcroPDTextSelect)pdDoc.CreateTextSelect(i, rect);
          int nbrText = pdTextSelect.GetNumText();

          for (int j = 0; j < nbrText; j++)
          {
            string token = pdTextSelect.GetText(j);
            sb.Append(token);
          }

        }

        _avDoc.Close(0);
        _avDoc = null;

        return sb.ToString();
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting to extract text from pdf file '" + _fileName + "'.", ex);
      }
    }

    public void Dispose()
    {

    }
  }
}

