using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.OpsManager.Controls
{
  public class NotifyChangeResult
  {
    public NotifyType NotifyType {
      get;
      set;
    }
    public int ObjectId {
      get;
      set;
    }
    public string NewObjectName {
      get;
      set;
    }
    public int? ParentId {
      get;
      set;
    }
    public int? XRefId {
      get;
      set;
    }

    public NotifyChangeResult()
    {
      this.NotifyType = NotifyType.NotSet;
      this.ObjectId = -1;
      this.NewObjectName = String.Empty;
      this.ParentId = null;
      this.XRefId = null;
    }

    public NotifyChangeResult(NotifyType notifyType, int objectId, string newObjectName)
    {
      this.NotifyType = notifyType;
      this.ObjectId = objectId;
      this.NewObjectName = newObjectName;
      this.ParentId = null;
      this.XRefId = null;
    }

    public NotifyChangeResult(NotifyType notifyType, int objectId, string newObjectName, int parentId)
    {
      this.NotifyType = notifyType;
      this.ObjectId = objectId;
      this.NewObjectName = newObjectName;
      this.ParentId = parentId;
      this.XRefId = null;
    }

    public NotifyChangeResult(NotifyType notifyType, int objectId, string newObjectName, int parentId, int xRefId)
    {
      this.NotifyType = notifyType;
      this.ObjectId = objectId;
      this.NewObjectName = newObjectName;
      this.ParentId = parentId;
      this.XRefId = xRefId;
    }
  }
}
