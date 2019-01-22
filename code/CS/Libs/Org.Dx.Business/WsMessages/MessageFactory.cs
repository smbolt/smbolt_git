using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Org.WSO;
using Org.WSO.Transactions;
using Org.GS;

namespace Org.Dx.Business
{
  [Export(typeof(IMessageFactory))]
  [ExportMetadata("Name", "Org.Dx.Business.MessageFactory")]
  [ExportMetadata("Version", "1.0.0.0")]
  [ExportMetadata("Transactions",
                  "ExcelExtract_1.0.0.0 " +
                  "PdfExtract_1.0.0.0 " +
                  "FileCompare_1.0.0.0 " +
                  "MappedFileRegressionTester"
                 )]
  public class MessageFactory : MessageFactoryBase, IMessageFactory
  {
    public MessageFactory() { }

    public WsMessage CreateRequestMessage(WsParms wsParms)
    {
      try
      {
        WsMessage requestMessage = base.InitWsMessage(wsParms);
        TransactionBase trans = null;

        switch (wsParms.TransactionName)
        {
          case "ExcelExtract":
            trans = Build_ExcelExtract(wsParms);
            break;

          case "PdfExtract":
            trans = Build_PdfExtract(wsParms);
            break;

          case "FileCompare":
            trans = Build_FileCompare(wsParms);
            break;

          case "GetMap":
            trans = Build_GetMap(wsParms);
            break;

          default:
            trans = base.CreateTransaction(wsParms);
            break;
        }

        if (trans == null)
          throw new Exception("MessageFactory is not able to create web service request message for transaction '" + wsParms.TransactionName + "'.");

        trans.Name = wsParms.TransactionName;
        trans.Version = wsParms.TransactionVersion;
        requestMessage.TransactionBody = this.ObjectFactory.Serialize(trans);
        return requestMessage;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to create the request message.", ex);
      }
    }

    private TransactionBase Build_ExcelExtract(WsParms wsParms)
    {
      var trans = new ExcelExtractRequest();

      var fullPathParm = wsParms.ParmSet.Where(p => p.ParameterName == "FullPath").FirstOrDefault();
      if (fullPathParm != null)
        trans.FullPath = fullPathParm.ParameterValue.ToString();

      var mapNameParm = wsParms.ParmSet.Where(p => p.ParameterName == "MapName").FirstOrDefault();
      if (mapNameParm != null)
        trans.MapName = mapNameParm.ParameterValue.ToString();

      var fileExtractModeParm = wsParms.ParmSet.Where(p => p.ParameterName == "FileExtractMode").FirstOrDefault();
      if (fileExtractModeParm == null)
        throw new Exception("The FileExtractMode parameter is required for building the ExcelExtractRequest transaction.");

      trans.FileExtractMode = g.ToEnum<FileExtractMode>(fileExtractModeParm.ParameterValue.ToString());

      if (fileExtractModeParm.ParameterValue.ToString() == "WriteExtractedDxWorkbookToFile")
      {
        var fullMapPathParm = wsParms.ParmSet.Where(p => p.ParameterName == "FullMapPath").FirstOrDefault();
        if (fullMapPathParm == null)
          throw new Exception("The FullMapPath parameter is required for the ExcelExtractRequest when writing the DxWorkbook file to disk.");
        trans.FullMapPath = fullMapPathParm.ParameterValue.ToString();
      }

      var fileNamePrefixParm = wsParms.ParmSet.Where(p => p.ParameterName == "FileNamePrefix").FirstOrDefault();
      if (fileNamePrefixParm == null)
        throw new Exception("The FileNamePrefix parameter is required for building the ExcelExtractRequest transaction.");
      trans.FileNamePrefix = fileNamePrefixParm.ParameterValue.ToString();

      return trans;
    }

    private TransactionBase Build_PdfExtract(WsParms wsParms)
    {
      var trans = new PdfExtractRequest();

      var fullPathParm = wsParms.ParmSet.Where(p => p.ParameterName == "FullPath").FirstOrDefault();
      if (fullPathParm != null)
        trans.FullPath = fullPathParm.ParameterValue.ToString();


      var mapNameParm = wsParms.ParmSet.Where(p => p.ParameterName == "MapName").FirstOrDefault();
      if (mapNameParm != null)
        trans.MapName = mapNameParm.ParameterValue.ToString();

      var fileExtractModeParm = wsParms.ParmSet.Where(p => p.ParameterName == "FileExtractMode").FirstOrDefault();
      if (fileExtractModeParm == null)
        throw new Exception("The FileExtractMode parameter is required for building the PdfExtractRequest transaction.");

      trans.FileExtractMode = g.ToEnum<FileExtractMode>(fileExtractModeParm.ParameterValue.ToString());

      if (fileExtractModeParm.ParameterValue.ToString() == "WriteExtractedDxWorkbookToFile")
      {
        var fullMapPathParm = wsParms.ParmSet.Where(p => p.ParameterName == "FullMapPath").FirstOrDefault();
        if (fullMapPathParm == null)
          throw new Exception("The FullMapPath parameter is required for the PdfExtractRequest when writing the DxWorkbook file to disk.");
        trans.FullMapPath = fullMapPathParm.ParameterValue.ToString();
      }

      var fileNamePrefixParm = wsParms.ParmSet.Where(p => p.ParameterName == "FileNamePrefix").FirstOrDefault();
      if (fileNamePrefixParm == null)
        throw new Exception("The FileNamePrefix parameter is required for building the PdfExtractRequest transaction.");
      trans.FileNamePrefix = fileNamePrefixParm.ParameterValue.ToString();

      return trans;
    }

    private TransactionBase Build_FileCompare(WsParms wsParms)
    {
      var trans = new FileCompareRequest();

      var basePathParm = wsParms.ParmSet.Where(p => p.ParameterName == "BaseFilePath").FirstOrDefault();
      if (basePathParm != null)
        trans.BaseFilePath = basePathParm.ParameterValue.ToString();

      var comparePathParm = wsParms.ParmSet.Where( p=> p.ParameterName == "CompareFilePath").FirstOrDefault();
      if (comparePathParm != null)
        trans.CompareFilePath = comparePathParm.ParameterValue.ToString();

      var scriptFileParm = wsParms.ParmSet.Where(p => p.ParameterName == "ScriptFilePath").FirstOrDefault();
      if (scriptFileParm != null)
        trans.ScriptFilePath = scriptFileParm.ParameterValue.ToString();

      var reportFileParm = wsParms.ParmSet.Where(p => p.ParameterName == "CompareFilePath").FirstOrDefault();
      if (reportFileParm != null)
      {
        var reportFileParmString = reportFileParm.ParameterValue.ToString();
        var reportFilePath = Path.GetDirectoryName(reportFileParmString);
        var reportFileName = Path.GetFileNameWithoutExtension(reportFileParmString);
        var fullReportPath = reportFilePath + "\\" + reportFileName + ".rpt";
        trans.ReportFilePath = fullReportPath;
      }

      return trans;
    }

    private TransactionBase Build_GetMap(WsParms wsParms)
    {
      var trans = new GetMapRequest();

      var mapNameParm = wsParms.ParmSet.Where(p => p.ParameterName == "MapName").FirstOrDefault();
      if (mapNameParm != null)
        trans.MapName = mapNameParm.ParameterValue.ToString();

      var extractTransParm = wsParms.ParmSet.Where(p => p.ParameterName == "ExtractTransName").FirstOrDefault();
      if (extractTransParm != null)
        trans.ExtractTransName = extractTransParm.ParameterValue.ToString();

      return trans;
    }
  }
}
