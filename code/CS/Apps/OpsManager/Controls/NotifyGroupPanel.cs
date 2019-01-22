using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;
using Org.GS.Configuration;

namespace Org.OpsManager.Controls
{
  public partial class NotifyGroupPanel : BasePanel
  {
    private NotifyGroup NotifyGroup { get; set; }
    private NotifyEventGroup NotifyEventGroup { get; set; }

    public NotifyGroupPanel(PanelData panelData, ChangeType changeType)
    {
      InitializeComponent();
      base.InitializeBaseProperties(panelData, changeType);

      this.NotifyGroup = new NotifyGroup();
      this.NotifyEventGroup = new NotifyEventGroup();

      lblTreeNodePath.Text += panelData.TreeNodePath;

      if (base.ChangeType == ChangeType.Update)
      {
        using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
        {
          this.NotifyGroup = notifyRepo.GetNotifyGroup(panelData.ObjectId.Value);
          if (base.NodeType == NodeType.Reference)
            this.NotifyEventGroup = notifyRepo.GetNotifyEventGroup(panelData.XRefId.Value);
          else
          {
            chkIsActiveInEvent.Visible = false;
            lblDivider.Visible = false;
            btnSave.Location = new Point(40, btnSave.Location.Y - 40);
          }
        }

        txtName.Text = this.NotifyGroup.Name;
        chkIsActive.Checked = this.NotifyGroup.IsActive;
        chkIsActiveInEvent.Checked = this.NotifyEventGroup.IsActive;
      }
    }

    private void DirtyCheck(object sender, EventArgs e)
    {
      bool isDirty = false;

      if (txtName.Text.GetStringValueOrNull() != this.NotifyGroup.Name ||
          chkIsActive.Checked != this.NotifyGroup.IsActive ||
          chkIsActiveInEvent.Checked != this.NotifyEventGroup.IsActive)
        isDirty = true;

      base.IsDirty = btnSave.Enabled = isDirty;
    }

    private void Save(object sender, EventArgs e)
    {
      try
      {
        if (txtName.Text.IsBlank())
        {
          MessageBox.Show("NotifyGroup Name cannot be blank.", "OpsManager - Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        base.IsDirty = btnSave.Enabled = false;

        //Update TreeNode with new Name
        if (base.ChangeType == ChangeType.Update && this.NotifyGroup.Name != txtName.Text.Trim())
        {
          int lastBackslashIndex = lblTreeNodePath.Text.LastIndexOf('\\');
          lblTreeNodePath.Text = lblTreeNodePath.Text.Substring(0, lastBackslashIndex + 1) + txtName.Text.Trim();

          base.FireNotifyUpdate(new NotifyChangeResult(NotifyType.NotifyGroup, this.NotifyGroup.NotifyGroupId, txtName.Text.Trim()));
        }

        if (base.ChangeType == ChangeType.Update)
        {
          //Update NotifyGroup
          if (txtName.Text.GetStringValueOrNull() != this.NotifyGroup.Name ||
              chkIsActive.Checked != this.NotifyGroup.IsActive)
          {
            this.NotifyGroup.Name = txtName.Text.Trim();
            this.NotifyGroup.IsActive = chkIsActive.Checked;
            this.NotifyGroup.ModifiedBy = g.SystemInfo.DomainAndUser;
            this.NotifyGroup.ModifiedOn = DateTime.Now;

            using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
              notifyRepo.UpdateNotifyGroup(this.NotifyGroup);
          }

          //Update NotifyEventGroup
          if (chkIsActiveInEvent.Checked != this.NotifyEventGroup.IsActive)
          {
            this.NotifyEventGroup.IsActive = chkIsActiveInEvent.Checked;
            this.NotifyEventGroup.ModifiedBy = g.SystemInfo.DomainAndUser;
            this.NotifyEventGroup.ModifiedOn = DateTime.Now;

            using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
              notifyRepo.UpdateNotifyEventGroup(this.NotifyEventGroup);
          }
        }
        //Insert NotifyGroup
        else
        {
          this.NotifyGroup.Name = txtName.Text.Trim();
          this.NotifyGroup.IsActive = chkIsActive.Checked;
          this.NotifyGroup.CreatedBy = g.SystemInfo.DomainAndUser;
          this.NotifyGroup.CreatedOn = DateTime.Now;

          using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
            this.NotifyGroup.NotifyGroupId = notifyRepo.InsertNotifyGroup(this.NotifyGroup);

          if (base.ChangeType == ChangeType.InsertWithXRef)
          {
            this.NotifyEventGroup.NotifyEventID = base.ParentId.Value;
            this.NotifyEventGroup.NotifyGroupID = this.NotifyGroup.NotifyGroupId;
            this.NotifyEventGroup.IsActive = chkIsActiveInEvent.Checked;
            this.NotifyEventGroup.CreatedBy = g.SystemInfo.DomainAndUser;
            this.NotifyEventGroup.CreatedOn = DateTime.Now;

            using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
              this.NotifyEventGroup.NotifyEventGroupID = notifyRepo.InsertNotifyEventGroup(this.NotifyEventGroup);

            base.FireNotifyInsert(new NotifyChangeResult(base.NotifyType, this.NotifyGroup.NotifyGroupId, this.NotifyGroup.Name, this.NotifyEventGroup.NotifyEventID,
                                                         this.NotifyEventGroup.NotifyEventGroupID));
          }
          else
            base.FireNotifyInsert(new NotifyChangeResult(base.NotifyType, this.NotifyGroup.NotifyGroupId, this.NotifyGroup.Name));
        } 
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while saving changes to the Notifications database with Notify Path '" + lblTreeNodePath.Text + "'" + g.crlf2 +
                        ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    public void Delete()
    {
      try
      {
        using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
        {
          switch (base.NodeType)
          {
            case NodeType.Object:
              DialogResult result = MessageBox.Show("Are you sure you want to permanently delete NotifyGroup '" + this.NotifyGroup.Name + "'?",
                                                    "OpsManager - Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
              if (result == DialogResult.Yes)
              {
                notifyRepo.DeleteNotifyGroup(this.NotifyGroup.NotifyGroupId);
                base.FireNotifyDelete(new NotifyChangeResult(base.NotifyType, this.NotifyGroup.NotifyGroupId, this.NotifyGroup.Name));
              }
              break;

            case NodeType.Reference: 
              notifyRepo.DeleteNotifyEventGroup(this.NotifyEventGroup.NotifyEventGroupID); 
              base.FireNotifyDelete(new NotifyChangeResult(base.NotifyType, this.NotifyGroup.NotifyGroupId, this.NotifyGroup.Name, this.NotifyEventGroup.NotifyEventID,
                                                              this.NotifyEventGroup.NotifyEventGroupID));
              break;
            default: return;
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while deleting NotifyGroup '" + this.NotifyGroup.Name + "'" + g.crlf2 +
                        ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
