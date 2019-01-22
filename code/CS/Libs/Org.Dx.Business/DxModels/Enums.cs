using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public enum FilterMethod
  {
    NotSet,
    CellValues,
    CellDataTypes,
    SheetFilterSheetNames,
    SheetFilterCellValues,
    RowFilterCellValues
  }

  public enum FilterType
  {
    NotSet,
    SheetFilter,
    RowFilter
  }

  public enum DxSearchTarget
  {
    NotSet,
    DxCell,
    DxRowIndex,
    DxColumnIndex
  }

  public enum DxComparisonType
  {
    Equals,
    StartsWith,
    EndsWith,
    Contains
  }

  public enum DxRegionExtractMethod
  {
    ContextDefault = 0,
    DefaultToRegion,
    RowToRow,
    RowToSheet,
    RightToLeft,
    SheetToSheet,
    SkipThisRegion,
    NotUsed
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

  public enum DataType
  {
    Varchar,
    Int,
    String,
    Float,
    Decimal,
    Integer,
    DateTime
  }

  public enum DxDocType
  {
    NonOpProduction,
    MidstreamPopStatement,
    OperatorReport,
    Invalid
  }

  public enum DxMapType
  {
    Invalid,
    RowToRow,
    SheetToSheet,
    RowToSheet
  }

  public enum DxActionType
  {
    NotSet,
    FilterSheetsAction,
    CreateRegionsAction,
    DataMappingAction,
    RunDxRoutine
  }

  public enum MapTiming
  {
    PerUnit = 0,
    StartJob,
    EndJob,
    StartUnit,
    EndUnit
  }

  public enum SheetControl
  {
    None = 0,
    NewSheetBefore,
    NewSheetAfter
  }

  public enum AlgorithmType
  {
    NotSet,
    RightMostNonBlankCell,
    LeftMostNonBlankCell,
    TopMostNonBlankCell,
    BottomMostNonBlankCell
  }

  public enum QueryExecutionStatus
  {
    Initial = 0,
    InProgress,
    Aligned,
    InInterval,
    Matched,
    MatchFailed,
    Target,
    MatchedTargetPending,
    MatchedTarget
  }

  public enum MatchType
  {
    MatchAny,
    MatchSpecific,
    Target
  }
}
