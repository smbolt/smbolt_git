using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Org.Software.Tasks.Transactions;
using Org.Software.Business;
using Org.WSO;
using Org.WSO.Transactions;
using Org.GS.Configuration;
using Org.GS;

namespace Org.Software.Tasks.Concrete
{
  public class GetFrameworkVersionsProcessor : RequestProcessorBase, IRequestProcessor, IDisposable
  {
    public static bool _isMapped = false;
    public ConfigDbSpec _configDbSpec;

    public GetFrameworkVersionsProcessor()
    {
      g.LogToMemory("GetFrameworkVersionsProcessor Created");
      if (!_isMapped)
      {
        if (XmlMapper.AddAssembly(Assembly.GetExecutingAssembly()))
          g.LogToMemory("GetFrameworkVersionsProcessor MappingTypes");
        _isMapped = true;
      }
    }

    public override XElement ProcessRequest()
    {
      base.Initialize(MethodBase.GetCurrentMethod());
      XmlMapper.AddAssembly(Assembly.GetExecutingAssembly());
      base.TransactionEngine.MessageHeader.AddPerfInfoEntry("Start of ProcessRequest");
      var f = new ObjectFactory2();
      GetFrameworkVersionsRequest request = f.Deserialize(base.TransactionEngine.TransactionBody) as GetFrameworkVersionsRequest;
      GetFrameworkVersionsResponse response = new GetFrameworkVersionsResponse();

      XElement transactionBody = null;

      try
      {
        string dbSpecPrefix = g.CI("SoftwareDbSpecPrefix");
        _configDbSpec = g.GetDbSpec(dbSpecPrefix);

        using (var repository = new SoftwareDataRepository(_configDbSpec))
        {
          var frameworkVersions = repository.GetFrameworkVersions();
          response.FxVersionSet = new FxVersionSet();
          foreach (var frameworkVersion in frameworkVersions)
          {
            var fxVersion = new FxVersion();
            fxVersion.FrameworkVersionId = frameworkVersion.FrameworkVersionId;
            fxVersion.FrameworkVersionString = frameworkVersion.FrameworkVersionString;
            fxVersion.Version = frameworkVersion.Version;
            fxVersion.VersionNum = frameworkVersion.VersionNum;
            fxVersion.ServicePackString = frameworkVersion.ServicePackString;

            if (!response.FxVersionSet.ContainsKey(fxVersion.FxVersionKey))
              response.FxVersionSet.Add(fxVersion.FxVersionKey, fxVersion);
          }

          response.TransactionStatus = TransactionStatus.Success;
        }
      }
      catch (Exception ex)
      {
        base.WriteErrorLog("0000", "000");
        var errorResponse = new ErrorResponse();
        errorResponse.TransactionStatus = TransactionStatus.Error;
        errorResponse.Message = "An exception occurred processing the GetFrameworkVersions web service request.";
        errorResponse.Exception = ex;
        transactionBody = f.Serialize(errorResponse);
        base.TransactionEngine.MessageHeader.AddPerfInfoEntry("End of ProcessRequest");
        return transactionBody;
      }

      response.TransactionStatus = TransactionStatus.Success;
      base.WriteSuccessLog("0000", "000");
      transactionBody = f.Serialize(response);
      return transactionBody;
    }

    ~GetFrameworkVersionsProcessor()
    {
      g.LogToMemory("GetFrameworkVersionsProcessor Destructor");
      Dispose(false);
    }
  }
}