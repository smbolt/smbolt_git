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
  public partial class NotifyPersonPanel : BasePanel
  {
    private NotifyPerson NotifyPerson { get; set; }
    private NotifyPersonGroup NotifyPersonGroup { get; set; } 

    public NotifyPersonPanel(PanelData panelData, ChangeType changeType)
    {
      InitializeComponent();
      base.InitializeBaseProperties(panelData, changeType);
      this.NotifyPerson = new NotifyPerson();
      this.NotifyPersonGroup = new NotifyPersonGroup();

      lblTreeNodePath.Text += panelData.TreeNodePath;

      if (base.ChangeType == ChangeType.Update)
      {
        using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
        {
          this.NotifyPerson = notifyRepo.GetNotifyPerson(panelData.ObjectId.Value);
          if (base.NodeType == NodeType.Reference)
            this.NotifyPersonGroup = notifyRepo.GetNotifyPersonGroup(panelData.XRefId.Value);
          else
          {
            chkIsActiveInGroup.Visible = false;
            lblDivider.Visible = false;
            btnSave.Location = new Point(40, btnSave.Location.Y - 40);
          }
        }

        txtName.Text = this.NotifyPerson.Name;
        txtEmailAddress.Text = this.NotifyPerson.EmailAddress;
        txtSmsNumber.Text = this.NotifyPerson.SmsNumber;
        chkIsActive.Checked = this.NotifyPerson.IsActive;
        chkEmailActive.Checked = this.NotifyPerson.IsEmailActive;
        chkSmsActive.Checked = this.NotifyPerson.IsSmsActive;
        chkIsActiveInGroup.Checked = this.NotifyPersonGroup.IsActive;
      }
    }

    private void DirtyCheck(object sender, EventArgs e)
    {
      bool isDirty = false;

      if (txtName.Text.GetStringValueOrNull() != this.NotifyPerson.Name ||
          txtEmailAddress.Text.GetStringValueOrNull() != this.NotifyPerson.EmailAddress ||
          txtSmsNumber.Text.GetStringValueOrNull() != this.NotifyPerson.SmsNumber ||
          chkIsActive.Checked != this.NotifyPerson.IsActive ||
          chkEmailActive.Checked != this.NotifyPerson.IsEmailActive ||
          chkSmsActive.Checked != this.NotifyPerson.IsSmsActive ||
          chkIsActiveInGroup.Checked != this.NotifyPersonGroup.IsActive)
        isDirty = true;

      base.IsDirty = btnSave.Enabled = isDirty;
    }

    private void Save(object sender, EventArgs e)
    {
      try
      {
        if (chkEmailActive.Checked && txtEmailAddress.Text.IsBlank())
        {
          MessageBox.Show("Email Address cannot be blank if Email is active.", "OpsManager - Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        if (chkSmsActive.Checked && txtSmsNumber.Text.IsBlank())
        {
          MessageBox.Show("SMS Number cannot be blank if SMS is active.", "OpsManager - Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        if (base.ChangeType == ChangeType.Update && this.NotifyPerson.Name != txtName.Text.Trim())
        {
          int lastBackslashIndex = lblTreeNodePath.Text.LastIndexOf('\\');
          lblTreeNodePath.Text = lblTreeNodePath.Text.Substring(0, lastBackslashIndex + 1) + txtName.Text.Trim();

          base.FireNotifyUpdate(new NotifyChangeResult(NotifyType.NotifyPerson, this.NotifyPerson.NotifyPersonId, txtName.Text.Trim()));
        }

        base.IsDirty = btnSave.Enabled = false;

        if (base.ChangeType == ChangeType.Update)
        {
          //Update NotifyPerson
          if (txtName.Text.GetStringValueOrNull() != this.NotifyPerson.Name ||
            txtEmailAddress.Text.GetStringValueOrNull() != this.NotifyPerson.EmailAddress ||
            txtSmsNumber.Text.GetStringValueOrNull() != this.NotifyPerson.SmsNumber ||
            chkIsActive.Checked != this.NotifyPerson.IsActive ||
            chkEmailActive.Checked != this.NotifyPerson.IsEmailActive ||
            chkSmsActive.Checked != this.NotifyPerson.IsSmsActive)
          {
            this.NotifyPerson.Name = txtName.Text.Trim();
            this.NotifyPerson.IsActive = chkIsActive.Checked;
            this.NotifyPerson.EmailAddress = txtEmailAddress.Text.GetStringValueOrNull();
            this.NotifyPerson.IsEmailActive = chkEmailActive.Checked;
            this.NotifyPerson.SmsNumber = txtSmsNumber.Text.GetStringValueOrNull();
            this.NotifyPerson.IsSmsActive = chkSmsActive.Checked;
            this.NotifyPerson.ModifiedBy = g.SystemInfo.DomainAndUser;
            this.NotifyPerson.ModifiedOn = DateTime.Now;

            using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
              notifyRepo.UpdateNotifyPerson(this.NotifyPerson);
          }

          //Update NotifyPersonGroup
          if (chkIsActiveInGroup.Checked != this.NotifyPersonGroup.IsActive)
          {
            this.NotifyPersonGroup.IsActive = chkIsActiveInGroup.Checked;
            this.NotifyPersonGroup.ModifiedBy = g.SystemInfo.DomainAndUser;
            this.NotifyPersonGroup.ModifiedOn = DateTime.Now;

            using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
              notifyRepo.UpdateNotifyPersonGroup(this.NotifyPersonGroup);
          }
        }
        //InsertNotifyPerson
        else
        {
          this.NotifyPerson.Name = txtName.Text.Trim();
          this.NotifyPerson.IsActive = chkIsActive.Checked;
          this.NotifyPerson.EmailAddress = txtEmailAddress.Text.GetStringValueOrNull();
          this.NotifyPerson.IsEmailActive = chkEmailActive.Checked;
          this.NotifyPerson.SmsNumber = txtSmsNumber.Text.GetStringValueOrNull();
          this.NotifyPerson.IsSmsActive = chkSmsActive.Checked;
          this.NotifyPerson.CreatedBy = g.SystemInfo.DomainAndUser;
          this.NotifyPerson.CreatedOn = DateTime.Now;

          using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
            this.NotifyPerson.NotifyPersonId = notifyRepo.InsertNotifyPerson(this.NotifyPerson);

          if (base.ChangeType == ChangeType.InsertWithXRef)
          {
            this.NotifyPersonGroup.NotifyGroupId = base.ParentId.Value;
            this.NotifyPersonGroup.NotifyPersonId = this.NotifyPerson.NotifyPersonId;
            this.NotifyPersonGroup.IsActive = chkIsActiveInGroup.Checked;
            this.NotifyPersonGroup.CreatedBy = g.SystemInfo.DomainAndUser;
            this.NotifyPersonGroup.CreatedOn = DateTime.Now;

            using (var notifyRepo = new NotifyRepository(base.ConfigDbSpec))
              this.NotifyPersonGroup.NotifyPersonGroupId = notifyRepo.InsertNotifyPersonGroup(this.NotifyPersonGroup);

            base.FireNotifyInsert(new NotifyChangeResult(base.NotifyType, this.NotifyPerson.NotifyPersonId, this.NotifyPerson.Name,
                                                         this.NotifyPersonGroup.NotifyGroupId, this.NotifyPersonGroup.NotifyPersonGroupId));
          }
          else
            base.FireNotifyInsert(new NotifyChangeResult(base.NotifyType, this.NotifyPerson.NotifyPersonId, this.NotifyPerson.Name));
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
              DialogResult result = MessageBox.Show("Are you sure you want to permanently delete NotifyPerson '" + this.NotifyPerson.Name + "'?",
                                                    "OpsManager - Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
              if (result == DialogResult.Yes)
              {
                notifyRepo.DeleteNotifyPerson(this.NotifyPerson.NotifyPersonId);
                base.FireNotifyDelete(new NotifyChangeResult(base.NotifyType, this.NotifyPerson.NotifyPersonId, this.NotifyPerson.Name));
              }
              break;

            case NodeType.Reference:
              notifyRepo.DeleteNotifyPersonGroup(this.NotifyPersonGroup.NotifyPersonGroupId);
              base.FireNotifyDelete(new NotifyChangeResult(base.NotifyType, this.NotifyPerson.NotifyPersonId, this.NotifyPerson.Name,
                                                              this.NotifyPersonGroup.NotifyGroupId, this.NotifyPersonGroup.NotifyPersonGroupId));
              break;
            default: return;
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while deleting NotifyPerson '" + this.NotifyPerson.Name + "'" + g.crlf2 +
                        ex.ToReport(), "OpsManager - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
