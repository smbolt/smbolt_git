using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  public class LocatedToken
  {
    public string RawText { get; private set; }
    public int InitialPosition { get; private set; }
    public int CurrentPosition { get; private set; }
    public string TokenText { get; private set; }
    public bool TokenLocated { get; private set; }
    public int TokenBeginPosition { get; private set; }
    public int TokenEndPosition { get; private set; }
    public string AreaOfCurrPos { get { return Get_AreaOfCurrPos(); } }

    public int TokenLength { get { return this.TokenText == null ? 0 : this.TokenText.Length; } }
    public int TokenTrimmedLength { get { return this.TokenText == null ? 0 : this.TokenText.Trim().Length; } }
    public bool MoreTextForward { get { return Get_MoreTextForward(); } }
    public bool MoreTextBackward { get { return Get_MoreTextBackward(); } }
    public bool IsPrecededByABlank { get { return Get_IsPrecededByABlank(); } }
    public bool IsPrecededByANewLine { get { return Get_IsPrecededByANewLine(); } }
    public bool IsFollowedByABlank { get { return Get_IsFollowedByABlank(); } }
    public bool IsFollowedByANewLine { get { return Get_IsFollowedByANewLine(); } }
    private OptionsList _extractOptions;

    private TokenSearchCriteria _tsc;
    private TextUtility _u;

    private int _ptr;

    public LocatedToken(string rawText, int initialPosition, TokenSearchCriteria tokenSearchCriteria, OptionsList extractOptions)
    {
      _extractOptions = extractOptions == null ? new OptionsList() : extractOptions;
      _u = new TextUtility(_extractOptions);
      this.RawText = rawText;
      this.InitialPosition = initialPosition;
      this.CurrentPosition = initialPosition;
      _ptr = initialPosition;
      _tsc = tokenSearchCriteria;
      this.TokenText = String.Empty;
      this.TokenLocated = false;

      this.TokenBeginPosition = -1;
      this.TokenEndPosition = -1;

      if (this.RawText == null)
        throw new Exception("The raw text provided to the LocatedToken class is null.");

      if (this.RawText.Length == 0)
        return;

      if (this.InitialPosition < 0 || this.InitialPosition > this.RawText.Length - 1)
        throw new Exception("The initial position provided to the LocatedToken class '" + this.InitialPosition.ToString() + "' is outside the bounds of the " +
                            "length of the raw text which is '" + this.RawText.Length.ToString() + "'.");


      if (_tsc.BeforeToken.IsNotBlank())
        LocateTokenBefore();
      else
        LocateToken();
    }

    private void LocateToken()
    {
      try
      {
        _ptr = this.InitialPosition;
        int begOfToken = -1;
        int endOfToken = -1;
        int tokenLength = -1;

        switch (_tsc.Direction)
        {
          case TextProcessing.Direction.Next:
            if (_tsc.TextToFind.In("[toEol],[toEot]"))
            {
              if (_tsc.TextToFind == "[toEol]")
              {
                LocateNextToEnd();
                return;
              }

              throw new Exception("LocateToken with 'text to find'=[toEot]' is not implemented yet.");
            }

            while (true)
            {
              if (_ptr > this.RawText.Length - 1)
                return;

              _ptr = _u.AdjustInitialPositionForward(this.RawText, _ptr);

              if (_ptr > this.RawText.Length - 1)
                return;

              // if non-blank charcter not found (didn't exit from current token going forward), no next token found - return
              if (this.RawText[_ptr] == ' ' || this.RawText[_ptr] == '\n')
                return;

              begOfToken = _ptr;

              // continue forward to next blank character or new line character or end of raw text
              while (this.RawText[_ptr] != ' ' && this.RawText[_ptr] != '\n')
              {
                _ptr++;
                if (_ptr > this.RawText.Length - 1)
                {
                  _ptr = this.RawText.Length - 1;
                  break; ;
                }
              }
              
              tokenLength = -1;

              // If we found a blank character or new line prior to reaching the end of the raw text, then move backward one 
              // one character to the actual ending of the token - else, the token ends at the end of the raw text.
              if (_ptr < this.RawText.Length - 1)
                _ptr--;

              tokenLength = _ptr - begOfToken + 1;
              string potentialToken = this.RawText.Substring(begOfToken, tokenLength).Trim();

              if (potentialToken.TokenMatchesPattern(_tsc.Pattern))
              {
                this.TokenBeginPosition = begOfToken;
                this.TokenEndPosition = _ptr;
                this.TokenText = potentialToken;
                this.TokenLocated = true;

                this.CurrentPosition = begOfToken;
                return;
              }

              _ptr++;
            }

          case TextProcessing.Direction.Prev:
            while (true)
            {
              if (_ptr < 1)
                return;

              _ptr = _u.AdjustInitialPositionBackward(this.RawText, _ptr);

              if (_ptr < 0)
                return;

              // if non-blank charcter not found (didn't exit current token going backward), no prior token found - return
              if (this.RawText[_ptr] == ' ' || this.RawText[_ptr] == '\n')
                return;

              endOfToken = _ptr;

              // continue backward to next blank character or new line character or beginning of raw text
              while (this.RawText[_ptr] != ' ' && this.RawText[_ptr] != '\n')
              {
                _ptr--;
                if (_ptr < 0)
                {
                  _ptr = 0;
                  break; ;
                }
              }

              tokenLength = -1;

              // If we found a blank character or new line prior to reaching the beginning of the raw text, then move forward  
              // one character to the actual beginning of the token - else, the token begins at the beginning of the raw text.
              if (_ptr > 0)
                _ptr++;

              tokenLength = endOfToken - _ptr + 1;
              string potentialToken = this.RawText.Substring(_ptr, tokenLength);

              if (potentialToken.TokenMatchesPattern(_tsc.Pattern))
              {
                this.TokenBeginPosition = _ptr;
                this.TokenEndPosition = endOfToken;
                this.TokenText = potentialToken;
                this.TokenLocated = true;

                this.CurrentPosition = _ptr;
                return;
              }

              _ptr--;
            }
        }

      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new CxException(192, this, ex); 
      }
    }

    private void LocateTokenBefore()
    {
      try
      {
        _ptr = this.InitialPosition;

        if (_ptr > this.RawText.Length - 1)
          return;

        _ptr = _u.AdjustInitialPositionForward(this.RawText, _ptr);

        if (_ptr > this.RawText.Length - 1)
          return;

        // if non-blank charcter not found (didn't exit from current token going forward), no next token found - return
        if (this.RawText[_ptr] == ' ' || this.RawText[_ptr] == '\n')
          return;

        bool beforeTokenFound = false;
        string returnValue = String.Empty;
        string tk = String.Empty;

        var tns = new TextNodeSpec(_tsc.BeforeToken);         

        // if no text beyond current position, just return with token not located
        if (_ptr >= this.RawText.LastIndex())
          return;

        // get remaining text and put in a string array of tokens
        string remainingText = this.RawText.Substring(_ptr);
        string[] tokens = remainingText.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length == 0)
          return;

        // Loop through the tokens looking for the "before token", the token, when found defines the extent of the
        // text that is extracted.  We want all text "before" the "before token".
        for (int i = 0; i < tokens.Length; i++)
        {
          if (beforeTokenFound)
            break;

          // As each token is processed it is first a "candidate" token, a token that might be the "before token".
          // If it is not the "before token" it becomes part of the "returnValue" which, when the "before token"
          // is located, becomes the "located token".

          // from prior time through the loop, accumulate the return value
          // from the prior loops candidate token
          if (tk.IsNotBlank())
          {
            if (returnValue.IsNotBlank())
              returnValue += " " + tk;
            else
              returnValue = tk;
          }

          // get the next token in the array
          tk = tokens[i].Trim();

          if (tns.TokenMatch(tk))
          {
            beforeTokenFound = true;
            break;
          }
        }

        if (beforeTokenFound)
        {
          if (returnValue.IsBlank())
            return;

          int ptr = this.RawText.IndexOf(returnValue, this.CurrentPosition);
          if (ptr == -1)
            throw new CxException(199, this);

          this.TokenLocated = true;
          this.TokenText = returnValue;
          this.TokenBeginPosition = ptr;
          this.CurrentPosition = ptr;
          this.TokenEndPosition = this.TokenBeginPosition + returnValue.Length - 1;
        }
      }
      catch (CxException) { throw; }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to locate a token within a text string.", ex);
      }
    }

    private void LocateNextToEnd()
    {
      if (_ptr > this.RawText.Length - 1)
        return;

      // position between tokens
      if (_ptr > 0)
      {
        while (this.RawText[_ptr] != ' ' && this.RawText[_ptr] != '\n')
        {
          _ptr++;
          if (_ptr > this.RawText.Length - 1)
            return;
        }
      }

      // if already positioned at a new line, advance off of it
      int ptr = _ptr;
      while (this.RawText[ptr] == '\n')
      {
        ptr++;
        if (ptr > this.RawText.Length)
          return;
      }

      int eolIndex = this.RawText.IndexOf('\n', ptr);
      if (eolIndex == -1)
        this.TokenText = this.RawText.Substring(_ptr);
      else
        this.TokenText = this.RawText.Substring(_ptr, eolIndex - _ptr);

      if (_tsc.IsRequired && this.TokenText.IsBlank())
        throw new Exception("The required token was not located using TokenSearchCriteria.");

      if (_tsc.Trim)
        this.TokenText = this.TokenText.Trim();

      this.TokenBeginPosition = _ptr;
      this.TokenEndPosition = _ptr + this.TokenText.Length - 1;
      this.TokenLocated = true;
      this.CurrentPosition = _ptr;
      return;
    }


    private bool Get_IsPrecededByABlank()
    {
      if (!this.TokenLocated)
        return false;

      if (this.RawText == null || this.TokenBeginPosition < 1)
        return false;

      if (this.TokenBeginPosition > this.RawText.Length - 1)
        return false;

      return this.RawText[this.TokenBeginPosition - 1] == ' ';
    }

    private bool Get_IsFollowedByABlank()
    {
      if (!this.TokenLocated)
        return false;

      if (this.RawText == null)
        return false;

      if (this.TokenEndPosition > this.RawText.Length - 1)
        return false;

      return this.RawText[this.TokenEndPosition + 1] == ' ';
    }

    private bool Get_IsPrecededByANewLine()
    {
      if (!this.TokenLocated)
        return false;

      if (this.RawText == null || this.TokenBeginPosition < 1)
        return false;

      if (this.TokenBeginPosition > this.RawText.Length - 1)
        return false;

      return this.RawText[this.TokenBeginPosition - 1] == '\n';
    }

    private bool Get_IsFollowedByANewLine()
    {
      if (!this.TokenLocated)
        return false;

      if (this.RawText == null)
        return false;

      if (this.TokenEndPosition > this.RawText.Length - 1)
        return false;

      return this.RawText[this.TokenEndPosition + 1] == '\n';
    }

    private bool Get_MoreTextForward()
    {
      if (!this.TokenLocated)
        return false;

      if (this.RawText == null)
        return false;

      if (this.RawText.Length == 0)
        return false;

      return this.TokenEndPosition < this.RawText.Length - 1;
    }

    private bool Get_MoreTextBackward()
    {
      if (!this.TokenLocated)
        return false;

      if (this.RawText == null)
        return false;

      if (this.RawText.Length == 0)
        return false;

      return this.TokenBeginPosition > 0;
    }


    public string Get_AreaOfCurrPos()
    {
      try
      {
        string zoom = String.Empty;

        if (this.RawText == null)
          return "[text is null]";

        if (this.RawText.IsBlank())
          return "[text is blank]";

        int showLength = this.RawText.Length > 130 ? 130 : this.RawText.Length;
        int currPos = _ptr;
        if (currPos < 0)
          currPos = 0;

        int lowPos = _ptr - 50;
        if (lowPos < 0)
          lowPos = 0;
        int highPos = lowPos + 130;
        if (highPos > this.RawText.Length - 1)
          highPos = this.RawText.Length - 1;

        string textToShow = this.RawText.Substring(lowPos, highPos - lowPos).Replace("\n", "\xA4").Replace(" ", "\xB7");

        int lowRuler = lowPos;
        while (lowRuler > 100)
          lowRuler -= 100;

        int highRuler = lowRuler + showLength;

        string ruler = g.Ruler.Substring(lowRuler, highRuler - lowRuler);

        zoom = ruler + g.crlf +
               textToShow + g.crlf +
               g.BlankString(currPos - lowPos) + "^" + g.BlankString(highPos - currPos) + g.crlf +
               "Text length: " + this.RawText.Length.ToString("###,##0").PadToJustifyRight(8) + g.crlf +
               "Current Pos: " + this._ptr.ToString("###,##0").PadToJustifyRight(8);

        return zoom;
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
    }
  }
}
