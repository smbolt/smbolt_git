using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Adsdi.GS;

namespace Adsdi.EncryptedFileUtility
{
  public partial class frmMain : Form
  {
    private string _password = "admin";
    private List<string> _arguments = new List<string>();
    private bool _isFirstShowing = true;
    private string _openFileName = String.Empty;
    private string _originalText = String.Empty;
    private string _initialDirectory = @"C:\";
    private bool _isLoggedIn = false;
    private Encryptor encryptor;

    public frmMain()
    {
      InitializeComponent();
      InitializeApplication();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "Open":
          SelectFileUsingDirectoryBrowser();
          break;

        case "Save":
          if (ckObfuscatedFile.Checked)
            SaveObfuscatedFile();
          else
            SaveFile();
          break;

        case "SaveAs":
          if (ckObfuscatedFile.Checked)
            SaveObfuscatedFileAs();
          else
            SaveFileAs();
          break;

        case "Close":
          CloseFile();
          break;

        case "Exit":
          if (VerifyClose())
            this.Close();
          break;
      }
    }

    private void SelectFileUsingDirectoryBrowser()
    {
      dlgFileOpen.InitialDirectory = _initialDirectory;
      dlgFileOpen.Title = "Open Encrypted File";

      if (dlgFileOpen.ShowDialog() != DialogResult.OK)
        return;

      string fileName = dlgFileOpen.FileName;

      if (ckObfuscatedFile.Checked)
        OpenObfuscatedFile(fileName);
      else
        OpenFile(fileName);
    }

