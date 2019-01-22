using Org.GS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Dx.Business
{
  public class Node 
  {
    private NodeType _nodeType;
    public NodeType NodeType { get { return _nodeType; } }

    private DxObjectType _dxObjectType;
    public DxObjectType DxObjectType { get { return _dxObjectType; } }

    private object _dxObject;
    public object DxObject
    {
      get { return _dxObject; }
      set
      {
        _dxObject = value;
        SetDxObjectType(value);
      }
    }

    public string TextValue { get { return Get_TextValue(); } }
    public bool IsFirstInSet { get; set; }
    public bool IsLastInSet { get; set; }
    public bool IsFirstInSequence { get; set; }
    public bool IsLastInSequence { get; set; }
    public string SetSeqIndicator { get { return Get_SetSeqIndicator(); } }
    public int Ex { get; set; } // Element index, the position of the node in the sequence of nodes
    public int Ax { get; set; } // Alignment index, relates this object to a specific node query element within the query
    public QueryExecutionStatus QueryExecutionStatus { get; set; }

    public Node Parent { get; set; }
    public NodeSet NodeSet { get; set; }
    public Dictionary<string, object> NodeData;

    public Node()
    {
      this.NodeData = new Dictionary<string, object>();
      this.Ex = -1;
      this.Ax = -1;
      this.QueryExecutionStatus = QueryExecutionStatus.Initial;
    }

    public DxCell GetNodeQueryResult(NodeQuery nodeQuery)
    {
      try
      {
        if (this.NodeType == NodeType.Elemental)
          throw new Exception("DxCell extraction cannot be requested from an elemental node - DxMapItem is '" + nodeQuery.DxMapItem.Report + "'.");

        var cells = GetDxCells();
        nodeQuery.InitializeExecution();
        return nodeQuery.ProcessQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to retrieve a cell based on an ExtractMask.", ex);
      }
    }

    public SortedList<int, DxCell> GetDxCells()
    {
      switch (this.DxObjectType)
      {
        case DxObjectType.DxRowSet:
          return ((DxRowSet)_dxObject).GetCells();

        case DxObjectType.DxCellSet:
          return ((DxCellSet)_dxObject).GetCells();

        case DxObjectType.DxColumnSet:
          return ((DxColumnSet)_dxObject).GetCells();

        case DxObjectType.DxColumn:
          return ((DxColumn)_dxObject).GetCells();

        default:
          throw new Exception("An unexpected object type '" + this.DxObjectType.ToString() + "' encountered while attempting to extract cell list from DxObject.");
      }
    }

    public void SetNodeData(string nodeData)
    {
      try
      {
        if (nodeData.IsBlank())
          return;

        string[] kvpSets = nodeData.Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);

        foreach (var kvp in kvpSets)
        {
          string[] tokens = kvp.Split(Constants.EqualsDelimiter, StringSplitOptions.RemoveEmptyEntries);
          if (tokens.Length != 2)
            throw new Exception("The NodeData was in an invalid format. The values should be key-value pairs of strings separate by an equals sign with each " +
                                "paired set being separated by commas - i.e. 'Key1=Value1,Key2=Value2'.");

          string key = tokens[0];
          string value = tokens[1];

          if (this.NodeData.ContainsKey(key))
            this.NodeData[key] = value;
          else
            this.NodeData.Add(key, value);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to process the NodeData value '" + nodeData + "'.", ex);
      }    
    }

    private void SetDxObjectType(object dxObject)
    {
      if (dxObject == null)
        throw new Exception("The DxObject object passed to the Node object cannot be null.");

      string objectType = dxObject.GetType().Name;

      switch (objectType)
      {
        case "DxCell":
          _dxObjectType = DxObjectType.DxCell;
          _nodeType = NodeType.Elemental;
          break;

        case "DxRow":
        case "DxCellSet":
          _dxObjectType = DxObjectType.DxCellSet;
          _nodeType = NodeType.Group;
          break;

        case "DxWorksheet":
        case "DxRowSet":
          _dxObjectType = DxObjectType.DxRowSet;
          _nodeType = NodeType.Group;
          break;

        case "DxColumnSet":
          _dxObjectType = DxObjectType.DxColumnSet;
          _nodeType = NodeType.Group;
          break;

        case "DxColumn":
          _dxObjectType = DxObjectType.DxColumn;
          _nodeType = NodeType.Group;
          break;

        default:
          throw new Exception("The object passed to the Node object as its DxObject is not a valid type '" + objectType + "'.");
      }
    }

    public void InitializePosition()
    {
      this.IsFirstInSet = false;
      this.IsLastInSet = false;
      this.IsFirstInSequence = false;
      this.IsLastInSequence = false;
    }

    private string Get_TextValue()
    {
      if (this.DxObject == null || this.DxObject.GetType().Name != "DxCell")
        return String.Empty;

      return ((DxCell)this.DxObject).TextValue;
    }

    private string Get_SetSeqIndicator()
    {
      string first = "-";
      string last = "-";

      if (this.IsFirstInSet)
      {
        first = "A";
      }
      else
      {
        if (this.IsFirstInSequence)
          first = "B";
      }

      if (this.IsLastInSet)
      {
        last = "Z";
      }
      else
      {
        if (this.IsLastInSequence)
          last = "Y";
      }

      return first + last;
    }
  }
}
