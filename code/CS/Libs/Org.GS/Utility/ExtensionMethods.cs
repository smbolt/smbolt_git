using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Diagnostics;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO.Compression;
using Org.GS.Database;
using Org.GS.Configuration;
using Org.GS;

namespace Org.GS
{
  public static class ExtensionMethods
  {
    public static string ToGrid(this int[] i, int cols, int width)
    {
      var sb = new StringBuilder();

      if (i == null)
        return "ARRAY IS NULL";

      if (i.Length == 0)
        return "ARRAY IS EMPTY";

      if (cols < 1 || cols > 100)
        cols = 10;

      if (width < 4 || cols > 20)
        width = 6;

      sb.Append("        ");
      for (int j = 0; j < cols; j++)
      {
        sb.Append(j.ToString().PadToJustifyRight(6));
      }
      sb.Append(g.crlf);

      sb.Append("        ");
      for (int j = 0; j < cols; j++)
      {
        sb.Append(new String(' ', 2) + new string('-', width - 2));
      }
      sb.Append(g.crlf);

      int r = 0;
      int c = 0;

      for (int ptr = 0; ptr < i.Length; ptr++)
      {
        if (c == 0)
          sb.Append(r.ToString().PadWithLeadingZeros(6) + "  ");

        int intValue = i[ptr];
        sb.Append(intValue.ToString().PadToJustifyRight(width));
        c++;

        if (ptr < i.Length - 1)
        {
          if (c >= cols)
          {
            sb.Append(g.crlf);
            c = 0;
            r++;
          }
        }
      }

      string grid = sb.ToString();
      return grid;
    }

    public static string ToGrid(this bool[] i, int cols, int width)
    {
      var sb = new StringBuilder();

      if (i == null)
        return "ARRAY IS NULL";

      if (i.Length == 0)
        return "ARRAY IS EMPTY";

      if (cols < 1 || cols > 100)
        cols = 10;

      if (width < 4 || cols > 20)
        width = 6;

      sb.Append("        ");
      for (int j = 0; j < cols; j++)
      {
        sb.Append(j.ToString().PadToJustifyRight(6));
      }
      sb.Append(g.crlf);

      sb.Append("        ");
      for (int j = 0; j < cols; j++)
      {
        sb.Append(new String(' ', 2) + new string('-', width - 2));
      }
      sb.Append(g.crlf);

      int r = 0;
      int c = 0;

      for (int ptr = 0; ptr < i.Length; ptr++)
      {
        if (c == 0)
          sb.Append(r.ToString().PadWithLeadingZeros(6) + "  ");

        string trueInd = i[ptr] ? "M" : ".";
        sb.Append(trueInd.PadToJustifyRight(width));
        c++;

        if (ptr < i.Length - 1)
        {
          if (c >= cols)
          {
            sb.Append(g.crlf);
            c = 0;
            r++;
          }
        }
      }

      string grid = sb.ToString();
      return grid;
    }

    public static string GetProcessFileName(this string baseFileName, FileProcessType fileProcessType, string newPrefix = "")
    {


      return baseFileName;
    }

    public static string[] GetRelationalExpression(this string s)
    {
      if (s.IsBlank())
        return new string[0];

      string foundRelOp = String.Empty;

      int pos = s.IndexOfAny(Constants.ConditionRelOps, out foundRelOp);

      if (pos == -1)
        return new string[] { s };

      string[] expressionTokens = new string[3];
      expressionTokens[0] = s.Substring(0, pos).Trim();
      expressionTokens[1] = s.Substring(pos, foundRelOp.Length).Trim();
      expressionTokens[2] = s.Substring(pos + foundRelOp.Length).Trim();

      return expressionTokens;
    }

    public static int IndexOfAny(this string s, string[] tokens, out string foundToken)
    {
      if (tokens == null || tokens.Length == 0)
      {
        foundToken = String.Empty;
        return -1;
      }

      int lowestIndex = s.Length;
      string lowestToken = String.Empty;

      foreach (var token in tokens)
      {
        int pos = s.IndexOf(token);
        if (pos != -1 && pos < lowestIndex)
        {
          lowestIndex = pos;
          lowestToken = token;
        }
      }

      foundToken = lowestToken;

      if (foundToken.IsBlank())
        return -1;

      return lowestIndex;
    }

