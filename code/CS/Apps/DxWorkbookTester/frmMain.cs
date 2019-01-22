using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Reflection;
using Bus = Gulfport.Stmt.Business;
using Nb = Gulfport.NonOp.Business;
using Gulfport.NonOp.Tasks;
using Gulfport.MWStmt.Business;
using Gulfport.MWStmt.Tasks;
using Gulfport.AllocDec.Business;
using Org.TSK.Business;
using Org.TSK.Business.Models;
using Org.TP;
using Org.TP.Concrete;
using Org.Dx.Business;
using Org.Dx.Business.TextProcessing;
using Org.DxDocs;
using Org.WSO;
using Org.WSO.Transactions;
using Org.GS.Configuration;
using Org.GS;

namespace Org.DxWorkbookTester
{
  public partial class frmMain : Form
  {
    private a a;
    private ColumnIndexMap _ciMap;
    private string _overrideMapNameParameter;
    private string _mapPath;
    private ConfigDbSpec _taskConfigDbSpec;
    private ConfigDbSpec _stmtConfigDbSpec;
    private ConfigWsSpec _configWsSpec;
    private bool _isDryRun;
    private Nb.ManifestUtility _manifestUtility;

    private string _dbEnv;
    private string _wsEnv;
    private ParmSet _taskParms;
    private ScheduledTask _selectedScheduledTask;
    private string _selectedTaskName;
    private string _originalText;


    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void Action (object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "TestWebService":
          TestWebService();
          break;

        case "GetMap":
          GetMap();
          break;

        case "ShowParameters":
          ShowParameters();
          break;

        case "RunTask":
          RunTask();
          break;

        case "SaveOutputToFile":
          SaveOutputToFile();
          break;

        case "SplitExcelFile":
          SplitExcelFile();
          break;

        case "ClearMappedOutput":
          txtOut.Text = String.Empty;
          _originalText = String.Empty;
          txtOutputFilter.Clear();
          Application.DoEvents();
          break;

        case "ClearRawOutput":
          txtUnmappedFile.Text = String.Empty;
          Application.DoEvents();
          RunTask();
          break;

        case "Exit":
          Close();
          break;
      }
    }

