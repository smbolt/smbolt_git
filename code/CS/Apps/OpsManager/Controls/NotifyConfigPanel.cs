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
  public partial class NotifyConfigPanel : BasePanel
  {
    private NotifyConfig NotifyConfig { get; set; }

    public NotifyConfigPanel(PanelData panelData, ChangeType changeType)
    {
      InitializeComponent();
      base.InitializeBaseProperties(panelData, changeType);

      this.NotifyConfig = new NotifyConfig();

      lblTreeNodePath.Text += panelData.TreeNodePath;

      if (base.ChangeType == ChangeType.Update)
      {
        using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
          this.NotifyConfig = notifyRepo.GetNotifyConfig(panelData.ObjectId.Value);
  
        txtName.Text = this.NotifyConfig.Name;
        txtSupportEmail.Text = this.NotifyConfig.SupportEmail;
        txtSupportPhone.Text = this.NotifyConfig.SupportPhone;
        chkIsActive.Checked = this.NotifyConfig.IsActive;
        chkSendEmail.Checked = this.NotifyConfig.SendEmail;
        chkSendSms.Checked = this.NotifyConfig.SendSms;
      }
    }

    private void DirtyCheck(object sender, EventArgs e)
    {
      bool isDirty = false;

      if (txtName.Text.Trim() != this.NotifyConfig.Name ||
          txtSupportEmail.Text.GetStringValueOrNull() != this.NotifyConfig.SupportEmail ||
          txtSupportPhone.Text.GetStringValueOrNull() != this.NotifyConfig.SupportPhone ||
          chkIsActive.Checked != this.NotifyConfig.IsActive ||
          chkSendEmail.Checked != this.NotifyConfig.SendEmail ||
          chkSendSms.Checked != this.NotifyConfig.SendSms)
        isDirty = true;

      base.IsDirty = btnSave.Enabled = isDirty;
    }

    private void Save(object sender, EventArgs e)
    {
      try
      {
        if (txtName.Text.IsBlank())
        {
          MessageBox.Show("NotifyConfig Name cannot be blank.", "OpsManager - Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        if (chkSendEmail.Checked && txtSupportEmail.Text.IsBlank())
        {
          MessageBox.Show("Support Email cannot be blank if Send Email is checked.", "OpsManager - Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        if (chkSendSms.Checked && txtSupportPhone.Text.IsBlank())
        {
          MessageBox.Show("Support Phone cannot be blank if Send SMS is checked.", "OpsManager - Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        base.IsDirty = btnSave.Enabled = false;

        //Update TreeNode with new Name
        if (base.ChangeType == ChangeType.Update && this.NotifyConfig.Name != txtName.Text.Trim())
        {
          int lastBackslashIndex = lblTreeNodePath.Text.LastIndexOf('\\');
          lblTreeNodePath.Text = lblTreeNodePath.Text.Substring(0, lastBackslashIndex + 1) + txtName.Text.Trim();

          base.FireNotifyUpdate(new NotifyChangeResult(base.NotifyType, this.NotifyConfig.NotifyConfigId, txtName.Text.Trim()));
        }

        this.NotifyConfig.Name = txtName.Text.Trim();
        this.NotifyConfig.SupportEmail = txtSupportEmail.Text.GetStringValueOrNull();
        this.NotifyConfig.SupportPhone = txtSupportPhone.Text.GetStringValueOrNull();
        this.NotifyConfig.IsActive = chkIsActive.Checked;
        this.NotifyConfig.SendEmail = chkSendEmail.Checked;
        this.NotifyConfig.SendSms = chkSendSms.Checked;

        //Update NotifyConfig
        if (base.ChangeType == ChangeType.Update)
        {
          this.NotifyConfig.ModifiedBy = g.SystemInfo.DomainAndUser;
          this.NotifyConfig.ModifiedOn = DateTime.Now;

          using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
            notifyRepo.UpdateNotifyConfig(this.NotifyConfig);  
        }
        //Insert NotifyConfig
        else
        {
          this.NotifyConfig.CreatedBy = g.SystemInfo.DomainAndUser;
          this.NotifyConfig.CreatedOn = DateTime.Now;

          using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
            this.NotifyConfig.NotifyConfigId = notifyRepo.InsertNotifyConfig(this.NotifyConfig);

          if (base.ChangeType == ChangeType.InsertWithXRef)
          {
            using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
              notifyRepo.InsertNotifyConfigXRef(base.ParentId.Value, this.NotifyConfig.NotifyConfigId);

            base.FireNotifyInsert(new NotifyChangeResult(base.NotifyType, this.NotifyConfig.NotifyConfigId, this.NotifyConfig.Name, base.ParentId.Value));
          }
          else
            base.FireNotifyInsert(new NotifyChangeResult(base.NotifyType, this.NotifyConfig.NotifyConfigId, this.NotifyConfig.Name));
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while saving NotifyConfig changes to the Notifications database with Notify Path '" + lblTreeNodePath.Text + "'" + g.crlf2 +
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
              DialogResult result = MessageBox.Show("Are you sure you want to permanently delete NotifyConfig '" + this.NotifyConfig.Name + "'?",
                                                    "OpsManager - Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
              if (result == DialogResult.Yes)
              {
                notifyRepo.DeleteNotifyConfig(this.NotifyConfig.NotifyConfigId);
                base.FireNotifyDelete(new NotifyChangeResult(base.NotifyType, this.NotifyConfig.NotifyConfigId, this.NotifyConfig.Name));
              }
              break;

            case NodeType.Reference:
              notifyRepo.DeleteNotifyConfigXRef(base.ParentId.Value, this.NotifyConfig.NotifyConfigId);
              base.FireNotifyDelete(new NotifyChangeResult(base.NotifyType, this.NotifyConfig.NotifyConfigId, this.NotifyConfig.Name, base.ParentId.Value));
              break;

            default: return;
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while deleting NotifyConfig '" + this.NotifyConfig.Name + "'" + g.crlf2 +
                        ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
