using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS.Math;

namespace Org.GS
{
  public static class TextNodeSpecExtensionMethods
  {
    public static bool StringTokenMatch(this TextNodeSpec tns, string token)
    {
      string stringValue = tns.StringValue.Trim();

      if (stringValue.IsBlank())
        stringValue = "*";

      // need to consider how to process strings...
      // when the value of tns.StringValue is blank it will be set to "*" which means "any string"
      // if there are not DetailSpecs, we need to decide whether the string can be blank, non-blank, null, etc.
      // need default rules - all the explicit rules must be in detail specs...

      if (stringValue == "*")
      {
        if (tns.DetailSpecs.Count == 0)
          return true;


        // Results for each DetailSpecification are accumulated in the DetailSpecResults collection (List<bool>)
        // and are summarized in the the list's .Resolve extension method depending on whether AND or OR logic is being used.
        foreach (var spec in tns.DetailSpecs)
        {
          if (spec.DetailSpecSwitch.IsRelationalOperator())
          {
            tns.DetailSpecResults.Add(token.CompareValues(spec.CompareValue, tns.DataTypeSpec, spec.DetailSpecSwitch));
            continue;
          }

          string tokenCompare = tns.CaseSensitive ? token : token.ToLower();
          string specData = tns.CaseSensitive ? spec.DetailSpecData : spec.DetailSpecData.ToLower();

          switch (spec.DetailSpecSwitch)
          {
            case DetailSpecSwitch.Split:
              string[] tokens = tokenCompare.Split(new string[] { specData }, StringSplitOptions.RemoveEmptyEntries);
              if (spec.Index > tokens.Length - 1)
                throw new Exception("The text node when split using the value '" + specData + "' has " + tokens.Length.ToString() + " tokens which is " +
                                    "less than the index being referenced which is " + spec.Index.ToString() + ".");
              tns.ProcessedValue = tokens[spec.Index].Trim();
              tns.DetailSpecResults.Add(true);
              break;

            case DetailSpecSwitch.Contains:
              tns.DetailSpecResults.Add(tokenCompare.Contains(specData));
              break;

            case DetailSpecSwitch.Blank:
              tns.DetailSpecResults.Add(tokenCompare.IsBlank());
              break;

            case DetailSpecSwitch.NotBlank:
              tns.DetailSpecResults.Add(tokenCompare.IsNotBlank());
              break;

            case DetailSpecSwitch.Value:
              break;

            default:
              throw new Exception("The Detail Specification " + spec.DetailSpecSwitch.ToString() + " has not been implemented yet.");
          }
        }

        return tns.DetailSpecResults.Resolve(tns.UseOrLogic);
      }


      if (tns.DetailSpecs.Count > 0)
      {
        // need to rework this... the literal comparisions can use "contains", case sensitivity, etc. 
        // need to think through comparisions involving literal values.
        throw new Exception("TextNodeSpec comparisions to literal string values cannot have DetailSpecifications.");
      }

      string tokenValue = token;

      if (!tns.CaseSensitive)
      {
        stringValue = stringValue.ToLower();
        tokenValue = tokenValue.ToLower();
      }

      if (stringValue.EndsWith("*"))
      {
        string compareValue = stringValue.Substring(0, stringValue.Length - 1);
        return tokenValue.Trim().StartsWith(compareValue);
      }
      else
      {
        if (stringValue.StartsWith("*"))
        {
          string compareValue = stringValue.Substring(1);
          return tokenValue.Trim().EndsWith(compareValue);
        }
        else
        {
          return stringValue == tokenValue.Trim();
        }
      }
    }

    public static bool IntegerTokenMatch(this TextNodeSpec tns, string token)
    {
      if (!token.IsInteger())
        return false;

      if (tns.DetailSpecs.Count == 0)
        return true;

      foreach (var spec in tns.DetailSpecs)
      {
        if (spec.DetailSpecSwitch.IsRelationalOperator())
        {
          tns.DetailSpecResults.Add(token.CompareValues(spec.CompareValue, tns.DataTypeSpec, spec.DetailSpecSwitch));
          continue;
        }

        switch (spec.DetailSpecSwitch)
        {
          default:
            throw new Exception("The Detail Spec " + spec.DetailSpecSwitch.ToString() + " has not been implemented yet.");
        }
      }

      return tns.DetailSpecResults.Resolve(tns.UseOrLogic);
    }

    public static bool DecimalTokenMatch(this TextNodeSpec tns, string token, bool decimalPointRequired = false)
    {
      if (!token.IsDecimal())
        return false;

      if (tns.DetailSpecs.Count == 0)
        return true;

      throw new NotImplementedException("DetailSpecs are not yet implemented for Decimal values in the TextNodeSpec class.");
    }

