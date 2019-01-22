using Org.GS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class NodeQueryFrameSlot
  {
    public int Fx {
      get;  // the Frame Index (Fx) is used to reference the Frame Slot within the Frame
      set;
    }
    public int Nx {
      get;  // the Node Index (Nx) is used to relate the Frame Slot to the Nodes collection of the Frame
      set;
    }
    public int Qx {
      get;  // the Query Index (Qx) is used to relate the Frame Slot to the NodeQueryElements collection of the Frame
      set;
    }
    private NodeQueryFrame _f;
    public string Report {
      get {
        return Get_Report();
      }
    }
    public string NodeText {
      get {
        return Get_NodeText();
      }
    }
    public string QueryText {
      get {
        return Get_QueryText();
      }
    }
    public bool HasNode {
      get {
        return Get_HasNode();
      }
    }
    public bool HasQuery {
      get {
        return Get_HasQuery();
      }
    }
    public Node N {
      get {
        return Get_Node();
      }
    }
    public NodeQueryElement Q {
      get {
        return Get_Query();
      }
    }

    public NodeQueryFrameSlot(NodeQueryFrame f, int frameIndex, int nodeIndex, int queryIndex)
    {
      if (nodeIndex == -1 && queryIndex == -1)
        throw new Exception("Both the NodeIndex and the NodeQueryElementIndex are are '-1', at least one must have valid index to their respective collections.");

      _f = f;
      this.Fx = frameIndex;
      this.Nx = nodeIndex;
      this.Qx = queryIndex;
    }

    private string Get_Report()
    {
      var q = _f?.NodeQueryElements.Values?.ElementAt(this.Qx);
      var n = _f?.Nodes.Values?.ElementAt(this.Nx);

      return "F:" + this.Fx.ToString() + " " +
             "N:" + this.Nx.ToString() +
             "[" + (n == null ? "~" : n.TextValue.PadToLength(12).Trim()) + "] " +
             "Q:" + this.Qx.ToString() +
             "[" + (q == null ? "~" : q.QueryString.PadToLength(12).Trim()) + "]";
    }

    private string Get_NodeText()
    {
      var n = _f?.Nodes.Values?.ElementAt(this.Nx);
      return n?.TextValue ?? String.Empty;
    }

    private string Get_QueryText()
    {
      var q = _f?.NodeQueryElements.Values?.ElementAt(this.Qx);
      return q?.QueryString ?? String.Empty;
    }

    private bool Get_HasNode()
    {
      if (this.Nx < 0)
        return false;

      var n = _f?.Nodes?.Values.ElementAt(this.Nx);

      if (n == null)
        throw new Exception("The Node at nodeIndex " + this.Nx.ToString() + " doesn't exist.");

      return true;
    }

    private Node Get_Node()
    {
      if (this.Nx < 0)
        return null;

      var n = _f?.Nodes?.Values.ElementAt(this.Nx);

      if (n == null)
        throw new Exception("The Node at nodeIndex " + this.Nx.ToString() + " doesn't exist.");

      return n;
    }

    private NodeQueryElement Get_Query()
    {
      if (this.Qx < 0)
        return null;

      var q = _f?.NodeQueryElements.Values.ElementAt(this.Qx);

      if (q == null)
        throw new Exception("The NodeQueryElement at queryIndex " + this.Qx.ToString() + " doesn't exist.");

      return q;
    }

    private bool Get_HasQuery()
    {
      if (this.Qx < 0)
        return false;

      var q = _f?.NodeQueryElements.Values.ElementAt(this.Qx);

      if (q == null)
        throw new Exception("The NodeQueryElement at queryIndex " + this.Qx.ToString() + " doesn't exist.");

      return true;
    }
  }
}
