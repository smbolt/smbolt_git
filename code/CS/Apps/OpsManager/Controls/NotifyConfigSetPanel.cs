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
  public partial class NotifyConfigSetPanel : BasePanel
  {
    private NotifyConfigSet NotifyConfigSet {
      get;
      set;
    }

    public NotifyConfigSetPanel(PanelData panelData, ChangeType changeType)
    {
      InitializeComponent();
      base.InitializeBaseProperties(panelData, changeType);

      lblTreeNodePath.Text += panelData.TreeNodePath;

      if (base.ChangeType == ChangeType.Update)
      {
        using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
          this.NotifyConfigSet = notifyRepo.GetNotifyConfigSet(panelData.ObjectId.Value);

        txtName.Text = this.NotifyConfigSet.Name;
        chkIsActive.Checked = this.NotifyConfigSet.IsActive;
      }
      else
        this.NotifyConfigSet = new NotifyConfigSet();
    }

    private void DirtyCheck(object sender, EventArgs e)
    {
      bool isDirty = false;

      if (txtName.Text.Trim() != this.NotifyConfigSet.Name ||
          chkIsActive.Checked != this.NotifyConfigSet.IsActive)
        isDirty = true;

      base.IsDirty = btnSave.Enabled = isDirty;
    }

    private void Save(object sender, EventArgs e)
    {
      try
      {
        if (txtName.Text.IsBlank())
        {
          MessageBox.Show("NotifyConfigSet Name cannot be blank.", "OpsManager - Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        base.IsDirty = btnSave.Enabled = false;

        //Update TreeNode with new Name
        if (base.ChangeType == ChangeType.Update && this.NotifyConfigSet.Name != txtName.Text.Trim())
        {
          int lastBackslashIndex = lblTreeNodePath.Text.LastIndexOf('\\');
          lblTreeNodePath.Text = lblTreeNodePath.Text.Substring(0, lastBackslashIndex + 1) + txtName.Text.Trim();

          base.FireNotifyUpdate(new NotifyChangeResult(base.NotifyType, this.NotifyConfigSet.NotifyConfigSetId, txtName.Text.Trim()));
        }

        this.NotifyConfigSet.Name = txtName.Text.Trim();
        this.NotifyConfigSet.IsActive = chkIsActive.Checked;

        //Update NotifyConfigSet
        if (base.ChangeType == ChangeType.Update)
        {
          this.NotifyConfigSet.ModifiedBy = g.SystemInfo.DomainAndUser;
          this.NotifyConfigSet.ModifiedOn = DateTime.Now;

          using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
            notifyRepo.UpdateNotifyConfigSet(this.NotifyConfigSet);
        }
        //Insert NotifyConfigSet
        else
        {
          this.NotifyConfigSet.CreatedBy = g.SystemInfo.DomainAndUser;
          this.NotifyConfigSet.CreatedOn = DateTime.Now;

          using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
            this.NotifyConfigSet.NotifyConfigSetId = notifyRepo.InsertNotifyConfigSet(this.NotifyConfigSet);

          base.FireNotifyInsert(new NotifyChangeResult(base.NotifyType, this.NotifyConfigSet.NotifyConfigSetId, this.NotifyConfigSet.Name));
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
        DialogResult result = MessageBox.Show("Are you sure you want to permanently delete NotifyConfigSet '" + this.NotifyConfigSet.Name + "'?",
                                              "OpsManager - Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result == DialogResult.Yes)
        {
          using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
            notifyRepo.DeleteNotifyConfigSet(this.NotifyConfigSet.NotifyConfigSetId);

          base.FireNotifyDelete(new NotifyChangeResult(base.NotifyType, this.NotifyConfigSet.NotifyConfigSetId, this.NotifyConfigSet.Name));
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while deleting NotifyConfigSet '" + this.NotifyConfigSet.Name + "'" + g.crlf2 +
                        ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