    public static bool DateTokenMatch(this TextNodeSpec tns, string token)
    {
      if (!token.CheckIsDate())
        return false;

      if (tns.DetailSpecs.Count == 0)
        return true;

      throw new NotImplementedException("DetailSpecs are not yet implemented for Date values in the TextNodeSpec class.");
    }

    public static string ProcessValue(this TextNodeSpec tns, string textIn, bool validate = false)
    {
      string textWork = tns.TextNodeBefore(textIn);
      textWork = tns.TextNodeAfter(textWork);
      textWork = tns.SplitTextNode(textWork);
      textWork = tns.TransformTextNode(textWork);
      //textWork = tns.Compute(textWork);
      textWork = tns.ValidateTextNode(textWork, validate);
      return tns.FormatTextNode(textWork);
    }

    public static string Compute(this TextNodeSpec tns, string textIn)
    {
      TextDetailSpec computeSpec = tns.DetailSpecs.Where(s => s.DetailSpecSwitch == DetailSpecSwitch.Compute).FirstOrDefault();

      if (computeSpec == null || textIn.IsBlank())
        return textIn;

      var equation = new Equation();


      return textIn;
    }

    public static string TextNodeBefore(this TextNodeSpec tns, string textIn)
    {
      TextDetailSpec beforeSpec = tns.DetailSpecs.Where(s => s.DetailSpecSwitch == DetailSpecSwitch.Before).FirstOrDefault();

      if (beforeSpec == null || textIn.IsBlank())
        return textIn;

      int pos = textIn.ToLower().IndexOf(beforeSpec.DetailSpecData.ToLower());

      if (pos == -1)
        return textIn;

      return textIn.Substring(0, pos).Trim();
    }

    public static string TextNodeAfter(this TextNodeSpec tns, string textIn)
    {
      TextDetailSpec afterSpec = tns.DetailSpecs.Where(s => s.DetailSpecSwitch == DetailSpecSwitch.After).FirstOrDefault();

      if (afterSpec == null || textIn.IsBlank())
        return textIn;

      int pos = textIn.ToLower().IndexOf(afterSpec.DetailSpecData.ToLower());

      if (pos == -1)
        return textIn;

      int endOfFoundToken = pos + afterSpec.DetailSpecData.Length;
      string afterText = textIn.Substring(endOfFoundToken);
      return afterText.Trim();
    }

    public static string SplitTextNode(this TextNodeSpec tns, string textIn)
    {
      TextDetailSpec splitSpec = tns.DetailSpecs.Where(s => s.DetailSpecSwitch == DetailSpecSwitch.Split).FirstOrDefault();

      if (splitSpec == null)
        return textIn;

      string[] tokens = textIn.Split(new string[] { splitSpec.DetailSpecData }, StringSplitOptions.RemoveEmptyEntries);

      if (splitSpec.Index > tokens.Length - 1)
        return String.Empty;

      return tokens[splitSpec.Index].Trim();
    }

    public static string TransformTextNode(this TextNodeSpec tns, string textIn)
    {
      TextDetailSpec concatSpec = tns.DetailSpecs.Where(s => s.DetailSpecSwitch == DetailSpecSwitch.Concatenate).FirstOrDefault();
      if (concatSpec != null)
      {
        if (concatSpec.DetailSpecData.StartsWith("$"))
        {
          string variableName = concatSpec.DetailSpecData.Substring(1);
          string variableValue = String.Empty;

          if (tns.GlobalVariables.ContainsKey(variableName))
            variableValue = tns.GlobalVariables[variableName];

          if (tns.LocalVariables.ContainsKey(variableName))
            variableValue = tns.LocalVariables[variableName];

          if (variableValue.IsBlank())
            throw new Exception("The value of the variable '" + variableName + "' is blank or not found - " +
                                "TextNodeSpec is '" + tns.Report + "'.");

          return textIn + variableValue;
        }
        else
        {
          return textIn.Trim() + concatSpec.DetailSpecData.Trim();
        }
      }

      TextDetailSpec domSpec = tns.DetailSpecs.Where(s => s.DetailSpecSwitch == DetailSpecSwitch.DayOfMonth).FirstOrDefault();
      if (domSpec != null)
      {
        DateTime dateValue = DateTime.MinValue;
        if (!DateTime.TryParse(textIn, out dateValue))
          throw new Exception("The value cannot be parsed into a DateTime object '" + textIn + "' TextNodeSpec = '" + tns.Report + "'.");

        switch (domSpec.DetailSpecData.ToLower())
        {
          case "first":
            return dateValue.ToBeginOfMonth().ToString();

          case "last":
            return dateValue.ToEndOfMonth().ToString();

          default:
            int dayOfMonth = domSpec.DetailSpecData.ToInt32();
            int lastDayOfMonth = dateValue.LastDayOfMonth();
            if (dayOfMonth > lastDayOfMonth)
              dayOfMonth = lastDayOfMonth;
            return new DateTime(dateValue.Year, dateValue.Month, dayOfMonth).ToString();
        }
      }

      TextDetailSpec containsSpec = tns.DetailSpecs.Where(s => s.DetailSpecSwitch == DetailSpecSwitch.Contains).FirstOrDefault();

      if (tns.TransformSpec.IsBlank())
      {
        if (containsSpec == null)
          return textIn;

        if (textIn.IsBlank())
          return false.ToString();

        bool containsResult = textIn.ToLower().Trim().Contains(containsSpec.DetailSpecData.ToLower());
        return containsResult.ToString();
      }
            
      string[] txTokens = tns.TransformSpec.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);

