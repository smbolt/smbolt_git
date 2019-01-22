using Org.GS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class NodeQueryElement
  {
    private string _rawValue;
    private string _originalValue;

    public QueryExecutionStatus QueryExecutionStatus {
      get;
      set;
    }
    public MatchType MatchType {
      get;
      private set;
    }
    public int LastMatchAttemptIndex {
      get;
      set;
    }
    public int Ax {
      get;  // Alignment index, the index of the node the query element is aligned against
      set;
    }
    public int Ex {
      get;  // Element index, the position of the this object in the NodeQuery
      set;
    }
    public int PriorMatchedIndex {
      get {
        return Get_PriorMatchedIndex();
      }
    }

    public string QueryString {
      get;
      private set;
    }
    public bool IsFlexTokens {
      get;
      private set;
    }
    public int LowTokenCount {
      get;
      private set;
    }
    public int HighTokenCount {
      get;
      private set;
    }
    public bool MatchFirstInSet {
      get;
      private set;
    }
    public bool MatchFirstInSequence {
      get;
      private set;
    }
    public bool MatchLastInSet {
      get;
      private set;
    }
    public bool MatchLastInSequence {
      get;
      private set;
    }
    public string SetSeqIndicator {
      get {
        return Get_SetSeqIndicator();
      }
    }
    public bool IsMatchedTarget {
      get {
        return Get_IsMatchedTarget();
      }
    }
    public string Report {
      get {
        return Get_Report();
      }
    }

    public bool IsTarget {
      get;
      private set;
    }

    public NodeQuery NodeQuery {
      get;
      private set;
    }

    public NodeQueryElement(NodeQuery nodeQuery, int elementIndex, string rawValue)
    {
      try
      {
        if (nodeQuery == null)
          throw new Exception("The NodeQuery (parent property) cannot be null.");

        this.NodeQuery = nodeQuery;

        this.QueryString = String.Empty;
        this.QueryExecutionStatus = QueryExecutionStatus.Initial;
        this.LastMatchAttemptIndex = -1;

        if (rawValue.IsBlank())
          throw new Exception("The raw value of the NodeQueryElement is null or blank.");

        _originalValue = rawValue.Trim();
        _rawValue = rawValue.Trim();

        this.MatchFirstInSet = false;
        this.MatchFirstInSequence = false;
        this.MatchLastInSet = false;
        this.MatchLastInSequence = false;
        this.Ex = elementIndex;

        if (_rawValue.StartsWith("``"))
        {
          this.MatchFirstInSet = true;
          this.MatchFirstInSequence = true;
          _rawValue = _rawValue.Substring(2);
        }

        if (_rawValue.StartsWith("`"))
        {
          this.MatchFirstInSequence = true;
          _rawValue = _rawValue.Substring(1);
        }

        if (_rawValue.EndsWith("``"))
        {
          this.MatchLastInSet = true;
          this.MatchLastInSequence = true;
          _rawValue = _rawValue.Substring(0, _rawValue.Length - 2);
        }

        if (_rawValue.EndsWith("`"))
        {
          this.MatchLastInSequence = true;
          _rawValue = _rawValue.Substring(0, _rawValue.Length - 1);
        }

        if (_rawValue.IsBlank())
          throw new Exception("The raw query value is an empty string after removing back ticks (`) which indicate position in set or sequence - " +
                              "the original value is '" + _originalValue + "' - on DxMapItem + '" + this.NodeQuery.DxMapItem.Report + "'.");


        int slashPos = _rawValue.IndexOf("/");
        if (slashPos != -1)
        {
          if (slashPos != 1)
            throw new Exception("The raw query value is not valid.  If the query includes a slash, it must be in the second position (index = 1) " +
                                "immediately following the period or asterisk. DxMapItem is '" + this.NodeQuery.DxMapItem.Report + "'.");

          string detailSwitch = _rawValue.Substring(slashPos + 1);

          _rawValue = _rawValue.Substring(0, slashPos);


          if (detailSwitch.Contains("-"))
          {
            string[] tokens = detailSwitch.Split(Constants.DashDelimiter, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length != 2)
              throw new Exception("The raw query value is not valid.  When the detail switch includes a hyphen, it must be used to separate two integer values " +
                                  "indicating the low and high values of the number of cells. DxMapItem is '" + this.NodeQuery.DxMapItem.Report + "'.");

            if (tokens[0].IsNotInteger() || tokens[1].IsNotInteger())
              throw new Exception("The raw query value is not valid.  When the detail switch includes a hyphen, it must be used to separate two integer values " +
                                  "indicating the low and high values of the number of cells. DxMapItem is '" + this.NodeQuery.DxMapItem.Report + "'.");

            this.LowTokenCount = tokens[0].ToInt32();
            this.HighTokenCount = tokens[1].ToInt32();

            if (this.HighTokenCount <= this.LowTokenCount)
              throw new Exception("The 'high token count' (" + this.HighTokenCount.ToString() + ") must be greater than the 'low token count'" + this.LowTokenCount.ToString() +
                                  " when a range of cell counts is specified in the node query. DxMapItem is '" + this.NodeQuery.DxMapItem.Report + "'.");

            this.IsFlexTokens = true;
          }
          else
          {
            if (detailSwitch.IsNotInteger())
              throw new Exception("The detail switch of the query which begins with an asterisk (match to any value) must be a valid integer or " +
                                  "range of integers (i.e. '1', '1-3', etc.");

            this.LowTokenCount = detailSwitch.ToInt32();
            this.HighTokenCount = detailSwitch.ToInt32();
            this.IsFlexTokens = true;
          }
        }

        if (_rawValue.StartsWith("."))
        {
          if (_rawValue.Length > 1)
            throw new Exception("The query is invalid.  After removing the details switches (beginning with the forward slash) the only allowed " +
                                "remainder is the period - DxMapItem is '" + this.NodeQuery.DxMapItem.Report + "'.");


          this.MatchType = MatchType.Target;
          this.IsTarget = true;
          this.QueryString = ".";
          if (slashPos == -1)
          {
            this.LowTokenCount = 1;
            this.HighTokenCount = 1;
          }

          if (this.LowTokenCount == 0)
            throw new Exception("The low token count in the detail switch for the target query element cannot be zero.  There must be at least once cell " +
                                "specified for retrieval - DxMapItem is '" + this.NodeQuery.DxMapItem.Report + "'.");
          return;
        }

        if (_rawValue.StartsWith("*"))
        {
          if (_rawValue.Length > 1)
            throw new Exception("The query is invalid.  After removing the details switches (beginning with the forward slash) the only allowed " +
                                "remainder is the asterisk - DxMapItem is '" + this.NodeQuery.DxMapItem.Report + "'.");
          this.MatchType = MatchType.MatchAny;
          this.QueryString = "*";
          if (slashPos == -1)
          {
            this.LowTokenCount = 1;
            this.HighTokenCount = 1;
          }
          return;
        }

        this.LowTokenCount = 1;
        this.HighTokenCount = 1;
        this.MatchType = MatchType.MatchSpecific;
        this.QueryString = _rawValue;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred in the NodeQueryElement constructor with raw data '" + rawValue + "'.", ex);
      }
    }

    private int Get_PriorMatchedIndex()
    {
      if (this.NodeQuery == null)
        throw new Exception("The parent NodeQuery object is null on NodeQueryElement '" + this.Report + "'.");

      return this.NodeQuery.GetMatchedIndexBefore(this.Ex);
    }

    private string Get_SetSeqIndicator()
    {
      string first = "-";
      string last = "-";

      if (this.MatchFirstInSet)
      {
        first = "A";
      }
      else
      {
        if (this.MatchFirstInSequence)
          first = "B";
      }

      if (this.MatchLastInSet)
      {
        first = "Z";
      }
      else
      {
        if (this.MatchLastInSequence)
          first = "Y";
      }

      return first + last;
    }

    private bool Get_IsMatchedTarget()
    {
      if (this.Ax < 0)
        return false;

      switch (this.QueryExecutionStatus)
      {
        case QueryExecutionStatus.MatchedTarget:
        case QueryExecutionStatus.MatchedTargetPending:
          return true;
      }

      return false;
    }

    private string Get_Report()
    {
      return _rawValue;
    }
  }
}
