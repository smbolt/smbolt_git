using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business.TextProcessing
{
  public enum Verb
  {
    SetXml,
    SetVariableFromXmlElementValue,
    ExtractXmlElementValue,
    SetTextStart,
    SetTextEnd,
    SetTextPosition,
    SetTsdCondition,
    SetVariable,
    SetGlobalVariable,
    SetRowIndex,
    LocateToken,
    ExtractToken,
    ExtractLiteralToken,
    ExtractNextTokenOfType,
    ExtractPriorTokenOfType,
    ExtractPriorTokensOfType,
    ExtractNextToken,
    ExtractNextTokens,
    ExtractNextLine,
    ExtractTextBefore,
    TokenizeNextLine,
    RemoveStoredTokens,
    ExtractStoredToken,
    ExtractStoredTokens,
    ExtractStoredTokenBefore,
    ExtractFromPeerCell,
    ProcessingCommand,
    ReplaceText,
    Truncate,
    InvalidVerb
  }

  public enum Direction
  {
    Next,
    Prev,
    Stored,
    Literal,
    Variable
  }

  public enum DataType
  {
    String,
    Integer,
    Percentage,
    PercentagePeriodOptional,
    Decimal,
    DecimalPeriodOptional,
    Date,
    Time,
    DateTime,
    MMYYYY
  }

  public enum ExtractionUnit
  {
    Page,
    NotSet
  }

  public enum TextUnit
  {
    Character,
    Token,
    Line,
  }

  public enum PositionAt
  {
    Start,
    End,
    Before,
    After
  }

  public enum TruncateDirection
  {
    Before,
    After
  }

  public enum FormatSpecType
  {
    Inclusion,
    Exclusion
  }

  public enum MatchType
  {
    Equals,
    StartsWith,
    EndsWith
  }

  public enum OffsetDir
  {
    None,
    Back,
    Forward
  }

  public enum OffsetType
  {
    Match,
    Scalar,
    MatchAndScalar
  }

  public enum SearchDirection
  {
    Forward,
    Backward
  }

  public enum VariableType
  {
    Local,
    Global
  }

}
