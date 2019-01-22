using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using iText.IO;
using iText.Layout;
using iText.Kernel;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using iText.Kernel.Pdf.Collection;
using iText.Layout.Element;
using Org.GS;

namespace Org.Pdf
{
  public delegate void ParserEventDelegate(IEventData eventData, EventType eventType);

  public class Document : IDisposable
  {
    public long FileLength {
      get;
      private set;
    }
    public long Permissions {
      get;
      private set;
    }
    public string Author {
      get;
      set;
    }
    public string Creator {
      get;
      set;
    }
    public string Keywords {
      get;
      set;
    }
    public Dictionary<string, string> MoreInfo {
      get;
      set;
    }
    public string Producer {
      get;
      set;
    }
    public string Subject {
      get;
      set;
    }
    public string Title {
      get;
      set;
    }
    public string Report {
      get {
        return Get_Report();
      }
    }
    private PdfReader _pdfReader;
    internal PdfReader PdfReader {
      get {
        return _pdfReader;
      }
    }
    private int _nextObjectNumber;
    public int NextObjectNumber {
      get {
        return _nextObjectNumber++;
      }
    }

    public string DocumentPath {
      get;
      private set;
    }
    public int PageCount {
      get;
      private set;
    }
    public int TotalObjectCount {
      get {
        return Get_TotalObjectCount();
      }
    }
    public PageSet PageSet {
      get;
      private set;
    }
    private StringBuilder _logSb;
    public string Log {
      get {
        return _logSb.ToString();
      }
    }
    public int BreakOnObject {
      get;
      private set;
    }

    public Document(string documentPath, string breakOnObject)
    {
      try
      {
        this.BreakOnObject = breakOnObject.IsInteger() ? breakOnObject.ToInt32() : -1;

        _nextObjectNumber = 0;
        _logSb = new StringBuilder();
        this.PageSet = new PageSet();

        this.DocumentPath = documentPath;

        if (!File.Exists(this.DocumentPath))
          throw new Exception("There is no file at the provided path for the document '" + this.DocumentPath + "'.");

        using (var pdfReader = new PdfReader(this.DocumentPath))
        {
          _pdfReader = pdfReader;
          var doc = new iText.Kernel.Pdf.PdfDocument(pdfReader);
          PdfDocumentInfo docInfo = doc.GetDocumentInfo();
          this.Author = docInfo.GetAuthor().DbToString();
          this.Creator = docInfo.GetCreator().DbToString();
          this.Producer = docInfo.GetProducer().DbToString();
          this.Subject = docInfo.GetSubject().DbToString();
          this.Title = docInfo.GetTitle().DbToString();

          this.MoreInfo = new Dictionary<string, string>();
          List<string> keyWords = docInfo.GetKeywords().DbToString().ToListOfStrings(Constants.CommaDelimiter);
          foreach (var keyWord in keyWords)
          {
            var moreInfo = docInfo.GetMoreInfo(keyWord);
            if (moreInfo != null && !this.MoreInfo.ContainsKey(keyWord))
              this.MoreInfo.Add(keyWord, moreInfo);
          }

          PdfCatalog catalog = doc.GetCatalog();

          // all of these were null in first document tried (only had 1 image per page)
          PdfCollection catalogCollection = catalog.GetCollection();
          var pageMode = catalog.GetPageMode();
          var pageLayout = catalog.GetPageLayout();
          var viewerPreferences = catalog.GetViewerPreferences();
          var ocProperties = catalog.GetOCProperties(false);
          var language = catalog.GetLang();
          var pageLabelsTree = catalog.GetPageLabelsTree(false);

          this.PageCount = doc.GetNumberOfPages();
          this.FileLength = pdfReader.GetFileLength();
          var permissions = pdfReader.GetPermissions();


          var parser = new iText.Kernel.Pdf.Canvas.Parser.PdfDocumentContentParser(doc);

          for (int i = 1; i < this.PageCount; i++)
          {
            var filters = new iText.Kernel.Pdf.Canvas.Parser.Filter.IEventFilter[] { };
            var listener = new PdfParsingListener();
            listener.AttachEventListener(new ParserEventDelegate(ParserEventHandler), filters);

            parser.ProcessContent(i, listener);



            var page = new Page(i, doc.GetPage(i), this);
            this.PageSet.Add(i, page);
          }

          pdfReader.Close();
          _pdfReader = null;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception was encountered attempting to create the iTextSharp.text.Document object using path '" +
                            this.DocumentPath + "'.", ex);
      }
    }

    public void ParserEventHandler(IEventData eventData, EventType eventType)
    {

    }

    private int Get_TotalObjectCount()
    {
      int objectCount = 0;
      foreach (var page in this.PageSet.Values)
      {
        objectCount++;
        objectCount += page.TotalObjectCount;
      }
      return objectCount;
    }

    public void WriteLog(string message)
    {
      string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
      _logSb.Append(currentTime + " - " + message + g.crlf);
    }

    private string Get_Report()
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("doc-root" + g.crlf);
      foreach (var page in this.PageSet.Values)
      {
        page.GetReport(sb);
      }

      string report = sb.ToString();
      return report;
    }

    public void Dispose()
    {

    }
  }
}
