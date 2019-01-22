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
  public partial class frmNewProject : Form
  {
    private FsoRepository _fsoRepo;

    public string NewProjectName;

    public frmNewProject(FsoRepository fsoRepo)
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
          if (ProjectNameExists(txtProjectName.Text.Trim()))
          {
            MessageBox.Show("A project already exists with project name '" + txtProjectName.Text.Trim() + "'." + g.crlf2 +
                            "Please  choose a different project name.", "Project Name Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtProjectName.SelectAll();
            txtProjectName.Focus();
            return;
          }

          InsertNewProject(txtProjectName.Text.Trim());
          
          this.NewProjectName = txtProjectName.Text.Trim();
          this.DialogResult = DialogResult.OK;
          break;

        case "Cancel":
          this.DialogResult = DialogResult.Cancel;
          Close();
          break;
      }
    }

    private bool ProjectNameExists(string projectName)
    {
      try
      {
        var project = _fsoRepo.GetProjectByName(projectName);

        if (project != null)
          return true;

        return false;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to determine if a project exists for project name '" + projectName + "'.", ex); 
      }
    }

    private void InsertNewProject(string projectName)
    {
      try
      {
        _fsoRepo.InsertNewProject(projectName);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to insert a new project for project name '" + projectName + "'.", ex); 
      }
    }

    private void InitializeForm()
    {
      this.NewProjectName = String.Empty;
      btnOK.Enabled = false;
      btnCancel.Enabled = true;
    }

    private void txtProjectName_TextChanged(object sender, EventArgs e)
    {
      btnOK.Enabled = txtProjectName.Text.Length > 0; 
    }
  }
}
