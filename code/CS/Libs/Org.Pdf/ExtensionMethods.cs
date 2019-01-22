using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.IO;
using iText.Layout;
using iText.Kernel;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Collection;
using iText.Layout.Element;
using Org.GS;

namespace Org.Pdf
{
  public static class ExtensionMethods
  {
    public static PObjectType GetPObjectType(this PdfObject o)
    {
      if (o == null)
        return PObjectType.Null;

      if (o.IsString())
        return PObjectType.PdfString;

      if (o.IsName())
        return PObjectType.PdfName;

      if (o.IsNumber())
        return PObjectType.PdfNumber;

      if (o.IsArray())
        return PObjectType.PdfArray;

      if (o.IsDictionary())
        return PObjectType.PdfDictionary;

      if (o.IsStream())
        return PObjectType.PdfStream;

      if (o.IsBoolean())
        return PObjectType.PdfBoolean;

      if (o.IsLiteral())
        return PObjectType.PdfLiteral;

      if (o.IsNull())
        return PObjectType.PdfNull;

      return PObjectType.Undefined;
    }
  }
}