    public static string NormalizePathString(this string s)
    {
      if (s.IsBlank())
        return String.Empty;

      s = s.Replace("@/", @"\").Trim();

      while (s.Contains(@"\\"))
        s = s.Replace(@"\\", @"\");

      while (s.Length > 1 && s.EndsWith(@"\"))
        s = s.Substring(0, s.Length - 1);

      return s;
    }


    public static ParmSet ToParmSet(this TaskParmSet tps)
    {
      if (tps == null || tps.Count == 0)
        return new ParmSet();

      var parmSet = new ParmSet();

      foreach (var tp in tps.Values)
      {
        parmSet.Add(tp.ToParm());
      }

      return parmSet;
    }

    public static Parm ToParm(this TaskParm p)
    {
      if (p == null)
        return null;

      var parm = new Parm();
      parm.ParameterName = p.Key;
      parm.ParameterValue = p.Value;
      parm.ParameterType = p.DataType;

      return parm;
    }

    public static void AssertFileExistence(this string folderName, int count, string searchPattern = "", bool allowOpenFiles = true)
    {
      if (folderName.IsBlank())
        throw new Exception("The folder name is blank or null.");

      if (!Directory.Exists(folderName))
        throw new Exception("The folder '" + folderName + "' does not exist.");

      string[] files = null;

      if (searchPattern.IsNotBlank())
        files = Directory.GetFiles(folderName, searchPattern);
      else
        files = Directory.GetFiles(folderName);


      if (allowOpenFiles)
      {
        if (files.Count() == 2)
        {
          string fullFilePath0 = files[0];
          string fullFilePath1 = files[1];

          string fileName0 = Path.GetFileName(fullFilePath0).Replace("~$", String.Empty).ToLower();
          string fileName1 = Path.GetFileName(fullFilePath1).Replace("~$", String.Empty).ToLower();

          if (fileName0 == fileName1)
            return;
        }
      }

      if (files.Length != count)
        throw new Exception("The number of files present in the directory '" + folderName + "' is " + files.Length.ToString() +
                            " which is different than the count to assert which is " + count.ToString() + ".");
    }

    public static string ActionTag(this object o)
    {
      if (o == null)
        return String.Empty;

      var pi = o.GetType().GetProperty("Tag");

      if (pi == null)
        return String.Empty;

      var tagValue = pi.GetValue(o);

      if (tagValue == null)
        return String.Empty;

      return tagValue.ToString();
    }

    public static string ControlTag(this object o)
    {
      if (o == null)
        return String.Empty;

      var pi = o.GetType().GetProperty("Tag");

      if (pi == null)
        return String.Empty;

      var tagValue = pi.GetValue(o);

      if (tagValue == null)
        return String.Empty;

      return tagValue.ToString();
    }

    public static void SetInitialSizeAndLocation(this System.Windows.Forms.Form f, int horizontalSize = 0, int verticalSize = 0)
    {
      if (horizontalSize == 0)
        horizontalSize = 80;

      if (verticalSize == 0)
        verticalSize = 80;


      int formHorizontalSize = g.GetCI("MainFormHorizontalSize").ToInt32OrDefault(horizontalSize);
      int formVerticalSize = g.GetCI("MainFormVerticalSize").ToInt32OrDefault(verticalSize);

      f.Size = new Size(Screen.PrimaryScreen.Bounds.Width * formHorizontalSize / 100,
                        Screen.PrimaryScreen.Bounds.Height * formVerticalSize / 100);
      f.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - f.Width / 2,
                             Screen.PrimaryScreen.Bounds.Height / 2 - f.Height / 2);
    }

    //[DebuggerStepThrough]
    public static void AppendAndScroll(this TextBox t, string message, string crlf = "")
    {
      if (t == null || message == null)
        return;

      if (crlf == null || crlf == "")
        crlf = g.crlf;

      t.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " " + message + crlf;
      t.SelectionStart = t.Text.Length;
      t.SelectionLength = 0;
      t.ScrollToCaret();
      Application.DoEvents();
    }

    [DebuggerStepThrough]
    public static void ResizeAndCenterVertically(this System.Windows.Forms.Form f, Size? sz = null)
    {
      if (sz.HasValue)
        f.Size = sz.Value;

      f.Location = new Point(f.Location.X, Screen.PrimaryScreen.Bounds.Height / 2 - f.Height / 2);
    }

    [DebuggerStepThrough]
    public static void CenterFormOnScreen(this System.Windows.Forms.Form f)
    {
      f.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - f.Width / 2,
                             Screen.PrimaryScreen.Bounds.Height / 2 - f.Height / 2);
    }

    public static string GetUniqueFile(this string folderName)
    {
      AssertFileExistence(folderName, 1);

      string[] files = Directory.GetFiles(folderName);

      if (files.Count() == 2)
      {
        string fullFilePath0 = files[0];
        string fullFilePath1 = files[1];

        string fileName0 = Path.GetFileName(fullFilePath0).Replace("~$", String.Empty).ToLower();
        string fileName1 = Path.GetFileName(fullFilePath1).Replace("~$", String.Empty).ToLower();

        if (fileName0 == fileName1)
        {
          if (fullFilePath0.Contains(@"\~$"))
            return fullFilePath1;
          if (fullFilePath1.Contains(@"\~$"))
            return fullFilePath0;
        }
      }

      return Directory.GetFiles(folderName).First();
    }

    public static bool MatchesPattern(this string s, string pattern)
    {
      if (s.IsBlank() || pattern.IsBlank())
        return false;

      return Regex.Match(s, pattern).Success;
    }

    public static string ClipStartCharacters(this string s, int count = 1, bool trimFirst = true)
    {
      if (s.IsBlank())
        return String.Empty;

      if (trimFirst)
        s = s.Trim();

      if (count >= s.Length - 1)
        return String.Empty;

      return s.Substring(count);
    }

    public static string ClipEndCharacters(this string s, int count = 1, bool trimFirst = true)
    {
      if (s.IsBlank())
        return String.Empty;

      if (trimFirst)
        s = s.Trim();

      if (count >= s.Length)
        return String.Empty;

      return s.Substring(0, s.Length - count);
    }

    public static string[] TrimArrayTokens(this string[] array)
    {
      if (array == null)
        return array;

      for (int i = 0; i < array.Length; i++)
        array[i] = array[i].Trim();

      return array;
    }

    //public static FileSystemItemSet CreateFSISet(this ZipArchive item, FileSystemItem parent)
    //{
    //  var fsiSet = new FileSystemItemSet();

    //  foreach (var entry in item.Entries)
    //  {
    //    FileSystemItemSet targetFsiSet = null;
    //    string folderName = null;
    //    bool isFile = entry.Name.IsNotBlank();
    //    if (entry.Name.IsNotBlank())
    //      folderName = entry.FullName.Replace(entry.Name, String.Empty);
    //    if (entry.Name.IsBlank())
    //      folderName = entry.FullName;

    //    if (folderName.IsBlank())
    //    {
    //      targetFsiSet = fsiSet;
    //    }
    //    else
    //    {
    //      var folder = fsiSet.EnsureFolder(folderName);
    //      targetFsiSet = folder.FileSystemItemSet;
    //    }

    //    if (isFile)
    //    {
    //      var fsi = new FileSystemItem(entry.FullName);
    //      targetFsiSet.Add(fsi);
    //    }
    //  }

    //  return fsiSet;
    //}

    public static void ScrollToBottom(this TextBox t)
    {
      t.SelectionStart = t.Text.Length;
      t.SelectionLength = 0;
      t.ScrollToCaret();
    }

    public static string ExceptionReport(this Exception e)
    {
      StringBuilder sb = new StringBuilder();

      sb.Append("ExceptionType: " + e.GetType().ToString() + g.crlf +
                "Message: " + e.Message + g.crlf +
                "StackTrace:" + e.StackTrace + g.crlf);

      if (e.InnerException != null)
      {
        sb.Append("InnerExceptionType: " + e.InnerException.GetType().ToString() + g.crlf +
                  "InnerExceptionMessage: " + e.InnerException.Message + g.crlf +
                  "InnerExceptionStackTrace:" + e.InnerException.StackTrace + g.crlf);
      }

      return sb.ToString();
    }

    public static List<string> ToLowerCaseAndTrim(this List<string> list)
    {
      var lcList = new List<string>();

      if (list == null || list.Count == 0)
        return lcList;

      foreach (string s in list)
        lcList.Add(s.ToLower().Trim());

      return lcList;
    }

    public static List<string> ToFileNameList(this string[] array)
    {
      var list = new List<string>();

      if (array == null || array.Length == 0)
        return list;

      foreach (var path in array)
      {
        list.Add(System.IO.Path.GetFileName(path));
      }

      return list;
    }

    public static string GetTypeName(this Type t)
    {
      string typeName = t.Name;


      if (typeName == "Nullable`1")
      {
        Type[] genericTypes = GetGenericTypeArgs(t);
        if (genericTypes.Length == 1)
        {
          return genericTypes[0].Name + "?";
        }
      }

      return typeName;
    }


    public static Type[] GetGenericTypeArgs(this Type t)
    {
      Type[] args = new Type[0];

      Type theType = t;
      while (args.Length == 0 && t != null)
      {
        args = t.GetGenericArguments();
        if (args.Length > 0)
          return args;
        t = t.BaseType;
      }

      return args;
    }

    public static string ObjectArrayToString(this object[] array)
    {
      if (array == null)
        return String.Empty;

      if (array.Length == 0)
        return String.Empty;

      string stringValue = String.Empty;
      for (int i = 0; i < array.Length; i++)
      {
        if (array[i] != null)
        {
          if (stringValue.Length > 0)
            stringValue += "," + array[i].ToString();
          else
            stringValue = array[i].ToString();
        }
      }

      return stringValue;
    }

    [DebuggerStepThrough]
    public static bool IsBracketed(this string value)
    {
      if (value.IsBlank())
        return false;

      string trimmedValue = value.Trim();

      if (trimmedValue.Length < 2)
        return false;

      return trimmedValue.StartsWith("[") && trimmedValue.EndsWith("]");
    }

    [DebuggerStepThrough]
    public static bool IsSingleQuoted(this string value)
    {
      if (value.IsBlank())
        return false;

      string trimmedValue = value.Trim();

      if (trimmedValue.Length < 2)
        return false;

      return trimmedValue.StartsWith("'") && trimmedValue.EndsWith("'");
    }

    //[DebuggerStepThrough]
    public static string GetBracketedText(this string value)
    {
      if (value.IsBlank())
        return String.Empty;

      string trimmedValue = value.Trim();

      if (!trimmedValue.IsBracketed())
        return String.Empty;

      return trimmedValue.Substring(1, trimmedValue.Length - 2);
    }

    //[DebuggerStepThrough]
    public static string GetSingleQuotedText(this string value)
    {
      if (value.IsBlank())
        return String.Empty;

      string trimmedValue = value.Trim();

      if (!trimmedValue.IsSingleQuoted())
        return trimmedValue;

      return trimmedValue.Substring(1, trimmedValue.Length - 2);
    }

    //[DebuggerStepThrough]
    public static string GetTextBetweenBrackets(this string value)
    {
      if (value.IsBlank())
        return String.Empty;

      string trimmedValue = value.Trim();

      int posOpenBracket = trimmedValue.IndexOf('[');
      if (posOpenBracket == -1)
        return String.Empty;

      int posCloseBracket = trimmedValue.IndexOf(']', posOpenBracket);
      if (posCloseBracket == -1)
        return String.Empty;

      string returnValue = trimmedValue.Substring(posOpenBracket + 1, posCloseBracket - posOpenBracket - 1).Trim();

      return returnValue;
    }

    //[DebuggerStepThrough]
    public static string GetTextBetweenParenthesis(this string value)
    {
      if (value.IsBlank())
        return String.Empty;

      string trimmedValue = value.Trim();

      int posOpenParen = trimmedValue.IndexOf('(');
      if (posOpenParen == -1)
        return String.Empty;

      int posCloseParen = trimmedValue.IndexOf(')', posOpenParen);
      if (posCloseParen == -1)
        return String.Empty;

      string returnValue = trimmedValue.Substring(posOpenParen + 1, posCloseParen - posOpenParen - 1).Trim();

      return returnValue;
    }

    [DebuggerStepThrough]
    public static bool IsCaseInsensitiveEqual(this string value, string compareValue)
    {
      if (value == null || compareValue == null)
        return false;

      string valueLc = value.ToLower();
      string compareValueLc = compareValue.ToLower();

      return String.Equals(valueLc, compareValueLc, StringComparison.OrdinalIgnoreCase);
    }

    [DebuggerStepThrough]
    public static string StringArrayToString(this string[] array)
    {
      if (array == null)
        return String.Empty;

      if (array.Length == 0)
        return String.Empty;

      string stringValue = String.Empty;
      for (int i = 0; i < array.Length; i++)
      {
        if (array[i] != null)
        {
          if (stringValue.Length > 0)
            stringValue += "," + array[i];
          else
            stringValue = array[i];
        }
      }

      return stringValue;
    }

    //[DebuggerStepThrough]
    public static string[] StripFirstElement(this string[] array)
    {
      if (array == null)
        return new string[0];

      if (array.Length == 1)
        return new string[0];

      var truncatedArray = new string[array.Length - 1];

      for (int i = 1; i < array.Length; i++)
        truncatedArray[i - 1] = array[i];

      return truncatedArray;
    }

    //[DebuggerStepThrough]
    public static string[] StripLastElement(this string[] array)
    {
      if (array == null)
        return new string[0];

      if (array.Length == 1)
        return new string[0];

      var truncatedArray = new string[array.Length - 1];

      for (int i = 0; i < array.Length - 1; i++)
        truncatedArray[i] = array[i];

      return truncatedArray;
    }

    //[DebuggerStepThrough]
    public static string[] NumericTokensOnly(this string[] array)
    {
      if (array == null || array.Length == 0)
        return new string[0];

      var numericTokenList = new List<string>();
      for (int i = 0; i < array.Length; i++)
      {
        if (array[i].IsValidIntOrDecimal())
        {
          numericTokenList.Add(array[i].Trim());
        }
      }

      return numericTokenList.ToArray();
    }

    [DebuggerStepThrough]
    public static string[] ObjectArrayToStringArray(this object[] array)
    {
      if (array == null)
        return new string[0];

      if (array.Length == 0)
        return new string[0];

      string stringValue = String.Empty;
      for (int i = 0; i < array.Length; i++)
      {
        if (array[i] != null)
        {
          if (stringValue.Length > 0)
            stringValue += "," + array[i].ToString();
          else
            stringValue = array[i].ToString();
        }
      }

      return stringValue.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);
    }

    public static string GetNextToken(this string value, int beginPos = 0)
    {
      if (value.IsBlank())
        return String.Empty;

      if (beginPos < 0)
        beginPos = 0;

      if (beginPos > value.Length - 1)
        return String.Empty;

      string s = beginPos > 0 ? value.Substring(beginPos).Trim() : value.Trim();

      int delimiter = s.IndexOfAny(new char[] { ' ', '\n' });

      if (delimiter == -1)
        return s;

      string token = s.Substring(0, delimiter).Trim();
      return token;
    }

    public static bool IsValidShortDate(this string value)
    {
      if (value == null)
        return false;

      value = value.Trim();

      if (value.Length == 0)
        return false;

      int slashCount = value.CountOfChar('/');
      int dashCount = value.CountOfChar('-');

      if (!Char.IsLetter(value[0]) && slashCount == 0 && dashCount == 0)
        return false;

      DateTime testDt = DateTime.MinValue;
      if (DateTime.TryParse(value, out testDt))
      {
        if (testDt.Year < 1950 || testDt.Year > 2100)
          return false;

        return true;
      }

      return false;
    }

    [DebuggerStepThrough]
    public static string RemoveTrailingSlash(this string path)
    {
      if (path == null)
        return String.Empty;

      path = path.Trim();

      if (path.Length == 0)
        return String.Empty;

      int lastCharPos = path.Length - 1;

      if (lastCharPos == 0)
        return String.Empty;

      string lastChar = path[lastCharPos].ToString();

      if (lastChar == @"\" || lastChar == @"/")
        return path.Substring(0, path.Length - 1);

      return path;
    }

    [DebuggerStepThrough]
    public static string EnsureSingleTrailingSlash(this string path)
    {
      if (path.IsBlank())
        return String.Empty;

      path = path.Replace("/", @"\");

      while (path.Length > 1 && path.EndsWith(@"\"))
      {
        int newLth = path.Length - 1;
        path = path.Substring(0, newLth);
      }

      return path + @"\";
    }

    [DebuggerStepThrough]
    public static string RemoveTrailingComma(this string value)
    {
      if (value == null)
        return String.Empty;

      value = value.Trim();

      if (value.Length == 0)
        return String.Empty;

      int lastCharPos = value.Length - 1;

      if (lastCharPos == 0)
        return String.Empty;

      string lastChar = value[lastCharPos].ToString();

      if (lastChar == @",")
        return value.Substring(0, value.Length - 1);

      return value;
    }

    [DebuggerStepThrough]
    public static bool IsValidCCYYMMDD(this string value)
    {
      if (value == null)
        return false;

      value = value.Trim();

      if (!value.IsNumeric())
        return false;

      if (value.Length != 8)
        return false;

      string year = value.Substring(0, 4);
      string month = value.Substring(4, 2);
      string day = value.Substring(6, 2);

      DateTime dateCheck;

      if (!DateTime.TryParse(month + "/" + day + "/" + year, out dateCheck))
        return false;

      return true;
    }

    public static bool IsValidMMMYY(this string value)
    {

      return true;
    }

    public static bool IsValidMMYYYY(this string value)
    {
      if (value == null)
        return false;

      value = value.Trim();

      if (!value.IsNumeric())
        return false;

      if (value.Length != 6)
        return false;

      string year = value.Substring(2, 4);
      string month = value.Substring(0, 2);
      string day = "01";

      DateTime dateCheck;

      if (!DateTime.TryParse(month + "/" + day + "/" + year, out dateCheck))
        return false;

      return true;
    }

    public static bool IsValidMMDDYY(this string value)
    {
      if (value == null)
        return false;

      value = value.Trim();

      if (!value.IsNumeric())
        return false;

      if (value.Length != 6)
        return false;

      string month = value.Substring(0, 2);
      string day = value.Substring(2, 2);
      string year = value.Substring(4, 2);

      DateTime dateCheck;

      if (!DateTime.TryParse(month + "/" + day + "/" + year, out dateCheck))
        return false;

      return true;
    }

    public static bool IsValidMMDDCCYY(this string value)
    {
      if (value == null)
        return false;

      value = value.Trim();

      if (!value.IsNumeric())
        return false;

      if (value.Length != 8)
        return false;

      string month = value.Substring(0, 2);
      string day = value.Substring(2, 2);
      string year = value.Substring(4, 4);

      DateTime dateCheck;

      if (!DateTime.TryParse(month + "/" + day + "/" + year, out dateCheck))
        return false;

      return true;
    }

    public static DbTable ToDbTable(this Type t)
    {
      if (t == null)
        throw new Exception("The type parameter is null.");

      var entityMap = t.GetCustomAttributes(typeof(EntityMap), false).FirstOrDefault() as EntityMap;

      if (entityMap == null)
        throw new Exception("The EntityMap attribute is null on type '" + t.Name + "'.");

      var dbTable = new DbTable();
      dbTable.TableName = entityMap.TableName.IsBlank() ? t.Name : entityMap.TableName;

      var columnList = new List<string>();

      var piSet = t.GetProperties();

      foreach (var pi in piSet)
      {
        var colEntityMap = pi.GetCustomAttributes(typeof(EntityMap), false).FirstOrDefault() as EntityMap;
        if (colEntityMap != null)
        {
          var dbColumn = new DbColumn();
          dbColumn.Name = colEntityMap.ColumnName.IsBlank() ? pi.Name : colEntityMap.ColumnName;
          dbColumn.IsSequencer = colEntityMap.Sequencer;
          dbColumn.DbType = new Database.DbType() {
            SqlDbType = pi.PropertyType.ToSqlDbType()
          };
          dbTable.DbColumnSet.Add(dbColumn.Name, dbColumn);
        }
      }

      if (dbTable.DbColumnSet.Count == 0)
        throw new Exception("There are no columns decorated with 'EntityMap' attributes in the entity type '" + t.Name + "'.");

      return dbTable;
    }

    //[DebuggerStepThrough]
    public static SqlDbType ToSqlDbType(this Type t)
    {
      switch (t.Name)
      {
        case "Image":
          return SqlDbType.Image;
        case "String":
          return SqlDbType.VarChar;
        case "DateTime":
          return SqlDbType.Date;
        case "TimeSpan":
          return SqlDbType.Time;
        case "Int16":
          return SqlDbType.SmallInt;
        case "Int32":
          return SqlDbType.Int;
        case "Int64":
          return SqlDbType.BigInt;
        case "Decimal":
          return SqlDbType.Real;
        case "Boolean":
          return SqlDbType.Bit;
        case "Char":
          return SqlDbType.Char;
      }

      // may need to implement more types...
      throw new Exception("Translation of data type '" + t.Name + "' to SqlDbType is not yet implemented.");
    }

    [DebuggerStepThrough]
    public static Color ToColor(this string value)
    {
      if (value == null)
        return Color.White;

      if (value.IsBlank())
        return Color.White;

      if (value.CharCount('.') != 2)
        return Color.White;

      int[] tokens = value.ToTokenArrayInt32(Constants.PeriodDelimiter);

      if (tokens.Length != 3)
        return Color.White;

      foreach (int token in tokens)
      {
        if (token < 0 || token > 255)
          return Color.White;
      }

      return Color.FromArgb(tokens[0], tokens[1], tokens[2]);
    }

    [DebuggerStepThrough]
    public static string GetStringValueOrNull(this string value)
    {
      if (value == null)
        return null;

      if (value.IsBlank())
        return null;

      if (value == "")
        return null;

      return value.Trim();
    }

    [DebuggerStepThrough]
    public static DateTime? GetDateValueOrNull(this string value)
    {
      if (value == null)
        return null;

      if (value.IsValidShortDate())
        return value.ShortDateToDateTime();

      return null;
    }

    public static bool IsValidMMSlashYYYY(this string s)
    {
      if (s.IsBlank())
        return false;

      string[] tokens = s.Split(Constants.FSlashDelimiter, StringSplitOptions.RemoveEmptyEntries);

      if (tokens.Length != 2)
        return false;

      if (tokens[0].IsNotNumeric() || tokens[1].IsNotNumeric())
        return false;

      int mm = tokens[0].ToInt32();
      int yyyy = tokens[1].ToInt32();

      if (mm < 1 || mm > 12)
        return false;

      if (yyyy < 1900 || yyyy > 2199)
        return false;

      return true;
    }

    [DebuggerStepThrough]
    public static bool IsFullySpelledMonth(this string s)
    {
      if (s == null)
        return false;

      string trimmedValue = s.Trim().ToLower();

      switch (trimmedValue)
      {
        case "january":
        case "february":
        case "march":
        case "april":
        case "may":
        case "june":
        case "july":
        case "august":
        case "september":
        case "october":
        case "november":
        case "december":
          return true;
      }

      return false;
    }

    [DebuggerStepThrough]
    public static bool IsAbbreviatedMonth(this string s)
    {
      if (s == null)
        return false;

      string trimmedValue = s.Trim().ToLower();

      switch (trimmedValue)
      {
        case "jan":
        case "feb":
        case "mar":
        case "apr":
        case "may":
        case "jun":
        case "jul":
        case "aug":
        case "sep":
        case "oct":
        case "nov":
        case "dec":
          return true;
      }

      return false;
    }

    [DebuggerStepThrough]
    public static bool IsReasonableYYYY(this string value)
    {
      if (value == null)
        return false;

      string trimmedValue = value.Trim();

      if (trimmedValue.Length != 4)
        return false;

      foreach (char c in trimmedValue)
      {
        if (!Char.IsDigit(c))
          return false;
      }

      int year = trimmedValue.ToInt32();

      if (year < g.EarliestReasonableYear || year > g.LatestReasonableYear)
        return false;

      return true;
    }

    [DebuggerStepThrough]
    public static bool IsYY(this string value)
    {
      if (value == null)
        return false;

      string trimmedValue = value.Trim();

      if (trimmedValue.Length != 2)
        return false;

      foreach (char c in trimmedValue)
      {
        if (!Char.IsDigit(c))
          return false;
      }

      return true;
    }

    //[DebuggerStepThrough]
    public static bool IsValidDD(this string value)
    {
      if (value == null)
        return false;

      string trimmedValue = value.Trim().Replace(",", String.Empty);

      if (trimmedValue.Length != 2)
        return false;

      if (!trimmedValue.IsInteger())
        return false;

      int dd = trimmedValue.ToInt32();

      if (dd < 1 | dd > 31)
        return false;

      return true;
    }

    [DebuggerStepThrough]
    public static bool IsValidTime(this string value)
    {
      if (value == null)
        return false;

      value = value.Trim().ToUpper();

      bool amFound = value.Contains("AM");
      bool pmFound = value.Contains("PM");

      value = value.Replace("AM", String.Empty).Replace("PM", String.Empty);

      int colonCount = value.CountOfChar(':');
      if (colonCount < 1 || colonCount > 2)
        return false;

      string[] tokens = value.Split(Constants.ColonDelimiter, StringSplitOptions.RemoveEmptyEntries);

      if (tokens.Length < 2 || tokens.Length > 3)
        return false;

      int hour = 0;
      int minute = 0;
      int second = 0;

      if (tokens[0].IsNotNumeric())
        return false;

      hour = tokens[0].ToInt32();

      if (tokens[1].IsNotNumeric())
        return false;

      minute = tokens[1].ToInt32();

      if (tokens.Length == 3)
      {
        if (tokens[2].IsNotNumeric())
          return false;

        second = tokens[2].ToInt32();
      }

      if (hour > 23)
        return false;

      if (minute > 59)
        return false;

      if (second > 59)
        return false;

      return true;
    }

    [DebuggerStepThrough]
    public static TimeSpan ToTimeSpan(this string value)
    {
      TimeSpan zeroTimeSpan = new TimeSpan(0);

      if (value == null)
        return zeroTimeSpan;

      value = value.Trim();

      if (value.CountOfChar(':') != 1)
        return zeroTimeSpan;

      string[] tokens = value.Split(Constants.ColonDelimiter, StringSplitOptions.RemoveEmptyEntries);

      if (tokens.Length != 2)
        return zeroTimeSpan;

      int hour = 0;
      int minute = 0;

      if (tokens[0].IsNotNumeric())
        return zeroTimeSpan;
      hour = tokens[0].ToInt32();

      if (tokens[1].IsNotNumeric())
        return zeroTimeSpan;
      minute = tokens[1].ToInt32();

      if (hour > 23)
        return zeroTimeSpan;

      if (minute > 59)
        return zeroTimeSpan;

      return new TimeSpan(hour, minute, 0);
    }

    [DebuggerStepThrough]
    public static TimeSpan? ToNullableTimeSpan(this string value)
    {
      if (value == null)
        return (TimeSpan?)null;

      return value.ToTimeSpan();
    }

    [DebuggerStepThrough]
    public static bool IsFormattedNumeric(this string value)
    {
      if (value == null)
        return false;

      if (value.Trim().Length == 0)
        return false;

      if (value.CountOfChar('.') > 1)
        return false;

      value = value.Trim().Replace(".", String.Empty).Replace(",", String.Empty);

      foreach (Char c in value)
        if (!Char.IsNumber(c))
          return false;

      return true;
    }


    [DebuggerStepThrough]
    public static int CountOfToken(this string value, string token)
    {
      if (value == null || token == null)
        return 0;

      if (value.Trim().Length == 0)
        return 0;

      int count = 0;
      int pos = 0;
      while (pos != -1)
      {
        pos = value.IndexOf(token, pos);
        if (pos != -1)
        {
          count++;
          pos++;
        }
      }

      return count;
    }

    public static DateTime? ShortDateToDateTime(this string value)
    {
      if (value == null)
        return null;

      DateTime testDt = DateTime.MinValue;
      if (DateTime.TryParse(value, out testDt))
        return testDt;

      return null;
    }

    [DebuggerStepThrough]
    public static DateTime CCYYMMDDToDateTime(this string value)
    {
      if (value == null)
        return DateTime.MinValue;

      if (value.Trim().Length == 0)
        return DateTime.MinValue;

      DateTime dtReturnValue = DateTime.MinValue;

      value = value.Trim();

      if (value.IsNotNumeric())
        return DateTime.MinValue;

      if (value.Length != 8)
        return DateTime.MinValue;

      string year = value.Substring(0, 4);
      string month = value.Substring(4, 2);
      string day = value.Substring(6, 2);


      if (!DateTime.TryParse(month + "/" + day + "/" + year, out dtReturnValue))
        return DateTime.MinValue;

      return dtReturnValue;
    }

    [DebuggerStepThrough]
    public static DateTime CCYYMMDDHHMMSSToDateTime(this string value)
    {
      if (value == null)
        return DateTime.MinValue;

      if (value.Trim().Length == 0)
        return DateTime.MinValue;

      DateTime dtReturnValue = DateTime.MinValue;

      value = value.Trim();

      if (value.IsNotNumeric())
        return DateTime.MinValue;

      if (value.Length != 14)
        return DateTime.MinValue;

      string year = value.Substring(0, 4);
      string month = value.Substring(4, 2);
      string day = value.Substring(6, 2);
      string hour = value.Substring(8, 2);
      string minute = value.Substring(10, 2);
      string second = value.Substring(12, 2);

      if (!DateTime.TryParse(month + "/" + day + "/" + year + " " + hour + ":" + minute + ":" + second, out dtReturnValue))
        return DateTime.MinValue;

      return dtReturnValue;
    }

    [DebuggerStepThrough]
    public static DateTime EndOfDay(this DateTime value)
    {
      if (value == null)
        return DateTime.MaxValue;

      return new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
    }

    private static string GetExceptionData(int level, Exception e, bool includeStackTrace)
    {
      StringBuilder sb = new StringBuilder();

      string stackTrace = includeStackTrace ? "StackTrace:" + e.StackTrace + g.crlf : String.Empty;

      sb.Append("(" + level.ToString() + ") ExceptionType: " + e.GetType().ToString() + g.crlf +
                "Message: " + e.Message + g.crlf + stackTrace);

      if (e.InnerException != null)
        sb.Append(g.crlf + GetExceptionData(++level, e.InnerException, includeStackTrace));

      return sb.ToString();
    }

    public static string GetBits(this byte b)
    {
      BitArray bitArray = new BitArray(new byte[] { b });

      int[] bits = new int[8];
      for (int i = 0; i < 8; i++)
        bits[i] = bitArray[i] ? 1 : 0;

      string returnString =
        bits[7].ToString() +
        bits[6].ToString() +
        bits[5].ToString() +
        bits[4].ToString() +
        bits[3].ToString() +
        bits[2].ToString() +
        bits[1].ToString() +
        bits[0].ToString();

      return returnString;
    }

    public static byte SetBitOn(this byte b, int bitNumber)
    {
      if (bitNumber < 0 || bitNumber > 7)
        return b;

      byte mask = (byte)(1 << bitNumber);
      b = (byte)(b | mask);

      return b;
    }

    public static byte SetBitOff(this byte b, int bitNumber)
    {
      if (bitNumber < 0 || bitNumber > 7)
        return b;

      byte mask = (byte)(255 - (1 << bitNumber));
      b = (byte)(b & mask);

      return b;
    }

    public static string ToHex(this byte b)
    {
      string returnValue = BitConverter.ToString(new byte[] { b });
      return returnValue;
    }

    public static byte ToByte(this string s, int pos)
    {
      byte returnValue = Convert.ToByte(s.Substring(pos, 2), 16);
      return returnValue;
    }

    [DebuggerStepThrough]
    public static bool IsValidOrdinalNumber(this string value)
    {
      if (value == null)
        return false;

      if (value.Length < 3)
        return false;

      if (value.Substring(0, 1).IsNotNumeric())
        return false;

      string last2 = value.Substring(value.Length - 2, 2).ToLower();

      if (last2 != "st" && last2 != "nd" && last2 != "rd" && last2 != "th")
        return false;

      string numericPart = value.Substring(0, value.Length - 2);

      if (numericPart.IsNotNumeric())
        return false;

      if (numericPart.Length > 1)
      {
        string last2Numbers = numericPart.Substring(numericPart.Length - 2, 2);

        if (last2Numbers == "11" || last2Numbers == "12" || last2Numbers == "13")
        {
          if (last2 == "th")
            return true;
          else
            return false;
        }
      }

      string lastNumber = numericPart.Substring(numericPart.Length - 1, 1);

      switch (lastNumber)
      {
        case "1":
          return last2 == "st";
        case "2":
          return last2 == "nd";
        case "3":
          return last2 == "rd";
      }

      return last2 == "th";
    }

    [DebuggerStepThrough]
    public static bool HasSentenceEndingPunctuation(this string value)
    {
      if (value == null)
        return false;

      string testValue = value.Trim();

      if (testValue.EndsWith("."))
        return true;

      if (testValue.EndsWith("!"))
        return true;

      if (testValue.EndsWith("?"))
        return true;

      return false;
    }

    [DebuggerStepThrough]
    public static string PrepForSpellCheck(this string value)
    {
      if (value == null)
        return String.Empty;

      string noPunctValue = value.Replace(",", String.Empty)
                            .Replace(".", String.Empty)
                            .Replace(";", String.Empty)
                            .Replace(":", String.Empty)
                            .Replace("—", String.Empty)
                            .Replace("\"", String.Empty)
                            .Replace("“", String.Empty)
                            .Replace("”", String.Empty)
                            .Replace("‑", "-");

      if (noPunctValue.IsNumeric())
        return String.Empty;

      return noPunctValue;
    }

    [DebuggerStepThrough]
    public static int IndexOfNoRepeatChar(this string value, char c, int startIndex)
    {
      if (value == null)
        return -1;

      int pos = value.IndexOf(c, startIndex);
      if (pos == -1)
        return -1;

      if (pos > value.Length - 2)
        return -1;

      int next = pos + 1;
      if (value[next] == c)
        return -1;

      return pos;
    }

    [DebuggerStepThrough]
    public static string DoubleCharAtPos(this string value, int pos)
    {
      if (value == null)
        return String.Empty;

      if (pos > value.Length - 1)
        return String.Empty;

      string charToDouble = value.Substring(pos, 1);
      string newValue = value.Substring(0, pos) + charToDouble + value.Substring(pos, value.Length - pos);

      return newValue;
    }

    [DebuggerStepThrough]
    public static string ToDelimitedList(this List<string> value, string delimiter)
    {
      if (value == null || delimiter == null)
        return String.Empty;

      StringBuilder sb = new StringBuilder();

      foreach (string s in value)
      {
        if (sb.Length > 0)
          sb.Append("," + s.Trim());
        else
          sb.Append(s.Trim());
      }

      return sb.ToString();
    }

    [DebuggerStepThrough]
    public static string ToWhereIn(this List<int> value, string columnName, bool addNewLine = true)
    {
      if (value == null || value.Count == 0 || columnName.IsBlank())
        return String.Empty;

      return "WHERE " + columnName + " IN (" + value.ToDelimitedList(",") + ")" + (addNewLine ? g.crlf : String.Empty);
    }

    [DebuggerStepThrough]
    public static string ToDelimitedList(this List<int> value, string delimiter)
    {
      if (value == null || delimiter == null)
        return String.Empty;

      StringBuilder sb = new StringBuilder();

      foreach (int i in value)
      {
        if (sb.Length > 0)
          sb.Append("," + i.ToString().Trim());
        else
          sb.Append(i.ToString().Trim());
      }

      return sb.ToString();
    }

    [DebuggerStepThrough]
    public static Size Inflate(this Size value, int width, int height)
    {
      return new Size(value.Width + width, value.Height + height);
    }

    [DebuggerStepThrough]
    public static Size Deflate(this Size value, int width, int height)
    {
      int widthToSubtract = width;
      int heightToSubtract = height;

      if (widthToSubtract > value.Width)
        widthToSubtract = value.Width;

      if (heightToSubtract > value.Height)
        heightToSubtract = value.Height;

      return new Size(value.Width - widthToSubtract, value.Height - heightToSubtract);
    }

    [DebuggerStepThrough]
    public static PointF ShiftRight(this PointF value, float offset)
    {
      value.X += offset;
      return value;
    }

    [DebuggerStepThrough]
    public static PointF ShiftDown(this PointF value, float offset)
    {
      value.Y += offset;
      return value;
    }

    [DebuggerStepThrough]
    public static float ToFloat(this string value)
    {
      if (value == null)
        return 0F;

      if (value.Trim().Length == 0)
        return 0F;

      string valueNoDecimals = value.Replace(".", String.Empty).Trim();

      int signMultiplier = 1;
      int minusSigns = valueNoDecimals.CountOfChar('-');
      if (minusSigns == 1)
      {
        if (valueNoDecimals.StartsWith("-") || valueNoDecimals.EndsWith("-"))
        {
          valueNoDecimals = valueNoDecimals.Replace("-", String.Empty);
          signMultiplier = -1;
        }
      }

      if (valueNoDecimals.IsNotNumeric())
        return 0F;

      if (value.CountOfChar('.') > 1)
        return 0F;

      string noSignValue = value.Replace("-", String.Empty);

      return (float)decimal.Parse(noSignValue) * signMultiplier;
    }

    [DebuggerStepThrough]
    public static decimal ToDecimal(this string value)
    {
      if (value == null)
        return 0M;

      if (value.Trim().Length == 0)
        return 0M;

      string valueNoDecimals = value.Replace(".", String.Empty).Trim().Replace(",", String.Empty);

      int signMultiplier = 1;
      int minusSigns = valueNoDecimals.CountOfChar('-');
      if (minusSigns == 1)
      {
        if (valueNoDecimals.StartsWith("-") || valueNoDecimals.EndsWith("-"))
        {
          valueNoDecimals = valueNoDecimals.Replace("-", String.Empty);
          signMultiplier = -1;
        }
      }

      if (valueNoDecimals.IsNotNumeric())
        return 0M;

      if (value.CountOfChar('.') > 1)
        return 0M;

      string noSignValue = value.Replace("-", String.Empty);

      return decimal.Parse(noSignValue) * signMultiplier;
    }

    [DebuggerStepThrough]
    public static decimal ToDecimal(this object value)
    {
      if (value == null)
        return 0M;

      try
      {
        return Convert.ToDecimal(value);
      }
      catch
      {
        return 0M;
      }
    }

    [DebuggerStepThrough]
    public static decimal? ToNullableDecimal(this object value)
    {
      if (value == null)
        return (decimal?)null;

      try
      {
        return Convert.ToDecimal(value);
      }
      catch
      {
        return (decimal?)null;
      }
    }

    [DebuggerStepThrough]
    public static float ToFloat(this object value)
    {
      if (value == null)
        return 0F;

      try
      {
        return Convert.ToSingle(value);
      }
      catch
      {
        return 0F;
      }
    }

    [DebuggerStepThrough]
    public static DateTime ToDateTime(this object value, bool useMMYYYYFormat = false)
    {
      if (value == null)
        return DateTime.MinValue;

      string dt = value.ToString().Trim();

      int month = 0;
      int day = 0;
      int year = 0;

      if (dt.IsValidMMDDYY() && !useMMYYYYFormat)
      {
        month = dt.Substring(0, 2).ToInt32();
        day = dt.Substring(2, 2).ToInt32();
        year = dt.Substring(4, 2).ToInt32();
        if (year < 50)
          year = 2000 + year;
        else
          year = 1900 + year;
        return new DateTime(year, month, day);
      }

      if (dt.IsValidMMYYYY())
      {
        month = dt.Substring(0, 2).ToInt32();
        year = dt.Substring(2, 4).ToInt32();
        day = 1;
        return new DateTime(year, month, day);
      }

      if (dt.IsValidCCYYMMDD())
      {
        year = dt.Substring(0, 4).ToInt32();
        month = dt.Substring(4, 2).ToInt32();
        day = dt.Substring(6, 2).ToInt32();
        return new DateTime(year, month, day);
      }

      if (dt.IsValidMMDDCCYY())
      {
        month = dt.Substring(0, 2).ToInt32();
        day = dt.Substring(2, 2).ToInt32();
        year = dt.Substring(4, 4).ToInt32();
        return new DateTime(year, month, day);
      }

      try
      {
        return Convert.ToDateTime(value);
      }
      catch
      {
        return DateTime.MinValue;
      }
    }

    public static bool InIntegerRange(this string s, int lowValue, int highValue)
    {
      if (s.IsBlank() || s.IsNotNumeric())
        return false;

      int n = s.ToInt32();

      if (n < lowValue || n > highValue)
        return false;

      return true;
    }

    public static bool InIntegerRange(this int n, int lowValue, int highValue)
    {
      if (n < lowValue || n > highValue)
        return false;

      return true;
    }

    public static string[] RemoveItemAt(this string[] s, int index)
    {
      if (s == null)
        return new string[0];

      if (index > s.Length - 1)
        return s;

      int newLength = s.Length - 1;
      string[] newArray = new string[newLength];

      int j = 0;
      for (int i = 0; i < s.Length; i++)
      {
        if (i != index)
          newArray[j++] = s[i];
      }

      return newArray;
    }

    public static string[] RemoveItem(this string[] s, string removeValue)
    {
      if (s == null)
        return new string[0];

      if (removeValue.IsBlank())
        return s;

      int countToRemove = s.Where(v => v == removeValue).Count();

      if (countToRemove == 0)
        return s;

      int newLength = s.Length - countToRemove;
      if (newLength == 0)
        return new string[0];

      string[] newArray = new string[newLength];

      int j = 0;
      for (int i = 0; i < s.Length; i++)
      {
        if (s[i] != removeValue)
          newArray[j++] = s[i];
      }

      return newArray;
    }

    [DebuggerStepThrough]
    public static string[] ToLower(this string[] array)
    {
      if (array == null)
        return null;

      string[] newArray = new string[array.Length];
      for (int i = 0; i < array.Length; i++)
        newArray[i] = array[i].ToLower();

      return newArray;
    }

    [DebuggerStepThrough]
    public static string[] ToUpper(this string[] array)
    {
      if (array == null)
        return null;

      string[] newArray = new string[array.Length];
      for (int i = 0; i < array.Length; i++)
        newArray[i] = array[i].ToUpper();

      return newArray;
    }

    public static string JoinRemainingTokens(this string[] s, string separator, string beforeToken)
    {
      if (s == null || s.Length == 0)
        return String.Empty;

      // This method joins the the tokens in the array "s" together into a single string with the specified separator between them.

      // If the "beforeToken" value is non-blank, it will contain a token or a pipe-delimited set of tokens
      // that, if found in the "s" array, will exclude those tokens from the joined result. This is essentially
      // to join the tokens in the "s" array "before" the token or tokens in the "beforeToken" value.

      int beforeTokensIndex = -1;

      if (beforeToken.IsNotBlank())
      {
        string[] beforeTokens = beforeToken.Split(Constants.PipeDelimiter, StringSplitOptions.RemoveEmptyEntries);
        beforeTokensIndex = s.GetIndexOfToken(beforeTokens);

        if (beforeTokensIndex == -1)
          return String.Empty;
      }

      if (beforeTokensIndex == -1)
        beforeTokensIndex = s.Length;

      string joinedTokens = String.Empty;
      for (int i = 0; i < beforeTokensIndex; i++)
      {
        string tokenValue = s[i].Trim();

        if (joinedTokens.Length > 0)
          joinedTokens += separator + tokenValue;
        else
          joinedTokens = tokenValue;
      }

      return joinedTokens;
    }

    public static int GetIndexOfToken(this string[] s, string[] matchTokens)
    {
      // this method will return the index of the first token in the "s" array when there is a case-insensitive match
      // between the "match tokens" and corresponding consecutive tokens in the "s " array.

      // EXAMPLE
      // INDEX              0   1   2   3   4   5   6   7   8   9
      // "s" array          A   B   C   A   B   C   D   E   F   G
      // matchToken array                       C   D
      // return value will be "5"

      if (s == null || s.Length == 0 || matchTokens == null || matchTokens.Length == 0 || matchTokens.Length > s.Length)
        return -1;

      // This array is used to keep track of which token in the "s" array to use when
      // we need to "start over" attempting to get a match.  This is used after matching on
      // "some" elements in the matchTokens array, but not "all" elements which means we need
      // to start over, but not all the way "to the left".
      int[] mLvl = new int[matchTokens.Length];
      for (int i = 0; i < mLvl.Length; i++)
      {
        mLvl[i] = 0;
      }

      int sPtr = 0;                   // pointer for "s" array
      int mPtr = 0;                   // pointer for "matchToken" array
      int mLevel = 0;                 // level within the "matchToken" array
      int loopCount = 0;
      int maxLoopCount = 1000;

      while (true)
      {
        // endless loop prevention
        loopCount++;
        if (loopCount >= maxLoopCount)
          return -1;

        if (mPtr > matchTokens.Length - 1 || sPtr > s.Length - 1)
          return -1;

        string mToken = matchTokens[mPtr].ToLower().Trim();
        string sToken = s[sPtr].ToLower().Trim();

        if (sToken == mToken)
        {
          if (mLevel == mLvl.Length - 1)
            return sPtr - (mLvl.Length - 1);

          mLvl[mLevel] = sPtr + 1;
          mLevel++;
          for (int i = mLevel; i < mLvl.Length; i++)
            mLvl[i] = sPtr + 2;
          sPtr++;
          mPtr++;
        }
        else
        {
          // if no match and not on the first match token
          // go back to the first match token and to the
          // first sToken beyond the most recent first token match

          if (mLevel > 0)
          {
            mLevel = 0;
            mPtr = 0;
            sPtr = mLvl[mLevel];
          }
          else
          {
            sPtr++;
          }
        }
      }
    }

    [DebuggerStepThrough]
    public static DateTime? ToNullableDateTime(this object value)
    {
      if (value == null)
        return (DateTime?)null;

      try
      {
        return Convert.ToDateTime(value);
      }
      catch
      {
        return (DateTime?)null;
      }
    }

    [DebuggerStepThrough]
    public static DateTime CCYYMMToDateTime(this int value)
    {
      string ccyymm = value.ToString();

      if (ccyymm.Length != 6)
        return DateTime.MinValue;

      try
      {
        int year = ccyymm.Substring(0, 4).ToInt32();
        int month = ccyymm.Substring(4, 2).ToInt32();
        DateTime dt = new DateTime(year, month, 1);
        return dt;
      }
      catch
      {
        return DateTime.MinValue;
      }
    }

    ////[DebuggerStepThrough]
    //public static int CCYYMMToNextMonth(this int value)
    //{
    //  string ccyymm = value.ToString();

    //  if (ccyymm.Length != 6)
    //    return DateTime.MinValue;

    //  try
    //  {
    //    int year = ccyymm.Substring(0, 4).ToInt32();
    //    int month = ccyymm.Substring(4, 2).ToInt32();
    //    DateTime dt = new DateTime(year, month, 1);
    //    return dt;
    //  }
    //  catch
    //  {
    //    return DateTime.MinValue;
    //  }
    //}

    public static TimeSpan? ToTimeSpan(this object value)
    {
      if (value == null)
        return (TimeSpan?)null;

      return (TimeSpan?)value;
    }

    [DebuggerStepThrough]
    public static long ToTimeStampInt64(this DateTime value)
    {
      return Convert.ToInt64(value.ToString("yyyyMMddHHmmss"));
    }

    [DebuggerStepThrough]
    public static int ToInt32(this object value)
    {
      if (value == null)
        return 0;

      try
      {
        return Convert.ToInt32(value);
      }
      catch
      {
        return 0;
      }
    }

    [DebuggerStepThrough]
    public static int CCYYMMToNextMonth(this int value)
    {
      if (value < 195000 || value > 210000)
        return -1;
      int month = value % 100;
      int c = value / 100;
      if (month > 12)
        return -1;
      if (month == 12)
      {
        month = 1;
        int ccyy = (int)(value / 100) + 1;
        value = (ccyy * 100) + 1;
        int nvalue = int.Parse(ccyy.ToString() + month.ToString("00"));
        return nvalue;
      }
      month = month + 1;
      int newvalue = int.Parse(c.ToString() + month.ToString("00"));

      return newvalue;
    }

    [DebuggerStepThrough]
    public static int CCYYMMToPreviousMonth(this int value)
    {
      if (value < 195000 || value > 210000)
        return -1;
      int month = value % 100;
      int c = value / 100;
      if (month > 12)
        return -1;
      if (month == 1)
      {
        month = 12;
        int ccyy = (int)(value / 100) - 1;
        int nvalue = int.Parse(ccyy.ToString() + month.ToString("00"));
        return nvalue;
      }
      month = month - 1;
      int newvalue = int.Parse(c.ToString() + month.ToString("00"));

      return newvalue;
    }


    [DebuggerStepThrough]
    public static Int64 ToInt64(this object value)
    {
      if (value == null)
        return 0;

      try
      {
        return Convert.ToInt64(value);
      }
      catch
      {
        return 0;
      }
    }

    [DebuggerStepThrough]
    public static int CountOfChar(this string value, char ch)
    {
      if (value == null)
        return 0;

      if (value.Trim().Length == 0)
        return 0;

      int count = 0;

      foreach (Char c in value)
      {
        if (c == ch)
          count++;
      }

      return count;
    }

    [DebuggerStepThrough]
    public static int[] ToIntArray(this string value)
    {
      if (value == null)
        return null;

      if (value.Trim().Length == 0)
        return new int[0];

      int[] tokens = value.ToTokenArrayInt32(Constants.CommaDelimiter);

      return tokens;
    }

    [DebuggerStepThrough]
    public static string ToAlphaOnly(this string value)
    {
      if (value == null)
        return String.Empty;

      if (value.Trim().Length == 0)
        return String.Empty;

      StringBuilder sb = new StringBuilder();

      foreach (Char c in value)
      {
        if (Char.IsLetter(c))
          sb.Append(c);
      }

      return sb.ToString();
    }

    [DebuggerStepThrough]
    public static float ToPointSize(this float value)
    {
      return value / 2;
    }

    [DebuggerStepThrough]
    public static Rectangle ToRectangle(this RectangleF value)
    {
      if (value == null)
        return new Rectangle();

      return new Rectangle(value.X.RoundToInt(), value.Y.RoundToInt(), value.Width.RoundToInt(), value.Height.RoundToInt());
    }

    [DebuggerStepThrough]
    public static Point ToPoint(this PointF value)
    {
      if (value == null)
        return new Point();

      return new Point(value.X.RoundToInt(), value.Y.RoundToInt());
    }

    [DebuggerStepThrough]
    public static int RoundToInt(this float value)
    {
      return System.Math.Round(value, 0).ToInt32();
    }

    [DebuggerStepThrough]
    public static int ToInt32(this float value)
    {
      return (int)value;
    }

    //[DebuggerStepThrough]
    public static bool ToBoolean(this string value)
    {
      if (value == null)
        return false;

      value = value.Trim().ToLower();

      if (value.Trim().Length == 0)
        return false;

      if (value.In("t, true, 1, y, yes"))
        return true;

      return false;
    }

    [DebuggerStepThrough]
    public static bool ToBoolean(this string value, bool defaultValue)
    {
      if (value == null)
        return defaultValue;

      value = value.Trim().ToLower();

      if (value.Trim().Length == 0)
        return defaultValue;

      if (value.In("t, true, 1, y, yes"))
        return true;

      if (value.In("f, false, 0, n, no"))
        return false;

      return defaultValue;
    }

    [DebuggerStepThrough]
    public static UInt32 ToUInt32(this string value)
    {
      if (value == null)
        return 0;

      value = value.Trim().ToLower();

      if (value.Trim().Length == 0)
        return 0;

      if (!value.IsNumeric())
        return 0;

      return UInt32.Parse(value);
    }

    [DebuggerStepThrough]
    public static UInt16 ToUInt16(this string value)
    {
      if (value == null)
        return 0;

      value = value.Trim().ToLower();

      if (value.Trim().Length == 0)
        return 0;

      if (!value.IsNumeric())
        return 0;

      return UInt16.Parse(value);
    }

    [DebuggerStepThrough]
    public static UInt64 ToUInt64(this string value)
    {
      if (value == null)
        return 0;

      value = value.Trim().ToLower();

      if (value.Trim().Length == 0)
        return 0;

      if (!value.IsNumeric())
        return 0;

      return UInt64.Parse(value);
    }

    [DebuggerStepThrough]
    public static bool StringStartsWith(this string value, string set)
    {
      if (value == null || set == null)
        return false;

      set = set.Trim();

      if (set.Trim().Length == 0)
        return false;

      string[] tokens = set.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);

      foreach (string token in tokens)
      {
        if (value.StartsWith(token.Trim()))
          return true;
      }

      return false;
    }

    [DebuggerStepThrough]
    public static bool WrappedWith(this string value, string start, string end)
    {
      if (value == null)
        return false;

      value = value.Trim();

      if (value.Trim().Length == 0)
        return false;

      return value.StartsWith(start) && value.EndsWith(end);
    }

    [DebuggerStepThrough]
    public static string[] ToXmlStringArray(this XElement xml)
    {
      string xmlString = xml.ToString();
      string[] array = xmlString.Split("\r\n".ToCharArray());
      var temp = new List<string>();
      foreach (var c in array)
      {
        if (!string.IsNullOrEmpty(c))
          temp.Add(c);
      }
      array = temp.ToArray();

      return array;
    }

    [DebuggerStepThrough]
    public static string RemoveChars(this string value, string chars)
    {
      if (value == null)
        return String.Empty;

      chars = chars.Trim();

      if (chars.Trim().Length == 0)
        return value;

      foreach (char c in chars)
      {
        value = value.Replace(c.ToString(), String.Empty);
        if (value.Length == 0)
          return String.Empty;
      }

      return value;
    }

    //[DebuggerStepThrough]
    public static bool In(this string value, string set, bool caseSensitive = true)
    {
      if (value == null || set == null)
        return false;

      value = value.Trim();
      set = set.Trim();

      if (set.Trim().Length == 0)
        return false;

      string[] tokens = set.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);

      foreach (string token in tokens)
      {
        if (caseSensitive)
        {
          if (value == token.Trim())
            return true;
        }
        else
        {
          if (value.ToLower() == token.ToLower().Trim())
            return true;
        }
      }

      return false;
    }

    //[DebuggerStepThrough]
    public static bool StartsWithIn(this string value, string set, bool caseSensitive = true)
    {
      if (value == null || set == null)
        return false;

      value = value.Trim();
      set = set.Trim();

      if (set.Trim().Length == 0)
        return false;

      string[] tokens = set.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);

      foreach (string token in tokens)
      {
        if (caseSensitive)
        {
          if (value.StartsWith(token.Trim()))
            return true;
        }
        else
        {
          if (value.ToLower().StartsWith(token.Trim().ToLower()))
            return true;
        }
      }

      return false;
    }

    //[DebuggerStepThrough]
    public static bool EndsWithIn(this string value, string set, bool caseSensitive = true)
    {
      if (value == null || set == null)
        return false;

      value = value.Trim();
      set = set.Trim();

      if (set.Trim().Length == 0)
        return false;

      string[] tokens = set.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);

      foreach (string token in tokens)
      {
        if (caseSensitive)
        {
          if (value.EndsWith(token.Trim()))
            return true;
        }
        else
        {
          if (value.ToLower().EndsWith(token.Trim().ToLower()))
            return true;
        }
      }

      return false;
    }

    [DebuggerStepThrough]
    public static bool ToListContains(this string value, char[] delim, string containsValue, bool caseSensitive = false)
    {
      if (value.IsBlank() || containsValue.IsBlank() || delim == null)
        return false;

      string thisValue = value;

      if (!caseSensitive)
      {
        thisValue = value.ToLower();
        containsValue = containsValue.ToLower();
      }

      var tokens = thisValue.Split(delim, StringSplitOptions.RemoveEmptyEntries).ToList();
      if (tokens.Count == 0)
        return false;

      return tokens.Contains(containsValue);
    }

    [DebuggerStepThrough]
    public static bool ContainsAll<T>(this IEnumerable<T> source, IEnumerable<T> values)
    {
      if (source == null || values == null)
        return false;

      return !values.Except(source).Any();
    }

    public static bool Between(this int value, int lowInt, int highInt)
    {
      if (value < lowInt || value > highInt)
        return false;

      return true;
    }

    [DebuggerStepThrough]
    public static bool In(this int value, string set)
    {
      if (set == null)
        return false;

      set = set.Trim();

      if (set.Trim().Length == 0)
        return false;

      string[] tokens = set.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);

      foreach (string token in tokens)
      {
        if (token.Trim().IsNumeric())
        {
          if (value == token.Trim().ToInt32())
            return true;
        }
      }

      return false;
    }

