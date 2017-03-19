using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using Org.GS;

namespace Org.GS.TextProcessing
{
  public static class ExtensionMethods
  {
    //[DebuggerStepThrough]
    public static List<Text> Decompose(this Text value, char[] delimiter)
    {
      if (value == null)
        return new List<Text>();

      if (value.RawText.IsBlank())
        return new List<Text>();

      return value.RawText.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).StringArrayToTextList(value); 
    }

    public static string GetTextBefore(this string value, char[] c)
    {
      if (value.IsBlank())
        return String.Empty;

      if (c == null || c.Length == 0)
        return value;

      int pos = value.IndexOfAny(c);
      if (pos == -1)
        return value;

      return value.Substring(0, pos); 
    }

    public static string GetTextAfter(this string value, char[] chars)
    {
      if (value.IsBlank())
        return String.Empty;

      if (chars == null)
        return String.Empty;

      int pos = 0;
      int index = 0;

      while (true)
      {
        if (index > chars.Length - 1)
          break;

        pos = value.IndexOf(chars[index], pos);
        if (pos == -1)
          return String.Empty;

        index++;
      }

      if (pos == -1)
        return String.Empty;

      if (pos > value.Length - 1)
        return String.Empty;

      return value.Substring(pos + 1); 
    }

    public static string GetTextBetween(this string value, char[] leftChar, char[] rightChar)
    {
      if (value.IsBlank())
        return String.Empty;

      if (leftChar == null || leftChar.Length == 0)
        return String.Empty;

      if (rightChar == null || rightChar.Length == 0)
        return String.Empty;

      int left = value.IndexOfAny(leftChar);
      if (left == -1)
        return String.Empty;

      int right = value.LastIndexOfAny(rightChar);
      if (right == -1)
        return String.Empty;

      if (right <= left)
        return String.Empty;

      string betweenChars = value.Substring(left + 1, (right - left) - 1);

      return betweenChars;
    }

    public static string ParmValue(this object[] parms, int index)
    {
      if (parms == null || parms.Length == 0)
        return String.Empty;

      if (parms[index] == null)
        return String.Empty;

      return parms[index].ToString();
    }

    public static List<Text> StringArrayToTextList(this string[] array, Text parent)
    {
      if (array == null)
        return new List<Text>();

      var list = new List<Text>();
      for (int i = 0; i < array.Length; i++)
        list.Add(new Text(array[i].Trim(), parent));

      return list;
    }

    public static string TextListToString(this List<Text> textList)
    {
      if (textList == null)
        return String.Empty;

      return String.Join("\n", textList.Select(t => t.RawText));
    }

    public static string RemoveExtraSpacesAndLines(this string str, bool replaceTabsWithConstantReplacement = false)
    {
      if (str.IsBlank())
        return String.Empty;

      char lastChar = '\n';

      var sb = new StringBuilder();
      foreach (var c in str)
      {
        int intValue = (int)c;

        if (intValue < 9 || intValue == 11 || intValue == 12 || (intValue > 13 && intValue < 32) || intValue > 126)
          continue;

        if (c == '\r')
          continue;

        if (c == '\t')
        {
          if (replaceTabsWithConstantReplacement)
            sb.Append(Constants.TabReplacementCharacter);
          else if (lastChar != '\n' && lastChar != ' ')
          {
            sb.Append(' ');
            lastChar = ' ';
          }
          continue;
        }

        if (c == ' ')
        {
          if (lastChar == '\n' || lastChar == ' ')
          {
            lastChar = c;
            continue;
          }
        }

        if (c == '\n')
        {
          if (lastChar == ' ')
            sb.Remove(sb.Length - 1, 1);

          if (lastChar == '\n')
            continue;
        }

        sb.Append(c);
        lastChar = c;
      }

      return sb.ToString();
    }

    //[DebuggerStepThrough]
    public static int IndexOf(this List<Text> lines, string searchString, MatchType matchType)
    {
      if (lines == null)
        return -1;

      List<string> stringLines = lines.Select(l => l.RawText.ToLower()).ToList();
      switch (matchType)
      {
        case MatchType.Equals:
          return stringLines.IndexOf(searchString);

        case MatchType.StartsWith:
          return stringLines.FindIndex(l => l.StartsWith(searchString));

        case MatchType.EndsWith:
          return stringLines.FindIndex(l => l.EndsWith(searchString));

        default:
          throw new Exception("MatchType: " + matchType.ToString() + " has not been configured to find an index of a search string.");
      }
    }

