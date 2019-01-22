using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using Org.EDI;
using Org.GS.Configuration;
using Org.GS;

namespace Org.EdiWorkbench
{
  public partial class frmMain : Form
  {
    private string _userName;
    private string _exePath;
    private string _xsltPath;
    private string _ediWorkRoot;
    private string _fileSystemPath;
    private string _ediFolder;
    private string _ediFmtFolder;
    private string _busXml_A_Folder;
    private string _ediXml_A_Folder;
    private string _busXml_B_Folder;
    private string _ediXml_B_Folder;
    private string _xsltFolder;
    private string _xsltSaveFolder;
    private bool _useDisplayForm;
    private AppFocus _appFocus;
    private string _docNbr;
    private string _saveToEdiXslt;
    private string _currentToEdiXslt;
    private string _lastMessage;

    private frmDisplay fDisplay;
    private bool _isStartingUp;

    private System.Timers.Timer _messageTimer;

    private string crlf;
    private string crlf2;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = sender.ActionTag();

      switch(action)
      {
        case "LoadEdi":
          LoadEdi();
          break;

        case "TransformToEdiXml_B":
          TransformToEdiXml_B();
          break;

        case "ConvertEdiToFormattedEdi":
          ConvertEdiToFormattedEdi();
          break;

        case "ParseRawEdiToEdiXml":
          ParseRawEdiToEdiXml();
          break;

        case "TransformToBusXml_A":
          TransformToBusXml_A();
          break;

        case "EdiFileCboChange":
          EdiFileCboChange();
          break;

        case "BusXmlCboChange":
          BusXmlCboChange();
          break;

        case "FocusChange":
          FocusChange();
          break;

        case "ToEdiXmlXsltChanged":
          ToEdiXmlXsltChanged();
          break;

        case "SaveToEdiXslt":
          SaveToEdiXslt();
          break;

        case "ReloadEdiXslt":
          ReloadEdiXslt();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void LoadEdi()
    {
      string ediPath = GetEdiFolderPath() + @"\" + cboFileToProcess.Text + ".txt";
      string ediFile = File.ReadAllText(ediPath);
      rtxtRawEdi.Text = ediFile;

      rtxtRawEdi.SelectionStart = 0;
      rtxtRawEdi.SelectionLength = 0;
      tabMain.SelectedTab = tabPageRawEdi;

      string fileName = Path.GetFileName(ediPath);
      DisplayMessage("File loaded: " + fileName);
    }

    private void ConvertEdiToFormattedEdi()
    {
      string ediPath = GetEdiFolderPath() + @"\" + cboFileToProcess.Text + ".txt";
      string ediFmtPath = GetEdiFmtFolderPath() + @"\" + cboFileToProcess.Text + "_Fmt.txt";

      string formattedEdi = EdiHelper.TransformEdiToFormattedEdi(ediPath);
      File.WriteAllText(ediFmtPath, formattedEdi);

      rtxtFmtEdi.Text = formattedEdi;
      rtxtFmtEdi.SelectionStart = 0;
      rtxtFmtEdi.SelectionLength = 0;
      tabMain.SelectedTab = tabPageFmtEdi;

      string fileName = Path.GetFileName(ediFmtPath);
      DisplayMessage("File written: " + fileName);
    }

    private void ParseRawEdiToEdiXml()
    {
      string ediPath = GetEdiFolderPath() + @"\" + cboFileToProcess.Text + ".txt";
      string ediXmlPath_A = GetEdiXmlAFolderPath() + @"\" + cboFileToProcess.Text + "_EdiA.xml";

      string ediXml_A = EdiHelper.ParseRawEdiToEdiXml(ediPath);
      File.WriteAllText(ediXmlPath_A, ediXml_A);

      rtxtEdiXml_A.Text = ediXml_A;
      rtxtEdiXml_A.SelectionStart = 0;
      rtxtEdiXml_A.SelectionLength = 0;
      tabMain.SelectedTab = tabPageEdiXml_A;

      string fileName = Path.GetFileName(ediXmlPath_A);
      DisplayMessage("File written: " + fileName);
    }

    private void LoadEdiToXsltToEdi()
    {
      string ediPath = GetEdiFolderPath() + @"\" + cboFileToProcess.Text + ".txt";
      string ediFile = File.ReadAllText(ediPath);

      rtxtToEdiXslt.Text = ediFile;

      rtxtToEdiXslt.SelectionStart = 0;
      rtxtToEdiXslt.SelectionLength = 0;
      tabMain.SelectedTab = tabPageXsltToEdiXml;
    }

    private void TransformToBusXml_A()
    {
      string ediPath = GetEdiFolderPath() + @"\" + cboFileToProcess.Text + ".txt";
      string busXmlPath = GetBusXmlAFolderPath() + @"\" + cboFileToProcess.Text + "_BusA.xml";

      string busXml = EdiHelper.TransformEdiToOopBus(ediPath);
      File.WriteAllText(busXmlPath, busXml);

      rtxtBusXml_A.Text = busXml;
      rtxtBusXml_A.SelectionStart = 0;
      rtxtBusXml_A.SelectionLength = 0;
      tabMain.SelectedTab = tabPageBusXml_A;

      string fileName = Path.GetFileName(busXmlPath);
      DisplayMessage("File written: " + fileName);
    }

    private void TransformToEdiXml_B()
    {
      string toEdiXslt = GetXsltFolderPath() + @"\ToEdiXml_" + _docNbr + ".xslt";
      string busXmlPath_A = GetBusXmlAFolderPath() + @"\" + cboFileToProcess.Text + "_BusA.xml";

      if (!File.Exists(busXmlPath_A))
      {
        MessageBox.Show("File '" + busXmlPath_A + "' does not exist." + crlf2 +
                        "Transform the input EDI file to Bus Xml to create the file.", "EdiWorkbook - Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      string ediXmlPath_B = GetEdiXmlBFolderPath() + @"\" + cboFileToProcess.Text + "_EdiB.xml";

      string ediXml_B = String.Empty;

      try
      {
        ediXml_B = EdiHelper.TransformBusXmlToEdiXml(busXmlPath_A, toEdiXslt);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToReport(),
                        "EdiWorkbook - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      File.WriteAllText(ediXmlPath_B, ediXml_B);

      rtxtBusXml_B.Text = ediXml_B;
      rtxtBusXml_B.SelectionStart = 0;
      rtxtBusXml_B.SelectionLength = 0;

      if (fDisplay.Visible)
      {
        fDisplay.SetText(ediXml_B);
      }
      else
      {
        tabMain.SelectedTab = tabPageBusXml_B;
      }


      string fileName = Path.GetFileName(ediXmlPath_B);
      DisplayMessage("File written: " + fileName);
    }

    private string GetEdiFolderPath()
    {
      return ResolveVariables(_ediFolder);
    }

    private string GetEdiFmtFolderPath()
    {
      return ResolveVariables(_ediFmtFolder);
    }

    private string GetBusXmlAFolderPath()
    {
      return ResolveVariables(_busXml_A_Folder);
    }

    private string GetBusXmlBFolderPath()
    {
      return ResolveVariables(_busXml_B_Folder);
    }

    private string GetEdiXmlAFolderPath()
    {
      return ResolveVariables(_ediXml_A_Folder);
    }

    private string GetEdiXmlBFolderPath()
    {
      return ResolveVariables(_ediXml_B_Folder);
    }

    private string GetXsltFolderPath()
    {
      return ResolveVariables(_xsltFolder);
    }

    private string GetXsltSaveFolderPath()
    {
      return ResolveVariables(_xsltSaveFolder);
    }

    private string ResolveVariables(string value)
    {
      if (value.CountOfChar('$') < 2)
        return value;

      if (value.Contains("$User$"))
        value = value.Replace("$User$", _userName);

      if (value.Contains("$EdiWorkRoot$"))
        value = value.Replace("$EdiWorkRoot$", _ediWorkRoot);

      return value;
    }

    private void InitializeForm()
    {
      try
      {
        new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occured during the initialization of the application object (a)." + g.crlf2 + ex.ToReport(),
                        "EdiWorkbook - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }


      try
      {
        this.SetInitialSizeAndLocation();

        _isStartingUp = true;
        lblEdiXsltModified.Text = String.Empty;
        btnSaveToEdiXslt.Enabled = false;

        _xsltPath = g.CI("XsltFolder");
        _ediWorkRoot = g.CI("EdiWorkRoot");
        _fileSystemPath = g.CI("FileSystemPath");
        _ediFolder = g.CI("EdiFolder");
        _ediFmtFolder = g.CI("EdiFmtFolder");
        _busXml_A_Folder = g.CI("BusXml_A_Folder");
        _ediXml_A_Folder = g.CI("ediXml_A_Folder");
        _busXml_B_Folder = g.CI("BusXml_B_Folder");
        _ediXml_B_Folder = g.CI("ediXml_B_Folder");
        _xsltFolder = g.CI("XsltFolder");
        _xsltSaveFolder = g.CI("XsltSaveFolder");

        _appFocus = AppFocus.EdiInput;
        if (ConfigurationManager.AppSettings.AllKeys.Contains("Focus"))
          _appFocus = g.ToEnum<AppFocus>(g.CI("Focus"), AppFocus.EdiInput);

        for (int i = 0; i < cboFocus.Items.Count; i++)
        {
          if (cboFocus.Items[i].ToString() == _appFocus.ToString())
          { cboFocus.SelectedIndex = i;
            break;

          }
        }

        _useDisplayForm = false;
        if (ConfigurationManager.AppSettings.AllKeys.Contains("UseDisplayForm"))
          _useDisplayForm = Boolean.Parse(ConfigurationManager.AppSettings["UseDisplayForm"]);



        fDisplay = new frmDisplay();

        LoadEdiFilesToComboBox();
        LoadBusXmlAFilesToComboBox();

        DisplayMessage("Xslt files loaded");
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occured during application start up." + g.crlf2 + ex.ToReport(),
                        "EdiWorkbook - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadEdiFilesToComboBox()
    {
      cboFileToProcess.Items.Clear();

      string ediFolder = GetEdiFolderPath();
      List<string> ediFilePaths = Directory.GetFiles(ediFolder).ToList();

      foreach (string ediFilePath in ediFilePaths)
      {
        cboFileToProcess.Items.Add(Path.GetFileNameWithoutExtension(ediFilePath));
      }

      if (cboFileToProcess.Items.Count > 0)
      {
        cboFileToProcess.SelectedIndex = 0;
        EdiFileCboChange();
      }
    }

    private void LoadBusXmlAFilesToComboBox()
    {
      cboBusXmlAFiles.Items.Clear();

      string busXmlAFolder = GetBusXmlAFolderPath();
      List<string> busXmlAFiles = Directory.GetFiles(busXmlAFolder).ToList();

      foreach (string busXmlAFile in busXmlAFiles)
      {
        cboBusXmlAFiles.Items.Add(Path.GetFileNameWithoutExtension(busXmlAFile));
      }

      if (cboBusXmlAFiles.Items.Count > 0)
      {
        cboBusXmlAFiles.SelectedIndex = 0;
        BusXmlCboChange();
      }
    }

    private void EdiFileCboChange()
    {
      _docNbr = "???";

      if (cboFileToProcess.Text.Trim().Length > 3)
        _docNbr = cboFileToProcess.Text.Trim().Substring(0, 3);

      lblStatus.Text = "Selected document: " + _docNbr;

      rtxtToBusXslt.Text = String.Empty;
      rtxtToEdiXslt.Text = String.Empty;

      if (!_isStartingUp)
        LoadXslt(_docNbr);
    }

    private void BusXmlCboChange()
    {
      _docNbr = "???";

      if (cboBusXmlAFiles.Text.Trim().Length > 3)
        _docNbr = cboBusXmlAFiles.Text.Trim().Substring(0, 3);

      lblStatus.Text = "Selected document: " + _docNbr;

      rtxtToBusXslt.Text = String.Empty;
      rtxtToEdiXslt.Text = String.Empty;

      if (!_isStartingUp)
        LoadXslt(_docNbr);
    }

    private void FocusChange()
    {
      if(_isStartingUp)
        return;

      _appFocus = (AppFocus)Enum.Parse(typeof(AppFocus), cboFocus.Text);
    }

    private void LoadXslt(string docNbr)
    {
      if (_isStartingUp)
        return;

      if (docNbr.IsNotNumeric())
        return;

      string toBusXmlFile = GetXsltFolderPath() + @"\ToBusXml_" + docNbr + ".xslt";
      string toEdiXmlFile = GetXsltFolderPath() + @"\ToEdiXml_" + docNbr + ".xslt";

      if (File.Exists(toBusXmlFile))
        rtxtToBusXslt.Text = File.ReadAllText(toBusXmlFile);

      if (File.Exists(toEdiXmlFile))
        rtxtToEdiXslt.Text = File.ReadAllText(toEdiXmlFile);

      _currentToEdiXslt = rtxtToEdiXslt.Text.Replace("\r\n", "\n");
      _saveToEdiXslt = _currentToEdiXslt;
    }

    private void ToEdiXmlXsltChanged()
    {
      btnSaveToEdiXslt.Enabled = false;

      if (_saveToEdiXslt == null)
        return;

      _currentToEdiXslt = rtxtToEdiXslt.Text.Replace("\r\n", "\n");

      if (_saveToEdiXslt == _currentToEdiXslt)
      {
        lblEdiXsltModified.Text = String.Empty;
        btnSaveToEdiXslt.Enabled = false;
      }
      else
      {
        lblEdiXsltModified.Text = "XSLT MODIFIED";
        btnSaveToEdiXslt.Enabled = true;
      }
    }

    private void SaveToEdiXslt()
    {
      string timestamp = DateTime.Now.ToString("yyyyMMdd-HHmmssfff");
      string toEdiXslt = GetXsltFolderPath() + @"\ToEdiXml_" + _docNbr + ".xslt";
      string toEdiXsltSave =  GetXsltSaveFolderPath() + @"\ToEdiXml_"+ timestamp + "_" + _docNbr + ".xslt";

      File.Move(toEdiXslt, toEdiXsltSave);
      File.WriteAllText(toEdiXslt, rtxtToEdiXslt.Text);

      _currentToEdiXslt = rtxtToEdiXslt.Text.Replace("\r\n", "\n");
      _saveToEdiXslt = _currentToEdiXslt;
      ToEdiXmlXsltChanged();
      DisplayMessage("To EDI Xslt file saved");
    }

    private void ReloadEdiXslt()
    {
      string toEdiXmlFile = GetXsltFolderPath() + @"\ToEdiXml_" + _docNbr + ".xslt";

      if (File.Exists(toEdiXmlFile))
        rtxtToEdiXslt.Text = File.ReadAllText(toEdiXmlFile);

      _currentToEdiXslt = rtxtToEdiXslt.Text.Replace("\r\n", "\n");
      _saveToEdiXslt = _currentToEdiXslt;

      string fileName = Path.GetFileName(toEdiXmlFile);
      DisplayMessage("File reloaded: " + fileName);
    }

    private void DisplayMessage(string message)
    {
      _lastMessage = message;
      _messageTimer = new System.Timers.Timer();
      _messageTimer.Interval = 2000;
      _messageTimer.Elapsed += _messageTimer_Elapsed;
      _messageTimer.AutoReset = false;
      lblMessage.Text = message;
      _messageTimer.Enabled = true;
    }

    private void _messageTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      _messageTimer.Enabled = false;
      lblMessage.Invoke((Action)((() => lblMessage.Text = String.Empty)));
      Application.DoEvents();
    }



    private void lblMessage_DoubleClick(object sender, EventArgs e)
    {
      DisplayMessage(_lastMessage);
    }

    private void frmMain_KeyUp(object sender, KeyEventArgs e)
    {
      if (tabMain.SelectedTab != tabPageXsltToEdiXml)
        return;

      if (e.Control && e.KeyCode == Keys.S)
        SaveToEdiXslt();
    }

    private void ckUseDisplayForm_CheckedChanged(object sender, EventArgs e)
    {
      bool showDisplayForm = ckUseDisplayForm.Checked;

      if (showDisplayForm)
      {
        if (fDisplay.IsDisposed)
          fDisplay = new frmDisplay();

        fDisplay.Show();
      }
      else
      {
        fDisplay.Hide();
      }
    }

    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (fDisplay == null)
        return;

      if (fDisplay.IsDisposed)
      {
        fDisplay = null;
        return;
      }

      fDisplay.Close();
      fDisplay.Dispose();
      fDisplay = null;
    }

    private void frmMain_Shown(object sender, EventArgs e)
    {
      if (!_isStartingUp)
        return;

      if (_useDisplayForm)
        ckUseDisplayForm.Checked = true;

      string docNbr = GetDocNbr();
      LoadXslt(docNbr);

      _isStartingUp = false;
    }

    private string GetDocNbr()
    {
      string docNbr = "???";

      string selectedFile = String.Empty;
      switch (_appFocus)
      {
        case AppFocus.EdiInput:
          selectedFile = cboFileToProcess.Text;
          break;

        case AppFocus.BusXmlA:
          selectedFile = cboBusXmlAFiles.Text;
          break;
      }

      if (selectedFile.Trim().Length > 3)
      {
        string nbr = selectedFile.Substring(0, 3);
        if (nbr.IsNumeric())
          docNbr = nbr;
      }

      return docNbr;
    }
  }
}
