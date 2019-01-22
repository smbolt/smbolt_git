using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Org.GS;

namespace Org.GS.Configuration
{
  public partial class frmSecurityCache : Form
  {
    private string _user;
    private ProgramRoleSet _roleSet;
    private string _path;

    public frmSecurityCache(string user, ProgramRoleSet roleSet, string path)
    {
      InitializeComponent();

      _user = user;
      _roleSet = roleSet;
      _path = path;

      InitializeForm();
    }

    private void InitializeForm()
    {
      lblUserNameValue.Text = _user;
      LoadGroups();
      btnOK.Enabled = false;
    }


    private void LoadGroups()
    {
      lbGroups.Items.Clear();

      if (_roleSet == null)
        return;

      foreach (ProgramRole r in _roleSet.Values)
      {
        if (r.ClientRoleName != "NotUsed")
          lbGroups.Items.Add(r.ClientRoleName);
      }
    }

    private void lbGroups_SelectedValueChanged(object sender, EventArgs e)
    {
      if (lbGroups.CheckedIndices.Count == 0)
        btnOK.Enabled = false;
      else
        btnOK.Enabled = true;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Are you sure you do not want to establish the temporary security cache?" + g.crlf2 +
                          "This program is having trouble accessing Active Directory in order to establish security group membership for this user, " +
                          "without which this program may not be able to run." + g.crlf2 +
                          "Click 'Yes' to cancel or 'No' to return to the form and select security group membership for this user.", "Confirm Cancel",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
        return;
      }
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      List<string> groups = new List<string>();

      for (int i = 0; i < lbGroups.CheckedIndices.Count; i++)
      {
        int index = lbGroups.CheckedIndices[i];
        groups.Add(lbGroups.Items[index].ToString());
      }

      XElement securityCache = new XElement("SecurityCache");
      securityCache.Add(new XAttribute("Updated", DateTime.Now.ToString("yyyyMMddHHmmss")));

      XElement domainUser = new XElement("DomainUser");
      domainUser.Add(new XAttribute("Account", _user));

      foreach (string group in groups)
      {
        XElement groupElement = new XElement("Group");
        groupElement.Add(new XAttribute("Name", group));
        domainUser.Add(groupElement);
      }

      securityCache.Add(domainUser);

      string securityCacheString = securityCache.ToString();
      Encryptor encryptor = new Encryptor();
      string encryptedCache = encryptor.EncryptString(securityCacheString);

      string folderName = Path.GetDirectoryName(_path);
      if (!Directory.Exists(folderName))
        Directory.CreateDirectory(folderName);

      File.WriteAllText(_path, encryptedCache);

      this.DialogResult = DialogResult.OK;
      this.Close();
    }
  }
}
