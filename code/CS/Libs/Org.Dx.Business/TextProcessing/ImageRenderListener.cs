using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  public class ImageRenderListener : IRenderListener
  {
    private Dictionary<string, System.Drawing.Image> _images = new Dictionary<string, System.Drawing.Image>();
    public Dictionary<string, System.Drawing.Image> Images {
      get {
        return _images;
      }
    }

    public void RenderImage(ImageRenderInfo renderInfo)
    {
      try
      {
        PdfImageObject image = renderInfo.GetImage();
        PdfName filter = (PdfName)image.Get(PdfName.FILTER);

        if (filter != null)
        {
          System.Drawing.Image drawingImage = image.GetDrawingImage();

          string extension = ".";

          if (filter == PdfName.DCTDECODE)
          {
            extension += PdfImageObject.ImageBytesType.JPG.FileExtension;
          }
          else if (filter == PdfName.JPXDECODE)
          {
            extension += PdfImageObject.ImageBytesType.JP2.FileExtension;
          }
          else if (filter == PdfName.FLATEDECODE)
          {
            extension += PdfImageObject.ImageBytesType.PNG.FileExtension;
          }
          else if (filter == PdfName.LZWDECODE)
          {
            extension += PdfImageObject.ImageBytesType.CCITT.FileExtension;
          }

          this.Images.Add("IMAGE" + this.Images.Count.ToString() + extension, drawingImage);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to render an image from the PDF file.", ex);
      }
    }

    public void BeginTextBlock() { }
    public void EndTextBlock() { }
    public void RenderText(TextRenderInfo renderInfo) { }
  }
}
