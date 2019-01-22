using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS.Configuration;

namespace Org.OpsManager.Controls
{
  public class PanelData
  {
    public NotifyType NotifyType { get; set; }
    public NodeType NodeType { get; set; }
    public int? ObjectId { get; set; }
    public int? XRefId { get; set; }
    public int? ParentId { get; set; }
    public string TreeNodePath { get; set; }
    public ConfigDbSpec ConfigDbSpec { get; set; }

    public PanelData(NotifyType notifyType, NodeType nodeType, int? objectId, int? xRefId, int? parentId, string treeNodePath, ConfigDbSpec configDbSpec)
    {
      this.NotifyType = notifyType;
      this.NodeType = nodeType;
      this.ObjectId = objectId;
      this.XRefId = xRefId;
      this.ParentId = parentId;
      this.TreeNodePath = treeNodePath;
      this.ConfigDbSpec = configDbSpec;
    }
  }
}
