using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public enum DataTypeSpec
  {
    NotSet,
    Literal,
    String,
    Integer,
    Decimal,
    DecimalWithRequiredDecimalPoint,
    Date
  }

  public enum DetailSpecSwitch
  {
    NotSet,
    MatchesPattern,
    MatchesRegex,
    CheckLength,
    CheckLengthRange,
    CheckDecimalPlaces,
    CheckDecimalPlacesRange,
    DayOfMonth,
    ValueGreaterThan,
    ValueGreaterThanOrEqualTo,
    ValueLessThan,
    ValueLessThanOrEqualTo,
    ValueEquals,
    ValueInList,
    ValueBetween,
    Contains,
    StartsWith,
    EndsWith,
    SuppressCondenseAndTrim,
    UseOrLogic,
    CaseSensitive,
    NotBlank,
    Blank,
    Split,
    Index,
    Value,
    Optional,
    Before,
    After,
    Concatenate,
    Compute
  }
}
