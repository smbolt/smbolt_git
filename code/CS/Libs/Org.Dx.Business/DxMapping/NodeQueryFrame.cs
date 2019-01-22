using Org.GS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class NodeQueryFrame : List<NodeQueryFrameSlot>
  {
    public SortedList<int, Node> Nodes {
      get;
      private set;
    }
    public SortedList<int, NodeQueryElement> NodeQueryElements {
      get;
      private set;
    }
    public QueryExecutionStatus QueryExecutionStatus {
      get {
        return Get_QueryExecutionStatus();
      }
    }
    public string Report {
      get {
        return Get_Report();
      }
    }

    public NodeQueryFrame(SortedList<int, Node> nodes, SortedList<int, NodeQueryElement> nodeQueryElements)
    {
      try
      {
        if (nodes == null)
          throw new Exception("The Nodes collection is null.");

        if (nodeQueryElements == null || nodeQueryElements.Count == 0)
          throw new Exception("The NodeQueryElements collection is null or empty.");

        this.Nodes = nodes;
        this.NodeQueryElements = nodeQueryElements;
        LoadFrame();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to build the NodeQueryFrame.", ex);
      }
    }

    public object ProcessQuery()
    {
      try
      {
        while (true)
        {
          AlignQueryHead();

          if (this.QueryExecutionStatus == QueryExecutionStatus.MatchFailed)
            return null;


        }

        return null;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to process the query.");

      }
    }

    private void AlignQueryHead()
    {
      // get query head frame index (qhfx)
      int qhfx = this.Where(f => f.Qx > -1).First().Fx;
      int minHeadAx = _qF(qhfx).Ax + 1;

      var q = _qF(qhfx);
      var tns = new TextNodeSpec(q.QueryString);

      for (int i = 0; i < this.Count; i++)
      {
        var slot = _s(i);
        if (slot.HasNode)
        {
          var n = slot.N;

          // we're good to this point.  Now we need to implement some code that
          // 1. Starts the query head matching attempt at the first node (when we're just starting the query)
          // 2. Continues forward to see if subsequent query head alignments are possible when a prior head-match location
          //    didn't ending successfully lining up and locking in the query to the nodes.


          if (n.Ax != -2 && n.Ax >= minHeadAx)
          {
            string matchView = "F:" + i.ToString() + " N:" + slot.Nx.ToString() + "[" + slot.NodeText + "] Q:" + q.Ex.ToString() + "[" + q.QueryString + "]";
            if (tns.TokenMatch(n.TextValue))
            {


            }
          }

        }
      }


      q.QueryExecutionStatus = QueryExecutionStatus.MatchFailed;
    }

    private void LoadFrame()
    {
      foreach (var kvp in this.Nodes)
      {
        if (kvp.Key != kvp.Value.Ex)
          throw new Exception("The key of the Nodes collection (" + kvp.Key.ToString() + ") does not match the ElementIndex of the Node (" +
                              kvp.Value.Ex.ToString() + ".");

        kvp.Value.QueryExecutionStatus = QueryExecutionStatus.Initial;
        kvp.Value.Ax = -1;
      }

      foreach (var kvp in this.NodeQueryElements)
      {
        if (kvp.Key != kvp.Value.Ex)
          throw new Exception("The key of the NodeQueryElements collection (" + kvp.Key.ToString() + ") does not match the Element Index of " +
                              "the NodeQureyElement (" + kvp.Value.Ex.ToString() + ").");

        kvp.Value.QueryExecutionStatus = QueryExecutionStatus.Initial;
        kvp.Value.Ax = -1;;
      }

      string nodesReport = GetNodesReport() + g.crlf2 + GetNodeQueryElementsReport();

      int nodeCollectionIndex = 0;
      int nodeQueryElementCollectionIndex = 0;

      while (true)
      {
        var node = this.Nodes.Count > nodeCollectionIndex ? this.Nodes.Values.ElementAt(nodeCollectionIndex++) : null;
        var nodeQueryElement = this.NodeQueryElements.Count > nodeQueryElementCollectionIndex ? this.NodeQueryElements.Values.ElementAt(nodeQueryElementCollectionIndex++) : null;

        if (node == null && nodeQueryElement == null)
          break;

        int nodeIndex = node?.Ex ?? -1;
        int nodeQueryElementIndex = nodeQueryElement?.Ex ?? -1;

        this.Add(new NodeQueryFrameSlot(this, this.Count, nodeIndex, nodeQueryElementIndex));
      }

      string report = this.Report;
    }

    private string Get_Report()
    {
      var sb = new StringBuilder();

      sb.Append("NODE QUERY FRAME DUMP" + g.crlf2);
      sb.Append("FRX NEX NAX SSI NSTA NDATA         |    QEX QAX SSI FLX QSTA QDATA" + g.crlf);
      sb.Append("-----------------------------------|---------------------------------" + g.crlf);

      foreach (var slot in this)
      {
        int frx = slot.Fx;
        int nex = slot.Nx;
        int qex = slot.Qx;

        string nexStr = nex == -1 ? "   " : nex.ToString("000");
        string qexStr = qex == -1 ? "   " : qex.ToString("000");

        var node = this.Nodes.Values.Where(n => n.Ex == nex).FirstOrDefault();
        var query = this.NodeQueryElements.Values.Where(q => q.Ex == qex).FirstOrDefault();

        string nax = node != null ? node.Ax == -1 ? " -1" : node.Ax.ToString("000") : "   ";
        string nssi = node != null ? node.SetSeqIndicator : "  ";
        string nst = node != null ? node.QueryExecutionStatus.ToCode() : "   ";
        string ndat = node != null ? node.TextValue.PadToLength(12) : new String(' ', 12);

        string qax = query != null ? query.Ax == -1 ? " -1" : query.Ax.ToString("000") : "   ";
        string qssi = query != null ? query.SetSeqIndicator : "  ";
        string qflx = query != null ? (query.IsFlexTokens ? query.LowTokenCount.ToString() + "-" + query.HighTokenCount.ToString() : "1  ") : "   ";
        string qst = query != null ? query.QueryExecutionStatus.ToCode() : "    ";
        string qdat = query != null ? query.QueryString.PadToLength(12) : new String(' ', 12);

        sb.Append(frx.ToString("000") + " " +
                  nexStr + " " +
                  nax + " " +
                  nssi + "  " +
                  nst + " " +
                  ndat + " " +

                  " | " +
                  (nax == " -1" ? "   " : " <=") +

                  qexStr + " " +
                  qax + " " +
                  qssi + "  " +
                  qflx + " " +
                  qst + " " +
                  qdat +
                  g.crlf);
      }

      string report = sb.ToString();
      return report;
    }

    private string GetNodesReport()
    {
      var sb = new StringBuilder();

      if (this.Nodes == null)
        return "Nodes collection is null.";

      if (this.Nodes.Count == 0)
        return "Nodes collection is empty.";

      sb.Append("Nodes Report" + g.crlf2);

      foreach (var kvp in this.Nodes)
      {
        string alignedIndex = kvp.Value.Ax > -1 ? kvp.Value.Ax.ToString("000") : " -1";
        sb.Append(kvp.Key.ToString("000") + " " +
                  kvp.Value.SetSeqIndicator + " " +
                  alignedIndex + " " +
                  kvp.Value.TextValue + g.crlf);
      }

      string report = sb.ToString();
      return report;
    }

    private string GetNodeQueryElementsReport()
    {
      var sb = new StringBuilder();

      sb.Append("Node Query Element Report" + g.crlf2);

      foreach (var kvp in this.NodeQueryElements)
      {
        string alignedIndex = kvp.Value.Ax > -1 ? kvp.Value.Ax.ToString("000") : " -1";
        sb.Append(kvp.Key.ToString("000") + " " +
                  (kvp.Value.IsTarget ? "T" : " ") +
                  alignedIndex + " " +
                  kvp.Value.QueryString + " " + g.crlf);

      }

      string report = sb.ToString();
      return report;
    }

    // get slot based on frameIndex
    private NodeQueryFrameSlot _s(int frameIndex)
    {
      return this.ElementAt(frameIndex);
    }

    // get node based on node index
    private Node _n(int nodeIndex)
    {
      return this.Nodes[nodeIndex];
    }

    // get node based on frame index
    private Node _nF(int frameIndex)
    {
      int nodeIndex = this.ElementAt(frameIndex).Nx;

      if (nodeIndex > -1)
        return this.Nodes[nodeIndex];

      throw new Exception("No Node exists at frameIndex " + frameIndex.ToString() + ".");
    }

    // get query element base on query index
    private NodeQueryElement _q (int queryIndex)
    {
      return this.NodeQueryElements[queryIndex];
    }

    // get query element based on frame index
    private NodeQueryElement _qF(int frameIndex)
    {
      int queryIndex = this.ElementAt(frameIndex).Qx;

      if (queryIndex > -1)
        return this.NodeQueryElements[queryIndex];

      throw new Exception("No NodeQueryElement exists at frameIndex " + frameIndex.ToString() + ".");
    }

    private QueryExecutionStatus Get_QueryExecutionStatus()
    {
      var nodeQueryFailedElement = this.NodeQueryElements.Values.Where(e => e.QueryExecutionStatus == QueryExecutionStatus.MatchFailed).FirstOrDefault();

      // if any element is MatchFailed, the query is MatchFailed
      if (nodeQueryFailedElement != null)
        return QueryExecutionStatus.MatchFailed;

      // if all elements are Initial, the query is Initial
      var nodeQueryElementsInitial = this.NodeQueryElements.Values.Where(e => e.QueryExecutionStatus == QueryExecutionStatus.Initial);
      if (nodeQueryElementsInitial.Count() == this.NodeQueryElements.Count())
        return QueryExecutionStatus.Initial;

      // otherwise the query is InProgress
      return QueryExecutionStatus.InProgress;
    }
  }
}