    //[DebuggerStepThrough]
    public static int IndexOf(this List<Text> lines, string searchString, MatchType matchType, int startIndex)
    {
      if (lines == null)
        return -1;

      List<string> stringLines = lines.Select(l => l.RawText.ToLower()).ToList();
      switch (matchType)
      {
        case MatchType.Equals:
          return stringLines.FindIndex(startIndex, l => l == searchString);

        case MatchType.StartsWith:
          return stringLines.FindIndex(startIndex, l => l.StartsWith(searchString));

        case MatchType.EndsWith:
          return stringLines.FindIndex(startIndex, l => l.EndsWith(searchString));

        default:
          throw new Exception("MatchType: " + matchType.ToString() + " has not been configured to find an index of a search string.");
      }
    }

    //[DebuggerStepThrough]
    public static int IndexOf(this List<Text> lines, string searchString, MatchType matchType, int startIndex, int count)
    {
      if (lines == null)
        return -1;

      List<string> stringLines = lines.Select(l => l.RawText.ToLower()).ToList();
      switch (matchType)
      {
        case MatchType.Equals:
          return stringLines.FindIndex(startIndex, count, l => l == searchString);

        case MatchType.StartsWith:
          return stringLines.FindIndex(startIndex, count, l => l.StartsWith(searchString));

        case MatchType.EndsWith:
          return stringLines.FindIndex(startIndex, count, l => l.EndsWith(searchString));

        default:
          throw new Exception("MatchType: " + matchType.ToString() + " has not been configured to find an index of a search string.");
      }
    }

    //[DebuggerStepThrough]
    public static int LastIndexOf(this List<Text> lines, string searchString, MatchType matchType)
    {
      if (lines == null)
        return -1;

      List<string> stringLines = lines.Select(l => l.RawText.ToLower()).ToList();
      switch (matchType)
      {
        case MatchType.Equals:
          return stringLines.LastIndexOf(searchString);

        case MatchType.StartsWith:
          return stringLines.FindLastIndex(l => l.StartsWith(searchString));

        case MatchType.EndsWith:
          return stringLines.FindLastIndex(l => l.EndsWith(searchString));

        default:
          throw new Exception("MatchType: " + matchType.ToString() + " has not been configured to find the last index of a search string.");
      }
    }

    //[DebuggerStepThrough]
    public static int LastIndexOf(this List<Text> lines, string searchString, MatchType matchType, int startIndex)
    {
      if (lines == null)
        return -1;

      List<string> stringLines = lines.Select(l => l.RawText.ToLower()).ToList();
      switch (matchType)
      {
        case MatchType.Equals:
          return stringLines.FindLastIndex(startIndex, l => l == searchString);

        case MatchType.StartsWith:
          return stringLines.FindLastIndex(startIndex, l => l.StartsWith(searchString));

        case MatchType.EndsWith:
          return stringLines.FindLastIndex(startIndex, l => l.EndsWith(searchString));

        default:
          throw new Exception("MatchType: " + matchType.ToString() + " has not been configured to find the last index of a search string.");
      }
    }

    //[DebuggerStepThrough]
    public static int LastIndexOf(this List<Text> lines, string searchString, MatchType matchType, int startIndex, int count)
    {
      if (lines == null)
        return -1;

      List<string> stringLines = lines.Select(l => l.RawText.ToLower()).ToList();
      switch (matchType)
      {
        case MatchType.Equals:
          return stringLines.FindLastIndex(startIndex, count, l => l == searchString);

        case MatchType.StartsWith:
          return stringLines.FindLastIndex(startIndex, count, l => l.StartsWith(searchString));

        case MatchType.EndsWith:
          return stringLines.FindLastIndex(startIndex, count, l => l.EndsWith(searchString));

        default:
          throw new Exception("MatchType: " + matchType.ToString() + " has not been configured to find the last index of a search string.");
      }
    }

    public static FileSystemItemSet CreateFSISet(this ZipArchive item, FileSystemItem parent)
    {
      var fsiSet = new FileSystemItemSet();

      foreach(var entry in item.Entries)
      {
        FileSystemItemSet targetFsiSet = null;
        string folderName = null;
        bool isFile = entry.Name.IsNotBlank();
        if(entry.Name.IsNotBlank())
          folderName = entry.FullName.Replace(entry.Name, String.Empty);
        if(entry.Name.IsBlank())
          folderName = entry.FullName;

        if (folderName.IsBlank())
        {
          targetFsiSet = fsiSet;
        }
        else
        {
          var folder = fsiSet.EnsureFolder(folderName);
          targetFsiSet = folder.FileSystemItemSet;
        }

        if (isFile)
        {
          var fsi = new FileSystemItem(entry.FullName);
          targetFsiSet.Add(fsi); 
        }
      }

      return fsiSet;
    }
  }
}

