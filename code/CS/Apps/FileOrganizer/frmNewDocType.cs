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
  public partial class frmNewDocType : Form 
  {
    private FsoRepository _fsoRepo;

    public string NewDocumentType;
    
    public frmNewDocType(FsoRepository fsoRepo) 
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
          if (DocumentTypeExists(txtDocType.Text.Trim()))
          {
            MessageBox.Show("A document type already exists with the name '" + txtDocType.Text.Trim() + "'." + g.crlf2 +
                            "Please choose a different document type.", "Document Type already exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtDocType.SelectAll();
            txtDocType.Focus();
            return;
          }
          
          InsertNewDocType(txtDocType.Text.Trim());

          this.NewDocumentType = txtDocType.Text.Trim();
          this.DialogResult = DialogResult.OK;
            break;
        

        case "Cancel":
          this.DialogResult = DialogResult.Cancel;
          Close();
          break;
      }
    }

    private bool DocumentTypeExists(string docTypeName)
    {
      try
      {
        var docType = _fsoRepo.GetDocTypeByName(docTypeName);

          if(docType != null)
            return true;

        return false;
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred while attempting to determine if document type '" + docTypeName + "' already exists.", ex);
      }
    }

    private void InsertNewDocType(string docTypeName)
    {
      try
      {
        _fsoRepo.InsertNewDocType(docTypeName);
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred while attempting to insert a new document type with name '" + docTypeName + "'.", ex); 
      }
    }

    private void InitializeForm()
    {
      this.NewDocumentType = String.Empty;
      btnOK.Enabled = false;
      btnCancel.Enabled = true;
    }

    private void txtDocType_TextChanged(object sender,EventArgs e) 
    {
      btnOK.Enabled = txtDocType.Text.Length > 0;
    }

    private void btnCancel_Click(object sender,EventArgs e) 
    {
      this.Close();
    }
  }
}
