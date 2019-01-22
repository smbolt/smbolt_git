using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using iText.Kernel;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Element;
using Org.GS;

namespace Org.Pdf
{
  public class Page : PObject
  {
    public PdfPage PdfPage { get; private set; }
    public int PageNumber { get; private set; }
    private PdfDictionary _pageDictionary { get { return (PdfDictionary) base.iPdfObject; } }
    private int _totalObjectCount;
    public int TotalObjectCount { get { return _totalObjectCount; } }

    private System.Drawing.RectangleF? _pageSizeRect;
    public System.Drawing.SizeF PageSizeF { get { return Get_PageSizeF(); } }
    public System.Drawing.PointF PageLocationF { get { return Get_PageLocationF(); } }
    public System.Drawing.RectangleF PageRectangleF { get { return Get_PageRectangleF(); } }
    private System.Drawing.RectangleF? _artBoxRect;
    public System.Drawing.SizeF ArtBoxSizeF { get { return Get_ArtBoxSizeF(); } }
    public System.Drawing.PointF ArtBoxLocationF { get { return Get_ArtBoxLocationF(); } }
    public System.Drawing.RectangleF ArtBoxRectangleF { get { return Get_ArtBoxRectangleF(); } }

    public int PageRotation { get { return this.PdfPage.GetRotation(); } }
    public string PageRawText { get; set; }

    public Document Document { get; private set; }
    private List<PObject> _pdfStreamList;
    public List<PObject> PdfStreamList { get { return _pdfStreamList; } }


    public Page(int pageNumber, PdfPage pdfPage, Document document)
      : base(document, "Page-" + pageNumber.ToString(), pdfPage.GetPdfObject(), null, true)
    {
      try
      {
        _pdfStreamList = new List<PObject>();
        this.PageNumber = pageNumber;
        this.PdfPage = pdfPage;
        this.Document = document;

        this.PageRawText = iText.Kernel.Pdf.Canvas.Parser.PdfTextExtractor.GetTextFromPage(this.PdfPage);

        PdfResources res = this.PdfPage.GetResources();
        var resNames = res.GetResourceNames();

        
        var pageKeySet = ((PdfDictionary)base.iPdfObject).KeySet();

        foreach (var pageDictKey in pageKeySet)
        {
          string key = pageDictKey.ToString();

          if (key == "/Parent")
            continue;

          PdfObject pdfObject = _pageDictionary.Get(pageDictKey);
          var pObject = new PObject(this.Document, key, pdfObject, this);
          if (this.ChildObjects.ContainsKey(key))
            throw new Exception("The ChildObjectSet of '" + this.Name + "' already contains an object with the key value '" + key + "'.");
          this.ChildObjects.Add(key, pObject);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the Page constructor.", ex);
      }
    }

    public void IncrementObjectCount()
    {
      _totalObjectCount++;
    }    

    private System.Drawing.SizeF Get_PageSizeF()
    {
      EnsurePageSize();      
      return new SizeF(_pageSizeRect.Value.Width, _pageSizeRect.Value.Height);
    }

    private System.Drawing.PointF Get_PageLocationF()
    {
      EnsurePageSize();
      return new PointF(_pageSizeRect.Value.X, _pageSizeRect.Value.Y);
    }

    private System.Drawing.RectangleF Get_PageRectangleF()
    {
      EnsurePageSize();
      return _pageSizeRect.Value;
    }

    private void EnsurePageSize()
    {
      if (!_pageSizeRect.HasValue)
      {
        var rect = this.PdfPage.GetPageSize();
        _pageSizeRect = new RectangleF(rect.GetX(), rect.GetY(), rect.GetWidth(), rect.GetHeight());
      }
    }

    private System.Drawing.SizeF Get_ArtBoxSizeF()
    {
      EnsureArtBoxRect();
      return new SizeF(_artBoxRect.Value.Width, _artBoxRect.Value.Height);
    }

    private System.Drawing.PointF Get_ArtBoxLocationF()
    {
      EnsureArtBoxRect();
      return new PointF(_artBoxRect.Value.X, _artBoxRect.Value.Y);
    }

    private System.Drawing.RectangleF Get_ArtBoxRectangleF()
    {
      EnsureArtBoxRect();
      return _artBoxRect.Value;
    }

    private void EnsureArtBoxRect()
    {
      if (!_artBoxRect.HasValue)
      {
        var rect = this.PdfPage.GetArtBox();
        _artBoxRect = new RectangleF(rect.GetX(), rect.GetY(), rect.GetWidth(), rect.GetHeight());
      }
    }

    public void AddToPdfStreamList(PObject s)
    {
      _pdfStreamList.Add(s);
    }
  }
}
