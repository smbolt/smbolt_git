using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Pdf
{
  public class PdfImage
  {
    private byte[] _bytes;
    public string Name {
      get;
      private set;
    }
    public int Height {
      get;
      private set;
    }
    public int Width {
      get;
      private set;
    }
    public Size Size {
      get {
        return Get_Size();
      }
    }
    public int Length {
      get;
      private set;
    }
    public int BitsPerComponent {
      get;
      private set;
    }
    public PdfColorSpace PdfColorSpace {
      get;
      private set;
    }
    public PdfImageFilter PdfImageFilter {
      get;
      private set;
    }
    public PdfImageDecodeParms PdfImageDecodeParms {
      get;
      private set;
    }
    private Image _image;
    public Image Image {
      get {
        return Get_Image();
      }
    }

    public PdfImage(byte[] bytes, string name, int height, int width, int length, int bitsPerComponent, PdfColorSpace pdfColorSpace,
                    PdfImageFilter pdfImageFilter, PdfImageDecodeParms pdfImageDecodeParms, bool generateImage = true)
    {
      try
      {
        _bytes = bytes;
        this.Name = name;
        this.Height = height;
        this.Width = width;
        this.Length = length;
        this.BitsPerComponent = bitsPerComponent;
        this.PdfColorSpace = pdfColorSpace;
        this.PdfImageFilter = pdfImageFilter;
        this.PdfImageDecodeParms = pdfImageDecodeParms;

        if (generateImage)
        {
          _image = CreateImage();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the PdfImage constructor.", ex);
      }
    }

    private Image CreateImage()
    {
      try
      {


        return new Bitmap(1, 1) as Image;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the PdfImage constructor.", ex);
      }
    }

    private Size Get_Size()
    {
      if (_image != null)
        return _image.Size;

      return new Size(this.Width, this.Height);
    }

    private Image Get_Image()
    {
      if (_image != null)
        return _image;

      _image = CreateImage();

      return _image;
    }


  }
}
