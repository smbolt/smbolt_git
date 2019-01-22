using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Xml.Linq;
using Gulfport.NonOp.Business;
using Org.WSO;
using Org.WSO.Transactions;
using Org.TP.Concrete;
using Org.TP;
using Org.GS;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.Dx.Business;
using Gulfport.Common.Tasks;
using Bus = Gulfport.Stmt.Business;

namespace Gulfport.NonOp.Tasks
{
  [Export(typeof(ITaskProcessor))]
  [ExportMetadata("Name", "NonOpProductionDataImport")]
  [ExportMetadata("Version", "1.0.0.0")]
  public class NonOpProductionDataImport : StatementTaskProcessor
  {
    public override int EntityId {
      get {
        return 513;
      }
    }
    public int _operatorId {
      get;
      set;
    }
    private Logger _logger;

    public override async Task<TaskResult> ProcessTaskAsync(Func<bool> checkContinue)
    {
      TaskResult taskResult = base.InitializeTaskResult();
      CheckContinue = checkContinue;
      _logger = new Logger();
      _logger.ModuleId = g.AppInfo.ModuleCode;

      try
      {
        return await Task.Run<TaskResult>(() =>
        {
          this.Initialize();
          taskResult.IsDryRun = IsDryRun;

          if (!GetWsAndDbSpecs(taskResult, _logger))
            return taskResult;

          Bus.StatementType statementType = g.ToEnum<Bus.StatementType>(base.GetParmValue("StatementType"), Bus.StatementType.Unknown);
          if (statementType != Bus.StatementType.NonOpStatement)
          {
            int code = 6117;
            string message = DryRunIndicator + "The supplied StatementType parameter is not valid for this task processor. The valid values are " + g.crlf +
                             Bus.StatementType.NonOpStatement.ToString() + ", (code " + code.ToString() + ").";
            _logger.Log(message, code, this.EntityId);
            return taskResult.Failed(message, code);
          }

          _operatorId = base.GetParmValue("OperatorID").ToInt32();

          if (!GetFilesToProcess(taskResult, _logger))
            return taskResult;

          if (IsDryRun)
            _logger.Log("Task processor is configured for a 'Dry Run'", 6106, this.EntityId);

          var wb = GetDxWorkbookFromFile();

          if (wb == null)
          {
            if (taskResult.TaskResultStatus == TaskResultStatus.Failed)
            {
              MoveInputFilesToError();
            }
            return taskResult;
          }

          Bus.StmtFile nonOpProductionStmtFile = ExtractFromDxWorkbookNonOpProdDetail(_dstWb, Bus.StatementType.Unknown);

          using (var noRepo = new NonOpRepository(OutputDbSpec))
          {
            noRepo.SaveNonOpProduction(nonOpProductionStmtFile);
          }

          string successMessage = FinalizeProcessing("NonOpRiceImport", _logger);

          if (IsDryRun)
          {
            taskResult.NoWorkDone = true;
          }

          return taskResult.Success(successMessage);
        });
      }
      catch (Exception ex)
      {
        base.MoveInputFilesToError();
        return taskResult.Failed("An exception occurred during " + base.TaskRequest.TaskName + " task processing." + g.crlf + ex.ToReport(), ex);
      }
    }

    private DxWorkbook GetFile()
    {
      var wb = new DxWorkbook();

      string inputFilePath = @"\\gulfport.net\data\Applications\Data_Integration\Test\NonOp_Continental\Ready\Wb.xml";
      var xmlFile = File.Open(inputFilePath, FileMode.Open);

      using(var f = new ObjectFactory2())
      {
        wb = f.Deserialize(xmlFile) as DxWorkbook;
      }

      return wb;
    }

    private Bus.StmtFile ExtractFromDxWorkbookNonOpProdDetail(DxWorkbook wb, Bus.StatementType statementType)
    {
      string propertyName = String.Empty;

      try
      {
        var nonOpStatementFile = new Bus.StmtFile();
        nonOpStatementFile.StatementType = statementType;
        nonOpStatementFile.ExtractDateTime = DateTime.Now;
        nonOpStatementFile.FullFilePath = _TP_InputFullFilePath;
        nonOpStatementFile.StatementFileStatusID = Bus.StatementFileStatus.Loaded.ToInt32();
        nonOpStatementFile.OperatorID = _TP_OperatorId;
        nonOpStatementFile.CreateDateTime = nonOpStatementFile.ExtractDateTime;
        nonOpStatementFile.CreatedBy = g.SystemInfo.DomainAndUser;

        Type dbObjectType = typeof(NonOpProductionDetail);
        List<PropertyInfo> piSet = dbObjectType.GetIsDbColumnProperties();

        foreach (var wsKvp in wb)
        {
          var wsName = wsKvp.Key;
          var ws = wsKvp.Value;
          ws.PopulateDxCellsArray();

          foreach(var row in ws.Values)
          {
            row.PopulateNamedCellsArray();
            var nonOpProductionDetail = new NonOpProductionDetail();

            foreach (var pi in piSet)
            {
              propertyName = pi.Name;
              pi.SetValue(nonOpProductionDetail, row.CellValue(pi.Name, pi.PropertyType));
            }

            nonOpStatementFile.Statements.Add(nonOpProductionDetail);
          }
        }
        return nonOpStatementFile;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to extract statement data from the Excel file - property being populated is '" + propertyName + "'.", ex);
      }
    }
  }
}
