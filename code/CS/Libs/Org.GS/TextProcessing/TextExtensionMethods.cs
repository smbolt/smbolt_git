using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace Org.GS.TextProcessing
{
  public static class TextExtensionMethods
  {
    public static int FindTextPosition(this Text t, Cmdx cmdx, int searchStartPos)
    {
      try
      {
        if (cmdx.UsePriorEnd)
          return t.PriorEndPosition;

        if (cmdx.TextToFind.IsBlank())
          return cmdx.PositionAtEnd ? t.TextLength - 1 : 0;

        return FindTextPosition(cmdx.ToCmdxData(t.RawText, searchStartPos));
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(61, new object[] { t, cmdx, searchStartPos, ex });
      }
    }

    public static int FindTextPosition(this CmdxData cdx)
    {
      try
      {
        if (cdx.TextToFind.Contains('['))
        {
          string textMatch = cdx.FindString();
          if (textMatch.IsNotBlank())
          {
            int tokenPos = cdx.RawText.IndexOf(textMatch, cdx.StartPos, StringComparison.CurrentCultureIgnoreCase);
            if (tokenPos != -1)
            {
              if (cdx.ExcludeLastToken)
              {
                // this always returns the beginning of the last token (same thing as start of last token)
                return tokenPos;
              }
              else
              {
                return cdx.PositionAtEndOfToken ? tokenPos + textMatch.Trim().Length : tokenPos;
              }
            }

            return -1;
          }

          return -1;
        }

        int findPos = cdx.RawText.IndexOf(cdx.TextToFind, cdx.StartPos, StringComparison.CurrentCultureIgnoreCase);

        if (cdx.IsRequired && findPos == -1)
          throw new CxException(9998, new object[] { cdx });

        if (cdx.ExcludeLastToken)
        {
          return findPos;
        }
        else
        {
          if (cdx.PositionAtEndOfToken)
            findPos += cdx.TextToFind.Length;
        }

        return findPos;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(62, new object[] { cdx, ex });
      }
    }

    public static void RemoveTokens(this Text t, Cmdx cmdx)
    {
      try
      {
        t.RemoveTokens(cmdx.TokensToRemove);
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(83, new object[] { cmdx, ex });
      }
    }

    public static string ExtractNextToken(this Text t, Cmdx cmdx)
    {
      try
      {
        string dataName = cmdx.DataName;
        string token = String.Empty;
        string specialRoutine = cmdx.SpecialRoutine.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket);
        int currPos = t.CurrPos;

        if (specialRoutine.IsNotBlank())
        {
          switch (specialRoutine.ToLower().Trim())
          {
            case "signedparen1":
              while (currPos < t.TextLength && t.RawText[currPos] == ' ' || t.RawText[currPos] == '\n')
                currPos++;
              if (currPos < t.TextLength)
              {
                char currChar = t.RawText[currPos];
                if (currChar == '(')
                {
                  int openParPos = currPos;
                  int closeParPos = t.RawText.IndexOf(')', currPos);
                  if (closeParPos == -1)
                    throw new CxException(34, new object[] { t, cmdx, currPos });
                  int tokenLength = closeParPos - openParPos + 1;
                  if (tokenLength > 15)
                    throw new CxException(35, new object[] { t, cmdx, currPos });
                  token = "-" + t.RawText.Substring(openParPos + 1, tokenLength - 2).CompressBlanksTo(0);
                  t.CurrPos = closeParPos;
                  if (t.TextLength > t.CurrPos + 1)
                  {
                    if (t.RawText[t.CurrPos + 1] == ' ' || t.RawText[t.CurrPos + 1] == '\n')
                      t.CurrPos++;
                  }
                  if (token.IsBlank() && cmdx.IsRequired)
                    throw new CxException(36, new object[] { t, cmdx, currPos });
                  return token.FormatValue(cmdx);
                }
                else
                {
                  token += currChar;
                  currPos++;
                  while (currPos < t.TextLength && t.RawText[currPos] != ' ' && t.RawText[currPos] != '\n')
                    token += t.RawText[currPos++];
                  t.CurrPos = currPos;
                  return token.FormatValue(cmdx);
                }
              }
              break;

          }
        }

        if (t.CurrPos < t.TextLength)
        {
          int beg = t.FindNextBlankOrNewLine();
          if (beg != -1)
          {
            int end = t.FindNextBlankOrNewLine(beg + 1);

            if (end > -1)
            {
              token = t.GetToken(beg + 1, end - beg).Trim();
              if (cmdx.PositionAtEnd)
                t.CurrPos = end;
              else
                t.CurrPos = beg + 1;
            }
            else
            {
              token = t.GetToken(beg);
              if (cmdx.PositionAtEnd)
                t.CurrPos = end;
              else
                t.CurrPos = beg + 1;
            }
          }
        }

        if (token.IsBlank())
        {
          if (!cmdx.IsRequired)
            return String.Empty;
          else
            throw new CxException(68, new object[] { t });
        }

        if (cmdx.TokenType.IsNotBlank())
        {
          if (!token.IsTokenType(cmdx.TokenType))
            throw new CxException(25, new object[] { cmdx, token });
        }

        return token.FormatValue(cmdx);
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(23, new object[] { cmdx, ex });
      }
    }

    public static string ExtractNextTokens(this Text t, Cmdx cmdx)
    {
      try
      {
        string dataName = cmdx.DataName;
        string token = String.Empty;
        int currPos = t.CurrPos;
        int begPos = -1;
        int lastEndPos = -1;

        if (t.CurrPos < t.TextLength)
        {
          int beg = t.FindNextBlankOrNewLine();
          if (beg != -1)
          {
            int searchPos = beg;
            begPos = beg;
            int numberOfTokens = cmdx.NumberOfTokens;
            while (numberOfTokens > 0)
            {
              int end = t.FindNextBlankOrNewLine(searchPos + 1);
              if (end == -1 && cmdx.IsRequired)
                throw new CxException(74, new object[] { t });

              token += t.RawText.Substring(searchPos + 1, end - (searchPos + 1));
              lastEndPos = end;
              numberOfTokens--;
              searchPos = end;
              if (numberOfTokens > 0)
                token += " ";
            }
          }

          if (cmdx.PositionAtEnd)
            t.CurrPos = lastEndPos;
          else
            t.CurrPos = begPos;
        }

        if (token.IsBlank())
        {
          if (!cmdx.IsRequired)
            return String.Empty;
          else
            throw new CxException(70, new object[] { t });
        }

        if (cmdx.TokenType.IsNotBlank())
        {
          if (!token.IsTokenType(cmdx.TokenType))
            throw new CxException(25, new object[] { cmdx, token });
        }

        return token.FormatValue(cmdx);
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(71, new object[] { cmdx, ex });
      }
    }

    public static string ExtractTextBefore(this Text t, Cmdx cmdx)
    {
      try
      {
        string dataName = cmdx.DataName;

        string token = String.Empty;
        int currPos = t.CurrPos;

        if (t.CurrPos < t.TextLength)
        {
          int beg = t.FindNextBlankOrNewLine();
          if (beg != -1)
          {
            var cdx = new CmdxData();
            cdx.RawText = t.RawText;
            cdx.TextToFind = cmdx.TextParmValue;
            cdx.StartPos = beg;
            int nextTokenPos = cdx.FindTextPosition();

            if (nextTokenPos == -1 && cmdx.IsRequired)
              throw new CxException(69, new object[] { t });

            token = t.GetToken(beg + 1, (nextTokenPos - (beg + 1)));

            if (cmdx.PositionAtEnd)
              t.CurrPos = nextTokenPos - 1;
            else
              t.CurrPos = beg;
          }
        }

        if (token.IsBlank())
        {
          if (!cmdx.IsRequired)
            return String.Empty;
          else
            throw new CxException(68, new object[] { t });
        }

        if (cmdx.TokenType.IsNotBlank())
        {
          if (!token.IsTokenType(cmdx.TokenType))
            throw new CxException(25, new object[] { cmdx, token });
        }

        return token.FormatValue(cmdx);
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(66, new object[] { cmdx, ex });
      }
    }

    public static string Truncate(this Text t, Cmdx cmdx)
    {
      try
      {
        var direction = cmdx.Direction;

        // START HERE

        string token = String.Empty;
        int currPos = t.CurrPos;

        if (t.CurrPos < t.TextLength)
        {
          int beg = t.FindNextBlankOrNewLine();
          if (beg != -1)
          {
            var cdx = new CmdxData();
            cdx.RawText = t.RawText;
            cdx.TextToFind = cmdx.TextParmValue;
            cdx.StartPos = beg;
            int nextTokenPos = cdx.FindTextPosition();

            if (nextTokenPos == -1 && cmdx.IsRequired)
              throw new CxException(69, new object[] { t });

            token = t.GetToken(beg + 1, (nextTokenPos - (beg + 1)));

            if (cmdx.PositionAtEnd)
              t.CurrPos = nextTokenPos - 1;
            else
              t.CurrPos = beg;
          }
        }



        if (token.IsBlank())
        {
          if (!cmdx.IsRequired)
            return String.Empty;
          else
            throw new CxException(68, new object[] { t });
        }

        if (cmdx.TokenType.IsNotBlank())
        {
          if (!token.IsTokenType(cmdx.TokenType))
            throw new CxException(25, new object[] { cmdx, token });
        }

        return token.FormatValue(cmdx);
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(66, new object[] { cmdx, ex });
      }
    }

    public static string ExtractNextLine(this Text t, Cmdx cmdx)
    {
      try
      {
        if (cmdx.Verb == Verb.ExtractNextLine && cmdx.DataName.IsBlank())
          throw new Exception("No data name is specified in the 'ExtractNextLine' command with parameters '" + cmdx.Parms.ObjectArrayToString() + "' in the Text object named '" + t.Name + "'.");

        string textDump = t.TextDump;

        string extractedLine = String.Empty;

        if (t.CurrPos < t.TextLength)
        {
          if (cmdx.AdvanceToEol)
          {
            if (t.RawText[t.CurrPos] == '\n')
            {
              t.CurrPos++;
            }
            else
            {
              while (t.RawText[t.CurrPos] != '\n')
              {
                t.CurrPos++;
                if (t.CurrPos > t.TextLength - 1)
                  break;
              }
            }

            if (t.CurrPos > t.TextLength - 1)
              return String.Empty;
          }

          int end = t.RawText.IndexOf('\n', t.CurrPos);

          if (end > -1)
          {
            extractedLine = t.RawText.Substring(t.CurrPos, end - t.CurrPos).Trim();
            t.CurrPos = end;
          }
          else
          {
            extractedLine = t.RawText.Substring(t.CurrPos).Trim();
            t.CurrPos = t.TextLength - 1;
          }
        }

        return extractedLine;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(89, new object[] { t, ex });
      }
    }

    public static void TokenizeNextLine(this Text t, Cmdx cmdx)
    {
      try
      {
        string specialRoutine = cmdx.SpecialRoutine.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket);
        t.Tokens = new string[0];
        string line = t.ExtractNextLine(cmdx);

        if (specialRoutine.IsNotBlank())
        {
          switch (specialRoutine)
          {
            case "signedparen1":
              string workLine = String.Empty;
              bool inParenthesis = false;
              foreach (char c in line)
              {
                if (inParenthesis)
                {
                  if (c == '(')
                    throw new CxException(38, new object[] { cmdx });

                  if (c == ')')
                    inParenthesis = false;

                  if (c != ' ' && c != '(' && c != ')')
                    workLine += c;
                }
                else
                {
                  if (c == ')')
                    throw new CxException(39, new object[] { cmdx });

                  if (c == '(')
                  {
                    inParenthesis = true;
                    workLine += '-';
                  }
                  else
                  {
                    workLine += c;
                  }
                }
              }

              line = workLine.CompressBlanksTo(1);
              break;

            default:
              throw new CxException(37, new object[] { cmdx });
          }
        }

        if (line.IsBlank())
          return;

        if (cmdx.NumericOnly)
          t.Tokens = line.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries).NumericTokensOnly();
        else
          t.Tokens = line.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(40, new object[] { t, cmdx, ex });
      }
    }

    public static XElement CreateElement(this Text t, Cmdx cmdx)
    {
      try
      {
        var newElement = new XElement(cmdx.ElementName);

        foreach (var kvp in cmdx.Attributes)
          newElement.Add(new XAttribute(kvp.Key, kvp.Value));

        return newElement;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(88, new object[] { t, cmdx, ex });
      }
    }

    public static string ExtractStoredToken(this Text t, Cmdx cmdx)
    {
      try
      {
        string code = cmdx.Code;

        int index = cmdx.StoredTokenIndex;

        if (index < 0)
          throw new CxException(26, new object[] { cmdx });

        bool isRequired = cmdx.IsRequired;

        if (cmdx.IsRequiredIf.IsNotBlank())
        {
          string condition = cmdx.IsRequiredIf.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket);
          if (t.LocalVariables != null && t.LocalVariables.ContainsKey(condition))
          {
            isRequired = t.LocalVariables[condition].ToBoolean();
          }
        }

        if (t.Tokens == null)
        {
          if (isRequired)
          {
            throw new CxException(27, new object[] { t, cmdx });
          }
          else return String.Empty;
        }

        if (index == 99999)
          index = t.Tokens.Length - 1;

        if (index == 99998)
        {
          string joinedTokens = t.Tokens.JoinRemainingTokens(" ");
          if (cmdx.RemoveStoredToken)
            t.Tokens = new string[0];
          return joinedTokens;
        }

        if (index > t.Tokens.Length - 1)
        {
          if (isRequired)
          {
            throw new CxException(28, new object[] { t, cmdx });
          }
          else return String.Empty;
        }

        string token = t.Tokens[index];
        string tokenType = cmdx.TokenType;

        if (tokenType.IsNotBlank())
        {
          if (!token.IsTokenType(tokenType))
          {
            if (isRequired)
            {
              throw new CxException(29, new object[] { cmdx, token, tokenType });
            }
            else return String.Empty;
          }
        }

        if (cmdx.RemoveStoredToken)
          t.Tokens = t.Tokens.RemoveItemAt(index);

        return token.FormatValue(cmdx);
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(30, new object[] { t, cmdx, ex });
      }
    }

    public static string ExtractStoredTokens(this Text t, Cmdx cmdx)
    {
      try
      {
        string code = cmdx.Code;

        int index = cmdx.StoredTokenIndex;
        int tokensToExtractCount = cmdx.StoredTokenCount;

        if (index < 0)
          throw new CxException(26, new object[] { t });

        if (tokensToExtractCount < 1)
          throw new CxException(78, new object[] { t });

        bool isRequired = cmdx.IsRequired;

        if (cmdx.IsRequiredIf.IsNotBlank())
        {
          string condition = cmdx.IsRequiredIf.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket);
          if (t.LocalVariables != null && t.LocalVariables.ContainsKey(condition))
          {
            isRequired = t.LocalVariables[condition].ToBoolean();
          }
        }

        if (t.Tokens == null)
        {
          if (isRequired)
          {
            throw new CxException(27, new object[] { t, cmdx });
          }
          else return String.Empty;
        }

        if (index + (tokensToExtractCount - 1) > t.Tokens.Length - 1)
        {
          if (isRequired)
          {
            throw new CxException(75, new object[] { t });
          }
          else return String.Empty;
        }

        int tokensRemainingToExtract = tokensToExtractCount;
        int tokenToExtract = index;
        string tokens = String.Empty;
        var tokensToRemove = new List<int>();

        while (tokensRemainingToExtract > 0)
        {
          tokensToRemove.Add(tokenToExtract);
          tokens += t.Tokens[tokenToExtract];
          tokensRemainingToExtract--;
          tokenToExtract++;

          if (tokensRemainingToExtract > 0)
            tokens += " ";
        }

        string tokenType = cmdx.TokenType;

        if (tokenType.IsNotBlank())
        {
          if (!tokens.IsTokenType(tokenType))
          {
            if (isRequired)
            {
              throw new CxException(76, new object[] { cmdx, tokens, tokenType });
            }
            else return String.Empty;
          }
        }

        int newArrayIndex = 0;
        if (cmdx.RemoveStoredToken)
        {
          var newTokensArray = new string[t.Tokens.Length - tokensToRemove.Count];
          for (int i = 0; i < t.Tokens.Length; i++)
          {
            if (!tokensToRemove.Contains(i))
            {
              newTokensArray[newArrayIndex++] = t.Tokens[i];
            }
          }

          t.Tokens = newTokensArray;
        }

        return tokens.FormatValue(cmdx);
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(30, new object[] { t, cmdx, ex });
      }
    }

    public static int FindNextBlankOrNewLine(this Text t)
    {
      if (!t.MoreText)
        return -1;

      return t.RawText.IndexOfAny(Constants.BlankOrNewLine, t.CurrPos);
    }

    public static int FindNextBlankOrNewLine(this Text t, int startPos)
    {
      if (!t.MoreText)
        return -1;

      return t.RawText.IndexOfAny(Constants.BlankOrNewLine, startPos);
    }

    public static string FormatValue(this string s, Cmdx cmdx)
    {
      try
      {
        string dataFormat = cmdx.DataFormat;
        string token = s.Trim();

        if (s.IsBlank() && token.IsBlank())
          return String.Empty;

        if (cmdx.DataFormat.IsBlank())
          return token;

        DateTime dt;
        string[] tokens;

        switch (cmdx.TokenType)
        {
          case "date":
            dt = token.ToDateTime();
            switch (cmdx.DataFormat)
            {
              case "ccyymmdd": return dt.ToCCYYMMDD();
              case "mm/dd/yyyy": return dt.ToString("MM/dd/yyyy");
            }
            break;

          case "mm/yyyy":
            tokens = token.Split(Constants.FSlashDelimiter, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length != 2 || !tokens[0].InIntegerRange(1, 12) || !tokens[1].InIntegerRange(1900, 2199))
              throw new Exception("Tokens with data type '" + cmdx.TokenType + "' must have two numeric tokens with valid month and year values. The values found " +
                                  "were '" + tokens.ObjectArrayToString() + "'.");
            dt = new DateTime(tokens[1].ToInt32(), tokens[0].ToInt32(), 1);
            switch (cmdx.DataFormat)
            {
              case "ccyymmdd": return dt.ToCCYYMMDD();
              case "mm/dd/yyyy": return dt.ToString("MM/dd/yyyy");
            }
            break;

          case "time":
            dt = token.ToDateTime();
            switch (cmdx.DataFormat)
            {
              case "h24:mm:ss": return dt.ToString("HH:MM:ss");
              case "h12:mm:ss": return dt.ToString("hh:MM:ss");

            }
            break;
        }

        throw new CxException(42, new object[] { cmdx, s });
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(41, new object[] { cmdx, s, ex });
      }
    }

    public static string[] GetTypePlaceholders(this string s)
    {
      try
      {
        string originalString = s;
        string workString = s;

        while (workString.IndexOf('[') > -1)
        {
          int openPos = workString.IndexOf('[');
          int closePos = workString.IndexOf(']', openPos > -1 ? openPos : 0);

          if (closePos == -1 || closePos < openPos)
            throw new Exception("Code string has unmatched brackets '" + originalString + "'.");

          string token = workString.Substring(openPos + 1, closePos - (openPos + 1));
          workString = workString.ReplaceFirstOccurrence("[" + token + "]", "`" + token).CompressBlanksTo(1);
        }

        string[] tokens = workString.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);

        return tokens;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(43, new object[] { s, ex });
      }
    }

    public static string FindString(this CmdxData cdx)
    {
      try
      {
        if (cdx == null || cdx.RawText.IsBlank() || cdx.OriginalCmdx == null || cdx.OriginalCmdx.Parms == null || cdx.OriginalCmdx.Parms.Length == 0)
          return String.Empty;

        string t = cdx.RawText;

        if (t.Length < cdx.StartPos)
          return String.Empty;

        string[] parms = cdx.TextToFind.GetTypePlaceholders();

        string textDump = t.ToTextDump();

        if (parms[parms.Length - 1] == "`eol")
        {
          string[] lines = t.Split(Constants.NewLineDelimiter, StringSplitOptions.RemoveEmptyEntries);

          var parmsTruncated = new string[parms.Length - 1];
          for (int i = 0; i < parmsTruncated.Length; i++)
            parmsTruncated[i] = parms[i];
          parms = parmsTruncated;

          int searchPosition = 0;
          foreach (string line in lines)
          {
            if (searchPosition + line.Length > cdx.StartPos)
            {
              string textToSearch = line;

              int charsToExclude = cdx.StartPos - searchPosition;
              if (charsToExclude < 0)
                charsToExclude = 0;

              if (charsToExclude >= textToSearch.Length)
              {
                searchPosition += line.Length + 1;
                continue;
              }

              if (charsToExclude > 0 && charsToExclude < textToSearch.Length)
                textToSearch = textToSearch.Substring(charsToExclude);

              var searchTextItem = new SearchText(textToSearch, parms);
              string foundText = searchTextItem.GetFirstMatchingString(0);
              if (foundText.IsNotBlank())
              {
                return foundText += "\n";
              }
            }

            searchPosition += line.Length + 1;
          }
        }
        else
        {
          var searchTextItem = new SearchText(t, parms);
          string foundText = searchTextItem.GetFirstMatchingString(cdx.StartPos);
          if (foundText.IsNotBlank())
            return foundText;
        }

        return String.Empty;
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(44, new object[] { cdx, ex });
      }
    }

    public static void SetExportTemplate(this Text t, Cmdx cmdx)
    {
      try
      {


      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(120, new object[] { t, ex });
      }
    }

    public static void SetVariable(this Text t, Cmdx cmdx)
    {
      try
      {
        int startPos = 0;
        int endPos = t.TextLength;

        if (cmdx.DataName.IsBlank())
          throw new CxException(63, new object[] { t, cmdx });

        if (cmdx.SubCommand.IsBlank())
          throw new CxException(64, new object[] { t, cmdx });

        int holdCurrPos = t.CurrPos;

        switch (cmdx.SubCommand)
        {
          case "find":
            int foundPos = FindTextPosition(cmdx.ToCmdxData(t.RawText, startPos));

            if (t.LocalVariables == null)
              t.LocalVariables = new Dictionary<string, string>();

            if (t.LocalVariables.ContainsKey(cmdx.DataName))
              // need to allow replacing values? - later?
              throw new CxException(65, new object[] { t, cmdx });

            if (foundPos > -1)
            {
              t.LocalVariables.Add(cmdx.DataName, "true");
            }
            else
            {
              t.LocalVariables.Add(cmdx.DataName, "false");
            }

            break;

          default:
            throw new CxException(45, new object[] { t, cmdx });
        }
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(46, new object[] { t, cmdx, ex });
      }
    }

    public static bool IsTokenType(this string value, string type)
    {
      if (value.IsBlank() || type.IsBlank())
        return false;

      switch (type.ToLower())
      {
        case "dec": return value.IsDecimal(true);
        case "decn": return value.IsDecimal(false);
        case "pct": return value.IsDecimal(false);
        case "int": return value.IsInteger();
        case "date": return value.IsValidShortDate();
        case "time": return value.IsValidTime();
        case "mm/yyyy": return value.IsValidMMSlashYYYY();
      }

      throw new CxException(47, new object[] { value, type });
    }

    public static void AssertValidDataType(this string dataType)
    {
      switch (dataType)
      {
        case "dec":
        case "bool":
          return;
      }

      throw new CxException(24, new string[] { dataType });
    }

    public static CmdxData ToCmdxData(this Cmdx cmdx, string rawText = "", int startPos = 0)
    {
      var cmdxData = new CmdxData();

      cmdxData.RawText = rawText;
      cmdxData.TextToFind = cmdx.TextToFind;
      if (cmdx.Verb == Verb.SetVariable)
        cmdxData.TextToFind = cmdx.TextParmValue;
      cmdxData.StartPos = startPos;
      cmdxData.ExcludeLastToken = cmdx.ExcludeLastToken;
      cmdxData.PositionAtEndOfToken = cmdx.PositionAtEnd;
      cmdxData.IsRequired = cmdx.IsRequired;
      cmdxData.OriginalCmdx = cmdx;
      return cmdxData;
    }

    public static string ToReport(this CxException cx)
    {
      var sb = new StringBuilder();

      CxException cxp = null;
      Exception ex = null;
      Text t = null;
      Cmdx cmdx = null;
      CmdxData cmdxData = null;
      ExtractSpec extractSpec = null;

      var sb2 = new StringBuilder();

      var otherValues = new List<string>();
      var messages = new List<string>();

      if (cx.ExParms != null && cx.ExParms.Length > 0)
      {
        foreach (var parm in cx.ExParms)
        {
          string typeName = parm.GetType().Name;
          switch (typeName)
          {
            case "ExtractSpec": extractSpec = (ExtractSpec)parm; break;
            case "Text": t = (Text)parm; break;
            case "Cmdx": cmdx = (Cmdx)parm; break;
            case "Exception": ex = (Exception)parm; break;
            case "CmdxData": cmdxData = (CmdxData)parm; break;
            case "CxException": cxp = (CxException)parm; break;
            case "String": otherValues.Add((string)parm); break;
            case "Int32": otherValues.Add(parm.ToInt32().ToString()); break;
            default:
              if (parm.GetType().IsSubclassOf(typeof(Exception)))
              {
                ex = (Exception)parm;
                break;
              }

              if (sb2.Length > 0)
                sb2.Append(g.crlf);
              sb2.Append("Unhandled parm of type '" + typeName + "' encountered - value is '" + parm.ToString() + "'.");
              break;
          }
        }
      }


      if (cmdx == null && t != null && t.Cmdx != null)
        cmdx = t.Cmdx;

      sb.Append(cx.Message + g.crlf2);

      if (cmdx != null)
        sb.Append(cmdx.ToExceptionReport());

      if (t != null)
        sb.Append(t.ToExceptionReport());

      if (cmdxData != null)
        sb.Append(cmdxData.ToExceptionReport());

      if (extractSpec != null)
        sb.Append("Need to build ExtractSpec.ToExceptionReport extension method." + g.crlf2);

      if (ex != null)
        sb.Append("Native Exception Report" + g.crlf2 + ex.ToReport() + g.crlf2);

      string report = sb.ToString();
      return report;
    }

    public static string ToExceptionReport(this Text t)
    {
      var sb = new StringBuilder();

      sb.Append("Text   " + t.Name + g.crlf2 +
        t.First50 + g.crlf2 +
        t.AreaOfCurrPos);

      sb.Append(g.crlf2);
      string report = sb.ToString();
      return report;
    }

    public static string ToExceptionReport(this Cmdx c)
    {
      var sb = new StringBuilder();

      sb.Append("Cmdx Object" + g.crlf);
      sb.Append("  Code:              " + c.Code + g.crlf +
                "  Verb:              " + c.Verb.ToString() + g.crlf +
                "  ParmString:        " + c.ParmString + g.crlf +
                "  DataName:          " + c.DataName + g.crlf +
                "  DataType:          " + c.DataType + g.crlf +
                "  TokenType:         " + c.TokenType + g.crlf +
                "  IsRequired:        " + c.IsRequired.ToString() + g.crlf +
                "  IsRequiredIf:      " + c.IsRequiredIf + g.crlf +
                "  NumericOnly:       " + c.NumericOnly.ToString() + g.crlf +
                "  PositionAtEnd:     " + c.PositionAtEnd.ToString() + g.crlf +
                "  ExcludeLastToken:  " + c.ExcludeLastToken.ToString() + g.crlf +
                "  AdvanceToEol:      " + c.AdvanceToEol.ToString() + g.crlf +
                "  RemoveStoredToken: " + c.RemoveStoredToken.ToString() + g.crlf +
                "  TextParmValue:     " + c.TextParmValue + g.crlf +
                "  StoredTokenIndex:  " + c.StoredTokenIndex.ToString() + g.crlf +
                "  Sub-Command:       " + c.SubCommand + g.crlf +
                "  TextToFind:        " + c.TextToFind + g.crlf +
                "  UsePriorEnd:       " + c.UsePriorEnd.ToString() + g.crlf +
                "  SpecialRoutine:    " + c.SpecialRoutine + g.crlf +
                g.crlf2
                );

      string report = sb.ToString();
      return report;
    }

    public static string ToExceptionReport(this CmdxData cdx)
    {
      var sb = new StringBuilder();

      sb.Append("CmdxData Object" + g.crlf);
      sb.Append("  IsRequired:        " + cdx.IsRequired.ToString() + g.crlf +
                "  NumericOnly:       " + cdx.NumericOnly.ToString() + g.crlf +
                "  PositionAtEnd:     " + cdx.PositionAtEndOfToken.ToString() + g.crlf +
                "  ExcludeLastToken:  " + cdx.ExcludeLastToken.ToString() +
                "  TextToFind:        " + cdx.TextToFind + g.crlf2);

      if (cdx.OriginalCmdx != null)
        sb.Append(cdx.OriginalCmdx.ToExceptionReport());
      else
        sb.Append("RAW TEXT:" + g.crlf + cdx.RawText);

      sb.Append(g.crlf2);

      string report = sb.ToString();
      return report;
    }



  }




}
