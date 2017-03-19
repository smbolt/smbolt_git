using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.FSO;
using Org.GS;
using Org.GS.Configuration;

namespace Org.FileOrganizer 
{
  public partial class frmNewTagType : Form 
  {
    private FsoRepository _fsoRepo;

    public string NewTagType;

    public frmNewTagType(FsoRepository fsoRepo) 
    {
      InitializeComponent();

      _fsoRepo = fsoRepo;
      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "OK":
          if(TagTypeExists(txtTagType.Text.Trim()))
          {
            MessageBox.Show("A Tag Type already exists with the name '" + txtTagType.Text.Trim() + "'." + g.crlf2 +
                            "Please  choose a different tag name.", "Tag Type Name Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtTagType.SelectAll();
            txtTagType.Focus();
          }

          InsertNewTagType(txtTagType.Text.Trim());

          this.NewTagType = txtTagType.Text.Trim();
          this.DialogResult = DialogResult.OK;
          break;

        case "Cancel":
          this.DialogResult = DialogResult.Cancel;
          Close();
          break;
      }
    }
      
    private bool TagTypeExists(string tagTypeName)
    {
      try 
      {
        var tagType = _fsoRepo.GetTagTypeByName(tagTypeName);

        if(tagType != null)

          return true;

        return false;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to determine if a tag type already exists.", ex);
      }
    }

    private void InsertNewTagType(string tagTypeName)
    {
      try
      {
        _fsoRepo.InsertNewTagType(tagTypeName);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to insert a new tag type with name '" + tagTypeName + "'.", ex);
      }
    }

    private void InitializeForm()
    {
      this.NewTagType = String.Empty;
      btnOK.Enabled = false;
      btnCancel.Enabled = true;
    }

    private void txtTagType_TextChanged(object sender,EventArgs e) 
    {
      btnOK.Enabled = txtTagType.Text.Length > 0;
    }
  }
}