    private async void RunTask()
    {
      this.Cursor = Cursors.WaitCursor;

      txtOut.Text = String.Empty;
      txtOutputFilter.Clear();
      _originalText = String.Empty;
      string wbText = String.Empty;

      g.Perf.Clear();

      Application.DoEvents();

      try
      {

        UpdateParameters();

        _stmtConfigDbSpec = g.GetDbSpec("Stmt");

        if (cboDxWorkbookSource.Text != "Use task processor")
        {
          var wb = GetDxWorkbook();

          if (wb == null)
          {
            wbText = "Target DxWorkbook object is null.";
          }
          else
          {
            using (var f = new ObjectFactory2())
            {
              XElement wbXml = f.Serialize(wb);
              wbText = wbXml.ToString();
            }
          }

          if (!ckRunDatabaseLoad.Checked)
          {
            this.Cursor = Cursors.Default;
            if (ckSuppressMapping.Checked)
            {
              txtOut.Text = wbText;
              _originalText = wbText;
              txtOut.SelectionStart = 0;
              txtOut.SelectionLength = 0;

              //txtUnmappedFile.Text = wbText;
              //tabMain.SelectedTab = tabPageUnMapped;
              //txtUnmappedFile.SelectionStart = 0;
              //txtUnmappedFile.SelectionLength = 0;
              txtRawReport.Text = wb == null ? "Raw DxWorkbook is null." : wb.Report;
              ckSuppressMapping.Checked = false;
            }
            else
            {
              txtOut.Text = wbText;
              _originalText = wbText;
              tabMain.SelectedTab = tabPageMapped;
              txtOut.SelectionStart = 0;
              txtOut.SelectionLength = 0;
            }
            return;
          }

          _taskParms.Add(new Parm("DxWorkbook", wb));
        }

        var tp = GetTaskProcessor(_selectedScheduledTask.ProcessorName);

        tp.TaskRequest = new TaskRequest(1, _selectedScheduledTask.TaskName, _selectedScheduledTask.ProcessorName, _selectedScheduledTask.ProcessorVersion,
                                         1, TaskRequestType.ScheduledTask, _taskParms, DateTime.Now, false, false, -3, -7200, false);

        var result = await tp.ProcessTaskAsync(null).ContinueWith(r => r);

        switch (result.Status)
        {
          case TaskStatus.RanToCompletion:
            var taskResult = result.Result;

            switch (taskResult.TaskResultStatus)
            {
              case TaskResultStatus.Success:
                InsertDummyRunHistory();
                this.Cursor = Cursors.Default;
                string topMessage = "TARGET WORKBOOK FOLLOWS";
                // if the task processor is used to get the workbook
                if (wbText.IsBlank() && taskResult.Object?.GetType().Name == "DxWorkbook")
                {
                  using (var f = new ObjectFactory2())
                  {
                    var wbXml = f.Serialize(taskResult.Object);
                    wbText = wbXml.ToString();
                  }
                  topMessage = "MAPPED WORKBOOK CREATED BY TASK PROCESSOR FOLLOWS";
                }
                string message = "The task '" + _selectedScheduledTask.TaskName + "' completed successfully.";
                txtOut.Text = message + g.crlf2 + topMessage + g.crlf2 + wbText;
                _originalText = txtOut.Text;
                txtOutputFilter.Clear();
                MessageBox.Show(message, "DxWorkbookTester - Task Completed Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                break;

              default:
                this.Cursor = Cursors.Default;
                string errorMessage = "The task '" + _selectedScheduledTask.TaskName + "' failed with TaskResult.Status = '" + taskResult.TaskResultStatus.ToString() +
                                      "'." + g.crlf2 + taskResult.Message + (taskResult.Exception == null ? String.Empty : taskResult.Exception.ToReport());
                MessageBox.Show(errorMessage, "DxWorkbook Tester - Task Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOut.Text = errorMessage;
                _originalText = txtOut.Text;
                txtOutputFilter.Clear();
                if (wbText.IsNotBlank())
                {
                  txtOut.Text += g.crlf2 + "TARGET WORKBOOK FOLLOWS" + g.crlf2 + wbText;
                  _originalText = txtOut.Text;
                  txtOutputFilter.Clear();
                }
                break;
            }
            break;

          default:
            this.Cursor = Cursors.Default;
            string faultMessage = "The TPL task for '" + _selectedScheduledTask.TaskName + "' was faulted - status is '" + result.Status.ToString() + "'.";
            MessageBox.Show(faultMessage, "DxWorkbookTester - TPL Task Faulted", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtOut.Text = faultMessage;
            _originalText = txtOut.Text;
            txtOutputFilter.Clear();
            if (wbText.IsNotBlank())
            {
              txtOut.Text += g.crlf2 + "TARGET WORKBOOK FOLLOWS" + g.crlf2 + wbText;
              _originalText = txtOut.Text;
              txtOutputFilter.Clear();
            }
            break;
        }
      }
      catch (CxException cx)
      {
        txtOut.Text = "An exception occurred while attempting to run the task." + g.crlf2 + cx.ToReport();
        _originalText = txtOut.Text;
        txtOutputFilter.Clear();
        tabMain.SelectedTab = tabPageMapped;
      }
      catch (Exception ex)
      {
        txtOut.Text = "An exception occurred while attempting to run the task." + g.crlf2 + ex.ToReport();
        _originalText = txtOut.Text;
        txtOutputFilter.Clear();
        tabMain.SelectedTab = tabPageMapped;
      }

      this.Cursor = Cursors.Default;
    }

    private void SaveOutputToFile()
    {
      if (txtOut.Text.IsBlank())
        return;

      if (txtOutputFilter.Text.IsNotBlank())
      {
        MessageBox.Show("Remove Output Filter before saving text.", "DxWorkbookTester - Remove Output Filter before Saving", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        return;
      }

      dlgSaveAs.Filter = "All files (*.*)|*.*";
      if (dlgSaveAs.ShowDialog() == DialogResult.OK)
      {
        File.WriteAllText(dlgSaveAs.FileName, txtOut.Text);
        lblStatus.Text = "Output saved to file '" + dlgSaveAs.FileName + "'";
      }
    }

    private void SplitExcelFile()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        UpdateParameters();

        var parmValue = _taskParms.GetParmValue("WorksheetsToInclude");
        List<string> sheetsToInclude = new List<string>();
        if (parmValue.IsNotBlank())
        {
          var sheets = parmValue as Dictionary<string, string>;
          foreach (var v in sheets)
          {
            sheetsToInclude.Add(v.Value);
          }
        }

        string inputFileFolder = ResolveInputFilePath();
        string inputFilePath = inputFileFolder.GetUniqueFile();

        if (txtMaxRows.Text.IsNotInteger() || txtMaxRows.Text.ToInt32() < 100 || txtMaxRows.Text.ToInt32() > 5000)
        {
          this.Cursor = Cursors.Default;
          txtMaxRows.SelectAll();
          txtMaxRows.Focus();
          MessageBox.Show("A numeric value from 100 to 5000 must be provided in the Max Rows textbox.",
                          g.AppInfo.AppName + " - File Split Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        int maxRows = txtMaxRows.Text.ToInt32();

        DxUtility.SplitWorkbook(inputFilePath, maxRows);

        this.Cursor = Cursors.Default;

        MessageBox.Show("File split operation complete.", "DxWorkbookTester - Split Excel File");
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to split the Excel file." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - File Split Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private ITaskProcessor GetTaskProcessor(string processorName)
    {
      switch (processorName)
      {
        case "MidstreamPopStmtExtractAndLoad":
          return new MidstreamPopStmtExtractAndLoad();

        case "NonOpProductionDataImport":
          return new NonOpProductionDataImport();

        case "EQTDecimalAllocExtractAndLoad":
          return new AllocDecimalLoad();

        case "TIKScheduleExtractAndLoad":
          return new TIKScheduleExtractAndLoad();

        case "NonOpAscentImport":
          return new NonOpAscentImport();
      }

      throw new Exception("Creation of TaskProcessor '" + processorName + "' has not yet been implemented.");
    }

    private void TestWebService()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        string inputFolder = ResolveInputFilePath();
        string[] inputFiles = Directory.GetFiles(inputFolder);
        string mapName = _taskParms.GetParmValue("MapName").ToString();

        if (mapName.IsBlank() && _overrideMapNameParameter.IsNotBlank())
          mapName = _overrideMapNameParameter;

        if (inputFiles.Length != 1)
        {
          txtOut.Text = "There are " + inputFiles.Length.ToString() + " files in the input file path '" +
                        inputFolder + ". Only one file at a time may be in the directory.";
          _originalText = txtOut.Text;
          txtOutputFilter.Clear();
          this.Cursor = Cursors.Default;
          return;
        }

        var f = new ObjectFactory2();

        DateTime dtBegin = DateTime.Now;

        string extractTransName = _taskParms.GetParmValue("ExtractTransName").ToString();
        var wsParms = BuildWsParms(extractTransName);
        wsParms.ParmSet.Add(new Parm("FullPath", inputFiles[0]));
        wsParms.ParmSet.Add(new Parm("MapName", mapName));

        var messageFactory = new Org.Dx.Business.MessageFactory();
        var requestMessage = messageFactory.CreateRequestMessage(wsParms);

        var responseMessage = WsClient.InvokeServiceCall(wsParms, requestMessage);

        switch (responseMessage.TransactionHeader.TransactionName)
        {
          case "ErrorResponse":
            ErrorResponse errorResponse = f.Deserialize(responseMessage.TransactionBody, true) as ErrorResponse;
            string errorResponseMessage = errorResponse.Message;
            txtOut.Text += errorResponse.HasException ? (g.crlf + errorResponse.WsException.ToReport()) : (g.crlf + "No exception" + g.crlf);
            _originalText = txtOut.Text;
            txtOutputFilter.Clear();
            break;

          case "ExcelExtract":
            ExcelExtractResponse excelExtractResponse = f.Deserialize(responseMessage.TransactionBody, true) as ExcelExtractResponse;
            txtOut.Text = responseMessage.TransactionBody.ToString();
            _originalText = txtOut.Text;
            txtOutputFilter.Clear();
            break;

          case "PdfExtract":
            PdfExtractResponse pdfExtractResponse = f.Deserialize(responseMessage.TransactionBody, true) as PdfExtractResponse;
            txtOut.Text = responseMessage.TransactionBody.ToString();
            _originalText = txtOut.Text;
            txtOutputFilter.Clear();
            break; ;
        }

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to test the GPFileDataExtract web service." + g.crlf2 +
                        ex.ToReport(), "DxWorkbook Tester - Web Service Test Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void GetMap()
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        txtOut.Text = String.Empty;
        _originalText = txtOut.Text;
        txtOutputFilter.Clear();
        Application.DoEvents();
        System.Threading.Thread.Sleep(50);

        string mapName = _taskParms.GetParmValue("MapName").ToString();

        if (mapName.IsBlank() && _overrideMapNameParameter.IsNotBlank())
          mapName = _overrideMapNameParameter;

        var f = new ObjectFactory2();

        DateTime dtBegin = DateTime.Now;

        string extractTransName = _taskParms.GetParmValue("ExtractTransName").ToString();
        if (extractTransName.IsBlank())
          extractTransName = "ExcelExtract";
        var wsParms = BuildWsParms("GetMap");
        wsParms.ParmSet.Add(new Parm("MapName", mapName));
        wsParms.ParmSet.Add(new Parm("ExtractTransName", extractTransName));

        var messageFactory = new Org.Dx.Business.MessageFactory();
        var requestMessage = messageFactory.CreateRequestMessage(wsParms);

        var responseMessage = WsClient.InvokeServiceCall(wsParms, requestMessage);

        switch (responseMessage.TransactionHeader.TransactionName)
        {
          case "ErrorResponse":
            ErrorResponse errorResponse = f.Deserialize(responseMessage.TransactionBody, true) as ErrorResponse;
            string errorResponseMessage = errorResponse.Message;
            txtOut.Text += errorResponse.HasException ? (g.crlf + errorResponse.WsException.ToReport()) : (g.crlf + "No exception" + g.crlf);
            _originalText = txtOut.Text;
            txtOutputFilter.Clear();
            break;

          default:
            GetMapResponse getMapResponse = f.Deserialize(responseMessage.TransactionBody, true) as GetMapResponse;
            txtOut.Text = getMapResponse.Map;
            _originalText = txtOut.Text;
            txtOutputFilter.Clear();
            break; ;
        }

        tabMain.SelectedTab = tabPageMapped;

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to test the GPFileDataExtract web service." + g.crlf2 +
                        ex.ToReport(), "DxWorkbook Tester - Web Service Test Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private WsParms BuildWsParms(string trans)
    {
      WsParms wsParms = new WsParms();
      wsParms.TransactionName = trans;
      wsParms.TransactionVersion = "1.0.0.0";
      wsParms.MessagingParticipant = MessagingParticipant.Sender;
      wsParms.ConfigWsSpec = _configWsSpec;
      wsParms.TrackPerformance = false;

      wsParms.DomainName = g.SystemInfo.DomainName;
      wsParms.MachineName = g.SystemInfo.ComputerName;
      wsParms.UserName = g.SystemInfo.UserName;
      wsParms.ModuleCode = g.AppInfo.ModuleCode;
      wsParms.ModuleName = g.AppInfo.ModuleName;
      wsParms.ModuleVersion = g.AppInfo.AppVersion;
      wsParms.AppName = g.AppInfo.AppName;
      wsParms.AppVersion = g.AppInfo.AppVersion;

      wsParms.ModuleCode = 1234;
      wsParms.ModuleName = "DxWorkbookTester";
      wsParms.ModuleVersion = "1.0.0.0";
      wsParms.OrgId = 3;

      return wsParms;
    }

    private void ShowParameters()
    {
      UpdateParameters();
      txtOut.Text = _taskParms.ParmsReport;
      _originalText = txtOut.Text;
      txtOutputFilter.Clear();
    }

    private void UpdateParameters()
    {
      _taskParms = GetParmsForTask();

      _taskParms.AddParm("FileNamePrefix", g.GetFileNamePrefix());

      if (_taskParms == null)
      {
        txtOut.Text = "Task Parameters is null.";
        _originalText = txtOut.Text;
        txtOutputFilter.Clear();
        return;
      }

      if (_taskParms.Count == 0)
      {
        txtOut.Text = "Task Parameters collection is empty.";
        _originalText = txtOut.Text;
        txtOutputFilter.Clear();
        return;
      }

      if (cboDxWorkbookSource.Text == "Use task processor" && ckOverrideConfigWsSpec.Checked)
      {
        _taskParms.SetParmValue("ConfigWsSpec", _configWsSpec);
      }

      ApplyParmOverrides();

      _taskParms.SetParmValue("IsDryRun", ckDryRun.Checked);

      if (!_taskParms.ParmExists("InputFolder") && ckUseManualFolder.Checked)
        _taskParms.Add(new Parm("InputFolder", "Manual"));

      if (cboDxWorkbookSource.Text == "Use task processor")
        _taskParms.Add(new Parm("ReturnDxWorkbookInTaskResult", "True"));

      _isDryRun = _taskParms.GetParmValue("IsDryRun").ToBoolean();
    }

    private void ApplyParmOverrides()
    {
      var overrides = txtTaskParms.Text.ToParmDictionary();
      _taskParms.ProcessOverrides(overrides);
    }

    private void InsertDummyRunHistory()
    {
      try
      {
        if (!ckDryRun.Checked)
        {
          var runHistory = BuildDummyRunHistory();
          using (var taskRepo = new TaskRepository(_taskConfigDbSpec))
          {
            taskRepo.InsertRunHistory(runHistory);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to insert the dummy RunHistory record." + g.crlf2 +
                        ex.ToReport(), "DxWorkbook Tester - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private RunHistory BuildDummyRunHistory()
    {
      var runHistory = new RunHistory();
      runHistory.ScheduledTaskId = _selectedScheduledTask.ScheduledTaskId;
      runHistory.TaskName = _selectedScheduledTask.TaskName;
      runHistory.ProcessorName = _selectedScheduledTask.ProcessorName;
      runHistory.ProcessorVersion = _selectedScheduledTask.ProcessorVersion;
      runHistory.ProcessorType = g.ToEnum<ProcessorType>(_selectedScheduledTask.ProcessorTypeId, ProcessorType.NotSet);
      runHistory.ExecutionStatus = ExecutionStatus.Completed;
      runHistory.RunStatus = RunStatus.Success;
      runHistory.RunCode = 0;
      runHistory.NoWorkDone = false;
      runHistory.StartDateTime = DateTime.Now.AddSeconds(-5);
      runHistory.EndDateTime = DateTime.Now;
      runHistory.RunHost = g.SystemInfo.DomainAndComputer;
      runHistory.RunUser = g.SystemInfo.DomainAndUser;
      runHistory.Message = "Dummy record used for downstream processing until task schedules are implemented.";
      runHistory.RunUntilTask = false;
      runHistory.RunUntilPeriod = Period.Month;
      return runHistory;
    }

    private void OutputToScreen(DxWorkbook wb)
    {
      using (var f = new ObjectFactory2())
      {
        XElement xml = f.Serialize(wb);
        txtOut.Text = xml.ToString();
        _originalText = txtOut.Text;
        txtOutputFilter.Clear();
      }
    }

    private DxWorkbook GetDxWorkbook()
    {
      try
      {
        string wbSource = cboDxWorkbookSource.Text;

        switch (wbSource)
        {
          case "Create from map":
            return MapDxWorkbook();

          case "Use mapped file":
            return GetMappedFile();

          case "Use web service":
            return GetDxWorkbookFromWebService();
        }

        return null;
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create the DxWorkbook.", ex);
      }
    }

    private DxWorkbook MapDxWorkbook()
    {
      try
      {
        string inputFileFolder = ResolveInputFilePath();
        string inputFilePath = inputFileFolder.GetUniqueFile();
        string mapName = String.Empty;

        if (!ckSuppressMapping.Checked)
        {
          string mapPath = ResolveMapPath();
          var mapSet = GetMapSet(mapPath);
          _taskParms.AssertParmExistence("MapName");
          mapName = _taskParms.GetParmValue("MapName").ToString();
        }

        _taskParms.AssertParmExistence("FileExtractMode");
        _taskParms.AssertParmExistence("FileNamePrefix");

        string fileNamePrefix = _taskParms.GetParmValue("FileNamePrefix").ToString();

        if (ckSuppressMapping.Checked)
          mapName = String.Empty;

        // transition to eliminating the "sheetsToInclude" parameter from the mapping utility...
        var wb = DxUtility.GetWorkbook(inputFilePath, fileNamePrefix, new List<string>(), mapName);

        string perfEntry = g.Perf.Start("SerializeUnmappedWorkbook", "Creating the DxWorkbook as XML");

        using (var f = new ObjectFactory2())
        {
          var wbXml = f.Serialize(wb);
          string mappedWorkbook = wbXml.ToString();
          string mappedFilePath = Path.GetDirectoryName(Path.GetDirectoryName(inputFilePath)) + @"\MappedFiles";

          if (!Directory.Exists(mappedFilePath))
            Directory.CreateDirectory(mappedFilePath);

          string mappedWorkbookFileName = fileNamePrefix + Path.GetFileNameWithoutExtension(inputFilePath) + ".xml";

          string perfReport = g.Perf.End(perfEntry);

          File.WriteAllText(mappedFilePath + @"\" + mappedWorkbookFileName, mappedWorkbook);
        }

        return wb;
      }
      catch (CxException) {
        throw;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create the DxWorkbook via mapping.", ex);
      }
    }

    private DxWorkbook GetMappedFile()
    {
      try
      {
        string mappedFileFolder = ResolveInputFilePath("ManualMapped");
        string mappedFilePath = mappedFileFolder.GetUniqueFile();

        using (var f = new ObjectFactory2())
        {
          var mappedWb = (DxWorkbook)f.Deserialize(XElement.Parse(File.ReadAllText(mappedFilePath)), true);
          return mappedWb;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to create the DxWorkbook from a mapped file.", ex);
      }

    }

    private DxWorkbook GetDxWorkbookFromWebService()
    {
      try
      {
        string inputFolder = ResolveInputFilePath();
        string[] inputFiles = Directory.GetFiles(inputFolder);
        string mapName = _taskParms.GetParmValue("MapName").ToString();
        string fileNamePrefix = _taskParms.GetParmValue("FileNamePrefix").ToString();
        string fileExtractModeString = _taskParms.GetParmValue("FileExtractMode").ToString();

        if (fileExtractModeString.IsBlank())
          fileExtractModeString = "ReturnExtractedDxWorkbook";

        var fileExtractMode = g.ToEnum<FileExtractMode>(fileExtractModeString);

        if (mapName.IsBlank() && _overrideMapNameParameter.IsNotBlank())
          mapName = _overrideMapNameParameter;

        if (inputFiles.Length != 1)
          throw new Exception("There should be exactly one file in the input folder - number of files found is " + inputFiles.Length.ToString() + ".");

        var f = new ObjectFactory2();

        DateTime dtBegin = DateTime.Now;

        string extractTransName = _taskParms.GetParmValue("ExtractTransName").ToString();
        if (extractTransName.IsBlank())
          extractTransName = "ExcelExtract";
        var wsParms = BuildWsParms(extractTransName);
        wsParms.ParmSet.Add(new Parm("FullPath", inputFiles[0]));

        if (ckSuppressMapping.Checked)
          mapName = String.Empty;

        wsParms.ParmSet.Add(new Parm("MapName", mapName));
        wsParms.ParmSet.Add(new Parm("FileNamePrefix", fileNamePrefix));
        wsParms.ParmSet.Add(new Parm("FileExtractMode", fileExtractMode.ToString()));
        wsParms.SendTimeoutSeconds = 300;

        var messageFactory = new Org.Dx.Business.MessageFactory();
        var requestMessage = messageFactory.CreateRequestMessage(wsParms);

        var responseMessage = WsClient.InvokeServiceCall(wsParms, requestMessage);

        switch (responseMessage.TransactionName)
        {
          case "ErrorResponse":
            ErrorResponse errorResponse = f.Deserialize(responseMessage.TransactionBody, true) as ErrorResponse;
            string errorResponseMessage = errorResponse.Message;
            if (errorResponse.WsException != null)
              errorResponseMessage += g.crlf + "Web Service Exception" + g.crlf + errorResponse.WsException.ToReport();
            throw new Exception("An ErrorResponse object was received from the web service." + g.crlf + errorResponseMessage);

          case "ExcelExtract":
            responseMessage.TransactionBody = ConvertLegacyDxWorkbook(responseMessage.TransactionBody);
            ExcelExtractResponse excelExtractResponse = f.Deserialize(responseMessage.TransactionBody, true) as ExcelExtractResponse;
            return excelExtractResponse.DxWorkbook;

          case "PdfExtract":
            responseMessage.TransactionBody = ConvertLegacyDxWorkbook(responseMessage.TransactionBody);
            PdfExtractResponse pdfExtractResponse = f.Deserialize(responseMessage.TransactionBody, true) as PdfExtractResponse;
            return pdfExtractResponse.DxWorkbook;

          default:
            throw new Exception("An unexpected web service transaction '" + responseMessage.TransactionName + "' was received from the web service.");
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to retrieve the DxWorkbook from the web service.", ex);
      }
    }

    private XElement ConvertLegacyDxWorkbook(XElement wb)
    {
      IEnumerable<XElement> cells = wb.Descendants("DxCell");
      foreach (XElement cell in cells)
      {
        if (cell.AttributeExists("RawValue"))
        {
          cell.Attribute("Types")?.Remove();
          var rawValueAttr = cell.Attribute("RawValue");
          cell.Add(new XAttribute("V", rawValueAttr.Value));
          rawValueAttr.Remove();
        }
        else
        {
          return wb;
        }
      }

      return wb;
    }

    private string ResolveInputFilePath(string overrideInputFolder = "")
    {
      try
      {
        _taskParms.AssertParmExistence("$FSSTEM$");
        _taskParms.AssertParmExistence("$ENV$");
        _taskParms.AssertParmExistence("$TASKNODE$");

        string fsStem = _taskParms.GetParmValue("$FSSTEM$").ToString();
        string env = _taskParms.GetParmValue("$ENV$").ToString();
        string taskNode = _taskParms.GetParmValue("$TASKNODE$").ToString();
        string inputFilePathUnresolved = _taskParms.GetParmValue("InputFilePath", @"$FSSTEM$\$ENV$\$TASKNODE$\Ready").ToString();
        string inputFilePath = inputFilePathUnresolved.Replace("$FSSTEM$", fsStem)
                               .Replace("$ENV$", env)
                               .Replace("$TASKNODE$", taskNode);

        if (overrideInputFolder.IsBlank())
        {
          if (_taskParms.ContainsParmName("InputFolder"))
            overrideInputFolder = _taskParms.GetParmValue("InputFolder").ToString();
        }

        if (overrideInputFolder.IsNotBlank())
        {
          string overrideInputFilePath = Path.GetDirectoryName(inputFilePath) + @"\" + overrideInputFolder;
          if (!Directory.Exists(overrideInputFilePath))
            throw new Exception("The overridden input file path '" + overrideInputFolder + "' does not exist.");
          inputFilePath = overrideInputFilePath;
        }

        return inputFilePath;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to resolve the input file path.", ex);
      }
    }

    private string ResolveMapPath()
    {
      try
      {
        _taskParms.AssertParmExistence("MapName");
        string mapName = _taskParms.GetParmValue("MapName").ToString();
        string ext = Path.GetExtension(mapName);

        if (ext.IsBlank())
        {
          string extractTransName = _taskParms.GetParmValue("ExtractTransName", "ExcelExtract").ToString();
          mapName += extractTransName == "PdfExtract" ? ".ext" : ".dxmap";
        }

        string mapPath = g.CI("MapPath");
        string fullMapPath = mapPath + @"\" + mapName;

        if (!File.Exists(fullMapPath))
          throw new Exception("The map file does not exist at '" + fullMapPath + "'.");

        return fullMapPath;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to resolve the map file path.", ex);
      }
    }

    private DxMapSet GetMapSet(string fullMapPath)
    {
      if (fullMapPath.ToLower().Trim() == "@map")
      {
        var mapSet = new DxMapSet();
        mapSet.Name = "@Map";
        var map = new DxMap();
        map.Name = "@Map";
        map.DxMapType = DxMapType.RowToRow;
        map.ColumnIndexMap = _ciMap;
        mapSet.Add("@Map", map);
        return mapSet;
      }

      using (var f = new ObjectFactory2(g.CI("InDiagnosticsMode").ToBoolean()))
      {
        var mapXml = XElement.Parse(File.ReadAllText(fullMapPath));

        var mapx = XElement.Parse(File.ReadAllText(fullMapPath));

        var mapSet = f.Deserialize(mapXml) as DxMapSet;

        foreach (var map in mapSet.Values)
        {
          map.ColumnIndexMap = _ciMap;
          if (map.DxRegionSet != null && map.DxRegionSet.Count > 0)
          {
            foreach (var dxRegion in map.DxRegionSet.Values)
            {
              if (dxRegion.DxMapSet != null && dxRegion.DxMapSet.Count > 0)
              {
                foreach (var regionMap in dxRegion.DxMapSet.Values)
                {
                  regionMap.ColumnIndexMap = _ciMap;
                }
              }
            }
          }
        }

        return mapSet;
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
        string errorMessage = ex.ToReport();

        if (errorMessage.Length > 10000)
          errorMessage = errorMessage.Substring(0, 10000);

        MessageBox.Show(errorMessage, "DxWorkbookTester - Application Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }


      try
      {
        int formHorizontalSize = g.GetCI("MainFormHorizontalSize").ToInt32OrDefault(90);
        int formVerticalSize = g.GetCI("MainFormVerticalSize").ToInt32OrDefault(90);

        this.Size = new Size(Screen.PrimaryScreen.Bounds.Width * formHorizontalSize / 100,
                             Screen.PrimaryScreen.Bounds.Height * formVerticalSize / 100);
        this.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                  Screen.PrimaryScreen.Bounds.Height / 2 - this.Height / 2);

        DxUtility.NotifyHost += DxUtility_NotifyHost;

        _mapPath = g.CI("MapPath");

        ckRunDatabaseLoad.Checked = false;
        ckDryRun.Checked = false;
        ckDryRun.Enabled = false;

        ckUseManualFolder.Checked = g.CI("UseManualInputFolder").ToBoolean();

        txtTaskParms.Text = g.CI("TaskOverrideParms");
        txtTaskParms.SelectionStart = 0;
        txtTaskParms.SelectionLength = 0;
        cboDxWorkbookSource.SelectedIndex = 0;

        cboWsEnvironment.LoadItems(g.GetList("WsEnvironment"));
        cboDbEnvironment.LoadItems(g.GetList("DbEnvironment"));
        cboDbEnvironment.SelectItem(g.CI("SelectedDbEnvironment"));

        cboTaskGroup.Enabled = false;

        _configWsSpec = g.GetWsSpec("WsHost");
        btnRunTask.Focus();

        using (var f = new ObjectFactory2())
        {
          _ciMap = f.Deserialize(XElement.Parse(File.ReadAllText(_mapPath + @"\ColumnIndexMap.xml"))) as ColumnIndexMap;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during program initialization." + g.crlf2 +
                        ex.ToReport(), "DxWorkbookTester - Initialization Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
      }
    }

    private void DxUtility_NotifyHost(string message)
    {
      if (this.InvokeRequired)
      {
        this.Invoke((Action)((() =>
        {
          lblStatus.Text = message;
          Application.DoEvents();
        })));
      }
      else
      {
        lblStatus.Text = message;
        Application.DoEvents();
      }
    }

    private void LoadTasks()
    {
      string selectedDbServer = g.CI(_dbEnv.ToUpper() + "_DBSERVER");
      if (g.AppConfig.Variables.ContainsKey("DB_SERVER") && _dbEnv.In("Prod,Test,Dev"))
      {
        g.AppConfig.Variables["DB_SERVER"] = selectedDbServer;
      }

      var tasks = new List<string>();

      try
      {
        using (var repo = new TaskRepository(_taskConfigDbSpec))
        {
          var taskList = repo.GetTaskNames();
          tasks.AddRange(taskList);
        }

        cboScheduledTasks.LoadItems(tasks, true);
        string taskToRun = g.CI("TaskToRun");
        if (taskToRun.IsNotBlank())
          cboScheduledTasks.SelectItem(taskToRun);
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to get a list of tasks to run." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.ModuleName + " - Error Retrieving Tasks", MessageBoxButtons.OK, MessageBoxIcon.Error);

      }
    }

    private void cboDbEnvironment_SelectedIndexChanged(object sender, EventArgs e)
    {
      _dbEnv = cboDbEnvironment.Text.Trim();

      // When the DB environment changes, force the WS environment to match it.
      // But the WS environment can be changed to be different without forcing an update to the DB environment.
      cboWsEnvironment.SelectItem(_dbEnv);

      string selectedDbServer = g.CI(_dbEnv.ToUpper() + "_DBSERVER");
      if (g.AppConfig.Variables.ContainsKey("DB_SERVER") && _dbEnv.In("Prod,Test,Dev"))
      {
        g.AppConfig.Variables["DB_SERVER"] = selectedDbServer;
      }

      _taskConfigDbSpec = g.GetDbSpec("Tasks");

      lblDbServer.Text = selectedDbServer;

      string selectedWsHost = g.CI(_wsEnv.ToUpper() + "_WSHOST");
      if (g.AppConfig.Variables.ContainsKey("WS_HOST") && _wsEnv.In("Prod,Test,Dev"))
      {
        g.AppConfig.Variables["WS_HOST"] = selectedWsHost;
      }

      string selectedWsPort = g.CI(_wsEnv.ToUpper() + "_WSPORT");
      if (g.AppConfig.Variables.ContainsKey("WS_PORT") && _wsEnv.In("Prod,Test,Dev"))
      {
        g.AppConfig.Variables["WS_PORT"] = selectedWsPort;
      }

      _configWsSpec = g.GetWsSpec("WsHost");

      lblWebServiceEndpoint.Text = _configWsSpec.WebServiceEndpoint;

      string selectedTask = cboScheduledTasks.Text;

      LoadTasks();

      if (selectedTask.IsNotBlank())
        cboScheduledTasks.SelectItem(selectedTask);
    }

    private void cboWsEnvironment_SelectedIndexChanged(object sender, EventArgs e)
    {
      _wsEnv = cboWsEnvironment.Text;

      string selectedWsHost = g.CI(_wsEnv.ToUpper() + "_WSHOST");
      if (g.AppConfig.Variables.ContainsKey("WS_HOST") && _wsEnv.In("Prod,Prod2,Prod_ISO,Test,Test2,Test_ISO,Dev"))
      {
        g.AppConfig.Variables["WS_HOST"] = selectedWsHost;
      }

      string selectedWsPort = g.CI(_wsEnv.ToUpper() + "_WSPORT");
      if (g.AppConfig.Variables.ContainsKey("WS_PORT") && _wsEnv.In("Prod,Prod2,Prod_ISO,Test,Test2,Test_ISO,Dev"))
      {
        g.AppConfig.Variables["WS_PORT"] = selectedWsPort;
      }

      _configWsSpec = g.GetWsSpec("WsHost");

      lblWebServiceEndpoint.Text = _configWsSpec.WebServiceEndpoint;
    }

    private void cboScheduledTasks_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        if (cboScheduledTasks.Text.IsNotBlank())
        {
          _selectedTaskName = cboScheduledTasks.Text;
          _selectedScheduledTask = GetScheduledTask();
          _taskParms = GetParmsForTask();
          _taskParms.ProcessOverrides(txtTaskParms.Text.ToParmDictionary());
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred based on the selection of the scheduled task." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.ModuleName + " - Error Retrieving Scheduled Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private ScheduledTask GetScheduledTask()
    {
      try
      {
        bool inProdDb = _dbEnv == "Prod";

        using (var repo = new TaskRepository(_taskConfigDbSpec))
        {
          return repo.GetScheduledTask(_selectedTaskName, inProdDb);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to get the scheduled task." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.ModuleName + " - Error Retrieving Scheduled Task", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return null;
      }
    }

    private ParmSet GetParmsForTask()
    {
      if (g.AppConfig.GetBoolean("LoadTaskFromConfig"))
      {
        var taskConfigs = g.AppConfig.GetTaskConfigurations();
        foreach (var taskConfigSet in taskConfigs.Values)
        {
          if (taskConfigSet.ContainsKey(_selectedTaskName))
          {
            var task = taskConfigSet[_selectedTaskName];
            return task.TaskParmSet.ToParmSet();
          }
        }

        throw new Exception("The task configuration and parameters for task name '" + _selectedTaskName + "' is not defined " +
                            "in the AppConfig file. The 'LoadTaskFromConfig' configuration option is set to 'True' to cause " +
                            "the task and parameters to be loaded from the AppConfig file instead of the database.");
      }
      else
      {
        return GetParmsForTaskFromDB();
      }
    }

    private ParmSet GetParmsForTaskFromDB()
    {
      try
      {
        using (var repo = new TaskRepository(_taskConfigDbSpec))
        {
          return repo.GetTaskParms(_selectedScheduledTask.ScheduledTaskId, true);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to get a list of parameters for the task from the database." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.ModuleName + " - Error Retrieving Task Parameters from Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return null;
      }
    }

    private ParmSet GetParmsForTaskFromAppConfig()
    {
      try
      {

        return null;
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred while attempting to get a list of parameters for the task from the AppConfig file." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.ModuleName + " - Error Retrieving Task Parameters from AppConfig", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return null;
      }
    }

    private void ckRunDatabaseLoad_CheckedChanged(object sender, EventArgs e)
    {
      if (ckRunDatabaseLoad.Checked)
      {
        ckDryRun.Enabled = true;
        ckDryRun.Checked = true;
      }
      else
      {
        ckDryRun.Checked = false;
        ckDryRun.Enabled = false;
      }
    }

    private void cboDxWorkbookSource_SelectedIndexChanged(object sender, EventArgs e)
    {
      ckSuppressMapping.Enabled = true;
      ckRunDatabaseLoad.Enabled = true;
      ckRunDatabaseLoad.Checked = false;
      ckDryRun.Checked = false;
      ckDryRun.Enabled = false;
      ckOverrideConfigWsSpec.Visible = false;

      switch (cboDxWorkbookSource.Text)
      {
        case "Create from map":
          ckSuppressMapping.Checked = false;
          break;


        case "Use mapped file":
          ckSuppressMapping.Checked = false;
          ckSuppressMapping.Enabled = false;
          ckUseManualFolder.Enabled = false;
          ckUseManualFolder.Checked = false;
          break;


        case "Use web service":
          ckSuppressMapping.Enabled = true;
          ckSuppressMapping.Checked = false;
          break;

        case "Use task processor":
          ckRunDatabaseLoad.Checked = true;
          ckDryRun.Enabled = true;
          ckDryRun.Checked = true;
          ckSuppressMapping.Checked = false;
          ckSuppressMapping.Enabled = false;
          ckOverrideConfigWsSpec.Visible = true;
          ckOverrideConfigWsSpec.Checked = false;
          break;
      }
    }

    private void ckSuppressMapping_CheckedChanged(object sender, EventArgs e)
    {
      if (ckSuppressMapping.Checked)
      {
        ckRunDatabaseLoad.Checked = false;
        ckRunDatabaseLoad.Enabled = false;
      }
      else
      {
        ckRunDatabaseLoad.Enabled = true;
      }
    }

    private void txtOutputFilter_TextChanged(object sender, EventArgs e)
    {
      if (txtOutputFilter.Text.IsNotBlank())
        RunFilters();
      else
        txtOut.Text = _originalText;
    }

    private void RunFilters()
    {

      if (txtOutputFilter.Text.Trim().IsBlank())
      {
        txtOut.Text = _originalText;
        return;
      }

      string[] filters = txtOutputFilter.Text.Trim().ToLower().Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);

      string t = _originalText;

      StringBuilder sb = new StringBuilder();

      string[] lines = _originalText.Replace("\r", String.Empty).Split(Constants.NewLineDelimiter, StringSplitOptions.RemoveEmptyEntries);

      foreach (string line in lines)
      {
        if (ckIncludeDxStructure.Checked && (line.Contains("DxWorkbook") || line.Contains("DxWorksheet") || line.Contains("DxCellArray")))
        {
          sb.Append(line + g.crlf);
        }
        else
        {
          string lineLc = line.ToLower();
          foreach (var filter in filters)
          {
            if (lineLc.Contains(filter))
            {
              sb.Append(line + g.crlf);
              break;
            }
          }
        }
      }

      txtOut.Text = sb.ToString();
    }
  }
}
