using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class Constants
  {
    public static string DoubleQuote = "\"";
    public static char[] AsteriskDelimiter = new char[] { '*' };
    public static char[] PipeDelimiter = new char[] { '|' };
    public static char[] CaretDelimiter = new char[] { '^' };
    public static char[] DashDelimiter = new char[] { '-' };
    public static char[] HyphenDelimiter = new char[] { '-' };
    public static char[] ColonDelimiter = new char[] { ':' };
    public static char[] FSlashDelimiter = new char[] { '/' };
    public static char[] BSlashDelimiter = new char[] { '\\' };
    public static char[] CommaDelimiter = new char[] { ',' };
    public static char[] DotDelimiter = new char[] { '.' };
    public static char[] PeriodDelimiter = new char[] { '.' };
    public static char[] EqualsDelimiter = new char[] { '=' };
    public static char[] UnderscoreDelimiter = new char[] { '_' };
    public static char[] OpenParen = new char[] { '(' };
    public static char[] CloseParen = new char[] { ')' };
    public static char[] SpaceDelimiter = new char[] { ' ' };
    public static char[] TildeDelimiter = new char[] { '~' };
    public static char[] NewLineDelimiter = new char[] { '\n' };
    public static char[] CrlfDelimiters = new char[] { '\n', '\r' };
    public static char[] RelOps = new char[] { '~', '=', '<', '>' };
    public static char[] SpaceOrNewLineDelimiter = new char[] { ' ', '\n' };
    public static char[] SpellCheckDelimiter = new char[] { ' ', '—', '\r', '\n', '\t' };
    public static char[] WhiteSpaceDelimiter = new char[] { ' ', '\r', '\n', '\t' };
    public static char[] BlankOrNewLine = new char[] { ' ', '\n' };
    public static char[] OpenAndCloseParen = new char[] { '(', ')' };
    public static char[] OpenAndCloseBrackets = new char[] { '[', ']' };
    public static char[] OpenBracket = new char[] { '[' };
    public static char[] CloseBracket = new char[] { ']' };
    public static char[] FSlashOrCloseBracket = new char[] { '/', ']' };
    public static char[] SlashDelimiter = new char[] { '\\', '/' };
    public static char[] NewLineReplacementCharacters = new char[] { '\xA4' }; 
    public static char CopyrightCharacter = '\u00A9';
    public static char TabReplacementCharacter = '\u00B1';
    public static char NewLineReplacementCharacter = '\xA4';
    public static char NewLineCharacter = '\n';
    public static char CarriageReturnCharacter = '\r';
    public static string[] ConditionRelOps = new string[] { "!=", ">=", "<=", "=", ">", "<" };
    public static string Copyright { get { return CopyrightCharacter.ToString(); } }
  }
}
