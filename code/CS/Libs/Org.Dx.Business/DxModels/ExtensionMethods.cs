using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.Dx.Business
{
  public static class ExtensionMethods
  {
    public static SortedList<int, Node> ToNodeSet(this SortedList<int, DxCell> c)
    {
      if (c == null)
        return null;

      var nodeSet = new SortedList<int, Node>();
      foreach (var kvp in c)
        nodeSet.Add(kvp.Key, kvp.Value);

      return nodeSet;
    }

    public static string ToCode(this QueryExecutionStatus s)
    {
      switch (s)
      {
        case QueryExecutionStatus.Aligned:
          return "ALGN";
        case QueryExecutionStatus.InInterval:
          return "INTV";
        case QueryExecutionStatus.InProgress:
          return "INPR";
        case QueryExecutionStatus.Matched:
          return "MTCH";
        case QueryExecutionStatus.MatchedTarget:
          return "MTGT";
        case QueryExecutionStatus.MatchedTargetPending:
          return "MTPN";
        case QueryExecutionStatus.MatchFailed:
          return "FAIL";
        case QueryExecutionStatus.Initial:
          return "INIT";
        case QueryExecutionStatus.Target:
          return "TGRT";
      }

      return "UNKN";
    }

    public static string[] SplitRelation(this string s)
    {
      if (s.IsBlank())
        return new string[] { };

      s = s.Trim();

      int relOpPos = s.IndexOfAny(Constants.RelOps);

      if (relOpPos == -1 || relOpPos == 0 || relOpPos == s.Length - 1)
        return new string[] { s };

      string relOp = s.Substring(relOpPos, 1);
      string[] tokens = s.Split(Constants.RelOps, StringSplitOptions.RemoveEmptyEntries);

      if (tokens.Length != 2)
        return new string[] { s };

      string[] array = new string[3];

      array[0] = tokens[0];
      array[1] = relOp;
      array[2] = tokens[1];

      return array;
    }

    public static RelOp ToRelOp(this string s)
    {
      if (s.IsBlank())
        return RelOp.NotSet;

      s = s.Trim();

      switch (s)
      {
        case "~":
          return RelOp.Contains;
        case "=":
          return RelOp.Equals;
        case ">":
          return RelOp.GreaterThan;
        case "<":
          return RelOp.LessThan;
      }

      return RelOp.NotSet;
    }

    public static List<DxRowSet> ToRowSets(this DxWorkbook wb, DxFilterSet fs = null)
    {
      var rowSets = new List<DxRowSet>();

      if(wb == null)
        return rowSets;

      foreach(var ws in wb.Values)
      {
        bool includeWorksheet = true;

        if (fs != null)
          includeWorksheet = fs.IncludeWorksheet(ws.WorksheetName);

        DxRowSet filteredRowSet = null;

        if (includeWorksheet)
        {
          var rowSet = (DxRowSet)ws;

          if (wb.DxMap != null)
          {
            filteredRowSet = wb.DxMap.FilterRowSet(rowSet);
            filteredRowSet.WorksheetName = ws.WorksheetName;
            rowSets.Add(filteredRowSet);
          }
          else
          {
            rowSet.WorksheetName = ws.WorksheetName;
            rowSets.Add(rowSet);
          }
        }
      }

      return rowSets;
    }

    public static DxRowSet ToRowSet(DxRowSet rowSet, DxFilterSet fs)
    {
      DxRowSet ws = null;
      DxMap map = new DxMap();

      if(rowSet == null)
        return ws;
      else if(fs == null)
        return rowSet;
      else
        ws = map.FilterRowSet(rowSet);
      return ws;
    }
  }
}
