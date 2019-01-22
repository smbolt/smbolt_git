using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  public class SearchText
  {
    private string _text;
    private int _parmIndex;
    private string[] _parms;
    private int[] _startPos;
    private int[] _endPos;
    private bool _atStartOfLine;
    private bool _atEndOfLine;

    public SearchText(string text, string[] parms, bool atStartOfLine = false, bool atEndOfLine = false)
    {
      _text = text;
      _parms = parms;
      _parmIndex = 0;
      _startPos = new int[parms.Length];
      _endPos = new int[parms.Length];
      _atStartOfLine = atStartOfLine;
      _atEndOfLine = atEndOfLine;
    }

    public string GetFirstMatchingString(int startPosition)
    {
      try
      {
        if (_text.IsBlank() || _parms == null || _parms.Length == 0)
          return String.Empty;

        if (startPosition >= _text.Length)
          return String.Empty;

        string textToSearch = String.Empty;

        if (startPosition > 0)
          textToSearch = _text.Substring(startPosition);
        else
          textToSearch = _text;

        string[] lineTokens = textToSearch.Split(Constants.SpaceOrNewLineDelimiter, StringSplitOptions.RemoveEmptyEntries);

        // We cannot match the parms if the number of tokens in the "line" is less than the number
        // of parameters, which is the specification of the tokens we are looking for.
        if (lineTokens.Length < _parms.Length)
          return String.Empty;

        for (int i = 0; i < _parms.Length; i++)
        {
          _startPos[i] = -1;
          _endPos[i] = -1;

          bool tokenIsPlaceholder = false;
          string searchToken = _parms[i];

          if (searchToken.StartsWith("`"))
          {
            tokenIsPlaceholder = true;
            searchToken = searchToken.Substring(1);
          }

          if (tokenIsPlaceholder)
          {
            int beg = 0;
            if (i > 0)
              beg = _endPos[i - 1] + 1;

            while (true) // lets loop
            {
              string token = textToSearch.GetNextToken(beg);

              // if there are no more tokens, then we get out
              if (token.IsBlank())
                return String.Empty;

              // capture the location of the token, so we can continue looking for more
              // if the token is not what we're looking for

              int startPos = textToSearch.IndexOf(token, beg, StringComparison.CurrentCultureIgnoreCase);
              if (startPos == -1) // if token can't be re-found in string, then bail
                return String.Empty;

              int endPos = startPos + token.Length - 1;

              // if we got a token of the placeholder type we're looking for then we record
              // the start and end positions and see if there are more tokens to locate
              if (token.IsTokenType(searchToken))
              {
                int foundPos = startPos;
                if (foundPos == -1)
                  return String.Empty;

                _startPos[i] = foundPos;
                _endPos[i] = foundPos + token.Length - 1;
                break;
              }
              else
              {
                // if there is not more text to search then bail
                if (endPos + 1 >= textToSearch.Length - 1)
                  return String.Empty;

                // set the begin position to the position of the end of the last token
                // and loop back to see if there's another token to check
                beg = endPos + 1;
              }
            }
          }
          else
          {
            int beg = 0;
            if (i > 0)
              beg = _endPos[i - 1];

            int foundPos = textToSearch.IndexOf(searchToken, beg, StringComparison.CurrentCultureIgnoreCase);
            if (foundPos == -1)
              return String.Empty;

            _startPos[i] = foundPos;
            _endPos[i] = foundPos + searchToken.Length - 1;
          }
        }

        if (_startPos.Length == 0 || _endPos.Length == 0)
          return String.Empty;

        int start = _startPos[0];
        int end = _endPos[_endPos.Length - 1];

        if (start == -1 || end <= start)
          return String.Empty;

        int textLength = end - start + 1;

        if (textLength < 1)
          return String.Empty;

        if (textToSearch.Length < start + textLength)
          return String.Empty;

        string locatedString = textToSearch.Substring(start, textLength);

        // Now we need to determine if the located string is specified to be adjacent to the start of the line
        // or adjacent to the the end of the line.

        // If the string is specified to be adjacent to the start of the line and there are one or more tokens
        // before it in the line, then we have failed to find the token adjacent to the start of the line.

        if (_atStartOfLine)
        {
          if (start > 0)
          {
            string before = textToSearch.Substring(0, start).ConvertWhiteSpaceToBlanks();
            if (before.IsNotBlank())
              return String.Empty;
          }
        }

        // If the string is specified to be adjacent to the end of the line and there are one or more tokens
        // after it in the line, then we have failed to find the token adjacent to the end of the line.

        if (_atEndOfLine)
        {
          int afterIndex = start + textLength;
          if (afterIndex < textToSearch.Length - 1)
          {
            string after = textToSearch.Substring(afterIndex).ConvertWhiteSpaceToBlanks();
            if (after.IsNotBlank())
              return String.Empty;
          }
        }

        return locatedString;
      }
      catch(Exception ex)
      {
        throw new CxException(149, new object[] { this, startPosition, ex });
      }
    }


  }
}