    [DebuggerStepThrough]
    public static bool In(this char c, string chars)
    {
      if (chars.IsBlank())
        return false;

      return chars.IndexOf(c) > -1;
    }

    [DebuggerStepThrough]
    public static bool In(this int value, List<int> set)
    {
      if (set == null)
        return false;

      return set.Contains(value);
    }

    [DebuggerStepThrough]
    public static string Int32ListToString(this List<int> intList)
    {
      if (intList == null)
        return String.Empty;

      if (intList.Count == 0)
        return String.Empty;

      StringBuilder sb = new StringBuilder();
      foreach (var i in intList)
      {
        if (sb.Length == 0)
          sb.Append(i.ToString());
        else
          sb.Append("," + i.ToString());
      }

      return sb.ToString();
    }

    [DebuggerStepThrough]
    public static int LastIndex(this string value)
    {
      if (value == null)
        return -1;

      return value.Length - 1;
    }

    [DebuggerStepThrough]
    public static bool IsBlankOrNewLine(this char c)
    {
      if (c == null)
        return false;

      if (c == ' ' || c == '\n')
        return true;

      return false;
    }

    [DebuggerStepThrough]
    public static bool IsNumeric(this string value)
    {
      if (value == null)
        return false;

      if (value.Trim().Length == 0)
        return false;

      foreach (Char c in value)
        if (!Char.IsNumber(c))
          return false;

      return true;
    }

    //[DebuggerStepThrough]
    public static bool IsValidIntOrDecimal(this string value)
    {
      if (value.IsValidInteger())
        return true;

      if (value.IsFloat(false))
        return true;

      return false;
    }

    [DebuggerStepThrough]
    public static bool IsAlphabetic(this string value)
    {
      if (value == null)
        return false;

      if (value.Trim().Length == 0)
        return false;

      foreach (Char c in value)
        if (!Char.IsLetter(c))
          return false;

      return true;
    }

    [DebuggerStepThrough]
    public static bool IsAlphaNumeric(this string value)
    {
      if (value == null)
        return false;

      if (value.Trim().Length == 0)
        return false;

      foreach (Char c in value)
        if (!Char.IsLetter(c) && !Char.IsDigit(c))
          return false;

      return true;
    }

    [DebuggerStepThrough]
    public static bool IsValidNumeric(this string value)
    {
      if (value == null)
        return false;

      if (value.Trim().Length == 0)
        return false;

      int periodCount = value.CharCount('.');
      if (periodCount > 1)
        return false;

      int minusCount = value.CharCount('-');
      if (minusCount > 1)
        return false;

      if (minusCount == 1)
      {
        if (!value.StartsWith("-") && !value.EndsWith("-"))
          return false;
        value = value.Replace("-", String.Empty);
      }

      foreach (Char c in value)
      {
        if (!Char.IsNumber(c) && c != ',' && c != '.')
          return false;
      }

      return true;
    }

