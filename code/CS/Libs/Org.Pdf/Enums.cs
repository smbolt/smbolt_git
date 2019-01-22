using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Pdf
{
  public enum PObjectType
  {
    NotSet = 0,

    PdfBoolean = 1,
    PdfNumber = 2,
    PdfString = 3,
    PdfName = 4,
    PdfArray = 5,
    PdfDictionary = 6,
    PdfStream = 7,
    PdfNull = 8,

    PdfLiteral = 97,
    Null = 98,
    Undefined = 99
  }

  public enum PObjectSetType
  {
    NotSet,
    Dictionary,
    Array,
  }

  public enum PdfColorSpace
  {
    NotSet,
    DeviceGray,
    DeviceRGB,
    DeviceCMYK,
    Unidentified
  }

  public enum PdfImageFilter
  {
    NotSet,
    None,
    CCITTFaxDecode,
    Unidentified
  }
}
