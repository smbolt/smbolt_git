using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Org.Software.Business;
using Org.Software.Business.Models;
using Org.WSO;
using Org.WSO.Transactions;
using Org.Software.Tasks.Transactions;
using Org.GS;
using Org.GS.Configuration;

namespace Org.Software.Tasks.Concrete
{
  public class DownloadSoftwareProcessor : RequestProcessorBase, IRequestProcessor
  { 
    private static bool _isMapped = false;
    private ConfigDbSpec _configDbSpec;

    public DownloadSoftwareProcessor()
    {
      if (!_isMapped)
      {
        XmlMapper.AddAssembly(Assembly.GetExecutingAssembly()); 
        _isMapped = true;
      }
    }

    public override XElement ProcessRequest()
    {
      base.Initialize(MethodBase.GetCurrentMethod());
      XmlMapper.AddAssembly(Assembly.GetExecutingAssembly()); 
      base.TransactionEngine.MessageHeader.AddPerfInfoEntry("Start of ProcessRequest");       
      var f = new ObjectFactory2();
      DownloadSoftwareRequest request = f.Deserialize(base.TransactionEngine.TransactionBody) as DownloadSoftwareRequest;
      DownloadSoftwareResponse response = new DownloadSoftwareResponse();
      XElement transactionBody = null; 
      
      try
      {
        string dbSpecPrefix = g.CI("SoftwareDbSpecPrefix");
        _configDbSpec = g.GetDbSpec(dbSpecPrefix); 

        using (var repository = new SoftwareDataRepository(_configDbSpec))
        {
          var moduleVersionForPlatform = repository.GetModuleVersionForPlatform(request.ModuleCode, request.UpgradeVersion, request.UpgradePlatformString);

          if (moduleVersionForPlatform == null)
          {
            response.TransactionStatus = TransactionStatus.Failed;
            response.Message = "Module not found.";
            base.WriteErrorLog("0000", "000"); 
            transactionBody = f.Serialize(response); 
            base.TransactionEngine.MessageHeader.AddPerfInfoEntry("End of ProcessRequest");
            return transactionBody;
          }

          if (moduleVersionForPlatform.ModuleStatus != 1)
          {
            response.TransactionStatus = TransactionStatus.Failed;
            response.Message = "Module is not in active status.";
            base.WriteErrorLog("0000", "000"); 
            transactionBody = f.Serialize(response);
            base.TransactionEngine.MessageHeader.AddPerfInfoEntry("End of ProcessRequest");
            return transactionBody; 
          }

          if (moduleVersionForPlatform.VersionStatus != 1)
          {
            response.TransactionStatus = TransactionStatus.Failed;
            response.Message = "Module version is not in active status.";
            base.WriteErrorLog("0000", "000"); 
            transactionBody = f.Serialize(response);
            base.TransactionEngine.MessageHeader.AddPerfInfoEntry("End of ProcessRequest");
            return transactionBody; 
          }

          if (moduleVersionForPlatform.PlatformStatus != 1)
          {
            response.TransactionStatus = TransactionStatus.Failed;
            response.Message = "Module platform is not in active status.";
            base.WriteErrorLog("0000", "000"); 
            transactionBody = f.Serialize(response);
            base.TransactionEngine.MessageHeader.AddPerfInfoEntry("End of ProcessRequest");
            return transactionBody; 
          }

          if (moduleVersionForPlatform.RepositoryStatus != 1)
          {
            response.TransactionStatus = TransactionStatus.Failed;
            response.Message = "Module repository is not in active status.";
            base.WriteErrorLog("0000", "000"); 
            transactionBody = f.Serialize(response);
            base.TransactionEngine.MessageHeader.AddPerfInfoEntry("End of ProcessRequest");
            return transactionBody; 
          }

          string segmentPath = GetRepositoryFullPath(moduleVersionForPlatform); 

          response.UpgradeVersion = moduleVersionForPlatform.VersionValue; 
          response.UpgradePlatformString = moduleVersionForPlatform.PlatformString;          

          SoftwareSegment softwareSegment = GetSoftwareSegment(segmentPath, request.SegmentNumber);
          response.SegmentNumber = softwareSegment.SegmentNumber;
          response.TotalSegments = softwareSegment.TotalSegments;
          response.RemainingSegments = softwareSegment.TotalSegments - softwareSegment.SegmentNumber;
          response.TotalFileSize = softwareSegment.TotalFileSize;
          response.SegmentSize = softwareSegment.SegmentSize;
          response.SegmentData = softwareSegment.SegmentData;

          switch(request.RequestType)
          {
            case RequestType.InitialRequest:
              response.ResponseType = ResponseType.Ready;

              break;

            case RequestType.GetNextSegment:
              if (softwareSegment.ErrorCode == 1)
              {
                response.TransactionStatus = TransactionStatus.Failed;
                response.Message = "Segment file '" + request.SegmentNumber.ToString() + 
                                   " could not be located.";
                base.WriteErrorLog("0000", "000");
                transactionBody = f.Serialize(response);
                base.TransactionEngine.MessageHeader.AddPerfInfoEntry("End of ProcessRequest");
                return transactionBody; 
              }

              response.ResponseType = ResponseType.SegmentReturned;
              break;

            case RequestType.CancelDownload:
              break;

            case RequestType.DownloadComplete:
              break;
          }


          response.TransactionStatus = TransactionStatus.Success;
        }

        base.WriteSuccessLog("0000", "000");
        transactionBody = f.Serialize(response);
        base.TransactionEngine.MessageHeader.AddPerfInfoEntry("End of ProcessRequest");
        return transactionBody;
      }
      catch (Exception ex)
      {
        base.WriteErrorLog("0000", "000");

        var errorResponse = new ErrorResponse();
        errorResponse.TransactionStatus = TransactionStatus.Error;
        errorResponse.Message = "An exception occurred processing the DownloadSoftware web service request.";
        errorResponse.Exception = ex;
        transactionBody = f.Serialize(errorResponse);
        base.TransactionEngine.MessageHeader.AddPerfInfoEntry("End of ProcessRequest"); 
        return transactionBody;
      }
    }

