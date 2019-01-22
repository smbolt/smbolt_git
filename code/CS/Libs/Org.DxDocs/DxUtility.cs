using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;
using Org.GS;
using Org.Dx.Business;
using Org.Dx.Business.TextProcessing;

namespace Org.DxDocs
{
  public class EndRowSearchParms
  {
    public string FileName {
      get;
      set;
    }
    public int StartColumn {
      get;
      set;
    }
    public int EndColumn {
      get;
      set;
    }
    public int StartRow {
      get;
      set;
    }
    public string EndRowSearchString {
      get;
      set;
    }
    public bool EndRowSearchCaseSensitive {
      get;
      set;
    }
    public int EndRowSearchColumn {
      get;
      set;
    }
    public int EndRowSearchStart {
      get;
      set;
    }
    public int EndRowSearchLimit {
      get;
      set;
    }

    public EndRowSearchParms()
    {
      this.FileName = String.Empty;
      this.StartColumn = 0;
      this.EndColumn = 0;
      this.StartRow = 0;
      this.EndRowSearchString = String.Empty;
      this.EndRowSearchCaseSensitive = false;
      this.EndRowSearchColumn = 0;
      this.EndRowSearchStart = 0;
      this.EndRowSearchLimit = 9999;
    }
  }

  public class DxUtility
  {
    public static event Action<string> NotifyHost;

    public static DxWorkbook GetWorkbook(ExcelExtractRequest request)
    {
      return GetWorkbook(request.FullPath, request.FileNamePrefix, request.WorksheetsToInclude, request.MapName);
    }

    public static string WriteWorkbookToFile(ExcelExtractRequest request)
    {
      try
      {
        var dxWb = GetDxWorkbook(request.FullPath);
        var dxMapSet = GetDxMapSet(request.FullMapPath);

        using (var mapEngine = new MapEngine())
        {
          dxWb = mapEngine.MapDxWorkbook(dxWb, dxMapSet);
        }

        dxWb.EnsureIntegrity();

        XElement wbXml = null;

        using (var f = new ObjectFactory2())
        {
          wbXml = f.Serialize(dxWb);
        }

        string processedFolder = Path.GetDirectoryName(request.FullPath);
        string regressionFolder = Path.GetFullPath(Path.Combine(processedFolder, @"..\MappedRegression\"));
        string fileName = Path.GetFileNameWithoutExtension(request.FullPath);
        DirectoryInfo di = Directory.CreateDirectory(regressionFolder);

        string wbXmlString = wbXml.ToString();
        string regressionFilePath = regressionFolder + request.FileNamePrefix + fileName + ".xml";

        File.WriteAllText(regressionFilePath, wbXmlString);

        return regressionFilePath;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create a Compare MappedFile from the Excel file '" +
                            request.FullPath + "'.", ex);
      }
    }

    public static DxWorkbook GetWorkbook(string fullPath, string fileNamePrefix, List<string> worksheetsToInclude = null, string mapName = "")
    {
      try
      {
        DxMapSet dxMapSet = null;

        string mapPath = g.CI("MapPath");
        if (mapName.IsNotBlank())
        {
          dxMapSet = GetDxMapSet(mapPath, mapName);
        }

        var dxWb = GetDxWorkbook(fullPath, dxMapSet, worksheetsToInclude);

        string perfEntry = g.Perf.Start("BuildWbReport", "Building report from DxWorkbook object (text grid)");

        string report = dxWb.ToReport();

        if (dxMapSet != null)
        {
          using (var mapEngine = new MapEngine())
          {
            dxWb = mapEngine.MapDxWorkbook(dxWb, dxMapSet);
          }
        }

        dxWb.EnsureIntegrity();

        string perfReport = g.Perf.End(perfEntry);

        return dxWb;
      }

      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to create a DxWorkbook object from the Excel file '" +
                            fullPath + "'.", ex);
      }
    }

    private static DxWorkbook GetDxWorkbook(string fullPath, DxMapSet dxMapSet = null, List<string> worksheetsToInclude = null)
    {
      try
      {
        string ext = Path.GetExtension(fullPath).ToLower();

        switch (ext)
        {
          case ".pdf":
            return GetDxWorkbookFromPdf(fullPath, dxMapSet);

          case ".xml":
            return GetDxWorkbookFromXml(fullPath);

          default:
            return GetDxWorkbookFromExcel(fullPath, worksheetsToInclude);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create a DxWorkbook from a DevExpress Workbook object for " +
                            "the file located at '" + fullPath + "'.", ex);
      }
    }

