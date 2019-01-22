using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.TP;
using System.Xml.Linq;

namespace Org.TP.Concrete 
{
  public class FileTaskProcessorBase : TaskProcessorBase
  {
    public string InputFilePath { get; set; }
    public string InputFullFilePath { get; set; }
    public string ErrorFilePath { get; set; }
    public FileExtractMode FileExtractMode { get; set; }
    public List<string> WorksheetsToInclude { get; set; }
    public string ProcessedFilePath { get; set; }
    public string ExtractTransactionName { get; set; }
    public string MapName { get; set; }
    public bool FileSystemAccessOccurred { get; set; }
    public int ExcelExtractWcfTimeoutSeconds { get; set; }
    public string FileNamePrefix { get; set; }
    public bool ReturnDxWorkbookInTaskResult { get; set; }

    public FileTaskProcessorBase()
    {
      this.InputFilePath = String.Empty;
      this.InputFullFilePath = String.Empty;
      this.ErrorFilePath = String.Empty;
      this.FileExtractMode = FileExtractMode.NotSet;
      this.WorksheetsToInclude = new List<string>();
      this.ProcessedFilePath = String.Empty;
      this.ExtractTransactionName = String.Empty;
      this.MapName = String.Empty;
      this.FileSystemAccessOccurred = false;
      this.ExcelExtractWcfTimeoutSeconds = 0;
      this.FileNamePrefix = g.GetFileNamePrefix();
      this.ReturnDxWorkbookInTaskResult = false;
    }

    protected override void AssertParmExistence(string parmName)
    {
      base.AssertParmExistence(parmName);
    }

    protected override TaskResult InitializeTaskResult()
    {
      return base.InitializeTaskResult();
    }

    protected override void Initialize()
    {
      base.Initialize();

      base.AssertParmExistence("$FSSTEM$");
      base.AssertParmExistence("$ENV$");
      base.AssertParmExistence("$TASKNODE$");
      base.AssertParmExistence("FileExtractMode");

      InputFilePath = base.GetParmValue("InputFilePath", @"$FSSTEM$\$ENV$\$TASKNODE$\Ready").ToString();
      ProcessedFilePath = base.GetParmValue("ProcessedFilePath", @"$FSSTEM$\$ENV$\$TASKNODE$\Processed").ToString();
      ErrorFilePath = base.GetParmValue("ErrorFilePath", @"$FSSTEM$\$ENV$\$TASKNODE$\Error").ToString();
      FileExtractMode = g.ToEnum<FileExtractMode>(base.GetParmValue("FileExtractMode").ToString());

      string fsStem = base.GetParmValue("$FSSTEM$").ToString();
      string env = base.GetParmValue("$ENV$").ToString();
      string taskNode = base.GetParmValue("$TASKNODE$").ToString();

      if (InputFilePath.Contains("$FSSTEM$"))
        InputFilePath = InputFilePath.Replace("$FSSTEM$", fsStem);

      if (InputFilePath.Contains("$ENV$"))
        InputFilePath = InputFilePath.Replace("$ENV$", env);

      if (InputFilePath.Contains("$TASKNODE$"))
        InputFilePath = InputFilePath.Replace("$TASKNODE$", taskNode);

      if (ProcessedFilePath.Contains("$FSSTEM$"))
        ProcessedFilePath = ProcessedFilePath.Replace("$FSSTEM$", fsStem);

      if (ProcessedFilePath.Contains("$ENV$"))
        ProcessedFilePath = ProcessedFilePath.Replace("$ENV$", env);

      if (ProcessedFilePath.Contains("$TASKNODE$"))
        ProcessedFilePath = ProcessedFilePath.Replace("$TASKNODE$", taskNode);

      if (ErrorFilePath.Contains("$FSSTEM$"))
        ErrorFilePath = ErrorFilePath.Replace("$FSSTEM$", fsStem);

      if (ErrorFilePath.Contains("$ENV$"))
        ErrorFilePath = ErrorFilePath.Replace("$ENV$", env);

      if (ErrorFilePath.Contains("$TASKNODE$"))
        ErrorFilePath = ErrorFilePath.Replace("$TASKNODE$", taskNode);

      if (base.ParmExists("ExcelExtractWcfTimeoutSeconds"))
        ExcelExtractWcfTimeoutSeconds = base.GetParmValue("ExcelExtractWcfTimeoutSeconds").ToInt32();

      if (base.ParmExists("ExtractTransName"))
        ExtractTransactionName = base.GetParmValue("ExtractTransName").ToString();
      else
        ExtractTransactionName = "ExcelExtract";

      if (base.ParmExists("MapName"))
        MapName = base.GetParmValue("MapName").ToString();

      if (base.ParmExists("WorksheetsToInclude"))
      {
        var wsDict = base.GetParmValue("WorksheetsToInclude") as Dictionary<string, string>;
        WorksheetsToInclude = wsDict.ToList(DictionaryPart.Value, true); 
      }

      if (base.ParmExists("ReturnDxWorkbookInTaskResult"))
        ReturnDxWorkbookInTaskResult = base.GetParmValue("ReturnDxWorkbookInTaskResult").ToString().ToBoolean();
    }

