using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class TextNodeSpec
  {
    private string _originalValue;
    private string _rawValue;
    public string RawValue { get { return _rawValue; } }

    // data elements derived from the comparison value
    public DataTypeSpec DataTypeSpec { get; private set; }
    public List<TextDetailSpec> DetailSpecs { get; private set; }
    public List<DetailSpecSwitch> DetailSpecSwitches { get; private set; }
    public bool DetailSpecified { get { return this.DetailSpecs != null && this.DetailSpecs.Count > 0; } }
    public List<bool> DetailSpecResults { get; set; }
    
    public bool IsNegated { get; private set; }
    public bool UseRegex { get; private set; }
    public bool UseOrLogic { get; private set; }
    public bool CaseSensitive { get; private set; }
    public bool SuppressCondenseAndTrim { get; private set; }
    public bool IsValueProvider { get; set; }
    
    public decimal? NumericValue { get; set; }
    public decimal? HighNumericValue { get; set; }
    public decimal? LowNumericValue { get; set; }
    public string StringValue { get; set; }
    public string HighStringValue { get; set; }
    public string LowStringValue { get; set; }
    public DateTime? DateValue { get; set; }
    public DateTime? HighDateValue { get; set; }
    public DateTime? LowDateValue { get; set; }
    public string TransformSpec { get; set; }
    public string FormatSpec { get; set; }
    public string SplitSpec { get; set; }
    public bool IsOptional { get; set; }
    public string ProcessedValue { get; set; }

    public Dictionary<string, string> GlobalVariables { get; set; }
    public Dictionary<string, string> LocalVariables { get; set; }
    
    public string Report { get { return Get_Report(); } }
    private bool _isBracketed;
    
    public TextNodeSpec(string rawValue)
    {
      try
      {
        if (rawValue.IsBlank())
          throw new Exception("Cannot create a TextNodeSpec object from a null or empty string.");

        _originalValue = rawValue;
        _rawValue = rawValue.Trim().CondenseText(true);

        Initialize();
        ParseInput();
        PreValidation();
        AssertValid();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the constructor of the TextNodeSpec - the rawValue parameter is '" + rawValue + "'.", ex);
      }
    }

    public bool TokenMatch(string token)
    {
      try
      {
        this.DetailSpecResults.Clear();

        switch (this.DataTypeSpec)
        {
          case DataTypeSpec.Integer: return this.IntegerTokenMatch(token);
          case DataTypeSpec.Decimal: return this.DecimalTokenMatch(token);
          case DataTypeSpec.DecimalWithRequiredDecimalPoint: return this.DecimalTokenMatch(token, true);
          case DataTypeSpec.Date: return this.DateTokenMatch(token);
          case DataTypeSpec.String: return this.StringTokenMatch(token);

          default:
            throw new NotImplementedException("The TextNodeSpec TokenMatch method is not implemented for DataTypeSpec = " + this.DataTypeSpec.ToString() + ".");
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to match the token received '" +
                            (token == null ? "[NULL]" : token.IsBlank() ? "[BLANK]" : token) + " with the TextNodeSpec " +
                            this.Report + ".", ex);
      }
    }

    private void ParseInput()
    {
      try
      {
        // This parsing routine looks for specific characters, sets values when certain characters are found and
        // removes them from the string for subsequent parsing.

        // Get negation operator if present.
        if (_rawValue.StartsWith("!"))
        {
          if (_rawValue.Length == 1)
            throw new Exception("A TextNodeSpec object cannot be created from the value '!'.");
          this.IsNegated = true;
          _rawValue = _rawValue.Substring(1);
        }

        // Ensure that brackets are placed correctly if used.
        AssertValidBracketUse(_rawValue);

        _isBracketed = _rawValue.IsBracketed();

        // Remove brackets if used.
        if (_isBracketed)
          _rawValue = _rawValue.GetBracketedText();

        // Set the data type.
        _rawValue = SetDataType(_rawValue);

        // Process detail specification switches and data.
        while (_rawValue.Contains("/"))
        {
          _rawValue = ProcessSwitches(_rawValue);
        }

        // If there is left over data, that's a problem... 
        if (_rawValue.IsNotBlank())
          throw new Exception("The text not comparision specifier has left over text in it after the removal of all expected elements. The remaining text is '" +
                              _rawValue + "'. The TextNodeSpec object dump follows: " + g.crlf + this.Report);

        // The detail specifications (essentially the parameter values for the switches) get processed after
        // all switches are processed.  Because some switches have broad effect (such as 'lit' which suppresses 
        // trimming and condensing text), processing the detail specificaitons must wait until all switches are loaded.
        ProcessDetailSpecs();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to initialize the TextNodeSpec object from raw value '" + RawValue + "'.", ex); 
      }
    }

    private string SetDataType(string s)
    {
      if (s.IsBlank())
        throw new Exception("The data type of the text comparision value cannot be determined from a blank or null string.");
  
      s = s.Trim();
      
      int startType = s.ToLower().IndexOf("/type:");
      if (startType != -1)
      {
        // get the type clause
        string typeClause = startType == 0 ? s : s.Substring(startType);
        int endType = typeClause.IndexOf("/", 1);
        typeClause = endType == -1 ? typeClause : typeClause.Substring(0, endType);

        // Use the position of the type clause to determine how to recompose the remaining string.
        startType = s.IndexOf(typeClause);
        endType = startType + typeClause.Length - 1;
        string preClause = startType == 0 ? String.Empty : s.Substring(0, startType);
        string postClause = s.Substring(endType + 1);
        s = preClause + postClause;

        typeClause = typeClause.ToLower().Replace("/type:", String.Empty);
        this.DataTypeSpec = GetDataTypeSpec(typeClause);

        return s;
      }

      if (_isBracketed)
      {
        // Locate trailing close bracket or forward slash (start of detail specification switch) to determine
        // the end of the comparision value and whether remaining data is present.
        int pos = s.IndexOfAny(Constants.FSlashOrCloseBracket);

        // Get the comparision value (preserve original case - needed for literal values).
        string comparisonValue = pos > -1 ? s.Substring(0, pos) : s;
        this.StringValue = comparisonValue.Replace("'", "");

        // Get any remaining value which will contain detail specification data if present.
        string remainingValue = pos > -1 ? s.Substring(pos).Trim() : String.Empty;

        this.DataTypeSpec = GetDataTypeSpec(comparisonValue);

        return remainingValue;
      }
      else
      {
        int pos = s.IndexOfAny(Constants.FSlashDelimiter);

        // Get the comparision value (preserve original case - needed for literal values).
        string comparisonValue = pos > -1 ? s.Substring(0, pos) : s;
        this.StringValue = comparisonValue.Replace("'", "");

        // Get any remaining value which will contain detail specification data if present.
        string remainingValue = pos > -1 ? s.Substring(pos).Trim() : String.Empty;

        this.DataTypeSpec = DataTypeSpec.String;

        return remainingValue;
      }
    }


    private string ProcessSwitches(string s)
    {
      s = s.Trim();

      if (!s.Contains("/"))
        return s; 

      string doubleSlashReplacement = "\xA4\xA4";
      s = s.Replace("//", doubleSlashReplacement);      

      if (!s.StartsWith("/"))
        throw new Exception("The value of the detail specification must begin with a forward slash (/) - found '" + s + "'.");

      // Split off the first detail spec which is the text before the next forward slash (the start of the next detail spec)
      // or the whole body of text if no subsequent forward slash exists.
      int posNextSlash = s.IndexOf("/", 1);
      string detailSpecText = posNextSlash > -1 ? s.Substring(0, posNextSlash) : s;
      string remainingValue = posNextSlash > -1 ? s.Substring(posNextSlash).Trim() : String.Empty;

      detailSpecText = detailSpecText.Replace(doubleSlashReplacement, "/");
      remainingValue = remainingValue.Replace(doubleSlashReplacement, "//"); 

      if (detailSpecText.ToLower().StartsWith("/fmt:"))
      {
        this.FormatSpec = detailSpecText.Substring(5);
        return remainingValue;
      }

      if (detailSpecText.ToLower().StartsWith("/tx:"))
      {
        this.TransformSpec = detailSpecText.Substring(4);
        return remainingValue;
      }

      var detailSpec = new TextDetailSpec(this, detailSpecText);
      this.DetailSpecs.Add(detailSpec);
      return remainingValue;
    }

    private void ProcessDetailSpecs()
    {
      foreach (var ds in this.DetailSpecs)
      {
        ds.SetDetailDataProperties();
        this.DetailSpecSwitches.Add(ds.DetailSpecSwitch);
      }
    }

    private DataTypeSpec GetDataTypeSpec(string s)
    {
      switch (s.ToLower().Trim())
      {
        case "int":
          return DataTypeSpec.Integer;
          
        case "dec":
          return DataTypeSpec.DecimalWithRequiredDecimalPoint;
          
        case "decn":
          return DataTypeSpec.Decimal;

        case "date":
          return DataTypeSpec.Date;

        case "str":
        case "*":
          return DataTypeSpec.String;
      }

      throw new Exception("An invalid data type is specified '" + s + "'.");
    }

    private void Initialize()
    {
      this.DetailSpecs = new List<TextDetailSpec>();
      this.DetailSpecSwitches = new List<DetailSpecSwitch>();
      this.DetailSpecResults = new List<bool>();
      this.GlobalVariables = new Dictionary<string, string>();
      this.LocalVariables = new Dictionary<string, string>();

      _isBracketed = false;
      this.DataTypeSpec = DataTypeSpec.NotSet;
      this.IsNegated = false;
      this.IsValueProvider = false;
      this.SplitSpec = String.Empty;
      this.TransformSpec = String.Empty;
      this.FormatSpec = String.Empty;
      this.IsOptional = false;
    }

    private void PreValidation()
    {
      try
      {
        if (this.DetailSpecs == null)
          throw new Exception("The DetailSpecs collection is null.");

        this.UseOrLogic = this.DetailSpecs.Where(s => s.DetailSpecSwitch == DetailSpecSwitch.UseOrLogic).Count() > 0;
        this.UseRegex = this.DetailSpecs.Where(s => s.DetailSpecSwitch == DetailSpecSwitch.MatchesRegex).Count() > 0;
        this.CaseSensitive = this.DetailSpecs.Where(s => s.DetailSpecSwitch == DetailSpecSwitch.CaseSensitive).Count() > 0;
        this.SuppressCondenseAndTrim = this.DetailSpecs.Where(s => s.DetailSpecSwitch == DetailSpecSwitch.SuppressCondenseAndTrim).Count() > 0;
        this.IsOptional = this.DetailSpecs.Where(s => s.DetailSpecSwitch == DetailSpecSwitch.Optional).Count() > 0;

        foreach (var spec in this.DetailSpecs)
        {
          if (this.DetailSpecs.Contains(spec) && this.DetailSpecs.Count(x => x == spec) > 1)
            throw new Exception("The detail specifications contain more than one switch of the same type " + spec.DetailSpecSwitch.ToString() + "." + g.crlf +
                                "You can only have one switch type per set of specifications.");

          var specSwitch = spec.DetailSpecSwitch;
          var dataTypeSpec = this.DataTypeSpec;

          switch(specSwitch)
          {
            case DetailSpecSwitch.ValueGreaterThan:
            case DetailSpecSwitch.ValueGreaterThanOrEqualTo:
              switch(dataTypeSpec)
              {
                case DataTypeSpec.Integer:
                  if (!this.LowNumericValue.IsNotBlank())
                    this.LowNumericValue = spec.LowNumericValue;
                  else
                    throw new Exception("An exception occurred attempting to set a low numeric value in the TextNodeSpec, the value was already set.");
                  break;

                case DataTypeSpec.Date:
                  if (!this.LowDateValue.IsNotBlank())
                    this.LowDateValue = spec.LowDateValue;
                  else
                    throw new Exception("An exception occurred attempting to set a low date value in the TextNodeSpec, the value was already set.");
                  break;

                case DataTypeSpec.String:
                  if (!this.LowStringValue.IsNotBlank())
                    this.LowStringValue = spec.LowStringValue;
                  else
                    throw new Exception("An exception occurred attempting to set a low string value in the TextNodeSpec, the value was already set.");
                  break;

              }
              break;

            case DetailSpecSwitch.ValueLessThan:
            case DetailSpecSwitch.ValueLessThanOrEqualTo:
              switch(dataTypeSpec)
              {
                case DataTypeSpec.Integer:
                  if (!this.HighNumericValue.IsNotBlank())
                    this.HighNumericValue = spec.HighNumericValue;
                  else
                    throw new Exception("An exception occurred attempting to set a high numeric value in the TextNodeSpec, the value was already set.");
                  break;

                case DataTypeSpec.Date:
                  if (!this.HighDateValue.IsNotBlank())
                    this.HighDateValue = spec.HighDateValue;
                  else
                    throw new Exception("An exception occurred attempting to set a high date value in the TextNodeSpec, the value was already set.");
                  break;

                case DataTypeSpec.String:
                  if (!this.HighStringValue.IsNotBlank())
                    this.HighStringValue = spec.HighStringValue;
                  else
                    throw new Exception("An exception occurred attempting to set a high string value in the TextNodeSpec, the value was already set.");
                  break;
              }
              break;

            case DetailSpecSwitch.ValueEquals:
              switch(dataTypeSpec)
              {
                case DataTypeSpec.Integer:
                  if (!this.NumericValue.IsNotBlank())
                    this.NumericValue = spec.NumericValue;
                  else
                    throw new Exception("An exception occurred attempting to set a numeric value in the TextNodeSpec, the value was already set.");
                  break;

                case DataTypeSpec.Date:
                  if (!this.DateValue.IsNotBlank())
                    this.DateValue = spec.DateValue;
                  else
                    throw new Exception("An exception occurred attempting to set a date value in the TextNodeSpec, the value was already set.");
                  break;

                case DataTypeSpec.String:
                  if (!this.StringValue.IsNotBlank())
                    this.StringValue = spec.StringValue;
                  else
                    throw new Exception("An exception occurred attempting to set a string value in the TextNodeSpec, the value was already set.");
                  break;
              }
              break;

            case DetailSpecSwitch.NotBlank:
            case DetailSpecSwitch.Blank:
              switch(dataTypeSpec)
              {
                case DataTypeSpec.String:
                  if (this.StringValue.IsBlank())
                  this.StringValue = spec.StringValue;
                  break;
              }

              break;

            case DetailSpecSwitch.DayOfMonth:
              if (dataTypeSpec != DataTypeSpec.Date)
              {
                throw new Exception("The DayOfMonth switch requires a DataTypeSpec of 'Date', you have a Spec of " + dataTypeSpec.ToString() + " please change your DataTypeSpec.");
              }

              break;

            case DetailSpecSwitch.Concatenate:
              if (spec.DetailSpecData.IsBlank())
                throw new Exception("The Concatenate switch requires a value in the DetailSpecData representing the value that " +
                                    "is to be concatenated - TextNodeSpec is '" + this.Report + "'.");
              break;

            case DetailSpecSwitch.Compute:
              if (spec.DetailSpecData.IsBlank())
                throw new Exception("The Compute switch requires a value in the DetailSpecData representing the value that " +
                                    "is to be computed - TextNodeSpec is '" + this.Report + "'.");
              break;
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to populate the parent properties from the detail specifications.", ex);
      }
    }

    private void AssertValid()
    {
      try
      {
        if (this.DetailSpecs.Count <= 1)
          return;
        
        if (!UseOrLogic)
        {
          bool gtAndLt = DetailSpecSwitches.Contains(DetailSpecSwitch.ValueGreaterThan) && DetailSpecSwitches.Contains(DetailSpecSwitch.ValueLessThan);
          bool gteAndLte = DetailSpecSwitches.Contains(DetailSpecSwitch.ValueGreaterThanOrEqualTo) && DetailSpecSwitches.Contains(DetailSpecSwitch.ValueLessThanOrEqualTo);
          bool gtAndLte = DetailSpecSwitches.Contains(DetailSpecSwitch.ValueGreaterThan) && DetailSpecSwitches.Contains(DetailSpecSwitch.ValueLessThanOrEqualTo);
          bool gteAndLt = DetailSpecSwitches.Contains(DetailSpecSwitch.ValueGreaterThanOrEqualTo) && DetailSpecSwitches.Contains(DetailSpecSwitch.ValueLessThan);
          if (gtAndLt || gtAndLte || gteAndLt || gteAndLte)
          {
            if (this.DataTypeSpec == DataTypeSpec.Integer)
              if (this.LowNumericValue > this.HighNumericValue)
              {
                throw new Exception("An illogical condition exists, the low numeric value is greater than the high numeric value.");
              }

            if (this.DataTypeSpec == DataTypeSpec.Date)
              if (this.LowDateValue > this.HighDateValue)
              {
                throw new Exception("An illogical condition exists, the low date value is greater than the high date value.");
              }
          }

          bool gtAndE = DetailSpecSwitches.Contains(DetailSpecSwitch.ValueGreaterThan) && DetailSpecSwitches.Contains(DetailSpecSwitch.ValueEquals);
          if (gtAndE)
            throw new Exception("An illogical condition exists, you cannot have 2 conditions where a value is 'greater than' and it is also 'equal to'.");

          bool ltAndE = DetailSpecSwitches.Contains(DetailSpecSwitch.ValueLessThan) && DetailSpecSwitches.Contains(DetailSpecSwitch.ValueEquals);
          if (ltAndE)
            throw new Exception("An illogical condition exists, you cannot have 2 conditions where a value is 'less than' and it is also 'equal to'.");                  
        }
        if (UseOrLogic)
        {
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to assert the validity of the detail specifications.", ex);
      }
    }

    private void AssertValidBracketUse(string s)
    {
      int openBracketCount = s.CountOfChar('[');
      int closeBracketCount = s.CountOfChar(']');

      if (openBracketCount > 0 || closeBracketCount > 0)
      {
        if (openBracketCount > 1 || closeBracketCount > 1)
          throw new Exception("Invalid use of brackets in TextNodeSpec '" + _originalValue + "' - only a single matching set of brackets are allowed.");
        int openBracketPos = s.IndexOf('[');
        int closeBracketPos = s.IndexOf(']');
        if (closeBracketPos < openBracketPos)
          throw new Exception("Invalid use of brackets in TextNodeSpec '" + _originalValue + "' - the open bracket must come before the close bracket.");
        if (openBracketPos != 0)
          throw new Exception("Invalid use of brackets in TextNodeSpec '" + _originalValue + "' - the open bracket, if used, must be the first character in " +
                              "the TextNodeSpec except that it may be preceded by an exclaimation mark for negation.");
        if (closeBracketPos != s.Length - 1)
          throw new Exception("Invalid use of brackets in TextNodeSpec '" + _originalValue + "' - the close bracket, if used, must be the last character in " +
                               "the TextNodeSpec.");

      }
    }

    private string Get_Report()
    {
      return "Report not yet implemented.";
    }
  }
}
