using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Org.GS;

namespace Org.TextUtility
{
  public partial class frmSaveAs : Form
  {
    private string _holdNewFileName;
    private bool _existingFilesExist;

    private string _originalFileName;
    private string _ext;
    private string _folderPath;

    public string NewFilePath { get; private set; }

    public frmSaveAs(string originalFileName, string folderPath)
    {
      InitializeComponent();

      _originalFileName = Path.GetFileName(originalFileName);
      _folderPath = folderPath;
      _ext = Path.GetExtension(_originalFileName);

      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      switch (sender.ActionTag())
      {
        case "Save":
          SaveFile();
          this.DialogResult = DialogResult.OK;
          this.Close();
          break;

        case "Cancel":
          this.DialogResult = DialogResult.Cancel;
          this.Close();
          break;
      }
    }

    private void SaveFile()
    {
      if (ckOverwriteExisting.Checked)
      {
        this.NewFilePath = _folderPath + @"\" + cboExistingFiles.Text;
      }
      else
      {
        this.NewFilePath = _folderPath + @"\" + txtNewFileName.Text;
        if (File.Exists(this.NewFilePath))
        {
          MessageBox.Show("The file '" + this.NewFilePath + "' already exists." + g.crlf2 +
                          "If your intention is to overwrite the file, please check the 'Overwrite existing file checkbox " +
                          "And select a file name from the existing files drop-down.", "File Already Exists",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning);
          ckOverwriteExisting.Select();
        }
      }
    }

    private void InitializeForm()
    {
      this.NewFilePath = String.Empty;

      lblNewFileName.Text += "  (" + _ext + " extension will be supplied)";

      _holdNewFileName = String.Empty;

      cboExistingFiles.LoadItems(Directory.GetFiles(_folderPath + @"\", "*" + _ext).ToList(), true);
      _existingFilesExist = cboExistingFiles.Items.Count > 1;
      ckOverwriteExisting.Enabled = _existingFilesExist;

      ckOverwriteExisting.Checked = false;    

      btnSave.Enabled = false;
    }

    private void ckOverwriteExisting_CheckedChanged(object sender, EventArgs e)
    {
      if (ckOverwriteExisting.Checked)
      {
        if (txtNewFileName.Text.IsNotBlank())
        {
          _holdNewFileName = txtNewFileName.Text;
          txtNewFileName.Text = String.Empty;
          txtNewFileName.Enabled = false;
          cboExistingFiles.Enabled = true;
          cboExistingFiles.SelectedIndex = -1;
          cboExistingFiles.Select();
        }
      }
      else
      {
        cboExistingFiles.Enabled = false;
        cboExistingFiles.SelectedIndex = -1;
        txtNewFileName.Enabled = true;
        if (_holdNewFileName.IsNotBlank())
          txtNewFileName.Text = _holdNewFileName;
      }
    }

    private void CheckReadyToSave()
    {
      if (ckOverwriteExisting.Checked)
      {
        btnSave.Enabled = cboExistingFiles.Text.IsNotBlank();
      }
      else
      {
        btnSave.Enabled = txtNewFileName.Text.IsNotBlank();
      }
    }

    private void cboExistingFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckReadyToSave();
    }

    private void txtNewFileName_TextChanged(object sender, EventArgs e)
    {
      CheckReadyToSave();
    }
  }
}