      foreach (var txToken in txTokens)
      {
        string[] txSubTokens = txToken.Split(Constants.EqualsDelimiter, StringSplitOptions.RemoveEmptyEntries);
        if (txSubTokens.Length != 2)
          throw new Exception("The /tx (translate) spec is invalid, two sub-tokens are required, separated by '=' - found '" + tns.TransformSpec + "'.");

        bool contains = false;
        string fromValue = txSubTokens[0].Trim();
        string toValue = txSubTokens[1].Trim().GetSingleQuotedText();

        if (fromValue.StartsWith("~"))
        {
          contains = true;
          fromValue = fromValue.Substring(1);
        }

        fromValue = fromValue.GetSingleQuotedText();

        if (fromValue == "*")
        {
          if (toValue == "ex")
          {
            throw new Exception("The /tx (translate) spec '" + tns.TransformSpec + "' does not match the source value '" + textIn + "' " +
                                "and the spec requires an exception to be thrown when no match is found.");
          }
        }

        if (contains)
        {
          if (textIn.Contains(fromValue))
          {
            textIn = toValue;
            break;
          }
        }
        else
        {
          if (textIn.Trim().CompareTo(fromValue.Trim()) == 0)
          {
            textIn = toValue;
            break;
          }
        }
      }

      if (containsSpec == null)
      {
        return textIn;
      }
      else
      {

      }

      return textIn;
    }

    public static string ValidateTextNode(this TextNodeSpec tns, string textIn, bool validate)
    {
      if (!validate)
        return textIn;

      if (textIn.IsBlank())
      {
        if (tns.IsOptional)
          return textIn;
        else
          throw new Exception("The text node is blank or null but is required.");
      }

      switch (tns.DataTypeSpec)
      {
        case DataTypeSpec.Integer:
          if (textIn.IsNotInteger())
            throw new Exception("The text node is required to be an integer, but the value '" + textIn + "' was found.");
          break;

        case DataTypeSpec.Decimal:
          if (!textIn.IsDecimal(false))
            throw new Exception("The text node is required to be a decimal value (decimal point optional), but the value '" + textIn + "' was found.");
          break;

        case DataTypeSpec.DecimalWithRequiredDecimalPoint:
          if (!textIn.IsDecimal(true))
            throw new Exception("The text node is required to be a decimal value (with decimal point required), but the value '" + textIn + "' was found.");
          break;

        case DataTypeSpec.Date:
          if (!textIn.CheckIsDate())
            throw new Exception("The text node is required to be a date value but the value '" + textIn + "' was found.");
          break;
      }

      return textIn;
    }

    public static string FormatTextNode(this TextNodeSpec tns, string textIn)
    {
      if (tns.FormatSpec.IsBlank())
        return textIn;

      switch (tns.DataTypeSpec)
      {
        case DataTypeSpec.String:
        case DataTypeSpec.Literal:
          return textIn;

        case DataTypeSpec.Date:
          DateTime dt;
          if (!DateTime.TryParse(textIn, out dt))
            throw new Exception("The value '" + textIn + "' could not be parsed into a DateTime object so that it could be formatted.");
          return dt.ToString(tns.FormatSpec);

        default:
          throw new Exception("The /fmt switch on the is not yet implemented in the FormatTextNode method.");
      }
    }

    public static bool Resolve(this List<bool> results, bool useOrLogic)
    {
      // disallow defaults based on null or empty array
      if (results == null || results.Count == 0)
        throw new Exception("The results array is null or empty.");

      // if using "OR" logic, any "true" count resolves to true
      if (useOrLogic)
        return results.Where(r => r == true).Count() > 0;

      // if using "AND" logic (the default), any false count resolves to false 
      return results.Where(r => r == false).Count() == 0;
    }

    public static bool IsRelationalOperator(this DetailSpecSwitch s)
    {
      switch (s)
      {
        case DetailSpecSwitch.ValueLessThan:
        case DetailSpecSwitch.ValueLessThanOrEqualTo:
        case DetailSpecSwitch.ValueEquals:
        case DetailSpecSwitch.ValueGreaterThan:
        case DetailSpecSwitch.ValueGreaterThanOrEqualTo:
          return true;
      }

      return false;
    }
  }
}
