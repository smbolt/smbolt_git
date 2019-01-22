using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class TextDetailSpec
  {
    public DetailSpecSwitch DetailSpecSwitch {
      get;
      private set;
    }
    public string DetailSpecData {
      get;
      private set;
    }
    public string StringValue {
      get;
      private set;
    }
    public string LowStringValue {
      get;
      private set;
    }
    public string HighStringValue {
      get;
      private set;
    }
    public decimal? NumericValue {
      get;
      private set;
    }
    public decimal? LowNumericValue {
      get;
      private set;
    }
    public decimal? HighNumericValue {
      get;
      private set;
    }
    public DateTime? DateValue {
      get;
      private set;
    }
    public DateTime? LowDateValue {
      get;
      private set;
    }
    public DateTime? HighDateValue {
      get;
      private set;
    }
    public List<decimal> NumericValueList {
      get;
      private set;
    }
    public List<string> StringValueList {
      get;
      private set;
    }
    public int Index {
      get;
      private set;
    }
    public object CompareValue {
      get {
        return Get_CompareValue();
      }
    }

    private TextNodeSpec _tns;
    private string _rawSpecifier;

    public TextDetailSpec(TextNodeSpec tns, string rawSpecifier)
    {
      try
      {
        _tns = tns;
        this.DetailSpecSwitch = DetailSpecSwitch.NotSet;
        this.DetailSpecData = String.Empty;
        this.StringValue = null;
        this.LowStringValue = null;
        this.HighStringValue = null;
        this.NumericValue = null;
        this.LowNumericValue = null;
        this.HighNumericValue = null;
        this.DateValue = null;
        this.LowDateValue = null;
        this.HighDateValue = null;
        this.Index = -1;

        if (_tns == null)
          throw new Exception("The reference to the TextNodeSpec object is null.");

        if (rawSpecifier.IsBlank())
          throw new Exception("A TextDetailSpecification object cannot be constructed from a null or empty string.");

        _rawSpecifier = rawSpecifier.Trim();

        ParseSpecifier();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred during the constructor of the TextDetailSpecification object.", ex);
      }
    }

    private void ParseSpecifier()
    {
      if (!_rawSpecifier.StartsWith("/"))
        throw new Exception("The initialization value of the TextDetailSpecification must start with a forward slash '/'. " +
                            "The initialization value received is '" + _rawSpecifier + "'.");

      string spec = _rawSpecifier.Substring(1);
      string specSwitch = String.Empty;
      string compareValue = String.Empty;

      if (spec.Contains(":"))
      {
        if (spec.StartsWith(":"))
          throw new Exception("The initialization value of the TextDetailSpecification is invalid. The colon must be placed immediately after " +
                              "the relational operator (i.e. /gt:5) - the value received is '" + _rawSpecifier + "'.");
        string[] tokens = spec.Split(Constants.ColonDelimiter, StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length != 2)
          throw new Exception("The initialization value of the TextDetailSpecification is invalid. When a colon is used to split the " +
                              "relational operator from the comparision value the result is expected to be two tokens.  The number of tokens " +
                              "from the split is " + tokens.Length.ToString() + ".");
        specSwitch = tokens[0].Trim().ToLower();
        compareValue = tokens[1].Trim();

        if (specSwitch == "split")
        {
          string[] splitTokens = compareValue.Split(Constants.OpenParen, StringSplitOptions.RemoveEmptyEntries);
          if (splitTokens.Length != 2)
            throw new Exception("The /split detail switch must contain an index value in parenthesis i.e. /split:x(0) - found + '" + _rawSpecifier + "'.");

          compareValue = splitTokens[0];

          string indexValue = splitTokens[1].Replace(")", String.Empty);
          if (indexValue.IsNotInteger())
            throw new Exception("The /split detail switch must contain a numeric index value in parenthesis i.e. /split:x(0) - found + '" + _rawSpecifier + "'.");

          this.Index = indexValue.ToInt32();
        }

        this.DetailSpecData = TranslateCompareValue(compareValue);
      }
      else
      {
        specSwitch = spec.Trim().ToLower();
      }

      switch (specSwitch)
      {
        case "gt":
          this.DetailSpecSwitch = DetailSpecSwitch.ValueGreaterThan;
          break;

        case "ge":
          this.DetailSpecSwitch = DetailSpecSwitch.ValueGreaterThanOrEqualTo;
          break;

        case "eq":
          this.DetailSpecSwitch = DetailSpecSwitch.ValueEquals;
          break;

        case "lt":
          this.DetailSpecSwitch = DetailSpecSwitch.ValueLessThan;
          break;

        case "le":
          this.DetailSpecSwitch = DetailSpecSwitch.ValueLessThanOrEqualTo;
          break;

        case "len":
          this.DetailSpecSwitch = DetailSpecSwitch.CheckLength;
          break;

        case "in":
          this.DetailSpecSwitch = DetailSpecSwitch.ValueInList;
          break;

        case "between":
          this.DetailSpecSwitch = DetailSpecSwitch.ValueBetween;
          break;

        case "dp":
          this.DetailSpecSwitch = DetailSpecSwitch.CheckDecimalPlaces;
          break;

        case "p":
          this.DetailSpecSwitch = DetailSpecSwitch.MatchesPattern;
          break;

        case "cs":
          this.DetailSpecSwitch = DetailSpecSwitch.CaseSensitive;
          break;

        case "or":
          this.DetailSpecSwitch = DetailSpecSwitch.UseOrLogic;
          break;

        case "lit":
          this.DetailSpecSwitch = DetailSpecSwitch.SuppressCondenseAndTrim;
          break;

        case "rx":
          this.DetailSpecSwitch = DetailSpecSwitch.MatchesRegex;
          break;

        case "nb":
          this.DetailSpecSwitch = DetailSpecSwitch.NotBlank;
          break;

        case "blank":
          this.DetailSpecSwitch = DetailSpecSwitch.Blank;
          break;

        case "contains":
          this.DetailSpecSwitch = DetailSpecSwitch.Contains;
          break;

        case "sw":
          this.DetailSpecSwitch = DetailSpecSwitch.StartsWith;
          break;

        case "ew":
          this.DetailSpecSwitch = DetailSpecSwitch.EndsWith;
          break;

        case "split":
          this.DetailSpecSwitch = DetailSpecSwitch.Split;
          break;

        case "index":
          this.DetailSpecSwitch = DetailSpecSwitch.Index;
          break;

        case "value":
          this.DetailSpecSwitch = DetailSpecSwitch.Value;
          break;

        case "after":
          this.DetailSpecSwitch = DetailSpecSwitch.After;
          break;

        case "before":
          this.DetailSpecSwitch = DetailSpecSwitch.Before;
          break;

        case "opt":
          this.DetailSpecSwitch = DetailSpecSwitch.Optional;
          break;

        case "dom":
          this.DetailSpecSwitch = DetailSpecSwitch.DayOfMonth;
          break;

        case "concat":
          this.DetailSpecSwitch = DetailSpecSwitch.Concatenate;
          break;

        case "compute":
          this.DetailSpecSwitch = DetailSpecSwitch.Compute;
          break;

        default:
          throw new Exception("The value '" + specSwitch + "' is not a recognized value for the DetailSpecSwitch.");
      }
    }

    private string TranslateCompareValue(string compareValue)
    {
      switch (compareValue)
      {
        case "$AMP":
          return "&";
        case "$COLON":
          return ":";
        case "$COMMA":
          return ",";
        case "$DASH":
          return "-";
      }

      return compareValue;
    }

    public void SetDetailDataProperties()
    {
      if (_tns == null)
        throw new Exception("The TextNodeSpec is null.");

      var ds = this.DetailSpecSwitch;
      string dd = this.DetailSpecData;

      if (ds == DetailSpecSwitch.Value)
      {
        _tns.IsValueProvider = true;
        return;
      }

      if (!_tns.SuppressCondenseAndTrim)
        dd = dd.CondenseText(true);

      var dataTypeSpec = _tns.DataTypeSpec;
      string[] range = null;

      switch (ds)
      {
        case DetailSpecSwitch.ValueGreaterThan:
        case DetailSpecSwitch.ValueGreaterThanOrEqualTo:
          switch (dataTypeSpec)
          {
            case DataTypeSpec.Integer:
              range = dd.ToRange();
              switch (range.Length)
              {
                case 1:
                case 2:
                case 3:
                  if (!range[0].IsValidInteger())
                    throw new Exception("The value '" + range[0] + "' is not a valid integer value.");
                  this.LowNumericValue = range[0].ToInt32();
                  if (ds == DetailSpecSwitch.ValueGreaterThanOrEqualTo)
                  {
                    this.NumericValue = range[0].ToInt32();
                  }
                  break;
              }
              break;
          }
          break;

        case DetailSpecSwitch.ValueLessThan:
        case DetailSpecSwitch.ValueLessThanOrEqualTo:
          switch (dataTypeSpec)
          {
            case DataTypeSpec.Integer:
              range = dd.ToRange();
              switch (range.Length)
              {
                case 1:
                case 2:
                case 3:
                  if (!range[0].IsValidInteger())
                    throw new Exception("The value '" + range[0] + "' is not a valid integer value.");
                  this.HighNumericValue = range[0].ToInt32();
                  if (ds == DetailSpecSwitch.ValueLessThanOrEqualTo)
                  {
                    this.NumericValue = range[0].ToInt32();
                  }
                  break;
              }
              break;
          }
          break;


        case DetailSpecSwitch.ValueEquals:
          switch (dataTypeSpec)
          {
            //case DataTypeSpec.Literal:
            //  this.StringValue = dd;
            //  break;

            case DataTypeSpec.String:
              range = dd.ToRange();
              switch (range.Length)
              {
                case 1:
                  this.StringValue = range[0];
                  break;

                default:
                  throw new Exception("The value '" + dd + "' does not represent a valid single comparision value. The number of comparision values found " +
                                      "is " + range.Length.ToString() + " - only one comparision value is allowed for detail specification switch " + ds.ToString() + ".");
              }
              this.StringValue = dd;
              break;

            case DataTypeSpec.Integer:
              range = dd.ToRange();
              switch (range.Length)
              {
                case 1:
                  if (!range[0].IsValidInteger())
                    throw new Exception("The value '" + range[0] + "' is not a valid integer value.");
                  this.NumericValue = range[0].ToInt32();
                  break;

                default:
                  throw new Exception("The value '" + dd + "' does not represent a valid single comparision value. The number of comparision values found " +
                                      "is " + range.Length.ToString() + " - only one comparision value is allowed for detail specification switch " + ds.ToString() + ".");
              }
              break;

            case DataTypeSpec.Decimal:
            case DataTypeSpec.DecimalWithRequiredDecimalPoint:
              // Allow the specification to not have a decimal point, but the located values must have
              // a decimal point when [dec] is specified as the data type.
              range = dd.ToRange();
              switch (range.Length)
              {
                case 1:
                  if (!range[0].IsDecimal())
                    throw new Exception("The value '" + range[0] + "' is not a valid decimal number with a decimal point.");
                  this.NumericValue = range[0].ToDecimal();
                  break;

                default:
                  throw new Exception("The value '" + dd + "' does not represent a valid single comparision value. The number of comparision values found " +
                                      "is " + range.Length.ToString() + " - only one comparision value is allowed for detail specification switch " + ds.ToString() + ".");
              }
              break;

            case DataTypeSpec.Date:
              range = dd.ToRange();
              switch (range.Length)
              {
                case 1:
                  if (!range[0].IsValidShortDate())
                    throw new Exception("The value '" + dd + "' is not a valid date.");
                  this.DateValue = range[0].ToDateTime();
                  break;

                default:
                  throw new Exception("The value '" + dd + "' does not represent a valid single comparision value. The number of comparision values found " +
                                      "is " + range.Length.ToString() + " - only one comparision value is allowed for detail specification switch " + ds.ToString() + ".");
              }
              break;

            default:
              throw new Exception("The DataTypeSpec '" + dataTypeSpec.ToString() + "' is not valid with DetailSpecSwitch + '" +
                                  ds.ToString() + "'.");
          }
          break;

        case DetailSpecSwitch.DayOfMonth:

          if (this.DetailSpecData.IsInteger())
          {
            int day = this.DetailSpecData.ToInt32();
            if (day < 1 || day > 31)
            {
              throw new Exception("A valid day of month must be between 1 and 31.");
            }
          }
          else
          {
            if (!this.DetailSpecData.ToLower().In("first,last"))
              throw new Exception("The value specified for DayOfMonth is not a valid integer or specifier, valid specifiers are 'first' and 'last'.");
          }

          break;

        case DetailSpecSwitch.NotBlank:
        case DetailSpecSwitch.Blank:
          this.StringValue = "*";
          break;

        case DetailSpecSwitch.MatchesPattern:
          throw new Exception("The processing for DetailSpecSwitch.MatchesPattern is not yet implemented.");

        case DetailSpecSwitch.MatchesRegex:
          throw new Exception("The processing for DetailSpecSwitch.MatchesRegex is not yet implemented.");

        case DetailSpecSwitch.CheckDecimalPlaces:
          // check also for range
          throw new Exception("The processing for DetailSpecSwitch.CheckDecimalPlaces is not yet implemented.");

        case DetailSpecSwitch.CheckLength:
          // check also for range
          throw new Exception("The processing for DetailSpecSwitch.CheckLength is not yet implemented.");

        case DetailSpecSwitch.ValueBetween:
          // require high and low values
          throw new Exception("The processing for DetailSpecSwitch.ValueBetween is not yet implemented.");

        case DetailSpecSwitch.ValueInList:
          // require list with at least one entry
          throw new Exception("The processing for DetailSpecSwitch.ValueInList is not yet implemented.");

      }
    }

    private object Get_CompareValue()
    {
      if (_tns == null)
        throw new Exception("The TextDetailsSpecification does not have a reference to the TextNodeSpec.");

      switch (_tns.DataTypeSpec)
      {
        case DataTypeSpec.String:
          switch (this.DetailSpecSwitch)
          {
            case DetailSpecSwitch.ValueGreaterThan:
            case DetailSpecSwitch.ValueGreaterThanOrEqualTo:
              return _tns.LowStringValue;

            case DetailSpecSwitch.ValueLessThan:
            case DetailSpecSwitch.ValueLessThanOrEqualTo:
              return _tns.HighStringValue;

            default:
              return _tns.StringValue;
          }

        case DataTypeSpec.Integer:
        case DataTypeSpec.Decimal:
        case DataTypeSpec.DecimalWithRequiredDecimalPoint:
          switch (this.DetailSpecSwitch)
          {
            case DetailSpecSwitch.ValueGreaterThan:
            case DetailSpecSwitch.ValueGreaterThanOrEqualTo:
              return _tns.LowNumericValue.Value;

            case DetailSpecSwitch.ValueLessThan:
            case DetailSpecSwitch.ValueLessThanOrEqualTo:
              return _tns.HighNumericValue.Value;

            default:
              return _tns.NumericValue.Value;
          }

        case DataTypeSpec.Date:
          switch (this.DetailSpecSwitch)
          {
            case DetailSpecSwitch.ValueGreaterThan:
            case DetailSpecSwitch.ValueGreaterThanOrEqualTo:
              return _tns.LowDateValue.Value;

            case DetailSpecSwitch.ValueLessThan:
            case DetailSpecSwitch.ValueLessThanOrEqualTo:
              return _tns.HighDateValue.Value;

            default:
              return _tns.DateValue.Value;
          }

        default:
          throw new Exception("The DataTypeSpec '" + _tns.DataTypeSpec.ToString() + "' is not yet implemented.");
      }
    }

  }
}
