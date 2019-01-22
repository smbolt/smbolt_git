using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.WSO
{
  [Serializable]
  public class RequestProcessorBase : IRequestProcessor, IDisposable
  {
    public virtual int EntityId { get { return 9999; } }
    protected ServiceBase ServiceBase;
    protected TransactionEngine TransactionEngine;
    protected string MethodName;
    protected string ClassName;
    protected string EngineName;
    protected string RequestName;
    private static bool _isBaseMapped = false;

    public RequestProcessorBase()
    {
      g.LogToMemory("RequestProcessorBase Constructor");

      if (!_isBaseMapped)    
      {
        if(XmlMapper.AddAssembly(Assembly.GetExecutingAssembly()))
          g.LogToMemory("RequestProcessorBase MappingTypes");
        _isBaseMapped = true; 
      }
    }

    public void SetBaseAndEngine(ServiceBase serviceBase, TransactionEngine transactionEngine)
    {
      ServiceBase = serviceBase;
      TransactionEngine = transactionEngine;
      MethodName = String.Empty;
      ClassName = String.Empty;
      EngineName = transactionEngine.GetType().Name;
      RequestName = String.Empty;
    }

    protected void Initialize(MethodBase methodBase)
    {
      string methodName = methodBase.Name;
      string className = methodBase.DeclaringType.Name;
      string requestName = className.Replace("_RequestProcessor", String.Empty);
    }

    public virtual XElement ProcessRequest()
    {
      throw new Exception("The class that derives from this class 'RequestProcessorBase' must override the ProcessRequest method."); 
    }

    protected void WriteErrorLog(string parm1, string parm2)
    {
      // transition this to a different logging approach... 
    }

    protected void WriteSuccessLog(string parm1, string parm2)
    {
      // transition this to a different logging approach... 
    }
    
    ~RequestProcessorBase()
    {
      g.LogToMemory("RequestProcessorBase Destructor"); 
      Dispose(false); 
    }

    public void Dispose()
    {
      g.LogToMemory("RequestProcessorBase Disposing"); 
      Dispose(true); 
    }  

    protected virtual void Dispose(bool disposing)
    {
    }
  }
}
