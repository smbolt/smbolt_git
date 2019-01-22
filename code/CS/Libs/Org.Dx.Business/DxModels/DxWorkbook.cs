using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Org.Dx.Business.TextProcessing;
using Org.GS;

namespace Org.Dx.Business
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements = "DxWorksheet", XType = XType.Element)] 
  public class DxWorkbook : Dictionary<string, DxWorksheet>
  {
    [XMap]
    public string FilePath { get; set; }

    [XMap]
    public string MapPath { get; set; }

    public DxMap DxMap { get; set; }

    [XMap (DefaultValue = "False")]
    public bool IsMapped { get; set; }

    public DxRow FirstRowInWorkbook { get { return Get_FirstRowInWorkbook(); } }
    
    public string Report { get { return ToReport(); } }

    public DxWorkbook()
    {
      this.FilePath = String.Empty;
      this.MapPath = String.Empty;
      this.IsMapped = false;
    }

    public DxWorkbook(string firstSheetName)
    {
      this.Add(firstSheetName, new DxWorksheet(this, firstSheetName));
    }

    public DxWorkbook TrimSheets(DxMapSet mapSet)
    {
      try
      {
        if (mapSet.DxRegionSet == null || mapSet.DxRegionSet.Count == 0)
          throw new Exception("The DxRegionSet is null or empty. To Trim a sheet you need a DxRegionSet.");

        foreach (var sheet in this.Values)
        {
          foreach (var region in mapSet.DxRegionSet.Values)
          {
            if (ApplyRegionToThisSheet(sheet, region))
            {
              sheet.Trim(region);
            }
          }
        }

        return this;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while performing the TrimSheets routine.", ex);
      }
    }

    public DxWorkbook FilterSheets(DxMapSet mapSet)
    {
      try
      {
        var sheetFilter = mapSet.DxFilterSet.SheetFilter;

        if (sheetFilter == null)
          return this;

        var filteredWb = new DxWorkbook();
        filteredWb.FilePath = this.FilePath;

        foreach (var ws in this.Values)
        {
          if (sheetFilter.MatchesWorksheet(ws))
            filteredWb.AddSheet(ws);
        }

        return filteredWb;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to run the FilterSheets routine for the workbook.", ex);
      }
    }

    private bool ApplyRegionToThisSheet(DxWorksheet sheet, DxRegion region)
    {
      try
      {
        if (region.SheetSelect.IsBlank())
          return true;

        string sheetSelectCriteria = region.SheetSelect;

        if (!sheetSelectCriteria.Contains(":"))
          throw new Exception("The sheet select criteria must be in the format eg. 'SheetName:[Sheet1]', or Row1:[str/eq:ABC].");

        string[] tokens = sheetSelectCriteria.Split(Constants.ColonDelimiter, StringSplitOptions.RemoveEmptyEntries);
        string removeBrackets = tokens[1].ToString();
        removeBrackets = removeBrackets.Replace("[", "").Replace("]", "");

        if (tokens[0].ToString() == "SheetName")
        {
          string[] sheetNames = removeBrackets.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);
          var sheetNameList = sheetNames.ToList();

          if (sheetNameList.Contains(sheet.WorksheetName.ToString()))
            return true;
        }
        else
        {
          throw new Exception("The method " + tokens[0].ToString() + " has not been implemented yet.");
        }

        return false;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to determine if the DxRegion named '" + region.Name + 
                            "' should be " + "applied to the sheet named '" + sheet.WorksheetName + "'.", ex);
      }
    }

    public void AddSheet(DxRowSet ws)
    {
      int seq = 0;
      string sheetName = ws.WorksheetName;

      while (this.ContainsKey(ws.WorksheetName))
      {
        ws.WorksheetName = sheetName + "-" + (++seq).ToString("000");
        ((DxWorksheet)ws).WorksheetName = ws.WorksheetName;
      }

      if (ws.DxCells == null)
      {
        ws.PopulateDxCellsArray();
      }

      ws.EnsureParentage();

      ((Dictionary<string, DxWorksheet>)this).Add(ws.WorksheetName, (DxWorksheet) ws); 
    }

    public DxCell GetCell(DxMapItem item)
    {
      var newCell = new DxCell();
      newCell.RowIndex = item.DestRow;
      newCell.ColumnIndex = item.DestCol;

      // if the sheet, row or column don't exist in the source, return a "null" cell with destination row and column indices.
      if (this.Count - 1 < item.SrcSheet || this.ElementAt(item.SrcSheet).Value.Rows.Count - 1 < item.SrcRow ||
          this.ElementAt(item.SrcSheet).Value.Rows[item.SrcRow].Cells.Count - 1 < item.SrcCol)
      {
        return newCell;
      }

      newCell.RawValue = this.ElementAt(item.SrcSheet).Value.Rows[item.SrcRow].Cells[item.SrcCol].RawValue;

      return newCell;
    }

    public void AddCell(int sheetIndex, DxCell cell)
    {
      while(this.Count - 1 < sheetIndex)
      {
        string sheetName = this.Count.ToString(); 
        this.Add(sheetName, new DxWorksheet(this, sheetName));
      }

      var ws = this.ElementAt(sheetIndex).Value;
      ws.AddCell(cell); 
    }

    public void AutoInit()
    {
      EnsureIntegrity();
    }

    public void EnsureIntegrity()
    {
      try
      {
        foreach (var kvpWs in this)
        {
          string sheetName = kvpWs.Key;
          var ws = kvpWs.Value;

          if (ws.WorksheetName.IsBlank())
            ws.WorksheetName = kvpWs.Key;

          ws.EnsureIntegrity();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the EnsureIntegrity method of the DxWorkbook.", ex); 
      }
    }

    private DxRow Get_FirstRowInWorkbook()
    {
      foreach (var ws in this.Values)
      {
        if (ws.Rows.Count > 0)
        {
          return ws.Rows.Values.First();
        }
      }

      return null; 
    }
    
    public string ToReport()
    {
      try
      {
        StringBuilder sb = new StringBuilder();
        sb.Append("DxWorkbook Report" + g.crlf +
                  "  File Path : " + this.FilePath +
                  "  Sheet Count : " + this.Count() + g.crlf2);

        foreach (var ws in this.Values)
          sb.Append(ws.Report); 

        return sb.ToString();
      }
      catch (Exception ex)
      {
        return "An exception occurred while attempting to create the Report from the DxWorkbook object." + g.crlf2 + ex.ToReport();
      }
    }

    public DxWorkbook Clone()
    {
      try
      {
        var cloneWb = new DxWorkbook();
        cloneWb.FilePath = this.FilePath;
        cloneWb.MapPath = this.MapPath;
        cloneWb.DxMap = this.DxMap;
        cloneWb.IsMapped = this.IsMapped;

        foreach (var kvpWs in this)
        {
          var cloneWs = kvpWs.Value.Clone(cloneWb);
          cloneWs.WorksheetName = kvpWs.Value.WorksheetName;

          cloneWb.Add(kvpWs.Key, cloneWs);
        }

        return cloneWb;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to clone a DxWorkbook.", ex);
      }
    }

    public string CompareToWorkbook(DxWorkbook compareWb)
    {
      try
      {
        var sb = new StringBuilder();

        using (var f = new ObjectFactory2())
        {
          string thisWbString = f.Serialize(this).ToString();
          string compareWbString = f.Serialize(compareWb).ToString();

          string[] thisWbLines = thisWbString.Split(Constants.NewLineDelimiter);
          string[] compareWbLines = compareWbString.Split(Constants.NewLineDelimiter);

          if (thisWbLines.Length != compareWbLines.Length)
          {
            return "The base DxWorkbook when serialized to XML has " + thisWbLines.Length.ToString() + " lines and the compare " + 
                   "workbook when serialized to XML has " + compareWbLines.Length.ToString() + " lines.";
          }

          int diffLinesLimit = 10;
          int diffLinesCount = 0;

          for (int i = 0; i < thisWbLines.Length; i++)
          {
            if (i < thisWbLines.Length && i < compareWbLines.Length)
            {
              if (!thisWbLines[i].Equals(compareWbLines[i]))
              {
                diffLinesCount++;
                sb.Append("The following lines are different:" + g.crlf +
                          "Base DxWorkbook: " + thisWbLines[i] + g.crlf +
                          "Comp DxWorkbook: " + compareWbLines[i] + g.crlf2);
                if (diffLinesCount > diffLinesLimit)
                  break;
              }
            }
          }
        }

        string compareReport = sb.ToString();

        if (compareReport.IsBlank())
          compareReport = "IDENTICAL";

        return compareReport;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to compare workbooks for equivalence.", ex);
      }
    }
  }
}
