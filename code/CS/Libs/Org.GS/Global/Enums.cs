using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Org.GS
{
  public enum FileProcessType
  {
    NotSet,
    ErrorFile,
    ProcessedFile,
    MappedFile,
    RegressionMappedFile,
    CompareReportFile
  }

  public enum FileExtractMode
  {
    NotSet,
    ReturnExtractedDxWorkbook,
    WriteExtractedDxWorkbookToFile
  }

  public enum ShowWindowConstants
  {
    SW_HIDE = 0,
    SW_SHOWNORMAL = 1,
    SW_NORMAL = 1,
    SW_SHOWMINIMIZED = 2,
    SW_SHOWMAXIMIZED = 3,
    SW_MAXIMIZE = 3,
    SW_SHOWNOACTIVATE = 4,
    SW_SHOW = 5,
    SW_MINIMIZE = 6,
    SW_SHOWMINNOACTIVE = 7,
    SW_SHOWNA = 8,
    SW_RESTORE = 9,
    SW_SHOWDEFAULT = 10,
    SW_FORCEMINIMIZE = 11,
    SW_MAX = 11
  }

  public enum ComponentLoadMode
  {
    NotSet,
    LocalCatalog,
    CentralCatalog
  }

  public enum RuntimeEnvironment
  {
    Unknown,
    Dev,
    Test,
    Prod
  }

  public enum Direction
  {
    Forward,
    Backward,
    Vertical,
    Horizontal
  }

  public enum RectangleSide
  {
    Top,
    Bottom,
    Right,
    Left
  }

  public enum IndexType
  {
    NotUsed,
    RowIndex,
    ColumnIndex
  }

  public enum SecurityModel
  {
    None = 0,
    Config,
    ActiveDirectory,
    WebApiAuthentication
  }

  public enum LogicOp
  {
    NONE,
    OR,
    AND
  }


  public enum RelOp
  {
    NotSet,
    Contains,
    Equals,
    GreaterThan,
    GreaterThanOrEqualTo,
    LessThan,
    LessThanOrEqualTo
  }

  public enum LicenseScheme
  {
    None,
    Simple1
  }

  public enum LicenseStatus
  {
    None,
    Current,
    CurrentOverridden,
    Perpetual,
    Expired,
    ExpiringSoon
  }

  public enum ConfigMgmtPerspective
  {
    Local,
    Central
  }

  public enum DiagnosticsLevel
  {
    NotSet,
    MinorProcess,
    MajorProcess,
    RunUnit
  }

  public enum DiagnosticsMode
  {
    None = 0,
    Debug = 1,
    Verbose = 2
  }

  public enum ConfigFileType
  {
    NotSet,
    AppConfig
  }

  public enum ControlType
  {
    ButtonSpec
  }

  public enum ControlEventType
  {
    Click
  }

  public enum ApplicationType
  {
    WinConsole,
    WinApp,
    WpfApp,
    WebSite,
    WebApi,
    WinService,
    WcfService,
    ControlLibrary,
    WinAppModule,
    WpfAppModule,
    MEFModule,
    PlugInModule,
    OrgGS,
    NotSet
  }

  public enum ServiceHostingLocation
  {
    WinService,
    IIS,
    NotSet
  }

  public enum TestParmValues
  {
    NotSet,
    Value1,
    Value2,
    Value3
  }

  public enum FileSystemAccess
  {
    NotSet,
    None,
    ReadOnly,
    ReadWrite,
    CreateDirectory,
    FullControl
  }

  public enum TaskResultStatus
  {
    Success = 0,
    Warning = 1,
    Failed = 2,
    NotYetTried = 41,
    NotExecuted = 43,
    BeingCanceled = 44,
    Canceled = 45,
    Aborted = 46,
    AlreadyExists = 47,
    NotFound = 48,
    Exception = 49,
    NotSet = 50,
    InProgress = 51,
    Complete = 52,
    CompleteWithNonFatalErrors = 53,
    CompleteWithFatalErrors = 54,
    NoConfiguration = 55
  }

  public enum TaskType
  {
    DummyTask,
    Ping,
    SvnBackup,
    SvnUpdate,
    SvnCommit,
    StatusCheck,
    DeleteFiles,
    AppLogCleanup,
    IISLogCleanup,
    GetControl,
    CheckConfig,
    LogTest,
    GetPerformanceCounters,
    CheckAppLogForErrors,
    GeminiMonitor,
    OtherTask,
    CraigslistExtract,
    CraigslistAutos,
    SendFilesToWebService
  }

  public enum LogDetailType
  {
    NotSet,
    TEXT,
    MSGX,
    EXCP,
    DATA,
    DUMP,
    OTHR
  }

  public enum Status
  {
    NotSet = 0,
    Setup = 1,
    Initial = 2,
    Active = 3,
    Overdue = 4,
    Suspended = 5,
    Inactive = 6,
    NotUsed = 7,
    Closed = 8,
    Deceased = 9,
    Defunct = 10,
    CreatedByMistake = 11,
    Deleted = 12,
    PlaceHolder = 13,
    Obsolete = 14,
    InProgress = 15,
    Failed = 16,
    Complete = 17,
    Required = 18,
    Expired = 19,
    Canceled = 20
  }

  public enum ApplicationFunction
  {
    None,
    PreDBInit,
    Initializing,
    RunNormal,
    Reconfigure,
    Shutdown
  }

  public enum ApplicationState
  {
    None,
    OK,
    CriticalError
  }

  public enum DatabaseType
  {
    NotSet,
    OracleDsn,
    SqlServerDsn,
    SqlServer,
    SqlServerEF,
    MySql
  }

  public enum NotifyConfigMode
  {
    None,
    Database,
    AppConfig
  }

  public enum WebServiceProtocol
  {
    NotSet,
    HTTP,
    NET_TCP
  }

  public enum WebServiceBinding
  {
    NotSet,
    BasicHttp,
    WsHttp,
    WsHttps,
    NetTcp
  }

  public enum WCFHostingModel
  {
    NotSet,
    IIS,
    HostedInWindowsService
  }

  public enum ConfigCreateMode
  {
    CreatePermanent,
    CreateAsUpdate,
    DoNotCreate
  }

  public enum ConfigurationType
  {
    NotSet,
    LocalFile,
    SharedFile,
    SharedService
  }

  public enum MappingRule
  {
    None,
    Boolean1,
    DoubleToDecimal,
    DoubleToFloat,
    DecimalToInt,
    CreateUserID,
    CreateDateTime,
    ModifiedUserID,
    ModifiedDateTime
  }

  public enum FileSystemCommand
  {
    NotSet,
    Create,
    Copy,
    Move,
    Rename,
    Delete,
    CreateMap,
    ReadTest,
    ReadWriteTest,
  }

  public enum LeftRightDirection
  {
    Left,
    Right,
    LeftToRight,
    RightToLeft
  }

  public enum FailureAction
  {
    Ignore,
    AbortGroup,
    AbortSet
  }

  public enum FileSystemAccessType
  {
    NotSet,
    ReadFile,
    WriteFile,
    CreateDirectory,
    ListDirectory
  }

  public enum TimeUnit
  {
    Second,
    Minute,
    Hour,
    Day,
    Week,
    Month,
    Quarter,
    Year
  }

  public enum FileCompareStatus
  {
    NotSet,
    OppositeMissing,
    OppositeExists,
    OppositeOlder,
    OppositeNewer,
    Equivalent,
    Identical
  }


  public enum PauseMethod
  {
    Drain,
    Hold,
    Clear
  }

  public enum StopMethod
  {
    Force,
    Drain
  }

  public enum WinServiceState
  {
    Running,
    Paused,
    Stopped
  }

  public enum StartDateOptions
  {
    UseMinValue,
    UseStartDateTime,
    UseStartTimeOnly
  }

  public enum EndDateOptions
  {
    UseMaxValue,
    UseEndDateTime,
    UseEndTimeOnly
  }

  public enum XObjectType
  {
    NotSet,
    GenericCollectionBased,
    Complex,
    Simple,
    XElement,
    Null
  }

  public enum DictionaryPart
  {
    Key,
    Value
  }
}
