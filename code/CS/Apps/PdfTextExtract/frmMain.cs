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
using fctb = FastColoredTextBoxNS;
using Org.PdfExtractToolWindows;
using Org.Dx.Business;
using Org.Dx.Business.TextProcessing;
using Org.TW.Forms;
using Org.TW.ToolPanels;
using Org.TW;
using Org.SF;
using Org.GS.UI;
using Org.GS;

namespace Org.PdfTextExtract
{
  public partial class frmMain : frmToolWindowParent
  {
    private a a;

    private ColumnIndexMap _ciMap;

    private string _filePath = String.Empty;
    private string _mapsFolder = String.Empty;
    private fctb.MarkerStyle _bgHighlightStyle;
    private fctb.MarkerStyle _bgClearStyle;
    private List<fctb.Range> _rangesToClear;

    private FileFormatSet _fileFormatSet;
    private RecogSpecSet _recogSpecSet;
    private ExtractSpecSet _extractSpecSet;
    private bool _isFirstShowing = true;
    private bool _allowDebugBreak = false;

    private bool _filesGridUpdating;
    private bool _formatsListBoxUpdating;
    private bool _runPostLoadCommands;
    private List<string> _postLoadCommands;
    private List<string> _extractSpecIncludes;
    private string _assignedMap;

    private SortedList<string, bool> _formats;

    #region Tool Window Management Fields

    private ToolWindowManager _twMgr;

    private UIState _uiState;
    private float _scale = 100.0F;
    private bool propertiesShown = false;
    private bool codePanelShown = false;
    private PictureBox pb;
    private Image _emptyCell;
    private Text _text;
    private TextPatternSet _textPatternSet;
    private List<string> _tabPageOrder;
    #endregion


    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      if (action.StartsWith("TW_"))
      {
        ToolWindowAction(action);
        return;
      }

      switch (action)
      {
        case "SaveAs":
          SaveAs();
          break;

        case "LoadFiles":
          LoadFiles();
          break;

        case "ProcessFile":
          ProcessFile();
          break;

        case "LoadFilteredList":
          LoadFilteredList();
          break;

        case "ReloadFormats":
          ReloadTextProcessingConfigs();
          break;

        case "FindPatterns":
          FindPatterns();
          break;

        case "RecognizeFormats":
          RecognizeFormats();
          break;

        case "DefineTextStructure":
          DefineTextStructure();
          break;

        case "UndefinedFormatOnly":
          SetUndefinedFormatOnly();
          break;

        case "SaveConfig":
          SaveConfig();
          break;

        case "LoadColumnIndexMap":
          LoadColumnIndexMap();
          break;

        case "EnableBreakpoint":
        case "KeepBreakpointEnabled":
          ManageBreakpoints();
          break;

        case "ReloadConfigs":
          ReloadTextProcessingConfigs();
          ProcessFile();
          break;

        case "Exit":
          TerminateProgram();
          break;
      }
    }

    private void RunPostLoadCommands()
    {
      if (!_runPostLoadCommands)
        return;

      if (_postLoadCommands == null || _postLoadCommands.Count == 0)
        return;

      foreach (var cmd in _postLoadCommands)
      {
        switch (cmd)
        {
          case "LoadFiles":
            LoadFiles();
            break;

          case "RecognizeFormats":
            RecognizeFormats();
            break;

          case "SelectOnlyItemInGrid":
            if (gvFiles.Rows.Count == 1)
              gvFiles.Rows[0].Selected = true;
            break;

          case "ProcessFile":
            ProcessFile();
            break;
        }
      }
    }

