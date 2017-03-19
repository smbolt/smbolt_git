using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public enum DxComparisonType
  {
    Equals,
    StartsWith,
    EndsWith,
    Contains
  }

  public enum DxTextCase
  {
    CaseSensitive,
    NotCaseSensitive,
  }

  public enum StringSetType
  {
    MonthsShort,
    MonthsLong,
    None
  }

  public enum DxDataType
  {
    Text,
    DateTime,
    Boolean,
    Empty,
    Numeric,
    Integer,
    Decimal,
    Any
  }
}