    [DebuggerStepThrough]
    public static DateTime FirstDayOfMonth(this DateTime value)
    {
      if (value.Day == 1)
        return value;

      return new DateTime(value.Year, value.Month, 1);
    }

    [DebuggerStepThrough]
    public static int LastDayOfMonth(this DateTime value)
    {
      DateTime firstOfCurrentMonth = new DateTime(value.Year, value.Month, 1);
      DateTime lastDateOfCurrentMonth = firstOfCurrentMonth.AddMonths(1).AddDays(-1);
      return lastDateOfCurrentMonth.Day;
    }

    [DebuggerStepThrough]
    public static DateTime EndOfLastDateOfMonth(this DateTime value)
    {
      DateTime firstOfCurrentMonth = new DateTime(value.Year, value.Month, 1);
      DateTime lastDateOfCurrentMonth = firstOfCurrentMonth.AddMonths(1).AddDays(-1);
      DateTime endOfDay = lastDateOfCurrentMonth.AddMilliseconds(86399999);
      return endOfDay;
    }

    [DebuggerStepThrough]
    public static DateTime FirstDayOfNextMonth(this DateTime value)
    {
      value = value.AddMonths(1);

      if (value.Day == 1)
        return value;

      return new DateTime(value.Year, value.Month, 1);
    }

    [DebuggerStepThrough]
    public static string ToDateFmt(this DateTime? value, string formatSpecifier)
    {
      if (value == null || !value.HasValue)
        return string.Empty;

      DateTime d = value.Value;

      return d.ToString(formatSpecifier);
    }

    //[DebuggerStepThrough]
    public static string ToCCYYMMDD(this DateTime value)
    {
      return value.ToString("yyyyMMdd");
    }

    [DebuggerStepThrough]
    public static int ToCCYYMM(this DateTime value)
    {
      return (value.Year * 100) + value.Month;
    }

    [DebuggerStepThrough]
    public static DateTime Date(this DateTime value)
    {
      return new DateTime(value.Year, value.Month, value.Day);
    }

    [DebuggerStepThrough]
    public static string ToMMDDYYHHMM(this DateTime value)
    {
      return value.ToString("MMddyy HHmm");
    }

    [DebuggerStepThrough]
    public static string ToCCYYMMDDHHMMSS(this DateTime value)
    {
      return value.ToString("yyyyMMddHHmmss");
    }

    [DebuggerStepThrough]
    public static bool IsMinValue(this DateTime value)
    {
      return value == DateTime.MinValue;
    }

    [DebuggerStepThrough]
    public static bool IsNotMinValue(this DateTime value)
    {
      return value != DateTime.MinValue;
    }

    [DebuggerStepThrough]
    public static bool IsIPV4Address(this string value)
    {
      if (value == null)
        return false;

      value = value.Trim();

      if (value.Length == 0)
        return false;

      if (value.CountOfChar('.') != 3)
        return false;

      string[] tokens = value.Split('.');

      if (tokens.Length != 4)
        return false;

      foreach (string token in tokens)
      {
        if (token.IsNotNumeric())
          return false;

        int octet = token.ToInt32();

        if (octet < 0 || octet > 255)
          return false;
      }

      return true;
    }

    [DebuggerStepThrough]
    public static bool IsInteger(this string value)
    {
      string trimmedValue = value == null ? String.Empty : value.Trim();

      if (trimmedValue.IsBlank())
        return false;

      if (trimmedValue.StartsWith("(") && trimmedValue.EndsWith(")"))
      {
        trimmedValue = trimmedValue.Replace("(", String.Empty).Replace(")", String.Empty);
      }

      trimmedValue = trimmedValue.Replace("-", String.Empty);
      trimmedValue = trimmedValue.Replace("+", String.Empty);

      if (trimmedValue.IsBlank())
        return false;

      foreach (char c in trimmedValue)
        if (!Char.IsNumber(c))
          return false;

      return true;
    }

    [DebuggerStepThrough]
    public static bool IsNotInteger(this string value)
    {
      return !value.IsInteger();
    }

    [DebuggerStepThrough]
    public static void LoadItems(this ComboBox cbo, List<string> itemsToLoad, bool loadBlankItemAtTop = false)
    {
      if (cbo == null)
        return;

      cbo.Items.Clear();

      if (itemsToLoad == null || itemsToLoad.Count == 0)
        return;

      if (loadBlankItemAtTop)
        cbo.Items.Add("");

      foreach (var item in itemsToLoad)
      {
        cbo.Items.Add(item);
      }
    }

    [DebuggerStepThrough]
    public static void LoadItems(this ComboBox cbo, IEnumerable<KeyValuePair<int, string>> itemsToLoad, bool loadBlankItemAtTop = false)
    {
      if (cbo == null)
        return;

      cbo.DataSource = null;
      cbo.DisplayMember = null;
      cbo.ValueMember = null;

      if (itemsToLoad == null || itemsToLoad.Count() == 0)
        return;

      var dict = new Dictionary<int, string>();

      if (loadBlankItemAtTop)
        dict.Add(-1, "");

      foreach (var item in itemsToLoad)
        dict.Add(item.Key, item.Value);

      cbo.DataSource = new BindingSource(dict, null);
      cbo.DisplayMember = "Value";
      cbo.ValueMember = "Key";
    }

    //[DebuggerStepThrough]
    public static void SelectItem(this ComboBox cbo, string itemToSelect)
    {
      if (cbo == null || itemToSelect == null)
        return;

      for (int i = 0; i < cbo.Items.Count; i++)
      {
        if (cbo.Items[i].ToString() == itemToSelect)
        {
          cbo.SelectedIndex = i;
          return;
        }
      }
    }

    public static List<string> ToList(this Dictionary<string, string> dict, DictionaryPart dictionaryPart, bool suppressDuplicates = false)
    {
      var list = new List<string>();

      if (dict == null || dict.Count == 0)
        return list;

      foreach (var kvp in dict)
      {
        if (dictionaryPart == DictionaryPart.Key)
        {
          if (list.Contains(kvp.Key) && suppressDuplicates)
            continue;
          list.Add(kvp.Key);
        }
        else
        {
          if (list.Contains(kvp.Value) && suppressDuplicates)
            continue;
          list.Add(kvp.Value);
        }
      }

      return list;
    }

    public static List<string> ToList(this SortedList<string, string> dict, DictionaryPart dictionaryPart, bool suppressDuplicates = false)
    {
      var list = new List<string>();

      if (dict == null || dict.Count == 0)
        return list;

      foreach (var kvp in dict)
      {
        if (dictionaryPart == DictionaryPart.Key)
        {
          if (list.Contains(kvp.Key) && suppressDuplicates)
            continue;
          list.Add(kvp.Key);
        }
        else
        {
          if (list.Contains(kvp.Value) && suppressDuplicates)
            continue;
          list.Add(kvp.Value);
        }
      }

      return list;
    }

    public static List<string> ValuesToList(this SortedList<int, string> dict, bool suppressDuplicates = false)
    {
      var list = new List<string>();

      if (dict == null || dict.Count == 0)
        return list;

      foreach (var kvp in dict)
      {

        if (list.Contains(kvp.Value) && suppressDuplicates)
          continue;
        list.Add(kvp.Value);
      }

      return list;
    }

    public static bool IsNullableType(this Type value)
    {
      string typeName = value.Name;

      if (typeName.Contains("Nullable"))
        return true;

      return false;
    }

    [DebuggerStepThrough]
    public static bool IsValidInteger(this string value)
    {
      string trimmedValue = value.Trim();

      if (trimmedValue.IsBlank())
        return false;

      if (trimmedValue.Contains("."))
        return false;

      if (trimmedValue.StartsWith("-"))
      {
        trimmedValue = trimmedValue.Substring(1);
      }

      if (trimmedValue.EndsWith("-"))
      {
        trimmedValue = trimmedValue.Substring(0, trimmedValue.Length - 1);
      }

      int commaCount = trimmedValue.CountOfChar(',');

      if (commaCount > 0)
      {
        if (trimmedValue.StartsWith(",") || trimmedValue.EndsWith(","))
          return false;

        string workValue = trimmedValue;
        int commaSearchPosition = workValue.Length - 1;

        int rightPos = workValue.Length;

        while (commaSearchPosition > 0)
        {
          int commaPosition = workValue.LastIndexOf(',', commaSearchPosition);
          if (commaPosition > -1)
            commaSearchPosition = commaPosition - 1;

          if (commaPosition == -1)
            break;

          int offset = rightPos - commaPosition;

          if (offset % 4 != 0)
            return false;
        }

        trimmedValue = trimmedValue.Replace(",", String.Empty);
      }

      foreach (char c in trimmedValue)
        if (!Char.IsNumber(c))
          return false;

      return true;
    }

    [DebuggerStepThrough]
    public static bool CheckIsDate(this string value)
    {
      try
      {
        DateTime dt = DateTime.Parse(value);
        return true;
      }
      catch
      {
        return false;
      }
    }

    [DebuggerStepThrough]
    public static bool IsDecimal(this string value, bool requirePeriod = false)
    {
      return value.IsFloat(requirePeriod);
    }

    [DebuggerStepThrough]
    public static bool IsString(this string value)
    {
      if (value == null)
        return false;

      if (value.Trim().Length == 0)
        return false;

      return true;
    }

    public static bool IsDecimalPointZero(this string value)
    {
      if (value == null)
        return false;

      value = value.ToString();

      var indexOfPeriod = value.IndexOf(".");
      string c = value.Substring(indexOfPeriod + 1);
      if (c.In("0, 00, 000, 0000, 00000, 000000, 0000000"))
        return true;
      else
        return false;
    }

    //[DebuggerStepThrough]
    public static bool IsFloat(this string value, bool requirePeriod = false)
    {
      if (value == null)
        return false;

      string trimmedValue = value.Trim();

      if (trimmedValue.Length == 0)
        return false;

      int dollarSignCount = trimmedValue.CountOfChar('$');
      if (dollarSignCount > 1)
        return false;

      if (dollarSignCount == 1)
      {
        if (trimmedValue.StartsWith("$"))
          trimmedValue = trimmedValue.Substring(1);
      }

      int percentCount = trimmedValue.CountOfChar('%');
      if (percentCount > 1)
        return false;

      if (percentCount == 1 && trimmedValue.EndsWith("%"))
        trimmedValue = trimmedValue.Substring(0, trimmedValue.Length - 1);

      int periodCount = trimmedValue.CountOfChar('.');

      bool isNegative = false;
      if (trimmedValue.StartsWith("(") && trimmedValue.EndsWith(")"))
      {
        isNegative = true;
        trimmedValue = trimmedValue.Replace("(", String.Empty).Replace(")", String.Empty);
      }

      if (requirePeriod && periodCount == 0)
        return false;

      if (periodCount > 1)
        return false;

      int periodPos = trimmedValue.IndexOf('.');

      int dashCount = trimmedValue.CountOfChar('-');
      if (dashCount > 1)
        return false;

      if (trimmedValue.StartsWith("-"))
      {
        trimmedValue = trimmedValue.Substring(1);
        isNegative = true;
      }

      if (trimmedValue.EndsWith("-"))
      {
        trimmedValue = trimmedValue.Substring(0, trimmedValue.Length - 1);
        isNegative = true;
      }

      if (trimmedValue.EndsWith("."))
        return false;

      int commaCount = trimmedValue.CountOfChar(',');

      if (commaCount > 0)
      {
        if (trimmedValue.StartsWith(",") || trimmedValue.EndsWith(","))
          return false;

        string workValue = trimmedValue;
        int commaSearchPosition = workValue.Length - 1;

        int rightPos = workValue.LastIndexOf('.');
        if (rightPos == -1)
          rightPos = workValue.Length;

        while (commaSearchPosition > 0)
        {
          int commaPosition = workValue.LastIndexOf(',', commaSearchPosition);
          if (commaPosition > -1)
            commaSearchPosition = commaPosition - 1;

          if (commaPosition == -1)
            break;

          int offset = rightPos - commaPosition;

          if (offset % 4 != 0)
            return false;
        }

        trimmedValue = trimmedValue.Replace(",", String.Empty);
      }

      bool result = trimmedValue.Replace(".", String.Empty).IsNumeric();

      return result;
    }

    public static string GetEntry(this string[] array, string valueToCheck, bool caseInsensitive = true)
    {
      if (array == null || array.Length == 0 || valueToCheck.IsBlank())
        return String.Empty;

      if (valueToCheck.Trim() == "*")
        return String.Empty;

      foreach (string value in array)
      {
        if (caseInsensitive)
        {
          if (valueToCheck.StartsWith("*"))
          {
            string checkValue = valueToCheck.Substring(1).Trim();
            if (value.ToLower().Trim().EndsWith(checkValue.ToLower()))
              return value;
          }
          else
          {
            if (valueToCheck.EndsWith("*"))
            {
              string checkValue = valueToCheck.Substring(0, valueToCheck.Length - 1).Trim();
              if (value.ToLower().Trim().StartsWith(checkValue.ToLower()))
                return value;
            }
            else
            {
              if (value.ToLower().Trim() == valueToCheck.ToLower().Trim())
                return value;
            }
          }
        }
        else
        {
          if (valueToCheck.StartsWith("*"))
          {
            string checkValue = valueToCheck.Substring(1).Trim();
            if (value.Trim().EndsWith(checkValue))
              return value;
          }
          else
          {
            if (valueToCheck.EndsWith("*"))
            {
              string checkValue = valueToCheck.Substring(0, valueToCheck.Length - 1).Trim();
              if (value.Trim().StartsWith(checkValue))
                return value;
            }
            else
            {
              if (value.Trim() == valueToCheck.Trim())
                return value;
            }
          }
        }
      }

      return String.Empty;
    }


    [DebuggerStepThrough]
    public static bool ContainsEntry(this string[] array, string valueToCheck, bool caseInsensitive = true)
    {
      if (array == null || array.Length == 0 || valueToCheck.IsBlank())
        return false;

      foreach (string value in array)
      {
        if (caseInsensitive)
        {
          if (value.ToLower().Trim() == valueToCheck.ToLower().Trim())
            return true;
        }
        else
        {
          if (value.Trim() == valueToCheck.Trim())
            return true;
        }
      }

      return false;
    }

    [DebuggerStepThrough]
    public static string[] RemoveEntry(this string[] array, string valueToRemove, bool caseInsensitive = true)
    {
      if (array == null || array.Length == 0 || valueToRemove.IsBlank())
        return array;

      int entryIndexToRemove = -1;

      for (int i = 0; i < array.Length; i++)
      {
        string value = array[i];
        if (caseInsensitive)
        {
          if (value.ToLower().Trim() == valueToRemove.ToLower().Trim())
          {
            entryIndexToRemove = i;
            break;
          }
        }
        else
        {
          entryIndexToRemove = i;
          break;
        }
      }

      if (entryIndexToRemove == -1)
        return array;

      var newArray = new string[array.Length - 1];
      int j = 0;

      for (int i = 0; i < array.Length; i++)
      {
        if (i != entryIndexToRemove)
          newArray[j++] = array[i];
      }

      return newArray;
    }

    [DebuggerStepThrough]
    public static void Clear(this string value)
    {
      value = String.Empty;
    }

    [DebuggerStepThrough]
    public static bool IsNotNumeric(this string value)
    {
      if (value == null)
        return true;

      if (value.Trim().Length == 0)
        return true;

      foreach (Char c in value)
        if (!Char.IsNumber(c))
          return true;

      return false;
    }

    //[DebuggerStepThrough]
    public static int ToInt32(this string value)
    {
      if (value == null)
        return 0;

      if (value.Trim().Length == 0)
        return 0;

      value = value.Trim();
      int negationValue = 1;

      if (value.StartsWith("-"))
      {
        value = value.Substring(1);
        negationValue = -1;
      }
      else
      {
        if (value.EndsWith("-"))
        {
          value = value.Substring(0, value.Length - 1);
          negationValue = -1;
        }
      }

      if (value.IsNotNumeric())
        return 0;

      return Int32.Parse(value) * negationValue;
    }

    public static int ExtractInteger(this string value)
    {
      if (value.IsBlank())
        return 0;

      string intChars = String.Empty;

      foreach (char c in value)
      {
        if (Char.IsDigit(c))
          intChars += c.ToString();
      }

      if (intChars.Length == 0)
        return 0;

      return intChars.ToInt32();
    }

    public static string[] ToRange(this string s)
    {
      // This extension method checks for a valid range of values separated by a hyphen and returns
      // a string array with the tokens on either side of the hyphen.  If no separating hyphen exists
      // a single element string array is returned with the original value.

      // If the method cannot separate the string into two values on either side of a hyphen
      // it will return the original string in the first element of a single element string array.

      string[] array = new string[2];

      if (s == null)
        return new string[0];

      if (s.Length == 0)
        return new string[] { s };

      string s2 = s.Trim();

      // Check for a double hyphen after index 1 which could indicate a range of negative numbers...
      // i.e., the value -3--1 would indicate a range from negative 3 to negative 1.
      int pos = s2.IndexOf("--", 1);
      if (pos > -1)
      {
        if (pos == s2.Length - 1)
          return new string[] { s };

        array[0] = s2.Substring(0, pos).Trim();
        array[1] = s2.Substring(pos + 1).Trim();
        return array;
      }

      pos = s2.IndexOf("-", 1);
      if (pos > -1)
      {
        if (pos == s2.Length - 1)
          return new string[] { s };

        array[0] = s2.Substring(0, pos).Trim();
        array[1] = s2.Substring(pos + 1).Trim();
        return array;
      }

      return new string[] { s };
    }

    public static int TrimToInt32(this string value)
    {
      var num = "";
      foreach (Char c in value)
        if (Char.IsNumber(c))
          num += c;

      return num.ToInt32();
    }

    [DebuggerStepThrough]
    public static string TrimLastChar(this string value)
    {
      if (value == null)
        return String.Empty;

      value = value.Trim();
      if (value.Length < 2)
        return String.Empty;

      return value.Substring(0, value.Length - 1);
    }

    [DebuggerStepThrough]
    public static int? ToNullableInt32(this object value)
    {
      if (value == null)
        return (int?) null;

      if (value.GetType() == typeof(System.DBNull))
        return (int?)null;

      string stringValue = value.ToString().Trim();

      int negationValue = 1;

      if (stringValue.StartsWith("-"))
      {
        stringValue = stringValue.Substring(1);
        negationValue = -1;
      }
      else
      {
        if (stringValue.EndsWith("-"))
        {
          stringValue = stringValue.Substring(0, stringValue.Length - 1);
          negationValue = -1;
        }
      }

      if (stringValue.IsNotNumeric())
        return 0;

      return (int?) Int32.Parse(stringValue) * negationValue;
    }

    [DebuggerStepThrough]
    public static bool ToBoolean(this object value)
    {
      if (value == null)
        return false;

      string stringValue = value.ToString().Trim().ToLower();

      switch (stringValue)
      {
        case "1":
          return true;
        case "y":
          return true;
        case "true":
          return true;
        case "yes":
          return true;
        case "t":
          return true;
      }

      return false;
    }

    [DebuggerStepThrough]
    public static bool IsBoolean(this string value)
    {
      if (value == null)
        return false;

      switch (value.ToLower().Trim())
      {
        case "0":
          return true;
        case "1":
          return true;
        case "y":
          return true;
        case "n":
          return true;
        case "true":
          return true;
        case "false":
          return true;
        case "t":
          return true;
        case "f":
          return true;
        case "yes":
          return true;
        case "no":
          return true;
      }

      return false;
    }

    [DebuggerStepThrough]
    public static int ToInt32OrDefault(this string value, int defaultValue)
    {
      if (value == null)
        return defaultValue;

      if (value.Trim().Length == 0)
        return defaultValue;

      if (value.IsNotNumeric())
        return defaultValue;

      return Int32.Parse(value);
    }

    [DebuggerStepThrough]
    public static string OrDefault(this string value, string defaultValue)
    {
      if (value.IsBlank())
        return defaultValue;

      return value;
    }

    [DebuggerStepThrough]
    public static Size Shrink(this Size value, int width, int height)
    {
      if (width > value.Width)
        width = value.Width;

      if (height > value.Height)
        height = value.Height;

      return new Size(value.Width - width, value.Height - height);
    }

    [DebuggerStepThrough]
    public static Size Grow(this Size value, int width, int height)
    {
      if (width < 0)
      {
        if (System.Math.Abs(width) > value.Width)
          width = value.Width * -1;
      }

      if (height < 0)
      {
        if (System.Math.Abs(height) > value.Height)
          height = value.Height * -1;
      }

      return new Size(value.Width + width, value.Height + height);
    }

    [DebuggerStepThrough]
    public static Point Move(this Point value, int x, int y)
    {
      return new Point(value.X + x, value.Y + y);
    }

    [DebuggerStepThrough]
    public static int ToInt32OrDefault(this object value, int defaultValue)
    {
      if (value == null)
        return defaultValue;

      return value.ToInt32();
    }

    [DebuggerStepThrough]
    public static string ObjectToTrimmedStringOrNull(this object value)
    {
      if (value == null)
        return null;

      return value.ToString().Trim();
    }

