using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Org.GS.TextProcessing;
using Org.GS;

namespace Org.Pdfx
{
  public class TextExtractor : IDisposable
  {
    public Text ExtractTextFromPdf(string fullPath)
    {
      try
      {
        if (!File.Exists(fullPath))
          throw new Exception("File '" + fullPath + "' does not exist.");

        var text = new Text();
        StringBuilder sb = new StringBuilder();

        using (PdfReader reader = new PdfReader(fullPath))
        {
          var infoItems = reader.Info;
          foreach (var infoItem in infoItems)
          {
            if (!text.MetaData.ContainsKey(infoItem.Key))
              text.MetaData.Add(infoItem.Key, infoItem.Value); 
          }

          for (int i = 1; i <= reader.NumberOfPages; i++)
          {
            string pageText = PdfTextExtractor.GetTextFromPage(reader, i);
            sb.Append(pageText + "\n");
          }
        }

        text.RawText = sb.ToString().RemoveExtraSpacesAndLines();

        int twoNewLinePos = text.RawText.IndexOf("\n\n");
        if (twoNewLinePos > -1)
          throw new Exception("The raw text value contains back to back new lines at position " + twoNewLinePos.ToString() + ".");

        int twoBlanksPos = text.RawText.IndexOf("  ");
        if (twoBlanksPos > -1)
          throw new Exception("The raw text value contains back to back blanks at position " + twoBlanksPos.ToString() + ".");

        return text;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to extract text from the file '" + fullPath + "'.", ex); 
      }
    }

    public string ExtractTextFromPdf(string fullPath, bool flagOnlyOnError, string pageBreakIndicator)
    {
      try
      {
        if (!File.Exists(fullPath))
          throw new Exception("File '" + fullPath + "' does not exist.");

        StringBuilder sb = new StringBuilder();

        using (PdfReader reader = new PdfReader(fullPath))
        {
          for (int i = 1; i <= reader.NumberOfPages; i++)
          {
            string pageText = PdfTextExtractor.GetTextFromPage(reader, i);
            if (pageBreakIndicator.IsNotBlank())
              sb.Append(pageText + g.crlf + pageBreakIndicator + g.crlf);
            else
              sb.Append(pageText + g.crlf);
          }
        }

        string extractedText = sb.ToString();
        return extractedText;
      }
      catch (Exception ex)
      {
        if (flagOnlyOnError)
          return "ERROR EXTRACTING TEXT: " + ex.Message;

        throw new Exception("An exception occurred attempting to extract text from the file '" + fullPath + "'.", ex);
      }
    }

    public void Dispose()
    {
    }
  }
}
