using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Org.Dx.Business;
using Org.TOCR;
using Org.GS;

namespace Org.Pdfx
{
  public class TextExtractor : IDisposable
  {
    public Text ExtractTextFromPdf(bool allowDebugBreak, string fullPath, ExtractionMap extractionMap = null)
    {
      try
      {
        if (!File.Exists(fullPath))
          throw new Exception("File '" + fullPath + "' does not exist.");

        if (extractionMap != null)
        {
          if (extractionMap.ExtractionUnit != ExtractionUnit.Page)
            throw new Exception("The ExtractionUnit specified for the ExtractionMap '" + extractionMap.ExtractionUnit.ToString() + "' is not implemented.");

          if (extractionMap.ExtractSectionSet == null || extractionMap.ExtractSectionSet.Count < 1)
            throw new Exception("The ExtractSectionSet collection of the ExtractionMap is null or empty.");
        }

        var text = new Text(allowDebugBreak, FileType.PDF, null, fullPath);

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
            if (extractionMap == null)
            {
              string pageText = PdfTextExtractor.GetTextFromPage(reader, i);
              sb.Append(pageText + "\n");
            }
            else
            {
              if (extractionMap.Prefix.IsNotBlank())
              {
                sb.Append("\n" + extractionMap.Prefix.Replace("$PageNumber", i.ToString()) + "\n");
              }

              foreach (var extractSection in extractionMap.ExtractSectionSet.Values)
              {
                if (extractSection.Rectangle == null)
                  throw new Exception("The ExtractSection Rectangle property is null for the ExtractSection named '" + extractSection.SectionName + "'.");

                string[] rect = extractSection.Rectangle.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);
                if (rect.Length != 4)
                  throw new Exception("The ExtractSection Rectangle property is invalid - must contain 4 integers. Value found is '" + rect + "'.");

                if (rect[0].IsNotInteger() || rect[1].IsNotInteger() || rect[2].IsNotInteger() || rect[3].IsNotInteger())
                  throw new Exception("The ExtractSection Rectangle property is invalid - must contain 4 integers. Value found is '" + rect + "'.");

                iTextSharp.text.Rectangle r = new iTextSharp.text.Rectangle(rect[0].ToInt32(), rect[1].ToInt32(), rect[2].ToInt32(), rect[3].ToInt32());

                var filter = new RegionTextRenderFilter(r);
                var strategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), filter);
                string extractedText = PdfTextExtractor.GetTextFromPage(reader, i, strategy);

                if (extractSection.Prefix.IsNotBlank())
                  sb.Append("\n" + extractSection.Prefix.Replace("$SectionName", extractSection.SectionName) + "\n");
                else
                  sb.Append("\n" + "SEC_BEG:" + extractSection.SectionName + "\n");

                sb.Append("\n" + extractedText + "\n");

                if (extractSection.Suffix.IsNotBlank())
                  sb.Append("\n" + extractSection.Suffix.Replace("$SectionName", extractSection.SectionName) + "\n");
                else
                  sb.Append("\n" + "SEC_END" + "\n");
              }

              if (extractionMap.Suffix.IsNotBlank())
              {
                sb.Append("\n" + extractionMap.Suffix.Replace("$PageNumber", i.ToString()) + "\n");
              }
            }

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
        throw new Exception("An exception occurred attempting to extract text from the PDF file '" + fullPath + "'.", ex);
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

    public Text ExtractTextFromXml(bool allowDebugBreak, string fullPath)
    {
      try
      {
        if (!File.Exists(fullPath))
          throw new Exception("File '" + fullPath + "' does not exist.");

        var text = new Text(allowDebugBreak, FileType.XML, null, fullPath);

        string xml = File.ReadAllText(fullPath);

        if (!xml.IsValidXml())
          throw new Exception("The file '" + fullPath + "' does not contain a valid XML XElement.");

        text.XElement = XElement.Parse(File.ReadAllText(fullPath));

        return text;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to extract text from the XML file '" + fullPath + "'.", ex);
      }
    }

