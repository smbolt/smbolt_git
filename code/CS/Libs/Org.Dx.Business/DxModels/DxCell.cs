using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using Org.Dx.Business.TextProcessing;
using Org.GS;

namespace Org.Dx.Business
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements = "DxCell", XType = XType.Element)] 
  public class DxCell : Node
  {
    [XMap]
    public int RowIndex { get; set; }

    [XMap]
    public int ColumnIndex { get; set; }

    [XMap]
    public string Name { get; set; }

    public DxCellSet DxCellSet { get; set; }
    public string DefaultValue { get; set; }
    public bool IsDummyCell { get; set; }

    public string TextValue { get { return Get_TextValue(); } }
    public int Int32Value { get { return Get_Int32Value(); } }
    public decimal DecimalValue { get { return Get_DecimalValue(); } }
    public bool BooleanValue { get { return Get_BooleanValue(); } }
    public DateTime DateTimeValue { get { return Get_DateTimeValue(); } }
    public DateTime DateTimeMMYYYYValue { get { return Get_DateTimeValue(true); } }
    public object ValueOrDefault { get { return Get_ValueOrDefault(); } }
    public bool IsCellEmpty { get { return Get_IsCellEmpty(); } }
    public bool IsString { get { return Get_IsString(); } }
    public bool IsBlank { get { return Get_IsCellEmpty(); } }
    public bool IsNotBlank { get { return !Get_IsCellEmpty(); } }

    [XMap]
    public bool IsBoolean 
    { 
      get 
      {
        return Get_IsBoolean();
      }
    } 

    [XMap]
    public bool IsDateTime 
    {
      get
      {
        return Get_IsDateTime();
      }
    }

    [XMap]
    public bool IsEmpty 
    {
      get
      {
        return Get_IsEmpty();
      }
    }

    [XMap]
    public bool IsNumeric 
    {
      get
      {
        return Get_IsNumeric();
      }
    }


    [XMap]
    public bool IsText 
    {
      get
      {
        return Get_IsText();
      }
    }

    public bool IsInteger { get { return Get_IsInteger(); } }
    public bool IsDecimal { get { return Get_IsDecimal(); } }

    [XMap]
    public object RawValue { get; set; }

    public DxMapItem DxMapItem { get; set; }

    public DxCell()
    {
      this.DxObject = this;
      this.RowIndex = -1;
      this.ColumnIndex = -1;
      this.RawValue = null;
      this.IsDummyCell = false;
    }

    public DxCell(DxMapItem dxMapItem = null)
    {
      this.DxObject = this;
      this.DxMapItem = dxMapItem;
      this.RowIndex = -1;
      this.ColumnIndex = -1;
      this.RawValue = null;
      this.IsDummyCell = false;
    }

    public DxCell(string name, int rowIndex, int colIndex, object rawValue)
    {
      this.DxObject = this;
      this.Name = name;
      this.RowIndex = rowIndex;
      this.ColumnIndex = colIndex;
      this.RawValue = rawValue;
      this.IsDummyCell = false;
    }

    public void AutoInit()
    {
      if (this.Name.IsNotBlank())
        return;

      object cellObject = this.ValueOrDefault;
      if (cellObject == null)
        return;

      string cellValue = cellObject.ToString();

      if (cellValue.ToString().IsBlank())
        return;

      if (cellValue.Contains("[") && cellValue.Contains("]"))
      {
        string cellName = cellValue.GetTextBetween(Constants.OpenBracket, Constants.CloseBracket);
        if (cellName.IsNotBlank())
          this.Name = cellName;
        string cellValueAfterName = cellValue.GetTextAfter(Constants.CloseBracket);
        this.RawValue = cellValueAfterName.Trim();
      }
    }

    public string Get_TextValue()
    {
      return this.RawValue.ObjectToTrimmedString();
    }

    public bool Get_IsCellEmpty()
    {
      return !this.IsString;
    }

    public bool Get_IsString()
    {
      if (this.RawValue == null)
        return false;

      return this.RawValue.ToString().Trim().Length > 0;
    }

    public int Get_Int32Value()
    {
      return this.RawValue.ToInt32();
    }

    public bool Get_BooleanValue()
    {
      return this.RawValue.ToBoolean();
    }

    public DateTime Get_DateTimeValue(bool useMMYYYYFormat = false)
    {
      return this.RawValue.ToDateTime(useMMYYYYFormat);
    }

    public object Get_ValueOrDefault()
    {
      try
      {
        object returnValue = null;

        // this is used to limit decimal values to have no more than 11 digits to the right of the decimal
        if (this.RawValue != null)
        {
          string value = this.RawValue.ToString();
          if (value.Length > 11 && value.IsDecimal(true))
          {
            int periodPos = value.IndexOf('.');
            int decimalDigits = value.Length - periodPos - 1;
            if (decimalDigits > 11)
            {
              int digitsToTruncate = decimalDigits - 11;
              this.RawValue = value.Substring(0, value.Length - digitsToTruncate); 
            }
          }
        }

        if (this.DefaultValue == null && this.DxMapItem != null && this.DxMapItem.DefaultValue.IsNotBlank())
          this.DefaultValue = this.DxMapItem.DefaultValue; 

        if (this.RawValue != null && this.RawValue.ToString().IsNotBlank())
          returnValue = this.RawValue;

        if (returnValue == null && this.RawValue == null && this.DefaultValue.IsNotBlank())
          returnValue = this.DefaultValue;

        if (returnValue == null && this.IsCellEmpty && this.DefaultValue.IsNotBlank())
          returnValue = this.DefaultValue;

        return returnValue;
      }
      catch (Exception ex)
      {
        string message = ex.Message;
        return null;
      }
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

      string rawValueString = this.RawValue.ToString().Trim();

      if (rawValueString.IsBlank())
        return false;

      if (rawValueString.Length < 4)
        return false;

      if (rawValueString.ContainsNoDigits())
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
      }


      if (rawValueString.IsBlank())
        return false;

      if (rawValueString.IndexOf('.') > -1)
        return false;

      if (rawValueString.IsNumeric() && (rawValueString.Length == 6 || rawValueString.Length == 8))
      {
        if (rawValueString.IsValidMMYYYY() || rawValueString.IsValidMMDDYY() || rawValueString.IsValidCCYYMMDD() ||  rawValueString.IsValidMMDDCCYY())
        {
          return true;
        }
        else
          return false;
      }
            
      DateTime? dt = (DateTime?)null;

      try
      {
        dt = Convert.ToDateTime(rawValueString);
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

    public bool ExcludeBaseOnFilter(DxFilterSet fs)
    {
      try
      {
        if (fs == null || fs.Count == 0)
          return false;

        foreach (var f in fs)
        {
          var filterMethod = g.ToEnum<FilterMethod>(f.FilterMethod, FilterMethod.NotSet);

          switch (filterMethod)
          {
            case FilterMethod.RowFilterCellValues:
              var filterTypes = f.Types;

              if (this.ColumnIndex < filterTypes.Length)
              {
                string filterType = filterTypes[this.ColumnIndex].ToLower().Trim();

                switch (filterType)
                {
                  case "*":
                    break;

                  case "s":
                    if (!this.IsString)
                      return true;
                    break;

                  case "n":
                    if (!this.IsNumeric)
                      return true;
                    break;

                  case "!n":
                    if (this.IsNumeric)
                      return true;
                    break;

                  case "d":
                    if (!this.IsDecimal)
                      return true;
                    break;

                  case "!d":
                    if (this.IsDecimal)
                      return true;
                    break;

                  case "i":
                    if (!this.IsInteger)
                      return true;
                    break;

                  case "!i":
                    if (this.IsInteger)
                      return true;
                    break;

                  case "b":
                    if (!this.IsBoolean)
                      return true;
                    break;

                  case "!b":
                    if (this.IsBoolean)
                      return true;
                    break;

                  case "!dt":
                    if (this.IsDateTime)
                        return true;
                    break;

                  case "dt":
                    if (!this.IsDateTime)
                        return true;
                    break;

                  case "!":
                    if (!this.IsCellEmpty)
                      return true;
                    break;
                }
              }

              break;
          }
        }

        return false;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to exclude a DxCell object based on its value and the DxFilterSet.", ex);
      }
    }

    public bool IncludeBasedOnString(string compareValue)
    {
      string parmValue = String.Empty;
      if(compareValue.StartsWith("@CONTAINS"))
      {
        parmValue = compareValue.Split('(', ')')[1];
        compareValue = "@CONTAINS@";
      }

      switch(compareValue)
      {
        case "@ISNOTBLANK@":
          return this.IsNotBlank;

        case "@ISBLANK@":
          return this.IsBlank;

        case "@CONTAINS@":
          if(this.IsBlank)
            return false;
          if(!this.ValueOrDefault.ToString().Contains(parmValue))
            return false;
          break;
      }
      
      return true;
    }

    public bool IncludeBasedOnValue(string compareValue)
    {
      if (compareValue.StartsWith("@"))
      {
        bool b = IncludeBasedOnString(compareValue);
        return b;
      }

      switch (compareValue)
      {
        case "*":
          return true;

        case "s":
          if (!this.IsString)
            return false;
          break;

        case "!n":
          if (this.IsNumeric)
            return false;
          break;

        case "n":
          if (!this.IsNumeric)
            return false;
          break;

        case "!d":
          if (this.IsDecimal)
            return false;
          break;

        case "d":
          if (!this.IsDecimal)
            return false;
          break;

        case "!i":
          if (this.IsInteger)
            return false;
          break;

        case "i":
          if (!this.IsInteger)
            return false;
          break;

        case "!b":
          if (this.IsBoolean)
            return false;
          break;

        case "b":
          if (!this.IsBoolean)
            return false;
          break;

        case "!dt":
          if (this.IsDateTime)
            return false;
          break;

        case "dt":
          if (!this.IsDateTime)
            return false;
          break;

        case "!":
          if (!this.IsCellEmpty)
            return false;
          break;

        default:
          string cellValue = this.ValueOrDefault.ToString();
          try
          {
            var specifier = new TextNodeSpec(compareValue);
            return specifier.TokenMatch(cellValue);
          }
          catch (Exception ex)
          {
            throw new Exception("An exception occurred while attempting to compare values using TextNodeSpec with compare value '" +
                                compareValue + "' and cell value '" + cellValue + "'.", ex);
          }
      }

      return true;
    }

    public bool MatchesCriteria(TextNodeSpec tns)
    {
      switch (tns.DataTypeSpec)
      {
        case DataTypeSpec.String:
          return tns.TokenMatch(this.TextValue);

        case DataTypeSpec.Integer:
          return tns.TokenMatch(this.TextValue);

        case DataTypeSpec.Date:
          return tns.DateTokenMatch(this.TextValue);

        case DataTypeSpec.Decimal:
          return tns.DecimalTokenMatch(this.TextValue);

        // need to implement the rest of the data types...

        default:
          throw new Exception("DataType '" + tns.DataTypeSpec.ToString() + "' is not yet implemented in the DxCell.MatchesCriteria method.");
      }
    }

    public DxCell Clone(DxCellSet cloneDxCellSet)
    {
      var clone = new DxCell();
      clone.RowIndex = this.RowIndex;
      clone.ColumnIndex = this.ColumnIndex;
      clone.Name = this.Name;
      clone.DxCellSet = clone.DxCellSet;
      clone.DefaultValue = this.DefaultValue;
      clone.IsDummyCell = this.IsDummyCell;
      clone.RawValue = this.RawValue;
      clone.DxMapItem = this.DxMapItem;
      return clone;
    }
  }
}