    private SoftwareSegment GetSoftwareSegment(string segmentPath, int segmentNumber)
    {
      var seg = new SoftwareSegment();
      seg.SegmentNumber = segmentNumber; 

      List<string> segmentFiles = Directory.GetFiles(segmentPath + @"\segments", "*.seg").ToList();
      seg.TotalSegments = segmentFiles.Count;

      int totalFileSize = 0;
      foreach (var segmentFile in segmentFiles)
      {
        FileInfo fi = new FileInfo(segmentFile);
        totalFileSize += (int) fi.Length;
      }

      seg.TotalFileSize = totalFileSize;

      if (segmentNumber == 0)
        return seg; 

      string segmentSearch = "(seg-" + segmentNumber.ToString("000") + "-of-" +
                                       seg.TotalSegments.ToString("000") + ")";

      string segmentFileName = segmentFiles.Where(x => x.Contains(segmentSearch)).FirstOrDefault();

      if (segmentFileName.IsBlank())
      {
        seg.ErrorCode = 1;
        return seg; 
      }

      seg.SegmentData = File.ReadAllText(segmentFileName);
      seg.SegmentSize = seg.SegmentData.Length;

      return seg;
    }

    private string GetRepositoryFullPath(ModuleVersionForPlatform moduleVersion)
    {
      int[] versionTokens = moduleVersion.VersionValue.ToTokenArrayInt32(Constants.PeriodDelimiter);

      return moduleVersion.RepositoryRoot.RemoveTrailingSlash() + @"\" +
             moduleVersion.ModuleName + @"\" +
             versionTokens[0].ToString() + "." + versionTokens[1].ToString() + "." +
             versionTokens[2].ToString() + "." + versionTokens[3].ToString() + @"\" +
             moduleVersion.PlatformString; 
    }

    ~DownloadSoftwareProcessor()
    {
      Dispose(false); 
    }    
  }
}