    private void SaveAs()
    {
      try
      {
        if (_twMgr == null || _twMgr.ToolPanels == null || !_twMgr.ToolPanels.ContainsKey("TextExtractResults"))
          return;

        var toolWindow = _twMgr.ToolPanels["TextExtractResults"] as RichTextViewer;
        string mappedFileText = toolWindow.Text;

        if (!mappedFileText.IsValidXml())
        {
          this.Cursor = Cursors.Default;
          MessageBox.Show("The mapped file data is not valid XML.", g.AppInfo.AppName + " - Invalid XML", MessageBoxButtons.OK, MessageBoxIcon.Information);
          return;
        }

        string mappedFilePath = g.CI("MappedFilePath");
        if (!Directory.Exists(mappedFilePath))
          mappedFilePath = @"C:\";

        dlgFileSave.InitialDirectory = mappedFilePath;

        if (gvFiles.SelectedRows.Count > 0)
        {
          var row = gvFiles.SelectedRows[0];
          string filePath = row.Cells["FilePath"].Value.ToString();
          string fileName = row.Cells["FileName"].Value.ToString();

          if (filePath.EndsWith(@"\Ready"))
          {
            filePath = filePath.Replace(@"\Ready", @"\MappedFiles");

            while (!Directory.Exists(filePath))
            {
              if (!filePath.Contains(@"\") && !filePath.Contains(@"/"))
                break;
              filePath = Path.GetDirectoryName(filePath);
            }

            string ext = Path.GetExtension(fileName).ToLower();
            if (ext != ".xml")
              fileName = Path.ChangeExtension(fileName, ".xml");

            dlgFileSave.InitialDirectory = filePath;
            dlgFileSave.FileName = fileName;
          }
        }

        dlgFileSave.Title = "Save mapped file as...";

        if (dlgFileSave.ShowDialog() == DialogResult.OK)
        {
          File.WriteAllText(dlgFileSave.FileName, mappedFileText);
          this.Cursor = Cursors.Default;
          MessageBox.Show("The mapped file has been saved." + g.crlf2 + "Path is: '" +g.crlf2 + dlgFileSave.FileName + "'.",
                          g.AppInfo.AppName + " - Mapped File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to save the mapped file." + g.crlf2 + ex.ToReport(),
                        "PdfTextExtract - Save Mapped File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadFiles()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        gvFiles.Rows.Clear();
        Application.DoEvents();

        _fileFormatSet = new FileFormatSet();
        var fileFormat = new FileFormat();
        fileFormat.Name = "UNDEFINED";
        _fileFormatSet.Add(fileFormat.Name, fileFormat);

        var searchParms = new SearchParms();

        Application.DoEvents();

        char[] delim = new char[] { ',' };

        string rootPath = cboRootFolder.Text;

        if (rootPath.IsBlank())
        {
          this.Cursor = Cursors.Default;
          MessageBox.Show("The root folder specified in the ComboBox is blank.", "Root Folder is Blank", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        string location = g.CI("Location");
        if (location == "Home")
        {
          string homeFsStem = g.CI("HomeFsStem");
          rootPath = rootPath.Replace(@"\\gulfport.net\data\Applications\Data_Integration", homeFsStem);
        }

        searchParms.RootPath = rootPath;
        searchParms.FileNameIncludes.AddRange(cboFileNameFilters.Text.ToListOfStrings(Constants.CommaDelimiter));

        if (searchParms.FileNameIncludes.Count == 1 && searchParms.FileNameIncludes[0] == "*")
          searchParms.FileNameIncludes.Clear();

        if (searchParms.FileNameIncludes.Count > 0)
          searchParms.ExtensionAndFileNameIncludeLogicOp = LogicOp.AND;
        searchParms.Extensions.Add(".pdf");
        searchParms.Extensions.Add(".xml");
        searchParms.FileCountLimit = 200;

        var rootFolder = RecurseFileSystem(searchParms, true);

        // This logic and the logic in the callback function below needs to be examined to ensure that any files directly in the root folder are included.
        // It may be that with the current logic, we might not include files that are directly in the root folder.  This is because we start the collection process
        // by iterating over the direct child folders of the root.


        int fileCount = 0;

        foreach (var rootFile in rootFolder.FileList)
        {
          fileCount++;
          _fileFormatSet["UNDEFINED"].Files.Add(rootFile.Value);
        }

        foreach (var folder in rootFolder.OSFolderSet)
        {
          if (!folder.IsProcessed)
          {
            foreach (var file in folder.FileList)
            {
              fileCount++;
              _fileFormatSet["UNDEFINED"].Files.Add(file.Value);
            }
          }
        }

        LoadFilteredList();

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the processing of the root folder." + g.crlf2 + ex.ToReport(),
                        "PdfTextExtract - Processing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }


    private void LoadFilteredList()
    {
      var filterStrings = cboFileNameFilters.Text.ToListOfStrings(Constants.CommaDelimiter);
      LoadFilesGrid(filterStrings);
    }

    private void LoadFilesGrid(List<string> filterStrings)
    {
      if (_fileFormatSet == null || _fileFormatSet.Count == 0)
        return;

      _filesGridUpdating = true;

      int fileCount = 0;
      gvFiles.Rows.Clear();
      Application.DoEvents();

      var formatsToInclude = new List<string>();
      if (ckUseFormatFilters.Checked)
      {
        foreach (var item in ckListFormats.CheckedItems)
        {
          formatsToInclude.Add(item.ToString());
        }
      }
      else
      {
        foreach (var formatName in _fileFormatSet.Keys)
        {
          formatsToInclude.Add(formatName);
        }
      }

      foreach (var formatToInclude in formatsToInclude)
      {
        if (_fileFormatSet.ContainsKey(formatToInclude))
        {
          foreach (var file in _fileFormatSet[formatToInclude].Files)
          {
            bool include = true;
            if (filterStrings.Count > 0)
            {
              include = false;
              foreach (string filteredString in filterStrings)
              {
                var filteredStringLower = filteredString.ToLower().Trim();
                if (file.FileName.ToLower().Contains(filteredStringLower))
                {
                  include = true;
                  break;
                }
              }
            }

            if (!include)
              continue;

            fileCount++;
            var fileDataGrid = new string[4];
            fileDataGrid[0] = fileCount.ToString("###,##0");
            fileDataGrid[1] = file.FileName;
            fileDataGrid[2] = formatToInclude;
            fileDataGrid[3] = file.ParentFolder.FullPath;
            gvFiles.Rows.Add(fileDataGrid);
          }
        }
      }

      gvFiles.ClearSelection();
      _filesGridUpdating = false;

      btnRecognizeFormats.Enabled = fileCount > 0;

      lblStatus.Text = gvFiles.Rows.Count.ToString("###,##0") + " files loaded.";
    }

    private void gvFiles_SelectionChanged(object sender, EventArgs e)
    {
    }

    private void ProcessFile()
    {
      if (_filesGridUpdating)
        return;

      if (gvFiles.SelectedRows.Count == 0)
        return;

      this.Cursor = Cursors.WaitCursor;

      try
      {
        var row = gvFiles.SelectedRows[0];
        string filePath = row.Cells["FilePath"].Value.ToString();
        string fileName = row.Cells["FileName"].Value.ToString();
        string formatName = row.Cells["FileFormat"].Value.ToString();
        string fullFilePath = filePath + @"\" + fileName;

        //if (formatName == "UNDEFINED")
        //{
        //  this.Cursor = Cursors.Default;
        //  MessageBox.Show("The selected file has a file format of 'UNDEFINED'." + g.crlf2 +
        //                  "A valid file format must be identified before the file can be processed.", "PdfTextExtract - Processing Error",
        //                  MessageBoxButtons.OK, MessageBoxIcon.Error);
        //  return;
        //}

        RecogSpec recogSpec = null;
        if (_recogSpecSet.ContainsKey(formatName))
          recogSpec = _recogSpecSet[formatName];

        // if file recognition has been used to identify the file format
        // get an extract spec for the format
        ExtractSpec extractSpec = null;
        if (_extractSpecSet != null && recogSpec != null && recogSpec.Name != "UNDEFINED")
          extractSpec = _extractSpecSet.Values.Where(s => s.RecogSpecName == recogSpec.Name).FirstOrDefault();

        // when no recog spec is used and a single file is being processed we can plug an extractSpec by using _assignedMap CI.
        if (formatName == "ASSIGNED")
          formatName = "UNDEFINED";

        if (!formatName.In("ASSIGNED,UNDEFINED") && extractSpec == null && _assignedMap.IsNotBlank())
          formatName = "UNDEFINED";

        if (formatName == "UNDEFINED" && extractSpec == null && _assignedMap.IsNotBlank() && gvFiles.SelectedRows.Count == 1)
        {
          if (_extractSpecSet.ContainsKey(_assignedMap))
          {
            extractSpec = _extractSpecSet[_assignedMap];
            row.Cells["FileFormat"].Value = "ASSIGNED";
          }
        }

        ExtractionMap extractionMap = null;
        if (extractSpec != null && !ckSuppressSections.Checked)
          extractionMap = extractSpec.ExtractionMap;

        if (extractSpec == null)
        {
          MessageBox.Show("The ExtractSpec is null.", "PDFTextExtractor - ExtractSpec is Null", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        using (var textExtractor = new TextExtractor())
        {
          switch (extractSpec.FileType)
          {
            case FileType.XML:
              _text = textExtractor.ExtractTextFromXml(_allowDebugBreak, fullFilePath);
              break;

            case FileType.PDF:
              _text = textExtractor.ExtractTextFromPdf(_allowDebugBreak, fullFilePath, extractionMap);
              _text.ExtractSpec = extractSpec;
              break;

            case FileType.ImageBased:
              string extractedText = textExtractor.ExtractTextFromImages(_allowDebugBreak, fullFilePath, "");
              break;

          }

          if (g.CI("InDiagnosticsMode").ToBoolean())
            Org.Dx.Business.TextProcessing.Text.InDiagnosticsMode = true;
        }

        if (_twMgr.ToolPanels.ContainsKey("RawExtractedText"))
        {
          var toolPanel = _twMgr.ToolPanels["RawExtractedText"] as RichTextViewer;

          switch (_text.FileType)
          {
            case FileType.XML:
              toolPanel.Text = _text.XElement.ToString();
              break;

            case FileType.PDF:
              toolPanel.Text = _text.RawText;
              break;
          }
        }

        if (recogSpec != null && _twMgr.ToolPanels.ContainsKey("ConfigEdit"))
        {
          string cfg = new ObjectFactory2().Serialize(recogSpec).ToString();
          var toolPanel = _twMgr.ToolPanels["ConfigEdit"] as RichTextViewer;
          toolPanel.Text = cfg;
        }

        if (_twMgr.ToolPanels.ContainsKey("PdfViewer") && _text.FileType == FileType.PDF)
        {
          var toolPanel = _twMgr.ToolPanels["PdfViewer"] as PdfViewerControl;
          toolPanel.CloseDocument();
          toolPanel.LoadDocument(fullFilePath);
        }

        if (_twMgr.ToolPanels.ContainsKey("TextExtractDesigner") && extractSpec != null)
        {
          var toolPanel = _twMgr.ToolPanels["TextExtractDesigner"] as TextExtractDesigner;
          toolPanel.LoadTextObject(recogSpec, extractSpec, _text);
        }

        tabMain.SelectedTab = tabPageTextExtractDesigner;

      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to load the pdf file and extract text." + g.crlf2 + ex.ToReport(),
                        "PdfTextExtract - Processing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      this.Cursor = Cursors.Default;
    }

    private OSFolder RecurseFileSystem(SearchParms searchParms, bool wireUpEvents)
    {
      var rootFolder = new OSFolder();
      rootFolder.FullPath = searchParms.RootPath;
      rootFolder.RootFolderPath = searchParms.RootPath;
      rootFolder.IsRootFolder = true;
      rootFolder.DepthFromRoot = 0;
      rootFolder.SearchParms.ProcessChildFolders = true;
      if (wireUpEvents)
      {
        OSFolder.SetLimitReachedFunction(FileLimitReached);
        OSFolder.FSNotification += NotifyHost;
      }

      rootFolder.SearchParms = searchParms;
      rootFolder.BuildFolderAndFileList();
      return rootFolder;
    }

    private bool FileLimitReached(OSFolder rootFolder, bool processAllFolders)
    {
      int folderOmitCount = processAllFolders ? 0 : 1;

      if (!processAllFolders && rootFolder.OSFolderSet.Count < 2)
        return false;

      for (int i = 0; i < rootFolder.OSFolderSet.Count - folderOmitCount; i++)
      {
        var folder = rootFolder.OSFolderSet[i];

        foreach (var file in folder.FileList)
        {
          _fileFormatSet["UNDEFINED"].Files.Add(file.Value);
        }

        folder.IsProcessed = true;
      }

      return true;
    }

    private void NotifyHost(string notifyMessage)
    {
      if (lblStatus.InvokeRequired)
      {
        lblStatus.Invoke((Action)((() =>
        {
          lblStatus.Text = notifyMessage;
          Application.DoEvents();
        })));
      }
      else
      {
        lblStatus.Text = notifyMessage;
        Application.DoEvents();
      }
    }

    private void FindPatterns()
    {
      if (_text == null || _text.RawText.IsBlank())
        return;

      _textPatternSet = new TextPatternSet(_text.RawText);

      if (_twMgr.ToolPanels.ContainsKey("FormatRecognition"))
      {
        var tp = (RichTextViewer)_twMgr.ToolPanels["FormatRecognition"];
        tp.Text = _textPatternSet.ToReport();
        if (!tp.IsDockedInToolWindow)
        {
          tabMain.SelectedTab = tabPageFormatRecognition;
        }
      }
    }

    private void _fDisplayText_FormClosed(object sender, FormClosedEventArgs e)
    {
    }

    private string GetPatternText(string text)
    {
      using (var textEngine = new TextEngine())
      {
        return textEngine.GetPatterns(text);
      }
    }

    private void DefineTextStructure()
    {
      try
      {
        //if (gvFiles.SelectedRows.Count != 1)
        //  return;

        //var row = gvFiles.SelectedRows[0];
        //string formatName = row.Cells[2].Value.ToString();

        //if (!_formatSpecSet.ContainsKey(formatName))
        //  return;

        //var formatSpec = _formatSpecSet[formatName];
        //var rs = formatSpec.RecogSpecSet;
        //var ts = formatSpec.TextStructureDefinition;

        //if (ts == null || rs == null)
        //  return;

        //if (_text == null || _text.RawText.IsBlank())
        //  return;

        //this.Cursor = Cursors.WaitCursor;
        //ts.SetTextStructure(_text, rs);
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception while attempting to define the text structure using format." + g.crlf2 + ex.ToReport(),
                        "PdfTextExtract - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      this.Cursor = Cursors.Default;
    }


    private void SaveConfig()
    {
      if (_twMgr == null || _twMgr.ToolPanels == null || !_twMgr.ToolPanels.ContainsKey("ConfigEdit"))
        return;

      var toolPanel = _twMgr.ToolPanels["ConfigEdit"] as RichTextViewer;
      string configText = toolPanel.Text;

      if (configText.IsBlank())
        return;

      XElement xml = null;

      try
      {
        xml = XElement.Parse(configText);
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to create a valid xml element from the data in the Configuration Editor tool window." + g.crlf2 +
                        "The exception is shown below." + g.crlf2 + ex.ToReport(),
                        "PdfTextExtract - Configuration Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      string configType = String.Empty;
      string fullXmlPath = String.Empty;
      string fullFilePath = String.Empty;

      Label lblConfigType = toolPanel.GetTopPanelControl("lblConfigType") as Label;
      if (lblConfigType != null)
      {
        string cfgTypeLabelText = lblConfigType.Text;
        configType = cfgTypeLabelText.Replace("Config Type:", String.Empty).Trim();
      }

      Label lblFullXmlPath = toolPanel.GetTopPanelControl("lblFullXmlPath") as Label;
      if (lblFullXmlPath != null)
      {
        string fullXmlPathLabelText = lblFullXmlPath.Text;
        fullXmlPath = fullXmlPathLabelText.Replace("Full XML Path:", String.Empty).Trim();
      }

      Label lblFullFilePath = toolPanel.GetTopPanelControl("lblFullFilePath") as Label;
      if (lblFullFilePath != null)
      {
        string fullFilePathLabelText = lblFullFilePath.Text;
        fullFilePath = fullFilePathLabelText.Replace("Config File Path:", String.Empty).Trim();
      }

      if (!System.IO.File.Exists(fullFilePath))
      {
        MessageBox.Show("An exception occurred while attempting to save the configuration data from Configuration Editor tool window." + g.crlf2 +
                        "The file '" + fullFilePath + "' does not exist.",
                        "PdfTextExtract - Configuration Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      string fileText = String.Empty;

      XElement extractSpecSet = XElement.Parse(System.IO.File.ReadAllText(fullFilePath));
      XElement oldExtractSpec = null;
      XElement newExtractSpec = xml;
      string specName = String.Empty;

      if (fullXmlPath.StartsWith("root:"))
      {
        specName = fullXmlPath.Replace("root:", String.Empty);
        foreach (var element in extractSpecSet.Elements("ExtractSpec"))
        {
          if (element.Attribute("Name") != null && element.Attribute("Name").Value == specName)
          {
            oldExtractSpec = element;
            break;
          }
        }

        if (oldExtractSpec == null)
        {
          MessageBox.Show("An exception occurred while attempting to save the configuration data from Configuration Editor tool window." + g.crlf2 +
                          "The existing ExtractSpec named  '" + specName + "' could not be found in the ExtractExtractSpec in the file '" +
                          fullFilePath + "'.",
                          "PdfTextExtract - Configuration Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        oldExtractSpec.AddBeforeSelf(newExtractSpec);
        oldExtractSpec.Remove();
        fileText = extractSpecSet.ToString();

      }
      else
      {
        string[] pathTokens = fullXmlPath.Split(Constants.BSlashDelimiter, StringSplitOptions.RemoveEmptyEntries);
        if (pathTokens.Length < 2)
        {
          MessageBox.Show("The full xml path '" + fullXmlPath + "' contains less than the minimum of 2 tokens which are used for locating the specific elements in " +
                          "the configuration file that are to be saved." + g.crlf2 + "The update to the configuration file failed for the file named'" +
                          fullFilePath + "'.", "PdfTextExtract - Configuration Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        specName = pathTokens[0];
        foreach (var element in extractSpecSet.Elements("ExtractSpec"))
        {
          if (element.Attribute("Name") != null && element.Attribute("Name").Value == specName)
          {
            oldExtractSpec = element;
            break;
          }
        }

        if (oldExtractSpec == null)
        {
          MessageBox.Show("An exception occurred while attempting to save the configuration data from Configuration Editor tool window." + g.crlf2 +
                          "The existing ExtractSpec named  '" + specName + "' could not be found in the ExtractExtractSpec in the file '" +
                          fullFilePath + "'.",
                          "PdfTextExtract - Configuration Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        XElement parentElement = oldExtractSpec;
        XElement oldElement = null;
        int tokenIndex = 1;


        while (tokenIndex < pathTokens.Length)
        {
          string tsdName = pathTokens[tokenIndex];
          if (parentElement.Element("TsdSet") != null)
          {
            parentElement = parentElement.Element("TsdSet");
          }

          var tsdSet = parentElement.Elements("Tsd");
          foreach (var tsd in tsdSet)
          {
            var nameAttr = tsd.Attribute("Name");
            if (nameAttr != null && nameAttr.Value == tsdName)
            {
              if (tokenIndex == pathTokens.Length - 1)
              {
                oldElement = tsd;
                tokenIndex++;
                break;
              }
              else
              {
                var childTsdSet = tsd.Element("TsdSet");
                if (childTsdSet != null)
                {
                  parentElement = childTsdSet;
                  tokenIndex++;
                }
                else
                {
                  throw new Exception("The Tsd xml object named '" + tsdName + "' does not have a TsdSet in which lower level Tsd xml objects are located. " +
                                      "The full xml path of Tsd to be updated is '" + fullXmlPath + "'.");
                }
              }
            }
          }
        }

        if (oldElement == null)
          throw new Exception("Unable to find Tsd xml object with full path '" + fullXmlPath + "' in the ExtractSpec named '" + specName + "'.");

        oldElement.AddBeforeSelf(newExtractSpec);
        oldElement.Remove();

        fileText = extractSpecSet.ToString();
      }

      try
      {
        System.IO.File.WriteAllText(fullFilePath, fileText);
        LoadExtractSpecSet();

        if (_text != null)
        {
          _text.RefreshExtractSpec(_extractSpecSet[specName]);
        }

        if (_twMgr != null && _twMgr.ToolPanels != null && _twMgr.ToolPanels.ContainsKey("TextExtractDesigner") && _text != null)
        {
          var extractSpec = _extractSpecSet[specName];
          string recogSpecName = extractSpec != null ? extractSpec.RecogSpecName : String.Empty;

          if (recogSpecName.IsNotBlank() && _recogSpecSet.ContainsKey(recogSpecName))
          {
            var recogSpec = _recogSpecSet[recogSpecName];
            var textExtractDesigner = _twMgr.ToolPanels["TextExtractDesigner"] as TextExtractDesigner;
            textExtractDesigner.LoadTextObject(recogSpec, extractSpec, _text);
          }
        }

        MessageBox.Show("The configuration was saved to disk and to the in-memory collections.", "Pdf Text Extractor", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to save the configuration data from Configuration Editor tool window." + g.crlf2 +
                        "The attempt to write to file '" + fullFilePath + "' failed - see exception report below." + g.crlf2 + ex.ToReport(),
                        "PdfTextExtract - Configuration Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }


    }

    private void InitializeForm()
    {
      try
      {
        a = new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the creation of the application object 'a'." + g.crlf2 + ex.ToReport(),
                        "PdfTextExtract - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      try
      {
        int formHorizontalSize = g.GetCI("MainFormHorizontalSize").ToInt32OrDefault(90);
        int formVerticalSize = g.GetCI("MainFormVerticalSize").ToInt32OrDefault(90);

        this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * formHorizontalSize / 100,
                             Screen.PrimaryScreen.Bounds.Height * formVerticalSize / 100);
        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                  Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2);


        mnuFileSaveAs.Enabled = false;

        _extractSpecIncludes = g.GetList("ExtractSpecIncludes");
        if (_extractSpecIncludes.Count == 1 && _extractSpecIncludes[0] == "*")
          _extractSpecIncludes.Clear();

        _formats = new SortedList<string, bool>();
        _formats.Add("UNDEFINED", true);
        RefreshFormatsListBox();

        _filesGridUpdating = false;
        _allowDebugBreak = g.CI("AllowDebugBreak").ToBoolean();

        _bgClearStyle = new fctb.MarkerStyle(Brushes.White);
        _bgHighlightStyle = new fctb.MarkerStyle(Brushes.LightSteelBlue);
        _rangesToClear = new List<fctb.Range>();

        _assignedMap = g.CI("AssignedMap");
        _mapsFolder = g.CI("MapsFolder");

        _runPostLoadCommands = g.CI("RunPostLoadCommands").ToBoolean();
        _postLoadCommands = g.GetList("PostLoadCommands");

        btnLoadFiles.Enabled = false;

        LoadRootFolderCombo();
        LoadFileNameFilters();

        InitializeGrid();
        InitializeToolWindowForms();

        LoadRecogSpecSet();
        LoadExtractSpecSet();
        LoadColumnIndexMap();
        Tsd.ColumnIndexMap = _ciMap;

        _tabPageOrder = new List<string>();
        foreach (TabPage tabPage in tabMain.TabPages)
        {
          if (tabPage.Tag != null)
            _tabPageOrder.Add(tabPage.Tag.ToString().Replace("TabPage_", String.Empty));
        }

        btnRecognizeFormats.Enabled = false;

      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization of the application." + g.crlf2 + ex.ToReport(),
                        "PdfTextExtract - Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadColumnIndexMap()
    {
      var xml = XElement.Parse(File.ReadAllText(_mapsFolder + @"\ColumnIndexMap.xml"));

      using (var f = new ObjectFactory2())
      {
        _ciMap = f.Deserialize(xml) as ColumnIndexMap;
      }
    }

    private void ReloadTextProcessingConfigs()
    {
      LoadRecogSpecSet();
      LoadExtractSpecSet();
      LoadColumnIndexMap();
      Tsd.ColumnIndexMap = _ciMap;
    }

    private void RecognizeFormats()
    {
      this.Cursor = Cursors.WaitCursor;

      var processTimer = new ProcessTimer();

      try
      {
        for (int i = 0; i < gvFiles.Rows.Count; i++)
        {
          var r = gvFiles.Rows[i];

          string assignedFormat = r.Cells["FileFormat"].Value.ToString().ToUpper();
          if (assignedFormat != "UNDEFINED")
            continue;

          string fullFilePath = r.Cells["FilePath"].Value.ToString() + @"\" + r.Cells["FileName"].Value.ToString();

          using (var textExtractor = new TextExtractor())
          {
            if (Path.GetExtension(fullFilePath).ToUpper() != ".PDF")
              continue;

            var text = textExtractor.ExtractTextFromPdf(_allowDebugBreak, fullFilePath);
            string formatName = _recogSpecSet.RecognizeFormat(text.RawText);

            r.Cells["FileFormat"].Value = formatName;
            string fileName = r.Cells["FileName"].Value.ToString().Trim();
            string filePath = r.Cells["FilePath"].Value.ToString().Trim();

            lblStatus.Text =  "Processed " + (i + 1).ToString("#,##0") + " of " + gvFiles.Rows.Count.ToString("###,##0") + " in " +
                              processTimer.SecondsSoFar().Value.ToString("#,##0.000") + " seconds - " + fullFilePath;
            Application.DoEvents();

            if (formatName != "UNDEFINED" && !_fileFormatSet.ContainsKey(formatName))
              _fileFormatSet.Add(formatName, new FileFormat());

            if (_fileFormatSet.ContainsKey("UNDEFINED"))
            {
              string fileFullPath = filePath.ToLower() + @"\" + fileName.ToLower();

              var undefinedFormat = _fileFormatSet["UNDEFINED"];
              int undefinedIndex = -1;
              for (int j = 0; j < undefinedFormat.Files.Count; j++)
              {
                var undefinedFile = undefinedFormat.Files[j];
                if (undefinedFile.FullPath.ToLower() == fileFullPath)
                {
                  undefinedIndex = j;
                  break;
                }
              }

              if (undefinedIndex > -1)
              {
                var fileToMove = undefinedFormat.Files[undefinedIndex];
                undefinedFormat.Files.RemoveAt(undefinedIndex);
                _fileFormatSet[formatName].Files.Add(fileToMove);
              }
            }

            if (!_formats.ContainsKey(formatName))
            {
              _formatsListBoxUpdating = true;
              _formats.Add(formatName, true);
              RefreshFormatsListBox();
              Application.DoEvents();
              _formatsListBoxUpdating = false;
            }

            int displayedRowCount = gvFiles.DisplayedRowCount(true);
            int topRowIndex = gvFiles.FirstDisplayedScrollingRowIndex;
            int bottomRowIndex = topRowIndex + displayedRowCount;

            if (i > bottomRowIndex)
            {
              int rowIndexToMakeVisible = bottomRowIndex + 5;
              if (rowIndexToMakeVisible > gvFiles.Rows.Count - 1)
                rowIndexToMakeVisible = gvFiles.Rows.Count - 1;
              rowIndexToMakeVisible = rowIndexToMakeVisible - displayedRowCount;
              gvFiles.FirstDisplayedScrollingRowIndex = rowIndexToMakeVisible;
              Application.DoEvents();
            }

          }
        }

        if (gvFiles.Rows.Count > 10)
          MessageBox.Show("Recognition process is complete.", "PdfTextExtract", MessageBoxButtons.OK, MessageBoxIcon.Information);

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to recognize file formats." + g.crlf2 + ex.ToReport(),
                        "PdfTextExtract - File Format Recognition Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void InitializeGrid()
    {
      gvFiles.Columns.Clear();

      DataGridViewColumn col = new DataGridViewTextBoxColumn();
      col.Name = "Count";
      col.HeaderText = "Count";
      col.Width = 60;
      col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gvFiles.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "FileName";
      col.HeaderText = "File Name";
      col.Width = 500;
      gvFiles.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "FileFormat";
      col.HeaderText = "File Format";
      col.Width = 120;
      gvFiles.Columns.Add(col);

      col = new DataGridViewTextBoxColumn();
      col.Name = "FilePath";
      col.HeaderText = "File Path";
      col.Width = 750;
      col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      gvFiles.Columns.Add(col);
    }

    private void LoadRecogSpecSet()
    {
      try
      {
        _recogSpecSet = new RecogSpecSet();
        var recogSpecFiles = Directory.GetFiles(_mapsFolder, "*.rcg").ToList();

        using (var f = new ObjectFactory2())
        {
          foreach (var recogSpecFile in recogSpecFiles)
          {
            var xml = XElement.Parse(File.ReadAllText(recogSpecFile));
            var recogSpecSet = f.Deserialize(xml, true) as RecogSpecSet;
            foreach (var kvp in recogSpecSet)
            {
              if (_recogSpecSet.ContainsKey(kvp.Key))
                throw new Exception("RecogSpec named '" + kvp.Key + "' already exists in the RecogSpecSet.  RecogSpec names " +
                                    "must be unique across all files imported.  The duplicate name exists in the file '" + recogSpecFile + "'.");
              _recogSpecSet.Add(kvp.Key, kvp.Value);
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to load the FormatSpecSet.", ex);
      }
    }

    private void LoadExtractSpecSet()
    {
      string fileBeingProcessed = String.Empty;

      try
      {
        _extractSpecSet = new ExtractSpecSet();
        var extractSpecFiles = Directory.GetFiles(_mapsFolder, "*.ext").ToList();
        extractSpecFiles.AddRange(Directory.GetFiles(_mapsFolder, "*.map").ToList());

        using (var f = new ObjectFactory2())
        {
          foreach (var extractSpecFile in extractSpecFiles)
          {
            bool includeFile = true;

            if (_extractSpecIncludes.Count > 0)
            {
              includeFile = false;
              string fileName = Path.GetFileName(extractSpecFile).ToLower().Trim();

              foreach (var includeFileName in _extractSpecIncludes)
              {
                if (fileName == includeFileName.ToLower().Trim())
                {
                  includeFile = true;
                  break;
                }
              }
            }

            if (!includeFile)
              continue;

            fileBeingProcessed = extractSpecFile;

            var xml = XElement.Parse(File.ReadAllText(extractSpecFile));

            if (xml.Name.LocalName != "ExtractSpecSet")
              continue;

            var extractSpecSet = f.Deserialize(xml, true) as ExtractSpecSet;

            if (extractSpecSet != null)
            {
              foreach (var kvp in extractSpecSet)
              {
                if (_extractSpecSet.ContainsKey(kvp.Key))
                  throw new Exception("ExtractSpec named '" + kvp.Key + "' already exists in the ExtractSpecSet.  ExtractSpec names " +
                                      "must be unique across all files imported.  The duplicate name exists in the file '" + extractSpecFile + "'.");
                kvp.Value.FullFilePath = extractSpecFile;
                _extractSpecSet.Add(kvp.Key, kvp.Value);
              }
            }
            else
            {
              throw new Exception("The ExtractSpecSet file (PDF map) named '" + fileBeingProcessed + "' was not successfullyl deserialized " +
                                  "into an ExtractSpecSet object.");
            }
          }
        }

        _extractSpecSet.PopulateReferences();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to load the ExtractSpecSet.", ex);
      }
    }


    private void LoadRootFolderCombo()
    {
      var folderList = g.GetList("RootFolders");

      cboRootFolder.Items.Clear();

      foreach (var folder in folderList)
      {
        cboRootFolder.Items.Add(folder);
      }

      if (cboRootFolder.Items.Count == 1)
        cboRootFolder.SelectedIndex = 0;

      string selectedRootFolder = g.CI("SelectedRootFolder");
      if (selectedRootFolder.IsNotBlank())
      {
        for (int i = 0; i < cboRootFolder.Items.Count; i++)
        {
          string itemText = cboRootFolder.Items[i].ToString();
          if (itemText == selectedRootFolder)
          {
            cboRootFolder.SelectedIndex = i;
            break;
          }
        }
      }

    }

    private void LoadFileNameFilters()
    {
      var filterList = g.GetList("FileNameFilters");

      cboFileNameFilters.Items.Clear();
      cboFileNameFilters.Items.Add(String.Empty);

      foreach (var filter in filterList)
      {
        if (filter.Trim() == "*")
          continue;

        cboFileNameFilters.Items.Add(filter);
      }

      cboFileNameFilters.SelectedIndex = 0;

      string selectedFileNameFilter = g.CI("SelectedFileNameFilter");
      if (selectedFileNameFilter.IsNotBlank())
      {
        for (int i = 0; i < cboFileNameFilters.Items.Count; i++)
        {
          string itemText = cboFileNameFilters.Items[i].ToString();
          if (itemText == selectedFileNameFilter)
          {
            cboFileNameFilters.SelectedIndex = i;
            break;
          }
        }
      }
    }

    private void RefreshFormatsListBox()
    {
      _formatsListBoxUpdating = true;

      ckListFormats.Items.Clear();

      foreach (var kvp in _formats)
      {
        ckListFormats.Items.Add(kvp.Key, kvp.Value);
      }

      _formatsListBoxUpdating = false;
    }


    private void FormatControls_Changed(object sender, EventArgs e)
    {
      if (_formatsListBoxUpdating)
        return;

      LoadFilteredList();
    }

    private void ckListFormats_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      if (_formatsListBoxUpdating)
        return;

      bool undefinedOnly = true;
      foreach (var item in ckListFormats.CheckedItems)
      {
        if (item.ToString() != "UNDEFINED")
        {
          undefinedOnly = false;
          break;
        }
      }

      ckUndefinedOnly.Checked = !undefinedOnly;
      LoadFilteredList();
    }

    private void SetUndefinedFormatOnly()
    {
      if (ckUndefinedOnly.Checked)
      {
        for (int i = 0; i < _formats.Count; i++)
        {
          if (_formats.Keys[i] == "UNDEFINED")
            _formats[_formats.Keys[i]] = true;
          else
            _formats[_formats.Keys[i]] = false;
        }
        RefreshFormatsListBox();
        LoadFilteredList();
      }
    }

    private void cboFileNameFilters_SelectedIndexChanged(object sender, EventArgs e)
    {
      LoadFilteredList();
    }

    private void ManageBreakpoints()
    {
      if (_twMgr == null || _twMgr.ToolPanels == null || !_twMgr.ToolPanels.ContainsKey("TextExtractDesigner"))
        return;

      TextExtractDesigner td = (TextExtractDesigner)_twMgr.ToolPanels["TextExtractDesigner"];
      td.ManageBreakpoints(ckBreakpointEnabled.Checked, ckKeepBreakpointEnabled.Checked);
    }

    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      try
      {
        if (g.AppConfig.IsUpdated)
          g.AppConfig.Save();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred attempting to save the updated AppConfig file." + g.crlf2 +
                        "Exception Message:" + ex.ToReport(), "PdfTextExtract - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      try
      {
        if (_twMgr != null)
          _twMgr.MarkTimeToClose();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while closing the main form and child forms." + g.crlf2 +
                        "Exception Message:" + ex.ToReport(), "PdfTextExtract - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void TerminateProgram()
    {
      this.Close();
    }

    #region Tool Panel Processing


    private void InitializeToolWindowForms()
    {
      int secondScreenWidth = 0;
      int thirdScreenWidth = 0;

      Rectangle primaryScreenRectangle = new Rectangle(new Point(0, 0), Screen.PrimaryScreen.Bounds.Size);

      if (Screen.AllScreens.Count() > 1)
        secondScreenWidth = Screen.AllScreens[1].Bounds.Width;

      if (Screen.AllScreens.Count() > 2)
        thirdScreenWidth = Screen.AllScreens[2].Bounds.Width;

      Rectangle totalScreenArea =
        new Rectangle(new Point(0, 0), new Size(primaryScreenRectangle.Width + secondScreenWidth + thirdScreenWidth, primaryScreenRectangle.Height));

      List<string> splitterPanelsManaged = new List<string>();

      Point initialLocation = new Point(650, 250);

      var dockedPanels = new Dictionary<string, Panel>();
      foreach(TabPage tabPage in tabMain.TabPages)
      {
        foreach(Control control in tabPage.Controls)
        {
          if (control.GetType().Name == "Panel")
          {
            if (control.Tag != null && control.Tag.ToString().StartsWith("DockTarget_"))
            {
              string dockedPanelKey = control.Tag.ToString().Trim();
              if (dockedPanels.ContainsKey(dockedPanelKey))
              {
                MessageBox.Show("A duplicate tag values exists among the docked panels which are children of the tabPages included in tabMain." + g.crlf2 +
                                "The duplicate tag value is '" + dockedPanelKey + "'." + g.crlf2 +
                                "Please correct so that each docked panel has a unique tag value.",
                                "PdfTextExtract - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
              }

              dockedPanels.Add(dockedPanelKey, (Panel) control);
            }
          }
        }
      }

      _twMgr = new ToolWindowManager();
      _uiState = g.AppConfig.ProgramConfigSet[g.AppConfig.ConfigName].UIState;

      foreach (UIWindow uiWindow in _uiState.UIWindowSet.Values)
      {
        if (uiWindow.IsMainForm)
        {
          MainFormHelper.ManageInitialSize(this, uiWindow);
          continue;
        }

        string toolName = uiWindow.Name.Trim();

        if (_twMgr.ToolWindowComponentsSet.ContainsKey(toolName))
          throw new Exception("A duplicate name exists in the set of UIWindow configurations in the AppConfig file. The duplicate name is '" + toolName + "'.");

        if (!dockedPanels.ContainsKey("DockTarget_" + toolName))
          throw new Exception("No panel has been designated for docking the tool window control named '" + toolName + "'.");

        var twComponents = new ToolWindowComponents();
        twComponents.Name = toolName;

        switch (uiWindow.TypeName)
        {
          case "PdfViewer":
            twComponents.ToolPanel = new PdfViewerControl();
            break;

          case "RichTextViewer":
            twComponents.ToolPanel = new RichTextViewer();
            RichTextViewer rtv = (RichTextViewer)twComponents.ToolPanel;
            AddButtonsToToolWindow(uiWindow.Name, rtv);
            break;

          case "TextExtractDesigner":
            twComponents.ToolPanel = new TextExtractDesigner();
            break;

            // other types in Org.TW may be possible - implement later, or as needed.
        }

        twComponents.ToolWindow = new frmToolWindowBase(this, uiWindow.WindowTitle);
        twComponents.ToolWindow.Owner = this;
        twComponents.ToolWindow.Tag = "ToolWindow_" + toolName;
        twComponents.ToolWindow.ToolAction += ToolWindow_ToolAction;
        twComponents.ToolPanel.Tag = "ToolPanel_" + toolName;
        twComponents.ToolPanel.NotifyHostEvent += ToolPanel_NotifyHostEvent;
        twComponents.ToolPanel.DockButton.Click += Action;
        twComponents.FloatedTarget = twComponents.ToolWindow.DockPanel;
        twComponents.DockedTarget = dockedPanels["DockTarget_" + toolName];
        _twMgr.ToolWindowComponentsSet.Add(toolName, twComponents);

        if (uiWindow.WindowLocation.IsDocked)
        {
          twComponents.ToolWindow.Visible = false;
          twComponents.ToolWindow.DockPanel.Controls.Remove(twComponents.ToolPanel);
          twComponents.DockedTarget.Controls.Clear();
          twComponents.DockedTarget.Controls.Add(twComponents.ToolPanel);
          twComponents.ToolPanel.Dock = DockStyle.Fill;
        }
        else
        {
          Point defaultLocation = new Point(200, 200);
          twComponents.FloatedTarget.Controls.Clear();
          Size tpSize = twComponents.ToolPanel.Size;
          twComponents.FloatedTarget.Controls.Add(twComponents.ToolPanel);
          twComponents.ToolPanel.Dock = DockStyle.Fill;
          if (twComponents.ToolWindow.FormBorderStyle == System.Windows.Forms.FormBorderStyle.FixedToolWindow)
            twComponents.ToolWindow.Size = tpSize.Inflate(14, 14);
          else
            twComponents.ToolWindow.Size = uiWindow.WindowLocation.Size.ToSize();

          Rectangle toolWindowRectangle = new Rectangle(uiWindow.WindowLocation.Location, twComponents.ToolWindow.Size);
          Rectangle visibleOverlap = totalScreenArea;
          visibleOverlap.Intersect(toolWindowRectangle);

          if (visibleOverlap.IsEmpty)
          {
            uiWindow.WindowLocation.Location = defaultLocation;
            defaultLocation.Offset(50, 50);
          }

          twComponents.ToolWindow.Location = uiWindow.WindowLocation.Location;
          twComponents.ToolWindow.Visible = uiWindow.WindowLocation.IsVisible;

          switch (toolName)
          {

            case "PdfViewer":
              tabMain.TabPages.Remove(tabPagePdfViewer);
              break;

            case "RawExtractedText":
              tabMain.TabPages.Remove(tabPageRawExtractedText);
              break;

            case "TextExtractDesigner":
              tabMain.TabPages.Remove(tabPageTextExtractDesigner);
              break;

            case "TextExtractResults":
              tabMain.TabPages.Remove(tabPageTextExtractResults);
              break;
          }
        }
      }

      MainFormHelper.InitializeUIState(this, _twMgr);
      base.SetToolWindows(_twMgr);
    }

    private void AddButtonsToToolWindow(string name, RichTextViewer rtv)
    {
      switch (name)
      {
        case "RawExtractedText":
          rtv.TopPanel.Height = 50;
          Button btnFindPatterns = new Button();
          btnFindPatterns.Name = "btnFindPatterns";
          btnFindPatterns.Tag = "FindPatterns";
          btnFindPatterns.Text = "Find Patterns";
          btnFindPatterns.Click += Action;
          rtv.TopPanel.Controls.Add(btnFindPatterns);
          btnFindPatterns.Size = new Size(100, 23);
          btnFindPatterns.Location = new Point(15, 5);
          break;

        case "FormatRecognition":
          break;

        case "ConfigEdit":
          rtv.TopPanel.Height = 35;
          Button btnSaveConfig = new Button();
          btnSaveConfig.Name = "btnSaveConfig";
          btnSaveConfig.Tag = "SaveConfig";
          btnSaveConfig.Text = "Save Config";
          btnSaveConfig.Click += Action;
          rtv.TopPanel.Controls.Add(btnSaveConfig);
          btnSaveConfig.Size = new Size(100, 23);
          btnSaveConfig.Location = new Point(15, 5);

          Label lblConfigType = new Label();
          lblConfigType.Name = "lblConfigType";
          lblConfigType.AutoSize = false;
          rtv.TopPanel.Controls.Add(lblConfigType);
          lblConfigType.Size = new Size(100, 23);
          lblConfigType.Location = new Point(130, 5);
          lblConfigType.Text = String.Empty;

          Label lblFullXmlPath = new Label();
          lblFullXmlPath.AutoSize = false;
          lblFullXmlPath.Name = "lblFullXmlPath";
          rtv.TopPanel.Controls.Add(lblFullXmlPath);
          lblFullXmlPath.Size = new Size(300, 23);
          lblFullXmlPath.Location = new Point(250, 5);
          lblFullXmlPath.Text = String.Empty;

          Label lblFullFilePath = new Label();
          lblFullFilePath.Name = "lblFullFilePath";
          lblFullFilePath.AutoSize = false;
          rtv.TopPanel.Controls.Add(lblFullFilePath);
          lblFullFilePath.Size = new Size(400, 23);
          lblFullFilePath.Location = new Point(15, 30);
          lblFullFilePath.Text = String.Empty;
          break;
      }

    }

    private void ToolPanel_NotifyHostEvent(ToolPanelNotifyEvent e)
    {
      if (e.Sender == null || e.Sender.Tag == null)
        return;

      string tag = e.Sender.Tag.ToString();
      if (tag.IsBlank())
        return;

      string command = e.Command.ToString();
      string toolWindowAction = "TW_" + command + "_" + tag.Replace("ToolPanel_", String.Empty);


      ToolWindowAction(toolWindowAction, e.ToolPanelUpdateParms);
    }

    private void ToolWindowAction(string action, ToolPanelUpdateParms parms = null)
    {
      string[] tokens = action.Split('_');

      if (tokens.Length != 3)
        return;

      if (tokens[0] != "TW")
        return;

      string toolWindowAction = tokens[1];

      if (toolWindowAction == "ReloadConfigs")
      {
        ReloadTextProcessingConfigs();
        ProcessFile();
        return;
      }

      string toolWindowTarget = tokens[2];

      List<string> toolTargets = new List<string>();

      if (toolWindowTarget == "All")
      {
        toolTargets.Add("PdfViewer");
        toolTargets.Add("RawExtractedText");
        toolTargets.Add("FormatRecognition");
        toolTargets.Add("ConfigEdit");
        toolTargets.Add("TextExtractDesigner");
        toolTargets.Add("TextExtractResults");
      }
      else
      {
        toolTargets.Add(toolWindowTarget);
      }

      foreach (string toolTarget in toolTargets)
      {
        if (!_uiState.UIWindowSet.ContainsKey(toolTarget))
          return;

        UIWindow uiWindow = _uiState.UIWindowSet[toolTarget];

        if (_twMgr == null || _twMgr.ToolWindowComponentsSet == null || !_twMgr.ToolWindowComponentsSet.ContainsKey(toolTarget))
          return;

        var twc = _twMgr.ToolWindowComponentsSet[toolTarget];

        string dockingTargetName = "DockedTarget_" + toolTarget;

        switch (toolWindowAction)
        {
          case "Dock":
            if (twc.DockedTarget != null)
            {
              twc.ToolWindow.Visible = false;
              twc.ToolWindow.DockPanel.Controls.Remove(twc.ToolPanel);
              twc.DockedTarget.Controls.Clear();
              twc.DockedTarget.Controls.Add(twc.ToolPanel);
              twc.ToolPanel.Dock = DockStyle.Fill;
              uiWindow.WindowLocation.IsDocked = true;

              int tabInsertIndex = GetTabInsertIndex(toolTarget);

              switch (toolTarget)
              {
                case "PdfViewer":
                  if (!tabMain.TabPages.Contains(tabPagePdfViewer))
                    tabMain.TabPages.Insert(tabInsertIndex, tabPagePdfViewer);
                  tabMain.SelectedTab = tabPagePdfViewer;
                  tabPagePdfViewer.Focus();
                  break;

                case "RawExtractedText":
                  if (!tabMain.TabPages.Contains(tabPageRawExtractedText))
                    tabMain.TabPages.Insert(tabInsertIndex, tabPageRawExtractedText);
                  tabMain.SelectedTab = tabPageRawExtractedText;
                  tabPageRawExtractedText.Focus();
                  break;

                case "FormatRecognition":
                  if (!tabMain.TabPages.Contains(tabPageFormatRecognition))
                    tabMain.TabPages.Insert(tabInsertIndex, tabPageFormatRecognition);
                  tabMain.SelectedTab = tabPageFormatRecognition;
                  tabPageRawExtractedText.Focus();
                  break;

                case "ConfigEdit":
                  if (!tabMain.TabPages.Contains(tabPageConfigEdit))
                    tabMain.TabPages.Insert(tabInsertIndex, tabPageConfigEdit);
                  tabMain.SelectedTab = tabPageConfigEdit;
                  tabPageRawExtractedText.Focus();
                  break;

                case "TextExtractDesigner":
                  if (!tabMain.TabPages.Contains(tabPageTextExtractDesigner))
                    tabMain.TabPages.Insert(tabInsertIndex, tabPageTextExtractDesigner);
                  tabMain.SelectedTab = tabPageTextExtractDesigner;
                  tabPageTextExtractDesigner.Focus();
                  break;

                case "TextExtractResults":
                  if (!tabMain.TabPages.Contains(tabPageTextExtractResults))
                    tabMain.TabPages.Insert(tabInsertIndex, tabPageTextExtractResults);
                  tabMain.SelectedTab = tabPageTextExtractResults;
                  tabPageTextExtractResults.Focus();
                  break;
              }

              twc.ToolPanel.UpdateDockButtonTagAndText();
              break;
            }
            else
            {
              if (twc.ToolWindow.Visible)
                twc.ToolWindow.Visible = false;
            }
            break;

          case "Float":
            if (twc.FloatedTarget != null)
            {
              twc.DockedTarget.Controls.Remove(twc.ToolPanel);
              twc.ToolWindow.DockPanel.Controls.Clear();
              twc.ToolWindow.DockPanel.Controls.Add(twc.ToolPanel);
              twc.ToolWindow.Location = uiWindow.WindowLocation.Location;
              twc.ToolWindow.Size = uiWindow.WindowLocation.Size.ToSize();
              twc.ToolPanel.Dock = DockStyle.Fill;
              uiWindow.WindowLocation.IsDocked = false;

              switch (toolTarget)
              {

                case "PdfViewer":
                  if (tabMain.TabPages.Contains(tabPagePdfViewer))
                    tabMain.TabPages.Remove(tabPagePdfViewer);
                  break;

                case "RawExtractedText":
                  if (tabMain.TabPages.Contains(tabPageRawExtractedText))
                    tabMain.TabPages.Remove(tabPageRawExtractedText);
                  break;

                case "FormatRecognition":
                  if (tabMain.TabPages.Contains(tabPageFormatRecognition))
                    tabMain.TabPages.Remove(tabPageFormatRecognition);
                  break;

                case "ConfigEdit":
                  if (tabMain.TabPages.Contains(tabPageConfigEdit))
                    tabMain.TabPages.Remove(tabPageConfigEdit);
                  break;

                case "TextExtractDesigner":
                  if (tabMain.TabPages.Contains(tabPageTextExtractDesigner))
                    tabMain.TabPages.Remove(tabPageTextExtractDesigner);
                  break;

                case "TextExtractResults":
                  if (tabMain.TabPages.Contains(tabPageTextExtractResults))
                    tabMain.TabPages.Remove(tabPageTextExtractResults);
                  break;
              }

              twc.ToolWindow.Visible = true;
            }
            else
            {
              if (!twc.ToolWindow.Visible)
                twc.ToolWindow.Visible = true;
            }
            twc.ToolPanel.UpdateDockButtonTagAndText();
            break;

          case "Hide":
            twc.ToolWindow.Visible = false;
            uiWindow.WindowLocation.IsVisible = twc.ToolWindow.Visible;
            break;

          case "Show":
            twc.ToolWindow.Visible = true;
            uiWindow.WindowLocation.IsVisible = twc.ToolWindow.Visible;
            break;

          case "Toggle":
            twc.ToolWindow.Visible = !twc.ToolWindow.Visible;
            uiWindow.WindowLocation.IsVisible = twc.ToolWindow.Visible;
            break;

          case "UpdateToolWindow":
            if (parms == null || parms.ToolPanelName.IsBlank())
              return;

            if (_twMgr == null || _twMgr.ToolPanels == null || !_twMgr.ToolPanels.ContainsKey(parms.ToolPanelName))
              return;

            var targetToolPanel = _twMgr.ToolPanels[parms.ToolPanelName];
            ExecuteToolWindowCommand(targetToolPanel, parms);
            break;
        }
      }
    }

    private void ExecuteToolWindowCommand(ToolPanelBase toolPanel, ToolPanelUpdateParms parms)
    {
      if (toolPanel.Tag == null)
        return;

      string toolPanelTag = toolPanel.Tag.ToString().Replace("ToolPanel_", String.Empty);
      string command = parms.Command;

      mnuFileSaveAs.Enabled = false;

      switch (toolPanelTag)
      {
        case "ConfigEdit":
          var configEditToolPanel = toolPanel as RichTextViewer;
          string path = parms.ConfigFullXmlPath;

          switch (command)
          {
            case "LoadTsd":
              string[] pathTokens = path.Split(Constants.BSlashDelimiter, StringSplitOptions.RemoveEmptyEntries);

              switch (parms.TextConfigType)
              {
                case TextConfigType.TextStructureAndExtract:
                  if (_extractSpecSet == null || _extractSpecSet.Count == 0)
                    return;

                  Label lblConfigType = null;
                  Label lblFullXmlPath = null;
                  Label lblFullFilePath = null;

                  if (pathTokens.Length == 1 && pathTokens[0].StartsWith("root:"))
                  {
                    var configName = pathTokens[0].Replace("root:", String.Empty);
                    string config = "Configuration '" + configName + "' not found.";
                    if (_extractSpecSet.ContainsKey(configName))
                    {
                      var spec = _extractSpecSet[configName];
                      var f2 = new ObjectFactory2();
                      var xml = f2.Serialize(spec);
                      config = xml.ToString();
                    }

                    configEditToolPanel.Text = config;

                    lblConfigType = configEditToolPanel.GetTopPanelControl("lblConfigType") as Label;
                    if (lblConfigType != null)
                    {
                      configEditToolPanel.TopPanel.Height = 60;
                      configEditToolPanel.TopPanel.BringToFront();
                      configEditToolPanel.Controls["txtData"].BringToFront();
                      lblConfigType.Width = 250;
                      lblConfigType.Text = "Config Type:   " + parms.TextConfigType.ToString();
                    }

                    lblFullXmlPath = configEditToolPanel.GetTopPanelControl("lblFullXmlPath") as Label;
                    if (lblFullXmlPath != null)
                    {
                      lblFullXmlPath.Left = 450;
                      lblFullXmlPath.Width = 300;
                      lblFullXmlPath.Text = "Full XML Path:   " + parms.ConfigFullXmlPath;
                    }

                    lblFullFilePath = configEditToolPanel.GetTopPanelControl("lblFullFilePath") as Label;
                    if (lblFullFilePath != null)
                    {
                      lblFullFilePath.Left = lblConfigType.Left;
                      lblFullFilePath.Width = 1100;
                      lblFullFilePath.Text = "Config File Path:   " + parms.ConfigFileFullPath;
                    }

                    return;
                  }


                  string specName = pathTokens[0];
                  if (!_extractSpecSet.ContainsKey(specName))
                    return;
                  var extractSpec = _extractSpecSet[specName];
                  if (extractSpec.Count == 0)
                    return;

                  if (pathTokens.Length < 2)
                    return;

                  string topTsdName = pathTokens[1];
                  if (!extractSpec.ContainsKey(topTsdName))
                    return;

                  Tsd tsd = extractSpec[topTsdName];

                  int level = 1;

                  while (pathTokens.Length - 1 > level)
                  {
                    level++;
                    string tsdName = pathTokens[level];
                    if (tsd.TsdSet.ContainsKey(tsdName))
                      tsd = tsd.TsdSet[tsdName];
                  }

                  if (tsd == null)
                    return;

                  var f = new ObjectFactory2();
                  var tsdXml = f.Serialize(tsd);

                  if (tsdXml.Element("TsdSet") != null)
                  {
                    var tsdSet = tsdXml.Element("TsdSet");
                    tsdSet.Remove();
                  }

                  string tsdString = tsdXml.ToString();
                  configEditToolPanel.Text = tsdString;

                  lblConfigType = configEditToolPanel.GetTopPanelControl("lblConfigType") as Label;
                  if (lblConfigType != null)
                  {
                    configEditToolPanel.TopPanel.Height = 60;
                    configEditToolPanel.TopPanel.BringToFront();
                    configEditToolPanel.Controls["txtData"].BringToFront();
                    lblConfigType.Width = 250;
                    lblConfigType.Text = "Config Type:   " + parms.TextConfigType.ToString();
                  }

                  lblFullXmlPath = configEditToolPanel.GetTopPanelControl("lblFullXmlPath") as Label;
                  if (lblFullXmlPath != null)
                  {
                    lblFullXmlPath.Left = 450;
                    lblFullXmlPath.Width = 300;
                    lblFullXmlPath.Text = "Full XML Path:   " + parms.ConfigFullXmlPath;
                  }

                  lblFullFilePath = configEditToolPanel.GetTopPanelControl("lblFullFilePath") as Label;
                  if (lblFullFilePath != null)
                  {
                    lblFullFilePath.Left = lblConfigType.Left;
                    lblFullFilePath.Width = 1100;
                    lblFullFilePath.Text = "Config File Path:   " + parms.ConfigFileFullPath;
                  }

                  break;

              }

              break;


          }

          break;

        case "TextExtractResults":
          var textExtractResultsToolPanel = toolPanel as RichTextViewer;

          switch (command)
          {
            case "LoadResults":
              mnuFileSaveAs.Enabled = true;
              string results = _text.ExtractXml != null ? _text.ExtractXml.ToString() : "No results are available.";
              textExtractResultsToolPanel.Text = results;
              break;
          }
          break;

        case "TextExtractErrors":
          var textExtractErrorsToolPanel = toolPanel as RichTextViewer;

          switch (command)
          {
            case "LoadErrors":
              mnuFileSaveAs.Enabled = _text.ExtractionErrorReport.IsBlank();
              string results = _text.ExtractionErrorReport.IsNotBlank() ? _text.ExtractionErrorReport : "No error occurred in text extraction process.";
              textExtractErrorsToolPanel.Text = results;
              if (!parms.SuppressSwitchToErrorTab)
                tabMain.SelectedTab = tabPageTextExtractErrors;
              break;
          }
          break;
      }
    }

    private int GetTabInsertIndex(string tabName)
    {
      for (int i = 0; i < _tabPageOrder.Count; i++)
      {
        if (_tabPageOrder[i] == tabName)
          return i;
      }

      return 0;
    }


    public void ToolWindow_NotifyHostEvent(ToolPanelNotifyEvent e)
    {
      if (e == null)
        return;
    }

    private void ToolWindow_ToolAction(ToolActionEventArgs e)
    {
      if (e == null)
        return;

      frmToolWindowBase toolWindow = e.ToolWindow as frmToolWindowBase;
      string name = toolWindow.Tag.ToString().Replace("ToolWindow_", String.Empty);

      switch (e.ToolActionEvent)
      {
        case ToolActionEvent.ToolWindowMoved:
          _uiState.UIWindowSet[name].WindowLocation.Location = toolWindow.Location;
          break;

        case ToolActionEvent.ToolWindowResized:
          _uiState.UIWindowSet[name].WindowLocation.Size = toolWindow.Size;
          break;

        case ToolActionEvent.ToolWindowVisibleChanged:
          _uiState.UIWindowSet[name].WindowLocation.IsVisible = toolWindow.Visible;
          break;
      }
    }

    #endregion

    private void cboRootFolder_SelectedIndexChanged(object sender, EventArgs e)
    {
      btnLoadFiles.Enabled = cboRootFolder.Text.IsNotBlank();
    }

    private void tabMain_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (tabMain.SelectedTab.Tag == null)
        return;

      string selectedTab = tabMain.SelectedTab.Tag.ToString();

      switch (tabMain.SelectedTab.Tag.ToString())
      {
        case "TabPage_PdfFiles":
          break;

        case "TabPage_PdfViewer":
          ToolWindowAction("TW_Float_PdfViewer");
          break;

        case "TabPage_RawExtractedText":
          ToolWindowAction("TW_Float_RawExtractedText");
          break;

        case "TabPage_FormatRecognition":
          ToolWindowAction("TW_Float_FormatRecognition");
          break;

        case "TabPage_ConfigEdit":
          ToolWindowAction("TW_Float_ConfigEdit");
          break;

        case "TabPage_TextExtractDesigner":
          ToolWindowAction("TW_Float_TextExtractDesigner");
          break;

        case "TabPage_TextExtractResults":
          ToolWindowAction("TW_Float_TextExtractResults");
          break;

      }
    }

    private void gvFiles_MouseUp(object sender, MouseEventArgs e)
    {
      var hitTestInfo = gvFiles.HitTest(e.X, e.Y);

      if (hitTestInfo.RowIndex < 0)
        return;

      gvFiles.Rows[hitTestInfo.RowIndex].Selected = true;
    }

    private void ctxMnuFileGrid_Opening(object sender, CancelEventArgs e)
    {
      if (gvFiles.SelectedRows.Count < 1)
      {
        e.Cancel = true;
      }
    }

    private void gvFiles_MouseDown(object sender, MouseEventArgs e)
    {
      var hitTestInfo = gvFiles.HitTest(e.X, e.Y);

      if (hitTestInfo.RowIndex < 0)
        gvFiles.ClearSelection();
    }

    private void frmMain_Shown(object sender, EventArgs e)
    {
      if (!_isFirstShowing)
        return;

      this.Focus();
      this.BringToFront();

      RunPostLoadCommands();
      _isFirstShowing = false;
    }
  }
}