    private void OpenFile(string fileName)
    {
      if (IsFileUpdated())
      {
        switch (MessageBox.Show("Do you want to save the changes to the file being edited?",
                                "Encrypted File Utility", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
        {
          case DialogResult.Yes:
            SaveFile();
            break;

          case DialogResult.No:
            break;

          case DialogResult.Cancel:
            return;
        }
      }

      _initialDirectory = Path.GetDirectoryName(fileName);

      g.LocalConfig.AddCI("LastDirectoryUsed", _initialDirectory);
      g.LocalConfig.Save();

      string data = File.ReadAllText(fileName);
      string decryptedData = String.Empty;

      try
      {
        decryptedData = encryptor.DecryptString(data);
      }
      catch (Exception ex)
      {
        string message = ex.Message;

        if (message.ToLower().IndexOf("base-64 string") > -1)
        {
          MessageBox.Show("The selected file is either not an encrypted file or the encrypted data is corrupt and count not be decrypted.",
                          "Encryption File Utility Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        MessageBox.Show("An error has occurred attempting to decrypt the file.  See the message below." + g.crlf2 +
                        ex.Message, "Encryption File Utility Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      _openFileName = fileName;
      _originalText = decryptedData;
      txtMain.Text = _originalText;
      txtMain.Visible = true;
      SetTitle();
    }

    private void OpenObfuscatedFile(string fileName)
    {
      if (IsFileUpdated())
      {
        switch (MessageBox.Show("Do you want to save the changes to the file being edited?",
                                "Encrypted File Utility", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
        {
          case DialogResult.Yes:
            SaveObfuscatedFile();
            break;

          case DialogResult.No:
            break;

          case DialogResult.Cancel:
            return;
        }
      }

      _initialDirectory = Path.GetDirectoryName(fileName);

      g.LocalConfig.AddCI("LastDirectoryUsed", _initialDirectory);
      g.LocalConfig.Save();

      string data = File.ReadAllText(fileName);
      string plainTextData = String.Empty;

      try
      {
        plainTextData = TokenMaker.DecodeToken(data);
      }
      catch (Exception ex)
      {
        string message = ex.Message;

        MessageBox.Show("An error has occurred attempting to de-obfuscate the file.  See the message below." + g.crlf2 +
                        ex.Message, "Encryption File Utility Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      _openFileName = fileName;
      _originalText = plainTextData;
      txtMain.Text = _originalText;
      txtMain.Visible = true;
      SetTitle();
    }

    private void SaveFile()
    {
      string textToSave = txtMain.Text.Replace("\r\n", "\n");
      string encryptedText = encryptor.EncryptString(textToSave);
      File.WriteAllText(_openFileName, encryptedText);
      _originalText = textToSave;
      SetTitle();
    }

    private void SaveObfuscatedFile()
    {
      string textToSave = txtMain.Text.Replace("\r\n", "\n");
      string obfuscatedText = TokenMaker.GenerateToken(textToSave);
      File.WriteAllText(_openFileName, obfuscatedText);
      _originalText = textToSave;
      SetTitle();
    }

    private void SaveFileAs()
    {
      dlgFileSaveAs.InitialDirectory = _initialDirectory;
      dlgFileSaveAs.Title = "Save File As...";

      if (dlgFileSaveAs.ShowDialog() != DialogResult.OK)
        return;

      string saveFileName = dlgFileSaveAs.FileName;

      string unencryptedText = txtMain.Text.Replace("\r\n", "\n");
      string textToSave = String.Empty;

      if (saveFileName[saveFileName.Length - 1] == 'x')
        textToSave = encryptor.EncryptString(unencryptedText);
      else
        textToSave = unencryptedText;

      File.WriteAllText(saveFileName, textToSave);

      _openFileName = saveFileName;
      _originalText = textToSave;
      SetTitle();
    }

    private void SaveObfuscatedFileAs()
    {
      dlgFileSaveAs.InitialDirectory = _initialDirectory;
      dlgFileSaveAs.Title = "Save File As...";

      if (dlgFileSaveAs.ShowDialog() != DialogResult.OK)
        return;

      string saveFileName = dlgFileSaveAs.FileName;

      string plainText = txtMain.Text.Replace("\r\n", "\n");
      string textToSave = String.Empty;

      if (saveFileName[saveFileName.Length - 1] == 'x')
        textToSave = TokenMaker.GenerateToken(plainText);
      else
        textToSave = plainText;

      File.WriteAllText(saveFileName, textToSave);

      _openFileName = saveFileName;
      _originalText = textToSave;
      SetTitle();
    }

    private void CloseFile()
    {
      if (IsFileUpdated())
      {
        switch (MessageBox.Show("Do you want to save the changes to the file being edited?",
                                "Encrypted File Utility", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
        {
          case DialogResult.Yes:
            if (ckObfuscatedFile.Checked)
              SaveObfuscatedFile();
            else
              SaveFile();
            break;

          case DialogResult.No:
            break;

          case DialogResult.Cancel:
            return;
        }
      }

      txtMain.Text = String.Empty;
      txtMain.Visible = false;
      _openFileName = String.Empty;
      SetTitle();
    }

    private bool VerifyClose()
    {
      if (IsFileUpdated())
      {
        switch (MessageBox.Show("Do you want to save the changes to the file being edited?",
                                "Encrypted File Utility", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
        {
          case DialogResult.Yes:
            SaveFile();
            break;

          case DialogResult.No:
            break;

          case DialogResult.Cancel:
            return false;
        }
      }

      return true;
    }

    private void InitializeApplication()
    {
      encryptor = new Encryptor();
      _arguments = Environment.GetCommandLineArgs().ToList();

      if (g.LocalConfig.ContainsKey("LastDirectoryUsed"))
        _initialDirectory = g.LocalConfig.GetCI("LastDirectoryUsed");

      txtMain.Visible = false;
      SetTitle();
      lblStatus.Text = String.Empty;
    }


    private void SetTitle()
    {
      if (_openFileName.Trim().Length > 0)
      {
        if (IsFileUpdated())
        {
          this.Text = "Encrypted File Utility [" + _openFileName.Trim() + "] (changed)";
          lblStatus.Text = "File is updated";
        }
        else
        {
          this.Text = "Encrypted File Utility [" + _openFileName.Trim() + "]";
          lblStatus.Text = "File is not updated";
        }
      }
      else
      {
        this.Text = "Encrypted File Utility";
        lblStatus.Text = String.Empty;
      }
    }

    private bool IsFileUpdated()
    {
      if (_openFileName.Trim().Length == 0)
        return false;

      if (txtMain.Text == _originalText.Replace("\r\n", "\n"))
        return false;

      return true;
    }

    private void frmMain_Shown(object sender, EventArgs e)
    {
      if (!_isFirstShowing)
        return;

      frmPassword fPassword = new frmPassword(_password);
      if (fPassword.ShowDialog() != DialogResult.OK)
      {
        MessageBox.Show("The program will close.", "Encrypted File Utility", MessageBoxButtons.OK, MessageBoxIcon.Information);
        this.Close();
      }
      else
      {
        _isLoggedIn = true;
      }
    }

    private void txtMain_TextChanged(object sender, EventArgs e)
    {
      SetTitle();
    }

    private void mnuFile_DropDownOpening(object sender, EventArgs e)
    {
      if (_openFileName.Trim().Length > 0)
      {
        mnuFileClose.Enabled = true;
        if (IsFileUpdated())
        {
          mnuFileSave.Enabled = true;
          mnuFileSaveAs.Enabled = true;
        }
        else
        {
          mnuFileSave.Enabled = false;
          mnuFileSaveAs.Enabled = true;
        }
      }
      else
      {
        mnuFileClose.Enabled = false;
        mnuFileSave.Enabled = false;
        mnuFileSaveAs.Enabled = false;
      }
    }

    private void frmMain_DragDrop(object sender, DragEventArgs e)
    {
      if (!_isLoggedIn)
        return;

      string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
      if (filePaths == null)
        return;

      if (filePaths.Count() > 1 || filePaths.Count() == 0)
        return;

      string file = filePaths[0];

      if (!file.EndsWith("x"))
      {
        MessageBox.Show("Cannot open an unencrypted file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      OpenFile(file);
    }

    private void frmMain_DragEnter(object sender, DragEventArgs e)
    {
      if (!_isLoggedIn)
        return;

      if (e.Data.GetDataPresent(DataFormats.FileDrop))
      {
        e.Effect = DragDropEffects.Copy;
      }
      else
      {
        e.Effect = DragDropEffects.None;
      }
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
      this.AllowDrop = true;
    }
  }
}


