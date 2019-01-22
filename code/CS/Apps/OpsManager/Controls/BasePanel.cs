using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS.Configuration;

namespace Org.OpsManager.Controls
{
  public partial class BasePanel : UserControl
  {
    public NotifyType NotifyType { get; set; }
    public ChangeType ChangeType { get; set; }
    public NodeType NodeType { get; set; }
    public bool IsDirty { get; set; }
    public int? ParentId { get; set; }
    public ConfigDbSpec ConfigDbSpec { get; set; }

    public event Action<NotifyChangeResult> NotifyUpdate;
    public event Action<NotifyChangeResult> NotifyInsert;
    public event Action<NotifyChangeResult> NotifyDelete;

    public BasePanel()
    {
      InitializeComponent();
      this.Dock = DockStyle.Fill;
      this.Name = "pnlBasePanel";
    }

    public void InitializeBaseProperties(PanelData panelData, ChangeType changeType)
    {
      this.NotifyType = panelData.NotifyType;
      this.ChangeType = changeType;
      this.NodeType = panelData.NodeType;
      this.IsDirty = false;
      this.ParentId = panelData.ParentId;
      this.ConfigDbSpec = panelData.ConfigDbSpec;
    }

    public void FireNotifyUpdate(NotifyChangeResult ncr)
    {
      if (this.NotifyUpdate != null)
        this.NotifyUpdate(ncr);
    }

    public void FireNotifyInsert(NotifyChangeResult ncr)
    {
      if (this.NotifyInsert != null)
        this.NotifyInsert(ncr);
    }

    public void FireNotifyDelete(NotifyChangeResult ncr)
    {
      if (this.NotifyDelete != null)
        this.NotifyDelete(ncr);
    }
  }

  public enum NotifyType
  {
    NotSet = 0,
    NotifyConfigSet = 1,
    NotifyConfig = 2,
    NotifyEvent = 3,
    NotifyGroup = 4,
    NotifyPerson = 5
  }

  public enum ChangeType
  {
    Insert,
    InsertWithXRef,
    Update
  }

  public enum NodeType
  {
    NotSet,
    Object,
    Reference
  }
}
