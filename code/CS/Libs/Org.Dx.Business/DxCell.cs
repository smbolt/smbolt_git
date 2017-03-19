using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.Dx.Business
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements = "DxCell", XType = XType.Element)] 
  public class DxCell 
  {
    [XMap]
    public int RowIndex { get; set; }

    [XMap]
    public int ColumnIndex { get; set; }

    public string TextValue { get { return Get_TextValue(); } }
    public int Int32Value { get { return Get_Int32Value(); } }
    public decimal DecimalValue { get { return Get_DecimalValue(); } }
    public bool BooleanValue { get { return Get_BooleanValue(); } }
    public DateTime DateTimeValue { get { return Get_DateTimeValue(); } }

    private bool _isBoolean;
    private bool _isDateTime;
    private bool _isEmpty;
    private bool _isNumeric;
    private bool _isText;

    [XMap]
    public bool IsBoolean 
    { 
      get 
      {
        _isBoolean = Get_IsBoolean();
        return _isBoolean;
      }
      set { _isBoolean = value; }
    } 

    [XMap]
    public bool IsDateTime 
    {
      get
      {
        _isDateTime = Get_IsDateTime();
        return _isDateTime;
      }
      set { _isDateTime = value; }
    }

    [XMap]
    public bool IsEmpty 
    {
      get
      {
        _isEmpty = Get_IsEmpty();
        return _isEmpty;
      }
      set { _isEmpty = value; }
    }

    [XMap]
    public bool IsNumeric 
    {
      get
      {
        _isNumeric = Get_IsNumeric();
        return _isNumeric;
      }
      set { _isNumeric = value; }
    }


    [XMap]
    public bool IsText 
    {
      get
      {
        _isText = Get_IsText();
        return _isText;
      }
      set { _isText = value; }
    }

    public bool IsInteger { get { return Get_IsInteger(); } }
    public bool IsDecimal { get { return Get_IsDecimal(); } }

    [XMap]
    public object RawValue { get; set; }

    public DxCell()
    {
      this.RowIndex = -1;
      this.ColumnIndex = -1;
      this.RawValue = null;
    }

    public string Get_TextValue()
    {
      return this.RawValue.ObjectToTrimmedString();
    }

    public int Get_Int32Value()
    {
      return this.RawValue.ToInt32();
    }

    public bool Get_BooleanValue()
    {
      return this.RawValue.ToBoolean();
    }

    public DateTime Get_DateTimeValue()
    {
      return this.RawValue.ToDateTime();
    }

    private decimal Get_DecimalValue()
    {
      return this.RawValue.ToDecimal();
    }

    private bool Get_IsInteger()
    {
      if (!this.IsNumeric)
        return false;

      decimal value = this.DecimalValue - Math.Round(this.DecimalValue);
      if (value == 0)
        return true;

      return false;
    }

    private bool Get_IsDecimal()
    {
      return this.IsNumeric;
    }

    private bool Get_IsText()
    {
      if (this.RawValue == null)
        return false;

      return true;
    }

    private bool Get_IsBoolean()
    {
      if (this.RawValue == null)
        return false;

      string value = this.RawValue.ToString().Trim();

      if (value.IsBlank())
        return false;

      value = value.ToLower();

      return value.In("true, false");
    }

    private bool Get_IsDateTime()
    {
      if (this.RawValue == null)
        return false;

      string type = this.RawValue.GetType().Name;
      switch (type)
      {
        case "DateTime":
          return true;

        case "String":
          break;

        case "Double":
          return false;

        default:
          string data = this.RawValue.ToString();
          break;
      }

      string rawValueString = this.RawValue.ToString();
      if (rawValueString.IsBlank())
        return false;

      if (rawValueString.ContainsAlphaChar())
        return false;

      if (rawValueString.IndexOf('.') > -1)
        return false;
      
      DateTime? dt = (DateTime?)null;

      try
      {
        dt = Convert.ToDateTime(this.RawValue);
      }
      catch
      {
        return false;
      }

      return dt.HasValue;
    }

    private bool Get_IsEmpty()
    {
      if (this.RawValue == null)
        return true;

      return false;
    }

    private bool Get_IsNumeric()
    {
      if (this.RawValue == null)
        return false;

      return this.RawValue.ToString().IsValidNumeric();
    }

    public bool Match(DxSearchCriteria searchCriteria)
    {
      switch (searchCriteria.DataType)
      {
        case DxDataType.Empty:
          if (this.IsEmpty)
            return true;
          break;

        case DxDataType.Text:
          if (!this.IsText)
            return false;
          break;

        case DxDataType.DateTime:
          if (!this.IsDateTime)
            return false;
          break;

        case DxDataType.Boolean:
          if (!this.IsBoolean)
            return false;
          break;

        case DxDataType.Numeric:
          if (!this.IsNumeric)
            return false;
          break;

        case DxDataType.Integer:
          if (!this.IsInteger)
            return false;
          break;

        case DxDataType.Decimal:
          if (!this.IsDecimal)
            return false;
          break;
      }

      switch (searchCriteria.DataType)
      {
        case DxDataType.Text:
          return this.MatchString(searchCriteria);
      }

      return false;
    }

    private bool MatchString(DxSearchCriteria searchCriteria)
    {
      foreach (var compareValue in searchCriteria.CompareValueSet)
      {
        string matchValue = compareValue.ToString().Trim();
        if (searchCriteria.TextCase == DxTextCase.NotCaseSensitive)
          matchValue = matchValue.ToLower();

        string cellTextValue = this.TextValue.Trim();
        string cellCompareValue = cellTextValue;
        if (searchCriteria.TextCase == DxTextCase.NotCaseSensitive)
          cellCompareValue = cellTextValue.ToLower();

        switch (searchCriteria.ComparisonType)
        {

          case DxComparisonType.StartsWith:
            if (cellCompareValue.StartsWith(matchValue))
              return true;
            break;

          case DxComparisonType.EndsWith:
            if (cellCompareValue.EndsWith(matchValue))
              return true;
            break;

          case DxComparisonType.Contains:
            if (cellCompareValue.Contains(matchValue))
              return true;
            break;

          default:
            if (cellCompareValue == matchValue)
              return true;
            break;
        }
      }

      return false;
    }

  }
}
