using Org.GS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class NodeQuery
  {
    private string _rawQuery;
    public string Switches { get; set; }

    public QueryExecutionStatus QueryExecutionStatus { get { return Get_QueryExecutionStatus(); } }
    public int HeadAnchorPoint { get; private set; }
    public int MaxAlignedSpecificQueryIndex { get { return Get_MaxAlignedSpecificQueryIndex(); } }
    public int MaxAlignedCellIndex { get { return Get_MaxAlignedCellIndex(); } }

    private DxMapItem _dxMapItem;
    public DxMapItem DxMapItem { get { return _dxMapItem; } }
    public SortedList<int, NodeQueryElement> NodeQueryElements;
    private SortedList<int, DxCell> Cells { get; set; }
    public NodeQueryFrame NodeQueryFrame { get; private set; }
        
    public int Count
    {
      get
      {
        if (this.NodeQueryElements == null)
          this.NodeQueryElements = new SortedList<int, NodeQueryElement>();
        return this.NodeQueryElements.Count;
      }
    }

    public string Report { get { return Get_Report(); } }
    public string Report2 { get { return Get_Report2(); } }

    public NodeQuery(DxMapItem dxMapItem, string rawQuery, SortedList<int, DxCell> cells)
    {
      try
      {
        if (dxMapItem == null)
          throw new Exception("The DxMapItem is null - the rawText of the NodeQuery is '" + (rawQuery != null ? rawQuery : "NULL") + "'.");

        _dxMapItem = dxMapItem;

        if (rawQuery.IsBlank())
          throw new Exception("The rawText is blank or null - the DxMapItem is '" + dxMapItem.Report + "'.");

        if (cells == null)
          throw new Exception("The cells collection is null.");

        this.Cells = cells;

        for (int c = 0; c < this.Cells.Count; c++)
        {
          var cell = this.Cells.Values.ElementAt(c);
          cell.QueryExecutionStatus = QueryExecutionStatus.Initial;
          cell.Ex = c;
          cell.Ax = -1;
        }

        this.Switches = String.Empty;

        _dxMapItem = DxMapItem;
        _rawQuery = rawQuery.Trim();
        this.HeadAnchorPoint = -1;

        this.NodeQueryElements = new SortedList<int, NodeQueryElement>();

        Parse();
        Validate();

        InitializeExecution();
        this.NodeQueryFrame = new NodeQueryFrame(this.Cells.ToNodeSet(), this.NodeQueryElements); 
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the NodeQuery constructor with raw query of '" + rawQuery + "'.", ex); 
      }
    }
    
    public void InitializeExecution()
    {
      foreach (var kvp in this.NodeQueryElements)
      {
        kvp.Value.Ax = -1;
        kvp.Value.LastMatchAttemptIndex = -1;
        if (kvp.Value.IsTarget)
          kvp.Value.QueryExecutionStatus = QueryExecutionStatus.Target;
        else
          kvp.Value.QueryExecutionStatus = QueryExecutionStatus.Initial;
      }

      if (this.Cells != null)
      {
        foreach (var kvp in this.Cells)
        {
          kvp.Value.Ax = -1;
          kvp.Value.QueryExecutionStatus = QueryExecutionStatus.Initial;
        }
      }
    }

    public DxCell ProcessQuery()
    {
      try
      {
        var result = this.NodeQueryFrame.ProcessQuery();


        string report = this.Report;

        InitializeExecution();

        while (true)
        {
          // locate where the head (first) queryElement aligns with the cells
          this.HeadAnchorPoint = GetHeadAnchorPoint(this.HeadAnchorPoint);

          report = this.Report;

          // if there is no anchor point, the query returns null
          if (this.HeadAnchorPoint == -1)
            return null;

          // if there is an anchor point, set up the rest of the query processing
          InitializeQueryBody(this.HeadAnchorPoint);

          // Starting with the second query element, attempt to align the query with the cells
          
          for (int q = 1; q < this.NodeQueryElements.Count; q++)
          {
            string report3 = this.Report;

            var queryElement = this.NodeQueryElements.Values.ElementAt(q);

            if (!queryElement.IsTarget)
            {
              var tncs = new TextNodeSpec(queryElement.QueryString);

              for (int c = this.MaxAlignedCellIndex + 1; c < this.Cells.Count; c++)
              {
                var cell = this.Cells.Values.ElementAt(c);
                string matchView = "Q" + q.ToString() + " [" + queryElement.QueryString + "] : C:" + c.ToString() + " [" + cell.TextValue + "]";

                if (tncs.TokenMatch(cell.TextValue))
                {
                  queryElement.QueryExecutionStatus = QueryExecutionStatus.Matched;
                  SetAlignment(queryElement, cell);
                  break;
                }
                else
                {
                  if (this.IntervalAllowed(q, c))
                  {
                    SetIntervalAlignment(queryElement, cell);
                    continue;
                  }
                  else
                  {
                    queryElement.QueryExecutionStatus = QueryExecutionStatus.MatchFailed;
                    break;
                  }
                }
              }
            }
          }

          if (this.QueryExecutionStatus == QueryExecutionStatus.InProgress)
            this.CheckForSuccess();

          if (this.QueryExecutionStatus == QueryExecutionStatus.MatchFailed)
            break;
        }

        if (this.QueryExecutionStatus == QueryExecutionStatus.Matched)
        {
          var targetQueryElement = this.NodeQueryElements.Values.Where(e => e.IsTarget).FirstOrDefault();
          return this.Cells[targetQueryElement.Ax];
        }

        return null;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to process the NodeQuery for DxMapItem '" + _dxMapItem.Report + "'.", ex);
      }
    }

    private void SetAlignment(NodeQueryElement queryElement, DxCell cell)
    {
      cell.QueryExecutionStatus = queryElement.QueryExecutionStatus;

      queryElement.Ax = cell.Ex;
      cell.Ax = queryElement.Ex;

      // align remaining queryElements with remaining cells
      int alignmentIndex = queryElement.Ax + 1;
      int qIndex = queryElement.Ex + 1;

      string report2 = this.Report2;

      for (int j = qIndex; j < this.Count; j++)
      {
        var nextQuery = this.NodeQueryElements.Values.ElementAt(j);

        if (nextQuery.IsTarget)
        {
          if (nextQuery.QueryExecutionStatus == QueryExecutionStatus.MatchedTarget ||
              nextQuery.QueryExecutionStatus == QueryExecutionStatus.MatchedTargetPending)
          {
            nextQuery.Ax = alignmentIndex++;
          }
        }
        else
        {
          nextQuery.Ax = alignmentIndex++;
        }

        if (nextQuery.IsTarget)
          nextQuery.QueryExecutionStatus = QueryExecutionStatus.Target;
        else
          nextQuery.QueryExecutionStatus = QueryExecutionStatus.Initial;
      }

      string report = this.Report;
    }

    private void SetIntervalAlignment(NodeQueryElement queryElement, DxCell cell)
    {
      queryElement.QueryExecutionStatus = QueryExecutionStatus.InInterval;
      cell.QueryExecutionStatus = QueryExecutionStatus.InInterval;

      int currQueryIndex = queryElement.Ex;
      int currCellIndex = cell.Ex;

      string report2 = this.Report2;
      string report = this.Report;

      int prevMatchedIndex = queryElement.PriorMatchedIndex;


      queryElement.QueryExecutionStatus = QueryExecutionStatus.InProgress;


    }

    private void CheckForSuccess()
    {
      string report = this.Report;


    }

    private void Validate()
    {
      if (this.NodeQueryElements == null || this.NodeQueryElements.Count == 0)
        throw new Exception("The NodeQueryElements collection is null or empty.");

      // Ensure that the last MatchAny is followed by a MatchSpecific match type
      bool matchSpecificFound = false;
      for (int i = this.Count - 1; i > -1; i--)
      {
        var queryElement = this.NodeQueryElements.Values.ElementAt(i);

        if (!queryElement.IsTarget)
        {
          if (queryElement.MatchType == MatchType.MatchSpecific)
            matchSpecificFound = true;

          if (queryElement.MatchType == MatchType.MatchAny)
          {
            if (!matchSpecificFound)
              throw new Exception("There must be at least one 'MatchSpecific' query type element following the last 'MatchAny' element in the node query - " +
                                  "DxMapItem = '" + _dxMapItem?.Report + "'.");
          }
        }
      }

      // Disallow back-to-back flex-token query elements
      bool lastQueryElementIsFlexToken = false;
      for (int i = 0; i < this.Count; i++)
      {
        var queryElement = this.NodeQueryElements.Values.ElementAt(i);
        if (queryElement.IsFlexTokens)
        {
          if (lastQueryElementIsFlexToken)
            throw new Exception("Back to back flex-tokens query elements are not allowed.  Second query element is '" + queryElement.Report + "'.");
          lastQueryElementIsFlexToken = true;
        }
        else
        {
          lastQueryElementIsFlexToken = false;
        }
      }
    }

    private int GetHeadAnchorPoint(int prevHeadAnchorPoint)
    {
      try
      {
        int queryIndex = prevHeadAnchorPoint + 1;

        if (this.Count - 1 < queryIndex)
          return -1;

        var queryElement = this.NodeQueryElements.Values.ElementAt(queryIndex);
        var tncs = new TextNodeSpec(queryElement.QueryString);

        foreach (var kvp in this.Cells)
        {
          string matchView = kvp.Key.ToString() + " [" + kvp.Value.TextValue + "] : " + queryIndex.ToString() + " [" + queryElement.QueryString + "]";
          if (tncs.TokenMatch(kvp.Value.TextValue))
          {
            return kvp.Key;
          }
        }
        
        return -1;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to determine the head anchor point for the NodeQuery '" + this.Report, ex);
      }
    }

    private void InitializeQueryBody(int headAnchorPoint)
    {
      // This method updates the matched "headQueryElement" (first queryElement in the query) with the 
      // index of the cell it was matched against (the headAnchorPoint) and updates its status to "Matched".
      // Then the rest of the query elements (the "body") are updated with subsequent/ascending cell indices
      // to "provisionally align" the query elements with the cells after the headAnchorPoint.  The provisional
      // alignment is intended to support the "Report" property in the display of current query status and alignment.
      // The "body" elements will thus have (provisional) alignment, indicated by the "NotAttempted" query status.

      var headQueryElement = this.NodeQueryElements.Values.First();
      headQueryElement.QueryExecutionStatus = QueryExecutionStatus.Matched;
      headQueryElement.Ax = headAnchorPoint;

      int alignedIndex = headAnchorPoint + 1;

      for (int i = 1; i < this.NodeQueryElements.Count; i++)
      {
        var queryElement = this.NodeQueryElements.Values.ElementAt(i);

        if (queryElement.IsTarget)
          queryElement.Ax = -1;
        else
          queryElement.Ax = alignedIndex++;

        queryElement.QueryExecutionStatus = QueryExecutionStatus.Initial;
      }

      string report = this.Report;
    }

    private void Parse()
    {
      try
      {
        if (_rawQuery.IsBlank())
          throw new Exception("The raw text of the NodeQuery is blank or null.");

        if (!_rawQuery.StartsWith("("))
          throw new Exception("The raw text of the NodeQuery must begin with an open parenthesis.");

        int endParen = _rawQuery.LastIndexOf(")");
        int begSwitch = _rawQuery.IndexOf("/", endParen);

        if (begSwitch > -1)
        {
          this.Switches = _rawQuery.Substring(begSwitch);
          _rawQuery = _rawQuery.Substring(0, endParen + 1); 
        }

        if (!_rawQuery.EndsWith(")"))
          throw new Exception("The raw text of the NodeQuery must end with an close parenthesis.");

        int beg = 0;
        int end = -1;

        while (true)
        {
          end = _rawQuery.IndexOf(")", beg + 1);

          if (end == -1)
            throw new Exception("An open parenthesis in position (index) " + beg.ToString() + " does not have a matching close parenthesis - raw text of NodeQuery is '" + _rawQuery + "'.");

          string queryNode = _rawQuery.Substring(beg + 1, (end - beg) - 1);

          var nodeQueryElement = new NodeQueryElement(this, this.NodeQueryElements.Count, queryNode);

          this.NodeQueryElements.Add(nodeQueryElement.Ex, nodeQueryElement);

          if (end >= _rawQuery.Length - 1)
            break;

          beg = end + 1;

          var nextChar = _rawQuery[beg];
          if (nextChar != '(')
            throw new Exception("Only an open parenthesis may follow the a close parenthesis which is not the final character in the NodeQuery, found '" +
                                 nextChar.ToString() + "' in position " + beg.ToString() + ", full text of NodeQuery is '" + _rawQuery + "'.");
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to parse the NodeQuery portion of the DxMapItem '" + _dxMapItem.Report + "'.", ex);
      }
    }

    private string Get_Report2()
    {
      if (this.Cells == null || this.Cells.Count == 0 || this.NodeQueryElements == null || this.NodeQueryElements.Count == 0)
        return "Query not ready to report.";

      string reportO = this.Report;

      var sb = new StringBuilder();


      string report = sb.ToString();

      return report;
    }


    private string Get_Report()
    {
      if (this.Cells == null || this.Cells.Count == 0 || this.NodeQueryElements == null || this.NodeQueryElements.Count == 0)
        return "Query not ready to report.";
      
      var sb = new StringBuilder();
      sb.Append("NodeQuery Dump" + g.crlf);
      sb.Append("Status: " + this.QueryExecutionStatus.ToString() + g.crlf2);

      int limitCount = this.Cells.Count > this.NodeQueryElements.Count ? this.Cells.Count : this.NodeQueryElements.Count;

      for (int i = 0; i < limitCount; i++)
      {
        var cell = this.Cells.Count > i ? this.Cells.Values.ElementAt(i) : null;
        var nodeQueryElement = GetAlignedNodeQuery(i);

        sb.Append(i.ToString("000 "));

        if (cell != null)
        {
          sb.Append("R" + cell.RowIndex.ToString("000") + " C" + cell.ColumnIndex.ToString("000") + " " + cell.SetSeqIndicator + " ");
          sb.Append(cell.TextValue.PadToLength(12));
        }
        else
        {
          sb.Append(new String(' ', 25));
        }

        sb.Append("  ");

        if (nodeQueryElement != null)
        {
          switch (nodeQueryElement.QueryExecutionStatus)
          {
            case QueryExecutionStatus.Matched:
              sb.Append("<M");
              break;

            case QueryExecutionStatus.MatchedTargetPending:
              sb.Append("<P");
              break;

            case QueryExecutionStatus.InInterval:
              sb.Append("<I");
              break;

            case QueryExecutionStatus.Target:
              sb.Append("<T");
              break;

            case QueryExecutionStatus.Initial:
              sb.Append(" -");
              break;

            case QueryExecutionStatus.MatchFailed:
              sb.Append(" F");
              break;
          }
            
          sb.Append(" " + nodeQueryElement.Ex.ToString("000") + " " + nodeQueryElement.SetSeqIndicator + " ");
          sb.Append((nodeQueryElement.Ax > -1 ? nodeQueryElement.Ax.ToString("000") : "   ") + " ");

          if (nodeQueryElement.IsTarget)
          {
            if (nodeQueryElement.QueryExecutionStatus == QueryExecutionStatus.MatchedTarget)
            {
              var matchedCell = this.Cells.Values.ElementAt(nodeQueryElement.Ax);
              sb.Append(nodeQueryElement.IsFlexTokens ? "F" : "S");
              sb.Append(" " + nodeQueryElement.LowTokenCount.ToString() + "-" + nodeQueryElement.HighTokenCount.ToString() + " ");
              sb.Append(matchedCell.TextValue);
            }
            else
            {
              sb.Append(nodeQueryElement.IsFlexTokens ? "F" : "S");
              sb.Append(" " + nodeQueryElement.LowTokenCount.ToString() + "-" + nodeQueryElement.HighTokenCount.ToString() + " ");
              sb.Append(nodeQueryElement.QueryString);
            }
          }
          else
          {
            sb.Append(nodeQueryElement.IsFlexTokens ? "F" : "S");
            sb.Append(" " + nodeQueryElement.LowTokenCount.ToString() + "-" + nodeQueryElement.HighTokenCount.ToString() + " "); 
            sb.Append(nodeQueryElement.QueryString);
          }
        }

        sb.Append(g.crlf);
      }

      string report = sb.ToString();      

      return report;
    }

    private NodeQueryElement GetAlignedNodeQuery(int index)
    {
      var matchedQueryElement = this.NodeQueryElements.Values.Where(e => e.QueryExecutionStatus == QueryExecutionStatus.Matched).FirstOrDefault();

      NodeQueryElement alignedElement = null;

      if (matchedQueryElement != null)
      {
        alignedElement = this.NodeQueryElements.Values.Where(e => e.Ax == index).FirstOrDefault();
      }
      else
      {
        alignedElement = this.NodeQueryElements.Values.Where(e => e.Ex == index).FirstOrDefault();
      }

      return alignedElement;
    }

    private QueryExecutionStatus Get_QueryExecutionStatus()
    {
      var nodeQueryFailedElement = this.NodeQueryElements.Values.Where(e => e.QueryExecutionStatus == QueryExecutionStatus.MatchFailed).FirstOrDefault();

      if (nodeQueryFailedElement != null)
        return QueryExecutionStatus.MatchFailed;

      var nodeQueryElementsNotAttempted = this.NodeQueryElements.Values.Where(e => e.QueryExecutionStatus == QueryExecutionStatus.Initial);
      if (nodeQueryElementsNotAttempted.Count() == this.NodeQueryElements.Count())
        return QueryExecutionStatus.Initial;

      return QueryExecutionStatus.InProgress;
    }

    private int Get_MaxAlignedSpecificQueryIndex()
    {
      if (this.NodeQueryElements == null || this.NodeQueryElements.Count == 0)
        return -1;

      int lastAlignedQueryIndex = -1;

      foreach (var queryElement in this.NodeQueryElements.Values)
      {
        if (!queryElement.IsTarget)
        {
          if (queryElement.QueryExecutionStatus == QueryExecutionStatus.Matched && queryElement.MatchType == MatchType.MatchSpecific)
            lastAlignedQueryIndex = queryElement.Ex;
        }
      }

      return lastAlignedQueryIndex;
    }

    private int Get_MaxAlignedCellIndex()
    {
      if (this.NodeQueryElements == null || this.NodeQueryElements.Count == 0)
        return -1;

      int lastAlignedCellIndex = -1;

      foreach (var queryElement in this.NodeQueryElements.Values)
      {
        if (!queryElement.IsTarget)
        {
          if (queryElement.QueryExecutionStatus == QueryExecutionStatus.Matched)
            lastAlignedCellIndex = queryElement.Ax;
        }
      }

      return lastAlignedCellIndex;
    }

    private bool IntervalAllowed(int currQueryIndex, int currCellIndex)
    {
      var queryElement = this.NodeQueryElements.Values.ElementAt(currQueryIndex);

      int maxAlignedCellIndex = this.MaxAlignedCellIndex;
      int currCellInterval = currCellIndex - maxAlignedCellIndex;
      
      int minAllowedInterval = 0;
      int maxAllowedInterval = 0;
      int maxAlignedSpecificQueryIndex = this.MaxAlignedSpecificQueryIndex;

      for (int q = maxAlignedSpecificQueryIndex + 1; q < currQueryIndex; q++)
      {
        var intervalQueryElement = this.NodeQueryElements.Values.ElementAt(q);
        minAllowedInterval += intervalQueryElement.LowTokenCount;
        maxAllowedInterval += intervalQueryElement.HighTokenCount;
      }

      if (currCellInterval < minAllowedInterval || currCellInterval > maxAllowedInterval)
        return false;

      return true;
    }

    public int GetMatchedIndexBefore(int index)
    {
      if (this.NodeQueryElements == null || this.NodeQueryElements.Count == 0 || this.NodeQueryElements.Count - 1 < index - 1)
        return -1;

      for (int i = index - 1; i > -1; i--)
      {
        var queryElement = this.NodeQueryElements.Values.ElementAt(i);
        if (!queryElement.IsTarget && queryElement.QueryExecutionStatus == QueryExecutionStatus.Matched && queryElement.MatchType == MatchType.MatchSpecific)
          return queryElement.Ex;
      }

      return -1;
    }
  }
}