    private static DxWorkbook GetDxWorkbookFromExcel(string fullPath, List<string> worksheetsToInclude = null)
    {
      try
      {
        string perfEntry = g.Perf.Start("LoadDxWorkbookFromFile", "Loading DxWorkbook from Excel (not including mapping)");

        using (var wb = new Workbook())
        {
          wb.LoadDocument(fullPath);
          DxWorkbook dxWb = new DxWorkbook();
          dxWb.FilePath = fullPath;

          foreach (var ws in wb.Worksheets)
          {
            if (ws.Columns.LastUsedIndex == 0 && ws.Rows.LastUsedIndex == -1)
              continue;

            int numberOfRows = ws.Rows.LastUsedIndex + 1;
            int numberOfColumns = ws.Columns.LastUsedIndex + 1;

            var dxWs = new DxWorksheet(dxWb, numberOfRows, numberOfColumns);
            string wsName = ws.Name;
            dxWs.IsHidden = !ws.Visible;
            dxWs.WorksheetName = ws.Name;

            for (int r = 0; r < numberOfRows; r++)
            {
              for (int c = 0; c < numberOfColumns; c++)
              {
                var cell = ws.Cells[r, c];
                dxWs.DxCells[r, c] = cell.ToDxCell();
              }
            }

            if (dxWb.ContainsKey(dxWs.WorksheetName))
              throw new Exception("The DxWorkbook already contains a worksheet named '" + dxWs.WorksheetName + "'.");

            dxWs.AutoInit();
            dxWb.AddSheet(dxWs);
          }

          string perfReport = g.Perf.End(perfEntry);

          return dxWb;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create a DxWorkbook from a DevExpress Workbook object for " +
                            "the file located at '" + fullPath + "'.", ex);
      }
    }

