using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;

namespace Org.GS.Configuration
{
  public partial class frmConfigEdit : Form
  {
    private bool _fileLoaded = false;
    private AppConfig _appConfig;
    private string _originalImage = String.Empty;
    private string _currentImage;
    private bool _saveOnCloseHandled = false;

    public frmConfigEdit()
    {
      InitializeComponent();
      InitializeForm();
    }


    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "Open":
          Save();
          break;

        case "Save":
          Save();
          break;

        case "SaveAs":
          SaveAs();
          break;

        case "Close":
          if (CloseForm())
            this.Close();
          break;
      }
    }

    private void InitializeForm()
    {
      mnuFileOpen.Visible = false;
      mnuFileSaveAs.Visible = false;

      _appConfig = g.AppConfig;

      if (!g.AppConfig.IsLoaded)
      {
        txtMain.Visible = false;
        mnuFileSave.Enabled = false;
        mnuFileSaveAs.Enabled = false;
        mnuFileClose.Enabled = false;
        _fileLoaded = false;
        lblStatus.Text = "AppConfig file is not loaded.";
        this.Text = "AppConfig Editor - no file loaded";
      }
      else
      {
        _currentImage = _appConfig.GetCurrentImage();
        _originalImage = _currentImage;
        mnuFileSave.Enabled = false;
        mnuFileSaveAs.Enabled = true;
        mnuFileClose.Enabled = true;

        txtMain.Visible = true;
        _fileLoaded = true;
        txtMain.Text = _currentImage;
        txtMain.SelectionStart = 0;
        txtMain.SelectionLength = 0;
        lblStatus.Text = "AppConfig file loaded.";
        this.Text = "AppConfig Editor - (unchanged)";
      }

      _saveOnCloseHandled = false;
    }

    private void Open()
    {

      _saveOnCloseHandled = false;
    }

    private void Save()
    {
      if (!ValidateCurrentXml())
        return;

      SaveFile();
    }

    private bool ValidateCurrentXml()
    {
      try
      {
        string currentXml = txtMain.Text;
        XElement xml = XElement.Parse(currentXml);
        return true;
      }
      catch(Exception ex)
      {
        MessageBox.Show("The updated AppConfig data is not valid XML." + g.crlf2 + ex.Message, "XML Data Format Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }
    }

    private void SaveAs()
    {

    }

    private bool CloseForm()
    {
      _saveOnCloseHandled = true;

      _currentImage = txtMain.Text;
      if (_currentImage != _originalImage)
      {
        if (MessageBox.Show("The AppConfig file has been modified." + g.crlf2 + "Do you want to save your changes?", "Save changes?",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
          return false;


        SaveFile();
        return true; 
      }

      return true;
    }

    private void SaveFile()
    {
      string encryptedFilePath = g.AppConfig.FullPath;
      try
      {
        XElement unencryptedXml = XElement.Parse(txtMain.Text);
        string unencryptedFormattedXml = unencryptedXml.ToString();

        string unencryptedFilePath = encryptedFilePath.Replace(".xmlx", ".xml");
        if (File.Exists(unencryptedFilePath))
          File.WriteAllText(unencryptedFilePath, unencryptedFormattedXml);

        var encryptor = new Encryptor();
        string encryptedXml = encryptor.EncryptString(unencryptedFormattedXml);
        File.WriteAllText(encryptedFilePath, encryptedXml);
        g.AppConfig.LoadFromFile();
        _currentImage = g.AppConfig.GetCurrentImage();
        _originalImage = _currentImage;
        this.Text = "AppConfig Editor - (unchanged)";
        lblStatus.Text = "File is unchanged.";
        MessageBox.Show("AppConfig file successfully saved.", "AppConfig File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information); 
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to save the AppConfig file to the path'" + encryptedFilePath + "'." + g.crlf2 + 
                        "Exception Message: " + g.crlf2 + ex.Message, "Error Saving AppConfig File",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void txtMain_TextChanged(object sender, EventArgs e)
    {
      if (!_fileLoaded)
      {
        mnuFileClose.Enabled = false;
        mnuFileSave.Enabled = false;
        mnuFileSaveAs.Enabled = false;
      }
      else
      {
        _currentImage = txtMain.Text;
        if (_currentImage == _originalImage)
        {
          mnuFileSave.Enabled = false;
          this.Text = "AppConfig Editor - (unchanged)";
          lblStatus.Text = "File is unchanged.";
        }
        else
        {
          mnuFileSave.Enabled = true;
          this.Text = "AppConfig Editor - (changed)";
          lblStatus.Text = "File is changed.";

        }
      }
    }

    private void frmConfigEdit_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (_saveOnCloseHandled)
        return;

    }

  }
}
