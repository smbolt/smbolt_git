using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;
using FastColoredTextBoxNS;

namespace Org.XsdTest
{
  public partial class frmMain : Form
  {
    private string _appRootFolder;
    private string _xmlFilePath;
    private string _xsdFilePath;
    private string _appName = "IdmXsdTest";

    private string _xmlFileName = String.Empty;
    private string _openXmlFileName = String.Empty;
    private string _xsdFileName = String.Empty;
    private string _openXsdFileName = String.Empty;

    private string _xmlFileData = String.Empty;
    private bool _xmlDataChanged = false;
    private string _xsdFileData = String.Empty;
    private bool _xsdDataChanged = false;

    private bool _suppressXmlTextChangedEvent = false;
    private bool _suppressXsdTextChangedEvent = false;

    private List<string> _validationErrors;

    private TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
    private TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
    private TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
    private TextStyle MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
    private TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
    private TextStyle BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
    private TextStyle MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
    private MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

    public frmMain()
    {
      InitializeComponent();

      if (!InitializeForm())
        this.Close();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "RunValidation":
          RunValidation();
          break;

        case "ReloadXml":
          txtValidation.Text = String.Empty;
          LoadXmlFromFile();
          break;

        case "ReloadXsd":
          txtValidation.Text = String.Empty;
          LoadXsdFromFile();
          break;

        case "FormatXml":
          FormatXml();
          break;

        case "FormatXsd":
          FormatXsd();
          break;

        case "SaveXml":
          SaveXml();
          break;

        case "SaveXsd":
          SaveXsd();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void Initialize()
    {
    }

    private void RunValidation()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        // load the xml from the 'XML File' tab
        XDocument doc = XDocument.Load(new StringReader(txtXml.Text));

        // load the xsd from the 'XSD File' tab
        XmlSchemaSet schemas = new XmlSchemaSet();
        schemas.Add("", XmlReader.Create(new StringReader(txtXsd.Text)));

        _validationErrors = new List<string>();

        doc.Validate(schemas, SchemaValidationHandler);

        if (_validationErrors.Count == 0)
        {
          txtValidation.Text = "XML validation was successful.";
        }
        else
        {
          StringBuilder sb = new StringBuilder();
          foreach (var error in _validationErrors)
          {
            sb.Append(error + g.crlf2);
          }
          txtValidation.Text = sb.ToString();
        }

        tabMain.SelectedTab = tabPageValidationResults;

        txtValidation.SelectionStart = 0;
        txtValidation.SelectionLength = 0;

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;

        MessageBox.Show("An error occurred during application start up." + g.crlf2 + ex.ToReport(), _appName + " - Program Initialization Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void SchemaValidationHandler(object o, ValidationEventArgs e)
    {
      int errorNumber = _validationErrors.Count + 1;
      int lineNumber = -1;
      int linePosition = -1;
      object sourceObject = null;
      XmlSchemaObject sourceSchemaObject = null;
      string sourceUri = String.Empty;

      string errorMessage = "ERROR " + errorNumber.ToString("00000") + " - Message         : " + e.Message + g.crlf +
                            "              Severity        : " + e.Severity.ToString() + g.crlf;

      var exceptionElement = o as XElement;


      if (e.Exception != null)
      {
        var ex = e.Exception;
        string exceptionTypeName = ex.GetType().Name;

        switch (exceptionTypeName)
        {
          case "XmlSchemaValidationException":
            var xsvx = ex as XmlSchemaValidationException;
            lineNumber = xsvx.LineNumber;
            linePosition = xsvx.LinePosition;
            sourceObject = xsvx.SourceObject;
            sourceSchemaObject = xsvx.SourceSchemaObject;
            sourceUri = xsvx.SourceUri != null ? xsvx.SourceUri : String.Empty;
            break;

          default:
            break;
        }
      }


      if (lineNumber > 0)
      {
        errorMessage += "              Line Number     : " + lineNumber.ToString() + g.crlf +
                        "              Line Position   : " + linePosition.ToString() + g.crlf;
      }

      if (exceptionElement != null)
      {
        errorMessage += "              Invalid Xml     : " + exceptionElement.ToErrorReport(32, 160) + g.crlf;
      }
      else
      {
        errorMessage += "              Invalid Xml     : *** REPORTING FOR XML NODE TYPE '" + o.GetType().Name + "' NOT YET IMPLEMENTED" + g.crlf;
      }

      if (sourceObject != null)
      {
        System.Diagnostics.Debugger.Break();
      }

      if (sourceSchemaObject != null)
      {
        System.Diagnostics.Debugger.Break();
      }

      if (sourceUri.IsNotBlank())
      {
        System.Diagnostics.Debugger.Break();
      }

      _validationErrors.Add(errorMessage);
    }

    private bool InitializeForm()
    {
      try
      {
        _appRootFolder = GetAppRootFolder();

        var appSettings = ConfigurationManager.AppSettings;

        _xmlFilePath = appSettings["XmlFilePath"];
        _xsdFilePath = appSettings["XsdFilePath"];

        if (_xmlFilePath.StartsWith("@"))
          _xmlFilePath = _xmlFilePath.Replace("@", _appRootFolder);

        if (_xsdFilePath.StartsWith("@"))
          _xsdFilePath = _xsdFilePath.Replace("@", _appRootFolder);

        if (!Directory.Exists(_xmlFilePath))
          Directory.CreateDirectory(_xmlFilePath);

        if (!Directory.Exists(_xsdFilePath))
          Directory.CreateDirectory(_xsdFilePath);

        btnRunValidation.Enabled = false;
        btnReloadXml.Enabled = false;
        btnReloadXsd.Enabled = false;
        btnFormatXml.Enabled = false;
        btnFormatXsd.Enabled = false;
        btnSaveXml.Enabled = false;
        btnSaveXsd.Enabled = false;

        lblXmlFileMessage.Text = String.Empty;
        lblXmlFileMessage.Visible = false;
        lblXsdFileMessage.Text = String.Empty;
        lblXsdFileMessage.Visible = false;

        LoadXmlFilesToComboBox();
        LoadXsdFilesToComboBox();

        int formHorizontalSize = appSettings["MainFormHorizontalSize"].ToInt32OrDefault(90);
        int formVerticalSize = appSettings["MainFormVerticalSize"].ToInt32OrDefault(90);

        this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * formHorizontalSize / 100,
                             Screen.PrimaryScreen.Bounds.Height * formVerticalSize / 100);
        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                  Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2);

        tabMain.SelectedTab = tabPageXml;

        return true;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An error occurred during application start up." + g.crlf2 + ex.ToReport(), _appName + " - Program Initialization Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

        return false;
      }

    }