    [DebuggerStepThrough]
    public static string ObjectToTrimmedString(this object value)
    {
      if (value == null)
        return String.Empty;

      return value.ToString().Trim();
    }

    [DebuggerStepThrough]
    public static bool IsBlank(this string value)
    {
      if (value == null)
        return true;

      if (value.Trim().Length == 0)
        return true;

      return false;
    }

    public static bool ContainsNoDigits(this string s)
    {
      if (s == null || s.Trim().Length == 0)
        return true;

      foreach (char c in s)
      {
        if (Char.IsDigit(c))
          return false;
      }

      return true;
    }

    [DebuggerStepThrough]
    public static bool IsValidXml(this string xml)
    {
      if (xml.IsBlank())
        return false;

      try
      {
        XElement.Parse(xml);
        return true;
      }
      catch
      {
        return false;
      }
    }

    [DebuggerStepThrough]
    public static XElement ToXElement(this string xml)
    {
      if (xml.IsBlank())
        return null;

      try
      {
        var xElement = XElement.Parse(xml);
        return xElement;
      }
      catch
      {
        return null;
      }
    }

    //[DebuggerStepThrough]
    public static string Expanded(this XElement xml)
    {
      if (xml == null)
        return null;

      string elementValue = xml.Value.Trim();

      StringBuilder sb = new StringBuilder();

      sb.Append("<" + xml.Name.LocalName);

      var attrs = xml.Attributes();

      if (attrs.Count() == 0)
      {
        sb.Append(">");
      }
      else
      {
        foreach (var attr in attrs)
        {
          sb.Append(" " + attr.Name.LocalName + "=\"" + attr.Value + "\"");
        }
        sb.Append(" >");
      }

      if (elementValue.IsNotBlank() && xml.Elements().Count() == 0)
      {
        sb.Append(elementValue.Trim() + "<" + xml.Name.LocalName + "/>");
      }

      return sb.ToString();
    }

    [DebuggerStepThrough]
    public static string ConvertWhiteSpaceToBlanks(this string s)
    {
      if (s == null)
        return String.Empty;

      return s.Replace('\n', ' ').Replace('\t', ' ').Replace('\n', ' ');
    }

    [DebuggerStepThrough]
    public static int MonthToMonthNumber(this string monthString)
    {
      if (monthString.IsBlank())
        throw new Exception("The month string value passed to the MonthToMonthNumber extension method is " + (monthString == null ? "null" : "blank") + ".");

      string month = monthString.Trim().ToLower();

      switch (month)
      {
        case "january":
        case "jan":
          return 1;
        case "february":
        case "feb":
          return 2;
        case "march":
        case "mar":
          return 3;
        case "april":
        case "apr":
          return 4;
        case "may":
          return 5;
        case "june":
        case "jun":
          return 6;
        case "july":
        case "jul":
          return 7;
        case "august":
        case "aug":
          return 8;
        case "september":
        case "sep":
          return 9;
        case "october":
        case "oct":
          return 10;
        case "november":
        case "nov":
          return 11;
        case "december":
        case "dec":
          return 12;
      }

      throw new Exception("The month string value passed to the MonthToMonthNumber extension method is invalid: " + monthString + ".");
    }

    [DebuggerStepThrough]
    public static bool IsMonthString(this string value)
    {
      if (value.IsBlank())
        return false;

      value = value.ToLower().Trim();

      switch (value)
      {
        case "january":
        case "jan":
        case "february":
        case "feb":
        case "march":
        case "mar":
        case "april":
        case "apr":
        case "may":
        case "june":
        case "jun":
        case "july":
        case "jul":
        case "august":
        case "aug":
        case "september":
        case "sep":
        case "october":
        case "oct":
        case "november":
        case "nov":
        case "december":
        case "dec":
          return true;
      }

      return false;
    }

    [DebuggerStepThrough]
    public static bool ContainsAlphaChar(this string value)
    {
      if (value == null)
        return false;

      if (value.Trim().Length == 0)
        return false;

      foreach (char c in value)
        if (Char.IsLetter(c))
          return true;

      return false;
    }

    [DebuggerStepThrough]
    public static string CondenseText(this string value, bool omitQuotedText = false)
    {
      if (value == null)
        return String.Empty;

      if (value.Length == 0)
        return String.Empty;

      string holdValue = value;

      var sb = new StringBuilder();

      bool inQuotes = false;
      char quoteType = ' ';

      foreach (Char c in value)
      {
        if (c == '"' || c == '\'')
        {
          if (inQuotes)
          {
            if (quoteType == c)
            {
              inQuotes = false;
              quoteType = ' ';
            }
          }
          else
          {
            if (omitQuotedText)
            {
              inQuotes = true;
              quoteType = c;
            }
          }
        }

        if (!Char.IsWhiteSpace(c) || inQuotes)
          sb.Append(c);
      }

      if (inQuotes)
        return holdValue.CondenseText();

      return sb.ToString();
    }

    [DebuggerStepThrough]
    public static string CondenseExtraSpaces(this string value)
    {
      if (value == null)
        return String.Empty;

      if (value.Length == 0)
        return String.Empty;

      while (value.Contains("  "))
        value = value.Replace("  ", " ");

      return value.Trim();
    }

    [DebuggerStepThrough]
    public static List<String> ToListOfStrings(this string value, char[] delimiter)
    {
      if (value == null)
        return new List<String>();

      if (value.Trim().Length == 0)
        return new List<String>();

      return value.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    [DebuggerStepThrough]
    public static int DaysInMonth(this DateTime value)
    {
      int month = value.Month;

      switch (month)
      {
        case 1:
        case 3:
        case 5:
        case 7:
        case 8:
        case 10:
        case 12:
          return 31;

        case 2:
          if (value.IsLeapYear())
            return 29;
          return 28;

        default:
          return 30;
      }
    }

    [DebuggerStepThrough]
    public static UInt64 ToInverseLongSortKey(this DateTime value)
    {
      string dateString = value.ToString("yyMMddHHmmss") + "000";
      UInt64 sortKey = UInt64.Parse(dateString);
      UInt64 inverseSortKey = 999999999999999 - sortKey;
      return inverseSortKey;
    }

    [DebuggerStepThrough]
    public static UInt64 ToLongSortKey(this DateTime value)
    {
      string dateString = value.ToString("yyMMddHHmmss") + "000";
      UInt64 sortKey = UInt64.Parse(dateString);
      return sortKey;
    }

    [DebuggerStepThrough]
    public static bool IsNotBlank(this string value)
    {
      if (value == null)
        return false;

      if (value.Trim().Length == 0)
        return false;

      return true;
    }

    public static int GetKeyByValue(this Dictionary<int, string> dict, string value, bool matchCase = true)
    {
      if (dict == null || dict.Count == 0 || value == null)
        return -1;

      foreach (var e in dict)
      {
        if (matchCase)
        {
          if (e.Value == value)
            return e.Key;
        }
        else
        {
          if (e.Value.ToLower() == value.ToLower())
            return e.Key;
        }
      }

      return -1;
    }


    //[DebuggerStepThrough]
    //public static bool IsBlank(this object value)
    //{
    //  if (value == null)
    //    return true;

    //  if (value.ToString().Trim().Length == 0)
    //    return true;

    //  return false;
    //}

    [DebuggerStepThrough]
    public static bool IsNotBlank(this object value)
    {
      if (value == null)
        return false;

      if (value.ToString().Trim().Length == 0)
        return false;

      return true;
    }

    [DebuggerStepThrough]
    public static string ToAlphaTokens(this string value, int minLth)
    {
      value = value.Replace("\r\n", " ");
      value = value.Replace("\r", " ");
      value = value.Replace("\n", " ");
      value = value.Trim().ToUpper();

      if (value.IsBlank())
        return String.Empty;

      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < value.Length; i++)
      {
        int c = value[i];
        if (c > 47 && c < 58)  // 0-9
        {
          sb.Append(value[i]);
          continue;
        }
        if (c > 64 && c < 91)  // A-Z
        {
          sb.Append(value[i]);
          continue;
        }
        if (c > 96 && c < 123)  // a-z
        {
          sb.Append(value[i]);
          continue;
        }
        if ((c > 191 && c < 199) || (c > 223 && c < 231))  // A-like
        {
          sb.Append("A");
          continue;
        }
        if (c == 199 || c == 231)  // C-like
        {
          sb.Append("C");
          continue;
        }
        if ((c > 199 && c < 204) || (c > 231 && c < 236))  // E-like
        {
          sb.Append("E");
          continue;
        }
        if ((c > 203 && c < 208) || (c > 235 && c < 240))  // I-like
        {
          sb.Append("I");
          continue;
        }
        if (c == 208 || c == 240)  // D-like
        {
          sb.Append("D");
          continue;
        }
        if (c == 209 || c == 241)  // N-like
        {
          sb.Append("N");
          continue;
        }
        if ((c > 209 && c < 215) || (c > 241 && c < 247))  // O-like
        {
          sb.Append("O");
          continue;
        }
        if ((c > 216 && c < 221) || (c > 248 && c < 253))  // U-like
        {
          sb.Append("U");
          continue;
        }
        if (c == 216 || c == 248)  // 0-like
        {
          sb.Append("0");
          continue;
        }
        if (c == 221 || c == 253 || c == 255)  // Y-like
        {
          sb.Append("Y");
          continue;
        }
        if (c == 223 || c == 254)  // B-like
        {
          sb.Append("B");
          continue;
        }
        if (c == 215)  // X-like
        {
          sb.Append("X");
          continue;
        }

        sb.Append(" ");
      }

      value = sb.ToString().Trim();

      sb.Clear();

      int zeroAmidstLetters = value.LocateZeroAmistLetters();
      while(zeroAmidstLetters != -1)
      {
        value = value.ReplaceAtPosition("O", zeroAmidstLetters);
        zeroAmidstLetters = value.LocateZeroAmistLetters();
      }

      string[] tokens = value.Trim().Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);

      int usedTokenCount = 0;

      for (int i = 0; i < tokens.Length; i++)
      {
        string token = tokens[i];

        if (token == "0K")
          token = "OK";
        if (token == "0R")
          token = "OR";


        if (token.Length > 1)
        {
          if (!token.In("DR,OK,ST,OKLAHOMA,RD,PL,AVE,DATE,CITY,AND,OR"))
          {
            usedTokenCount++;
            if (usedTokenCount > 10)
              break;

            if ((token.In("PAY,DOLLAR,DOLLARS") && usedTokenCount > 5))
              break;

            if (sb.Length == 0)
              sb.Append(token);
            else
              sb.Append(" " + token);
          }
        }
      }

      return sb.ToString();
    }

    //[DebuggerStepThrough]
    public static int LocateZeroAmistLetters(this string value)
    {
      if (value == null)
        return -1;

      if (value.IsBlank())
        return -1;

      value = value.Trim();

      if (value.Length < 2)
        return -1;

      for (int i = 0; i < value.Length; i++)
      {
        char c = value[i];
        if (c == '0')
        {
          if (i == 0 && i + 1 < value.Length - 1 && (Char.IsLetter(value[i + 1]) || value[i + 1] == ' '))
            return i;

          if (i == value.Length - 1)
          {
            if (Char.IsLetter(value[i - 1]) || value[i - 1] == ' ')
              return i;
          }

          if (i > 0 && i < value.Length - 1)
          {
            if ((Char.IsLetter(value[i - 1])|| value[i - 1] == ' ') && (Char.IsLetter(value[i + 1]) || value[i + 1] == ' '))
              return i;
          }
        }
      }

      return -1;
    }

    [DebuggerStepThrough]
    public static Type ToType(this string value)
    {
      try
      {
        if (value == null || value.IsBlank())
          return null;

        value = value.Replace(" ", String.Empty);

        if (value == "Dictionary<string,string>")

          return typeof(Dictionary<,>).MakeGenericType(typeof(System.String), typeof(System.String));

        if (value == "List<string>")
          return typeof(List<>).MakeGenericType(typeof(System.String));

        return Type.GetType(value);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to convert the string value '" + value + "' to a Type.", ex);
      }
    }

    [DebuggerStepThrough]
    public static int CharCount(this string value, char c)
    {
      if (value == null)
        return 0;

      return value.Count(f => f == c);
    }

    [DebuggerStepThrough]
    public static string SetIfBlank(this string value, string newValue)
    {
      if (value == null && newValue == null)
        return String.Empty;

      if (value == null && newValue != null)
        return newValue;

      if (value.IsNotBlank())
        return value;

      return newValue;
    }


    [DebuggerStepThrough]
    public static bool IsLeapYear(this DateTime value)
    {
      if (value.Year % 4 == 0 && value.Year % 100 != 0 || value.Year % 400 == 0)
        return true;

      return false;
    }


    [DebuggerStepThrough]
    public static DateTime ToCCYYMMDD(this string value)
    {
      return new DateTime(Convert.ToInt32(value.Substring(0,4)),Convert.ToInt32(value.Substring(4,2)),Convert.ToInt32(value.Substring(6,2)));
    }

    [DebuggerStepThrough]
    public static XElement GetElement(this XElement value, string elementName)
    {
      if (value == null || elementName == null)
        return null;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Element(ns + elementName) == null)
        return null;

