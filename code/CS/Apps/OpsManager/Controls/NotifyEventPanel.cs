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
  public partial class NotifyEventPanel : BasePanel
  {
    private NotifyEvent NotifyEvent { get; set; }

    public NotifyEventPanel(PanelData panelData, ChangeType changeType)
    {
      InitializeComponent();
      base.InitializeBaseProperties(panelData, changeType);
      
      lblTreeNodePath.Text += panelData.TreeNodePath;

      if (base.ChangeType == ChangeType.Update)
      {
        using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
          this.NotifyEvent = notifyRepo.GetNotifyEvent(panelData.ObjectId.Value);

        txtName.Text = this.NotifyEvent.Name;
        chkIsActive.Checked = this.NotifyEvent.IsActive;
        txtDefaultSubject.Text = this.NotifyEvent.DefaultSubject;
      }
      else
        this.NotifyEvent = new NotifyEvent();
    }

    private void DirtyCheck(object sender, EventArgs e)
    {
      bool isDirty = false;

      if (txtName.Text.GetStringValueOrNull() != this.NotifyEvent.Name ||
          chkIsActive.Checked != this.NotifyEvent.IsActive ||
          txtDefaultSubject.Text.Trim() != this.NotifyEvent.DefaultSubject)
        isDirty = true;
      
      base.IsDirty = btnSave.Enabled = isDirty;
    }

    private void Save(object sender, EventArgs e)
    {
      try
      {
        if (txtName.Text.IsBlank())
        {
          MessageBox.Show("NotifyEvent Name cannot be blank.", "OpsManager - Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        if (txtDefaultSubject.Text.IsBlank())
        {
          MessageBox.Show("NotifyEvent DefaultSubject cannot be blank.", "OpsManager - Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        base.IsDirty = btnSave.Enabled = false;

        //Update TreeNode with new Name
        if (base.ChangeType == ChangeType.Update && this.NotifyEvent.Name != txtName.Text.Trim())
        {
          int lastBackslashIndex = lblTreeNodePath.Text.LastIndexOf('\\');
          lblTreeNodePath.Text = lblTreeNodePath.Text.Substring(0, lastBackslashIndex + 1) + txtName.Text.Trim();

          base.FireNotifyUpdate(new NotifyChangeResult(base.NotifyType, this.NotifyEvent.NotifyEventId, txtName.Text.Trim()));
        }

        this.NotifyEvent.Name = txtName.Text.Trim();
        this.NotifyEvent.IsActive = chkIsActive.Checked;
        this.NotifyEvent.DefaultSubject = txtDefaultSubject.Text.Trim();

        //Update NotifyEvent
        if (base.ChangeType == ChangeType.Update)
        {
          this.NotifyEvent.ModifiedBy = g.SystemInfo.DomainAndUser;
          this.NotifyEvent.ModifiedOn = DateTime.Now;

          using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
            notifyRepo.UpdateNotifyEvent(this.NotifyEvent);      
        }
        //Insert NotifyEvent
        else
        {
          this.NotifyEvent.NotifyConfigId = base.ParentId.Value;
          this.NotifyEvent.CreatedBy = g.SystemInfo.DomainAndUser;
          this.NotifyEvent.CreatedOn = DateTime.Now;

          using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
            this.NotifyEvent.NotifyEventId = notifyRepo.InsertNotifyEvent(this.NotifyEvent);

          base.FireNotifyInsert(new NotifyChangeResult(base.NotifyType, this.NotifyEvent.NotifyEventId, this.NotifyEvent.Name, this.NotifyEvent.NotifyConfigId));
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
        DialogResult result = MessageBox.Show("Are you sure you want to permanently delete NotifyEvent '" + this.NotifyEvent.Name + "'?",
                                              "OpsManager - Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result == DialogResult.Yes)
        {
          using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
            notifyRepo.DeleteNotifyEvent(this.NotifyEvent.NotifyEventId);

          base.FireNotifyDelete(new NotifyChangeResult(base.NotifyType, this.NotifyEvent.NotifyEventId, this.NotifyEvent.Name, this.NotifyEvent.NotifyConfigId));
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while deleting NotifyEvent '" + this.NotifyEvent.Name + "'" + g.crlf2 +
                        ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