    private static DxWorkbook GetDxWorkbookFromPdf(string fullPath, DxMapSet dxMapSet)
    {
      try
      {
        var wb = new DxWorkbook();

        using (var textExtractor = new TextExtractor())
        {
          var pageTextList = textExtractor.ExtractPageTextFromPdf(false, fullPath, dxMapSet.ExtractionMap);

          foreach (var kvp in pageTextList)
          {
            var ws = new DxWorksheet(wb);
            ws.WorksheetName = "Sheet[" + kvp.Key.ToString() + "]";
            string pageText = kvp.Value;
            string[] lines = pageText.Split(Constants.NewLineDelimiter, StringSplitOptions.RemoveEmptyEntries);

            for (int rowIndex = 0; rowIndex < lines.Length; rowIndex++)
            {
              string line = lines[rowIndex].Trim();
              string[] tokens = line.Split(Constants.SpaceDelimiter, StringSplitOptions.RemoveEmptyEntries);
              for (int colIndex = 0; colIndex < tokens.Length; colIndex++)
              {
                var cell = new DxCell();
                cell.RowIndex = rowIndex;
                cell.ColumnIndex = colIndex;
                cell.RawValue = tokens[colIndex];
                ws.AddCell(cell);
              }
            }

            wb.AddSheet(ws);
          }

          return wb;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to build a DxWorkbook from the PDF file located at '" + fullPath + "'.", ex);
      }
    }

    private static DxWorkbook GetDxWorkbookFromXml(string fullPath)
    {
      try
      {
        var wb = new DxWorkbook();


        using (var textExtractor = new TextExtractor())
        {
          //string text = textExtractor.ExtractRawTextFromXml(false, fullPath);



          return wb;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to build a DxWorkbook from the PDF file located at '" + fullPath + "'.", ex);
      }
    }

    private static DxMapSet GetDxMapSet(string mapPath, string mapName)
    {
      try
      {
        var dxMapSet = GetDxMapSetObject(mapPath, mapName);
        var columnIndexMap = GetColumnIndexMap(mapPath, dxMapSet.ColumnIndexMapName);

        foreach (var map in dxMapSet.Values)
        {
          map.ColumnIndexMap = columnIndexMap;
          if (map.DxRegionSet != null && map.DxRegionSet.Count > 0)
          {
            foreach (var dxRegion in map.DxRegionSet.Values)
            {
              if (dxRegion.DxMapSet != null && dxRegion.DxMapSet.Count > 0)
              {
                foreach (var regionMap in dxRegion.DxMapSet.Values)
                  regionMap.ColumnIndexMap = columnIndexMap;
              }
            }
          }
        }

        return dxMapSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the DxMapSet object for map name '" + mapName + "' for mapping the workbook.", ex);
      }
    }

    private static DxMapSet GetDxMapSet(string fullMapPath)
    {
      try
      {
        var dxMapSet = GetDxMapSetObject(fullMapPath);
        var mapPath = Path.GetDirectoryName(fullMapPath);
        var columnIndexMap = GetColumnIndexMap(mapPath, dxMapSet.ColumnIndexMapName);

        foreach (var map in dxMapSet.Values)
        {
          map.ColumnIndexMap = columnIndexMap;
          if (map.DxRegionSet != null && map.DxRegionSet.Count > 0)
          {
            foreach (var dxRegion in map.DxRegionSet.Values)
            {
              if (dxRegion.DxMapSet != null && dxRegion.DxMapSet.Count > 0)
              {
                foreach (var regionMap in dxRegion.DxMapSet.Values)
                  regionMap.ColumnIndexMap = columnIndexMap;
              }
            }
          }
        }

        return dxMapSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the DxMapSet from the file '" + fullMapPath + "'", ex);
      }
    }

    private static DxMapSet GetDxMapSetObject(string mapPath, string mapName)
    {
      var ext = Path.GetExtension(mapName).ToLower();

      if (ext != ".map")
        mapName += ".dxmap";

      string fullMapPath = mapPath + @"\" + mapName;
      return GetDxMapSetObject(fullMapPath);
    }

    private static DxMapSet GetDxMapSetObject(string fullMapPath)
    {
      try
      {
        using (var f = new ObjectFactory2())
        {
          if (!File.Exists(fullMapPath))
            throw new Exception("The map does not exist at the path '" + fullMapPath + "'.");

          string mapXmlString = File.ReadAllText(fullMapPath);

          if (!mapXmlString.IsValidXml())
            throw new Exception("The contents of the map file at path '" + fullMapPath + "' is not valid XML.");

          var mapXml = XElement.Parse(mapXmlString);

          var dxMapSet = f.Deserialize(mapXml) as DxMapSet;
          dxMapSet.FullMapPath = fullMapPath;

          return dxMapSet;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the DxMapSet from map file '" + fullMapPath);
      }
    }


    private static ColumnIndexMap GetColumnIndexMap(string mapPath, string columnIndexMapName)
    {
      try
      {
        var ext = Path.GetExtension(columnIndexMapName).ToLower();

        if (ext != ".xml")
          ext = ".xml";

        string fullFilePath = mapPath + @"\" + columnIndexMapName + ext;

        if (!File.Exists(fullFilePath))
          throw new Exception("The ColumnIndexMap file does not exist at '" + fullFilePath + "'.");

        var xmlString = File.ReadAllText(fullFilePath);

        if (!xmlString.IsValidXml())
          throw new Exception("The ColumnIndexMap file at '" + fullFilePath + "' does not contain valid XML.");

        using (var f = new ObjectFactory2())
        {

          var xml = XElement.Parse(xmlString);
          return f.Deserialize(xml) as ColumnIndexMap;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the ColumnIndexMap named '" + columnIndexMapName + "' " +
                            "from the path '" + mapPath + "'.");
      }
    }

    public static List<GridCellSet> GetGridCellSetList(EndRowSearchParms parms)
    {
      var gridCellSetList = new List<GridCellSet>();

      try
      {
        DateTime _holdProductionDate = DateTime.MinValue;

        using (var wb = new Workbook())
        {
          wb.LoadDocument(parms.FileName);

          foreach (var ws in wb.Worksheets)
          {
            int lastColumnUsed = ws.Columns.LastUsedIndex;
            int lastRowUsed = ws.Rows.LastUsedIndex;

            if (lastColumnUsed == 0 && lastRowUsed == -1)
              continue;

            var cell = ws.Cells[2, 0];
            var value = cell.Value;
            var stringValue = cell.Value.ToString();
            DateTime dateValue = cell.Value.DateTimeValue;

            int endRow = 0;

            endRow = FindRowByColValue(ws, parms.EndRowSearchColumn, parms.EndRowSearchString, parms.EndRowSearchCaseSensitive, parms.EndRowSearchStart, parms.EndRowSearchLimit);
            if (endRow == -1)
              throw new Exception("Unable to locate the vertical extent of the worksheet.");

            int rows = endRow - parms.StartRow;
            int cols = parms.EndColumn - parms.StartColumn + 1;

            var gc = new GridCellSet(ws, rows, cols, parms.StartRow, parms.StartColumn);

            gridCellSetList.Add(gc);
          }
        }

        return gridCellSetList;
      }

      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to extract statement data from the Excel file '" + parms.FileName + "'.", ex);
      }
    }

    public static int FindRowByColValue(Worksheet ws, int col, string value, bool caseSensitive, int rowStart, int rowLimit)
    {
      int row = rowStart;

      if (!caseSensitive)
        value = value.ToLower().Trim();
      else
        value = value.Trim();

      while (true)
      {
        string cellValue = ws.Cells[row, col].Value.ToString().Trim();
        if (caseSensitive)
        {
          if (cellValue == value)
            return row;
        }
        else
        {
          if (cellValue.ToLower() == value)
            return row;
        }

        row++;
        if (row > rowLimit)
          return -1;
      }
    }

    public static void SplitWorkbook(string fullFilePath, int maxRows)
    {
      try
      {
        using (var wb = new Workbook())
        {
          wb.LoadDocument(fullFilePath);

          var ws = wb.Worksheets[0];

          string ext = Path.GetExtension(fullFilePath);
          string folderName = Path.GetDirectoryName(fullFilePath);
          string fileName = Path.GetFileNameWithoutExtension(fullFilePath);
          int fileNumber = 0;

          var outWb = new Workbook();

          int remainingRows = ws.Rows.LastUsedIndex;
          int rowIndex = 0;

          // This routine uses both row indices and row counts (how many rows go in a split out file).
          // Therefore the math involving the size of the range to select is not simple or intuitive.

          while (remainingRows > 0)
          {
            int rowCount = remainingRows > maxRows ? maxRows : remainingRows + 1;
            var sourceRange = ws.Range.FromLTRB(0, rowIndex, ws.Columns.LastUsedIndex, rowIndex + rowCount - 1);
            outWb.Worksheets[0].Cells[0, 0].CopyFrom(sourceRange);

            string splitFolder = folderName + @"\Split";
            if (!Directory.Exists(splitFolder))
              Directory.CreateDirectory(splitFolder);

            string splitFileName = fileName + "_Split" + fileNumber.ToString("000") + ext;
            string path = splitFolder + @"\" + splitFileName;
            outWb.SaveDocument(path);

            NotifyHost("Created " + splitFileName + " - " + rowCount.ToString() + " rows included...");

            fileNumber++;
            outWb = new Workbook();

            remainingRows -= rowCount;
            rowIndex += rowCount;
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to split the workbook '" + fullFilePath + "'.", ex);
      }
    }

    public static string GetMap(GetMapRequest request)
    {
      try
      {
        string mapPath = g.CI("MapPath");
        string mapName = request.MapName;

        var ext = Path.GetExtension(mapName).ToLower();

        if (ext != ".map")
        {
          mapName += request.ExtractTransName == "ExcelExtract" ? ".dxmap" : ".ext";
        }

        string fullMapPath = mapPath + @"\" + mapName;

        if (!File.Exists(fullMapPath))
          return "No map file exists at '" + fullMapPath + "'.";

        string mapText = File.ReadAllText(fullMapPath);

        string returnText = String.Empty;

        if (!mapText.IsValidXml())
          returnText = "*** THE DATA IN THE MAP FILE IS NOT VALID XML ***" + g.crlf2;

        returnText += "MAP FILE READ FROM: " + fullMapPath + g.crlf2 + mapText;

        return returnText;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to get the map named '" + request.MapName + "' for " +
                            "extract trans type '" + request.ExtractTransName + "'.", ex);
      }
    }
  }
}
