using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class DxSearchCriteria
  {
    public List<object> CompareValueSet {
      get;
      set;
    }
    public int StartIndex {
      get;
      set;
    }
    public int EndIndex {
      get;
      set;
    }
    public DxComparisonType ComparisonType {
      get;
      set;
    }
    public DxDataType DataType {
      get;
      set;
    }
    public DxTextCase TextCase {
      get;
      set;
    }

    public DxSearchCriteria()
    {
      this.Initialize();
    }

    public DxSearchCriteria(StringSetType stringSetType, DxComparisonType comparisonType, DxTextCase textCase)
    {
      this.Initialize();
      this.SetSearchStringSet(stringSetType);
      this.ComparisonType = comparisonType;
      this.TextCase = textCase;
      this.DataType = DxDataType.Text;
    }

    public DxSearchCriteria(DxComparisonType comparisonType, DxTextCase textCase)
    {
      this.Initialize();
      this.SetSearchStringSet(StringSetType.None);
      this.ComparisonType = comparisonType;
      this.TextCase = textCase;
      this.DataType = DxDataType.Text;
    }

    public DxSearchCriteria(StringSetType stringSetType, int startIndex, DxComparisonType comparisonType, DxTextCase textCase)
    {
      this.Initialize();
      this.SetSearchStringSet(stringSetType);
      this.StartIndex = startIndex;
      this.ComparisonType = comparisonType;
      this.TextCase = textCase;
      this.DataType = DxDataType.Text;
    }

    public DxSearchCriteria(int startIndex, DxComparisonType comparisonType, DxTextCase textCase)
    {
      this.Initialize();
      this.SetSearchStringSet(StringSetType.None);
      this.StartIndex = startIndex;
      this.ComparisonType = comparisonType;
      this.TextCase = textCase;
      this.DataType = DxDataType.Text;
    }

    public DxSearchCriteria(StringSetType stringSetType, int startIndex, int endIndex, DxComparisonType comparisonType, DxTextCase textCase)
    {
      this.Initialize();
      this.SetSearchStringSet(stringSetType);
      this.StartIndex = startIndex;
      this.EndIndex = endIndex;
      this.ComparisonType = comparisonType;
      this.TextCase = textCase;
      this.DataType = DxDataType.Text;
    }

    public DxSearchCriteria(int startIndex, int endIndex, DxComparisonType comparisonType, DxTextCase textCase)
    {
      this.Initialize();
      this.SetSearchStringSet(StringSetType.None);
      this.StartIndex = startIndex;
      this.EndIndex = endIndex;
      this.ComparisonType = comparisonType;
      this.TextCase = textCase;
      this.DataType = DxDataType.Text;
    }

    private void SetSearchStringSet(StringSetType stringSetType)
    {
      switch (stringSetType)
      {
        case StringSetType.MonthsLong:
          this.CompareValueSet.AddRange(new List<string>() {
            "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
          });
          break;

        case StringSetType.MonthsShort:
          this.CompareValueSet.AddRange(new List<string>() {
            "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
          });
          break;
      }
    }

    private void Initialize()
    {
      this.StartIndex = -1;
      this.EndIndex = -1;
      this.CompareValueSet = new List<object>();
      this.ComparisonType = DxComparisonType.Equals;
      this.TextCase = DxTextCase.CaseSensitive;
      this.DataType = DxDataType.Text;
    }
  }
}
