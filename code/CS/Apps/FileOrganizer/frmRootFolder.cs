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
  public partial class frmRootFolder : Form
  {
    private FsoRepository _fsoRepo;

    public string NewRootFolderPath;
    public string NewRootFolderName;

    public frmRootFolder(FsoRepository fsoRepo)
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
          if(txtRootFolderName.Text.Length < 1)
          {
            MessageBox.Show("You must choose a root folder before you Click the OK button.",
                            "Please choose a valid root folder.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
          if(txtRootFolderPath.Text.Length < 1)
          {
            MessageBox.Show("You must choose a root folder name before you Click the OK button.",
                            "Please choose a valid root folder name.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }

          InsertNewRootFolder(txtRootFolderName.Text.Trim(), txtRootFolderPath.Text.Trim());

          this.NewRootFolderName = txtRootFolderName.Text.Trim();
          this.NewRootFolderPath = txtRootFolderPath.Text.Trim();
          this.DialogResult = DialogResult.OK;
          break;
      }
    }

    private void InsertNewRootFolder(string folderName, string pathName)
    {
      try
      {
        _fsoRepo.InsertNewRootFolder(folderName, pathName);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to insert a new root folder with the name '" + folderName + "'.", ex);
      }
    }

    private void btnBrowseForRootFolder_Click(object sender,EventArgs e)
    {
      FolderBrowserDialog fd = new FolderBrowserDialog();
      if(fd.ShowDialog() == DialogResult.OK)
      {
        txtRootFolderPath.Text = fd.SelectedPath;
        return;
      }
    }

    private void InitializeForm()
    {
      this.NewRootFolderName = String.Empty;
      this.NewRootFolderPath = String.Empty;
      btnOK.Enabled = true;
      btnCancel.Enabled = true;
    }

    private void btnCancel_Click(object sender,EventArgs e)
    {
      this.Close();
    }
  }
}