    public string ExtractTextFromImages(bool allowDebugBreak, string fullPath, string pageBreakIndicator)
    {
      try
      {
        if (!File.Exists(fullPath))
          throw new Exception("File '" + fullPath + "' does not exist.");

        StringBuilder sb = new StringBuilder();

        using (var ocr = new OCR())
        {
          using (PdfReader reader = new PdfReader(fullPath))
          {
            string pageText = String.Empty;
            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
              var images = PdfImageExtractor.ExtractImages(reader, i);
              foreach (var image in images.Values)
              {
                //string ocrText = ocr.GetText(path, imgFmt);
              }
            }
          }
        }

        string extractedText = sb.ToString();
        return extractedText;

      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to extract text from images in the file '" + fullPath + "'.", ex);
      }
    }

    public object ExtractImagesFromFile(string fullPath)
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
            var pageContent = reader.GetPdfObject(0);

            string pageText = PdfTextExtractor.GetTextFromPage(reader, i);

          }
        }

        string extractedText = sb.ToString();
        return extractedText;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to extract images from the file '" + fullPath + "'.", ex);
      }
    }


    public void ExtractImagesFromPDF(string sourcePdf, string outputPath)
    {
      // NOTE:  This will only get the first image it finds per page.
      PdfReader pdf = new PdfReader(sourcePdf);
      RandomAccessFileOrArray raf = new iTextSharp.text.pdf.RandomAccessFileOrArray(sourcePdf);

      try
      {
        for (int pageNumber = 1; pageNumber <= pdf.NumberOfPages; pageNumber++)
        {
          PdfDictionary pg = pdf.GetPageN(pageNumber);

          // recursively search pages, forms and groups for images.
          PdfObject obj = FindImageInPDFDictionary(pg);
          if (obj != null)
          {

            int XrefIndex = Convert.ToInt32(((PRIndirectReference)obj).Number.ToString(System.Globalization.CultureInfo.InvariantCulture));
            PdfObject pdfObj = pdf.GetPdfObject(XrefIndex);
            PdfStream pdfStrem = (PdfStream)pdfObj;
            byte[] bytes = PdfReader.GetStreamBytesRaw((PRStream)pdfStrem);


            ImageConverter ic = new ImageConverter();

            Image img4 = (Image)ic.ConvertFrom(bytes);

            Bitmap bitmap1 = new Bitmap(img4);


            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);

            ms.Position = 0;
            Image img3 = Image.FromStream(ms, true);





            System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
            Image img2 = (Image)converter.ConvertFrom(bytes);





            if ((bytes != null))
            {
              using (System.IO.MemoryStream memStream = new System.IO.MemoryStream(bytes))
              {
                memStream.Position = 0;
                var o = System.Drawing.Image.FromStream(memStream);

                System.Drawing.Image img = System.Drawing.Image.FromStream(memStream);
                // must save the file while stream is open.
                if (!Directory.Exists(outputPath))
                  Directory.CreateDirectory(outputPath);

                string path = System.IO.Path.Combine(outputPath, String.Format(@"{0}.jpg", pageNumber));
                System.Drawing.Imaging.EncoderParameters parms = new System.Drawing.Imaging.EncoderParameters(1);
                parms.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Compression, 0);
                //System.Drawing.Imaging.ImageCodecInfo jpegEncoder = Utilities.GetImageEncoder("JPEG");
                //img.Save(path, jpegEncoder, parms);
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to read images from the pdf file.", ex);
      }
      finally
      {
        pdf.Close();
        raf.Close();
      }

    }

    private PdfObject FindImageInPDFDictionary(PdfDictionary pg)
    {
      PdfDictionary res =
        (PdfDictionary)PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES));


      PdfDictionary xobj =
        (PdfDictionary)PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT));
      if (xobj != null)
      {
        foreach (PdfName name in xobj.Keys)
        {

          PdfObject obj = xobj.Get(name);
          if (obj.IsIndirect())
          {
            PdfDictionary tg = (PdfDictionary)PdfReader.GetPdfObject(obj);

            PdfName type =
              (PdfName)PdfReader.GetPdfObject(tg.Get(PdfName.SUBTYPE));

            //image at the root of the pdf
            if (PdfName.IMAGE.Equals(type))
            {
              return obj;
            }// image inside a form
            else if (PdfName.FORM.Equals(type))
            {
              return FindImageInPDFDictionary(tg);
            } //image inside a group
            else if (PdfName.GROUP.Equals(type))
            {
              return FindImageInPDFDictionary(tg);
            }

          }
        }
      }

      return null;

    }




    public void Dispose()
    {
    }
  }
}
