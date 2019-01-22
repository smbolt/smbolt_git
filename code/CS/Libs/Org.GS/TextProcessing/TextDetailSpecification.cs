using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class TextDetailSpecification
  {
    public DetailSpecificationSwitch DetailSpecificationSwitch {
      get;
      private set;
    }
    public string DetailSpecificationData {
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

    private TextNodeComparisonSpecifier _comparisonSpecifier;
    private string _rawSpecifier;

    public TextDetailSpecification(TextNodeComparisonSpecifier comparisionSpecifier, string rawSpecifier)
    {
      try
      {
        _comparisonSpecifier = comparisionSpecifier;
        this.DetailSpecificationSwitch = DetailSpecificationSwitch.NotSet;
        this.DetailSpecificationData = String.Empty;
        this.StringValue = null;
        this.LowStringValue = null;
        this.HighStringValue = null;
        this.NumericValue = null;
        this.LowNumericValue = null;
        this.HighNumericValue = null;
        this.DateValue = null;
        this.LowDateValue = null;
        this.HighDateValue = null;

        if (_comparisonSpecifier == null)
          throw new Exception("The reference to the TextxNodeComparisionSpecifier object is null.");

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
        this.DetailSpecificationData = compareValue;
      }
      else
      {
        specSwitch = spec.Trim().ToLower();
      }

      switch (specSwitch)
      {
        case "gt":
          this.DetailSpecificationSwitch = DetailSpecificationSwitch.ValueGreaterThan;
          break;

        case "ge":
          this.DetailSpecificationSwitch = DetailSpecificationSwitch.ValueGreaterThanOrEqualTo;
          break;

        case "eq":
          this.DetailSpecificationSwitch = DetailSpecificationSwitch.ValueEquals;
          break;

        case "lt":
          this.DetailSpecificationSwitch = DetailSpecificationSwitch.ValueLessThan;
          break;

        case "le":
          this.DetailSpecificationSwitch = DetailSpecificationSwitch.ValueLessThanOrEqualTo;
          break;

        case "len":
          this.DetailSpecificationSwitch = DetailSpecificationSwitch.CheckLength;
          break;

        case "in":
          this.DetailSpecificationSwitch = DetailSpecificationSwitch.ValueInList;
          break;

        case "between":
          this.DetailSpecificationSwitch = DetailSpecificationSwitch.ValueBetween;
          break;

        case "dp":
          this.DetailSpecificationSwitch = DetailSpecificationSwitch.CheckDecimalPlaces;
          break;

        case "p":
          this.DetailSpecificationSwitch = DetailSpecificationSwitch.MatchesPattern;
          break;

        case "cs":
          this.DetailSpecificationSwitch = DetailSpecificationSwitch.CaseSensitive;
          break;

        case "or":
          this.DetailSpecificationSwitch = DetailSpecificationSwitch.UseOrLogic;
          break;

        case "lit":
          this.DetailSpecificationSwitch = DetailSpecificationSwitch.SuppressCondenseAndTrim;
          break;

        case "rx":
          this.DetailSpecificationSwitch = DetailSpecificationSwitch.MatchesRegex;
          break;

        case "nb":
          this.DetailSpecificationSwitch = DetailSpecificationSwitch.NotBlank;
          break;

        case "blank":
          this.DetailSpecificationSwitch = DetailSpecificationSwitch.Blank;
          break;

        default:
          throw new Exception("The value '" + specSwitch + "' is not a recognized value for the DetailSpecificationSwitch.");
      }
    }

    public void SetDetailDataProperties()
    {
      if (_comparisonSpecifier == null)
        throw new Exception("The TextNodeComparisionSpecifier is null.");

      var ds = this.DetailSpecificationSwitch;
      string dd = this.DetailSpecificationData;

      if (!_comparisonSpecifier.SuppressCondenseAndTrim)
        dd = dd.CondenseText(true);

      var dataTypeSpec = _comparisonSpecifier.DataTypeSpec;
      string[] range = null;

      switch (ds)
      {
        case DetailSpecificationSwitch.ValueGreaterThan:
        case DetailSpecificationSwitch.ValueGreaterThanOrEqualTo:
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
                  if (ds == DetailSpecificationSwitch.ValueGreaterThanOrEqualTo)
                  {
                    this.NumericValue = range[0].ToInt32();
                  }
                  break;
              }
              break;
          }
          break;

        case DetailSpecificationSwitch.ValueLessThan:
        case DetailSpecificationSwitch.ValueLessThanOrEqualTo:
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
                  if (ds == DetailSpecificationSwitch.ValueLessThanOrEqualTo)
                  {
                    this.NumericValue = range[0].ToInt32();
                  }
                  break;
              }
              break;
          }
          break;


        case DetailSpecificationSwitch.ValueEquals:
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
              throw new Exception("The DataTypeSpec '" + dataTypeSpec.ToString() + "' is not valid with DetailSpecificationSwitch + '" +
                                  ds.ToString() + "'.");
          }
          break;

        case DetailSpecificationSwitch.NotBlank:
        case DetailSpecificationSwitch.Blank:
          this.StringValue = "*";
          break;

        case DetailSpecificationSwitch.MatchesPattern:
          throw new Exception("The processing for DetailSpecificationSwitch.MatchesPattern is not yet implemented.");

        case DetailSpecificationSwitch.MatchesRegex:
          throw new Exception("The processing for DetailSpecificationSwitch.MatchesRegex is not yet implemented.");

        case DetailSpecificationSwitch.CheckDecimalPlaces:
          // check also for range
          throw new Exception("The processing for DetailSpecificationSwitch.CheckDecimalPlaces is not yet implemented.");

        case DetailSpecificationSwitch.CheckLength:
          // check also for range
          throw new Exception("The processing for DetailSpecificationSwitch.CheckLength is not yet implemented.");

        case DetailSpecificationSwitch.ValueBetween:
          // require high and low values
          throw new Exception("The processing for DetailSpecificationSwitch.ValueBetween is not yet implemented.");

        case DetailSpecificationSwitch.ValueInList:
          // require list with at least one entry
          throw new Exception("The processing for DetailSpecificationSwitch.ValueInList is not yet implemented.");

      }

    }

  }
}
