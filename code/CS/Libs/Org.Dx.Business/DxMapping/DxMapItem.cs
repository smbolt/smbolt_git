using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Org.Dx.Business.TextProcessing;
using Org.GS;
 
namespace Org.Dx.Business
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(XType = XType.Element)]
  public class DxMapItem
  {
    [XMap(IsKey = true, IsRequired = true)]
    public string Name { get; set; }

    [XMap]
    public string Src { get; set; }

    [XMap]
    public string Dest { get; set; }

    [XMap(DefaultValue = "String")]
    public DataType DataType { get; set; }

    public DxDataType DxDataType { get; set; }

    [XMap(DefaultValue = "False")]
    public bool DebugBreak { get; set; }

    [XMap]
    public string DefaultValue { get; set; }

    [XMap]
    public string PluggedValue { get; set; }
 
    [XMap]
    public string Cond { get; set; }
 
    [XMap]
    public string MapCommand { get; set; }

    private string _nodeQuery;

    private DxCell _srcCell;
    public DxCell SrcCell { get { return Get_SrcCell(); } }
    public string SrcCellValue { get { return Get_SrcCellValue(); } }

    public List<string> DetailSwitches { get; set; }
    public string DetailSwitchString { get { return Get_DetailSwitchString(); } }

    public bool CreatesVariable { get { return Get_CreatesVariable(); } }

    public string TranslateSpec { get; private set; }
    public bool TranslateValue { get { return this.TranslateSpec.IsNotBlank(); } }

    public string FormatSpec { get; private set; }
    public bool FormatResult { get { return this.FormatSpec.IsNotBlank(); } }

    private int? _srcSheet;
    public int SrcSheet { get { return Get_SrcSheet(); } }

    private int? _srcRow;
    public int SrcRow { get { return Get_SrcRow(); } }

    private int? _srcCol;
    public int SrcCol { get { return Get_SrcCol(); } }

    private int? _destSheet;
    public int DestSheet { get { return Get_DestSheet(); } }

    private int? _destRow;
    public int DestRow { get { return Get_DestRow(); } }

    private int? _destCol;
    public int DestCol { get { return Get_DestCol(); } }

    public DxMap DxMap { get; set; }

    public string Report { get { return Get_Report(); } }
 
    public Object DxObject { get; set; }
    public int SheetIndex { get; set; }
    public int SrcRowIndex { get; set; }
    public int DestRowIndex { get; set; }
    public int ColIndex { get; set; }
     
    public DxMapItem()
    {
      _srcSheet = null;
      _srcRow = null;
      _srcCol = null;
      _destSheet = null;
      _destRow = null;
      _destCol = null;
      _nodeQuery = null;
      this.Src = String.Empty;
      this.Dest = String.Empty;
      this.Cond = String.Empty;
      this.DetailSwitches = new List<string>();
    }
 
    public void AutoInit()
    {
      if (this.Src == null)
        this.Src = String.Empty;

      if (this.PluggedValue == null)
        this.PluggedValue = String.Empty;

      if (this.DefaultValue == null)
        this.DefaultValue = String.Empty;

      // peel off specific detail specifications
      // handle double slashes includes as literal values in the switches
      // double slashes is how to indicate a slash that is not a delimiter.

      string doubleSlashReplacement = "\xA4\xA4";

      this.Src = this.Src.Replace("//", doubleSlashReplacement);

      this.Src = ProcessNodeQuery(this.Src);

      int slashPos = this.Src.IndexOf("/");
      if (slashPos != -1)
      {
        string switches = this.Src.Substring(slashPos);
        string[] switchArray = switches.Split(Constants.FSlashDelimiter, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < switchArray.Length; i++)
        {
          switchArray[i] = switchArray[i].Replace(doubleSlashReplacement, "//");
          this.DetailSwitches.Add("/" + switchArray[i]);
        }

        this.Src = this.Src.Substring(0, slashPos).Replace(doubleSlashReplacement, "//");
      }
    }

    public void Initialize()
    {
      _srcCell = null;
    }

    private bool Get_CreatesVariable()
    {
      if (this.Dest.IsBlank())
        return false;

      if (this.Dest.StartsWith("@") || this.Dest.StartsWith("$"))
        return true;

      return false;
    }
 
    private int Get_SrcSheet()
    {
      if (!_srcSheet.HasValue)
        SetSrcIndices();

      return _srcSheet.Value;
    }

    private int Get_SrcRow()
    {
      if (!_srcRow.HasValue)
        SetSrcIndices();

      return _srcRow.Value;
    }

    private int Get_SrcCol()
    {
      if (!_srcCol.HasValue)
        SetSrcIndices();

      return _srcCol.Value;
    }

    private int Get_DestSheet()
    {
      if (!_destSheet.HasValue)
        SetDestIndices();

      return _destSheet.Value;
    }

    private int Get_DestRow()
    {
      if (!_destRow.HasValue)
        SetDestIndices();

      return _destRow.Value;
    }
 
    private int Get_DestCol()
    {
      if (!_destCol.HasValue)
        SetDestIndices();

      return _destCol.Value;
    }

    private void SetSrcIndices()
    {
      try
      {
        if (_srcSheet.HasValue && _srcRow.HasValue && _srcCol.HasValue)
          return;

        if (this.Src.IsBlank())
          throw new Exception("The Src property is blank or null for DxMapItem '" + this.Report + "'.");

        // what about variables?? $abc

        string[] tokens = this.Src.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length != 3)
          throw new Exception("The DxMapItem.Src property (when it doesn't reference a variable) should always contain three comma-separated tokens - " +
                              "found '" + this.Src + "' on DxMapItem '" + this.Report + "'.");

        if (tokens[0] != "*")
          throw new Exception("The first token in the DxMapItem.Src property (indicating the sheet index) should always be an asterisk - found '" + 
                              this.Src + "' on DxMapItem '" + this.Report + "'.");

        _srcSheet = -1;

        if (this.DxMap.DxMapType == DxMapType.SheetToSheet)
        {
          if (tokens[1].IsNotInteger())
            throw new Exception("On SheetToSheet maps the second token of the DxMapItem.Src property (indicating the row index) should always be a numeric value" +
                                " - found '" + this.Src + "' on DxMapItem '" + this.Report + "'.");

          _srcRow = tokens[1].ToInt32();
        }
        else
        {
          if (tokens[1] != "*")
            throw new Exception("On RowToRow and RowToSheet maps, the second token of the DxMapItem.Src property must be an asterisk - found '" + this.Src + "' on " +
                                "DxMapItem '" + this.Report + "'.");
          _srcRow = -1;
        }

        if (tokens[2].IsNotInteger())
          throw new Exception("The third token (column index) of the DxMapItem.Src property (when it doesn't reference a variable) must be an integer - found '" +
                              this.Src + "' on DxMapItem '" + this.Report + "'.");

        _srcCol = tokens[2].ToInt32();

        if (!_srcSheet.HasValue || !_srcRow.HasValue || !_srcCol.HasValue)
          throw new Exception("The source sheet or source row or source column indices are not set for DxMapItem '" + this.Report + "' - " +
                              "the source sheet value is " + (_srcSheet.HasValue ? _srcSheet.Value.ToString() : "not set") + ", " +
                              "the source row value is " + (_srcRow.HasValue ? _srcRow.Value.ToString() : "not set") + ", " +
                              "the source column value is " + (_srcCol.HasValue ? _srcCol.Value.ToString() : "not set") + ".");


        if (this.DxMap.DxMapType == DxMapType.SheetToSheet)
        {
          if (_srcSheet.Value != -1 || _srcRow.Value == -1 || _srcCol.Value == -1)
            throw new Exception("On SheetToSheet maps, the source sheet index must be -1 (current sheet), the source row index must be a valid row index " +
                                "and the source column index must be a valid column index - found source sheet index " + _srcSheet.Value.ToString() + 
                                "and source row index " + _srcRow.Value.ToString() + " and source column index " + _srcCol.Value.ToString() + " for DxMapItem " +
                                this.Report + ".");
        }
        else
        {
          if (_srcSheet.Value != -1 || _srcRow.Value != -1 || _srcCol.Value == -1)
            throw new Exception("On RowToRow and RowToSheet maps, the source sheet index must be -1 (current sheet), the source row index must be a -1 (current row) " +
                                "and the source column index must be a valid column index - found source sheet index " + _srcSheet.Value.ToString() +
                                "and source row index " + _srcRow.Value.ToString() + " and source column index " + _srcCol.Value.ToString() + " for DxMapItem " +
                                this.Report + ".");
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to set the Src indices for the DxMapItem '" + this.Report + "'.", ex);
      }
    }

    private void SetDestIndices()
    {
      try
      {
        // all maps should have a reference to the ColumnIndexMap
        if (this.DxMap.ColumnIndexMap == null)
          throw new Exception("No ColumnIndexMap is associated with the DxMap named '" + this.DxMap.Name + "'.");


        // determine if row and column index are determined by the name property
        bool rowIndexDetermined = false;

        if (this.DxMap.DxMapType == DxMapType.RowToRow)
        {
          _destRow = -1;
          rowIndexDetermined = true;
        }

        bool colIndexDetermined = false;

        string mapItemName = this.Name;
        if (mapItemName.Contains("["))
        {
          int openPos = mapItemName.IndexOf("[");
          int closePos = mapItemName.IndexOf("]", openPos);
          string rowIndex = mapItemName.Substring(openPos + 1, (closePos - openPos) - 1);
          if (rowIndex.IsNotInteger())
            throw new Exception("The value specified in brackets at the end of the DxMapItem.Name property must be a valid integer indicating the row index - found '" + this.Name + "'.");
          _destRow = rowIndex.ToInt32();
          rowIndexDetermined = true;
          mapItemName = mapItemName.Substring(0, openPos);
        }

        if (this.DxMap.ColumnIndexMap.EntryExists(mapItemName))
        {
          colIndexDetermined = true;
          _destCol = this.DxMap.ColumnIndexMap.IndexOf(mapItemName);
        }

        if (this.Dest.IsBlank())
        {
          if (!rowIndexDetermined || !colIndexDetermined)
            throw new Exception("When the MapItem.Dest property is left blank, the column index must be determined by the ColumnIndexMap using the DxMap.Name property " +
                                "and the row index must be determined by being specified in a bracketed value at the end of the DxMap.Name property. The column index " +
                                (colIndexDetermined ? "was" : "was not") + " found and the row index " + (rowIndexDetermined ? "was" : "was not") + " found.");

          _destSheet = -1;
          return;
        }

        string[] tokens = this.Dest.Split(Constants.CommaDelimiter);

        if (tokens.Length != 3)
          throw new Exception("The DxMapItem.Dest property (when not a variable specification) must contain three comma-separated " +
                              "tokens indicating the sheet, row and column indices. The DxMapItem is '" + this.Report + "' and the " +
                              "Dest property is '" + this.Dest + "'.");

        if (tokens[0].IsNotInteger() && tokens[0] != "*")
          throw new Exception("The first token (sheet index) of the Dest property must be an integer or asterisk - found '" + this.Dest + "'.");

        _destSheet = tokens[0].IsInteger() ? tokens[0].ToInt32() : -1;

        if (tokens[1].IsNotInteger() && tokens[1] != "*")
          throw new Exception("The second token (row index) of the Dest property must be an integer or asterisk - found '" + this.Dest + "'.");

        if (rowIndexDetermined)
          throw new Exception("The row index has already been determined using the DxMapItem.Name property '" + this.Name + "'. " +
                              "Therefore the specification of the row index in the Dest property '" + this.Dest + "' is redundant.");

        _destRow = tokens[1].IsInteger() ? tokens[1].ToInt32() : -1;


        if (tokens[2] == "@" && !colIndexDetermined)
          throw new Exception("The specification of '@' in the column indicated position of the Dest property indicates that the column index " +
                              "is to be determined by the ColumnIndexMap, but the value was not determined.  The DxMapItem is '" + this.Report +
                              "' and the Dest property is '" + this.Dest + "'.");

        if (tokens[2].IsNotInteger())
          throw new Exception("An invalid value '" + tokens[2] + "' is specified in the column index position of the Dest property of the DxMapItem '" +
                              this.Report + "' - the value must be numeric or '@'.");

        if (colIndexDetermined)
          throw new Exception("The column index has already been determined using the DxMapItem.Name property '" + this.Name + "' " +
                              "and the ColumnIndexMap, therefore the specification of the column index in the Dest property '" + 
                              this.Dest + "' is redundant.");

        _destCol = tokens[2].DbToInt32();

        if (!_destSheet.HasValue || !_destRow.HasValue || _destCol.HasValue)
          throw new Exception("The indices of the destination sheeet, row and column are not all set. The sheet index is " +
                              (_destSheet.HasValue ? _destSheet.Value.ToString() : " not set") + ", the row index is " +
                              (_destRow.HasValue ? _destRow.Value.ToString() : "not set") + ", and the column index is " +
                              (_destCol.HasValue ? _destCol.Value.ToString() : "not set") + ".");
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to determine the DxMapItem.Src indices for DxMapItem '" + this.Report + "' " +
                            "with Src property of '" + this.Src + "'.", ex);
      }
    }

    private DxCell Get_SrcCell()
    {
      try
      {
        // If the source cell has already been determined, simply return it.
        if (_srcCell != null)
          return _srcCell;

        // If the map item is configured to use a "plugged value" build a cell with the plugged value, ignore anything in Src.
        if (this.PluggedValue.IsNotBlank())
        {
          var pluggedValueCell = new DxCell(this);
          pluggedValueCell.IsDummyCell = true;
          pluggedValueCell.RawValue = this.PluggedValue;
          _srcCell = pluggedValueCell;
          _srcCell.DxMapItem = this;
          return _srcCell;
        }

        if (_nodeQuery != null)
        {
          //return ((Node)this.DxObject).GetNodeQueryResult(_nodeQuery);
        }

        // If the Src indicates a variable, then build a variable-based cell and return it.
        if (this.Src.StartsWith("$"))
        {
          var variableValueCell = new DxCell(this);
          variableValueCell.RawValue = this.Src.Trim();
          _srcCell = variableValueCell;
          _srcCell.DxMapItem = this;
          return _srcCell;
        }
    
        // The Source indexes (sheet, row and column) are set in the SetSrcIndices method.
        // Any invalid indices will throw an exception in that method. Therefore the indices
        // can be counted on to be valid after the method is called.

        this.SetSrcIndices();

        if (this.DxObject == null)
          throw new Exception("The DxObject property of the DxMapItem is null on DxMapItem '" + this.Report + "'.");

        var dxObjectType = this.DxObject.GetType();

        switch (dxObjectType.Name)
        {
          case "DxWorksheet":
          case "DxRowSet":
            var srcRows = (DxRowSet)this.DxObject;
            var srcRow = srcRows.Rows.Values.ElementAtOrDefault(this.SrcRow);

            if (srcRow == null)
            {
              _srcCell = null;
              return _srcCell;
            }
            
            var srcCell = srcRow.Cells.Values.ElementAtOrDefault(this.SrcCol);

            if (srcCell == null)
            {
              _srcCell = null;
              return _srcCell;
            }

            _srcCell = srcCell;
            _srcCell.DxMapItem = this;
            return _srcCell;  

          case "DxRow":
          case "DxCellSet":
            var sourceRow = (DxRow)this.DxObject;
            var sourceCell = sourceRow.Cells.Values.ElementAtOrDefault(this.SrcCol);
          
            if (sourceCell == null)
            {
              _srcCell = null;
              return _srcCell;
            }

            _srcCell = sourceCell;
            _srcCell.DxMapItem = this;
            return _srcCell;
        }

        return null;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the Src cell.", ex);
      }
    }

    private string Get_SrcCellValue()
    {
      try
      {
        string report = this.Report;

        string cellValue = String.Empty;

        if (this.Src.StartsWith("$"))
        {
          cellValue = MapEngine.GetVariableValue(this.Src).Trim();
        }
        else
        {
          cellValue = this.SrcCell?.ValueOrDefault?.ToString().Trim() ?? String.Empty;
        }

        if (this.DetailSwitches.Count == 0)
        {
          if (cellValue.IsBlank())
            throw new Exception("The source value is blank but the data is required for DxMapItem '" + this.Report + "'.");
        }
        else 
        {
          var tns = new TextNodeSpec(this.DetailSwitchString);
          tns.GlobalVariables = MapEngine.GlobalVariables;
          tns.LocalVariables = MapEngine.LocalVariables;
          cellValue = tns.ProcessValue(cellValue, true);
        }

        return cellValue;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the Src cell value - DxMapItem is '" + this.Report + "'.", ex);
      }
    }

    private string ProcessNodeQuery(string src)
    {
      if (src.IsBlank())
        return String.Empty;

      if (!src.StartsWith("nq:("))
        return src;

      int endOfNodeQuery = src.IndexOf(')', 4);
      if (endOfNodeQuery == -1)
        throw new Exception("NodeQuery is invalid - cannot locate the ending parenthesis ')' in the value '" + src + "'.");

      while (endOfNodeQuery < src.Length - 1)
      {
        if (src[endOfNodeQuery + 1] != '(')
          break;

        endOfNodeQuery = src.IndexOf(')', endOfNodeQuery + 1);
      }

      _nodeQuery = src.Substring(3, endOfNodeQuery - 2);

      string remainder = src.Substring(endOfNodeQuery + 1).Trim();

      return remainder;
    }
        
    private string Get_Report()
    {
      return "DxMapItem: Name " + this.Name + " Src: " + this.Src + " Dst: " + this.Dest + " Cond: " + this.Cond;
    }

    private string Get_DetailSwitchString()
    {
      if (this.DetailSwitches == null || this.DetailSwitches.Count == 0)
        return String.Empty;

      var sb = new StringBuilder();
      foreach (var detailSwitch in this.DetailSwitches)
      {
        sb.Append(detailSwitch);
      }

      return sb.ToString();
    }

  }
}

