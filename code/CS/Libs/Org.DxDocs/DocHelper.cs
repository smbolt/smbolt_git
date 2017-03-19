using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;
using Org.GS;

namespace Org.DxDocs
{
  public class DocHelper
  {
    private static object GenerateXlsx_LockObject = new object();
    public static TaskResult GenerateXlsx()
    {
      if (Monitor.TryEnter(GenerateXlsx_LockObject, 5000))
      {
        var taskResult = new TaskResult("GenerateXlsx");

        try
        {
          Workbook wb = new Workbook();
          wb.Worksheets[0].Cells["A1"].Value = "Hello"; 
          wb.SaveDocument(@"C:\_work\DxDocs\Doc1.xlsx");
          taskResult.EndDateTime = DateTime.Now;
          return taskResult;
        }
        catch (Exception ex)
        {
          taskResult.TaskResultStatus = TaskResultStatus.Failed;
          taskResult.Message = "Generation of Excel document failed.";
          taskResult.Code = 4999;
          taskResult.Exception = ex;
          taskResult.FullErrorDetail = ex.ToReport();
          taskResult.EndDateTime = DateTime.Now;
          taskResult.TaskResultStatus = TaskResultStatus.Success;
          return taskResult;
        }
        finally
        {
          Monitor.Exit(GenerateXlsx_LockObject);
        }
      }
      else
      {
        var taskResult = new TaskResult("GenerateXlsx", "Failed to obtain lock for generating Excel document.", TaskResultStatus.Failed, 4999);
        taskResult.EndDateTime = DateTime.Now;
        return taskResult;
      }
    }

  }
}
