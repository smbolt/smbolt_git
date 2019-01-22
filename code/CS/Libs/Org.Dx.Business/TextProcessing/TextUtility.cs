using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  public class TextUtility : IDisposable
  {
    public OptionsList OptionsList {
      get;
      set;
    }

    public TextUtility(OptionsList optionsList = null)
    {
      if (optionsList == null)
        this.OptionsList = new OptionsList();
      else
        this.OptionsList = optionsList;
    }

    public bool TokenMatchesLiteralValue(string tk, string literalValue, bool matchCase)
    {
      if (tk.IsBlank())
        throw new Exception("The token parameter is null.");

      if (literalValue.IsBlank())
        throw new Exception("The literalValue parameter is null.");

      if (matchCase)
      {
        if (literalValue.StartsWith("*") && literalValue.EndsWith("*"))
        {
          string litValue = literalValue.Substring(0, literalValue.Length - 2);
          if (tk.Contains(litValue))
          {
            return true;
          }
        }
        else
        {
          if (literalValue.StartsWith("*"))
          {
            string litValue = literalValue.Substring(1);
            if (tk.EndsWith(litValue))
            {
              return true;
            }
          }
          else
          {
            if (literalValue.EndsWith("*"))
            {
              string litValue = literalValue.Substring(0, literalValue.Length - 1);
              if (tk.StartsWith(litValue))
              {
                return true;
              }
            }
            else
            {
              if (tk == literalValue)
              {
                return true;
              }
            }
          }
        }
      }
      else
      {
        if (literalValue.StartsWith("*") && literalValue.EndsWith("*"))
        {
          string litValue = literalValue.Substring(0, literalValue.Length - 2);
          if (tk.ToLower().Contains(litValue.ToLower()))
          {
            return true;
          }
        }
        else
        {
          if (literalValue.StartsWith("*"))
          {
            string litValue = literalValue.Substring(1);
            if (tk.ToLower().EndsWith(litValue.ToLower()))
            {
              return true;
            }
          }
          else
          {
            if (literalValue.EndsWith("*"))
            {
              string litValue = literalValue.Substring(0, literalValue.Length - 1);
              if (tk.ToLower().StartsWith(litValue.ToLower()))
              {
                return true;
              }
            }
            else
            {
              if (tk.ToLower() == literalValue.ToLower())
              {
                return true;
              }
            }
          }
        }
      }

      return false;
    }

    public bool TokenMatchesTypedValue(string tk, string dataType, string typeParms)
    {
      switch (dataType)
      {
        case "regex":
          string regExString = typeParms.Replace("*/rx:", String.Empty);
          var regex = new System.Text.RegularExpressions.Regex(regExString);
          bool isMatch = regex.IsMatch(tk);
          return isMatch;

        case "int":
          if (tk.IsTokenType(dataType))
          {
            if (typeParms.IsBlank())
            {
              return true;
            }
            else
            {
              string parmType = typeParms.Substring(0, 1).ToLower();
              string parmRest = typeParms.Substring(1);
              switch (parmType)
              {
                case "l":
                  int minLength = -1;
                  int maxLength = -1;

                  // format for "l" = length criteria is l/6 to specify a specific length (6)
                  // or l/6-7 to specify a range of lengths that are compared inclusively
                  string[] lengthTokens = parmRest.Split(Constants.DashDelimiter, StringSplitOptions.RemoveEmptyEntries);
                  if (lengthTokens.Length == 1)
                  {
                    if (lengthTokens[0].IsNotNumeric())
                      throw new Exception("The length tokens '" + lengthTokens[0] + "' is not numeric.");
                    minLength = lengthTokens[0].ToInt32();
                    maxLength = minLength;
                  }
                  else
                  {
                    // length criteria must be numeric and logically correct
                    if (lengthTokens.Length != 2)
                      throw new Exception("The specified parameters are not valid '" + typeParms + "'.  A maximum of two length tokens are allowed to specify a length range.");
                    if (lengthTokens[0].IsNotNumeric())
                      throw new Exception("The specified parameters are not valid '" + typeParms + "'.  The length range specifications must be numeric.");
                    if (lengthTokens[1].IsNotNumeric())
                      throw new Exception("The specified parameters are not valid '" + typeParms + "'.  The length range specifications must be numeric.");
                    minLength = lengthTokens[0].ToInt32();
                    maxLength = lengthTokens[1].ToInt32();
                    if (maxLength < minLength)
                      throw new Exception("The specified parameters are not valid '" + typeParms + "'.  The maximum length must be greater than the minimum length.");

                    string intToken = tk.ToInt32().ToString();

                    if (intToken.Length >= minLength && intToken.Length <= maxLength)
                    {
                      return true;
                    }
                  }
                  break;

                // only "l" = length criteria are processed currently
                default:
                  throw new Exception("The specified parameters are not currently supported '" + typeParms + "'.");
              }

            }
          }
          break;


        case "dec":
          if (tk.IsTokenType(dataType))
          {
            if (typeParms.IsBlank())
            {
              return true;
            }
          }
          break;

        case "decn":

          break;

      }

      return false;

    }

    public int AdjustInitialPositionForward(string text, int ptr)
    {
      // This method will (if necessary) advance the pointer (ptr) forward until it is positioned
      // at the beginning of the next text token. This is in preparation for locating the next token
      // for extraction or positioning.

      // If the pointer is positioned at position zero (at the beginning of the text), then the
      // behavior is controlled by options.  If the option "NoAdvanceFromZero" is defined then
      // the pointer will not be advanced to a position "between" tokens since there may be
      // no way to be "between" tokens if the first token starts at position zero which is typical.

      // position between tokens

      if (this.OptionsList.OptionIsDefined("NoAdvanceFromZero"))
      {
        // Under this option we only move forward to a position between tokens
        // if the pointer is greater than zero.

        if (ptr > 0)
        {
          while (text[ptr] != ' ' && text[ptr] != '\n')
          {
            ptr++;
            if (ptr > text.Length - 1)
              return ptr;
          }
        }
      }
      else
      {
        // The default (with no option set) is to move forward to a position between tokens
        // regardless if whether the pointer is at zero. This default function should eventually
        // be replaced with the "NoAdvanceFromZero" option as it prevents the location and
        // extraction of the first token.

        while (text[ptr] != ' ' && text[ptr] != '\n')
        {
          ptr++;
          if (ptr > text.Length - 1)
            return ptr;
        }
      }


      // find next non-blank and non-new-line character (beginning of next token)
      while (text[ptr] == ' ' || text[ptr] == '\n')
      {
        ptr++;
        if (ptr > text.Length - 1)
        {
          ptr = text.Length - 1;
          break;
        }
      }

      return ptr;
    }

    public int AdjustInitialPositionBackward(string text, int ptr)
    {
      // position between tokens
      while (text[ptr] != ' ' && text[ptr] != '\n')
      {
        ptr--;
        if (ptr < 0)
          return ptr;
      }

      // find prior non-blank character (potential end of token)
      while (text[ptr] == ' ' || text[ptr] == '\n')
      {
        ptr--;
        if (ptr < 0)
        {
          ptr = 0;
          break;
        }
      }

      return ptr;
    }


    public void Dispose() { }
  }
}