      return value.Element(ns + elementName);
    }

    [DebuggerStepThrough]
    public static XElement GetRequiredElement(this XElement value, string elementName)
    {
      if (value == null)
        throw new Exception("Null element passed into GetRequiredElement.");

      if (elementName == null)
        throw new Exception("Null elementName parameter passed into GetRequiredElement.");

      XNamespace ns = value.Name.NamespaceName;

      if (value.Element(ns + elementName) == null)
        throw new Exception("Required xml element '" + elementName + "' is not found in the xml '" + value.ToString() + "'.");

      return value.Element(ns + elementName);
    }

    [DebuggerStepThrough]
    public static string GetAttributeValue(this XElement value, string attributeName)
    {
      if (value == null || attributeName == null)
        return String.Empty;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        if (value.Attribute(attributeName) != null)
          return value.Attribute(attributeName).Value.Trim();
        else
          return String.Empty;

      return value.Attribute(ns + attributeName).Value.Trim();
    }

    [DebuggerStepThrough]
    public static string GetAttributeValueOrNull(this XElement value, string attributeName)
    {
      if (value == null || attributeName == null)
        return null;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return null;

      return value.Attribute(ns + attributeName).Value.Trim();
    }

    [DebuggerStepThrough]
    public static string GetRequiredElementAttributeValue(this XElement value, string elementName, string attributeName)
    {
      if (value == null)
        throw new Exception("Null element passed into GetRequiredElementAttributeValue.");

      if (elementName == null)
        throw new Exception("Null elementName parameter passed into GetRequiredElementAttributeValue.");

      if (attributeName == null)
        throw new Exception("Null attributeName parameter passed into GetRequiredElementAttributeValue.");

      XNamespace ns = value.Name.NamespaceName;

      if (value.Element(ns + elementName) == null)
        throw new Exception("Required xml element '" + elementName + "' is missing from element '" + value.ToString() + "'.");

      XElement e = value.Element(ns + elementName);

      if (e.Attribute(ns + attributeName) == null)
        throw new Exception("Required xml attribute '" + attributeName + "' is missing from element '" + elementName + "' in xml '" + value.ToString() + "'.");

      string attributeValue = e.Attribute(ns + attributeName).Value.Trim();

      if (attributeValue.IsBlank())
        throw new Exception("Required xml attribute '" + attributeName + "' is blank in the xml '" + value.ToString() + "'.");

      return attributeValue;
    }

    [DebuggerStepThrough]
    public static string GetElementAttributeValueOrBlank(this XElement value, string elementName, string attributeName)
    {
      if (value == null || elementName == null || attributeName == null)
        return String.Empty;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Element(ns + elementName) == null)
        return String.Empty;

      XElement e = value.Element(ns + elementName);

      if (e.Attribute(ns + attributeName) == null)
        return String.Empty;

      string attributeValue = e.Attribute(ns + attributeName).Value.Trim();

      return attributeValue;
    }

    [DebuggerStepThrough]
    public static string GetElementAttributeValueOrNull(this XElement value, string elementName, string attributeName)
    {
      if (value == null || elementName == null || attributeName == null)
        return null;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Element(ns + elementName) == null)
        return null;

      XElement e = value.Element(ns + elementName);

      if (e.Attribute(ns + attributeName) == null)
        return null;

      string attributeValue = e.Attribute(ns + attributeName).Value.Trim();

      return attributeValue;
    }

    [DebuggerStepThrough]
    public static string GetRequiredAttributeValue(this XElement value, string attributeName)
    {
      if (value == null && attributeName == null)
        throw new Exception("Null element and attribute name passed into GetRequiredAttributeValue.");

      if (value == null && attributeName != null)
        throw new Exception("Required xml attribute '" + attributeName + "' cannot be retrived from null element.'");

      if (value != null && attributeName == null)
        throw new Exception("Required xml attributeName is null, cannot be retrieved from element '" + value.ToString() + "'.");

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        throw new Exception("Required xml attribute '" + attributeName + "' is missing from element '" + value.ToString() + "'.");

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();

      if (attributeValue.IsBlank())
        throw new Exception("Required xml attribute '" + attributeName + "' is blank in the xml '" + value.ToString() + "'.");

      return attributeValue;
    }

    [DebuggerStepThrough]
    public static UInt32 GetRequiredAttributeUInt32(this XElement value, string attributeName)
    {
      if (value == null && attributeName == null)
        throw new Exception("Null element and attribute name passed into GetRequiredAttributeUInt32.");

      if (value == null && attributeName != null)
        throw new Exception("Required xml attribute '" + attributeName + "' cannot be retrived from null element.'");

      if (value != null && attributeName == null)
        throw new Exception("Required xml attributeName is null, cannot be retrieved from element '" + value.ToString() + "'.");

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        throw new Exception("Required xml attribute '" + attributeName + "' is missing from element '" + value.ToString() + "'.");

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();

      if (attributeValue.IsNotNumeric())
        throw new Exception("Required numeric xml attribute '" + attributeName + "' is not numeric in the xml '" + value.ToString() + "'.");

      return UInt32.Parse(attributeValue);
    }

    [DebuggerStepThrough]
    public static Int32 GetRequiredAttributeInt32(this XElement value, string attributeName)
    {
      if (value == null && attributeName == null)
        throw new Exception("Null element and attribute name passed into GetRequiredAttributeInt32.");

      if (value == null && attributeName != null)
        throw new Exception("Required xml attribute '" + attributeName + "' cannot be retrived from null element.'");

      if (value != null && attributeName == null)
        throw new Exception("Required xml attributeName is null, cannot be retrieved from element '" + value.ToString() + "'.");

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        throw new Exception("Required xml attribute '" + attributeName + "' is missing from element '" + value.ToString() + "'.");

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();

      if (attributeValue.IsNotNumeric())
        throw new Exception("Required numeric xml attribute '" + attributeName + "' is not numeric in the xml '" + value.ToString() + "'.");

      return Int32.Parse(attributeValue);
    }

    [DebuggerStepThrough]
    public static int GetIntegerAttribute(this XElement value, string attributeName)
    {
      if (value == null)
        return 0;

      if (attributeName == null)
        return 0;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return 0;

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();
      if (attributeValue.IsNumeric())
        return Int32.Parse(attributeValue);

      throw new Exception("Required xml integer attribute '" + attributeName + "' has an illegal (non-integer) value '" + attributeValue + "'.");
    }

    [DebuggerStepThrough]
    public static UInt16 GetUInt16Attribute(this XElement value, string attributeName)
    {
      if (value == null)
        return 0;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return 0;

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();
      if (attributeValue.IsNumeric())
        return UInt16.Parse(attributeValue);

      throw new Exception("Required xml UInt16 attribute '" + attributeName + "' has an illegal (non-integer) value '" + attributeValue + "'.");
    }

    [DebuggerStepThrough]
    public static UInt16? GetUInt16AttributeOrNull(this XElement value, string attributeName)
    {
      if (value == null || attributeName == null)
        return null;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return null;

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();
      if (attributeValue.IsNumeric())
        return UInt16.Parse(attributeValue);

      throw new Exception("Required xml UInt16 attribute '" + attributeName + "' has an illegal (non-integer) value '" + attributeValue + "'.");
    }

    [DebuggerStepThrough]
    public static UInt32? GetUInt32AttributeOrNull(this XElement value, string attributeName)
    {
      if (value == null || attributeName == null)
        return null;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return null;

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();
      if (attributeValue.IsNumeric())
        return UInt32.Parse(attributeValue);

      throw new Exception("Required xml UInt32 attribute '" + attributeName + "' has an illegal (non-integer) value '" + attributeValue + "'.");
    }

    [DebuggerStepThrough]
    public static UInt32 GetUInt32Attribute(this XElement value, string attributeName)
    {
      if (value == null || attributeName == null)
        return 0;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return 0;

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();
      if (attributeValue.IsNumeric())
        return UInt32.Parse(attributeValue);

      throw new Exception("Required xml UInt32 attribute '" + attributeName + "' has an illegal (non-integer) value '" + attributeValue + "'.");
    }

    [DebuggerStepThrough]
    public static Int32 GetInt32Attribute(this XElement value, string attributeName)
    {
      if (value == null || attributeName == null)
        return 0;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return 0;

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();
      if (attributeValue.IsNumeric())
        return Int32.Parse(attributeValue);

      throw new Exception("Required xml Int32 attribute '" + attributeName + "' has an illegal (non-integer) value '" + attributeValue + "'.");
    }

    [DebuggerStepThrough]
    public static int GetRequiredIntegerAttribute(this XElement value, string attributeName)
    {
      if (value == null)
        throw new Exception("Null element passed into GetRequiredIntegerAttribute.");

      if (attributeName == null)
        throw new Exception("Null attributeName parameter passed into GetRequiredIntegerAttribute.");

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        throw new Exception("Required xml integer attribute '" + attributeName + "' is missing from element '" + value.ToString() + "'.");

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();
      if (attributeValue.IsNumeric())
        return Int32.Parse(attributeValue);

      throw new Exception("Required xml integer attribute '" + attributeName + "' has an illegal (non-integer) value '" + attributeValue + "'.");
    }

    [DebuggerStepThrough]
    public static bool GetBooleanAttribute(this XElement value, string attributeName)
    {
      if (value == null || attributeName == null)
        return false;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return false;

      return Boolean.Parse(value.Attribute(ns + attributeName).Value.Trim());
    }

    [DebuggerStepThrough]
    public static bool GetBooleanAttributeWithDefault(this XElement value, string attributeName, bool defaultValue)
    {
      if (value == null || attributeName == null)
        return defaultValue;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
      {
        return defaultValue;
      }

      return Boolean.Parse(value.Attribute(ns + attributeName).Value.Trim());
    }

    [DebuggerStepThrough]
    public static bool? GetBooleanAttributeValueOrNull(this XElement value, string attributeName)
    {
      if (value == null || attributeName == null)
        return null;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return null;

      string attributeValue = value.Attribute(ns + attributeName).Value.ToLower().Trim();

      switch (attributeValue)
      {
        case "0":
        case "false":
        case "off":
        case "no":
          return false;

        case "1":
        case "true":
        case "on":
        case "yes":
          return true;
      }

      return Boolean.Parse(attributeValue);
    }

    [DebuggerStepThrough]
    public static Int32? GetInt32AttributeValueOrNull(this XElement value, string attributeName)
    {
      if (value == null || attributeName == null)
        return null;

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return null;

      string attributeValue = value.Attribute(ns + attributeName).Value.ToLower().Trim();

      return Int32.Parse(attributeValue);
    }

    [DebuggerStepThrough]
    public static bool GetRequiredBooleanAttribute(this XElement value, string attributeName)
    {
      if (value == null)
        throw new Exception("Null element parameter passed into GetRequiredBooleanAttribute.");

      if (attributeName == null)
        throw new Exception("Null attributeName parameter passed into GetRequiredBooleanAttribute.");

      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        throw new Exception("Required xml attribute '" + attributeName + "' is missing from element '" + value.ToString() + "'.");

      return Boolean.Parse(value.Attribute(ns + attributeName).Value.Trim());
    }

    [DebuggerStepThrough]
    public static object GetEnumAttribute(this XElement value, string attributeName, Type type)
    {
      if (value == null)
        throw new Exception("Null element parameter passed into GetEnumAttribute.");

      if (attributeName == null)
        throw new Exception("Null attributeName parameter passed into GetEnumAttribute.");

      XNamespace ns = value.Name.NamespaceName;

      string attributeValue = String.Empty;

      if (value.Attribute(ns + attributeName) != null)
        attributeValue = value.Attribute(ns + attributeName).Value.Trim();

      string defaultValue = String.Empty;
      Array enumValues = Enum.GetValues(type);
      foreach (object enumValue in enumValues)
      {
        int intValue = Convert.ToInt32(Enum.Parse(type, enumValue.ToString()));
        if (intValue == 0)
        {
          defaultValue = enumValue.ToString();
          break;
        }
      }

      foreach (object enumValue in enumValues)
      {
        if (enumValue.ToString().ToLower() == attributeValue.ToLower())
          return Enum.Parse(type, enumValue.ToString());
      }

      if (defaultValue.IsBlank())
        throw new Exception("The value for xml attribute '" + attributeName + "' is '" + attributeValue + "' which is not a valid value for the enumeration of type '" + type.Name + "'  and no default value is defined.");

      return Enum.Parse(type, defaultValue);
    }

    [DebuggerStepThrough]
    public static object GetRequiredElementAttributeEnum(this XElement value, string elementName, string attributeName, Type type)
    {
      if (value == null)
        throw new Exception("Null element parameter passed into GetRequiredElementAttributeEnum.");

      if (elementName == null)
        throw new Exception("Null elementName parameter passed into GetRequiredElementAttributeEnum.");

      if (attributeName == null)
        throw new Exception("Null attributeName parameter passed into GetRequiredElementAttributeEnum.");

      XNamespace ns = value.Name.NamespaceName;

      if (value.Element(ns + elementName) == null)
        throw new Exception("Required xml element '" + elementName + "' is missing from element '" + value.ToString() + "'.");

      XElement e = value.Element(ns + elementName);

      if (e.Attribute(ns + attributeName) == null)
        throw new Exception("Required xml attribute '" + attributeName + "' is missing from element '" + elementName + "' in xml '" + value.ToString() + "'.");

      string attributeValue = e.Attribute(ns + attributeName).Value.Trim();

      if (attributeValue.IsBlank())
        throw new Exception("Required xml attribute '" + attributeName + "' is blank in the xml '" + value.ToString() + "'.");

      string defaultValue = String.Empty;
      Array enumValues = Enum.GetValues(type);
      foreach (object enumValue in enumValues)
      {
        int intValue = Convert.ToInt32(Enum.Parse(type, enumValue.ToString()));
        if (intValue == 0)
        {
          defaultValue = enumValue.ToString();
          break;
        }
      }

      foreach (object enumValue in enumValues)
      {
        if (enumValue.ToString().ToLower() == attributeValue.ToLower())
          return Enum.Parse(type, enumValue.ToString());
      }

      throw new Exception("The value for xml attribute '" + attributeName + "' is '" + attributeValue + "' which is not a valid value for the enumeration of type '" + type.Name + "'.");
    }

    [DebuggerStepThrough]
    public static object GetEnumAttributeOrDefault(this XElement value, string attributeName, Type type)
    {
      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
      {
        string defaultValue = String.Empty;
        Array enumValues = Enum.GetValues(type);
        foreach (object enumValue in enumValues)
        {
          int intValue = Convert.ToInt32(Enum.Parse(type, enumValue.ToString()));
          if (intValue == 0)
          {
            defaultValue = enumValue.ToString();
            return Enum.Parse(type, enumValue.ToString());
          }
        }

        throw new Exception("The value for xml attribute '" + attributeName + "' does not exist and there is no default value for the enumeration." + type.Name + "'.");
      }
      else
      {
        string attributeValue = value.Attribute(ns + attributeName).Value.Trim();

        string defaultValue = String.Empty;
        Array enumValues = Enum.GetValues(type);
        foreach (object enumValue in enumValues)
        {
          int intValue = Convert.ToInt32(Enum.Parse(type, enumValue.ToString()));
          if (intValue == 0)
          {
            defaultValue = enumValue.ToString();
            break;
          }
        }

        foreach (object enumValue in enumValues)
        {
          if (enumValue.ToString().ToLower() == attributeValue.ToLower())
            return Enum.Parse(type, enumValue.ToString());
        }

        throw new Exception("The value for xml attribute '" + attributeName + "' is '" + attributeValue + "' which is not a valid value for the enumeration of type '" + type.Name + "'.");
      }
    }

    [DebuggerStepThrough]
    public static object GetEnumAttributeOrSpecific(this XElement value, string attributeName, Type type, object specific)
    {
      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
      {
        return specific;
      }
      else
      {
        string attributeValue = value.Attribute(ns + attributeName).Value.Trim();
        Array enumValues = Enum.GetValues(type);

        foreach (object enumValue in enumValues)
        {
          if (enumValue.ToString().ToLower() == attributeValue.ToLower())
            return Enum.Parse(type, enumValue.ToString());
        }

        throw new Exception("The value for xml attribute '" + attributeName + "' is '" + attributeValue + "' which is not a valid value for the enumeration of type '" + type.Name + "'.");
      }
    }

    [DebuggerStepThrough]
    public static object GetEnumAttributeOrNull(this XElement value, string attributeName, Type type)
    {
      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        return null;

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();

      Array enumValues = Enum.GetValues(type);
      foreach (object enumValue in enumValues)
      {
        if (enumValue.ToString().ToLower() == attributeValue.ToLower())
          return Enum.Parse(type, enumValue.ToString());
      }

      throw new Exception("The value for xml attribute '" + attributeName + "' is '" + attributeValue + "' which is not a valid value for the enumeration of type '" + type.Name + "'.");
    }

    [DebuggerStepThrough]
    public static object GetRequiredEnumAttribute(this XElement value, string attributeName, Type type)
    {
      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        throw new Exception("Required xml attribute '" + attributeName + "' is missing from element '" + value.ToString() + "'.");

      string attributeValue = value.Attribute(ns + attributeName).Value.Trim();

      string defaultValue = String.Empty;
      Array enumValues = Enum.GetValues(type);
      foreach (object enumValue in enumValues)
      {
        int intValue = Convert.ToInt32(Enum.Parse(type, enumValue.ToString()));
        if (intValue == 0)
        {
          defaultValue = enumValue.ToString();
          break;
        }
      }

      foreach (object enumValue in enumValues)
      {
        if (enumValue.ToString().ToLower() == attributeValue.ToLower())
          return Enum.Parse(type, enumValue.ToString());
      }

      throw new Exception("The value for xml attribute '" + attributeName + "' is '" + attributeValue + "' which is not a valid value for the enumeration of type '" + type.Name + "'.");
    }

    [DebuggerStepThrough]
    public static string GetRequiredElementValue(this XElement value, string elementName)
    {
      XNamespace ns = value.Name.NamespaceName;

      if (value.Element(ns + elementName) == null)
        throw new Exception("Required xml child element '" + elementName + "' is missing from parent element '" + value.ToString() + "'.");

      string elementValue = value.Element(ns + elementName).Value.Trim();

      if (elementValue.IsBlank())
        throw new Exception("Required xml child element '" + elementName + "' exists but is blank in the xml '" + value.ToString() + "'.");

      return elementValue.Trim();
    }

    [DebuggerStepThrough]
    public static string GetElementValue(this XElement value, string elementName)
    {
      XNamespace ns = value.Name.NamespaceName;

      if (value.Element(ns + elementName) == null)
        return String.Empty;

      string elementValue = value.Element(ns + elementName).Value.Trim();

      return elementValue.Trim();
    }

    //[DebuggerStepThrough]
    public static string ToReport(this Exception value)
    {
      StringBuilder sb = new StringBuilder();

      Exception ex = value;
      bool moreExceptions = true;
      int level = 0;


      var messageList = new List<string>();
      Exception ex2 = ex;
      messageList.Add(ex2.Message);
      while (ex2.InnerException != null)
      {
        ex2 = ex2.InnerException;
        messageList.Add(ex2.Message);
      }

      sb.Append("Exception Message Summary:" + g.crlf);
      for (int i = messageList.Count - 1; i > -1; i--)
      {
        sb.Append("[" + i.ToString("00") + "] " + messageList.ElementAt(i) + g.crlf);
      }

      sb.Append(g.crlf);

      string additionalInfo = String.Empty;

      string type = ex.GetType().Name;

      switch (type)
      {
        case "ReflectionTypeLoadException":
          var rlx = ex as ReflectionTypeLoadException;
          foreach (var lx in rlx.LoaderExceptions)
          {
            if (additionalInfo.IsBlank())
              additionalInfo = "*** TYPED EXCEPTION INFORMATION ***" + g.crlf + "From ReflectionTypeLoadException:" + g.crlf + lx.ToReport();
            else
              additionalInfo += g.crlf + lx.ToReport();
          }
          break;
      }

      if (additionalInfo.IsNotBlank())
        additionalInfo += "***" + g.crlf;

      while (moreExceptions)
      {
        if (ex.Message.StartsWith("!"))
          return ex.Message.Substring(1);

        sb.Append("Level:" + level.ToString() + " Type=" + ex.GetType().ToString() + Environment.NewLine +
                  "Message: " + ex.Message + Environment.NewLine + additionalInfo +
                  "StackTrace:" + ex.StackTrace + Environment.NewLine);

        if (ex.InnerException == null)
          moreExceptions = false;
        else
        {
          sb.Append(Environment.NewLine);
          ex = ex.InnerException;
          level++;
        }
      }

      string report = sb.ToString();
      return report;
    }

    [DebuggerStepThrough]
    public static bool ContainsText(this Exception value, string text)
    {
      string compareText = text.ToUpper();
      Exception ex = value;
      bool moreExceptions = true;
      int level = 0;

      while (moreExceptions)
      {
        if (ex.Message.ToUpper().Contains(compareText))
          return true;

        if (ex.InnerException == null)
          moreExceptions = false;
        else
        {
          ex = ex.InnerException;
          level++;
        }
      }

      return false;
    }

    [DebuggerStepThrough]
    public static string MessageReport(this Exception value)
    {
      StringBuilder sb = new StringBuilder();

      Exception ex = value;
      bool moreExceptions = true;
      int level = 0;

      while (moreExceptions)
      {
        sb.Append("[" + level.ToString() + "] " + ex.Message);

        if (ex.InnerException == null)
          moreExceptions = false;
        else
        {
          sb.Append(g.crlf);
          ex = ex.InnerException;
          level++;
        }
      }

      string report = sb.ToString();
      return report;
    }

    [DebuggerStepThrough]
    public static void AssertElement(this XElement value, string elementName)
    {
      XNamespace ns = value.Name.NamespaceName;

      if (value.Element(ns + elementName) == null)
        throw new Exception("Required xml child element '" + elementName + "' is missing from element '" + value.ToString() + "'.");
    }

    [DebuggerStepThrough]
    public static void AssertAttribute(this XElement value, string attributeName)
    {
      XNamespace ns = value.Name.NamespaceName;

      if (value.Attribute(ns + attributeName) == null)
        throw new Exception("Required xml attribute '" + attributeName + "' is missing from element '" + value.ToString() + "'.");
    }

    [DebuggerStepThrough]
    public static string InQuotes(this string value)
    {
      if (value == null)
        return "\"\"";

      return "\"" + value.Trim() + "\"";
    }

    [DebuggerStepThrough]
    public static string CamelCase(this string value)
    {
      if (value == null)
        return String.Empty;

      string trimmedValue = value.Trim();

      if (trimmedValue.Length == 0)
        return String.Empty;

      if (trimmedValue.Length == 1)
        return trimmedValue.ToLower();

      return trimmedValue.Substring(0, 1).ToLower() + trimmedValue.Substring(1);
    }

    [DebuggerStepThrough]
    public static string CaptializeFirstLetter(this string value)
    {
      string trimmedValue = value.Trim();

      if (trimmedValue.Length == 0)
        return String.Empty;

      if (trimmedValue.Length == 1)
        return trimmedValue.ToUpper();

      return trimmedValue.Substring(0, 1).ToUpper() + trimmedValue.Substring(1);
    }


    public static string ToHex(this byte[] b)
    {
      // need to finish this sometime
      return String.Empty;
    }

    public static string ToTextDump(this string value)
    {
      string header = "        Special characters:  '" + ("\xA4").ToString() + "' is new line, '" + ("\xB6").ToString() + " is carriage return, " + ("\xB7").ToString() +
                      "' is a blank character." + g.crlf +
                      "        Total length = " + (value.IsBlank() ? "0" : value.Length.ToString("###,##0")) + g.crlf +
                      "        0----+----1----+----2----+----3----+----4----+----5----+----6----+----7----+----8----+----9----+----";

      if (value.IsBlank())
        return header + g.crlf + "        *** TEXT VALUE IS BLANK OR NULL ***" + g.crlf;

      var sb = new StringBuilder();
      sb.Append(header + g.crlf);

      var lines = new List<string>();

      int lth = value.Length;
      int remainingLength = lth;
      int begPos = 0;
      int lineNbr = -1;

      while (remainingLength > 0)
      {
        lineNbr++;
        if (remainingLength > 99)
        {
          string fullLine = value.Substring(begPos, 100).Replace("\r", "\xB6").Replace("\n", "\xA4").Replace(" ", "\xB7");
          lines.Add(lineNbr.ToString("000000") + "  " + fullLine);
          remainingLength -= 100;
          begPos += 100;
        }
        else
        {
          string partialLine = value.Substring(begPos, remainingLength).Replace("\r", "\xB6").Replace("\n", "\xA4").Replace(" ", "\xB7");
          lines.Add(lineNbr.ToString("000000") + "  " + partialLine);
          begPos += remainingLength;
          remainingLength = 0;
        }
      }

      foreach (var line in lines)
        sb.Append(line + g.crlf);

      sb.Append(g.crlf);

      string report = sb.ToString();
      return report;
    }

    public static string To100CharLines(this string value, string indent)
    {
      if (value.IsBlank())
        return String.Empty;

      var sb = new StringBuilder();

      var lines = new List<string>();

      int lth = value.Length;
      int remainingLength = lth;
      int begPos = 0;
      int lineNbr = 0;

      while (remainingLength > 0)
      {
        lineNbr++;
        if (remainingLength > 99)
        {
          string fullLine = value.Substring(begPos, 100).Replace("\n", "\xA4").Replace(" ", "\xB7");
          lines.Add(fullLine);
          remainingLength -= 100;
          begPos += 100;
        }
        else
        {
          string partialLine = value.Substring(begPos, remainingLength).Replace("\n", "\xA4").Replace(" ", "\xB7");
          lines.Add(partialLine);
          begPos += remainingLength;
          remainingLength = 0;
        }
      }

      foreach (var line in lines)
        sb.Append(indent + line + g.crlf);

      sb.Append(g.crlf);

      string report = sb.ToString();
      return report;
    }

    public static string ToBinHexDump(this string value)
    {
      if (value.IsBlank())
        return String.Empty;

      byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(value);
      return ToBinHexDump(byteArray);
    }

    public static string ToHexDump(this byte[] a)
    {
      if (a == null)
        return "Byte array is null";

      //string aStr = System.Text.Encoding.Default.GetString(a);
      int remainder = a.Length % 16 > 0 ? 1 : 0;
      int bytesRemaining = a.Length;

      string reportHeader = "OFFSET  00 01 02 03 04 05 06 07   08 09 0A 0B 0C 0D 0E 0F    OFFSET  ---CHARACTER----";
      StringBuilder sbReport = new StringBuilder(reportHeader + g.crlf);

      int pos = 0;

      while (true)
      {
        if (pos >= a.Length)
          break;

        sbReport.Append(pos.ToHexAddress(6) + "  ");

        if (pos + 16 <= a.Length)
        {
          int start = pos;
          int end = pos + 16;
          for (int i = 0; i < 16; i++)
          {
            sbReport.Append(a[pos++].ToHex() + ((i == 7 || i == 15) ? "   " : " "));
          }

          sbReport.Append(pos.ToString("0000000") + "  ");
          sbReport.Append(System.Text.Encoding.Default.GetString(a.Slice(start, end)));
          sbReport.Append(g.crlf);
        }
        else
        {
          int remainingBytes = a.Length - pos;
          int start = pos;
          int end = a.Length - 1;
          for (int i = 0; i < remainingBytes; i++)
          {
            sbReport.Append(a[pos++].ToHex() + (i == 7 ? "   " : i < 15 ? " " : String.Empty));
          }

          sbReport.Append(pos.ToString("0000000") + "  ");
          sbReport.Append(System.Text.Encoding.Default.GetString(a.Slice(start, end)));
          sbReport.Append(g.crlf);
          break;
        }
      }

      sbReport.Append(g.crlf2 + "TOTAL LENGTH IN BYTES: " + a.Length.ToString("###,###,###,##0") + g.crlf);

      string report = sbReport.ToString();

      return report;
    }

    public static T[] Slice<T>(this T[] source, int start, int end)
    {
      // Handles negative ends.
      if (end < 0)
      {
        end = source.Length + end;
      }
      int len = end - start;

      // Return new array.
      T[] res = new T[len];
      for (int i = 0; i < len; i++)
      {
        res[i] = source[i + start];
      }
      return res;
    }

    public static string ToBinHexDump(this byte[] a)
    {
      string aStr = System.Text.Encoding.Default.GetString(a);
      int remainder = a.Length % 8 > 0 ? 1 : 0;
      string[] chr = new string[Convert.ToInt32(a.Length / 8) + remainder];

      int charsRemaining = aStr.Length;
      int pos = 0;
      int address = 0;

      for (int i = 0; i < chr.Length; i++)
      {
        int length = charsRemaining;
        if (length > 8)
          length = 8;
        chr[i] = aStr.Substring(pos, length).PadTo(8);
        pos += length;
        charsRemaining -= length;
      }

      string reportHeader = "ADDR-X | ----1--- ----2--- ----3--- ----4---  ----5--- ----6--- ----7--- ----8--- | ----------HEX----------- | ADDR-D | --CHAR-- | --------------DEC---------------" + g.crlf;
      StringBuilder sbReport = new StringBuilder(reportHeader);

      string[] bin = new string[8];
      string[] hex = new string[8];
      string[] dec = new string[8];
      int bytesProcessed = 0;
      pos = 0;

      for (int i = 0; i < a.Length - 1; i++)
      {
        bin[bytesProcessed] = a[i].GetBits();
        hex[bytesProcessed] = a[i].ToHex().ToLower();
        dec[bytesProcessed] = ((int)a[i]).ToString("000");

        bytesProcessed++;

        if (bytesProcessed == 8)
        {
          if (address > 0 && address % 256 == 0)
          {
            sbReport.Append(g.crlf + reportHeader);
          }

          string addressHex = address.ToHex().PadWithLeadingZeros(6);
          string addressDec = address.ToString("000000");

          sbReport.Append(addressHex + " | " + bin[0] + " " + bin[1] + " " + bin[2] + " " + bin[3] + "  " +
                          bin[4] + " " + bin[5] + " " + bin[6] + " " + bin[7] + " | " +
                          hex[0] + " " + hex[1] + " " + hex[2] + " " + hex[3] + "  " +
                          hex[4] + " " + hex[5] + " " + hex[6] + " " + hex[7] + " | " +
                          addressDec + " | " + chr[Convert.ToInt32(i / 8)] + " | " +
                          dec[0] + " " + dec[1] + " " + dec[2] + " " + dec[3] + "  " +
                          dec[4] + " " + dec[5] + " " + dec[6] + " " + dec[7] + g.crlf);

          bytesProcessed = 0;

          address += 8;
          pos += 8;
        }
      }

      bytesProcessed = 0;

      if (pos < a.Length)
      {
        bin = new string[8];
        hex = new string[8];
        dec = new string[8];

        for (int i = 0; i < 8; i++)
        {
          bin[i] = "        ";
          hex[i] = "  ";
          dec[i] = "   ";
        }

        for (int i = pos; i < a.Length; i++)
        {
          bin[bytesProcessed] = a[i].GetBits();
          hex[bytesProcessed] = a[i].ToHex().ToLower();
          dec[bytesProcessed] = ((int)a[i]).ToString("000");
          bytesProcessed++;
        }

        string addressHex = address.ToHex().PadWithLeadingZeros(6);
        string addressDec = address.ToString("000000");

        sbReport.Append(addressHex + " | " + bin[0] + " " + bin[1] + " " + bin[2] + " " + bin[3] + "  " +
                        bin[4] + " " + bin[5] + " " + bin[6] + " " + bin[7] + " | " +
                        hex[0] + " " + hex[1] + " " + hex[2] + " " + hex[3] + "  " +
                        hex[4] + " " + hex[5] + " " + hex[6] + " " + hex[7] + " | " +
                        addressDec + " | " + chr[chr.Length - 1] + " | " +
                        dec[0] + " " + dec[1] + " " + dec[2] + " " + dec[3] + "  " +
                        dec[4] + " " + dec[5] + " " + dec[6] + " " + dec[7] + g.crlf);

      }

      sbReport.Append(g.crlf + "TOTAL LENGTH IN BYTES: " + a.Length.ToString("###,###,###,##0") + g.crlf);

      string report = sbReport.ToString();

      return report;
    }

    public static string ToHexAddress(this int value, int minWidth)
    {
      int remainder = value;
      string hexAddress = String.Empty;

      while (value > 15)
      {
        remainder = value % 16;
        value = value / 16;
        hexAddress = remainder.ToHexDigit() + hexAddress;
      }

      hexAddress = value.ToHexDigit() + hexAddress;

      while (hexAddress.Length < minWidth)
        hexAddress = "0" + hexAddress;

      return hexAddress;
    }

    public static string ToHexDigit(this int value)
    {
      if (value < 10)
        return value.ToString();

      switch (value)
      {
        case 10:
          return "A";
        case 11:
          return "B";
        case 12:
          return "C";
        case 13:
          return "D";
        case 14:
          return "E";
        case 15:
          return "F";
      }

      return "?";
    }

    public static string ToHex(this int n)
    {
      return n.ToString("X");
    }

    public static string First50(this string s)
    {
      if (s == null || s.IsBlank())
        return String.Empty;

      if (s.Length > 49)
        return s.Substring(0, 50);

      return s.Trim();
    }

    public static string First50(this XElement x)
    {
      if (x == null)
        return String.Empty;

      return x.ToString().First50();
    }

    public static bool AttributeExists(this XElement e, string attributeName)
    {
      if (e == null)
        return false;

      if (e.Attributes(attributeName).Count() > 0)
        return true;

      return false;
    }

    public static void AddAttribute(this XElement e, string attributeName, string attributeValue)
    {
      if (attributeName.IsBlank())
      {
        if (e != null)
          throw new Exception("Cannot add an attribute with a blank name to the xml element '" + e.First50() + "'.");
      }

      if (attributeValue == null)
        attributeValue = String.Empty;

      if (e == null)
        throw new Exception("The xml element is null - the attribute named '" + attributeName + "' with value '" +
                            attributeValue + "' cannot be added to a null xml element.");

      if (e.Attributes(attributeName).Count() > 0)
        throw new Exception("The attribute named '" + attributeName + "' already exists in the xml element '" + e.First50() + "'.");

      e.Add(new XAttribute(attributeName.Trim(), attributeValue.Trim()));
    }

    private static int HexToInteger(this string s)
    {
      if (!s.IsValidHexNumber())
        return -1;

      s = s.Trim().ToLower();

      return int.Parse(s, System.Globalization.NumberStyles.HexNumber);
    }

    public static bool IsValidHexNumber(this string s)
    {
      s = s.Trim().ToLower();

      if (s.Length % 2 > 0)
        return false;

      foreach (char c in s)
      {
        if (!Char.IsNumber(c))
        {
          if (c < 'a' || c > 'f')
            return false;
        }
      }

      return true;
    }

    public static byte[] HexNumberToByteArray(this string s)
    {
      byte[] emptyByteArray = new byte[0];

      s = s.Trim();

      if (!s.IsValidHexNumber())
        return emptyByteArray;

      int lth = s.Length;
      int byteArrayLength = lth / 2;

      byte[] b = new byte[byteArrayLength];

      for (int i = 0; i < lth; i += 2)
      {
        string hexCode = s.Substring(i, 2);
        int intValue = int.Parse(hexCode, System.Globalization.NumberStyles.HexNumber);
        b[i/2] = (byte)intValue;
      }

      return b;
    }

    public static string ToEfConnectionString(this Org.GS.Configuration.ConfigDbSpec spec)
    {
      return "metadata=res://*/EntityFramework." + spec.DbName + ".csdl|" +
             "res://*/EntityFramework." + spec.DbName + ".ssdl|" +
             "res://*/EntityFramework." + spec.DbName + ".msl;" +
             "provider=System.Data.SqlClient;" +
             "provider connection string=\"Data Source=" + spec.DbServer + ";" +
             "Initial Catalog=" + spec.DbName + ";" +
             "Integrated Security=False;" +
             "User ID=" + spec.DbUserId + ";" +
             "Password=" + spec.DbPassword + "\"";
    }

    public static string ToFullTypeName(this Type type)
    {
      if (type == null)
        return "NULL";

      Type theType = type;
      bool isGenericCollection = false;
      bool isNullableType = false;

      Type collectionType = null;
      Type nullableType = null;

      while (theType != null)
      {
        if (!isGenericCollection)
        {
          if (theType.FullName.Contains("System.Collections.Generic."))
          {
            isGenericCollection = true;
            collectionType = theType;
          }
        }

        if (!isNullableType)
        {
          if (theType.FullName.Contains("System.Nullable"))
          {
            isNullableType = true;
            nullableType = theType;
          }
        }

        if (isNullableType)
        {
          Type[] nullableArgTypes = nullableType.GetGenericArguments();
          string nullableArgs = String.Empty;
          foreach (Type nullableArgType in nullableArgTypes)
          {
            if (nullableArgs.IsBlank())
              nullableArgs = nullableArgType.Name;
            else
              nullableArgs += "," + nullableArgType.Name;
          }

          return nullableType.Name + "(" + nullableArgs + ")";
        }

        if (isGenericCollection)
        {
          Type[] collectionArgTypes = collectionType.GetGenericArguments();
          string collectionArgs = String.Empty;
          foreach (Type collectionArgType in collectionArgTypes)
          {
            if (collectionArgs.IsBlank())
              collectionArgs = collectionArgType.Name;
            else
              collectionArgs += "," + collectionArgType.Name;
          }

          return collectionType.Name + "(" + collectionArgs + ")";
        }

        theType = theType.BaseType;
      }



      return type.Name;
    }

    public static string ToMMSSFFF(this TimeSpan ts)
    {
      if (ts == null)
        return String.Empty;

      int min = ts.Minutes;
      int sec = ts.Seconds;
      int ms = ts.Milliseconds;

      string formatted = min.ToString("00") + ":" + sec.ToString("00") + "." + ms.ToString("000");

      return formatted;
    }

    public static string ToApiStartOfDateString(this DateTime dt)
    {
      string apiStartDtString = dt.ToString("yyyy-MM-dd") + "T00:00:00.0000000Z";
      return apiStartDtString;
    }

    public static string ToApiEndOfDateString(this DateTime dt)
    {
      string apiEndDtString = dt.ToString("yyyy-MM-dd") + "T23:59:59.0000000Z";
      return apiEndDtString;
    }

    public static DateTime ToStartOfDate(this DateTime dt)
    {
      return new DateTime(dt.Year, dt.Month, dt.Day);
    }

    public static DateTime ToEndOfDate(this DateTime dt)
    {
      return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
    }

    public static DateTime ToBeginOfWeek(this DateTime dt)
    {
      var dateWork = dt;
      while (dateWork.DayOfWeek != DayOfWeek.Sunday)
      {
        dateWork = dateWork.AddDays(-1);
      }
      return new DateTime(dateWork.Year, dateWork.Month, dateWork.Day, 0, 0, 0);
    }

    public static string[] ToArgs (this string input)
    {
      input = input.Trim();

      int numberOfQuotes = input.Count(n => n == '"');
      if(numberOfQuotes % 2 != 0)
      {
        throw new Exception("There are an unbalanced number of double quotes in the string passed into the ToArgs extension method.");
      }

      List<string> workingList = new List<string>();
      bool inQuotes = false;
      string workingString = string.Empty;
      for(int i = 0; i < input.Length; i++)
      {
        var c = input[i];
        if(c == '"')
        {
          if(inQuotes)
          {
            inQuotes = false;
            {
              workingList.Add(workingString);
              workingString = String.Empty;
            }

            continue;
          }
          if(!inQuotes)
          {
            inQuotes = true;
            continue;
          }
        }
        if(inQuotes)
        {
          workingString += c;
          continue;
        }

        if(!inQuotes)
          if(c == ' ')
            if(workingString.Length > 0)
            {
              workingList.Add(workingString);
              workingString = String.Empty;
              continue;
            }
            else
              continue;
        if(c != ' ')
        {
          workingString += c;
          continue;
        }
      }

      if(workingString.Length > 0)
        workingList.Add(workingString);

      var args = workingList.ToArray();
      return args;
    }

    public static DateTime ToEndOfWeek(this DateTime dt)
    {
      var dateWork = dt;
      while (dateWork.DayOfWeek != DayOfWeek.Saturday)
      {
        dateWork = dateWork.AddDays(1);
      }
      return new DateTime(dateWork.Year, dateWork.Month, dateWork.Day, 23, 59, 59, 999);
    }

    public static DateTime ToBeginOfMonth(this DateTime dt)
    {
      return new DateTime(dt.Year, dt.Month, 1, 0, 0, 0, 0);
    }

    public static DateTime ToEndOfMonth(this DateTime dt)
    {
      var dateWork = dt.ToBeginOfMonth();
      return new DateTime(dt.Year, dt.Month, dt.LastDayOfMonth(), 23, 59, 59, 999);
    }

    public static DateTime ToBeginOfQuarter(this DateTime dt)
    {
      int month = dt.Month;
      while (month != 1 && month != 4 && month != 7 && month != 10)
        month--;

      return new DateTime(dt.Year, month, 1, 0, 0, 0, 0);
    }

    public static DateTime ToEndOfQuarter(this DateTime dt)
    {
      int month = dt.Month;
      while (month != 3 && month != 6 && month != 9 && month != 12)
        month++;

      return new DateTime(dt.Year, month, 1, 1, 0, 0, 0).ToEndOfMonth().ToEndOfDate();
    }

    public static DateTime ToBeginOfYear(this DateTime dt)
    {
      return new DateTime(dt.Year, 1, 1, 0, 0, 0, 0);
    }

    public static DateTime ToEndOfYear(this DateTime dt)
    {
      return new DateTime(dt.Year, 12, 31, 23, 59, 59, 999);
    }

    public static DateTime ToNextDateAtMidnight(this DateTime dt)
    {
      return new DateTime(dt.Year, dt.Month, dt.Day).AddDays(1);
    }

    public static DateTime ToLastMidnight(this DateTime dt)
    {
      return new DateTime(dt.Year, dt.Month, dt.Day);
    }

    public static DateTime ToEndOfTonight(this DateTime dt)
    {
      return dt.ToNextDateAtMidnight().AddSeconds(-1);
    }

    public static bool IsDbNull(this object value)
    {
      if (value == null)
        return false;

      if (value == DBNull.Value)
        return true;

      return false;
    }

    public static decimal? DbToDecimal(this object value)
    {
      if (value == null || value == DBNull.Value)
        return (decimal?)null;

      double doubleValue = Convert.ToDouble(value);
      decimal decimalValue = (decimal)doubleValue;
      if ((decimalValue > 0 && decimalValue < 0.00000001M ||
           decimalValue < 0 && decimalValue > -0.00000001M))
        return 0;

      return decimalValue;
    }

    public static int? DbToInt32(this object value)
    {
      if (value == null || value == DBNull.Value)
        return (int?)null;

      return Convert.ToInt32(value);
    }

    public static Int64? DbToInt64(this object value)
    {
      if (value == null || value == DBNull.Value)
        return (Int64?)null;

      return Convert.ToInt64(value);
    }

    public static string DbToString(this object value)
    {
      if (value == null || value == DBNull.Value)
        return null;

      return value.ToString();
    }

    public static char? DbToChar(this object value)
    {
      if (value == null || value == DBNull.Value)
        return null;

      string stringValue = value.ToString().Trim();

      if (stringValue.Length > 0)
        return stringValue[0];

      return null;
    }

    public static bool? DbToBoolean(this object value)
    {
      if (value == null || value == DBNull.Value)
        return (bool?)null;

      return Convert.ToBoolean(value);
    }

    public static DateTime? DbToDateTime(this object value)
    {
      if (value == null || value == DBNull.Value)
        return (DateTime?)null;

      return Convert.ToDateTime(value);
    }

    public static DateTime? ToNullDateTime(this object value)
    {
      //Nullable<DateTime> dt = null;
      //if (value == null || value == DBNull.Value)
      //  return dt;

      if(value == null)
        return value.DbToDateTime();

      return Convert.ToDateTime(value);
    }

    public static TimeSpan? DbToTimeSpan(this object value)
    {
      if (value == null || value == DBNull.Value)
        return (TimeSpan?)null;

      return (TimeSpan?)value;
    }

    public static bool ContainsCustomAttribute(this Assembly value, Type type)
    {
      foreach(var attribute in value.CustomAttributes)
      {
        if(attribute.AttributeType == type)
        {
          return true;
        }
      }
      return false;
    }

    public static bool IsDerivedFromGenericCollection(this Type type)
    {
      if (type == null)
        return false;

      Type theType = type;

      while (theType != null)
      {
        if (theType.IsGenericCollection())
          return true;
        theType = theType.BaseType;
      }

      return false;
    }

    public static bool IsDerivedFromGenericCollection(this PropertyInfo pi)
    {
      if (pi ==  null)
        return false;

      return pi.PropertyType.IsDerivedFromGenericCollection();
    }

    public static bool IsDerivedFromGenericCollection(this object o)
    {
      if (o == null)
        return false;

      return o.GetType().IsDerivedFromGenericCollection();
    }

    public static bool IsEquivalent(this XElement xml, XElement xmlComp)
    {
      using (var xMapHelper = new XMapHelper())
      {
        XmlMapper.Load();
        var xmlReport = new XmlReport(xml);

        CompareElements(xml, xmlComp, xMapHelper, xmlReport, Direction.Forward, -1, -1);

        if (xmlReport.ErrorCount > 0)
          return false;

        CompareElements(xmlComp, xml, xMapHelper, xmlReport, Direction.Backward, -1, -1);

        return xmlReport.ErrorCount == 0;
      }
    }

    private static void CompareElements(XElement xmlBase, XElement xmlComp, XMapHelper xMapHelper, XmlReport rpt, Direction direction, int currentLine, int level)
    {
      if (rpt.ErrorCount > 10)
        return;

      level++;

      int lineNumber = rpt.GetLineNumberOfElement(xmlBase, rpt, currentLine);

      // compare equivalence of attributes
      string className = xmlBase.Name.LocalName;
      if(xmlBase.GetAttributeValue("ClassName").IsNotBlank())
        className = xmlBase.GetAttributeValue("ClassName");

      var elementType = xMapHelper.GetType(className);
      var xMap = elementType.GetXMap();
      var piSet = elementType.GetXMapProperties(XType.Attribute);

      foreach(var pi in piSet)
      {
        XMap propXMap = pi.GetXMap();

        string defaultValue = propXMap.DefaultValue.Trim();
        bool defaultExists = defaultValue.IsNotBlank();

        string attrName = propXMap.Name.IsBlank() ? pi.Name : propXMap.Name;
        string baseValue = String.Empty;
        string compValue = String.Empty;

        if(xmlBase.Attribute(attrName) != null)
          baseValue = xmlBase.Attribute(attrName).Value.DbToString().Trim();

        if(xmlComp.Attribute(attrName) != null)
          compValue = xmlComp.Attribute(attrName).Value.DbToString().Trim();

        if (defaultExists)
        {
          if (baseValue.IsBlank())
            baseValue = defaultValue;

          if (compValue.IsBlank())
            compValue = defaultValue;
        }

        baseValue.IsXMappedValueEquivalent(compValue, pi.PropertyType, rpt, direction, lineNumber, level);
      }

      // compare equivalence of element textual value
      if (xmlBase.Value.Trim() != xmlComp.Value.Trim())
      {
        rpt.AddErrorToLine(lineNumber, new XmlLineError(level, "The text value of the XElements are not equal.", direction));
      }

      // compare equivalence of child elements (resursion)
      // wrap each set of elements in an XWrapperSet to assist with processing

      var baseElements = new XWrapperSet(xmlBase);
      var compElements = new XWrapperSet(xmlComp);

      if (baseElements.Count != compElements.Count)
      {
        var errorMessage = "The count of child elements in the base Xml is " + baseElements.Count.ToString() + " and the count " +
                           "of child elements in the compare Xml is " + compElements.Count.ToString() + ".";
        rpt.AddErrorToLine(lineNumber, new XmlLineError(level, errorMessage, direction));
      }

      foreach (var kvpBase in baseElements)
      {
        if (compElements.ContainsKey(kvpBase.Key))
        {
          var baseElement = kvpBase.Value.XElement;
          var compElement = compElements[kvpBase.Key].XElement;
          CompareElements(baseElement, compElement, xMapHelper, rpt, direction, lineNumber, level);
        }
      }
    }

    public static bool IsXMappedValueEquivalent(this string baseValue, string compValue, Type type, XmlReport rpt, Direction direction, int lineNumber, int level)
    {
      if (type == null)
        throw new Exception("The 'type' parameter is null.");

      if (baseValue == null && compValue == null)
        return true;

      var typeName = type.Name.ToString();

      if (typeName == "Nullable`1")
      {
        var underlyingType = Nullable.GetUnderlyingType(type);
        typeName = underlyingType.Name.ToString();
      }

      switch (typeName)
      {
        case "TimeSpan":
          return baseValue.IsTimeSpanEquivalent(compValue, rpt, direction, lineNumber, level);

        case "DateTime":
          return baseValue.IsDateTimeEquivalent(compValue, rpt, direction, lineNumber, level);

        case "Decimal":
          return baseValue.IsDecimalEquivalent(compValue, rpt, direction, lineNumber, level);

        case "Boolean":
          return baseValue.IsBooleanEquivalent(compValue, rpt, direction, lineNumber, level);

        case "Int32":
          return baseValue.IsInt32Equivalent(compValue, rpt, direction, lineNumber, level);

        // all other types, but particularly (based on use) strings and enums
        default:
          if (baseValue != compValue)
          {
            string errorMessage = "The base value '" + baseValue + "' does not equal the compare value '" + compValue + "'.";
            rpt.AddErrorToLine(lineNumber, new XmlLineError(level, errorMessage, direction));
            return false;
          }

          return true;
      }
    }

    public static bool IsInt32Equivalent(this string baseXml, string compXml, XmlReport rpt, Direction direction, int lineNumber, int level)
    {
      bool isIntEquivalent = true;

      int baseInt = 0;
      int compInt = 0;
      bool baseIs = false;
      bool compIs = false;
      decimal compDec = 0M;
      decimal baseDec = 0M;

      if (baseXml.IsBlank() && compXml.IsBlank())
        return isIntEquivalent = true;

      if (baseIs = baseXml.IsValidInteger())
      {
        baseInt = baseXml.ToInt32();
        if (baseIs = baseXml.IsDecimalPointZero())
        {
          baseDec = baseXml.ToDecimal();
          baseInt = baseDec.ToInt32();
        }
      }
      else
      {
        string errorMessage = "The base value '" + baseXml + "' is not a valid integer or it  has a decimal place that is something other than zero.";
        rpt.AddErrorToLine(lineNumber, new XmlLineError(level, errorMessage, direction));
        return isIntEquivalent = false;
      }

      if (compIs = compXml.IsValidInteger())
      {
        compInt = compXml.ToInt32();
        if (compIs = compXml.IsDecimalPointZero())
        {
          compDec = compXml.ToDecimal();
          compInt = compDec.ToInt32();
        }
      }
      else
      {
        string errorMessage = "The compare value '" + compXml + "' is not a valid integer or it  has a decimal place that is something other than zero.";
        rpt.AddErrorToLine(lineNumber, new XmlLineError(level, errorMessage, direction));
        return isIntEquivalent = false;
      }

      if(!baseInt.Equals(compInt))
      {
        string errorMessage = "The base value '" + baseXml + "' does not equal the compare value '" + compXml + "'.";
        rpt.AddErrorToLine(lineNumber, new XmlLineError(level, errorMessage, direction));
        return isIntEquivalent = false;
      }

      return isIntEquivalent;
    }

    public static bool IsBooleanEquivalent(this string baseXml, string compXml, XmlReport rpt, Direction direction, int lineNumber, int level)
    {
      if (baseXml.IsBlank() && compXml.IsBlank())
        return true;

      bool isBaseABool = baseXml.IsBoolean();
      bool isCompABool = compXml.IsBoolean();

      if (!isBaseABool || !isCompABool)
      {
        string errorMessage = "The base boolean value '" + baseXml + "' and/or the compare boolean value '" + compXml + "', " + "are not logical boolean value(s).";
        rpt.AddErrorToLine(lineNumber, new XmlLineError(level, errorMessage, direction));
        return false;
      }

      bool baseBool = baseXml.ToBoolean();
      bool compBool = compXml.ToBoolean();

      if (baseBool != compBool)
      {
        string errorMessage = "The base boolean value '" + baseBool + "' does not equal the compare bool '" + compBool + "'.";
        rpt.AddErrorToLine(lineNumber, new XmlLineError(level, errorMessage, direction));
        return false;
      }

      return true;
    }

    public static bool IsDecimalEquivalent(this string baseXml, string compXml, XmlReport rpt, Direction direction, int lineNumber, int level)
    {
      bool isDecimalEquivalent = true;

      if (baseXml.IsBlank() && compXml.IsBlank())
        return isDecimalEquivalent = true;

      bool isBaseDec = baseXml.IsDecimal();
      bool isCompDec = compXml.IsDecimal();
      decimal baseDecimal = 0M;
      decimal compDecimal = 0M;

      if (isBaseDec && isCompDec)
      {
        baseDecimal = baseXml.ToDecimal();
        compDecimal = compXml.ToDecimal();

        if(!baseDecimal.Equals(compDecimal))
        {
          string errorMessage = "The base decimal value '" + baseDecimal + "' does not equal the compare decimal '" + compDecimal + "'.";
          rpt.AddErrorToLine(lineNumber, new XmlLineError(level, errorMessage, direction));
          return isDecimalEquivalent = false;
        }
      }

      if((!isBaseDec && isCompDec)||(isBaseDec && !isCompDec)||(!isBaseDec && !isCompDec))
      {
        string errorMessage = "The base decimal value '" + baseDecimal + "' and/or the compare decimal value '" + compDecimal + "' are not valid decimal values.";
        rpt.AddErrorToLine(lineNumber, new XmlLineError(level, errorMessage, direction));
        return isDecimalEquivalent = false;
      }

      return isDecimalEquivalent;
    }

    public static bool IsTimeSpanEquivalent(this string baseXml, string compXml, XmlReport rpt, Direction direction, int lineNumber, int level)
    {
      bool isTimeSpanEquivalent = true;

      if (baseXml.IsBlank() && compXml.IsBlank())
        return isTimeSpanEquivalent =true;

      string baseXmlTimeSpan = baseXml == null ? String.Empty : baseXml.Trim();
      string compXmlTimeSpan = compXml == null ? String.Empty : compXml.Trim();

      if (baseXmlTimeSpan == compXmlTimeSpan)
        return isTimeSpanEquivalent = true;

      TimeSpan baseTimeSpan = TimeSpan.MinValue;
      TimeSpan compTimeSpan = TimeSpan.MinValue;

      if (!TimeSpan.TryParse(baseXml, out baseTimeSpan)||(!TimeSpan.TryParse(compXml, out compTimeSpan)))
      {
        string errorMessage = "The base time span value '" + baseTimeSpan + "' and/or the compare time span value '" +
                              compTimeSpan + "' did not parse to a valid time span value.";
        rpt.AddErrorToLine(lineNumber, new XmlLineError(level, errorMessage, direction));
        return isTimeSpanEquivalent = false;
      }

      if(!baseTimeSpan.Equals(compTimeSpan))
      {
        string errorMessage = "The base time span value '" + baseTimeSpan.ToString() + "' does not equal the comp time span value '" + compTimeSpan.ToString() + "'.";
        rpt.AddErrorToLine(lineNumber, new XmlLineError(level, errorMessage, direction));
        return isTimeSpanEquivalent = false;
      }

      return isTimeSpanEquivalent;
    }

    public static bool IsDateTimeEquivalent(this string baseXml, string compXml, XmlReport rpt, Direction direction, int lineNumber, int level)
    {
      bool isDateTimeEquivalent = true;

      if (baseXml.IsBlank() && compXml.IsBlank())
        return isDateTimeEquivalent = true;

      string baseXmlString = baseXml == null ? String.Empty : baseXml.Trim();
      string compXmlString = compXml == null ? String.Empty : compXml.Trim();

      if (baseXmlString == compXmlString)
        return isDateTimeEquivalent = true;

      DateTime baseDateTime = DateTime.MinValue;
      DateTime compDateTime = DateTime.MinValue;

      if (!DateTime.TryParse(baseXml, out baseDateTime)||(!DateTime.TryParse(compXml, out compDateTime)))
      {
        string errorMessage = "The base date time value '" + baseDateTime.ToString() + "' and/or the compare date timevalue '" +
                              compDateTime.ToString() + "' did not parse to a valid date time value.";
        rpt.AddErrorToLine(lineNumber, new XmlLineError(level, errorMessage, direction));
        return isDateTimeEquivalent = false;
      }

      if(!baseDateTime.Equals(compDateTime))
      {
        string errorMessage = "The base date time value '" + baseDateTime.ToString() + "' does not equal the comp date time value '" +
                              compDateTime.ToString() + "'.";
        rpt.AddErrorToLine(lineNumber, new XmlLineError(level, errorMessage, direction));
        return isDateTimeEquivalent = false;
      }

      return isDateTimeEquivalent;
    }

    public static void RemoveByName(this List<PropertyInfo> piSet, string name)
    {
      if (piSet == null || piSet.Count == 0 || name.IsBlank())
        return;

      PropertyInfo piToRemove = null;

      foreach (var pi in piSet)
      {
        if (pi.Name == name)
        {
          piToRemove = pi;
          break;
        }
      }

      if (piToRemove != null)
        piSet.Remove(piToRemove);
    }

    [DebuggerStepThrough]
    public static string FirstLine(this string s, int maxLength = -1)
    {
      if (s.IsBlank())
        return String.Empty;

      string firstLine = String.Empty;

      int pos = s.IndexOfAny(Constants.CrlfDelimiters);
      if (pos == -1)
        firstLine = s;
      else
        firstLine = s.Substring(0, pos);

      if (maxLength < 0)
        return firstLine;

      return firstLine.TrimToMax(maxLength);
    }

    public static string ListToString(this List<string> list)
    {
      if (list == null || list.Count == 0)
        return String.Empty;

      string listString = String.Empty;
      foreach (var listItem in list)
      {
        if (listString.IsBlank())
        {
          listString = listItem.Trim();
        }
        else
        {
          listString += "," + listItem.Trim();
        }
      }

      return listString;
    }

    public static bool IsGenericCollection(this Type type)
    {
      if (type == null)
        return false;

      if (type.FullName.Contains("System.Collections.Generic."))
        return true;

      return false;
    }

    public static bool IsGenericCollection(this object o)
    {
      if (o == null)
        return false;

      return o.GetType().IsGenericCollection();
    }

    public static Type[] GetGenericArguments(this object o)
    {
      if (o == null)
        return null;

      return o.GetType().GetGenericArguments();
    }

    public static bool IsGenericCollection(this PropertyInfo pi)
    {
      if (pi ==  null)
        return false;

      return pi.GetType().IsGenericCollection();
    }

    public static bool CompareValues(this string value, object compareValue, DataTypeSpec dataTypeSpec, DetailSpecSwitch detailSpecSwitch)
    {
      if (value.IsBlank())
        return false;

      if (compareValue == null)
        throw new Exception("The compareValue parameter is null.");

      string compValue = compareValue.ToString();

      switch (dataTypeSpec)
      {
        case DataTypeSpec.String:


        case DataTypeSpec.Integer:
          int intValue = -1;
          if (!Int32.TryParse(compValue, out intValue))
            throw new Exception("The value '" + compValue + "' cannot be parsed to an integer.");
          return value.CompareNumericValues(intValue, detailSpecSwitch);

        case DataTypeSpec.Decimal:
        case DataTypeSpec.DecimalWithRequiredDecimalPoint:
          decimal decValue = -1;
          if (!Decimal.TryParse(compValue, out decValue))
            throw new Exception("The value '" + compValue + "' cannot be parsed to a decimal.");
          return value.CompareNumericValues(decValue, detailSpecSwitch);


        default:
          throw new NotImplementedException("The DataTypeSpec '" + dataTypeSpec.ToString() + "' is not yet implemented in the extension method CompareValue.");
      }
    }

    public static bool CompareNumericValues(this string value, decimal compareValue, DetailSpecSwitch detailSpecSwitch)
    {
      if (value.IsBlank())
        return false;

      decimal decValue = -1;
      if (!decimal.TryParse(value, out decValue))
        return false;

      switch (detailSpecSwitch)
      {
        case DetailSpecSwitch.ValueGreaterThan:
          return decValue > compareValue;
        case DetailSpecSwitch.ValueGreaterThanOrEqualTo:
          return decValue >= compareValue;
        case DetailSpecSwitch.ValueEquals:
          return decValue == compareValue;
        case DetailSpecSwitch.ValueLessThan:
          return decValue < compareValue;
        case DetailSpecSwitch.ValueLessThanOrEqualTo:
          return decValue <= compareValue;

        default:
          throw new NotImplementedException("Comparison using DetailSpecificationSwitch value '" + detailSpecSwitch.ToString() + "' is not yet implemented.");
      }
    }

    public static void InvokeAutoInit(this object o)
    {
      if (o == null)
        return;

      MethodInfo mi = o.GetType().GetMethod("AutoInit");

      if (mi != null)
        mi.Invoke(o, null);
    }

    public static List<string> TrimItems(this List<string> list)
    {
      var newList = new List<string>();

      if (list == null || list.Count == 0)
        return newList;

      foreach (var item in list)
        newList.Add(item.Trim());

      return newList;
    }


    public static XMap GetXMap(this Type type)
    {
      if (type == null)
        return null;

      return type.GetCustomAttribute(typeof(XMap)) as XMap;
    }

    public static XMap GetXMap(this PropertyInfo pi)
    {
      if (pi == null)
        return null;

      var xMap = pi.GetCustomAttribute<XMap>();

      if (xMap == null)
        return null;

      return xMap;
    }

    public static bool HasXMap(this object o)
    {
      if (o == null)
        return false;

      var xMap = o.GetType().GetXMap();

      return xMap != null;
    }

    public static bool HasXMap(this PropertyInfo pi)
    {
      if (pi == null)
        return false;

      return pi.GetCustomAttribute<XMap>() != null;
    }

    public static XObjectType GetXObjectType(this Type type)
    {
      if (type == null)
        return XObjectType.Null;

      if (type.Name == "XElement")
        return XObjectType.XElement;

      if (type.IsDerivedFromGenericCollection())
        return XObjectType.GenericCollectionBased;

      XMap xMap = type.GetXMap();
      if (xMap != null)
        return XObjectType.Complex;

      return XObjectType.Simple;
    }

    public static XMap XMap(this object o)
    {
      if (o == null)
        return null;

      return ((System.Reflection.MemberInfo)o).GetCustomAttribute<XMap>();
    }

    public static bool IsXElement(this object o)
    {
      return o?.XMap().XType == XType.Element;
    }

    public static string TypeName(this PropertyInfo pi)
    {
      return pi.GetType().Name;
    }

    public static bool PropTypeIsObject(this PropertyInfo pi)
    {
      return pi.TypeName() == "Object";
    }

    public static XMap XMap(this PropertyInfo pi)
    {
      if (pi == null)
        return null;

      return pi.GetCustomAttribute<XMap>();
    }

    public static XMap PropTypeXMap(this PropertyInfo pi)
    {
      if (pi == null)
        return null;

      return pi.GetType().GetCustomAttribute<XMap>();
    }

    public static XObjectType GetXObjectType(this PropertyInfo pi, object value = null)
    {
      if (pi == null)
        return XObjectType.Null;

      if (pi.PropertyType.Name == "Object" && value != null)
      {
        if (value.GetType()?.XMap().XType == XType.Element)
          return XObjectType.Complex;
      }

      return pi.PropertyType.GetXObjectType();
    }

    public static List<PropertyInfo> GetXMapProperties(this Type t)
    {
      List<PropertyInfo> xmapPiList = new List<PropertyInfo>();
      List<PropertyInfo> piList = t.GetProperties().ToList();

      foreach (PropertyInfo pi in piList)
      {
        XMap propXMap = (XMap)pi.GetCustomAttributes(typeof(XMap), true).ToList().FirstOrDefault();
        if (propXMap != null)
          xmapPiList.Add(pi);
      }

      // If the elements are specified to be sequenced
      var elementSequence = t.GetCustomAttributes(typeof(XElementSequence), true).ToList().FirstOrDefault();
      if (elementSequence != null)
      {
        var ns = ((XElementSequence)elementSequence).NameSequence;  // grab the sequence dictionary from the custom attribute
        var piSeq = new SortedList<int, PropertyInfo>();            // set up a sorted list to sequence the PIs
        var attrList = new List<PropertyInfo>();                    // set up a list to hold any attributes (unsequenced)
        // loop through the XMapped PIs
        foreach (var pi in xmapPiList)
        {
          // determine the name (the property name or the override in the property XMap
          XMap piXMap = (XMap)pi.GetCustomAttributes(typeof(XMap), true).ToList().First();
          if (piXMap.XType == XType.Element)
          {
            string name = pi.Name;
            if (piXMap.Name.IsNotBlank())
              name = piXMap.Name;
            // if any elements are sequenced, they all must be
            if (!ns.ContainsValue(name))
              throw new Exception("The element named '" + name + "' is not found in the XElementSequence values for type '" + t.Name + "'.");
            // add the element to the sorted list
            int seq = ns.Where(e => e.Value == name).First().Key;
            piSeq.Add(seq, pi);
          }
          else
          {
            // attributes get collected here
            attrList.Add(pi);
          }
        }

        // create the new ordered collection
        var sequencedPiList = new List<PropertyInfo>();
        foreach (var kvp in piSeq)
          sequencedPiList.Add(kvp.Value);
        // add the attributes (if any) at the end
        sequencedPiList.AddRange(attrList);
        return sequencedPiList;
      }

      return xmapPiList;
    }

    public static string PiXMapName(this PropertyInfo pi)
    {
      if (pi == null)
        return String.Empty;

      var xMap = pi.XMap();
      if (xMap != null)
      {
        if (xMap.Name.IsNotBlank())
          return xMap.Name;
      }

      return pi.Name;
    }

    public static string TypeXMapName(this Type t)
    {
      var xMap = t.XMap();
      string xMapName = xMap.Name;

      if (xMapName.IsNotBlank())
        return xMapName;

      return t.Name;
    }

    public static List<PropertyInfo> GetXMapProperties(this Type type, XType xType = XType.All)
    {
      List<PropertyInfo> xmapPiList = new List<PropertyInfo>();
      var piSet = type.GetProperties().ToList();

      foreach (PropertyInfo pi in piSet)
      {
        if (pi.HasXMap())
        {
          XMap xMapProp = pi.GetXMap();
          if (xType == XType.All)
            xmapPiList.Add (pi);
          else
          {
            if (xMapProp.XType == xType)
              xmapPiList.Add(pi);
          }
        }
      }

      return xmapPiList;
    }

    public static List<PropertyInfo> GetIsDbColumnProperties(this Type type)
    {
      List<PropertyInfo> dbColumnProperties = new List<PropertyInfo>();
      var piSet = type.GetProperties().ToList();

      foreach (PropertyInfo pi in piSet)
      {
        var dbColumnAttribute = pi.GetCustomAttribute<IsDbColumn>();
        if (dbColumnAttribute != null)
          dbColumnProperties.Add(pi);
      }

      return dbColumnProperties;
    }

    public static XObject GetPropertyValue(this XElement x, PropertyInfo pi, Type objectType)
    {
      if (pi == null || x == null)
        return null;

      var xMap = pi.GetXMap();

      if (xMap == null)
        throw new Exception("The property '" + pi.Name + "' does not have an XMap attribute - xml being processed is '" + x.ToString().PadToLength(35).Trim() + "'.");

      string propertyName = pi.GetPropertyName();

      XObject propertyValue = null;

      if (xMap.XType == XType.Attribute)
      {
        propertyValue = x.Attribute(propertyName);
      }
      else
      {
        if (xMap.IsObject)
        {
          if (x.Elements().Count() > 1)
            throw new Exception("When XMap.IsObject is true, there cannot be more than one xml element present. Found " + x.Elements().Count().ToString() +
                                " elements when processing object of type '" + objectType.Name + "'.");
          propertyValue = x.Elements().FirstOrDefault();
        }
        else
        {
          propertyValue = x.Element(propertyName);
        }
      }

      return propertyValue;
    }

    public static string GetPropertyName(this PropertyInfo pi)
    {
      if (pi == null)
        return String.Empty;

      string propertyName = pi.Name;
      var xMap = pi.GetXMap();
      if (xMap != null && xMap.Name.IsNotBlank())
        propertyName = xMap.Name;

      return propertyName;
    }

    public static int ToNearestQuarterHour(this int m)
    {
      int min = 0;

      if(m <= 59 && m >= 45)
        min = 45;
      if(m < 45 && m >= 30)
        min = 30;
      if(m < 30 && m >= 15)
        min = 15;
      if(m < 15)
        min = 0;

      return min;
    }

    public static string ToQuotedString(this string value)
    {
      if (value == null)
        return null;

      if (value == "")
        return value;

      string s = value.Replace("'", "''");
      return s;
    }

    public static object RunXPathQuery(this XElement xml, string q, bool returnException = false)
    {
      try
      {
        var extractRoot = new XElement("QueryResult");
        string ns = xml.Name.NamespaceName;

        if (xml == null)
          return extractRoot;

        using (var sr = new System.IO.StringReader(xml.ToString()))
        {
          var xDoc = new XmlDocument();
          xDoc.Load(sr);

          XmlNamespaceManager nsm = null;
          if (ns.IsNotBlank())
          {
            nsm = new XmlNamespaceManager(xDoc.NameTable);
            nsm.AddNamespace("ns", ns);
          }

          var nodes = xDoc.SelectNodes(q, nsm);

          foreach (var node in nodes)
          {
            var xmlNode = (XmlNode)node;
            switch (xmlNode.NodeType)
            {
              case XmlNodeType.Element:
                extractRoot.Add(((XmlElement)xmlNode).ToXElement());
                break;

              case XmlNodeType.Attribute:
                throw new Exception("Attribute extraction is not yet implemented in method 'RunXPathQuery'.");

              case XmlNodeType.Text:
                extractRoot.Add(((XmlText)node).ToXText());
                break;
            }
          }
        }

        return extractRoot;
      }
      catch (Exception ex)
      {
        if (returnException)
          return ex;

        throw new Exception();
      }
    }

    [DebuggerStepThrough]
    public static XElement ToXElement(this XmlElement e)
    {
      if (e == null)
        return null;

      return XElement.Parse(e.OuterXml);
    }


    public static XText ToXText(this XmlText t)
    {
      if (t == null)
        return null;

      var XText = new XText(t.Value);

      return XText;
    }


    public static Dictionary<string, string> ToParmDictionary(this string s)
    {
      var parmDictionary = new Dictionary<string, string>();

      if (s.IsBlank())
        return parmDictionary;

      var parmSet = s.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);
      if (parmSet.Length == 0)
        return parmDictionary;

      foreach (var parm in parmSet)
      {
        var tokens = parm.Split(Constants.EqualsDelimiter, StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length == 2)
        {
          if (!parmDictionary.ContainsKey(tokens[0].Trim()))
          {
            parmDictionary.Add(tokens[0].Trim(), tokens[1].Trim());
          }
        }
      }

      return parmDictionary;
    }

    public static List<string> ClassNames(this Type[] types)
    {
      var classNames = new List<string>();

      if (types == null || types.Length == 0)
        return classNames;

      foreach (var type in types)
        classNames.Add(type.Name);

      return classNames;
    }
  }
}
