using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.TP;

namespace Org.TP.Concrete 
{
  public class FileTaskProcessorBase : TaskProcessorBase
  {
    public string InputFilePath { get; set; }
    public string InputFullFilePath { get; set; }
    public string ErrorFilePath { get; set; }
    public string ProcessedFilePath { get; set; }
    public bool FileSystemAccessOccurred { get; set; }
    public int ExcelExtractWcfTimeoutSeconds { get; set; }

    public FileTaskProcessorBase()
    {
      this.InputFilePath = String.Empty;
      this.InputFullFilePath = String.Empty;
      this.ErrorFilePath = String.Empty;
      this.ProcessedFilePath = String.Empty;
      this.FileSystemAccessOccurred = false;
      this.ExcelExtractWcfTimeoutSeconds = 0;
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
      base.AssertParmExistence("InputFilePath");
      base.AssertParmExistence("ErrorFilePath");
      base.AssertParmExistence("ProcessedFilePath");

      InputFilePath = base.GetParmValue("InputFilePath").ToString();
      ProcessedFilePath = base.GetParmValue("ProcessedFilePath").ToString();
      ErrorFilePath = base.GetParmValue("ErrorFilePath").ToString();

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
        string datedProcessedFileName = DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-" + Path.GetFileName(this.InputFullFilePath);
        string processedFileFullPath = this.ProcessedFilePath + @"\" + datedProcessedFileName;

        while (File.Exists(processedFileFullPath))
        {
          System.Threading.Thread.Sleep(1000);
          datedProcessedFileName = DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-" + Path.GetFileName(this.InputFullFilePath);
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
