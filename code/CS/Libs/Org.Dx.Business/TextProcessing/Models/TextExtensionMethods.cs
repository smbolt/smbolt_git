using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  public static class TextExtensionMethods
  {
    public static XElement SetXml(this Text t, Cmdx cmdx)
    {
      try
      {
        if (cmdx.HasNoParms)
          throw new CxException(167, t, cmdx);

        var queryResult = t.XElement.RunXPathQuery(cmdx.Parms[0]);

        if (queryResult == null && cmdx.IsRequired)
          throw new CxException(168, t, cmdx);

        if (queryResult.GetType().Name != "XElement")
          throw new CxException(169, t, cmdx);

        return (XElement)queryResult;
      }
      catch (Exception)
      {
        throw new CxException(169, t, cmdx);
      }
    }

    public static string ExtractXmlElementValue(this Text t, Cmdx cmdx)
    {
      try
      {
        if (cmdx.HasNoParms)
          throw new CxException(170, t, cmdx);

        if (cmdx.ParmCount < 2)
          throw new CxException(171, t, cmdx);

        var queryResult = t.XElement.RunXPathQuery(cmdx.Parms[1]);

        if (queryResult == null && cmdx.IsRequired)
          throw new CxException(168, t, cmdx);

        if (queryResult.GetType().Name != "XElement")
          throw new CxException(169, t, cmdx);

        string extractedValue = ((XElement)queryResult).Value.Trim();

        if (extractedValue.IsBlank() && cmdx.IsRequired)
          throw new CxException(172, t, cmdx);

        return extractedValue.FormatValue(cmdx);
      }
      catch (Exception)
      {
        throw new CxException(169, t, cmdx);
      }
    }

    public static void SetVariableFromXmlElementValue(this Text t, Cmdx cmdx)
    {
      try
      {
        if (cmdx.HasNoParms)
          throw new CxException(170, t, cmdx);

        if (cmdx.ParmCount < 2)
          throw new CxException(171, t, cmdx);

        string variableName = cmdx.Parms[0].Trim();

        var queryResult = t.XElement.RunXPathQuery(cmdx.Parms[1]);

        if (queryResult == null && cmdx.IsRequired)
          throw new CxException(168, t, cmdx);

        if (queryResult.GetType().Name != "XElement")
          throw new CxException(169, t, cmdx);

        string extractedValue = ((XElement)queryResult).Value.Trim();

        if (extractedValue.IsBlank() && cmdx.IsRequired)
          throw new CxException(172, t, cmdx);

        string variableValue = extractedValue.FormatValue(cmdx);

        var variableType = cmdx.ParmCount > 2 && cmdx.Parms[2].Trim().ToLower() == "global" ? VariableType.Global : VariableType.Local;

        t.SetVariableValue(variableName, variableValue, variableType);
      }
      catch (Exception)
      {
        throw new CxException(169, t, cmdx);
      }
    }

    public static int FindTextPosition(this Text t, Cmdx cmdx, int searchStartPos)
    {
      try
      {
        if (cmdx.IsRequired && (searchStartPos < 0 || searchStartPos > t.RawText.Length - 1))
          throw new CxException(155, new object[] { t, cmdx, searchStartPos });

        int returnValue = -1;

        if (cmdx.UsePriorEnd)
        {
          returnValue = t.PriorEndPosition;
          if (cmdx.IsRequired && returnValue == -1)
            throw new CxException(156,  t, cmdx, searchStartPos );
          return t.PriorEndPosition;
        }

        if (cmdx.TextToFind.ToLower().Trim() == "[eol]")
        {
          if (cmdx.Verb == Verb.SetTextEnd)
          {
            while (t.RawText[searchStartPos] == '\n')
            {
              searchStartPos++;
              if (searchStartPos > t.RawText.Length - 2)
              {
                return -1;
              }
            }
          }

          int endOfLine = t.RawText.IndexOf("\n", searchStartPos);
          if (endOfLine > -1)
            return endOfLine;
          else
            return t.RawText.Length;
        }

        if (cmdx.TextToFind.ToLower().Trim() == "[bol]")
        {
          if (t.RawText[searchStartPos] == '\n')
          {
            return searchStartPos;
          }

          int begOfLine = t.RawText.IndexOf('\n', searchStartPos);
          if (begOfLine > -1)
            return begOfLine;
          else
            return t.RawText.Length;
        }

        if (cmdx.TextToFind.IsBlank())
        {
          if (cmdx.Verb == Verb.SetTextStart)
            return 0;

          returnValue = cmdx.PositionAtEnd ? t.TextLength - 1 : 0;
          if (cmdx.IsRequired && returnValue < 0)
            throw new CxException(157, new object[] { t, cmdx, searchStartPos });
          return returnValue;
        }

        returnValue = FindTextPosition(cmdx.ToCmdxData(t.RawText, searchStartPos));

        int tokenOffset = cmdx.TokenOffset;
        if (tokenOffset != 0)
        {
          int holdReturnValue = returnValue;
          int pos = returnValue;
          int remainingTokens = Math.Abs(tokenOffset);
          if (tokenOffset > 0)
          {
            while (remainingTokens > 0)
            {
              pos++;
              if (pos >= t.LastIndex)
                return -1;
              while (t.RawText[pos] != ' ' && t.RawText[pos] != '\n')
              {
                pos++;
                if (pos >= t.LastIndex)
                  return -1;
              }
              remainingTokens--;
            }
            returnValue = pos;
          }
          else
          {
            while (remainingTokens > 0)
            {
              pos--;
              if (pos == 0)
                return -1;
              while (t.RawText[pos] != ' ' && t.RawText[pos] != '\n')
              {
                pos--;
                if (pos < 1)
                  return -1;
              }
              remainingTokens--;
              returnValue = pos;
            }
          }

          // if we could not perform the token offset return -1 (not found)
          if (holdReturnValue == returnValue)
            return -1;
        }

        if (returnValue == -1 && cmdx.IsProcessingReportUnit)
          return -1;

        if (cmdx.IsRequired && returnValue < 0)
          throw new CxException(154, new object[] { t, cmdx, searchStartPos });

        return returnValue;
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(61, new object[] { t, cmdx, searchStartPos, ex });
      }
    }

    public static int TokenCount(this string s)
    {
      if (s.IsBlank())
        return 0;

      return s.Split(Constants.SpaceOrNewLineDelimiter, StringSplitOptions.RemoveEmptyEntries).Length;
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
              if (cdx.TextToFind.ToLower().StartsWith("[bol]"))
                return cdx.PositionAtEnd ? tokenPos + textMatch.TrimEnd().Length : tokenPos;
              else
                return cdx.PositionAtEnd ? tokenPos + textMatch.Trim().Length : tokenPos;
            }

            return -1;
          }

          return -1;
        }

        int findPos = cdx.RawText.IndexOf(cdx.TextToFind, cdx.StartPos, StringComparison.CurrentCultureIgnoreCase);

        if (findPos == -1 && cdx.IsReportUnit)
        {
          if (cdx.OriginalCmdx.OrEnd)
            return cdx.RawText.LastIndex();

          return -1;
        }

        if (findPos == -1 && cdx.OriginalCmdx.OrEnd)
          return cdx.RawText.Length - 1;

        if (cdx.IsRequired && findPos == -1)
          throw new CxException(95, cdx);

        if (cdx.PositionAtEnd)
          findPos += cdx.TextToFind.Length;

        return findPos;
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(62, new object[] { cdx, ex });
      }
    }

    public static bool IsTextFound(this CmdxData cdx)
    {
      try
      {
        string[] findTokens = cdx.TextToFind.Split(Constants.PipeDelimiter, StringSplitOptions.RemoveEmptyEntries);

        if (findTokens.Length == 0)
          return false;

        // see if there's a reason to return "false"
        // below defaults to true

        foreach (string findTokenRaw in findTokens)
        {
          string findToken = findTokenRaw.Trim();
          if (findToken.Contains("[") && findToken.Contains("]"))
            //if (findToken.StartsWith("[") && findToken.EndsWith("]")) - remove this later if it doesn't cause issues
          {
            cdx.TextToFind = findToken;
            string textFound = cdx.FindString();
            if (textFound.IsBlank())
              return false;
          }
          else
          {
            int findPos = cdx.RawText.IndexOf(findToken, cdx.StartPos, StringComparison.CurrentCultureIgnoreCase);

            if (findPos == -1)
              return false;
          }
        }

        return true;
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(62, new object[] { cdx, ex });
      }
    }

    public static void Truncate(this Text t, Cmdx cmdx)
    {
      try
      {
        string code = cmdx.Code;

        int pos = cmdx.StartPosition;
        string textToFind = cmdx.TextToFind;
        var dir = cmdx.TruncateDirection;

        if (pos == -1)
          pos = t.CurrPos;

        // if not "use current location", find the desired location
        if (textToFind != "[*]")
        {
          int foundPos = t.FindTextPosition(cmdx, pos);
          if (foundPos == -1)
            throw new CxException(187);
          pos = foundPos;
        }

        if (dir == TruncateDirection.Before)
          t.RawText = t.RawText.Substring(pos);
        else
          t.RawText = t.RawText.Substring(0, pos);

        t.CurrPos = 0;
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(181, t, cmdx, ex);
      }
    }

    public static void ReplaceText(this Text t, Cmdx cmdx)
    {
      try
      {
        if (cmdx.IsCaseSensitive)
        {
          t.ReplaceTextCaseSensitive(cmdx);
          return;
        }

        string workText = t.RawText;
        string rawText = t.RawText.ToLower();
        string textToReplace = cmdx.TextToReplace;
        if (textToReplace.In("[eol],[bol]"))
          textToReplace = "\n";
        string replacementText = cmdx.ReplacementText;
        string findText = textToReplace.ToLower();

        if (cmdx.RunAsStructureCommand)
        {
          if (findText.Length != replacementText.Length)
            throw new CxException(188, cmdx, t);
        }

        int replCount = 0;

        if (findText.Contains(" [eol]"))
          findText = findText.Replace(" [eol]", "\n");

        if (findText.Contains("[eol]"))
          findText = findText.Replace("[eol]",  "\n");

        if (findText.Contains("[bol] "))
          findText = findText.Replace("[bol] ", "\n");

        if (findText.Contains("[bol]"))
          findText = findText.Replace("[bol]", "\n");

        if (replacementText.Contains(" [eol]"))
          replacementText = replacementText.Replace(" [eol]",  "\n");

        if (replacementText.Contains("[eol]"))
          replacementText = replacementText.Replace("[eol]", " \n");

        if (replacementText.Contains("[bol] "))
          replacementText = replacementText.Replace("[bol] ", "\n ");

        if (replacementText.Contains("[bol]"))
          replacementText = replacementText.Replace("[bol]", "\n ");

        while (rawText.Contains(findText))
        {
          int beg = rawText.IndexOf(findText);

          if (beg == -1 && cmdx.IsRequired && replCount == 0)
            throw new CxException(167, t, cmdx);

          if (beg == -1 && replCount == 0)
            return;

          int end = beg + findText.Length;

          string frontPart = workText.Substring(0, beg);
          string endPart = workText.Substring(end);
          string newString = frontPart + replacementText + endPart;
          replCount++;

          if (cmdx.Trim)
            newString = newString.Trim();

          workText = newString;
          rawText = workText.ToLower();
        }

        if (replCount == 0)
          return;

        t.RawText = workText;

        if (!cmdx.RunAsStructureCommand)
          t.CurrPos = 0;
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(164, t, cmdx, ex);
      }
    }

    public static void ReplaceTextCaseSensitive(this Text t, Cmdx cmdx)
    {
      throw new CxException(168, t, cmdx);
    }

    public static void RemoveStoredTokens(this Text t, Cmdx cmdx)
    {
      try
      {
        t.RemoveStoredTokens(cmdx.TokensToRemove);
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(83, new object[] { cmdx, ex });
      }
    }

    public static string ExtractNextTokenOfType(this Text t, Cmdx cmdx)
    {
      try
      {
        string dataName = cmdx.DataName;
        string dataType = cmdx.DataType;
        string token = String.Empty;
        int currPos = t.CurrPos;

        while (currPos < t.TextLength)
        {
          int beg = t.FindNextBlankOrNewLine(currPos);
          if (beg != -1)
          {
            int end = t.FindNextBlankOrNewLine(beg + 1);
            if (end > -1)
            {
              token = t.GetToken(beg + 1, end - beg).Trim();
              if (token.IsNotBlank() && token.IsTokenType(dataType))
              {
                if (cmdx.PositionAtEnd)
                  t.CurrPos = end;
                else
                  t.CurrPos = beg + 1;
                break;
              }
              else
              {
                currPos = end == currPos ? end + 1 : end;
              }
            }
            else
            {
              token = t.GetToken(beg);
              if (token.IsNotBlank() && token.IsTokenType(dataType))
              {
                if (cmdx.PositionAtEnd)
                  t.CurrPos = end;
                else
                  t.CurrPos = beg + 1;
                break;
              }
              else
              {
                currPos = end == currPos ? end + 1 : end;
              }
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
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(121, t, cmdx, ex);
      }
    }

    public static string ExtractPriorTokenOfType(this Text t, Cmdx cmdx)
    {
      try
      {
        string dataName = cmdx.DataName;
        string dataType = cmdx.DataType;
        string token = String.Empty;
        int currPos = t.CurrPos;

        if (currPos > 0)
        {
          int beg = t.FindPrevBlankOrNewLine();
          int ptr = beg - 1;

          while (ptr > 0)
          {
            if (t.RawText[ptr] == ' ' || t.RawText[ptr] == '\n')
            {
              string text = t.RawText.Substring(ptr + 1, beg - ptr);
              if (text.IsTokenType(dataType))
              {
                if (cmdx.PositionAtEnd)
                  t.CurrPos = ptr + 1 + text.Length;
                else
                  t.CurrPos = ptr + 1;
                token = text.Trim();
                break;
              }
              else
              {
                beg = ptr - 1;
                if (beg < 1)
                  return String.Empty;

                while (t.RawText[beg] == ' ' || t.RawText[beg] == '\n')
                {
                  beg--;
                  if (beg < 1)
                    return String.Empty;
                }
                ptr = beg;
              }
            }

            ptr--;
          }
        }

        return token.FormatValue(cmdx);
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(102, new object[] { cmdx, ex });
      }
    }

    public static string ExtractPriorTokensOfType(this Text t, Cmdx cmdx)
    {
      try
      {
        if (!t.PriorText)
        {
          if (cmdx.IsRequired)
          {
            throw new CxException(95, new object[] { t, cmdx });
          }
          return String.Empty;
        }

        int numberOfTokens = cmdx.NumberOfTokens;
        string[] tokenTypes = cmdx.TokenTypes;

        if (tokenTypes.Length != numberOfTokens)
          throw new CxException(106, new object[] { t, cmdx });

        string[] tokens = new string[numberOfTokens];
        LocatedToken[] locatedTokens = new LocatedToken[numberOfTokens];
        int tokenIndex = -1;
        int pos = t.CurrPos;

        bool continueSearch = true;

        while (continueSearch)
        {
          var tsc = cmdx.TokenSearchCriteria;
          tsc.Direction = Direction.Prev;

          var token = t.GetPriorToken(pos, tsc);
          if (!token.TokenLocated)
            continueSearch = false;

          tokenIndex++;
          tokens[tokenIndex] = token.TokenText;
          locatedTokens[tokenIndex] = token;

          if (tokenIndex == tokens.Length - 1)
          {
            // reorder the tokens to avoid debugging brain damage :)
            string[] orderedTokens = new string[tokens.Length];
            LocatedToken[] orderedLocatedTokens = new LocatedToken[tokens.Length];
            int oPtr = 0;
            for (int tPtr = tokens.Length - 1; tPtr > -1; tPtr--)
            {
              orderedLocatedTokens[oPtr] = locatedTokens[tPtr];
              orderedTokens[oPtr] = tokens[tPtr];
              oPtr++;
            }

            // now see if the tokens extracted from the text satisfy the pattern specified
            bool tokensAreOfRequestedTypes = true;
            for (int i = 0; i < orderedTokens.Length; i++)
            {
              string orderedToken = orderedTokens[i].Trim();
              string tokenPattern = tokenTypes[i].Trim();

              if (tokenPattern.IsBracketed())
              {
                string tokenType = tokenPattern.GetBracketedText();
                if (!orderedToken.IsTokenType(tokenType))
                {
                  tokensAreOfRequestedTypes = false;
                  break;
                }
              }
              else
              {
                if (orderedToken.ToLower() != tokenPattern.ToLower())
                {
                  tokensAreOfRequestedTypes = false;
                  break;
                }
              }
            }

            if (tokensAreOfRequestedTypes)
            {
              int startPosition = orderedLocatedTokens[0].TokenBeginPosition;
              int endPosition = orderedLocatedTokens[orderedLocatedTokens.Length - 1].TokenEndPosition;
              int totalLength = endPosition - startPosition + 1;
              string reassembledString = t.RawText.Substring(startPosition, totalLength);

              if (cmdx.PositionAtEnd)
                t.CurrPos = endPosition;
              else
                t.CurrPos = startPosition;

              return reassembledString.FormatValue(cmdx);
            }
            else
            {
              // we're searching backward here, so we must have "gone backward" to the beginning of a token
              // that is "behind" the current position in the raw text.
              if (orderedLocatedTokens[orderedLocatedTokens.Length - 1].TokenBeginPosition < t.CurrPos)
              {
                pos = orderedLocatedTokens[orderedLocatedTokens.Length - 1].TokenBeginPosition;
                if (pos < 1)
                  return String.Empty;

                tokenIndex = -1;
                for (int i = 0; i < numberOfTokens; i++)
                {
                  tokens[i] = null;
                  locatedTokens[i] = null;
                  orderedTokens[i] = null;
                  orderedLocatedTokens[i] = null;
                }
              }
              else
              {
                return String.Empty;
              }
            }
          }
          else
          {
            pos = token.TokenBeginPosition;
          }
        }

        return String.Empty;
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(105, new object[] { cmdx, ex });
      }
    }

    public static LocatedToken GetNextToken(this Text t, int pos, TokenSearchCriteria tsc)
    {
      return new LocatedToken(t.RawText, pos, tsc, t.ExtractOptions);
    }

    public static LocatedToken GetPriorToken(this Text t, int pos, TokenSearchCriteria tsc)
    {
      return new LocatedToken(t.RawText, pos, tsc, t.ExtractOptions);
    }

    public static bool TokenMatchesPattern(this string token, string pattern, string range = "")
    {
      if (pattern.IsBlank() || pattern == "*")
        return true;

      if (token.Length != pattern.Length)
        return false;

      int tokenLength = token.Length;

      for (int i = 0; i < tokenLength; i++)
      {
        switch (pattern[i])
        {
          case 'A':
            if (!Char.IsLetter(token[i]))
              return false;
            break;

          case 'X':
            break;

          case '9':
            if (!Char.IsDigit(token[i]))
              return false;
            break;

          default:
            throw new Exception("An invalid pattern character was encountered '" + pattern[i].ToString() + "'.");
        }
      }

      if (range.IsNotBlank())
      {
        string[] r = range.ToLower().Trim().Split(Constants.PipeDelimiter, StringSplitOptions.RemoveEmptyEntries);
        if (r.ContainsEntry(token.ToLower()))
          return true;
        return false;
      }

      return true;
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

        if (cmdx.TokenType.IsNotBlank() && cmdx.Verb != Verb.SetVariable && cmdx.Verb != Verb.SetGlobalVariable)
        {
          if (!token.IsTokenType(cmdx.TokenType))
            throw new CxException(25, new object[] { cmdx, token });
        }

        return token.FormatValue(cmdx);
      }
      catch (CxException) {
        throw;
      }
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
      catch (CxException) {
        throw;
      }
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
        int numberOfTokens = cmdx.NumberOfTokens;

        string varName = cmdx.VariableName;
        bool storeValueAsVariable = varName.IsNotBlank();

        string textToFind = cmdx.TextToFind;

        string token = String.Empty;
        int currPos = t.CurrPos;

        if (t.CurrPos < t.TextLength)
        {
          int beg = t.FindNextBlankOrNewLine();

          if (beg != -1)
          {
            if (textToFind.StartsWith("$"))
            {
              string variableName = textToFind.Substring(1).Trim();
              string variableValue = t.GetVariable(variableName, true).Trim();
              if (variableValue.IsBlank())
                throw new CxException(109, cmdx, t);
              textToFind = variableValue;
            }

            if (textToFind.In("[bol],[eol]"))
            {
              int pos = t.RawText.IndexOf('\n', beg);
              token = pos == -1 ? t.RawText.Substring(beg, t.RawText.Length - beg) : t.RawText.Substring(beg, pos - beg);

              if (cmdx.PositionAtEnd)
              {
                t.CurrPos = beg + token.Length;
              }
              else
              {
                // advance to beginning of returned token if currently positioned at a blank or new line
                while (beg < t.RawText.Length && t.RawText[beg] == ' ' && t.RawText[beg] == '\n')
                  beg++;
                t.CurrPos = beg;
              }

              string tokenToReturn = token.Trim();

              if (tokenToReturn.IsBlank() && cmdx.DefaultValue.IsNotBlank())
                tokenToReturn = cmdx.DefaultValue;

              if (cmdx.IsRequired && tokenToReturn.IsBlank())
                throw new CxException(159, new object[] { t, cmdx });

              if (storeValueAsVariable && tokenToReturn.IsNotBlank())
                t.SetLocalVariable(varName, tokenToReturn);

              return tokenToReturn.FormatValue(cmdx);
            }

            var cdx = new CmdxData();
            cdx.OriginalCmdx = cmdx;
            cdx.RawText = t.RawText;
            cdx.TextToFind = textToFind;
            cdx.IsReportUnit = cmdx.IsProcessingReportUnit;
            cdx.StartPos = beg;
            int nextTokenPos = cdx.FindTextPosition();

            if (nextTokenPos == -1 && cmdx.IsRequired)
              throw new CxException(69, new object[] { t });

            token = t.GetToken(beg + 1, (nextTokenPos - (beg + 1)));

            if (numberOfTokens != -1)
            {
              string[] tokens = token.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);
              if (tokens.Length < numberOfTokens)
                throw new CxException(160, cmdx, t);

              if (tokens.Length != numberOfTokens)
              {
                string tokensValue = String.Empty;
                int tokensRemainingToInclude = numberOfTokens;
                int tokenPtr = tokens.Length - 1;
                while (tokensRemainingToInclude > 0)
                {
                  string tokenToInclude = tokens[tokenPtr--];
                  if (tokensValue.IsBlank())
                    tokensValue = tokenToInclude.Trim();
                  else
                    tokensValue = (tokenToInclude + " " + tokensValue).Trim();
                  tokensRemainingToInclude--;
                }
                token = tokensValue;
              }
            }

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

        if (storeValueAsVariable && token.IsNotBlank())
          t.SetLocalVariable(varName, token);

        return token.FormatValue(cmdx);
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(66, new object[] { cmdx, ex });
      }
    }

    public static string[] SplitByStringToken(this string s, string[] splitTokens, bool returnSplitTokenFound = false)
    {
      if (s.IsBlank() || splitTokens == null || splitTokens.Length == 0)
        return new string[0];

      if (splitTokens.Length == 1)
        return s.SplitByStringToken(splitTokens[0], returnSplitTokenFound);

      foreach (var st in splitTokens)
      {
        if (s.Contains(st))
        {
          return s.SplitByStringToken(st, returnSplitTokenFound);
        }
      }

      return new string[0];
    }

    public static string[] SplitByStringToken(this string s, string splitToken, bool returnSplitTokenFound = false)
    {
      if (s.IsBlank() || splitToken.IsBlank())
        return new string[0];

      if (!s.Contains(splitToken))
        return new string[0];

      if (s.StartsWith(splitToken) || s.EndsWith(splitToken))
        return new string[0];

      int splitTokenStart = s.IndexOf(splitToken);

      if (splitTokenStart == -1)
        return new string[0];

      string leftToken = s.Substring(0, splitTokenStart).Trim();
      int rightTokenStart = splitTokenStart + splitToken.Length;
      string rightToken = s.Substring(rightTokenStart).Trim();

      if (returnSplitTokenFound)
        return new string[3] { leftToken, splitToken, rightToken };

      return new string[2] { leftToken, rightToken };
    }

    public static void SetTextPosition(this Text t, Cmdx cmdx)
    {
      try
      {
        int currPos = t.CurrPos;

        Direction direction = cmdx.Direction;
        int unitCount = cmdx.UnitCount;
        int unitsRemainingToMove = unitCount;
        var textUnit = cmdx.TextUnit;
        var positionAt = cmdx.PositionAt;

        if (cmdx.ParmCount == 1)
        {
          if (cmdx.Parms[0] == "0")
          {
            t.CurrPos = 0;
            return;
          }

          if (cmdx.Parms[0] == "end")
          {
            t.CurrPos = t.LastIndex;
            return;
          }
        }

        if (direction == Direction.Next)
        {
          // throw exception if "already" positioned at the end of text, in all other cases,
          // return the end of text, if the next desired target can't be found.
          if (currPos > t.TextLength - 1)
            throw new CxException(120, t, cmdx);

          switch (textUnit)
          {
            case TextUnit.Character:
              // move the specified number of characters forward
              while (unitsRemainingToMove > 0)
              {
                if (currPos > t.TextLength - 2)
                {
                  t.CurrPos = t.TextLength - 1;
                  return;
                }

                currPos++;
                unitsRemainingToMove--;
              }

              t.CurrPos = currPos;
              return;

            case TextUnit.Token:
              // if we're between tokens (currently positioned on blank or new-line), move forward
              // onto the next token character, then advance from there.
              while (t.RawText[currPos] == ' ' || t.RawText[currPos] == '\n')
              {
                currPos++;
                if (currPos == t.TextLength - 1)
                {
                  t.CurrPos = currPos;
                  return;
                }
              }

              while (unitsRemainingToMove > 0)
              {
                // find next blank or new line
                while (t.RawText[currPos] != ' ' && t.RawText[currPos] != '\n')
                {
                  currPos++;
                  if (currPos > t.TextLength - 2)
                  {
                    t.CurrPos = t.TextLength - 1;
                    return;
                  }
                }

                // at this point, we should be positioned on a blank or new line
                // move forward to next non-blank or non-new-line character and
                // count this as one token moved forward.
                while (t.RawText[currPos] == ' ' || t.RawText[currPos] == '\n')
                {
                  currPos++;
                  if (currPos > t.TextLength - 2)
                  {
                    t.CurrPos = t.TextLength - 1;
                    return;
                  }
                }

                unitsRemainingToMove--;
              }

              switch (positionAt)
              {
                case PositionAt.Before:
                  // back up onto the token
                  while (t.RawText[currPos] == ' ' || t.RawText[currPos] == '\n')
                  {
                    currPos--;
                    if (currPos == 0)
                    {
                      t.CurrPos = 0;
                      return;
                    }
                  }

                  // back up to the before the beginning of the token
                  while (t.RawText[currPos] != ' ' && t.RawText[currPos] != '\n')
                  {
                    currPos--;
                    if (currPos == 0)
                    {
                      t.CurrPos = 0;
                      return;
                    }
                  }
                  break;

                case PositionAt.Start:
                  // back up onto the token
                  while (t.RawText[currPos] == ' ' || t.RawText[currPos] == '\n')
                  {
                    currPos--;
                    if (currPos == 0)
                    {
                      t.CurrPos = 0;
                      return;
                    }
                  }

                  // back up to the before the beginning of the token
                  while (t.RawText[currPos] != ' ' && t.RawText[currPos] != '\n')
                  {
                    currPos--;
                    if (currPos == 0)
                    {
                      t.CurrPos = 0;
                      return;
                    }
                  }

                  currPos++;
                  break;

                case PositionAt.End:
                  // back up onto the token
                  while (t.RawText[currPos] == ' ' || t.RawText[currPos] == '\n')
                  {
                    currPos--;
                    if (currPos == 0)
                    {
                      t.CurrPos = 0;
                      return;
                    }
                  }
                  break;

                case PositionAt.After:
                  // already positioned after the token
                  break;

              }

              t.CurrPos = currPos;
              return;

            case TextUnit.Line:
              // if we're current sitting on a line break (\n), then position forward onto
              // the first token and advance from there.
              while (t.RawText[currPos] == '\n')
              {
                currPos++;
                if (currPos == t.TextLength - 1)
                {
                  t.CurrPos = currPos;
                  return;
                }
              }

              while (unitsRemainingToMove > 0)
              {
                // find the next new line character
                while (t.RawText[currPos] != '\n')
                {
                  currPos++;
                  if (currPos > t.TextLength - 2)
                  {
                    t.CurrPos = t.TextLength - 1;
                    return;
                  }
                }

                // move forward to the first non-blank character on the new line
                while (t.RawText[currPos] == ' ' || t.RawText[currPos] == '\n')
                {
                  currPos++;
                  if (currPos > t.TextLength - 2)
                  {
                    t.CurrPos = t.TextLength - 1;
                    return;
                  }
                }

                unitsRemainingToMove--;
              }

              switch (positionAt)
              {
                case PositionAt.Before:
                  // back up off of the token
                  while (t.RawText[currPos] != ' ' && t.RawText[currPos] != '\n' && currPos > 0)
                  {
                    currPos--;
                    if (currPos == 0)
                    {
                      t.CurrPos = 0;
                      return;
                    }
                  }
                  break;

                case PositionAt.Start:
                  // already positioned at start of token
                  break;

                case PositionAt.End:
                  // go forward off the end of the token
                  while (t.RawText[currPos] == ' ' || t.RawText[currPos] == '\n')
                  {
                    currPos++;
                    if (currPos > t.TextLength - 2)
                    {
                      t.CurrPos = t.TextLength - 1;
                      return;
                    }
                  }

                  // back up onto the last character of the token
                  currPos--;
                  break;

                case PositionAt.After:
                  // go forward off the end of the token
                  while (t.RawText[currPos] == ' ' || t.RawText[currPos] == '\n')
                  {
                    currPos++;
                    if (currPos > t.TextLength - 2)
                    {
                      t.CurrPos = t.TextLength - 1;
                      return;
                    }
                  }
                  break;

              }

              t.CurrPos = currPos;
              return;
          }
        }
        else
        {
          // throw exception if "already" positioned at the start of text, in all other cases,
          // return the start of text, if the next desired target can't be found.
          if (currPos < 1)
            throw new CxException(119, t, cmdx);

          switch (textUnit)
          {
            case TextUnit.Character:
              // move the specified number of characters backward
              while (unitsRemainingToMove > 0)
              {
                if (currPos == 0)
                {
                  t.CurrPos = 0;
                  return;
                }

                currPos--;
                unitsRemainingToMove--;
              }

              t.CurrPos = currPos;
              return;

            case TextUnit.Token:
              // if we're between tokens (currently positioned on blank or new-line), move backward
              // onto the previous token character, then go back from.
              while (t.RawText[currPos] == ' ' || t.RawText[currPos] == '\n')
              {
                currPos--;
                if (currPos == 0)
                {
                  t.CurrPos = currPos;
                  return;
                }
              }

              // the goal here is to position on the first character of the requested token
              while (unitsRemainingToMove > 0)
              {
                // locate the prior blank or new-line
                while (t.RawText[currPos] != ' ' && t.RawText[currPos] != '\n')
                {
                  currPos--;
                  if (currPos == 0)
                  {
                    t.CurrPos = 0;
                    return;
                  }
                }

                unitsRemainingToMove--;
              }

              switch (positionAt)
              {
                case PositionAt.Before:
                  // already positioned before
                  break;

                case PositionAt.Start:
                  while (t.RawText[currPos] == ' ' || t.RawText[currPos] == '\n')
                  {
                    currPos++;
                    if (currPos > t.TextLength - 2)
                    {
                      t.CurrPos = t.TextLength - 1;
                      return;
                    }
                  }
                  break;

                case PositionAt.End:
                  // move forward off blank or new line
                  while (t.RawText[currPos] == ' ' || t.RawText[currPos] == '\n')
                  {
                    currPos++;
                    if (currPos > t.TextLength - 2)
                    {
                      t.CurrPos = t.TextLength - 1;
                      return;
                    }
                  }

                  // find end of token
                  while (t.RawText[currPos] != ' ' && t.RawText[currPos] != '\n')
                  {
                    currPos++;
                    if (currPos > t.TextLength - 2)
                    {
                      t.CurrPos = t.TextLength - 1;
                      return;
                    }
                  }
                  currPos--;
                  break;

                case PositionAt.After:
                  // move forward off blank or new line
                  while (t.RawText[currPos] == ' ' || t.RawText[currPos] == '\n')
                  {
                    currPos++;
                    if (currPos > t.TextLength - 2)
                    {
                      t.CurrPos = t.TextLength - 1;
                      return;
                    }
                  }

                  // find end of token
                  while (t.RawText[currPos] != ' ' && t.RawText[currPos] != '\n')
                  {
                    currPos++;
                    if (currPos > t.TextLength - 2)
                    {
                      t.CurrPos = t.TextLength - 1;
                      return;
                    }
                  }
                  break;
              }

              t.CurrPos = currPos;
              return;


            case TextUnit.Line:
              // if we're current sitting on a line break (\n), then position backward onto
              // the previous token and go back from there.
              while (t.RawText[currPos] == '\n')
              {
                currPos--;
                if (currPos == 0)
                {
                  t.CurrPos = currPos;
                  return;
                }
              }

              // the goal here is to position on the first character of the first token on the target line
              while (unitsRemainingToMove > 0)
              {
                while (t.RawText[currPos] != '\n')
                {
                  currPos--;
                  if (currPos == 0)
                  {
                    t.CurrPos = 0;
                    return;
                  }
                }

                unitsRemainingToMove--;

                if (unitsRemainingToMove > 0)
                {
                  if (t.RawText[currPos] == '\n')
                  {
                    currPos--;
                    if (currPos == 0)
                    {
                      t.CurrPos = 0;
                      return;
                    }
                  }
                }
              }

              switch (positionAt)
              {
                case PositionAt.Before:
                  // already positioned before
                  break;

                case PositionAt.Start:
                  while (t.RawText[currPos] == ' ' || t.RawText[currPos] == '\n')
                  {
                    currPos++;
                    if (currPos > t.TextLength - 2)
                    {
                      t.CurrPos = t.TextLength - 1;
                      return;
                    }
                  }
                  break;


                  // IT DOESN'T MAKE SENSE TO POSITION AT THE END OF THE LINE OR AFTER THE END OF THE LINE
              }

              t.CurrPos = currPos;
              return;
          }
        }
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(110, t, cmdx, ex);
      }
    }

    public static string GetVariable(this Text t, string variableName, bool isRequired)
    {
      if (variableName.StartsWith("$"))
      {
        string varName = variableName.Substring(1);
        if (varName.StartsWith("_"))
          return t.GetDynamicVariable(varName);

        string varValue = GetVariable(t, varName, isRequired);
        if (varValue.IsNotBlank())
          variableName = varValue;
      }

      if (t.LocalVariables != null && t.LocalVariables.ContainsKey(variableName))
        return t.LocalVariables[variableName].Trim();

      if (Text.GlobalVariables != null && Text.GlobalVariables.ContainsKey(variableName))
        return Text.GlobalVariables[variableName].Trim();

      if (isRequired)
        throw new CxException(108, t, variableName);

      return String.Empty;
    }

    public static string GetDynamicVariable(this Text t, string variableName)
    {
      int currPos = t.CurrPos;

      if (t.LocalVariables != null && t.LocalVariables.ContainsKey(variableName))
        return t.LocalVariables[variableName].Trim();

      if (Text.GlobalVariables != null && Text.GlobalVariables.ContainsKey(variableName))
        return Text.GlobalVariables[variableName].Trim();

      switch (variableName.Trim().ToLower())
      {
        case "_curr_line_tokens":
          return t.CurrentLineTokenCount().ToString();
      }

      throw new CxException(108, t, variableName);
    }

    public static int CurrentLineTokenCount(this Text t)
    {
      int ptr = t.CurrPos;

      if (ptr < 0 || ptr > t.TextLength  || t.RawText.IsBlank())
        return 0;

      while (ptr > 0 && t.RawText[ptr] != Constants.NewLineCharacter)
        ptr--;

      if (t.RawText.Length <= ptr + 1)
        return 0;

      int endOfLine = t.RawText.IndexOf(Constants.NewLineCharacter, ptr + 1);

      int lengthOfLine = endOfLine - ptr - 1;

      if (lengthOfLine == 0)
        return 0;

      if (ptr == 0)
        ptr -=1;

      string line = t.RawText.Substring(ptr + 1, lengthOfLine);

      string[] tokens = line.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);

      return tokens.Length;
    }

    public static void SetLocalVariable(this Text t, string variableName, string variableValue)
    {
      if (t == null || variableName.IsBlank() || variableValue.IsBlank())
        return;

      variableName = variableName.Trim();
      variableValue = variableValue.Trim();

      if (t.LocalVariables == null)
        t.LocalVariables = new Dictionary<string, string>();

      if (t.LocalVariables.ContainsKey(variableName))
        t.LocalVariables[variableName] = variableValue.Trim();
      else
        t.LocalVariables.Add(variableName, variableValue);
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

              if (t.CurrPos < t.TextLength - 1)
                t.CurrPos++;
            }

            if (t.CurrPos > t.TextLength - 1)
              return String.Empty;
          }

          int end = t.RawText.IndexOf('\n', t.CurrPos + 1);

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
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(89, new object[] { t, ex});
      }
    }

    public static void TokenizeNextLine(this Text t, Cmdx cmdx)
    {
      try
      {
        string zap = cmdx.Zap;
        string specialRoutine = cmdx.SpecialRoutine.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket).ToLower();

        t.Tokens = new string[0];
        string line = t.ExtractNextLine(cmdx);

        if (zap.IsNotBlank())
        {
          string[] zapStrings = zap.Split(Constants.PipeDelimiter, StringSplitOptions.RemoveEmptyEntries);
          foreach (string zapString in zapStrings)
          {
            line = line.Replace(zapString, String.Empty);
          }
        }

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

        int tokenCountRequired = cmdx.TokensRequired;

        if (tokenCountRequired != -1)
        {
          if (t.Tokens.Length != tokenCountRequired)
          {
            throw new CxException(96, new object[] { t, cmdx });
          }
        }
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(40, new object[] { t, cmdx, ex });
      }
    }

    public static string ExtractStoredToken(this Text t, Cmdx cmdx)
    {
      try
      {
        string code = cmdx.Code;
        string beforeToken = cmdx.BeforeToken;

        int index = cmdx.StoredTokenIndex;

        if (index < 0)
          throw new CxException(26, new object[] { cmdx });

        bool isRequired = cmdx.IsRequired;

        if (t.Tokens == null)
        {
          if (isRequired)
          {
            throw new CxException(27, new object[] { t, cmdx });
          }
          else return String.Empty;
        }

        // when 'last' is used as a parm, the index value is set to 99999
        if (index == 99999)
          index = t.Tokens.Length - 1;

        // when 'join' is used as a parm, it means to join the remaining tokens with a single blank between them
        if (index == 99998)
        {
          string joinedTokens = t.Tokens.JoinRemainingTokens(" ", beforeToken);
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
      catch (CxException) {
        throw;
      }
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
          throw new CxException(78, new object[] { t } );

        bool isRequired = cmdx.IsRequired;

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
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(30, new object[] { t, cmdx, ex });
      }
    }

    public static string ExtractStoredTokenBefore(this Text t, Cmdx cmdx)
    {
      try
      {
        string code = cmdx.Code;
        bool isRequired = cmdx.IsRequired;

        if (t.Tokens == null || t.Tokens.Length == 0)
        {
          if (isRequired)
          {
            throw new CxException(27, new object[] { t, cmdx });
          }
          else
          {
            return String.Empty;
          }
        }

        int indexOfFoundToken = -1;
        string textToFind = cmdx.TextToFind;
        string tokenType = String.Empty;
        string token = String.Empty;
        string returnValue = String.Empty;

        // if the value to find is a "type" of value
        if (textToFind.StartsWith("[") && textToFind.EndsWith("]"))
        {
          tokenType = cmdx.TokenType;
          for (int i = 0; i < t.Tokens.Length; i++)
          {
            token = t.Tokens[i];
            if (token.IsTokenType(tokenType))
            {
              indexOfFoundToken = i;
              break;
            }
          }
        }
        else // value to find is a literal value
        {
          for (int i = 0; i < t.Tokens.Length; i++)
          {
            token = t.Tokens[i].Trim();

            if (token.ToLower() == textToFind.ToLower())
            {
              indexOfFoundToken = i;
              break;
            }
          }
        }

        if (indexOfFoundToken == -1 && isRequired)
          throw new CxException(161, new object[] { t, cmdx });

        for (int i = 0; i < indexOfFoundToken; i++)
        {
          if (returnValue.IsBlank())
            returnValue = t.Tokens[i];
          else
            returnValue = returnValue + " " + t.Tokens[i];
        }

        if (returnValue.IsBlank() && isRequired)
          throw new CxException(162, new object[] { t, cmdx });

        if (cmdx.RemoveStoredToken)
        {
          string[] newTokenArray = new string[t.Tokens.Length - indexOfFoundToken];
          int j = 0;
          for (int i = 0; i < t.Tokens.Length; i++)
          {
            if (i > indexOfFoundToken - 1)
            {
              newTokenArray[j++] = t.Tokens[i];
            }
          }
          t.Tokens = newTokenArray;
        }

        return returnValue.FormatValue(cmdx);
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(30, new object[] { t, cmdx, ex });
      }
    }

    public static int FindPrevBlankOrNewLine(this Text t)
    {
      if (t.CurrPos < 1 || t.CurrPos > t.RawText.Length - 1)
        return -1;

      int curPos = t.CurrPos;

      while (t.RawText[curPos] != ' ' && t.RawText[curPos] != '\n')
      {
        curPos--;
        if (curPos < 1)
          return -1;
      }

      return curPos;
    }

    public static int FindNextBlankOrNewLine(this Text t)
    {
      if (!t.MoreText)
        return -1;

      if (t.CurrPos > t.RawText.Length - 1)
        return -1;

      return t.RawText.IndexOfAny(Constants.BlankOrNewLine, t.CurrPos);
    }

    public static int FindNextBlankOrNewLine(this Text t, int startPos)
    {
      if (!t.MoreText)
        return -1;

      if (startPos > t.RawText.Length - 1)
        return -1;

      return t.RawText.IndexOfAny(Constants.BlankOrNewLine, startPos);
    }

    public static string FormatValue(this string s, Cmdx cmdx)
    {
      try
      {
        string helperFunction = cmdx.HelperFunction;

        string math = cmdx.Math;
        if (math.IsNotBlank()  & !cmdx.MathIsDone)
        {
          if (s.IsDecimal())
          {
            string op = math[0].ToString();
            string operand = math.Substring(1);
            if (!operand.IsDecimal())
              throw new CxException(163, new object[] { cmdx, s });

            int intValue = 0;
            decimal decValue = 0.0M;

            bool integerMath = true;
            if (s.IsDecimal(true))
            {
              integerMath = false;
              decValue = s.ToDecimal();
            }
            else
            {
              intValue = s.ToInt32();
            }

            switch (op)
            {
              case "*":
                if (integerMath)
                  s = (intValue * operand.ToInt32()).ToString();
                else
                  s = (decValue * operand.ToDecimal()).ToString();
                break;

              case "+":
                if (integerMath)
                  s = (intValue + operand.ToInt32()).ToString();
                else
                  s = (decValue + operand.ToDecimal()).ToString();
                break;

              case "-":
                if (integerMath)
                  s = (intValue - operand.ToInt32()).ToString();
                else
                  s = (decValue - operand.ToDecimal()).ToString();
                break;

              case "/":
                if (integerMath)
                {
                  if (operand.ToInt32() == 0)
                    throw new CxException(164, new object[] { cmdx, s } );
                  s = (intValue / operand.ToInt32()).ToString();
                }
                else
                {
                  if (operand.ToDecimal() == 0)
                    throw new CxException(165, new object[] { cmdx, s } );
                  s = (decValue / operand.ToDecimal()).ToString();
                }
                break;
            }
          }

          cmdx.MathIsDone = true;
        }

        string token = s.Trim();

        string dataFormat = cmdx.DataFormat.ToLower().Trim();

        switch (dataFormat)
        {
          case "stripcommas":
            token = token.Replace(",", String.Empty);
            dataFormat = String.Empty;
            break;

          case "strippunctuation":
            token = token.Replace(",", String.Empty).Replace(".", String.Empty);
            dataFormat = String.Empty;
            break;
        }


        if (s.IsBlank() && token.IsBlank())
          return String.Empty;

        // default formatting for numerics
        if (dataFormat.IsBlank())
        {
          switch (cmdx.TokenType)
          {
            case "dec":
            case "decn":
            case "int":
            case "intn":
            case "pct":
            case "pctn":
              if (token.Contains("$"))
                token = token.Replace("$", String.Empty);
              if (token.Contains("%"))
                token = token.Replace("%", String.Empty);
              if (token.StartsWith("(") && token.EndsWith(")"))
              {
                token = "-" + token.Replace("(", String.Empty).Replace(")", String.Empty);
              }
              break;
          }

          return token;
        }

        DateTime dt;
        string[] tokens;

        switch (cmdx.TokenType)
        {
          case "date":
            dt = token.ToDateTime();

            if (helperFunction.IsNotBlank())
            {
              string helperFunctionName = helperFunction.GetTextBefore(Constants.OpenBracket).Replace("hf=", String.Empty);
              string parmString = helperFunction.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket);
              string[] parms = parmString.Split(Constants.PipeDelimiter, StringSplitOptions.RemoveEmptyEntries);

              switch (helperFunctionName)
              {
                case "addmonth":
                  if (parms.Length != 1)
                    throw new CxException(129, cmdx, s);
                  if (!parms[0].IsInteger())
                    throw new CxException(135, cmdx, s);
                  int monthAdjust = parms[0].ToInt32();
                  dt = dt.AddMonths(monthAdjust);
                  break;
              }
            }

            switch (dataFormat)
            {
              case "ccyymmdd":
                return dt.ToCCYYMMDD();
              case "mm/dd/yyyy":
                return dt.ToString("MM/dd/yyyy");
            }
            break;

          case "mm/yyyy":
            tokens = token.Split(Constants.FSlashDelimiter, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length != 2 || !tokens[0].InIntegerRange(1, 12) || !tokens[1].InIntegerRange(1900, 2199))
              throw new Exception("Tokens with data type '" + cmdx.TokenType + "' must have two numeric tokens with valid month and year values. The values found " +
                                  "were '" + tokens.ObjectArrayToString() + "'.");
            dt = new DateTime(tokens[1].ToInt32(), tokens[0].ToInt32(), 1);

            if (helperFunction.IsNotBlank())
            {
              string helperFunctionName = helperFunction.GetTextBefore(Constants.OpenBracket).Replace("hf=", String.Empty);
              string parmString = helperFunction.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket);
              string[] parms = parmString.Split(Constants.PipeDelimiter, StringSplitOptions.RemoveEmptyEntries);

              switch (helperFunctionName)
              {
                case "addmonth":
                  if (parms.Length != 1)
                    throw new CxException(129, cmdx, s);
                  if (!parms[0].IsInteger())
                    throw new CxException(135, cmdx, s);
                  int monthAdjust = parms[0].ToInt32();
                  dt = dt.AddMonths(monthAdjust);
                  break;
              }
            }

            switch (dataFormat)
            {
              case "ccyymmdd":
                return dt.ToCCYYMMDD();
              case "mm/dd/yyyy":
                return dt.ToString("MM/dd/yyyy");
            }
            break;

          case "time":
            dt = token.ToDateTime();
            switch (dataFormat)
            {
              case "h24:mm:ss":
                return dt.ToString("HH:MM:ss");
              case "h12:mm:ss":
                return dt.ToString("hh:MM:ss");

            }
            break;
        }

        throw new CxException(42, new object[] { cmdx, s });
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(41, new object[] { cmdx, s, ex });
      }
    }

    public static string RunHelperFunction(this Cmdx cmdx, string s)
    {
      try
      {
        return s.FormatValue(cmdx);
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(128, cmdx, s, ex);
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

        string[] tokens = workString.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries).ToLower();

        return tokens;
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(43, new object[] { s, ex });
      }
    }

    public static string FindString(this CmdxData cdx)
    {
      try
      {
        if ( cdx == null || cdx.RawText.IsBlank() || cdx.OriginalCmdx == null || cdx.OriginalCmdx.Parms == null || cdx.OriginalCmdx.Parms.Length == 0)
          return String.Empty;

        string t = cdx.RawText;

        if (t.Length < cdx.StartPos)
          return String.Empty;

        string[] parms = cdx.TextToFind.GetTypePlaceholders().ToLower();

        string textDump = t.ToTextDump();

        int searchPosition = 0;
        string textToSearch = String.Empty;

        if (parms[0] == "`bol" || parms[parms.Length - 1] == "`eol")
        {
          if (parms[0] == "`bol" && parms[parms.Length - 1] == "`eol")
            throw new Exception("Cannot specify that values to be located include both beginning of line '[bol]' and end of line '[eol]' elements.  The " +
                                "text to find is '" + cdx.TextToFind + "'.");


          // the values to locate must start at the beginning or end of a line
          string[] lines = t.Split(Constants.NewLineDelimiter, StringSplitOptions.RemoveEmptyEntries);

          bool startOfLine = parms[0] == "`bol";
          bool endOfLine = parms[parms.Length - 1] == "`eol";

          if (startOfLine)
            parms = parms.StripFirstElement();

          if (endOfLine)
            parms = parms.StripLastElement();

          searchPosition = 0;
          int lineCount = 0;

          foreach (string line in lines)
          {
            if (searchPosition + line.Length > cdx.StartPos)
            {
              textToSearch = line;

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

              var searchTextItem = new SearchText(textToSearch, parms, startOfLine, endOfLine);
              string foundText = searchTextItem.GetFirstMatchingString(0);

              if (foundText.Trim().IsNotBlank())
              {
                if (endOfLine)
                  return foundText += "\n";
                else
                  return "\n" + foundText;
              }
            }

            searchPosition += line.Length + 1;
            lineCount++;
          }
        }
        else
        {
          textToSearch = t;
          searchPosition = 0;
          int textToSearchLength = textToSearch.Length;
          var searchTextItem = new SearchText(t, parms);

          string foundText = searchTextItem.GetFirstMatchingString(cdx.StartPos);

          if (foundText.IsNotBlank())
            return foundText;
        }

        return String.Empty;
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(44, new object[] { cdx, ex });
      }
    }

    public static void SetVariable(this Text t, Cmdx cmdx, VariableType variableType = VariableType.Local)
    {
      try
      {
        TokenSearchCriteria tsc = null;
        LocatedToken locatedToken = null;

        if (cmdx.DataName.IsBlank())
          throw new CxException(63, new object[] { t, cmdx });

        if (cmdx.SubCommandVerb.IsBlank())
          throw new CxException(64, new object[] { t, cmdx });

        int startPos = 0;
        int endPos = t.TextLength;
        string variableValue = String.Empty;
        string subCommandVerb = cmdx.SubCommandVerb.ToLower();
        string dataType = cmdx.DataType;
        string variableName = cmdx.DataName.Trim();
        string pattern = cmdx.Pattern;
        string range = cmdx.Range;
        int holdCurrPos = t.CurrPos;
        bool? booleanValue = null;

        switch (subCommandVerb)
        {
          case "stored":
            int tokenIndex = cmdx.StoredTokenIndex;
            bool removeToken = cmdx.RemoveStoredToken;
            bool isRequired = cmdx.IsRequired;
            string beforeToken = cmdx.BeforeToken.Trim();

            // tokenIndex of 99998 means to join tokens
            if (tokenIndex == 99998)
            {
              string joinedTokens = t.Tokens.JoinRemainingTokens(" ", beforeToken);

              if (joinedTokens.IsBlank() && isRequired)
                throw new CxException(137, t, cmdx);

              if (removeToken && joinedTokens.IsNotBlank())
              {
                string[] tokensToRemove = joinedTokens.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);
                foreach (var tokenToRemove in tokensToRemove)
                  t.Tokens = t.Tokens.RemoveEntry(tokenToRemove);
              }

              if (joinedTokens.IsNotBlank())
                t.SetVariableValue(variableName, joinedTokens, variableType);

              return;
            }

            // tokenIndex of 99999 means the last token
            if (tokenIndex == 99999)
            {
              if (t.Tokens != null || t.Tokens.Length == 0)
              {
                if (isRequired)
                  return;
              }
              tokenIndex = t.Tokens.Length - 1;
            }
            else
            {
              if (t.Tokens == null || t.Tokens.Length == 0)
              {
                if (isRequired)
                  throw new CxException(132, t, cmdx);
              }
            }

            if (t.Tokens.Length < tokenIndex - 1)
              throw new CxException(133, t, cmdx);

            variableValue = t.Tokens[tokenIndex].Trim();
            if (variableValue.IsBlank() && isRequired)
              throw new CxException(134, t, cmdx);

            if (pattern.IsNotBlank())
            {
              if (!variableValue.TokenMatchesPattern(pattern, range))
              {
                if (isRequired)
                  throw new CxException(136, t, cmdx);
                return;
              }
            }

            t.SetVariableValue(variableName, variableValue, variableType);

            if (removeToken)
              t.Tokens = t.Tokens.RemoveItemAt(tokenIndex);
            break;

          case "extracttokens": // no instance of this in maps as of 3/21/2018
            string tokensValue = t.ExtractNextTokens(cmdx);
            variableName = cmdx.DataName;
            variableValue = tokensValue;
            if (variableValue.IsNotBlank())
            {
              t.SetVariableValue(variableName, variableValue, variableType);
              t.CurrPos = holdCurrPos;
            }
            break;

          case "extractvalue":
            tsc = new TokenSearchCriteria();
            tsc.DataName = variableName;
            tsc.TextToFind = cmdx.TextToFind;
            tsc.IsRequired = cmdx.IsRequired;
            locatedToken = new LocatedToken(t.RawText, t.CurrPos, tsc, t.ExtractOptions);
            variableValue = locatedToken.TokenText;
            if (variableValue.IsNotBlank())
            {
              t.SetVariableValue(variableName, variableValue, variableType);
              t.CurrPos = holdCurrPos;
            }
            break;

          case "extracttoeol":
            tsc = new TokenSearchCriteria();
            tsc.DataName = variableName;
            tsc.TextToFind = "[toEol]";
            tsc.IsRequired = true;
            tsc.Trim = true;
            locatedToken = new LocatedToken(t.RawText, t.CurrPos, tsc, t.ExtractOptions);
            variableValue = locatedToken.TokenText;
            if (variableValue.IsNotBlank())
            {
              t.SetVariableValue(variableName, variableValue, variableType);
              t.CurrPos = holdCurrPos;
            }
            break;

          case "getvariable": // obsolete, replace with var[$varname]
            throw new CxException(45, new object[] { t, cmdx });
          //string varName = cmdx.Parms[2].Trim();
          //variableValue = t.GetVariable(varName, true);
          //if (variableValue.IsNotBlank())
          //{
          //  if (cmdx.HelperFunction.IsNotBlank())
          //    variableValue = cmdx.RunHelperFunction(variableValue);
          //  t.SetVariableValue(variableName, variableValue, variableType);
          //}
          //break;

          case "var":
            string varParm = cmdx.FullSubCommand;
            string varName = varParm.GetTextBetweenBrackets();
            variableValue = t.GetVariable(varName, cmdx.IsRequired);
            if (variableValue.IsNotBlank())
            {
              if (cmdx.HelperFunction.IsNotBlank())
                variableValue = cmdx.RunHelperFunction(variableValue);
              t.SetVariableValue(variableName, variableValue, variableType);
            }
            break;

          case "find":
            bool boolValue = IsTextFound(cmdx.ToCmdxData(t.RawText, startPos));
            t.SetVariableValue(variableName, boolValue.ToString(), variableType);
            break;

          case "lit":
            if (cmdx.ParmCount < 3)
              throw new CxException(166, new object[] { t, cmdx });
            variableValue = cmdx.Parms[2].Trim();
            t.SetVariableValue(variableName, variableValue, variableType);
            break;

          case "expression":
            if (cmdx.ParmCount < 3)
              throw new CxException(167, new object[] { t, cmdx });
            string expression = cmdx.Parms[2];
            int relPos = expression.IndexOfAny(new char[] { '=', '<', '>', '!' });
            if (relPos == -1)
              throw new CxException(168, new object[] { t, cmdx });
            char relOp = expression[relPos];
            string[] operands = expression.Split(new char[] { relOp }, StringSplitOptions.RemoveEmptyEntries);
            if (operands.Length != 2)
              throw new CxException(169, new object[] { t, cmdx });
            string leftOperand = operands[0].ToLower().Trim();
            string rightOperand = operands[1].ToLower().Trim();
            string value = String.Empty;
            switch (leftOperand)
            {
              case "tokencount":
                value = t.Tokens != null ? t.Tokens.Length.ToString() : "0";
                if (!rightOperand.IsDecimal(false))
                  throw new CxException(171, new object[] { t, cmdx });
                if (!value.IsDecimal(false))
                  throw new CxException(172, new object[] { t, cmdx });
                decimal rightDec = rightOperand.ToDecimal();
                decimal valueDec = value.ToDecimal();
                booleanValue = null;

                switch (relOp)
                {
                  case '<':
                    booleanValue = valueDec < rightDec;
                    break;
                  case '=':
                    booleanValue = valueDec == rightDec;
                    break;
                  case '>':
                    booleanValue = valueDec > rightDec;
                    break;
                  case '!':
                    booleanValue = !(valueDec == rightDec);
                    break;
                }

                t.SetVariableValue(variableName, booleanValue.ToString(), variableType);
                break;

              case "extractvalue":
                value = t.ExtractNextToken(cmdx).ToLower().Trim();
                t.CurrPos = holdCurrPos;

                switch (relOp)
                {
                  case '<':
                    booleanValue = String.Compare(value, rightOperand) < 0;
                    break;

                  case '=':
                    booleanValue = String.Compare(value, rightOperand) == 0;
                    break;

                  case '>':
                    booleanValue = String.Compare(value, rightOperand) > 0;
                    break;

                  case '!':
                    booleanValue = !(String.Compare(value, rightOperand) == 0);
                    break;
                }

                t.SetVariableValue(variableName, booleanValue.ToString(), variableType);
                break;

              default:
                throw new CxException(170, new object[] { t, cmdx });
            }

            break;

          default:
            throw new CxException(45, new object[] { t, cmdx });
        }
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(46, new object[] { t, cmdx, ex });
      }
    }

    public static List<XAttribute> GetAttributesWithVariables(this XElement e)
    {
      var attrsWithVariables = new List<XAttribute>();

      if (e == null || e.Attributes().Count() == 0)
        return attrsWithVariables;

      foreach (var attr in e.Attributes())
      {
        if (attr.Value.StartsWith("$"))
          attrsWithVariables.Add(attr);
      }

      return attrsWithVariables;
    }

    public static bool IsTokenType(this string value, string type)
    {
      if (value.IsBlank() || type.IsBlank())
        return false;

      switch (type.ToLower())
      {
        case "str":
          return value.IsString();
        case "dec":
          return value.IsDecimal(true);
        case "decn":
          return value.IsDecimal(false);
        case "pct":
          return value.IsDecimal(false);
        case "int":
          return value.IsInteger();
        case "date":
          return value.IsValidShortDate();
        case "time":
          return value.IsValidTime();
        case "mm/yyyy":
          return value.IsValidMMSlashYYYY();
        case "mmmm":
          return value.IsFullySpelledMonth();
        case "mmm":
          return value.IsAbbreviatedMonth();
        case "yyyy":
          return value.IsReasonableYYYY();
        case "yy":
          return value.IsYY();
        case "dd":
          return value.IsValidDD();
      }

      throw new CxException(47, new object[] { value, type } );
    }

    public static void AssertValidDataType(this string dataType)
    {
      switch (dataType)
      {
        case "str":
        case "dec":
        case "decn":
        case "bool":
        case "date":
        case "int":
        case "pct":
        case "regex":
          return;
      }

      throw new CxException(24, new string[] { dataType });
    }

    public static bool MatchesPattern(this string[] array, string[] pattern)
    {
      if (array == null || pattern == null)
        return false;

      if (array.Length == 0 || pattern.Length == 0)
        return false;

      int pPtr = 0;
      int aPtr = 0;
      int pPrevPtr = -1;
      int aPrevPtr = -1;
      string pItem = String.Empty;
      string aItem = String.Empty;

      while (pPtr < pattern.Length && aPtr < array.Length)
      {
        if (pPtr == pPrevPtr && aPtr == aPrevPtr)
          throw new Exception("A logical problem exists in the MatchesPattern extension method. The array pointer variable and the pattern pointer " +
                              "variable both hold the same value as in the previous iteration of the loop. This exception is being thrown to prevent " +
                              "an endless looping condition. The array values are '" + array.StringArrayToString() + "' and the pattern values are '" +
                              pattern.StringArrayToString() + "'.");

        pPrevPtr = pPtr;
        aPrevPtr = aPtr;

        pItem = pattern[pPtr].Trim();
        aItem = array[aPtr].Trim();

        string pType = String.Empty;

        if (pItem.IsBracketed())
          pType = pItem.GetBracketedText();

        bool foundMatch = false;
        int aMatchIndex = -1;
        int aItemsRemaining = 0;
        int pItemsRemaining = 0;

        for (int aPtr2 = aPtr; aPtr2 < array.Length; aPtr2++)
        {
          aItem = array[aPtr2].Trim();

          if (pType.IsNotBlank())
            foundMatch = aItem.IsTokenType(pType);
          else
            foundMatch = aItem.IsCaseInsensitiveEqual(pItem);

          if (foundMatch)
          {
            aMatchIndex = aPtr2;
            break;
          }
        }

        if (!foundMatch)
          return false;

        aItemsRemaining = array.Length - aMatchIndex - 1;
        pItemsRemaining = pattern.Length - pPtr - 1;

        if (aItemsRemaining < pItemsRemaining)
          return false;

        if (aItemsRemaining == 0 && pItemsRemaining == 0)
          return true;

        aPtr = aMatchIndex + 1;
        pPtr++;
      }

      return true;
    }

    public static CmdxData ToCmdxData(this Cmdx cmdx, string rawText = "", int startPos = 0)
    {
      var cmdxData = new CmdxData();
      cmdxData.RawText = rawText;
      cmdxData.TextToFind = cmdx.TextToFind;
      cmdxData.StartPos = startPos;
      cmdxData.PositionAtEnd = cmdx.PositionAtEnd;
      cmdxData.IsRequired = cmdx.IsRequired;
      cmdxData.IsReportUnit = cmdx.IsProcessingReportUnit;
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
            case "ExtractSpec":
              extractSpec = (ExtractSpec)parm;
              break;

            case "Text":
              t = (Text)parm;
              if (t.Cmdx != null)
                cmdx = t.Cmdx;

              var textObject = t;
              do
              {
                if (textObject.ExtractSpec != null)
                {
                  extractSpec = textObject.ExtractSpec;
                  break;
                }
                textObject = textObject.Parent;
              } while (textObject != null);
              break;

            case "Cmdx":
              cmdx = (Cmdx)parm;
              break;

            case "Exception":
              ex = (Exception)parm;
              break;

            case "CmdxData":
              cmdxData = (CmdxData)parm;
              if (cmdxData.OriginalCmdx != null)
              {
                cmdx = cmdxData.OriginalCmdx;
                if (cmdx.Text != null)
                {
                  t = cmdx.Text;
                  if (t.Root != null && t.Root.ExtractSpec != null)
                  {
                    extractSpec = t.Root.ExtractSpec;
                  }
                }
              }

              break;

            case "CxException":
              cxp = (CxException)parm;
              break;

            case "String":
              otherValues.Add((string)parm);
              break;

            case "Int32":
              otherValues.Add(parm.ToInt32().ToString());
              break;

            default:
              if (parm.GetType().IsSubclassOf(typeof(Cmdx)))
              {
                cmdx = (Cmdx)parm;
                if (cmdx.ExtractSpec != null)
                  extractSpec = cmdx.ExtractSpec;
                if (cmdx.Text != null)
                  t = cmdx.Text;
                break;
              }

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
        sb.Append(extractSpec.ToReport(cmdx) + g.crlf2);

      if (ex != null)
        sb.Append("Native Exception Report" + g.crlf2 + ex.ToReport() + g.crlf2);

      if (otherValues != null && otherValues.Count > 0)
      {
        foreach (var otherValue in otherValues)
        {
          string objectType = otherValue.GetType().Name;
          switch (objectType)
          {
            case "String":
              string otherValueString = otherValue.ToString();
              sb.Append(g.crlf + otherValueString + g.crlf);
              break;

            default:
              continue;
          }
        }
      }

      if (otherValues != null && otherValues.Count > 0)
      {
        foreach (var otherValue in otherValues)
        {
          string objectType = otherValue.GetType().Name;
          switch (objectType)
          {
            case "String":
              string otherValueString = otherValue.ToString();
              sb.Append(g.crlf + otherValueString + g.crlf);
              break;

            default:
              continue;
          }
        }
      }

      string report = sb.ToString();
      return report;
    }

    public static List<Text> RunSpecialRoutine(this Text t, string specialRoutineName)
    {
      throw new CxException(126, t, specialRoutineName);

      //try
      //{
      //  if (specialRoutineName == null)
      //    throw new Exception("The Tsd SpecialRoutine name is null.");

      //  var textSet = new List<Text>();

      //  switch (specialRoutineName)
      //  {
      //    case "enablegasgrid1":
      //      return t.RunEnableGasGrid1();

      //    default:
      //      throw new Exception("Tsd SpecialRoutine named '" + specialRoutineName + "' is not implemented.");
      //  }
      //}
      //catch (CxException) { throw; }
      //catch (Exception ex)
      //{
      //  throw new CxException(124, t, specialRoutineName, ex);
      //}
    }

    public static List<Text> RunEnableGasGrid1(this Text t)
    {
      try
      {
        var textList = new List<Text>();

        if (t.RawText.IsBlank())
          throw new Exception("The Tsd SpecialRoutine 'RunEnableGasGrid1' failed due to the RawText property of the Text object being null or blank.");

        string[] tokens = t.RawText.Split(Constants.BlankOrNewLine, StringSplitOptions.RemoveEmptyEntries);
        string[] filteredTokens = tokens.Filter("date,decn");



        return textList;
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new CxException(125, t, ex);
      }
    }

    public static string[] Filter(this string[] array, string types)
    {
      var filteredTokens = new List<string>();

      if (array == null || array.Length == 0 || types == null || types.Length == 0)
        return filteredTokens.ToArray<String>();

      foreach(var s in array)
      {



      }


      string[] filteredArray = filteredTokens.ToArray<String>();

      return filteredArray;
    }

    public static string ToExceptionReport(this Text t)
    {
      var sb = new StringBuilder();

      List<Text> textStack = new List<Text>();
      var textObject = t;
      do
      {
        textStack.Add(t);
        t = t.Parent;
      } while (t != null);

      int ptr = textStack.Count - 1;

      if (textObject.FullPath.IsNotBlank())
        sb.Append("Text Full Path: " + textObject.FullPath + g.crlf2);

      string globalVariables = String.Empty;
      if (Text.GlobalVariables != null && Text.GlobalVariables.Count > 0)
      {
        for (int k = 0; k < Text.GlobalVariables.Count; k++)
        {
          var variable = Text.GlobalVariables.ElementAt(k);
          globalVariables += "      Variable Name: " + variable.Key.Trim() + "  Value:" + variable.Value + g.crlf;
        }
      }

      if (globalVariables.IsNotBlank())
        sb.Append("Global Variables (static)" + g.crlf + globalVariables + g.crlf2);
      else
        sb.Append("Global Variables (static)" + g.crlf + "      None" + g.crlf2);


      sb.Append("Text Stack" + g.crlf);
      for (int i = ptr; i > -1; i--)
      {
        var textItem = textStack.ElementAt(i);

        string storedTokens = String.Empty;
        if (textItem.Tokens != null)
        {
          for (int j = 0; j < textItem.Tokens.Length; j++)
          {
            storedTokens = storedTokens + "      Token " + j.ToString("00") + " : " + textItem.Tokens[j] + g.crlf;
          }
        }

        string localVariables = String.Empty;
        if (textItem.LocalVariables != null && textItem.LocalVariables.Count > 0)
        {
          for (int j = 0; j < textItem.LocalVariables.Count; j++)
          {
            var variable = textItem.LocalVariables.ElementAt(j);
            localVariables += "      Local Variable Name: " + variable.Key.Trim() + "  Value:" + variable.Value + g.crlf;
          }
        }

        sb.Append("  (" + (textStack.Count - i).ToString() + ") Name: " + textItem.Name + g.crlf);
        sb.Append("      BegPos:  " + textItem.BegPos.ToString() + g.crlf +
                  "      EndPos:  " + textItem.EndPos.ToString() + g.crlf +
                  "      CurrPos: " + textItem.CurrPos.ToString() + g.crlf +
                  "      Path:    " + textItem.FullPath + g.crlf +
                  (storedTokens.IsNotBlank() ? storedTokens : "      Tokens:  None" + g.crlf) +
                  (localVariables.IsNotBlank() ? localVariables : "      Variables: None" + g.crlf) +
                  "      First50: " + textItem.First50.Replace("\n", "\xA4").Replace("\r", "\xA4") + g.crlf +
                  "      Zoom:    " + textItem.AreaOfCurrPos.Replace(g.crlf, g.crlf + "               ") + g.crlf2);
      }


      sb.Append(g.crlf2);
      string report = sb.ToString();
      return report;
    }

    public static string ToExceptionReport(this Cmdx c)
    {
      try
      {
        var sb = new StringBuilder();

        try
        {

          string condition = String.Empty;
          if (c.Parent != null && c.Parent.Condition.IsNotBlank())
            condition = "cond=" + c.Parent.Condition + " [from Tsd]";
          if (c.Condition.IsNotBlank())
            condition = c.Condition + (condition.IsNotBlank() ? " OVERRIDES " + condition : String.Empty);

          sb.Append("Cmdx Object" + g.crlf);
          sb.Append("  LineNumber:        " + c.LineNumber.ToString() + g.crlf +
                    "  Code:              " + c.Code + g.crlf +
                    "  Condition:         " + c.Condition + g.crlf +
                    "  Verb:              " + c.Verb.ToString() + g.crlf +
                    "  ParmString:        " + c.ParmString + g.crlf +
                    "  DataName:          " + c.DataName + g.crlf +
                    "  DataType:          " + c.DataType + g.crlf +
                    "  TokenType:         " + c.TokenType + g.crlf +
                    "  IsRequired:        " + c.IsRequired.ToString() + g.crlf +
                    "  NumericOnly:       " + c.NumericOnly.ToString() + g.crlf +
                    "  PositionAtEnd:     " + c.PositionAtEnd.ToString() + g.crlf +
                    "  AdvanceToEol:      " + c.AdvanceToEol.ToString() + g.crlf +
                    "  RemoveStoredToken: " + c.RemoveStoredToken.ToString() + g.crlf +
                    "  StoredTokenIndex:  " + c.StoredTokenIndex.ToString() + g.crlf +
                    "  SubCommandVerb:    " + c.SubCommandVerb + g.crlf +
                    "  FullSubCommand:    " + c.FullSubCommand + g.crlf +
                    "  TextToFind:        " + c.TextToFind + g.crlf +
                    "  UsePriorEnd:       " + c.UsePriorEnd.ToString() + g.crlf +
                    "  SpecialRoutine:    " + c.SpecialRoutine + g.crlf +
                    g.crlf2
                   );
        }
        catch (Exception ex)
        {
          sb.Append(g.crlf2 + "An exception occurred while attempting to get the report from the Cmdx object." + ex.ToReport() + g.crlf2);
        }

        string report = sb.ToString();
        return report;
      }
      catch (Exception ex)
      {
        return "Exception occurred during exception reporting: " + g.crlf + ex.ToReport() + g.crlf2;
      }
    }

    public static string ToExceptionReport(this CmdxData cdx)
    {
      var sb = new StringBuilder();

      sb.Append("CmdxData Object" + g.crlf);
      sb.Append("  IsRequired:        " + cdx.IsRequired.ToString() + g.crlf +
                "  NumericOnly:       " + cdx.NumericOnly.ToString() + g.crlf +
                "  PositionAtEnd:     " + cdx.PositionAtEnd.ToString() + g.crlf +
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
