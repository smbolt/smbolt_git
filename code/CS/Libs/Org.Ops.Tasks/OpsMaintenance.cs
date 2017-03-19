using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS.Performance;
using Org.TP.Concrete;
using Org.TSK.Business;

namespace Org.Ops.Tasks
{
  public class OpsMaintenance : TaskProcessorBase
  {
    private Logger _logger;
    private string _taskMessage = String.Empty;

    private int _retentionDaysAUDT = 60;
    private int _retentionDaysDIAG = 60;
    private int _retentionDaysINFO = 60;
    private int _retentionDaysMAJR = 180;
    private int _retentionDaysMINR = 180;
    private int _retentionDaysSEVR = 180;
    private int _retentionDaysTRAC = 60;
    private int _retentionDaysWARN = 90;
    private string _modulesToExclude = String.Empty;
    private string _eventsToExclude = String.Empty;
    private string _entitiesToExclude = String.Empty;

    private int _retentionDaysOverdueTaskNotify = 90;
    
    public override async Task<TaskResult> ProcessTaskAsync(Func<bool> checkContinue)
    {
      TaskResult taskResult = base.InitializeTaskResult();

      _logger = new Logger();

      try
      {
        taskResult = await Task.Run<TaskResult>(() =>
        {
          this.Initialize();

          LogMaintenance();

          if (IsDryRun)
            taskResult.NoWorkDone = true;

          return taskResult.Success(_taskMessage);
        });     

        return taskResult;
      }
      catch (Exception ex)
      {
        return taskResult.Failed("An exception occurred during " + base.TaskRequest.TaskName + " task processing.", ex);
      }
    }

    private void LogMaintenance()
    {
      try
      {
        var loggingDbSpec = GetParmValue("LoggingDbSpec") as ConfigDbSpec;

        if (ParmExists("ModulesToExclude"))
        {
          _modulesToExclude = GetParmValue("ModulesToExclude").ToString();
          if (!IsCommaDelimitedNumbers(_modulesToExclude))
            throw new Exception("ModulesToExclude parameter contains invalid characters or comma placement. Must be comma delimited string of integers.");
        }
        if (ParmExists("EventsToExclude"))
        {
          _eventsToExclude = GetParmValue("EventsToExclude").ToString();
          if (!IsCommaDelimitedNumbers(_eventsToExclude))
            throw new Exception("EventsToExclude parameter contains invalid characters or comma placement. Must be comma delimited string of integers.");
        }
        if (ParmExists("EntitiesToExclude"))
        {
          _entitiesToExclude = GetParmValue("EntitiesToExclude").ToString();
          if (!IsCommaDelimitedNumbers(_entitiesToExclude))
            throw new Exception("EntitiesToExclude parameter contains invalid characters or comma placement. Must be comma delimited string of integers.");
        }

        if (ParmExists("RetentionDaysAUDT"))
          _retentionDaysAUDT = GetParmValue("RetentionDaysAUDT").ToInt32();
        if (ParmExists("RetentionDaysDIAG"))
          _retentionDaysDIAG = GetParmValue("RetentionDaysDIAG").ToInt32();
        if (ParmExists("RetentionDaysINFO"))
          _retentionDaysINFO = GetParmValue("RetentionDaysINFO").ToInt32();
        if (ParmExists("RetentionDaysMAJR"))
          _retentionDaysMAJR = GetParmValue("RetentionDaysMAJR").ToInt32();
        if (ParmExists("RetentionDaysMINR"))
          _retentionDaysMINR = GetParmValue("RetentionDaysMINR").ToInt32();
        if (ParmExists("RetentionDaysSEVR"))
          _retentionDaysSEVR = GetParmValue("RetentionDaysSEVR").ToInt32();
        if (ParmExists("RetentionDaysTRAC"))
          _retentionDaysTRAC = GetParmValue("RetentionDaysTRAC").ToInt32();
        if (ParmExists("RetentionDaysWARN"))
          _retentionDaysWARN = GetParmValue("RetentionDaysWARN").ToInt32();

        using (var repo = new LoggingRepository(loggingDbSpec))
        {
          int deletedRows = repo.DeleteOldLogRecords(_retentionDaysAUDT, _retentionDaysDIAG, _retentionDaysINFO, _retentionDaysMAJR, _retentionDaysMINR, _retentionDaysSEVR, _retentionDaysTRAC,
                                   _retentionDaysWARN, _modulesToExclude, _eventsToExclude, _entitiesToExclude, IsDryRun);
          _taskMessage = deletedRows + " log records were deleted.";
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while trying to maintain the Logging Database", ex);
      }
    }

    private bool IsCommaDelimitedNumbers(string str)
    {
      if (str == null)
        return true;

      if (str.StartsWith(",") || str.EndsWith(","))
        return false;

      char prevChar = ' ';

      foreach (char c in str)
      {
        if (!char.IsDigit(c) && c != ',')
          return false;

        if (prevChar == ',' && c == ',')
          return false;

        prevChar = c;
      }

      return true;
    }

    protected override void Initialize()
    {
      base.Initialize();

      this.AssertParmExistence("LoggingDbSpec");
      this.AssertParmExistence("TasksDbSpec");
    }
  }
}