    protected void ValidateDirectories()
    {
      try
      {
        if (!Directory.Exists(this.InputFilePath))
          throw new Exception("The Input file path directory '" + this.InputFilePath + "' does not exist.");

        if (!Directory.Exists(this.ProcessedFilePath))
          throw new Exception("The Processed file path directory '" + this.ProcessedFilePath + "' does not exist.");

        if (!Directory.Exists(this.ErrorFilePath))
          throw new Exception("The Error file path directory '" + this.ErrorFilePath + "' does not exist.");

        using (var fsu = new FileSystemUtility())
        {
          if (!fsu.AssertDirectoryAccess(this.InputFilePath, FileSystemAccess.ReadWrite))
            throw new Exception("The task processor does not have Read/Write permissions on InputFilePath '" + this.InputFilePath + "'.");

          if (!fsu.AssertDirectoryAccess(this.ProcessedFilePath, FileSystemAccess.ReadWrite))
            throw new Exception("The task processor does not have Read/Write permissions on ProcessedFilePath '" + this.ProcessedFilePath + "'.");

          if (!fsu.AssertDirectoryAccess(this.ErrorFilePath, FileSystemAccess.ReadWrite))
            throw new Exception("The task processor does not have Read/Write permissions on ErrorFilePath '" + this.ErrorFilePath + "'.");
        }
      }
      catch (Exception ex)
      {
        this.FileSystemAccessOccurred = true;
        throw new Exception("An exception occurred attempting to validate the existence of and access to required file system directories.", ex);
      }
    }

    protected int GetFilesToProcess()
    {
      string overrideInputFolder = this.GetParmValue("InputFolder").ToString();

      if (overrideInputFolder.IsNotBlank())
      {
        string overrideInputFilePath = Path.GetDirectoryName(this.InputFilePath) + @"\" + overrideInputFolder;
        if (!Directory.Exists(overrideInputFilePath))
          throw new Exception("The overridden input file path '" + overrideInputFolder + "' does not exist.");
        this.InputFilePath = overrideInputFilePath;
      }

      List<string> filesToProcess = Directory.GetFiles(this.InputFilePath).ToList();

      if (filesToProcess.Count == 0)
        return 0; 

      if (filesToProcess.Count == 1)
        this.InputFullFilePath = filesToProcess.First();
      
      return filesToProcess.Count;
    }
    
    protected void MoveInputFileToProcessed()
    {
      if (this.IsDryRun)
        return;

      try
      {
        if (this.FileNamePrefix.IsBlank())
          throw new Exception("The required FileNamePrefix value is blank or null.");

        string datedProcessedFileName = this.FileNamePrefix + Path.GetFileName(this.InputFullFilePath);
        string processedFileFullPath = this.ProcessedFilePath + @"\" + datedProcessedFileName;

        int seq = 0;

        while (File.Exists(processedFileFullPath))
        {
          string ext = Path.GetExtension(this.InputFullFilePath);
          string fileName = Path.GetFileNameWithoutExtension(this.InputFullFilePath) + "(" + (++seq).ToString() + ")" + ext;         
          datedProcessedFileName = this.FileNamePrefix + fileName;
          processedFileFullPath = this.ProcessedFilePath + @"\" + datedProcessedFileName;
        }

        File.Move(this.InputFullFilePath, processedFileFullPath);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to move the input file to the processed folder.", ex); 
      }
    }

    protected void MoveThisFileToError(string fullFilePath)
    {
      if (this.IsDryRun)
        return;
      try
      {
        string datedErrorFileName = DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-" + Path.GetFileName(fullFilePath);
        string errorFileFullPath = this.ErrorFilePath + @"\" + datedErrorFileName;

        while (File.Exists(errorFileFullPath))
        {
          System.Threading.Thread.Sleep(1000);
          datedErrorFileName = DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-" + Path.GetFileName(fullFilePath);
          errorFileFullPath = this.ErrorFilePath + @"\" + datedErrorFileName;
        }

        File.Move(fullFilePath, errorFileFullPath);
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to move file '" + fullFilePath + "' to the error folder.", ex);
      }
    }

    protected void MoveInputFilesToError()
    {
      if (this.IsDryRun)
        return;

      try
      {
        // this will move any and all files in the input directory to the error directory
        if (this.InputFullFilePath.IsBlank())
        {
          List<string> inputFiles = Directory.GetFiles(this.InputFilePath).ToList();
          foreach(string inputFileName in inputFiles)
          {
            string datedErrorFileName = DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-" + Path.GetFileName(inputFileName);
            string errorFileFullPath = this.ErrorFilePath + @"\" + datedErrorFileName;

            while (File.Exists(errorFileFullPath))
            {
              System.Threading.Thread.Sleep(1000);
              datedErrorFileName = DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-" + Path.GetFileName(inputFileName);
              errorFileFullPath = this.ErrorFilePath + @"\" + datedErrorFileName;
            }

            File.Move(inputFileName, errorFileFullPath);
          }
        }
        else
        {
          // this will move the single located input file to the error file directory
          string datedErrorFileName = DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-" + Path.GetFileName(this.InputFullFilePath);
          string errorFileFullPath = this.ErrorFilePath + @"\" + datedErrorFileName;

          while (File.Exists(errorFileFullPath))
          {
            System.Threading.Thread.Sleep(1000);
            datedErrorFileName = DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-" + Path.GetFileName(this.InputFullFilePath);
            errorFileFullPath = this.ErrorFilePath + @"\" + datedErrorFileName;
          }

          File.Move(this.InputFullFilePath, errorFileFullPath);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to move the input file to the error folder.", ex);
      }
    }
  }
}
