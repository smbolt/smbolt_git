using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS.OrgScript.Compilation
{
  public enum TokenType
  {
    Unprocessed = 0,
    Unrecognized,
    WhiteSpace,

  }

  public enum CompilationPhase
  {
    Loading,
    Loaded,
    Parsing,


  }

  public enum ParserNextStep
  {
    SplitCodeForThreading,
  }


  public enum CompilerNextStep
  {
    ParseToSyntaxNodes
  }

  public enum CompilationSummaryResult
  {
    NotStarted,
    InProgress,
    CompletedWithWarnings,
    CompletedWithErrors,
    CompletedClean
  }

  public enum SyntaxNodeType
  {
    NotSet,
    Unidentified,
    CombinedOut,
    OpenCurlyBracket, CloseCurlyBracket,
    OpenParen, CloseParen,
    OpenSquareBracket, CloseSquareBracket,

    DollarSign, Period, DecimalPoint, Dot, Comma,
    QuestionMark, ExclamationPoint, DoubleQuote, Apostrophe,
    Semicolon, Colon, EqualSign, DoubleEqual,
    NotEqual, Ampersand, DoubleAmpersand, Asterisk,
    Dash, Minus, ForwardSlash, BackSlash, Pipe,
    BackTick, Tilde, AtSign, PoundSign, PercentSign,
    Caret, Underscore, PlusSign,

    GreaterThan, LessThan,

    Spaces, Tab, CarriageReturn, LineFeed,

    Text,
    Number
  }

}