    private string GetAppRootFolder()
    {
      string appRootFolder = String.Empty;
      string exeFolder = AppDomain.CurrentDomain.BaseDirectory;

      if (exeFolder.ToLower().Contains(@"\bin"))
      {
        int pos = exeFolder.ToLower().IndexOf(@"\bin");
        appRootFolder = exeFolder.Substring(0, pos);
      }
      else
      {
        appRootFolder = exeFolder;
      }

      while (appRootFolder.IsNotBlank() && (appRootFolder.EndsWith(@"\") || appRootFolder.EndsWith(@"/")))
        appRootFolder = appRootFolder.Substring(0, appRootFolder.Length - 1);


      return appRootFolder;
    }

    private void LoadXmlFilesToComboBox()
    {
      cboXmlFile.Items.Clear();
      var files = Directory.GetFiles(_xmlFilePath, "*.xml");

      foreach (var file in files)
      {
        cboXmlFile.Items.Add(Path.GetFileName(file));
      }
    }

    private void LoadXsdFilesToComboBox()
    {
      cboXsdFile.Items.Clear();
      var files = Directory.GetFiles(_xsdFilePath, "*.xsd");

      foreach (var file in files)
      {
        cboXsdFile.Items.Add(Path.GetFileName(file));
      }
    }

    private void SelectionChanged(object sender, EventArgs e)
    {
      if (cboXmlFile.Text.IsNotBlank())
      {
        _xmlFileName = _xmlFilePath + @"\" + cboXmlFile.Text;
        if (_xmlFileName != _openXmlFileName)
        {
          _openXmlFileName = _xmlFileName;
          LoadXmlFromFile();
          btnReloadXml.Enabled = true;
          btnFormatXml.Enabled = true;
        }
      }
      else
      {
        btnReloadXml.Enabled = false;
        btnFormatXml.Enabled = false;
      }

      if (cboXsdFile.Text.IsNotBlank())
      {
        _xsdFileName = _xsdFilePath + @"\" + cboXsdFile.Text;
        LoadXsdFromFile();
        btnReloadXsd.Enabled = true;
        btnFormatXsd.Enabled = true;
      }
      else
      {
        btnReloadXsd.Enabled = false;
        btnFormatXsd.Enabled = false;
      }

      if (cboXmlFile.Text.IsBlank() || cboXsdFile.Text.IsBlank())
      {
        btnRunValidation.Enabled = false;
        return;
      }

      txtValidation.Text = String.Empty;

      btnRunValidation.Enabled = true;
    }

    private void LoadXmlFromFile()
    {
      try
      {
        if (_xmlDataChanged)
        {
          if (MessageBox.Show("The XML file being edited has been changed and reloading the XML file will cause all changes to be discarded." + g.crlf2 +
                              "Click the 'No' button and save the XML file if you want to retain your changes." + g.crlf2 +
                              "Click the 'Yes' if you want to reload the XML from the file and discard your changes.", _appName + " - Confirm Discard XML Changes",
                              MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            return;
        }

        string xmlData = File.ReadAllText(_xmlFileName);
        var xml = XElement.Parse(xmlData);
        _suppressXmlTextChangedEvent = true;
        txtXml.Text = xml.ToString();
        txtXml.SelectionStart = 0;
        txtXml.SelectionLength = 0;
        _xmlFileData = txtXml.Text;
        tabMain.SelectedTab = tabPageXml;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An error occurred while attempting to load the xml from file '" + _xmlFilePath + "'." + g.crlf2 + ex.ToReport(),
                        _appName + " - XML Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadXsdFromFile()
    {
      try
      {
        if (_xsdDataChanged)
        {
          if (MessageBox.Show("The XSD file being edited has been changed and reloading the XSD data will cause all changes to be discarded." + g.crlf2 +
                              "Click the 'No' button and save the XSD file if you want to retain your changes." + g.crlf2 +
                              "Click the 'Yes' if you want to reload the XSD from the file and discard your changes.", _appName + " - Confirm Discard XSD Changes",
                              MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            return;
        }

        string xsdData = File.ReadAllText(_xsdFileName);
        var xsd = XElement.Parse(xsdData);
        _suppressXsdTextChangedEvent = true;
        txtXsd.Text = xsd.ToString();
        txtXsd.SelectionStart = 0;
        txtXsd.SelectionLength = 0;
        _xsdFileData = txtXsd.Text;
        tabMain.SelectedTab = tabPageXsd;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An error occurred while attempting to load the xsd from file '" + _xsdFilePath + "'." + g.crlf2 + ex.ToReport(),
                        _appName + " - XSD Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

    }

    private void FormatXml()
    {
      try
      {
        string xmlText = txtXml.Text;
        var xml = XElement.Parse(xmlText);
        _suppressXmlTextChangedEvent = true;
        txtXml.Text = xml.ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An error occurred while attempting to format the XML data in the XML File tab." + g.crlf2 + ex.ToReport(),
                        _appName + " - XML Formatting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void FormatXsd()
    {
      try
      {
        string xsdText = txtXsd.Text;
        var xsd = XElement.Parse(xsdText);
        _suppressXsdTextChangedEvent = true;
        txtXsd.Text = xsd.ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An error occurred while attempting to format the XSD data in the XSD File tab." + g.crlf2 + ex.ToReport(),
                        _appName + " - XSD Formatting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void SaveXml()
    {
      try
      {
        string xmlText = txtXml.Text;
        var xml = XElement.Parse(xmlText);
        string formattedText = xml.ToString();
        File.WriteAllText(_xmlFileName, formattedText);
      }
      catch (Exception ex)
      {
        MessageBox.Show("An error occurred while attempting to save the XML data in the XML File tab." + g.crlf2 + ex.ToReport(),
                        _appName + " - XML File Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void SaveXsd()
    {
      try
      {
        string xsdText = txtXsd.Text;
        var xsd = XElement.Parse(xsdText);
        string formattedText = xsd.ToString();
        File.WriteAllText(_xsdFilePath, formattedText);
      }
      catch (Exception ex)
      {
        MessageBox.Show("An error occurred while attempting to save the XSD data in the XSD File tab." + g.crlf2 + ex.ToReport(),
                        _appName + " - XSD File Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void txtXml_TextChanged(object sender, EventArgs e)
    {
      Task.Run(() => CheckForXmlChange());
    }

    private void txtXsd_TextChanged(object sender, EventArgs e)
    {
      Task.Run(() => CheckForXsdChange());
    }

    private void CheckForXmlChange()
    {
      if (_suppressXmlTextChangedEvent)
      {
        _suppressXmlTextChangedEvent = false;
        return;
      }

      if (_xmlFileData == null)
        return;

      string xmlText = txtXml.Text;
      var xsd = XElement.Parse(xmlText);
      string formattedText = xsd.ToString();

      this.Invoke((Action)((() =>
      {
        if (formattedText == _xmlFileData)
        {
          tabPageXml.Text = "XML File";
          _xmlDataChanged = false;
          lblXmlFileMessage.Text = String.Empty;
          lblXmlFileMessage.Visible = false;
          btnSaveXml.Enabled = false;

        }
        else
        {
          tabPageXml.Text = "XML File *";
          _xmlDataChanged = true;
          lblXmlFileMessage.Text = "XML is modified";
          lblXmlFileMessage.Visible = true;
          btnSaveXml.Enabled = true;
        }
      }
                           )));
    }

    private void CheckForXsdChange()
    {
      if (_suppressXsdTextChangedEvent)
      {
        _suppressXsdTextChangedEvent = false;
        return;
      }

      if (_xsdFileData == null)
        return;

      string xsdText = txtXsd.Text;
      var xsd = XElement.Parse(xsdText);
      string formattedText = xsd.ToString();


      this.Invoke((Action)((() =>
      {
        if (formattedText == _xsdFileData)
        {
          tabPageXsd.Text = "XSD File";
          _xsdDataChanged = false;
          lblXsdFileMessage.Text = String.Empty;
          lblXsdFileMessage.Visible = false;
          btnSaveXsd.Enabled = false;
        }
        else
        {
          tabPageXsd.Text = "XSD File *";
          _xsdDataChanged = true;
          lblXsdFileMessage.Text = "XSD is modified";
          lblXsdFileMessage.Visible = true;
          btnSaveXsd.Enabled = true;
        }
      }
                           )));
    }

    private void udFontSize_ValueChanged(object sender, EventArgs e)
    {
      float newFontSize = (float) Convert.ToDecimal(udFontSize.Value);

      if (txtXml.Font.Size != newFontSize)
      {
        var fontFamily = txtXml.Font.FontFamily;
        float fontSize = txtXml.Font.Size;

        txtXml.Font = new Font(fontFamily, newFontSize);
        //txtXml.Refresh();
        txtXsd.Font = new Font(fontFamily, newFontSize);
        //txtXsd.Refresh();
        txtValidation.Font = new Font(fontFamily, newFontSize);
        //txtValidation.Refresh();
        //Application.DoEvents();

      }
    }

    private void TextValueChanged(object sender, TextChangedEventArgs e)
    {
      if (sender.GetType().Name != "FastColoredTextBox")
        return;

      this.Cursor = Cursors.WaitCursor;

      var fctb = (FastColoredTextBox)sender;

      if (fctb.Tag == null)
        return;

      string tag = fctb.Tag.ToString();

      switch (tag)
      {
        case "XML":
          fctb.Language = Language.XML;
          fctb.ClearStylesBuffer();
          fctb.Range.ClearStyle(StyleIndex.All);
          fctb.AddStyle(SameWordsStyle);
          fctb.AutoIndentNeeded -= fctb_AutoIndentNeeded;
          fctb.OnSyntaxHighlight(new TextChangedEventArgs(fctb.Range));
          break;

        case "TEXT":

          break;
      }

      this.Cursor = Cursors.Default;
    }

    private void fctb_AutoIndentNeeded(object sender, AutoIndentEventArgs args)
    {
      //block {}
      if (Regex.IsMatch(args.LineText, @"^[^""']*\{.*\}[^""']*$"))
        return;
      //start of block {}
      if (Regex.IsMatch(args.LineText, @"^[^""']*\{"))
      {
        args.ShiftNextLines = args.TabLength;
        return;
      }
      //end of block {}
      if (Regex.IsMatch(args.LineText, @"}[^""']*$"))
      {
        args.Shift = -args.TabLength;
        args.ShiftNextLines = -args.TabLength;
        return;
      }
      //label
      if (Regex.IsMatch(args.LineText, @"^\s*\w+\s*:\s*($|//)") &&
          !Regex.IsMatch(args.LineText, @"^\s*default\s*:"))
      {
        args.Shift = -args.TabLength;
        return;
      }
      //some statements: case, default
      if (Regex.IsMatch(args.LineText, @"^\s*(case|default)\b.*:\s*($|//)"))
      {
        args.Shift = -args.TabLength / 2;
        return;
      }
      //is unclosed operator in previous line ?
      if (Regex.IsMatch(args.PrevLineText, @"^\s*(if|for|foreach|while|[\}\s]*else)\b[^{]*$"))
        if (!Regex.IsMatch(args.PrevLineText, @"(;\s*$)|(;\s*//)"))//operator is unclosed
        {
          args.Shift = args.TabLength;
          return;
        }
    }
  }
}
