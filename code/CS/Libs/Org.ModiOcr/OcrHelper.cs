using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Org.GS;
using MODI;

namespace Org.ModiOcr
{
  public class OcrHelper : IDisposable
  {
		MODI.Document md;
		MODI.Image img;

		public OcrHelper()
		{
		}

    public string GetTextFromImage(string path)
    {
      try
      {
        md = new MODI.Document();
        md.Create(path);
        md.OCR(MODI.MiLANGUAGES.miLANG_ENGLISH, false, false);
        img = (MODI.Image)md.Images[0];

        return img.Layout.Text;
      }
      catch (Exception ex)
      {
        return ex.ToReport();
      }
    }

		public void Dispose()
		{
			md = null;
			img = null;

			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

  }
}
